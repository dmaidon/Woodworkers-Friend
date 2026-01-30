Imports System.ComponentModel

Public Class FrmManageCosts
    Private _currentTab As String = "Wood" ' "Wood" or "Epoxy"
    Private _woodCosts As BindingList(Of WoodCost)
    Private _epoxyCosts As BindingList(Of EpoxyCost)
    Private _loading As Boolean = False
    Private _woodSortColumn As String = ""
    Private _woodSortOrder As SortOrder = SortOrder.None
    Private _epoxySortColumn As String = ""
    Private _epoxySortOrder As SortOrder = SortOrder.None

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub FrmManageCosts_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            ' Set form properties
            Me.Text = "Manage Costs - Wood & Epoxy"
            Me.Size = New Size(900, 600)
            Me.StartPosition = FormStartPosition.CenterParent
            Me.FormBorderStyle = FormBorderStyle.Sizable
            Me.MinimumSize = New Size(800, 500)

            ' Load data
            LoadWoodCosts()
            LoadEpoxyCosts()

            ' Attach sorting handlers (must be after data is loaded)
            AddHandler DgvWoodCosts.ColumnHeaderMouseClick, AddressOf DgvWoodCosts_ColumnHeaderMouseClick
            AddHandler DgvEpoxyCosts.ColumnHeaderMouseClick, AddressOf DgvEpoxyCosts_ColumnHeaderMouseClick

            ' Select first tab
            TcCosts.SelectedIndex = 0
        Catch ex As Exception
            ErrorHandler.LogError(ex, "FrmManageCosts_Load")
            MessageBox.Show($"Error loading form: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#Region "Wood Costs"

    Private Sub LoadWoodCosts()
        Try
            _loading = True

            ' Get data from database
            Dim woodCosts = DatabaseManager.Instance.GetAllWoodCosts()
            _woodCosts = New BindingList(Of WoodCost)(woodCosts)

            ' Bind to DataGridView
            DgvWoodCosts.DataSource = _woodCosts
            DgvWoodCosts.AutoGenerateColumns = False

            ' Configure columns if not already done
            If DgvWoodCosts.Columns.Count = 0 Then
                ConfigureWoodCostsGrid()
            End If

            _loading = False
        Catch ex As Exception
            ErrorHandler.LogError(ex, "LoadWoodCosts")
            MessageBox.Show($"Error loading wood costs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigureWoodCostsGrid()
        DgvWoodCosts.Columns.Clear()

        ' ID (hidden)
        Dim colID As New DataGridViewTextBoxColumn With {
            .Name = "WoodCostID",
            .DataPropertyName = "WoodCostID",
            .Visible = False
        }
        DgvWoodCosts.Columns.Add(colID)

        ' Thickness
        Dim colThickness As New DataGridViewTextBoxColumn With {
            .Name = "Thickness",
            .HeaderText = "Thickness",
            .DataPropertyName = "Thickness",
            .Width = 100,
            .SortMode = DataGridViewColumnSortMode.Automatic
        }
        DgvWoodCosts.Columns.Add(colThickness)

        ' Wood Name
        Dim colName As New DataGridViewTextBoxColumn With {
            .Name = "WoodName",
            .HeaderText = "Wood Species",
            .DataPropertyName = "WoodName",
            .Width = 200,
            .SortMode = DataGridViewColumnSortMode.Automatic
        }
        DgvWoodCosts.Columns.Add(colName)

        ' Cost Per Board Foot
        Dim colCost As New DataGridViewTextBoxColumn With {
            .Name = "CostPerBoardFoot",
            .HeaderText = "Cost/BF",
            .DataPropertyName = "CostPerBoardFoot",
            .Width = 100,
            .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "C2"},
            .SortMode = DataGridViewColumnSortMode.Automatic
        }
        DgvWoodCosts.Columns.Add(colCost)

        ' Active
        Dim colActive As New DataGridViewCheckBoxColumn With {
            .Name = "Active",
            .HeaderText = "Active",
            .DataPropertyName = "Active",
            .Width = 60,
            .SortMode = DataGridViewColumnSortMode.Automatic
        }
        DgvWoodCosts.Columns.Add(colActive)

        ' User Added
        Dim colUserAdded As New DataGridViewCheckBoxColumn With {
            .Name = "IsUserAdded",
            .HeaderText = "Custom",
            .DataPropertyName = "IsUserAdded",
            .Width = 60,
            .ReadOnly = True,
            .SortMode = DataGridViewColumnSortMode.Automatic
        }
        DgvWoodCosts.Columns.Add(colUserAdded)

        ' Date Added
        Dim colDateAdded As New DataGridViewTextBoxColumn With {
            .Name = "DateAdded",
            .HeaderText = "Date Added",
            .DataPropertyName = "DateAdded",
            .Width = 120,
            .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "MM/dd/yyyy"},
            .ReadOnly = True,
            .SortMode = DataGridViewColumnSortMode.Automatic
        }
        DgvWoodCosts.Columns.Add(colDateAdded)

        ' Configure grid appearance
        DgvWoodCosts.AllowUserToAddRows = False
        DgvWoodCosts.AllowUserToDeleteRows = False
        DgvWoodCosts.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DgvWoodCosts.MultiSelect = False
        DgvWoodCosts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub DgvWoodCosts_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        Try
            Dim columnName = DgvWoodCosts.Columns(e.ColumnIndex).DataPropertyName
            If String.IsNullOrEmpty(columnName) Then
                ErrorHandler.LogError(New Exception($"Column at index {e.ColumnIndex} has no DataPropertyName"), "DgvWoodCosts_ColumnHeaderMouseClick")
                Return
            End If

            ' Toggle sort order
            If _woodSortColumn = columnName Then
                _woodSortOrder = If(_woodSortOrder = SortOrder.Ascending, SortOrder.Descending, SortOrder.Ascending)
            Else
                _woodSortColumn = columnName
                _woodSortOrder = SortOrder.Ascending
            End If

            ' Get current data as list
            Dim sortedList = _woodCosts.ToList()

            ' Sort based on column
            Select Case columnName
                Case "Thickness"
                    sortedList = If(_woodSortOrder = SortOrder.Ascending,
                        sortedList.OrderBy(Function(x) x.Thickness).ToList(),
                        sortedList.OrderByDescending(Function(x) x.Thickness).ToList())
                Case "WoodName"
                    sortedList = If(_woodSortOrder = SortOrder.Ascending,
                        sortedList.OrderBy(Function(x) x.WoodName).ToList(),
                        sortedList.OrderByDescending(Function(x) x.WoodName).ToList())
                Case "CostPerBoardFoot"
                    sortedList = If(_woodSortOrder = SortOrder.Ascending,
                        sortedList.OrderBy(Function(x) x.CostPerBoardFoot).ToList(),
                        sortedList.OrderByDescending(Function(x) x.CostPerBoardFoot).ToList())
                Case "Active"
                    sortedList = If(_woodSortOrder = SortOrder.Ascending,
                        sortedList.OrderBy(Function(x) x.Active).ToList(),
                        sortedList.OrderByDescending(Function(x) x.Active).ToList())
                Case "IsUserAdded"
                    sortedList = If(_woodSortOrder = SortOrder.Ascending,
                        sortedList.OrderBy(Function(x) x.IsUserAdded).ToList(),
                        sortedList.OrderByDescending(Function(x) x.IsUserAdded).ToList())
                Case "DateAdded"
                    sortedList = If(_woodSortOrder = SortOrder.Ascending,
                        sortedList.OrderBy(Function(x) x.DateAdded).ToList(),
                        sortedList.OrderByDescending(Function(x) x.DateAdded).ToList())
            End Select

            ' Update binding list
            _woodCosts = New BindingList(Of WoodCost)(sortedList)
            DgvWoodCosts.DataSource = _woodCosts

            ' Update column header to show sort direction
            For Each col As DataGridViewColumn In DgvWoodCosts.Columns
                col.HeaderCell.SortGlyphDirection = SortOrder.None
            Next
            DgvWoodCosts.Columns(e.ColumnIndex).HeaderCell.SortGlyphDirection = _woodSortOrder
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DgvWoodCosts_ColumnHeaderMouseClick")
            MessageBox.Show($"Error sorting: {ex.Message}", "Sort Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub BtnAddWoodCost_Click(sender As Object, e As EventArgs) Handles BtnAddWoodCost.Click
        Try
            ' Prompt for values
            Dim thickness = InputBox("Enter thickness (e.g., 4/4, 5/4, 8/4):", "Add Wood Cost", "4/4")
            If String.IsNullOrWhiteSpace(thickness) Then Return

            Dim woodName = InputBox("Enter wood species name:", "Add Wood Cost", "")
            If String.IsNullOrWhiteSpace(woodName) Then Return

            Dim costStr = InputBox("Enter cost per board foot (e.g., 5.60):", "Add Wood Cost", "")
            If String.IsNullOrWhiteSpace(costStr) Then Return

            Dim cost As Double
            If Not Double.TryParse(costStr, cost) Then
                MessageBox.Show("Invalid cost value. Please enter a number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Create new wood cost
            Dim newCost As New WoodCost With {
                .Thickness = thickness.Trim(),
                .WoodName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(woodName.Trim().ToLower()),
                .CostPerBoardFoot = cost,
                .Active = True,
                .IsUserAdded = True
            }

            ' Save to database
            If DatabaseManager.Instance.AddWoodCost(newCost) Then
                MessageBox.Show("Wood cost added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadWoodCosts() ' Reload grid
            Else
                MessageBox.Show("Failed to add wood cost. It may already exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnAddWoodCost_Click")
            MessageBox.Show($"Error adding wood cost: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnSaveWoodChanges_Click(sender As Object, e As EventArgs) Handles BtnSaveWoodChanges.Click
        Try
            Dim saveCount = 0
            Dim errorCount = 0

            For Each row As DataGridViewRow In DgvWoodCosts.Rows
                If row.IsNewRow Then Continue For

                ' Get wood cost from row
                Dim woodCost = DirectCast(_woodCosts(row.Index), WoodCost)

                ' Update from cells (in case user edited)
                woodCost.Thickness = row.Cells("Thickness").Value?.ToString()
                woodCost.WoodName = row.Cells("WoodName").Value?.ToString()
                woodCost.CostPerBoardFoot = Convert.ToDouble(row.Cells("CostPerBoardFoot").Value)
                woodCost.Active = Convert.ToBoolean(row.Cells("Active").Value)

                ' Save to database
                If DatabaseManager.Instance.UpdateWoodCost(woodCost) Then
                    saveCount += 1
                Else
                    errorCount += 1
                End If
            Next

            MessageBox.Show($"Saved {saveCount} wood cost(s).{If(errorCount > 0, $" {errorCount} error(s).", "")}",
                          "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadWoodCosts() ' Reload to get fresh data
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnSaveWoodChanges_Click")
            MessageBox.Show($"Error saving changes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnDeleteWoodCost_Click(sender As Object, e As EventArgs) Handles BtnDeleteWoodCost.Click
        Try
            If DgvWoodCosts.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select a wood cost to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim selectedRow = DgvWoodCosts.SelectedRows(0)
            Dim woodCost = DirectCast(_woodCosts(selectedRow.Index), WoodCost)

            Dim result = MessageBox.Show($"Delete '{woodCost.DisplayName}'?{vbCrLf}{vbCrLf}This will mark it as inactive (soft delete).",
                                       "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                If DatabaseManager.Instance.DeleteWoodCost(woodCost.WoodCostID) Then
                    MessageBox.Show("Wood cost deleted (marked inactive).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadWoodCosts() ' Reload grid
                Else
                    MessageBox.Show("Failed to delete wood cost.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnDeleteWoodCost_Click")
            MessageBox.Show($"Error deleting wood cost: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

#Region "Epoxy Costs"

    Private Sub LoadEpoxyCosts()
        Try
            _loading = True

            ' Get data from database
            Dim epoxyCosts = DatabaseManager.Instance.GetAllEpoxyCosts()
            _epoxyCosts = New BindingList(Of EpoxyCost)(epoxyCosts)

            ' Bind to DataGridView
            DgvEpoxyCosts.DataSource = _epoxyCosts
            DgvEpoxyCosts.AutoGenerateColumns = False

            ' Configure columns if not already done
            If DgvEpoxyCosts.Columns.Count = 0 Then
                ConfigureEpoxyCostsGrid()
            End If

            _loading = False
        Catch ex As Exception
            ErrorHandler.LogError(ex, "LoadEpoxyCosts")
            MessageBox.Show($"Error loading epoxy costs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ConfigureEpoxyCostsGrid()
        DgvEpoxyCosts.Columns.Clear()

        ' ID (hidden)
        Dim colID As New DataGridViewTextBoxColumn With {
            .Name = "EpoxyCostID",
            .DataPropertyName = "EpoxyCostID",
            .Visible = False
        }
        DgvEpoxyCosts.Columns.Add(colID)

        ' Brand
        Dim colBrand As New DataGridViewTextBoxColumn With {
            .Name = "Brand",
            .HeaderText = "Brand",
            .DataPropertyName = "Brand",
            .Width = 150,
            .SortMode = DataGridViewColumnSortMode.Automatic
        }
        DgvEpoxyCosts.Columns.Add(colBrand)

        ' Type
        Dim colType As New DataGridViewTextBoxColumn With {
            .Name = "Type",
            .HeaderText = "Type",
            .DataPropertyName = "Type",
            .Width = 150,
            .SortMode = DataGridViewColumnSortMode.Automatic
        }
        DgvEpoxyCosts.Columns.Add(colType)

        ' Cost Per Gallon
        Dim colCost As New DataGridViewTextBoxColumn With {
            .Name = "CostPerGallon",
            .HeaderText = "Cost/Gal",
            .DataPropertyName = "CostPerGallon",
            .Width = 100,
            .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "C2"},
            .SortMode = DataGridViewColumnSortMode.Automatic
        }
        DgvEpoxyCosts.Columns.Add(colCost)

        ' Active
        Dim colActive As New DataGridViewCheckBoxColumn With {
            .Name = "Active",
            .HeaderText = "Active",
            .DataPropertyName = "Active",
            .Width = 60,
            .SortMode = DataGridViewColumnSortMode.Automatic
        }
        DgvEpoxyCosts.Columns.Add(colActive)

        ' User Added
        Dim colUserAdded As New DataGridViewCheckBoxColumn With {
            .Name = "IsUserAdded",
            .HeaderText = "Custom",
            .DataPropertyName = "IsUserAdded",
            .Width = 60,
            .ReadOnly = True,
            .SortMode = DataGridViewColumnSortMode.Automatic
        }
        DgvEpoxyCosts.Columns.Add(colUserAdded)

        ' Date Added
        Dim colDateAdded As New DataGridViewTextBoxColumn With {
            .Name = "DateAdded",
            .HeaderText = "Date Added",
            .DataPropertyName = "DateAdded",
            .Width = 120,
            .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "MM/dd/yyyy"},
            .ReadOnly = True,
            .SortMode = DataGridViewColumnSortMode.Automatic
        }
        DgvEpoxyCosts.Columns.Add(colDateAdded)

        ' Configure grid appearance
        DgvEpoxyCosts.AllowUserToAddRows = False
        DgvEpoxyCosts.AllowUserToDeleteRows = False
        DgvEpoxyCosts.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        DgvEpoxyCosts.MultiSelect = False
        DgvEpoxyCosts.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
    End Sub

    Private Sub DgvEpoxyCosts_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs)
        Try
            Dim columnName = DgvEpoxyCosts.Columns(e.ColumnIndex).DataPropertyName
            If String.IsNullOrEmpty(columnName) Then
                ErrorHandler.LogError(New Exception($"Column at index {e.ColumnIndex} has no DataPropertyName"), "DgvEpoxyCosts_ColumnHeaderMouseClick")
                Return
            End If

            ' Toggle sort order
            If _epoxySortColumn = columnName Then
                _epoxySortOrder = If(_epoxySortOrder = SortOrder.Ascending, SortOrder.Descending, SortOrder.Ascending)
            Else
                _epoxySortColumn = columnName
                _epoxySortOrder = SortOrder.Ascending
            End If

            ' Get current data as list
            Dim sortedList = _epoxyCosts.ToList()

            ' Sort based on column
            Select Case columnName
                Case "Brand"
                    sortedList = If(_epoxySortOrder = SortOrder.Ascending,
                        sortedList.OrderBy(Function(x) x.Brand).ToList(),
                        sortedList.OrderByDescending(Function(x) x.Brand).ToList())
                Case "Type"
                    sortedList = If(_epoxySortOrder = SortOrder.Ascending,
                        sortedList.OrderBy(Function(x) x.Type).ToList(),
                        sortedList.OrderByDescending(Function(x) x.Type).ToList())
                Case "CostPerGallon"
                    sortedList = If(_epoxySortOrder = SortOrder.Ascending,
                        sortedList.OrderBy(Function(x) x.CostPerGallon).ToList(),
                        sortedList.OrderByDescending(Function(x) x.CostPerGallon).ToList())
                Case "Active"
                    sortedList = If(_epoxySortOrder = SortOrder.Ascending,
                        sortedList.OrderBy(Function(x) x.Active).ToList(),
                        sortedList.OrderByDescending(Function(x) x.Active).ToList())
                Case "IsUserAdded"
                    sortedList = If(_epoxySortOrder = SortOrder.Ascending,
                        sortedList.OrderBy(Function(x) x.IsUserAdded).ToList(),
                        sortedList.OrderByDescending(Function(x) x.IsUserAdded).ToList())
                Case "DateAdded"
                    sortedList = If(_epoxySortOrder = SortOrder.Ascending,
                        sortedList.OrderBy(Function(x) x.DateAdded).ToList(),
                        sortedList.OrderByDescending(Function(x) x.DateAdded).ToList())
            End Select

            ' Update binding list
            _epoxyCosts = New BindingList(Of EpoxyCost)(sortedList)
            DgvEpoxyCosts.DataSource = _epoxyCosts

            ' Update column header to show sort direction
            For Each col As DataGridViewColumn In DgvEpoxyCosts.Columns
                col.HeaderCell.SortGlyphDirection = SortOrder.None
            Next
            DgvEpoxyCosts.Columns(e.ColumnIndex).HeaderCell.SortGlyphDirection = _epoxySortOrder
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DgvEpoxyCosts_ColumnHeaderMouseClick")
            MessageBox.Show($"Error sorting: {ex.Message}", "Sort Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End Try
    End Sub

    Private Sub BtnAddEpoxyCost_Click(sender As Object, e As EventArgs) Handles BtnAddEpoxyCost.Click
        Try
            ' Prompt for values
            Dim brand = InputBox("Enter brand name:", "Add Epoxy Cost", "")
            If String.IsNullOrWhiteSpace(brand) Then Return

            Dim type = InputBox("Enter type/product:", "Add Epoxy Cost", "")
            If String.IsNullOrWhiteSpace(type) Then Return

            Dim costStr = InputBox("Enter cost per gallon (e.g., 59.99):", "Add Epoxy Cost", "")
            If String.IsNullOrWhiteSpace(costStr) Then Return

            Dim cost As Double
            If Not Double.TryParse(costStr, cost) Then
                MessageBox.Show("Invalid cost value. Please enter a number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Create new epoxy cost
            Dim newCost As New EpoxyCost With {
                .Brand = brand.Trim(),
                .Type = type.Trim(),
                .CostPerGallon = cost,
                .DisplayText = $"{brand.Trim()} {type.Trim()} - ${cost:F2}/gal",
                .Active = True,
                .IsUserAdded = True
            }

            ' Save to database
            If DatabaseManager.Instance.AddEpoxyCost(newCost) Then
                MessageBox.Show("Epoxy cost added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                LoadEpoxyCosts() ' Reload grid
            Else
                MessageBox.Show("Failed to add epoxy cost. It may already exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnAddEpoxyCost_Click")
            MessageBox.Show($"Error adding epoxy cost: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnSaveEpoxyChanges_Click(sender As Object, e As EventArgs) Handles BtnSaveEpoxyChanges.Click
        Try
            Dim saveCount = 0
            Dim errorCount = 0

            For Each row As DataGridViewRow In DgvEpoxyCosts.Rows
                If row.IsNewRow Then Continue For

                ' Get epoxy cost from row
                Dim epoxyCost = DirectCast(_epoxyCosts(row.Index), EpoxyCost)

                ' Update from cells (in case user edited)
                epoxyCost.Brand = row.Cells("Brand").Value?.ToString()
                epoxyCost.Type = row.Cells("Type").Value?.ToString()
                epoxyCost.CostPerGallon = Convert.ToDouble(row.Cells("CostPerGallon").Value)
                epoxyCost.Active = Convert.ToBoolean(row.Cells("Active").Value)
                epoxyCost.DisplayText = $"{epoxyCost.Brand} {epoxyCost.Type} - ${epoxyCost.CostPerGallon:F2}/gal"

                ' Save to database
                If DatabaseManager.Instance.UpdateEpoxyCost(epoxyCost) Then
                    saveCount += 1
                Else
                    errorCount += 1
                End If
            Next

            MessageBox.Show($"Saved {saveCount} epoxy cost(s).{If(errorCount > 0, $" {errorCount} error(s).", "")}",
                          "Save Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)

            LoadEpoxyCosts() ' Reload to get fresh data
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnSaveEpoxyChanges_Click")
            MessageBox.Show($"Error saving changes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub BtnDeleteEpoxyCost_Click(sender As Object, e As EventArgs) Handles BtnDeleteEpoxyCost.Click
        Try
            If DgvEpoxyCosts.SelectedRows.Count = 0 Then
                MessageBox.Show("Please select an epoxy cost to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            Dim selectedRow = DgvEpoxyCosts.SelectedRows(0)
            Dim epoxyCost = DirectCast(_epoxyCosts(selectedRow.Index), EpoxyCost)

            Dim result = MessageBox.Show($"Delete '{epoxyCost.DisplayName}'?{vbCrLf}{vbCrLf}This will mark it as inactive (soft delete).",
                                       "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

            If result = DialogResult.Yes Then
                If DatabaseManager.Instance.DeleteEpoxyCost(epoxyCost.EpoxyCostID) Then
                    MessageBox.Show("Epoxy cost deleted (marked inactive).", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    LoadEpoxyCosts() ' Reload grid
                Else
                    MessageBox.Show("Failed to delete epoxy cost.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnDeleteEpoxyCost_Click")
            MessageBox.Show($"Error deleting epoxy cost: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

#End Region

    Private Sub BtnClose_Click(sender As Object, e As EventArgs) Handles BtnClose.Click
        Me.Close()
    End Sub

End Class
