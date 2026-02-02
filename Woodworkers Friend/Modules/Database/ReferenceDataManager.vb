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
        ' Database location: Program Files\Woodworkers Friend\Data\Resources\Reference.db
        Dim resourcesDir = Globals.AppResourcesDir
        Directory.CreateDirectory(resourcesDir)

        _databasePath = Path.Combine(resourcesDir, "Reference.db")
        _connectionString = $"Data Source={_databasePath};Version=3;Read Only=True;"

        InitializeDatabase()
    End Sub

#End Region

#Region "Initialization"

    Private Sub InitializeDatabase()
        Try
            Dim needsCreation = Not File.Exists(_databasePath)
            
            If needsCreation Then
                CreateDatabaseSchema()
                ErrorHandler.LogError(New Exception("Reference.db created successfully"), "ReferenceDataManager.InitializeDatabase")
            Else
                ' File exists - verify it has tables. If schema is invalid, recreate it.
                If Not VerifyDatabaseSchema() Then
                    ErrorHandler.LogError(New Exception("Reference.db schema invalid - recreating"), "ReferenceDataManager.InitializeDatabase")
                    File.Delete(_databasePath)
                    CreateDatabaseSchema()
                    ErrorHandler.LogError(New Exception("Reference.db schema recreated"), "ReferenceDataManager.InitializeDatabase")
                End If
            End If

            ' NOTE: Do NOT set read-only here! 
            ' Migrations need to write data AFTER schema creation.
            ' Read-only attribute will be set in DataMigration.PerformInitialMigration() after all data is seeded.
        Catch ex As Exception
            ErrorHandler.LogError(ex, "ReferenceDataManager.InitializeDatabase")
        End Try
    End Sub
    
    ''' <summary>
    ''' Sets Reference.db to read-only after all migrations complete.
    ''' Called by DataMigration.PerformInitialMigration() after seeding data.
    ''' </summary>
    Public Sub FinalizeDatabase()
        SetReadOnlyAttribute()
        ErrorHandler.LogError(New Exception("Reference.db finalized as read-only"), "ReferenceDataManager.FinalizeDatabase")
    End Sub

    Private Sub CreateDatabaseSchema()
        Dim createConnectionString = $"Data Source={_databasePath};Version=3;"

        Using conn As New SQLiteConnection(createConnectionString)
            conn.Open()

            ' Execute each CREATE TABLE separately - SQLite.NET doesn't support multi-statement commands
            Using cmd As New SQLiteCommand(conn)
                ' Wood Species Table
                cmd.CommandText = "
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
                    );"
                cmd.ExecuteNonQuery()

                cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_wood_common_name ON WoodSpecies(CommonName);"
                cmd.ExecuteNonQuery()

                cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_wood_type ON WoodSpecies(WoodType);"
                cmd.ExecuteNonQuery()

                ' Joinery Types Table
                cmd.CommandText = "
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
                    );"
                cmd.ExecuteNonQuery()

                cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_joinery_name ON JoineryTypes(Name);"
                cmd.ExecuteNonQuery()

                cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_joinery_category ON JoineryTypes(Category);"
                cmd.ExecuteNonQuery()

                ' Hardware Standards Table
                cmd.CommandText = "
                    CREATE TABLE IF NOT EXISTS HardwareStandards (
                        HardwareID INTEGER PRIMARY KEY AUTOINCREMENT,
                        Category TEXT NOT NULL,
                        Type TEXT NOT NULL,
                        Brand TEXT,
                        PartNumber TEXT,
                        Description TEXT,
                        Dimensions TEXT,
                        MountingRequirements TEXT,
                        WeightCapacity TEXT,
                        TypicalUses TEXT,
                        InstallationNotes TEXT,
                        PurchaseLink TEXT,
                        IsUserAdded INTEGER DEFAULT 0,
                        DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP
                    );"
                cmd.ExecuteNonQuery()

                cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_hardware_type ON HardwareStandards(Type);"
                cmd.ExecuteNonQuery()

                cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_hardware_category ON HardwareStandards(Category);"
                cmd.ExecuteNonQuery()

                ' Version Table
                cmd.CommandText = "
                    CREATE TABLE IF NOT EXISTS DatabaseVersion (
                        Version TEXT NOT NULL,
                        UpdatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
                    );"
                cmd.ExecuteNonQuery()

                cmd.CommandText = "INSERT INTO DatabaseVersion (Version) VALUES ('1.0');"
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Function VerifyDatabaseSchema() As Boolean
        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    SELECT COUNT(*) FROM sqlite_master
                    WHERE type='table' AND name IN ('WoodSpecies', 'JoineryTypes', 'HardwareStandards')
                ", conn)
                    Dim tableCount = Convert.ToInt32(cmd.ExecuteScalar())
                    If tableCount < 3 Then
                        ErrorHandler.LogError(New Exception($"Reference.db schema invalid - found {tableCount}/3 tables"), "VerifyDatabaseSchema")
                        Return False
                    End If
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "VerifyDatabaseSchema")
            Return False
        End Try
    End Function

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

    Private Shared Function CreateWoodSpeciesFromReader(reader As SQLiteDataReader) As WoodSpecies
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

    Private Shared Function CreateJoineryTypeFromReader(reader As SQLiteDataReader) As JoineryType
        Return New JoineryType With {
            .Name = reader.GetString(1),
            .Category = If(reader.IsDBNull(2), String.Empty, reader.GetString(2)),
            .StrengthRating = If(reader.IsDBNull(3), 0, reader.GetInt32(3)),
            .DifficultyLevel = If(reader.IsDBNull(4), String.Empty, reader.GetString(4)),
            .Description = If(reader.IsDBNull(5), String.Empty, reader.GetString(5)),
            .TypicalUses = If(reader.IsDBNull(6), String.Empty, reader.GetString(6)),
            .RequiredTools = If(reader.IsDBNull(7), String.Empty, reader.GetString(7)),
            .StrengthCharacteristics = If(reader.IsDBNull(8), String.Empty, reader.GetString(8)),
            .GlueRequired = Not reader.IsDBNull(9) AndAlso reader.GetInt32(9) = 1,
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
                    SELECT HardwareID, Category, Type, Brand, PartNumber, Description, 
                           Dimensions, MountingRequirements, WeightCapacity, TypicalUses, 
                           InstallationNotes, PurchaseLink, IsUserAdded, DateAdded
                    FROM HardwareStandards
                    ORDER BY Category, Type
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
                    SELECT HardwareID, Category, Type, Brand, PartNumber, Description,
                           Dimensions, MountingRequirements, WeightCapacity, TypicalUses,
                           InstallationNotes, PurchaseLink, IsUserAdded, DateAdded
                    FROM HardwareStandards
                    WHERE Category = @Category
                    ORDER BY Type
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

    Private Shared Function CreateHardwareStandardFromReader(reader As SQLiteDataReader) As HardwareStandard
        Return New HardwareStandard With {
            .HardwareID = reader.GetInt32(0),
            .Category = reader.GetString(1),
            .Type = reader.GetString(2),
            .Brand = If(reader.IsDBNull(3), String.Empty, reader.GetString(3)),
            .PartNumber = If(reader.IsDBNull(4), String.Empty, reader.GetString(4)),
            .Description = If(reader.IsDBNull(5), String.Empty, reader.GetString(5)),
            .Dimensions = If(reader.IsDBNull(6), String.Empty, reader.GetString(6)),
            .MountingRequirements = If(reader.IsDBNull(7), String.Empty, reader.GetString(7)),
            .WeightCapacity = If(reader.IsDBNull(8), String.Empty, reader.GetString(8)),
            .TypicalUses = If(reader.IsDBNull(9), String.Empty, reader.GetString(9)),
            .InstallationNotes = If(reader.IsDBNull(10), String.Empty, reader.GetString(10)),
            .PurchaseLink = If(reader.IsDBNull(11), String.Empty, reader.GetString(11)),
            .IsUserAdded = Not reader.IsDBNull(12) AndAlso reader.GetInt32(12) = 1,
            .DateAdded = If(reader.IsDBNull(13), DateTime.Now, reader.GetDateTime(13))
        }
    End Function

    ''' <summary>
    ''' Adds a single hardware standard (used during migration)
    ''' </summary>
    Public Function AddHardwareStandard(hardware As HardwareStandard) As Boolean
        Try
            RemoveReadOnlyAttribute()

            Using conn = GetWriteConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    INSERT INTO HardwareStandards
                    (Category, Type, Brand, PartNumber, Description, Dimensions,
                     MountingRequirements, WeightCapacity, TypicalUses, InstallationNotes,
                     PurchaseLink, IsUserAdded)
                    VALUES (@Category, @Type, @Brand, @PartNumber, @Description, @Dimensions,
                            @MountingRequirements, @WeightCapacity, @TypicalUses, @InstallationNotes,
                            @PurchaseLink, 0)
                ", conn)
                    cmd.Parameters.AddWithValue("@Category", hardware.Category)
                    cmd.Parameters.AddWithValue("@Type", hardware.Type)
                    cmd.Parameters.AddWithValue("@Brand", If(String.IsNullOrEmpty(hardware.Brand), CObj(DBNull.Value), CObj(hardware.Brand)))
                    cmd.Parameters.AddWithValue("@PartNumber", If(String.IsNullOrEmpty(hardware.PartNumber), CObj(DBNull.Value), CObj(hardware.PartNumber)))
                    cmd.Parameters.AddWithValue("@Description", If(String.IsNullOrEmpty(hardware.Description), CObj(DBNull.Value), CObj(hardware.Description)))
                    cmd.Parameters.AddWithValue("@Dimensions", If(String.IsNullOrEmpty(hardware.Dimensions), CObj(DBNull.Value), CObj(hardware.Dimensions)))
                    cmd.Parameters.AddWithValue("@MountingRequirements", If(String.IsNullOrEmpty(hardware.MountingRequirements), CObj(DBNull.Value), CObj(hardware.MountingRequirements)))
                    cmd.Parameters.AddWithValue("@WeightCapacity", If(String.IsNullOrEmpty(hardware.WeightCapacity), CObj(DBNull.Value), CObj(hardware.WeightCapacity)))
                    cmd.Parameters.AddWithValue("@TypicalUses", If(String.IsNullOrEmpty(hardware.TypicalUses), CObj(DBNull.Value), CObj(hardware.TypicalUses)))
                    cmd.Parameters.AddWithValue("@InstallationNotes", If(String.IsNullOrEmpty(hardware.InstallationNotes), CObj(DBNull.Value), CObj(hardware.InstallationNotes)))
                    cmd.Parameters.AddWithValue("@PurchaseLink", If(String.IsNullOrEmpty(hardware.PurchaseLink), CObj(DBNull.Value), CObj(hardware.PurchaseLink)))
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "AddHardwareStandard")
            Return False
        Finally
            SetReadOnlyAttribute()
        End Try
    End Function

    ''' <summary>
    ''' Adds a single joinery type (used during migration)
    ''' </summary>
    Public Function AddJoineryType(joinery As JoineryType) As Boolean
        Try
            RemoveReadOnlyAttribute()

            Using conn = GetWriteConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    INSERT INTO JoineryTypes
                    (Name, Category, StrengthRating, DifficultyLevel, Description, TypicalUses,
                     RequiredTools, StrengthCharacteristics, GlueRequired, ReinforcementOptions, HistoricalNotes)
                    VALUES (@Name, @Category, @StrengthRating, @DifficultyLevel, @Description, @TypicalUses,
                            @RequiredTools, @StrengthCharacteristics, @GlueRequired, @ReinforcementOptions, @HistoricalNotes)
                ", conn)
                    cmd.Parameters.AddWithValue("@Name", joinery.Name)
                    cmd.Parameters.AddWithValue("@Category", joinery.Category)
                    cmd.Parameters.AddWithValue("@StrengthRating", joinery.StrengthRating)
                    cmd.Parameters.AddWithValue("@DifficultyLevel", joinery.DifficultyLevel)
                    cmd.Parameters.AddWithValue("@Description", If(String.IsNullOrEmpty(joinery.Description), CObj(DBNull.Value), CObj(joinery.Description)))
                    cmd.Parameters.AddWithValue("@TypicalUses", If(String.IsNullOrEmpty(joinery.TypicalUses), CObj(DBNull.Value), CObj(joinery.TypicalUses)))
                    cmd.Parameters.AddWithValue("@RequiredTools", If(String.IsNullOrEmpty(joinery.RequiredTools), CObj(DBNull.Value), CObj(joinery.RequiredTools)))
                    cmd.Parameters.AddWithValue("@StrengthCharacteristics", If(String.IsNullOrEmpty(joinery.StrengthCharacteristics), CObj(DBNull.Value), CObj(joinery.StrengthCharacteristics)))
                    cmd.Parameters.AddWithValue("@GlueRequired", If(joinery.GlueRequired, 1, 0))
                    cmd.Parameters.AddWithValue("@ReinforcementOptions", If(String.IsNullOrEmpty(joinery.ReinforcementOptions), CObj(DBNull.Value), CObj(joinery.ReinforcementOptions)))
                    cmd.Parameters.AddWithValue("@HistoricalNotes", If(String.IsNullOrEmpty(joinery.HistoricalNotes), CObj(DBNull.Value), CObj(joinery.HistoricalNotes)))
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "AddJoineryType")
            Return False
        Finally
            SetReadOnlyAttribute()
        End Try
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
                                (Category, Type, Brand, PartNumber, Description, Dimensions,
                                 MountingRequirements, WeightCapacity, TypicalUses, InstallationNotes,
                                 PurchaseLink, IsUserAdded)
                                VALUES (@Category, @Type, @Brand, @PartNumber, @Description, @Dimensions,
                                        @MountingRequirements, @WeightCapacity, @TypicalUses, @InstallationNotes,
                                        @PurchaseLink, @IsUserAdded)
                            ", conn, transaction)
                                cmd.Parameters.AddWithValue("@Category", item.Category)
                                cmd.Parameters.AddWithValue("@Type", item.Type)
                                cmd.Parameters.AddWithValue("@Brand", If(String.IsNullOrEmpty(item.Brand), CObj(DBNull.Value), CObj(item.Brand)))
                                cmd.Parameters.AddWithValue("@PartNumber", If(String.IsNullOrEmpty(item.PartNumber), CObj(DBNull.Value), CObj(item.PartNumber)))
                                cmd.Parameters.AddWithValue("@Description", If(String.IsNullOrEmpty(item.Description), CObj(DBNull.Value), CObj(item.Description)))
                                cmd.Parameters.AddWithValue("@Dimensions", If(String.IsNullOrEmpty(item.Dimensions), CObj(DBNull.Value), CObj(item.Dimensions)))
                                cmd.Parameters.AddWithValue("@MountingRequirements", If(String.IsNullOrEmpty(item.MountingRequirements), CObj(DBNull.Value), CObj(item.MountingRequirements)))
                                cmd.Parameters.AddWithValue("@WeightCapacity", If(String.IsNullOrEmpty(item.WeightCapacity), CObj(DBNull.Value), CObj(item.WeightCapacity)))
                                cmd.Parameters.AddWithValue("@TypicalUses", If(String.IsNullOrEmpty(item.TypicalUses), CObj(DBNull.Value), CObj(item.TypicalUses)))
                                cmd.Parameters.AddWithValue("@InstallationNotes", If(String.IsNullOrEmpty(item.InstallationNotes), CObj(DBNull.Value), CObj(item.InstallationNotes)))
                                cmd.Parameters.AddWithValue("@PurchaseLink", If(String.IsNullOrEmpty(item.PurchaseLink), CObj(DBNull.Value), CObj(item.PurchaseLink)))
                                cmd.Parameters.AddWithValue("@IsUserAdded", If(item.IsUserAdded, 1, 0))
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
