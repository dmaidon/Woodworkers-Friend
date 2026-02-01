' ============================================================================
' Last Updated: February 1, 2026
' Changes: Initial creation - Miter and bevel angle calculator for various
'          frame types including flat frames, tilted frames, crown molding,
'          and polygonal projects like picture frames
' ============================================================================

Imports System.Math

''' <summary>
''' Calculates miter and bevel angles for woodworking projects including
''' flat frames, tilted frames, crown molding, and polygonal projects
''' </summary>
Public Class MiterAngleCalculator

#Region "Enumerations"

    ''' <summary>Frame type for miter calculations</summary>
    Public Enum FrameType
        FlatFrame = 0           ' Simple flat picture frame or box
        TiltedFrame = 1         ' Frame with tilted sides (e.g., shadow box)
        CrownMolding = 2        ' Crown molding with spring angle
        PolygonalProject = 3    ' Multi-sided polygonal frames
    End Enum

#End Region

#Region "Data Structures"

    ''' <summary>Results from miter angle calculation</summary>
    Public Structure MiterAngleResult
        Public MiterAngle As Double         ' Miter angle (horizontal cut angle)
        Public BevelAngle As Double         ' Bevel angle (vertical tilt angle)
        Public CornerAngle As Double        ' Corner angle of the frame
        Public NumberOfSides As Integer     ' Number of sides (for polygonal)
        Public FrameType As FrameType       ' Type of frame
        Public SpringAngle As Double        ' Spring angle (for crown molding)
        Public TiltAngle As Double          ' Tilt angle (for tilted frames)
        Public Description As String        ' Human-readable description
    End Structure

#End Region

#Region "Flat Frame Calculations"

    ''' <summary>
    ''' Calculate miter angle for a flat frame (simple picture frame)
    ''' For a 90-degree corner, miter angle is 45 degrees
    ''' </summary>
    ''' <param name="cornerAngle">Angle at the corner in degrees (typically 90)</param>
    ''' <returns>Miter angle result</returns>
    Public Shared Function CalculateFlatFrameMiter(cornerAngle As Double) As MiterAngleResult
        ' Validate input
        If cornerAngle <= 0 OrElse cornerAngle >= 180 Then
            Throw New ArgumentOutOfRangeException(NameOf(cornerAngle), "Corner angle must be between 0 and 180 degrees")
        End If

        ' For a flat frame, miter angle = corner angle / 2
        Dim miterAngle As Double = cornerAngle / 2.0
        Dim bevelAngle As Double = 0.0  ' No bevel for flat frames

        Return New MiterAngleResult With {
            .MiterAngle = miterAngle,
            .BevelAngle = bevelAngle,
            .CornerAngle = cornerAngle,
            .NumberOfSides = 4,
            .FrameType = FrameType.FlatFrame,
            .SpringAngle = 0,
            .TiltAngle = 0,
            .Description = $"Flat frame with {cornerAngle:N1}° corners"
        }
    End Function

    ''' <summary>
    ''' Calculate miter angle for a polygonal flat frame (e.g., hexagon picture frame)
    ''' </summary>
    ''' <param name="numberOfSides">Number of sides in the polygon (3-25)</param>
    ''' <returns>Miter angle result</returns>
    Public Shared Function CalculatePolygonalFrameMiter(numberOfSides As Integer) As MiterAngleResult
        ' Validate input
        If numberOfSides < 3 OrElse numberOfSides > 25 Then
            Throw New ArgumentOutOfRangeException(NameOf(numberOfSides), "Number of sides must be between 3 and 25")
        End If

        ' Interior angle of regular polygon = (n-2) * 180 / n
        Dim interiorAngle As Double = ((numberOfSides - 2) * 180.0) / numberOfSides
        
        ' Miter angle = (180 - interior angle) / 2
        ' Or equivalently: miter angle = 90 - (interior angle / 2)
        Dim miterAngle As Double = 90.0 - (interiorAngle / 2.0)
        Dim bevelAngle As Double = 0.0  ' No bevel for flat polygons

        Dim polygonName As String = GetPolygonName(numberOfSides)

        Return New MiterAngleResult With {
            .MiterAngle = miterAngle,
            .BevelAngle = bevelAngle,
            .CornerAngle = interiorAngle,
            .NumberOfSides = numberOfSides,
            .FrameType = FrameType.PolygonalProject,
            .SpringAngle = 0,
            .TiltAngle = 0,
            .Description = $"{polygonName} frame ({numberOfSides} sides)"
        }
    End Function

#End Region

#Region "Tilted Frame Calculations"

    ''' <summary>
    ''' Calculate miter and bevel angles for a tilted frame (e.g., shadow box with slanted sides)
    ''' Uses compound miter formulas
    ''' </summary>
    ''' <param name="cornerAngle">Angle at the corner in degrees (typically 90)</param>
    ''' <param name="tiltAngle">Tilt angle from vertical in degrees</param>
    ''' <returns>Miter angle result with both miter and bevel angles</returns>
    Public Shared Function CalculateTiltedFrameMiter(cornerAngle As Double, tiltAngle As Double) As MiterAngleResult
        ' Validate inputs
        If cornerAngle <= 0 OrElse cornerAngle >= 180 Then
            Throw New ArgumentOutOfRangeException(NameOf(cornerAngle), "Corner angle must be between 0 and 180 degrees")
        End If
        If tiltAngle < 0 OrElse tiltAngle >= 90 Then
            Throw New ArgumentOutOfRangeException(NameOf(tiltAngle), "Tilt angle must be between 0 and 90 degrees")
        End If

        ' Convert to radians
        Dim cornerRad As Double = cornerAngle * PI / 180.0
        Dim tiltRad As Double = tiltAngle * PI / 180.0

        ' Compound miter formulas
        ' Miter angle: tan(M) = cos(T) * tan(C/2)
        ' Bevel angle: sin(B) = sin(T) * sin(C/2)
        
        Dim miterAngleRad As Double = Atan(Cos(tiltRad) * Tan(cornerRad / 2.0))
        Dim bevelAngleRad As Double = Asin(Sin(tiltRad) * Sin(cornerRad / 2.0))

        ' Convert back to degrees
        Dim miterAngle As Double = miterAngleRad * 180.0 / PI
        Dim bevelAngle As Double = bevelAngleRad * 180.0 / PI

        Return New MiterAngleResult With {
            .MiterAngle = miterAngle,
            .BevelAngle = bevelAngle,
            .CornerAngle = cornerAngle,
            .NumberOfSides = 4,
            .FrameType = FrameType.TiltedFrame,
            .SpringAngle = 0,
            .TiltAngle = tiltAngle,
            .Description = $"Tilted frame with {tiltAngle:N1}° tilt and {cornerAngle:N1}° corners"
        }
    End Function

#End Region

#Region "Crown Molding Calculations"

    ''' <summary>
    ''' Calculate miter and bevel angles for crown molding
    ''' Crown molding sits at a spring angle (typically 38° or 45°) against wall and ceiling
    ''' </summary>
    ''' <param name="cornerAngle">Angle at the corner in degrees (typically 90 for inside/outside corners)</param>
    ''' <param name="springAngle">Spring angle of crown molding (typically 38° or 45°)</param>
    ''' <param name="isInsideCorner">True for inside corner, False for outside corner</param>
    ''' <returns>Miter angle result for crown molding</returns>
    Public Shared Function CalculateCrownMoldingMiter(cornerAngle As Double, springAngle As Double, Optional isInsideCorner As Boolean = True) As MiterAngleResult
        ' Validate inputs
        If cornerAngle <= 0 OrElse cornerAngle >= 180 Then
            Throw New ArgumentOutOfRangeException(NameOf(cornerAngle), "Corner angle must be between 0 and 180 degrees")
        End If
        If springAngle < 0 OrElse springAngle >= 90 Then
            Throw New ArgumentOutOfRangeException(NameOf(springAngle), "Spring angle must be between 0 and 90 degrees")
        End If

        ' Convert to radians
        Dim cornerRad As Double = cornerAngle * PI / 180.0
        Dim springRad As Double = springAngle * PI / 180.0

        ' Crown molding compound miter formulas
        ' Miter angle: tan(M) = cos(S) * tan(C/2)
        ' Bevel angle: sin(B) = sin(S) * sin(C/2)
        
        Dim miterAngleRad As Double = Atan(Cos(springRad) * Tan(cornerRad / 2.0))
        Dim bevelAngleRad As Double = Asin(Sin(springRad) * Sin(cornerRad / 2.0))

        ' Convert back to degrees
        Dim miterAngle As Double = miterAngleRad * 180.0 / PI
        Dim bevelAngle As Double = bevelAngleRad * 180.0 / PI

        ' For outside corners, adjust angles
        If Not isInsideCorner Then
            miterAngle = 90.0 - miterAngle
            bevelAngle = -bevelAngle
        End If

        Dim cornerType As String = If(isInsideCorner, "inside", "outside")

        Return New MiterAngleResult With {
            .MiterAngle = miterAngle,
            .BevelAngle = bevelAngle,
            .CornerAngle = cornerAngle,
            .NumberOfSides = 4,
            .FrameType = FrameType.CrownMolding,
            .SpringAngle = springAngle,
            .TiltAngle = 0,
            .Description = $"Crown molding ({springAngle:N1}° spring) - {cornerType} corner at {cornerAngle:N1}°"
        }
    End Function

#End Region

#Region "Helper Methods"

    ''' <summary>
    ''' Get the common name for a polygon based on number of sides
    ''' </summary>
    Private Shared Function GetPolygonName(sides As Integer) As String
        Select Case sides
            Case 3 : Return "Triangle"
            Case 4 : Return "Square"
            Case 5 : Return "Pentagon"
            Case 6 : Return "Hexagon"
            Case 7 : Return "Heptagon"
            Case 8 : Return "Octagon"
            Case 9 : Return "Nonagon"
            Case 10 : Return "Decagon"
            Case 12 : Return "Dodecagon"
            Case Else : Return $"{sides}-sided polygon"
        End Select
    End Function

    ''' <summary>
    ''' Get common spring angle presets for crown molding
    ''' </summary>
    Public Shared Function GetCrownSpringAnglePresets() As Dictionary(Of String, Double)
        Return New Dictionary(Of String, Double) From {
            {"38° (Common)", 38.0},
            {"45° (Standard)", 45.0},
            {"52° (Steep)", 52.0}
        }
    End Function

    ''' <summary>
    ''' Get common corner angle presets
    ''' </summary>
    Public Shared Function GetCornerAnglePresets() As Dictionary(Of String, Double)
        Return New Dictionary(Of String, Double) From {
            {"90° (Square corner)", 90.0},
            {"135° (Octagon corner)", 135.0},
            {"120° (Hexagon corner)", 120.0},
            {"108° (Pentagon corner)", 108.0}
        }
    End Function

    ''' <summary>
    ''' Validate that saw can cut the required angles
    ''' Most miter saws can cut 0-50° miter and 0-45° bevel
    ''' </summary>
    Public Shared Function ValidateSawCapability(result As MiterAngleResult, 
                                                   Optional maxMiter As Double = 50.0, 
                                                   Optional maxBevel As Double = 45.0) As (IsValid As Boolean, Message As String)
        Dim absMiter As Double = Abs(result.MiterAngle)
        Dim absBevel As Double = Abs(result.BevelAngle)

        If absMiter > maxMiter Then
            Return (False, $"Miter angle ({absMiter:N1}°) exceeds saw capacity ({maxMiter:N1}°)")
        End If

        If absBevel > maxBevel Then
            Return (False, $"Bevel angle ({absBevel:N1}°) exceeds saw capacity ({maxBevel:N1}°)")
        End If

        Return (True, "Angles are within saw capability")
    End Function

#End Region

End Class
