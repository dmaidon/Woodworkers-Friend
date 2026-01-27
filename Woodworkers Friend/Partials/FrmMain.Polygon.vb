Partial Public Class FrmMain

#Region "Polygon Calculations"

#Region "Constants"

    Private Const MIN_POLYGON_SIDES As Integer = 3
    Private Const MAX_POLYGON_SIDES As Integer = 25
    Private Const DEFAULT_ROTATION_SPEED As Double = 0.75
    Private Const ANIMATION_FULL_CIRCLE As Double = 360.0
    Private Const MARGIN_SIZE As Single = 20.0F
    Private Const EXTRUDE_OFFSET_X As Single = 10.0F
    Private Const EXTRUDE_OFFSET_Y As Single = 10.0F
    Private Const SHADOW_OFFSET_X As Single = 5.0F
    Private Const SHADOW_OFFSET_Y As Single = 5.0F
    Private Const VERTEX_MARKER_RADIUS As Single = 3.0F
    Private Const ARC_RADIUS As Single = 15.0F
    Private Const SHADOW_ALPHA As Integer = 80
    Private Const LIGHTING_BASE As Single = 0.5F
    Private Const LIGHTING_VARIATION As Single = 0.5F

#End Region

#Region "Configuration Structure"

    Friend Structure PolygonConfig
        Public AnimationSpeed As Double
        Public ShowShadow As Boolean
        Public ShowVertexMarkers As Boolean
        Public ShowSideLabels As Boolean
        Public ShowAngleArc As Boolean
        Public ShowCenterText As Boolean
        Public EnableLighting As Boolean
        Public TopFaceColor1 As Color
        Public TopFaceColor2 As Color
        Public SideColor As Color
        Public OutlineColor As Color
        Public VertexColor As Color
        Public TextColor As Color
        Public ArcColor As Color

        Public Shared Function GetDefault() As PolygonConfig
            Return New PolygonConfig With {
                .AnimationSpeed = DEFAULT_ROTATION_SPEED,
                .ShowShadow = True,
                .ShowVertexMarkers = True,
                .ShowSideLabels = True,
                .ShowAngleArc = True,
                .ShowCenterText = True,
                .EnableLighting = True,
                .TopFaceColor1 = Color.LightSkyBlue,
                .TopFaceColor2 = Color.DeepSkyBlue,
                .SideColor = Color.DeepSkyBlue,
                .OutlineColor = Color.DarkBlue,
                .VertexColor = Color.Red,
                .TextColor = Color.Black,
                .ArcColor = Color.Red
            }
        End Function

    End Structure

#End Region

#Region "Module Variables"

    Friend currentRotationAngle As Double = 30.0
    Friend currentConfig As PolygonConfig = PolygonConfig.GetDefault()
    Private cachedNSides As Integer = 0
    Private cachedPoints() As PointF
    Private cachedCenter As PointF
    Private cachedRadius As Single
    Friend RotationEnabled As Boolean = True

#End Region

    Private Sub TxtPolygonSides_TextChanged(sender As Object, e As EventArgs) Handles TxtPolygonSides.TextChanged
        Try
            ' Handle empty or whitespace
            If String.IsNullOrWhiteSpace(TxtPolygonSides.Text) Then
                Return ' Let user continue typing
            End If

            Dim inputValue As Integer
            If Not Integer.TryParse(TxtPolygonSides.Text, inputValue) Then
                ' Invalid input - reset to minimum and prevent infinite recursion
                RemoveHandler TxtPolygonSides.TextChanged, AddressOf TxtPolygonSides_TextChanged
                TxtPolygonSides.Text = MIN_POLYGON_SIDES.ToString()
                TxtPolygonSides.SelectionStart = TxtPolygonSides.Text.Length
                AddHandler TxtPolygonSides.TextChanged, AddressOf TxtPolygonSides_TextChanged
                inputValue = MIN_POLYGON_SIDES
            End If

            ' Clamp the numeric value between min and max
            Dim sides As Integer = Math.Max(MIN_POLYGON_SIDES, Math.Min(MAX_POLYGON_SIDES, inputValue))

            ' Update textbox if value was clamped to prevent infinite recursion
            If inputValue <> sides Then
                RemoveHandler TxtPolygonSides.TextChanged, AddressOf TxtPolygonSides_TextChanged
                TxtPolygonSides.Text = sides.ToString()
                TxtPolygonSides.SelectionStart = TxtPolygonSides.Text.Length
                AddHandler TxtPolygonSides.TextChanged, AddressOf TxtPolygonSides_TextChanged
            End If

            UpdateAngleLabels(sides)
            InvalidateCache()
            DrawResultingPolygon(sides)
        Catch ex As Exception
            Debug.WriteLine($"Error in TxtPolygonSides_TextChanged: {ex.Message}")
            ' Reset to minimum on any error
            TxtPolygonSides.Text = MIN_POLYGON_SIDES.ToString()
        End Try
    End Sub

    Private Sub TxtPolygonSides_Leave(sender As Object, e As EventArgs) Handles TxtPolygonSides.Leave
        ' Ensure we always have at least the minimum value when leaving the control
        If String.IsNullOrWhiteSpace(TxtPolygonSides.Text) Then
            TxtPolygonSides.Text = MIN_POLYGON_SIDES.ToString()
            Return
        End If

        Dim inputValue As Integer
        If Not Integer.TryParse(TxtPolygonSides.Text, inputValue) OrElse
           inputValue < MIN_POLYGON_SIDES OrElse inputValue > MAX_POLYGON_SIDES Then
            TxtPolygonSides.Text = MIN_POLYGON_SIDES.ToString()
            UpdateAngleLabels(MIN_POLYGON_SIDES)
            InvalidateCache()
            DrawResultingPolygon(MIN_POLYGON_SIDES)
        End If
    End Sub

    Private Sub TxtPolygonSides_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtPolygonSides.KeyPress
        ' Only allow digits and backspace
        If Not Char.IsDigit(e.KeyChar) AndAlso e.KeyChar <> ControlChars.Back Then
            e.Handled = True
        End If
    End Sub

    Friend Sub TmrRotation_Tick()
        Try
            If RotationEnabled Then
                currentRotationAngle += currentConfig.AnimationSpeed
                If currentRotationAngle >= ANIMATION_FULL_CIRCLE Then
                    currentRotationAngle -= ANIMATION_FULL_CIRCLE
                End If
            End If

            ' Get sides from the appropriate control
            Dim sides As Integer
            If TxtPolygonSides IsNot Nothing AndAlso Integer.TryParse(TxtPolygonSides.Text, sides) Then
                DrawResultingPolygon(sides)
            End If
        Catch ex As Exception
            Debug.WriteLine($"Error in TmrRotation_Tick: {ex.Message}")
        End Try
    End Sub

    Private Sub PbPolygon_Click(sender As Object, e As EventArgs) Handles PbPolygon.Click
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
        RotationEnabled = Not RotationEnabled
    End Sub

    Friend Sub UpdatePolygonConfig(newConfig As PolygonConfig)
        currentConfig = newConfig
        InvalidateCache()
    End Sub

    Private Sub UpdateAngleLabels(sides As Integer)
        Dim totalAngle As Double = ANIMATION_FULL_CIRCLE / sides

        ' Use the correct label names from your form
        If LblPolygonSideAngle IsNot Nothing Then
            Dim formatAngle As String = GetFormatString(LblPolygonSideAngle.Tag, "{0:F2}")
            LblPolygonSideAngle.Text = String.Format(formatAngle, totalAngle)
        End If

        If LblPolygonPieceAngle IsNot Nothing Then
            Dim formatCut As String = GetFormatString(LblPolygonPieceAngle.Tag, "{0:F2}")
            LblPolygonPieceAngle.Text = String.Format(formatCut, totalAngle / 2)
        End If
    End Sub

    Private Shared Function GetFormatString(tag As Object, defaultFormat As String) As String
        If tag Is Nothing Then Return defaultFormat
        Dim tagStr As String = CStr(tag)
        Return If(String.IsNullOrWhiteSpace(tagStr), defaultFormat, tagStr)
    End Function

    Private Sub InvalidateCache()
        cachedNSides = 0
        cachedPoints = Nothing
    End Sub

#Region "Drawing Methods"

    Private Sub DrawResultingPolygon(nSides As Integer)

        If nSides < MIN_POLYGON_SIDES Then Return

        ' Only draw if TpCalcs is the selected tab
        If Tc.SelectedTab IsNot TpCalcs Then
            If PbPolygon.Image IsNot Nothing Then
                PbPolygon.Image.Dispose()
                PbPolygon.Image = Nothing
            End If
            Return
        End If

        ' Dispose previous image if needed
        If PbPolygon.Image IsNot Nothing Then
            PbPolygon.Image.Dispose()
            PbPolygon.Image = Nothing
        End If

        Using bmp As New Bitmap(PbPolygon.Width, PbPolygon.Height)
            Using g As Graphics = Graphics.FromImage(bmp)
                ConfigureGraphics(g)
                Dim drawingParams As DrawingParameters = CalculateDrawingParameters(PbPolygon, nSides)

                If currentConfig.ShowShadow Then DrawDropShadow(g, drawingParams)
                If currentConfig.EnableLighting Then DrawExtrudedSides(g, drawingParams)
                DrawTopFace(g, drawingParams)
                DrawOutline(g, drawingParams)
                If currentConfig.ShowVertexMarkers Then DrawVertexMarkers(g, drawingParams)
                If currentConfig.ShowSideLabels Then DrawSideLabels(g, drawingParams)
                If currentConfig.ShowAngleArc Then DrawAngleArc(g, drawingParams)
                If currentConfig.ShowCenterText Then DrawCenterText(g, drawingParams)
            End Using
            PbPolygon.Image = CType(bmp.Clone(), Bitmap)
        End Using
    End Sub

    Private Sub ConfigureGraphics(g As Graphics)
        ArgumentNullException.ThrowIfNull(g)
        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        g.Clear(Color.White)
    End Sub

    Private Function CalculateDrawingParameters(pictureBox As PictureBox, nSides As Integer) As DrawingParameters
        ArgumentNullException.ThrowIfNull(pictureBox)
        Dim center As New PointF(pictureBox.Width / 2.0F, pictureBox.Height / 2.0F)
        Dim radius As Single = CSng(Math.Min(pictureBox.Width, pictureBox.Height) / 2 - MARGIN_SIZE)
        Dim points() As PointF = CalculatePolygonPoints(center, radius, nSides, currentRotationAngle)
        Dim extrudedPoints() As PointF = CreateOffsetPoints(points, EXTRUDE_OFFSET_X, EXTRUDE_OFFSET_Y)

        Return New DrawingParameters With {
            .Center = center,
            .Radius = radius,
            .NSides = nSides,
            .Points = points,
            .ExtrudedPoints = extrudedPoints,
            .ExtrudeOffset = New PointF(EXTRUDE_OFFSET_X, EXTRUDE_OFFSET_Y)
        }
    End Function

    Private Sub DrawDropShadow(g As Graphics, params As DrawingParameters)
        ArgumentNullException.ThrowIfNull(g)
        Dim shadowPoints() As PointF = CreateOffsetPoints(params.Points, SHADOW_OFFSET_X, SHADOW_OFFSET_Y)
        Using shadowBrush As New SolidBrush(Color.FromArgb(SHADOW_ALPHA, Color.Black))
            g.FillPolygon(shadowBrush, shadowPoints)
        End Using
    End Sub

    Private Sub DrawExtrudedSides(g As Graphics, params As DrawingParameters)
        ArgumentNullException.ThrowIfNull(g)
        Dim baseSideColor As Color = ControlPaint.Dark(currentConfig.SideColor)
        Dim lightDirection As New PointF(-0.7071F, -0.7071F) ' Normalized diagonal

        For i As Integer = 0 To params.NSides - 1
            Dim nextIndex As Integer = (i + 1) Mod params.NSides

            Dim sideColor As Color = If(currentConfig.EnableLighting,
                    CalculateLitColor(baseSideColor, params.Points(i), params.Points(nextIndex),
                                    params.ExtrudeOffset, lightDirection),
                    baseSideColor)

            Dim quad() As PointF = {
                    params.Points(i), params.Points(nextIndex),
                    params.ExtrudedPoints(nextIndex), params.ExtrudedPoints(i)
                }

            Using sideBrush As New SolidBrush(sideColor)
                g.FillPolygon(sideBrush, quad)
            End Using
        Next
    End Sub

#Region "Private Helper Methods"

    Private Function AdjustColor(baseColor As Color, factor As Single) As Color
        Dim clampedFactor As Single = Math.Max(0.0F, Math.Min(2.0F, factor))
        Dim r As Integer = Math.Max(0, Math.Min(255, CInt(baseColor.R * clampedFactor)))
        Dim g As Integer = Math.Max(0, Math.Min(255, CInt(baseColor.G * clampedFactor)))
        Dim b As Integer = Math.Max(0, Math.Min(255, CInt(baseColor.B * clampedFactor)))
        Return Color.FromArgb(baseColor.A, r, g, b)
    End Function

    Private Function CalculatePolygonPoints(center As PointF, radius As Single, nSides As Integer, rotationAngle As Double) As PointF()
        If cachedNSides = nSides AndAlso cachedPoints IsNot Nothing AndAlso
               cachedCenter.Equals(center) AndAlso cachedRadius = radius Then
            ' Return rotated cached points
            Return RotatePoints(cachedPoints, center, rotationAngle)
        End If

        Dim points(nSides - 1) As PointF
        Dim baseRotation As Double = -Math.PI / 2  ' Start from top

        For i As Integer = 0 To nSides - 1
            Dim vertexAngle As Double = 2 * Math.PI / nSides * i + baseRotation
            points(i) = New PointF(
                    center.X + CSng(radius * Math.Cos(vertexAngle)),
                    center.Y + CSng(radius * Math.Sin(vertexAngle))
                )
        Next

        ' Cache the base points
        cachedNSides = nSides
        cachedPoints = CType(points.Clone(), PointF())
        cachedCenter = center
        cachedRadius = radius

        Return RotatePoints(points, center, rotationAngle)
    End Function

    Private Function RotatePoints(points() As PointF, center As PointF, rotationAngleDegrees As Double) As PointF()
        ArgumentNullException.ThrowIfNull(points)
        If Math.Abs(rotationAngleDegrees) < 0.001 Then Return points

        Dim rotationAngle As Double = rotationAngleDegrees * Math.PI / 180
        Dim cosAngle As Double = Math.Cos(rotationAngle)
        Dim sinAngle As Double = Math.Sin(rotationAngle)
        Dim rotatedPoints(points.Length - 1) As PointF

        For i As Integer = 0 To points.Length - 1
            Dim dx As Double = points(i).X - center.X
            Dim dy As Double = points(i).Y - center.Y

            rotatedPoints(i) = New PointF(
                    center.X + CSng(dx * cosAngle - dy * sinAngle),
                    center.Y + CSng(dx * sinAngle + dy * cosAngle)
                )
        Next

        Return rotatedPoints
    End Function

    Private Function CreateOffsetPoints(points() As PointF, offsetX As Single, offsetY As Single) As PointF()
        ArgumentNullException.ThrowIfNull(points)
        Dim offsetPoints(points.Length - 1) As PointF
        For i As Integer = 0 To points.Length - 1
            offsetPoints(i) = New PointF(points(i).X + offsetX, points(i).Y + offsetY)
        Next
        Return offsetPoints
    End Function

#End Region

    Private Function CalculateLitColor(baseColor As Color, p1 As PointF, p2 As PointF,
                                         extrudeOffset As PointF, lightDirection As PointF) As Color
        ' Calculate surface normal
        Dim edgeVector As New PointF(p2.X - p1.X, p2.Y - p1.Y)
        Dim normal As New PointF(-edgeVector.Y, edgeVector.X)

        ' Ensure normal points outward
        If (normal.X * extrudeOffset.X + normal.Y * extrudeOffset.Y) < 0 Then
            normal = New PointF(-normal.X, -normal.Y)
        End If

        ' Normalize
        Dim length As Single = CSng(Math.Sqrt(normal.X * normal.X + normal.Y * normal.Y))
        If length > 0 Then
            normal = New PointF(normal.X / length, normal.Y / length)

            ' Calculate lighting
            Dim dot As Single = normal.X * lightDirection.X + normal.Y * lightDirection.Y
            Dim brightnessFactor As Single = LIGHTING_BASE + LIGHTING_VARIATION * (dot + 1) / 2

            Return AdjustColor(baseColor, brightnessFactor)
        End If

        Return baseColor
    End Function

    Private Sub DrawTopFace(g As Graphics, params As DrawingParameters)
        ArgumentNullException.ThrowIfNull(g)

        Using gradientBrush As New Drawing2D.LinearGradientBrush(
                New Point(0, 0), New Point(CInt(params.Center.X * 2), CInt(params.Center.Y * 2)),
                currentConfig.TopFaceColor1, currentConfig.TopFaceColor2)
            g.FillPolygon(gradientBrush, params.Points)
        End Using
    End Sub

    Private Sub DrawOutline(g As Graphics, params As DrawingParameters)
        ArgumentNullException.ThrowIfNull(g)

        Using pen As New Pen(currentConfig.OutlineColor, 2) With {.DashStyle = Drawing2D.DashStyle.Dash}
            g.DrawPolygon(pen, params.Points)
        End Using
    End Sub

    Private Sub DrawVertexMarkers(g As Graphics, params As DrawingParameters)
        ArgumentNullException.ThrowIfNull(g)

        Using brush As New SolidBrush(currentConfig.VertexColor)
            For Each point As PointF In params.Points
                g.FillEllipse(brush, point.X - VERTEX_MARKER_RADIUS, point.Y - VERTEX_MARKER_RADIUS,
                                VERTEX_MARKER_RADIUS * 2, VERTEX_MARKER_RADIUS * 2)
            Next
        End Using
    End Sub

    Private Sub DrawSideLabels(g As Graphics, params As DrawingParameters)
        ArgumentNullException.ThrowIfNull(g)

        Using font As New Font("Arial", 12, FontStyle.Bold)
            Using brush As New SolidBrush(currentConfig.TextColor)
                For i As Integer = 0 To params.NSides - 1
                    Dim nextIndex As Integer = (i + 1) Mod params.NSides
                    Dim midPoint As New PointF(
                            (params.Points(i).X + params.Points(nextIndex).X) / 2,
                            (params.Points(i).Y + params.Points(nextIndex).Y) / 2
                        )

                    Dim label As String = (i + 1).ToString()
                    Dim textSize As SizeF = g.MeasureString(label, font)
                    Dim textPoint As New PointF(
                            midPoint.X - textSize.Width / 2,
                            midPoint.Y - textSize.Height / 2
                        )

                    g.DrawString(label, font, brush, textPoint)
                Next
            End Using
        End Using
    End Sub

    Private Sub DrawAngleArc(g As Graphics, params As DrawingParameters)
        ArgumentNullException.ThrowIfNull(g)
        Dim topVertex As Integer = FindTopVertex(params.Points)
        If topVertex = -1 Then Return

        Dim vertexPoint As PointF = params.Points(topVertex)
        Dim prevIndex As Integer = If(topVertex = 0, params.NSides - 1, topVertex - 1)
        Dim nextIndex As Integer = (topVertex + 1) Mod params.NSides

        Dim anglePrev As Double = CalculateAngle(params.Points(prevIndex), vertexPoint)
        Dim angleNext As Double = CalculateAngle(params.Points(nextIndex), vertexPoint)

        Dim sweepAngle As Double = NormalizeAngleDifference(angleNext - anglePrev)
        If sweepAngle > 180 Then
            sweepAngle = 360 - sweepAngle
            anglePrev = angleNext
        End If

        Dim arcRect As New RectangleF(
                vertexPoint.X - ARC_RADIUS, vertexPoint.Y - ARC_RADIUS,
                ARC_RADIUS * 2, ARC_RADIUS * 2
            )

        Using pen As New Pen(currentConfig.ArcColor, 2)
            g.DrawArc(pen, arcRect, CSng(anglePrev), CSng(sweepAngle))
        End Using
    End Sub

    Private Function FindTopVertex(points() As PointF) As Integer
        ArgumentNullException.ThrowIfNull(points)
        Dim topIndex As Integer = 0
        For i As Integer = 1 To points.Length - 1
            If points(i).Y < points(topIndex).Y Then topIndex = i
        Next
        Return topIndex
    End Function

    Private Function CalculateAngle(point As PointF, center As PointF) As Double
        Return ((Math.Atan2(point.Y - center.Y, point.X - center.X) * 180 / Math.PI) + 360) Mod 360
    End Function

    Private Function NormalizeAngleDifference(angleDiff As Double) As Double
        While angleDiff < 0
            angleDiff += 360
        End While
        While angleDiff >= 360
            angleDiff -= 360
        End While
        Return angleDiff
    End Function

    Private Sub DrawCenterText(g As Graphics, params As DrawingParameters)
        ArgumentNullException.ThrowIfNull(g)
        Dim centerText As String = params.NSides.ToString()
        Using font As New Font("Microsoft Sans Serif", 20, FontStyle.Bold)
            Using brush As New SolidBrush(Color.Maroon)
                Dim textSize As SizeF = g.MeasureString(centerText, font)
                Dim textPoint As New PointF(
                        params.Center.X - textSize.Width / 2,
                        params.Center.Y - textSize.Height / 2
                    )
                g.DrawString(centerText, font, brush, textPoint)
            End Using
        End Using
    End Sub

    Private Structure DrawingParameters
        Public Center As PointF
        Public Radius As Single
        Public NSides As Integer
        Public Points() As PointF
        Public ExtrudedPoints() As PointF
        Public ExtrudeOffset As PointF
    End Structure

#End Region

#End Region

End Class