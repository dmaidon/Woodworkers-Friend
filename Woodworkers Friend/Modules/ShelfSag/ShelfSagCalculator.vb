' ============================================================================
' Last Updated: January 29, 2026
' Changes: Initial creation - Shelf sag calculator with deflection formulas,
'          material properties, and safe load calculations
'          Added support type calculations (Bracket vs Dado)
' ============================================================================

''' <summary>
''' Calculates shelf sag (deflection) and safe load limits based on beam theory
''' Uses the standard deflection formula: δ = (5 * w * L^4) / (384 * E * I)
''' where: δ = deflection, w = distributed load, L = span, E = modulus of elasticity, I = moment of inertia
''' </summary>
Public Class ShelfSagCalculator

#Region "Constants"

    ''' <summary>Acceptable sag limit as a ratio of span (1/360 for shelving)</summary>
    Private Const ACCEPTABLE_SAG_RATIO As Double = 1.0 / 360.0

    ''' <summary>Maximum sag ratio (1/240 - structural limit)</summary>
    Private Const MAX_SAG_RATIO As Double = 1.0 / 240.0

    ''' <summary>Safety factor for load calculations</summary>
    Private Const SAFETY_FACTOR As Double = 2.0

#End Region

#Region "Material Properties Database"

    ''' <summary>
    ''' Gets material properties by type
    ''' Modulus of Elasticity (E) values in psi (pounds per square inch)
    ''' </summary>
    Public Shared Function GetMaterialProperties(materialType As ShelfMaterialType) As MaterialProperties
        Select Case materialType
            Case ShelfMaterialType.Plywood
                Return New MaterialProperties With {
                    .Name = "Plywood",
                    .ModulusOfElasticity = 1500000,  ' 1.5 million psi
                    .Density = 35,
                    .Notes = "Typical construction grade"
                }

            Case ShelfMaterialType.MDF
                Return New MaterialProperties With {
                    .Name = "MDF (Medium Density Fiberboard)",
                    .ModulusOfElasticity = 400000,   ' 400k psi
                    .Density = 47,
                    .Notes = "Standard density"
                }

            Case ShelfMaterialType.ParticleBoard
                Return New MaterialProperties With {
                    .Name = "Particleboard",
                    .ModulusOfElasticity = 300000,   ' 300k psi
                    .Density = 43,
                    .Notes = "Standard grade"
                }

            Case ShelfMaterialType.SolidWoodSYPine
                Return New MaterialProperties With {
                    .Name = "Southern Yellow Pine (SYP)",
                    .ModulusOfElasticity = 1640000,  ' 1.64 million psi
                    .Density = 36,
                    .Notes = "Structural grade, higher strength"
                }

            Case ShelfMaterialType.SolidWoodWPine
                Return New MaterialProperties With {
                    .Name = "White Pine (Solid Wood)",
                    .ModulusOfElasticity = 1240000,  ' 1.24 million psi
                    .Density = 25,
                    .Notes = "Eastern White Pine, softer than SYP"
                }

            Case ShelfMaterialType.SolidWoodPoplar
                Return New MaterialProperties With {
                    .Name = "Poplar (Solid Wood)",
                    .ModulusOfElasticity = 1580000,  ' 1.58 million psi
                    .Density = 28,
                    .Notes = "Good paint-grade hardwood"
                }

            Case ShelfMaterialType.SolidWoodOak
                Return New MaterialProperties With {
                    .Name = "Oak (Solid Wood)",
                    .ModulusOfElasticity = 1780000,  ' 1.78 million psi
                    .Density = 47,
                    .Notes = "Red Oak, stronger species"
                }

            Case ShelfMaterialType.SolidWoodMaple
                Return New MaterialProperties With {
                    .Name = "Maple (Solid Wood)",
                    .ModulusOfElasticity = 1830000,  ' 1.83 million psi
                    .Density = 44,
                    .Notes = "Hard Maple, very stiff"
                }

            Case ShelfMaterialType.SolidWoodWalnut
                Return New MaterialProperties With {
                    .Name = "Walnut (Solid Wood)",
                    .ModulusOfElasticity = 1680000,  ' 1.68 million psi
                    .Density = 38,
                    .Notes = "Black Walnut"
                }

            Case ShelfMaterialType.SolidWoodCherry
                Return New MaterialProperties With {
                    .Name = "Cherry (Solid Wood)",
                    .ModulusOfElasticity = 1490000,  ' 1.49 million psi
                    .Density = 35,
                    .Notes = "American Cherry"
                }

            Case ShelfMaterialType.SolidWoodMahogany
                Return New MaterialProperties With {
                    .Name = "Mahogany (Solid Wood)",
                    .ModulusOfElasticity = 1460000,  ' 1.46 million psi
                    .Density = 31,
                    .Notes = "Genuine Mahogany"
                }

            Case ShelfMaterialType.MelamineBoard
                Return New MaterialProperties With {
                    .Name = "Melamine Board",
                    .ModulusOfElasticity = 350000,   ' 350k psi
                    .Density = 45,
                    .Notes = "Particleboard core with melamine surface"
                }

            Case ShelfMaterialType.OSB
                Return New MaterialProperties With {
                    .Name = "OSB (Oriented Strand Board)",
                    .ModulusOfElasticity = 450000,   ' 450k psi
                    .Density = 38,
                    .Notes = "Structural grade"
                }

            Case ShelfMaterialType.Bamboo
                Return New MaterialProperties With {
                    .Name = "Bamboo",
                    .ModulusOfElasticity = 1600000,  ' 1.6 million psi
                    .Density = 43,
                    .Notes = "Strand-woven bamboo"
                }

            Case Else
                Return New MaterialProperties With {
                    .Name = "Unknown",
                    .ModulusOfElasticity = 1000000,
                    .Density = 35,
                    .Notes = "Default values"
                }
        End Select
    End Function

#End Region

#Region "Main Calculation Methods"

    ''' <summary>
    ''' Calculates shelf sag and safe load limits
    ''' </summary>
    Public Shared Function CalculateShelfSag(input As ShelfSagInput) As ShelfSagResult
        ' Get material properties
        Dim material = GetMaterialProperties(input.MaterialType)

        ' Calculate effective span based on support type
        Dim effectiveSpan = CalculateEffectiveSpan(input)

        ' Calculate moment of inertia based on cross-section
        Dim momentOfInertia As Double
        Dim effectiveModulus As Double

        If input.HasFrontStiffener OrElse input.HasBackStiffener Then
            ' Composite beam calculation (T-beam or I-beam)
            Dim composite = CalculateCompositeSection(input, material)
            momentOfInertia = composite.MomentOfInertia
            effectiveModulus = composite.EffectiveModulus
        Else
            ' Simple rectangular beam: I = (b * h^3) / 12
            momentOfInertia = (input.Width * Math.Pow(input.Thickness, 3)) / 12.0
            effectiveModulus = material.ModulusOfElasticity
        End If

        ' Calculate distributed load (pounds per inch)
        Dim distributedLoad = input.Load / effectiveSpan

        ' Calculate deflection based on support type
        Dim deflection As Double
        Select Case input.SupportType
            Case ShelfSupportType.Bracket, ShelfSupportType.Pin
                ' Simple support - standard beam formula: δ = (5 * w * L^4) / (384 * E * I)
                deflection = (5 * distributedLoad * Math.Pow(effectiveSpan, 4)) /
                            (384 * effectiveModulus * momentOfInertia)

            Case ShelfSupportType.Dado
                ' Partial fixity at ends - reduced deflection
                ' Use fixity factor based on dado depth
                Dim fixityFactor = CalculateDadoFixityFactor(input.DadoDepth, input.Thickness)
                ' Fixed-end beam formula reduces deflection: δ = (w * L^4) / (384 * E * I)
                ' Blend between simple and fixed based on fixity factor
                Dim simpleDeflection = (5 * distributedLoad * Math.Pow(effectiveSpan, 4)) /
                                      (384 * effectiveModulus * momentOfInertia)
                Dim fixedDeflection = (distributedLoad * Math.Pow(effectiveSpan, 4)) /
                                     (384 * effectiveModulus * momentOfInertia)
                deflection = simpleDeflection * (1 - fixityFactor) + fixedDeflection * fixityFactor

            Case Else
                ' Default to simple support
                deflection = (5 * distributedLoad * Math.Pow(effectiveSpan, 4)) /
                            (384 * effectiveModulus * momentOfInertia)
        End Select

        ' Calculate acceptable sag for the effective span
        Dim acceptableSag = effectiveSpan * ACCEPTABLE_SAG_RATIO
        Dim maxSag = effectiveSpan * MAX_SAG_RATIO

        ' Calculate safe load (load that produces acceptable sag)
        Dim safeLoad As Double
        Select Case input.SupportType
            Case ShelfSupportType.Bracket, ShelfSupportType.Pin
                safeLoad = (acceptableSag * 384 * effectiveModulus * momentOfInertia) /
                          (5 * Math.Pow(effectiveSpan, 4))
            Case ShelfSupportType.Dado
                Dim fixityFactor = CalculateDadoFixityFactor(input.DadoDepth, input.Thickness)
                Dim simpleSafeLoad = (acceptableSag * 384 * effectiveModulus * momentOfInertia) /
                                    (5 * Math.Pow(effectiveSpan, 4))
                Dim fixedSafeLoad = (acceptableSag * 384 * effectiveModulus * momentOfInertia) /
                                   (Math.Pow(effectiveSpan, 4))
                safeLoad = simpleSafeLoad * (1 - fixityFactor) + fixedSafeLoad * fixityFactor
            Case Else
                safeLoad = (acceptableSag * 384 * effectiveModulus * momentOfInertia) /
                          (5 * Math.Pow(effectiveSpan, 4))
        End Select

        ' Calculate maximum load (load that produces maximum sag)
        Dim maxLoad As Double
        Select Case input.SupportType
            Case ShelfSupportType.Bracket, ShelfSupportType.Pin
                maxLoad = (maxSag * 384 * effectiveModulus * momentOfInertia) /
                         (5 * Math.Pow(effectiveSpan, 4))
            Case ShelfSupportType.Dado
                Dim fixityFactor = CalculateDadoFixityFactor(input.DadoDepth, input.Thickness)
                Dim simpleMaxLoad = (maxSag * 384 * effectiveModulus * momentOfInertia) /
                                   (5 * Math.Pow(effectiveSpan, 4))
                Dim fixedMaxLoad = (maxSag * 384 * effectiveModulus * momentOfInertia) /
                                  (Math.Pow(effectiveSpan, 4))
                maxLoad = simpleMaxLoad * (1 - fixityFactor) + fixedMaxLoad * fixityFactor
            Case Else
                maxLoad = (maxSag * 384 * effectiveModulus * momentOfInertia) /
                         (5 * Math.Pow(effectiveSpan, 4))
        End Select

        ' Calculate safety factor
        Dim safetyFactor = If(input.Load > 0, safeLoad / input.Load, 0)

        ' Determine if current load is safe
        Dim isSafe = input.Load <= safeLoad

        ' Calculate recommended max span for this load and material
        Dim recommendedMaxSpan = CalculateMaxSpan(input.Load, input.Thickness, input.Width, effectiveModulus)

        ' Build warning message if needed
        Dim warningMessage As String = ""
        If Not isSafe Then
            If deflection > maxSag Then
                warningMessage = "UNSAFE: Sag exceeds structural limits! Risk of failure."
            Else
                warningMessage = "WARNING: Sag exceeds recommended limits. Consider additional support."
            End If
        End If

        ' Create result
        Return New ShelfSagResult With {
            .SagInches = deflection,
            .SagMillimeters = deflection * 25.4,
            .SagFraction = ConvertDecimalToFraction(deflection),
            .SafeLoad = safeLoad,
            .MaxLoad = maxLoad,
            .IsSafe = isSafe,
            .SafetyFactor = safetyFactor,
            .ModulusOfElasticity = effectiveModulus,
            .MomentOfInertia = momentOfInertia,
            .RecommendedMaxSpan = recommendedMaxSpan,
            .WarningMessage = warningMessage
        }
    End Function

    ''' <summary>
    ''' Calculates effective span based on support type
    ''' </summary>
    Private Shared Function CalculateEffectiveSpan(input As ShelfSagInput) As Double
        Select Case input.SupportType
            Case ShelfSupportType.Bracket
                ' For bracket support, effective span = total length - (bracket width × 2)
                ' User inputs width of ONE bracket, app multiplies by 2 for both sides
                Return Math.Max(1.0, input.SpanLength - (input.BracketWidth * 2))
            Case ShelfSupportType.Pin
                ' For pin support, effective span = total length - (pin width × 2)
                ' User inputs diameter/width of ONE pin/arrangement, app multiplies by 2 for both sides
                Return Math.Max(1.0, input.SpanLength - (input.PinWidth * 2))
            Case ShelfSupportType.Dado
                ' For dado support, use full span (fixity is handled in deflection formula)
                Return input.SpanLength
            Case Else
                ' Default to full span
                Return input.SpanLength
        End Select
    End Function

    ''' <summary>
    ''' Calculates fixity factor for dado support based on dado depth
    ''' </summary>
    ''' <param name="dadoDepth">Depth of dado groove in inches</param>
    ''' <param name="shelfThickness">Thickness of shelf in inches</param>
    ''' <returns>Fixity factor from 0 (simple support) to 1 (fixed support)</returns>
    Private Shared Function CalculateDadoFixityFactor(dadoDepth As Double, shelfThickness As Double) As Double
        ' Calculate dado depth as percentage of shelf thickness
        Dim depthRatio = dadoDepth / shelfThickness

        ' Fixity factor based on depth ratio (empirical relationship)
        ' Shallow dado (< 25%): ~10% fixity
        ' Medium dado (37.5%): ~25% fixity
        ' Deep dado (50%+): ~40% fixity
        Dim fixityFactor As Double
        If depthRatio < 0.25 Then
            ' Linear interpolation from 0 to 0.10
            fixityFactor = 0.1 * (depthRatio / 0.25)
        ElseIf depthRatio < 0.5 Then
            ' Linear interpolation from 0.10 to 0.40
            fixityFactor = 0.1 + 0.3 * ((depthRatio - 0.25) / 0.25)
        Else
            ' Cap at 40% fixity for deep dados
            fixityFactor = 0.4
        End If

        Return fixityFactor
    End Function

    ''' <summary>
    ''' Calculates maximum recommended span for a given load and material
    ''' </summary>
    Private Shared Function CalculateMaxSpan(
        load As Double,
        thickness As Double,
        width As Double,
        modulusOfElasticity As Double) As Double

        ' Calculate moment of inertia
        Dim momentOfInertia = (width * Math.Pow(thickness, 3)) / 12.0

        ' Use acceptable sag ratio to determine span
        ' Rearrange deflection formula to solve for L
        ' δ = (5 * w * L^4) / (384 * E * I)
        ' L = ((δ * 384 * E * I) / (5 * w))^(1/4)

        ' Assume acceptable sag ratio
        ''Dim targetSag = 1.0  ' Start with 1 inch, will scale with span

        ' Iteratively solve for span (since δ depends on L)
        Dim span = 12.0  ' Start with 12 inches
        For i = 1 To 20
            Dim distributedLoad = load / span
            Dim acceptableSag = span * ACCEPTABLE_SAG_RATIO
            Dim newSpan = Math.Pow((acceptableSag * 384 * modulusOfElasticity * momentOfInertia) /
                                  (5 * distributedLoad), 0.25)
            If Math.Abs(newSpan - span) < 0.1 Then
                Return newSpan
            End If
            span = newSpan
        Next

        Return span
    End Function

    ''' <summary>
    ''' Calculates composite beam properties (T-beam or I-beam with stiffeners)
    ''' Uses parallel axis theorem and transformed section method for different materials
    ''' </summary>
    Private Shared Function CalculateCompositeSection(
        input As ShelfSagInput,
        shelfMaterial As MaterialProperties) As (MomentOfInertia As Double, EffectiveModulus As Double)

        ' Get stiffener material properties
        Dim stiffenerMaterial = GetMaterialProperties(input.StiffenerMaterial)

        ' Calculate modulus ratio for transformed section (if materials differ)
        Dim n = stiffenerMaterial.ModulusOfElasticity / shelfMaterial.ModulusOfElasticity

        ' Base shelf section
        Dim shelfArea = input.Width * input.Thickness

        ' Stiffener sections (transformed to equivalent shelf material)
        Dim stiffenerCount = 0
        If input.HasFrontStiffener Then stiffenerCount += 1
        If input.HasBackStiffener Then stiffenerCount += 1

        Dim stiffenerArea = input.StiffenerHeight * input.StiffenerThickness * n * stiffenerCount
        Dim totalArea = shelfArea + stiffenerArea

        ' Calculate centroid (neutral axis) from bottom of shelf
        ' yBar = Σ(Ai * yi) / Σ(Ai)
        Dim shelfCentroidY = input.Thickness / 2.0
        Dim stiffenerCentroidY = input.Thickness + (input.StiffenerHeight / 2.0)

        Dim yBar = (shelfArea * shelfCentroidY + stiffenerArea * stiffenerCentroidY) / totalArea

        ' Calculate moment of inertia about neutral axis using parallel axis theorem
        ' I = Σ(Ii + Ai * di²)
        ' where Ii = moment of inertia about own centroid, di = distance from own centroid to neutral axis

        ' Shelf section
        Dim IshelfOwn = (input.Width * Math.Pow(input.Thickness, 3)) / 12.0
        Dim dShelf = yBar - shelfCentroidY
        Dim Ishelf = IshelfOwn + shelfArea * Math.Pow(dShelf, 2)

        ' Stiffener section(s)
        Dim IstiffenerOwn = (input.StiffenerThickness * Math.Pow(input.StiffenerHeight, 3)) / 12.0 * n * stiffenerCount
        Dim dStiffener = stiffenerCentroidY - yBar
        Dim Istiffener = IstiffenerOwn + stiffenerArea * Math.Pow(dStiffener, 2)

        ' Total moment of inertia
        Dim Itotal = Ishelf + Istiffener

        ' Effective modulus (shelf material is reference)
        Dim effectiveModulus = shelfMaterial.ModulusOfElasticity

        Return (Itotal, effectiveModulus)
    End Function

    ''' <summary>
    ''' Validates shelf sag input parameters
    ''' </summary>
    Public Shared Function ValidateInput(input As ShelfSagInput) As (IsValid As Boolean, ErrorMessage As String)
        If input Is Nothing Then
            Return (False, "Input cannot be null")
        End If

        If input.SpanLength <= 0 Then
            Return (False, "Span length must be greater than 0")
        End If

        If input.SpanLength > 96 Then
            Return (False, "Span length exceeds reasonable limits (max 96 inches)")
        End If

        If input.Load < 0 Then
            Return (False, "Load cannot be negative")
        End If

        If input.Load > 10000 Then
            Return (False, "Load exceeds reasonable limits (max 10,000 lbs)")
        End If

        If input.Thickness <= 0 Then
            Return (False, "Thickness must be greater than 0")
        End If

        If input.Thickness > 6 Then
            Return (False, "Thickness exceeds reasonable limits (max 6 inches)")
        End If

        If input.Width <= 0 Then
            Return (False, "Width must be greater than 0")
        End If

        If input.Width > 48 Then
            Return (False, "Width exceeds reasonable limits (max 48 inches)")
        End If

        ' Validate support-type-specific parameters
        If input.SupportType = ShelfSupportType.Bracket Then
            If input.BracketWidth < 0 Then
                Return (False, "Bracket width cannot be negative")
            End If

            If input.BracketWidth * 2 >= input.SpanLength Then
                Return (False, "Bracket width (×2) must be less than total span length")
            End If

            If input.BracketWidth > 6 Then
                Return (False, "Bracket width exceeds reasonable limits (max 6 inches per side)")
            End If
        End If

        If input.SupportType = ShelfSupportType.Pin Then
            If input.PinWidth < 0 Then
                Return (False, "Pin width cannot be negative")
            End If

            If input.PinWidth * 2 >= input.SpanLength Then
                Return (False, "Pin width (×2) must be less than total span length")
            End If

            If input.PinWidth > 2 Then
                Return (False, "Pin width exceeds reasonable limits (max 2 inches per side)")
            End If
        End If

        If input.SupportType = ShelfSupportType.Dado Then
            If input.DadoDepth <= 0 Then
                Return (False, "Dado depth must be greater than 0")
            End If

            If input.DadoDepth >= input.Thickness Then
                Return (False, "Dado depth must be less than shelf thickness")
            End If

            If input.DadoDepth > 1 Then
                Return (False, "Dado depth exceeds reasonable limits (max 1 inch)")
            End If
        End If

        ' Validate stiffener parameters if stiffeners are enabled
        If input.HasFrontStiffener OrElse input.HasBackStiffener Then
            If input.StiffenerHeight <= 0 Then
                Return (False, "Stiffener height must be greater than 0")
            End If

            If input.StiffenerHeight > 6 Then
                Return (False, "Stiffener height exceeds reasonable limits (max 6 inches)")
            End If

            If input.StiffenerThickness <= 0 Then
                Return (False, "Stiffener thickness must be greater than 0")
            End If

            If input.StiffenerThickness > 3 Then
                Return (False, "Stiffener thickness exceeds reasonable limits (max 3 inches)")
            End If
        End If

        Return (True, "")
    End Function

#End Region

#Region "Helper Methods"

    ''' <summary>
    ''' Converts decimal inches to nearest fraction string
    ''' </summary>
    Private Shared Function ConvertDecimalToFraction(decimalValue As Double) As String
        If decimalValue < 0.001 Then
            Return "0"
        End If

        ' Get whole number part
        Dim wholePart = Math.Floor(decimalValue)
        Dim fractionalPart = decimalValue - wholePart

        ' Find nearest 64th
        Dim numerator As Integer = CInt(Math.Round(fractionalPart * 64))
        Dim denominator As Integer = 64

        ' Simplify fraction
        Dim divisor = GCD(numerator, denominator)
        numerator \= divisor
        denominator \= divisor

        ' Build result string
        If wholePart = 0 And numerator = 0 Then
            Return "0"
        ElseIf wholePart > 0 And numerator = 0 Then
            Return wholePart.ToString()
        ElseIf wholePart = 0 Then
            Return $"{numerator}/{denominator}"
        Else
            Return $"{wholePart} {numerator}/{denominator}"
        End If
    End Function

    ''' <summary>
    ''' Greatest Common Divisor for fraction simplification
    ''' </summary>
    Private Shared Function GCD(a As Integer, b As Integer) As Integer
        While b <> 0
            Dim temp As Integer = b
            b = a Mod b
            a = temp
        End While
        Return a
    End Function

#End Region

End Class
