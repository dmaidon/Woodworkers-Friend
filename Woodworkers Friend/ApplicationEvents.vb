Imports Microsoft.VisualBasic.ApplicationServices

Namespace My
    ' The following events are available for MyApplication:
    ' Startup: Raised when the application starts, before the startup form is created.
    ' Shutdown: Raised after all application forms are closed.  This event is not raised if the application terminates abnormally.
    ' UnhandledException: Raised if the application encounters an unhandled exception.
    ' StartupNextInstance: Raised when launching a single-instance application and the application is already active. 
    ' NetworkAvailabilityChanged: Raised when the network connection is connected or disconnected.

    ' **NEW** ApplyApplicationDefaults: Raised when the application queries default values to be set for the application.

    ' Example:
    ' Private Sub MyApplication_ApplyApplicationDefaults(sender As Object, e As ApplyApplicationDefaultsEventArgs) Handles Me.ApplyApplicationDefaults
    '
    '   ' Setting the application-wide default Font:
    '   e.Font = New Font(FontFamily.GenericSansSerif, 12, FontStyle.Regular)
    '
    '   ' Setting the HighDpiMode for the Application:
    '   e.HighDpiMode = HighDpiMode.PerMonitorV2
    '
    '   ' If a splash dialog is used, this sets the minimum display time:
    '   e.MinimumSplashScreenDisplayTime = 4000
    ' End Sub

    Partial Friend Class MyApplication

        ''' <summary>
        ''' Handles StartupNextInstance event for single-instance application
        ''' This fires when user tries to start a second instance
        ''' </summary>
        Private Sub MyApplication_StartupNextInstance(sender As Object, e As StartupNextInstanceEventArgs) Handles Me.StartupNextInstance
            Try
                ' Get the main form
                Dim mainForm = TryCast(Me.MainForm, FrmMain)
                If mainForm IsNot Nothing Then
                    ' Restore the window if minimized
                    If mainForm.WindowState = FormWindowState.Minimized Then
                        mainForm.WindowState = FormWindowState.Normal
                    End If

                    ' Bring window to front
                    mainForm.Show()
                    mainForm.Activate()
                    mainForm.BringToFront()
                    
                    ' Set focus
                    mainForm.Focus()
                End If
            Catch ex As Exception
                ' Log error but don't crash
                System.Diagnostics.Debug.WriteLine($"StartupNextInstance error: {ex.Message}")
            End Try
        End Sub

        ''' <summary>
        ''' Apply application-wide defaults
        ''' </summary>
        Private Sub MyApplication_ApplyApplicationDefaults(sender As Object, e As ApplyApplicationDefaultsEventArgs) Handles Me.ApplyApplicationDefaults
            ' Set color mode for dark mode support (.NET 9+)
            e.ColorMode = SystemColorMode.System
            
            ' Set HighDPI mode
            e.HighDpiMode = HighDpiMode.SystemAware
        End Sub

    End Class
End Namespace
