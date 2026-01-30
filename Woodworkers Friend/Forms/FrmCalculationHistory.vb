Imports System.Windows.Forms

''' <summary>
''' Dialog for viewing and loading calculation history
''' </summary>
Public Class FrmCalculationHistory
    Inherits Form

    Private lvHistory As ListView
    Private txtSearch As TextBox
    Private chkFavoritesOnly As CheckBox
    Private btnLoad As Button
    Private btnDelete As Button
    Private btnToggleFavorite As Button
    Private btnClose As Button
    Private btnRename As Button
    Private lblCount As Label

    Private ReadOnly _calculatorType As String
    Private _allHistory As List(Of CalculationHistory)
    Private _selectedHistory As CalculationHistory

    ''' <summary>
    ''' Gets the selected calculation history item
    ''' </summary>
    Public ReadOnly Property SelectedHistory As CalculationHistory
        Get
            Return _selectedHistory
        End Get
    End Property

    Public Sub New(calculatorType As String)
        _calculatorType = calculatorType
        InitializeComponent()
        LoadHistory()
    End Sub

    Private Sub InitializeComponent()
        Me.Text = $"Calculation History - {_calculatorType}"
        Me.Size = New Size(900, 600)
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.Sizable
        Me.MinimumSize = New Size(700, 400)

        Dim tlp As New TableLayoutPanel With {
            .Dock = DockStyle.Fill,
            .ColumnCount = 1,
            .RowCount = 4,
            .Padding = New Padding(10)
        }

        ' Row styles
        tlp.RowStyles.Add(New RowStyle(SizeType.Absolute, 40)) ' Search row
        tlp.RowStyles.Add(New RowStyle(SizeType.Percent, 100)) ' List view
        tlp.RowStyles.Add(New RowStyle(SizeType.Absolute, 30)) ' Count label
        tlp.RowStyles.Add(New RowStyle(SizeType.Absolute, 50)) ' Buttons

        ' Search panel
        Dim searchPanel As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .FlowDirection = FlowDirection.LeftToRight,
            .AutoSize = True
        }

        searchPanel.Controls.Add(New Label With {.Text = "Search:", .AutoSize = True, .TextAlign = ContentAlignment.MiddleLeft, .Padding = New Padding(0, 8, 5, 0)})

        txtSearch = New TextBox With {.Width = 300}
        AddHandler txtSearch.TextChanged, AddressOf TxtSearch_TextChanged
        searchPanel.Controls.Add(txtSearch)

        chkFavoritesOnly = New CheckBox With {.Text = "Favorites Only", .AutoSize = True, .Padding = New Padding(10, 8, 0, 0)}
        AddHandler chkFavoritesOnly.CheckedChanged, AddressOf ChkFavoritesOnly_CheckedChanged
        searchPanel.Controls.Add(chkFavoritesOnly)

        tlp.Controls.Add(searchPanel, 0, 0)

        ' ListView
        lvHistory = New ListView With {
            .Dock = DockStyle.Fill,
            .View = View.Details,
            .FullRowSelect = True,
            .GridLines = True,
            .MultiSelect = False
        }

        lvHistory.Columns.Add("★", 30)
        lvHistory.Columns.Add("Name", 200)
        lvHistory.Columns.Add("Date", 140)
        lvHistory.Columns.Add("Inputs", 250)
        lvHistory.Columns.Add("Results", 220)

        AddHandler lvHistory.DoubleClick, AddressOf LvHistory_DoubleClick
        AddHandler lvHistory.SelectedIndexChanged, AddressOf LvHistory_SelectedIndexChanged

        tlp.Controls.Add(lvHistory, 0, 1)

        ' Count label
        lblCount = New Label With {
            .Dock = DockStyle.Fill,
            .TextAlign = ContentAlignment.MiddleLeft,
            .Text = "0 calculations"
        }
        tlp.Controls.Add(lblCount, 0, 2)

        ' Buttons panel
        Dim btnPanel As New FlowLayoutPanel With {
            .Dock = DockStyle.Fill,
            .FlowDirection = FlowDirection.RightToLeft,
            .AutoSize = True
        }

        btnClose = New Button With {.Text = "Close", .Width = 80, .DialogResult = DialogResult.Cancel}
        btnLoad = New Button With {.Text = "Load", .Width = 80, .DialogResult = DialogResult.OK, .Enabled = False}
        btnDelete = New Button With {.Text = "Delete", .Width = 80, .Enabled = False}
        btnToggleFavorite = New Button With {.Text = "★ Favorite", .Width = 100, .Enabled = False}
        btnRename = New Button With {.Text = "Rename...", .Width = 90, .Enabled = False}

        AddHandler btnLoad.Click, AddressOf BtnLoad_Click
        AddHandler btnDelete.Click, AddressOf BtnDelete_Click
        AddHandler btnToggleFavorite.Click, AddressOf BtnToggleFavorite_Click
        AddHandler btnRename.Click, AddressOf BtnRename_Click

        btnPanel.Controls.Add(btnClose)
        btnPanel.Controls.Add(btnLoad)
        btnPanel.Controls.Add(btnDelete)
        btnPanel.Controls.Add(btnRename)
        btnPanel.Controls.Add(btnToggleFavorite)

        tlp.Controls.Add(btnPanel, 0, 3)

        Me.Controls.Add(tlp)
        Me.AcceptButton = btnLoad
        Me.CancelButton = btnClose
    End Sub

    Private Sub LoadHistory()
        Try
            _allHistory = DatabaseManager.Instance.GetCalculationHistory(_calculatorType, 100)
            ApplyFilter()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "LoadHistory")
            MessageBox.Show($"Error loading history: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ApplyFilter()
        lvHistory.Items.Clear()

        Dim searchText = txtSearch.Text.Trim().ToLower()
        Dim filteredHistory = _allHistory.Where(Function(h)
                                                    If chkFavoritesOnly.Checked AndAlso Not h.IsFavorite Then Return False
                                                    If String.IsNullOrEmpty(searchText) Then Return True

                                                    Dim nameMatch = h.CalculationName IsNot Nothing AndAlso h.CalculationName.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)
                                                    Dim inputMatch = h.InputParameters IsNot Nothing AndAlso h.InputParameters.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)
                                                    Dim resultMatch = h.Results IsNot Nothing AndAlso h.Results.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)
                                                    Dim notesMatch = h.Notes IsNot Nothing AndAlso h.Notes.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)

                                                    Return nameMatch OrElse inputMatch OrElse resultMatch OrElse notesMatch
                                                End Function).ToList()

        For Each calc In filteredHistory
            Dim item As New ListViewItem(If(calc.IsFavorite, "★", ""))
            item.SubItems.Add(If(String.IsNullOrEmpty(calc.CalculationName), "(unnamed)", calc.CalculationName))
            item.SubItems.Add(calc.DateCalculated.ToString("yyyy-MM-dd HH:mm"))
            item.SubItems.Add(GetInputsSummary(calc))
            item.SubItems.Add(GetResultsSummary(calc))
            item.Tag = calc
            lvHistory.Items.Add(item)
        Next

        lblCount.Text = $"{filteredHistory.Count} calculation{If(filteredHistory.Count <> 1, "s", "")}"
    End Sub

    Private Shared Function GetInputsSummary(calc As CalculationHistory) As String
        Try
            ' Basic summary - just show first 40 chars
            Dim json = calc.InputParameters
            If json.Length > 40 Then
                Return String.Concat(json.AsSpan(0, 37), "...")
            Else
                Return json
            End If
        Catch
            Return "(error)"
        End Try
    End Function

    Private Shared Function GetResultsSummary(calc As CalculationHistory) As String
        Try
            ' Basic summary - just show first 35 chars
            Dim json = calc.Results
            Return If(json.Length > 35, String.Concat(json.AsSpan(0, 32), "..."), json)
        Catch
            Return "(error)"
        End Try
    End Function

    Private Sub TxtSearch_TextChanged(sender As Object, e As EventArgs)
        ApplyFilter()
    End Sub

    Private Sub ChkFavoritesOnly_CheckedChanged(sender As Object, e As EventArgs)
        ApplyFilter()
    End Sub

    Private Sub LvHistory_SelectedIndexChanged(sender As Object, e As EventArgs)
        Dim hasSelection = lvHistory.SelectedItems.Count > 0
        btnLoad.Enabled = hasSelection
        btnDelete.Enabled = hasSelection
        btnToggleFavorite.Enabled = hasSelection
        btnRename.Enabled = hasSelection
    End Sub

    Private Sub LvHistory_DoubleClick(sender As Object, e As EventArgs)
        If lvHistory.SelectedItems.Count > 0 Then
            BtnLoad_Click(sender, e)
        End If
    End Sub

    Private Sub BtnLoad_Click(sender As Object, e As EventArgs)
        If lvHistory.SelectedItems.Count > 0 Then
            _selectedHistory = CType(lvHistory.SelectedItems(0).Tag, CalculationHistory)
            Me.DialogResult = DialogResult.OK
            Me.Close()
        End If
    End Sub

    Private Sub BtnDelete_Click(sender As Object, e As EventArgs)
        If lvHistory.SelectedItems.Count = 0 Then Return

        Dim calc = CType(lvHistory.SelectedItems(0).Tag, CalculationHistory)
        Dim result = MessageBox.Show($"Delete calculation '{If(String.IsNullOrEmpty(calc.CalculationName), "(unnamed)", calc.CalculationName)}'?",
                                    "Confirm Delete",
                                    MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            If DatabaseManager.Instance.DeleteCalculation(calc.HistoryID) Then
                LoadHistory()
            Else
                MessageBox.Show("Failed to delete calculation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub BtnToggleFavorite_Click(sender As Object, e As EventArgs)
        If lvHistory.SelectedItems.Count = 0 Then Return

        Dim calc = CType(lvHistory.SelectedItems(0).Tag, CalculationHistory)
        If DatabaseManager.Instance.ToggleFavorite(calc.HistoryID) Then
            LoadHistory()
        End If
    End Sub

    Private Sub BtnRename_Click(sender As Object, e As EventArgs)
        If lvHistory.SelectedItems.Count = 0 Then Return

        Dim calc = CType(lvHistory.SelectedItems(0).Tag, CalculationHistory)
        Dim currentName = If(String.IsNullOrEmpty(calc.CalculationName), "", calc.CalculationName)

        Dim newName = InputBox("Enter new name for this calculation:", "Rename Calculation", currentName)
        If Not String.IsNullOrEmpty(newName) AndAlso newName <> currentName Then
            If DatabaseManager.Instance.UpdateCalculation(calc.HistoryID, newName, calc.Notes) Then
                LoadHistory()
            Else
                MessageBox.Show("Failed to rename calculation.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

End Class
