Partial Public Class FrmMain

    ' ================== CONFIGURABLE DRAW AREA BEHAVIOR ==================
    ' Zoom factor for door drawings (1 = fit). When != 1 we enlarge / shrink
    Private _doorZoom As Single = 1.0F

    Private Const DoorZoomMin As Single = 0.35F
    Private Const DoorZoomMax As Single = 3.0F

    ' Optional: reserve space at right for overall height dimension text
    Private Const DoorDimReserveRightPx As Integer = 140

    ' Optional bottom reserve (overall width + legend)
    Private Const DoorDimReserveBottomPx As Integer = 110

    ' Call from Form load (after PictureBox created) ONCE
    Private Sub InitializeDrawingZoomSupport()
        AddHandler PbOutputDrawing.MouseWheel, AddressOf PbOutputDrawing_MouseWheel
    End Sub

    Private Sub PbOutputDrawing_MouseWheel(sender As Object, e As MouseEventArgs)
        ' Ctrl + wheel = zoom; without Ctrl ignore
        If (ModifierKeys And Keys.Control) = Keys.Control Then
            Dim stepFactor As Single = If(e.Delta > 0, 1.1F, 0.9F)
            SetDoorZoom(_doorZoom * stepFactor)
        End If
    End Sub

    Public Sub ResetDoorZoom()
        _doorZoom = 1.0F
        PbOutputDrawing.Invalidate()
    End Sub

    Public Sub SetDoorZoom(value As Single)
        _doorZoom = Math.Max(DoorZoomMin, Math.Min(DoorZoomMax, value))
        PbOutputDrawing.Invalidate()
    End Sub

    Public Sub ZoomDoorIncrease()
        SetDoorZoom(_doorZoom * 1.2F)
    End Sub

    Public Sub ZoomDoorDecrease()
        SetDoorZoom(_doorZoom / 1.2F)
    End Sub

    Private Enum OutputDrawingMode
        None
        Drawer
        Door
        DoorExploded
    End Enum

    Private _outputDrawingMode As OutputDrawingMode = OutputDrawingMode.None

    ' Door drawing state
    Private _lastDoorResult As DoorCalculationResult

    Private _lastDoorParameters As DoorCalculationParameters
    Private _doorExploded As Boolean = False

    Public Sub SetDoorDrawingContext(result As DoorCalculationResult, parameters As DoorCalculationParameters, Optional exploded As Boolean = False)
        _lastDoorResult = result
        _lastDoorParameters = parameters
        _doorExploded = exploded
        _outputDrawingMode = If(exploded, OutputDrawingMode.DoorExploded, OutputDrawingMode.Door)
        PbOutputDrawing.Invalidate()
    End Sub

    Public Sub ToggleDoorExplodedView()
        If _outputDrawingMode = OutputDrawingMode.Door OrElse _outputDrawingMode = OutputDrawingMode.DoorExploded Then
            _doorExploded = Not _doorExploded
            _outputDrawingMode = If(_doorExploded, OutputDrawingMode.DoorExploded, OutputDrawingMode.Door)
            PbOutputDrawing.Invalidate()
        End If
    End Sub

    Public Sub SetDrawerDrawingContext()
        _outputDrawingMode = OutputDrawingMode.Drawer
        PbOutputDrawing.Invalidate()
    End Sub

    Private Sub PbOutputDrawing_Paint(sender As Object, e As PaintEventArgs) Handles PbOutputDrawing.Paint
        Select Case _outputDrawingMode
            Case OutputDrawingMode.Drawer
                DrawerDrawings.PbOutputDrawing_Paint(sender, e)
            Case OutputDrawingMode.Door
                DrawDoorShopDrawing(e.Graphics, False)
            Case OutputDrawingMode.DoorExploded
                DrawDoorShopDrawing(e.Graphics, True)
            Case Else
                e.Graphics.Clear(Color.White)
                Using f As New Font("Arial", 10, FontStyle.Italic)
                    e.Graphics.DrawString("No drawing available", f, Brushes.Gray, 10.0F, 10.0F)
                End Using
        End Select
    End Sub

    ' Helper (no lambdas with Optional params)
    Private Sub DrawStackedText(g As Graphics,
                                font As Font,
                                defaultBrush As Brush,
                                imperial As String,
                                metric As String,
                                px As Single,
                                py As Single,
                                Optional center As Boolean = False,
                                Optional customBrush As Brush = Nothing)

        Dim br = If(customBrush, defaultBrush)
        Dim szImp = g.MeasureString(imperial, font)
        Dim szMet = g.MeasureString(metric, font)
        Dim offImp = If(center, szImp.Width / 2.0F, 0F)
        Dim offMet = If(center, szMet.Width / 2.0F, 0F)
        g.DrawString(imperial, font, br, px - offImp, py)
        g.DrawString(metric, font, br, px - offMet, py + szImp.Height - 1.0F)
    End Sub

    ' ------------- UPDATED DrawDoorShopDrawing -------------
    Private Sub DrawDoorShopDrawing(g As Graphics, exploded As Boolean)
        If _lastDoorResult Is Nothing OrElse Not _lastDoorResult.IsValid Then
            g.Clear(Color.White)
            Return
        End If

        g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias
        g.Clear(Color.White)

        Using dimFont As New Font("Arial", 9.0F, FontStyle.Regular),
              noteFont As New Font("Arial", 7.5F, FontStyle.Italic),
              titleFont As New Font("Arial", 11.0F, FontStyle.Bold),
              railPen As New Pen(Color.DimGray, 1.4F),
              stilePen As New Pen(Color.DarkSlateGray, 1.4F),
              panelPen As New Pen(Color.DimGray, 1.0F),
              dimPen As New Pen(Color.DarkBlue, 1.15F),
              panelBrush As New SolidBrush(Color.Gainsboro),
              stileBrush As New SolidBrush(Color.LightSteelBlue),
              railBrush As New SolidBrush(Color.LightSkyBlue),
              textBrush As New SolidBrush(Color.Black)

            ' Base margins (reduced to allow more canvas usage)
            Dim marginLeft As Single = 50.0F
            Dim marginTop As Single = 40.0F
            Dim marginRight As Single = 40.0F
            Dim marginBottom As Single = 40.0F

            Dim pbW = PbOutputDrawing.ClientSize.Width
            Dim pbH = PbOutputDrawing.ClientSize.Height

            Dim doorW_in As Single = CSng(_lastDoorResult.DoorWidth)
            Dim doorH_in As Single = CSng(_lastDoorResult.DoorHeight)
            If doorW_in <= 0 OrElse doorH_in <= 0 Then
                g.DrawString("Door dimensions unavailable.", dimFont, Brushes.Red, 10.0F, 10.0F)
                Return
            End If

            Dim stileFace_in As Single = If(_lastDoorParameters.StileWidth > 0, CSng(_lastDoorParameters.StileWidth), 2.0F)
            Dim railFace_in As Single = If(_lastDoorParameters.RailWidth > 0, CSng(_lastDoorParameters.RailWidth), 2.0F)
            Dim isTwoDoor = _lastDoorParameters.IsTwoDoor
            Dim gap_in As Single = If(isTwoDoor, CSng(_lastDoorParameters.GapSize), 0.0F)

            Dim eachLeaf_in As Single = If(isTwoDoor, (doorW_in - gap_in) / 2.0F, doorW_in)
            Dim panelW_in As Single = eachLeaf_in - 2 * stileFace_in
            Dim panelH_in As Single = doorH_in - 2 * railFace_in

            ' Compute drawable region excluding reserved dimension zones
            Dim drawableW = pbW - marginLeft - marginRight - DoorDimReserveRightPx
            Dim drawableH = pbH - marginTop - marginBottom - DoorDimReserveBottomPx

            If drawableW < 50 OrElse drawableH < 50 Then
                drawableW = Math.Max(drawableW, 50)
                drawableH = Math.Max(drawableH, 50)
            End If

            ' Fit scale
            Dim fitScale = Math.Min(drawableW / doorW_in, drawableH / doorH_in)

            ' Apply zoom (with clamp so drawing doesn't exceed available)
            Dim scale = fitScale * _doorZoom

            ' If scaled width/height spill into reserved areas, reduce scale slightly
            Dim maxScaleW = drawableW / doorW_in
            Dim maxScaleH = drawableH / doorH_in
            scale = Math.Min(scale, Math.Min(maxScaleW, maxScaleH))

            ' If still a lot of free space (using less than 70%), auto bump (Fill factor)
            Dim usedWidthPct = (doorW_in * scale) / drawableW
            Dim usedHeightPct = (doorH_in * scale) / drawableH
            Dim fillFactor = 1.0F
            If usedWidthPct < 0.7F AndAlso usedHeightPct < 0.7F AndAlso _doorZoom = 1.0F Then
                fillFactor = Math.Min(0.95F / Math.Max(usedWidthPct, usedHeightPct), 1.25F)
                scale *= fillFactor
            End If

            ' Explosion offsets relative to scale
            Dim explodeOffsetY As Single = If(exploded, 35.0F * scale, 0.0F)
            Dim panelOffsetY As Single = If(exploded, explodeOffsetY * 1.6F, 0.0F)

            ' Center the assembly inside its drawable rect
            Dim totalDrawW = doorW_in * scale
            Dim totalDrawH = doorH_in * scale
            Dim extraX = (drawableW - totalDrawW) / 2.0F
            Dim extraY = (drawableH - totalDrawH) / 2.0F
            Dim x0 As Single = marginLeft + Math.Max(0, extraX)
            Dim y0 As Single = marginTop + Math.Max(0, extraY)

            ' Title (left top)
            Dim titleText As String

            If exploded Then
                If exploded Then
                    titleText = If(isTwoDoor,
                                         "Two-Door Exploded View",
                                         "Door Exploded View")
                Else
                    titleText = If(isTwoDoor,
                                         "Two-Door Exploded View",
                                         "Door Assembly")
                End If
            Else
                If exploded Then
                    titleText = If(isTwoDoor,
                                         "Two-Door Assembly",
                                         "Door Exploded View")
                Else
                    titleText = If(isTwoDoor,
                                         "Two-Door Assembly",
                                         "Door Assembly")
                End If
            End If

            g.DrawString(titleText, titleFont, textBrush, x0, y0 - 32.0F)

            Dim inchToMm = Function(v As Single) v * 25.4F
            Dim arrowSize As Single = 4.0F

            ' Arrow helper
            Dim drawArrow =
                Sub(p1 As PointF, p2 As PointF, pClr As Color)
                    Using p As New Pen(pClr, dimPen.Width)
                        g.DrawLine(p, p1, p2)
                        Dim ang As Double = Math.Atan2(p2.Y - p1.Y, p2.X - p1.X)
                        Dim tx As Single = CSng(Math.Cos(ang + Math.PI / 2) * arrowSize)
                        Dim ty As Single = CSng(Math.Sin(ang + Math.PI / 2) * arrowSize)
                        g.DrawLine(p, p1.X - tx, p1.Y - ty, p1.X + tx, p1.Y + ty)
                        g.DrawLine(p, p2.X - tx, p2.Y - ty, p2.X + tx, p2.Y + ty)
                    End Using
                End Sub

            ' Leaf drawing
            Dim leafParts =
                Sub(leafOriginX_in As Single, leafIndex As Integer)
                    Dim leafBaseX_px = x0 + leafOriginX_in * scale
                    Dim stileW_px = stileFace_in * scale
                    Dim railH_px = railFace_in * scale
                    Dim leafW_px = eachLeaf_in * scale
                    Dim leafH_px = doorH_in * scale

                    Dim topRailRect = New RectangleF(leafBaseX_px + stileW_px, y0, leafW_px - 2 * stileW_px, railH_px)
                    Dim bottomRailRect = New RectangleF(leafBaseX_px + stileW_px,
                                                        y0 + leafH_px - railH_px + If(exploded, explodeOffsetY + panelOffsetY, 0),
                                                        leafW_px - 2 * stileW_px,
                                                        railH_px)

                    Dim stileTopY = y0 + If(exploded, explodeOffsetY, 0)
                    Dim stileHeightPx = leafH_px + If(exploded, panelOffsetY, 0) - If(exploded, explodeOffsetY, 0)
                    Dim leftStileRect = New RectangleF(leafBaseX_px, stileTopY, stileW_px, stileHeightPx)
                    Dim rightStileRect = New RectangleF(leafBaseX_px + leafW_px - stileW_px, stileTopY, stileW_px, stileHeightPx)

                    Dim panelRect = New RectangleF(leafBaseX_px + stileW_px,
                                                   y0 + railH_px + If(exploded, (explodeOffsetY + panelOffsetY) / 2.0F, 0),
                                                   leafW_px - 2 * stileW_px,
                                                   leafH_px - 2 * railH_px)

                    g.FillRectangle(stileBrush, leftStileRect)
                    g.FillRectangle(stileBrush, rightStileRect)
                    g.DrawRectangle(stilePen, leftStileRect.X, leftStileRect.Y, leftStileRect.Width, leftStileRect.Height)
                    g.DrawRectangle(stilePen, rightStileRect.X, rightStileRect.Y, rightStileRect.Width, rightStileRect.Height)

                    g.FillRectangle(railBrush, topRailRect)
                    g.FillRectangle(railBrush, bottomRailRect)
                    g.DrawRectangle(railPen, topRailRect.X, topRailRect.Y, topRailRect.Width, topRailRect.Height)
                    g.DrawRectangle(railPen, bottomRailRect.X, bottomRailRect.Y, bottomRailRect.Width, bottomRailRect.Height)

                    g.FillRectangle(panelBrush, panelRect)
                    g.DrawRectangle(panelPen, panelRect.X, panelRect.Y, panelRect.Width, panelRect.Height)

                    Dim annotateThisLeaf = (Not isTwoDoor) OrElse leafIndex = 0
                    If annotateThisLeaf Then
                        ' Stile width
                        Dim sx0 = leftStileRect.X
                        Dim sx1 = leftStileRect.Right
                        Dim sy = leftStileRect.Y - 14.0F
                        drawArrow(New PointF(sx0, sy), New PointF(sx1, sy), Color.DarkBlue)
                        DrawStackedText(g, dimFont, textBrush, $"{stileFace_in:N2} in", $"{inchToMm(stileFace_in):N1} mm", (sx0 + sx1) / 2.0F, sy - 26.0F, True)

                        ' Rail height
                        Dim ry0 = topRailRect.Y
                        Dim ry1 = topRailRect.Bottom
                        Dim rx = topRailRect.X - 18.0F
                        drawArrow(New PointF(rx, ry0), New PointF(rx, ry1), Color.DarkBlue)
                        DrawStackedText(g, dimFont, textBrush, $"{railFace_in:N2} in", $"{inchToMm(railFace_in):N1} mm", rx - 6.0F, ry0 + (ry1 - ry0) / 2.0F - 14.0F)

                        ' Panel width
                        Dim pwx0 = panelRect.X
                        Dim pwx1 = panelRect.Right
                        Dim pwy = panelRect.Y - 10.0F
                        drawArrow(New PointF(pwx0, pwy), New PointF(pwx1, pwy), Color.SaddleBrown)
                        DrawStackedText(g, dimFont, textBrush, $"{panelW_in:N2} in", $"{inchToMm(panelW_in):N1} mm", (pwx0 + pwx1) / 2.0F, pwy - 24.0F, True, Brushes.SaddleBrown)

                        ' Panel height
                        Dim phy0 = panelRect.Y
                        Dim phy1 = panelRect.Bottom
                        Dim phx = panelRect.X - 15.0F
                        drawArrow(New PointF(phx, phy0), New PointF(phx, phy1), Color.SaddleBrown)
                        DrawStackedText(g, dimFont, textBrush, $"{panelH_in:N2} in", $"{inchToMm(panelH_in):N1} mm", phx - 6.0F, phy0 + (phy1 - phy0) / 2.0F - 14.0F, False, Brushes.SaddleBrown)

                        ' Leaf width
                        Dim lwx0 = leafBaseX_px
                        Dim lwx1 = leafBaseX_px + leafW_px
                        Dim lwy = bottomRailRect.Bottom + 18.0F
                        drawArrow(New PointF(lwx0, lwy), New PointF(lwx1, lwy), Color.OliveDrab)
                        DrawStackedText(g, dimFont, textBrush, $"{eachLeaf_in:N2} in", $"{inchToMm(eachLeaf_in):N1} mm", (lwx0 + lwx1) / 2.0F, lwy + 4.0F, True, Brushes.OliveDrab)
                    End If
                End Sub

            If isTwoDoor Then
                leafParts(0.0F, 0)
                leafParts(eachLeaf_in + gap_in, 1)

                If gap_in > 0 Then
                    Dim gapLeft_px = x0 + eachLeaf_in * scale
                    Dim gapRight_px = gapLeft_px + gap_in * scale
                    Dim gy = y0 + doorH_in * scale / 2.0F
                    drawArrow(New PointF(gapLeft_px, gy), New PointF(gapRight_px, gy), Color.Maroon)
                    DrawStackedText(g, dimFont, textBrush, $"{gap_in:N3} in", $"{inchToMm(gap_in):N2} mm", (gapLeft_px + gapRight_px) / 2.0F, gy - 30.0F, True, Brushes.Maroon)
                    Using dashPen As New Pen(Color.Maroon, 1.0F) With {.DashStyle = Drawing2D.DashStyle.Dot}
                        g.DrawLine(dashPen, gapLeft_px, y0, gapLeft_px, y0 + doorH_in * scale)
                        g.DrawLine(dashPen, gapRight_px, y0, gapRight_px, y0 + doorH_in * scale)
                    End Using
                    g.DrawString("TYP", noteFont, Brushes.Maroon, gapLeft_px - 14.0F, gy - 12.0F)
                End If

                g.DrawString("Component callouts TYP both leaves", noteFont, Brushes.DimGray, x0, y0 + doorH_in * scale + 58.0F)
            Else
                leafParts(0.0F, 0)
            End If

            ' Overall height (reserved right side)
            Dim overallH_px = doorH_in * scale
            Dim dimX = x0 + doorW_in * scale + 20.0F
            drawArrow(New PointF(dimX, y0), New PointF(dimX, y0 + overallH_px), Color.DarkBlue)
            DrawStackedText(g, dimFont, textBrush, $"{doorH_in:N2} in", $"{inchToMm(doorH_in):N1} mm", dimX + 12.0F, y0 + overallH_px / 2.0F - 18.0F)

            ' Overall width (bottom)
            Dim dimY = y0 + overallH_px + 24.0F
            drawArrow(New PointF(x0, dimY), New PointF(x0 + doorW_in * scale, dimY), Color.DarkBlue)
            DrawStackedText(g, dimFont, textBrush, $"{doorW_in:N2} in", $"{inchToMm(doorW_in):N1} mm", x0 + (doorW_in * scale) / 2.0F, dimY + 4.0F, True)

            ' Legend (under width)
            Dim legendY = y0 + overallH_px + 50.0F
            g.DrawString($"Fit scale: {scale / _doorZoom:0.###} | Zoom: {_doorZoom:0.00}x  (Ctrl+Wheel to zoom)", noteFont, Brushes.DimGray, x0, legendY - 14.0F)
            g.DrawString("Imperial over Metric  | Colors: Stiles (Blue), Rails (Light Blue), Panel (Gray)", noteFont, Brushes.DimGray, x0, legendY)
            If exploded Then
                g.DrawString("EXPLODED VIEW - Vertical separation only", noteFont, Brushes.Firebrick, x0, legendY + 14.0F)
            End If
        End Using
    End Sub

    Private Sub TsslToggleDoorExploded_Click(sender As Object, e As EventArgs) Handles TsslToggleDoorExploded.Click
        ToggleDoorExplodedView()
    End Sub

End Class