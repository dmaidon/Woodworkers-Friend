' ============================================================================
' Last Updated: January 27, 2026
' Changes: Applied improvements - Use UnitConversionConstants, ErrorHandler,
'          ReentrancyGuardHelper for cleaner reentrancy prevention
' ============================================================================

Partial Public Class FrmMain

    Private _topCoatGridInitialized As Boolean = False
    Private _isUpdatingTotalArea As Boolean = False
    Private _isUpdatingAreaCalc As Boolean = False

    Private Sub TpEpoxy_Enter(sender As Object, e As EventArgs) Handles TpEpoxy.Enter
        If Not _topCoatGridInitialized Then
            CreateAreaCalcGrid()
            _topCoatGridInitialized = True

            ' Set default radio button
            If RbAreaBoth IsNot Nothing Then
                RbAreaBoth.Checked = True
            End If

            ' Set default epoxy depth to 1/8" (0.125")
            If TxtEpoxyDepth IsNot Nothing AndAlso String.IsNullOrWhiteSpace(TxtEpoxyDepth.Text) Then
                TxtEpoxyDepth.Text = "0.125"
            End If
        End If
    End Sub

    Private Sub CreateAreaCalcGrid()
        Try
            With DgvAreaCalc
                .Columns.Clear()
                .AllowUserToAddRows = True
                .AllowUserToDeleteRows = True
                .EditMode = DataGridViewEditMode.EditOnEnter

                ' Length column
                Dim colLength As New DataGridViewTextBoxColumn With {
                    .Name = "Length",
                    .HeaderText = "Length (in)",
                    .ValueType = GetType(Double),
                    .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "N2"}
                }
                .Columns.Add(colLength)

                ' Width column
                Dim colWidth As New DataGridViewTextBoxColumn With {
                    .Name = "Width",
                    .HeaderText = "Width (in)",
                    .ValueType = GetType(Double),
                    .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "N2"}
                }
                .Columns.Add(colWidth)

                ' Area column (read-only, calculated)
                Dim colArea As New DataGridViewTextBoxColumn With {
                    .Name = "Area",
                    .HeaderText = "Area (Sq In)",
                    .ValueType = GetType(Double),
                    .ReadOnly = True,
                    .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "N2"}
                }
                .Columns.Add(colArea)

            End With
        Catch ex As Exception
            ErrorHandler.HandleErrorWithMessage(ex, "CreateAreaCalcGrid",
                "Error initializing Area Calculator grid. Please restart the application.")
        End Try
    End Sub

    Private Sub DgvAreaCalc_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DgvAreaCalc.CellValueChanged
        Try
            If e.RowIndex < 0 Then Return

            ' Calculate area for the row if Length or Width changed
            If e.ColumnIndex = 0 OrElse e.ColumnIndex = 1 Then ' Length or Width column
                Dim row = DgvAreaCalc.Rows(e.RowIndex)
                Dim lengthCell = row.Cells("Length")
                Dim widthCell = row.Cells("Width")
                Dim areaCell = row.Cells("Area")

                If Not IsDBNull(lengthCell.Value) AndAlso Not IsDBNull(widthCell.Value) AndAlso
                   lengthCell.Value IsNot Nothing AndAlso widthCell.Value IsNot Nothing Then

                    Dim length As Double
                    Dim width As Double

                    If Double.TryParse(lengthCell.Value.ToString(), length) AndAlso
                       Double.TryParse(widthCell.Value.ToString(), width) Then
                        ' Calculate area in square inches
                        areaCell.Value = length * width
                    End If
                End If
            End If

            ' Recalculate total area from grid
            UpdateAreaFromGrid()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DgvAreaCalc_CellValueChanged")
        End Try
    End Sub

    Private Sub DgvAreaCalc_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles DgvAreaCalc.RowsRemoved
        UpdateAreaFromGrid()
    End Sub

    Private Sub UpdateAreaFromGrid()
        If Not ReentrancyGuardHelper.TryEnter(_isUpdatingAreaCalc) Then Return

        Try
            ' Calculate total area from all rows in the grid
            Dim totalAreaSqIn As Double = 0

            For Each row As DataGridViewRow In DgvAreaCalc.Rows
                If Not row.IsNewRow Then
                    Dim areaCell = row.Cells("Area")
                    If areaCell.Value IsNot Nothing AndAlso Not IsDBNull(areaCell.Value) Then
                        Dim area As Double
                        If Double.TryParse(areaCell.Value.ToString(), area) Then
                            totalAreaSqIn += area
                        End If
                    End If
                End If
            Next

            ' Convert to square feet using constant
            Dim totalAreaSqFt As Double = totalAreaSqIn / UnitConversionConstants.SQ_INCHES_TO_SQ_FEET

            ' Update the appropriate textbox(es) based on radio button selection
            If RbAreaPour IsNot Nothing AndAlso RbAreaPour.Checked Then
                ' Update only Epoxy Area
                If TxtEpoxyArea IsNot Nothing Then
                    TxtEpoxyArea.Text = totalAreaSqFt.ToString("N2")
                End If
            ElseIf RbAreaTopcoat IsNot Nothing AndAlso RbAreaTopcoat.Checked Then
                ' Update only TopCoat Area
                TxtTotalArea.Text = totalAreaSqFt.ToString("N2")
            Else ' RbAreaBoth is checked (default) or no radio buttons available
                ' Update both
                If TxtEpoxyArea IsNot Nothing Then
                    TxtEpoxyArea.Text = totalAreaSqFt.ToString("N2")
                End If
                TxtTotalArea.Text = totalAreaSqFt.ToString("N2")
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "UpdateAreaFromGrid")
        Finally
            ReentrancyGuardHelper.Exit(_isUpdatingAreaCalc)
        End Try
    End Sub

    Private Sub RbAreaSelection_CheckedChanged(sender As Object, e As EventArgs) Handles RbAreaPour.CheckedChanged, RbAreaTopcoat.CheckedChanged, RbAreaBoth.CheckedChanged
        ' Recalculate and update when radio button selection changes
        UpdateAreaFromGrid()
    End Sub

    Private Sub TxtTotalArea_TextChanged(sender As Object, e As EventArgs) Handles TxtTotalArea.TextChanged
        ' Allow manual entry in TxtTotalArea to trigger calculations
        If Not _isUpdatingTotalArea Then
            CalculateTopCoatTotals()
        End If
    End Sub

    Private Sub RbTcWaste_CheckedChanged(sender As Object, e As EventArgs) Handles RbTcWaste0.CheckedChanged, RbTcWaste10.CheckedChanged, RbTcWaste15.CheckedChanged, RbTcWaste20.CheckedChanged
        ' Recalculate when waste percentage changes
        CalculateTopCoatTotals()
    End Sub

    Private Sub CalculateTopCoatTotals()
        Try
            ' Get total area from TxtTotalArea (either from grid or manual entry)
            Dim totalAreaSqFt As Double = 0

            If String.IsNullOrWhiteSpace(TxtTotalArea.Text) OrElse Not Double.TryParse(TxtTotalArea.Text, totalAreaSqFt) Then
                ' Invalid or empty value, clear all labels
                LblTcMultiplier.Text = "Ultimate Top Coat"
                LblPartA.Text = "Part A (2:1 Ratio)"
                LblPartB.Text = "Part B (2:1 Ratio)"
                LblTopCoatWaterMult.Text = "Room Temp. Water"
                LblTCTotalMixture.Text = "Total Mixture"
                Exit Sub
            End If

            ' Determine waste percentage from radio buttons
            Dim wastePercent As Double = 0
            If RbTcWaste10.Checked Then
                wastePercent = 0.1
            ElseIf RbTcWaste15.Checked Then
                wastePercent = 0.15
            ElseIf RbTcWaste20.Checked Then
                wastePercent = 0.2
            End If

            ' Calculate TopCoat multiplier (Ultimate Top Coat amount)
            ' Apply waste percentage to the base calculation
            Dim tcMultiplier As Double = totalAreaSqFt * TOPCOAT_MULTIPLIER * (1 + wastePercent)

            ' Calculate BOTH water multipliers from the Ultimate Top Coat amount
            Dim matteWaterMultiplier As Double = tcMultiplier * MATTE_WATER_MULTIPLIER
            Dim glossWaterMultiplier As Double = tcMultiplier * GLOSS_WATER_MULTIPLIER

            ' Calculate TopCoat A (2/3 of multiplier) - 2:1 ratio Part A
            Dim topCoatA As Double = (2.0 / 3.0) * tcMultiplier

            ' Calculate TopCoat B (1/3 of multiplier) - 2:1 ratio Part B
            Dim topCoatB As Double = (1.0 / 3.0) * tcMultiplier

            ' Calculate total mixtures for both options
            Dim totalMixtureMatte As Double = topCoatA + topCoatB + matteWaterMultiplier
            Dim totalMixtureGloss As Double = topCoatA + topCoatB + glossWaterMultiplier

            ' Conversion constant: 1 oz = 29.5735 ml
            Const OZ_TO_ML As Double = 29.5735

            ' Update TC Multiplier label (Ultimate Top Coat amount before adding water)
            If LblTcMultiplier.Tag IsNot Nothing Then
                LblTcMultiplier.Text = String.Format(LblTcMultiplier.Tag.ToString(), tcMultiplier.ToString("N2")) & $" ({tcMultiplier * OZ_TO_ML:N0} ml)"
            Else
                LblTcMultiplier.Text = $"Top Coat Multiplier: {tcMultiplier:N2} oz ({tcMultiplier * OZ_TO_ML:N0} ml)"
            End If

            ' Update TopCoat A label (Part A - 2 parts of 2:1 ratio)
            ' Part A is the SAME for both Matte and Gloss (it's mixed before water is added)
            LblPartA.Text = $"Part A: {topCoatA:N2} oz ({topCoatA * OZ_TO_ML:N0} ml)"

            ' Update TopCoat B label (Part B - 1 part of 2:1 ratio)
            ' Part B is the SAME for both Matte and Gloss (it's mixed before water is added)
            LblPartB.Text = $"Part B: {topCoatB:N2} oz ({topCoatB * OZ_TO_ML:N0} ml)"

            ' Update Water label with both options (this is added AFTER mixing A+B)
            LblTopCoatWaterMult.Text = $"Room Temperature Water:" & Environment.NewLine &
                                       $"  Matte: {matteWaterMultiplier:N2} oz ({matteWaterMultiplier * OZ_TO_ML:N0} ml)" & Environment.NewLine &
                                       $"  Gloss: {glossWaterMultiplier:N2} oz ({glossWaterMultiplier * OZ_TO_ML:N0} ml)"

            ' Update Total Mixture label to show BOTH options
            If LblTCTotalMixture.Tag IsNot Nothing AndAlso LblTCTotalMixture.Tag.ToString().Contains("{0}") Then
                ' Tag format: "Total Mixture: {0}  Matte: {1:N0} oz or ({2:N0} ml){0}  Gloss: {3:N0} oz ({4:N0} ml)"
                ' Parameters: {0}=NewLine, {1}=Matte oz (number), {2}=Matte ml (number), {3}=Gloss oz (number), {4}=Gloss ml (number)
                LblTCTotalMixture.Text = String.Format(LblTCTotalMixture.Tag.ToString(),
                                                      Environment.NewLine,
                                                      totalMixtureMatte,
                                                      totalMixtureMatte * OZ_TO_ML,
                                                      totalMixtureGloss,
                                                      totalMixtureGloss * OZ_TO_ML)
            Else
                ' Fallback if tag is missing or invalid
                LblTCTotalMixture.Text = $"Total Mixture:" & Environment.NewLine &
                                         $"  Matte: {totalMixtureMatte:N2} oz ({totalMixtureMatte * OZ_TO_ML:N0} ml)" & Environment.NewLine &
                                         $"  Gloss: {totalMixtureGloss:N2} oz ({totalMixtureGloss * OZ_TO_ML:N0} ml)"
            End If
        Catch ex As Exception
            ' Silently handle calculation errors
        End Try
    End Sub

End Class
