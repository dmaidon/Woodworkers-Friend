Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports System.Text.Json

Partial Public Class FrmMain

    Public WoodCostList As New List(Of WoodCostInfo)
    Private Shared ReadOnly JsonOptions As New JsonSerializerOptions With {.WriteIndented = True}

    Public Class WoodCostInfo
        Public Property Name As String
        Public Property Thickness As String
        Public Property CostPerBoardFoot As Double

        Public ReadOnly Property DisplayName As String
            Get
                Return $"{Name} {Thickness}"
            End Get
        End Property

    End Class

#Region "Board Foot Calculations"

    Private Sub TpBoardfeet_Enter(sender As Object, e As EventArgs) Handles TpBoardfeet.Enter
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)

        LoadWoodCosts()

        With DgvBoardfeet
            .Columns.Clear()
            .AllowUserToAddRows = True
            .AllowUserToDeleteRows = True
            .EditMode = DataGridViewEditMode.EditOnEnter

            ' Length
            Dim colLength As New DataGridViewTextBoxColumn With {
            .Name = "Length",
            .HeaderText = "Length",
            .ValueType = GetType(Double),
            .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "N2"}
        }
            .Columns.Add(colLength)

            ' Width
            Dim colWidth As New DataGridViewTextBoxColumn With {
            .Name = "Width",
            .HeaderText = "Width",
            .ValueType = GetType(Double),
            .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "N2"}
        }
            .Columns.Add(colWidth)

            ' Thickness
            Dim colThickness As New DataGridViewTextBoxColumn With {
            .Name = "Thickness",
            .HeaderText = "Thickness",
            .ValueType = GetType(Double),
            .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "N2"}
        }
            .Columns.Add(colThickness)

            ' Quantity
            Dim colQuantity As New DataGridViewTextBoxColumn With {
            .Name = "Quantity",
            .HeaderText = "Quantity",
            .ValueType = GetType(Integer),
            .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "N0"}
        }
            .Columns.Add(colQuantity)

            Dim colWoodType As New DataGridViewComboBoxColumn With {
    .Name = "WoodType",
    .HeaderText = "Wood Type",
    .DataSource = WoodCostList,
    .DisplayMember = "DisplayName",
    .ValueMember = "DisplayName",
    .FlatStyle = FlatStyle.Flat
}
            .Columns.Add(colWoodType)

            ' After Quantity column, add:
            Dim colCostPerBf As New DataGridViewTextBoxColumn With {
                .Name = "CostPerBoardFoot",
                .HeaderText = "Cost/Bf",
                .ValueType = GetType(Double),
                .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "C2"}
            }
            .Columns.Add(colCostPerBf)

            ' Total Board Feet (read-only)
            Dim colTotalBF As New DataGridViewTextBoxColumn With {
            .Name = "TotalBoardFeet",
            .HeaderText = "Total Board Feet",
            .ValueType = GetType(Double),
            .ReadOnly = True,
            .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "N2", .BackColor = Color.LightGray}
        }
            .Columns.Add(colTotalBF)
            .ColumnHeadersDefaultCellStyle.Font = New Font(.Font, FontStyle.Bold)
        End With

    End Sub

    Private Sub DgvBoardFeet_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles DgvBoardfeet.CellEndEdit
        UpdateTotalBoardFeetLabel()
        UpdateTotalBoardFeetCostLabels()
    End Sub

    ' Also update the total when rows are added or removed
    Private Sub DgvBoardFeet_RowsRemoved(sender As Object, e As DataGridViewRowsRemovedEventArgs) Handles DgvBoardfeet.RowsRemoved
        UpdateTotalBoardFeetLabel()
        UpdateTotalBoardFeetCostLabels()
    End Sub

    Private Sub DgvBoardFeet_RowsAdded(sender As Object, e As DataGridViewRowsAddedEventArgs) Handles DgvBoardfeet.RowsAdded
        UpdateTotalBoardFeetLabel()
        UpdateTotalBoardFeetCostLabels()
    End Sub

    Private Sub DgvBoardFeet_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles DgvBoardfeet.CellValueChanged
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Return

        Dim row As DataGridViewRow = DgvBoardfeet.Rows(e.RowIndex)
        If row.IsNewRow Then Return

        If DgvBoardfeet.Columns(e.ColumnIndex).Name = "WoodType" Then
            Dim selected = TryCast(row.Cells("WoodType").Value, String)
            If Not String.IsNullOrEmpty(selected) Then
                Dim match = WoodCostList.FirstOrDefault(Function(w) w.DisplayName = selected)
                If match IsNot Nothing Then
                    row.Cells("CostPerBoardFoot").Value = match.CostPerBoardFoot
                End If
            End If
        End If

        '------------------
        Dim length As Double, width As Double, thickness As Double
        Dim quantity As Integer

        If Not TryGetCellValue(row, "Length", length) Then length = 0
        If Not TryGetCellValue(row, "Width", width) Then width = 0
        If Not TryGetCellValue(row, "Thickness", thickness) Then thickness = 0
        If Not TryGetCellValue(row, "Quantity", quantity) Then quantity = 0

        Dim boardFeet As Double = 0
        If length > 0 AndAlso width > 0 AndAlso thickness > 0 AndAlso quantity > 0 Then
            boardFeet = length * width * thickness / 144.0 * quantity
        End If

        row.Cells("TotalBoardFeet").Value = Math.Round(boardFeet, 2)

        UpdateTotalBoardFeetLabel()
        UpdateTotalBoardFeetCostLabels()
    End Sub

    Friend Sub LoadWoodCosts()
        Dim filePath As String = Path.Combine(SetDir, "bfCost.csv")
        If Not File.Exists(filePath) Then Return

        WoodCostList.Clear()

        Try
            For Each line In File.ReadAllLines(filePath)
                If String.IsNullOrWhiteSpace(line) Then Continue For

                ' Parse CSV line - handle quoted fields
                Dim parts = ParseCsvLine(line)
                If parts.Length >= 3 Then
                    ' Clean the thickness field (remove quotes and extra characters)
                    Dim thickness = parts(0).Replace("""", "").Trim()

                    ' Clean the name field
                    Dim name = parts(1).Replace("""", "").Trim()

                    ' Clean the cost field (remove $ and parse as double)
                    Dim costString = parts(2).Replace("$", "").Replace("""", "").Trim()
                    Dim cost As Double

                    If Double.TryParse(costString, cost) Then
                        Dim info As New WoodCostInfo With {
                            .Name = name,
                            .Thickness = thickness,
                            .CostPerBoardFoot = cost
                        }
                        WoodCostList.Add(info)
                    End If
                End If
            Next
        Catch ex As Exception
            MessageBox.Show($"Error loading wood costs from CSV: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    ' Helper method to parse CSV lines with quoted fields
    Private Function ParseCsvLine(line As String) As String()
        Dim result As New List(Of String)
        Dim currentField As New StringBuilder()
        Dim inQuotes As Boolean = False

        For i As Integer = 0 To line.Length - 1
            Dim c = line(i)

            If c = """"c Then
                inQuotes = Not inQuotes
            ElseIf c = ","c AndAlso Not inQuotes Then
                result.Add(currentField.ToString())
                currentField.Clear()
            Else
                currentField.Append(c)
            End If
        Next

        ' Add the last field
        result.Add(currentField.ToString())

        Return result.ToArray()
    End Function

    Friend Sub DgvBoardFeet_CellValidating(frm As FrmMain, sender As Object, e As DataGridViewCellValidatingEventArgs) 'Handles DgvBoardFeet.CellValidating
        ArgumentNullException.ThrowIfNull(frm)
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)

        If e.ColumnIndex >= 0 AndAlso e.ColumnIndex <= 3 Then
            Dim value As String = e.FormattedValue.ToString()
            If String.IsNullOrWhiteSpace(value) Then
                frm.DgvBoardfeet.Rows(e.RowIndex).ErrorText = "Value required"
                e.Cancel = True
            ElseIf e.ColumnIndex = 3 Then ' Quantity must be integer
                Dim intVal As Integer
                If Not Integer.TryParse(value, intVal) OrElse intVal < 0 Then
                    frm.DgvBoardfeet.Rows(e.RowIndex).ErrorText = "Enter a valid non-negative integer"
                    e.Cancel = True
                Else
                    frm.DgvBoardfeet.Rows(e.RowIndex).ErrorText = ""
                End If
            Else
                Dim dblVal As Double
                If Not Double.TryParse(value, dblVal) OrElse dblVal < 0 Then
                    frm.DgvBoardfeet.Rows(e.RowIndex).ErrorText = "Enter a valid non-negative number"
                    e.Cancel = True
                Else
                    frm.DgvBoardfeet.Rows(e.RowIndex).ErrorText = ""
                End If
            End If
        End If
    End Sub

    ' Sums the TotalBoardFeet column and updates the label
    Friend Sub UpdateTotalBoardFeetLabel()

        Dim total As Double = 0
        For Each row As DataGridViewRow In DgvBoardfeet.Rows
            If Not row.IsNewRow Then
                Dim valObj = row.Cells("TotalBoardFeet").Value
                Dim val As Double
                If valObj IsNot Nothing AndAlso Double.TryParse(valObj.ToString(), val) Then
                    total += val
                End If
            End If
        Next
        LblTotalBoardFeet.Text = $"{total:N2}"
        LblTotalBoardFeet10.Text = $"{total * 1.1:N2}"
        LblTotalBoardFeet15.Text = $"{total * 1.15:N2}"
        LblTotalBoardFeet20.Text = $"{total * 1.2:N2}"

    End Sub

    Friend Sub UpdateTotalBoardFeetCostLabels()
        Dim totalCost As Double = 0
        Dim totalCost10 As Double = 0
        Dim totalCost15 As Double = 0
        Dim totalCost20 As Double = 0

        For Each row As DataGridViewRow In DgvBoardfeet.Rows
            If row.IsNewRow Then Continue For

            Dim boardFeet As Double = 0
            Dim costPerBf As Double = 0

            'Double.TryParse(Convert.ToString(row.Cells("TotalBoardFeet").Value), boardFeet)
            'Double.TryParse(Convert.ToString(row.Cells("CostPerBoardFoot").Value), costPerBf)
            TryGetCellValue(row, "TotalBoardFeet", boardFeet)
            TryGetCellValue(row, "CostPerBoardFoot", costPerBf)

            totalCost += boardFeet * costPerBf
            totalCost10 += boardFeet * 1.1 * costPerBf
            totalCost15 += boardFeet * 1.15 * costPerBf
            totalCost20 += boardFeet * 1.2 * costPerBf
        Next

        LblBoardFeetCost.Text = totalCost.ToString("C2")
        LblBoardFeetCost10.Text = totalCost10.ToString("C2")
        LblBoardFeetCost15.Text = totalCost15.ToString("C2")
        LblBoardFeetCost20.Text = totalCost20.ToString("C2")
    End Sub

    ' Helper to safely get cell value as Double/Integer
    Friend Function TryGetCellValue(Of T)(row As DataGridViewRow, colName As String, ByRef value As T) As Boolean
        ArgumentNullException.ThrowIfNull(row)
        ArgumentException.ThrowIfNullOrEmpty(colName)
        value = Nothing
        Try
            Dim cellVal = row.Cells(colName).Value
            If cellVal Is Nothing OrElse String.IsNullOrWhiteSpace(cellVal.ToString()) Then Return False
            value = CType(Convert.ChangeType(cellVal, GetType(T)), T)
            Return True
        Catch
            Return False
        End Try
    End Function

#End Region

    Private Sub BtnSaveBfProject_Click(sender As Object, e As EventArgs) Handles BtnSaveBfProject.Click
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)

        If String.IsNullOrWhiteSpace(TxtBfProjectName.Text) Then
            MessageBox.Show("Please enter a project name before saving.", "Missing Project Name", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            TxtBfProjectName.Focus()
            Return
        End If

        Try

            Using sfd As New SaveFileDialog()
                sfd.InitialDirectory = ProjectDir
                sfd.Filter = "CSV Files (*.csv)|*.csv|JSON Files (*.json)|*.json"
                sfd.FileName = $"bf_{TxtBfProjectName.Text}_{Date.Now:yyyyMMdd}"
                If sfd.ShowDialog() <> DialogResult.OK Then Return

                Dim filePath = sfd.FileName
                If filePath.EndsWith(".json", StringComparison.OrdinalIgnoreCase) Then
                    ' Save as JSON
                    Dim rows As New List(Of Dictionary(Of String, Object))
                    For Each row As DataGridViewRow In DgvBoardfeet.Rows
                        If row.IsNewRow Then Continue For
                        Dim dict As New Dictionary(Of String, Object) From {
                            {"Length", row.Cells("Length").Value},
                            {"Width", row.Cells("Width").Value},
                            {"Thickness", row.Cells("Thickness").Value},
                            {"Quantity", row.Cells("Quantity").Value},
                            {"WoodType", row.Cells("WoodType").Value},
                            {"CostPerBoardFoot", row.Cells("CostPerBoardFoot").Value},
                            {"TotalBoardFeet", Math.Round(Convert.ToDouble(row.Cells("TotalBoardFeet").Value), 2)}
                        }
                        rows.Add(dict)
                    Next
                    Dim json = JsonSerializer.Serialize(rows, JsonOptions)
                    File.WriteAllText(filePath, json, Encoding.UTF8)
                Else
                    ' Save as CSV
                    Using writer As New StreamWriter(filePath, False, Encoding.UTF8)
                        writer.WriteLine("Length,Width,Thickness,Quantity,WoodType,CostPerBoardFoot,TotalBoardFeet")
                        For Each row As DataGridViewRow In DgvBoardfeet.Rows
                            If row.IsNewRow Then Continue For
                            Dim length = If(row.Cells("Length").Value IsNot Nothing, row.Cells("Length").Value.ToString(), "")
                            Dim width = If(row.Cells("Width").Value IsNot Nothing, row.Cells("Width").Value.ToString(), "")
                            Dim thickness = If(row.Cells("Thickness").Value IsNot Nothing, row.Cells("Thickness").Value.ToString(), "")
                            Dim quantity = If(row.Cells("Quantity").Value IsNot Nothing, row.Cells("Quantity").Value.ToString(), "")
                            Dim woodType = If(row.Cells("WoodType").Value IsNot Nothing, row.Cells("WoodType").Value.ToString(), "")
                            Dim costPerBf = If(row.Cells("CostPerBoardFoot").Value IsNot Nothing, row.Cells("CostPerBoardFoot").Value.ToString(), "")
                            Dim totalBfVal As Double = 0
                            If Double.TryParse(row.Cells("TotalBoardFeet").Value?.ToString(), totalBfVal) Then
                                totalBfVal = Math.Round(totalBfVal, 2)
                            End If
                            Dim totalBf = totalBfVal.ToString("N2")
                            writer.WriteLine($"{length},{width},{thickness},{quantity},{woodType},{costPerBf},{totalBf}")
                        Next
                    End Using
                End If

                MessageBox.Show($"Project saved to {filePath}", "Save Project", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End Using
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Printing"

    Private ReadOnly PrintFontHeader As New Font("Segoe UI", 10, FontStyle.Bold)
    Private ReadOnly PrintFontRow As New Font("Segoe UI", 10)
    Private ReadOnly PrintFontTitle As New Font("Segoe UI", 12, FontStyle.Bold)

    Private Sub BtnPrtBfProject_Click(sender As Object, e As EventArgs) Handles BtnPrtBfProject.Click
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)

        Dim printDoc As New PrintDocument()
        AddHandler printDoc.PrintPage, AddressOf PrintBfProjectPage

        Dim printDlg As New PrintDialog() With {.Document = printDoc}
        If printDlg.ShowDialog() = DialogResult.OK Then
            Dim previewDlg As New PrintPreviewDialog() With {.Document = printDoc}
            previewDlg.ShowDialog()
        End If
    End Sub

    Private Sub PrintBfProjectPage(sender As Object, e As PrintPageEventArgs)
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
        Dim g = e.Graphics
        Dim leftMargin = e.MarginBounds.Left
        Dim topMargin = e.MarginBounds.Top
        Dim lineHeight As Integer = CInt(PrintFontRow.GetHeight(g)) + 4
        Dim x = leftMargin
        Dim y = topMargin

        Try

            ' Print Project Name or space for it
            Dim projectName = TxtBfProjectName.Text.Trim()
            If String.IsNullOrEmpty(projectName) Then
                g.DrawString("Project Name: ___________________________", PrintFontTitle, Brushes.Black, x, y)
            Else
                g.DrawString($"Project Name: {projectName}", PrintFontTitle, Brushes.Black, x, y)
            End If
            y += lineHeight * 2

            ' Print column headers
            Dim headers = {"Length", "Width", "Thickness", "Quantity", "Wood Type", "Cost/Bf", "Total Board Feet"}
            Dim colWidths = {70, 70, 80, 70, 120, 80, 110}
            Dim colX = x
            For i = 0 To headers.Length - 1
                g.DrawString(headers(i), PrintFontHeader, Brushes.Black, colX, y)
                colX += colWidths(i)
            Next
            y += lineHeight

            ' Print rows
            For Each row As DataGridViewRow In DgvBoardfeet.Rows
                If row.IsNewRow Then Continue For
                colX = x
                g.DrawString(GetCellString(row, "Length"), PrintFontRow, Brushes.Black, colX, y)
                colX += colWidths(0)
                g.DrawString(GetCellString(row, "Width"), PrintFontRow, Brushes.Black, colX, y)
                colX += colWidths(1)
                g.DrawString(GetCellString(row, "Thickness"), PrintFontRow, Brushes.Black, colX, y)
                colX += colWidths(2)
                g.DrawString(GetCellString(row, "Quantity"), PrintFontRow, Brushes.Black, colX, y)
                colX += colWidths(3)
                g.DrawString(GetCellString(row, "WoodType"), PrintFontRow, Brushes.Black, colX, y)
                colX += colWidths(4)
                g.DrawString(GetCellString(row, "CostPerBoardFoot"), PrintFontRow, Brushes.Black, colX, y)
                colX += colWidths(5)
                Dim totalBfVal As Double = 0
                If Double.TryParse(row.Cells("TotalBoardFeet").Value?.ToString(), totalBfVal) Then
                    g.DrawString(totalBfVal.ToString("N2"), PrintFontRow, Brushes.Black, colX, y)
                Else
                    g.DrawString("", PrintFontRow, Brushes.Black, colX, y)
                End If
                y += lineHeight
            Next

            ' Print totals
            y += lineHeight
            g.DrawString($"Total Board Feet: {LblTotalBoardFeet.Text}", PrintFontHeader, Brushes.Black, x, y)
            y += lineHeight
            g.DrawString($"Total Cost: {LblBoardFeetCost.Text}", PrintFontHeader, Brushes.Black, x, y)
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function GetCellString(row As DataGridViewRow, colName As String) As String
        Dim val = row.Cells(colName).Value
        Return If(val IsNot Nothing, val.ToString(), "")
    End Function

#End Region

End Class