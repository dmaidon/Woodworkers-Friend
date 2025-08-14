Friend Module DrawerDrawings

    ' Store the last drawer calculation result for drawing
    Public _lastDrawerResult As DrawerCalculationResult

    Public _lastDrawerParameters As DrawerCalculationParameters

    ' Call this after calculation is complete
    Public Sub DrawDrawerShopDrawing(result As DrawerCalculationResult, parameters As DrawerCalculationParameters)
        _lastDrawerResult = result
        _lastDrawerParameters = parameters
        FrmMain.PbOutputDrawing.Invalidate()
    End Sub

    ' Paint event handler for pbOutputDrawing
    Public Sub PbOutputDrawing_Paint(sender As Object, e As PaintEventArgs)
        ArgumentNullException.ThrowIfNull(sender)
        ArgumentNullException.ThrowIfNull(e)
        If _lastDrawerResult Is Nothing OrElse Not _lastDrawerResult.IsValid OrElse _lastDrawerParameters Is Nothing Then Return

        Dim g As Graphics = e.Graphics
        g.Clear(Color.White)

        ' Drawing constants (increase margins for safety)
        Dim marginLeft As Integer = 60
        Dim marginTop As Integer = 40
        Dim marginRight As Integer = 80
        Dim marginBottom As Integer = 60
        'Dim drawerSpacingPx As Integer = 8
        Dim font As New Font("Arial", 9)
        Dim dimFont As New Font("Arial", 8, FontStyle.Italic)
        Dim penDim As New Pen(Color.DarkRed, 2)
        Dim penOutline As New Pen(Color.Black, 2)
        Dim brushDrawer As New SolidBrush(Color.LightSteelBlue)
        Dim brushText As New SolidBrush(Color.Black)

        ' Use imperial for drawing, but you can switch to metric if desired
        Dim heights = _lastDrawerResult.DrawerHeightsImperial
        Dim heightsMetric = _lastDrawerResult.DrawerHeightsMetric
        Dim drawerWidth = _lastDrawerParameters.DrawerWidth
        Dim drawerWidthMetric = If(_lastDrawerParameters.Scale = MeasurementScale.Imperial, heightsMetric(0), drawerWidth)
        Dim drawerCount = heights.Length
        Dim spacing = _lastDrawerParameters.DrawerSpacing

        ' Calculate scaling
        Dim totalHeight = heights.Sum() + spacing * (drawerCount - 1)
        Dim availableHeightPx = FrmMain.PbOutputDrawing.Height - marginTop - marginBottom
        Dim availableWidthPx = FrmMain.PbOutputDrawing.Width - marginLeft - marginRight

        ' Prevent scale from being too large (avoid overflow)
        Dim scaleY As Single = CSng(availableHeightPx / totalHeight)
        Dim scaleX As Single = CSng(availableWidthPx / drawerWidth)

        ' Cabinet outline
        Dim cabinetX As Single = CSng(marginLeft)
        Dim cabinetY As Single = CSng(marginTop)
        Dim cabinetW As Single = CSng(drawerWidth * scaleX)
        Dim cabinetH As Single = CSng(totalHeight * scaleY)
        g.DrawRectangle(penOutline, cabinetX, cabinetY, cabinetW, cabinetH)

        ' Draw drawers
        Dim y As Single = cabinetY
        For i As Integer = 0 To drawerCount - 1
            Dim hPx As Single = CSng(heights(i) * scaleY)
            Dim rect = New RectangleF(cabinetX, y, cabinetW, hPx)
            g.FillRectangle(brushDrawer, rect)
            g.DrawRectangle(Pens.Black, rect.X, rect.Y, rect.Width, rect.Height)

            ' Drawer label (number and heights)
            Dim label = $"Drawer {i + 1}: {heights(i):N2} in / {heightsMetric(i):N1} mm"
            ' Ensure label is inside the drawer rectangle
            g.DrawString(label, font, brushText, rect.X + 4, rect.Y + 4)

            y += hPx
            If i < drawerCount - 1 Then
                ' Draw spacing line
                g.DrawLine(Pens.Gray, cabinetX, y, cabinetX + cabinetW, y)
                y += CSng(spacing * scaleY)
            End If
        Next

        ' Draw overall height dimension (draw inside right margin)
        Dim dimX As Single = CSng(Math.Min(cabinetX + cabinetW + 20, FrmMain.PbOutputDrawing.Width - marginRight + 10))
        g.DrawLine(penDim, dimX, cabinetY, dimX, cabinetY + cabinetH)
        g.DrawLine(penDim, dimX - 5, cabinetY, dimX + 5, cabinetY)
        g.DrawLine(penDim, dimX - 5, cabinetY + cabinetH, dimX + 5, cabinetY + cabinetH)
        Dim heightLabelImperial = $"{totalHeight:N2} in"
        Dim heightLabelMetric = $"{DrawerCalculationEngine.InchesToMillimeters(totalHeight):N1} mm"
        Dim heightLabelX As Single = dimX + 8
        Dim heightLabelY As Single = cabinetY + cabinetH / 2 - 16 ' Move up for stacking

        g.DrawString(heightLabelImperial, dimFont, brushText, heightLabelX, heightLabelY)
        g.DrawString(heightLabelMetric, dimFont, brushText, heightLabelX, heightLabelY + dimFont.Height + 2)

        ' Draw overall width dimension (draw inside bottom margin)
        Dim dimY As Single = CSng(Math.Min(cabinetY + cabinetH + 20, FrmMain.PbOutputDrawing.Height - marginBottom + 10))
        g.DrawLine(penDim, cabinetX, dimY, cabinetX + cabinetW, dimY)
        g.DrawLine(penDim, cabinetX, dimY - 5, cabinetX, dimY + 5)
        g.DrawLine(penDim, cabinetX + cabinetW, dimY - 5, cabinetX + cabinetW, dimY + 5)
        Dim widthLabel = $"{drawerWidth:N2} in / {drawerWidthMetric:N1} mm"
        g.DrawString(widthLabel, dimFont, brushText, cabinetX + cabinetW / 2 - 30, dimY + 8)

        ' Dispose resources
        font.Dispose()
        dimFont.Dispose()
        penDim.Dispose()
        penOutline.Dispose()
        brushDrawer.Dispose()
        brushText.Dispose()
    End Sub

End Module