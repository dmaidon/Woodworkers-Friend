' ============================================================================
' Last Updated: January 30, 2026
' Changes: Phase 7.1 - Joinery reference UI implementation
' ============================================================================

Imports System.ComponentModel

Partial Public Class FrmMain

#Region "Joinery Reference (Phase 7.1)"

    Private _joineryData As BindingList(Of JoineryType)
    Private _allJoineryData As List(Of JoineryType)
    Private _currentJoinerySortColumn As String = ""
    Private _currentJoinerySortDirection As ListSortDirection = ListSortDirection.Ascending
    Private _joineryLoading As Boolean = False
    Private _joineryInitialized As Boolean = False

    ''' <summary>
    ''' Initializes joinery reference - called once to wire up events
    ''' </summary>
    Private Sub InitializeJoineryReference()
        Try
            If TcReferences Is Nothing Then
                ErrorHandler.LogError(New Exception("TcReferences is Nothing!"), "InitializeJoineryReference")
                Return
            End If

            ' Find TpJoineryReference (which is TabPage2)
            Dim joineryTab = TcReferences.TabPages.Cast(Of TabPage)().FirstOrDefault(Function(tp) tp.Name = "TpJoineryReference")

            If joineryTab IsNot Nothing Then
                ErrorHandler.LogError(New Exception("✅ TpJoineryReference found! Wiring up Enter event"), "InitializeJoineryReference")
                RemoveHandler joineryTab.Enter, AddressOf TpJoineryReference_Enter
                AddHandler joineryTab.Enter, AddressOf TpJoineryReference_Enter
            Else
                ErrorHandler.LogError(New Exception("❌ TpJoineryReference NOT found!"), "InitializeJoineryReference")
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeJoineryReference")
        End Try
    End Sub

    ''' <summary>
    ''' Loads joinery data from database
    ''' </summary>
    Private Sub LoadJoineryData()
        Try
            ' Set loading flag to prevent events during initialization
            _joineryLoading = True

            ' Load data from SQLite database
            ErrorHandler.LogError(New Exception("Loading joinery types from database..."), "LoadJoineryData")

            _allJoineryData = DatabaseManager.Instance.GetAllJoineryTypes()

            Dim loadedCount = If(_allJoineryData IsNot Nothing, _allJoineryData.Count, 0)
            ErrorHandler.LogError(New Exception($"Loaded {loadedCount} joinery types from database"), "InitializeJoineryReference")

            If _allJoineryData Is Nothing OrElse _allJoineryData.Count = 0 Then
                ErrorHandler.LogError(New Exception("No joinery types found in database"), "InitializeJoineryReference")
                _allJoineryData = New List(Of JoineryType)
            End If

            ' Create a COPY of the list for BindingList to avoid shared reference issues
            _joineryData = New BindingList(Of JoineryType)(New List(Of JoineryType)(_allJoineryData))

            ' Setup grid (assuming DgvJoineryTypes exists in TabPage2)
            InitializeJoineryGrid()

            ' Bind data
            If DgvJoineryTypes IsNot Nothing Then
                DgvJoineryTypes.DataSource = _joineryData
            End If

            ' Set default filter (all types)
            If RbJoineryAll IsNot Nothing Then
                RbJoineryAll.Checked = True
            End If

            ' Setup tooltips
            SetupJoineryTooltips()

            ' Clear details initially
            ClearJoineryDetails()

            ' Clear loading flag BEFORE applying filter
            _joineryLoading = False

            ' Force initial load of data
            ApplyJoineryFilter()
        Catch ex As Exception
            _joineryLoading = False
            ErrorHandler.LogError(ex, "LoadJoineryData")
        End Try
    End Sub

    ''' <summary>
    ''' Initializes joinery grid columns and settings
    ''' </summary>
    Private Sub InitializeJoineryGrid()
        Try
            If DgvJoineryTypes Is Nothing Then Return

            With DgvJoineryTypes
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

                ' Name column
                .Columns.Add(New DataGridViewTextBoxColumn With {
                    .Name = "Name",
                    .HeaderText = "Joinery Type",
                    .DataPropertyName = "Name",
                    .Width = 180,
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

                ' Difficulty column
                .Columns.Add(New DataGridViewTextBoxColumn With {
                    .Name = "DifficultyLevel",
                    .HeaderText = "Difficulty",
                    .DataPropertyName = "DifficultyLevel",
                    .Width = 100,
                    .SortMode = DataGridViewColumnSortMode.Programmatic
                })

                ' Strength Rating column
                .Columns.Add(New DataGridViewTextBoxColumn With {
                    .Name = "StrengthRating",
                    .HeaderText = "Strength",
                    .DataPropertyName = "StrengthRating",
                    .Width = 80,
                    .DefaultCellStyle = New DataGridViewCellStyle With {.Alignment = DataGridViewContentAlignment.MiddleCenter},
                    .SortMode = DataGridViewColumnSortMode.Programmatic
                })

                ' Glue Required column
                .Columns.Add(New DataGridViewCheckBoxColumn With {
                    .Name = "GlueRequired",
                    .HeaderText = "Glue",
                    .DataPropertyName = "GlueRequired",
                    .Width = 60,
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
            ErrorHandler.LogError(ex, "InitializeJoineryGrid")
        End Try
    End Sub

    ''' <summary>
    ''' Applies category/difficulty filter
    ''' </summary>
    Private Sub ApplyJoineryFilter()
        Try
            If _joineryLoading Then Return

            If _allJoineryData Is Nothing OrElse _allJoineryData.Count = 0 Then
                ErrorHandler.LogError(New Exception("_allJoineryData is null or empty!"), "ApplyJoineryFilter")
                ' Attempt reload
                _allJoineryData = DatabaseManager.Instance.GetAllJoineryTypes()
                If _allJoineryData Is Nothing OrElse _allJoineryData.Count = 0 Then
                    Return
                End If
            End If

            Dim filtered As List(Of JoineryType) = Nothing

            ' Determine which filter is active
            If RbJoineryAll IsNot Nothing AndAlso RbJoineryAll.Checked Then
                filtered = New List(Of JoineryType)(_allJoineryData)
            ElseIf RbJoineryFrame IsNot Nothing AndAlso RbJoineryFrame.Checked Then
                filtered = _allJoineryData.Where(Function(j) j.Category = JoineryCategory.Frame).ToList()
            ElseIf RbJoineryBox IsNot Nothing AndAlso RbJoineryBox.Checked Then
                filtered = _allJoineryData.Where(Function(j) j.Category = JoineryCategory.Box).ToList()
            ElseIf RbJoineryEdge IsNot Nothing AndAlso RbJoineryEdge.Checked Then
                filtered = _allJoineryData.Where(Function(j) j.Category = JoineryCategory.Edge).ToList()
            ElseIf RbJoineryBeginner IsNot Nothing AndAlso RbJoineryBeginner.Checked Then
                filtered = _allJoineryData.Where(Function(j) j.DifficultyLevel = JoineryDifficulty.Beginner).ToList()
            Else
                filtered = New List(Of JoineryType)(_allJoineryData)
            End If

            ' Update binding list with a COPY
            _joineryData = New BindingList(Of JoineryType)(filtered)

            If DgvJoineryTypes IsNot Nothing Then
                DgvJoineryTypes.DataSource = _joineryData
            End If

            ' Update count label
            If LblJoineryCount IsNot Nothing Then
                LblJoineryCount.Text = $"{_joineryData.Count} joinery type{If(_joineryData.Count <> 1, "s", "")}"
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "ApplyJoineryFilter")
        End Try
    End Sub

    ''' <summary>
    ''' Displays selected joinery details
    ''' </summary>
    Private Sub DisplayJoineryDetails(joinery As JoineryType)
        Try
            If joinery Is Nothing Then
                ClearJoineryDetails()
                Return
            End If

            ' Assuming detail labels exist in TabPage2
            If LblJoineryName IsNot Nothing Then LblJoineryName.Text = "Name: " & joinery.Name
            If LblJoineryCategory IsNot Nothing Then LblJoineryCategory.Text = "Category: " & joinery.Category
            If LblJoineryDifficulty IsNot Nothing Then
                LblJoineryDifficulty.Text = "Difficulty: " & joinery.DifficultyLevel
                LblJoineryDifficulty.ForeColor = joinery.DifficultyColor
            End If
            If LblJoineryStrength IsNot Nothing Then LblJoineryStrength.Text = $"Strength: {joinery.StrengthRating}/10 {joinery.StrengthStars}"
            If LblJoineryGlue IsNot Nothing Then LblJoineryGlue.Text = If(joinery.GlueRequired, "Glue Required: " & "Yes", "Glue Required: " & "No")

            If TxtJoineryDescription IsNot Nothing Then TxtJoineryDescription.Text = joinery.Description
            If TxtJoineryUses IsNot Nothing Then TxtJoineryUses.Text = joinery.TypicalUses
            If TxtJoineryTools IsNot Nothing Then TxtJoineryTools.Text = joinery.RequiredTools
            If TxtJoineryStrengthChar IsNot Nothing Then TxtJoineryStrengthChar.Text = joinery.StrengthCharacteristics
            If TxtJoineryReinforcement IsNot Nothing Then TxtJoineryReinforcement.Text = joinery.ReinforcementOptions
            If TxtJoineryHistory IsNot Nothing Then TxtJoineryHistory.Text = joinery.HistoricalNotes
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DisplayJoineryDetails")
        End Try
    End Sub

    ''' <summary>
    ''' Clears joinery details panel
    ''' </summary>
    Private Sub ClearJoineryDetails()
        Try
            If LblJoineryName IsNot Nothing Then LblJoineryName.Text = "Select a joinery type"
            If LblJoineryCategory IsNot Nothing Then LblJoineryCategory.Text = "-"
            If LblJoineryDifficulty IsNot Nothing Then
                LblJoineryDifficulty.Text = "-"
                LblJoineryDifficulty.ForeColor = SystemColors.ControlText
            End If
            If LblJoineryStrength IsNot Nothing Then LblJoineryStrength.Text = "-"
            If LblJoineryGlue IsNot Nothing Then LblJoineryGlue.Text = "-"

            If TxtJoineryDescription IsNot Nothing Then TxtJoineryDescription.Clear()
            If TxtJoineryUses IsNot Nothing Then TxtJoineryUses.Clear()
            If TxtJoineryTools IsNot Nothing Then TxtJoineryTools.Clear()
            If TxtJoineryStrengthChar IsNot Nothing Then TxtJoineryStrengthChar.Clear()
            If TxtJoineryReinforcement IsNot Nothing Then TxtJoineryReinforcement.Clear()
            If TxtJoineryHistory IsNot Nothing Then TxtJoineryHistory.Clear()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "ClearJoineryDetails")
        End Try
    End Sub

    ''' <summary>
    ''' Sets up tooltips for joinery controls
    ''' </summary>
    Private Sub SetupJoineryTooltips()
        Try
            Dim tt As New ToolTip()

            If RbJoineryAll IsNot Nothing Then tt.SetToolTip(RbJoineryAll, "Show all joinery types")
            If RbJoineryFrame IsNot Nothing Then tt.SetToolTip(RbJoineryFrame, "Frame joinery (legs, rails, stretchers)")
            If RbJoineryBox IsNot Nothing Then tt.SetToolTip(RbJoineryBox, "Box joinery (drawers, boxes, corners)")
            If RbJoineryEdge IsNot Nothing Then tt.SetToolTip(RbJoineryEdge, "Edge joinery (panels, miters)")
            If RbJoineryBeginner IsNot Nothing Then tt.SetToolTip(RbJoineryBeginner, "Easy joints for beginners")

            If DgvJoineryTypes IsNot Nothing Then tt.SetToolTip(DgvJoineryTypes, "Click to view detailed information")
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SetupJoineryTooltips")
        End Try
    End Sub

#End Region

#Region "Joinery Event Handlers"

    ''' <summary>
    ''' Handles tab page enter event - initializes data on first visit
    ''' </summary>
    Private Sub TpJoineryReference_Enter(sender As Object, e As EventArgs)
        Try
            ' Load data only once
            If Not _joineryInitialized Then
                _joineryInitialized = True
                LoadJoineryData()
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "TpJoineryReference_Enter")
        End Try
    End Sub

    ''' <summary>
    ''' Handles joinery grid selection changed
    ''' </summary>
    Private Sub DgvJoineryTypes_SelectionChanged(sender As Object, e As EventArgs) Handles DgvJoineryTypes.SelectionChanged
        Try
            If _joineryLoading Then Return

            If DgvJoineryTypes.SelectedRows.Count > 0 Then
                Dim selectedJoinery = TryCast(DgvJoineryTypes.SelectedRows(0).DataBoundItem, JoineryType)
                DisplayJoineryDetails(selectedJoinery)
            Else
                ClearJoineryDetails()
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DgvJoineryTypes_SelectionChanged")
        End Try
    End Sub

    ''' <summary>
    ''' Handles filter radio button changes
    ''' </summary>
    Private Sub RbJoineryAll_CheckedChanged(sender As Object, e As EventArgs) Handles RbJoineryAll.CheckedChanged
        If RbJoineryAll.Checked AndAlso Not _joineryLoading Then
            ApplyJoineryFilter()
        End If
    End Sub

    Private Sub RbJoineryFrame_CheckedChanged(sender As Object, e As EventArgs) Handles RbJoineryFrame.CheckedChanged
        If RbJoineryFrame.Checked AndAlso Not _joineryLoading Then
            ApplyJoineryFilter()
        End If
    End Sub

    Private Sub RbJoineryBox_CheckedChanged(sender As Object, e As EventArgs) Handles RbJoineryBox.CheckedChanged
        If RbJoineryBox.Checked AndAlso Not _joineryLoading Then
            ApplyJoineryFilter()
        End If
    End Sub

    Private Sub RbJoineryEdge_CheckedChanged(sender As Object, e As EventArgs) Handles RbJoineryEdge.CheckedChanged
        If RbJoineryEdge.Checked AndAlso Not _joineryLoading Then
            ApplyJoineryFilter()
        End If
    End Sub

    Private Sub RbJoineryBeginner_CheckedChanged(sender As Object, e As EventArgs) Handles RbJoineryBeginner.CheckedChanged
        If RbJoineryBeginner.Checked AndAlso Not _joineryLoading Then
            ApplyJoineryFilter()
        End If
    End Sub

    ''' <summary>
    ''' Handles column header click for sorting
    ''' </summary>
    Private Sub DgvJoineryTypes_ColumnHeaderMouseClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles DgvJoineryTypes.ColumnHeaderMouseClick
        Try
            Dim clickedColumn = DgvJoineryTypes.Columns(e.ColumnIndex)

            ' Toggle sort direction
            If _currentJoinerySortColumn = clickedColumn.Name Then
                _currentJoinerySortDirection = If(_currentJoinerySortDirection = ListSortDirection.Ascending,
                                                 ListSortDirection.Descending,
                                                 ListSortDirection.Ascending)
            Else
                _currentJoinerySortDirection = ListSortDirection.Ascending
            End If

            _currentJoinerySortColumn = clickedColumn.Name

            ' Sort the data
            Dim sortedList As List(Of JoineryType) = Nothing

            Select Case _currentJoinerySortColumn
                Case "Name"
                    sortedList = If(_currentJoinerySortDirection = ListSortDirection.Ascending,
                                   _joineryData.OrderBy(Function(j) j.Name).ToList(),
                                   _joineryData.OrderByDescending(Function(j) j.Name).ToList())
                Case "Category"
                    sortedList = If(_currentJoinerySortDirection = ListSortDirection.Ascending,
                                   _joineryData.OrderBy(Function(j) j.Category).ToList(),
                                   _joineryData.OrderByDescending(Function(j) j.Category).ToList())
                Case "DifficultyLevel"
                    sortedList = If(_currentJoinerySortDirection = ListSortDirection.Ascending,
                                   _joineryData.OrderBy(Function(j) j.DifficultyLevel).ToList(),
                                   _joineryData.OrderByDescending(Function(j) j.DifficultyLevel).ToList())
                Case "StrengthRating"
                    sortedList = If(_currentJoinerySortDirection = ListSortDirection.Ascending,
                                   _joineryData.OrderBy(Function(j) j.StrengthRating).ToList(),
                                   _joineryData.OrderByDescending(Function(j) j.StrengthRating).ToList())
                Case Else
                    Return
            End Select

            ' Update binding list
            _joineryData = New BindingList(Of JoineryType)(sortedList)
            DgvJoineryTypes.DataSource = _joineryData

            ' Update column header to show sort indicator
            For Each col As DataGridViewColumn In DgvJoineryTypes.Columns
                col.HeaderCell.SortGlyphDirection = SortOrder.None
            Next
            clickedColumn.HeaderCell.SortGlyphDirection = If(_currentJoinerySortDirection = ListSortDirection.Ascending,
                                                            SortOrder.Ascending,
                                                            SortOrder.Descending)
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DgvJoineryTypes_ColumnHeaderMouseClick")
        End Try
    End Sub

#End Region

End Class
