Imports System.Drawing

Partial Public Class FrmMain

#Region "System Tray - NotifyIcon and Context Menu"

    Private _trayIcon As System.Windows.Forms.NotifyIcon
    Private _trayContextMenu As ContextMenuStrip

    ''' <summary>
    ''' Initializes the system tray icon and context menu
    ''' </summary>
    Private Sub InitializeSystemTray()
        Try
            If Me.components Is Nothing Then
                Me.components = New System.ComponentModel.Container()
            End If

            _trayIcon = New System.Windows.Forms.NotifyIcon(Me.components)
            
            ' Set icon
            If Me.Icon IsNot Nothing Then
                _trayIcon.Icon = Me.Icon
            Else
                _trayIcon.Icon = SystemIcons.Application
            End If

            _trayIcon.Text = $"{AppName} - {Version}"
            
            ' Create context menu
            _trayContextMenu = New ContextMenuStrip()
            _trayContextMenu.Items.Add("Show Woodworker's Friend", Nothing, AddressOf TrayMenu_Show)
            _trayContextMenu.Items.Add("Locate/Center on Screen", Nothing, AddressOf TrayMenu_Locate)
            _trayContextMenu.Items.Add(New ToolStripSeparator())
            _trayContextMenu.Items.Add("Toggle Theme", Nothing, AddressOf TrayMenu_ToggleTheme)
            _trayContextMenu.Items.Add("Toggle Scale", Nothing, AddressOf TrayMenu_ToggleScale)
            _trayContextMenu.Items.Add(New ToolStripSeparator())
            _trayContextMenu.Items.Add("About...", Nothing, AddressOf TrayMenu_About)
            _trayContextMenu.Items.Add("Help...", Nothing, AddressOf TrayMenu_Help)
            _trayContextMenu.Items.Add(New ToolStripSeparator())
            _trayContextMenu.Items.Add("Exit", Nothing, AddressOf TrayMenu_Exit)
            
            ' Assign context menu - this SHOULD work in .NET 10
            _trayIcon.ContextMenuStrip = _trayContextMenu
            
            ' Handle double-click
            AddHandler _trayIcon.DoubleClick, AddressOf TrayIcon_DoubleClick
            
            ' Make visible
            _trayIcon.Visible = True
            
            ' Show balloon to PROVE the icon is working
            _trayIcon.ShowBalloonTip(
                3000,
                "Woodworker's Friend",
                "Right-click THIS ICON for menu. Look in system tray near clock!",
                ToolTipIcon.Info)

            Debug.WriteLine("System tray initialized")

        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeSystemTray")
            MessageBox.Show($"Error: {ex.Message}", "System Tray Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub TrayMenu_Show(sender As Object, e As EventArgs)
        RestoreWindow()
    End Sub

    Private Sub TrayMenu_Locate(sender As Object, e As EventArgs)
        Try
            Dim screen As Screen = Screen.PrimaryScreen
            Dim workingArea As Rectangle = screen.WorkingArea
            Me.Location = New Point(
                workingArea.Left + (workingArea.Width - Me.Width) \ 2,
                workingArea.Top + (workingArea.Height - Me.Height) \ 2)
            If Me.WindowState = FormWindowState.Minimized Then
                Me.WindowState = FormWindowState.Normal
            End If
            Me.Show()
            Me.Activate()
            Me.BringToFront()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "TrayMenu_Locate")
        End Try
    End Sub

    Private Sub TrayMenu_ToggleTheme(sender As Object, e As EventArgs)
        Try
            TsslToggleTheme_Click(sender, e)
        Catch ex As Exception
            ErrorHandler.LogError(ex, "TrayMenu_ToggleTheme")
        End Try
    End Sub

    Private Sub TrayMenu_ToggleScale(sender As Object, e As EventArgs)
        Try
            If _scaleManager IsNot Nothing Then
                If _scaleManager.CurrentScale = ScaleManager.ScaleType.Imperial Then
                    _scaleManager.SetScale(ScaleManager.ScaleType.Metric)
                    TsslScale.Text = "Metric"
                Else
                    _scaleManager.SetScale(ScaleManager.ScaleType.Imperial)
                    TsslScale.Text = "Imperial"
                End If
                DatabaseManager.Instance.SavePreference("Scale",
                    If(_scaleManager.CurrentScale = ScaleManager.ScaleType.Metric, "Metric", "Imperial"),
                    "String", "UI")
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "TrayMenu_ToggleScale")
        End Try
    End Sub

    Private Sub TrayMenu_About(sender As Object, e As EventArgs)
        Tc.SelectedTab = TpAbout
        RestoreWindow()
    End Sub

    Private Sub TrayMenu_Help(sender As Object, e As EventArgs)
        Tc.SelectedTab = TpHelp
        RestoreWindow()
    End Sub

    Private Sub TrayMenu_Exit(sender As Object, e As EventArgs)
        If _trayIcon IsNot Nothing Then
            _trayIcon.Visible = False
            _trayIcon.Dispose()
        End If
        Application.Exit()
    End Sub

    Private Sub TrayIcon_DoubleClick(sender As Object, e As EventArgs)
        RestoreWindow()
    End Sub

    Private Sub RestoreWindow()
        If Me.WindowState = FormWindowState.Minimized Then
            Me.WindowState = FormWindowState.Normal
        End If
        Me.Show()
        Me.Activate()
        Me.BringToFront()
        Me.Focus()
    End Sub

    Private Sub CleanupSystemTray()
        If _trayIcon IsNot Nothing Then
            _trayIcon.Visible = False
            _trayIcon.Dispose()
            _trayIcon = Nothing
        End If
    End Sub

#End Region

End Class
