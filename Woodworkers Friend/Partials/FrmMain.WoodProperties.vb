' ============================================================================
' Last Updated: January 29, 2026
' Changes: Initial creation - Wood properties reference UI
' ============================================================================

Imports System.ComponentModel
Imports System.Text

Partial Public Class FrmMain

#Region "Wood Properties Reference"

    Private _woodPropertiesData As BindingList(Of WoodPropertiesData)
    Private _allWoodPropertiesData As List(Of WoodPropertiesData)
    Private _currentSortColumn As String = ""
    Private _currentSortDirection As ListSortDirection = ListSortDirection.Ascending
    Private _woodPropertiesLoading As Boolean = False

    ''' <summary>
    ''' Initializes wood properties reference
    ''' </summary>
    Private Sub InitializeWoodPropertiesReference()
        Try
            ' Set loading flag to prevent events from firing during initialization
            _woodPropertiesLoading = True

            ' Load data from SQLite database (Phase 2: Database migration)
            ErrorHandler.LogError(New Exception("Starting to load wood species from database..."), "InitializeWoodPropertiesReference")

            _allWoodPropertiesData = DatabaseManager.Instance.GetAllWoodSpecies()

            Dim loadedCount = If(_allWoodPropertiesData IsNot Nothing, _allWoodPropertiesData.Count, 0)
            ErrorHandler.LogError(New Exception($"Loaded {loadedCount} species from database"), "InitializeWoodPropertiesReference")

            If _allWoodPropertiesData Is Nothing OrElse _allWoodPropertiesData.Count = 0 Then
                ' Fallback to in-code database if SQLite fails
                ErrorHandler.LogError(New Exception("Database returned empty! Falling back to in-code database..."), "InitializeWoodPropertiesReference")
#Disable Warning BC40000
                _allWoodPropertiesData = WoodPropertiesDatabase.GetWoodSpeciesList()
#Enable Warning BC40000
                ErrorHandler.LogError(New Exception($"Loaded {_allWoodPropertiesData.Count} species from in-code fallback"), "InitializeWoodPropertiesReference")
            End If

            _woodPropertiesData = New BindingList(Of WoodPropertiesData)(New List(Of WoodPropertiesData)(_allWoodPropertiesData))

            ' Setup grid
            InitializeWoodPropertiesGrid()

            ' Bind data
            DgvWoodProperties.DataSource = _woodPropertiesData

            ' Set default filter (this will trigger CheckedChanged event, but flag will prevent processing)
            If RbWoodAll IsNot Nothing Then
                RbWoodAll.Checked = True
            End If

            ' Setup tooltips
            SetupWoodPropertiesTooltips()

            ' Wire up Add Species button event (if button exists)
            If BtnAddWoodSpecies IsNot Nothing Then
                AddHandler BtnAddWoodSpecies.Click, AddressOf BtnAddWoodSpecies_Click
            End If

            ' Clear details initially
            ClearWoodDetails()

            ' Clear loading flag BEFORE calling ApplyWoodFilter
            _woodPropertiesLoading = False

            ' Force initial load of data
            ApplyWoodFilter()
        Catch ex As Exception
            _woodPropertiesLoading = False ' Make sure flag is cleared on error
            ErrorHandler.LogError(ex, "InitializeWoodPropertiesReference")
        End Try
    End Sub

    ''' <summary>
    ''' Configures the DataGridView for wood properties
    ''' </summary>
    Private Sub InitializeWoodPropertiesGrid()
        Try
            With DgvWoodProperties
                .AutoGenerateColumns = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .ReadOnly = True
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .MultiSelect = False
                .RowHeadersVisible = False
                .AllowUserToResizeRows = False
                .AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
                .BorderStyle = BorderStyle.Fixed3D
                .BackgroundColor = SystemColors.Window
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 240)
                .AllowUserToOrderColumns = False
                .ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing

                ' Clear existing columns
                .Columns.Clear()

                ' Species column (wider)
                .Columns.Add(New DataGridViewTextBoxColumn With {
                    .Name = "Species",
                    .HeaderText = "Species",
                    .DataPropertyName = "CommonName",
                    .FillWeight = 30
                })

                ' Janka Hardness
                .Columns.Add(New DataGridViewTextBoxColumn With {
                    .Name = "Janka",
                    .HeaderText = "Janka",
                    .DataPropertyName = "JankaHardness",
                    .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "N0", .Alignment = DataGridViewContentAlignment.MiddleRight},
                    .FillWeight = 12
                })

                ' Specific Gravity
                .Columns.Add(New DataGridViewTextBoxColumn With {
                    .Name = "SpecGrav",
                    .HeaderText = "Sp.Gr",
                    .DataPropertyName = "SpecificGravity",
                    .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "0.00", .Alignment = DataGridViewContentAlignment.MiddleRight},
                    .FillWeight = 10
                })

                ' Moisture %
                .Columns.Add(New DataGridViewTextBoxColumn With {
                    .Name = "Moisture",
                    .HeaderText = "Moist%",
                    .DataPropertyName = "MoistureContent",
                    .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "0%", .Alignment = DataGridViewContentAlignment.MiddleRight},
                    .FillWeight = 10
                })

                ' Density
                .Columns.Add(New DataGridViewTextBoxColumn With {
                    .Name = "Density",
                    .HeaderText = "Density",
                    .DataPropertyName = "Density",
                    .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "N0", .Alignment = DataGridViewContentAlignment.MiddleRight},
                    .FillWeight = 12
                })

                ' Shrinkage Radial
                .Columns.Add(New DataGridViewTextBoxColumn With {
                    .Name = "ShrinkR",
                    .HeaderText = "Shrink R",
                    .DataPropertyName = "ShrinkageRadial",
                    .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "0.0%", .Alignment = DataGridViewContentAlignment.MiddleRight},
                    .FillWeight = 13
                })

                ' Shrinkage Tangential
                .Columns.Add(New DataGridViewTextBoxColumn With {
                    .Name = "ShrinkT",
                    .HeaderText = "Shrink T",
                    .DataPropertyName = "ShrinkageTangential",
                    .DefaultCellStyle = New DataGridViewCellStyle With {.Format = "0.0%", .Alignment = DataGridViewContentAlignment.MiddleRight},
                    .FillWeight = 13
                })

                ' Hidden column for wood type (for filtering)
                .Columns.Add(New DataGridViewTextBoxColumn With {
                    .Name = "WoodType",
                    .DataPropertyName = "WoodType",
                    .Visible = False
                })
            End With
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeWoodPropertiesGrid")
        End Try
    End Sub

    ''' <summary>
    ''' Sets up tooltips for wood properties controls
    ''' </summary>
    Private Sub SetupWoodPropertiesTooltips()
        Try
            Dim tooltip As New ToolTip With {
                .AutoPopDelay = 10000,
                .InitialDelay = 500,
                .ReshowDelay = 200,
                .ShowAlways = True
            }

            If RbWoodAll IsNot Nothing Then
                tooltip.SetToolTip(RbWoodAll, "Show all wood species (hardwoods and softwoods)")
            End If

            If RbWoodHardwoods IsNot Nothing Then
                tooltip.SetToolTip(RbWoodHardwoods,
                    "Show only hardwoods (deciduous trees - Oak, Maple, Cherry, etc.)" & vbCrLf &
                    "Generally denser and more durable than softwoods.")
            End If

            If RbWoodSoftwoods IsNot Nothing Then
                tooltip.SetToolTip(RbWoodSoftwoods,
                    "Show only softwoods (coniferous trees - Pine, Cedar, Fir, etc.)" & vbCrLf &
                    "Generally easier to work with, but less dense than hardwoods.")
            End If

            If TxtWoodSearch IsNot Nothing Then
                tooltip.SetToolTip(TxtWoodSearch,
                    "Type to search for wood species by name" & vbCrLf &
                    "Search is case-insensitive and filters as you type.")
            End If

            If DgvWoodProperties IsNot Nothing Then
                tooltip.SetToolTip(DgvWoodProperties,
                    "Wood Properties Reference Table" & vbCrLf &
                    "Click any row to view detailed information below." & vbCrLf &
                    "Click column headers to sort by that property." & vbCrLf & vbCrLf &
                    "Janka: Hardness rating in lbf (higher = harder)" & vbCrLf &
                    "Sp.Gr: Specific gravity (density relative to water)" & vbCrLf &
                    "Moist%: Typical moisture content at equilibrium" & vbCrLf &
                    "Density: Weight in pounds per cubic foot" & vbCrLf &
                    "Shrink R/T: Dimensional change with moisture (% of dimension)")
            End If

            If BtnCompareWoods IsNot Nothing Then
                tooltip.SetToolTip(BtnCompareWoods,
                    "Compare multiple wood species side-by-side" & vbCrLf &
                    "(Feature coming soon)")
            End If

            If BtnExportWoodData IsNot Nothing Then
                tooltip.SetToolTip(BtnExportWoodData,
                    "Export the current wood properties data to CSV file" & vbCrLf &
                    "Can be opened in Excel or other spreadsheet programs.")
            End If

            If BtnPrintWoodData IsNot Nothing Then
                tooltip.SetToolTip(BtnPrintWoodData,
                    "Print the wood properties reference table" & vbCrLf &
                    "(Feature coming soon)")
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SetupWoodPropertiesTooltips")
        End Try
    End Sub

    ''' <summary>
    ''' Applies filter to wood species list
    ''' </summary>
    Private Sub ApplyWoodFilter()
        Try
            Dim searchText = If(TxtWoodSearch?.Text?.Trim().ToLower(), "")
            Dim filterType = ""

            If RbWoodHardwoods IsNot Nothing AndAlso RbWoodHardwoods.Checked Then
                filterType = "Hardwood"
            ElseIf RbWoodSoftwoods IsNot Nothing AndAlso RbWoodSoftwoods.Checked Then
                filterType = "Softwood"
            End If

            ' Check if data is loaded - reload if needed
            If _allWoodPropertiesData Is Nothing OrElse _allWoodPropertiesData.Count = 0 Then
#Disable Warning BC40000
                _allWoodPropertiesData = WoodPropertiesDatabase.GetWoodSpeciesList()
#Enable Warning BC40000
                If _allWoodPropertiesData Is Nothing OrElse _allWoodPropertiesData.Count = 0 Then
                    MessageBox.Show("Wood properties data is not loaded. Please restart the application.", "Data Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Return
                End If
            End If

            ' Filter the list
            Dim filteredList = _allWoodPropertiesData.Where(Function(w)
                                                                Dim matchesSearch = String.IsNullOrEmpty(searchText) OrElse
                                                                           w.CommonName.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)
                                                                Dim matchesType = String.IsNullOrEmpty(filterType) OrElse
                                                                         w.WoodType = filterType
                                                                Return matchesSearch AndAlso matchesType
                                                            End Function).ToList()

            ' Apply current sort if any
            If Not String.IsNullOrEmpty(_currentSortColumn) Then
                filteredList = ApplySortToList(filteredList, _currentSortColumn, _currentSortDirection)
            End If

            ' Update binding list
            _woodPropertiesData.Clear()
            For Each wood In filteredList
                _woodPropertiesData.Add(wood)
            Next

            ' Clear selection if no results
            If _woodPropertiesData.Count = 0 Then
                ClearWoodDetails()
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "ApplyWoodFilter")
            MessageBox.Show($"Error filtering wood properties: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Applies sorting to a list based on property name and direction
    ''' </summary>
    Private Function ApplySortToList(list As List(Of WoodPropertiesData), propertyName As String, sortDirection As ListSortDirection) As List(Of WoodPropertiesData)
        Select Case propertyName
            Case "CommonName"
                Return If(sortDirection = ListSortDirection.Ascending,
                         list.OrderBy(Function(w) w.CommonName).ToList(),
                         list.OrderByDescending(Function(w) w.CommonName).ToList())
            Case "JankaHardness"
                Return If(sortDirection = ListSortDirection.Ascending,
                         list.OrderBy(Function(w) w.JankaHardness).ToList(),
                         list.OrderByDescending(Function(w) w.JankaHardness).ToList())
            Case "SpecificGravity"
                Return If(sortDirection = ListSortDirection.Ascending,
                         list.OrderBy(Function(w) w.SpecificGravity).ToList(),
                         list.OrderByDescending(Function(w) w.SpecificGravity).ToList())
            Case "MoistureContent"
                Return If(sortDirection = ListSortDirection.Ascending,
                         list.OrderBy(Function(w) w.MoistureContent).ToList(),
                         list.OrderByDescending(Function(w) w.MoistureContent).ToList())
            Case "Density"
                Return If(sortDirection = ListSortDirection.Ascending,
                         list.OrderBy(Function(w) w.Density).ToList(),
                         list.OrderByDescending(Function(w) w.Density).ToList())
            Case "ShrinkageRadial"
                Return If(sortDirection = ListSortDirection.Ascending,
                         list.OrderBy(Function(w) w.ShrinkageRadial).ToList(),
                         list.OrderByDescending(Function(w) w.ShrinkageRadial).ToList())
            Case "ShrinkageTangential"
                Return If(sortDirection = ListSortDirection.Ascending,
                         list.OrderBy(Function(w) w.ShrinkageTangential).ToList(),
                         list.OrderByDescending(Function(w) w.ShrinkageTangential).ToList())
            Case Else
                Return list ' No sort
        End Select
    End Function

    ''' <summary>
    ''' Displays details for selected wood species
    ''' </summary>
    Private Sub DisplayWoodDetails(wood As WoodPropertiesData)
        Try
            If wood Is Nothing Then
                ClearWoodDetails()
                Return
            End If

            ' Update header
            If LblWoodDetailsHeader IsNot Nothing Then
                LblWoodDetailsHeader.Text = $"Details for: {wood.CommonName}"
            End If

            ' Build details text
            Dim details As New StringBuilder()

            ' Typical Uses section
            details.AppendLine("═══ TYPICAL USES ═══")
            details.AppendLine(wood.TypicalUses)
            details.AppendLine()

            ' Workability section
            details.AppendLine("═══ WORKABILITY ═══")
            details.AppendLine(wood.Workability)
            details.AppendLine()

            ' Cautions section
            details.AppendLine("═══ CAUTIONS & NOTES ═══")
            details.AppendLine(wood.Cautions)

            ' Add scientific name if available
            If Not String.IsNullOrEmpty(wood.ScientificName) Then
                details.AppendLine()
                details.AppendLine($"Scientific Name: {wood.ScientificName}")
            End If

            If RtbWoodDetails IsNot Nothing Then
                RtbWoodDetails.Text = details.ToString()
                RtbWoodDetails.SelectionStart = 0
                RtbWoodDetails.ScrollToCaret()
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DisplayWoodDetails")
        End Try
    End Sub

    ''' <summary>
    ''' Clears wood details display
    ''' </summary>
    Private Sub ClearWoodDetails()
        Try
            If LblWoodDetailsHeader IsNot Nothing Then
                LblWoodDetailsHeader.Text = "Details for: (Select a wood species)"
            End If

            If RtbWoodDetails IsNot Nothing Then
                RtbWoodDetails.Text = "Select a wood species from the table above to view detailed information about typical uses, workability, and cautions."
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "ClearWoodDetails")
        End Try
    End Sub

    ''' <summary>
    ''' Exports wood properties to CSV
    ''' </summary>
    Private Sub ExportWoodPropertiesToCSV()
        Try
            Using sfd As New SaveFileDialog With {
                .Filter = "CSV files (*.csv)|*.csv|All files (*.*)|*.*",
                .DefaultExt = "csv",
                .FileName = "WoodProperties.csv",
                .Title = "Export Wood Properties to CSV"
            }
                If sfd.ShowDialog() = DialogResult.OK Then
                    Using writer As New IO.StreamWriter(sfd.FileName)
                        ' Write header
                        writer.WriteLine("Species,Janka Hardness (lbf),Specific Gravity,Moisture Content (%),Density (lb/ft³),Shrinkage Radial (%),Shrinkage Tangential (%),Wood Type,Typical Uses,Workability,Cautions")

                        ' Write data
                        For Each wood In _woodPropertiesData
                            writer.WriteLine($"""{wood.CommonName}"",""{wood.JankaHardness}"",""{wood.SpecificGravity:0.00}"",""{wood.MoistureContent * 100:0}"",""{wood.Density}"",""{wood.ShrinkageRadial * 100:0.0}"",""{wood.ShrinkageTangential * 100:0.0}"",""{wood.WoodType}"",""{wood.TypicalUses.Replace("""", """""")}"",""{wood.Workability.Replace("""", """""")}"",""{wood.Cautions.Replace("""", """""")}""")
                        Next
                    End Using

                    MessageBox.Show($"Wood properties exported successfully to:{vbCrLf}{sfd.FileName}",
                                  "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "ExportWoodPropertiesToCSV", showToUser:=True)
        End Try
    End Sub

#End Region

#Region "Wood Properties Event Handlers"

    ''' <summary>
    ''' Handles wood filter change (All)
    ''' </summary>
    Private Sub RbWoodAll_CheckedChanged(sender As Object, e As EventArgs) Handles RbWoodAll.CheckedChanged
        If RbWoodAll.Checked AndAlso Not _woodPropertiesLoading Then
            ApplyWoodFilter()
        End If
    End Sub

    ''' <summary>
    ''' Handles wood filter change (Hardwoods)
    ''' </summary>
    Private Sub RbWoodHardwoods_CheckedChanged(sender As Object, e As EventArgs) Handles RbWoodHardwoods.CheckedChanged
        If RbWoodHardwoods.Checked AndAlso Not _woodPropertiesLoading Then
            ApplyWoodFilter()
        End If
    End Sub

    ''' <summary>
    ''' Handles wood filter change (Softwoods)
    ''' </summary>
    Private Sub RbWoodSoftwoods_CheckedChanged(sender As Object, e As EventArgs) Handles RbWoodSoftwoods.CheckedChanged
        If RbWoodSoftwoods.Checked AndAlso Not _woodPropertiesLoading Then
            ApplyWoodFilter()
        End If
    End Sub

    ''' <summary>
    ''' Handles search text change
    ''' </summary>
    Private Sub TxtWoodSearch_TextChanged(sender As Object, e As EventArgs) Handles TxtWoodSearch.TextChanged
        If Not _loading Then
            ApplyWoodFilter()
        End If
    End Sub

    ''' <summary>
    ''' Handles clear search button click
    ''' </summary>
    Private Sub BtnWoodClearSearch_Click(sender As Object, e As EventArgs) Handles BtnWoodClearSearch.Click
        If TxtWoodSearch IsNot Nothing Then
            TxtWoodSearch.Clear()
            TxtWoodSearch.Focus()
        End If
    End Sub

    ''' <summary>
    ''' Handles grid selection change
    ''' </summary>
    Private Sub DgvWoodProperties_SelectionChanged(sender As Object, e As EventArgs) Handles DgvWoodProperties.SelectionChanged
        Try
            If DgvWoodProperties.SelectedRows.Count = 0 Then
                ClearWoodDetails()
                Return
            End If

            Dim selectedRow = DgvWoodProperties.SelectedRows(0)
            Dim wood = TryCast(selectedRow.DataBoundItem, WoodPropertiesData)
            DisplayWoodDetails(wood)
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DgvWoodProperties_SelectionChanged")
        End Try
    End Sub

    ''' <summary>
    ''' Handles compare woods button click
    ''' </summary>
    Private Sub BtnCompareWoods_Click(sender As Object, e As EventArgs) Handles BtnCompareWoods.Click
        MessageBox.Show("Wood species comparison feature coming soon!", "Feature In Development",
                      MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' Handles export button click
    ''' </summary>
    Private Sub BtnExportWoodData_Click(sender As Object, e As EventArgs) Handles BtnExportWoodData.Click
        ExportWoodPropertiesToCSV()
    End Sub

    ''' <summary>
    ''' Handles print button click
    ''' </summary>
    Private Sub BtnPrintWoodData_Click(sender As Object, e As EventArgs) Handles BtnPrintWoodData.Click
        MessageBox.Show("Print feature coming soon!", "Feature In Development",
                      MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    ''' <summary>
    ''' Handles add species button click
    ''' </summary>
    Private Sub BtnAddWoodSpecies_Click(sender As Object, e As EventArgs)
        Try
            Using dlg As New FrmAddWoodSpecies()
                If dlg.ShowDialog(Me) = DialogResult.OK Then
                    ' Add species to database
                    If DatabaseManager.Instance.AddWoodSpecies(dlg.WoodSpeciesData) Then
                        MessageBox.Show($"Successfully added '{dlg.WoodSpeciesData.CommonName}' to the database!",
                                      "Species Added",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Information)

                        ' Reload data from database
                        _allWoodPropertiesData = DatabaseManager.Instance.GetAllWoodSpecies()
                        If _allWoodPropertiesData Is Nothing OrElse _allWoodPropertiesData.Count = 0 Then
#Disable Warning BC40000
                            _allWoodPropertiesData = WoodPropertiesDatabase.GetWoodSpeciesList()
#Enable Warning BC40000
                        End If

                        ' Re-apply current filter to refresh grid
                        ApplyWoodFilter()

                        ' Try to select the newly added species
                        For i = 0 To DgvWoodProperties.Rows.Count - 1
                            If DgvWoodProperties.Rows(i).Cells("CommonName").Value?.ToString() = dlg.WoodSpeciesData.CommonName Then
                                DgvWoodProperties.ClearSelection()
                                DgvWoodProperties.Rows(i).Selected = True
                                DgvWoodProperties.FirstDisplayedScrollingRowIndex = i
                                Exit For
                            End If
                        Next
                    Else
                        MessageBox.Show($"Failed to add '{dlg.WoodSpeciesData.CommonName}' to the database. The species may already exist.",
                                      "Error",
                                      MessageBoxButtons.OK,
                                      MessageBoxIcon.Error)
                    End If
                End If
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnAddWoodSpecies_Click")
            MessageBox.Show($"An error occurred while adding the wood species: {ex.Message}",
                          "Error",
                          MessageBoxButtons.OK,
                          MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Handles column header click for sorting
    ''' </summary>
    Private Sub DgvWoodProperties_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgvWoodProperties.ColumnHeaderMouseClick
        Try
            If DgvWoodProperties.Columns.Count = 0 OrElse _woodPropertiesData Is Nothing Then Return

            Dim column = DgvWoodProperties.Columns(e.ColumnIndex)
            Dim propertyName = column.DataPropertyName

            ' Don't sort the hidden WoodType column
            If String.IsNullOrEmpty(propertyName) OrElse propertyName = "WoodType" Then Return

            ' Determine sort direction
            Dim newSortOrder As ListSortDirection
            If _currentSortColumn = propertyName Then
                ' Same column clicked - toggle direction
                If _currentSortDirection = ListSortDirection.Ascending Then
                    newSortOrder = ListSortDirection.Descending
                Else
                    newSortOrder = ListSortDirection.Ascending
                End If
            Else
                ' New column clicked - start with ascending
                newSortOrder = ListSortDirection.Ascending
            End If

            ' Update tracking variables
            _currentSortColumn = propertyName
            _currentSortDirection = newSortOrder

            ' Update visual sort indicators
            For Each col As DataGridViewColumn In DgvWoodProperties.Columns
                If col.Index = e.ColumnIndex Then
                    col.HeaderCell.SortGlyphDirection = If(newSortOrder = ListSortDirection.Ascending, SortOrder.Ascending, SortOrder.Descending)
                Else
                    col.HeaderCell.SortGlyphDirection = SortOrder.None
                End If
            Next

            ' Get the current filtered list and sort it
            Dim currentList = _woodPropertiesData.ToList()
            Dim sortedList = ApplySortToList(currentList, propertyName, newSortOrder)

            ' Update the binding list with sorted data
            _woodPropertiesData.Clear()
            For Each wood In sortedList
                _woodPropertiesData.Add(wood)
            Next
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DgvWoodProperties_ColumnHeaderMouseClick")
        End Try
    End Sub

#End Region

End Class
