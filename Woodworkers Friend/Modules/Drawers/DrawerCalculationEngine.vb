Imports System.Text

Public Class DrawerCalculationEngine

#Region "Public Methods"

    Public Shared Function Calculate(parameters As DrawerCalculationParameters) As DrawerCalculationResult
        Try
            Dim validationResult As ValidationResult = ValidateParameters(parameters)
            If Not validationResult.IsValid Then
                Return New DrawerCalculationResult() With {
                    .IsValid = False,
                    .ErrorMessage = validationResult.ErrorMessage
                }
            End If

            ' Calculate drawer heights in native scale
            Dim nativeHeights As Double()
            Select Case parameters.CalculationMethod
                Case DrawerCalculationMethod.Hambridge
                    nativeHeights = CalculateHambridgeHeights(parameters)
                Case DrawerCalculationMethod.Geometric
                    nativeHeights = CalculateGeometricHeights(parameters)
                Case DrawerCalculationMethod.Fibonacci
                    nativeHeights = CalculateFibonacciHeights(parameters)
                Case DrawerCalculationMethod.Arithmetic
                    nativeHeights = CalculateArithmeticHeights(parameters)
                Case DrawerCalculationMethod.ReverseArithmetic
                    nativeHeights = CalculateReverseArithmeticHeights(parameters)
                Case DrawerCalculationMethod.GoldenRatio
                    nativeHeights = CalculateGoldenRatioHeights(parameters)
                Case DrawerCalculationMethod.Logarithmic
                    nativeHeights = CalculateLogarithmicHeights(parameters)
                Case DrawerCalculationMethod.Exponential
                    nativeHeights = CalculateExponentialHeights(parameters)
                Case DrawerCalculationMethod.CustomRatio
                    nativeHeights = CalculateCustomRatioHeights(parameters)
                Case DrawerCalculationMethod.Uniform
                    nativeHeights = CalculateUniformHeights(parameters)
                Case Else
                    Throw New InvalidDrawerParametersException("Unknown calculation method")
            End Select

            ' Convert heights to both units
            Dim heightsImperial As Double()
            Dim heightsMetric As Double()
            If parameters.Scale = MeasurementScale.Imperial Then
                heightsImperial = nativeHeights
                heightsMetric = nativeHeights.Select(Function(h) InchesToMillimeters(h)).ToArray()
            Else
                heightsMetric = nativeHeights
                heightsImperial = nativeHeights.Select(Function(h) MillimetersToInches(h)).ToArray()
            End If

            ' Calculate summary data for both units
            Dim result As New DrawerCalculationResult() With {
                .IsValid = True,
                .DrawerHeightsImperial = heightsImperial,
                .DrawerHeightsMetric = heightsMetric
            }

            CalculateSummaryData(result, parameters)

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

    Public Shared Function ValidateParameters(parameters As DrawerCalculationParameters) As ValidationResult
        Dim result As New ValidationResult()
        If parameters.DrawerCount <= 0 Then
            result.AddError("Number of drawers must be greater than 0")
        ElseIf parameters.DrawerCount = 1 Then
            result.AddWarning("Single drawer calculation - consider if multiple drawers would be more practical")
        ElseIf parameters.DrawerCount > 15 Then
            result.AddWarning($"Large number of drawers ({parameters.DrawerCount}) may result in very small individual drawers")
        End If

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

    Public Shared Function GenerateDetailedReport(result As DrawerCalculationResult, parameters As DrawerCalculationParameters) As String
        Dim report As New StringBuilder()
        report.AppendLine("DRAWER CALCULATION REPORT")
        report.AppendLine("=" & New String("="c, 40))
        report.AppendLine($"Method: {GetMethodDisplayName(parameters.CalculationMethod)}")
        report.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
        report.AppendLine()
        report.AppendLine("PARAMETERS:")
        report.AppendLine($"  Number of Drawers: {parameters.DrawerCount}")
        report.AppendLine($"  Drawer Spacing: {parameters.DrawerSpacing:N3} {If(parameters.Scale = MeasurementScale.Imperial, "in", "mm")} ({If(parameters.Scale = MeasurementScale.Imperial, InchesToMillimeters(parameters.DrawerSpacing).ToString("N2") & " mm", MillimetersToInches(parameters.DrawerSpacing).ToString("N2") & " in")})")
        report.AppendLine($"  Drawer Width: {parameters.DrawerWidth:N3} {If(parameters.Scale = MeasurementScale.Imperial, "in", "mm")} ({If(parameters.Scale = MeasurementScale.Imperial, InchesToMillimeters(parameters.DrawerWidth).ToString("N2") & " mm", MillimetersToInches(parameters.DrawerWidth).ToString("N2") & " in")})")
        If parameters.FirstDrawerHeight > 0 Then
            report.AppendLine($"  First Drawer Height: {parameters.FirstDrawerHeight:N3} {If(parameters.Scale = MeasurementScale.Imperial, "in", "mm")} ({If(parameters.Scale = MeasurementScale.Imperial, InchesToMillimeters(parameters.FirstDrawerHeight).ToString("N2") & " mm", MillimetersToInches(parameters.FirstDrawerHeight).ToString("N2") & " in")})")
        End If
        If parameters.Multiplier > 0 Then
            report.AppendLine($"  Multiplier: {parameters.Multiplier:N3}")
        End If
        If parameters.ArithmeticIncrement > 0 Then
            report.AppendLine($"  Arithmetic Increment: {parameters.ArithmeticIncrement:N3} {If(parameters.Scale = MeasurementScale.Imperial, "in", "mm")}")
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
        report.AppendLine("INDIVIDUAL DRAWER HEIGHTS:")
        For i As Integer = 0 To result.DrawerHeightsImperial.Length - 1
            Dim percentImp As Double = result.DrawerHeightsImperial(i) / result.TotalDrawerHeightImperial * 100
            Dim percentMet As Double = result.DrawerHeightsMetric(i) / result.TotalDrawerHeightMetric * 100
            report.AppendLine($"  Drawer {i + 1}: {result.DrawerHeightsImperial(i):N3} in ({result.DrawerHeightsMetric(i):N3} mm) ({percentImp:N1}% / {percentMet:N1}%)")
        Next
        report.AppendLine()
        report.AppendLine("SUMMARY:")
        report.AppendLine($"  Total Height: {result.TotalHeightImperial:N3} in ({result.TotalHeightMetric:N3} mm)")
        report.AppendLine($"  Total Drawer Height: {result.TotalDrawerHeightImperial:N3} in ({result.TotalDrawerHeightMetric:N3} mm)")
        report.AppendLine($"  Average Drawer Height: {result.AverageDrawerHeightImperial:N3} in ({result.AverageDrawerHeightMetric:N3} mm)")
        report.AppendLine($"  Height Ratio: {result.HeightRatio:N2}:1")
        report.AppendLine($"  Total Material Area: {result.TotalMaterialAreaImperial:N3} sq ft ({result.TotalMaterialAreaMetric:N3} m²)")
        Return report.ToString()
    End Function

#End Region

#Region "Calculation Methods (Return Native Heights)"

    ' All these methods now return Double() in the native scale
    Private Shared Function CalculateHambridgeHeights(parameters As DrawerCalculationParameters) As Double()
        Dim totalAvailableHeight As Double = CalculateAvailableHeight(parameters)
        Dim selectedRatios As Double() = GetHambridgeRatios(parameters.DrawerCount)
        Dim ratioSum As Double = selectedRatios.Sum()
        Return selectedRatios.Select(Function(ratio) ratio / ratioSum * totalAvailableHeight).ToArray()
    End Function

    Private Shared Function CalculateGeometricHeights(parameters As DrawerCalculationParameters) As Double()
        Dim heights As New List(Of Double)()
        Dim currentHeight As Double = parameters.FirstDrawerHeight
        For i As Integer = 0 To parameters.DrawerCount - 1
            heights.Add(currentHeight)
            currentHeight *= parameters.Multiplier
        Next
        Return heights.ToArray()
    End Function

    Private Shared Function CalculateFibonacciHeights(parameters As DrawerCalculationParameters) As Double()
        Dim fibSequence As Integer() = GenerateFibonacciSequence(parameters.DrawerCount)
        Dim totalFib As Integer = fibSequence.Sum()
        Dim totalAvailableHeight As Double = CalculateAvailableHeight(parameters)
        Return fibSequence.Select(Function(fibNumber) CDbl(fibNumber) / totalFib * totalAvailableHeight).ToArray()
    End Function

    Private Shared Function CalculateArithmeticHeights(parameters As DrawerCalculationParameters) As Double()
        Dim heights As New List(Of Double)()
        Dim currentHeight As Double = parameters.FirstDrawerHeight
        For i As Integer = 0 To parameters.DrawerCount - 1
            heights.Add(currentHeight)
            currentHeight += parameters.ArithmeticIncrement
        Next
        Return heights.ToArray()
    End Function

    Private Shared Function CalculateReverseArithmeticHeights(parameters As DrawerCalculationParameters) As Double()
        Dim heights As New List(Of Double)()
        Dim currentHeight As Double = parameters.FirstDrawerHeight
        For i As Integer = 0 To parameters.DrawerCount - 1
            heights.Add(currentHeight)
            currentHeight -= parameters.ArithmeticIncrement
        Next
        Return heights.ToArray()
    End Function

    Private Shared Function CalculateLogarithmicHeights(parameters As DrawerCalculationParameters) As Double()
        Dim totalAvailableHeight As Double = CalculateAvailableHeight(parameters)
        Dim logValues As New List(Of Double)()
        For i As Integer = 1 To parameters.DrawerCount
            logValues.Add(Math.Log(i + 1))
        Next
        Dim totalLogSum As Double = logValues.Sum()
        Return logValues.Select(Function(logValue) (logValue / totalLogSum) * totalAvailableHeight).ToArray()
    End Function

    Private Shared Function CalculateExponentialHeights(parameters As DrawerCalculationParameters) As Double()
        Dim heights As New List(Of Double)()
        Dim exponentialBase As Double = If(parameters.Multiplier > 0, parameters.Multiplier, 1.5)
        Dim baseHeight As Double = parameters.FirstDrawerHeight
        For i As Integer = 0 To parameters.DrawerCount - 1
            heights.Add(baseHeight * Math.Pow(exponentialBase, i))
        Next
        Return heights.ToArray()
    End Function

    Private Shared Function CalculateCustomRatioHeights(parameters As DrawerCalculationParameters) As Double()
        Dim totalAvailableHeight As Double = CalculateAvailableHeight(parameters)
        Dim ratios As Double()
        If parameters.CustomRatios IsNot Nothing AndAlso parameters.CustomRatios.Length = parameters.DrawerCount Then
            ratios = parameters.CustomRatios
        Else
            ratios = New Double(parameters.DrawerCount - 1) {}
            For i As Integer = 0 To parameters.DrawerCount - 1
                ratios(i) = 1.0 + (i * 0.2)
            Next
        End If
        Dim totalRatio As Double = ratios.Sum()
        Return ratios.Select(Function(ratio) (ratio / totalRatio) * totalAvailableHeight).ToArray()
    End Function

    Private Shared Function CalculateUniformHeights(parameters As DrawerCalculationParameters) As Double()
        Dim totalAvailableHeight As Double = CalculateAvailableHeight(parameters)
        Dim uniformHeight As Double = totalAvailableHeight / parameters.DrawerCount
        Return Enumerable.Repeat(uniformHeight, parameters.DrawerCount).ToArray()
    End Function

    Private Shared Function CalculateGoldenRatioHeights(parameters As DrawerCalculationParameters) As Double()
        Dim phi As Double = 1.6180339887
        Dim totalAvailableHeight As Double = CalculateAvailableHeight(parameters)
        Dim seriesSum As Double = 0
        For i As Integer = 0 To parameters.DrawerCount - 1
            seriesSum += Math.Pow(phi, i)
        Next
        Dim heights As New List(Of Double)()
        For i As Integer = 0 To parameters.DrawerCount - 1
            heights.Add(Math.Pow(phi, i) / seriesSum * totalAvailableHeight)
        Next
        Return heights.ToArray()
    End Function

#End Region

#Region "Helper Methods"

    Private Shared Function CalculateAvailableHeight(parameters As DrawerCalculationParameters) As Double
        Dim baseHeight As Double = Math.Max(parameters.DrawerCount * 6.0, 18.0)
        Dim totalSpacing As Double = parameters.DrawerSpacing * (parameters.DrawerCount - 1)
        Return Math.Max(baseHeight - totalSpacing, parameters.DrawerCount * 3.0)
    End Function

    Private Shared Function GetHambridgeRatios(drawerCount As Integer) As Double()
        Select Case drawerCount
            Case 2
                Return {1.0, 1.618}
            Case 3
                Return {1.0, 1.414, 1.732}
            Case 4
                Return {1.0, 1.414, 1.732, 2.236}
            Case Else
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

        ' Imperial
        result.TotalDrawerHeightImperial = result.DrawerHeightsImperial.Sum()
        result.TotalHeightImperial = result.TotalDrawerHeightImperial + (If(parameters.Scale = MeasurementScale.Imperial, parameters.DrawerSpacing, MillimetersToInches(parameters.DrawerSpacing)) * (parameters.DrawerCount - 1))
        result.AverageDrawerHeightImperial = result.DrawerHeightsImperial.Average()
        result.TotalMaterialAreaImperial = CalculateMaterialArea(result.DrawerHeightsImperial, If(parameters.Scale = MeasurementScale.Imperial, parameters.DrawerWidth, MillimetersToInches(parameters.DrawerWidth)), MeasurementScale.Imperial)
        ' Metric
        result.TotalDrawerHeightMetric = result.DrawerHeightsMetric.Sum()
        result.TotalHeightMetric = result.TotalDrawerHeightMetric + (If(parameters.Scale = MeasurementScale.Metric, parameters.DrawerSpacing, InchesToMillimeters(parameters.DrawerSpacing)) * (parameters.DrawerCount - 1))
        result.AverageDrawerHeightMetric = result.DrawerHeightsMetric.Average()
        result.TotalMaterialAreaMetric = CalculateMaterialArea(result.DrawerHeightsMetric, If(parameters.Scale = MeasurementScale.Metric, parameters.DrawerWidth, InchesToMillimeters(parameters.DrawerWidth)), MeasurementScale.Metric)

        ' Height ratio (unitless)
        result.HeightRatio = If(result.DrawerHeightsImperial.Length > 1,
                               result.DrawerHeightsImperial.Max() / Math.Max(result.DrawerHeightsImperial.Min(), 0.0001),
                               1.0)
    End Sub

    Private Shared Function CalculateMaterialArea(heights() As Double, drawerWidth As Double, scale As MeasurementScale) As Double
        Dim totalArea As Double = 0
        Dim drawerDepth As Double = If(scale = MeasurementScale.Imperial, 18.0, 457.2)
        For Each height In heights
            totalArea += 2 * (drawerWidth * height)
            totalArea += 2 * (drawerDepth * height)
            totalArea += drawerWidth * drawerDepth
        Next
        If scale = MeasurementScale.Imperial Then
            totalArea /= 144
        Else
            totalArea /= 1000000
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

    Friend Shared Function InchesToMillimeters(inches As Double) As Double
        Return inches * 25.4
    End Function

    Friend Shared Function MillimetersToInches(mm As Double) As Double
        Return mm / 25.4
    End Function

#End Region

End Class