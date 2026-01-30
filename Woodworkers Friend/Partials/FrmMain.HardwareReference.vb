' ============================================================================
' Last Updated: January 30, 2026
' Changes: Phase 7.2 - Hardware standards reference UI implementation
' ============================================================================

Imports System.ComponentModel

Partial Public Class FrmMain

#Region "Hardware Reference (Phase 7.2)"

    Private _hardwareData As BindingList(Of HardwareStandard)
    Private _allHardwareData As List(Of HardwareStandard)
    Private _currentHardwareSortColumn As String = ""
    Private _currentHardwareSortDirection As ListSortDirection = ListSortDirection.Ascending
    Private _hardwareLoading As Boolean = False
    Private _hardwareInitialized As Boolean = False

    ''' <summary>
    ''' Initializes hardware reference - called once to wire up events
    ''' </summary>
    Private Sub InitializeHardwareReference()
        Try
            If TcReferences Is Nothing Then
                ErrorHandler.LogError(New Exception("TcReferences is Nothing!"), "InitializeHardwareReference")
                Return
            End If

            ' Find TpHardwareStandards (which is TabPage3)
            Dim hardwareTab = TcReferences.TabPages.Cast(Of TabPage)().FirstOrDefault(Function(tp) tp.Name = "TpHardwareStandards")

            If hardwareTab IsNot Nothing Then
                ErrorHandler.LogError(New Exception("✅ TpHardwareStandards found! Wiring up Enter event"), "InitializeHardwareReference")
                RemoveHandler hardwareTab.Enter, AddressOf TpHardwareStandards_Enter
                AddHandler hardwareTab.Enter, AddressOf TpHardwareStandards_Enter
            Else
                ErrorHandler.LogError(New Exception("❌ TpHardwareStandards NOT found!"), "InitializeHardwareReference")
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeHardwareReference")
        End Try
    End Sub

    ''' <summary>
    ''' Loads hardware data from database
    ''' </summary>
    Private Sub LoadHardwareData()
        Try
            ' Set loading flag to prevent events during initialization
            _hardwareLoading = True

            ' Load data from SQLite database
            ErrorHandler.LogError(New Exception("Loading hardware standards from database..."), "LoadHardwareData")

            _allHardwareData = DatabaseManager.Instance.GetAllHardwareStandards()

            Dim loadedCount = If(_allHardwareData IsNot Nothing, _allHardwareData.Count, 0)
            ErrorHandler.LogError(New Exception($"Loaded {loadedCount} hardware standards from database"), "InitializeHardwareReference")

            If _allHardwareData Is Nothing OrElse _allHardwareData.Count = 0 Then
                ErrorHandler.LogError(New Exception("No hardware standards found in database"), "InitializeHardwareReference")
                _allHardwareData = New List(Of HardwareStandard)
            End If

            ' Create a COPY of the list for BindingList to avoid shared reference issues
            _hardwareData = New BindingList(Of HardwareStandard)(New List(Of HardwareStandard)(_allHardwareData))

            ' Setup grid
            InitializeHardwareGrid()

            ' Bind data
            If DgvHardware IsNot Nothing Then
                DgvHardware.DataSource = _hardwareData
            End If

            ' Set default filter (all types)
            If RbHardwareAll IsNot Nothing Then
                RbHardwareAll.Checked = True
            End If

            ' Setup tooltips
            SetupHardwareTooltips()

            ' Clear details initially
            ClearHardwareDetails()

            ' Clear loading flag BEFORE applying filter
            _hardwareLoading = False

            ' Force initial load of data
            ApplyHardwareFilter()
        Catch ex As Exception
            _hardwareLoading = False
            ErrorHandler.LogError(ex, "LoadHardwareData")
        End Try
    End Sub

    ''' <summary>
    ''' Initializes hardware grid columns and settings
    ''' </summary>
    Private Sub InitializeHardwareGrid()
        Try
            If DgvHardware Is Nothing Then Return

            With DgvHardware
                .AutoGenerateColumns = False
                .AllowUserToAddRows = False
                .AllowUserToDeleteRows = False
                .ReadOnly = True
                .SelectionMode = DataGridViewSelectionMode.FullRowSelect
                .MultiSelect = False
                .RowHeadersVisible = False
                .AllowUserToResizeRows = False
                .EnableHeadersVisualStyles = False

                ' Clear existing columns
                .Columns.Clear()

                ' Type column
                .Columns.Add(New DataGridViewTextBoxColumn With {
                    .Name = "Type",
                    .HeaderText = "Hardware Type",
                    .DataPropertyName = "Type",
                    .Width = 200,
                    .SortMode = DataGridViewColumnSortMode.Programmatic
                })

                ' Category column
                .Columns.Add(New DataGridViewTextBoxColumn With {
                    .Name = "Category",
                    .HeaderText = "Category",
                    .DataPropertyName = "Category",
                    .Width = 120,
                    .SortMode = DataGridViewColumnSortMode.Programmatic
                })

                ' Brand column
                .Columns.Add(New DataGridViewTextBoxColumn With {
                    .Name = "Brand",
                    .HeaderText = "Brand",
                    .DataPropertyName = "Brand",
                    .Width = 100,
                    .SortMode = DataGridViewColumnSortMode.Programmatic
                })

                ' Dimensions column
                .Columns.Add(New DataGridViewTextBoxColumn With {
                    .Name = "Dimensions",
                    .HeaderText = "Dimensions",
                    .DataPropertyName = "Dimensions",
                    .Width = 150,
                    .SortMode = DataGridViewColumnSortMode.Programmatic
                })

                ' Description column (hidden, used for details)
                .Columns.Add(New DataGridViewTextBoxColumn With {
                    .Name = "Description",
                    .HeaderText = "Description",
                    .DataPropertyName = "Description",
                    .Visible = False
                })

                ' Style the header
                .ColumnHeadersDefaultCellStyle.Font = New Font(.Font, FontStyle.Bold)
                .ColumnHeadersDefaultCellStyle.BackColor = SystemColors.ControlLight
                .ColumnHeadersHeight = 30

                ' Alternating row colors
                .AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(240, 240, 255)
            End With
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeHardwareGrid")
        End Try
    End Sub

    ''' <summary>
    ''' Applies category filter
    ''' </summary>
    Private Sub ApplyHardwareFilter()
        Try
            If _hardwareLoading Then Return

            If _allHardwareData Is Nothing OrElse _allHardwareData.Count = 0 Then
                ErrorHandler.LogError(New Exception("_allHardwareData is null or empty!"), "ApplyHardwareFilter")
                ' Attempt reload
                _allHardwareData = DatabaseManager.Instance.GetAllHardwareStandards()
                If _allHardwareData Is Nothing OrElse _allHardwareData.Count = 0 Then
                    Return
                End If
            End If

            Dim filtered As List(Of HardwareStandard) = Nothing

            ' Determine which filter is active
            If RbHardwareAll IsNot Nothing AndAlso RbHardwareAll.Checked Then
                filtered = New List(Of HardwareStandard)(_allHardwareData)
            ElseIf RbHardwareHinges IsNot Nothing AndAlso RbHardwareHinges.Checked Then
                filtered = _allHardwareData.Where(Function(h) h.Category = HardwareCategory.Hinges).ToList()
            ElseIf RbHardwareSlides IsNot Nothing AndAlso RbHardwareSlides.Checked Then
                filtered = _allHardwareData.Where(Function(h) h.Category = HardwareCategory.Slides).ToList()
            ElseIf RbHardwareShelf IsNot Nothing AndAlso RbHardwareShelf.Checked Then
                filtered = _allHardwareData.Where(Function(h) h.Category = HardwareCategory.Shelf).ToList()
            ElseIf RbHardwareFasteners IsNot Nothing AndAlso RbHardwareFasteners.Checked Then
                filtered = _allHardwareData.Where(Function(h) h.Category = HardwareCategory.Fasteners).ToList()
            Else
                filtered = New List(Of HardwareStandard)(_allHardwareData)
            End If

            ' Update binding list with a COPY
            _hardwareData = New BindingList(Of HardwareStandard)(filtered)

            If DgvHardware IsNot Nothing Then
                DgvHardware.DataSource = _hardwareData
            End If

            ' Update count label
            If LblHardwareCount IsNot Nothing Then
                LblHardwareCount.Text = $"{_hardwareData.Count} hardware item{If(_hardwareData.Count <> 1, "s", "")}"
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "ApplyHardwareFilter")
        End Try
    End Sub

    ''' <summary>
    ''' Displays selected hardware details
    ''' </summary>
    Private Sub DisplayHardwareDetails(hardware As HardwareStandard)
        Try
            If hardware Is Nothing Then
                ClearHardwareDetails()
                Return
            End If

            ' Update summary labels
            If LblHardwareType IsNot Nothing Then LblHardwareType.Text = "Type: " & hardware.Type
            If LblHardwareCategory IsNot Nothing Then LblHardwareCategory.Text = "Category: " & hardware.Category
            If LblhardwareBrand IsNot Nothing Then LblhardwareBrand.Text = "Brand: " & If(String.IsNullOrEmpty(hardware.Brand), "Generic", hardware.Brand)
            If LblHardwareDimensions IsNot Nothing Then LblHardwareDimensions.Text = "Dimensions: " & If(String.IsNullOrEmpty(hardware.Dimensions), "N/A", hardware.Dimensions)
            If LblHardwareWeight IsNot Nothing Then LblHardwareWeight.Text = "Weight Capacity: " & If(String.IsNullOrEmpty(hardware.WeightCapacity), "N/A", hardware.WeightCapacity)

            ' Update detail textboxes
            If TxtHardwareDescription IsNot Nothing Then TxtHardwareDescription.Text = hardware.Description
            If TxtHardwareUses IsNot Nothing Then TxtHardwareUses.Text = hardware.TypicalUses
            If TxtHardwareMounting IsNot Nothing Then TxtHardwareMounting.Text = hardware.MountingRequirements
            If TxtHardwareInstallation IsNot Nothing Then TxtHardwareInstallation.Text = hardware.InstallationNotes
            If TxtHardwarePartNumber IsNot Nothing Then TxtHardwarePartNumber.Text = If(String.IsNullOrEmpty(hardware.PartNumber), "N/A", hardware.PartNumber)
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DisplayHardwareDetails")
        End Try
    End Sub

    ''' <summary>
    ''' Clears hardware details panel
    ''' </summary>
    Private Sub ClearHardwareDetails()
        Try
            If LblHardwareType IsNot Nothing Then LblHardwareType.Text = "Type: Select a hardware item"
            If LblHardwareCategory IsNot Nothing Then LblHardwareCategory.Text = "Category: -"
            If LblhardwareBrand IsNot Nothing Then LblhardwareBrand.Text = "Brand: -"
            If LblHardwareDimensions IsNot Nothing Then LblHardwareDimensions.Text = "Dimensions: -"
            If LblHardwareWeight IsNot Nothing Then LblHardwareWeight.Text = "Weight Capacity: -"

            If TxtHardwareDescription IsNot Nothing Then TxtHardwareDescription.Clear()
            If TxtHardwareUses IsNot Nothing Then TxtHardwareUses.Clear()
            If TxtHardwareMounting IsNot Nothing Then TxtHardwareMounting.Clear()
            If TxtHardwareInstallation IsNot Nothing Then TxtHardwareInstallation.Clear()
            If TxtHardwarePartNumber IsNot Nothing Then TxtHardwarePartNumber.Clear()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "ClearHardwareDetails")
        End Try
    End Sub

    ''' <summary>
    ''' Sets up tooltips for hardware controls
    ''' </summary>
    Private Sub SetupHardwareTooltips()
        Try
            Dim tt As New ToolTip()

            If RbHardwareAll IsNot Nothing Then tt.SetToolTip(RbHardwareAll, "Show all hardware types")
            If RbHardwareHinges IsNot Nothing Then tt.SetToolTip(RbHardwareHinges, "Cabinet and furniture hinges")
            If RbHardwareSlides IsNot Nothing Then tt.SetToolTip(RbHardwareSlides, "Drawer slides and runners")
            If RbHardwareShelf IsNot Nothing Then tt.SetToolTip(RbHardwareShelf, "Shelf support pins and brackets")
            If RbHardwareFasteners IsNot Nothing Then tt.SetToolTip(RbHardwareFasteners, "Screws, connectors, and fasteners")

            If DgvHardware IsNot Nothing Then tt.SetToolTip(DgvHardware, "Click to view detailed specifications")
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SetupHardwareTooltips")
        End Try
    End Sub

#End Region

#Region "Hardware Event Handlers"

    ''' <summary>
    ''' Handles tab page enter event - initializes data on first visit
    ''' </summary>
    Private Sub TpHardwareStandards_Enter(sender As Object, e As EventArgs)
        Try
            ' Load data only once
            If Not _hardwareInitialized Then
                _hardwareInitialized = True
                LoadHardwareData()
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "TpHardwareStandards_Enter")
        End Try
    End Sub

    ''' <summary>
    ''' Handles hardware grid selection changed
    ''' </summary>
    Private Sub DgvHardware_SelectionChanged(sender As Object, e As EventArgs) Handles DgvHardware.SelectionChanged
        Try
            If _hardwareLoading Then Return

            If DgvHardware.SelectedRows.Count > 0 Then
                Dim selectedHardware = TryCast(DgvHardware.SelectedRows(0).DataBoundItem, HardwareStandard)
                DisplayHardwareDetails(selectedHardware)
            Else
                ClearHardwareDetails()
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DgvHardware_SelectionChanged")
        End Try
    End Sub

    ''' <summary>
    ''' Handles filter radio button changes
    ''' </summary>
    Private Sub RbHardwareAll_CheckedChanged(sender As Object, e As EventArgs) Handles RbHardwareAll.CheckedChanged
        If RbHardwareAll.Checked AndAlso Not _hardwareLoading Then
            ApplyHardwareFilter()
        End If
    End Sub

    Private Sub RbHardwareHinges_CheckedChanged(sender As Object, e As EventArgs) Handles RbHardwareHinges.CheckedChanged
        If RbHardwareHinges.Checked AndAlso Not _hardwareLoading Then
            ApplyHardwareFilter()
        End If
    End Sub

    Private Sub RbHardwareSlides_CheckedChanged(sender As Object, e As EventArgs) Handles RbHardwareSlides.CheckedChanged
        If RbHardwareSlides.Checked AndAlso Not _hardwareLoading Then
            ApplyHardwareFilter()
        End If
    End Sub

    Private Sub RbHardwareShelf_CheckedChanged(sender As Object, e As EventArgs) Handles RbHardwareShelf.CheckedChanged
        If RbHardwareShelf.Checked AndAlso Not _hardwareLoading Then
            ApplyHardwareFilter()
        End If
    End Sub

    Private Sub RbHardwareFasteners_CheckedChanged(sender As Object, e As EventArgs) Handles RbHardwareFasteners.CheckedChanged
        If RbHardwareFasteners.Checked AndAlso Not _hardwareLoading Then
            ApplyHardwareFilter()
        End If
    End Sub

    ''' <summary>
    ''' Handles column header click for sorting
    ''' </summary>
    Private Sub DgvHardware_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgvHardware.ColumnHeaderMouseClick
        Try
            Dim clickedColumn = DgvHardware.Columns(e.ColumnIndex)

            ' Toggle sort direction
            If _currentHardwareSortColumn = clickedColumn.Name Then
                _currentHardwareSortDirection = If(_currentHardwareSortDirection = ListSortDirection.Ascending,
                                                 ListSortDirection.Descending,
                                                 ListSortDirection.Ascending)
            Else
                _currentHardwareSortDirection = ListSortDirection.Ascending
            End If

            _currentHardwareSortColumn = clickedColumn.Name

            ' Sort the data
            Dim sortedList As List(Of HardwareStandard) = Nothing

            Select Case _currentHardwareSortColumn
                Case "Type"
                    sortedList = If(_currentHardwareSortDirection = ListSortDirection.Ascending,
                                   _hardwareData.OrderBy(Function(h) h.Type).ToList(),
                                   _hardwareData.OrderByDescending(Function(h) h.Type).ToList())
                Case "Category"
                    sortedList = If(_currentHardwareSortDirection = ListSortDirection.Ascending,
                                   _hardwareData.OrderBy(Function(h) h.Category).ToList(),
                                   _hardwareData.OrderByDescending(Function(h) h.Category).ToList())
                Case "Brand"
                    sortedList = If(_currentHardwareSortDirection = ListSortDirection.Ascending,
                                   _hardwareData.OrderBy(Function(h) h.Brand).ToList(),
                                   _hardwareData.OrderByDescending(Function(h) h.Brand).ToList())
                Case "Dimensions"
                    sortedList = If(_currentHardwareSortDirection = ListSortDirection.Ascending,
                                   _hardwareData.OrderBy(Function(h) h.Dimensions).ToList(),
                                   _hardwareData.OrderByDescending(Function(h) h.Dimensions).ToList())
                Case Else
                    Return
            End Select

            ' Update binding list
            _hardwareData = New BindingList(Of HardwareStandard)(sortedList)
            DgvHardware.DataSource = _hardwareData

            ' Update column header to show sort indicator
            For Each col As DataGridViewColumn In DgvHardware.Columns
                col.HeaderCell.SortGlyphDirection = SortOrder.None
            Next
            clickedColumn.HeaderCell.SortGlyphDirection = If(_currentHardwareSortDirection = ListSortDirection.Ascending,
                                                            SortOrder.Ascending,
                                                            SortOrder.Descending)
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DgvHardware_ColumnHeaderMouseClick")
        End Try
    End Sub

#End Region

End Class
