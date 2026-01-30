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

    ' Add fields to store both scales
    Private TotalBoardFeetImperial As Double = 0

    Private TotalBoardFeetMetric As Double = 0

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

            ' Total Board Feet (read-only)
            Dim colTotalBM As New DataGridViewTextBoxColumn With {
            .Name = "TotalBoardMeters",
            .HeaderText = "Total Board Meters",
            .ValueType = GetType(Double),
            .ReadOnly = True,
            .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "N2", .BackColor = Color.LightGray}
        }
            .Columns.Add(colTotalBM)
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

        Dim length As Double, width As Double, thickness As Double
        Dim quantity As Integer

        If Not TryGetCellValue(row, "Length", length) Then length = 0
        If Not TryGetCellValue(row, "Width", width) Then width = 0
        If Not TryGetCellValue(row, "Thickness", thickness) Then thickness = 0
        If Not TryGetCellValue(row, "Quantity", quantity) Then quantity = 0

        Dim boardFeetImperial As Double = 0
        Dim boardFeetMetric As Double = 0

        ' Use ScaleManager to convert and calculate both scales
        If length > 0 AndAlso width > 0 AndAlso thickness > 0 AndAlso quantity > 0 Then
            If _scaleManager.CurrentScale = ScaleManager.ScaleType.Imperial Then
                boardFeetImperial = length * width * thickness / 144.0 * quantity
                ' Convert all dimensions to millimeters for metric calculation
                Dim lengthMM = ScaleManager.ToMetricInches(length)
                Dim widthMM = ScaleManager.ToMetricInches(width)
                Dim thicknessMM = ScaleManager.ToMetricInches(thickness)
                ' Cubic millimeters to cubic meters, then to board meters (1 board meter = 1m x 1m x 25.4mm)
                boardFeetMetric = (lengthMM * widthMM * thicknessMM * quantity) / (1000 * 1000 * 25.4)
            Else
                ' Metric input
                boardFeetMetric = length * width * thickness * quantity / (1000 * 1000 * 25.4)
                ' Convert all dimensions to inches for imperial calculation
                Dim lengthIn = ScaleManager.ToImperialMillimeters(length)
                Dim widthIn = ScaleManager.ToImperialMillimeters(width)
                Dim thicknessIn = ScaleManager.ToImperialMillimeters(thickness)
                boardFeetImperial = lengthIn * widthIn * thicknessIn / 144.0 * quantity
            End If
        End If

        row.Cells("TotalBoardFeet").Value = Math.Round(boardFeetImperial, 2)
        row.Cells("TotalBoardMeters").Value = Math.Round(boardFeetMetric, 2) ' If you add a metric column

        UpdateTotalBoardFeetLabel()
        UpdateTotalBoardFeetCostLabels()
    End Sub

    Friend Sub LoadWoodCosts()
        ' Phase 7.3: Load from database with CSV fallback
        WoodCostList.Clear()

        Try
            ' Try database first
            Dim dbCosts = DatabaseManager.Instance.GetAllWoodCosts()

            If dbCosts.Count > 0 Then
                ' Convert WoodCost to WoodCostInfo
                For Each cost In dbCosts
                    WoodCostList.Add(New WoodCostInfo With {
                        .Name = cost.WoodName,
                        .Thickness = cost.Thickness,
                        .CostPerBoardFoot = cost.CostPerBoardFoot
                    })
                Next
                ErrorHandler.LogError(New Exception($"Loaded {WoodCostList.Count} wood costs from database"), "LoadWoodCosts")
                Return
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "LoadWoodCosts - Database failed, trying CSV fallback")
        End Try

        ' Fallback to CSV if database empty or fails
        Dim filePath As String = Path.Combine(SetDir, "bfCost.csv")
        If Not File.Exists(filePath) Then Return

        Try
            For Each line In File.ReadAllLines(filePath)
                If String.IsNullOrWhiteSpace(line) Then Continue For

                ' Parse CSV line - handle quoted fields
                Dim parts = ParseCsvLine(line)
                If parts.Length >= 3 Then
                    ' Clean the thickness field (remove quotes and extra characters)
                    Dim thickness = parts(0).Replace("""", "").Trim()

                    ' Clean the name field and convert to Title Case
                    Dim name = parts(1).Replace("""", "").Trim()
                    name = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(name.ToLower())

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
        ' Sum both scales
        TotalBoardFeetImperial = 0
        TotalBoardFeetMetric = 0
        For Each row As DataGridViewRow In DgvBoardfeet.Rows
            If Not row.IsNewRow Then
                Dim valObj = row.Cells("TotalBoardFeet").Value
                Dim valImp As Double
                If valObj IsNot Nothing AndAlso Double.TryParse(valObj.ToString(), valImp) Then
                    TotalBoardFeetImperial += valImp
                End If
                Dim valMetObj = row.Cells("TotalBoardMeters").Value
                Dim valMet As Double
                If valMetObj IsNot Nothing AndAlso Double.TryParse(valMetObj.ToString(), valMet) Then
                    TotalBoardFeetMetric += valMet
                End If
            End If
        Next
        LblTotalBoardFeet.Text = $"{TotalBoardFeetImperial:N2} bf ({TotalBoardFeetMetric:N2} bm)"
        LblTotalBoardFeet10.Text = $"{TotalBoardFeetImperial * 1.1:N2} bf ({TotalBoardFeetMetric * 1.1:N2} bm)"
        LblTotalBoardFeet15.Text = $"{TotalBoardFeetImperial * 1.15:N2} bf ({TotalBoardFeetMetric * 1.15:N2} bm)"
        LblTotalBoardFeet20.Text = $"{TotalBoardFeetImperial * 1.2:N2} bf ({TotalBoardFeetMetric * 1.2:N2} bm)"
    End Sub

    Friend Sub UpdateTotalBoardFeetCostLabels()
        Dim totalCostImp As Double = 0
        Dim totalCost10Imp As Double = 0
        Dim totalCost15Imp As Double = 0
        Dim totalCost20Imp As Double = 0

        For Each row As DataGridViewRow In DgvBoardfeet.Rows
            If row.IsNewRow Then Continue For

            Dim boardFeetImp As Double = 0
            Dim costPerBf As Double = 0

            TryGetCellValue(row, "TotalBoardFeet", boardFeetImp)
            TryGetCellValue(row, "CostPerBoardFoot", costPerBf)

            totalCostImp += boardFeetImp * costPerBf
            totalCost10Imp += boardFeetImp * 1.1 * costPerBf
            totalCost15Imp += boardFeetImp * 1.15 * costPerBf
            totalCost20Imp += boardFeetImp * 1.2 * costPerBf
        Next

        LblBoardFeetCost.Text = $"{totalCostImp:C2}"
        LblBoardFeetCost10.Text = $"{totalCost10Imp:C2}"
        LblBoardFeetCost15.Text = $"{totalCost15Imp:C2}"
        LblBoardFeetCost20.Text = $"{totalCost20Imp:C2}"
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

#Region "Board Feet History (Phase 6)"

    ''' <summary>
    ''' Saves current board feet calculation to history
    ''' </summary>
    Private Sub BtnSaveBoardFeetHistory_Click(sender As Object, e As EventArgs) Handles BtnSaveBoardFeetHistory.Click
        Try
            ' Validate we have data
            If DgvBoardfeet.Rows.Count = 0 OrElse DgvBoardfeet.Rows.Count = 1 AndAlso DgvBoardfeet.Rows(0).IsNewRow Then
                MessageBox.Show("No calculations to save! Enter dimensions first.",
                              "Save History", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Get values from first data row
            Dim row = DgvBoardfeet.Rows(0)
            Dim thickness, width, length As Double
            Dim quantity As Integer

            If Not TryGetCellValue(row, "Thickness", thickness) Then thickness = 0
            If Not TryGetCellValue(row, "Width", width) Then width = 0
            If Not TryGetCellValue(row, "Length", length) Then length = 0
            If Not TryGetCellValue(row, "Quantity", quantity) Then quantity = 1

            ' Validate we have actual values
            If thickness = 0 OrElse width = 0 OrElse length = 0 Then
                MessageBox.Show("Please enter thickness, width, and length values.",
                              "Save History", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Get calculated board feet
            Dim boardFeet As Double
            If Not TryGetCellValue(row, "TotalBoardFeet", boardFeet) Then
                boardFeet = thickness * width * length / 144.0 * quantity
            End If

            ' Calculate cubic measurements
            Dim cubicInches = thickness * width * length * quantity
            Dim cubicFeet = cubicInches / 1728.0

            ' Ask for optional name
            Dim name = InputBox($"Save calculation:{vbCrLf}" &
                               $"Thickness: {thickness}""{vbCrLf}" &
                               $"Width: {width}""{vbCrLf}" &
                               $"Length: {length}""{vbCrLf}" &
                               $"Quantity: {quantity}{vbCrLf}{vbCrLf}" &
                               "Enter a name (or leave blank):",
                               "Save Calculation",
                               $"{thickness}x{width}x{length} ({quantity})")

            ' Save to database
            If BoardFeetHistoryHelper.SaveCalculation(thickness, width, length, quantity,
                                                     boardFeet, cubicInches, cubicFeet, name) Then
                MessageBox.Show("✅ Calculation saved to history!", "Success",
                              MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to save calculation. Check error log for details.",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnSaveBoardFeetHistory_Click")
            MessageBox.Show($"Error saving calculation:{vbCrLf}{ex.Message}",
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Loads a calculation from history
    ''' </summary>
    Private Sub BtnLoadBoardFeetHistory_Click(sender As Object, e As EventArgs) Handles BtnLoadBoardFeetHistory.Click
        Try
            ' Show history dialog
            Dim history = BoardFeetHistoryHelper.ShowHistoryDialog()
            If history Is Nothing Then Return ' User cancelled

            ' Extract values from history
            Dim thickness, width, length As Double
            Dim quantity As Integer

            If Not BoardFeetHistoryHelper.LoadCalculation(history, thickness, width, length, quantity) Then
                MessageBox.Show("Failed to load calculation data.",
                              "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Return
            End If

            ' Clear existing rows
            DgvBoardfeet.Rows.Clear()

            ' Add new row with loaded values
            Dim rowIndex = DgvBoardfeet.Rows.Add()
            Dim newRow = DgvBoardfeet.Rows(rowIndex)

            ' Populate cells
            newRow.Cells("Thickness").Value = thickness
            newRow.Cells("Width").Value = width
            newRow.Cells("Length").Value = length
            newRow.Cells("Quantity").Value = quantity

            ' Trigger recalculation
            DgvBoardFeet_CellValueChanged(DgvBoardfeet, New DataGridViewCellEventArgs(0, rowIndex))

            ' Success message
            Dim calcName = If(String.IsNullOrEmpty(history.CalculationName), "(unnamed)", history.CalculationName)
            MessageBox.Show($"✅ Loaded: {calcName}{vbCrLf}{vbCrLf}" &
                          $"Thickness: {thickness}""{vbCrLf}" &
                          $"Width: {width}""{vbCrLf}" &
                          $"Length: {length}""{vbCrLf}" &
                          $"Quantity: {quantity}",
                          "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnLoadBoardFeetHistory_Click")
            MessageBox.Show($"Error loading calculation:{vbCrLf}{ex.Message}",
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

End Class
