' ============================================================================
' Created: January 2026
' Purpose: Manages Help.db - isolated read-only database for help content
' Architecture: Part of multi-database split (Help.db, Reference.db, UserData.db)
' ============================================================================

Imports System.Data.SQLite
Imports System.IO

''' &lt;summary&gt;
''' Manages Help.db database containing application help content, tutorials, and definitions.
''' Read-only access mode for safety and concurrent access.
''' Database is shipped with application and extracted to AppData/Resources.
''' &lt;/summary&gt;
Public Class HelpDataManager
    Implements IDisposable

#Region "Singleton Pattern"

    Private Shared _instance As HelpDataManager
    Private Shared ReadOnly _lock As New Object()

    Public Shared ReadOnly Property Instance As HelpDataManager
        Get
            If _instance Is Nothing Then
                SyncLock _lock
                    If _instance Is Nothing Then
                        _instance = New HelpDataManager()
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
        ' Database location: Program Files\Woodworkers Friend\Data\Resources\Help.db
        Dim resourcesDir = Globals.AppResourcesDir
        Directory.CreateDirectory(resourcesDir)

        _databasePath = Path.Combine(resourcesDir, "Help.db")
        _connectionString = $"Data Source={_databasePath};Version=3;Read Only=True;"

        InitializeDatabase()
    End Sub

#End Region

#Region "Initialization"

    ''' &lt;summary&gt;
    ''' Creates database schema if not exists
    ''' &lt;/summary&gt;
    Private Sub InitializeDatabase()
        Try
            If Not File.Exists(_databasePath) Then
                ' Create new database with schema
                CreateDatabaseSchema()
                ErrorHandler.LogError(New Exception("Help.db created successfully"), "HelpDataManager.InitializeDatabase")
            Else
                ' Verify schema
                VerifyDatabaseSchema()
            End If

            ' Set file as read-only for safety
            SetReadOnlyAttribute()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "HelpDataManager.InitializeDatabase")
        End Try
    End Sub

    ''' &lt;summary&gt;
    ''' Creates the Help.db schema
    ''' &lt;/summary&gt;
    Private Sub CreateDatabaseSchema()
        ' Temporarily create as read-write
        Dim createConnectionString = $"Data Source={_databasePath};Version=3;"

        Using conn As New SQLiteConnection(createConnectionString)
            conn.Open()

            Using cmd As New SQLiteCommand("
                CREATE TABLE IF NOT EXISTS HelpContent (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    ModuleName TEXT NOT NULL UNIQUE,
                    Title TEXT NOT NULL,
                    Content TEXT NOT NULL,
                    Keywords TEXT,
                    Category TEXT,
                    SortOrder INTEGER DEFAULT 0,
                    Version TEXT DEFAULT '1.0',
                    LastUpdated DATETIME DEFAULT CURRENT_TIMESTAMP
                );

                CREATE INDEX IF NOT EXISTS idx_help_module ON HelpContent(ModuleName);
                CREATE INDEX IF NOT EXISTS idx_help_category ON HelpContent(Category);
                CREATE INDEX IF NOT EXISTS idx_help_keywords ON HelpContent(Keywords);

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

    ''' &lt;summary&gt;
    ''' Verifies database schema is correct
    ''' &lt;/summary&gt;
    Private Sub VerifyDatabaseSchema()
        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='HelpContent'", conn)
                    Dim tableExists = Convert.ToInt32(cmd.ExecuteScalar()) > 0
                    If Not tableExists Then
                        ErrorHandler.LogError(New Exception("Help.db schema invalid - needs recreation"), "VerifyDatabaseSchema")
                    End If
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "VerifyDatabaseSchema")
        End Try
    End Sub

    ''' &lt;summary&gt;
    ''' Sets database file as read-only
    ''' &lt;/summary&gt;
    Private Sub SetReadOnlyAttribute()
        Try
            If File.Exists(_databasePath) Then
                Dim fileInfo As New FileInfo(_databasePath)
                If Not fileInfo.IsReadOnly Then
                    fileInfo.IsReadOnly = True
                End If
            End If
        Catch ex As Exception
            ' Don't fail if we can't set read-only - log and continue
            ErrorHandler.LogError(ex, "SetReadOnlyAttribute")
        End Try
    End Sub

#End Region

#Region "Connection Management"

    ''' &lt;summary&gt;
    ''' Gets a read-only connection to Help.db (allows concurrent access)
    ''' &lt;/summary&gt;
    Private Function GetReadOnlyConnection() As SQLiteConnection
        Return New SQLiteConnection(_connectionString)
    End Function

    ''' &lt;summary&gt;
    ''' Gets a read-write connection (only for updates from app resources)
    ''' &lt;/summary&gt;
    Private Function GetWriteConnection() As SQLiteConnection
        ' Temporarily remove read-only for updates
        Dim writeConnectionString = $"Data Source={_databasePath};Version=3;"
        Return New SQLiteConnection(writeConnectionString)
    End Function

#End Region

#Region "Public API - Help Content"

    ''' &lt;summary&gt;
    ''' Gets help content for a specific module/topic
    ''' &lt;/summary&gt;
    Public Function GetContent(moduleName As String) As DatabaseManager.HelpContentData
        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    SELECT ModuleName, Title, Content, Keywords, Category, SortOrder, Version
                    FROM HelpContent
                    WHERE ModuleName = @ModuleName
                ", conn)
                    cmd.Parameters.AddWithValue("@ModuleName", moduleName)

                    Using reader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Return New DatabaseManager.HelpContentData With {
                                .ModuleName = reader.GetString(0),
                                .Title = reader.GetString(1),
                                .Content = reader.GetString(2),
                                .Keywords = If(reader.IsDBNull(3), String.Empty, reader.GetString(3)),
                                .Category = If(reader.IsDBNull(4), String.Empty, reader.GetString(4)),
                                .SortOrder = If(reader.IsDBNull(5), 0, reader.GetInt32(5)),
                                .Version = If(reader.IsDBNull(6), "1.0", reader.GetString(6))
                            }
                        End If
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, $"GetContent - {moduleName}")
        End Try

        Return Nothing
    End Function

    ''' &lt;summary&gt;
    ''' Searches help content by keywords and content
    ''' &lt;/summary&gt;
    Public Function SearchContent(searchTerm As String) As List(Of DatabaseManager.HelpContentData)
        Dim results As New List(Of DatabaseManager.HelpContentData)

        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    SELECT ModuleName, Title, Content, Keywords, Category, SortOrder, Version
                    FROM HelpContent
                    WHERE Title LIKE @Search 
                       OR Keywords LIKE @Search 
                       OR Content LIKE @Search
                    ORDER BY SortOrder, Title
                ", conn)
                    cmd.Parameters.AddWithValue("@Search", $"%{searchTerm}%")

                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            results.Add(New DatabaseManager.HelpContentData With {
                                .ModuleName = reader.GetString(0),
                                .Title = reader.GetString(1),
                                .Content = reader.GetString(2),
                                .Keywords = If(reader.IsDBNull(3), String.Empty, reader.GetString(3)),
                                .Category = If(reader.IsDBNull(4), String.Empty, reader.GetString(4)),
                                .SortOrder = If(reader.IsDBNull(5), 0, reader.GetInt32(5)),
                                .Version = If(reader.IsDBNull(6), "1.0", reader.GetString(6))
                            })
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, $"SearchContent - {searchTerm}")
        End Try

        Return results
    End Function

    ''' &lt;summary&gt;
    ''' Gets all help topics (lightweight, no content body)
    ''' &lt;/summary&gt;
    Public Function GetAllTopics() As List(Of DatabaseManager.HelpContentData)
        Dim results As New List(Of DatabaseManager.HelpContentData)

        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    SELECT ModuleName, Title, Category, SortOrder
                    FROM HelpContent
                    ORDER BY SortOrder, Title
                ", conn)
                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            results.Add(New DatabaseManager.HelpContentData With {
                                .ModuleName = reader.GetString(0),
                                .Title = reader.GetString(1),
                                .Category = If(reader.IsDBNull(2), String.Empty, reader.GetString(2)),
                                .SortOrder = If(reader.IsDBNull(3), 0, reader.GetInt32(3))
                            })
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetAllTopics")
        End Try

        Return results
    End Function

    ''' &lt;summary&gt;
    ''' Gets help topics by category
    ''' &lt;/summary&gt;
    Public Function GetContentByCategory(category As String) As List(Of DatabaseManager.HelpContentData)
        Dim results As New List(Of DatabaseManager.HelpContentData)

        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    SELECT ModuleName, Title, Content, Keywords, Category, SortOrder, Version
                    FROM HelpContent
                    WHERE Category = @Category
                    ORDER BY SortOrder, Title
                ", conn)
                    cmd.Parameters.AddWithValue("@Category", category)

                    Using reader = cmd.ExecuteReader()
                        While reader.Read()
                            results.Add(New DatabaseManager.HelpContentData With {
                                .ModuleName = reader.GetString(0),
                                .Title = reader.GetString(1),
                                .Content = reader.GetString(2),
                                .Keywords = If(reader.IsDBNull(3), String.Empty, reader.GetString(3)),
                                .Category = If(reader.IsDBNull(4), String.Empty, reader.GetString(4)),
                                .SortOrder = If(reader.IsDBNull(5), 0, reader.GetInt32(5)),
                                .Version = If(reader.IsDBNull(6), "1.0", reader.GetString(6))
                            })
                        End While
                    End Using
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, $"GetContentByCategory - {category}")
        End Try

        Return results
    End Function

    ''' &lt;summary&gt;
    ''' Checks if help content has been seeded
    ''' &lt;/summary&gt;
    Public Function IsContentSeeded() As Boolean
        Try
            Using conn = GetReadOnlyConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT COUNT(*) FROM HelpContent", conn)
                    Return Convert.ToInt32(cmd.ExecuteScalar()) > 0
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "IsContentSeeded")
            Return False
        End Try
    End Function

#End Region

#Region "Admin Operations - Write Access"

    ''' &lt;summary&gt;
    ''' Bulk inserts help content (used during initial seed or update)
    ''' Temporarily removes read-only attribute
    ''' &lt;/summary&gt;
    Friend Function BulkInsertContent(items As List(Of DatabaseManager.HelpContentData)) As Integer
        Dim successCount = 0

        Try
            ' Temporarily remove read-only
            RemoveReadOnlyAttribute()

            Using conn = GetWriteConnection()
                conn.Open()
                Using transaction = conn.BeginTransaction()
                    Try
                        For Each item In items
                            Using cmd As New SQLiteCommand("
                                INSERT OR REPLACE INTO HelpContent 
                                (ModuleName, Title, Content, Keywords, Category, SortOrder, Version, LastUpdated)
                                VALUES (@ModuleName, @Title, @Content, @Keywords, @Category, @SortOrder, @Version, CURRENT_TIMESTAMP)
                            ", conn, transaction)
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
                        ErrorHandler.LogError(ex, "BulkInsertContent - Transaction failed")
                        Return 0
                    End Try
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BulkInsertContent")
        Finally
            ' Restore read-only
            SetReadOnlyAttribute()
        End Try

        Return successCount
    End Function

    ''' &lt;summary&gt;
    ''' Updates database version metadata
    ''' &lt;/summary&gt;
    Friend Sub UpdateVersion(version As String)
        Try
            RemoveReadOnlyAttribute()

            Using conn = GetWriteConnection()
                conn.Open()
                Using cmd As New SQLiteCommand("
                    INSERT INTO DatabaseVersion (Version) VALUES (@Version)
                ", conn)
                    cmd.Parameters.AddWithValue("@Version", version)
                    cmd.ExecuteNonQuery()
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "UpdateVersion")
        Finally
            SetReadOnlyAttribute()
        End Try
    End Sub

    ''' &lt;summary&gt;
    ''' Removes read-only attribute for updates
    ''' &lt;/summary&gt;
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
