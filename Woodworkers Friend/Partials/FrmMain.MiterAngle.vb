' ============================================================================
' Last Updated: January 31, 2026
' Changes: Initial implementation of Miter Angle Calculator
'          Calculates miter and bevel angles for flat and tilted frames
'          Supports polygonal projects including crown molding and picture frames
' ============================================================================

Partial Public Class FrmMain

#Region "Miter Angle Calculator"

#Region "Constants"

    Private Const MIN_MITER_SIDES As Integer = 3
    Private Const MAX_MITER_SIDES As Integer = 24
    Private Const DEFAULT_MITER_SIDES As Integer = 4
    Private Const DEFAULT_TILT_ANGLE As Decimal = 38D ' Common for crown molding
    Private Const MIN_TILT_ANGLE As Decimal = 0D
    Private Const MAX_TILT_ANGLE As Decimal = 90D

#End Region

#Region "Module Variables"

    Private _miterCalculatorInitialized As Boolean = False
    Private _suppressMiterCalculation As Boolean = False

#End Region

#Region "Initialization"

    ''' <summary>
    ''' Initializes the miter angle calculator when the Angles tab is entered
    ''' </summary>
    Private Sub InitializeMiterAngleCalculator()
        If _miterCalculatorInitialized Then Return

        Try
            _suppressMiterCalculation = True

            ' Set default values
            If TxtMiterNumSides IsNot Nothing Then
                TxtMiterNumSides.Text = DEFAULT_MITER_SIDES.ToString()
            End If

            If TxtMiterTiltAngle IsNot Nothing Then
                TxtMiterTiltAngle.Text = DEFAULT_TILT_ANGLE.ToString("0.0")
                TxtMiterTiltAngle.Enabled = False ' Disabled by default for flat frames
            End If

            If TxtMiterMaterialThickness IsNot Nothing Then
                TxtMiterMaterialThickness.Text = "0.75"
            End If

            ' Set default frame type to Flat
            If RbMiterFrameFlat IsNot Nothing Then
                RbMiterFrameFlat.Checked = True
            End If

            ' Set tooltips
            SetupMiterTooltips()

            ' Initialize display
            ClearMiterResults()

            _miterCalculatorInitialized = True

        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeMiterAngleCalculator")
        Finally
            _suppressMiterCalculation = False
        End Try

        ' Perform initial calculation with default values
        ' Use BeginInvoke to ensure UI is fully initialized
        If TxtMiterNumSides IsNot Nothing Then
            BeginInvoke(Sub() CalculateMiterAngles())
        End If
    End Sub

    ''' <summary>
    ''' Sets up tooltips for miter angle calculator controls
    ''' </summary>
    Private Sub SetupMiterTooltips()
        Try
            If tTip IsNot Nothing Then
                tTip.SetToolTip(TxtMiterNumSides, "Enter the number of sides for your polygon (3-24). Common: 4=Square, 6=Hexagon, 8=Octagon")
                tTip.SetToolTip(RbMiterFrameFlat, "Standard flat frame (picture frames, table tops)")
                tTip.SetToolTip(RbMiterFrameTilted, "Tilted frame (crown molding, cove molding)")
                tTip.SetToolTip(TxtMiterTiltAngle, "Spring angle for crown molding. Common: 38° (52/38), 45° (45/45)")
                tTip.SetToolTip(TxtMiterMaterialThickness, "Thickness of material (optional, for reference)")
                tTip.SetToolTip(LblMiterSawAngle, "Set your miter saw to this angle")
                tTip.SetToolTip(LblMiterBevelAngle, "Set your saw blade bevel to this angle (for compound cuts)")
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SetupMiterTooltips")
        End Try
    End Sub

#End Region

#Region "Event Handlers"

    ''' <summary>
    ''' Handles text change in number of sides textbox
    ''' </summary>
    Private Sub TxtMiterNumSides_TextChanged(sender As Object, e As EventArgs) Handles TxtMiterNumSides.TextChanged
        If _suppressMiterCalculation Then Return

        Try
            If String.IsNullOrWhiteSpace(TxtMiterNumSides.Text) Then
                ClearMiterResults()
                Return
            End If

            Dim sides As Integer
            If Integer.TryParse(TxtMiterNumSides.Text, sides) Then
                If sides < MIN_MITER_SIDES Then
                    TxtMiterNumSides.Text = MIN_MITER_SIDES.ToString()
                    TxtMiterNumSides.SelectAll()
                    Return
                ElseIf sides > MAX_MITER_SIDES Then
                    TxtMiterNumSides.Text = MAX_MITER_SIDES.ToString()
                    TxtMiterNumSides.SelectAll()
                    Return
                End If

                CalculateMiterAngles()
            Else
                ClearMiterResults()
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "TxtMiterNumSides_TextChanged")
        End Try
    End Sub

    ''' <summary>
    ''' Handles text change in tilt angle textbox
    ''' </summary>
    Private Sub TxtMiterTiltAngle_TextChanged(sender As Object, e As EventArgs) Handles TxtMiterTiltAngle.TextChanged
        If _suppressMiterCalculation OrElse Not RbMiterFrameTilted.Checked Then Return

        Try
            If String.IsNullOrWhiteSpace(TxtMiterTiltAngle.Text) Then
                Return
            End If

            Dim tiltAngle As Decimal
            If Decimal.TryParse(TxtMiterTiltAngle.Text, tiltAngle) Then
                If tiltAngle < MIN_TILT_ANGLE Then
                    TxtMiterTiltAngle.Text = MIN_TILT_ANGLE.ToString("0.0")
                    TxtMiterTiltAngle.SelectAll()
                    Return
                ElseIf tiltAngle > MAX_TILT_ANGLE Then
                    TxtMiterTiltAngle.Text = MAX_TILT_ANGLE.ToString("0.0")
                    TxtMiterTiltAngle.SelectAll()
                    Return
                End If

                CalculateMiterAngles()
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "TxtMiterTiltAngle_TextChanged")
        End Try
    End Sub

    ''' <summary>
    ''' Handles frame type radio button changes
    ''' </summary>
    Private Sub RbMiterFrame_CheckedChanged(sender As Object, e As EventArgs) Handles RbMiterFrameFlat.CheckedChanged, RbMiterFrameTilted.CheckedChanged
        If _suppressMiterCalculation Then Return

        Try
            ' Enable/disable tilt angle based on frame type
            If TxtMiterTiltAngle IsNot Nothing Then
                TxtMiterTiltAngle.Enabled = RbMiterFrameTilted.Checked

                If RbMiterFrameFlat.Checked Then
                    TxtMiterTiltAngle.Text = "0.0"
                ElseIf String.IsNullOrWhiteSpace(TxtMiterTiltAngle.Text) OrElse TxtMiterTiltAngle.Text = "0.0" Then
                    TxtMiterTiltAngle.Text = DEFAULT_TILT_ANGLE.ToString("0.0")
                End If
            End If

            CalculateMiterAngles()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "RbMiterFrame_CheckedChanged")
        End Try
    End Sub

    ''' <summary>
    ''' Handles Angles tab entry to initialize calculator
    ''' </summary>
    Private Sub TpAngles_Enter(sender As Object, e As EventArgs) Handles TpAngles.Enter
        If Not _miterCalculatorInitialized Then
            InitializeMiterAngleCalculator()
        End If
    End Sub

#End Region

#Region "Calculation Logic"

    ''' <summary>
    ''' Calculates miter and bevel angles based on current inputs
    ''' </summary>
    Private Sub CalculateMiterAngles()
        If _suppressMiterCalculation Then Return

        Try
            ' Validate required controls exist
            If TxtMiterNumSides Is Nothing OrElse RbMiterFrameFlat Is Nothing OrElse RbMiterFrameTilted Is Nothing Then
                Return
            End If

            ' Validate inputs
            If String.IsNullOrWhiteSpace(TxtMiterNumSides.Text) Then
                ClearMiterResults()
                Return
            End If

            Dim numSides As Integer
            If Not Integer.TryParse(TxtMiterNumSides.Text, numSides) Then
                ClearMiterResults()
                Return
            End If

            If numSides < MIN_MITER_SIDES OrElse numSides > MAX_MITER_SIDES Then
                ClearMiterResults()
                Return
            End If

            Dim isFlat As Boolean = RbMiterFrameFlat.Checked
            Dim tiltAngle As Decimal = 0D

            If Not isFlat Then
                If TxtMiterTiltAngle Is Nothing OrElse String.IsNullOrWhiteSpace(TxtMiterTiltAngle.Text) Then
                    ClearMiterResults()
                    Return
                End If

                If Not Decimal.TryParse(TxtMiterTiltAngle.Text, tiltAngle) Then
                    ClearMiterResults()
                    Return
                End If
            End If

            ' Calculate angles
            Dim result = CalculateMiterAnglesCore(numSides, isFlat, tiltAngle)

            ' Display results
            DisplayMiterResults(result)
        Catch ex As Exception
            ErrorHandler.LogError(ex, "CalculateMiterAngles")
            ClearMiterResults()
        End Try
    End Sub

    ''' <summary>
    ''' Core calculation logic for miter angles
    ''' </summary>
    Private Function CalculateMiterAnglesCore(numSides As Integer, isFlat As Boolean, tiltAngleDegrees As Decimal) As MiterAngleResult
        ' Convert to radians for calculations
        Dim tiltAngleRad As Double = CDbl(tiltAngleDegrees) * Math.PI / 180.0

        ' Calculate interior angle of polygon
        Dim interiorAngleDeg As Double = (numSides - 2) * 180.0 / numSides

        ' Calculate miter angle (half of the corner angle)
        Dim cornerAngleDeg As Double = 180.0 - interiorAngleDeg
        Dim miterAngleDeg As Double = cornerAngleDeg / 2.0

        Dim bevelAngleDeg As Double = 0

        If Not isFlat AndAlso tiltAngleDegrees > 0 Then
            ' For tilted frames (compound miter cuts)
            ' Use the compound miter formula
            Dim miterAngleRad As Double = miterAngleDeg * Math.PI / 180.0

            ' Calculate compound miter angle
            Dim compoundMiterRad As Double = Math.Atan(Math.Cos(tiltAngleRad) * Math.Tan(miterAngleRad))
            miterAngleDeg = compoundMiterRad * 180.0 / Math.PI

            ' Calculate bevel angle
            Dim bevelRad As Double = Math.Asin(Math.Sin(tiltAngleRad) * Math.Sin(miterAngleRad))
            bevelAngleDeg = bevelRad * 180.0 / Math.PI
        End If

        ' Calculate complementary angle (for reference)
        Dim complementaryAngleDeg As Double = 90.0 - miterAngleDeg

        Return New MiterAngleResult With {
            .NumSides = numSides,
            .IsFlat = isFlat,
            .TiltAngle = tiltAngleDegrees,
            .InteriorAngle = interiorAngleDeg,
            .MiterAngle = miterAngleDeg,
            .BevelAngle = bevelAngleDeg,
            .ComplementaryAngle = complementaryAngleDeg
        }
    End Function

    ''' <summary>
    ''' Displays calculated miter angle results
    ''' </summary>
    Private Sub DisplayMiterResults(result As MiterAngleResult)
        Try
            ' Display miter angle (saw angle)
            If LblMiterSawAngle IsNot Nothing Then
                LblMiterSawAngle.Text = $"Miter (Saw) Angle: {result.MiterAngle:F2}°"
                LblMiterSawAngle.Font = New Font(LblMiterSawAngle.Font, FontStyle.Bold)
            End If

            ' Display bevel angle
            If LblMiterBevelAngle IsNot Nothing Then
                If result.IsFlat Then
                    LblMiterBevelAngle.Text = "Bevel Angle: 0° (not needed for flat frames)"
                    LblMiterBevelAngle.Font = New Font(LblMiterBevelAngle.Font, FontStyle.Regular)
                Else
                    LblMiterBevelAngle.Text = $"Bevel Angle: {result.BevelAngle:F2}°"
                    LblMiterBevelAngle.Font = New Font(LblMiterBevelAngle.Font, FontStyle.Bold)
                End If
            End If

            ' Display complementary angle
            If LblComplementaryAngle IsNot Nothing Then
                LblComplementaryAngle.Text = $"Complementary: {result.ComplementaryAngle:F2}° | Interior: {result.InteriorAngle:F2}°"
                LblComplementaryAngle.Font = New Font(LblComplementaryAngle.Font, FontStyle.Regular)
            End If

            ' Update diagram if PictureBox exists
            If PictureBox1 IsNot Nothing Then
                DrawMiterDiagram(result)
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DisplayMiterResults")
        End Try
    End Sub

    ''' <summary>
    ''' Clears all miter angle result displays
    ''' </summary>
    Private Sub ClearMiterResults()
        Try
            If LblMiterSawAngle IsNot Nothing Then
                LblMiterSawAngle.Text = "Miter (Saw) Angle: --"
                LblMiterSawAngle.Font = New Font(LblMiterSawAngle.Font, FontStyle.Regular)
            End If

            If LblMiterBevelAngle IsNot Nothing Then
                LblMiterBevelAngle.Text = "Bevel Angle: --"
                LblMiterBevelAngle.Font = New Font(LblMiterBevelAngle.Font, FontStyle.Regular)
            End If

            If LblComplementaryAngle IsNot Nothing Then
                LblComplementaryAngle.Text = "Complementary: -- | Interior: --"
                LblComplementaryAngle.Font = New Font(LblComplementaryAngle.Font, FontStyle.Regular)
            End If

            If PictureBox1 IsNot Nothing Then
                PictureBox1.Image = Nothing
                PictureBox1.Refresh()
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "ClearMiterResults")
        End Try
    End Sub

#End Region

#Region "Diagram Drawing"

    ''' <summary>
    ''' Draws a visual diagram of the miter cut
    ''' </summary>
    Private Sub DrawMiterDiagram(result As MiterAngleResult)
        Try
            If PictureBox1 Is Nothing OrElse PictureBox1.Width <= 0 OrElse PictureBox1.Height <= 0 Then
                Return
            End If

            Dim bmp As New Bitmap(PictureBox1.Width, PictureBox1.Height)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
                g.Clear(Color.White)

                ' Draw a simple miter joint diagram
                Dim centerX As Single = bmp.Width / 2.0F
                Dim centerY As Single = bmp.Height / 2.0F
                Dim armLength As Single = Math.Min(bmp.Width, bmp.Height) * 0.35F

                ' Calculate angle in radians
                Dim angleRad As Single = CSng(result.MiterAngle * Math.PI / 180.0)

                ' Draw two pieces meeting at miter angle
                Using penBoard As New Pen(Color.SaddleBrown, 3)
                    Using penCut As New Pen(Color.Red, 2) With {.DashStyle = Drawing2D.DashStyle.Dash}
                        Using brushText As New SolidBrush(Color.Black)
                            Using font As New Font("Segoe UI", 9, FontStyle.Bold)

                                ' Left piece
                                Dim leftX1 As Single = centerX - armLength * CSng(Math.Cos(angleRad))
                                Dim leftY1 As Single = centerY - armLength * CSng(Math.Sin(angleRad))
                                g.DrawLine(penBoard, centerX, centerY, leftX1, leftY1)

                                ' Right piece
                                Dim rightX1 As Single = centerX + armLength * CSng(Math.Cos(angleRad))
                                Dim rightY1 As Single = centerY - armLength * CSng(Math.Sin(angleRad))
                                g.DrawLine(penBoard, centerX, centerY, rightX1, rightY1)

                                ' Draw cut line (miter)
                                g.DrawLine(penCut, centerX - 15, centerY, centerX + 15, centerY)

                                ' Draw angle arc
                                Dim arcRect As New RectangleF(centerX - 30, centerY - 30, 60, 60)
                                Dim startAngle As Single = 180 - CSng(result.MiterAngle)
                                Dim sweepAngle As Single = CSng(result.MiterAngle * 2)
                                Using penArc As New Pen(Color.Blue, 1.5F)
                                    g.DrawArc(penArc, arcRect, startAngle, sweepAngle)
                                End Using

                                ' Label the angle
                                Dim angleText As String = $"{result.MiterAngle:F1}°"
                                Dim textSize As SizeF = g.MeasureString(angleText, font)
                                g.DrawString(angleText, font, brushText,
                                centerX - textSize.Width / 2,
                                centerY + 35)

                                ' Add frame type label
                                Dim frameType As String = If(result.IsFlat, "Flat Frame", $"Tilted Frame ({result.TiltAngle:F1}°)")
                                g.DrawString(frameType, font, brushText, 5, 5)

                                ' Add sides label
                                Dim sidesText As String = $"{result.NumSides} Sides"
                                g.DrawString(sidesText, font, brushText, 5, bmp.Height - 25)

                            End Using
                        End Using
                    End Using
                End Using
            End Using

            PictureBox1.Image = bmp
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DrawMiterDiagram")
        End Try
    End Sub

#End Region

#End Region

#Region "Support Classes"

    ''' <summary>
    ''' Result structure for miter angle calculations
    ''' </summary>
    Private Structure MiterAngleResult
        Public NumSides As Integer
        Public IsFlat As Boolean
        Public TiltAngle As Decimal
        Public InteriorAngle As Double
        Public MiterAngle As Double
        Public BevelAngle As Double
        Public ComplementaryAngle As Double
    End Structure

#End Region

End Class
