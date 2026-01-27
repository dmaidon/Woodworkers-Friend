Imports System.IO
Imports System.Drawing.Printing

Partial Public Class FrmMain

    ' Removed nested DoorCalculationInput and DoorCalculationResult to avoid type conflicts

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
            Dim result As DoorCalculationResult = DoorCalculationRoutines.CalculateDoorComponents(parameters)

            ' Display results
            DisplayDoorResults(result)

            ' Store for drawing & enable door image button (if it exists)
            _lastDoorResult = result
            _lastDoorParameters = parameters
            If result.IsValid Then
                _outputDrawingMode = OutputDrawingMode.Door
                If BtnDrawDoorImage IsNot Nothing Then BtnDrawDoorImage.Enabled = True
            Else
                If BtnDrawDoorImage IsNot Nothing Then BtnDrawDoorImage.Enabled = False
            End If
            PbOutputDrawing.Invalidate()

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

            Dim success As Boolean = DoorProjectManager.SaveDoorProject(Me, $"door_{TxtDoorProjectName.Text.Trim()}-{Now:MMMdyy}.txt")
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

#Region "Door Configuration Control Logic"

    Private Sub RbOverlay_CheckedChanged(sender As Object, e As EventArgs) Handles RbOverlay.CheckedChanged
        UpdateDoorControlStates()
    End Sub

    Private Sub RbInset_CheckedChanged(sender As Object, e As EventArgs) Handles RbInset.CheckedChanged
        UpdateDoorControlStates()
    End Sub

    Private Sub Rb1Door_CheckedChanged(sender As Object, e As EventArgs) Handles Rb1Door.CheckedChanged
        UpdateDoorControlStates()
    End Sub

    Private Sub Rb2Door_CheckedChanged(sender As Object, e As EventArgs) Handles Rb2Door.CheckedChanged
        UpdateDoorControlStates()
    End Sub

    ''' <summary>
    ''' Updates the enabled/disabled state of controls based on door configuration
    ''' </summary>
    Private Sub UpdateDoorControlStates()
        If _loading Then Return
        Try
            WwFriend.Modules.Doors.DoorUiStateController.Apply(
                isInset:=RbInset IsNot Nothing AndAlso RbInset.Checked,
                isSingleDoor:=Rb1Door IsNot Nothing AndAlso Rb1Door.Checked,
                txtDoorOverlay:=TxtDoorOverlay,
                txtGapSize:=TxtGapSize,
                updatePresetButtons:=Sub() UpdatePresetButtons()
            )
        Catch ex As Exception
            MessageBox.Show($"Error updating door control states: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Updates preset buttons based on current door configuration
    ''' </summary>
    Private Sub UpdatePresetButtons()
        ' You can add logic here to enable/disable certain presets
        ' based on the current door configuration if needed
    End Sub

#End Region

#Region "Door Calculation Helper Methods"

    ''' <summary>
    ''' Collects door calculation parameters from UI controls with conditional logic
    ''' </summary>
    Private Function CollectDoorParameters() As DoorCalculationParameters
        Return New DoorCalculationParameters() With {
            .CabinetOpeningHeight = ValidationManager.GetDoubleFromControl(TxtCabinetOpeningHeight, "Cabinet Opening Height"),
            .CabinetOpeningWidth = ValidationManager.GetDoubleFromControl(TxtCabinetOpeningWidth, "Cabinet Opening Width"),
            .StileWidth = ValidationManager.GetDoubleFromControl(TxtStileWidth, "Stile Width"),
            .RailWidth = ValidationManager.GetDoubleFromControl(TxtRailWidth, "Rail Width"),
            .IsTwoDoor = Rb2Door IsNot Nothing AndAlso Rb2Door.Checked,
            .GapSize = If(Rb1Door IsNot Nothing AndAlso Rb1Door.Checked, 0, ValidationManager.GetDoubleFromControl(TxtGapSize, "Gap Size")),
            .DoorOverlay = If(RbInset IsNot Nothing AndAlso RbInset.Checked, 0, ValidationManager.GetDoubleFromControl(TxtDoorOverlay, "Door Overlay")),
            .IsOverlay = RbOverlay IsNot Nothing AndAlso RbOverlay.Checked,
            .PanelGrooveDepth = ValidationManager.GetDoubleFromControl(TxtPanelGrooveDepth, "Panel Groove Depth"),
            .PanelExpansionGap = ValidationManager.GetDoubleFromControl(TxtPanelExpansionGap, "Panel Expansion Gap"),
            .Scale = If(RbImperial IsNot Nothing AndAlso RbImperial.Checked, MeasurementScale.Imperial, MeasurementScale.Metric)
        }
    End Function

#End Region

#Region "Door Presets - Updated"

    Private Sub ApplyDoorPreset(presetType As WwFriend.Modules.Doors.DoorPresetType)
        _loading = True
        Try
            Dim preset = WwFriend.Modules.Doors.DoorPresets.GetPreset(presetType)

            TxtCabinetOpeningHeight.Text = preset.OpeningHeight
            TxtCabinetOpeningWidth.Text = preset.OpeningWidth
            TxtStileWidth.Text = preset.StileWidth
            TxtRailWidth.Text = preset.RailWidth
            TxtPanelGrooveDepth.Text = preset.GrooveDepth
            TxtPanelExpansionGap.Text = preset.ExpansionGap

            If preset.IsOverlay AndAlso RbOverlay IsNot Nothing Then RbOverlay.Checked = True
            If Not preset.IsOverlay AndAlso RbInset IsNot Nothing Then RbInset.Checked = True

            If preset.IsSingleDoor AndAlso Rb1Door IsNot Nothing Then Rb1Door.Checked = True
            If Not preset.IsSingleDoor AndAlso Rb2Door IsNot Nothing Then Rb2Door.Checked = True
        Finally
            _loading = False
            UpdateDoorControlStates()
        End Try

        UpdateDoorStatus($"{presetType} door preset applied", Color.Green)
    End Sub

#End Region

#Region "Initialize Door Controls"

    ''' <summary>
    ''' Initialize door control states when the form loads
    ''' </summary>
    Private Sub InitializeDoorControls()
        ' Set default values
        If RbOverlay IsNot Nothing Then RbOverlay.Checked = True
        If Rb1Door IsNot Nothing Then Rb1Door.Checked = True

        ' Set default expansion gap
        If TxtPanelExpansionGap IsNot Nothing AndAlso String.IsNullOrEmpty(TxtPanelExpansionGap.Text) Then
            TxtPanelExpansionGap.Text = "0.0625"
        End If

        If TxtPanelGrooveDepth IsNot Nothing AndAlso String.IsNullOrEmpty(TxtPanelGrooveDepth.Text) Then
            TxtPanelGrooveDepth.Text = "0.375"
        End If

        ' Update control states
        UpdateDoorControlStates()
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
            Dim result As DoorCalculationResult = DoorCalculationRoutines.CalculateDoorComponents(parameters)

            If result.IsValid Then
                ' Convert values for display
                Dim isImperial = _scaleManager.CurrentScale = ScaleManager.ScaleType.Imperial

                Dim railLengthDisplay = If(isImperial,
                    $"{result.RailLength:N3} in ({ScaleManager.ToMetricInches(result.RailLength):N3} mm)",
                    $"{ScaleManager.ToImperialMillimeters(result.RailLength):N3} in ({result.RailLength:N3} mm)")

                Dim stileLengthDisplay = If(isImperial,
                    $"{result.StileLength:N3} in ({ScaleManager.ToMetricInches(result.StileLength):N3} mm)",
                    $"{ScaleManager.ToImperialMillimeters(result.StileLength):N3} in ({result.StileLength:N3} mm)")

                Dim panelHeightDisplay = If(isImperial,
                    $"{result.PanelHeight:N3} in ({ScaleManager.ToMetricInches(result.PanelHeight):N3} mm)",
                    $"{ScaleManager.ToImperialMillimeters(result.PanelHeight):N3} in ({result.PanelHeight:N3} mm)")

                Dim panelWidthDisplay = If(isImperial,
                    $"{result.PanelWidth:N3} in ({ScaleManager.ToMetricInches(result.PanelWidth):N3} mm)",
                    $"{ScaleManager.ToImperialMillimeters(result.PanelWidth):N3} in ({result.PanelWidth:N3} mm)")

                ' Update only the basic dimensions for real-time feedback
                UpdateLabelControl(LblRailLength, $"Rail Length: {railLengthDisplay}")
                UpdateLabelControl(LblStileLength, $"Stile Length: {stileLengthDisplay}")
                UpdateLabelControl(LblPanelHeight, $"Panel Height: {panelHeightDisplay}")
                UpdateLabelControl(LblPanelWidth, $"Panel Width: {panelWidthDisplay}")
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

        ' Using the new validation function
        Select Case textBox.Name
            Case "TxtCabinetOpeningHeight", "TxtCabinetOpeningWidth"
                ValidateNumericInput(textBox, 0.01, 120, "Value must be between 0 and 120 inches")
            Case "TxtStileWidth", "TxtRailWidth"
                ValidateNumericInput(textBox, 0.01, 6, "Value must be between 0 and 6 inches")
        End Select
    End Sub

    Private Function ValidateNumericInput(textBox As TextBox, minValue As Double, maxValue As Double, errorMessage As String) As Boolean
        Dim value As Double
        If Not Double.TryParse(textBox.Text, value) OrElse value < minValue OrElse value > maxValue Then
            ShowInputValidationError(textBox, errorMessage)
            Return False
        End If
        ClearInputValidationError(textBox)
        Return True
    End Function

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
            WwFriend.Modules.Doors.DoorResultsExporter.Export(Me, RtbDoorResults, Sub(msg, clr) UpdateDoorStatus(msg, clr))
        Catch ex As Exception
            ShowErrorMessage("Export Error", $"Failed to export results: {ex.Message}")
        End Try
    End Sub

    ' Replace BtnPrintDoorResults_Click with:
    Private Sub BtnPrintDoorResults_Click(sender As Object, e As EventArgs) Handles BtnPrintDoorResults.Click
        Try
            WwFriend.Modules.Doors.DoorResultsPrinter.Print(Me, RtbDoorResults, Sub(msg, clr) UpdateDoorStatus(msg, clr))
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

    ''' <summary>
    ''' Displays door calculation results in both imperial and metric
    ''' </summary>
    Private Sub DisplayDoorResults(result As DoorCalculationResult)
        If result Is Nothing Then Return

        If Not result.IsValid Then
            ClearDoorResults()
            Return
        End If

        Try
            ' Convert existing single values to both units using ScaleManager
            Dim railLengthImp, railLengthMet As Double
            Dim stileLengthImp, stileLengthMet As Double
            Dim panelHeightImp, panelHeightMet As Double
            Dim panelWidthImp, panelWidthMet As Double
            Dim doorWidthImp, doorWidthMet As Double
            Dim doorHeightImp, doorHeightMet As Double

            ' Determine if the result is in imperial or metric based on Scale parameter
            Dim isImperial = _scaleManager.CurrentScale = ScaleManager.ScaleType.Imperial

            If isImperial Then
                ' Result is in imperial, convert to metric
                railLengthImp = result.RailLength
                railLengthMet = ScaleManager.ToMetricInches(result.RailLength)
                stileLengthImp = result.StileLength
                stileLengthMet = ScaleManager.ToMetricInches(result.StileLength)
                panelHeightImp = result.PanelHeight
                panelHeightMet = ScaleManager.ToMetricInches(result.PanelHeight)
                panelWidthImp = result.PanelWidth
                panelWidthMet = ScaleManager.ToMetricInches(result.PanelWidth)
                doorWidthImp = result.DoorWidth
                doorWidthMet = ScaleManager.ToMetricInches(result.DoorWidth)
                doorHeightImp = result.DoorHeight
                doorHeightMet = ScaleManager.ToMetricInches(result.DoorHeight)
            Else
                ' Result is in metric, convert to imperial
                railLengthMet = result.RailLength
                railLengthImp = ScaleManager.ToImperialMillimeters(result.RailLength)
                stileLengthMet = result.StileLength
                stileLengthImp = ScaleManager.ToImperialMillimeters(result.StileLength)
                panelHeightMet = result.PanelHeight
                panelHeightImp = ScaleManager.ToImperialMillimeters(result.PanelHeight)
                panelWidthMet = result.PanelWidth
                panelWidthImp = ScaleManager.ToImperialMillimeters(result.PanelWidth)
                doorWidthMet = result.DoorWidth
                doorWidthImp = ScaleManager.ToImperialMillimeters(result.DoorWidth)
                doorHeightMet = result.DoorHeight
                doorHeightImp = ScaleManager.ToImperialMillimeters(result.DoorHeight)
            End If

            ' Show both units using existing labels
            UpdateLabelControl(LblRailLength, $"Rail Length: {railLengthImp:N3} in ({railLengthMet:N3} mm)")
            UpdateLabelControl(LblStileLength, $"Stile Length: {stileLengthImp:N3} in ({stileLengthMet:N3} mm)")
            UpdateLabelControl(LblPanelHeight, $"Panel Height: {panelHeightImp:N3} in ({panelHeightMet:N3} mm)")
            UpdateLabelControl(LblPanelWidth, $"Panel Width: {panelWidthImp:N3} in ({panelWidthMet:N3} mm)")

            ' Only update these if the labels exist
            If LblDoorWidth IsNot Nothing Then
                UpdateLabelControl(LblDoorWidth, $"Door Width: {doorWidthImp:N3} in ({doorWidthMet:N3} mm)")
            End If
            If LblDoorHeight IsNot Nothing Then
                UpdateLabelControl(LblDoorHeight, $"Door Height: {doorHeightImp:N3} in ({doorHeightMet:N3} mm)")
            End If
            If LblTotalMaterialArea IsNot Nothing Then
                UpdateLabelControl(LblTotalMaterialArea, $"Material Area: {result.TotalMaterialNeeded:N2} {result.AreaUnit}")
            End If

            ' Update detailed results
            UpdateTextControl(RtbDoorResults, result.Details)
        Catch ex As Exception
            MessageBox.Show($"Error displaying door results: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            If LblDoorWidth IsNot Nothing Then UpdateLabelControl(LblDoorWidth, "Door Width: --")
            If LblDoorHeight IsNot Nothing Then UpdateLabelControl(LblDoorHeight, "Door Height: --")
            UpdateTextControl(RtbDoorResults, "")
        Catch ex As Exception
            MessageBox.Show($"Error clearing door results: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    ' Add a click handler (ensure BtnDrawDoorImage exists in Designer)
    Private Sub BtnDrawDoorImage_Click(sender As Object, e As EventArgs) Handles BtnDrawDoorImage.Click
        If _lastDoorResult.IsValid Then
            _outputDrawingMode = OutputDrawingMode.Door
            PbOutputDrawing.Invalidate()
        Else
            MessageBox.Show("No valid door calculation to draw.", "Draw Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        End If
    End Sub

End Class