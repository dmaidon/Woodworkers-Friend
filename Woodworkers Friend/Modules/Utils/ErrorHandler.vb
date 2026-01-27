' ============================================================================
' Last Updated: {Current Date}
' Changes: Initial creation - Centralized error handling and logging system
'          with file-based logging, user notifications, and warning support
' ============================================================================

Imports System.IO

''' <summary>
''' Centralized error handling and logging functionality
''' </summary>
Public Class ErrorHandler

    Private Shared ReadOnly _logFilePath As String = Path.Combine(LogDir, $"errors_{DateTime.Now:yyyy-MM-dd}.log")

    ''' <summary>
    ''' Handles an exception with optional user notification
    ''' </summary>
    ''' <param name="ex">The exception to handle</param>
    ''' <param name="context">Context information about where the error occurred</param>
    ''' <param name="showToUser">Whether to show a message to the user</param>
    Public Shared Sub HandleError(ex As Exception, context As String, Optional showToUser As Boolean = False)
        ' Log the error
        LogError(ex, context)

        ' Optionally show to user
        If showToUser Then
            Dim message As String = $"An error occurred: {context}" & Environment.NewLine &
                                   "Please check the error log for details."
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ''' <summary>
    ''' Logs an error to the error log file
    ''' </summary>
    Public Shared Sub LogError(ex As Exception, context As String)
        Try
            ' Ensure log directory exists
            Dim logDir As String = Path.GetDirectoryName(_logFilePath)
            If Not Directory.Exists(logDir) Then
                Directory.CreateDirectory(logDir)
            End If

            ' Create log entry
            Dim logEntry As New Text.StringBuilder()
            logEntry.AppendLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] ERROR in {context}")
            logEntry.AppendLine($"Exception Type: {ex.GetType().FullName}")
            logEntry.AppendLine($"Message: {ex.Message}")
            logEntry.AppendLine($"Stack Trace: {ex.StackTrace}")

            If ex.InnerException IsNot Nothing Then
                logEntry.AppendLine($"Inner Exception: {ex.InnerException.Message}")
            End If

            logEntry.AppendLine(New String("-"c, 80))

            ' Write to log file
            File.AppendAllText(_logFilePath, logEntry.ToString())
        Catch logEx As Exception
            ' If logging fails, try to show a message box as last resort
            Debug.WriteLine($"Failed to log error: {logEx.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Handles an error with a custom message to the user
    ''' </summary>
    Public Shared Sub HandleErrorWithMessage(ex As Exception, context As String, userMessage As String)
        LogError(ex, context)
        MessageBox.Show(userMessage, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    ''' <summary>
    ''' Logs a warning message without an exception
    ''' </summary>
    Public Shared Sub LogWarning(context As String, message As String)
        Try
            Dim logDir As String = Path.GetDirectoryName(_logFilePath)
            If Not Directory.Exists(logDir) Then
                Directory.CreateDirectory(logDir)
            End If

            Dim logEntry As String = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] WARNING in {context}: {message}" &
                                    Environment.NewLine
            File.AppendAllText(_logFilePath, logEntry)
        Catch
            ' Silently fail if warning logging fails
        End Try
    End Sub

End Class
