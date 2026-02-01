' ============================================================================
' Last Updated: January 27, 2026
' Changes: Initial creation - Wood movement calculator using EMC tables
'          and species shrinkage coefficients
' ============================================================================

''' <summary>
''' Calculates wood movement based on moisture content changes
''' </summary>
Public Class WoodMovementCalculator

    ''' <summary>
    ''' Grain orientation for movement calculation
    ''' </summary>
    Public Enum GrainOrientation
        Tangential  ' Flat sawn - most movement
        Radial      ' Quarter sawn - less movement
    End Enum

    ''' <summary>
    ''' Calculates expected wood movement
    ''' </summary>
    ''' <param name="species">Wood species</param>
    ''' <param name="widthInches">Width of board (across grain)</param>
    ''' <param name="initialHumidity">Starting relative humidity %</param>
    ''' <param name="finalHumidity">Ending relative humidity %</param>
    ''' <param name="orientation">Grain orientation</param>
    ''' <returns>Movement in inches (positive = expansion, negative = shrinkage)</returns>
    Public Shared Function CalculateMovement(
        species As WoodSpeciesLegacy,
        widthInches As Double,
        initialHumidity As Double,
        finalHumidity As Double,
        orientation As GrainOrientation) As Double

        ArgumentNullException.ThrowIfNull(species)

        ' Convert humidity to approximate moisture content (EMC)
        Dim initialMC As Double = HumidityToMoistureContent(initialHumidity)
        Dim finalMC As Double = HumidityToMoistureContent(finalHumidity)

        ' Calculate moisture content change
        Dim mcChange As Double = finalMC - initialMC

        ' Get shrinkage coefficient based on orientation
        Dim shrinkageCoefficient As Double
        Select Case orientation
            Case GrainOrientation.Tangential
                shrinkageCoefficient = species.TangentialShrinkage
            Case GrainOrientation.Radial
                shrinkageCoefficient = species.RadialShrinkage
            Case Else
                shrinkageCoefficient = species.TangentialShrinkage
        End Select

        ' Calculate movement
        ' Formula: Movement = Width × (Shrinkage% / 100) × ΔMC
        Dim movement As Double = widthInches * (shrinkageCoefficient / 100) * mcChange

        Return movement
    End Function

    ''' <summary>
    ''' Converts relative humidity % to equilibrium moisture content (EMC)
    ''' Simplified Hailwood-Horrobin equation
    ''' </summary>
    Private Shared Function HumidityToMoistureContent(relativeHumidity As Double) As Double
        ' Clamp humidity to reasonable range
        relativeHumidity = InputValidator.Clamp(relativeHumidity, 0, 100)

        ' Convert to decimal (0-1)
        Dim rh As Double = relativeHumidity / 100.0

        ' Simplified EMC calculation (approximation at 70°F)
        ' More accurate would use temperature, but this is good for practical use
        Dim emc As Double

        If rh < 0.1 Then
            emc = 0
        ElseIf rh >= 0.1 AndAlso rh < 0.9 Then
            ' Approximation formula
            emc = 1800 / 99 * (rh / (1 - rh))
            emc /= (1 + (1800 / 99) * (rh / (1 - rh)))
            emc *= 100
        Else
            ' High humidity approximation
            emc = 25  ' Cap at reasonable maximum
        End If

        Return emc
    End Function

    ''' <summary>
    ''' Gets standard humidity levels for different conditions
    ''' </summary>
    Public Shared Function GetStandardHumidityLevels() As Dictionary(Of String, Double)
        Return New Dictionary(Of String, Double) From {
            {"Heated Home (Winter)", 25},
            {"Air Conditioned (Summer)", 45},
            {"Average Indoor", 35},
            {"Basement", 50},
            {"Outdoor (Average)", 65},
            {"Humid Climate", 75},
            {"Dry Climate", 20}
        }
    End Function

    ''' <summary>
    ''' Calculates recommended gap allowance for panel in frame
    ''' </summary>
    Public Shared Function CalculatePanelGap(
        species As WoodSpeciesLegacy,
        panelWidth As Double,
        currentHumidity As Double,
        orientation As GrainOrientation) As (MinGap As Double, MaxGap As Double)

        ' Calculate movement to dry conditions (25% RH)
        Dim shrinkage As Double = Math.Abs(CalculateMovement(
            species, panelWidth, currentHumidity, 25, orientation))

        ' Calculate movement to humid conditions (75% RH)
        Dim expansion As Double = Math.Abs(CalculateMovement(
            species, panelWidth, currentHumidity, 75, orientation))

        ' Recommended gaps (per side)
        ' Account for worst case in each direction with safety margin
        Dim minGap As Double = (expansion / 2) * 1.1  ' 10% safety margin
        Dim maxGap As Double = (shrinkage / 2) * 1.1

        ' Ensure minimums
        minGap = Math.Max(minGap, 0.0625)  ' At least 1/16"
        maxGap = Math.Max(maxGap, 0.0625)

        Return (minGap, maxGap)
    End Function

    ''' <summary>
    ''' Gets movement category for display
    ''' </summary>
    Public Shared Function GetMovementCategory(movementInches As Double) As String
        Dim absMovement = Math.Abs(movementInches)

        If absMovement < 0.03125 Then  ' Less than 1/32"
            Return "Minimal"
        ElseIf absMovement < 0.0625 Then  ' Less than 1/16"
            Return "Low"
        ElseIf absMovement < 0.125 Then  ' Less than 1/8"
            Return "Moderate"
        ElseIf absMovement < 0.25 Then  ' Less than 1/4"
            Return "High"
        Else
            Return "Very High"
        End If
    End Function

End Class
