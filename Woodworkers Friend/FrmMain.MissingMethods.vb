Partial Public Class FrmMain

#Region "Missing Fields and Properties"

    '' Manager instances
    'Private _eventCoordinator As CalculatorEventCoordinator

    'Private _themeManager As ThemeManager
    'Private _projectManager As ProjectManager
    'Private _validationManager As ValidationManager
    'Private _presetManager As PresetManager

    '' State variables
    'Private _loading As Boolean = False  ' UNCOMMENT THIS LINE

    'Private _calculatorInitialized As Boolean = False

    '' Declare these as WithEvents to support the Handles clause
    'Private WithEvents BtnKitchenStandardPreset As Button

    'Private WithEvents BtnBathroomVanityPreset As Button
    'Private WithEvents BtnOfficeDeskPreset As Button
    'Private WithEvents BtnCustomCabinetPreset As Button

    '' Other required controls that may be missing
    'Public Property TxtDrawerCount As TextBox

    'Public Property TxtDrawerSpacing As TextBox
    'Public Property TxtDrawerWidth As TextBox
    'Public Property TxtFirstDrawerHeight As TextBox
    'Public Property TxtMultiplier As TextBox
    'Public Property TxtArithmeticIncrement As TextBox
    'Public Property TxtProjectName As TextBox

    'Public Property RbHambridge As RadioButton
    'Public Property RbGeometric As RadioButton
    'Public Property RbFibonacci As RadioButton
    'Public Property RbArithmetic As RadioButton
    'Public Property RbImperial As RadioButton
    'Public Property RbMetric As RadioButton

    'Public Property BtnCalculateDrawers As Button
    'Public Property BtnSaveProject As Button

    'Public Property LblStatus As Label
    'Public Property LblTotalHeightResults As Label
    'Public Property LbltotalDrawerHeightResults As Label
    'Public Property LblTotalMaterialResults As Label
    'Public Property LblAverageHeightResults As Label
    'Public Property LblHeightRatioResults As Label

    'Public Property RtbResults As RichTextBox
    'Public Property RtbLog As RichTextBox
    'Public Property DgvDrawerHeights As DataGridView

#End Region

#Region "Missing Methods"

    ''' <summary>
    ''' Shows an error message dialog
    ''' </summary>
    Private Sub ShowErrorMessage(title As String, message As String)
        MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Sub

    ''' <summary>
    ''' Enables or disables export buttons
    ''' </summary>
    Private Sub EnableExportButtons(enabled As Boolean)
        ' Add your export buttons here as they become available
        ' Example:
        ' BtnExportToPDF.Enabled = enabled
        ' BtnExportToExcel.Enabled = enabled
        ' BtnPrintResults.Enabled = enabled
    End Sub

    ''' <summary>
    ''' Clears all result displays
    ''' </summary>
    Private Sub ClearResults()
        Try
            ' Clear text results
            If RtbResults IsNot Nothing Then RtbResults.Clear()

            ' Clear summary labels
            If LblTotalHeightResults IsNot Nothing Then LblTotalHeightResults.Text = "Total Height: --"
            If LbltotalDrawerHeightResults IsNot Nothing Then LbltotalDrawerHeightResults.Text = "Total Drawer Height: --"
            If LblTotalMaterialResults IsNot Nothing Then LblTotalMaterialResults.Text = "Total Material: --"
            If LblAverageHeightResults IsNot Nothing Then LblAverageHeightResults.Text = "Average Height: --"
            If LblHeightRatioResults IsNot Nothing Then LblHeightRatioResults.Text = "Height Ratio: --"

            ' Clear data grid
            If DgvDrawerHeights IsNot Nothing Then DgvDrawerHeights.Rows.Clear()
        Catch ex As Exception
            RtbLog?.AppendText($"{Now:hh:mm:ss} - Error clearing results: {ex.Message}{vbCrLf}")
        End Try
    End Sub

    ''' <summary>
    ''' Initializes the calculator components
    ''' </summary>
    Private Sub InitializeCalculator()
        Try
            ' Initialize managers
            _eventCoordinator = New CalculatorEventCoordinator(Me)
            _themeManager = New ThemeManager()
            _projectManager = New ProjectManager()
            _validationManager = New ValidationManager()
            _presetManager = New PresetManager()

            ' Set up initial state
            SetupDataGridView()
            SetupLabelTags()

            _calculatorInitialized = True
        Catch ex As Exception
            RtbLog?.AppendText($"{Now:hh:mm:ss} - Error initializing calculator: {ex.Message}{vbCrLf}")
        End Try
    End Sub

    ''' <summary>
    ''' Sets up the DataGridView for drawer heights
    ''' </summary>
    Private Sub SetupDataGridView()
        If DgvDrawerHeights Is Nothing Then Return

        Try
            DgvDrawerHeights.Columns.Clear()

            ' Add columns
            DgvDrawerHeights.Columns.Add("DrawerNumber", "Drawer #")
            DgvDrawerHeights.Columns.Add("DwHeight", "Height")
            DgvDrawerHeights.Columns.Add("Unit", "Unit")
            DgvDrawerHeights.Columns.Add("Percentage", "% of Total")

            ' Set column properties
            DgvDrawerHeights.Columns("DrawerNumber").Width = 80
            DgvDrawerHeights.Columns("DwHeight").Width = 100
            DgvDrawerHeights.Columns("Unit").Width = 60
            DgvDrawerHeights.Columns("Percentage").Width = 80

            ' Make read-only
            DgvDrawerHeights.ReadOnly = True
            DgvDrawerHeights.AllowUserToAddRows = False
            DgvDrawerHeights.AllowUserToDeleteRows = False
        Catch ex As Exception
            RtbLog?.AppendText($"{Now:hh:mm:ss} - Error setting up DataGridView: {ex.Message}{vbCrLf}")
        End Try

    End Sub

    ''' <summary>
    ''' Sets up label tags for string formatting
    ''' </summary>
    Private Sub SetupLabelTags()
        Try
            If LblTotalHeightResults IsNot Nothing Then LblTotalHeightResults.Tag = "Total Height: {0}"
            If LbltotalDrawerHeightResults IsNot Nothing Then LbltotalDrawerHeightResults.Tag = "Total Drawer Height: {0}"
            If LblTotalMaterialResults IsNot Nothing Then LblTotalMaterialResults.Tag = "Total Material: {0}"
            If LblAverageHeightResults IsNot Nothing Then LblAverageHeightResults.Tag = "Average Height: {0}"
            If LblHeightRatioResults IsNot Nothing Then LblHeightRatioResults.Tag = "Height Ratio: {0}"
        Catch ex As Exception
            RtbLog?.AppendText($"{Now:hh:mm:ss} - Error setting up label tags: {ex.Message}{vbCrLf}")
        End Try
    End Sub

#End Region

#Region "Form Events"

    ''' <summary>
    ''' Override the Load event to initialize everything
    ''' </summary>
    Protected Overrides Sub OnLoad(e As EventArgs)
        MyBase.OnLoad(e)

        _loading = True
        InitializeCalculator()
        _loading = False
    End Sub

#End Region

End Class