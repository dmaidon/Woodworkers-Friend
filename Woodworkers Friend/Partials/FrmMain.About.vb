Imports System.IO
Imports System.Diagnostics

Partial Public Class FrmMain

#Region "About Tab - Log File Handling"

    ''' <summary>
    ''' Handles entering the About tab - populates log file list and loads current log
    ''' </summary>
    Private Sub TpAbout_Enter(sender As Object, e As EventArgs) Handles TpAbout.Enter
        PopulateAppInformation()
        PopulateLogFileList()
        LoadCurrentLogFile()
    End Sub

    ''' <summary>
    ''' Handles leaving the About tab - clears all displays to regain memory
    ''' </summary>
    Private Sub TpAbout_Leave(sender As Object, e As EventArgs) Handles TpAbout.Leave
        ClearLogDisplay()
        LbLogFiles.Items.Clear()
    End Sub

    ''' <summary>
    ''' Populates the application information label with comprehensive app details
    ''' </summary>
    Private Sub PopulateAppInformation()
        Try
            ' Get assembly information
            Dim assembly As Reflection.Assembly = Reflection.Assembly.GetExecutingAssembly()
            Dim assemblyName As Reflection.AssemblyName = assembly.GetName()
            Dim assemblyVersion As Version = assemblyName.Version
            Dim fileVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location)

            ' Get company and author from assembly attributes or FileVersionInfo
            Dim companyName As String = fileVersionInfo.CompanyName
            Dim authorName As String = fileVersionInfo.LegalCopyright

            ' If company is empty, use fallback
            If String.IsNullOrEmpty(companyName) Then companyName = "PAROLE Software"
            If String.IsNullOrEmpty(authorName) Then authorName = "Dennis N. Maidon"

            ' Build the information text
            Dim sb As New Text.StringBuilder()

            ' Application Title
            sb.AppendLine("═══════════════════════════════════════════════════")
            sb.AppendLine($"      {AppName}")
            sb.AppendLine($"      Version {Version}")
            sb.AppendLine("═══════════════════════════════════════════════════")
            sb.AppendLine()

            ' Description
            sb.AppendLine("DESCRIPTION:")
            sb.AppendLine("Comprehensive woodworking calculator and planning tool")
            sb.AppendLine("for cabinetmakers, furniture makers, and woodworking")
            sb.AppendLine("enthusiasts.")
            sb.AppendLine()

            ' Author & Company (from assembly FileVersionInfo)
            sb.AppendLine("AUTHOR:")
            sb.AppendLine($"  {authorName}")
            sb.AppendLine()
            sb.AppendLine("COMPANY:")
            sb.AppendLine($"  {companyName}")
            sb.AppendLine()

            ' Version Details
            sb.AppendLine("VERSION INFORMATION:")
            sb.AppendLine($"  Application Version: {Version}")
            sb.AppendLine($"  Assembly Version: {assemblyVersion}")
            sb.AppendLine($"  File Version: {fileVersionInfo.FileVersion}")
            If Not String.IsNullOrEmpty(fileVersionInfo.ProductVersion) Then
                sb.AppendLine($"  Product Version: {fileVersionInfo.ProductVersion}")
            End If
            sb.AppendLine()

            ' System Information
            sb.AppendLine("SYSTEM INFORMATION:")
            sb.AppendLine($"  .NET Runtime: {Environment.Version}")
            sb.AppendLine($"  Operating System: {Environment.OSVersion}")
            sb.AppendLine($"  64-bit OS: {Environment.Is64BitOperatingSystem}")
            sb.AppendLine($"  64-bit Process: {Environment.Is64BitProcess}")
            sb.AppendLine($"  Processor Count: {Environment.ProcessorCount}")
            sb.AppendLine()

            ' Installation Path
            sb.AppendLine("INSTALLATION:")
            sb.AppendLine($"  {Application.StartupPath}")
            sb.AppendLine()

            ' Features
            sb.AppendLine("KEY FEATURES:")
            sb.AppendLine("  ✓ Drawer Height Calculator (10 methods)")
            sb.AppendLine("  ✓ Cabinet Door Calculator")
            sb.AppendLine("  ✓ Board Feet Calculator")
            sb.AppendLine("  ✓ Epoxy Pour Calculator")
            sb.AppendLine("  ✓ Joinery Calculator (Mortise/Tenon, Dovetails, etc.)")
            sb.AppendLine("  ✓ Wood Movement Calculator")
            sb.AppendLine("  ✓ Shelf Sag Calculator")
            sb.AppendLine("  ✓ Cut List Optimizer")
            sb.AppendLine("  ✓ Unit Conversions")
            sb.AppendLine("  ✓ Dark/Light Themes")
            sb.AppendLine()

            ' Copyright & License
            sb.AppendLine("COPYRIGHT:")
            sb.AppendLine($"  {GetCopyrightNotice()}")
            sb.AppendLine()
            sb.AppendLine("LICENSE:")
            sb.AppendLine("  This software is open source and provided 'as-is'")
            sb.AppendLine("  without warranty. Always verify calculations and")
            sb.AppendLine("  use appropriate safety measures.")
            sb.AppendLine()

            ' Contact & Support
            sb.AppendLine("SUPPORT:")
            sb.AppendLine("  GitHub: https://github.com/dmaidon/Woodworkers-Friend")
            sb.AppendLine("  Issues: Report bugs via GitHub Issues")
            sb.AppendLine("  Discussions: Feature requests via GitHub Discussions")
            sb.AppendLine()

            ' Build Information
            sb.AppendLine("BUILD INFORMATION:")
            sb.AppendLine($"  Build Date: {GetBuildDate(assembly):MMMM d, yyyy}")
            sb.AppendLine($"  Configuration: {GetBuildConfiguration()}")
            sb.AppendLine()

            ' Footer
            sb.AppendLine("───────────────────────────────────────────────────")
            sb.AppendLine("Thank you for using Woodworker's Friend!")
            sb.AppendLine("Happy Woodworking! 🪵🔨")

            ' Set the textbox text
            TxtAppAbout.Text = sb.ToString()
            TxtAppAbout.Font = New Font("Consolas", 9, FontStyle.Regular)
            TxtAppAbout.ForeColor = Color.Black
            TxtAppAbout.BackColor = Color.WhiteSmoke
            TxtAppAbout.Multiline = True
            TxtAppAbout.ScrollBars = ScrollBars.Vertical
            TxtAppAbout.ReadOnly = True
            TxtAppAbout.WordWrap = False
        Catch ex As Exception
            TxtAppAbout.Text = "Error loading application information."
            ErrorHandler.HandleError(ex, "PopulateAppInformation", showToUser:=False)
        End Try
    End Sub

    ''' <summary>
    ''' Gets the build date from the assembly
    ''' </summary>
    Private Function GetBuildDate(assembly As Reflection.Assembly) As DateTime
        Try
            ' Get the build date from the PE header
            Dim filePath = assembly.Location
            Dim buffer(Math.Max(Diagnostics.Process.GetCurrentProcess().MainModule.ModuleMemorySize, 4) - 1) As Byte

            Using stream As New FileStream(filePath, FileMode.Open, FileAccess.Read)
                stream.ReadExactly(buffer)
            End Using

            ' Try to get from file creation time if PE header method fails
            Return New FileInfo(filePath).LastWriteTime
        Catch
            ' Fallback to current date if we can't determine build date
            Return DateTime.Now
        End Try
    End Function

    ''' <summary>
    ''' Gets the build configuration (Debug/Release)
    ''' </summary>
    Private Function GetBuildConfiguration() As String
#If DEBUG Then
            Return "Debug"
#Else
        Return "Release"
#End If
    End Function

    ''' <summary>
    ''' Populates the log file list with available log files
    ''' </summary>
    Private Sub PopulateLogFileList()
        Try
            LbLogFiles.Items.Clear()

            ' Ensure log directory exists
            If Not Directory.Exists(LogDir) Then
                LbLogFiles.Items.Add("(No log files found)")
                Return
            End If

            ' Get all log files sorted by date (newest first)
            Dim logFiles = Directory.GetFiles(LogDir, "errors_*.log") _
                .Select(Function(f) New FileInfo(f)) _
                .OrderByDescending(Function(fi) fi.LastWriteTime) _
                .ToList()

            If logFiles.Count = 0 Then
                LbLogFiles.Items.Add("(No log files found)")
                Return
            End If

            ' Add log files to listbox with formatted display
            For Each fileInfo In logFiles
                Dim displayText = fileInfo.Name
                LbLogFiles.Items.Add(displayText)
                LbLogFiles.Tag = If(LbLogFiles.Tag, New List(Of String))
                CType(LbLogFiles.Tag, List(Of String)).Add(fileInfo.FullName)
            Next

            ' Select the most recent log file (index 0)
            If LbLogFiles.Items.Count > 0 Then
                LbLogFiles.SelectedIndex = 0
            End If
        Catch ex As Exception
            LbLogFiles.Items.Clear()
            LbLogFiles.Items.Add("(Error loading log files)")
            ErrorHandler.HandleError(ex, "PopulateLogFileList", showToUser:=False)
        End Try
    End Sub

    ''' <summary>
    ''' Handles log file selection from listbox
    ''' </summary>
    Private Sub LbLogFiles_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LbLogFiles.SelectedIndexChanged
        If LbLogFiles.SelectedIndex < 0 Then Return
        If LbLogFiles.Tag Is Nothing Then Return

        Dim filePaths = CType(LbLogFiles.Tag, List(Of String))
        If LbLogFiles.SelectedIndex >= filePaths.Count Then Return

        LoadLogFile(filePaths(LbLogFiles.SelectedIndex))
    End Sub

    ''' <summary>
    ''' Loads a specific log file into RtbLog
    ''' </summary>
    Private Sub LoadLogFile(logFilePath As String)
        Try
            RtbLog.Clear()

            ' Check if log file exists
            If Not File.Exists(logFilePath) Then
                RtbLog.SelectionFont = New Font("Segoe UI", 10, FontStyle.Italic)
                RtbLog.SelectionColor = Color.Gray
                RtbLog.AppendText($"Log file not found: {Path.GetFileName(logFilePath)}")
                Return
            End If

            ' Show file info header
            Dim fileInfo As New FileInfo(logFilePath)
            RtbLog.SelectionFont = New Font("Segoe UI", 10, FontStyle.Bold)
            RtbLog.SelectionColor = Color.DarkBlue
            RtbLog.AppendText($"Log File: {fileInfo.Name}{Environment.NewLine}")
            RtbLog.SelectionFont = New Font("Segoe UI", 9, FontStyle.Regular)
            RtbLog.SelectionColor = Color.DarkGray
            RtbLog.AppendText($"Last Modified: {fileInfo.LastWriteTime:MMMM d, yyyy h:mm:ss tt}{Environment.NewLine}")
            RtbLog.AppendText($"Size: {fileInfo.Length:N0} bytes{Environment.NewLine}")
            RtbLog.SelectionFont = New Font("Segoe UI", 9, FontStyle.Regular)
            RtbLog.SelectionColor = Color.LightGray
            RtbLog.AppendText(New String("─"c, 80) & Environment.NewLine & Environment.NewLine)

            ' Read and display log file
            Dim logContent As String = File.ReadAllText(logFilePath)

            If String.IsNullOrWhiteSpace(logContent) Then
                RtbLog.SelectionFont = New Font("Segoe UI", 10, FontStyle.Italic)
                RtbLog.SelectionColor = Color.Gray
                RtbLog.AppendText("Log file is empty.")
            Else
                ' Format and display log content
                FormatLogContent(logContent)
            End If

            ' Scroll to end to show most recent entries
            RtbLog.SelectionStart = RtbLog.Text.Length
            RtbLog.ScrollToCaret()
        Catch ex As Exception
            RtbLog.Clear()
            RtbLog.SelectionFont = New Font("Segoe UI", 10, FontStyle.Bold)
            RtbLog.SelectionColor = Color.Red
            RtbLog.AppendText($"Error loading log file: {ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Loads the current day's log file into RtbLog
    ''' </summary>
    Private Sub LoadCurrentLogFile()
        ' Get today's log file path (using new format: errors_MMMd.log)
        ' Dim logFileName As String = $"errors_{DateTime.Now:MMMd}.log"
        ' Dim logFilePath As String = Path.Combine(LogDir, logFileName)

        ' Load the current log file
        LoadLogFile(LogFile)
    End Sub

    ''' <summary>
    ''' Formats log content with color coding
    ''' </summary>
    Private Sub FormatLogContent(logContent As String)
        Dim lines() As String = logContent.Split(New String() {Environment.NewLine}, StringSplitOptions.None)

        For Each line In lines
            ' Color code based on content
            If line.StartsWith("Log Started:") Then
                ' Startup log entry
                RtbLog.SelectionFont = New Font("Consolas", 9, FontStyle.Bold)
                RtbLog.SelectionColor = Color.DarkGreen
            ElseIf line.Contains("ERROR") Then
                RtbLog.SelectionFont = New Font("Consolas", 9, FontStyle.Bold)
                RtbLog.SelectionColor = Color.Red
            ElseIf line.Contains("WARNING") Then
                RtbLog.SelectionFont = New Font("Consolas", 9, FontStyle.Bold)
                RtbLog.SelectionColor = Color.DarkOrange
            ElseIf line.StartsWith("["c) AndAlso line.Contains("]"c) Then
                ' Timestamp line
                RtbLog.SelectionFont = New Font("Consolas", 9, FontStyle.Regular)
                RtbLog.SelectionColor = Color.DarkBlue
            ElseIf line.Contains("Exception Type:") OrElse line.Contains("Message:") Then
                RtbLog.SelectionFont = New Font("Consolas", 9, FontStyle.Italic)
                RtbLog.SelectionColor = Color.DarkRed
            ElseIf line.Contains("Stack Trace:") Then
                RtbLog.SelectionFont = New Font("Consolas", 8, FontStyle.Regular)
                RtbLog.SelectionColor = Color.DarkGray
            ElseIf line.StartsWith("---") Then
                ' Separator line
                RtbLog.SelectionFont = New Font("Consolas", 9, FontStyle.Regular)
                RtbLog.SelectionColor = Color.LightGray
            Else
                ' Default formatting
                RtbLog.SelectionFont = New Font("Consolas", 9, FontStyle.Regular)
                RtbLog.SelectionColor = Color.Black
            End If

            RtbLog.AppendText(line & Environment.NewLine)
        Next
    End Sub

    ''' <summary>
    ''' Clears the log display
    ''' </summary>
    Private Sub ClearLogDisplay()
        RtbLog.Clear()
    End Sub

#End Region

#Region "RtbLog Context Menu Handlers"

    ''' <summary>
    ''' Handles Select All menu item click
    ''' </summary>
    Private Sub CmsLogSelectAll_Click(sender As Object, e As EventArgs) Handles CmsLogSelectAll.Click
        If RtbLog.Text.Length > 0 Then
            RtbLog.SelectAll()
        End If
    End Sub

    ''' <summary>
    ''' Handles Copy menu item click
    ''' </summary>
    Private Sub CmsLogCopy_Click(sender As Object, e As EventArgs) Handles CmsLogCopy.Click
        If RtbLog.SelectionLength > 0 Then
            Clipboard.SetText(RtbLog.SelectedText)
        End If
    End Sub

    ''' <summary>
    ''' Handles Copy All menu item click
    ''' </summary>
    Private Sub CmsLogCopyAll_Click(sender As Object, e As EventArgs) Handles CmsLogCopyAll.Click
        If RtbLog.Text.Length > 0 Then
            Clipboard.SetText(RtbLog.Text)
        End If
    End Sub

    ''' <summary>
    ''' Handles Clear menu item click
    ''' </summary>
    Private Sub CmsLogClear_Click(sender As Object, e As EventArgs) Handles CmsLogClear.Click
        RtbLog.Clear()
    End Sub

    ''' <summary>
    ''' Handles Refresh menu item click - reloads the log file
    ''' </summary>
    Private Sub CmsLogRefresh_Click(sender As Object, e As EventArgs) Handles CmsLogRefresh.Click
        LoadCurrentLogFile()
    End Sub

    ''' <summary>
    ''' Handles Find menu item click
    ''' </summary>
    Private Sub CmsLogFind_Click(sender As Object, e As EventArgs) Handles CmsLogFind.Click
        ' Simple find dialog
        Dim searchText As String = InputBox("Enter text to find:", "Find in Log", "")

        If Not String.IsNullOrEmpty(searchText) Then
            Dim startIndex As Integer = RtbLog.SelectionStart + RtbLog.SelectionLength
            Dim foundIndex As Integer = RtbLog.Find(searchText, startIndex, RichTextBoxFinds.None)

            If foundIndex = -1 AndAlso startIndex > 0 Then
                ' Wrap around to beginning
                foundIndex = RtbLog.Find(searchText, 0, RichTextBoxFinds.None)
            End If

            If foundIndex >= 0 Then
                RtbLog.Select(foundIndex, searchText.Length)
                RtbLog.ScrollToCaret()
            Else
                MessageBox.Show($"Text '{searchText}' not found.", "Find", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Handles Save As menu item click - saves log to a file
    ''' </summary>
    Private Sub CmsLogSaveAs_Click(sender As Object, e As EventArgs) Handles CmsLogSaveAs.Click
        Try
            Using saveDialog As New SaveFileDialog With {
                .Filter = "Text Files (*.txt)|*.txt|Log Files (*.log)|*.log|All Files (*.*)|*.*",
                .DefaultExt = "txt",
                .FileName = $"log_{DateTime.Now:yyyy-MM-dd_HHmmss}.txt",
                .Title = "Save Log File As"
            }
                If saveDialog.ShowDialog() = DialogResult.OK Then
                    File.WriteAllText(saveDialog.FileName, RtbLog.Text)
                    MessageBox.Show("Log file saved successfully.", "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Error saving log file: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Handles context menu opening - enables/disables items based on content
    ''' </summary>
    Private Sub CmsLog_Opening(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles CmsLog.Opening
        ' Enable/disable menu items based on content
        Dim hasText As Boolean = RtbLog.Text.Length > 0
        Dim hasSelection As Boolean = RtbLog.SelectionLength > 0

        CmsLogSelectAll.Enabled = hasText
        CmsLogCopy.Enabled = hasSelection
        CmsLogCopyAll.Enabled = hasText
        CmsLogFind.Enabled = hasText
        CmsLogSaveAs.Enabled = hasText
    End Sub

    ''' <summary>
    ''' Opens the Manage Costs form for editing wood and epoxy cost data
    ''' Phase 7.3 - Cost Management
    ''' </summary>
    Private Sub BtnManageCosts_Click(sender As Object, e As EventArgs) Handles BtnManageCosts.Click
        Try
            Using frm As New FrmManageCosts()
                frm.ShowDialog(Me)
            End Using

            ' Reload cost lists after form closes
            ' This ensures any changes made in the management form are reflected immediately
            Try
                LoadWoodCosts()
            Catch ex As Exception
                ErrorHandler.LogError(ex, "BtnManageCosts_Click - LoadWoodCosts")
            End Try

            Try
                LoadEpoxyCostData()
            Catch ex As Exception
                ErrorHandler.LogError(ex, "BtnManageCosts_Click - LoadEpoxyCostData")
            End Try
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnManageCosts_Click")
            MessageBox.Show($"Error opening cost management: {ex.Message}",
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

End Class
