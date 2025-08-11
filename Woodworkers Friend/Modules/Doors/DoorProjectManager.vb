Imports System.IO
Imports System.Text.Json

Public Class DoorProjectManager

    Private Shared ReadOnly _projectsFolder As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Woodworkers Friend", "Door Projects")
    Private Shared ReadOnly _jsonOptions As New JsonSerializerOptions() With {.WriteIndented = True}

    ''' <summary>
    ''' Saves a door project to disk
    ''' </summary>
    Public Shared Function SaveDoorProject(form As FrmMain, projectName As String) As Boolean
        Try
            EnsureProjectsFolderExists()

            Dim project As New DoorProject() With {
                .ProjectName = projectName,
                .CabinetOpeningHeight = ValidationManager.GetSafeDoubleFromControl(form.TxtCabinetOpeningHeight, 24.0),
                .CabinetOpeningWidth = ValidationManager.GetSafeDoubleFromControl(form.TxtCabinetOpeningWidth, 30.0),
                .StileWidth = ValidationManager.GetSafeDoubleFromControl(form.TxtStileWidth, 2.0),
                .RailWidth = ValidationManager.GetSafeDoubleFromControl(form.TxtRailWidth, 2.0),
                .IsTwoDoor = form.Rb2Door IsNot Nothing AndAlso form.Rb2Door.Checked,
                .GapSize = ValidationManager.GetSafeDoubleFromControl(form.TxtGapSize, 0.125),
                .DoorOverlay = ValidationManager.GetSafeDoubleFromControl(form.TxtDoorOverlay, 0.5),
                .IsOverlay = form.RbOverlay IsNot Nothing AndAlso form.RbOverlay.Checked,
                .PanelGrooveDepth = ValidationManager.GetSafeDoubleFromControl(form.TxtPanelGrooveDepth, 0.25),
                .PanelExpansionGap = ValidationManager.GetSafeDoubleFromControl(form.TxtPanelExpansionGap, 0.0625),
                .Scale = If(form.RbImperial IsNot Nothing AndAlso form.RbImperial.Checked, MeasurementScale.Imperial, MeasurementScale.Metric),
                .SavedDate = DateTime.Now,
                .Notes = ControlUtility.GetControlText(form.TxtDoorProjectName)
            }

            Dim fileName As String = $"{SanitizeFileName(projectName)}.json"
            Dim filePath As String = Path.Combine(_projectsFolder, fileName)

            Dim jsonString As String = JsonSerializer.Serialize(project, _jsonOptions)
            File.WriteAllText(filePath, jsonString)
            Return True
        Catch ex As Exception
            MessageBox.Show($"Error saving door project: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Loads a door project from disk
    ''' </summary>
    Public Shared Function LoadDoorProject(form As FrmMain, projectName As String) As Boolean
        Try
            Dim fileName As String = $"{SanitizeFileName(projectName)}.json"
            Dim filePath As String = Path.Combine(_projectsFolder, fileName)

            If Not File.Exists(filePath) Then
                MessageBox.Show("Door project file not found.", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            Dim jsonString As String = File.ReadAllText(filePath)
            Dim project As DoorProject = JsonSerializer.Deserialize(Of DoorProject)(jsonString)

            ApplyProjectToForm(form, project)
            Return True
        Catch ex As Exception
            MessageBox.Show($"Error loading door project: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Gets a list of saved door project names
    ''' </summary>
    Public Shared Function GetSavedDoorProjects() As List(Of String)
        Try
            EnsureProjectsFolderExists()
            Dim projectNames As New List(Of String)

            For Each filePath In Directory.GetFiles(_projectsFolder, "*.json")
                Dim fileName As String = Path.GetFileNameWithoutExtension(filePath)
                projectNames.Add(fileName)
            Next

            Return projectNames
        Catch
            Return New List(Of String)
        End Try
    End Function

    ''' <summary>
    ''' Deletes a saved door project
    ''' </summary>
    Public Shared Function DeleteDoorProject(projectName As String) As Boolean
        Try
            Dim fileName As String = $"{SanitizeFileName(projectName)}.json"
            Dim filePath As String = Path.Combine(_projectsFolder, fileName)

            If File.Exists(filePath) Then
                File.Delete(filePath)
                Return True
            End If

            Return False
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Gets project information without loading it
    ''' </summary>
    Public Shared Function GetDoorProjectInfo(projectName As String) As DoorProject
        Try
            Dim fileName As String = $"{SanitizeFileName(projectName)}.json"
            Dim filePath As String = Path.Combine(_projectsFolder, fileName)

            If File.Exists(filePath) Then
                Dim jsonString As String = File.ReadAllText(filePath)
                Dim project As DoorProject = JsonSerializer.Deserialize(Of DoorProject)(jsonString)

                ' Ensure project has a GUID
                If project.ProjectId = Guid.Empty Then
                    project.ProjectId = Guid.NewGuid()
                    ' Optionally save the updated project with GUID
                    SaveDoorProjectInternal(project, filePath)
                End If

                Return project
            End If

            Return Nothing
        Catch ex As Exception
            ' Log error and return fallback project
            Console.WriteLine($"Error loading project info for '{projectName}': {ex.Message}")
            Return New DoorProject() With {
                .ProjectId = Guid.NewGuid(),
                .ProjectName = projectName,
                .SavedDate = If(File.Exists(Path.Combine(_projectsFolder, $"{SanitizeFileName(projectName)}.json")),
                               File.GetLastWriteTime(Path.Combine(_projectsFolder, $"{SanitizeFileName(projectName)}.json")),
                               DateTime.MinValue)
            }
        End Try
    End Function

    ''' <summary>
    ''' Internal method to save project data
    ''' </summary>
    Private Shared Sub SaveDoorProjectInternal(project As DoorProject, filePath As String)
        Try
            Dim jsonString As String = JsonSerializer.Serialize(project, _jsonOptions)
            File.WriteAllText(filePath, jsonString)
        Catch
            ' Ignore save errors in this context
        End Try
    End Sub

    Private Shared Sub EnsureProjectsFolderExists()
        If Not Directory.Exists(_projectsFolder) Then
            Directory.CreateDirectory(_projectsFolder)
        End If
    End Sub

    Private Shared Sub ApplyProjectToForm(form As FrmMain, project As DoorProject)
        With form
            If .TxtCabinetOpeningHeight IsNot Nothing Then .TxtCabinetOpeningHeight.Text = project.CabinetOpeningHeight.ToString("F3")
            If .TxtCabinetOpeningWidth IsNot Nothing Then .TxtCabinetOpeningWidth.Text = project.CabinetOpeningWidth.ToString("F3")
            If .TxtStileWidth IsNot Nothing Then .TxtStileWidth.Text = project.StileWidth.ToString("F3")
            If .TxtRailWidth IsNot Nothing Then .TxtRailWidth.Text = project.RailWidth.ToString("F3")
            If .TxtGapSize IsNot Nothing Then .TxtGapSize.Text = project.GapSize.ToString("F3")
            If .TxtDoorOverlay IsNot Nothing Then .TxtDoorOverlay.Text = project.DoorOverlay.ToString("F3")
            If .TxtPanelGrooveDepth IsNot Nothing Then .TxtPanelGrooveDepth.Text = project.PanelGrooveDepth.ToString("F3")
            If .TxtPanelExpansionGap IsNot Nothing Then .TxtPanelExpansionGap.Text = project.PanelExpansionGap.ToString("F3")
            If .TxtDoorProjectName IsNot Nothing Then .TxtDoorProjectName.Text = project.ProjectName

            ' Set door configuration
            If project.IsTwoDoor Then
                If .Rb2Door IsNot Nothing Then .Rb2Door.Checked = True
            Else
                If .Rb1Door IsNot Nothing Then .Rb1Door.Checked = True
            End If

            ' Set door type (overlay/inset)
            If project.IsOverlay Then
                If .RbOverlay IsNot Nothing Then .RbOverlay.Checked = True
            Else
                If .RbInset IsNot Nothing Then .RbInset.Checked = True
            End If

            ' Set measurement scale
            If project.Scale = MeasurementScale.Imperial Then
                If .RbImperial IsNot Nothing Then .RbImperial.Checked = True
            Else
                If .RbMetric IsNot Nothing Then .RbMetric.Checked = True
            End If
        End With
    End Sub

    Private Shared Function SanitizeFileName(fileName As String) As String
        Dim invalidChars As Char() = Path.GetInvalidFileNameChars()
        For Each invalidChar In invalidChars
            fileName = fileName.Replace(invalidChar, "_"c)
        Next
        Return fileName
    End Function

End Class