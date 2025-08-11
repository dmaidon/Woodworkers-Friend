Imports System.Globalization ' <-- Add this line
Imports System.Media ' <-- Add this line for SystemSounds

Partial Public Class FrmMain

#Region "Drawer Calculator Event Handlers - Simplified"

    ''' <summary>
    ''' Main drawer calculation button - delegates to engine
    ''' </summary>
    Private Sub BtnCalculateDrawers_Click(sender As Object, e As EventArgs) Handles BtnCalculateDrawers.Click
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)

        Try
            SetCalculatingState(True)

            ' Collect parameters using helper method
            Dim parameters As DrawerCalculationParameters = CollectDrawerParameters()

            ' Use extracted calculation engine
            Dim result As DrawerCalculationResult = DrawerCalculationEngine.Calculate(parameters)

            ' Display results using helper method
            DisplayDrawerResults(result)

            SetCalculatingState(False)

            If result.IsValid Then
                UpdateDrawerStatus("Calculation completed successfully", Color.Green)
                EnableExportButtons(True)
            Else
                UpdateDrawerStatus($"Calculation failed: {result.ErrorMessage}", Color.Red)
                ShowErrorMessage("Calculation Error", result.ErrorMessage)
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

    Private Function CollectDrawerParameters() As DrawerCalculationParameters
        Dim method = GetSelectedCalculationMethod()
        Dim multiplier As Double = 0
        Dim arithmeticIncrement As Double = 0
        Dim customRatios As Double() = Nothing ' <-- Fixed variable name

        ' Method-specific parameter collection
        Select Case method
            Case DrawerCalculationMethod.Geometric
                multiplier = ValidationManager.GetDoubleFromControl(TxtMultiplier, "Multiplier")

            Case DrawerCalculationMethod.Arithmetic, DrawerCalculationMethod.ReverseArithmetic
                arithmeticIncrement = ValidationManager.GetDoubleFromControl(TxtArithmeticIncrement, "Arithmetic Increment")

            Case DrawerCalculationMethod.Exponential
                multiplier = ValidationManager.GetDoubleFromControl(TxtMultiplier, "Exponential Base")

            Case DrawerCalculationMethod.CustomRatio
                ' Parse custom ratios from multiline textbox
                customRatios = ParseCustomRatios()

            Case DrawerCalculationMethod.Logarithmic, DrawerCalculationMethod.Uniform,
                 DrawerCalculationMethod.Fibonacci, DrawerCalculationMethod.Hambridge,
                 DrawerCalculationMethod.GoldenRatio
                ' These methods don't need additional parameters
        End Select

        Return New DrawerCalculationParameters() With {
            .CalculationMethod = method,
            .DrawerCount = ValidationManager.GetIntegerFromControl(TxtDrawerCount, "Number of Drawers"),
            .DrawerSpacing = ValidationManager.GetDoubleFromControl(TxtDrawerSpacing, "Drawer Spacing"),
            .DrawerWidth = ValidationManager.GetDoubleFromControl(TxtDrawerWidth, "Drawer Width"),
            .FirstDrawerHeight = GetFirstDrawerHeightSafely(method),
            .Multiplier = multiplier,
            .ArithmeticIncrement = arithmeticIncrement,
            .CustomRatios = customRatios, ' <-- Fixed property name
            .Scale = If(RbImperial IsNot Nothing AndAlso RbImperial.Checked, MeasurementScale.Imperial, MeasurementScale.Metric)
        }
    End Function

    ' Add this new helper method for parsing custom ratios:
    Private Function ParseCustomRatios() As Double()
        If TxtCustomRatioInput Is Nothing OrElse String.IsNullOrWhiteSpace(TxtCustomRatioInput.Text) Then
            Return Nothing
        End If

        Try
            Dim ratioList As New List(Of Double)()
            Dim lines As String() = TxtCustomRatioInput.Lines
            Dim invalidValues As New List(Of String)()

            For Each line In lines
                If Not String.IsNullOrWhiteSpace(line) Then
                    ' Support multiple formats: comma-separated, space-separated, or one per line
                    Dim values As String() = line.Split({","c, " "c, vbTab(0)}, StringSplitOptions.RemoveEmptyEntries)

                    For Each value In values
                        Dim cleanValue As String = value.Trim()
                        Dim ratio As Double
                        If Double.TryParse(cleanValue, ratio) Then
                            If ratio > 0 Then
                                ratioList.Add(ratio)
                            Else
                                invalidValues.Add($"'{cleanValue}' (must be positive)")
                            End If
                        Else
                            invalidValues.Add($"'{cleanValue}' (not a valid number)")
                        End If
                    Next
                End If
            Next

            If invalidValues.Count > 0 Then
                Throw New InvalidDrawerParametersException($"Invalid custom ratio values: {String.Join(", ", invalidValues)}")
            End If

            If ratioList.Count = 0 Then
                Throw New InvalidDrawerParametersException("Please enter valid custom ratios (positive numbers only)")
            End If

            Return ratioList.ToArray()
        Catch ex As InvalidDrawerParametersException
            Throw ' Re-throw our custom exceptions
        Catch ex As Exception
            Throw New InvalidDrawerParametersException($"Error parsing custom ratios: {ex.Message}")
        End Try
    End Function

    ' Add this helper method:
    Private Function GetFirstDrawerHeightSafely(method As DrawerCalculationMethod) As Double
        ' Some methods don't require FirstDrawerHeight
        If method = DrawerCalculationMethod.Uniform Then
            Return 0 ' Not used for uniform
        End If

        ' Methods that absolutely require FirstDrawerHeight
        Dim requiresHeight As Boolean = method = DrawerCalculationMethod.Geometric OrElse
                                       method = DrawerCalculationMethod.Arithmetic OrElse
                                       method = DrawerCalculationMethod.ReverseArithmetic OrElse
                                       method = DrawerCalculationMethod.Exponential

        Try
            Dim height As Double = ValidationManager.GetDoubleFromControl(TxtFirstDrawerHeight, "First Drawer Height")

            ' Additional validation for methods that require positive height
            If requiresHeight AndAlso height <= 0 Then
                Throw New InvalidDrawerParametersException($"{method} method requires a positive first drawer height")
            End If

            Return height
        Catch ex As InvalidDrawerParametersException
            Throw ' Re-throw validation exceptions
        Catch
            ' Return a reasonable default only for methods that don't strictly require it
            If requiresHeight Then
                Throw New InvalidDrawerParametersException("First drawer height is required for this calculation method")
            End If
            Return If(RbImperial IsNot Nothing AndAlso RbImperial.Checked, 3.0, 76.2) ' 3" or 76.2mm
        End Try
    End Function

    Private Function GetSelectedCalculationMethod() As DrawerCalculationMethod
        ' Find the checked radio button among the four options
        Dim selectedRb As RadioButton = Nothing
        For Each rb In {RbHambridge, RbGeometric, RbFibonacci, RbArithmetic, RbLogarithmic, RbExponential, RbCustomRatio, RbUniform, RbReverseArithmetic, RbGoldenRatio}
            If rb IsNot Nothing AndAlso rb.Checked Then
                selectedRb = rb
                Exit For
            End If
        Next

        If selectedRb Is Nothing OrElse selectedRb.Tag Is Nothing Then
            Throw New InvalidDrawerParametersException("Please select a calculation method")
        End If

        Select Case CInt(selectedRb.Tag)
            Case 0 : Return DrawerCalculationMethod.Hambridge
            Case 1 : Return DrawerCalculationMethod.Geometric
            Case 2 : Return DrawerCalculationMethod.Fibonacci
            Case 3 : Return DrawerCalculationMethod.Arithmetic
            Case 4 : Return DrawerCalculationMethod.Logarithmic
            Case 5 : Return DrawerCalculationMethod.Exponential
            Case 6 : Return DrawerCalculationMethod.CustomRatio
            Case 7 : Return DrawerCalculationMethod.Uniform
            Case 8 : Return DrawerCalculationMethod.ReverseArithmetic
            Case 9 : Return DrawerCalculationMethod.GoldenRatio
            Case Else
                Throw New InvalidDrawerParametersException("Unknown calculation method")
        End Select
    End Function

    ''' <summary>
    ''' Calculation method change handler - delegates to EventCoordinator
    ''' </summary>
    Private Sub CalculationMethod_Changed(sender As Object, e As EventArgs) Handles RbHambridge.CheckedChanged, RbGeometric.CheckedChanged, RbFibonacci.CheckedChanged, RbArithmetic.CheckedChanged, RbReverseArithmetic.CheckedChanged, RbLogarithmic.CheckedChanged, RbExponential.CheckedChanged, RbCustomRatio.CheckedChanged, RbUniform.CheckedChanged, RbGoldenRatio.CheckedChanged
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
        _eventCoordinator?.HandleDrawerCalculationMethodChange(sender, e)
    End Sub

    ''' <summary>
    ''' General calculator control change handler - delegates to EventCoordinator
    ''' </summary>
    Private Sub CalculatorControl_Changed(sender As Object, e As EventArgs)
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
        _eventCoordinator?.HandleCalculatorControlChanged(sender, e)
    End Sub

    Private Sub ProjectName_Changed(sender As Object, e As EventArgs) Handles TxtProjectName.TextChanged
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
        UpdateSaveButtonState()
    End Sub

    Private Sub DisplayDrawerResults(result As DrawerCalculationResult)
        ArgumentNullException.ThrowIfNull(result)

        If Not result.IsValid Then
            ClearResults()
            Return
        End If

        Try

            ' Update text controls using utility method
            ControlUtility.ClearTextControls(RtbResults, LblTotalHeightResults, LbltotalDrawerHeightResults, LblTotalMaterialResults, LblAverageHeightResults, LblHeightRatioResults)

            UpdateTextControl(RtbResults, result.Details)
            UpdateLabelControl(LblTotalHeightResults, String.Format(CStr(LblTotalHeightResults.Tag), $"{result.TotalHeight:N3} {result.Unit}"))
            UpdateLabelControl(LbltotalDrawerHeightResults, String.Format(CStr(LbltotalDrawerHeightResults.Tag), $"{result.TotalDrawerHeight:N3} {result.Unit}"))
            UpdateLabelControl(LblTotalMaterialResults, String.Format(CStr(LblTotalMaterialResults.Tag), $"{result.TotalMaterialArea:N3} {result.AreaUnit}"))
            UpdateLabelControl(LblAverageHeightResults, String.Format(CStr(LblAverageHeightResults.Tag), $"{result.AverageDrawerHeight:N3} {result.Unit}"))
            ' Defensive: Only call String.Format if LblHeightRatioResults.Tag is not null or empty
            If LblHeightRatioResults.Tag IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(CStr(LblHeightRatioResults.Tag)) Then
                UpdateLabelControl(LblHeightRatioResults, String.Format(CStr(LblHeightRatioResults.Tag), $"{result.HeightRatio:N2}:1"))
            Else
                UpdateLabelControl(LblHeightRatioResults, $"Height Ratio: {result.HeightRatio:N2}:1")
            End If

            ' Update grid
            PopulateDrawerHeightsGrid(result)
        Catch ex As Exception
            RtbLog.AppendText(Format(Now, "hh:mm:ss") & " - Error displaying drawer results: " & ex.Message & vbCrLf)
        End Try
    End Sub

    ''' <summary>
    ''' Populate the drawer heights grid with results
    ''' </summary>
    Private Sub PopulateDrawerHeightsGrid(result As DrawerCalculationResult)
        ArgumentNullException.ThrowIfNull(result)

        DgvDrawerHeights.Rows.Clear()

        For i As Integer = 0 To result.DrawerHeights.Length - 1
            Dim rowIndex As Integer = DgvDrawerHeights.Rows.Add()
            Dim row As DataGridViewRow = DgvDrawerHeights.Rows(rowIndex)

            row.Cells("DrawerNumber").Value = (i + 1).ToString()
            row.Cells("DwHeight").Value = $"{result.DrawerHeights(i):N3}"
            row.Cells("Unit").Value = result.Unit

            ' Guard against division by zero
            Dim percentage As Double = If(result.TotalDrawerHeight > 0,
                                         result.DrawerHeights(i) / result.TotalDrawerHeight * 100,
                                         0)
            row.Cells("Percentage").Value = $"{percentage:N1}%"
        Next
    End Sub

    Private Sub SetCalculatingState(calculating As Boolean)
        Cursor = If(calculating, Cursors.WaitCursor, Cursors.Default)
        _eventCoordinator?.SetControlsEnabled(Not calculating)
        If calculating Then UpdateDrawerStatus("Calculating...", Color.Blue)
        Application.DoEvents()
    End Sub

    Private Sub UpdateSaveButtonState()
        If BtnSaveProject IsNot Nothing Then
            BtnSaveProject.Enabled = Not String.IsNullOrWhiteSpace(TxtProjectName?.Text) AndAlso
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

End Class