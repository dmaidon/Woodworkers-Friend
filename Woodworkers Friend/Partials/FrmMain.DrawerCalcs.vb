Imports System.Media ' <-- Add this line for SystemSounds

Partial Public Class FrmMain

    Private _lastDrawerResult As DrawerCalculationResult
    Private _lastDrawerParameters As DrawerCalculationParameters

#Region "Drawer Calculator Event Handlers - Simplified"

    ''' <summary>
    ''' Main drawer calculation button - delegates to engine
    ''' </summary>
    Private Sub BtnCalculateDrawers_Click(sender As Object, e As EventArgs) Handles BtnCalculateDrawers.Click
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)

        Try
            SetCalculatingState(True)

            ' Collect using helper
            Dim parameters As DrawerCalculationParameters = WwFriend.Modules.Drawers.DrawerParameterCollector.Collect(Me)

            ' Calculate
            Dim result As DrawerCalculationResult = DrawerCalculationEngine.Calculate(parameters)

            ' Present results
            WwFriend.Modules.Drawers.DrawerResultsPresenter.Present(Me, result)

            _lastDrawerResult = result
            _lastDrawerParameters = parameters

            SetCalculatingState(False)

            If result.IsValid Then
                UpdateDrawerStatus("Calculation completed successfully", Color.Green)
                EnableExportButtons(True)
                BtnDrawDrawerImage.Enabled = True

                SetDrawerDrawingContext()
                If BtnDrawDrawerImage IsNot Nothing Then BtnDrawDrawerImage.Enabled = True
            Else
                UpdateDrawerStatus($"Calculation failed: {result.ErrorMessage}", Color.Red)
                ShowErrorMessage("Calculation Error", result.ErrorMessage)
                BtnDrawDrawerImage.Enabled = False
            End If
        Catch ex As Exception
            SetCalculatingState(False)
            UpdateDrawerStatus("Calculation failed - see details", Color.Red)
            ShowErrorMessage("Calculation Error", $"An unexpected error occurred:{vbCrLf}{vbCrLf}{ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Preset button handlers - delegate to PresetManager
    ''' </summary>
    Private Sub BtnKitchenStandardPreset_Click(sender As Object, e As EventArgs) Handles BtnKitchenStandardPreset.Click
        PresetManager.ApplyDrawerPreset("kitchen_standard", Me)
        UpdateDrawerStatus("Kitchen standard preset applied", Color.Green)
    End Sub

    Private Sub BtnBathroomVanityPreset_Click(sender As Object, e As EventArgs) Handles BtnBathroomVanityPreset.Click
        PresetManager.ApplyDrawerPreset("bathroom_vanity", Me)
        UpdateDrawerStatus("Bathroom vanity preset applied", Color.Green)
    End Sub

    Private Sub BtnOfficeDeskPreset_Click(sender As Object, e As EventArgs) Handles BtnOfficeDeskPreset.Click
        PresetManager.ApplyDrawerPreset("office_desk", Me)
        UpdateDrawerStatus("Office desk preset applied", Color.Green)
    End Sub

    Private Sub BtnCustomCabinetPreset_Click(sender As Object, e As EventArgs) Handles BtnCustomCabinetPreset.Click
        PresetManager.ApplyDrawerPreset("custom_cabinet", Me)
        UpdateDrawerStatus("Custom cabinet preset applied", Color.Green)
    End Sub

    Private Sub BtnGoldenRatioPreset_Click(sender As Object, e As EventArgs) Handles BtnGoldenRatioPreset.Click
        PresetManager.ApplyDrawerPreset("golden_ratio", Me)
        UpdateDrawerStatus("Golden ratio preset applied", Color.Green)
    End Sub

    Private Sub BtnReverseArithmeticPreset_Click(sender As Object, e As EventArgs) Handles BtnReverseArithmeticPreset.Click
        PresetManager.ApplyDrawerPreset("reverse_arithmetic", Me)
        UpdateDrawerStatus("Reverse Arithmetic preset applied", Color.Green)
    End Sub

    Private Sub BtnLogarithmicPreset_Click(sender As Object, e As EventArgs) Handles BtnLogarithmicProgressionPreset.Click
        PresetManager.ApplyDrawerPreset("logarithmic_example", Me)
        UpdateDrawerStatus("Logarithmic preset applied", Color.Green)
    End Sub

    Private Sub BtnExponentialPreset_Click(sender As Object, e As EventArgs) Handles BtnExponentialProgressionPreset.Click
        PresetManager.ApplyDrawerPreset("exponential_example", Me)
        UpdateDrawerStatus("Exponential preset applied", Color.Green)
    End Sub

    Private Sub BtnCustomRatioPreset_Click(sender As Object, e As EventArgs) Handles BtnCustomRatioPreset.Click
        PresetManager.ApplyDrawerPreset("custom_ratio_example", Me)
        UpdateDrawerStatus("Custom ratio preset applied", Color.Green)
    End Sub

    Private Sub BtnUniformPreset_Click(sender As Object, e As EventArgs) Handles BtnUniformPreset.Click
        PresetManager.ApplyDrawerPreset("uniform_example", Me)
        UpdateDrawerStatus("Uniform preset applied", Color.Green)
    End Sub

#End Region

    Private Sub DisplayDrawerResults(result As DrawerCalculationResult)
        ArgumentNullException.ThrowIfNull(result)
        If Not result.IsValid Then
            WwFriend.Modules.Drawers.DrawerResultsPresenter.Clear(Me)
            Return
        End If

        WwFriend.Modules.Drawers.DrawerResultsPresenter.Present(Me, result)
    End Sub

    Private Sub SetCalculatingState(calculating As Boolean)
        Cursor = If(calculating, Cursors.WaitCursor, Cursors.Default)
        _eventCoordinator?.SetControlsEnabled(Not calculating)
        If calculating Then UpdateDrawerStatus("Calculating...", Color.Blue)
        Application.DoEvents()
    End Sub

    Private Sub UpdateSaveButtonState()
        If BtnSaveProject IsNot Nothing Then
            BtnSaveProject.Enabled = Not String.IsNullOrWhiteSpace(TxtDrawerProjectName?.Text) AndAlso
                                    Not _loading AndAlso _calculatorInitialized
        End If
    End Sub

    Private Sub UpdateDrawerStatus(message As String, color As Color)
        If LblStatus IsNot Nothing Then
            LblStatus.Text = $"Drawer Calculator: {message}"
            LblStatus.ForeColor = color
        End If
    End Sub

    ''' <summary>
    ''' Update text control safely
    ''' </summary>
    Private Sub UpdateTextControl(control As Control, value As String)
        If control IsNot Nothing Then
            If TypeOf control Is TextBox Then
                DirectCast(control, TextBox).Text = value
            ElseIf TypeOf control Is RichTextBox Then
                DirectCast(control, RichTextBox).Text = value
            End If
        End If
    End Sub

    Private Sub UpdateLabelControl(control As Label, value As String)
        If control IsNot Nothing Then
            control.Text = value
        End If
    End Sub

    ' Real-time validation on TextBox Leave events
    Private Sub TxtDrawerCount_Leave(sender As Object, e As EventArgs) Handles TxtDrawerCount.Leave
        ValidateDrawerCountInput()
    End Sub

    Private Sub ValidateInputOnChange(sender As Object, e As EventArgs)
        ' Real-time validation feedback
        Dim textBox As TextBox = TryCast(sender, TextBox)
        If textBox IsNot Nothing Then
            ' Clear any previous error styling
            textBox.BackColor = SystemColors.Window

            ' You can add specific validation logic here based on the textbox
            Select Case textBox.Name
                Case "TxtDrawerCount"
                    ValidateDrawerCountInput()
                Case "TxtFirstDrawerHeight"
                    ValidateFirstDrawerHeight()
                    ' Add other cases as needed
            End Select
        End If
    End Sub

    Private Sub ValidateDrawerCountInput()
        If TxtDrawerCount Is Nothing Then Return

        Dim value As Integer
        If Not Integer.TryParse(TxtDrawerCount.Text, value) Then
            ShowInputError(TxtDrawerCount, "Please enter a valid whole number")
            Return
        End If

        If value < 1 Then
            ShowInputError(TxtDrawerCount, "Number of drawers must be at least 1")
        ElseIf value > 20 Then
            ShowInputError(TxtDrawerCount, "Number of drawers cannot exceed 20")
        Else
            ClearInputError(TxtDrawerCount)
        End If
    End Sub

    Private Sub ValidateFirstDrawerHeight()
        If TxtFirstDrawerHeight Is Nothing Then Return

        Dim value As Double
        If Not Double.TryParse(TxtFirstDrawerHeight.Text, value) Then
            ShowInputError(TxtFirstDrawerHeight, "Please enter a valid decimal number")
            Return
        End If

        If value <= 0 Then
            ShowInputError(TxtFirstDrawerHeight, "First drawer height must be greater than 0")
        Else
            ClearInputError(TxtFirstDrawerHeight)
        End If
    End Sub

    Private Sub ShowInputError(control As TextBox, message As String)
        control.BackColor = Color.LightPink
        ' Use ToolTip for immediate feedback
        Dim tooltip As New ToolTip()
        tooltip.SetToolTip(control, message)
        tooltip.Show(message, control, 0, control.Height, 3000)
    End Sub

    Private Sub ClearInputError(control As TextBox)
        control.BackColor = SystemColors.Window
    End Sub

    ' Add real-time validation setup method
    Private Sub SetupRealTimeValidation()
        ' Add KeyPress handlers for numeric-only inputs
        If TxtDrawerCount IsNot Nothing Then
            AddHandler TxtDrawerCount.KeyPress, AddressOf NumericOnly_KeyPress
            AddHandler TxtDrawerCount.TextChanged, AddressOf ValidateInputOnChange
        End If

        If TxtDrawerSpacing IsNot Nothing Then
            AddHandler TxtDrawerSpacing.KeyPress, AddressOf DecimalOnly_KeyPress
        End If

        If TxtDrawerWidth IsNot Nothing Then
            AddHandler TxtDrawerWidth.KeyPress, AddressOf DecimalOnly_KeyPress
        End If

        If TxtFirstDrawerHeight IsNot Nothing Then
            AddHandler TxtFirstDrawerHeight.KeyPress, AddressOf DecimalOnly_KeyPress
            AddHandler TxtFirstDrawerHeight.TextChanged, AddressOf ValidateInputOnChange
        End If
    End Sub

    Private Sub NumericOnly_KeyPress(sender As Object, e As KeyPressEventArgs)
        ' Allow digits, backspace, and delete
        If Not Char.IsDigit(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
            SystemSounds.Beep.Play()
        End If
    End Sub

    Private Sub DecimalOnly_KeyPress(sender As Object, e As KeyPressEventArgs)
        Dim txt As TextBox = DirectCast(sender, TextBox)

        ' Allow digits, decimal point (only one), backspace, and delete
        If Char.IsDigit(e.KeyChar) OrElse Char.IsControl(e.KeyChar) Then
            ' Always allow digits and control characters
        ElseIf e.KeyChar = "."c AndAlso Not txt.Text.Contains("."c) Then
            ' Allow decimal point if none exists
        Else
            e.Handled = True
            SystemSounds.Beep.Play()
        End If
    End Sub

    Private Sub BtnDrawDrawerImage_Click(sender As Object, e As EventArgs) Handles BtnDrawDrawerImage.Click
        If _lastDrawerResult IsNot Nothing AndAlso _lastDrawerResult.IsValid Then
            SetDrawerDrawingContext()
        Else
            MessageBox.Show("No calculation results available to draw.", "Draw Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

End Class