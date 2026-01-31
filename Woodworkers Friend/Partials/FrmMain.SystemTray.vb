Imports System.Drawing

Partial Public Class FrmMain

#Region "System Tray - NotifyIcon and Context Menu"

    Private NotifyIcon As NotifyIcon
    Private CmsNotifyIcon As ContextMenuStrip
    Private TsmiRestore As ToolStripMenuItem
    Private TsmiLocate As ToolStripMenuItem
    Private TsmiToggleThemeNotify As ToolStripMenuItem
    Private TsmiToggleScaleNotify As ToolStripMenuItem
    Private TsmiAboutNotify As ToolStripMenuItem
    Private TsmiHelpNotify As ToolStripMenuItem
    Private TsmiExitNotify As ToolStripMenuItem

    ''' <summary>
    ''' Initializes the system tray icon and context menu
    ''' Call this from FrmMain_Load or InitializeUI
    ''' </summary>
    Private Sub InitializeSystemTray()
        Try
            ' Ensure we have a components container
            If Me.components Is Nothing Then
                Me.components = New System.ComponentModel.Container()
            End If

            ' Create NotifyIcon with components container
            NotifyIcon = New NotifyIcon(Me.components)

            ' Set icon - use form icon if available, otherwise use system icon
            If Me.Icon IsNot Nothing Then
                NotifyIcon.Icon = Me.Icon
                Debug.WriteLine("Using form icon for NotifyIcon")
            Else
                ' Use default application icon or create one
                Try
                    NotifyIcon.Icon = SystemIcons.Application
                    Debug.WriteLine("Form icon is Nothing, using SystemIcons.Application")
                Catch
                    ' Last resort - create a simple icon
                    NotifyIcon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath)
                    Debug.WriteLine("Using extracted icon from executable")
                End Try
            End If

            NotifyIcon.Text = $"{AppName} - {Version}" ' Tooltip
            NotifyIcon.Visible = True

            ' Debug log
            Debug.WriteLine($"NotifyIcon created: Icon={NotifyIcon.Icon IsNot Nothing}, Visible={NotifyIcon.Visible}")

            ' Create context menu
            CmsNotifyIcon = New ContextMenuStrip()

            ' Restore/Show menu item
            TsmiRestore = New ToolStripMenuItem("Show Woodworker's Friend")
            TsmiRestore.Font = New Font(TsmiRestore.Font, FontStyle.Bold)
            AddHandler TsmiRestore.Click, AddressOf TsmiRestore_Click

            ' Locate/Center menu item
            TsmiLocate = New ToolStripMenuItem("Locate/Center on Screen")
            TsmiLocate.ToolTipText = "Centers the window on the primary screen (useful if window is off-screen)"
            AddHandler TsmiLocate.Click, AddressOf TsmiLocate_Click

            ' Separator
            Dim separator1 As New ToolStripSeparator()

            ' Toggle Theme menu item
            TsmiToggleThemeNotify = New ToolStripMenuItem("Toggle Theme")
            TsmiToggleThemeNotify.ToolTipText = "Switch between Light and Dark themes"
            AddHandler TsmiToggleThemeNotify.Click, AddressOf TsmiToggleThemeNotify_Click

            ' Toggle Scale menu item
            TsmiToggleScaleNotify = New ToolStripMenuItem("Toggle Scale")
            TsmiToggleScaleNotify.ToolTipText = "Switch between Imperial and Metric measurements"
            AddHandler TsmiToggleScaleNotify.Click, AddressOf TsmiToggleScaleNotify_Click

            ' Separator
            Dim separator2 As New ToolStripSeparator()

            ' About menu item
            TsmiAboutNotify = New ToolStripMenuItem("About...")
            AddHandler TsmiAboutNotify.Click, AddressOf TsmiAboutNotify_Click

            ' Help menu item
            TsmiHelpNotify = New ToolStripMenuItem("Help...")
            AddHandler TsmiHelpNotify.Click, AddressOf TsmiHelpNotify_Click

            ' Separator
            Dim separator3 As New ToolStripSeparator()

            ' Exit menu item
            TsmiExitNotify = New ToolStripMenuItem("Exit")
            TsmiExitNotify.Font = New Font(TsmiExitNotify.Font, FontStyle.Bold)
            AddHandler TsmiExitNotify.Click, AddressOf TsmiExitNotify_Click

            ' Add items to context menu
            CmsNotifyIcon.Items.AddRange({
                TsmiRestore,
                TsmiLocate,
                separator1,
                TsmiToggleThemeNotify,
                TsmiToggleScaleNotify,
                separator2,
                TsmiAboutNotify,
                TsmiHelpNotify,
                separator3,
                TsmiExitNotify
            })

            ' Attach context menu to NotifyIcon
            NotifyIcon.ContextMenuStrip = CmsNotifyIcon

            ' Handle double-click on tray icon
            AddHandler NotifyIcon.DoubleClick, AddressOf NotifyIcon_DoubleClick

            ' Hook into form minimize to hide to tray (optional behavior)
            ' AddHandler Me.Resize, AddressOf FrmMain_Resize

            ' Debug and log success
            Debug.WriteLine($"System tray icon initialized successfully. Visible={NotifyIcon.Visible}, Icon={NotifyIcon.Icon IsNot Nothing}")
            ErrorHandler.LogError(New Exception("System tray icon initialized"), "InitializeSystemTray")

            ' Force icon to show (sometimes needed)
            NotifyIcon.Visible = False
            System.Threading.Thread.Sleep(100) ' Brief delay
            NotifyIcon.Visible = True

            ' Show detailed debug info
            Dim iconInfo = If(NotifyIcon.Icon IsNot Nothing, $"Icon: {NotifyIcon.Icon.Width}x{NotifyIcon.Icon.Height}", "Icon: NULL")
            Debug.WriteLine($"Final NotifyIcon state: Visible={NotifyIcon.Visible}, {iconInfo}, ContextMenu={NotifyIcon.ContextMenuStrip IsNot Nothing}")

            ' Inform user (can remove after confirming it works)
            MessageBox.Show($"System Tray Icon Status:{Environment.NewLine}" &
                          $"Visible: {NotifyIcon.Visible}{Environment.NewLine}" &
                          $"{iconInfo}{Environment.NewLine}" &
                          $"Context Menu: {NotifyIcon.ContextMenuStrip IsNot Nothing}{Environment.NewLine}" &
                          $"Tooltip: {NotifyIcon.Text}{Environment.NewLine}{Environment.NewLine}" &
                          $"Look for icon in system tray (bottom-right corner){Environment.NewLine}" &
                          $"If not visible, click '^' arrow to show hidden icons.",
                          "System Tray Debug", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeSystemTray")
            ' Non-critical - app can function without tray icon
            MessageBox.Show($"Error initializing system tray:{Environment.NewLine}{ex.Message}{Environment.NewLine}{Environment.NewLine}Stack Trace:{Environment.NewLine}{ex.StackTrace}",
                          "System Tray Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Handles double-click on the tray icon - restores window
    ''' </summary>
    Private Sub NotifyIcon_DoubleClick(sender As Object, e As EventArgs)
        RestoreWindow()
    End Sub

    ''' <summary>
    ''' Handles "Show Woodworker's Friend" menu item - restores window
    ''' </summary>
    Private Sub TsmiRestore_Click(sender As Object, e As EventArgs)
        RestoreWindow()
    End Sub

    ''' <summary>
    ''' Restores the main window from minimized or hidden state
    ''' </summary>
    Private Sub RestoreWindow()
        Try
            If Me.WindowState = FormWindowState.Minimized Then
                Me.WindowState = FormWindowState.Normal
            End If

            Me.Show()
            Me.Activate()
            Me.BringToFront()
            Me.Focus()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "RestoreWindow")
        End Try
    End Sub

    ''' <summary>
    ''' Handles "Locate/Center on Screen" menu item
    ''' Centers the window on the primary screen - useful if window is off-screen
    ''' </summary>
    Private Sub TsmiLocate_Click(sender As Object, e As EventArgs)
        Try
            ' Get primary screen working area
            Dim screen As Screen = Screen.PrimaryScreen
            Dim workingArea As Rectangle = screen.WorkingArea

            ' Calculate center position
            Dim centerX As Integer = workingArea.Left + (workingArea.Width - Me.Width) \ 2
            Dim centerY As Integer = workingArea.Top + (workingArea.Height - Me.Height) \ 2

            ' Set window position
            Me.Location = New Point(centerX, centerY)

            ' Restore if minimized
            If Me.WindowState = FormWindowState.Minimized Then
                Me.WindowState = FormWindowState.Normal
            End If

            ' Bring to front
            Me.Show()
            Me.Activate()
            Me.BringToFront()

            ErrorHandler.LogWarning("SystemTray", "Window centered on screen via tray icon")
        Catch ex As Exception
            ErrorHandler.LogError(ex, "TsmiLocate_Click")
            MessageBox.Show("Error centering window: " & ex.Message,
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Handles "Toggle Theme" menu item from tray icon
    ''' </summary>
    Private Sub TsmiToggleThemeNotify_Click(sender As Object, e As EventArgs)
        Try
            ' Call the existing theme toggle logic
            TsslToggleTheme_Click(sender, e)
        Catch ex As Exception
            ErrorHandler.LogError(ex, "TsmiToggleThemeNotify_Click")
        End Try
    End Sub

    ''' <summary>
    ''' Handles "Toggle Scale" menu item from tray icon
    ''' </summary>
    Private Sub TsmiToggleScaleNotify_Click(sender As Object, e As EventArgs)
        Try
            ' Toggle scale between Imperial and Metric
            If _scaleManager IsNot Nothing Then
                If _scaleManager.CurrentScale = ScaleManager.ScaleType.Imperial Then
                    _scaleManager.SetScale(ScaleManager.ScaleType.Metric)
                    TsslScale.Text = "Metric"
                Else
                    _scaleManager.SetScale(ScaleManager.ScaleType.Imperial)
                    TsslScale.Text = "Imperial"
                End If

                ' Save preference
                DatabaseManager.Instance.SavePreference("Scale",
                    If(_scaleManager.CurrentScale = ScaleManager.ScaleType.Metric, "Metric", "Imperial"),
                    "String", "UI")

                ErrorHandler.LogWarning("SystemTray", $"Scale changed to {_scaleManager.CurrentScale} via tray icon")
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "TsmiToggleScaleNotify_Click")
        End Try
    End Sub

    ''' <summary>
    ''' Handles "About" menu item from tray icon
    ''' </summary>
    Private Sub TsmiAboutNotify_Click(sender As Object, e As EventArgs)
        Try
            ' Navigate to About tab
            Tc.SelectedTab = TpAbout
            RestoreWindow()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "TsmiAboutNotify_Click")
        End Try
    End Sub

    ''' <summary>
    ''' Handles "Help" menu item from tray icon
    ''' </summary>
    Private Sub TsmiHelpNotify_Click(sender As Object, e As EventArgs)
        Try
            ' Navigate to Help tab
            Tc.SelectedTab = TpHelp
            RestoreWindow()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "TsmiHelpNotify_Click")
        End Try
    End Sub

    ''' <summary>
    ''' Handles "Exit" menu item from tray icon - closes application
    ''' </summary>
    Private Sub TsmiExitNotify_Click(sender As Object, e As EventArgs)
        Try
            ' Dispose tray icon before closing
            If NotifyIcon IsNot Nothing Then
                NotifyIcon.Visible = False
                NotifyIcon.Dispose()
            End If

            ' Close application
            Application.Exit()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "TsmiExitNotify_Click")
            ' Force close anyway
            Application.Exit()
        End Try
    End Sub

    ''' <summary>
    ''' Optional: Handle form resize to minimize to tray
    ''' Uncomment to enable "minimize to tray" behavior
    ''' </summary>
    'Private Sub FrmMain_Resize(sender As Object, e As EventArgs)
    '    Try
    '        If Me.WindowState = FormWindowState.Minimized Then
    '            ' Hide form and show only tray icon
    '            Me.Hide()
    '            NotifyIcon.ShowBalloonTip(2000, AppName, "Application minimized to system tray", ToolTipIcon.Info)
    '        End If
    '    Catch ex As Exception
    '        ErrorHandler.LogError(ex, "FrmMain_Resize")
    '    End Try
    'End Sub

    ''' <summary>
    ''' Cleans up the NotifyIcon when form closes
    ''' Call this from FormClosing event
    ''' </summary>
    Private Sub CleanupSystemTray()
        Try
            If NotifyIcon IsNot Nothing Then
                NotifyIcon.Visible = False
                NotifyIcon.Dispose()
                NotifyIcon = Nothing
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "CleanupSystemTray")
        End Try
    End Sub

#End Region

End Class
