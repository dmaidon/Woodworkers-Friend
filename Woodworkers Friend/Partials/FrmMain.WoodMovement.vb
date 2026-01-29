' ============================================================================
' Last Updated: January 27, 2026
' Changes: Initial creation - Wood movement calculator UI with species selector
'          and humidity presets for practical calculations
' ============================================================================

Partial Public Class FrmMain

#Region "Wood Movement Calculator"

    ''' <summary>
    ''' Initializes wood movement calculator
    ''' </summary>
    Private Sub InitializeWoodMovementCalculator()
        Try
            ' Populate species dropdown
            If CmbWoodSpecies IsNot Nothing Then
                CmbWoodSpecies.Items.Clear()
                For Each species In WoodSpeciesDatabase.AllSpecies
                    CmbWoodSpecies.Items.Add(species.Name)
                Next
                If CmbWoodSpecies.Items.Count > 0 Then
                    CmbWoodSpecies.SelectedIndex = 0  ' Default to first species
                End If
            End If

            ' Populate humidity presets
            If CmbHumidityPreset IsNot Nothing Then
                CmbHumidityPreset.Items.Clear()
                For Each preset In WoodMovementCalculator.GetStandardHumidityLevels()
                    CmbHumidityPreset.Items.Add($"{preset.Key} ({preset.Value}%)")
                Next
            End If

            ' Set default values
            If TxtMovementWidth IsNot Nothing Then TxtMovementWidth.Text = "12"
            If TxtInitialHumidity IsNot Nothing Then TxtInitialHumidity.Text = "45"
            If TxtFinalHumidity IsNot Nothing Then TxtFinalHumidity.Text = "25"
            If RbTangential IsNot Nothing Then RbTangential.Checked = True
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeWoodMovementCalculator")
        End Try
    End Sub

    ''' <summary>
    ''' Calculates wood movement
    ''' </summary>
    Private Sub CalculateWoodMovement()
        Try
            ' Get selected species
            If CmbWoodSpecies Is Nothing OrElse CmbWoodSpecies.SelectedItem Is Nothing Then
                Return
            End If

            Dim speciesName = CmbWoodSpecies.SelectedItem.ToString()
            Dim species = WoodSpeciesDatabase.GetSpeciesByName(speciesName)

            If species Is Nothing Then
                MessageBox.Show("Please select a wood species", "Missing Information",
                              MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Get inputs
            Dim width = InputValidator.TryParseDoubleWithDefault(TxtMovementWidth.Text, 12)
            Dim initialHumidity = InputValidator.TryParseDoubleWithDefault(TxtInitialHumidity.Text, 45)
            Dim finalHumidity = InputValidator.TryParseDoubleWithDefault(TxtFinalHumidity.Text, 25)

            ' Validate inputs
            initialHumidity = InputValidator.Clamp(initialHumidity, 0, 100)
            finalHumidity = InputValidator.Clamp(finalHumidity, 0, 100)

            If width <= 0 Then
                MessageBox.Show("Width must be greater than zero", "Invalid Input",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Determine grain orientation
            Dim orientation = WoodMovementCalculator.GrainOrientation.Tangential
            If RbRadial IsNot Nothing AndAlso RbRadial.Checked Then
                orientation = WoodMovementCalculator.GrainOrientation.Radial
            End If

            ' Calculate movement
            Dim movement = WoodMovementCalculator.CalculateMovement(
                species, width, initialHumidity, finalHumidity, orientation)

            ' Calculate panel gaps if needed
            Dim gaps = WoodMovementCalculator.CalculatePanelGap(species, width, initialHumidity, orientation)

            ' Display results
            Dim movementDirection = If(movement > 0, "expansion", "shrinkage")
            Dim movementAbs = Math.Abs(movement)
            Dim movementCategory = WoodMovementCalculator.GetMovementCategory(movement)

            ' Update labels
            LabelFormatter.UpdateLabelWithFallback(LblMovementResult,
                $"Movement: {movementAbs:N4} inches", movementAbs)

            LabelFormatter.UpdateLabelWithFallback(LblMovementDirection,
                $"Direction: {movementDirection} ({movementCategory})", movementDirection)

            LabelFormatter.UpdateLabelWithFallback(LblMovementFraction,
                $"Approximately {ConvertToFraction(movementAbs)}", movementAbs)

            ' Panel gap recommendations
            LabelFormatter.UpdateLabelWithFallback(LblPanelGapMin,
                $"Min Gap (per side): {gaps.MinGap:N3}"" ({ConvertToFraction(gaps.MinGap)})", gaps.MinGap)

            LabelFormatter.UpdateLabelWithFallback(LblPanelGapMax,
                $"Max Gap (per side): {gaps.MaxGap:N3}"" ({ConvertToFraction(gaps.MaxGap)})", gaps.MaxGap)

            ' Display wood properties
            If LblWoodDensity IsNot Nothing Then
                LblWoodDensity.Text = $"Density: {species.Density} lbs/ft³"
            End If

            If LblWoodType IsNot Nothing Then
                LblWoodType.Text = $"Type: {If(species.IsHardwood, "Hardwood", "Softwood")}"
            End If

            ' Color code severity
            If LblMovementResult IsNot Nothing Then
                If movementCategory = "Minimal" OrElse movementCategory = "Low" Then
                    LblMovementResult.ForeColor = Color.DarkGreen
                ElseIf movementCategory = "Moderate" Then
                    LblMovementResult.ForeColor = Color.DarkOrange
                Else
                    LblMovementResult.ForeColor = Color.DarkRed
                End If
            End If
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "CalculateWoodMovement", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Converts decimal inches to nearest fraction
    ''' </summary>
    Private Function ConvertToFraction(inches As Double) As String
        Dim wholePart = CInt(Math.Floor(inches))
        Dim decimalPart = inches - wholePart

        ' Find nearest 64th
        Dim numerator = CInt(Math.Round(decimalPart * 64))
        Dim denominator = 64

        ' Reduce fraction
        Dim divisor As Integer = GCD(numerator, denominator)
        If divisor > 0 Then
            numerator \= divisor
            denominator \= divisor
        End If

        If numerator = 0 Then
            Return If(wholePart > 0, $"{wholePart}""", "0""")
        ElseIf wholePart = 0 Then
            Return $"{numerator}/{denominator}"""
        Else
            Return $"{wholePart} {numerator}/{denominator}"""
        End If
    End Function

    ''' <summary>
    ''' Handles humidity preset selection
    ''' </summary>
    Private Sub CmbHumidityPreset_SelectedIndexChanged(sender As Object, e As EventArgs) ' Handles CmbHumidityPreset.SelectedIndexChanged
        If CmbHumidityPreset Is Nothing OrElse CmbHumidityPreset.SelectedItem Is Nothing Then Return

        Dim presets = WoodMovementCalculator.GetStandardHumidityLevels()
        Dim selectedText = CmbHumidityPreset.SelectedItem.ToString()

        For Each preset In presets
            If selectedText.StartsWith(preset.Key) Then
                TxtFinalHumidity.Text = preset.Value.ToString()
                Exit For
            End If
        Next
    End Sub

    ''' <summary>
    ''' Wire up wood movement events
    ''' </summary>
    Private Sub InitializeWoodMovementEvents()
        If CmbWoodSpecies IsNot Nothing Then
            AddHandler CmbWoodSpecies.SelectedIndexChanged, AddressOf WoodMovementInput_Changed
        End If

        If TxtMovementWidth IsNot Nothing Then
            AddHandler TxtMovementWidth.TextChanged, AddressOf WoodMovementInput_Changed
            AddHandler TxtInitialHumidity.TextChanged, AddressOf WoodMovementInput_Changed
            AddHandler TxtFinalHumidity.TextChanged, AddressOf WoodMovementInput_Changed
        End If

        If RbTangential IsNot Nothing Then
            AddHandler RbTangential.CheckedChanged, AddressOf WoodMovementInput_Changed
            AddHandler RbRadial.CheckedChanged, AddressOf WoodMovementInput_Changed
        End If
    End Sub

    Private Sub WoodMovementInput_Changed(sender As Object, e As EventArgs)
        CalculateWoodMovement()
    End Sub

    Private Sub BtnCalculateMovement_Click(sender As Object, e As EventArgs) Handles BtnCalculateMovement.Click
        CalculateWoodMovement()
    End Sub

#End Region

End Class
