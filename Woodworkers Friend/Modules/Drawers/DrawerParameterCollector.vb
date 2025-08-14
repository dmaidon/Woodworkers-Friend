Imports System
Imports System.Linq

Namespace WwFriend.Modules.Drawers
    Public Module DrawerParameterCollector

        Public Function Collect(form As FrmMain) As DrawerCalculationParameters
            ArgumentNullException.ThrowIfNull(form)

            Dim method As DrawerCalculationMethod = GetSelectedCalculationMethod(form)

            Dim multiplier As Double = 0.0
            Dim arithmeticIncrement As Double = 0.0
            Dim customRatios As Double() = Nothing

            Select Case method
                Case DrawerCalculationMethod.Geometric
                    multiplier = ValidationManager.GetDoubleFromControl(form.TxtMultiplier, "Multiplier")

                Case DrawerCalculationMethod.Arithmetic, DrawerCalculationMethod.ReverseArithmetic
                    arithmeticIncrement = ValidationManager.GetDoubleFromControl(form.TxtArithmeticIncrement, "Arithmetic Increment")

                Case DrawerCalculationMethod.Exponential
                    multiplier = ValidationManager.GetDoubleFromControl(form.TxtMultiplier, "Exponential Base")

                Case DrawerCalculationMethod.CustomRatio
                    customRatios = ParseCustomRatios(form)
                Case DrawerCalculationMethod.Logarithmic, DrawerCalculationMethod.Uniform,
                     DrawerCalculationMethod.Fibonacci, DrawerCalculationMethod.Hambridge,
                     DrawerCalculationMethod.GoldenRatio
                    ' No extra params
            End Select

            Return New DrawerCalculationParameters With {
                .CalculationMethod = method,
                .DrawerCount = ValidationManager.GetIntegerFromControl(form.TxtDrawerCount, "Number of Drawers"),
                .DrawerSpacing = ValidationManager.GetDoubleFromControl(form.TxtDrawerSpacing, "Drawer Spacing"),
                .DrawerWidth = ValidationManager.GetDoubleFromControl(form.TxtDrawerWidth, "Drawer Width"),
                .FirstDrawerHeight = GetFirstDrawerHeightSafely(form, method),
                .Multiplier = multiplier,
                .ArithmeticIncrement = arithmeticIncrement,
                .CustomRatios = customRatios,
                .Scale = If(form.RbImperial IsNot Nothing AndAlso form.RbImperial.Checked, MeasurementScale.Imperial, MeasurementScale.Metric)
            }
        End Function

        Private Function GetSelectedCalculationMethod(form As FrmMain) As DrawerCalculationMethod
            Dim candidates As RadioButton() = {
                form.RbHambridge, form.RbGeometric, form.RbFibonacci, form.RbArithmetic,
                form.RbLogarithmic, form.RbExponential, form.RbCustomRatio, form.RbUniform,
                form.RbReverseArithmetic, form.RbGoldenRatio
            }

            Dim selectedRb As RadioButton = candidates.FirstOrDefault(Function(rb) rb IsNot Nothing AndAlso rb.Checked)
            If selectedRb Is Nothing OrElse selectedRb.Tag Is Nothing Then
                Throw New InvalidDrawerParametersException("Please select a calculation method")
            End If

            Dim tagValue As Integer = CInt(selectedRb.Tag)
            Select Case tagValue
                Case 0 : Return DrawerCalculationMethod.Hambridge
                Case 1 : Return DrawerCalculationMethod.Geometric
                Case 2 : Return DrawerCalculationMethod.Fibonacci
                Case 3 : Return DrawerCalculationMethod.Arithmetic
                Case 4 : Return DrawerCalculationMethod.Logarithmic
                Case 5 : Return DrawerCalculationMethod.Exponential
                Case 6 : Return DrawerCalculationMethod.CustomRatio
                Case 7 : Return DrawerCalculationMethod.Uniform
                Case 8 : Return DrawerCalculationMethod.ReverseArithmetic
                Case 9 : Return DrawerCalculationMethod.GoldenRatio
                Case Else
                    Throw New InvalidDrawerParametersException("Unknown calculation method")
            End Select
        End Function

        Private Function ParseCustomRatios(form As FrmMain) As Double()
            If form.TxtCustomRatioInput Is Nothing OrElse String.IsNullOrWhiteSpace(form.TxtCustomRatioInput.Text) Then
                Return Nothing
            End If

            Dim ratioList As New List(Of Double)
            Dim invalidValues As New List(Of String)
            Dim lines As String() = form.TxtCustomRatioInput.Lines

            For Each line As String In lines
                If String.IsNullOrWhiteSpace(line) Then Continue For
                Dim values As String() = line.Split({","c, " "c, ControlChars.Tab}, StringSplitOptions.RemoveEmptyEntries)

                For Each value As String In values
                    Dim cleanValue As String = value.Trim()
                    Dim ratio As Double
                    If Double.TryParse(cleanValue, ratio) AndAlso ratio > 0 Then
                        ratioList.Add(ratio)
                    Else
                        invalidValues.Add($"'{cleanValue}'")
                    End If
                Next
            Next

            If invalidValues.Count > 0 Then
                Throw New InvalidDrawerParametersException($"Invalid custom ratio values: {String.Join(", ", invalidValues)}")
            End If

            If ratioList.Count = 0 Then
                Throw New InvalidDrawerParametersException("Please enter valid custom ratios (positive numbers only)")
            End If

            Return ratioList.ToArray()
        End Function

        Private Function GetFirstDrawerHeightSafely(form As FrmMain, method As DrawerCalculationMethod) As Double
            Dim requiresHeight As Boolean =
                method = DrawerCalculationMethod.Geometric OrElse
                method = DrawerCalculationMethod.Arithmetic OrElse
                method = DrawerCalculationMethod.ReverseArithmetic OrElse
                method = DrawerCalculationMethod.Exponential

            Try
                Dim height As Double = ValidationManager.GetDoubleFromControl(form.TxtFirstDrawerHeight, "First Drawer Height")
                If requiresHeight AndAlso height <= 0 Then
                    Throw New InvalidDrawerParametersException($"{method} method requires a positive first drawer height")
                End If
                Return height
            Catch ex As InvalidDrawerParametersException
                Throw
            Catch
                If requiresHeight Then
                    Throw New InvalidDrawerParametersException("First drawer height is required for this calculation method")
                End If
                Return If(form.RbImperial IsNot Nothing AndAlso form.RbImperial.Checked, 3.0, 76.2) ' 3 in or 76.2 mm
            End Try
        End Function

    End Module
End Namespace