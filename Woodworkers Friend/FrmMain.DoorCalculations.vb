Imports System.IO
Imports System.Drawing.Printing

Partial Public Class FrmMain

    ' Private TmrDoorCalculationDelay As New Timer With {.Interval = 500}

#Region "Door Calculator Event Handlers"

    ''' <summary>
    ''' Calculate door components
    ''' </summary>
    Private Sub BtnCalculateDoors_Click(sender As Object, e As EventArgs) Handles BtnCalculateDoors.Click
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)

        Try
            SetDoorCalculatingState(True)

            ' Collect parameters
            Dim parameters As DoorCalculationParameters = CollectDoorParameters()

            ' Calculate door components - use explicit module reference
            Dim result As WwFriend.DoorCalculationResult = DoorCalculationRoutines.CalculateDoorComponents(parameters)

            ' Display results
            DisplayDoorResults(result)

            SetDoorCalculatingState(False)

            If result.IsValid Then
                UpdateDoorStatus("Door calculation completed successfully", Color.Green)
            Else
                UpdateDoorStatus($"Door calculation failed: {result.ErrorMessage}", Color.Red)
                ShowErrorMessage("Door Calculation Error", result.ErrorMessage)
            End If
        Catch ex As Exception
            SetDoorCalculatingState(False)
            UpdateDoorStatus("Door calculation failed - see details", Color.Red)
            ShowErrorMessage("Door Calculation Error", $"An unexpected error occurred:{vbCrLf}{vbCrLf}{ex.Message}")
        End Try
    End Sub

    ''' <summary>
    ''' Save door project
    ''' </summary>
    Private Sub BtnSaveDoorProject_Click(sender As Object, e As EventArgs) Handles BtnSaveDoorProject.Click
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)

        Try
            If String.IsNullOrWhiteSpace(TxtDoorProjectName?.Text) Then
                ShowErrorMessage("Save Error", "Please enter a project name before saving.")
                Return
            End If

            Dim success As Boolean = DoorProjectManager.SaveDoorProject(Me, TxtDoorProjectName.Text.Trim())
            If success Then
                UpdateDoorStatus($"Door project '{TxtDoorProjectName.Text}' saved successfully", Color.Green)
            Else
                UpdateDoorStatus("Failed to save door project", Color.Red)
            End If
        Catch ex As Exception
            UpdateDoorStatus("Error saving door project", Color.Red)
            ShowErrorMessage("Save Error", $"Failed to save door project: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "Door Project Management"

    Private Sub BtnLoadDoorProject_Click(sender As Object, e As EventArgs) Handles BtnLoadDoorProject.Click
        Try
            Dim projects As List(Of String) = DoorProjectManager.GetSavedDoorProjects()
            If projects.Count = 0 Then
                ShowErrorMessage("Load Project", "No saved door projects found.")
                Return
            End If

            Using dlg As New ProjectSelectionDialog("Select Door Project to Load", projects)
                If dlg.ShowDialog() = DialogResult.OK Then
                    ' Check if we have a valid GUID
                    If dlg.SelectedProjectGuid <> Guid.Empty Then
                        ' Use GUID-based loading if needed in future
                        Dim success As Boolean = DoorProjectManager.LoadDoorProject(Me, dlg.SelectedProject)
                        If success Then
                            UpdateDoorStatus($"Door project '{dlg.SelectedProject}' (ID: {dlg.SelectedProjectGuid.ToString().Substring(0, 8)}...) loaded successfully", Color.Green)
                        Else
                            UpdateDoorStatus("Failed to load door project", Color.Red)
                        End If
                    Else
                        ' Fallback to name-based loading
                        Dim success As Boolean = DoorProjectManager.LoadDoorProject(Me, dlg.SelectedProject)
                        If success Then
                            UpdateDoorStatus($"Door project '{dlg.SelectedProject}' loaded successfully", Color.Green)
                        Else
                            UpdateDoorStatus("Failed to load door project", Color.Red)
                        End If
                    End If
                End If
            End Using
        Catch ex As Exception
            ShowErrorMessage("Load Error", $"Error loading project: {ex.Message}")
        End Try
    End Sub

    Private Sub BtnDeleteDoorProject_Click(sender As Object, e As EventArgs) Handles BtnDeleteDoorProject.Click
        Try
            Dim projects As List(Of String) = DoorProjectManager.GetSavedDoorProjects()
            If projects.Count = 0 Then
                ShowErrorMessage("Delete Project", "No saved door projects found.")
                Return
            End If

            ' Simple approach using InputBox
            Dim projectList As String = String.Join(vbCrLf, projects)
            Dim selectedProject As String = InputBox($"Available projects:{vbCrLf}{projectList}{vbCrLf}{vbCrLf}Enter project name to delete:", "Delete Door Project")

            If Not String.IsNullOrWhiteSpace(selectedProject) AndAlso projects.Contains(selectedProject) Then
                Dim result As DialogResult = MessageBox.Show(
                    $"Are you sure you want to delete the project '{selectedProject}'?",
                    "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

                If result = DialogResult.Yes Then
                    Dim success As Boolean = DoorProjectManager.DeleteDoorProject(selectedProject)
                    If success Then
                        UpdateDoorStatus($"Door project '{selectedProject}' deleted successfully", Color.Green)
                    Else
                        UpdateDoorStatus("Failed to delete door project", Color.Red)
                    End If
                End If
            ElseIf Not String.IsNullOrWhiteSpace(selectedProject) Then
                ShowErrorMessage("Delete Error", $"Project '{selectedProject}' not found.")
            End If
        Catch ex As Exception
            ShowErrorMessage("Delete Error", $"Error deleting project: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "Door Calculation Helper Methods"

    ''' <summary>
    ''' Collects door calculation parameters from UI controls
    ''' </summary>
    Private Function CollectDoorParameters() As DoorCalculationParameters
        Return New DoorCalculationParameters() With {
            .CabinetOpeningHeight = ValidationManager.GetDoubleFromControl(TxtCabinetOpeningHeight, "Cabinet Opening Height"),
            .CabinetOpeningWidth = ValidationManager.GetDoubleFromControl(TxtCabinetOpeningWidth, "Cabinet Opening Width"),
            .StileWidth = ValidationManager.GetDoubleFromControl(TxtStileWidth, "Stile Width"),
            .RailWidth = ValidationManager.GetDoubleFromControl(TxtRailWidth, "Rail Width"),
            .IsTwoDoor = Rb2Door IsNot Nothing AndAlso Rb2Door.Checked,
            .GapSize = ValidationManager.GetDoubleFromControl(TxtGapSize, "Gap Size"),
            .DoorOverlay = ValidationManager.GetDoubleFromControl(TxtDoorOverlay, "Door Overlay"),
            .IsOverlay = RbOverlay IsNot Nothing AndAlso RbOverlay.Checked,
            .PanelGrooveDepth = ValidationManager.GetDoubleFromControl(TxtPanelGrooveDepth, "Panel Groove Depth"),
            .PanelExpansionGap = ValidationManager.GetDoubleFromControl(TxtPanelExpansionGap, "Panel Expansion Gap"),
            .Scale = If(RbImperial IsNot Nothing AndAlso RbImperial.Checked, MeasurementScale.Imperial, MeasurementScale.Metric)
        }
    End Function

    ''' <summary>
    ''' Displays door calculation results
    ''' </summary>
    Private Sub DisplayDoorResults(result As WwFriend.DoorCalculationResult)
        If result Is Nothing Then Return

        If Not result.IsValid Then
            ClearDoorResults()
            Return
        End If

        Try
            ' Update result labels
            UpdateLabelControl(LblRailLength, $"Rail Length: {result.RailLength:N3} {result.Unit}")
            UpdateLabelControl(LblStileLength, $"Stile Length: {result.StileLength:N3} {result.Unit}")
            UpdateLabelControl(LblPanelHeight, $"Panel Height: {result.PanelHeight:N3} {result.Unit}")
            UpdateLabelControl(LblPanelWidth, $"Panel Width: {result.PanelWidth:N3} {result.Unit}")

            ' Update detailed results
            UpdateTextControl(RtbDoorResults, result.Details)
        Catch ex As Exception
            RtbLog?.AppendText($"{Now:hh:mm:ss} - Error displaying door results: {ex.Message}{vbCrLf}")
        End Try
    End Sub

    ''' <summary>
    ''' Clears door calculation results
    ''' </summary>
    Private Sub ClearDoorResults()
        Try
            UpdateLabelControl(LblRailLength, "Rail Length: --")
            UpdateLabelControl(LblStileLength, "Stile Length: --")
            UpdateLabelControl(LblPanelHeight, "Panel Height: --")
            UpdateLabelControl(LblPanelWidth, "Panel Width: --")
            UpdateTextControl(RtbDoorResults, "")
        Catch ex As Exception
            RtbLog?.AppendText($"{Now:hh:mm:ss} - Error clearing door results: {ex.Message}{vbCrLf}")
        End Try
    End Sub

    ''' <summary>
    ''' Sets calculating state for door calculations
    ''' </summary>
    Private Sub SetDoorCalculatingState(calculating As Boolean)
        Cursor = If(calculating, Cursors.WaitCursor, Cursors.Default)

        ' Disable/enable door calculation controls
        ControlUtility.SetControlsEnabled(Not calculating,
            TxtCabinetOpeningHeight,
            TxtCabinetOpeningWidth,
            TxtStileWidth,
            TxtRailWidth,
            Rb1Door,
            Rb2Door,
            TxtGapSize,
            TxtPanelGrooveDepth,
            TxtPanelExpansionGap,
            BtnCalculateDoors,
            BtnSaveDoorProject)

        If calculating Then UpdateDoorStatus("Calculating door components...", Color.Blue)
        Application.DoEvents()
    End Sub

    ''' <summary>
    ''' Updates door calculation status
    ''' </summary>
    Private Sub UpdateDoorStatus(message As String, color As Color)
        If LblStatus IsNot Nothing Then
            LblStatus.Text = $"Door Calculator: {message}"
            LblStatus.ForeColor = color
        End If
    End Sub

#End Region

#Region "Door Presets"

    Private Sub BtnKitchenDoorPreset_Click(sender As Object, e As EventArgs) Handles BtnKitchenDoorPreset.Click
        ApplyDoorPreset("kitchen")
    End Sub

    Private Sub BtnBathroomDoorPreset_Click(sender As Object, e As EventArgs) Handles BtnBathroomDoorPreset.Click
        ApplyDoorPreset("bathroom")
    End Sub

    Private Sub BtnOfficeDoorPreset_Click(sender As Object, e As EventArgs) Handles BtnOfficeDoorPreset.Click
        ApplyDoorPreset("office")
    End Sub

    Private Sub ApplyDoorPreset(presetType As String)
        Select Case presetType.ToLower()
            Case "kitchen"
                TxtCabinetOpeningHeight.Text = "30"
                TxtCabinetOpeningWidth.Text = "24"
                TxtStileWidth.Text = "2.25"
                TxtRailWidth.Text = "2.25"
                TxtDoorOverlay.Text = "0.5"
                TxtGapSize.Text = "0.125"
                TxtPanelGrooveDepth.Text = "0.25"
                TxtPanelExpansionGap.Text = "0.0625"
                RbOverlay.Checked = True
                Rb1Door.Checked = True

            Case "bathroom"
                TxtCabinetOpeningHeight.Text = "24"
                TxtCabinetOpeningWidth.Text = "18"
                TxtStileWidth.Text = "2"
                TxtRailWidth.Text = "2"
                TxtDoorOverlay.Text = "0.375"
                TxtGapSize.Text = "0.125"
                TxtPanelGrooveDepth.Text = "0.25"
                TxtPanelExpansionGap.Text = "0.0625"
                RbOverlay.Checked = True
                Rb1Door.Checked = True

            Case "office"
                TxtCabinetOpeningHeight.Text = "28"
                TxtCabinetOpeningWidth.Text = "32"
                TxtStileWidth.Text = "2"
                TxtRailWidth.Text = "2.5"
                TxtDoorOverlay.Text = "0.25"
                TxtGapSize.Text = "0.125"
                TxtPanelGrooveDepth.Text = "0.25"
                TxtPanelExpansionGap.Text = "0.0625"
                RbOverlay.Checked = True
                Rb2Door.Checked = True
        End Select

        UpdateDoorStatus($"{presetType} door preset applied", Color.Green)
    End Sub

#End Region

#Region "Real-time Updates"

    Private Sub DoorParameter_Changed(sender As Object, e As EventArgs) Handles TxtCabinetOpeningHeight.TextChanged, TxtCabinetOpeningWidth.TextChanged, TxtStileWidth.TextChanged, TxtRailWidth.TextChanged
        If Not _loading AndAlso _calculatorInitialized Then
            ' Delay real-time calculation to avoid excessive updates
            TmrDoorCalculationDelay.Stop()
            TmrDoorCalculationDelay.Start()
        End If
    End Sub

    Private Sub TmrDoorCalculationDelay_Tick(sender As Object, e As EventArgs) Handles TmrDoorCalculationDelay.Tick
        TmrDoorCalculationDelay.Stop()
        PerformQuickDoorCalculation()
    End Sub

    Private Sub PerformQuickDoorCalculation()
        Try
            Dim parameters As DoorCalculationParameters = CollectDoorParameters()
            Dim result As WwFriend.DoorCalculationResult = DoorCalculationRoutines.CalculateDoorComponents(parameters)

            If result.IsValid Then
                ' Update only the basic dimensions for real-time feedback
                UpdateLabelControl(LblRailLength, $"Rail Length: {result.RailLength:N3} {result.Unit}")
                UpdateLabelControl(LblStileLength, $"Stile Length: {result.StileLength:N3} {result.Unit}")
                UpdateLabelControl(LblPanelHeight, $"Panel Height: {result.PanelHeight:N3} {result.Unit}")
                UpdateLabelControl(LblPanelWidth, $"Panel Width: {result.PanelWidth:N3} {result.Unit}")
            End If
        Catch
            ' Ignore errors during real-time updates
        End Try
    End Sub

#End Region

#Region "Input Validation"

    Private Sub ValidateDoorInput(sender As Object, e As EventArgs) Handles TxtCabinetOpeningHeight.Leave, TxtCabinetOpeningWidth.Leave, TxtStileWidth.Leave, TxtRailWidth.Leave
        Dim textBox As TextBox = TryCast(sender, TextBox)
        If textBox Is Nothing Then Return

        Dim value As Double
        If Not Double.TryParse(textBox.Text, value) Then
            ShowInputValidationError(textBox, "Please enter a valid number")
            Return
        End If

        Select Case textBox.Name
            Case "TxtCabinetOpeningHeight", "TxtCabinetOpeningWidth"
                If value <= 0 OrElse value > 120 Then
                    ShowInputValidationError(textBox, "Value must be between 0 and 120 inches")
                Else
                    ClearInputValidationError(textBox)
                End If

            Case "TxtStileWidth", "TxtRailWidth"
                If value <= 0 OrElse value > 6 Then
                    ShowInputValidationError(textBox, "Value must be between 0 and 6 inches")
                Else
                    ClearInputValidationError(textBox)
                End If
        End Select
    End Sub

    Private Sub ShowInputValidationError(textBox As TextBox, message As String)
        textBox.BackColor = Color.LightPink
        tTip.SetToolTip(textBox, message)
        tTip.Show(message, textBox, textBox.Width, 0, 3000)
    End Sub

    Private Sub ClearInputValidationError(textBox As TextBox)
        textBox.BackColor = SystemColors.Window
        tTip.SetToolTip(textBox, Nothing)
    End Sub

#End Region

#Region "Export Functions"

    Private Sub BtnExportDoorResults_Click(sender As Object, e As EventArgs) Handles BtnExportDoorResults.Click
        Try
            If String.IsNullOrWhiteSpace(RtbDoorResults.Text) Then
                ShowErrorMessage("Export Error", "No calculation results to export. Please calculate door dimensions first.")
                Return
            End If

            Using dlg As New SaveFileDialog()
                dlg.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|All Files (*.*)|*.*"
                dlg.DefaultExt = "txt"
                dlg.FileName = $"Door_Calculation_{DateTime.Now:yyyyMMdd_HHmmss}"

                If dlg.ShowDialog() = DialogResult.OK Then
                    If dlg.FilterIndex = 2 Then ' RTF
                        File.WriteAllText(dlg.FileName, RtbDoorResults.Rtf)
                    Else ' Plain text
                        File.WriteAllText(dlg.FileName, RtbDoorResults.Text)
                    End If

                    UpdateDoorStatus($"Results exported to {Path.GetFileName(dlg.FileName)}", Color.Green)
                End If
            End Using
        Catch ex As Exception
            ShowErrorMessage("Export Error", $"Failed to export results: {ex.Message}")
        End Try
    End Sub

    Private Sub BtnPrintDoorResults_Click(sender As Object, e As EventArgs) Handles BtnPrintDoorResults.Click
        Try
            If String.IsNullOrWhiteSpace(RtbDoorResults.Text) Then
                ShowErrorMessage("Print Error", "No calculation results to print. Please calculate door dimensions first.")
                Return
            End If

            Using printDoc As New Printing.PrintDocument()
                AddHandler printDoc.PrintPage, AddressOf PrintDoc_PrintPage
                printDoc.DocumentName = "Door Calculation Results"

                Using printDialog As New PrintDialog()
                    printDialog.Document = printDoc
                    If printDialog.ShowDialog() = DialogResult.OK Then
                        printDoc.Print()
                        UpdateDoorStatus("Results sent to printer", Color.Green)
                    End If
                End Using
            End Using
        Catch ex As Exception
            ShowErrorMessage("Print Error", $"Failed to print results: {ex.Message}")
        End Try
    End Sub

#End Region

#Region "Print Support"

    Private _printContent As String = String.Empty

    Private Sub PrintDoc_PrintPage(sender As Object, e As PrintPageEventArgs)
        Try
            ' Use the content from RtbDoorResults
            _printContent = RtbDoorResults.Text

            ' Set up font and brush
            Dim font As New Font("Courier New", 10)
            Dim brush As New SolidBrush(Color.Black)

            ' Calculate margins
            Dim leftMargin As Single = e.MarginBounds.Left
            Dim topMargin As Single = e.MarginBounds.Top
            '  Dim rightMargin As Single = e.MarginBounds.Right
            Dim bottomMargin As Single = e.MarginBounds.Bottom

            ' Calculate line height
            Dim lineHeight As Single = font.GetHeight(e.Graphics)

            ' Split content into lines
            Dim lines() As String = _printContent.Split({vbCrLf, vbLf}, StringSplitOptions.None)

            ' Calculate how many lines fit on a page
            Dim linesPerPage As Integer = CInt((bottomMargin - topMargin) / lineHeight)

            ' Print lines
            Dim currentY As Single = topMargin
            Dim linesPrinted As Integer = 0

            For Each line As String In lines
                If linesPrinted >= linesPerPage Then
                    e.HasMorePages = True
                    Exit For
                End If

                ' Wrap long lines if necessary
                Dim wrappedLines As List(Of String) = WrapText(line, font, e.Graphics, e.MarginBounds.Width)

                For Each wrappedLine As String In wrappedLines
                    If linesPrinted >= linesPerPage Then
                        e.HasMorePages = True
                        Exit For
                    End If

                    e.Graphics.DrawString(wrappedLine, font, brush, leftMargin, currentY)
                    currentY += lineHeight
                    linesPrinted += 1
                Next

                If e.HasMorePages Then Exit For
            Next

            ' Add header with date/time
            Dim headerFont As New Font("Arial", 8, FontStyle.Bold)
            Dim headerText As String = $"Door Calculation Results - {DateTime.Now:yyyy-MM-dd HH:mm:ss} - Page {e.PageSettings.PrinterSettings.FromPage}"
            e.Graphics.DrawString(headerText, headerFont, brush, leftMargin, topMargin - 20)

            ' Clean up
            font.Dispose()
            brush.Dispose()
            headerFont.Dispose()
        Catch ex As Exception
            MessageBox.Show($"Error during printing: {ex.Message}", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Function WrapText(text As String, font As Font, graphics As Graphics, maxWidth As Single) As List(Of String)
        Dim words As String() = text.Split(" "c)
        Dim lines As New List(Of String)
        Dim currentLine As String = ""

        For Each word As String In words
            Dim testLine As String = If(String.IsNullOrEmpty(currentLine), word, currentLine & " " & word)
            Dim size As SizeF = graphics.MeasureString(testLine, font)

            If size.Width <= maxWidth Then
                currentLine = testLine
            Else
                If Not String.IsNullOrEmpty(currentLine) Then
                    lines.Add(currentLine)
                End If
                currentLine = word
            End If
        Next

        If Not String.IsNullOrEmpty(currentLine) Then
            lines.Add(currentLine)
        End If

        Return lines
    End Function

#End Region

End Class