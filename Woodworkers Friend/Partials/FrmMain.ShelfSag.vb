' ============================================================================
' Last Updated: January 29, 2026
' Changes: Initial creation - Shelf sag calculator UI with material selector
'          and visual sag diagram
' ============================================================================

Partial Public Class FrmMain

#Region "Shelf Sag Calculator"

    ''' <summary>
    ''' Initializes shelf sag calculator
    ''' </summary>
    Private Sub InitializeShelfSagCalculator()
        Try
            ' Populate material type dropdown
            If CmbShelfMaterial IsNot Nothing Then
                CmbShelfMaterial.Items.Clear()
                For Each materialType As ShelfMaterialType In [Enum].GetValues(GetType(ShelfMaterialType))
                    Dim materialProps = ShelfSagCalculator.GetMaterialProperties(materialType)
                    CmbShelfMaterial.Items.Add(materialProps.Name)
                Next
                If CmbShelfMaterial.Items.Count > 0 Then
                    CmbShelfMaterial.SelectedIndex = 0  ' Default to Plywood
                End If
            End If

            ' Populate stiffener material dropdown
            If CmbStiffenerMaterial IsNot Nothing Then
                CmbStiffenerMaterial.Items.Clear()
                For Each materialType As ShelfMaterialType In [Enum].GetValues(GetType(ShelfMaterialType))
                    Dim materialProps = ShelfSagCalculator.GetMaterialProperties(materialType)
                    CmbStiffenerMaterial.Items.Add(materialProps.Name)
                Next
                If CmbStiffenerMaterial.Items.Count > 0 Then
                    CmbStiffenerMaterial.SelectedIndex = 4  ' Default to Oak
                End If
            End If

            ' Set default values
            If TxtShelfSpan IsNot Nothing Then TxtShelfSpan.Text = "36"        ' 36" span
            If TxtShelfLoad IsNot Nothing Then TxtShelfLoad.Text = "10"        ' 10 lbs load
            If TxtShelfThickness IsNot Nothing Then TxtShelfThickness.Text = "0.75"  ' 3/4" thick
            If TxtShelfWidth IsNot Nothing Then TxtShelfWidth.Text = "12"      ' 12" wide

            ' Set stiffener defaults
            If TxtStiffenerheight IsNot Nothing Then TxtStiffenerheight.Text = "1.5"  ' 1.5" high
            If TxtStiffenerThickness IsNot Nothing Then TxtStiffenerThickness.Text = "0.75"  ' 3/4" thick
            If ChkFrontStiffener IsNot Nothing Then ChkFrontStiffener.Checked = False
            If ChkBackStiffener IsNot Nothing Then ChkBackStiffener.Checked = False

            ' Set support type defaults - bracket is default
            If RbSupportBracket IsNot Nothing Then
                RbSupportBracket.Checked = True  ' Default to bracket
            ElseIf RbSupportPin IsNot Nothing Then
                RbSupportPin.Checked = False
            ElseIf RbSupportDado IsNot Nothing Then
                RbSupportDado.Checked = False
            End If

            If TxtshelfBracketWidth IsNot Nothing Then TxtshelfBracketWidth.Text = "0.75"  ' 0.75" per side
            If TxtPinWidth IsNot Nothing Then TxtPinWidth.Text = "0.375"  ' 3/8" diameter per pin
            If TxtDadoDepth1 IsNot Nothing Then TxtDadoDepth1.Text = "0.375"  ' 3/8" dado

            ' Setup tooltips for all controls
            SetupShelfSagTooltips()

            ' Update visibility based on support type
            UpdateSupportTypeVisibility()

            ' Initialize result labels
            ClearShelfSagResults()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeShelfSagCalculator")
        End Try
    End Sub

    ''' <summary>
    ''' Sets up tooltips for all shelf sag calculator controls
    ''' </summary>
    Private Sub SetupShelfSagTooltips()
        Try
            Dim tooltip As New ToolTip With {
                .AutoPopDelay = 10000,
                .InitialDelay = 500,
                .ReshowDelay = 200,
                .ShowAlways = True
            }

            ' Main shelf dimension inputs
            If CmbShelfMaterial IsNot Nothing Then
                tooltip.SetToolTip(CmbShelfMaterial,
                    "Select shelf material type." & vbCrLf &
                    "Different materials have different stiffness (modulus of elasticity)." & vbCrLf &
                    "Hardwoods are stiffer than plywood, which is stiffer than MDF.")
            End If

            If TxtShelfSpan IsNot Nothing Then
                tooltip.SetToolTip(TxtShelfSpan,
                    "Enter the TOTAL shelf length in inches (wall-to-wall or support-to-support)." & vbCrLf &
                    "For bracket/pin support, app subtracts support width automatically." & vbCrLf &
                    "Longer spans = more sag. Typical: 24-48 inches.")
            End If

            If TxtShelfLoad IsNot Nothing Then
                tooltip.SetToolTip(TxtShelfLoad,
                    "Enter expected total load on shelf in pounds." & vbCrLf &
                    "Books: ~10-20 lbs/foot, Heavy items: ~25+ lbs/foot" & vbCrLf &
                    "Include weight of items plus safety margin for dynamic loads.")
            End If

            If TxtShelfThickness IsNot Nothing Then
                tooltip.SetToolTip(TxtShelfThickness,
                    "Enter shelf thickness in inches." & vbCrLf &
                    "Common: 0.75 (3/4""), 1.0 (4/4), 0.5 (1/2"")" & vbCrLf &
                    "Thickness has HUGE impact - doubling thickness = 8x stiffer!")
            End If

            If TxtShelfWidth IsNot Nothing Then
                tooltip.SetToolTip(TxtShelfWidth,
                    "Enter shelf width/depth (front-to-back) in inches." & vbCrLf &
                    "Typical: 10-12"" for books, 16-24"" for storage" & vbCrLf &
                    "Wider shelves are proportionally stiffer.")
            End If

            ' Support type options
            If RbSupportBracket IsNot Nothing Then
                tooltip.SetToolTip(RbSupportBracket,
                    "Bracket/Cleat Support: Shelf sits on brackets or cleats." & vbCrLf &
                    "Simple support with free rotation at ends." & vbCrLf &
                    "Most common for adjustable shelves. Enter width of ONE bracket.")
            End If

            If RbSupportPin IsNot Nothing Then
                tooltip.SetToolTip(RbSupportPin,
                    "Pin Support: Shelf sits on cylindrical pins (shelf pins)." & vbCrLf &
                    "Common in adjustable shelving systems." & vbCrLf &
                    "Simple support, similar to brackets. Enter diameter of ONE pin.")
            End If

            If RbSupportDado IsNot Nothing Then
                tooltip.SetToolTip(RbSupportDado,
                    "Dado/Groove Support: Shelf sits in grooves cut into side panels." & vbCrLf &
                    "Provides partial fixity (20-40% sag reduction vs brackets)." & vbCrLf &
                    "Best for fixed shelves. Enter depth of groove.")
            End If

            ' Support-specific inputs
            If TxtshelfBracketWidth IsNot Nothing Then
                tooltip.SetToolTip(TxtshelfBracketWidth,
                    "Enter width of ONE bracket in inches (app multiplies by 2)." & vbCrLf &
                    "Typical: 0.5-0.75"" for small, 0.75-1.0"" for standard, 1.0-1.5"" for large" & vbCrLf &
                    "This width is subtracted from total span to get effective span.")
            End If

            If TxtPinWidth IsNot Nothing Then
                tooltip.SetToolTip(TxtPinWidth,
                    "Enter diameter of ONE pin/arrangement in inches (app multiplies by 2)." & vbCrLf &
                    "Typical shelf pins: 0.25-0.5"" diameter" & vbCrLf &
                    "For multiple pins, enter total width of pin arrangement on one side.")
            End If

            If TxtDadoDepth1 IsNot Nothing Then
                tooltip.SetToolTip(TxtDadoDepth1,
                    "Enter depth of dado groove in inches." & vbCrLf &
                    "Shallow (1/8-3/16""): ~10% fixity, Medium (1/4-3/8""): ~25% fixity" & vbCrLf &
                    "Deep (1/2""+): ~40% fixity. Must be less than shelf thickness!")
            End If

            ' Edge stiffeners
            If ChkFrontStiffener IsNot Nothing Then
                tooltip.SetToolTip(ChkFrontStiffener,
                    "Add a front edge stiffener (facing you)." & vbCrLf &
                    "Greatly increases shelf stiffness (60-80% sag reduction)." & vbCrLf &
                    "Creates a T-beam or I-beam cross-section.")
            End If

            If ChkBackStiffener IsNot Nothing Then
                tooltip.SetToolTip(ChkBackStiffener,
                    "Add a back edge stiffener (against wall)." & vbCrLf &
                    "Increases shelf stiffness, especially combined with front stiffener." & vbCrLf &
                    "Creates an I-beam cross-section for maximum strength.")
            End If

            If TxtStiffenerheight IsNot Nothing Then
                tooltip.SetToolTip(TxtStiffenerheight,
                    "Enter height of edge stiffener(s) in inches." & vbCrLf &
                    "Typical: 1.5-3.0"" depending on shelf thickness and span" & vbCrLf &
                    "Taller stiffeners provide more strength.")
            End If

            If TxtStiffenerThickness IsNot Nothing Then
                tooltip.SetToolTip(TxtStiffenerThickness,
                    "Enter thickness of edge stiffener(s) in inches." & vbCrLf &
                    "Typically matches shelf thickness (0.75"") or uses standard lumber." & vbCrLf &
                    "Thicker stiffeners add more strength.")
            End If

            If CmbStiffenerMaterial IsNot Nothing Then
                tooltip.SetToolTip(CmbStiffenerMaterial,
                    "Select material for edge stiffener(s)." & vbCrLf &
                    "Can be different from shelf material." & vbCrLf &
                    "Using hardwood stiffeners on plywood shelf adds significant strength.")
            End If

            ' Result display tooltips
            If LblShelfSagInches IsNot Nothing Then
                tooltip.SetToolTip(LblShelfSagInches,
                    "Expected sag/deflection at the center of the shelf under specified load." & vbCrLf &
                    "Acceptable: 1/360 of span, Maximum: 1/240 of span")
            End If

            If LblShelfSafeLoad IsNot Nothing Then
                tooltip.SetToolTip(LblShelfSafeLoad,
                    "Maximum load that produces acceptable sag (1/360 of span)." & vbCrLf &
                    "Conservative limit for long-term use without visible sag.")
            End If

            If LblShelfMaxLoad IsNot Nothing Then
                tooltip.SetToolTip(LblShelfMaxLoad,
                    "Absolute maximum load before structural limit (1/240 of span)." & vbCrLf &
                    "Not recommended for continuous use - risk of damage!")
            End If

            If LblShelfSafetyStatus IsNot Nothing Then
                tooltip.SetToolTip(LblShelfSafetyStatus,
                    "Safety factor = Safe Load ÷ Actual Load" & vbCrLf &
                    "1.0x = at limit, 1.5x = comfortable margin, 2.0x+ = very safe" & vbCrLf &
                    "Green = safe, Red = unsafe")
            End If

            If LblShelfMaxSpan IsNot Nothing Then
                tooltip.SetToolTip(LblShelfMaxSpan,
                    "Maximum recommended span for this material, thickness, and load." & vbCrLf &
                    "If your span exceeds this, consider thicker material or add support.")
            End If

            If PbShelfDiagram IsNot Nothing Then
                tooltip.SetToolTip(PbShelfDiagram,
                    "Visual representation of shelf deflection (sag exaggerated 50-200x)." & vbCrLf &
                    "Shows parabolic curve matching real-world beam deflection." & vbCrLf &
                    "Red = unsafe, Blue = safe")
            End If

        Catch ex As Exception
            ErrorHandler.LogError(ex, "SetupShelfSagTooltips")
        End Try
    End Sub

    ''' <summary>
    ''' Calculates shelf sag and safe loads
    ''' </summary>
    Private Sub CalculateShelfSag()
        Try
            ' Get material type
            Dim materialType = ShelfMaterialType.Plywood
            If CmbShelfMaterial IsNot Nothing AndAlso CmbShelfMaterial.SelectedIndex >= 0 Then
                materialType = CType(CmbShelfMaterial.SelectedIndex, ShelfMaterialType)
            End If

            ' Get inputs
            Dim span = InputValidator.TryParseDoubleWithDefault(TxtShelfSpan.Text, 36)
            Dim load = InputValidator.TryParseDoubleWithDefault(TxtShelfLoad.Text, 100)
            Dim thickness = InputValidator.TryParseDoubleWithDefault(TxtShelfThickness.Text, 0.75)
            Dim width = InputValidator.TryParseDoubleWithDefault(TxtShelfWidth.Text, 12)

            ' Get stiffener material
            Dim stiffenerMaterialType = ShelfMaterialType.SolidWoodOak
            If CmbStiffenerMaterial IsNot Nothing AndAlso CmbStiffenerMaterial.SelectedIndex >= 0 Then
                stiffenerMaterialType = CType(CmbStiffenerMaterial.SelectedIndex, ShelfMaterialType)
            End If

            ' Get stiffener inputs
            Dim hasFront = ChkFrontStiffener IsNot Nothing AndAlso ChkFrontStiffener.Checked
            Dim hasBack = ChkBackStiffener IsNot Nothing AndAlso ChkBackStiffener.Checked
            Dim stiffenerHeight = InputValidator.TryParseDoubleWithDefault(TxtStiffenerheight.Text, 1.5)
            Dim stiffenerThickness = InputValidator.TryParseDoubleWithDefault(TxtStiffenerThickness.Text, 0.75)

            ' Get support type
            Dim supportType = ShelfSupportType.Bracket
            If RbSupportPin IsNot Nothing AndAlso RbSupportPin.Checked Then
                supportType = ShelfSupportType.Pin
            ElseIf RbSupportDado IsNot Nothing AndAlso RbSupportDado.Checked Then
                supportType = ShelfSupportType.Dado
            End If

            ' Get support-type-specific inputs
            Dim bracketWidth = InputValidator.TryParseDoubleWithDefault(TxtshelfBracketWidth.Text, 0.75)
            Dim pinWidth = InputValidator.TryParseDoubleWithDefault(TxtPinWidth.Text, 0.375)
            Dim dadoDepth As Double
            If TxtDadoDepth1 IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(TxtDadoDepth1.Text) Then
                dadoDepth = InputValidator.TryParseDoubleWithDefault(TxtDadoDepth1.Text, 0.375)
                'ElseIf TxtDadoDepth2 IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(TxtDadoDepth2.Text) Then
                '    dadoDepth = InputValidator.TryParseDoubleWithDefault(TxtDadoDepth2.Text, 0.375)
            Else
                dadoDepth = 0.375
            End If

            ' Create input object
            Dim input As New ShelfSagInput With {
                .SpanLength = span,
                .Load = load,
                .MaterialType = materialType,
                .Thickness = thickness,
                .Width = width,
                .HasFrontStiffener = hasFront,
                .HasBackStiffener = hasBack,
                .StiffenerHeight = stiffenerHeight,
                .StiffenerThickness = stiffenerThickness,
                .StiffenerMaterial = stiffenerMaterialType,
                .SupportType = supportType,
                .BracketWidth = bracketWidth,
                .PinWidth = pinWidth,
                .DadoDepth = dadoDepth
            }

            ' Validate inputs
            Dim validation = ShelfSagCalculator.ValidateInput(input)
            If Not validation.IsValid Then
                MessageBox.Show(validation.ErrorMessage, "Invalid Input",
                              MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            ' Calculate sag
            Dim result = ShelfSagCalculator.CalculateShelfSag(input)

            ' Display results
            DisplayShelfSagResults(result, input)

            ' Draw visual diagram
            DrawShelfSagDiagram(result, input)
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "CalculateShelfSag", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Displays shelf sag calculation results
    ''' </summary>
    Private Sub DisplayShelfSagResults(result As ShelfSagResult, input As ShelfSagInput)
        Try
            ' Main sag display - combined inches and mm
            If LblShelfSagInches IsNot Nothing Then
                If LblShelfSagInches.Tag Is Nothing Then
                    LblShelfSagInches.Tag = "Expected Sag: {0:N4}"" ({1:N2} mm)"
                End If
                LblShelfSagInches.Text = String.Format(LblShelfSagInches.Tag.ToString(), result.SagInches, result.SagMillimeters)
            End If

            If LblShelfSagFraction IsNot Nothing Then
                LblShelfSagFraction.Text = $"Approximately {result.SagFraction}"
            End If

            ' Safe load display
            If LblShelfSafeLoad IsNot Nothing Then
                LblShelfSafeLoad.Text = $"Safe Load: {result.SafeLoad:N1} lbs"
            End If

            If LblShelfMaxLoad IsNot Nothing Then
                LblShelfMaxLoad.Text = $"Maximum Load: {result.MaxLoad:N1} lbs"
            End If

            ' Safety status
            If LblShelfSafetyStatus IsNot Nothing Then
                If result.IsSafe Then
                    LblShelfSafetyStatus.Text = $"Status: SAFE (Factor: {result.SafetyFactor:N2}x)"
                    LblShelfSafetyStatus.ForeColor = Color.DarkGreen
                Else
                    LblShelfSafetyStatus.Text = $"Status: UNSAFE (Factor: {result.SafetyFactor:N2}x)"
                    LblShelfSafetyStatus.ForeColor = Color.DarkRed
                End If
            End If

            ' Recommended max span
            If LblShelfMaxSpan IsNot Nothing Then
                LblShelfMaxSpan.Text = $"Recommended Max Span: {result.RecommendedMaxSpan:N1}"""
            End If

            ' Warning message
            If LblShelfWarning IsNot Nothing Then
                If Not String.IsNullOrEmpty(result.WarningMessage) Then
                    LblShelfWarning.Text = result.WarningMessage
                    LblShelfWarning.Visible = True
                    LblShelfWarning.ForeColor = If(result.WarningMessage.StartsWith("UNSAFE"), Color.DarkRed, Color.DarkOrange)
                Else
                    LblShelfWarning.Visible = False
                End If
            End If

            ' Material info
            If LblShelfMaterialInfo IsNot Nothing Then
                Dim materialProps = ShelfSagCalculator.GetMaterialProperties(input.MaterialType)
                LblShelfMaterialInfo.Text = $"Material E: {result.ModulusOfElasticity / 1000000:N2} million psi"
            End If

            ' Recommendations
            If LblShelfRecommendations IsNot Nothing Then
                Dim recommendations As New List(Of String)

                If Not result.IsSafe Then
                    recommendations.Add("• Reduce load, add center support, or use thicker/stiffer material")
                End If

                If input.SpanLength > result.RecommendedMaxSpan * 1.2 Then
                    recommendations.Add("• Consider reducing span or adding intermediate supports")
                End If

                If result.SafetyFactor < 1.5 And result.SafetyFactor >= 1.0 Then
                    recommendations.Add("• Safety factor is low - consider upgrading material")
                End If

                If recommendations.Count > 0 Then
                    LblShelfRecommendations.Text = "Recommendations:" & vbCrLf & String.Join(vbCrLf, recommendations)
                Else
                    LblShelfRecommendations.Text = "This configuration is within safe limits."
                End If
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DisplayShelfSagResults")
        End Try
    End Sub

    ''' <summary>
    ''' Draws a visual diagram of the shelf with exaggerated sag
    ''' </summary>
    Private Sub DrawShelfSagDiagram(result As ShelfSagResult, input As ShelfSagInput)
        If PbShelfDiagram Is Nothing Then Return

        Try
            Dim bmp As New Bitmap(PbShelfDiagram.Width, PbShelfDiagram.Height)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.White)
                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                ' Calculate drawing dimensions
                Dim margin = 40
                Dim diagramWidth = PbShelfDiagram.Width - (2 * margin)
                Dim diagramHeight = PbShelfDiagram.Height - (2 * margin)
                Dim baselineY = margin + diagramHeight * 0.4
                Dim supportHeight = 30

                ' Draw supports (walls/brackets)
                Using supportBrush As New SolidBrush(Color.Gray)
                    ' Left support
                    g.FillRectangle(supportBrush, CSng(margin - 10), CSng(baselineY), 10.0F, CSng(supportHeight))
                    ' Right support
                    g.FillRectangle(supportBrush, CSng(margin + diagramWidth), CSng(baselineY), 10.0F, CSng(supportHeight))
                End Using

                ' Draw shelf with sag curve
                ' Exaggerate the sag for visibility with reasonable scale (50-200x)
                Dim sagScale As Double
                If result.SagInches > 0 Then
                    ' Use moderate fixed scale, capped at reasonable limits
                    sagScale = Math.Min(200, Math.Max(50, diagramHeight * 0.15))
                Else
                    sagScale = 50
                End If
                Dim visualSag = result.SagInches * sagScale

                ' Create curved path for shelf using quadratic bezier
                Dim points As New List(Of PointF)
                Dim steps = 50
                For i = 0 To steps
                    Dim t = i / steps
                    Dim x = margin + (diagramWidth * t)
                    ' Parabolic curve: y = 4*sag*t*(1-t)
                    Dim sagOffset = 4 * visualSag * t * (1 - t)
                    Dim y = baselineY + sagOffset
                    points.Add(New PointF(CSng(x), CSng(y)))
                Next

                ' Draw the shelf
                Using shelfPen As New Pen(If(result.IsSafe, Color.DarkBlue, Color.DarkRed), 3)
                    g.DrawLines(shelfPen, points.ToArray())
                End Using

                ' Draw original straight line (no sag reference)
                Using dashedPen As New Pen(Color.LightGray, 1)
                    dashedPen.DashStyle = Drawing2D.DashStyle.Dash
                    g.DrawLine(dashedPen, CSng(margin), CSng(baselineY), CSng(margin + diagramWidth), CSng(baselineY))
                End Using

                ' Draw load indicator
                Dim loadX = margin + (diagramWidth / 2)
                Dim loadY = baselineY - 20
                Using loadPen As New Pen(Color.DarkGreen, 2)
                    loadPen.EndCap = Drawing2D.LineCap.ArrowAnchor
                    g.DrawLine(loadPen, CSng(loadX), CSng(loadY - 30), CSng(loadX), CSng(loadY))
                End Using

                ' Draw dimension annotations
                Using font As New Font("Arial", 8)
                    Using textBrush As New SolidBrush(Color.Black)
                        ' Span dimension
                        Dim spanText = $"Span: {input.SpanLength}"""
                        Dim spanSize = g.MeasureString(spanText, font)
                        g.DrawString(spanText, font, textBrush,
                               CSng(margin + (diagramWidth - spanSize.Width) / 2),
                               CSng(baselineY + supportHeight + 10))

                        ' Load annotation
                        g.DrawString($"{input.Load} lbs", font, textBrush,
                               CSng(loadX - 20), CSng(loadY - 45))

                        ' Sag annotation (at maximum sag point)
                        Dim maxSagY = baselineY + visualSag
                        Dim sagText = $"Sag: {result.SagInches:N4}"""
                        g.DrawString(sagText, font, textBrush,
                               CSng(loadX + 10), CSng(maxSagY))

                        ' Material and thickness
                        Dim materialProps = ShelfSagCalculator.GetMaterialProperties(input.MaterialType)
                        Dim infoText = $"{materialProps.Name}, {input.Thickness}"" thick"
                        g.DrawString(infoText, font, textBrush, CSng(margin), CSng(margin - 25))
                    End Using
                End Using

                ' Draw warning if unsafe
                If Not result.IsSafe Then
                    Using warnFont As New Font("Arial", 10, FontStyle.Bold)
                        Using warnBrush As New SolidBrush(Color.Red)
                            g.DrawString("⚠ UNSAFE", warnFont, warnBrush,
                                   CSng(PbShelfDiagram.Width - 80), CSng(margin - 25))
                        End Using
                    End Using
                End If
            End Using

            PbShelfDiagram.Image?.Dispose()
            PbShelfDiagram.Image = bmp
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DrawShelfSagDiagram")
        End Try
    End Sub

    ''' <summary>
    ''' Clears shelf sag results
    ''' </summary>
    Private Sub ClearShelfSagResults()
        Try
            ' Clear sag display - need 2 values (inches, mm) to match Tag format
            If LblShelfSagInches IsNot Nothing Then
                LblShelfSagInches.Text = "Expected Sag: -- (-- mm)"
            End If
            LabelFormatter.UpdateLabelWithFallback(LblShelfSagFraction, "Approximately --", 0)
            LabelFormatter.UpdateLabelWithFallback(LblShelfSafeLoad, "Safe Load: -- lbs", 0)
            LabelFormatter.UpdateLabelWithFallback(LblShelfMaxLoad, "Maximum Load: -- lbs", 0)
            LabelFormatter.UpdateLabelWithFallback(LblShelfSafetyStatus, "Status: --", 0)
            LabelFormatter.UpdateLabelWithFallback(LblShelfMaxSpan, "Recommended Max Span: --", 0)
            LabelFormatter.UpdateLabelWithFallback(LblShelfMaterialInfo, "Material E: --", 0)
            LabelFormatter.UpdateLabelWithFallback(LblShelfRecommendations, "", 0)

            If LblShelfWarning IsNot Nothing Then
                LblShelfWarning.Visible = False
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "ClearShelfSagResults")
        End Try
    End Sub

    ''' <summary>
    ''' Handles material selection change
    ''' </summary>
    Private Sub CmbShelfMaterial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbShelfMaterial.SelectedIndexChanged
        Try
            ' Auto-calculate when material changes
            If Not _loading Then
                CalculateShelfSag()
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "CmbShelfMaterial_SelectedIndexChanged")
        End Try
    End Sub

    ''' <summary>
    ''' Handles span input change - auto-calculate
    ''' </summary>
    Private Sub TxtShelfSpan_TextChanged(sender As Object, e As EventArgs) Handles TxtShelfSpan.TextChanged
        If Not _loading Then
            CalculateShelfSag()
        End If
    End Sub

    ''' <summary>
    ''' Handles load input change - auto-calculate
    ''' </summary>
    Private Sub TxtShelfLoad_TextChanged(sender As Object, e As EventArgs) Handles TxtShelfLoad.TextChanged
        If Not _loading Then
            CalculateShelfSag()
        End If
    End Sub

    ''' <summary>
    ''' Handles thickness input change - auto-calculate
    ''' </summary>
    Private Sub TxtShelfThickness_TextChanged(sender As Object, e As EventArgs) Handles TxtShelfThickness.TextChanged
        If Not _loading Then
            CalculateShelfSag()
        End If
    End Sub

    ''' <summary>
    ''' Handles width input change - auto-calculate
    ''' </summary>
    Private Sub TxtShelfWidth_TextChanged(sender As Object, e As EventArgs) Handles TxtShelfWidth.TextChanged
        If Not _loading Then
            CalculateShelfSag()
        End If
    End Sub

    ''' <summary>
    ''' Handles calculate button click
    ''' </summary>
    Private Sub BtnCalculateShelf_Click(sender As Object, e As EventArgs) Handles BtnCalculateShelf.Click
        CalculateShelfSag()
    End Sub

    ''' <summary>
    ''' Handles front stiffener checkbox change - auto-calculate
    ''' </summary>
    Private Sub ChkFrontStiffener_CheckedChanged(sender As Object, e As EventArgs) Handles ChkFrontStiffener.CheckedChanged
        If Not _loading Then
            CalculateShelfSag()
        End If
    End Sub

    ''' <summary>
    ''' Handles back stiffener checkbox change - auto-calculate
    ''' </summary>
    Private Sub ChkBackStiffener_CheckedChanged(sender As Object, e As EventArgs) Handles ChkBackStiffener.CheckedChanged
        If Not _loading Then
            CalculateShelfSag()
        End If
    End Sub

    ''' <summary>
    ''' Handles stiffener height input change - auto-calculate
    ''' </summary>
    Private Sub TxtStiffenerHeight_TextChanged(sender As Object, e As EventArgs) Handles TxtStiffenerheight.TextChanged
        If Not _loading Then
            CalculateShelfSag()
        End If
    End Sub

    ''' <summary>
    ''' Handles stiffener thickness input change - auto-calculate
    ''' </summary>
    Private Sub TxtStiffenerThickness_TextChanged(sender As Object, e As EventArgs) Handles TxtStiffenerThickness.TextChanged
        If Not _loading Then
            CalculateShelfSag()
        End If
    End Sub

    ''' <summary>
    ''' Handles stiffener material selection change - auto-calculate
    ''' </summary>
    Private Sub CmbStiffenerMaterial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CmbStiffenerMaterial.SelectedIndexChanged
        If Not _loading Then
            CalculateShelfSag()
        End If
    End Sub

    ''' <summary>
    ''' Handles support type selection change (bracket) - update visibility and recalculate
    ''' </summary>
    Private Sub RbSupportBracket_CheckedChanged(sender As Object, e As EventArgs) Handles RbSupportBracket.CheckedChanged
        If Not _loading Then
            UpdateSupportTypeVisibility()
            CalculateShelfSag()
        End If
    End Sub

    ''' <summary>
    ''' Handles support type selection change (pin) - update visibility and recalculate
    ''' </summary>
    Private Sub RbSupportPin_CheckedChanged(sender As Object, e As EventArgs) Handles RbSupportPin.CheckedChanged
        If Not _loading Then
            UpdateSupportTypeVisibility()
            CalculateShelfSag()
        End If
    End Sub

    ''' <summary>
    ''' Handles support type selection change (dado) - update visibility and recalculate
    ''' </summary>
    Private Sub RbSupportDado_CheckedChanged(sender As Object, e As EventArgs) Handles RbSupportDado.CheckedChanged
        If Not _loading Then
            UpdateSupportTypeVisibility()
            CalculateShelfSag()
        End If
    End Sub

    ''' <summary>
    ''' Handles bracket width input change - auto-calculate
    ''' </summary>
    Private Sub TxtShelfBracketWidth_TextChanged(sender As Object, e As EventArgs) Handles TxtshelfBracketWidth.TextChanged
        If Not _loading Then
            CalculateShelfSag()
        End If
    End Sub

    ''' <summary>
    ''' Handles pin width input change - auto-calculate
    ''' </summary>
    Private Sub TxtPinWidth_TextChanged(sender As Object, e As EventArgs) Handles TxtPinWidth.TextChanged
        If Not _loading Then
            CalculateShelfSag()
        End If
    End Sub

    '''' <summary>
    '''' Handles dado depth input change - auto-calculate
    '''' </summary>
    ''Private Sub TxtDadoDepth2_TextChanged(sender As Object, e As EventArgs) Handles TxtDadoDepth1.TextChanged
    '    If Not _loading Then
    '        CalculateShelfSag()
    '    End If
    'End Sub

    ''' <summary>
    ''' Handles dado depth input change (TxtDadoDepth1) - auto-calculate
    ''' </summary>
    Private Sub TxtDadoDepth1_TextChanged(sender As Object, e As EventArgs) Handles TxtDadoDepth1.TextChanged
        If Not _loading Then
            CalculateShelfSag()
        End If
    End Sub

    ''' <summary>
    ''' Updates readonly state of support type specific controls
    ''' </summary>
    Private Sub UpdateSupportTypeVisibility()
        Try
            Dim isBracketSelected = RbSupportBracket IsNot Nothing AndAlso RbSupportBracket.Checked
            Dim isPinSelected = RbSupportPin IsNot Nothing AndAlso RbSupportPin.Checked
            Dim isDadoSelected = RbSupportDado IsNot Nothing AndAlso RbSupportDado.Checked

            ' Enable/disable bracket controls
            If TxtshelfBracketWidth IsNot Nothing Then
                TxtshelfBracketWidth.ReadOnly = Not isBracketSelected
                TxtshelfBracketWidth.BackColor = If(isBracketSelected, SystemColors.Window, SystemColors.Control)
            End If

            ' Enable/disable pin controls
            If TxtPinWidth IsNot Nothing Then
                TxtPinWidth.ReadOnly = Not isPinSelected
                TxtPinWidth.BackColor = If(isPinSelected, SystemColors.Window, SystemColors.Control)
            End If

            ' Enable/disable dado controls (TxtDadoDepth1 version)
            If TxtDadoDepth1 IsNot Nothing Then
                TxtDadoDepth1.ReadOnly = Not isDadoSelected
                TxtDadoDepth1.BackColor = If(isDadoSelected, SystemColors.Window, SystemColors.Control)
            End If

            ' Enable/disable dado controls (TxtDadoDepth2 version - for backward compatibility)
            'If TxtDadoDepth2 IsNot Nothing Then
            '    TxtDadoDepth2.ReadOnly = Not isDadoSelected
            '    TxtDadoDepth2.BackColor = If(isDadoSelected, SystemColors.Window, SystemColors.Control)
            'End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "UpdateSupportTypeVisibility")
        End Try
    End Sub

#End Region

End Class
