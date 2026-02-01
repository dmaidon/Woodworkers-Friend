' ============================================================================
' Last Updated: February 1, 2026
' Changes: Initial implementation of Materials & Finishing Calculators
'          - Veneer & Inlay Calculator
'          - Finishing Materials Calculator
'          - Glue Coverage Calculator
'          All share common area input for efficiency
' ============================================================================

Partial Public Class FrmMain

#Region "Materials & Finishing Calculators"

#Region "Constants"

    ' Coverage rates (sq ft per unit)
    Private ReadOnly FINISH_COVERAGE As New Dictionary(Of String, Double) From {
        {"Stain", 200},           ' sq ft per gallon
        {"Polyurethane", 125},    ' sq ft per quart
        {"Lacquer", 150},         ' sq ft per quart
        {"Danish Oil", 200},      ' sq ft per quart
        {"Tung Oil", 150},        ' sq ft per quart
        {"Wax", 300},             ' sq ft per pound
        {"Shellac", 175},         ' sq ft per quart
        {"Varnish", 100}          ' sq ft per quart
    }

    ' Drying times in hours
    Private ReadOnly FINISH_DRY_TIME As New Dictionary(Of String, Double) From {
        {"Stain", 4},
        {"Polyurethane", 6},
        {"Lacquer", 0.5},
        {"Danish Oil", 8},
        {"Tung Oil", 24},
        {"Wax", 0.25},
        {"Shellac", 2},
        {"Varnish", 8}
    }

    ' Approximate cost per quart
    Private ReadOnly FINISH_COST As New Dictionary(Of String, Double) From {
        {"Stain", 12},
        {"Polyurethane", 16},
        {"Lacquer", 20},
        {"Danish Oil", 14},
        {"Tung Oil", 18},
        {"Wax", 10},
        {"Shellac", 15},
        {"Varnish", 18}
    }

    ' Glue coverage rates (sq in per oz)
    Private ReadOnly GLUE_COVERAGE As New Dictionary(Of String, Double) From {
        {"PVA (White)", 6},
        {"Yellow (Titebond)", 6},
        {"Titebond III", 6},
        {"Polyurethane", 4},
        {"Epoxy", 5},
        {"Hide Glue", 7},
        {"CA (Super Glue)", 8}
    }

    ' Glue open time in minutes
    Private ReadOnly GLUE_OPEN_TIME As New Dictionary(Of String, Integer) From {
        {"PVA (White)", 10},
        {"Yellow (Titebond)", 8},
        {"Titebond III", 10},
        {"Polyurethane", 15},
        {"Epoxy", 30},
        {"Hide Glue", 5},
        {"CA (Super Glue)", 1}
    }

    ' Glue clamp time in hours
    Private ReadOnly GLUE_CLAMP_TIME As New Dictionary(Of String, Double) From {
        {"PVA (White)", 1},
        {"Yellow (Titebond)", 0.75},
        {"Titebond III", 1},
        {"Polyurethane", 4},
        {"Epoxy", 24},
        {"Hide Glue", 2},
        {"CA (Super Glue)", 0.017}
    }

    ' Joint type multipliers (for glue consumption)
    Private ReadOnly JOINT_MULTIPLIER As New Dictionary(Of String, Double) From {
        {"Edge-to-Edge", 1.0},
        {"Face-to-Face", 1.0},
        {"End Grain", 2.0},
        {"Mortise & Tenon", 1.5},
        {"Dovetail", 1.3},
        {"Biscuit/Domino", 0.8}
    }

    ' Veneer waste factors by pattern
    Private ReadOnly VENEER_WASTE As New Dictionary(Of String, Double) From {
        {"Book Match", 20},
        {"Slip Match", 15},
        {"Random", 10},
        {"Radial", 25},
        {"Diamond", 30}
    }

#End Region

#Region "Module Variables"

    Private _materialsFinishingInitialized As Boolean = False
    Private _suppressMaterialsCalculation As Boolean = False
    Private _currentSharedAreaSqFt As Double = 0
    Private _currentSharedAreaSqIn As Double = 0

#End Region

#Region "Initialization"

    ''' <summary>
    ''' Initializes the Materials and Finishing calculators
    ''' </summary>
    Private Sub InitializeMaterialsFinishing()
        If _materialsFinishingInitialized Then Return

        Try
            _suppressMaterialsCalculation = True

            ' Initialize Shared Area controls
            InitializeSharedAreaControls()

            ' Initialize Veneer Calculator
            InitializeVeneerCalculator()

            ' Initialize Finishing Materials Calculator
            InitializeFinishingCalculator()

            ' Initialize Glue Coverage Calculator
            InitializeGlueCalculator()

            ' Set up tooltips
            SetupMaterialsFinishingTooltips()

            _materialsFinishingInitialized = True
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeMaterialsFinishing")
        Finally
            _suppressMaterialsCalculation = False
        End Try
    End Sub

    Private Sub InitializeSharedAreaControls()
        Try
            If CmbSharedAreaUnits IsNot Nothing Then
                CmbSharedAreaUnits.Items.Clear()
                CmbSharedAreaUnits.Items.AddRange({"Inches", "Feet", "Millimeters", "Centimeters"})
                CmbSharedAreaUnits.SelectedIndex = 0
            End If

            If TxtSharedAreaLength IsNot Nothing Then TxtSharedAreaLength.Text = ""
            If TxtSharedAreaWidth IsNot Nothing Then TxtSharedAreaWidth.Text = ""
            If LblSharedArea IsNot Nothing Then LblSharedArea.Text = "Total Area: -- sq ft"
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeSharedAreaControls")
        End Try
    End Sub

    Private Sub InitializeVeneerCalculator()
        Try
            If CmbVeneerPattern IsNot Nothing Then
                CmbVeneerPattern.Items.Clear()
                CmbVeneerPattern.Items.AddRange({"Book Match", "Slip Match", "Random", "Radial", "Diamond"})
                CmbVeneerPattern.SelectedIndex = 0
            End If

            If TxtVeneerSheetLength IsNot Nothing Then TxtVeneerSheetLength.Text = "96"
            If TxtVeneerSheetWidth IsNot Nothing Then TxtVeneerSheetWidth.Text = "48"
            If NudVeneerWaste IsNot Nothing Then NudVeneerWaste.Value = 15

            ClearVeneerResults()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeVeneerCalculator")
        End Try
    End Sub

    Private Sub InitializeFinishingCalculator()
        Try
            If CmbFinishType IsNot Nothing Then
                CmbFinishType.Items.Clear()
                CmbFinishType.Items.AddRange({"Stain", "Polyurethane", "Lacquer", "Danish Oil", "Tung Oil", "Wax", "Shellac", "Varnish"})
                CmbFinishType.SelectedIndex = 1 ' Polyurethane default
            End If

            If TxtFinishCoverage IsNot Nothing Then TxtFinishCoverage.Text = "125"
            If NudFinishCoats IsNot Nothing Then NudFinishCoats.Value = 3
            If NudDryTimeBetween IsNot Nothing Then NudDryTimeBetween.Value = 6
            If ChkSandBetweenCoats IsNot Nothing Then ChkSandBetweenCoats.Checked = True

            ClearFinishingResults()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeFinishingCalculator")
        End Try
    End Sub

    Private Sub InitializeGlueCalculator()
        Try
            If CmbGlueType IsNot Nothing Then
                CmbGlueType.Items.Clear()
                CmbGlueType.Items.AddRange({"PVA (White)", "Yellow (Titebond)", "Titebond III", "Polyurethane", "Epoxy", "Hide Glue", "CA (Super Glue)"})
                CmbGlueType.SelectedIndex = 1 ' Yellow glue default
            End If

            If CmbJointType IsNot Nothing Then
                CmbJointType.Items.Clear()
                CmbJointType.Items.AddRange({"Edge-to-Edge", "Face-to-Face", "End Grain", "Mortise & Tenon", "Dovetail", "Biscuit/Domino"})
                CmbJointType.SelectedIndex = 0
            End If

            If CmbApplicationMethod IsNot Nothing Then
                CmbApplicationMethod.Items.Clear()
                CmbApplicationMethod.Items.AddRange({"Brush", "Roller", "Squeeze Bottle", "Spreader"})
                CmbApplicationMethod.SelectedIndex = 1 ' Roller default
            End If

            If NudGlueWaste IsNot Nothing Then NudGlueWaste.Value = 10

            ClearGlueResults()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeGlueCalculator")
        End Try
    End Sub

    Private Sub SetupMaterialsFinishingTooltips()
        Try
            If tTip Is Nothing Then Return

            ' Shared Area tooltips
            tTip.SetToolTip(TxtSharedAreaLength, "Enter the length of your project surface")
            tTip.SetToolTip(TxtSharedAreaWidth, "Enter the width of your project surface")
            tTip.SetToolTip(CmbSharedAreaUnits, "Select your measurement units")
            tTip.SetToolTip(BtnApplyAreaToAll, "Calculate all three calculators with current area")

            ' Veneer tooltips
            tTip.SetToolTip(CmbVeneerPattern, "Book Match: Mirror image pairs. Slip Match: Repeat pattern. Random: No matching.")
            tTip.SetToolTip(NudVeneerWaste, "Extra material needed for matching and trimming")

            ' Finish tooltips
            tTip.SetToolTip(CmbFinishType, "Select your finish type - coverage rates auto-fill")
            tTip.SetToolTip(TxtFinishCoverage, "Coverage rate in sq ft per quart (auto-fills based on type)")
            tTip.SetToolTip(ChkSandBetweenCoats, "Add sanding time between coats (recommended for best results)")

            ' Glue tooltips
            tTip.SetToolTip(CmbGlueType, "Different glues have different coverage and working times")
            tTip.SetToolTip(CmbJointType, "End grain requires more glue; biscuits require less")
            tTip.SetToolTip(CmbApplicationMethod, "Rollers provide even coverage; brushes work well for joints")
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SetupMaterialsFinishingTooltips")
        End Try
    End Sub

#End Region

#Region "Event Handlers"

    ''' <summary>
    ''' Initialize when Finishing tab becomes visible
    ''' </summary>
    Private Sub TpFinishing_Enter(sender As Object, e As EventArgs) Handles TpFinishing.Enter
        If Not _materialsFinishingInitialized Then
            InitializeMaterialsFinishing()
        End If
    End Sub

    ''' <summary>
    ''' Calculate All button click
    ''' </summary>
    Private Sub BtnApplyAreaToAll_Click(sender As Object, e As EventArgs) Handles BtnApplyAreaToAll.Click
        CalculateSharedArea()
        CalculateAllMaterials()
    End Sub

    ''' <summary>
    ''' Shared area inputs changed
    ''' </summary>
    Private Sub SharedArea_Changed(sender As Object, e As EventArgs) Handles TxtSharedAreaLength.TextChanged, TxtSharedAreaWidth.TextChanged, CmbSharedAreaUnits.SelectedIndexChanged
        If _suppressMaterialsCalculation Then Return
        CalculateSharedArea()
    End Sub

    ''' <summary>
    ''' Veneer inputs changed
    ''' </summary>
    Private Sub Veneer_Changed(sender As Object, e As EventArgs) Handles CmbVeneerPattern.SelectedIndexChanged, TxtVeneerSheetLength.TextChanged, TxtVeneerSheetWidth.TextChanged, NudVeneerWaste.ValueChanged
        If _suppressMaterialsCalculation Then Return
        CalculateVeneer()
    End Sub

    ''' <summary>
    ''' Finish type changed - update coverage rate
    ''' </summary>
    Private Sub CmbFinishType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbFinishType.SelectedIndexChanged
        If _suppressMaterialsCalculation Then Return

        Try
            Dim finishType = CmbFinishType.SelectedItem?.ToString()

            Dim value As Double = Nothing

            If Not String.IsNullOrEmpty(finishType) AndAlso FINISH_COVERAGE.TryGetValue(finishType, value) Then
                TxtFinishCoverage.Text = value.ToString()

                Dim value1 As Double = Nothing

                If NudDryTimeBetween IsNot Nothing AndAlso FINISH_DRY_TIME.TryGetValue(finishType, value1) Then
                    NudDryTimeBetween.Value = CDec(value1)
                End If
            End If
            CalculateFinishing()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "CmbFinishType_SelectedIndexChanged")
        End Try
    End Sub

    ''' <summary>
    ''' Finishing inputs changed
    ''' </summary>
    Private Sub Finishing_Changed(sender As Object, e As EventArgs) Handles TxtFinishCoverage.TextChanged, NudFinishCoats.ValueChanged, NudDryTimeBetween.ValueChanged, ChkSandBetweenCoats.CheckedChanged
        If _suppressMaterialsCalculation Then Return
        CalculateFinishing()
    End Sub

    ''' <summary>
    ''' Glue type changed - update tips
    ''' </summary>
    Private Sub CmbGlueType_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbGlueType.SelectedIndexChanged
        If _suppressMaterialsCalculation Then Return
        CalculateGlue()
    End Sub

    ''' <summary>
    ''' Glue inputs changed
    ''' </summary>
    Private Sub Glue_Changed(sender As Object, e As EventArgs) Handles CmbJointType.SelectedIndexChanged, CmbApplicationMethod.SelectedIndexChanged, NudGlueWaste.ValueChanged
        If _suppressMaterialsCalculation Then Return
        CalculateGlue()
    End Sub

#End Region

#Region "Calculation Logic"

    ''' <summary>
    ''' Calculate shared area from inputs
    ''' </summary>
    Private Sub CalculateSharedArea()
        Try
            Dim length As Double = 0
            Dim width As Double = 0

            If TxtSharedAreaLength Is Nothing OrElse Not Double.TryParse(TxtSharedAreaLength.Text, length) OrElse length <= 0 Then
                LblSharedArea.Text = "Total Area: -- sq ft"
                _currentSharedAreaSqFt = 0
                _currentSharedAreaSqIn = 0
                Return
            End If

            If TxtSharedAreaWidth Is Nothing OrElse Not Double.TryParse(TxtSharedAreaWidth.Text, width) OrElse width <= 0 Then
                LblSharedArea.Text = "Total Area: -- sq ft"
                _currentSharedAreaSqFt = 0
                _currentSharedAreaSqIn = 0
                Return
            End If

            ' Convert to sq inches based on units
            Dim areaSqIn As Double = 0
            Dim units = If(CmbSharedAreaUnits?.SelectedItem IsNot Nothing, CmbSharedAreaUnits.SelectedItem.ToString(), "Inches")

            Select Case units
                Case "Inches"
                    areaSqIn = length * width
                Case "Feet"
                    areaSqIn = length * 12 * width * 12
                Case "Millimeters"
                    areaSqIn = (length / 25.4) * (width / 25.4)
                Case "Centimeters"
                    areaSqIn = (length / 2.54) * (width / 2.54)
            End Select

            _currentSharedAreaSqIn = areaSqIn
            _currentSharedAreaSqFt = areaSqIn / 144

            LblSharedArea.Text = $"Total Area: {_currentSharedAreaSqFt:F2} sq ft ({_currentSharedAreaSqIn:F0} sq in)"
        Catch ex As Exception
            ErrorHandler.LogError(ex, "CalculateSharedArea")
            LblSharedArea.Text = "Total Area: Error"
        End Try
    End Sub

    ''' <summary>
    ''' Calculate all three materials calculators
    ''' </summary>
    Private Sub CalculateAllMaterials()
        CalculateVeneer()
        CalculateFinishing()
        CalculateGlue()
    End Sub

    ''' <summary>
    ''' Calculate veneer requirements
    ''' </summary>
    Private Sub CalculateVeneer()
        Try
            If _currentSharedAreaSqIn <= 0 Then
                ClearVeneerResults()
                Return
            End If

            ' Get sheet dimensions
            Dim sheetLength As Double = 96
            Dim sheetWidth As Double = 48
            If TxtVeneerSheetLength IsNot Nothing Then
                Dim parsed = Double.TryParse(TxtVeneerSheetLength.Text, sheetLength)
            End If
            If TxtVeneerSheetWidth IsNot Nothing Then
                Dim parsed = Double.TryParse(TxtVeneerSheetWidth.Text, sheetWidth)
            End If

            If sheetLength <= 0 OrElse sheetWidth <= 0 Then
                ClearVeneerResults()
                Return
            End If

            ' Get waste factor
            Dim wastePercent As Double = If(NudVeneerWaste IsNot Nothing, CDbl(NudVeneerWaste.Value), 15)
            Dim pattern = If(CmbVeneerPattern?.SelectedItem IsNot Nothing, CmbVeneerPattern.SelectedItem.ToString(), "Random")

            ' If pattern selected, use pattern-specific waste
            Dim patternWaste As Double
            If VENEER_WASTE.TryGetValue(pattern, patternWaste) Then
                wastePercent = patternWaste
                _suppressMaterialsCalculation = True
                NudVeneerWaste.Value = CDec(wastePercent)
                _suppressMaterialsCalculation = False
            End If

            Dim wasteFactor = 1 + (wastePercent / 100)
            Dim areaNeeded = _currentSharedAreaSqIn * wasteFactor

            ' Calculate sheets needed
            Dim sheetArea = sheetLength * sheetWidth
            Dim sheetsNeeded = Math.Ceiling(areaNeeded / sheetArea)
            Dim totalAreaWithWaste = _currentSharedAreaSqFt * wasteFactor

            ' Display results
            LblVeneerSheetsNeeded.Text = $"Sheets Needed: {sheetsNeeded:F0}"
            LblVeneerTotalArea.Text = $"Total Area (with {wastePercent:F0}% waste): {totalAreaWithWaste:F2} sq ft"
        Catch ex As Exception
            ErrorHandler.LogError(ex, "CalculateVeneer")
            ClearVeneerResults()
        End Try
    End Sub

    ''' <summary>
    ''' Calculate finishing material requirements
    ''' </summary>
    Private Sub CalculateFinishing()
        Try
            If _currentSharedAreaSqFt <= 0 Then
                ClearFinishingResults()
                Return
            End If

            ' Get coverage rate
            Dim coverageRate As Double = 125
            If TxtFinishCoverage IsNot Nothing Then
                Dim parsed = Double.TryParse(TxtFinishCoverage.Text, coverageRate)
            End If
            If coverageRate <= 0 Then coverageRate = 125

            ' Get number of coats
            Dim numCoats = If(NudFinishCoats IsNot Nothing, CInt(NudFinishCoats.Value), 1)
            If numCoats <= 0 Then numCoats = 1

            ' Get dry time
            Dim dryTime = If(NudDryTimeBetween IsNot Nothing, CDbl(NudDryTimeBetween.Value), 6)

            ' Calculate quantity needed
            Dim totalAreaToFinish = _currentSharedAreaSqFt * numCoats
            Dim quartsNeeded = totalAreaToFinish / coverageRate
            Dim gallonsNeeded = quartsNeeded / 4

            ' Calculate time
            Dim totalDryTime = dryTime * (numCoats - 1) ' No dry time after last coat
            Dim sandingTime = If(ChkSandBetweenCoats?.Checked, (numCoats - 1) * 0.5, 0) ' 30 min per sand
            Dim applicationTime = numCoats * 0.25 ' 15 min per coat
            Dim totalTime = totalDryTime + sandingTime + applicationTime
            Dim totalDays = totalTime / 24

            ' Estimate cost
            Dim finishType = If(CmbFinishType?.SelectedItem IsNot Nothing, CmbFinishType.SelectedItem.ToString(), "Polyurethane")
            Dim costPerQuart As Double = 15
            FINISH_COST.TryGetValue(finishType, costPerQuart)

            Dim estimatedCost = quartsNeeded * costPerQuart

            ' Display results
            If quartsNeeded < 1 Then
                LblFinishQuantityNeeded.Text = $"Quantity Needed: {quartsNeeded * 32:F1} oz ({quartsNeeded:F2} qt)"
            ElseIf gallonsNeeded >= 1 Then
                LblFinishQuantityNeeded.Text = $"Quantity Needed: {gallonsNeeded:F2} gallons ({quartsNeeded:F1} qt)"
            Else
                LblFinishQuantityNeeded.Text = $"Quantity Needed: {quartsNeeded:F2} quarts"
            End If

            LblFinishTotalTime.Text = $"Total Time: {totalTime:F1} hours ({totalDays:F1} days)"
            LblFinishCostEstimate.Text = $"Est. Cost: ${estimatedCost:F2}"

            ' Tips based on finish type
            Dim tips = GetFinishTips(finishType)
            LblFinishTips.Text = $"Tips: {tips}"
        Catch ex As Exception
            ErrorHandler.LogError(ex, "CalculateFinishing")
            ClearFinishingResults()
        End Try
    End Sub

    ''' <summary>
    ''' Calculate glue requirements
    ''' </summary>
    Private Sub CalculateGlue()
        Try
            If _currentSharedAreaSqIn <= 0 Then
                ClearGlueResults()
                Return
            End If

            ' Get glue type
            Dim glueType = If(CmbGlueType?.SelectedItem IsNot Nothing, CmbGlueType.SelectedItem.ToString(), "Yellow (Titebond)")

            ' Get joint type multiplier
            Dim jointType = If(CmbJointType?.SelectedItem IsNot Nothing, CmbJointType.SelectedItem.ToString(), "Edge-to-Edge")
            Dim jointMultiplier As Double = 1.0
            JOINT_MULTIPLIER.TryGetValue(jointType, jointMultiplier)

            ' Get waste factor
            Dim wastePercent = If(NudGlueWaste IsNot Nothing, CDbl(NudGlueWaste.Value), 10)
            Dim wasteFactor = 1 + (wastePercent / 100)

            ' Calculate glue needed
            Dim coverageRate As Double = 6
            GLUE_COVERAGE.TryGetValue(glueType, coverageRate)
            Dim glueOz = (_currentSharedAreaSqIn / coverageRate) * jointMultiplier * wasteFactor
            Dim glueMl = glueOz * 29.5735

            ' Get timing info
            Dim openTime As Integer = 10
            GLUE_OPEN_TIME.TryGetValue(glueType, openTime)
            Dim clampTime As Double = 1
            GLUE_CLAMP_TIME.TryGetValue(glueType, clampTime)

            ' Display results
            LblGlueAmount.Text = $"Amount Needed: {glueOz:F1} oz ({glueMl:F0} ml)"
            LblGlueOpenTime.Text = $"Open Time: {openTime} minutes"

            If clampTime < 1 Then
                LblGlueClampTime.Text = $"Clamp Time: {clampTime * 60:F0} minutes"
            Else
                LblGlueClampTime.Text = $"Clamp Time: {clampTime:F1} hours"
            End If

            ' Tips based on glue type
            Dim tips = GetGlueTips(glueType, jointType)
            LblGlueTips.Text = $"Tips: {tips}"
        Catch ex As Exception
            ErrorHandler.LogError(ex, "CalculateGlue")
            ClearGlueResults()
        End Try
    End Sub

#End Region

#Region "Clear Results"

    Private Sub ClearVeneerResults()
        If LblVeneerSheetsNeeded IsNot Nothing Then LblVeneerSheetsNeeded.Text = "Sheets Needed: --"
        If LblVeneerTotalArea IsNot Nothing Then LblVeneerTotalArea.Text = "Total Area: -- sq ft"
    End Sub

    Private Sub ClearFinishingResults()
        If LblFinishQuantityNeeded IsNot Nothing Then LblFinishQuantityNeeded.Text = "Quantity Needed: --"
        If LblFinishTotalTime IsNot Nothing Then LblFinishTotalTime.Text = "Total Time: --"
        If LblFinishCostEstimate IsNot Nothing Then LblFinishCostEstimate.Text = "Est. Cost: --"
        If LblFinishTips IsNot Nothing Then LblFinishTips.Text = "Tips: Enter area to calculate"
    End Sub

    Private Sub ClearGlueResults()
        If LblGlueAmount IsNot Nothing Then LblGlueAmount.Text = "Amount Needed: --"
        If LblGlueOpenTime IsNot Nothing Then LblGlueOpenTime.Text = "Open Time: --"
        If LblGlueClampTime IsNot Nothing Then LblGlueClampTime.Text = "Clamp Time: --"
        If LblGlueTips IsNot Nothing Then LblGlueTips.Text = "Tips: Enter area to calculate"
    End Sub

#End Region

#Region "Helper Methods"

    Private Function GetFinishTips(finishType As String) As String
        Select Case finishType
            Case "Stain"
                Return "Wipe off excess. Test on scrap first."
            Case "Polyurethane"
                Return "Thin first coat 10%. Sand with 320 between."
            Case "Lacquer"
                Return "Apply thin coats. Good ventilation required!"
            Case "Danish Oil"
                Return "Wipe on, wait 15 min, wipe off excess."
            Case "Tung Oil"
                Return "Pure tung takes 5-7 coats. Wet sand final."
            Case "Wax"
                Return "Apply thin, buff when dry. Not for high-use."
            Case "Shellac"
                Return "Dewaxed for topcoat compatibility."
            Case "Varnish"
                Return "Apply thin coats. Best for exterior use."
            Case Else
                Return "Follow manufacturer instructions."
        End Select
    End Function

    Private Function GetGlueTips(glueType As String, jointType As String) As String
        Dim tip = ""

        Select Case glueType
            Case "PVA (White)"
                tip = "Interior only. Cleans up with water."
            Case "Yellow (Titebond)"
                tip = "Stronger than wood. Sand squeeze-out."
            Case "Titebond III"
                tip = "Waterproof. Good for cutting boards."
            Case "Polyurethane"
                tip = "Foams! Clamp well. Waterproof."
            Case "Epoxy"
                tip = "Gap-filling. Mix ratio critical."
            Case "Hide Glue"
                tip = "Traditional. Reversible with heat."
            Case "CA (Super Glue)"
                tip = "Fast! Use accelerator for instant bond."
        End Select

        If jointType = "End Grain" Then
            tip &= " Pre-seal end grain."
        End If

        Return tip
    End Function

#End Region

#End Region

End Class
