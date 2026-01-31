' ============================================================================
' Created: January 2026
' Purpose: Manages Reference.db - isolated read-only database for reference data
' Architecture: Part of multi-database split (Help.db, Reference.db, UserData.db)
' Contains: Wood species, joinery types, hardware standards
' ============================================================================

Imports System.Data.SQLite
Imports System.IO

''' &lt;summary&gt;
''' Manages Reference.db database containing wood species, joinery types, and hardware standards.
''' Read-only access mode for safety. Can be refreshed from app resources.
''' Database is shipped with application and extracted to AppData/Resources.
''' &lt;/summary&gt;
Public Class ReferenceDataManager
    Implements IDisposable

#Region "Singleton Pattern"

    Private Shared _instance As ReferenceDataManager
    Private Shared ReadOnly _lock As New Object()

    Public Shared ReadOnly Property Instance As ReferenceDataManager
        Get
            If _instance Is Nothing Then
                SyncLock _lock
                    If _instance Is Nothing Then
                        _instance = New ReferenceDataManager()
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
        ' Database location: %AppData%\WoodworkersFriend\Resources\Reference.db
        Dim resourcesDir = Path.Combine(Globals.DataDir, "Resources")
        Directory.CreateDirectory(resourcesDir)

        _databasePath = Path.Combine(resourcesDir, "Reference.db")
        _connectionString = $"Data Source={_databasePath};Version=3;Read Only=True;"

        InitializeDatabase()
    End Sub

#End Region

#Region "Initialization"

    Private Sub InitializeDatabase()
        Try
            If Not File.Exists(_databasePath) Then
                CreateDatabaseSchema()
                ErrorHandler.LogError(New Exception("Reference.db created successfully"), "ReferenceDataManager.InitializeDatabase")
            Else
                VerifyDatabaseSchema()
            End If

            SetReadOnlyAttribute()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "ReferenceDataManager.InitializeDatabase")
        End Try
    End Sub

    Private Sub CreateDatabaseSchema()
        Dim createConnectionString = $"Data Source={_databasePath};Version=3;"

        Using conn As New SQLiteConnection(createConnectionString)
            conn.Open()

            Using cmd As New SQLiteCommand("
                -- Wood Species Table
                CREATE TABLE IF NOT EXISTS WoodSpecies (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    CommonName TEXT NOT NULL,
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

                CREATE INDEX IF NOT EXISTS idx_wood_common_name ON WoodSpecies(CommonName);
                CREATE INDEX IF NOT EXISTS idx_wood_type ON WoodSpecies(WoodType);

                -- Joinery Types Table
                CREATE TABLE IF NOT EXISTS JoineryTypes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL UNIQUE,
                    Category TEXT NOT NULL,
                    StrengthRating INTEGER,
                    DifficultyLevel TEXT,
                    Description TEXT,
                    TypicalUses TEXT,
                    RequiredTools TEXT,
                    StrengthCharacteristics TEXT,
                    GlueRequired INTEGER,
                    ReinforcementOptions TEXT,
                    HistoricalNotes TEXT
                );

                CREATE INDEX IF NOT EXISTS idx_joinery_name ON JoineryTypes(Name);
                CREATE INDEX IF NOT EXISTS idx_joinery_category ON JoineryTypes(Category);

                -- Hardware Standards Table
                CREATE TABLE IF NOT EXISTS HardwareStandards (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    Category TEXT NOT NULL,
                    Specifications TEXT,
                    Dimensions TEXT,
                    MountingRequirements TEXT,
                    WeightCapacity TEXT,
                    CommonBrands TEXT,
                    PartNumbers TEXT,
                    Notes TEXT,
                    InstallationTips TEXT
                );

                CREATE INDEX IF NOT EXISTS idx_hardware_name ON HardwareStandards(Name);
                CREATE INDEX IF NOT EXISTS idx_hardware_category ON HardwareStandards(Category);

                -- Version Table
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
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    SELECT COUNT(*) FROM sqlite_master 
                    WHERE type='table' AND name IN ('WoodSpecies', 'JoineryTypes', 'HardwareStandards')
                ", conn)
                    Dim tableCount = Convert.ToInt32(cmd.ExecuteScalar())
                    If tableCount < 3 Then
                        ErrorHandler.LogError(New Exception("Reference.db schema invalid - needs recreation"), "VerifyDatabaseSchema")
                    End If
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "VerifyDatabaseSchema")
        End Try
    End Sub

    Private Sub SetReadOnlyAttribute()
        Try
            If File.Exists(_databasePath) Then
                Dim fileInfo As New FileInfo(_databasePath)
                If Not fileInfo.IsReadOnly Then
                    fileInfo.IsReadOnly = True
                End If
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SetReadOnlyAttribute")
        End Try
    End Sub

    Private Sub RemoveReadOnlyAttribute()
        Try
            If File.Exists(_databasePath) Then
                Dim fileInfo As New FileInfo(_databasePath)
                If fileInfo.IsReadOnly Then
                    fileInfo.IsReadOnly = False
                End If
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "RemoveReadOnlyAttribute")
        End Try
    End Sub

#End Region

#Region "Connection Management"

    Private Function GetReadOnlyConnection() As SQLiteConnection
        Return New SQLiteConnection(_connectionString)
    End Function

    Private Function GetWriteConnection() As SQLiteConnection
        Dim writeConnectionString = $"Data Source={_databasePath};Version=3;"
        Return New SQLiteConnection(writeConnectionString)
    End Function

#End Region

#Region "Public API - Wood Species"

    Public Function GetWoodSpecies(commonName As String) As WoodSpecies
        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    SELECT Id, CommonName, ScientificName, WoodType, JankaHardness, SpecificGravity, Density,
                           MoistureContent, ShrinkageRadial, ShrinkageTangential, TypicalUses, Workability, Cautions, Notes
                    FROM WoodSpecies
                    WHERE CommonName = @CommonName COLLATE NOCASE
                ", conn)
                    cmd.Parameters.AddWithValue("@CommonName", commonName)

                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return CreateWoodSpeciesFromReader(reader)
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, $"GetWoodSpecies - {commonName}")
        End Try

        Return Nothing
    End Function

    Public Function GetAllWoodSpecies() As List(Of WoodSpecies)
        Dim results As New List(Of WoodSpecies)

        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    SELECT Id, CommonName, ScientificName, WoodType, JankaHardness, SpecificGravity, Density,
                           MoistureContent, ShrinkageRadial, ShrinkageTangential, TypicalUses, Workability, Cautions, Notes
                    FROM WoodSpecies
                    ORDER BY CommonName
                ", conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            results.Add(CreateWoodSpeciesFromReader(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetAllWoodSpecies")
        End Try

        Return results
    End Function

    Public Function SearchWoodSpecies(searchTerm As String) As List(Of WoodSpecies)
        Dim results As New List(Of WoodSpecies)

        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    SELECT Id, CommonName, ScientificName, WoodType, JankaHardness, SpecificGravity, Density,
                           MoistureContent, ShrinkageRadial, ShrinkageTangential, TypicalUses, Workability, Cautions, Notes
                    FROM WoodSpecies
                    WHERE CommonName LIKE @Search OR ScientificName LIKE @Search OR TypicalUses LIKE @Search
                    ORDER BY CommonName
                ", conn)
                    cmd.Parameters.AddWithValue("@Search", $"%{searchTerm}%")

                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            results.Add(CreateWoodSpeciesFromReader(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, $"SearchWoodSpecies - {searchTerm}")
        End Try

        Return results
    End Function

    Private Function CreateWoodSpeciesFromReader(reader As SQLiteDataReader) As WoodSpecies
        Return New WoodSpecies With {
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
        }
    End Function

#End Region

#Region "Public API - Joinery Types"

    Public Function GetAllJoineryTypes() As List(Of JoineryType)
        Dim results As New List(Of JoineryType)

        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    SELECT Id, Name, Category, StrengthRating, DifficultyLevel, Description,
                           TypicalUses, RequiredTools, StrengthCharacteristics, GlueRequired,
                           ReinforcementOptions, HistoricalNotes
                    FROM JoineryTypes
                    ORDER BY Name
                ", conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            results.Add(CreateJoineryTypeFromReader(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetAllJoineryTypes")
        End Try

        Return results
    End Function

    Public Function GetJoineryByCategory(category As String) As List(Of JoineryType)
        Dim results As New List(Of JoineryType)

        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    SELECT Id, Name, Category, StrengthRating, DifficultyLevel, Description,
                           TypicalUses, RequiredTools, StrengthCharacteristics, GlueRequired,
                           ReinforcementOptions, HistoricalNotes
                    FROM JoineryTypes
                    WHERE Category = @Category
                    ORDER BY Name
                ", conn)
                    cmd.Parameters.AddWithValue("@Category", category)

                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            results.Add(CreateJoineryTypeFromReader(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, $"GetJoineryByCategory - {category}")
        End Try

        Return results
    End Function

    Private Function CreateJoineryTypeFromReader(reader As SQLiteDataReader) As JoineryType
        Return New JoineryType With {
            .Name = reader.GetString(1),
            .Category = [Enum].Parse(GetType(JoineryCategory), reader.GetString(2)),
            .StrengthRating = If(reader.IsDBNull(3), 0, reader.GetInt32(3)),
            .DifficultyLevel = [Enum].Parse(GetType(JoineryDifficulty), reader.GetString(4)),
            .Description = If(reader.IsDBNull(5), String.Empty, reader.GetString(5)),
            .TypicalUses = If(reader.IsDBNull(6), String.Empty, reader.GetString(6)),
            .RequiredTools = If(reader.IsDBNull(7), String.Empty, reader.GetString(7)),
            .StrengthCharacteristics = If(reader.IsDBNull(8), String.Empty, reader.GetString(8)),
            .GlueRequired = If(reader.IsDBNull(9), False, reader.GetInt32(9) = 1),
            .ReinforcementOptions = If(reader.IsDBNull(10), String.Empty, reader.GetString(10)),
            .HistoricalNotes = If(reader.IsDBNull(11), String.Empty, reader.GetString(11))
        }
    End Function

#End Region

#Region "Public API - Hardware Standards"

    Public Function GetAllHardwareStandards() As List(Of HardwareStandard)
        Dim results As New List(Of HardwareStandard)

        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    SELECT Id, Name, Category, Specifications, Dimensions, MountingRequirements,
                           WeightCapacity, CommonBrands, PartNumbers, Notes, InstallationTips
                    FROM HardwareStandards
                    ORDER BY Category, Name
                ", conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            results.Add(CreateHardwareStandardFromReader(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetAllHardwareStandards")
        End Try

        Return results
    End Function

    Public Function GetHardwareByCategory(category As String) As List(Of HardwareStandard)
        Dim results As New List(Of HardwareStandard)

        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    SELECT Id, Name, Category, Specifications, Dimensions, MountingRequirements,
                           WeightCapacity, CommonBrands, PartNumbers, Notes, InstallationTips
                    FROM HardwareStandards
                    WHERE Category = @Category
                    ORDER BY Name
                ", conn)
                    cmd.Parameters.AddWithValue("@Category", category)

                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            results.Add(CreateHardwareStandardFromReader(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, $"GetHardwareByCategory - {category}")
        End Try

        Return results
    End Function

    Private Function CreateHardwareStandardFromReader(reader As SQLiteDataReader) As HardwareStandard
        Return New HardwareStandard With {
            .Name = reader.GetString(1),
            .Category = reader.GetString(2),
            .Specifications = If(reader.IsDBNull(3), String.Empty, reader.GetString(3)),
            .Dimensions = If(reader.IsDBNull(4), String.Empty, reader.GetString(4)),
            .MountingRequirements = If(reader.IsDBNull(5), String.Empty, reader.GetString(5)),
            .WeightCapacity = If(reader.IsDBNull(6), String.Empty, reader.GetString(6)),
            .CommonBrands = If(reader.IsDBNull(7), String.Empty, reader.GetString(7)),
            .PartNumbers = If(reader.IsDBNull(8), String.Empty, reader.GetString(8)),
            .Notes = If(reader.IsDBNull(9), String.Empty, reader.GetString(9)),
            .InstallationTips = If(reader.IsDBNull(10), String.Empty, reader.GetString(10))
        }
    End Function

#End Region

#Region "Admin Operations - Bulk Insert"

    ''' &lt;summary&gt;
    ''' Bulk inserts wood species (used during initial seed)
    ''' &lt;/summary&gt;
    Friend Function BulkInsertWoodSpecies(species As List(Of WoodSpecies)) As Integer
        Dim successCount = 0

        Try
            RemoveReadOnlyAttribute()

            Using conn = GetWriteConnection()
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        For Each item In species
                            Using cmd As New SQLiteCommand("
                                INSERT OR REPLACE INTO WoodSpecies
                                (CommonName, ScientificName, WoodType, JankaHardness, SpecificGravity, Density,
                                 MoistureContent, ShrinkageRadial, ShrinkageTangential, TypicalUses, Workability, Cautions, Notes)
                                VALUES (@CommonName, @ScientificName, @WoodType, @JankaHardness, @SpecificGravity, @Density,
                                        @MoistureContent, @ShrinkageRadial, @ShrinkageTangential, @TypicalUses, @Workability, @Cautions, @Notes)
                            ", conn, transaction)
                                cmd.Parameters.AddWithValue("@CommonName", item.CommonName)
                                cmd.Parameters.AddWithValue("@ScientificName", If(String.IsNullOrEmpty(item.ScientificName), CObj(DBNull.Value), CObj(item.ScientificName)))
                                cmd.Parameters.AddWithValue("@WoodType", item.WoodType)
                                cmd.Parameters.AddWithValue("@JankaHardness", item.JankaHardness)
                                cmd.Parameters.AddWithValue("@SpecificGravity", item.SpecificGravity)
                                cmd.Parameters.AddWithValue("@Density", item.Density)
                                cmd.Parameters.AddWithValue("@MoistureContent", item.MoistureContent)
                                cmd.Parameters.AddWithValue("@ShrinkageRadial", item.ShrinkageRadial)
                                cmd.Parameters.AddWithValue("@ShrinkageTangential", item.ShrinkageTangential)
                                cmd.Parameters.AddWithValue("@TypicalUses", If(String.IsNullOrEmpty(item.TypicalUses), CObj(DBNull.Value), CObj(item.TypicalUses)))
                                cmd.Parameters.AddWithValue("@Workability", If(String.IsNullOrEmpty(item.Workability), CObj(DBNull.Value), CObj(item.Workability)))
                                cmd.Parameters.AddWithValue("@Cautions", If(String.IsNullOrEmpty(item.Cautions), CObj(DBNull.Value), CObj(item.Cautions)))
                                cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(item.Notes), CObj(DBNull.Value), CObj(item.Notes)))
                                cmd.ExecuteNonQuery()
                                successCount += 1
                            End Using
                        Next
                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        ErrorHandler.LogError(ex, "BulkInsertWoodSpecies - Transaction failed")
                        Return 0
                    End Try
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BulkInsertWoodSpecies")
        Finally
            SetReadOnlyAttribute()
        End Try

        Return successCount
    End Function

    ''' &lt;summary&gt;
    ''' Bulk inserts joinery types (used during initial seed)
    ''' &lt;/summary&gt;
    Friend Function BulkInsertJoineryTypes(joineryList As List(Of JoineryType)) As Integer
        Dim successCount = 0

        Try
            RemoveReadOnlyAttribute()

            Using conn = GetWriteConnection()
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        For Each item In joineryList
                            Using cmd As New SQLiteCommand("
                                INSERT OR REPLACE INTO JoineryTypes
                                (Name, Category, StrengthRating, DifficultyLevel, Description, TypicalUses,
                                 RequiredTools, StrengthCharacteristics, GlueRequired, ReinforcementOptions, HistoricalNotes)
                                VALUES (@Name, @Category, @StrengthRating, @DifficultyLevel, @Description, @TypicalUses,
                                        @RequiredTools, @StrengthCharacteristics, @GlueRequired, @ReinforcementOptions, @HistoricalNotes)
                            ", conn, transaction)
                                cmd.Parameters.AddWithValue("@Name", item.Name)
                                cmd.Parameters.AddWithValue("@Category", item.Category.ToString())
                                cmd.Parameters.AddWithValue("@StrengthRating", item.StrengthRating)
                                cmd.Parameters.AddWithValue("@DifficultyLevel", item.DifficultyLevel.ToString())
                                cmd.Parameters.AddWithValue("@Description", If(String.IsNullOrEmpty(item.Description), CObj(DBNull.Value), CObj(item.Description)))
                                cmd.Parameters.AddWithValue("@TypicalUses", If(String.IsNullOrEmpty(item.TypicalUses), CObj(DBNull.Value), CObj(item.TypicalUses)))
                                cmd.Parameters.AddWithValue("@RequiredTools", If(String.IsNullOrEmpty(item.RequiredTools), CObj(DBNull.Value), CObj(item.RequiredTools)))
                                cmd.Parameters.AddWithValue("@StrengthCharacteristics", If(String.IsNullOrEmpty(item.StrengthCharacteristics), CObj(DBNull.Value), CObj(item.StrengthCharacteristics)))
                                cmd.Parameters.AddWithValue("@GlueRequired", If(item.GlueRequired, 1, 0))
                                cmd.Parameters.AddWithValue("@ReinforcementOptions", If(String.IsNullOrEmpty(item.ReinforcementOptions), CObj(DBNull.Value), CObj(item.ReinforcementOptions)))
                                cmd.Parameters.AddWithValue("@HistoricalNotes", If(String.IsNullOrEmpty(item.HistoricalNotes), CObj(DBNull.Value), CObj(item.HistoricalNotes)))
                                cmd.ExecuteNonQuery()
                                successCount += 1
                            End Using
                        Next
                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        ErrorHandler.LogError(ex, "BulkInsertJoineryTypes - Transaction failed")
                        Return 0
                    End Try
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BulkInsertJoineryTypes")
        Finally
            SetReadOnlyAttribute()
        End Try

        Return successCount
    End Function

    ''' &lt;summary&gt;
    ''' Bulk inserts hardware standards (used during initial seed)
    ''' &lt;/summary&gt;
    Friend Function BulkInsertHardwareStandards(hardwareList As List(Of HardwareStandard)) As Integer
        Dim successCount = 0

        Try
            RemoveReadOnlyAttribute()

            Using conn = GetWriteConnection()
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        For Each item In hardwareList
                            Using cmd As New SQLiteCommand("
                                INSERT OR REPLACE INTO HardwareStandards
                                (Name, Category, Specifications, Dimensions, MountingRequirements, WeightCapacity,
                                 CommonBrands, PartNumbers, Notes, InstallationTips)
                                VALUES (@Name, @Category, @Specifications, @Dimensions, @MountingRequirements, @WeightCapacity,
                                        @CommonBrands, @PartNumbers, @Notes, @InstallationTips)
                            ", conn, transaction)
                                cmd.Parameters.AddWithValue("@Name", item.Name)
                                cmd.Parameters.AddWithValue("@Category", item.Category)
                                cmd.Parameters.AddWithValue("@Specifications", If(String.IsNullOrEmpty(item.Specifications), CObj(DBNull.Value), CObj(item.Specifications)))
                                cmd.Parameters.AddWithValue("@Dimensions", If(String.IsNullOrEmpty(item.Dimensions), CObj(DBNull.Value), CObj(item.Dimensions)))
                                cmd.Parameters.AddWithValue("@MountingRequirements", If(String.IsNullOrEmpty(item.MountingRequirements), CObj(DBNull.Value), CObj(item.MountingRequirements)))
                                cmd.Parameters.AddWithValue("@WeightCapacity", If(String.IsNullOrEmpty(item.WeightCapacity), CObj(DBNull.Value), CObj(item.WeightCapacity)))
                                cmd.Parameters.AddWithValue("@CommonBrands", If(String.IsNullOrEmpty(item.CommonBrands), CObj(DBNull.Value), CObj(item.CommonBrands)))
                                cmd.Parameters.AddWithValue("@PartNumbers", If(String.IsNullOrEmpty(item.PartNumbers), CObj(DBNull.Value), CObj(item.PartNumbers)))
                                cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(item.Notes), CObj(DBNull.Value), CObj(item.Notes)))
                                cmd.Parameters.AddWithValue("@InstallationTips", If(String.IsNullOrEmpty(item.InstallationTips), CObj(DBNull.Value), CObj(item.InstallationTips)))
                                cmd.ExecuteNonQuery()
                                successCount += 1
                            End Using
                        Next
                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        ErrorHandler.LogError(ex, "BulkInsertHardwareStandards - Transaction failed")
                        Return 0
                    End Try
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BulkInsertHardwareStandards")
        Finally
            SetReadOnlyAttribute()
        End Try

        Return successCount
    End Function

#End Region

#Region "Utility Methods"

    Public Function IsWoodSpeciesSeeded() As Boolean
        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT COUNT(*) FROM WoodSpecies", conn)
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "IsWoodSpeciesSeeded")
            Return False
        End Try
    End Function

    Public Function IsJoineryTypesSeeded() As Boolean
        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT COUNT(*) FROM JoineryTypes", conn)
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "IsJoineryTypesSeeded")
            Return False
        End Try
    End Function

    Public Function IsHardwareStandardsSeeded() As Boolean
        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT COUNT(*) FROM HardwareStandards", conn)
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "IsHardwareStandardsSeeded")
            Return False
        End Try
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
