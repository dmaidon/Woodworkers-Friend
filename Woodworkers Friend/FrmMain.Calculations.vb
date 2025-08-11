Imports System.Runtime.Intrinsics

Partial Public Class FrmMain

    ' Store the base ounces for recalculation
    Private EpoxyBaseOunces As Double = 0

#Region "Table Tip Force"

    Private Sub TableTipInputs_TextChanged(sender As Object, e As EventArgs) Handles TxtTtTableTopLength.TextChanged, TxtTtTableTopWeight.TextChanged, TxtTtTableBaselength.TextChanged, TxtTtTableBaseWeight.TextChanged
        Dim topLength, topWeight, baseLength, baseWeight As Double

        If Double.TryParse(TxtTtTableTopLength.Text, topLength) AndAlso
           Double.TryParse(TxtTtTableTopWeight.Text, topWeight) AndAlso
           Double.TryParse(TxtTtTableBaselength.Text, baseLength) AndAlso
           Double.TryParse(TxtTtTableBaseWeight.Text, baseWeight) Then

            ' Calculate the tipping force:
            ' Tipping force = (topWeight * (topLength / 2)) / (baseLength / 2)
            ' This assumes the force is applied at the edge of the table top, and the pivot is at the edge of the base.
            Dim force As Double = (topWeight * (topLength / 2)) / (baseLength / 2)

            LblTippingForce.Text = String.Format(CStr(LblTippingForce.Tag), Math.Round(force, 2))
        Else
            LblTippingForce.Text = "Invalid input"
        End If
    End Sub

#End Region

#Region "Conversions"

    Private Sub TxtInches2Mm_TextChanged(sender As Object, e As EventArgs) Handles TxtInches2Mm.TextChanged
        Dim inches As Double
        If Double.TryParse(TxtInches2Mm.Text, inches) Then
            Dim mm As Double = inches * 25.4
            LblInches2MM.Text = String.Format(CStr(LblInches2MM.Tag), Math.Round(mm, 3))
        Else
            LblInches2MM.Text = "Invalid input"
        End If
    End Sub

    Private Sub TxtMm2Inches_TextChanged(sender As Object, e As EventArgs) Handles TxtMm2Inches.TextChanged
        Dim mm As Double
        If Double.TryParse(TxtMm2Inches.Text, mm) Then
            Dim inches As Double = mm / 25.4
            LblMM2Inches.Text = String.Format(CStr(LblMM2Inches.Tag), Math.Round(inches, 3))
        Else
            LblMM2Inches.Text = "Invalid input"
        End If
    End Sub

    Private Sub TxtDecimal2Fraction_TextChanged(sender As Object, e As EventArgs) Handles TxtDecimal2Fraction.TextChanged
        Dim decimalValue As Double
        If Double.TryParse(TxtDecimal2Fraction.Text, decimalValue) Then
            ' Convert decimal to fraction with denominator up to 64
            Dim denominator As Integer = 64
            Dim numerator As Integer = CInt(Math.Round(decimalValue * denominator))
            Dim fraction As String = ReduceFraction(numerator, denominator)
            LblDecimal2Fraction.Text = String.Format(CStr(LblDecimal2Fraction.Tag), fraction)
        Else
            LblDecimal2Fraction.Text = "Invalid input"
        End If
    End Sub

    Private Sub TxtFraction2Decimal_TextChanged(sender As Object, e As EventArgs) Handles TxtFraction2Decimal.TextChanged
        Dim input As String = TxtFraction2Decimal.Text.Trim()
        Dim parts() As String = input.Split("/"c)
        If parts.Length = 2 Then
            Dim numerator, denominator As Double
            If Double.TryParse(parts(0), numerator) AndAlso Double.TryParse(parts(1), denominator) AndAlso denominator <> 0 Then
                Dim decimalValue As Double = numerator / denominator
                LblFraction2Decimal.Text = String.Format(CStr(LblFraction2Decimal.Tag), Math.Round(decimalValue, 4))
            Else
                LblFraction2Decimal.Text = "Invalid input"
            End If
        Else
            LblFraction2Decimal.Text = "Invalid input"
        End If
    End Sub

#End Region

#Region "Populate Fraction boxes"

    Private Sub TpCalculations_Enter(sender As Object, e As EventArgs) Handles TpCalculations.Enter
        RtbFraction2Decimal.Clear()
        RtbFraction2Mm.Clear()

        RtbFraction2Decimal.Height = 225
        RtbFraction2Mm.Height = 225

        For i As Integer = 1 To 64
            Dim reducedFraction As String = ReduceFraction(i, 64)
            Dim decimalValue As Double = Math.Round(i / 64.0, 4)
            Dim mmValue As Double = Math.Round(decimalValue * 25.4, 3)

            RtbFraction2Decimal.AppendText($"{reducedFraction} = {decimalValue}" & Environment.NewLine)
            RtbFraction2Mm.AppendText($"{reducedFraction} = {mmValue} mm" & Environment.NewLine)
        Next
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

#Region "Epoxy Pours"

    Private Sub EpoxyTextBox_Changed(sender As Object, e As EventArgs) Handles TxtEpoxyLength.TextChanged, TxtEpoxyWidth.TextChanged, TxtEpoxyDepth.TextChanged
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
        Dim length, width, depth As Double

        If Not Double.TryParse(TxtEpoxyLength.Text, length) Then length = 0
        If Not Double.TryParse(TxtEpoxyWidth.Text, width) Then width = 0
        If Not Double.TryParse(TxtEpoxyDepth.Text, depth) Then depth = 0

        Dim cubicInches = length * width * depth
        EpoxyBaseOunces = cubicInches * 0.554113

        UpdateEpoxyResults()
    End Sub

    ' Handles all waste radio button CheckedChanged events
    Private Sub RbEpoxyWaste_CheckedChanged(sender As Object, e As EventArgs) Handles RbEpoxyWaste0.CheckedChanged, RbEpoxyWaste10.CheckedChanged, RbEpoxyWaste15.CheckedChanged, RbEpoxyWaste20.CheckedChanged
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
        UpdateEpoxyResults()
    End Sub

    Private Sub UpdateEpoxyResults()
        Dim wastePercent As Double = 0
        If RbEpoxyWaste10.Checked Then
            wastePercent = 0.1
        ElseIf RbEpoxyWaste15.Checked Then
            wastePercent = 0.15
        ElseIf RbEpoxyWaste20.Checked Then
            wastePercent = 0.2
        End If

        Dim totalOunces = EpoxyBaseOunces * (1 + wastePercent)

        LblEpoxyOunces.Text = String.Format(CStr(LblEpoxyOunces.Tag), Math.Round(totalOunces, 2))
        Dim gallons = totalOunces / 128.0
        LblEpoxyGallons.Text = String.Format(CStr(LblEpoxyGallons.Tag), Math.Round(gallons, 2))
        Dim quarts = totalOunces / 32.0
        LblEpoxyQuarts.Text = String.Format(CStr(LblEpoxyQuarts.Tag), Math.Round(quarts, 2))
        Dim pints = totalOunces / 16.0
        LblEpoxyPints.Text = String.Format(CStr(LblEpoxyPints.Tag), Math.Round(pints, 2))
    End Sub

#End Region

End Class