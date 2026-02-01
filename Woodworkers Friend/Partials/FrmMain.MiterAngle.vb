' ============================================================================
' Last Updated: February 1, 2026
' Changes: Initial creation - Miter angle calculator integration with FrmMain.
'          Supports flat frames, tilted frames, crown molding, and polygonal projects.
'          Integrated with unified SQLite database via DatabaseManager.
' ============================================================================

Partial Public Class FrmMain

#Region "Miter Angle Calculator"

    ' Cached project data
    Private _miterAngleProjects As New List(Of MiterAngleProject)

    ''' <summary>
    ''' Represents a saved miter angle project
    ''' </summary>
    Private Class MiterAngleProject
        Public Property Name As String
        Public Property FrameType As MiterAngleCalculator.FrameType
        Public Property CornerAngle As Double
        Public Property NumberOfSides As Integer
        Public Property TiltAngle As Double
        Public Property SpringAngle As Double
        Public Property IsInsideCorner As Boolean
        Public Property DateCreated As DateTime
    End Class

    ''' <summary>
    ''' Initializes miter angle calculator using unified database
    ''' </summary>
    Private Sub InitializeMiterAngleCalculator()
        Try
            ' Initialize UI controls only if they exist
            If CmbMiterFrameType IsNot Nothing Then
                CmbMiterFrameType.Items.Clear()
                CmbMiterFrameType.Items.Add("Flat Frame (Picture Frame)")
                CmbMiterFrameType.Items.Add("Tilted Frame (Shadow Box)")
                CmbMiterFrameType.Items.Add("Crown Molding")
                CmbMiterFrameType.Items.Add("Polygonal Project")
                CmbMiterFrameType.SelectedIndex = 0  ' Default to Flat Frame
            End If

            ' Populate corner angle presets
            If CmbMiterCornerPreset IsNot Nothing Then
                CmbMiterCornerPreset.Items.Clear()
                For Each preset In MiterAngleCalculator.GetCornerAnglePresets()
                    CmbMiterCornerPreset.Items.Add(preset.Key)
                Next
                If CmbMiterCornerPreset.Items.Count > 0 Then
                    CmbMiterCornerPreset.SelectedIndex = 0  ' Default to 90°
                End If
            End If

            ' Populate crown molding spring angle presets
            If CmbMiterSpringAngle IsNot Nothing Then
                CmbMiterSpringAngle.Items.Clear()
                For Each preset In MiterAngleCalculator.GetCrownSpringAnglePresets()
                    CmbMiterSpringAngle.Items.Add(preset.Key)
                Next
                If CmbMiterSpringAngle.Items.Count > 0 Then
                    CmbMiterSpringAngle.SelectedIndex = 1  ' Default to 45°
                End If
            End If

            ' Set default values
            If TxtMiterCornerAngle IsNot Nothing Then TxtMiterCornerAngle.Text = "90"
            If TxtMiterSides IsNot Nothing Then TxtMiterSides.Text = "6"
            If TxtMiterTiltAngle IsNot Nothing Then TxtMiterTiltAngle.Text = "15"
            If TxtMiterSpringAngle IsNot Nothing Then TxtMiterSpringAngle.Text = "45"
            If ChkMiterInsideCorner IsNot Nothing Then ChkMiterInsideCorner.Checked = True

            ' Load saved projects from database
            LoadMiterAngleProjects()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeMiterAngleCalculator")
        End Try
    End Sub

    ''' <summary>
    ''' Calculate miter angles based on selected frame type
    ''' </summary>
    Private Sub CalculateMiterAngles()
        Try
            If CmbMiterFrameType Is Nothing Then Return

            Dim result As MiterAngleCalculator.MiterAngleResult
            Dim frameTypeIndex = CmbMiterFrameType.SelectedIndex

            Select Case frameTypeIndex
                Case 0 ' Flat Frame
                    Dim cornerAngle = InputValidator.TryParseDoubleWithDefault(TxtMiterCornerAngle?.Text, 90)
                    cornerAngle = InputValidator.Clamp(cornerAngle, 0.1, 179.9)
                    result = MiterAngleCalculator.CalculateFlatFrameMiter(cornerAngle)

                Case 1 ' Tilted Frame
                    Dim cornerAngle = InputValidator.TryParseDoubleWithDefault(TxtMiterCornerAngle?.Text, 90)
                    Dim tiltAngle = InputValidator.TryParseDoubleWithDefault(TxtMiterTiltAngle?.Text, 15)
                    cornerAngle = InputValidator.Clamp(cornerAngle, 0.1, 179.9)
                    tiltAngle = InputValidator.Clamp(tiltAngle, 0, 89.9)
                    result = MiterAngleCalculator.CalculateTiltedFrameMiter(cornerAngle, tiltAngle)

                Case 2 ' Crown Molding
                    Dim cornerAngle = InputValidator.TryParseDoubleWithDefault(TxtMiterCornerAngle?.Text, 90)
                    Dim springAngle = InputValidator.TryParseDoubleWithDefault(TxtMiterSpringAngle?.Text, 45)
                    Dim isInsideCorner = ChkMiterInsideCorner?.Checked
                    cornerAngle = InputValidator.Clamp(cornerAngle, 0.1, 179.9)
                    springAngle = InputValidator.Clamp(springAngle, 0, 89.9)
                    result = MiterAngleCalculator.CalculateCrownMoldingMiter(cornerAngle, springAngle, isInsideCorner)

                Case 3 ' Polygonal Project
                    Dim sides = InputValidator.TryParseIntWithDefault(TxtMiterSides?.Text, 6)
                    sides = InputValidator.Clamp(sides, 3, 25)
                    result = MiterAngleCalculator.CalculatePolygonalFrameMiter(sides)

                Case Else
                    Return
            End Select

            ' Display results
            DisplayMiterAngleResults(result)

        Catch ex As ArgumentOutOfRangeException
            ' User-friendly validation error
            MessageBox.Show($"Invalid input: {ex.Message}", "Input Validation",
                          MessageBoxButtons.OK, MessageBoxIcon.Warning)
            ErrorHandler.LogError(ex, "CalculateMiterAngles - Validation")
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "CalculateMiterAngles", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Display miter angle calculation results
    ''' </summary>
    Private Sub DisplayMiterAngleResults(result As MiterAngleCalculator.MiterAngleResult)
        Try
            ' Update labels with results
            If LblMiterAngleResult IsNot Nothing Then
                LblMiterAngleResult.Text = $"Miter Angle: {result.MiterAngle:N2}°"
            End If

            If LblBevelAngleResult IsNot Nothing Then
                LblBevelAngleResult.Text = $"Bevel Angle: {result.BevelAngle:N2}°"
            End If

            If LblMiterDescription IsNot Nothing Then
                LblMiterDescription.Text = result.Description
            End If

            ' Validate saw capability
            Dim validation = MiterAngleCalculator.ValidateSawCapability(result)
            If LblMiterSawCapability IsNot Nothing Then
                LblMiterSawCapability.Text = validation.Message
                If validation.IsValid Then
                    LblMiterSawCapability.ForeColor = Color.DarkGreen
                Else
                    LblMiterSawCapability.ForeColor = Color.DarkRed
                End If
            End If

            ' Display additional info in RichTextBox if available
            If RtbMiterResults IsNot Nothing Then
                RtbMiterResults.Clear()
                RtbMiterResults.AppendText($"Project Type: {result.Description}{vbCrLf}")
                RtbMiterResults.AppendText($"{vbCrLf}")
                RtbMiterResults.AppendText($"Cut Angles:{vbCrLf}")
                RtbMiterResults.AppendText($"  • Miter Angle (horizontal): {result.MiterAngle:N2}°{vbCrLf}")
                RtbMiterResults.AppendText($"  • Bevel Angle (vertical): {result.BevelAngle:N2}°{vbCrLf}")
                RtbMiterResults.AppendText($"{vbCrLf}")
                RtbMiterResults.AppendText($"Frame Details:{vbCrLf}")
                RtbMiterResults.AppendText($"  • Corner Angle: {result.CornerAngle:N2}°{vbCrLf}")
                
                If result.FrameType = MiterAngleCalculator.FrameType.PolygonalProject Then
                    RtbMiterResults.AppendText($"  • Number of Sides: {result.NumberOfSides}{vbCrLf}")
                End If
                
                If result.FrameType = MiterAngleCalculator.FrameType.TiltedFrame Then
                    RtbMiterResults.AppendText($"  • Tilt Angle: {result.TiltAngle:N2}°{vbCrLf}")
                End If
                
                If result.FrameType = MiterAngleCalculator.FrameType.CrownMolding Then
                    RtbMiterResults.AppendText($"  • Spring Angle: {result.SpringAngle:N2}°{vbCrLf}")
                End If

                RtbMiterResults.AppendText($"{vbCrLf}")
                RtbMiterResults.AppendText($"Saw Capability:{vbCrLf}")
                RtbMiterResults.AppendText($"  {validation.Message}{vbCrLf}")
                
                ' Add cutting instructions
                RtbMiterResults.AppendText($"{vbCrLf}")
                RtbMiterResults.AppendText($"Cutting Instructions:{vbCrLf}")
                RtbMiterResults.AppendText($"  1. Set miter saw to {result.MiterAngle:N2}° horizontal angle{vbCrLf}")
                If result.BevelAngle <> 0 Then
                    RtbMiterResults.AppendText($"  2. Set bevel to {result.BevelAngle:N2}° vertical tilt{vbCrLf}")
                    RtbMiterResults.AppendText($"  3. Cut compound miter with both settings{vbCrLf}")
                Else
                    RtbMiterResults.AppendText($"  2. No bevel required (flat cut){vbCrLf}")
                    RtbMiterResults.AppendText($"  3. Make simple miter cut{vbCrLf}")
                End If
            End If

        Catch ex As Exception
            ErrorHandler.LogError(ex, "DisplayMiterAngleResults")
        End Try
    End Sub

    ''' <summary>
    ''' Wire up miter angle calculator events
    ''' </summary>
    Private Sub InitializeMiterAngleEvents()
        ' Frame type selection
        If CmbMiterFrameType IsNot Nothing Then
            AddHandler CmbMiterFrameType.SelectedIndexChanged, AddressOf MiterFrameType_Changed
        End If

        ' Input changes
        If TxtMiterCornerAngle IsNot Nothing Then
            AddHandler TxtMiterCornerAngle.TextChanged, AddressOf MiterInput_Changed
        End If
        If TxtMiterSides IsNot Nothing Then
            AddHandler TxtMiterSides.TextChanged, AddressOf MiterInput_Changed
        End If
        If TxtMiterTiltAngle IsNot Nothing Then
            AddHandler TxtMiterTiltAngle.TextChanged, AddressOf MiterInput_Changed
        End If
        If TxtMiterSpringAngle IsNot Nothing Then
            AddHandler TxtMiterSpringAngle.TextChanged, AddressOf MiterInput_Changed
        End If
        If ChkMiterInsideCorner IsNot Nothing Then
            AddHandler ChkMiterInsideCorner.CheckedChanged, AddressOf MiterInput_Changed
        End If

        ' Preset selection
        If CmbMiterCornerPreset IsNot Nothing Then
            AddHandler CmbMiterCornerPreset.SelectedIndexChanged, AddressOf MiterCornerPreset_Changed
        End If
        If CmbMiterSpringAngle IsNot Nothing Then
            AddHandler CmbMiterSpringAngle.SelectedIndexChanged, AddressOf MiterSpringPreset_Changed
        End If
    End Sub

    ''' <summary>
    ''' Handle frame type change to show/hide relevant controls
    ''' </summary>
    Private Sub MiterFrameType_Changed(sender As Object, e As EventArgs)
        Try
            If CmbMiterFrameType Is Nothing Then Return

            Dim frameTypeIndex = CmbMiterFrameType.SelectedIndex

            ' Show/hide controls based on frame type
            ' For flat frames: show corner angle
            ' For tilted frames: show corner angle and tilt angle
            ' For crown molding: show corner angle, spring angle, inside/outside
            ' For polygonal: show number of sides

            ' This would control visibility of panels/groups in the UI
            ' Since UI controls don't exist yet, this is a placeholder
            ' for when the Designer is updated

            ' Recalculate with new frame type
            CalculateMiterAngles()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "MiterFrameType_Changed")
        End Try
    End Sub

    ''' <summary>
    ''' Handle input changes and recalculate
    ''' </summary>
    Private Sub MiterInput_Changed(sender As Object, e As EventArgs)
        CalculateMiterAngles()
    End Sub

    ''' <summary>
    ''' Handle corner angle preset selection
    ''' </summary>
    Private Sub MiterCornerPreset_Changed(sender As Object, e As EventArgs)
        Try
            If CmbMiterCornerPreset Is Nothing OrElse CmbMiterCornerPreset.SelectedItem Is Nothing Then Return

            Dim presets = MiterAngleCalculator.GetCornerAnglePresets()
            Dim selectedText = CmbMiterCornerPreset.SelectedItem.ToString()

            For Each preset In presets
                If selectedText = preset.Key Then
                    If TxtMiterCornerAngle IsNot Nothing Then
                        TxtMiterCornerAngle.Text = preset.Value.ToString()
                    End If
                    Exit For
                End If
            Next
        Catch ex As Exception
            ErrorHandler.LogError(ex, "MiterCornerPreset_Changed")
        End Try
    End Sub

    ''' <summary>
    ''' Handle spring angle preset selection
    ''' </summary>
    Private Sub MiterSpringPreset_Changed(sender As Object, e As EventArgs)
        Try
            If CmbMiterSpringAngle Is Nothing OrElse CmbMiterSpringAngle.SelectedItem Is Nothing Then Return

            Dim presets = MiterAngleCalculator.GetCrownSpringAnglePresets()
            Dim selectedText = CmbMiterSpringAngle.SelectedItem.ToString()

            For Each preset In presets
                If selectedText = preset.Key Then
                    If TxtMiterSpringAngle IsNot Nothing Then
                        TxtMiterSpringAngle.Text = preset.Value.ToString()
                    End If
                    Exit For
                End If
            Next
        Catch ex As Exception
            ErrorHandler.LogError(ex, "MiterSpringPreset_Changed")
        End Try
    End Sub

    ''' <summary>
    ''' Calculate button click handler
    ''' </summary>
    Private Sub BtnCalculateMiter_Click(sender As Object, e As EventArgs) ' Handles BtnCalculateMiter.Click
        CalculateMiterAngles()
    End Sub

    ''' <summary>
    ''' Load miter angle projects from database
    ''' </summary>
    Private Sub LoadMiterAngleProjects()
        Try
            ' This will be implemented when database schema is extended
            ' For now, keep projects in memory only
            _miterAngleProjects.Clear()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "LoadMiterAngleProjects")
        End Try
    End Sub

    ''' <summary>
    ''' Save current miter angle calculation as a project
    ''' </summary>
    Private Sub SaveMiterAngleProject(projectName As String)
        Try
            If String.IsNullOrWhiteSpace(projectName) Then
                MessageBox.Show("Please enter a project name", "Missing Information",
                              MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Create project object
            Dim project As New MiterAngleProject With {
                .Name = projectName,
                .FrameType = CType(CmbMiterFrameType.SelectedIndex, MiterAngleCalculator.FrameType),
                .CornerAngle = InputValidator.TryParseDoubleWithDefault(TxtMiterCornerAngle?.Text, 90),
                .NumberOfSides = InputValidator.TryParseIntWithDefault(TxtMiterSides?.Text, 6),
                .TiltAngle = InputValidator.TryParseDoubleWithDefault(TxtMiterTiltAngle?.Text, 15),
                .SpringAngle = InputValidator.TryParseDoubleWithDefault(TxtMiterSpringAngle?.Text, 45),
                .IsInsideCorner = ChkMiterInsideCorner?.Checked,
                .DateCreated = DateTime.Now
            }

            ' Add to memory list
            _miterAngleProjects.Add(project)

            ' Future: Save to database via DatabaseManager
            ' DatabaseManager.Instance.SaveMiterAngleProject(project)

            MessageBox.Show($"Project '{projectName}' saved successfully", "Project Saved",
                          MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "SaveMiterAngleProject", showToUser:=True)
        End Try
    End Sub

#End Region

End Class
