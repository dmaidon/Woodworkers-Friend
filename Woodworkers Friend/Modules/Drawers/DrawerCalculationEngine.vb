Imports System.Text

' DrawerCalculationEngine.vb - Extracted from FrmMain.vb
Public Class DrawerCalculationEngine

#Region "Public Methods"

    ''' <summary>
    ''' Performs drawer calculation and returns structured result
    ''' </summary>
    Public Shared Function Calculate(parameters As DrawerCalculationParameters) As DrawerCalculationResult
        Try
            ' Validate parameters first
            Dim validationResult As ValidationResult = ValidateParameters(parameters)
            If Not validationResult.IsValid Then
                Return New DrawerCalculationResult() With {
                    .IsValid = False,
                    .ErrorMessage = validationResult.ErrorMessage
                }
            End If

            ' Perform calculation based on method
            Dim result As DrawerCalculationResult = Nothing
            Select Case parameters.CalculationMethod
                Case DrawerCalculationMethod.Hambridge
                    result = CalculateHambridge(parameters)
                Case DrawerCalculationMethod.Geometric
                    result = CalculateGeometric(parameters)
                Case DrawerCalculationMethod.Fibonacci
                    result = CalculateFibonacci(parameters)
                Case DrawerCalculationMethod.Arithmetic
                    result = CalculateArithmetic(parameters)
                Case DrawerCalculationMethod.ReverseArithmetic
                    result = CalculateReverseArithmetic(parameters)
                Case DrawerCalculationMethod.GoldenRatio
                    result = CalculateGoldenRatio(parameters)
                Case DrawerCalculationMethod.Logarithmic
                    result = CalculateLogarithmic(parameters)
                Case DrawerCalculationMethod.Exponential
                    result = CalculateExponential(parameters)
                Case DrawerCalculationMethod.CustomRatio
                    result = CalculateCustomRatio(parameters)
                Case DrawerCalculationMethod.Uniform
                    result = CalculateUniform(parameters)
                Case Else
                    Throw New InvalidDrawerParametersException("Unknown calculation method")
            End Select

            ' Add summary calculations
            CalculateSummaryData(result, parameters)

            ' Generate detailed report
            result.Details = GenerateDetailedReport(result, parameters)

            Return result
        Catch ex As InvalidDrawerParametersException
            Return New DrawerCalculationResult() With {
                .IsValid = False,
                .ErrorMessage = ex.Message
            }
        Catch ex As Exception
            Return New DrawerCalculationResult() With {
                .IsValid = False,
                .ErrorMessage = $"Calculation error: {ex.Message}"
            }
        End Try
    End Function

    ' Enhanced parameter validation with helpful suggestions
    Public Shared Function ValidateParameters(parameters As DrawerCalculationParameters) As ValidationResult
        Dim result As New ValidationResult()

        ' Context-aware validation with suggestions
        If parameters.DrawerCount <= 0 Then
            result.AddError("Number of drawers must be greater than 0")
        ElseIf parameters.DrawerCount = 1 Then
            result.AddWarning("Single drawer calculation - consider if multiple drawers would be more practical")
        ElseIf parameters.DrawerCount > 15 Then
            result.AddWarning($"Large number of drawers ({parameters.DrawerCount}) may result in very small individual drawers")
        End If

        ' Physical constraints validation
        If parameters.DrawerWidth > 0 Then
            Dim aspectRatio As Double = parameters.FirstDrawerHeight / parameters.DrawerWidth
            If aspectRatio > 1.5 Then
                result.AddWarning("Drawer appears very tall relative to width - this may affect functionality")
            ElseIf aspectRatio < 0.1 Then
                result.AddWarning("Drawer appears very shallow relative to width - consider increasing height")
            End If
        End If

        Return result
    End Function

    ''' <summary>
    ''' Generates a detailed calculation report
    ''' </summary>
    Public Shared Function GenerateDetailedReport(result As DrawerCalculationResult, parameters As DrawerCalculationParameters) As String
        Dim report As New StringBuilder()

        report.AppendLine("DRAWER CALCULATION REPORT")
        report.AppendLine("=" & New String("="c, 40))
        report.AppendLine($"Method: {GetMethodDisplayName(parameters.CalculationMethod)}")
        report.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
        report.AppendLine()

        ' Parameters
        report.AppendLine("PARAMETERS:")
        report.AppendLine($"  Number of Drawers: {parameters.DrawerCount}")
        report.AppendLine($"  Drawer Spacing: {parameters.DrawerSpacing:N3} {result.Unit}")
        report.AppendLine($"  Drawer Width: {parameters.DrawerWidth:N3} {result.Unit}")

        If parameters.FirstDrawerHeight > 0 Then
            report.AppendLine($"  First Drawer Height: {parameters.FirstDrawerHeight:N3} {result.Unit}")
        End If

        If parameters.Multiplier > 0 Then
            report.AppendLine($"  Multiplier: {parameters.Multiplier:N3}")
        End If

        If parameters.ArithmeticIncrement > 0 Then
            report.AppendLine($"  Arithmetic Increment: {parameters.ArithmeticIncrement:N3} {result.Unit}")
        End If

        If parameters.CustomRatios IsNot Nothing AndAlso parameters.CustomRatios.Length > 0 Then
            report.AppendLine($"  Custom Ratios: {String.Join(", ", parameters.CustomRatios.Select(Function(r) r.ToString("N2")))}")
        End If

        If parameters.CalculationMethod = DrawerCalculationMethod.Logarithmic AndAlso parameters.LogarithmicBase > 0 Then
            report.AppendLine($"  Logarithmic Base: {parameters.LogarithmicBase:N3}")
        End If

        If parameters.CalculationMethod = DrawerCalculationMethod.Exponential AndAlso parameters.ExponentialBase > 0 Then
            report.AppendLine($"  Exponential Base: {parameters.ExponentialBase:N3}")
        End If

        report.AppendLine()

        ' Individual drawer heights
        report.AppendLine("INDIVIDUAL DRAWER HEIGHTS:")
        For i As Integer = 0 To result.DrawerHeights.Length - 1
            Dim percentage As Double = result.DrawerHeights(i) / result.TotalDrawerHeight * 100
            report.AppendLine($"  Drawer {i + 1}: {result.DrawerHeights(i):N3} {result.Unit} ({percentage:N1}%)")
        Next
        report.AppendLine()

        ' Summary
        report.AppendLine("SUMMARY:")
        report.AppendLine($"  Total Height: {result.TotalHeight:N3} {result.Unit}")
        report.AppendLine($"  Total Drawer Height: {result.TotalDrawerHeight:N3} {result.Unit}")
        report.AppendLine($"  Average Drawer Height: {result.AverageDrawerHeight:N3} {result.Unit}")
        report.AppendLine($"  Height Ratio: {result.HeightRatio:N2}:1")
        report.AppendLine($"  Total Material Area: {result.TotalMaterialArea:N3} {result.AreaUnit}")

        Return report.ToString()
    End Function

#End Region

#Region "Private Calculation Methods"

    Private Shared Function CalculateHambridge(parameters As DrawerCalculationParameters) As DrawerCalculationResult
        ' Hambridge ratio calculation (√2, √3, √5, φ ratios)
        Dim result As New DrawerCalculationResult()
        Dim heights As New List(Of Double)()

        ' Use golden ratio and other proportional relationships
        Dim totalAvailableHeight As Double = CalculateAvailableHeight(parameters)

        ' Get appropriate Hambridge ratios for drawer count
        Dim selectedRatios As Double() = GetHambridgeRatios(parameters.DrawerCount)
        Dim ratioSum As Double = selectedRatios.Sum()

        For Each ratio In selectedRatios
            Dim height As Double = ratio / ratioSum * totalAvailableHeight
            heights.Add(height)
        Next

        result.DrawerHeights = heights.ToArray()
        Return result
    End Function

    Private Shared Function CalculateGeometric(parameters As DrawerCalculationParameters) As DrawerCalculationResult
        Dim result As New DrawerCalculationResult()
        Dim heights As New List(Of Double)()

        Dim currentHeight As Double = parameters.FirstDrawerHeight
        For i As Integer = 0 To parameters.DrawerCount - 1
            heights.Add(currentHeight)
            currentHeight *= parameters.Multiplier
        Next

        result.DrawerHeights = heights.ToArray()
        Return result
    End Function

    Private Shared Function CalculateFibonacci(parameters As DrawerCalculationParameters) As DrawerCalculationResult
        Dim result As New DrawerCalculationResult()
        Dim heights As New List(Of Double)()

        ' Generate Fibonacci sequence for drawer count
        Dim fibSequence As Integer() = GenerateFibonacciSequence(parameters.DrawerCount)
        Dim totalFib As Integer = fibSequence.Sum()
        Dim totalAvailableHeight As Double = CalculateAvailableHeight(parameters)

        For Each fibNumber In fibSequence
            Dim height As Double = CDbl(fibNumber) / totalFib * totalAvailableHeight
            heights.Add(height)
        Next

        result.DrawerHeights = heights.ToArray()
        Return result
    End Function

    Private Shared Function CalculateArithmetic(parameters As DrawerCalculationParameters) As DrawerCalculationResult
        Dim result As New DrawerCalculationResult()
        Dim heights As New List(Of Double)()

        Dim currentHeight As Double = parameters.FirstDrawerHeight
        For i As Integer = 0 To parameters.DrawerCount - 1
            heights.Add(currentHeight)
            currentHeight += parameters.ArithmeticIncrement
        Next

        result.DrawerHeights = heights.ToArray()
        Return result
    End Function

    Private Shared Function CalculateReverseArithmetic(parameters As DrawerCalculationParameters) As DrawerCalculationResult
        Dim result As New DrawerCalculationResult()
        Dim heights As New List(Of Double)()

        Dim currentHeight As Double = parameters.FirstDrawerHeight
        For i As Integer = 0 To parameters.DrawerCount - 1
            heights.Add(currentHeight)
            currentHeight -= parameters.ArithmeticIncrement ' Subtract instead of add
        Next

        result.DrawerHeights = heights.ToArray()
        Return result
    End Function

    Private Shared Function CalculateLogarithmic(parameters As DrawerCalculationParameters) As DrawerCalculationResult
        Dim result As New DrawerCalculationResult()
        Dim heights As New List(Of Double)()

        Dim totalAvailableHeight As Double = CalculateAvailableHeight(parameters)

        ' Generate logarithmic sequence using natural log
        Dim logValues As New List(Of Double)()
        For i As Integer = 1 To parameters.DrawerCount
            logValues.Add(Math.Log(i + 1)) ' ln(2), ln(3), ln(4), etc.
        Next

        Dim totalLogSum As Double = logValues.Sum()

        ' Distribute heights proportionally based on log values
        For Each logValue In logValues
            Dim height As Double = (logValue / totalLogSum) * totalAvailableHeight
            heights.Add(height)
        Next

        result.DrawerHeights = heights.ToArray()
        Return result
    End Function

    Private Shared Function CalculateExponential(parameters As DrawerCalculationParameters) As DrawerCalculationResult
        Dim result As New DrawerCalculationResult()
        Dim heights As New List(Of Double)()

        ' Use multiplier as exponential base (default 1.5 if not provided)
        Dim exponentialBase As Double = If(parameters.Multiplier > 0, parameters.Multiplier, 1.5)
        Dim baseHeight As Double = parameters.FirstDrawerHeight

        ' Generate exponential sequence
        For i As Integer = 0 To parameters.DrawerCount - 1
            Dim height As Double = baseHeight * Math.Pow(exponentialBase, i)
            heights.Add(height)
        Next

        result.DrawerHeights = heights.ToArray()
        Return result
    End Function

    Private Shared Function CalculateCustomRatio(parameters As DrawerCalculationParameters) As DrawerCalculationResult
        Dim result As New DrawerCalculationResult()
        Dim heights As New List(Of Double)()

        Dim totalAvailableHeight As Double = CalculateAvailableHeight(parameters)

        ' Use custom ratios if provided, otherwise use default progressive ratios
        Dim ratios As Double()
        If parameters.CustomRatios IsNot Nothing AndAlso parameters.CustomRatios.Length = parameters.DrawerCount Then
            ratios = parameters.CustomRatios
        Else
            ' Default to progressive ratios: 1, 1.2, 1.4, 1.6, etc.
            ratios = New Double(parameters.DrawerCount - 1) {}
            For i As Integer = 0 To parameters.DrawerCount - 1
                ratios(i) = 1.0 + (i * 0.2)
            Next
        End If

        Dim totalRatio As Double = ratios.Sum()

        ' Distribute heights based on ratios
        For Each ratio In ratios
            Dim height As Double = (ratio / totalRatio) * totalAvailableHeight
            heights.Add(height)
        Next

        result.DrawerHeights = heights.ToArray()
        Return result
    End Function

    Private Shared Function CalculateUniform(parameters As DrawerCalculationParameters) As DrawerCalculationResult
        Dim result As New DrawerCalculationResult()
        Dim heights As New List(Of Double)()

        Dim totalAvailableHeight As Double = CalculateAvailableHeight(parameters)

        ' All drawers get equal height
        Dim uniformHeight As Double = totalAvailableHeight / parameters.DrawerCount

        For i As Integer = 0 To parameters.DrawerCount - 1
            heights.Add(uniformHeight)
        Next

        result.DrawerHeights = heights.ToArray()
        Return result
    End Function

#End Region

#Region "Helper Methods"

    Private Shared Function CalculateAvailableHeight(parameters As DrawerCalculationParameters) As Double
        ' Use a more realistic calculation based on actual input
        ' If no specific cabinet height is provided, use a reasonable default based on drawer count
        Dim baseHeight As Double = Math.Max(parameters.DrawerCount * 6.0, 18.0) ' Minimum 6" per drawer, 18" minimum total

        ' Subtract spacing between drawers
        Dim totalSpacing As Double = parameters.DrawerSpacing * (parameters.DrawerCount - 1)

        Return Math.Max(baseHeight - totalSpacing, parameters.DrawerCount * 3.0) ' Ensure at least 3" per drawer
    End Function

    Private Shared Function GetHambridgeRatios(drawerCount As Integer) As Double()
        ' Return appropriate Hambridge ratios based on drawer count
        Select Case drawerCount
            Case 2
                Return {1.0, 1.618} ' 1:φ
            Case 3
                Return {1.0, 1.414, 1.732} ' 1:√2:√3
            Case 4
                Return {1.0, 1.414, 1.732, 2.236} ' 1:√2:√3:√5
            Case Else
                ' Generate sequence for larger counts
                Dim ratios As New List(Of Double)()
                For i As Integer = 1 To drawerCount
                    ratios.Add(Math.Sqrt(i))
                Next
                Return ratios.ToArray()
        End Select
    End Function

    Private Shared Function GenerateFibonacciSequence(count As Integer) As Integer()
        If count <= 0 Then Return Array.Empty(Of Integer)()
        If count = 1 Then Return {1}

        Dim sequence As New List(Of Integer) From {1, 1}

        For i As Integer = 2 To count - 1
            sequence.Add(sequence(i - 1) + sequence(i - 2))
        Next

        Return sequence.ToArray()
    End Function

    Private Shared Sub CalculateSummaryData(result As DrawerCalculationResult, parameters As DrawerCalculationParameters)
        ArgumentNullException.ThrowIfNull(result)
        ArgumentNullException.ThrowIfNull(parameters)
        result.TotalDrawerHeight = result.DrawerHeights.Sum()
        result.TotalHeight = result.TotalDrawerHeight + (parameters.DrawerSpacing * (parameters.DrawerCount - 1))
        result.AverageDrawerHeight = result.DrawerHeights.Average()
        result.HeightRatio = If(result.DrawerHeights.Length > 1,
                               result.DrawerHeights.Max() / result.DrawerHeights.Min(),
                               1.0)

        ' Material calculation
        result.TotalMaterialArea = CalculateMaterialArea(result.DrawerHeights, parameters)

        ' Set units
        result.Unit = If(parameters.Scale = MeasurementScale.Imperial, "in", "mm")
        result.AreaUnit = If(parameters.Scale = MeasurementScale.Imperial, "sq ft", "sq m")
    End Sub

    Private Shared Function CalculateMaterialArea(heights() As Double, parameters As DrawerCalculationParameters) As Double
        ArgumentNullException.ThrowIfNull(heights)
        ArgumentNullException.ThrowIfNull(parameters)
        ' Calculate total material needed for drawer boxes (more realistic calculation)
        Dim totalArea As Double = 0

        ' Set appropriate depth based on scale (use more reasonable depths)
        Dim drawerDepth As Double = If(parameters.Scale = MeasurementScale.Imperial, 18.0, 457.2) ' 18" = 457.2mm

        ' Material thickness for calculations
        ''  Dim materialThickness As Double = If(parameters.Scale = MeasurementScale.Imperial, 0.75, 19.05) ' 3/4" = 19.05mm

        For Each height In heights
            ' Only calculate the actual panel areas (not including thickness)

            ' Front and Back pieces (2 pieces per drawer)
            totalArea += 2 * (parameters.DrawerWidth * height)

            ' Left and Right side pieces (2 pieces per drawer)
            totalArea += 2 * (drawerDepth * height)

            ' Bottom piece (1 piece per drawer)
            totalArea += parameters.DrawerWidth * drawerDepth
        Next

        ' Convert to appropriate area units
        If parameters.Scale = MeasurementScale.Imperial Then
            totalArea /= 144 ' Convert square inches to square feet
        Else
            totalArea /= 1000000 ' Convert square mm to square meters
        End If

        Return totalArea
    End Function

    Private Shared Function GetMethodDisplayName(method As DrawerCalculationMethod) As String
        Select Case method
            Case DrawerCalculationMethod.Hambridge
                Return "Hambridge Ratios (Proportional)"
            Case DrawerCalculationMethod.Geometric
                Return "Geometric Progression"
            Case DrawerCalculationMethod.Fibonacci
                Return "Fibonacci Sequence"
            Case DrawerCalculationMethod.Arithmetic
                Return "Arithmetic Progression"
            Case DrawerCalculationMethod.Logarithmic
                Return "Logarithmic Progression"
            Case DrawerCalculationMethod.Exponential
                Return "Exponential Progression"
            Case DrawerCalculationMethod.CustomRatio
                Return "Custom Ratio Distribution"
            Case DrawerCalculationMethod.Uniform
                Return "Uniform (Equal Heights)"
            Case DrawerCalculationMethod.ReverseArithmetic
                Return "Reverse Arithmetic Progression"
            Case DrawerCalculationMethod.GoldenRatio
                Return "Golden Ratio Progression"
            Case Else
                Return "Unknown Method"
        End Select
    End Function

#End Region

#Region "Golden Ratio"

    Private Shared Function CalculateGoldenRatio(parameters As DrawerCalculationParameters) As DrawerCalculationResult
        Dim result As New DrawerCalculationResult()
        Dim heights As New List(Of Double)()
        Dim phi As Double = 1.6180339887 ' Golden ratio

        Dim totalAvailableHeight As Double = CalculateAvailableHeight(parameters)

        ' Calculate the sum of the series: base * phi^0 + base * phi^1 + ... for n drawers
        ' We'll use a geometric series formula to distribute the heights proportionally
        Dim seriesSum As Double = 0
        For i As Integer = 0 To parameters.DrawerCount - 1
            seriesSum += Math.Pow(phi, i)
        Next

        For i As Integer = 0 To parameters.DrawerCount - 1
            Dim height As Double = Math.Pow(phi, i) / seriesSum * totalAvailableHeight
            heights.Add(height)
        Next

        result.DrawerHeights = heights.ToArray()
        Return result
    End Function

#End Region

End Class