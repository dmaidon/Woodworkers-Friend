Imports System.IO
Imports System.Runtime.Intrinsics

Partial Public Class FrmMain

    ' Store the base ounces for recalculation
    Private EpoxyBaseOunces As Double = 0

#Region "Table Tip Force"

    Private Sub TableTipInputs_TextChanged(sender As Object, e As EventArgs) Handles TxtTtTableTopLength.TextChanged, TxtTtTableTopWeight.TextChanged, TxtTtTableBaselength.TextChanged, TxtTtTableBaseWeight.TextChanged
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
        Dim topLength, topWeight, baseLength, baseWeight As Double

        If Double.TryParse(TxtTtTableTopLength.Text, topLength) AndAlso
           Double.TryParse(TxtTtTableTopWeight.Text, topWeight) AndAlso
           Double.TryParse(TxtTtTableBaselength.Text, baseLength) AndAlso
           Double.TryParse(TxtTtTableBaseWeight.Text, baseWeight) Then

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
                Dim topLengthInches As Double = topLength / 25.4 ' mm to inches
                Dim baseLengthInches As Double = baseLength / 25.4 ' mm to inches
                Dim topWeightLbs As Double = topWeight * 2.20462 ' kg to lbs

                ' Calculate imperial force
                imperialForce = (topWeightLbs * (topLengthInches / 2)) / (baseLengthInches / 2)

                ' Use original metric calculation
                metricForce = force ' This is already in kgf since inputs were kg and mm
            Else
                ' Imperial inputs - use direct calculation
                imperialForce = force ' This is in lbs force

                ' Convert to metric for display
                metricForce = force * 0.453592 ' lbs force to kgf
            End If

            ' Display both imperial (lbs) and metric (kgf) forces
            LblTippingForce.Text = String.Format(CStr(LblTippingForce.Tag),
                                               Math.Round(imperialForce, 2),
                                               Math.Round(metricForce, 2))
        Else
            LblTippingForce.Text = "Invalid input"
        End If
    End Sub

#End Region

#Region "Conversions"

    Private Sub TxtInches2Mm_TextChanged(sender As Object, e As EventArgs) Handles TxtInches2Mm.TextChanged
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
        Dim inches As Double
        If Double.TryParse(TxtInches2Mm.Text, inches) Then
            Dim mm As Double = inches * 25.4
            LblInches2MM.Text = String.Format(CStr(LblInches2MM.Tag), Math.Round(mm, 3))
        Else
            LblInches2MM.Text = "Invalid input"
        End If
    End Sub

    Private Sub TxtMm2Inches_TextChanged(sender As Object, e As EventArgs) Handles TxtMm2Inches.TextChanged
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
        Dim mm As Double
        If Double.TryParse(TxtMm2Inches.Text, mm) Then
            Dim inches As Double = mm / 25.4
            LblMM2Inches.Text = String.Format(CStr(LblMM2Inches.Tag), Math.Round(inches, 3))
        Else
            LblMM2Inches.Text = "Invalid input"
        End If
    End Sub

    Private Sub TxtDecimal2Fraction_TextChanged(sender As Object, e As EventArgs) Handles TxtDecimal2Fraction.TextChanged
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
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
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
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
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
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

    ' Handle epoxy cost selection change
    Private Sub CmbEpoxyCost_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbEpoxyCost.SelectedIndexChanged
        UpdateEpoxyResults()
    End Sub

    ' Load epoxy cost data from CSV file
    Private Sub LoadEpoxyCostData()
        Try
            CmbEpoxyCost.Items.Clear()

            Dim csvPath As String = Path.Combine(Application.StartupPath, "Settings", "epoxyCost.csv")
            If File.Exists(csvPath) Then
                Dim lines() As String = File.ReadAllLines(csvPath)

                For Each line In lines
                    If Not String.IsNullOrWhiteSpace(line) Then
                        Dim parts() As String = line.Split(","c)
                        If parts.Length >= 3 Then
                            ' Create display text: "Brand Type - $XX.XX"
                            Dim brand As String = parts(0).Trim()
                            Dim type As String = parts(1).Trim()
                            Dim costText As String = parts(2).Trim()

                            ' Clean up cost text (remove $ if present)
                            If costText.StartsWith("$"c) Then
                                costText = costText.Substring(1)
                            End If

                            If Double.TryParse(costText, Nothing) Then
                                Dim displayText As String = $"{brand} {type} - ${costText}/gal"
                                CmbEpoxyCost.Items.Add(New EpoxyCostItem(brand, type, Double.Parse(costText), displayText))
                            End If
                        End If
                    End If
                Next

                If CmbEpoxyCost.Items.Count > 0 Then
                    CmbEpoxyCost.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception
            ' Handle file reading errors gracefully
            CmbEpoxyCost.Items.Add("Error loading epoxy costs")
        End Try
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

        ' Imperial calculations only
        Dim gallons = totalOunces / 128.0
        Dim quarts = totalOunces / 32.0
        Dim pints = totalOunces / 16.0

        ' Metric calculation (convert ounces to liters)
        Dim ouncesToLiters As Double = 0.0295735 ' 1 fluid ounce = 0.0295735 liters
        Dim totalLiters = totalOunces * ouncesToLiters

        ' Update labels - imperial only for the first four labels
        LblEpoxyOunces.Text = String.Format(CStr(LblEpoxyOunces.Tag), Math.Round(totalOunces, 2))
        LblEpoxyGallons.Text = String.Format(CStr(LblEpoxyGallons.Tag), Math.Round(gallons, 2))
        LblEpoxyQuarts.Text = String.Format(CStr(LblEpoxyQuarts.Tag), Math.Round(quarts, 2))
        LblEpoxyPints.Text = String.Format(CStr(LblEpoxyPints.Tag), Math.Round(pints, 2))

        ' Update the dedicated liters label
        LblEpoxyLiters.Text = String.Format(CStr(LblEpoxyLiters.Tag), Math.Round(totalLiters, 2))

        ' Calculate and update cost
        UpdateEpoxyCost(gallons)
    End Sub

    Private Sub UpdateEpoxyCost(gallons As Double)
        Try
            If TypeOf CmbEpoxyCost.SelectedItem Is EpoxyCostItem Then
                Dim selectedItem As EpoxyCostItem = CType(CmbEpoxyCost.SelectedItem, EpoxyCostItem)
                Dim totalCost As Double = gallons * selectedItem.CostPerGallon
                LblEpoxyCost.Text = String.Format(CStr(LblEpoxyCost.Tag), totalCost.ToString("C2"))
            Else
                LblEpoxyCost.Text = String.Format(CStr(LblEpoxyCost.Tag), "$0.00")
            End If
        Catch ex As Exception
            LblEpoxyCost.Text = String.Format(CStr(LblEpoxyCost.Tag), "$0.00")
        End Try
    End Sub

#End Region

End Class

' Helper class to store epoxy cost data
Public Class EpoxyCostItem
    Public Property Brand As String
    Public Property Type As String
    Public Property CostPerGallon As Double
    Public Property DisplayText As String

    Public Sub New(brand As String, type As String, costPerGallon As Double, displayText As String)
        ArgumentException.ThrowIfNullOrEmpty(brand)
        ArgumentException.ThrowIfNullOrEmpty(type)
        ArgumentException.ThrowIfNullOrEmpty(displayText)
        Me.Brand = brand
        Me.Type = type
        Me.CostPerGallon = costPerGallon
        Me.DisplayText = displayText
    End Sub

    Public Overrides Function ToString() As String
        Return DisplayText
    End Function

End Class