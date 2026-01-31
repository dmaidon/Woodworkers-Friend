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

    ' Calculation constants
    Private Const MM_PER_INCH As Double = 25.4

    Private Const DEFAULT_DIMENSION As Decimal = 5D
    Private Const MIN_DIMENSION As Decimal = 0.125D
    Private Const MAX_DIMENSION As Decimal = 100D

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
    Private _polygonTooltip As ToolTip = Nothing
    Private _suppressDimensionUpdate As Boolean = False
    Private _lastUnitIndex As Integer = 0 ' Track by index: 0 = inches, 1 = mm

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
            ErrorHandler.LogError(ex, "TxtPolygonSides_TextChanged")
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
            ErrorHandler.LogError(ex, "TmrRotation_Tick")
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

        ' Recalculate all geometry if dimension is entered
        If NudPolygonDimension IsNot Nothing AndAlso NudPolygonDimension.Value > 0 Then
            CalculateAndDisplayResults()
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

#Region "Initialization"

    ''' <summary>
    ''' Initialize polygon calculator controls and tooltips
    ''' </summary>
    Friend Sub InitializePolygonCalculator()
        Try
            ' Initialize tooltips
            InitializePolygonTooltips()

            ' Initialize units ComboBox
            If CboPolygonUnits IsNot Nothing Then
                If CboPolygonUnits.Items.Count = 0 Then
                    CboPolygonUnits.Items.AddRange({"inches", "millimeters"})
                End If
                _lastUnitIndex = 0 ' Track initial index BEFORE setting selection
                CboPolygonUnits.SelectedIndex = 0 ' Default to inches

                ' Explicitly wire up the event handler to ensure it fires
                AddHandler CboPolygonUnits.SelectedIndexChanged, AddressOf OnPolygonUnitsChanged
            End If

            ' Initialize NumericUpDown
            If NudPolygonDimension IsNot Nothing Then
                NudPolygonDimension.DecimalPlaces = 4
                NudPolygonDimension.Increment = 0.125D
                NudPolygonDimension.Minimum = MIN_DIMENSION
                NudPolygonDimension.Maximum = MAX_DIMENSION
                NudPolygonDimension.Value = DEFAULT_DIMENSION
            End If

            ' Set default number of sides to 3 (triangle)
            If TxtPolygonSides IsNot Nothing Then
                TxtPolygonSides.Text = MIN_POLYGON_SIDES.ToString()
            End If

            ' Set default radio button
            If RbSidelength IsNot Nothing Then
                RbSidelength.Checked = True
            End If

            ' Initial calculation
            CalculateAndDisplayResults()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializePolygonCalculator")
        End Try
    End Sub

    ''' <summary>
    ''' Initialize tooltips for polygon calculator controls
    ''' </summary>
    Private Sub InitializePolygonTooltips()
        Try
            If _polygonTooltip Is Nothing Then
                _polygonTooltip = New ToolTip()
            End If

            Dim tooltip As ToolTip = _polygonTooltip

            If TxtPolygonSides IsNot Nothing Then
                tooltip.SetToolTip(TxtPolygonSides,
                    "Enter number of sides (3-25)" & vbCrLf &
                    "Common: Triangle(3), Square(4), Pentagon(5)" & vbCrLf &
                    "Hexagon(6), Octagon(8), Dodecagon(12)")
            End If

            If RbSidelength IsNot Nothing Then
                tooltip.SetToolTip(RbSidelength,
                    "Calculate from side length measurement" & vbCrLf &
                    "Use when you know the length of each edge")
            End If

            If RbRadius IsNot Nothing Then
                tooltip.SetToolTip(RbRadius,
                    "Calculate from radius (circumradius)" & vbCrLf &
                    "Distance from center to any vertex")
            End If

            If NudPolygonDimension IsNot Nothing Then
                tooltip.SetToolTip(NudPolygonDimension,
                    "Enter the dimension value" & vbCrLf &
                    "Side length OR radius depending on selection above")
            End If

            If CboPolygonUnits IsNot Nothing Then
                tooltip.SetToolTip(CboPolygonUnits,
                    "Select measurement unit" & vbCrLf &
                    "inches - Imperial (US standard)" & vbCrLf &
                    "millimeters - Metric")
            End If

            If BtnPolyTriangle IsNot Nothing Then
                tooltip.SetToolTip(BtnPolyTriangle, "Quick select: 3-sided polygon")
            End If

            If BtnPolySquare IsNot Nothing Then
                tooltip.SetToolTip(BtnPolySquare, "Quick select: 4-sided polygon (square)")
            End If

            If BtnPolyHexagon IsNot Nothing Then
                tooltip.SetToolTip(BtnPolyHexagon, "Quick select: 6-sided polygon (most common)")
            End If

            If BtnPolyOctagon IsNot Nothing Then
                tooltip.SetToolTip(BtnPolyOctagon, "Quick select: 8-sided polygon")
            End If

            If BtnPolyCalc IsNot Nothing Then
                tooltip.SetToolTip(BtnPolyCalc,
                    "Calculate polygon dimensions" & vbCrLf &
                    "Updates all results based on current inputs")
            End If

            If BtnCopyPolyResults IsNot Nothing Then
                tooltip.SetToolTip(BtnCopyPolyResults,
                    "Copy all calculation results to clipboard" & vbCrLf &
                    "Paste into spreadsheet or text editor")
            End If

            If BtnResetPolygon IsNot Nothing Then
                tooltip.SetToolTip(BtnResetPolygon,
                    "Reset to default values" & vbCrLf &
                    "3 sides, 5 inch side length")
            End If

            If PbPolygon IsNot Nothing Then
                tooltip.SetToolTip(PbPolygon,
                    "Click to pause/resume rotation animation" & vbCrLf &
                    "Visual representation of polygon geometry")
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializePolygonTooltips")
        End Try
    End Sub

#End Region

#Region "Event Handlers"

    ''' <summary>
    ''' Preset button clicks - set number of sides
    ''' </summary>
    Private Sub BtnPolyTriangle_Click(sender As Object, e As EventArgs) Handles BtnPolyTriangle.Click
        TxtPolygonSides.Text = "3"
    End Sub

    Private Sub BtnPolySquare_Click(sender As Object, e As EventArgs) Handles BtnPolySquare.Click
        TxtPolygonSides.Text = "4"
    End Sub

    Private Sub BtnPolyHexagon_Click(sender As Object, e As EventArgs) Handles BtnPolyHexagon.Click
        TxtPolygonSides.Text = "6"
    End Sub

    Private Sub BtnPolyOctagon_Click(sender As Object, e As EventArgs) Handles BtnPolyOctagon.Click
        TxtPolygonSides.Text = "8"
    End Sub

    ''' <summary>
    ''' Calculate button - manually trigger calculation
    ''' </summary>
    Private Sub BtnPolyCalc_Click(sender As Object, e As EventArgs) Handles BtnPolyCalc.Click
        CalculateAndDisplayResults()
    End Sub

    ''' <summary>
    ''' Radio button selection changed - update input prompt and recalculate
    ''' </summary>
    Private Sub RbSidelength_CheckedChanged(sender As Object, e As EventArgs) Handles RbSidelength.CheckedChanged
        If RbSidelength.Checked Then
            If LblPolyDimensionInput IsNot Nothing Then
                LblPolyDimensionInput.Text = "Enter Side Length:"
            End If
            CalculateAndDisplayResults()
        End If
    End Sub

    Private Sub RbRadius_CheckedChanged(sender As Object, e As EventArgs) Handles RbRadius.CheckedChanged
        If RbRadius.Checked Then
            If LblPolyDimensionInput IsNot Nothing Then
                LblPolyDimensionInput.Text = "Enter Radius:"
            End If
            CalculateAndDisplayResults()
        End If
    End Sub

    ''' <summary>
    ''' Dimension value changed - recalculate all geometry
    ''' </summary>
    Private Sub NudPolygonDimension_ValueChanged(sender As Object, e As EventArgs) Handles NudPolygonDimension.ValueChanged
        If Not _suppressDimensionUpdate Then
            CalculateAndDisplayResults()
        End If
    End Sub

    ''' <summary>
    ''' Unit selection changed - convert dimension and recalculate
    ''' </summary>
    Private Sub OnPolygonUnitsChanged(sender As Object, e As EventArgs)
        ' Guard clauses
        If CboPolygonUnits Is Nothing Then Return
        If NudPolygonDimension Is Nothing Then Return
        If CboPolygonUnits.SelectedIndex < 0 Then Return

        Try
            Dim newIndex As Integer = CboPolygonUnits.SelectedIndex

            ' Skip if same index (no actual change)
            If newIndex = _lastUnitIndex Then
                Return
            End If

            ' Get current value before conversion
            Dim currentValue As Decimal = NudPolygonDimension.Value

            ' Suppress the ValueChanged event during conversion
            _suppressDimensionUpdate = True

            If currentValue > 0 Then
                If newIndex = 0 AndAlso _lastUnitIndex = 1 Then
                    ' Converting FROM mm TO inches (index 1 -> 0)
                    NudPolygonDimension.Value = Math.Round(CDec(currentValue / MM_PER_INCH), 4)
                ElseIf newIndex = 1 AndAlso _lastUnitIndex = 0 Then
                    ' Converting FROM inches TO mm (index 0 -> 1)
                    NudPolygonDimension.Value = Math.Round(CDec(currentValue * MM_PER_INCH), 2)
                End If
            End If

            ' Update tracked index
            _lastUnitIndex = newIndex

            ' Re-enable updates and recalculate
            _suppressDimensionUpdate = False
            CalculateAndDisplayResults()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "OnPolygonUnitsChanged")
            _suppressDimensionUpdate = False
        End Try
    End Sub

    ''' <summary>
    ''' Copy results to clipboard
    ''' </summary>
    Private Sub BtnCopyPolyResults_Click(sender As Object, e As EventArgs) Handles BtnCopyPolyResults.Click
        Try
            Dim results As New System.Text.StringBuilder()

            results.AppendLine("POLYGON CALCULATION RESULTS")
            results.AppendLine("=" & New String("="c, 40))
            results.AppendLine()
            results.AppendLine($"Number of Sides: {TxtPolygonSides.Text}")
            results.AppendLine()

            If LblPolygonInteriorAngle IsNot Nothing Then
                results.AppendLine(LblPolygonInteriorAngle.Text)
            End If

            If LblPolygonSideAngle IsNot Nothing Then
                results.AppendLine(LblPolygonSideAngle.Text)
            End If

            If LblPolygonPieceAngle IsNot Nothing Then
                results.AppendLine(LblPolygonPieceAngle.Text)
            End If

            results.AppendLine()

            If LblPolygonSideLengthResult IsNot Nothing AndAlso LblPolygonSideLengthResult.Visible Then
                results.AppendLine(LblPolygonSideLengthResult.Text)
            End If

            If LblPolygonRadiusResult IsNot Nothing AndAlso LblPolygonRadiusResult.Visible Then
                results.AppendLine(LblPolygonRadiusResult.Text)
            End If

            If LblPolygonApothem IsNot Nothing Then
                results.AppendLine(LblPolygonApothem.Text)
            End If

            If LblPolygonPerimeter IsNot Nothing Then
                results.AppendLine(LblPolygonPerimeter.Text)
            End If

            If LblPolygonArea IsNot Nothing Then
                results.AppendLine(LblPolygonArea.Text)
            End If

            Clipboard.SetText(results.ToString())

            MessageBox.Show("Results copied to clipboard!", "Success",
                          MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnCopyPolyResults_Click")
            MessageBox.Show($"Error copying results: {ex.Message}", "Error",
                          MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Reset to default values
    ''' </summary>
    Private Sub BtnResetPolygon_Click(sender As Object, e As EventArgs) Handles BtnResetPolygon.Click
        Try
            _lastUnitIndex = 0 ' Reset tracked index BEFORE changing combobox
            TxtPolygonSides.Text = "3"
            NudPolygonDimension.Value = DEFAULT_DIMENSION
            CboPolygonUnits.SelectedIndex = 0 ' inches
            RbSidelength.Checked = True
            RotationEnabled = True
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BtnResetPolygon_Click")
        End Try
    End Sub

#End Region

#Region "Calculation Methods"

    ''' <summary>
    ''' Calculate and display all polygon geometry
    ''' </summary>
    Private Sub CalculateAndDisplayResults()
        Try
            If NudPolygonDimension Is Nothing OrElse NudPolygonDimension.Value = 0 Then
                ClearPolygonResults()
                Return
            End If

            Dim sides As Integer
            If Not Integer.TryParse(TxtPolygonSides.Text, sides) Then
                ClearPolygonResults()
                Return
            End If

            If sides < MIN_POLYGON_SIDES OrElse sides > MAX_POLYGON_SIDES Then
                ClearPolygonResults()
                Return
            End If

            Dim dimensionValue As Double = CDbl(NudPolygonDimension.Value)

            ' Use index for unit detection (0 = inches, 1 = millimeters)
            Dim isInches As Boolean = (CboPolygonUnits Is Nothing OrElse CboPolygonUnits.SelectedIndex = 0)
            Dim unitAbbrev As String = If(isInches, "in", "mm")
            ' For area: use sq.in for imperial, sq.cm for metric (more practical than sq.mm)
            Dim unitAbbrevSq As String = If(isInches, "sq.in", "sq.cm")

            ' Calculate geometry based on input mode
            Dim sideLength As Double
            Dim radius As Double
            Dim apothem As Double
            Dim perimeter As Double
            Dim area As Double
            Dim interiorAngle As Double

            If RbSidelength IsNot Nothing AndAlso RbSidelength.Checked Then
                ' User entered side length
                sideLength = dimensionValue
                radius = CalculateRadiusFromSide(sideLength, sides)
                apothem = CalculateApothemFromSide(sideLength, sides)
            Else
                ' User entered radius
                radius = dimensionValue
                sideLength = CalculateSideFromRadius(radius, sides)
                apothem = CalculateApothemFromRadius(radius, sides)
            End If

            perimeter = CalculatePerimeter(sideLength, sides)
            area = CalculateArea(sideLength, sides)
            interiorAngle = CalculateInteriorAngle(sides)

            ' Convert area from mm² to cm² for metric display (1 cm² = 100 mm²)
            If Not isInches Then
                area /= 100.0
            End If

            ' Display results
            DisplayResults(sides, sideLength, radius, apothem, perimeter, area,
                          interiorAngle, unitAbbrev, unitAbbrevSq)
        Catch ex As Exception
            ErrorHandler.LogError(ex, "CalculateAndDisplayResults")
        End Try
    End Sub

    ''' <summary>
    ''' Calculate interior angle
    ''' </summary>
    Private Function CalculateInteriorAngle(sides As Integer) As Double
        Return (sides - 2) * 180.0 / sides
    End Function

    ''' <summary>
    ''' Calculate radius (circumradius) from side length
    ''' </summary>
    Private Function CalculateRadiusFromSide(sideLength As Double, sides As Integer) As Double
        Return sideLength / (2 * Math.Sin(Math.PI / sides))
    End Function

    ''' <summary>
    ''' Calculate side length from radius (circumradius)
    ''' </summary>
    Private Function CalculateSideFromRadius(radius As Double, sides As Integer) As Double
        Return 2 * radius * Math.Sin(Math.PI / sides)
    End Function

    ''' <summary>
    ''' Calculate apothem (inradius) from side length
    ''' </summary>
    Private Function CalculateApothemFromSide(sideLength As Double, sides As Integer) As Double
        Return sideLength / (2 * Math.Tan(Math.PI / sides))
    End Function

    ''' <summary>
    ''' Calculate apothem (inradius) from radius (circumradius)
    ''' </summary>
    Private Function CalculateApothemFromRadius(radius As Double, sides As Integer) As Double
        Return radius * Math.Cos(Math.PI / sides)
    End Function

    ''' <summary>
    ''' Calculate perimeter
    ''' </summary>
    Private Function CalculatePerimeter(sideLength As Double, sides As Integer) As Double
        Return sideLength * sides
    End Function

    ''' <summary>
    ''' Calculate area
    ''' </summary>
    Private Function CalculateArea(sideLength As Double, sides As Integer) As Double
        Return (sides * sideLength * sideLength) / (4 * Math.Tan(Math.PI / sides))
    End Function

    ''' <summary>
    ''' Display all calculated results
    ''' </summary>
    Private Sub DisplayResults(sides As Integer, sideLength As Double, radius As Double,
                              apothem As Double, perimeter As Double, area As Double,
                              interiorAngle As Double, unit As String, unitSq As String)
        Try
            ' Interior angle
            If LblPolygonInteriorAngle IsNot Nothing Then
                Dim formatStr As String = GetFormatString(LblPolygonInteriorAngle.Tag, "Interior Angle: {0:F2}°")
                LblPolygonInteriorAngle.Text = String.Format(formatStr, interiorAngle)
            End If

            ' Show/hide derived dimensions based on input mode
            Dim enteringSide As Boolean = RbSidelength IsNot Nothing AndAlso RbSidelength.Checked

            If LblPolygonSideLengthResult IsNot Nothing Then
                If Not enteringSide Then
                    ' User entered radius, show calculated side
                    LblPolygonSideLengthResult.Visible = True
                    Dim formatStr As String = GetFormatString(LblPolygonSideLengthResult.Tag, "Side Length: {0:F3} {1}")
                    LblPolygonSideLengthResult.Text = String.Format(formatStr, sideLength, unit)
                Else
                    ' User entered side, hide this result
                    LblPolygonSideLengthResult.Visible = False
                End If
            End If

            If LblPolygonRadiusResult IsNot Nothing Then
                If enteringSide Then
                    ' User entered side, show calculated radius
                    LblPolygonRadiusResult.Visible = True
                    Dim formatStr As String = GetFormatString(LblPolygonRadiusResult.Tag, "Radius: {0:F3} {1}")
                    LblPolygonRadiusResult.Text = String.Format(formatStr, radius, unit)
                Else
                    ' User entered radius, hide this result
                    LblPolygonRadiusResult.Visible = False
                End If
            End If

            ' Apothem - ALWAYS show
            If LblPolygonApothem IsNot Nothing Then
                Dim formatStr As String = GetFormatString(LblPolygonApothem.Tag, "Apothem: {0:F3} {1}")
                LblPolygonApothem.Text = String.Format(formatStr, apothem, unit)
                LblPolygonApothem.Visible = True
            End If

            ' Perimeter - ALWAYS show
            If LblPolygonPerimeter IsNot Nothing Then
                Dim formatStr As String = GetFormatString(LblPolygonPerimeter.Tag, "Perimeter: {0:F3} {1}")
                LblPolygonPerimeter.Text = String.Format(formatStr, perimeter, unit)
                LblPolygonPerimeter.Visible = True
            End If

            ' Area - ALWAYS show
            If LblPolygonArea IsNot Nothing Then
                Dim formatStr As String = GetFormatString(LblPolygonArea.Tag, "Area: {0:F3} {1}")
                LblPolygonArea.Text = String.Format(formatStr, area, unitSq)
                LblPolygonArea.Visible = True
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DisplayResults")
        End Try
    End Sub

    ''' <summary>
    ''' Clear all result labels
    ''' </summary>
    Private Sub ClearPolygonResults()
        Try
            If LblPolygonInteriorAngle IsNot Nothing Then
                LblPolygonInteriorAngle.Text = "Interior Angle:"
            End If

            If LblPolygonSideLengthResult IsNot Nothing Then
                LblPolygonSideLengthResult.Text = "Side Length:"
                LblPolygonSideLengthResult.Visible = False
            End If

            If LblPolygonRadiusResult IsNot Nothing Then
                LblPolygonRadiusResult.Text = "Radius:"
                LblPolygonRadiusResult.Visible = False
            End If

            If LblPolygonApothem IsNot Nothing Then
                LblPolygonApothem.Text = "Apothem:"
            End If

            If LblPolygonPerimeter IsNot Nothing Then
                LblPolygonPerimeter.Text = "Perimeter:"
            End If

            If LblPolygonArea IsNot Nothing Then
                LblPolygonArea.Text = "Area:"
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "ClearPolygonResults")
        End Try
    End Sub

#End Region

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
