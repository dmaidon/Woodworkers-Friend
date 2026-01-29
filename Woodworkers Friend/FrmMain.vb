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
    'Private _scaleManager As New ScaleManager()

    Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
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
            InitializeSystem()
            InitializeManagers()
            InitializeUI()
            ApplyTheme(CurrentTheme)

            ' RtfParser.ParseRtfTableToCsv("c:\temp\hardwood.pdf.rtf", "c:\temp\hardwood.csv")
        Catch ex As Exception
            MessageBox.Show($"Error during form load: {ex.Message}{vbCrLf}{vbCrLf}Stack trace:{vbCrLf}{ex.StackTrace}",
                          "Startup Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub InitializeSystem()
        CreateprogramFolders()
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
        InitializeCutListOptimizer()

        TmrRotation.Interval = CInt(1000 / 60) ' 60 FPS
        TmrRotation.Start()
        AddHandler TmrRotation.Tick, AddressOf TmrRotation_Tick
        AddHandler PbOutputDrawing.Paint, AddressOf PbOutputDrawing_Paint

        Me.Text = $"{AppName} - {Version}"
        TsslCpy.Text = GetCopyrightNotice()
        TsslVersion.Text = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString()
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

End Class
