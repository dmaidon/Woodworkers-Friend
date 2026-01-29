' ============================================================================
' Last Updated: January 29, 2026
' Changes: Updated log file format to MMMd (e.g., Jan29), added startup logging,
'          and automatic cleanup of log files older than 10 days
' ============================================================================

Imports System.IO

''' <summary>
''' Centralized error handling and logging functionality
''' </summary>
Public Class ErrorHandler

    Private Shared ReadOnly _logFilePath As String = Path.Combine(LogDir, $"errors_{DateTime.Now:MMMd}.log")
    Private Const MaxLogAgeInDays As Integer = 10

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

    ''' <summary>
    ''' Logs application startup to the current log file
    ''' </summary>
    Public Shared Sub LogStartup()
        Try
            ' Ensure log directory exists
            Dim logDir As String = Path.GetDirectoryName(_logFilePath)
            If Not Directory.Exists(logDir) Then
                Directory.CreateDirectory(logDir)
            End If

            ' Create startup log entry
            Dim logEntry As String = $"Log Started: {DateTime.Now:yyyy-MM-dd HH:mm:ss}{Environment.NewLine}"
            File.AppendAllText(_logFilePath, logEntry)
        Catch
            ' Silently fail if startup logging fails
            Debug.WriteLine("Failed to log application startup")
        End Try
    End Sub

    ''' <summary>
    ''' Cleans up log files older than the specified number of days
    ''' </summary>
    Public Shared Sub CleanupOldLogs()
        Try
            ' Ensure log directory exists
            If Not Directory.Exists(LogDir) Then
                Return
            End If

            ' Get all log files in the log directory
            Dim logFiles As String() = Directory.GetFiles(LogDir, "errors_*.log")

            ' Calculate cutoff date
            Dim cutoffDate As DateTime = DateTime.Now.AddDays(-MaxLogAgeInDays)

            ' Delete old log files
            Dim deletedCount As Integer = 0
            For Each logFile As String In logFiles
                Dim fileInfo As New FileInfo(logFile)

                ' Delete if file is older than cutoff date
                If fileInfo.LastWriteTime < cutoffDate Then
                    Try
                        File.Delete(logFile)
                        deletedCount += 1
                    Catch deleteEx As Exception
                        ' Continue if we can't delete a specific file
                        Debug.WriteLine($"Could not delete log file {fileInfo.Name}: {deleteEx.Message}")
                    End Try
                End If
            Next

            ' Log cleanup activity if any files were deleted
            If deletedCount > 0 Then
                LogWarning("LogCleanup", $"Deleted {deletedCount} log file(s) older than {MaxLogAgeInDays} days")
            End If
        Catch ex As Exception
            ' Silently fail if cleanup fails
            Debug.WriteLine($"Failed to cleanup old logs: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Gets the current log file path
    ''' </summary>
    Public Shared ReadOnly Property CurrentLogFilePath As String
        Get
            Return _logFilePath
        End Get
    End Property

End Class
