Imports System.Drawing

''' <summary>
''' Partial class for FrmMain - Clamp Spacing & Biscuit/Domino Spacing Calculators
''' </summary>
Partial Public Class FrmMain

#Region "Clamp & Biscuit Spacing - Initialization"

    ''' <summary>
    ''' Initialize Clamp Spacing and Biscuit/Domino calculators with tooltips
    ''' </summary>
    Private Sub InitializeClampBiscuitCalculator()
        Try
            ' Initialize tooltips
            InitializeClampBiscuitTooltips()

            ' Set default values for Clamp Spacing
            If CboPanelWidthUnit.Items.Count = 0 Then
                CboPanelWidthUnit.Items.AddRange({"in", "mm"})
                CboPanelWidthUnit.SelectedIndex = 0 ' Default to inches
            End If

            If CCboClampWoodType.Items.Count = 0 Then
                CCboClampWoodType.Items.AddRange({"Hardwood", "Softwood"})
                CCboClampWoodType.SelectedIndex = 0 ' Default to Hardwood
            End If

            If CboGlueType.Items.Count = 0 Then
                CboGlueType.Items.AddRange({"PVA", "Polyurethane", "Epoxy"})
                CboGlueType.SelectedIndex = 0 ' Default to PVA
            End If

            ' Set default values for Biscuit/Domino
            If CboJointLengthUnit.Items.Count = 0 Then
                CboJointLengthUnit.Items.AddRange({"in", "mm"})
                CboJointLengthUnit.SelectedIndex = 0 ' Default to inches
            End If

            If CboJoineryType.Items.Count = 0 Then
                CboJoineryType.Items.AddRange({"Biscuit", "Festool Domino"})
                CboJoineryType.SelectedIndex = 0 ' Default to Biscuit
            End If

            If CboBiscuitSize.Items.Count = 0 Then
                CboBiscuitSize.Items.AddRange({"#0", "#10", "#20", "#FF", "#H9",
                                              "4x20mm", "5x30mm", "6x40mm", "8x50mm", "10x50mm"})
                CboBiscuitSize.SelectedIndex = 2 ' Default to #20
            End If

            If CboJointStrength.Items.Count = 0 Then
                CboJointStrength.Items.AddRange({"Light", "Medium", "Heavy"})
                CboJointStrength.SelectedIndex = 1 ' Default to Medium
            End If

            ErrorHandler.LogError(New Exception("Clamp & Biscuit calculator initialized"), "InitializeClampBiscuitCalculator")
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeClampBiscuitCalculator")
        End Try
    End Sub

    ''' <summary>
    ''' Initialize tooltips for Clamp Spacing and Biscuit/Domino input controls
    ''' </summary>
    Private Sub InitializeClampBiscuitTooltips()
        Try
            Dim tooltip As ToolTip = tTip
            If tooltip Is Nothing Then
                tooltip = New ToolTip()
            End If

            ' Clamp Spacing tooltips
            If TxtClampPanelWidth IsNot Nothing Then
                tooltip.SetToolTip(TxtClampPanelWidth,
                    "Enter the width of the panel being glued up." & vbCrLf &
                    "This is the distance across the glue joint." & vbCrLf &
                    "Typical range: 12-60 inches" & vbCrLf &
                    "Example: 24"" for a 2-foot wide tabletop")
            End If

            If TxtClampPanelThickness IsNot Nothing Then
                tooltip.SetToolTip(TxtClampPanelThickness,
                    "Enter the thickness of the boards being glued." & vbCrLf &
                    "Thicker boards allow wider clamp spacing (12x rule)." & vbCrLf &
                    "Common: 0.75"" (3/4), 1.0"" (4/4), 1.5"" (6/4)" & vbCrLf &
                    "Example: 0.75 for standard hardwood stock")
            End If

            If CCboClampWoodType IsNot Nothing Then
                tooltip.SetToolTip(CCboClampWoodType,
                    "Select wood type:" & vbCrLf &
                    "• Hardwood (Oak, Maple, Walnut) - Needs tighter spacing" & vbCrLf &
                    "• Softwood (Pine, Fir, Cedar) - Can space slightly wider" & vbCrLf &
                    "Hardwoods need more pressure for strong bonds")
            End If

            If CboGlueType IsNot Nothing Then
                tooltip.SetToolTip(CboGlueType,
                    "Select glue type:" & vbCrLf &
                    "• PVA (White/Yellow Glue) - Standard, 30-45 min clamp time" & vbCrLf &
                    "• Polyurethane - Foams, needs tighter spacing, 2-4 hours" & vbCrLf &
                    "• Epoxy - Gap-filling, less pressure, 4-6 hours" & vbCrLf &
                    "Different glues need different clamping strategies")
            End If

            If CboPanelWidthUnit IsNot Nothing Then
                tooltip.SetToolTip(CboPanelWidthUnit,
                    "Select measurement unit:" & vbCrLf &
                    "• in - Imperial inches (US standard)" & vbCrLf &
                    "• mm - Metric millimeters")
            End If

            ' Biscuit/Domino tooltips
            If TxtBiscuitJointLength IsNot Nothing Then
                tooltip.SetToolTip(TxtBiscuitJointLength,
                    "Enter the total length of the edge joint." & vbCrLf &
                    "This is the length of the boards being joined." & vbCrLf &
                    "Typical range: 12-96 inches (1-8 feet)" & vbCrLf &
                    "Example: 30"" for a 30-inch long tabletop edge")
            End If

            If CboJoineryType IsNot Nothing Then
                tooltip.SetToolTip(CboJoineryType,
                    "Select joinery type:" & vbCrLf &
                    "• Biscuit - Compressed beech wafers, oval shape" & vbCrLf &
                    "• Festool Domino - Rectangular loose tenons" & vbCrLf &
                    "Both provide alignment; strength comes from glue")
            End If

            If CboBiscuitSize IsNot Nothing Then
                tooltip.SetToolTip(CboBiscuitSize,
                    "Select biscuit or domino size:" & vbCrLf &
                    "Biscuits: #0 (1.75""), #10 (2.125""), #20 (2.375"" - most common)" & vbCrLf &
                    "Dominos: 4x20mm through 10x50mm" & vbCrLf &
                    "Larger sizes need thicker stock and wider spacing")
            End If

            If CboJointStrength IsNot Nothing Then
                tooltip.SetToolTip(CboJointStrength,
                    "Select joint strength requirement:" & vbCrLf &
                    "• Light - Face frames, panels (wider spacing)" & vbCrLf &
                    "• Medium - Standard edge joints (normal spacing)" & vbCrLf &
                    "• Heavy - Structural joints (tighter spacing)" & vbCrLf &
                    "Closer spacing = stronger joint")
            End If

            If TxtBiscuitStockThickness IsNot Nothing Then
                tooltip.SetToolTip(TxtBiscuitStockThickness,
                    "Enter the thickness of the boards being joined." & vbCrLf &
                    "Must be thick enough for biscuit/domino:" & vbCrLf &
                    "#0: 1/2"" min, #10: 5/8"" min, #20: 3/4"" min" & vbCrLf &
                    "Too thin = blowout through face!")
            End If

            If CboJointLengthUnit IsNot Nothing Then
                tooltip.SetToolTip(CboJointLengthUnit,
                    "Select measurement unit:" & vbCrLf &
                    "• in - Imperial inches (US standard)" & vbCrLf &
                    "• mm - Metric millimeters")
            End If

        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeClampBiscuitTooltips")
        End Try
    End Sub

#End Region

#Region "Clamp & Biscuit Spacing - Event Handlers"

    ''' <summary>
    ''' Calculate Clamp Spacing button click handler
    ''' </summary>
    Private Sub BtnCalcClampSpacing_Click(sender As Object, e As EventArgs) Handles BtnCalcClampSpacing.Click
        Try
            ' Validate inputs
            If Not ValidateClampInputs() Then Return

            ' Parse inputs
            Dim panelWidth As Double = Double.Parse(TxtClampPanelWidth.Text)
            Dim panelThickness As Double = Double.Parse(TxtClampPanelThickness.Text)
            Dim woodType As String = If(CCboClampWoodType.SelectedItem IsNot Nothing, CCboClampWoodType.SelectedItem.ToString(), "Hardwood")
            Dim glueType As String = If(CboGlueType.SelectedItem IsNot Nothing, CboGlueType.SelectedItem.ToString(), "PVA")
            Dim unit As String = If(CboPanelWidthUnit.SelectedItem IsNot Nothing, CboPanelWidthUnit.SelectedItem.ToString(), "in")

            ' Convert to inches if needed
            If unit = "mm" Then
                panelWidth /= 25.4
                panelThickness /= 25.4
            End If

            ' Calculate clamp spacing
            Dim spacing As Double = CalculateClampSpacing(panelWidth, panelThickness, woodType, glueType)
            Dim numClamps As Integer = CalculateNumberOfClamps(panelWidth, spacing)
            Dim pressure As Double = CalculateClampPressure(woodType, glueType)

            ' Display results using Tag format strings
            ' LblClampSpacingResult.Tag = "Recommended Spacing: {0} {1}" - needs value and unit
            LblClampSpacingResult.Text = String.Format(LblClampSpacingResult.Tag.ToString(), spacing.ToString("F1"), unit)
            LblNumClampsResults.Text = String.Format(LblNumClampsResults.Tag.ToString(), numClamps)
            LblClampPressure.Text = String.Format(LblClampPressure.Tag.ToString(), pressure.ToString("F0"))

            ' Update notes
            UpdateClampNotes(numClamps, spacing, woodType, glueType)
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnCalcClampSpacing_Click")
            MessageBox.Show($"Error calculating clamp spacing: {ex.Message}",
                          "Calculation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Calculate Biscuit/Domino Spacing button click handler
    ''' </summary>
    Private Sub BtnCalcBiscuit_Click(sender As Object, e As EventArgs) Handles BtnCalcBiscuit.Click
        Try
            ' Validate inputs
            If Not ValidateBiscuitInputs() Then Return

            ' Parse inputs
            Dim jointLength As Double = Double.Parse(TxtBiscuitJointLength.Text)
            Dim stockThickness As Double = Double.Parse(TxtBiscuitStockThickness.Text)
            Dim joineryType As String = If(CboJoineryType.SelectedItem IsNot Nothing, CboJoineryType.SelectedItem.ToString(), "Biscuit")
            Dim size As String = If(CboBiscuitSize.SelectedItem IsNot Nothing, CboBiscuitSize.SelectedItem.ToString(), "#20")
            Dim strength As String = If(CboJointStrength.SelectedItem IsNot Nothing, CboJointStrength.SelectedItem.ToString(), "Medium")
            Dim unit As String = If(CboJointLengthUnit.SelectedItem IsNot Nothing, CboJointLengthUnit.SelectedItem.ToString(), "in")

            ' Convert to inches if needed
            If unit = "mm" Then
                jointLength /= 25.4
                stockThickness /= 25.4
            End If

            ' Calculate spacing and positions
            Dim spacing As Double = CalculateBiscuitSpacing(joineryType, size, strength)
            Dim edgeDistance As Double = CalculateEdgeDistance(stockThickness, size)
            Dim positions As List(Of Double) = CalculateBiscuitPositions(jointLength, spacing, edgeDistance)

            ' Display results using Tag format strings
            ' Tags have format: "Label: {0} {1}" where {0}=value, {1}=unit
            LblNumberNeeded.Text = String.Format(LblNumberNeeded.Tag.ToString(), positions.Count)
            LblEdgeDistance.Text = String.Format(LblEdgeDistance.Tag.ToString(), edgeDistance.ToString("F2"), unit)
            LblRecommendedSpacing.Text = String.Format(LblRecommendedSpacing.Tag.ToString(), spacing.ToString("F1"), unit)

            ' Populate center mark positions
            PopulateCenterMarks(positions, unit)
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnCalcBiscuit_Click")
            MessageBox.Show($"Error calculating biscuit spacing: {ex.Message}",
                          "Calculation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Clamp Spacing - Calculation Methods"

    ''' <summary>
    ''' Calculate optimal clamp spacing based on panel properties
    ''' </summary>
    Private Function CalculateClampSpacing(panelWidth As Double, panelThickness As Double,
                                          woodType As String, glueType As String) As Double
        ' Base spacing on thickness and wood type
        Dim baseSpacing As Double = panelThickness * 12 ' Rule of thumb: 12x thickness

        ' Adjust for wood type
        Dim woodFactor As Double = If(woodType.Contains("Hardwood"), 1.0, 1.2) ' Softwoods can space slightly wider

        ' Adjust for glue type
        Dim glueFactor As Double
        Select Case glueType
            Case "PVA"
                glueFactor = 1.0
            Case "Polyurethane"
                glueFactor = 0.9 ' Needs more pressure
            Case "Epoxy"
                glueFactor = 1.1 ' Can space slightly wider
            Case Else
                glueFactor = 1.0
        End Select

        Dim spacing As Double = baseSpacing * woodFactor * glueFactor

        ' Clamp industry standards: 8-12" typical, max 15"
        spacing = Math.Max(8, Math.Min(spacing, 15))

        Return spacing
    End Function

    ''' <summary>
    ''' Calculate number of clamps needed
    ''' </summary>
    Private Function CalculateNumberOfClamps(panelWidth As Double, spacing As Double) As Integer
        ' Need clamps at edges plus interior spacing
        Dim numClamps As Integer = CInt(Math.Ceiling(panelWidth / spacing)) + 1

        ' Minimum 2 clamps (one each end)
        numClamps = Math.Max(numClamps, 2)

        Return numClamps
    End Function

    ''' <summary>
    ''' Calculate recommended clamp pressure
    ''' </summary>
    Private Function CalculateClampPressure(woodType As String, glueType As String) As Double
        ' Base pressure in PSI
        Dim basePressure As Double = 100

        ' Adjust for wood type
        If woodType.Contains("Hardwood") Then
            basePressure = 150
        ElseIf woodType.Contains("Softwood") Then
            basePressure = 100
        End If

        ' Adjust for glue type
        Select Case glueType
            Case "PVA"
                basePressure *= 1.0
            Case "Polyurethane"
                basePressure *= 1.2 ' Needs more pressure
            Case "Epoxy"
                basePressure *= 0.8 ' Needs less pressure
        End Select

        Return basePressure
    End Function

    ''' <summary>
    ''' Update clamp notes with tips and recommendations
    ''' </summary>
    Private Sub UpdateClampNotes(numClamps As Integer, spacing As Double, woodType As String, glueType As String)
        Dim notes As New System.Text.StringBuilder()

        notes.AppendLine($"CLAMPING TIPS:")
        notes.AppendLine()
        notes.AppendLine($"• Use {numClamps} clamps spaced {spacing:F1}"" apart")
        notes.AppendLine($"• Alternate clamps top/bottom to prevent bowing")
        notes.AppendLine($"• Use cauls (clamping boards) for flat panels")
        notes.AppendLine($"• Check for square before glue sets")
        notes.AppendLine()

        If glueType = "PVA" Then
            notes.AppendLine($"• PVA: 30-45 min clamp time minimum")
        ElseIf glueType = "Polyurethane" Then
            notes.AppendLine($"• Polyurethane: 2-4 hour clamp time")
            notes.AppendLine($"• Foams - wipe excess immediately!")
        ElseIf glueType = "Epoxy" Then
            notes.AppendLine($"• Epoxy: Check manufacturer specifications")
            notes.AppendLine($"• Typically 4-6 hour clamp time")
        End If

        notes.AppendLine()
        notes.AppendLine($"• Don't over-tighten - squeeze-out should be minimal")
        notes.AppendLine($"• Use wax paper under cauls to prevent sticking")

        TxtClampNotes.Text = notes.ToString()
    End Sub

    ''' <summary>
    ''' Validate clamp spacing inputs
    ''' </summary>
    Private Function ValidateClampInputs() As Boolean
        ' Panel Width
        If String.IsNullOrWhiteSpace(TxtClampPanelWidth.Text) Then
            MessageBox.Show("Please enter panel width.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtClampPanelWidth.Focus()
            Return False
        End If

        Dim panelWidth As Double
        If Not Double.TryParse(TxtClampPanelWidth.Text, panelWidth) OrElse panelWidth <= 0 Then
            MessageBox.Show("Panel width must be a positive number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtClampPanelWidth.Focus()
            Return False
        End If

        ' Panel Thickness
        If String.IsNullOrWhiteSpace(TxtClampPanelThickness.Text) Then
            MessageBox.Show("Please enter panel thickness.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtClampPanelThickness.Focus()
            Return False
        End If

        Dim panelThickness As Double
        If Not Double.TryParse(TxtClampPanelThickness.Text, panelThickness) OrElse panelThickness <= 0 Then
            MessageBox.Show("Panel thickness must be a positive number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtClampPanelThickness.Focus()
            Return False
        End If

        Return True
    End Function

#End Region

#Region "Biscuit/Domino Spacing - Calculation Methods"

    ''' <summary>
    ''' Calculate biscuit/domino spacing based on joinery type and size
    ''' </summary>
    Private Function CalculateBiscuitSpacing(joineryType As String, size As String, strength As String) As Double
        Dim baseSpacing As Double = 6.0 ' Default 6" spacing

        ' Adjust for joinery type and size
        If joineryType.Contains("Biscuit") Then
            Select Case size
                Case "#0"
                    baseSpacing = 4.0
                Case "#10"
                    baseSpacing = 5.0
                Case "#20", "#FF"
                    baseSpacing = 6.0
                Case Else
                    baseSpacing = 6.0
            End Select
        ElseIf joineryType.Contains("Domino") Then
            ' Domino spacing based on size
            If size.Contains("4x20") Then baseSpacing = 4.0
            If size.Contains("5x30") Then baseSpacing = 5.0
            If size.Contains("6x40") Then baseSpacing = 6.0
            If size.Contains("8x50") Then baseSpacing = 8.0
            If size.Contains("10x") Then baseSpacing = 10.0
        End If

        ' Adjust for strength requirement
        Select Case strength
            Case "Light"
                baseSpacing *= 1.25 ' Can space wider
            Case "Medium"
                baseSpacing *= 1.0
            Case "Heavy"
                baseSpacing *= 0.75 ' Need closer spacing
        End Select

        Return baseSpacing
    End Function

    ''' <summary>
    ''' Calculate edge distance based on biscuit/domino length
    ''' Edge distance must be at least half the joinery length to prevent exposure
    ''' Plus additional padding for trimming and error tolerance
    ''' </summary>
    Private Function CalculateEdgeDistance(stockThickness As Double, size As String) As Double
        ' Get biscuit/domino length (in inches)
        Dim joineryLength As Double = 2.375 ' Default #20

        ' Biscuit sizes (length in inches)
        If size.Contains("#0") Then
            joineryLength = 1.75
        ElseIf size.Contains("#10") Then
            joineryLength = 2.125
        ElseIf size.Contains("#20") Then
            joineryLength = 2.375
        ElseIf size.Contains("#FF") OrElse size.Contains("#H9") Then
            joineryLength = 2.75
        ElseIf size.Contains("4x") Then
            joineryLength = 0.79
        ElseIf size.Contains("5x") Then
            joineryLength = 1.18
        ElseIf size.Contains("6x") Then
            joineryLength = 1.57
        ElseIf size.Contains("8x") OrElse size.Contains("10x") Then
            joineryLength = 1.97
        End If

        ' Minimum edge distance is half the joinery length + trimming allowance
        ' Use 1.5" (1.5") padding to allow for trimming and error tolerance
        Dim minEdge As Double = (joineryLength / 2) + 1.5

        Return minEdge
    End Function

    ''' <summary>
    ''' Calculate biscuit/domino center mark positions
    ''' </summary>
    Private Function CalculateBiscuitPositions(jointLength As Double, spacing As Double,
                                              edgeDistance As Double) As List(Of Double)
        Dim positions As New List(Of Double)()

        ' Start at edge distance
        Dim currentPosition As Double = edgeDistance
        Dim endPosition As Double = jointLength - edgeDistance

        ' Add positions at spacing intervals
        While currentPosition <= endPosition
            positions.Add(currentPosition)
            currentPosition += spacing
        End While

        ' Ensure we have at least 2 positions (one at each end)
        If positions.Count = 0 Then
            positions.Add(edgeDistance)
            positions.Add(jointLength - edgeDistance)
        ElseIf positions.Count = 1 Then
            positions.Add(jointLength - edgeDistance)
        End If

        Return positions
    End Function

    ''' <summary>
    ''' Populate the center marks listbox
    ''' </summary>
    Private Sub PopulateCenterMarks(positions As List(Of Double), unit As String)
        LstCenterMarks.Items.Clear()

        For i As Integer = 0 To positions.Count - 1
            Dim position As Double = positions(i)
            Dim displayValue As String = FormatMeasurement(position, unit)
            LstCenterMarks.Items.Add($"{i + 1}.  {displayValue}")
        Next
    End Sub

    ''' <summary>
    ''' Validate biscuit spacing inputs
    ''' </summary>
    Private Function ValidateBiscuitInputs() As Boolean
        ' Joint Length
        If String.IsNullOrWhiteSpace(TxtBiscuitJointLength.Text) Then
            MessageBox.Show("Please enter joint length.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtBiscuitJointLength.Focus()
            Return False
        End If

        Dim jointLength As Double
        If Not Double.TryParse(TxtBiscuitJointLength.Text, jointLength) OrElse jointLength <= 0 Then
            MessageBox.Show("Joint length must be a positive number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtBiscuitJointLength.Focus()
            Return False
        End If

        ' Stock Thickness
        If String.IsNullOrWhiteSpace(TxtBiscuitStockThickness.Text) Then
            MessageBox.Show("Please enter stock thickness.", "Input Required", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtBiscuitStockThickness.Focus()
            Return False
        End If

        Dim stockThickness As Double
        If Not Double.TryParse(TxtBiscuitStockThickness.Text, stockThickness) OrElse stockThickness <= 0 Then
            MessageBox.Show("Stock thickness must be a positive number.", "Invalid Input", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtBiscuitStockThickness.Focus()
            Return False
        End If

        Return True
    End Function

#End Region

#Region "Clamp & Biscuit - Helper Methods"

    ''' <summary>
    ''' Format measurement for display with unit
    ''' </summary>
    Private Function FormatMeasurement(value As Double, unit As String) As String
        If unit = "mm" Then
            ' Convert back to mm for display
            Return $"{value * 25.4:F1} mm"
        Else
            ' Display in inches (fractional if less than 1", decimal otherwise)
            If value < 1.0 Then
                ' Convert to fractional inches - use existing ConvertToFraction method
                Return ConvertToFraction(value)
            Else
                Return $"{value:F2}"""
            End If
        End If
    End Function

#End Region

End Class
