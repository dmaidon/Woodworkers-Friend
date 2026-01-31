Imports System.ComponentModel
Imports System.Globalization

Partial Public Class FrmMain

    ''' <summary>
    ''' Initializes safety calculator controls and event handlers
    ''' </summary>
    Private Sub InitializeSafetyCalculator()
        ' Router Bit Speed Calculator defaults
        TxtRouterBitDiameter.Text = "1.0"
        TxtDesiredSurfaceSpeed.Text = "9000"

        ' Blade Height Calculator defaults
        TxtMaterialThickness.Text = "0.75"

        ' Push Stick Requirements defaults
        CbPushStickGuard.Checked = True
        CbPushStickFeatherboard.Checked = False

        ' Dust Collection CFM defaults
        TxtToolPort.Text = "4.0"
        CmbToolType.SelectedIndex = 0
    End Sub

#Region "Router Bit Speed Calculator"

    ''' <summary>
    ''' Calculates safe router bit RPM based on diameter and desired surface speed
    ''' Formula: RPM = (Surface Speed × 12) / (π × Diameter)
    ''' Safe surface speed for wood routing: 9,000-12,000 ft/min
    ''' </summary>
    Private Sub CalculateRouterSpeed(sender As Object, e As EventArgs) Handles BtnCalculateRouterSpeed.Click, TxtRouterBitDiameter.TextChanged, TxtDesiredSurfaceSpeed.TextChanged
        Try
            If Not Double.TryParse(TxtRouterBitDiameter.Text, NumberStyles.Any, CultureInfo.InvariantCulture, 0R) OrElse
               Not Double.TryParse(TxtDesiredSurfaceSpeed.Text, NumberStyles.Any, CultureInfo.InvariantCulture, 0R) Then
                Exit Sub
            End If

            Dim diameter As Double = Convert.ToDouble(TxtRouterBitDiameter.Text, CultureInfo.InvariantCulture)
            Dim surfaceSpeed As Double = Convert.ToDouble(TxtDesiredSurfaceSpeed.Text, CultureInfo.InvariantCulture)

            If diameter <= 0 Then
                LblRouterRPMResult.Text = "Invalid diameter"
                LblRouterRPMResult.ForeColor = Color.Red
                LblRouterSpeedWarning.Text = ""
                Exit Sub
            End If

            ' Calculate RPM: RPM = (Surface Speed × 12) / (π × Diameter)
            Dim calculatedRPM As Double = (surfaceSpeed * 12) / (Math.PI * diameter)

            ' Display result
            LblRouterRPMResult.Text = $"Maximum Safe RPM: {calculatedRPM:N0}"
            LblRouterRPMResult.ForeColor = Color.Green

            ' Add safety warnings
            Dim warnings As New List(Of String)

            If calculatedRPM < 8000 Then
                warnings.Add("⚠ Very low speed - check bit specifications")
            ElseIf calculatedRPM > 24000 Then
                warnings.Add("⚠ DANGER: Speed exceeds safe limits!")
                LblRouterRPMResult.ForeColor = Color.Red
            ElseIf calculatedRPM > 22000 Then
                warnings.Add("⚠ Caution: High speed - use with care")
                LblRouterRPMResult.ForeColor = Color.Orange
            End If

            If diameter > 3.5 Then
                warnings.Add("⚠ Large diameter bits require lower speeds")
                Dim maxRecommendedRPM As Double = (9000 * 12) / (Math.PI * diameter)
                warnings.Add($"   Recommended max: {maxRecommendedRPM:N0} RPM")
            End If

            If surfaceSpeed < 8000 Then
                warnings.Add("ℹ Surface speed is below typical range (9,000-12,000 ft/min)")
            ElseIf surfaceSpeed > 12000 Then
                warnings.Add("ℹ Surface speed exceeds typical range - check bit specs")
            End If

            LblRouterSpeedWarning.Text = String.Join(vbCrLf, warnings)
            LblRouterSpeedWarning.ForeColor = If(calculatedRPM > 22000, Color.Red, Color.DarkOrange)

            ' Calculate rim speed in MPH for reference
            Dim rimSpeedMPH As Double = (calculatedRPM * Math.PI * diameter) / (12 * 5280 / 60)
            LblRouterRimSpeed.Text = $"Rim Speed: {rimSpeedMPH:N1} mph ({surfaceSpeed:N0} ft/min)"
        Catch ex As Exception
            LblRouterRPMResult.Text = "Calculation error"
            LblRouterRPMResult.ForeColor = Color.Red
            LblRouterSpeedWarning.Text = ex.Message
            ErrorHandler.LogError(ex, "CalculateRouterSpeed")
        End Try
    End Sub

#End Region

#Region "Blade Height Recommendations"

    ''' <summary>
    ''' Calculates recommended blade height based on material thickness and operation
    ''' </summary>
    Private Sub CalculateBladeHeight(sender As Object, e As EventArgs) Handles BtnCalculateBladeHeight.Click, TxtMaterialThickness.TextChanged, CmbBladeOperation.SelectedIndexChanged
        Try
            If Not Double.TryParse(TxtMaterialThickness.Text, NumberStyles.Any, CultureInfo.InvariantCulture, 0R) Then
                Exit Sub
            End If

            Dim thickness As Double = Convert.ToDouble(TxtMaterialThickness.Text, CultureInfo.InvariantCulture)

            If thickness <= 0 Then
                LblBladeHeightResult.Text = "Invalid thickness"
                LblBladeHeightResult.ForeColor = Color.Red
                LblBladeHeightNotes.Text = ""
                Exit Sub
            End If

            Dim operation As String = If(CmbBladeOperation.SelectedItem?.ToString(), "Ripping")
            Dim recommendedHeight As Double
            Dim notes As New List(Of String)

            Select Case operation
                Case "Ripping"
                    ' For ripping: 1/8" to 1/4" above material
                    recommendedHeight = thickness + 0.125
                    notes.Add("✓ Blade height: 1/8"" to 1/4"" above material")
                    notes.Add("  • Minimizes kickback risk")
                    notes.Add("  • Reduces blade exposure")
                    notes.Add("  • Adequate tooth engagement")

                Case "Crosscutting"
                    ' For crosscutting: Full tooth above material
                    recommendedHeight = thickness + 0.25
                    notes.Add("✓ Blade height: Full tooth (≈1/4"") above material")
                    notes.Add("  • Cleaner cut On crossgrain")
                    notes.Add("  • Reduces tearout On Exit side")
                    notes.Add("  • Better tooth engagement")

                Case "Dado/Groove"
                    ' For dados: Just breaking through
                    recommendedHeight = thickness + 0.03125 ' 1/32"
                    notes.Add("✓ Blade height: Just breaking through (+1/32"")")
                    notes.Add("  • Precise depth control")
                    notes.Add("  • Test cut On scrap first")
                    notes.Add("  • Multiple passes For deep cuts")

                Case "Thin Stock (< 1/4\"")"
                    ' For thin stock: Lower blade
                    recommendedHeight = thickness + 0.0625 ' 1/16"
                    notes.Add("✓ Blade height: 1/16"" above thin material")
                    notes.Add("  • Use zero-clearance insert")
                    notes.Add("  • Featherboards recommended")
                    notes.Add("  • Push stick mandatory")

                Case Else
                    recommendedHeight = thickness + 0.125
            End Select

            ' Display result
            LblBladeHeightResult.Text = $"Recommended Height: {recommendedHeight:F3} in. ({recommendedHeight * 25.4:F1} mm)"
            LblBladeHeightResult.ForeColor = Color.Green

            ' Add safety warnings based on thickness
            If thickness < 0.25 Then
                notes.Add("")
                notes.Add("⚠ THIN STOCK WARNINGS:")
                notes.Add("  • Use push sticks and push blocks")
                notes.Add("  • Zero-clearance insert required")
                notes.Add("  • Featherboards highly recommended")
                notes.Add("  • Never use fingers near blade")
            ElseIf thickness > 2.0 Then
                notes.Add("")
                notes.Add("⚠ THICK STOCK WARNINGS:")
                notes.Add("  • Check blade capacity")
                notes.Add("  • Multiple passes may be safer")
                notes.Add("  • Use proper support/outfeed")
                notes.Add("  • Reduced blade speed may help")
            End If

            LblBladeHeightNotes.Text = String.Join(vbCrLf, notes)
            LblBladeHeightNotes.ForeColor = Color.Black
        Catch ex As Exception
            LblBladeHeightResult.Text = "Calculation error"
            LblBladeHeightResult.ForeColor = Color.Red
            LblBladeHeightNotes.Text = ex.Message
            ErrorHandler.LogError(ex, "CalculateBladeHeight")
        End Try
    End Sub

#End Region

#Region "Push Stick Requirements"

    ''' <summary>
    ''' Determines when push sticks and safety devices are required
    ''' </summary>
    Private Sub EvaluatePushStickRequirements(sender As Object, e As EventArgs) Handles BtnEvaluatePushStick.Click,
        TxtStockWidth.TextChanged, TxtStockThickness.TextChanged, CbPushStickGuard.CheckedChanged, CbPushStickFeatherboard.CheckedChanged

        Try
            If Not Double.TryParse(TxtStockWidth.Text, NumberStyles.Any, CultureInfo.InvariantCulture, 0R) OrElse
               Not Double.TryParse(TxtStockThickness.Text, NumberStyles.Any, CultureInfo.InvariantCulture, 0R) Then
                Exit Sub
            End If

            Dim width As Double = Convert.ToDouble(TxtStockWidth.Text, CultureInfo.InvariantCulture)
            Dim thickness As Double = Convert.ToDouble(TxtStockThickness.Text, CultureInfo.InvariantCulture)
            Dim guardInPlace As Boolean = CbPushStickGuard.Checked
            Dim featherboardUsed As Boolean = CbPushStickFeatherboard.Checked

            Dim requirements As New List(Of String)
            Dim riskLevel As String = "LOW"
            Dim riskColor As Color = Color.Green

            ' Determine risk level and requirements
            If width < 3.0 OrElse thickness < 0.5 Then
                riskLevel = "CRITICAL"
                riskColor = Color.Red
                requirements.Add("🚨 CRITICAL DANGER ZONE 🚨")
                requirements.Add("")
                requirements.Add("MANDATORY SAFETY DEVICES:")
                requirements.Add("  ✓ Push stick (TWO recommended)")
                requirements.Add("  ✓ Push block for downward pressure")
                requirements.Add("  ✓ Featherboards (front and side)")
                requirements.Add("  ✓ Blade guard MUST be in place")
                requirements.Add("  ✓ Zero-clearance insert")
                requirements.Add("")
                requirements.Add("⛔ NEVER use hands within 6"" of blade")
                requirements.Add("⛔ Consider using a sled or jig instead")

            ElseIf width < 6.0 OrElse thickness < 0.75 Then
                riskLevel = "HIGH"
                riskColor = Color.Orange
                requirements.Add("⚠ HIGH RISK OPERATION")
                requirements.Add("")
                requirements.Add("REQUIRED SAFETY DEVICES:")
                requirements.Add("  ✓ Push stick mandatory")
                requirements.Add("  ✓ Push block recommended")
                requirements.Add("  ✓ Featherboard highly recommended")
                requirements.Add("  ✓ Blade guard in place")
                requirements.Add("")
                requirements.Add("⚠ Keep hands at least 6"" from blade")
                requirements.Add("⚠ Use outfeed support")

            ElseIf width < 12.0 Then
                riskLevel = "MODERATE"
                riskColor = Color.DarkOrange
                requirements.Add("⚠ MODERATE RISK")
                requirements.Add("")
                requirements.Add("RECOMMENDED SAFETY DEVICES:")
                requirements.Add("  ✓ Push stick recommended for last 12""")
                requirements.Add("  ✓ Blade guard should be in place")
                requirements.Add("  • Featherboard helpful for consistency")
                requirements.Add("")
                requirements.Add("ℹ Maintain 6"" clearance from blade")
            Else
                riskLevel = "LOW"
                riskColor = Color.Green
                requirements.Add("✓ LOW RISK (Standard precautions)")
                requirements.Add("")
                requirements.Add("STANDARD SAFETY PRACTICES:")
                requirements.Add("  ✓ Push stick for final pass recommended")
                requirements.Add("  ✓ Blade guard in place")
                requirements.Add("  ✓ Maintain awareness of hand position")
                requirements.Add("  ✓ Use outfeed support for long stock")
            End If

            ' Add additional considerations
            requirements.Add("")
            requirements.Add("GENERAL SAFETY RULES:")
            requirements.Add("  • Never reach over or behind blade")
            requirements.Add("  • Stand to the side, not directly behind stock")
            requirements.Add("  • Remove jewelry and tie back long hair")
            requirements.Add("  • No gloves when operating power tools")
            requirements.Add("  • Eye and hearing protection mandatory")
            requirements.Add("  • Dust collection/mask for health")

            ' Check if safety devices are missing
            If Not guardInPlace Then
                requirements.Add("")
                requirements.Add("⚠ WARNING: Blade guard not in place!")
                requirements.Add("  Risk level increased - guard recommended unless")
                requirements.Add("  operation specifically requires its removal")
                riskLevel &= " (NO GUARD)"
                If riskColor = Color.Green Then riskColor = Color.Orange
            End If

            If Not featherboardUsed AndAlso (width < 6.0 OrElse thickness < 0.75) Then
                requirements.Add("")
                requirements.Add("⚠ RECOMMENDATION: Use featherboard")
                requirements.Add("  Helps maintain consistent pressure and position")
            End If

            ' Display results
            LblPushStickRisk.Text = $"RISK LEVEL: {riskLevel}"
            LblPushStickRisk.ForeColor = riskColor
            LblPushStickRequirements.Text = String.Join(vbCrLf, requirements)
            LblPushStickRequirements.ForeColor = Color.Black
        Catch ex As Exception
            LblPushStickRisk.Text = "Evaluation error"
            LblPushStickRisk.ForeColor = Color.Red
            LblPushStickRequirements.Text = ex.Message
            ErrorHandler.LogError(ex, "EvaluatePushStickRequirements")
        End Try
    End Sub

#End Region

#Region "Dust Collection CFM Calculator"

    ''' <summary>
    ''' Calculates required CFM for dust collection based on tool and port size
    ''' Formula: CFM = Area × FPM (feet per minute)
    ''' Typical FPM: 3500-4000 for wood dust
    ''' </summary>
    Private Sub CalculateDustCollectionCFM(sender As Object, e As EventArgs) Handles BtnCalculateCFM.Click,
        TxtToolPort.TextChanged, CmbToolType.SelectedIndexChanged, TxtDuctLength.TextChanged

        Try
            If Not Double.TryParse(TxtToolPort.Text, NumberStyles.Any, CultureInfo.InvariantCulture, 0R) Then
                Exit Sub
            End If

            Dim portDiameter As Double = Convert.ToDouble(TxtToolPort.Text, CultureInfo.InvariantCulture)
            Dim toolType As String = If(CmbToolType.SelectedItem?.ToString(), "Table Saw")
            Dim ductLength As Double = If(Double.TryParse(TxtDuctLength.Text, 0R), Convert.ToDouble(TxtDuctLength.Text, CultureInfo.InvariantCulture), 10.0)

            If portDiameter <= 0 Then
                LblCFMResult.Text = "Invalid port diameter"
                LblCFMResult.ForeColor = Color.Red
                LblCFMNotes.Text = ""
                Exit Sub
            End If

            ' Calculate port area in square feet
            Dim portArea As Double = Math.PI * Math.Pow(portDiameter / 2, 2) / 144 ' Convert sq in to sq ft

            ' Base FPM (feet per minute) for effective dust capture
            Dim baseFPM As Double = 4000 ' Industry standard for wood dust

            ' Tool-specific CFM recommendations
            Dim toolMultiplier As Double = 1.0
            Dim toolNotes As New List(Of String)

            Select Case toolType
                Case "Table Saw"
                    toolMultiplier = 1.2 ' Need extra for blade guard and below-table
                    toolNotes.Add("TABLE SAW REQUIREMENTS:")
                    toolNotes.Add("  • Minimum: 350 CFM")
                    toolNotes.Add("  • Recommended: 450-650 CFM")
                    toolNotes.Add("  • Collect from below table and blade guard")
                    toolNotes.Add("  • Larger blade = more CFM needed")

                Case "Router Table"
                    toolMultiplier = 1.0
                    toolNotes.Add("ROUTER TABLE REQUIREMENTS:")
                    toolNotes.Add("  • Minimum: 200 CFM")
                    toolNotes.Add("  • Recommended: 300-400 CFM")
                    toolNotes.Add("  • Port near bit produces fine dust")
                    toolNotes.Add("  • Consider overhead dust collection too")

                Case "Miter Saw"
                    toolMultiplier = 1.3 ' Need extra due to throwing action
                    toolNotes.Add("MITER SAW REQUIREMENTS:")
                    toolNotes.Add("  • Minimum: 400 CFM")
                    toolNotes.Add("  • Recommended: 500-700 CFM")
                    toolNotes.Add("  • Dust is thrown with cutting action")
                    toolNotes.Add("  • Consider dust shroud/hood")

                Case "Planer"
                    toolMultiplier = 1.5 ' Produces large volume of chips
                    toolNotes.Add("PLANER REQUIREMENTS:")
                    toolNotes.Add("  • Minimum: 400 CFM (portable)")
                    toolNotes.Add("  • Recommended: 600-1000 CFM (stationary)")
                    toolNotes.Add("  • Produces high volume of chips")
                    toolNotes.Add("  • Larger port = better performance")

                Case "Jointer"
                    toolMultiplier = 1.3
                    toolNotes.Add("JOINTER REQUIREMENTS:")
                    toolNotes.Add("  • Minimum: 350 CFM (6"")")
                    toolNotes.Add("  • Recommended 500-750 CFM (8"")")
                    toolNotes.Add("  • 8"" + jointers need more CFM")
                    toolNotes.Add("  • Chips can clog smaller ports")

                Case "Bandsaw"
                    toolMultiplier = 0.8 ' Lower dust production
                    toolNotes.Add("BANDSAW REQUIREMENTS: ")
                    toolNotes.Add("  • Minimum 200 CFM")
                    toolNotes.Add("  • Recommended 300-450 CFM")
                    toolNotes.Add("  • Dual ports (upper/lower) recommended")
                    toolNotes.Add("  • Fine dust accumulates inside")

                Case "Drum Sander"
                    toolMultiplier = 1.8 ' Very fine dust, high volume
                    toolNotes.Add("DRUM SANDER REQUIREMENTS")
                    toolNotes.Add("  • Minimum 600 CFM")
                    toolNotes.Add("  • Recommended 800-1200 CFM")
                    toolNotes.Add("  • Produces very fine hazardous dust")
                    toolNotes.Add("  • Cyclone pre-separator recommended")

                Case "Thickness Sander"
                    toolMultiplier = 2.0
                    toolNotes.Add("THICKNESS SANDER REQUIREMENTS")
                    toolNotes.Add("  • Minimum 800 CFM")
                    toolNotes.Add("  • Recommended 1000-1500 CFM")
                    toolNotes.Add("  • Extremely high dust production")
                    toolNotes.Add("  • Inadequate CFM = machine damage")
            End Select

            ' Calculate base CFM
            Dim calculatedCFM As Double = portArea * baseFPM * toolMultiplier

            ' Add losses for duct length
            Dim ductLossFactor As Double = 1.0
            If ductLength > 0 Then
                ' Approximate 10% loss per 10 feet of duct, plus fittings
                ductLossFactor = 1.0 + (ductLength / 100)
            End If

            Dim recommendedCFM As Double = calculatedCFM * ductLossFactor

            ' Display results
            LblCFMResult.Text = $"Required CFM {recommendedCFM:N0}"
            LblCFMResult.ForeColor = Color.Green

            ' Build detailed notes
            Dim allNotes As New List(Of String)
            allNotes.AddRange(toolNotes)
            allNotes.Add("")
            allNotes.Add($"CALCULATION DETAILS")
            allNotes.Add($"  • Port diameter {portDiameter}""")
            allNotes.Add($"  • Port area: {portArea * 144:F2} sq in")
            allNotes.Add($"  • Air velocity: {baseFPM:N0} FPM (feet per minute)")
            allNotes.Add($"  • Base CFM: {calculatedCFM:N0}")

            If ductLength > 0 Then
                allNotes.Add($"  • Duct length: {ductLength:F0} feet")
                allNotes.Add($"  • Loss factor: {(ductLossFactor - 1) * 100:F0}%")
                allNotes.Add($"  • Adjusted CFM: {recommendedCFM:N0}")
            End If

            allNotes.Add("")
            allNotes.Add("GENERAL DUST COLLECTION TIPS:")
            allNotes.Add("  • Shorter, straighter runs = better performance")
            allNotes.Add("  • Each 90° elbow = ~5 feet of straight duct loss")
            allNotes.Add("  • Smooth interior ducts perform better")
            allNotes.Add("  • Static pressure matters as much as CFM")
            allNotes.Add("  • Ground metal ducts to prevent static")
            allNotes.Add("  • Blast gates to direct flow to active tool")
            allNotes.Add("  • Fine dust requires 1-micron filtration")
            allNotes.Add("  • Consider cyclone separator to extend filter life")

            ' Add health warnings
            allNotes.Add("")
            allNotes.Add("⚠ HEALTH & SAFETY:")
            allNotes.Add("  • Wood dust is carcinogenic (especially hardwoods)")
            allNotes.Add("  • Inadequate collection = health hazard")
            allNotes.Add("  • Wear respirator even with good collection")
            allNotes.Add("  • Fine dust is most dangerous (< 10 microns)")
            allNotes.Add("  • MDF, plywood dust particularly hazardous")

            LblCFMNotes.Text = String.Join(vbCrLf, allNotes)
            LblCFMNotes.ForeColor = Color.Black
        Catch ex As Exception
            LblCFMResult.Text = "Calculation error"
            LblCFMResult.ForeColor = Color.Red
            LblCFMNotes.Text = ex.Message
            ErrorHandler.LogError(ex, "CalculateDustCollectionCFM")
        End Try
    End Sub

#End Region

End Class
