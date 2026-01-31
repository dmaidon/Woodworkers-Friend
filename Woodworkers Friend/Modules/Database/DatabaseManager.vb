' ============================================================================
' Last Updated: January 2026
' Changes: MAJOR REFACTOR - Database split into 3 specialized databases:
'          - Help.db: Read-only help content (HelpDataManager)
'          - Reference.db: Read-only reference data (ReferenceDataManager)
'          - UserData.db: User-modifiable data (UserDataManager)
'          DatabaseManager now acts as a coordinator/facade pattern
' ============================================================================

Imports System.Data.SQLite
Imports System.IO

''' <summary>
''' Coordinator/Facade for multi-database architecture.
''' Routes queries to appropriate specialized database manager:
'''   - Help.db (HelpDataManager): Help content, tutorials, definitions
'''   - Reference.db (ReferenceDataManager): Wood species, joinery, hardware
'''   - UserData.db (UserDataManager): Costs, preferences, history
''' </summary>
Public Class DatabaseManager
    Private Shared _instance As DatabaseManager
    Private Const DB_VERSION As Integer = 2 ' Incremented for multi-database architecture

    ' Specialized database managers (lazy-loaded singletons)
    Private _helpManager As HelpDataManager
    Private _referenceManager As ReferenceDataManager
    Private _userDataManager As UserDataManager

    ' Legacy monolithic database path (for migration detection)
    Private ReadOnly _legacyDbPath As String

    ''' <summary>
    ''' Private constructor for Singleton pattern
    ''' </summary>
    Private Sub New()
        Try
            ' Ensure Data directory exists
            If Not Directory.Exists(DataDir) Then
                Directory.CreateDirectory(DataDir)
            End If

            ' Legacy database path (for migration)
            _legacyDbPath = Path.Combine(DataDir, "WoodworkersFriend.db")

            ' Check if migration is needed
            If NeedsDatabaseSplit() Then
                PerformDatabaseSplit()
            End If

            ' Initialize specialized managers (lazy-loaded via properties)
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DatabaseManager Constructor")
            Throw New ApplicationException("Failed to initialize database manager", ex)
        End Try
    End Sub

    ''' <summary>
    ''' Gets the singleton instance of DatabaseManager
    ''' </summary>
    Public Shared ReadOnly Property Instance As DatabaseManager
        Get
            If _instance Is Nothing Then
                _instance = New DatabaseManager()
            End If
            Return _instance
        End Get
    End Property

#Region "Database Manager Properties"

    ''' <summary>
    ''' Help database manager (read-only, concurrent access)
    ''' </summary>
    Public ReadOnly Property Help As HelpDataManager
        Get
            If _helpManager Is Nothing Then
                _helpManager = HelpDataManager.Instance
            End If
            Return _helpManager
        End Get
    End Property

    ''' <summary>
    ''' Reference database manager (read-only, refreshable)
    ''' </summary>
    Public ReadOnly Property Reference As ReferenceDataManager
        Get
            If _referenceManager Is Nothing Then
                _referenceManager = ReferenceDataManager.Instance
            End If
            Return _referenceManager
        End Get
    End Property

    ''' <summary>
    ''' User data manager (read-write, user-modifiable)
    ''' </summary>
    Public ReadOnly Property UserData As UserDataManager
        Get
            If _userDataManager Is Nothing Then
                _userDataManager = UserDataManager.Instance
            End If
            Return _userDataManager
        End Get
    End Property

#End Region

#Region "Legacy Properties (for backward compatibility)"

    ''' <summary>
    ''' Gets the legacy database path (kept for migration detection)
    ''' </summary>
    Public ReadOnly Property DatabasePath As String
        Get
            Return _legacyDbPath
        End Get
    End Property

#End Region

#Region "Migration Logic"

    ''' <summary>
    ''' Checks if monolithic database exists and needs splitting
    ''' </summary>
    Private Function NeedsDatabaseSplit() As Boolean
        ' Check if old monolithic database exists
        If Not File.Exists(_legacyDbPath) Then
            Return False
        End If

        ' Check if new databases exist
        Dim helpExists = File.Exists(Path.Combine(DataDir, "Resources", "Help.db"))
        Dim refExists = File.Exists(Path.Combine(DataDir, "Resources", "Reference.db"))
        Dim userExists = File.Exists(Path.Combine(DataDir, "UserData.db"))

        ' If old exists and new ones don't, migration is needed
        Return Not (helpExists AndAlso refExists AndAlso userExists)
    End Function

    ''' <summary>
    ''' Performs database split from monolithic to multi-database architecture
    ''' </summary>
    Private Sub PerformDatabaseSplit()
        Try
            ErrorHandler.LogError(New Exception("Starting database split migration..."), "PerformDatabaseSplit")

            ' This will be implemented in DataMigration.SplitMonolithicDatabase()
            ' For now, just log that we detected the need
            ErrorHandler.LogError(New Exception("Legacy database detected. Migration will occur on first data access."), "PerformDatabaseSplit")

            ' Rename legacy database for safety
            Try
                Dim backupPath = _legacyDbPath & ".v1.backup"
                If Not File.Exists(backupPath) Then
                    File.Copy(_legacyDbPath, backupPath, False)
                    ErrorHandler.LogError(New Exception($"Legacy database backed up to: {backupPath}"), "PerformDatabaseSplit")
                End If
            Catch ex As Exception
                ErrorHandler.LogError(ex, "PerformDatabaseSplit - Backup failed")
            End Try

        Catch ex As Exception
            ErrorHandler.LogError(ex, "PerformDatabaseSplit")
        End Try
    End Sub

#End Region

#Region "Unified API - Help Content (Routes to HelpDataManager)"

    ''' <summary>
    ''' Gets help content for a specific module/topic
    ''' </summary>
    Public Function GetHelpContent(moduleName As String) As HelpContentData
        Return Help.GetContent(moduleName)
    End Function

    ''' <summary>
    ''' Searches help content by keywords
    ''' </summary>
    Public Function SearchHelpContent(searchTerm As String) As List(Of HelpContentData)
        Return Help.SearchContent(searchTerm)
    End Function

    ''' <summary>
    ''' Gets all help topics (lightweight, no content body)
    ''' </summary>
    Public Function GetHelpTopics() As List(Of HelpContentData)
        Return Help.GetAllTopics()
    End Function

    ''' <summary>
    ''' Checks if help content has been seeded
    ''' </summary>
    Public Function IsHelpContentSeeded() As Boolean
        Return Help.IsContentSeeded()
    End Function

    ''' <summary>
    ''' Bulk inserts help content (used during seeding)
    ''' </summary>
    Friend Function BulkInsertHelpContent(items As List(Of HelpContentData)) As Integer
        Return Help.BulkInsertContent(items)
    End Function

#End Region

#Region "Unified API - Wood Species (Routes to Reference + UserData)"

    ''' <summary>
    ''' Gets wood species by name (checks user-added first, then reference)
    ''' </summary>
    Public Function GetWoodSpecies(commonName As String) As WoodSpecies
        ' Check user-added species first
        Dim customSpecies = UserData.GetCustomWoodSpecies().FirstOrDefault(Function(s) s.CommonName.Equals(commonName, StringComparison.OrdinalIgnoreCase))
        If customSpecies IsNot Nothing Then
            Return customSpecies
        End If

        ' Fall back to reference database
        Return Reference.GetWoodSpecies(commonName)
    End Function

    ''' <summary>
    ''' Gets all wood species (merged: reference + user-added)
    ''' </summary>
    Public Function GetWoodSpeciesList() As List(Of WoodSpecies)
        Dim results As New List(Of WoodSpecies)

        ' Add reference species
        results.AddRange(Reference.GetAllWoodSpecies())

        ' Add user-added species
        results.AddRange(UserData.GetCustomWoodSpecies())

        Return results.OrderBy(Function(s) s.CommonName).ToList()
    End Function

#End Region

#Region "Unified API - Preferences (Routes to UserDataManager)"

    ''' <summary>
    ''' Gets a preference value
    ''' </summary>
    Public Function GetPreference(key As String) As String
        Return UserData.GetPreference(key)
    End Function

    ''' <summary>
    ''' Saves a preference value
    ''' </summary>
    Public Function SavePreference(key As String, value As String, dataType As String, category As String) As Boolean
        Return UserData.SavePreference(key, value, dataType, category)
    End Function

    ''' <summary>
    ''' Checks if any preferences exist
    ''' </summary>
    Public Function HasPreferences() As Boolean
        Return UserData.HasPreferences()
    End Function

    ''' <summary>
    ''' Gets a boolean preference with default
    ''' </summary>
    Public Function GetBoolPreference(key As String, defaultValue As Boolean) As Boolean
        Dim value = GetPreference(key)
        If String.IsNullOrEmpty(value) Then
            Return defaultValue
        End If
        Boolean.TryParse(value, defaultValue)
        Return defaultValue
    End Function

    ''' <summary>
    ''' Gets an integer preference with default
    ''' </summary>
    Public Function GetIntPreference(key As String, defaultValue As Integer) As Integer
        Dim value = GetPreference(key)
        If String.IsNullOrEmpty(value) Then
            Return defaultValue
        End If
        Integer.TryParse(value, defaultValue)
        Return defaultValue
    End Function

    ''' <summary>
    ''' Gets a double preference with default
    ''' </summary>
    Public Function GetDoublePreference(key As String, defaultValue As Double) As Double
        Dim value = GetPreference(key)
        If String.IsNullOrEmpty(value) Then
            Return defaultValue
        End If
        Double.TryParse(value, defaultValue)
        Return defaultValue
    End Function

#End Region

#Region "Data Models (Kept for compatibility)"

    ''' <summary>
    ''' Help content data model
    ''' </summary>
    Public Class HelpContentData
        Public Property ModuleName As String
        Public Property Title As String
        Public Property Content As String
        Public Property Keywords As String
        Public Property Category As String
        Public Property SortOrder As Integer
        Public Property Version As String
    End Class

#End Region

#Region "OBSOLETE METHODS - Redirecting to specialized managers"

    ' Note: ALL legacy monolithic database methods have been removed.
    ' Use the specialized managers instead:
    '   - Help.GetContent(), Help.SearchContent(), etc.
    '   - Reference.GetAllWoodSpecies(), Reference.GetAllJoineryTypes(), etc.
    '   - UserData.GetPreference(), UserData.SavePreference(), etc.
    '
    ' Old methods like GetAllWoodSpecies(), GetHelpTopics(), etc. have been
    ' removed to prevent conflicts with the new unified API above.

#End Region

End Class

            Try
                Using cmd = conn.CreateCommand()
                    cmd.Transaction = transaction

                    ' Database version tracking
                    cmd.CommandText = "
                        CREATE TABLE IF NOT EXISTS DatabaseVersion (
                            Version INTEGER PRIMARY KEY,
                            AppliedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
                            Description TEXT
                        );"
                    cmd.ExecuteNonQuery()

                    ' Wood Species table (unified)
                    cmd.CommandText = "
                        CREATE TABLE IF NOT EXISTS WoodSpecies (
                            SpeciesID INTEGER PRIMARY KEY AUTOINCREMENT,
                            CommonName TEXT NOT NULL UNIQUE,
                            ScientificName TEXT,
                            WoodType TEXT CHECK(WoodType IN ('Hardwood', 'Softwood')) NOT NULL,

                            -- Physical Properties
                            JankaHardness INTEGER,
                            SpecificGravity REAL,
                            Density INTEGER,
                            MoistureContent REAL,

                            -- Movement Properties
                            ShrinkageRadial REAL,
                            ShrinkageTangential REAL,

                            -- Reference Information
                            TypicalUses TEXT,
                            Workability TEXT,
                            Cautions TEXT,
                            Notes TEXT,

                            -- Metadata
                            IsUserAdded BOOLEAN DEFAULT 0,
                            DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
                            LastModified DATETIME DEFAULT CURRENT_TIMESTAMP
                        );"
                    cmd.ExecuteNonQuery()

                    ' Indexes for WoodSpecies
                    cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_woodtype ON WoodSpecies(WoodType);"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_commonname ON WoodSpecies(CommonName);"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_useradded ON WoodSpecies(IsUserAdded);"
                    cmd.ExecuteNonQuery()

                    ' Help Content table
                    cmd.CommandText = "
                        CREATE TABLE IF NOT EXISTS HelpContent (
                            HelpID INTEGER PRIMARY KEY AUTOINCREMENT,
                            ModuleName TEXT NOT NULL,
                            Title TEXT NOT NULL,
                            Content TEXT NOT NULL,
                            Keywords TEXT,
                            Category TEXT,
                            SortOrder INTEGER DEFAULT 0,
                            Version TEXT DEFAULT '1.0',
                            LastUpdated DATETIME DEFAULT CURRENT_TIMESTAMP
                        );"
                    cmd.ExecuteNonQuery()

                    ' Index for Help Content
                    cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_modulename ON HelpContent(ModuleName);"
                    cmd.ExecuteNonQuery()

                    ' User Preferences table
                    cmd.CommandText = "
                        CREATE TABLE IF NOT EXISTS UserPreferences (
                            PrefKey TEXT PRIMARY KEY,
                            PrefValue TEXT,
                            DataType TEXT CHECK(DataType IN ('String', 'Integer', 'Boolean', 'Double', 'JSON')) DEFAULT 'String',
                            Category TEXT,
                            Description TEXT,
                            LastModified DATETIME DEFAULT CURRENT_TIMESTAMP
                        );"
                    cmd.ExecuteNonQuery()

                    ' Calculation History table
                    cmd.CommandText = "
                        CREATE TABLE IF NOT EXISTS CalculationHistory (
                            HistoryID INTEGER PRIMARY KEY AUTOINCREMENT,
                            CalculatorType TEXT NOT NULL,
                            CalculationName TEXT,
                            InputParameters TEXT NOT NULL,
                            Results TEXT NOT NULL,
                            DateCalculated DATETIME DEFAULT CURRENT_TIMESTAMP,
                            IsFavorite BOOLEAN DEFAULT 0,
                            Notes TEXT
                        );"
                    cmd.ExecuteNonQuery()

                    ' Indexes for Calculation History
                    cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_calculator_type ON CalculationHistory(CalculatorType);"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_favorites ON CalculationHistory(IsFavorite);"
                    cmd.ExecuteNonQuery()

                    ' JoineryTypes table (Phase 7.1)
                    cmd.CommandText = "
                        CREATE TABLE IF NOT EXISTS JoineryTypes (
                            JoineryID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Category TEXT,
                            StrengthRating INTEGER,
                            DifficultyLevel TEXT,
                            Description TEXT,
                            TypicalUses TEXT,
                            RequiredTools TEXT,
                            StrengthCharacteristics TEXT,
                            GlueRequired BOOLEAN DEFAULT 1,
                            ReinforcementOptions TEXT,
                            HistoricalNotes TEXT,
                            DiagramFileName TEXT,
                            IsUserAdded BOOLEAN DEFAULT 0,
                            DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP
                        );"
                    cmd.ExecuteNonQuery()

                    ' Indexes for JoineryTypes
                    cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_joinery_category ON JoineryTypes(Category);"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_joinery_difficulty ON JoineryTypes(DifficultyLevel);"
                    cmd.ExecuteNonQuery()

                    ' HardwareStandards table (Phase 7.2)
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
                            IsUserAdded BOOLEAN DEFAULT 0,
                            DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP
                        );"
                    cmd.ExecuteNonQuery()

                    ' Indexes for HardwareStandards
                    cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_hardware_category ON HardwareStandards(Category);"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_hardware_type ON HardwareStandards(Type);"
                    cmd.ExecuteNonQuery()

                    ' WoodCosts table (Phase 7.3 - CSV Migration)
                    cmd.CommandText = "
                        CREATE TABLE IF NOT EXISTS WoodCosts (
                            WoodCostID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Thickness TEXT NOT NULL,
                            WoodName TEXT NOT NULL,
                            CostPerBoardFoot REAL NOT NULL,
                            Active BOOLEAN DEFAULT 1,
                            IsUserAdded BOOLEAN DEFAULT 0,
                            DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
                            LastModified DATETIME DEFAULT CURRENT_TIMESTAMP,
                            UNIQUE(Thickness, WoodName)
                        );"
                    cmd.ExecuteNonQuery()

                    ' Indexes for WoodCosts
                    cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_woodcost_active ON WoodCosts(Active);"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_woodcost_name ON WoodCosts(WoodName);"
                    cmd.ExecuteNonQuery()

                    ' EpoxyCosts table (Phase 7.3 - CSV Migration)
                    cmd.CommandText = "
                        CREATE TABLE IF NOT EXISTS EpoxyCosts (
                            EpoxyCostID INTEGER PRIMARY KEY AUTOINCREMENT,
                            Brand TEXT NOT NULL,
                            Type TEXT NOT NULL,
                            CostPerGallon REAL NOT NULL,
                            DisplayText TEXT,
                            Active BOOLEAN DEFAULT 1,
                            IsUserAdded BOOLEAN DEFAULT 0,
                            DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
                            LastModified DATETIME DEFAULT CURRENT_TIMESTAMP,
                            UNIQUE(Brand, Type)
                        );"
                    cmd.ExecuteNonQuery()

                    ' Indexes for EpoxyCosts
                    cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_epoxycost_active ON EpoxyCosts(Active);"
                    cmd.ExecuteNonQuery()

                    cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_epoxycost_brand ON EpoxyCosts(Brand);"
                    cmd.ExecuteNonQuery()

                    ' Record database version
                    cmd.CommandText = $"INSERT INTO DatabaseVersion (Version, Description) VALUES ({DB_VERSION}, 'Initial schema creation');"
                    cmd.ExecuteNonQuery()

                End Using

                transaction.Commit()
                ErrorHandler.LogError(New Exception("Database schema created successfully"), "CreateSchema")
            Catch ex As Exception
                transaction.Rollback()
                ErrorHandler.LogError(ex, "CreateSchema")
                Throw
            End Try
        End Using
    End Sub

    ''' <summary>
    ''' Seeds initial data into new database
    ''' </summary>
    Private Shared Sub SeedInitialData(conn As SQLiteConnection)
        ArgumentNullException.ThrowIfNull(conn)

        Try
            ' This will be called by DataMigration.vb
            ErrorHandler.LogError(New Exception("Database ready for initial data seeding"), "SeedInitialData")
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SeedInitialData")
        End Try
    End Sub

    ''' <summary>
    ''' Checks database version and upgrades schema if needed
    ''' </summary>
    Private Shared Sub CheckAndUpgradeSchema(conn As SQLiteConnection)
        Try
            Using cmd = conn.CreateCommand()
                ' Check if JoineryTypes table exists
                cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='JoineryTypes'"
                Dim joineryTableExists = cmd.ExecuteScalar() IsNot Nothing

                ' Check if HardwareStandards table exists
                cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='HardwareStandards'"
                Dim hardwareTableExists = cmd.ExecuteScalar() IsNot Nothing

                ' Check if WoodCosts table exists (Phase 7.3)
                cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='WoodCosts'"
                Dim woodCostsTableExists = cmd.ExecuteScalar() IsNot Nothing

                ' Check if EpoxyCosts table exists (Phase 7.3)
                cmd.CommandText = "SELECT name FROM sqlite_master WHERE type='table' AND name='EpoxyCosts'"
                Dim epoxyCostsTableExists = cmd.ExecuteScalar() IsNot Nothing

                ' If tables are missing, add them
                If Not joineryTableExists OrElse Not hardwareTableExists OrElse Not woodCostsTableExists OrElse Not epoxyCostsTableExists Then
                    ErrorHandler.LogError(New Exception($"Missing tables - Joinery:{Not joineryTableExists}, Hardware:{Not hardwareTableExists}, WoodCosts:{Not woodCostsTableExists}, EpoxyCosts:{Not epoxyCostsTableExists}"), "CheckAndUpgradeSchema")

                    Using transaction = conn.BeginTransaction()
                        Try
                            cmd.Transaction = transaction

                            If Not joineryTableExists Then
                                ' Create JoineryTypes table
                                cmd.CommandText = "
                                    CREATE TABLE IF NOT EXISTS JoineryTypes (
                                        JoineryID INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Name TEXT NOT NULL,
                                        Category TEXT,
                                        StrengthRating INTEGER,
                                        DifficultyLevel TEXT,
                                        Description TEXT,
                                        TypicalUses TEXT,
                                        RequiredTools TEXT,
                                        StrengthCharacteristics TEXT,
                                        GlueRequired BOOLEAN DEFAULT 1,
                                        ReinforcementOptions TEXT,
                                        HistoricalNotes TEXT,
                                        DiagramFileName TEXT,
                                        IsUserAdded BOOLEAN DEFAULT 0,
                                        DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP
                                    );"
                                cmd.ExecuteNonQuery()

                                cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_joinery_category ON JoineryTypes(Category);"
                                cmd.ExecuteNonQuery()

                                cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_joinery_difficulty ON JoineryTypes(DifficultyLevel);"
                                cmd.ExecuteNonQuery()

                                ErrorHandler.LogError(New Exception("JoineryTypes table created"), "CheckAndUpgradeSchema")
                            End If

                            If Not hardwareTableExists Then
                                ' Create HardwareStandards table
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
                                        IsUserAdded BOOLEAN DEFAULT 0,
                                        DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP
                                    );"
                                cmd.ExecuteNonQuery()

                                cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_hardware_category ON HardwareStandards(Category);"
                                cmd.ExecuteNonQuery()

                                cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_hardware_type ON HardwareStandards(Type);"
                                cmd.ExecuteNonQuery()

                                ErrorHandler.LogError(New Exception("HardwareStandards table created"), "CheckAndUpgradeSchema")
                            End If

                            If Not woodCostsTableExists Then
                                ' Create WoodCosts table
                                cmd.CommandText = "
                                    CREATE TABLE IF NOT EXISTS WoodCosts (
                                        WoodCostID INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Thickness TEXT NOT NULL,
                                        WoodName TEXT NOT NULL,
                                        CostPerBoardFoot REAL NOT NULL,
                                        Active BOOLEAN DEFAULT 1,
                                        IsUserAdded BOOLEAN DEFAULT 0,
                                        DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
                                        LastModified DATETIME DEFAULT CURRENT_TIMESTAMP,
                                        UNIQUE(Thickness, WoodName)
                                    );"
                                cmd.ExecuteNonQuery()

                                cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_woodcost_active ON WoodCosts(Active);"
                                cmd.ExecuteNonQuery()

                                cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_woodcost_name ON WoodCosts(WoodName);"
                                cmd.ExecuteNonQuery()

                                ErrorHandler.LogError(New Exception("WoodCosts table created"), "CheckAndUpgradeSchema")
                            End If

                            If Not epoxyCostsTableExists Then
                                ' Create EpoxyCosts table
                                cmd.CommandText = "
                                    CREATE TABLE IF NOT EXISTS EpoxyCosts (
                                        EpoxyCostID INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Brand TEXT NOT NULL,
                                        Type TEXT NOT NULL,
                                        CostPerGallon REAL NOT NULL,
                                        DisplayText TEXT,
                                        Active BOOLEAN DEFAULT 1,
                                        IsUserAdded BOOLEAN DEFAULT 0,
                                        DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
                                        LastModified DATETIME DEFAULT CURRENT_TIMESTAMP,
                                        UNIQUE(Brand, Type)
                                    );"
                                cmd.ExecuteNonQuery()

                                cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_epoxycost_active ON EpoxyCosts(Active);"
                                cmd.ExecuteNonQuery()

                                cmd.CommandText = "CREATE INDEX IF NOT EXISTS idx_epoxycost_brand ON EpoxyCosts(Brand);"
                                cmd.ExecuteNonQuery()

                                ErrorHandler.LogError(New Exception("EpoxyCosts table created"), "CheckAndUpgradeSchema")
                            End If

                            transaction.Commit()
                            ErrorHandler.LogError(New Exception("Schema upgrade completed successfully"), "CheckAndUpgradeSchema")
                        Catch upgradeEx As Exception
                            transaction.Rollback()
                            ErrorHandler.LogError(upgradeEx, "CheckAndUpgradeSchema - Transaction")
                            Throw
                        End Try
                    End Using
                End If
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "CheckAndUpgradeSchema")
        End Try
    End Sub

    ''' <summary>
    ''' Tests database connection
    ''' </summary>
    Public Function TestConnection() As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Return True
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "TestConnection")
            Return False
        End Try
    End Function

#Region "Wood Species Methods"

    ''' <summary>
    ''' Gets all wood species from database
    ''' </summary>
    Public Function GetAllWoodSpecies() As List(Of WoodPropertiesData)
        Dim species As New List(Of WoodPropertiesData)

        Try
            ErrorHandler.LogError(New Exception($"GetAllWoodSpecies called - Opening connection to: {_dbPath}"), "GetAllWoodSpecies")

            Using conn = GetConnection()
                conn.Open()
                ErrorHandler.LogError(New Exception("Database connection opened successfully"), "GetAllWoodSpecies")

                Using cmd As New SQLiteCommand("SELECT * FROM WoodSpecies ORDER BY CommonName", conn)
                    Using reader = cmd.ExecuteReader()
                        Dim count = 0
                        While reader.Read()
                            species.Add(MapReaderToWoodSpecies(reader))
                            count += 1
                        End While
                        ErrorHandler.LogError(New Exception($"Read {count} species from database"), "GetAllWoodSpecies")
                    End Using
                End Using
            End Using

            ErrorHandler.LogError(New Exception($"Returning {species.Count} species"), "GetAllWoodSpecies")
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetAllWoodSpecies")
        End Try

        Return species
    End Function

    ''' <summary>
    ''' Gets wood species by type (Hardwood/Softwood)
    ''' </summary>
    Public Function GetWoodSpeciesByType(woodType As String) As List(Of WoodPropertiesData)
        Dim species As New List(Of WoodPropertiesData)

        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT * FROM WoodSpecies WHERE WoodType = @Type ORDER BY CommonName", conn)
                    cmd.Parameters.AddWithValue("@Type", woodType)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            species.Add(MapReaderToWoodSpecies(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetWoodSpeciesByType")
        End Try

        Return species
    End Function

    ''' <summary>
    ''' Searches wood species by name
    ''' </summary>
    Public Function SearchWoodSpecies(searchTerm As String) As List(Of WoodPropertiesData)
        Dim species As New List(Of WoodPropertiesData)

        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT * FROM WoodSpecies WHERE CommonName LIKE @Search ORDER BY CommonName", conn)
                    cmd.Parameters.AddWithValue("@Search", $"%{searchTerm}%")
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            species.Add(MapReaderToWoodSpecies(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SearchWoodSpecies")
        End Try

        Return species
    End Function

    ''' <summary>
    ''' Adds a new wood species to database
    ''' </summary>
    Public Function AddWoodSpecies(species As WoodPropertiesData) As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    INSERT INTO WoodSpecies (
                        CommonName, ScientificName, WoodType,
                        JankaHardness, SpecificGravity, Density, MoistureContent,
                        ShrinkageRadial, ShrinkageTangential,
                        TypicalUses, Workability, Cautions, Notes,
                        IsUserAdded
                    ) VALUES (
                        @CommonName, @ScientificName, @WoodType,
                        @JankaHardness, @SpecificGravity, @Density, @MoistureContent,
                        @ShrinkageRadial, @ShrinkageTangential,
                        @TypicalUses, @Workability, @Cautions, @Notes,
                        @IsUserAdded
                    )", conn)

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
                    cmd.Parameters.AddWithValue("@IsUserAdded", 1) ' User-added species

                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "AddWoodSpecies")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Maps SQLite reader to WoodPropertiesData object
    ''' </summary>
    Private Shared Function MapReaderToWoodSpecies(reader As SQLiteDataReader) As WoodPropertiesData
        Return New WoodPropertiesData With {
            .CommonName = reader("CommonName").ToString(),
            .ScientificName = If(IsDBNull(reader("ScientificName")), "", reader("ScientificName").ToString()),
            .WoodType = reader("WoodType").ToString(),
            .JankaHardness = Convert.ToInt32(reader("JankaHardness")),
            .SpecificGravity = Convert.ToDouble(reader("SpecificGravity")),
            .Density = Convert.ToInt32(reader("Density")),
            .MoistureContent = Convert.ToDouble(reader("MoistureContent")),
            .ShrinkageRadial = Convert.ToDouble(reader("ShrinkageRadial")),
            .ShrinkageTangential = Convert.ToDouble(reader("ShrinkageTangential")),
            .TypicalUses = If(IsDBNull(reader("TypicalUses")), "", reader("TypicalUses").ToString()),
            .Workability = If(IsDBNull(reader("Workability")), "", reader("Workability").ToString()),
            .Cautions = If(IsDBNull(reader("Cautions")), "", reader("Cautions").ToString()),
            .Notes = If(IsDBNull(reader("Notes")), "", reader("Notes").ToString())
        }
    End Function

#End Region

#Region "Help Content Methods"

    ''' <summary>
    ''' Data model for help content from database
    ''' </summary>
    Public Class HelpContentData
        Public Property HelpID As Integer
        Public Property ModuleName As String
        Public Property Title As String
        Public Property Content As String
        Public Property Keywords As String
        Public Property Category As String
        Public Property SortOrder As Integer
        Public Property Version As String
        Public Property LastUpdated As DateTime
    End Class

    ''' <summary>
    ''' Gets all help topics (lightweight - no content body)
    ''' </summary>
    Public Function GetHelpTopics() As List(Of HelpContentData)
        Dim topics As New List(Of HelpContentData)
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand(
                    "SELECT HelpID, ModuleName, Title, Keywords, Category, SortOrder, Version, LastUpdated FROM HelpContent ORDER BY Category, SortOrder, Title", conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            topics.Add(New HelpContentData With {
                                .HelpID = Convert.ToInt32(reader("HelpID")),
                                .ModuleName = reader("ModuleName").ToString(),
                                .Title = reader("Title").ToString(),
                                .Keywords = If(IsDBNull(reader("Keywords")), "", reader("Keywords").ToString()),
                                .Category = If(IsDBNull(reader("Category")), "", reader("Category").ToString()),
                                .SortOrder = Convert.ToInt32(reader("SortOrder")),
                                .Version = If(IsDBNull(reader("Version")), "1.0", reader("Version").ToString()),
                                .LastUpdated = If(IsDBNull(reader("LastUpdated")), DateTime.Now, Convert.ToDateTime(reader("LastUpdated")))
                            })
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetHelpTopics")
        End Try
        Return topics
    End Function

    ''' <summary>
    ''' Gets help content by module name (topic key)
    ''' </summary>
    Public Function GetHelpContent(moduleName As String) As HelpContentData
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand(
                    "SELECT * FROM HelpContent WHERE ModuleName = @ModuleName", conn)
                    cmd.Parameters.AddWithValue("@ModuleName", moduleName)
                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return New HelpContentData With {
                                .HelpID = Convert.ToInt32(reader("HelpID")),
                                .ModuleName = reader("ModuleName").ToString(),
                                .Title = reader("Title").ToString(),
                                .Content = reader("Content").ToString(),
                                .Keywords = If(IsDBNull(reader("Keywords")), "", reader("Keywords").ToString()),
                                .Category = If(IsDBNull(reader("Category")), "", reader("Category").ToString()),
                                .SortOrder = Convert.ToInt32(reader("SortOrder")),
                                .Version = If(IsDBNull(reader("Version")), "1.0", reader("Version").ToString()),
                                .LastUpdated = If(IsDBNull(reader("LastUpdated")), DateTime.Now, Convert.ToDateTime(reader("LastUpdated")))
                            }
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetHelpContent")
        End Try
        Return Nothing
    End Function

    ''' <summary>
    ''' Searches help content by keyword across title, content, and keywords fields
    ''' </summary>
    Public Function SearchHelpContent(searchTerm As String) As List(Of HelpContentData)
        Dim results As New List(Of HelpContentData)
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand(
                    "SELECT HelpID, ModuleName, Title, Keywords, Category, SortOrder, Version, LastUpdated FROM HelpContent " &
                    "WHERE Title LIKE @Search OR Content LIKE @Search OR Keywords LIKE @Search " &
                    "ORDER BY CASE WHEN Title LIKE @Search THEN 0 WHEN Keywords LIKE @Search THEN 1 ELSE 2 END, SortOrder", conn)
                    cmd.Parameters.AddWithValue("@Search", $"%{searchTerm}%")
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            results.Add(New HelpContentData With {
                                .HelpID = Convert.ToInt32(reader("HelpID")),
                                .ModuleName = reader("ModuleName").ToString(),
                                .Title = reader("Title").ToString(),
                                .Keywords = If(IsDBNull(reader("Keywords")), "", reader("Keywords").ToString()),
                                .Category = If(IsDBNull(reader("Category")), "", reader("Category").ToString()),
                                .SortOrder = Convert.ToInt32(reader("SortOrder")),
                                .Version = If(IsDBNull(reader("Version")), "1.0", reader("Version").ToString()),
                                .LastUpdated = If(IsDBNull(reader("LastUpdated")), DateTime.Now, Convert.ToDateTime(reader("LastUpdated")))
                            })
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SearchHelpContent")
        End Try
        Return results
    End Function

    ''' <summary>
    ''' Gets help topics by category
    ''' </summary>
    Public Function GetHelpByCategory(category As String) As List(Of HelpContentData)
        Dim results As New List(Of HelpContentData)
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand(
                    "SELECT HelpID, ModuleName, Title, Keywords, Category, SortOrder, Version, LastUpdated FROM HelpContent " &
                    "WHERE Category = @Category ORDER BY SortOrder, Title", conn)
                    cmd.Parameters.AddWithValue("@Category", category)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            results.Add(New HelpContentData With {
                                .HelpID = Convert.ToInt32(reader("HelpID")),
                                .ModuleName = reader("ModuleName").ToString(),
                                .Title = reader("Title").ToString(),
                                .Keywords = If(IsDBNull(reader("Keywords")), "", reader("Keywords").ToString()),
                                .Category = If(IsDBNull(reader("Category")), "", reader("Category").ToString()),
                                .SortOrder = Convert.ToInt32(reader("SortOrder")),
                                .Version = If(IsDBNull(reader("Version")), "1.0", reader("Version").ToString()),
                                .LastUpdated = If(IsDBNull(reader("LastUpdated")), DateTime.Now, Convert.ToDateTime(reader("LastUpdated")))
                            })
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetHelpByCategory")
        End Try
        Return results
    End Function

    ''' <summary>
    ''' Inserts or updates help content in the database
    ''' </summary>
    Public Function SaveHelpContent(data As HelpContentData) As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand(
                    "INSERT OR REPLACE INTO HelpContent (ModuleName, Title, Content, Keywords, Category, SortOrder, Version, LastUpdated) " &
                    "VALUES (@ModuleName, @Title, @Content, @Keywords, @Category, @SortOrder, @Version, CURRENT_TIMESTAMP)", conn)
                    cmd.Parameters.AddWithValue("@ModuleName", data.ModuleName)
                    cmd.Parameters.AddWithValue("@Title", data.Title)
                    cmd.Parameters.AddWithValue("@Content", data.Content)
                    cmd.Parameters.AddWithValue("@Keywords", If(String.IsNullOrEmpty(data.Keywords), CObj(DBNull.Value), CObj(data.Keywords)))
                    cmd.Parameters.AddWithValue("@Category", If(String.IsNullOrEmpty(data.Category), CObj(DBNull.Value), CObj(data.Category)))
                    cmd.Parameters.AddWithValue("@SortOrder", data.SortOrder)
                    cmd.Parameters.AddWithValue("@Version", If(String.IsNullOrEmpty(data.Version), "1.0", data.Version))
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SaveHelpContent")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Checks if help content has been seeded
    ''' </summary>
    Public Function IsHelpContentSeeded() As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT COUNT(*) FROM HelpContent", conn)
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "IsHelpContentSeeded")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Batch inserts help content within a transaction (for seeding)
    ''' </summary>
    Public Function BulkInsertHelpContent(items As List(Of HelpContentData)) As Integer
        Dim successCount = 0
        Try
            Using conn = GetConnection()
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        For Each item In items
                            Using cmd As New SQLiteCommand(
                                "INSERT OR REPLACE INTO HelpContent (ModuleName, Title, Content, Keywords, Category, SortOrder, Version, LastUpdated) " &
                                "VALUES (@ModuleName, @Title, @Content, @Keywords, @Category, @SortOrder, @Version, CURRENT_TIMESTAMP)", conn, transaction)
                                cmd.Parameters.AddWithValue("@ModuleName", item.ModuleName)
                                cmd.Parameters.AddWithValue("@Title", item.Title)
                                cmd.Parameters.AddWithValue("@Content", item.Content)
                                cmd.Parameters.AddWithValue("@Keywords", If(String.IsNullOrEmpty(item.Keywords), CObj(DBNull.Value), CObj(item.Keywords)))
                                cmd.Parameters.AddWithValue("@Category", If(String.IsNullOrEmpty(item.Category), CObj(DBNull.Value), CObj(item.Category)))
                                cmd.Parameters.AddWithValue("@SortOrder", item.SortOrder)
                                cmd.Parameters.AddWithValue("@Version", If(String.IsNullOrEmpty(item.Version), "1.0", item.Version))
                                cmd.ExecuteNonQuery()
                                successCount += 1
                            End Using
                        Next
                        transaction.Commit()
                    Catch ex As Exception
                        transaction.Rollback()
                        ErrorHandler.LogError(ex, "BulkInsertHelpContent - Transaction failed")
                        Return 0
                    End Try
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BulkInsertHelpContent")
        End Try
        Return successCount
    End Function

#End Region

#Region "User Preferences Methods"

    ''' <summary>
    ''' Gets a user preference value
    ''' </summary>
    Public Function GetPreference(key As String, Optional defaultValue As String = "") As String
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT PrefValue FROM UserPreferences WHERE PrefKey = @Key", conn)
                    cmd.Parameters.AddWithValue("@Key", key)
                    Dim result = cmd.ExecuteScalar()
                    Return If(result IsNot Nothing, result.ToString(), defaultValue)
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetPreference")
            Return defaultValue
        End Try
    End Function

    ''' <summary>
    ''' Saves a user preference
    ''' </summary>
    Public Sub SavePreference(key As String, value As String, Optional dataType As String = "String", Optional category As String = "General")
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    INSERT OR REPLACE INTO UserPreferences (PrefKey, PrefValue, DataType, Category, LastModified)
                    VALUES (@Key, @Value, @DataType, @Category, CURRENT_TIMESTAMP)", conn)

                    cmd.Parameters.AddWithValue("@Key", key)
                    cmd.Parameters.AddWithValue("@Value", value)
                    cmd.Parameters.AddWithValue("@DataType", dataType)
                    cmd.Parameters.AddWithValue("@Category", category)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SavePreference")
        End Try
    End Sub

    ' --- Type-safe preference helpers (Phase 5) ---

    ''' <summary>
    ''' Gets a Boolean preference value
    ''' </summary>
    Public Function GetBoolPreference(key As String, Optional defaultValue As Boolean = False) As Boolean
        Dim raw = GetPreference(key, defaultValue.ToString())
        Dim result As Boolean
        Return If(Boolean.TryParse(raw, result), result, defaultValue)
    End Function

    ''' <summary>
    ''' Gets an Integer preference value
    ''' </summary>
    Public Function GetIntPreference(key As String, Optional defaultValue As Integer = 0) As Integer
        Dim raw = GetPreference(key, defaultValue.ToString())
        Dim result As Integer
        Return If(Integer.TryParse(raw, result), result, defaultValue)
    End Function

    ''' <summary>
    ''' Gets a Double preference value
    ''' </summary>
    Public Function GetDoublePreference(key As String, Optional defaultValue As Double = 0.0) As Double
        Dim raw = GetPreference(key, defaultValue.ToString())
        Dim result As Double
        Return If(Double.TryParse(raw, result), result, defaultValue)
    End Function

    ''' <summary>
    ''' Checks if any user preferences have been saved
    ''' </summary>
    Public Function HasPreferences() As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT COUNT(*) FROM UserPreferences", conn)
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "HasPreferences")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Gets the TimesRun counter from database
    ''' </summary>
    Public Function GetTimesRun() As Integer
        Try
            Dim raw = GetPreference("TimesRun", "0")
            Dim result As Integer
            Return If(Integer.TryParse(raw, result), result, 0)
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetTimesRun")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Increments and saves the TimesRun counter
    ''' </summary>
    Public Function IncrementTimesRun() As Integer
        Try
            Dim currentCount = GetTimesRun()
            currentCount += 1
            SavePreference("TimesRun", currentCount.ToString(), "Integer", "System")
            Return currentCount
        Catch ex As Exception
            ErrorHandler.LogError(ex, "IncrementTimesRun")
            Return 0
        End Try
    End Function

#End Region

#Region "Calculation History Methods"

    ''' <summary>
    ''' Saves a calculation to history
    ''' </summary>
    Public Function SaveCalculation(calculatorType As String, calculationName As String, inputParameters As String, results As String, Optional notes As String = "") As Integer
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    INSERT INTO CalculationHistory (
                        CalculatorType, CalculationName, InputParameters, Results, Notes, DateCalculated, IsFavorite
                    ) VALUES (
                        @Type, @Name, @Inputs, @Results, @Notes, CURRENT_TIMESTAMP, 0
                    );
                    SELECT last_insert_rowid();", conn)

                    cmd.Parameters.AddWithValue("@Type", calculatorType)
                    cmd.Parameters.AddWithValue("@Name", If(String.IsNullOrEmpty(calculationName), CObj(DBNull.Value), CObj(calculationName)))
                    cmd.Parameters.AddWithValue("@Inputs", inputParameters)
                    cmd.Parameters.AddWithValue("@Results", results)
                    cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(notes), CObj(DBNull.Value), CObj(notes)))

                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SaveCalculation")
            Return -1
        End Try
    End Function

    ''' <summary>
    ''' Gets calculation history for a specific calculator type
    ''' </summary>
    Public Function GetCalculationHistory(calculatorType As String, Optional limit As Integer = 50) As List(Of CalculationHistory)
        Dim history As New List(Of CalculationHistory)

        Try
            Using conn = GetConnection()
                conn.Open()
                Dim sql = "SELECT * FROM CalculationHistory WHERE CalculatorType = @Type ORDER BY DateCalculated DESC"
                If limit > 0 Then
                    sql &= $" LIMIT {limit}"
                End If

                Using cmd As New SQLiteCommand(sql, conn)
                    cmd.Parameters.AddWithValue("@Type", calculatorType)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            history.Add(MapReaderToCalculationHistory(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetCalculationHistory")
        End Try

        Return history
    End Function

    ''' <summary>
    ''' Gets all favorite calculations
    ''' </summary>
    Public Function GetFavoriteCalculations() As List(Of CalculationHistory)
        Dim favorites As New List(Of CalculationHistory)

        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT * FROM CalculationHistory WHERE IsFavorite = 1 ORDER BY CalculatorType, DateCalculated DESC", conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            favorites.Add(MapReaderToCalculationHistory(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetFavoriteCalculations")
        End Try

        Return favorites
    End Function

    ''' <summary>
    ''' Toggles favorite status of a calculation
    ''' </summary>
    Public Function ToggleFavorite(historyId As Integer) As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("UPDATE CalculationHistory SET IsFavorite = NOT IsFavorite WHERE HistoryID = @ID", conn)
                    cmd.Parameters.AddWithValue("@ID", historyId)
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "ToggleFavorite")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Deletes a calculation from history
    ''' </summary>
    Public Function DeleteCalculation(historyId As Integer) As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("DELETE FROM CalculationHistory WHERE HistoryID = @ID", conn)
                    cmd.Parameters.AddWithValue("@ID", historyId)
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DeleteCalculation")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Updates calculation name and notes
    ''' </summary>
    Public Function UpdateCalculation(historyId As Integer, calculationName As String, notes As String) As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("UPDATE CalculationHistory SET CalculationName = @Name, Notes = @Notes WHERE HistoryID = @ID", conn)
                    cmd.Parameters.AddWithValue("@ID", historyId)
                    cmd.Parameters.AddWithValue("@Name", If(String.IsNullOrEmpty(calculationName), CObj(DBNull.Value), CObj(calculationName)))
                    cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(notes), CObj(DBNull.Value), CObj(notes)))
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "UpdateCalculation")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Gets count of calculations by type
    ''' </summary>
    Public Function GetCalculationCount(calculatorType As String) As Integer
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT COUNT(*) FROM CalculationHistory WHERE CalculatorType = @Type", conn)
                    cmd.Parameters.AddWithValue("@Type", calculatorType)
                    Return Convert.ToInt32(cmd.ExecuteScalar())
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetCalculationCount")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Maps SQLite reader to CalculationHistory object
    ''' </summary>
    Private Shared Function MapReaderToCalculationHistory(reader As SQLiteDataReader) As CalculationHistory
        Return New CalculationHistory With {
            .HistoryID = Convert.ToInt32(reader("HistoryID")),
            .CalculatorType = reader("CalculatorType").ToString(),
            .CalculationName = If(IsDBNull(reader("CalculationName")), "", reader("CalculationName").ToString()),
            .InputParameters = reader("InputParameters").ToString(),
            .Results = reader("Results").ToString(),
            .DateCalculated = Convert.ToDateTime(reader("DateCalculated")),
            .IsFavorite = Convert.ToBoolean(reader("IsFavorite")),
            .Notes = If(IsDBNull(reader("Notes")), "", reader("Notes").ToString())
        }
    End Function

#End Region

#Region "Joinery Types Methods (Phase 7.1)"

    ''' <summary>
    ''' Gets all joinery types from database
    ''' </summary>
    Public Function GetAllJoineryTypes() As List(Of JoineryType)
        Dim types As New List(Of JoineryType)

        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT * FROM JoineryTypes ORDER BY Category, Name", conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            types.Add(MapReaderToJoineryType(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetAllJoineryTypes")
        End Try

        Return types
    End Function

    ''' <summary>
    ''' Gets joinery types by category
    ''' </summary>
    Public Function GetJoineryTypesByCategory(category As String) As List(Of JoineryType)
        Dim types As New List(Of JoineryType)

        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT * FROM JoineryTypes WHERE Category = @Category ORDER BY Name", conn)
                    cmd.Parameters.AddWithValue("@Category", category)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            types.Add(MapReaderToJoineryType(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetJoineryTypesByCategory")
        End Try

        Return types
    End Function

    ''' <summary>
    ''' Searches joinery types by name or description
    ''' </summary>
    Public Function SearchJoineryTypes(searchTerm As String) As List(Of JoineryType)
        Dim types As New List(Of JoineryType)

        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT * FROM JoineryTypes WHERE Name LIKE @Search OR Description LIKE @Search OR TypicalUses LIKE @Search ORDER BY Name", conn)
                    cmd.Parameters.AddWithValue("@Search", $"%{searchTerm}%")
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            types.Add(MapReaderToJoineryType(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SearchJoineryTypes")
        End Try

        Return types
    End Function

    ''' <summary>
    ''' Adds new joinery type
    ''' </summary>
    Public Function AddJoineryType(joinery As JoineryType) As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    INSERT INTO JoineryTypes (Name, Category, StrengthRating, DifficultyLevel, Description,
                                            TypicalUses, RequiredTools, StrengthCharacteristics, GlueRequired,
                                            ReinforcementOptions, HistoricalNotes, DiagramFileName, IsUserAdded)
                    VALUES (@Name, @Category, @StrengthRating, @DifficultyLevel, @Description,
                           @TypicalUses, @RequiredTools, @StrengthCharacteristics, @GlueRequired,
                           @ReinforcementOptions, @HistoricalNotes, @DiagramFileName, @IsUserAdded)", conn)

                    cmd.Parameters.AddWithValue("@Name", joinery.Name)
                    cmd.Parameters.AddWithValue("@Category", If(joinery.Category, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@StrengthRating", joinery.StrengthRating)
                    cmd.Parameters.AddWithValue("@DifficultyLevel", If(joinery.DifficultyLevel, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@Description", If(joinery.Description, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@TypicalUses", If(joinery.TypicalUses, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@RequiredTools", If(joinery.RequiredTools, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@StrengthCharacteristics", If(joinery.StrengthCharacteristics, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@GlueRequired", joinery.GlueRequired)
                    cmd.Parameters.AddWithValue("@ReinforcementOptions", If(joinery.ReinforcementOptions, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@HistoricalNotes", If(joinery.HistoricalNotes, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@DiagramFileName", If(joinery.DiagramFileName, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@IsUserAdded", joinery.IsUserAdded)

                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "AddJoineryType")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Maps SQLite reader to JoineryType object
    ''' </summary>
    Private Shared Function MapReaderToJoineryType(reader As SQLiteDataReader) As JoineryType
        Return New JoineryType With {
            .JoineryID = Convert.ToInt32(reader("JoineryID")),
            .Name = reader("Name").ToString(),
            .Category = If(IsDBNull(reader("Category")), "", reader("Category").ToString()),
            .StrengthRating = If(IsDBNull(reader("StrengthRating")), 0, Convert.ToInt32(reader("StrengthRating"))),
            .DifficultyLevel = If(IsDBNull(reader("DifficultyLevel")), "", reader("DifficultyLevel").ToString()),
            .Description = If(IsDBNull(reader("Description")), "", reader("Description").ToString()),
            .TypicalUses = If(IsDBNull(reader("TypicalUses")), "", reader("TypicalUses").ToString()),
            .RequiredTools = If(IsDBNull(reader("RequiredTools")), "", reader("RequiredTools").ToString()),
            .StrengthCharacteristics = If(IsDBNull(reader("StrengthCharacteristics")), "", reader("StrengthCharacteristics").ToString()),
            .GlueRequired = Convert.ToBoolean(reader("GlueRequired")),
            .ReinforcementOptions = If(IsDBNull(reader("ReinforcementOptions")), "", reader("ReinforcementOptions").ToString()),
            .HistoricalNotes = If(IsDBNull(reader("HistoricalNotes")), "", reader("HistoricalNotes").ToString()),
            .DiagramFileName = If(IsDBNull(reader("DiagramFileName")), "", reader("DiagramFileName").ToString()),
            .IsUserAdded = Convert.ToBoolean(reader("IsUserAdded")),
            .DateAdded = Convert.ToDateTime(reader("DateAdded"))
        }
    End Function

#End Region

#Region "Hardware Standards Methods (Phase 7.2)"

    ''' <summary>
    ''' Gets all hardware standards from database
    ''' </summary>
    Public Function GetAllHardwareStandards() As List(Of HardwareStandard)
        Dim hardware As New List(Of HardwareStandard)

        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT * FROM HardwareStandards ORDER BY Category, Type", conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            hardware.Add(MapReaderToHardwareStandard(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetAllHardwareStandards")
        End Try

        Return hardware
    End Function

    ''' <summary>
    ''' Gets hardware standards by category
    ''' </summary>
    Public Function GetHardwareByCategory(category As String) As List(Of HardwareStandard)
        Dim hardware As New List(Of HardwareStandard)

        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT * FROM HardwareStandards WHERE Category = @Category ORDER BY Type", conn)
                    cmd.Parameters.AddWithValue("@Category", category)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            hardware.Add(MapReaderToHardwareStandard(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetHardwareByCategory")
        End Try

        Return hardware
    End Function

    ''' <summary>
    ''' Searches hardware standards
    ''' </summary>
    Public Function SearchHardware(searchTerm As String) As List(Of HardwareStandard)
        Dim hardware As New List(Of HardwareStandard)

        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT * FROM HardwareStandards WHERE Type LIKE @Search OR Description LIKE @Search OR Brand LIKE @Search ORDER BY Category, Type", conn)
                    cmd.Parameters.AddWithValue("@Search", $"%{searchTerm}%")
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            hardware.Add(MapReaderToHardwareStandard(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SearchHardware")
        End Try

        Return hardware
    End Function

    ''' <summary>
    ''' Adds new hardware standard
    ''' </summary>
    Public Function AddHardwareStandard(hardware As HardwareStandard) As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    INSERT INTO HardwareStandards (Category, Type, Brand, PartNumber, Description,
                                                  Dimensions, MountingRequirements, WeightCapacity, TypicalUses,
                                                  InstallationNotes, PurchaseLink, IsUserAdded)
                    VALUES (@Category, @Type, @Brand, @PartNumber, @Description,
                           @Dimensions, @MountingRequirements, @WeightCapacity, @TypicalUses,
                           @InstallationNotes, @PurchaseLink, @IsUserAdded)", conn)

                    cmd.Parameters.AddWithValue("@Category", hardware.Category)
                    cmd.Parameters.AddWithValue("@Type", hardware.Type)
                    cmd.Parameters.AddWithValue("@Brand", If(hardware.Brand, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@PartNumber", If(hardware.PartNumber, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@Description", If(hardware.Description, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@Dimensions", If(hardware.Dimensions, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@MountingRequirements", If(hardware.MountingRequirements, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@WeightCapacity", If(hardware.WeightCapacity, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@TypicalUses", If(hardware.TypicalUses, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@InstallationNotes", If(hardware.InstallationNotes, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@PurchaseLink", If(hardware.PurchaseLink, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@IsUserAdded", hardware.IsUserAdded)

                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "AddHardwareStandard")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Maps SQLite reader to HardwareStandard object
    ''' </summary>
    Private Shared Function MapReaderToHardwareStandard(reader As SQLiteDataReader) As HardwareStandard
        Return New HardwareStandard With {
            .HardwareID = Convert.ToInt32(reader("HardwareID")),
            .Category = reader("Category").ToString(),
            .Type = reader("Type").ToString(),
            .Brand = If(IsDBNull(reader("Brand")), "", reader("Brand").ToString()),
            .PartNumber = If(IsDBNull(reader("PartNumber")), "", reader("PartNumber").ToString()),
            .Description = If(IsDBNull(reader("Description")), "", reader("Description").ToString()),
            .Dimensions = If(IsDBNull(reader("Dimensions")), "", reader("Dimensions").ToString()),
            .MountingRequirements = If(IsDBNull(reader("MountingRequirements")), "", reader("MountingRequirements").ToString()),
            .WeightCapacity = If(IsDBNull(reader("WeightCapacity")), "", reader("WeightCapacity").ToString()),
            .TypicalUses = If(IsDBNull(reader("TypicalUses")), "", reader("TypicalUses").ToString()),
            .InstallationNotes = If(IsDBNull(reader("InstallationNotes")), "", reader("InstallationNotes").ToString()),
            .PurchaseLink = If(IsDBNull(reader("PurchaseLink")), "", reader("PurchaseLink").ToString()),
            .IsUserAdded = Convert.ToBoolean(reader("IsUserAdded")),
            .DateAdded = Convert.ToDateTime(reader("DateAdded"))
        }
    End Function

#End Region

#Region "WoodCosts CRUD Methods (Phase 7.3)"

    ''' <summary>
    ''' Gets all active wood costs from database
    ''' </summary>
    Public Function GetAllWoodCosts() As List(Of WoodCost)
        Dim woodCosts As New List(Of WoodCost)()
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT * FROM WoodCosts WHERE Active = 1 ORDER BY WoodName, Thickness", conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            woodCosts.Add(MapReaderToWoodCost(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetAllWoodCosts")
        End Try
        Return woodCosts
    End Function

    ''' <summary>
    ''' Adds a new wood cost entry
    ''' </summary>
    Public Function AddWoodCost(woodCost As WoodCost) As Boolean
        ArgumentNullException.ThrowIfNull(woodCost)
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("INSERT INTO WoodCosts (Thickness, WoodName, CostPerBoardFoot, Active, IsUserAdded) 
                                               VALUES (@Thickness, @WoodName, @CostPerBoardFoot, @Active, @IsUserAdded)", conn)
                    cmd.Parameters.AddWithValue("@Thickness", woodCost.Thickness)
                    cmd.Parameters.AddWithValue("@WoodName", woodCost.WoodName)
                    cmd.Parameters.AddWithValue("@CostPerBoardFoot", woodCost.CostPerBoardFoot)
                    cmd.Parameters.AddWithValue("@Active", woodCost.Active)
                    cmd.Parameters.AddWithValue("@IsUserAdded", woodCost.IsUserAdded)
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "AddWoodCost")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Updates an existing wood cost entry
    ''' </summary>
    Public Function UpdateWoodCost(woodCost As WoodCost) As Boolean
        ArgumentNullException.ThrowIfNull(woodCost)
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("UPDATE WoodCosts SET Thickness = @Thickness, WoodName = @WoodName, 
                                               CostPerBoardFoot = @CostPerBoardFoot, Active = @Active, 
                                               LastModified = CURRENT_TIMESTAMP WHERE WoodCostID = @WoodCostID", conn)
                    cmd.Parameters.AddWithValue("@WoodCostID", woodCost.WoodCostID)
                    cmd.Parameters.AddWithValue("@Thickness", woodCost.Thickness)
                    cmd.Parameters.AddWithValue("@WoodName", woodCost.WoodName)
                    cmd.Parameters.AddWithValue("@CostPerBoardFoot", woodCost.CostPerBoardFoot)
                    cmd.Parameters.AddWithValue("@Active", woodCost.Active)
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "UpdateWoodCost")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Soft deletes a wood cost entry (sets Active = 0)
    ''' </summary>
    Public Function DeleteWoodCost(woodCostID As Integer) As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("UPDATE WoodCosts SET Active = 0, LastModified = CURRENT_TIMESTAMP 
                                               WHERE WoodCostID = @WoodCostID", conn)
                    cmd.Parameters.AddWithValue("@WoodCostID", woodCostID)
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DeleteWoodCost")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Maps SQLite reader to WoodCost object
    ''' </summary>
    Private Shared Function MapReaderToWoodCost(reader As SQLiteDataReader) As WoodCost
        Return New WoodCost With {
            .WoodCostID = Convert.ToInt32(reader("WoodCostID")),
            .Thickness = reader("Thickness").ToString(),
            .WoodName = reader("WoodName").ToString(),
            .CostPerBoardFoot = Convert.ToDouble(reader("CostPerBoardFoot")),
            .Active = Convert.ToBoolean(reader("Active")),
            .IsUserAdded = Convert.ToBoolean(reader("IsUserAdded")),
            .DateAdded = Convert.ToDateTime(reader("DateAdded")),
            .LastModified = Convert.ToDateTime(reader("LastModified"))
        }
    End Function

#End Region

#Region "EpoxyCosts CRUD Methods (Phase 7.3)"

    ''' <summary>
    ''' Gets all active epoxy costs from database
    ''' </summary>
    Public Function GetAllEpoxyCosts() As List(Of EpoxyCost)
        Dim epoxyCosts As New List(Of EpoxyCost)()
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT * FROM EpoxyCosts WHERE Active = 1 ORDER BY Brand, Type", conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            epoxyCosts.Add(MapReaderToEpoxyCost(reader))
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetAllEpoxyCosts")
        End Try
        Return epoxyCosts
    End Function

    ''' <summary>
    ''' Adds a new epoxy cost entry
    ''' </summary>
    Public Function AddEpoxyCost(epoxyCost As EpoxyCost) As Boolean
        ArgumentNullException.ThrowIfNull(epoxyCost)
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("INSERT INTO EpoxyCosts (Brand, Type, CostPerGallon, DisplayText, Active, IsUserAdded) 
                                               VALUES (@Brand, @Type, @CostPerGallon, @DisplayText, @Active, @IsUserAdded)", conn)
                    cmd.Parameters.AddWithValue("@Brand", epoxyCost.Brand)
                    cmd.Parameters.AddWithValue("@Type", epoxyCost.Type)
                    cmd.Parameters.AddWithValue("@CostPerGallon", epoxyCost.CostPerGallon)
                    cmd.Parameters.AddWithValue("@DisplayText", If(epoxyCost.DisplayText, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@Active", epoxyCost.Active)
                    cmd.Parameters.AddWithValue("@IsUserAdded", epoxyCost.IsUserAdded)
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "AddEpoxyCost")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Updates an existing epoxy cost entry
    ''' </summary>
    Public Function UpdateEpoxyCost(epoxyCost As EpoxyCost) As Boolean
        ArgumentNullException.ThrowIfNull(epoxyCost)
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("UPDATE EpoxyCosts SET Brand = @Brand, Type = @Type, 
                                               CostPerGallon = @CostPerGallon, DisplayText = @DisplayText, 
                                               Active = @Active, LastModified = CURRENT_TIMESTAMP 
                                               WHERE EpoxyCostID = @EpoxyCostID", conn)
                    cmd.Parameters.AddWithValue("@EpoxyCostID", epoxyCost.EpoxyCostID)
                    cmd.Parameters.AddWithValue("@Brand", epoxyCost.Brand)
                    cmd.Parameters.AddWithValue("@Type", epoxyCost.Type)
                    cmd.Parameters.AddWithValue("@CostPerGallon", epoxyCost.CostPerGallon)
                    cmd.Parameters.AddWithValue("@DisplayText", If(epoxyCost.DisplayText, CObj(DBNull.Value)))
                    cmd.Parameters.AddWithValue("@Active", epoxyCost.Active)
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "UpdateEpoxyCost")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Soft deletes an epoxy cost entry (sets Active = 0)
    ''' </summary>
    Public Function DeleteEpoxyCost(epoxyCostID As Integer) As Boolean
        Try
            Using conn = GetConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("UPDATE EpoxyCosts SET Active = 0, LastModified = CURRENT_TIMESTAMP 
                                               WHERE EpoxyCostID = @EpoxyCostID", conn)
                    cmd.Parameters.AddWithValue("@EpoxyCostID", epoxyCostID)
                    cmd.ExecuteNonQuery()
                    Return True
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DeleteEpoxyCost")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Maps SQLite reader to EpoxyCost object
    ''' </summary>
    Private Shared Function MapReaderToEpoxyCost(reader As SQLiteDataReader) As EpoxyCost
        Return New EpoxyCost With {
            .EpoxyCostID = Convert.ToInt32(reader("EpoxyCostID")),
            .Brand = reader("Brand").ToString(),
            .Type = reader("Type").ToString(),
            .CostPerGallon = Convert.ToDouble(reader("CostPerGallon")),
            .DisplayText = If(IsDBNull(reader("DisplayText")), "", reader("DisplayText").ToString()),
            .Active = Convert.ToBoolean(reader("Active")),
            .IsUserAdded = Convert.ToBoolean(reader("IsUserAdded")),
            .DateAdded = Convert.ToDateTime(reader("DateAdded")),
            .LastModified = Convert.ToDateTime(reader("LastModified"))
        }
    End Function

#End Region

End Class
