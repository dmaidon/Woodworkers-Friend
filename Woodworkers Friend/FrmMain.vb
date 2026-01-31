Imports System.ComponentModel

Public Class FrmMain

    Private CurrentTheme As AppTheme = AppTheme.Light
    Private _eventCoordinator As CalculatorEventCoordinator
    Private _themeManager As ThemeManager
    Private _projectManager As ProjectManager
    Private _printManager As PrintManager
    Private _validationManager As ValidationManager
    Private _presetManager As PresetManager
    Private _calculatorInitialized As Boolean = False
    Private _loading As Boolean = False  ' ADD THIS LINE
    Private _resourceManager As New List(Of IDisposable)

    ' Add a field to track the current scale
    ' Note: _scaleManager is declared in FrmMain.DoorCalculations.vb partial class
    'Private _scaleManager As New ScaleManager()

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ' CRITICAL: Initialize database FIRST - required for TimesRun
            CreateProgramFolders()

            Try
                Dim dbManager = DatabaseManager.Instance
                TimesRun = dbManager.GetTimesRun()  ' Get current value
                TimesRun += 1                        ' Increment it
                dbManager.SavePreference("TimesRun", TimesRun.ToString(), "Integer", "System")  ' Save as Integer, not Long

                ' Load LogKeepDays BEFORE cleanup so cleanup uses user's preferred retention
                Dim logKeepDays = dbManager.GetIntPreference("LogKeepDays", 10)
                If logKeepDays < 5 Then logKeepDays = 5
                If logKeepDays > 100 Then logKeepDays = 100
                MaxLogAgeInDays = logKeepDays
            Catch ex As Exception
                TimesRun = 0
                MaxLogAgeInDays = 5  ' Use minimum if load fails
            End Try

            ' Initialize logging system - uses TimesRun in filename
            ErrorHandler.CleanupOldLogs()  ' Clean up old log files (now uses user's MaxLogAgeInDays)
            ErrorHandler.LogStartup()      ' Log application startup
            ErrorHandler.LogError(New Exception($"Application started - Run #{TimesRun}"), "FrmMain_Load")

            ' Show splash screen
            Using splash As New FrmSplash()
                splash.Opacity = 1
                splash.Show()
                Application.DoEvents()
                Threading.Thread.Sleep(2500) ' Show splash for 1.8 seconds
                splash.FadeOut(1000)          ' Fade out over 0.8 seconds
                splash.Close()
            End Using

            AttachSelectAllHandlerToTextBoxes(Me)
            CompleteSystemInitialization()
            InitializeManagers()
            InitializeUI()
            LoadUserPreferences()
            ApplyTheme(CurrentTheme)

            ' RtfParser.ParseRtfTableToCsv("c:\temp\hardwood.pdf.rtf", "c:\temp\hardwood.csv")
        Catch ex As Exception
            MessageBox.Show($"Error during form load: {ex.Message}{vbCrLf}{vbCrLf}Stack trace:{vbCrLf}{ex.StackTrace}",
                          "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub CompleteSystemInitialization()
        ' Complete remaining database initialization (migrations, etc.)
        Try
            ' Perform initial data migration on first run
            DataMigration.PerformInitialMigration()

            ErrorHandler.LogError(New Exception($"Database initialized at: {DatabaseManager.Instance.DatabasePath}"), "CompleteSystemInitialization")
        Catch ex As Exception
            ErrorHandler.LogError(ex, "CompleteSystemInitialization - Database migration failed")
            ' Continue anyway - app can still function with in-code data
        End Try
    End Sub

    Private Sub InitializeSystem()
        CreateProgramFolders()

        ' Initialize database (SQLite) - Phase 1 of unified database migration
        Try
            ' This initializes the database and creates schema if needed
            Dim dbManager = DatabaseManager.Instance

            ' Perform initial data migration on first run
            DataMigration.PerformInitialMigration()

            ErrorHandler.LogError(New Exception($"Database initialized at: {dbManager.DatabasePath}"), "InitializeSystem")
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeSystem - Database initialization failed")
            ' Continue anyway - app can still function with in-code data
        End Try

        ' LogRoutines.InitializeLogging()
        ' Settings.LoadUserSettings()
    End Sub

    Private Sub InitializeUI()

#Region "Epoxy pour"

        RbEpoxyWaste0.Checked = True
        If LblEpoxyOunces.Tag Is Nothing Then LblEpoxyOunces.Tag = "Ounces: {0:N2} oz"
        If LblEpoxyGallons.Tag Is Nothing Then LblEpoxyGallons.Tag = "Gallons: {0:N2} gal"
        If LblEpoxyQuarts.Tag Is Nothing Then LblEpoxyQuarts.Tag = "Quarts: {0:N2} qt"
        If LblEpoxyPints.Tag Is Nothing Then LblEpoxyPints.Tag = "Pints: {0:N2} pt"

#End Region

#Region "Drawer Calculations"

        SetupDrawerCalculationRadioButtons()

#End Region

        InitializeDoorControls()
        InitializeJoineryCalculator()
        InitializeWoodMovementCalculator()
        InitializeWoodMovementEvents()
        InitializeShelfSagCalculator()
        InitializeCutListOptimizer()
        InitializeWoodPropertiesReference()
        InitializeSafetyCalculator()
        InitializeSandingGritCalculator()
        InitializeClampBiscuitCalculator()
        InitializeDadoStackCalculator()
        InitializePolygonCalculator()

        ' Phase 7.1 & 7.2: Initialize to wire up Enter events (data loads on first tab visit)
        InitializeJoineryReference()
        InitializeHardwareReference()

        TmrRotation.Interval = CInt(1000 / 60) ' 60 FPS
        TmrRotation.Start()
        AddHandler TmrRotation.Tick, AddressOf TmrRotation_Tick
        AddHandler PbOutputDrawing.Paint, AddressOf PbOutputDrawing_Paint

        Me.Text = $"{AppName} - {Version}"
        TsslCpy.Text = GetCopyrightNotice()
        TsslVersion.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
        TsslTimesRun.Text = TimesRun.ToString

        ' Initialize system tray icon
        InitializeSystemTray()

        Show()

    End Sub

    Private Sub InitializeManagers()
        ' Create managers
        _eventCoordinator = New CalculatorEventCoordinator(Me)
        _themeManager = New ThemeManager()
        _projectManager = New ProjectManager()
        _printManager = New PrintManager()
        _validationManager = New ValidationManager()
        _presetManager = New PresetManager()

        ' Initialize legacy board feet manager (Phase 2 refactoring)
        'Dim summaryLabels As New BoardFeetGridManager.BoardFeetSummaryLabels With {
        '    .Total = LblTotalBoardFeet,
        '    .Plus10Percent = LblTotalBoardFeet10,
        '    .Plus15Percent = LblTotalBoardFeet15,
        '    .Plus20Percent = LblTotalBoardFeet20
        '}
        ' Configure managers
        ''_themeManager.SetBlueTheme() ' Default theme
        CalculatorEventCoordinator.InitializeEventHandlers()

        ' Add managers to resource manager for proper disposal
        _resourceManager.AddRange({_themeManager, _printManager})
    End Sub

    Public Sub ApplyTheme(theme As AppTheme)
        CurrentTheme = theme
        If theme = AppTheme.Dark Then
            Me.BackColor = Color.FromArgb(32, 32, 32)
            Me.ForeColor = Color.White
            For Each ctrl As Control In Me.Controls
                If TypeOf ctrl IsNot TabPage Then
                    ctrl.BackColor = Color.FromArgb(48, 48, 48)
                    ctrl.ForeColor = Color.White
                End If
            Next
            ' Set specific controls
            Ss1.BackColor = Color.FromArgb(48, 48, 48)
            Ss1.ForeColor = Color.White
            Ss2.BackColor = Color.FromArgb(48, 48, 48)
            Ss2.ForeColor = Color.White
            Ss3.BackColor = Color.FromArgb(48, 48, 48)
            Ss3.ForeColor = Color.White
            Tc.BackColor = Color.Silver
        Else
            Me.BackColor = SystemColors.Control
            Me.ForeColor = Color.Black
            For Each ctrl As Control In Me.Controls
                If TypeOf ctrl IsNot TabPage Then
                    ctrl.BackColor = SystemColors.Control
                    ctrl.ForeColor = Color.Black
                End If
            Next
            ' Set specific controls
            Ss1.BackColor = SystemColors.Control
            Ss1.ForeColor = Color.Black
            Ss2.BackColor = SystemColors.Control
            Ss2.ForeColor = Color.Black
            Ss3.BackColor = SystemColors.Control
            Ss3.ForeColor = Color.Black
            Tc.BackColor = SystemColors.Control
            Tc.ForeColor = Color.Black
            TpBoardfeet.BackColor = Color.DarkGray

        End If
    End Sub

    Private Sub TsslToggleTheme_Click(sender As Object, e As EventArgs) Handles TsslToggleTheme.Click
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)

        If CurrentTheme = AppTheme.Light Then
            ApplyTheme(AppTheme.Dark)
            TsslToggleTheme.Text = "Dark Mode"
            TsslToggleTheme.ForeColor = Color.Red
            TsslToggleTheme.Image = My.Resources.moon_64
        Else
            ApplyTheme(AppTheme.Light)
            TsslToggleTheme.Text = "Light Mode"
            TsslToggleTheme.ForeColor = Color.DarkRed
            TsslToggleTheme.Image = My.Resources.bulb_64
        End If

        ' Persist theme preference to database (Phase 5)
        DatabaseManager.Instance.SavePreference("Theme", If(CurrentTheme = AppTheme.Dark, "Dark", "Light"), "String", "UI")
    End Sub

    ''' <summary>
    ''' Timer to Polygon rotation
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub TmrRotation_Tick(sender As Object, e As EventArgs)
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
        TmrRotation_Tick()
    End Sub

#Region "Textbox handler"

    Private Sub AttachSelectAllHandlerToTextBoxes(parent As Control)
        ArgumentNullException.ThrowIfNull(parent)

        For Each ctrl As Control In parent.Controls
            If TypeOf ctrl Is TextBox Then
                AddHandler ctrl.Enter, AddressOf SelectAllTextBoxText
            ElseIf TypeOf ctrl Is Panel OrElse ctrl.HasChildren Then
                AttachSelectAllHandlerToTextBoxes(ctrl)
            End If
        Next
    End Sub

    Private Sub SelectAllTextBoxText(sender As Object, e As EventArgs)
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
        Dim tb = TryCast(sender, TextBox)
        If tb IsNot Nothing Then tb.SelectAll()
    End Sub

#End Region

    ' Add this to your InitializeUI method:
    Private Sub SetupDrawerCalculationRadioButtons()
        ' Ensure all radio button tags are set
        If RbHambridge IsNot Nothing Then RbHambridge.Tag = 0
        If RbGeometric IsNot Nothing Then RbGeometric.Tag = 1
        If RbFibonacci IsNot Nothing Then RbFibonacci.Tag = 2
        If RbArithmetic IsNot Nothing Then RbArithmetic.Tag = 3
        If RbLogarithmic IsNot Nothing Then RbLogarithmic.Tag = 4
        If RbExponential IsNot Nothing Then RbExponential.Tag = 5
        If RbCustomRatio IsNot Nothing Then RbCustomRatio.Tag = 6
        If RbUniform IsNot Nothing Then RbUniform.Tag = 7
        If RbReverseArithmetic IsNot Nothing Then RbReverseArithmetic.Tag = 8
        If RbGoldenRatio IsNot Nothing Then RbGoldenRatio.Tag = 9

        ' Set default selection
        If RbHambridge IsNot Nothing Then RbHambridge.Checked = True
    End Sub

#Region "Scale Set Methods"

    Private _scaleManager As New ScaleManager()

    Private Sub RbImperial_CheckedChanged(sender As Object, e As EventArgs)
        If RbImperial.Checked Then
            _scaleManager.SetScale(ScaleManager.ScaleType.Imperial)
            TsslScale.Text = "Imperial"
            TsslScale.ForeColor = Color.ForestGreen
        End If
    End Sub

    Private Sub RbMetric_CheckedChanged(sender As Object, e As EventArgs)
        If RbMetric.Checked Then
            _scaleManager.SetScale(ScaleManager.ScaleType.Metric)
            TsslScale.Text = "Metric"
            TsslScale.ForeColor = Color.Firebrick

        End If
    End Sub

    ' Add this event handler to toggle scale when TsslScale is clicked
    Private Sub TsslScale_Click(sender As Object, e As EventArgs) Handles TsslScale.Click
        If _scaleManager.CurrentScale = ScaleManager.ScaleType.Imperial Then
            _scaleManager.SetScale(ScaleManager.ScaleType.Metric)
            TsslScale.Text = "Metric"
            TsslScale.ForeColor = Color.Firebrick
            RbMetric.Checked = True
        Else
            _scaleManager.SetScale(ScaleManager.ScaleType.Imperial)
            TsslScale.Text = "Imperial"
            TsslScale.ForeColor = Color.ForestGreen
            RbImperial.Checked = True
        End If

        ' Persist scale preference to database (Phase 5)
        DatabaseManager.Instance.SavePreference("Scale",
            If(_scaleManager.CurrentScale = ScaleManager.ScaleType.Metric, "Metric", "Imperial"), "String", "UI")
    End Sub

    Private Sub TpDrawings_Enter(sender As Object, e As EventArgs) Handles TpDrawings.Enter
        TsslToggleDoorExploded.Enabled = True
        TsslToggleDoorExploded.Visible = True
    End Sub

    Private Sub TpDrawings_Leave(sender As Object, e As EventArgs) Handles TpDrawings.Leave
        TsslToggleDoorExploded.Enabled = False
        TsslToggleDoorExploded.Visible = False
    End Sub

    Private Sub TmrClock_Tick(sender As Object, e As EventArgs)
        TsslClock.Text = Now.ToLongTimeString
    End Sub

#End Region

#Region "User Preferences (Phase 5)"

    ''' <summary>
    ''' Loads user preferences from database on startup
    ''' </summary>
    Private Sub LoadUserPreferences()
        Try
            Dim db = DatabaseManager.Instance

            ' Load theme preference
            Dim savedTheme = db.GetPreference("Theme", "Light")
            If savedTheme.Equals("Dark", StringComparison.OrdinalIgnoreCase) Then
                CurrentTheme = AppTheme.Dark
                TsslToggleTheme.Text = "Dark Mode"
                TsslToggleTheme.ForeColor = Color.Red
                TsslToggleTheme.Image = My.Resources.moon_64
            Else
                CurrentTheme = AppTheme.Light
                TsslToggleTheme.Text = "Light Mode"
                TsslToggleTheme.ForeColor = Color.DarkRed
                TsslToggleTheme.Image = My.Resources.bulb_64
            End If

            ' Load scale preference
            Dim savedScale = db.GetPreference("Scale", "Imperial")
            If savedScale.Equals("Metric", StringComparison.OrdinalIgnoreCase) Then
                _scaleManager.SetScale(ScaleManager.ScaleType.Metric)
                TsslScale.Text = "Metric"
                TsslScale.ForeColor = Color.Firebrick
                RbMetric.Checked = True
            Else
                _scaleManager.SetScale(ScaleManager.ScaleType.Imperial)
                TsslScale.Text = "Imperial"
                TsslScale.ForeColor = Color.ForestGreen
                RbImperial.Checked = True
            End If

            ' Load last active tab
            Dim lastTab = db.GetIntPreference("LastActiveTab", 0)
            If lastTab >= 0 AndAlso lastTab < Tc.TabCount Then
                Tc.SelectedIndex = lastTab
            End If

            ' Load window state
            Dim savedState = db.GetPreference("WindowState", "Normal")
            Dim savedWidth = db.GetIntPreference("WindowWidth", 1200)
            Dim savedHeight = db.GetIntPreference("WindowHeight", 800)

            If savedState.Equals("Maximized", StringComparison.OrdinalIgnoreCase) Then
                Me.WindowState = FormWindowState.Maximized
            Else
                If savedWidth > 100 AndAlso savedHeight > 100 Then
                    Me.Width = Math.Min(savedWidth, Screen.PrimaryScreen.WorkingArea.Width)
                    Me.Height = Math.Min(savedHeight, Screen.PrimaryScreen.WorkingArea.Height)
                End If
            End If

            ErrorHandler.LogError(New Exception($"Preferences loaded: Theme={savedTheme}, Scale={savedScale}"), "LoadUserPreferences")

            ' Load LogKeep setting
            LoadLogKeepSetting()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "LoadUserPreferences")
            ' Continue with defaults - non-critical
        End Try
    End Sub

    ''' <summary>
    ''' Saves user preferences to database (called on close and on changes)
    ''' </summary>
    Private Sub SaveUserPreferences()
        Try
            Dim db = DatabaseManager.Instance

            ' Save theme
            db.SavePreference("Theme", If(CurrentTheme = AppTheme.Dark, "Dark", "Light"), "String", "UI")

            ' Save scale
            db.SavePreference("Scale", If(_scaleManager.CurrentScale = ScaleManager.ScaleType.Metric, "Metric", "Imperial"), "String", "UI")

            ' Save last active tab
            db.SavePreference("LastActiveTab", Tc.SelectedIndex.ToString(), "Integer", "General")

            ' Save window state
            If Me.WindowState = FormWindowState.Maximized Then
                db.SavePreference("WindowState", "Maximized", "String", "UI")
            Else
                db.SavePreference("WindowState", "Normal", "String", "UI")
                If Me.WindowState = FormWindowState.Normal Then
                    db.SavePreference("WindowWidth", Me.Width.ToString(), "Integer", "UI")
                    db.SavePreference("WindowHeight", Me.Height.ToString(), "Integer", "UI")
                End If
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SaveUserPreferences")
        End Try
    End Sub

    ''' <summary>
    ''' Loads the LogKeep setting from database and sets global MaxLogAgeInDays
    ''' </summary>
    Private Sub LoadLogKeepSetting()
        Try
            Dim db = DatabaseManager.Instance
            Dim logKeepDays = db.GetIntPreference("LogKeepDays", 10)

            ' Ensure value is within valid range (5-100)
            If logKeepDays < 5 Then logKeepDays = 5
            If logKeepDays > 100 Then logKeepDays = 100

            ' Set the global variable
            MaxLogAgeInDays = logKeepDays

            ' Update the NumericUpDown control
            If NudLogKeep IsNot Nothing Then
                NudLogKeep.Value = logKeepDays
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "LoadLogKeepSetting")
            ' Use minimum of 5 if loading fails
            MaxLogAgeInDays = 5
            If NudLogKeep IsNot Nothing Then
                NudLogKeep.Value = 5
            End If
        End Try
    End Sub

    ''' <summary>
    ''' Saves the LogKeep setting to database and updates global MaxLogAgeInDays
    ''' </summary>
    Private Sub SaveLogKeepSetting(value As Integer)
        Try
            Dim db = DatabaseManager.Instance
            db.SavePreference("LogKeepDays", value.ToString(), "Integer", "System")
            MaxLogAgeInDays = value
            ErrorHandler.LogWarning("LogKeepSetting", $"Log retention changed to {value} days")
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SaveLogKeepSetting")
        End Try
    End Sub

    Private Sub FrmMain_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        SaveUserPreferences()
        CleanupSystemTray()
    End Sub

#End Region

End Class
