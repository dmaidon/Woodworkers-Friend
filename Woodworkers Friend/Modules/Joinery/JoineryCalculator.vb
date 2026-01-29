' ============================================================================
' Last Updated: January 27, 2026
' Changes: Initial creation - Joinery calculator for mortise & tenon,
'          dovetails, box joints, and dado calculations
' ============================================================================

''' <summary>
''' Calculates dimensions for various woodworking joints
''' </summary>
Public Class JoineryCalculator

    ''' <summary>
    ''' Calculates mortise and tenon dimensions based on stock dimensions
    ''' </summary>
    Public Shared Function CalculateMortiseAndTenon(
        stockThickness As Double,
        stockWidth As Double,
        tenonType As String) As JointDimensions

        ' Calculate tenon thickness (1/3 of stock)
        Dim result As New JointDimensions With {
            .JointType = tenonType,
            .TenonThickness = stockThickness * JoineryRules.TENON_THICKNESS_RATIO
        }

        ' Ensure minimum thickness
        If result.TenonThickness < JoineryRules.MIN_TENON_THICKNESS Then
            result.TenonThickness = JoineryRules.MIN_TENON_THICKNESS
        End If

        ' Calculate mortise depth (3/4 of stock width)
        result.MortiseDepth = stockWidth * JoineryRules.MORTISE_DEPTH_RATIO

        ' Tenon length matches mortise depth (or slightly less for clearance)
        result.TenonLength = result.MortiseDepth - 0.0625  ' 1/16" clearance

        ' Mortise width matches tenon thickness (snug fit)
        result.MortiseWidth = result.TenonThickness

        ' Calculate shoulder offset
        Select Case tenonType.ToLower()
            Case "standard"
                result.ShoulderOffset = JoineryRules.SHOULDER_OFFSET
            Case "haunched"
                result.ShoulderOffset = JoineryRules.SHOULDER_OFFSET * 1.5
            Case "through"
                result.ShoulderOffset = JoineryRules.SHOULDER_OFFSET
                result.TenonLength = stockWidth  ' Goes all the way through
            Case Else
                result.ShoulderOffset = JoineryRules.SHOULDER_OFFSET
        End Select

        ' Tenon width (across the grain)
        result.TenonWidth = stockWidth - (2 * result.ShoulderOffset)

        Return result
    End Function

    ''' <summary>
    ''' Calculates dovetail joint dimensions
    ''' </summary>
    Public Shared Function CalculateDovetails(
        boardThickness As Double,
        boardWidth As Double,
        isHardwood As Boolean,
        desiredSpacing As Double) As DovetailDimensions

        ' Set angle based on wood type
        ' Calculate pin width (narrow part)
        Dim result As New DovetailDimensions With {
            .BoardThickness = boardThickness,
.BoardWidth = boardWidth,
            .Angle = If(isHardwood, JoineryRules.DOVETAIL_HARDWOOD_ANGLE, JoineryRules.DOVETAIL_SOFTWOOD_ANGLE),
            .PinWidth = Math.Max(desiredSpacing, JoineryRules.DOVETAIL_MIN_PIN_WIDTH)
        }

        ' Calculate tail width (wider part)
        result.TailWidth = result.PinWidth * JoineryRules.DOVETAIL_TAIL_SPACING_RATIO

        ' Calculate number of tails
        Dim spacingUnit As Double = result.TailWidth + result.PinWidth
        result.NumberOfTails = CInt(Math.Floor(boardWidth / spacingUnit))

        ' Adjust for symmetry (half pins on ends)
        If result.NumberOfTails < 2 Then result.NumberOfTails = 2

        Return result
    End Function

    ''' <summary>
    ''' Calculates box joint (finger joint) dimensions
    ''' </summary>
    Public Shared Function CalculateBoxJoint(
        stockThickness As Double,
        boardWidth As Double) As (PinWidth As Double, PinCount As Integer)

        ' Calculate pin width (typically 1/2 stock thickness)
        Dim pinWidth As Double = stockThickness * JoineryRules.BOXJOINT_PIN_WIDTH_RATIO

        ' Clamp to reasonable values
        pinWidth = InputValidator.Clamp(pinWidth,
            JoineryRules.BOXJOINT_MIN_PIN_WIDTH,
            JoineryRules.BOXJOINT_MAX_PIN_WIDTH)

        ' Calculate number of pins
        Dim pinCount As Integer = CInt(Math.Floor(boardWidth / pinWidth))

        ' Adjust for alternating pattern (must be odd for symmetry)
        If pinCount Mod 2 = 0 Then pinCount += 1

        Return (pinWidth, pinCount)
    End Function

    ''' <summary>
    ''' Calculates dado depth and width
    ''' </summary>
    Public Shared Function CalculateDado(
        stockThickness As Double,
        shelfThickness As Double) As (Depth As Double, Width As Double)

        ' Dado depth is typically half the stock thickness
        Dim depth As Double = stockThickness * JoineryRules.DADO_DEPTH_RATIO

        ' Ensure minimum depth
        depth = Math.Max(depth, JoineryRules.DADO_MIN_DEPTH)

        ' Dado width matches shelf thickness (snug fit)
        Dim width As Double = shelfThickness

        Return (depth, width)
    End Function

    ''' <summary>
    ''' Validates joint dimensions are within safe/practical limits
    ''' </summary>
    Public Shared Function ValidateJointDimensions(
        stockThickness As Double,
        stockWidth As Double) As (IsValid As Boolean, ErrorMessage As String)

        If stockThickness < JoineryRules.MIN_STOCK_THICKNESS Then
            Return (False, $"Stock thickness must be at least {JoineryRules.MIN_STOCK_THICKNESS}""")
        End If

        If stockThickness > JoineryRules.MAX_STOCK_THICKNESS Then
            Return (False, $"Stock thickness must be no more than {JoineryRules.MAX_STOCK_THICKNESS}""")
        End If

        If stockWidth < JoineryRules.MIN_STOCK_WIDTH Then
            Return (False, $"Stock width must be at least {JoineryRules.MIN_STOCK_WIDTH}""")
        End If

        ' Check if mortise and tenon will work
        Dim tenonThickness As Double = stockThickness * JoineryRules.TENON_THICKNESS_RATIO
        If tenonThickness < JoineryRules.MIN_TENON_THICKNESS Then
            Return (False, "Stock too thin for practical mortise and tenon joint")
        End If

        Return (True, String.Empty)
    End Function

End Class
