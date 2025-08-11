Imports System.Windows.Forms
Imports System.ComponentModel

Public Class ProjectSelectionDialog
    Inherits Form

    Private WithEvents BtnOK As Button
    Private WithEvents BtnCancel As Button
    Private WithEvents ListBoxProjects As ListBox
    Private _lblTitle As Label
    Private ReadOnly _projectList As List(Of DoorProject)

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    Public Property SelectedProject As String

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    Public Property SelectedProjectGuid As Guid

    <DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)>
    <Browsable(False)>
    Public Property SelectedProjectInfo As DoorProject

    Public Sub New(title As String, projects As List(Of String))
        InitializeComponent()
        _lblTitle.Text = title

        ' Load project info with GUIDs
        _projectList = New List(Of DoorProject)()
        For Each projectName In projects
            Dim projectInfo As DoorProject = DoorProjectManager.GetDoorProjectInfo(projectName)
            If projectInfo IsNot Nothing Then
                _projectList.Add(projectInfo)
                ListBoxProjects.Items.Add($"{projectInfo.ProjectName} ({projectInfo.SavedDate:yyyy-MM-dd HH:mm})")
            Else
                ' Fallback for projects without GUID
                _projectList.Add(New DoorProject() With {
                    .ProjectId = Guid.NewGuid(),
                    .ProjectName = projectName,
                    .SavedDate = DateTime.MinValue
                })
                ListBoxProjects.Items.Add(projectName)
            End If
        Next

        If ListBoxProjects.Items.Count > 0 Then
            ListBoxProjects.SelectedIndex = 0
        End If
    End Sub

    Private Sub InitializeComponent()
        Me.Text = "Select Project"
        Me.Size = New Size(450, 350) ' Made slightly larger
        Me.StartPosition = FormStartPosition.CenterParent
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.ShowInTaskbar = False

        ' Title Label
        _lblTitle = New Label With {
            .Location = New Point(12, 15),
            .Size = New Size(410, 23),
            .Text = "Select a project:",
            .Font = New Font("Segoe UI", 9, FontStyle.Bold)
        }

        ' ListBox for projects
        ListBoxProjects = New ListBox With {
            .Location = New Point(12, 45),
            .Size = New Size(410, 220),
            .SelectionMode = SelectionMode.One
        }

        ' OK Button
        BtnOK = New Button With {
            .Text = "OK",
            .Location = New Point(267, 280),
            .Size = New Size(75, 23),
            .DialogResult = DialogResult.OK
        }

        ' Cancel Button
        BtnCancel = New Button With {
            .Text = "Cancel",
            .Location = New Point(347, 280),
            .Size = New Size(75, 23),
            .DialogResult = DialogResult.Cancel
        }

        ' Add controls
        Me.Controls.AddRange(_lblTitle, ListBoxProjects, BtnOK, BtnCancel)

        ' Set default buttons
        Me.AcceptButton = BtnOK
        Me.CancelButton = BtnCancel
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        SetSelectedProject()
    End Sub

    Private Sub ListBoxProjects_DoubleClick(sender As Object, e As EventArgs) Handles ListBoxProjects.DoubleClick
        SetSelectedProject()
        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub SetSelectedProject()
        Try
            If ListBoxProjects.SelectedIndex >= 0 AndAlso ListBoxProjects.SelectedIndex < _projectList.Count Then
                Dim selectedProjectInfo As DoorProject = _projectList(ListBoxProjects.SelectedIndex)
                SelectedProject = selectedProjectInfo.ProjectName
                SelectedProjectGuid = selectedProjectInfo.ProjectId
                'selectedProjectInfo = selectedProjectInfo
            End If
        Catch ex As Exception
            ' Fallback: try to get project name from display text
            If ListBoxProjects.SelectedItem IsNot Nothing Then
                Dim displayText As String = ListBoxProjects.SelectedItem.ToString()
                ' Extract project name (remove date part if exists)
                If displayText.Contains(" (") Then
                    SelectedProject = displayText.Substring(0, displayText.IndexOf(" ("))
                Else
                    SelectedProject = displayText
                End If
                SelectedProjectGuid = Guid.Empty ' Indicate no GUID available
            End If
        End Try
    End Sub

End Class