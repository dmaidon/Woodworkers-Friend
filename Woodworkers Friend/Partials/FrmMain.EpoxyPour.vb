' ============================================================================
' Last Updated: {Current Date}
' Changes: Applied high-priority improvements - Use UnitConversionConstants,
'          LabelFormatter, InputValidator, ErrorHandler, ReentrancyGuardHelper
'          Fixed FormatException by updating label Tag format strings
' ============================================================================

Imports System.IO

Partial Public Class FrmMain

    ' Store the base ounces for recalculation
    Private EpoxyBaseOunces As Double = 0

    ' Reentrancy guard for textbox coercion
    Private _isUpdatingEpoxy As Boolean = False

    Private _isUpdatingEpoxyArea As Boolean = False

#Region "Epoxy Pours"

    Private Sub EpoxyTextBox_Changed(sender As Object, e As EventArgs) Handles TxtEpoxyLength.TextChanged, TxtEpoxyWidth.TextChanged, TxtEpoxyDepth.TextChanged, TxtEpoxyDiameter.TextChanged
        If _isUpdatingEpoxy Then Return

        Dim length, width, depth, diameter As Double
        Dim hasLength As Boolean = Double.TryParse(TxtEpoxyLength.Text, length)
        Dim hasWidth As Boolean = Double.TryParse(TxtEpoxyWidth.Text, width)
        Dim hasDiameter As Boolean = Double.TryParse(TxtEpoxyDiameter.Text, diameter)
        Dim hasDepth As Boolean = Double.TryParse(TxtEpoxyDepth.Text, depth)

        ' Enforce mutual exclusivity using Tag (0=L,1=W,2=D,3=H)
        Dim tb = TryCast(sender, TextBox)
        Dim tagIndex As Integer = -1
        If tb IsNot Nothing AndAlso tb.Tag IsNot Nothing AndAlso Integer.TryParse(tb.Tag.ToString(), tagIndex) Then
            ' Valid tag index
        Else
            tagIndex = -1
        End If

        Try
            _isUpdatingEpoxy = True

            Select Case tagIndex
                Case 2 ' Diameter changed
                    If hasDiameter AndAlso diameter > 0 Then
                        If TxtEpoxyLength.Text <> "0" Then TxtEpoxyLength.Text = "0"
                        If TxtEpoxyWidth.Text <> "0" Then TxtEpoxyWidth.Text = "0"
                        hasLength = False : length = 0
                        hasWidth = False : width = 0
                    End If
                Case 0, 1 ' Length or Width changed
                    If ((hasLength AndAlso length > 0) OrElse (hasWidth AndAlso width > 0)) AndAlso TxtEpoxyDiameter.Text <> "0" Then
                        TxtEpoxyDiameter.Text = "0"
                        hasDiameter = False : diameter = 0
                    End If
            End Select

            ' Calculate and update area if using Length/Width (rectangular mold)
            If hasLength AndAlso hasWidth AndAlso length > 0 AndAlso width > 0 Then
                UpdateEpoxyAreaFromDimensions(length, width)
            ElseIf (Not hasLength OrElse length = 0) AndAlso (Not hasWidth OrElse width = 0) AndAlso Not _isUpdatingEpoxyArea Then
                ' Clear area if both length and width are zero/empty
                _isUpdatingEpoxyArea = True
                TxtEpoxyArea.Text = ""
                _isUpdatingEpoxyArea = False
            End If
        Finally
            _isUpdatingEpoxy = False
        End Try

        ' Fallback to 0 for any non-numeric inputs
        If Not hasLength Then length = 0
        If Not hasWidth Then width = 0
        If Not hasDiameter Then diameter = 0
        If Not hasDepth Then depth = 0

        CalculateEpoxyVolume()
    End Sub

    Private Sub UpdateEpoxyAreaFromDimensions(length As Double, width As Double)
        If Not ReentrancyGuardHelper.TryEnter(_isUpdatingEpoxyArea) Then Return

        Try
            ' Calculate area in square inches, then convert to square feet
            Dim areaSqIn As Double = length * width
            Dim areaSqFt As Double = areaSqIn / UnitConversionConstants.SQ_INCHES_TO_SQ_FEET

            ' Update TxtEpoxyArea with calculated value
            TxtEpoxyArea.Text = areaSqFt.ToString("N2")
        Catch ex As Exception
            ErrorHandler.LogError(ex, "UpdateEpoxyAreaFromDimensions")
        Finally
            ReentrancyGuardHelper.Exit(_isUpdatingEpoxyArea)
        End Try
    End Sub

    Private Sub TxtEpoxyArea_TextChanged(sender As Object, e As EventArgs) Handles TxtEpoxyArea.TextChanged
        ' Allow manual entry in TxtEpoxyArea to trigger calculations
        If Not _isUpdatingEpoxyArea Then
            CalculateEpoxyVolume()
        End If
    End Sub

    Private Sub CalculateEpoxyVolume()
        Dim length As Double = InputValidator.TryParseDoubleWithDefault(TxtEpoxyLength.Text, 0)
        Dim width As Double = InputValidator.TryParseDoubleWithDefault(TxtEpoxyWidth.Text, 0)
        Dim diameter As Double = InputValidator.TryParseDoubleWithDefault(TxtEpoxyDiameter.Text, 0)
        Dim depth As Double = InputValidator.TryParseDoubleWithDefault(TxtEpoxyDepth.Text, 0)
        Dim areaSqFt As Double = InputValidator.TryParseDoubleWithDefault(TxtEpoxyArea.Text, 0)

        ' Compute volume
        Dim cubicInches As Double = 0
        If depth > 0 Then
            If diameter > 0 Then
                ' Circular mold: V = π * r^2 * h
                Dim radius As Double = diameter / 2.0
                cubicInches = Math.PI * radius * radius * depth
            ElseIf areaSqFt > 0 Then
                ' Use area directly (user entered or calculated from L×W)
                Dim areaSqIn As Double = areaSqFt * UnitConversionConstants.SQ_INCHES_TO_SQ_FEET
                cubicInches = areaSqIn * depth
            ElseIf length > 0 AndAlso width > 0 Then
                ' Rectangular mold: V = L * W * H (fallback if area not available)
                cubicInches = length * width * depth
            End If
        End If

        EpoxyBaseOunces = cubicInches * UnitConversionConstants.CUBIC_INCH_TO_FLUID_OUNCES

        UpdateEpoxyResults()
    End Sub

    ' Handles all waste radio button CheckedChanged events
    Private Sub RbEpoxyWaste_CheckedChanged(sender As Object, e As EventArgs) Handles RbEpoxyWaste0.CheckedChanged, RbEpoxyWaste10.CheckedChanged, RbEpoxyWaste15.CheckedChanged, RbEpoxyWaste20.CheckedChanged
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

                            Dim cost As Double
                            If Double.TryParse(costText, cost) Then
                                Dim displayText As String = $"{brand} {type} - ${cost}/gal"
                                CmbEpoxyCost.Items.Add(New EpoxyCostItem(brand, type, cost, displayText))
                            End If
                        End If
                    End If
                Next

                If CmbEpoxyCost.Items.Count > 0 Then
                    CmbEpoxyCost.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "LoadEpoxyCostData", showToUser:=True)
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

        ' Imperial calculations using constants
        Dim gallons = totalOunces / UnitConversionConstants.GALLONS_TO_OUNCES
        Dim quarts = totalOunces / UnitConversionConstants.QUARTS_TO_OUNCES
        Dim pints = totalOunces / UnitConversionConstants.PINTS_TO_OUNCES

        ' Calculate milliliters and liters for each unit
        Dim totalMl = totalOunces * UnitConversionConstants.OUNCES_TO_ML
        Dim gallonsInLiters = gallons * UnitConversionConstants.GALLONS_TO_LITERS
        Dim quartsInLiters = quarts * UnitConversionConstants.QUARTS_TO_LITERS
        Dim pintsInLiters = pints * UnitConversionConstants.PINTS_TO_LITERS

        ' Update labels using LabelFormatter - no milliliters needed on individual labels
        LabelFormatter.UpdateLabelWithFallback(LblEpoxyOunces,
            $"Ounces: {totalOunces:N2} oz",
            Math.Round(totalOunces, 2))

        LabelFormatter.UpdateLabelWithFallback(LblEpoxyGallons,
            $"Gallons: {gallons:N2} gal",
            Math.Round(gallons, 2))

        LabelFormatter.UpdateLabelWithFallback(LblEpoxyQuarts,
            $"Quarts: {quarts:N2} qt",
            Math.Round(quarts, 2))

        LabelFormatter.UpdateLabelWithFallback(LblEpoxyPints,
            $"Pints: {pints:N2} pt",
            Math.Round(pints, 2))

        ' Update the dedicated milliliters label
        LabelFormatter.UpdateLabelWithFallback(LblEpoxyMilliliters,
            $"Milliliters: {totalMl:N0} ml",
            Math.Round(totalMl, 0))

        ' Update the dedicated liters label (keep existing)
        If LblEpoxyLiters IsNot Nothing Then
            Dim totalLiters = totalOunces * UnitConversionConstants.OUNCES_TO_ML / 1000 ' Convert ml to liters
            LabelFormatter.UpdateLabel(LblEpoxyLiters, Math.Round(totalLiters, 2))
        End If

        ' Set tooltips to show liters for gallons, quarts, and pints
        If tTip IsNot Nothing Then
            tTip.SetToolTip(LblEpoxyGallons, $"{gallonsInLiters:N2} liters")
            tTip.SetToolTip(LblEpoxyQuarts, $"{quartsInLiters:N2} liters")
            tTip.SetToolTip(LblEpoxyPints, $"{pintsInLiters:N2} liters")
        End If

        ' Calculate and update cost
        UpdateEpoxyCost(gallons)
    End Sub

    Private Sub UpdateEpoxyCost(gallons As Double)
        Try
            If LblEpoxyCost.Tag Is Nothing Then Return ' Guard against null Tag during initialization

            If TypeOf CmbEpoxyCost.SelectedItem Is EpoxyCostItem Then
                Dim selectedItem As EpoxyCostItem = CType(CmbEpoxyCost.SelectedItem, EpoxyCostItem)
                Dim totalCost As Double = gallons * selectedItem.CostPerGallon
                LabelFormatter.UpdateLabel(LblEpoxyCost, totalCost.ToString("C2"))
            Else
                LabelFormatter.UpdateLabel(LblEpoxyCost, "$0.00")
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "UpdateEpoxyCost")
            If LblEpoxyCost.Tag IsNot Nothing Then
                LabelFormatter.UpdateLabel(LblEpoxyCost, "$0.00")
            End If
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
