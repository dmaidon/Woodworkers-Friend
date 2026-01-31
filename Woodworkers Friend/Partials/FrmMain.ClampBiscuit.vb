Imports System.Drawing

Partial Public Class FrmMain

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
    ''' Calculate edge distance based on stock thickness and joinery size
    ''' </summary>
    Private Function CalculateEdgeDistance(stockThickness As Double, size As String) As Double
        ' Minimum edge distance is typically 2x the biscuit/domino height
        Dim minEdge As Double = 2.0 ' Default 2"

        ' Adjust based on size
        If size.Contains("#0") Then minEdge = 1.5
        If size.Contains("#10") Then minEdge = 1.75
        If size.Contains("#20") OrElse size.Contains("4x") OrElse size.Contains("5x") Then minEdge = 2.0
        If size.Contains("6x") OrElse size.Contains("8x") Then minEdge = 2.5
        If size.Contains("10x") Then minEdge = 3.0

        ' Ensure edge distance is reasonable for stock thickness
        minEdge = Math.Min(minEdge, stockThickness / 3)

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
