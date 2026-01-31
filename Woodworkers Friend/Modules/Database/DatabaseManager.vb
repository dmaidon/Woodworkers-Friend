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
    ''' Gets a preference value with optional default
    ''' </summary>
    Public Function GetPreference(key As String, Optional defaultValue As String = "") As String
        Return UserData.GetPreference(key, defaultValue)
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

    ''' <summary>
    ''' Gets the TimesRun counter from preferences
    ''' </summary>
    Public Function GetTimesRun() As Integer
        Return UserData.GetTimesRun()
    End Function

    ''' <summary>
    ''' Increments and saves the TimesRun counter
    ''' </summary>
    Public Function IncrementTimesRun() As Integer
        Return UserData.IncrementTimesRun()
    End Function

#End Region

#Region "Unified API - Joinery Types (Routes to ReferenceDataManager)"

    ''' <summary>
    ''' Gets all joinery types
    ''' </summary>
    Public Function GetAllJoineryTypes() As List(Of JoineryType)
        Return Reference.GetAllJoineryTypes()
    End Function

    ''' <summary>
    ''' Adds a new joinery type (user-added to Reference.db)
    ''' </summary>
    Public Function AddJoineryType(joinery As JoineryType) As Boolean
        Return Reference.AddJoineryType(joinery)
    End Function

#End Region

#Region "Unified API - Hardware Standards (Routes to ReferenceDataManager)"

    ''' <summary>
    ''' Gets all hardware standards
    ''' </summary>
    Public Function GetAllHardwareStandards() As List(Of HardwareStandard)
        Return Reference.GetAllHardwareStandards()
    End Function

    ''' <summary>
    ''' Adds a new hardware standard (user-added to Reference.db)
    ''' </summary>
    Public Function AddHardwareStandard(hardware As HardwareStandard) As Boolean
        Return Reference.AddHardwareStandard(hardware)
    End Function

#End Region

#Region "Unified API - Cost Data (Routes to UserDataManager)"

    ''' <summary>
    ''' Gets all wood costs
    ''' </summary>
    Public Function GetAllWoodCosts() As List(Of WoodCost)
        Return UserData.GetAllWoodCosts()
    End Function

    ''' <summary>
    ''' Adds a new wood cost entry
    ''' </summary>
    Public Function AddWoodCost(woodCost As WoodCost) As Boolean
        Return UserData.AddWoodCost(woodCost)
    End Function

    ''' <summary>
    ''' Updates an existing wood cost entry
    ''' </summary>
    Public Function UpdateWoodCost(woodCost As WoodCost) As Boolean
        Return UserData.UpdateWoodCost(woodCost)
    End Function

    ''' <summary>
    ''' Deletes (soft delete) a wood cost entry
    ''' </summary>
    Public Function DeleteWoodCost(woodCostID As Integer) As Boolean
        Return UserData.DeleteWoodCost(woodCostID)
    End Function

    ''' <summary>
    ''' Gets all epoxy costs
    ''' </summary>
    Public Function GetAllEpoxyCosts() As List(Of EpoxyCost)
        Return UserData.GetAllEpoxyCosts()
    End Function

    ''' <summary>
    ''' Adds a new epoxy cost entry
    ''' </summary>
    Public Function AddEpoxyCost(epoxyCost As EpoxyCost) As Boolean
        Return UserData.AddEpoxyCost(epoxyCost)
    End Function

    ''' <summary>
    ''' Updates an existing epoxy cost entry
    ''' </summary>
    Public Function UpdateEpoxyCost(epoxyCost As EpoxyCost) As Boolean
        Return UserData.UpdateEpoxyCost(epoxyCost)
    End Function

    ''' <summary>
    ''' Deletes (soft delete) an epoxy cost entry
    ''' </summary>
    Public Function DeleteEpoxyCost(epoxyCostID As Integer) As Boolean
        Return UserData.DeleteEpoxyCost(epoxyCostID)
    End Function

#End Region

#Region "Unified API - Wood Species Methods (Legacy Support)"

    ''' <summary>
    ''' Gets all wood species - returns WoodPropertiesData for legacy compatibility
    ''' Maps from unified WoodSpecies to old WoodPropertiesData
    ''' </summary>
    Public Function GetAllWoodSpecies() As List(Of WoodPropertiesData)
        Dim species = GetWoodSpeciesList()
        Dim result As New List(Of WoodPropertiesData)

        For Each s In species
            result.Add(New WoodPropertiesData With {
                .CommonName = s.CommonName,
                .ScientificName = s.ScientificName,
                .WoodType = s.WoodType,
                .JankaHardness = s.JankaHardness,
                .SpecificGravity = s.SpecificGravity,
                .Density = CInt(s.Density),
                .MoistureContent = s.MoistureContent,
                .ShrinkageRadial = s.ShrinkageRadial,
                .ShrinkageTangential = s.ShrinkageTangential,
                .TypicalUses = s.TypicalUses,
                .Workability = s.Workability,
                .Cautions = s.Cautions,
                .Notes = s.Notes
            })
        Next

        Return result
    End Function

    ''' <summary>
    ''' Adds a wood species - accepts WoodPropertiesData for legacy compatibility
    ''' </summary>
    Public Function AddWoodSpecies(species As WoodPropertiesData) As Boolean
        ' Convert to unified WoodSpecies and add to UserData
        Dim woodSpecies As New WoodSpecies With {
            .CommonName = species.CommonName,
            .ScientificName = species.ScientificName,
            .WoodType = species.WoodType,
            .JankaHardness = species.JankaHardness,
            .SpecificGravity = species.SpecificGravity,
            .Density = species.Density,
            .MoistureContent = species.MoistureContent,
            .ShrinkageRadial = species.ShrinkageRadial,
            .ShrinkageTangential = species.ShrinkageTangential,
            .TypicalUses = species.TypicalUses,
            .Workability = species.Workability,
            .Cautions = species.Cautions,
            .Notes = species.Notes
        }

        Return UserData.AddCustomWoodSpecies(woodSpecies)
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
