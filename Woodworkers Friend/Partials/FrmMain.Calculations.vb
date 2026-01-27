' ============================================================================
' Last Updated: {Current Date}
' Changes: Applied high-priority improvements - Removed unnecessary null checks,
'          use UnitConversionConstants, InputValidator, LabelFormatter
'          Enhanced error handling and consistent validation patterns
' ============================================================================

Partial Public Class FrmMain

#Region "Table Tip Force"

    Private Sub TableTipInputs_TextChanged(sender As Object, e As EventArgs) Handles TxtTtTableTopLength.TextChanged, TxtTtTableTopWeight.TextChanged, TxtTtTableBaselength.TextChanged, TxtTtTableBaseWeight.TextChanged
        Dim topLength As Double = InputValidator.TryParseDoubleWithDefault(TxtTtTableTopLength.Text, 0)
        Dim topWeight As Double = InputValidator.TryParseDoubleWithDefault(TxtTtTableTopWeight.Text, 0)
        Dim baseLength As Double = InputValidator.TryParseDoubleWithDefault(TxtTtTableBaselength.Text, 0)
        Dim baseWeight As Double = InputValidator.TryParseDoubleWithDefault(TxtTtTableBaseWeight.Text, 0)

        If topLength > 0 AndAlso topWeight > 0 AndAlso baseLength > 0 AndAlso baseWeight > 0 Then

            ' Calculate the tipping force:
            ' Tipping force = (topWeight * (topLength / 2)) / (baseLength / 2)
            ' This assumes the force is applied at the edge of the table top, and the pivot is at the edge of the base.
            Dim force As Double = (topWeight * (topLength / 2)) / (baseLength / 2)

            ' Calculate both imperial and metric forces
            Dim imperialForce As Double
            Dim metricForce As Double

            ' Check if we're currently in metric mode (inputs are in mm and kg)
            If RbMetric.Checked Then
                ' Convert metric inputs to imperial for calculation, then display both
                Dim topLengthInches As Double = topLength * UnitConversionConstants.MM_TO_INCHES
                Dim baseLengthInches As Double = baseLength * UnitConversionConstants.MM_TO_INCHES
                Dim topWeightLbs As Double = topWeight * UnitConversionConstants.KG_TO_LBS

                ' Calculate imperial force
                imperialForce = (topWeightLbs * (topLengthInches / 2)) / (baseLengthInches / 2)

                ' Use original metric calculation
                metricForce = force ' This is already in kgf since inputs were kg and mm
            Else
                ' Imperial inputs - use direct calculation
                imperialForce = force ' This is in lbs force

                ' Convert to metric for display
                metricForce = force * UnitConversionConstants.LBS_TO_KG
            End If

            ' Display both imperial (lbs) and metric (kgf) forces
            LabelFormatter.UpdateLabelWithFallback(LblTippingForce,
                $"Force: {imperialForce:N2} lbs ({metricForce:N2} kgf)",
                Math.Round(imperialForce, 2),
                Math.Round(metricForce, 2))
        Else
            LblTippingForce.Text = "Invalid input"
        End If
    End Sub

#End Region

#Region "Conversions"

    Private Sub TxtInches2Mm_TextChanged(sender As Object, e As EventArgs) Handles TxtInches2Mm.TextChanged
        Dim inches As Double = InputValidator.TryParseDoubleWithDefault(TxtInches2Mm.Text, 0)
        If inches <> 0 Then
            Dim mm As Double = inches * UnitConversionConstants.INCHES_TO_MM
            LabelFormatter.UpdateLabelWithFallback(LblInches2MM,
                $"Millimeters: {mm:N3} mm",
                Math.Round(mm, 3))
        Else
            LblInches2MM.Text = "Invalid input"
        End If
    End Sub

    Private Sub TxtMm2Inches_TextChanged(sender As Object, e As EventArgs) Handles TxtMm2Inches.TextChanged
        Dim mm As Double = InputValidator.TryParseDoubleWithDefault(TxtMm2Inches.Text, 0)
        If mm <> 0 Then
            Dim inches As Double = mm * UnitConversionConstants.MM_TO_INCHES
            LabelFormatter.UpdateLabelWithFallback(LblMM2Inches,
                $"Inches: {inches:N3} in",
                Math.Round(inches, 3))
        Else
            LblMM2Inches.Text = "Invalid input"
        End If
    End Sub

    Private Sub TxtDecimal2Fraction_TextChanged(sender As Object, e As EventArgs) Handles TxtDecimal2Fraction.TextChanged
        Dim decimalValue As Double = InputValidator.TryParseDoubleWithDefault(TxtDecimal2Fraction.Text, 0)
        If decimalValue <> 0 Then
            ' Convert decimal to fraction with denominator up to 64
            Dim denominator As Integer = 64
            Dim numerator As Integer = CInt(Math.Round(decimalValue * denominator))
            Dim fraction As String = ReduceFraction(numerator, denominator)
            LabelFormatter.UpdateLabelWithFallback(LblDecimal2Fraction,
                $"Fraction: {fraction}",
                fraction)
        Else
            LblDecimal2Fraction.Text = "Invalid input"
        End If
    End Sub

    Private Sub TxtFraction2Decimal_TextChanged(sender As Object, e As EventArgs) Handles TxtFraction2Decimal.TextChanged
        Try
            Dim input As String = TxtFraction2Decimal.Text.Trim()

            If String.IsNullOrWhiteSpace(input) Then
                LblFraction2Decimal.Text = "Invalid input"
                Return
            End If

            Dim parts() As String = input.Split("/"c)
            If parts.Length = 2 Then
                Dim numerator, denominator As Double
                If Double.TryParse(parts(0), numerator) AndAlso Double.TryParse(parts(1), denominator) AndAlso denominator <> 0 Then
                    Dim decimalValue As Double = numerator / denominator
                    LabelFormatter.UpdateLabelWithFallback(LblFraction2Decimal,
                        $"Decimal: {decimalValue:N4}",
                        decimalValue)
                Else
                    LblFraction2Decimal.Text = "Invalid input"
                End If
            Else
                LblFraction2Decimal.Text = "Invalid input"
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "TxtFraction2Decimal_TextChanged")
            LblFraction2Decimal.Text = "Invalid input"
        End Try
    End Sub

#End Region

#Region "Populate Fraction boxes"

    Private Sub TpCalcs_Enter(sender As Object, e As EventArgs) Handles TpCalcs.Enter
        RtbFraction2Decimal.Clear()
        RtbFraction2Mm.Clear()

        RtbFraction2Decimal.Height = 225
        RtbFraction2Mm.Height = 225

        For i As Integer = 1 To 64
            Dim reducedFraction As String = ReduceFraction(i, 64)
            Dim decimalValue As Double = Math.Round(i / 64.0, 4)
            Dim mmValue As Double = Math.Round(decimalValue * UnitConversionConstants.INCHES_TO_MM, 3)

            RtbFraction2Decimal.AppendText($"{reducedFraction} = {decimalValue}" & Environment.NewLine)
            RtbFraction2Mm.AppendText($"{reducedFraction} = {mmValue} mm" & Environment.NewLine)
        Next

        ' Load epoxy cost data
        LoadEpoxyCostData()
    End Sub

    ' Helper function to reduce a fraction
    Private Function ReduceFraction(numerator As Integer, denominator As Integer) As String
        Dim gcdValue As Integer = GCD(numerator, denominator)
        Dim reducedNum As Integer = numerator \ gcdValue
        Dim reducedDen As Integer = denominator \ gcdValue
        Return $"{reducedNum}/{reducedDen}"
    End Function

    ' Greatest Common Divisor
    Private Shared Function GCD(a As Integer, b As Integer) As Integer
        While b <> 0
            Dim temp As Integer = b
            b = a Mod b
            a = temp
        End While
        Return Math.Abs(a)
    End Function

#End Region

End Class
