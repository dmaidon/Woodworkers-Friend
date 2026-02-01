' ============================================================================
' Created: January 2026
' Purpose: Manages UserData.db - read-write database for user-modifiable data
' Architecture: Part of multi-database split (Help.db, Reference.db, UserData.db)
' Contains: Costs, preferences, calculation history, custom wood species
' ============================================================================

Imports System.Data.SQLite
Imports System.IO

''' &lt;summary&gt;
''' Manages UserData.db database containing user-modifiable data.
''' This is the ONLY database users should backup regularly.
''' Located in DataDir (not Resources) for easy access.
''' &lt;/summary&gt;
Public Class UserDataManager
    Implements IDisposable

#Region "Singleton Pattern"

    Private Shared _instance As UserDataManager
    Private Shared ReadOnly _lock As New Object()

    Public Shared ReadOnly Property Instance As UserDataManager
        Get
            If _instance Is Nothing Then
                SyncLock _lock
                    If _instance Is Nothing Then
                        _instance = New UserDataManager()
                    End If
                End SyncLock
            End If
            Return _instance
        End Get
    End Property

#End Region

#Region "Properties and Fields"

    Private ReadOnly _databasePath As String
    Private ReadOnly _connectionString As String
    Private _disposed As Boolean = False

    Public ReadOnly Property DatabasePath As String
        Get
            Return _databasePath
        End Get
    End Property

    Public ReadOnly Property IsInitialized As Boolean
        Get
            Return File.Exists(_databasePath)
        End Get
    End Property

#End Region

#Region "Constructor"

    Private Sub New()
        ' Database location: %AppData%\WoodworkersFriend\UserData.db (in DataDir, NOT Resources)
        _databasePath = Path.Combine(Globals.DataDir, "UserData.db")
        _connectionString = $"Data Source={_databasePath};Version=3;"

        InitializeDatabase()
    End Sub

#End Region

#Region "Initialization"

    Private Sub InitializeDatabase()
        Try
            If Not File.Exists(_databasePath) Then
                CreateDatabaseSchema()
                ErrorHandler.LogError(New Exception("UserData.db created successfully"), "UserDataManager.InitializeDatabase")
            Else
                VerifyDatabaseSchema()
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "UserDataManager.InitializeDatabase")
        End Try
    End Sub

    Private Sub CreateDatabaseSchema()
        Using conn As New SQLiteConnection(_connectionString)
            conn.Open()

            Using cmd As New SQLiteCommand("
                -- Wood Costs Table
                CREATE TABLE IF NOT EXISTS WoodCosts (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    WoodName TEXT NOT NULL,
                    Thickness TEXT NOT NULL,
                    CostPerBoardFoot REAL NOT NULL,
                    IsUserAdded INTEGER DEFAULT 0,
                    IsActive INTEGER DEFAULT 1,
                    DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
                    LastModified DATETIME DEFAULT CURRENT_TIMESTAMP,
                    UNIQUE(WoodName, Thickness)
                );

                CREATE INDEX IF NOT EXISTS idx_wood_costs_name ON WoodCosts(WoodName);
                CREATE INDEX IF NOT EXISTS idx_wood_costs_active ON WoodCosts(IsActive);

                -- Epoxy Costs Table
                CREATE TABLE IF NOT EXISTS EpoxyCosts (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Brand TEXT NOT NULL,
                    Type TEXT NOT NULL,
                    CostPerGallon REAL NOT NULL,
                    IsUserAdded INTEGER DEFAULT 0,
                    IsActive INTEGER DEFAULT 1,
                    DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
                    LastModified DATETIME DEFAULT CURRENT_TIMESTAMP,
                    UNIQUE(Brand, Type)
                );

                CREATE INDEX IF NOT EXISTS idx_epoxy_costs_brand ON EpoxyCosts(Brand);
                CREATE INDEX IF NOT EXISTS idx_epoxy_costs_active ON EpoxyCosts(IsActive);

                -- User Preferences Table
                CREATE TABLE IF NOT EXISTS Preferences (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Key TEXT NOT NULL UNIQUE,
                    Value TEXT NOT NULL,
                    DataType TEXT DEFAULT 'String',
                    Category TEXT,
                    LastModified DATETIME DEFAULT CURRENT_TIMESTAMP
                );

                CREATE INDEX IF NOT EXISTS idx_preferences_key ON Preferences(Key);
                CREATE INDEX IF NOT EXISTS idx_preferences_category ON Preferences(Category);

                -- Calculation History Table
                CREATE TABLE IF NOT EXISTS CalculationHistory (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    CalculatorType TEXT NOT NULL,
                    InputData TEXT NOT NULL,
                    ResultData TEXT NOT NULL,
                    TimesRun INTEGER DEFAULT 1,
                    DateCreated DATETIME DEFAULT CURRENT_TIMESTAMP,
                    LastRun DATETIME DEFAULT CURRENT_TIMESTAMP
                );

                CREATE INDEX IF NOT EXISTS idx_calc_history_type ON CalculationHistory(CalculatorType);
                CREATE INDEX IF NOT EXISTS idx_calc_history_lastrun ON CalculationHistory(LastRun);

                -- Custom Wood Species Table (user-added species)
                CREATE TABLE IF NOT EXISTS CustomWoodSpecies (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    CommonName TEXT NOT NULL UNIQUE,
                    ScientificName TEXT,
                    WoodType TEXT NOT NULL,
                    JankaHardness INTEGER,
                    SpecificGravity REAL,
                    Density REAL,
                    MoistureContent REAL,
                    ShrinkageRadial REAL,
                    ShrinkageTangential REAL,
                    TypicalUses TEXT,
                    Workability TEXT,
                    Cautions TEXT,
                    Notes TEXT,
                    DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP
                );

                CREATE INDEX IF NOT EXISTS idx_custom_wood_name ON CustomWoodSpecies(CommonName);

                -- Project Notes Table (future feature)
                CREATE TABLE IF NOT EXISTS ProjectNotes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Title TEXT NOT NULL,
                    Content TEXT,
                    Tags TEXT,
                    DateCreated DATETIME DEFAULT CURRENT_TIMESTAMP,
                    LastModified DATETIME DEFAULT CURRENT_TIMESTAMP
                );

                -- Database Version
                CREATE TABLE IF NOT EXISTS DatabaseVersion (
                    Version TEXT NOT NULL,
                    UpdatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
                );

                INSERT INTO DatabaseVersion (Version) VALUES ('1.0');
            ", conn)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub VerifyDatabaseSchema()
        Try
            Using conn As New SQLiteConnection(_connectionString)
                conn.Open()
                Using cmd As New SQLiteCommand("
                    SELECT COUNT(*) FROM sqlite_master
                    WHERE type='table' AND name IN ('WoodCosts', 'EpoxyCosts', 'Preferences', 'CalculationHistory')
                ", conn)
                    Dim tableCount = Convert.ToInt32(cmd.ExecuteScalar())
                    If tableCount < 4 Then
                        ErrorHandler.LogError(New Exception("UserData.db schema invalid - needs recreation"), "VerifyDatabaseSchema")
                    End If
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "VerifyDatabaseSchema")
        End Try
    End Sub

#End Region

#Region "Connection Management"

    Private Function GetConnection() As SQLiteConnection
        Return New SQLiteConnection(_connectionString)
    End Function

#End Region

#Region "Public API - Preferences"

    Public Function GetPreference(key As String) As String
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT Value FROM Preferences WHERE Key = @Key", conn)
                    cmd.Parameters.AddWithValue("@Key", key)
                    Dim result = cmd.ExecuteScalar()
                    If result IsNot Nothing Then
                        Return result.ToString()
                    End If
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, $"GetPreference - {key}")
        End Try

        Return Nothing
    End Function

    Public Function SavePreference(key As String, value As String, dataType As String, category As String) As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    INSERT OR REPLACE INTO Preferences (Key, Value, DataType, Category, LastModified)
                    VALUES (@Key, @Value, @DataType, @Category, CURRENT_TIMESTAMP)
                ", conn)
                    cmd.Parameters.AddWithValue("@Key", key)
                    cmd.Parameters.AddWithValue("@Value", value)
                    cmd.Parameters.AddWithValue("@DataType", dataType)
                    cmd.Parameters.AddWithValue("@Category", If(String.IsNullOrEmpty(category), CObj(DBNull.Value), CObj(category)))
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, $"SavePreference - {key}")
            Return False
        End Try
    End Function

    Public Function GetAllPreferences() As Dictionary(Of String, String)
        Dim results As New Dictionary(Of String, String)

        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT Key, Value FROM Preferences", conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            results(reader.GetString(0)) = reader.GetString(1)
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetAllPreferences")
        End Try

        Return results
    End Function

    Public Function HasPreferences() As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT COUNT(*) FROM Preferences", conn)
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "HasPreferences")
            Return False
        End Try
    End Function

#End Region

#Region "Public API - Calculation History"

    Public Function SaveCalculation(calculatorType As String, inputData As String, resultData As String) As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()

                ' Check if calculation already exists (same input)
                Using checkCmd As New SQLiteCommand("
                    SELECT Id, TimesRun FROM CalculationHistory
                    WHERE CalculatorType = @Type AND InputData = @Input
                ", conn)
                    checkCmd.Parameters.AddWithValue("@Type", calculatorType)
                    checkCmd.Parameters.AddWithValue("@Input", inputData)

                    Using reader = checkCmd.ExecuteReader()
                        If reader.Read() Then
                            ' Update existing: increment TimesRun, update LastRun and ResultData
                            Dim existingId = reader.GetInt32(0)
                            Dim timesRun = reader.GetInt32(1)
                            reader.Close()

                            Using updateCmd As New SQLiteCommand("
                                UPDATE CalculationHistory
                                SET ResultData = @Result, TimesRun = @TimesRun, LastRun = CURRENT_TIMESTAMP
                                WHERE Id = @Id
                            ", conn)
                                updateCmd.Parameters.AddWithValue("@Result", resultData)
                                updateCmd.Parameters.AddWithValue("@TimesRun", timesRun + 1)
                                updateCmd.Parameters.AddWithValue("@Id", existingId)
                                updateCmd.ExecuteNonQuery()
                            End Using
                        Else
                            reader.Close()

                            ' Insert new calculation
                            Using insertCmd As New SQLiteCommand("
                                INSERT INTO CalculationHistory (CalculatorType, InputData, ResultData)
                                VALUES (@Type, @Input, @Result)
                            ", conn)
                                insertCmd.Parameters.AddWithValue("@Type", calculatorType)
                                insertCmd.Parameters.AddWithValue("@Input", inputData)
                                insertCmd.Parameters.AddWithValue("@Result", resultData)
                                insertCmd.ExecuteNonQuery()
                            End Using
                        End If
                    End Using
                End Using

                Return True
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, $"SaveCalculation - {calculatorType}")
            Return False
        End Try
    End Function

    Public Function GetTopCalculations(calculatorType As String, count As Integer) As List(Of CalculationHistoryRecord)
        Dim results As New List(Of CalculationHistoryRecord)

        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    SELECT Id, CalculatorType, InputData, ResultData, TimesRun, LastRun
                    FROM CalculationHistory
                    WHERE CalculatorType = @Type
                    ORDER BY TimesRun DESC, LastRun DESC
                    LIMIT @Count
                ", conn)
                    cmd.Parameters.AddWithValue("@Type", calculatorType)
                    cmd.Parameters.AddWithValue("@Count", count)

                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            results.Add(New CalculationHistoryRecord With {
                                .Id = reader.GetInt32(0),
                                .CalculatorType = reader.GetString(1),
                                .InputData = reader.GetString(2),
                                .ResultData = reader.GetString(3),
                                .TimesRun = reader.GetInt32(4),
                                .LastRun = reader.GetDateTime(5)
                            })
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, $"GetTopCalculations - {calculatorType}")
        End Try

        Return results
    End Function

#End Region

#Region "Public API - Custom Wood Species"

    Public Function GetCustomWoodSpecies() As List(Of WoodSpecies)
        Dim results As New List(Of WoodSpecies)

        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    SELECT Id, CommonName, ScientificName, WoodType, JankaHardness, SpecificGravity, Density,
                           MoistureContent, ShrinkageRadial, ShrinkageTangential, TypicalUses, Workability, Cautions, Notes
                    FROM CustomWoodSpecies
                    ORDER BY CommonName
                ", conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            results.Add(New WoodSpecies With {
                                .CommonName = reader.GetString(1),
                                .ScientificName = If(reader.IsDBNull(2), String.Empty, reader.GetString(2)),
                                .WoodType = reader.GetString(3),
                                .JankaHardness = If(reader.IsDBNull(4), 0, reader.GetInt32(4)),
                                .SpecificGravity = If(reader.IsDBNull(5), 0.0, reader.GetDouble(5)),
                                .Density = If(reader.IsDBNull(6), 0.0, reader.GetDouble(6)),
                                .MoistureContent = If(reader.IsDBNull(7), 0.0, reader.GetDouble(7)),
                                .ShrinkageRadial = If(reader.IsDBNull(8), 0.0, reader.GetDouble(8)),
                                .ShrinkageTangential = If(reader.IsDBNull(9), 0.0, reader.GetDouble(9)),
                                .TypicalUses = If(reader.IsDBNull(10), String.Empty, reader.GetString(10)),
                                .Workability = If(reader.IsDBNull(11), String.Empty, reader.GetString(11)),
                                .Cautions = If(reader.IsDBNull(12), String.Empty, reader.GetString(12)),
                                .Notes = If(reader.IsDBNull(13), String.Empty, reader.GetString(13))
                            })
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetCustomWoodSpecies")
        End Try

        Return results
    End Function

    Public Function AddCustomWoodSpecies(species As WoodSpecies) As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    INSERT INTO CustomWoodSpecies
                    (CommonName, ScientificName, WoodType, JankaHardness, SpecificGravity, Density,
                     MoistureContent, ShrinkageRadial, ShrinkageTangential, TypicalUses, Workability, Cautions, Notes)
                    VALUES (@CommonName, @ScientificName, @WoodType, @JankaHardness, @SpecificGravity, @Density,
                            @MoistureContent, @ShrinkageRadial, @ShrinkageTangential, @TypicalUses, @Workability, @Cautions, @Notes)
                ", conn)
                    cmd.Parameters.AddWithValue("@CommonName", species.CommonName)
                    cmd.Parameters.AddWithValue("@ScientificName", If(String.IsNullOrEmpty(species.ScientificName), CObj(DBNull.Value), CObj(species.ScientificName)))
                    cmd.Parameters.AddWithValue("@WoodType", species.WoodType)
                    cmd.Parameters.AddWithValue("@JankaHardness", species.JankaHardness)
                    cmd.Parameters.AddWithValue("@SpecificGravity", species.SpecificGravity)
                    cmd.Parameters.AddWithValue("@Density", species.Density)
                    cmd.Parameters.AddWithValue("@MoistureContent", species.MoistureContent)
                    cmd.Parameters.AddWithValue("@ShrinkageRadial", species.ShrinkageRadial)
                    cmd.Parameters.AddWithValue("@ShrinkageTangential", species.ShrinkageTangential)
                    cmd.Parameters.AddWithValue("@TypicalUses", If(String.IsNullOrEmpty(species.TypicalUses), CObj(DBNull.Value), CObj(species.TypicalUses)))
                    cmd.Parameters.AddWithValue("@Workability", If(String.IsNullOrEmpty(species.Workability), CObj(DBNull.Value), CObj(species.Workability)))
                    cmd.Parameters.AddWithValue("@Cautions", If(String.IsNullOrEmpty(species.Cautions), CObj(DBNull.Value), CObj(species.Cautions)))
                    cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(species.Notes), CObj(DBNull.Value), CObj(species.Notes)))
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, $"AddCustomWoodSpecies - {species.CommonName}")
            Return False
        End Try
    End Function

#End Region

#Region "Utility Methods"

    Public Function IsWoodCostsSeeded() As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT COUNT(*) FROM WoodCosts WHERE IsActive = 1", conn)
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "IsWoodCostsSeeded")
            Return False
        End Try
    End Function

    Public Function IsEpoxyCostsSeeded() As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT COUNT(*) FROM EpoxyCosts WHERE IsActive = 1", conn)
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "IsEpoxyCostsSeeded")
            Return False
        End Try
    End Function

#End Region

#Region "Bulk Insert Operations (for migration)"

    Friend Function BulkInsertWoodCosts(costs As List(Of WoodCost)) As Integer
        Dim successCount = 0

        Try
            Using conn = GetConnection()
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        For Each cost In costs
                            Using cmd As New SQLiteCommand("
                                INSERT OR REPLACE INTO WoodCosts
                                (WoodName, Thickness, CostPerBoardFoot, IsUserAdded, IsActive)
                                VALUES (@WoodName, @Thickness, @CostPerBoardFoot, @IsUserAdded, @IsActive)
                            ", conn, transaction)
                                cmd.Parameters.AddWithValue("@WoodName", cost.WoodName)
                                cmd.Parameters.AddWithValue("@Thickness", cost.Thickness)
                                cmd.Parameters.AddWithValue("@CostPerBoardFoot", cost.CostPerBoardFoot)
                                cmd.Parameters.AddWithValue("@IsUserAdded", If(cost.IsUserAdded, 1, 0))
                                cmd.Parameters.AddWithValue("@IsActive", If(cost.Active, 1, 0))
                                cmd.ExecuteNonQuery()
                                successCount += 1
                            End Using
                        Next
                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        ErrorHandler.LogError(ex, "BulkInsertWoodCosts - Transaction failed")
                        Return 0
                    End Try
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BulkInsertWoodCosts")
        End Try

        Return successCount
    End Function

    Friend Function BulkInsertEpoxyCosts(costs As List(Of EpoxyCost)) As Integer
        Dim successCount = 0

        Try
            Using conn = GetConnection()
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        For Each cost In costs
                            Using cmd As New SQLiteCommand("
                                INSERT OR REPLACE INTO EpoxyCosts
                                (Brand, Type, CostPerGallon, IsUserAdded, IsActive)
                                VALUES (@Brand, @Type, @CostPerGallon, @IsUserAdded, @IsActive)
                            ", conn, transaction)
                                cmd.Parameters.AddWithValue("@Brand", cost.Brand)
                                cmd.Parameters.AddWithValue("@Type", cost.Type)
                                cmd.Parameters.AddWithValue("@CostPerGallon", cost.CostPerGallon)
                                cmd.Parameters.AddWithValue("@IsUserAdded", If(cost.IsUserAdded, 1, 0))
                                cmd.Parameters.AddWithValue("@IsActive", If(cost.Active, 1, 0))
                                cmd.ExecuteNonQuery()
                                successCount += 1
                            End Using
                        Next
                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        ErrorHandler.LogError(ex, "BulkInsertEpoxyCosts - Transaction failed")
                        Return 0
                    End Try
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BulkInsertEpoxyCosts")
        End Try

        Return successCount
    End Function

#End Region

#Region "Cost Data - Public API (Uses CostDataModels.vb classes)"

    ''' <summary>
    ''' Gets all wood costs from UserData.db
    ''' </summary>
    Public Function GetAllWoodCosts() As List(Of WoodCost)
        ' TODO: Implement actual database read
        ' For now, return empty list to fix compilation
        Return New List(Of WoodCost)()
    End Function

    ''' <summary>
    ''' Adds wood cost to UserData.db
    ''' </summary>
    Public Function AddWoodCost(cost As WoodCost) As Boolean
        ' TODO: Implement actual database write
        Return True
    End Function

    ''' <summary>
    ''' Updates wood cost in UserData.db
    ''' </summary>
    Public Function UpdateWoodCost(cost As WoodCost) As Boolean
        ' TODO: Implement actual database update
        Return True
    End Function

    ''' <summary>
    ''' Soft deletes wood cost (sets Active=False)
    ''' </summary>
    Public Function DeleteWoodCost(woodCostID As Integer) As Boolean
        ' TODO: Implement actual database soft delete
        Return True
    End Function

    ''' <summary>
    ''' Gets all epoxy costs from UserData.db
    ''' </summary>
    Public Function GetAllEpoxyCosts() As List(Of EpoxyCost)
        ' TODO: Implement actual database read
        Return New List(Of EpoxyCost)()
    End Function

    ''' <summary>
    ''' Adds epoxy cost to UserData.db
    ''' </summary>
    Public Function AddEpoxyCost(cost As EpoxyCost) As Boolean
        ' TODO: Implement actual database write
        Return True
    End Function

    ''' <summary>
    ''' Updates epoxy cost in UserData.db
    ''' </summary>
    Public Function UpdateEpoxyCost(cost As EpoxyCost) As Boolean
        ' TODO: Implement actual database update
        Return True
    End Function

    ''' <summary>
    ''' Soft deletes epoxy cost (sets Active=False)
    ''' </summary>
    Public Function DeleteEpoxyCost(epoxyCostID As Integer) As Boolean
        ' TODO: Implement actual database soft delete
        Return True
    End Function

#End Region

#Region "Preferences - Extended API"

    ''' <summary>
    ''' Gets a preference with optional default value
    ''' </summary>
    Public Function GetPreference(key As String, defaultValue As String) As String
        Dim result = GetPreference(key)
        Return If(String.IsNullOrEmpty(result), defaultValue, result)
    End Function

    ''' <summary>
    ''' Gets TimesRun counter
    ''' </summary>
    Public Function GetTimesRun() As Integer
        Dim value = GetPreference("TimesRun")
        Dim result As Integer
        If Integer.TryParse(value, result) Then
            Return result
        End If
        Return 0
    End Function

    ''' <summary>
    ''' Increments and saves TimesRun counter
    ''' </summary>
    Public Function IncrementTimesRun() As Integer
        Dim current = GetTimesRun()
        current += 1
        SavePreference("TimesRun", current.ToString(), "Integer", "System")
        Return current
    End Function

#End Region

#Region "Calculation History - Public API"

    ''' <summary>
    ''' Saves a calculation to history
    ''' </summary>
    Public Shared Function SaveCalculation(calculatorType As String, calculationName As String, inputParameters As String, results As String, Optional notes As String = "") As Integer
        ArgumentNullException.ThrowIfNull(inputParameters)
        ' TODO: Implement actual save
        Return 1
    End Function

    ''' <summary>
    ''' Gets calculation history for a calculator type
    ''' </summary>
    Public Shared Function GetCalculationHistory(calculatorType As String, Optional limit As Integer = 50) As List(Of CalculationHistory)
        ' TODO: Implement actual retrieval
        Return New List(Of CalculationHistory)()
    End Function

    ''' <summary>
    ''' Deletes a calculation from history
    ''' </summary>
    Public Shared Function DeleteCalculation(historyID As Integer) As Boolean
        ' TODO: Implement actual delete
        Return True
    End Function

    ''' <summary>
    ''' Toggles favorite status
    ''' </summary>
    Public Shared Function ToggleFavorite(historyID As Integer) As Boolean
        ' TODO: Implement actual toggle
        Return True
    End Function

    ''' <summary>
    ''' Updates calculation name and notes
    ''' </summary>
    Public Shared Function UpdateCalculation(historyID As Integer, calculationName As String, notes As String) As Boolean
        ' TODO: Implement actual update
        Return True
    End Function

#End Region

#Region "IDisposable Support"

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not _disposed Then
            If disposing Then
                ' Cleanup managed resources if needed
            End If
            _disposed = True
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

#End Region

End Class

''' &lt;summary&gt;
''' Calculation history record
''' &lt;/summary&gt;
Public Class CalculationHistoryRecord
    Public Property Id As Integer
    Public Property CalculatorType As String
    Public Property InputData As String
    Public Property ResultData As String
    Public Property TimesRun As Integer
    Public Property LastRun As DateTime
End Class

' Note: WoodCost and EpoxyCost models are defined in CostDataModels.vb
' Those are the official models with all properties (Active, DisplayName, WoodCostID, etc.)
