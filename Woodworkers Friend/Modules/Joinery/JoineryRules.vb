' ============================================================================
' Last Updated: January 27, 2026
' Changes: Initial creation - Joinery dimension rules and best practices
'          for mortise & tenon, dovetails, box joints, and other joinery
' ============================================================================

''' <summary>
''' Standard joinery ratios and rules based on woodworking best practices
''' </summary>
Public Module JoineryRules

    ' Mortise & Tenon Rules
    Public Const TENON_THICKNESS_RATIO As Double = 0.33       ' 1/3 of stock thickness

    Public Const MORTISE_DEPTH_RATIO As Double = 0.75         ' 3/4 of stock width
    Public Const TENON_LENGTH_RATIO As Double = 1.0           ' Equal to mortise depth
    Public Const SHOULDER_OFFSET As Double = 0.125            ' 1/8" typical shoulder
    Public Const MIN_TENON_THICKNESS As Double = 0.25         ' 1/4" minimum
    Public Const MAX_TENON_THICKNESS As Double = 1.0          ' 1" maximum

    ' Dovetail Rules
    Public Const DOVETAIL_HARDWOOD_ANGLE As Double = 8.0      ' 1:8 slope for hardwood

    Public Const DOVETAIL_SOFTWOOD_ANGLE As Double = 7.0      ' 1:7 slope for softwood
    Public Const DOVETAIL_MIN_PIN_WIDTH As Double = 0.0625    ' 1/16" minimum pin
    Public Const DOVETAIL_TAIL_SPACING_RATIO As Double = 2.5  ' Tail width to pin width

    ' Box Joint (Finger Joint) Rules
    Public Const BOXJOINT_PIN_WIDTH_RATIO As Double = 0.5     ' Half the stock thickness

    Public Const BOXJOINT_MIN_PIN_WIDTH As Double = 0.125     ' 1/8" minimum
    Public Const BOXJOINT_MAX_PIN_WIDTH As Double = 0.75      ' 3/4" maximum

    ' Dado/Groove Rules
    Public Const DADO_DEPTH_RATIO As Double = 0.5             ' Half stock thickness

    Public Const DADO_MIN_DEPTH As Double = 0.125             ' 1/8" minimum
    Public Const GROOVE_WIDTH_RATIO As Double = 0.33          ' 1/3 stock thickness (for plywood)

    ' General Rules
    Public Const MIN_STOCK_THICKNESS As Double = 0.375        ' 3/8" minimum

    Public Const MAX_STOCK_THICKNESS As Double = 3.0          ' 3" maximum typical
    Public Const MIN_STOCK_WIDTH As Double = 1.0              ' 1" minimum

End Module

''' <summary>
''' Data structure for joint dimensions
''' </summary>
Public Structure JointDimensions
    Public Property TenonThickness As Double
    Public Property TenonLength As Double
    Public Property TenonWidth As Double
    Public Property MortiseDepth As Double
    Public Property MortiseWidth As Double
    Public Property ShoulderOffset As Double
    Public Property JointType As String
End Structure

''' <summary>
''' Data structure for dovetail dimensions
''' </summary>
Public Structure DovetailDimensions
    Public Property PinWidth As Double
    Public Property TailWidth As Double
    Public Property Angle As Double
    Public Property NumberOfTails As Integer
    Public Property BoardThickness As Double
    Public Property BoardWidth As Double
End Structure
