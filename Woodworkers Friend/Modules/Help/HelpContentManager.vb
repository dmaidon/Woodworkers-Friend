''' ============================================================================
''' Last Updated: January 29, 2026
''' Changes: Initial creation of HelpContentManager for loading embedded markdown resources
''' ============================================================================

Imports System.IO
Imports System.Reflection
Imports System.Text

''' <summary>
''' Manages loading and caching of help content from embedded markdown resources
''' </summary>
Public Class HelpContentManager

    Private Shared ReadOnly _contentCache As New Dictionary(Of String, String)
    Private Shared ReadOnly _cacheLock As New Object()

    ''' <summary>
    ''' Loads help topic content from embedded markdown resource
    ''' </summary>
    ''' <param name="topic">The help topic name (e.g., "DrawerCalculator", "WoodMovement")</param>
    ''' <returns>Markdown content as string, or error message if not found</returns>
    Public Shared Function LoadHelpTopic(topic As String) As String
        If String.IsNullOrWhiteSpace(topic) Then
            Return "## Error" & vbCrLf & "No topic specified."
        End If

        ' Check cache first
        SyncLock _cacheLock
            If _contentCache.ContainsKey(topic) Then
                Return _contentCache(topic)
            End If
        End SyncLock

        ' Load from embedded resource
        Dim content As String = LoadFromEmbeddedResource(topic)

        ' Cache the result
        SyncLock _cacheLock
            If Not _contentCache.ContainsKey(topic) Then
                _contentCache.Add(topic, content)
            End If
        End SyncLock

        Return content
    End Function

    ''' <summary>
    ''' Loads content from embedded resource stream
    ''' </summary>
    Private Shared Function LoadFromEmbeddedResource(topic As String) As String
        Try
            Dim assembly As Assembly = Assembly.GetExecutingAssembly()
            ' SDK-style projects embed resources with simplified namespace
            Dim resourceName As String = $"WwFriend.{topic}.md"

            ' Try to get the resource stream
            Using stream As Stream = assembly.GetManifestResourceStream(resourceName)
                If stream Is Nothing Then
                    ' List available resources for debugging
                    Dim availableResources As String() = assembly.GetManifestResourceNames()
                    Dim resourceList As String = String.Join(vbCrLf, availableResources.Where(Function(r) r.EndsWith(".md")))

                    Return $"## Resource Not Found{vbCrLf}{vbCrLf}" &
                           $"Could not find resource: {resourceName}{vbCrLf}{vbCrLf}" &
                           $"Available Help resources:{vbCrLf}{resourceList}{vbCrLf}{vbCrLf}" &
                           $"Please ensure the markdown file is embedded in the project."
                End If

                Using reader As New StreamReader(stream, Encoding.UTF8)
                    Return reader.ReadToEnd()
                End Using
            End Using

        Catch ex As Exception
            Return $"## Error Loading Help{vbCrLf}{vbCrLf}" &
                   $"Failed to load help topic '{topic}'{vbCrLf}{vbCrLf}" &
                   $"Error: {ex.Message}"
        End Try
    End Function

    ''' <summary>
    ''' Clears the content cache (useful for testing or refreshing content)
    ''' </summary>
    Public Shared Sub ClearCache()
        SyncLock _cacheLock
            _contentCache.Clear()
        End SyncLock
    End Sub

    ''' <summary>
    ''' Gets list of cached topics
    ''' </summary>
    Public Shared Function GetCachedTopics() As String()
        SyncLock _cacheLock
            Return _contentCache.Keys.ToArray()
        End SyncLock
    End Function

End Class
