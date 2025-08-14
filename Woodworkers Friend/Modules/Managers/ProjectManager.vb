Imports System.IO
Imports System.Text.Json

Public Class ProjectManager

    ''' <summary>
    ''' Represents a saved project with all calculator settings
    ''' </summary>
    Public Class DrawerProject
        Public Property ProjectName As String
        Public Property DrawerCount As Integer
        Public Property DrawerSpacing As Double
        Public Property DrawerWidth As Double
        Public Property FirstDrawerHeight As Double
        Public Property Multiplier As Double
        Public Property ArithmeticIncrement As Double
        Public Property CalculationMethod As DrawerCalculationMethod
        Public Property Scale As MeasurementScale
        Public Property SavedDate As DateTime
        Public Property Notes As String
    End Class

    Private Shared ReadOnly _projectsFolder As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Woodworkers Friend", "Projects")

    ' Cache the options as a shared (static) field
    Private Shared ReadOnly _jsonOptions As New JsonSerializerOptions() With {
        .WriteIndented = True
    }

    ''' <summary>
    ''' Ensures the projects folder exists
    ''' </summary>
    Private Shared Sub EnsureProjectsFolderExists()
        If Not Directory.Exists(_projectsFolder) Then
            Directory.CreateDirectory(_projectsFolder)
        End If
    End Sub

    ''' <summary>
    ''' Saves a project to disk
    ''' </summary>
    Public Shared Function SaveProject(form As FrmMain, projectName As String) As Boolean
        Try
            EnsureProjectsFolderExists()

            Dim project As New DrawerProject() With {
                .ProjectName = projectName,
                .DrawerCount = CInt(ValidationManager.GetSafeDoubleFromControl(form.TxtDrawerCount, 0)),
                .DrawerSpacing = ValidationManager.GetSafeDoubleFromControl(form.TxtDrawerSpacing, 0),
                .DrawerWidth = ValidationManager.GetSafeDoubleFromControl(form.TxtDrawerWidth, 0),
                .FirstDrawerHeight = ValidationManager.GetSafeDoubleFromControl(form.TxtFirstDrawerHeight, 0),
                .Multiplier = ValidationManager.GetSafeDoubleFromControl(form.TxtMultiplier, 1.0),
                .ArithmeticIncrement = ValidationManager.GetSafeDoubleFromControl(form.TxtArithmeticIncrement, 0),
                .CalculationMethod = GetSelectedMethod(form),
                .Scale = If(form.RbImperial IsNot Nothing AndAlso form.RbImperial.Checked, MeasurementScale.Imperial, MeasurementScale.Metric),
                .SavedDate = DateTime.Now,
                .Notes = ControlUtility.GetControlText(form.TxtDrawerProjectName)
            }

            Dim fileName As String = $"draw_{SanitizeFileName(projectName)}-{Now:MMMdyy}.json"
            Dim filePath As String = Path.Combine(_projectsFolder, fileName)

            'Dim jsonString As String = JsonSerializer.Serialize(project, New JsonSerializerOptions() With {
            '    .WriteIndented = True
            '})

            ' Use the cached options
            Dim jsonString As String = JsonSerializer.Serialize(project, _jsonOptions)

            File.WriteAllText(filePath, jsonString)
            Return True
        Catch ex As Exception
            ' Log error or show message
            MessageBox.Show($"Error saving project: {ex.Message}", "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Loads a project from disk
    ''' </summary>
    Public Shared Function LoadProject(form As FrmMain, projectName As String) As Boolean
        Try
            Dim fileName As String = $"{SanitizeFileName(projectName)}.json"
            Dim filePath As String = Path.Combine(_projectsFolder, fileName)

            If Not File.Exists(filePath) Then
                MessageBox.Show("Project file not found.", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return False
            End If

            Dim jsonString As String = File.ReadAllText(filePath)
            Dim project As DrawerProject = JsonSerializer.Deserialize(Of DrawerProject)(jsonString)

            ' Apply project settings to form
            ApplyProjectToForm(form, project)
            Return True
        Catch ex As Exception
            MessageBox.Show($"Error loading project: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Gets a list of saved project names
    ''' </summary>
    Public Shared Function GetSavedProjects() As List(Of String)
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
    ''' Deletes a saved project
    ''' </summary>
    Public Shared Function DeleteProject(projectName As String) As Boolean
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
    ''' Applies loaded project settings to the form
    ''' </summary>
    Private Shared Sub ApplyProjectToForm(form As FrmMain, project As DrawerProject)
        With form
            If .TxtDrawerCount IsNot Nothing Then .TxtDrawerCount.Text = project.DrawerCount.ToString()
            If .TxtDrawerSpacing IsNot Nothing Then .TxtDrawerSpacing.Text = project.DrawerSpacing.ToString("F3")
            If .TxtDrawerWidth IsNot Nothing Then .TxtDrawerWidth.Text = project.DrawerWidth.ToString("F3")
            If .TxtFirstDrawerHeight IsNot Nothing Then .TxtFirstDrawerHeight.Text = project.FirstDrawerHeight.ToString("F3")
            If .TxtMultiplier IsNot Nothing Then .TxtMultiplier.Text = project.Multiplier.ToString("F3")
            If .TxtArithmeticIncrement IsNot Nothing Then .TxtArithmeticIncrement.Text = project.ArithmeticIncrement.ToString("F3")
            If .TxtDrawerProjectName IsNot Nothing Then .TxtDrawerProjectName.Text = project.ProjectName

            ' Set calculation method - NOW INCLUDES ALL METHODS
            Select Case project.CalculationMethod
                Case DrawerCalculationMethod.Hambridge
                    If .RbHambridge IsNot Nothing Then .RbHambridge.Checked = True
                Case DrawerCalculationMethod.Geometric
                    If .RbGeometric IsNot Nothing Then .RbGeometric.Checked = True
                Case DrawerCalculationMethod.Fibonacci
                    If .RbFibonacci IsNot Nothing Then .RbFibonacci.Checked = True
                Case DrawerCalculationMethod.Arithmetic
                    If .RbArithmetic IsNot Nothing Then .RbArithmetic.Checked = True
                Case DrawerCalculationMethod.Logarithmic
                    If .RbLogarithmic IsNot Nothing Then .RbLogarithmic.Checked = True
                Case DrawerCalculationMethod.Exponential
                    If .RbExponential IsNot Nothing Then .RbExponential.Checked = True
                Case DrawerCalculationMethod.CustomRatio
                    If .RbCustomRatio IsNot Nothing Then .RbCustomRatio.Checked = True
                Case DrawerCalculationMethod.Uniform
                    If .RbUniform IsNot Nothing Then .RbUniform.Checked = True
                Case DrawerCalculationMethod.ReverseArithmetic
                    If .RbReverseArithmetic IsNot Nothing Then .RbReverseArithmetic.Checked = True
                Case DrawerCalculationMethod.GoldenRatio
                    If .RbGoldenRatio IsNot Nothing Then .RbGoldenRatio.Checked = True
                Case Else
                    ' Default to Hambridge if method not found
                    If .RbHambridge IsNot Nothing Then .RbHambridge.Checked = True
            End Select

            ' Set measurement scale
            If project.Scale = MeasurementScale.Imperial Then
                If .RbImperial IsNot Nothing Then .RbImperial.Checked = True
            Else
                If .RbMetric IsNot Nothing Then .RbMetric.Checked = True
            End If
        End With
    End Sub

    ''' <summary>
    ''' Gets the currently selected calculation method from the form
    ''' </summary>
    Private Shared Function GetSelectedMethod(form As FrmMain) As DrawerCalculationMethod
        If form.RbHambridge IsNot Nothing AndAlso form.RbHambridge.Checked Then
            Return DrawerCalculationMethod.Hambridge
        ElseIf form.RbGeometric IsNot Nothing AndAlso form.RbGeometric.Checked Then
            Return DrawerCalculationMethod.Geometric
        ElseIf form.RbFibonacci IsNot Nothing AndAlso form.RbFibonacci.Checked Then
            Return DrawerCalculationMethod.Fibonacci
        ElseIf form.RbArithmetic IsNot Nothing AndAlso form.RbArithmetic.Checked Then
            Return DrawerCalculationMethod.Arithmetic
        ElseIf form.RbLogarithmic IsNot Nothing AndAlso form.RbLogarithmic.Checked Then
            Return DrawerCalculationMethod.Logarithmic
        ElseIf form.RbExponential IsNot Nothing AndAlso form.RbExponential.Checked Then
            Return DrawerCalculationMethod.Exponential
        ElseIf form.RbCustomRatio IsNot Nothing AndAlso form.RbCustomRatio.Checked Then
            Return DrawerCalculationMethod.CustomRatio
        ElseIf form.RbUniform IsNot Nothing AndAlso form.RbUniform.Checked Then
            Return DrawerCalculationMethod.Uniform
        ElseIf form.RbReverseArithmetic IsNot Nothing AndAlso form.RbReverseArithmetic.Checked Then
            Return DrawerCalculationMethod.ReverseArithmetic
        ElseIf form.RbGoldenRatio IsNot Nothing AndAlso form.RbGoldenRatio.Checked Then
            Return DrawerCalculationMethod.GoldenRatio
        Else
            Return DrawerCalculationMethod.Hambridge
        End If
    End Function

    ''' <summary>
    ''' Sanitizes a filename to remove invalid characters
    ''' </summary>
    Private Shared Function SanitizeFileName(fileName As String) As String
        Dim invalidChars As Char() = Path.GetInvalidFileNameChars()
        For Each invalidChar In invalidChars
            fileName = fileName.Replace(invalidChar, "_"c)
        Next
        Return fileName
    End Function

End Class