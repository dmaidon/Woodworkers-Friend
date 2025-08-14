Imports System.Windows.Forms

Namespace WwFriend.Modules.Drawers
    Public Module DrawerResultsPresenter

        Public Sub Present(form As FrmMain, result As DrawerCalculationResult)
            ArgumentNullException.ThrowIfNull(form)
            ArgumentNullException.ThrowIfNull(result)

            If Not result.IsValid Then
                Clear(form)
                Return
            End If

            ' Detailed results text
            If form.RtbResults IsNot Nothing Then
                form.RtbResults.Text = result.Details
            End If

            ' Labels with tags as format strings; fallback if Tag missing
            If form.LblTotalHeightResults IsNot Nothing Then
                Dim fmt As String = SafeTag(form.LblTotalHeightResults, "Total Height: {0}")
                form.LblTotalHeightResults.Text = String.Format(fmt, $"{result.TotalHeightImperial:N3} in  ({result.TotalHeightMetric:N3} mm)")
            End If

            If form.LbltotalDrawerHeightResults IsNot Nothing Then
                Dim fmt As String = SafeTag(form.LbltotalDrawerHeightResults, "Total Drawer Height: {0}")
                form.LbltotalDrawerHeightResults.Text = String.Format(fmt, $"{result.TotalDrawerHeightImperial:N3} in ({result.TotalDrawerHeightMetric:N3} mm)")
            End If

            If form.LblTotalMaterialResults IsNot Nothing Then
                Dim fmt As String = SafeTag(form.LblTotalMaterialResults, "Material: {0}")
                form.LblTotalMaterialResults.Text = String.Format(fmt, $"{result.TotalMaterialAreaImperial:N3} sqft ({result.TotalMaterialAreaMetric:N3} m²)")
            End If

            If form.LblAverageHeightResults IsNot Nothing Then
                Dim fmt As String = SafeTag(form.LblAverageHeightResults, "Average Height: {0}")
                form.LblAverageHeightResults.Text = String.Format(fmt, $"{result.AverageDrawerHeightImperial:N3} in ({result.AverageDrawerHeightMetric:N3} mm)")
            End If

            If form.LblHeightRatioResults IsNot Nothing Then
                Dim tag As Object = form.LblHeightRatioResults.Tag
                If tag IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(CStr(tag)) Then
                    form.LblHeightRatioResults.Text = String.Format(CStr(tag), $"{result.HeightRatio:N2}:1")
                Else
                    form.LblHeightRatioResults.Text = $"Height Ratio: {result.HeightRatio:N2}:1"
                End If
            End If

            ' Grid
            PopulateGrid(form, result)
        End Sub

        Public Sub Clear(form As FrmMain)
            If form Is Nothing Then Return

            If form.RtbResults IsNot Nothing Then form.RtbResults.Text = String.Empty
            If form.LblTotalHeightResults IsNot Nothing Then form.LblTotalHeightResults.Text = "Total Height: --"
            If form.LbltotalDrawerHeightResults IsNot Nothing Then form.LbltotalDrawerHeightResults.Text = "Total Drawer Height: --"
            If form.LblTotalMaterialResults IsNot Nothing Then form.LblTotalMaterialResults.Text = "Material: --"
            If form.LblAverageHeightResults IsNot Nothing Then form.LblAverageHeightResults.Text = "Average Height: --"
            If form.LblHeightRatioResults IsNot Nothing Then form.LblHeightRatioResults.Text = "Height Ratio: --"
            If form.DgvDrawerHeights IsNot Nothing Then form.DgvDrawerHeights.Rows.Clear()
        End Sub

        Private Sub PopulateGrid(form As FrmMain, result As DrawerCalculationResult)
            If form.DgvDrawerHeights Is Nothing Then Return

            form.DgvDrawerHeights.Rows.Clear()

            Dim count As Integer = If(result.DrawerHeightsImperial IsNot Nothing, result.DrawerHeightsImperial.Length, 0)
            For i As Integer = 0 To count - 1
                Dim rowIndex As Integer = form.DgvDrawerHeights.Rows.Add()
                Dim row As DataGridViewRow = form.DgvDrawerHeights.Rows(rowIndex)

                row.Cells("DrawerNumber").Value = (i + 1).ToString()
                row.Cells("DwHeight").Value = $"{result.DrawerHeightsImperial(i):N3} in ({result.DrawerHeightsMetric(i):N3} mm)"
                row.Cells("Unit").Value = "in / mm"

                Dim percentage As Double = If(result.TotalDrawerHeightImperial > 0.0R,
                                              result.DrawerHeightsImperial(i) / result.TotalDrawerHeightImperial * 100.0R,
                                              0.0R)
                row.Cells("Percentage").Value = $"{percentage:N1}%"
            Next
        End Sub

        Private Function SafeTag(lbl As Label, fallback As String) As String
            Dim tag As Object = lbl.Tag
            If tag Is Nothing Then Return fallback
            Dim s As String = CStr(tag)
            If String.IsNullOrWhiteSpace(s) Then Return fallback
            Return s
        End Function

    End Module
End Namespace