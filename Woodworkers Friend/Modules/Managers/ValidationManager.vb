Imports System.Globalization

Public Class ValidationManager

    ''' <summary>
    ''' Gets an integer value from a TextBox control with validation
    ''' </summary>
    Public Shared Function GetIntegerFromControl(control As TextBox, fieldName As String) As Integer
        If control Is Nothing Then
            Throw New InvalidDrawerParametersException($"{fieldName} control is not available")
        End If

        If String.IsNullOrWhiteSpace(control.Text) Then
            Throw New InvalidDrawerParametersException($"{fieldName} cannot be empty")
        End If

        Dim result As Integer
        If Not Integer.TryParse(control.Text.Trim(), result) Then
            Throw New InvalidDrawerParametersException($"{fieldName} must be a valid whole number")
        End If

        Return result
    End Function

    ''' <summary>
    ''' Gets a double value from a TextBox control with validation
    ''' </summary>
    Public Shared Function GetDoubleFromControl(control As TextBox, fieldName As String) As Double
        If control Is Nothing Then
            Throw New InvalidDrawerParametersException($"{fieldName} control is not available")
        End If

        If String.IsNullOrWhiteSpace(control.Text) Then
            Throw New InvalidDrawerParametersException($"{fieldName} cannot be empty")
        End If

        Dim result As Double
        If Not Double.TryParse(control.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, result) Then
            Throw New InvalidDrawerParametersException($"{fieldName} must be a valid decimal number")
        End If

        Return result
    End Function

    ''' <summary>
    ''' Validates that a TextBox contains a positive number
    ''' </summary>
    Public Shared Function ValidatePositiveNumber(control As TextBox, fieldName As String) As Boolean
        Try
            Dim value As Double = GetDoubleFromControl(control, fieldName)
            Return value > 0
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Validates that a TextBox contains a non-negative number
    ''' </summary>
    Public Shared Function ValidateNonNegativeNumber(control As TextBox, fieldName As String) As Boolean
        Try
            Dim value As Double = GetDoubleFromControl(control, fieldName)
            Return value >= 0
        Catch
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Gets a safe double value with a default fallback
    ''' </summary>
    Public Shared Function GetSafeDoubleFromControl(control As TextBox, defaultValue As Double) As Double
        If control Is Nothing OrElse String.IsNullOrWhiteSpace(control.Text) Then
            Return defaultValue
        End If

        Dim result As Double
        If Double.TryParse(control.Text.Trim(), NumberStyles.Float, CultureInfo.InvariantCulture, result) Then
            Return result
        Else
            Return defaultValue
        End If
    End Function

    ''' <summary>
    ''' Enhanced validation with detailed error reporting
    ''' </summary>
    Public Shared Function ValidateDrawerCalculationInput(parameters As DrawerCalculationParameters) As ValidationResult
        Dim result As New ValidationResult()

        ' Range validation with context-specific messages
        If parameters.DrawerCount < 1 Then
            result.AddError("At least one drawer is required")
        ElseIf parameters.DrawerCount > 20 Then
            result.AddError("Maximum 20 drawers supported for optimal calculations")
        End If

        ' Precision validation
        If parameters.DrawerSpacing < 0 Then
            result.AddError("Drawer spacing cannot be negative")
        ElseIf parameters.DrawerSpacing > 10 Then ' Reasonable upper limit
            result.AddWarning("Drawer spacing seems unusually large (>10 units)")
        End If

        ' Method-specific validation
        Select Case parameters.CalculationMethod
            Case DrawerCalculationMethod.ReverseArithmetic
                ValidateReverseArithmetic(parameters, result)
            Case DrawerCalculationMethod.CustomRatio
                ValidateCustomRatios(parameters, result)
            Case DrawerCalculationMethod.Exponential
                ValidateExponential(parameters, result)
        End Select

        Return result
    End Function

    Private Shared Sub ValidateCustomRatios(parameters As DrawerCalculationParameters, result As ValidationResult)
        If parameters.CustomRatios IsNot Nothing AndAlso parameters.CustomRatios.Length <> parameters.DrawerCount Then
            result.AddError($"Custom ratios must have exactly {parameters.DrawerCount} values")
        End If

        If parameters.CustomRatios IsNot Nothing Then
            For Each ratio In parameters.CustomRatios
                If ratio <= 0 Then
                    result.AddError("All custom ratios must be greater than 0")
                    Exit For
                End If
            Next
        End If
    End Sub

    Private Shared Sub ValidateExponential(parameters As DrawerCalculationParameters, result As ValidationResult)
        If parameters.FirstDrawerHeight <= 0 Then
            result.AddError("First drawer height must be greater than 0 for exponential calculation")
        End If

        If parameters.Multiplier <= 0 OrElse parameters.Multiplier > 5 Then
            result.AddError("Exponential base (multiplier) must be between 0 and 5")
        End If

        ' Check if exponential growth will create unreasonably large drawers
        If parameters.Multiplier > 2 AndAlso parameters.DrawerCount > 4 Then
            Dim largestDrawer As Double = parameters.FirstDrawerHeight * Math.Pow(parameters.Multiplier, parameters.DrawerCount - 1)
            If largestDrawer > 100 Then ' Assuming inches/cm
                result.AddWarning($"Largest drawer would be {largestDrawer:N1} units tall. Consider reducing the multiplier.")
            End If
        End If
    End Sub

    Private Shared Sub ValidateReverseArithmetic(parameters As DrawerCalculationParameters, result As ValidationResult)
        ' Calculate if smallest drawer will be positive
        Dim smallestHeight As Double = parameters.FirstDrawerHeight - ((parameters.DrawerCount - 1) * parameters.ArithmeticIncrement)

        If smallestHeight <= 0 Then
            Dim suggestedIncrement As Double = parameters.FirstDrawerHeight / parameters.DrawerCount * 0.8
            result.AddError($"Smallest drawer would be {smallestHeight:N2} units (negative/zero). " &
                           $"Try reducing increment to {suggestedIncrement:N2} or less.")
        ElseIf smallestHeight < 1 Then ' Warn about very small drawers
            result.AddWarning($"Smallest drawer will be only {smallestHeight:N2} units tall. Consider adjusting parameters.")
        End If
    End Sub

    ''' <summary>
    ''' Sanitizes and validates numeric input with culture awareness
    ''' </summary>
    Public Shared Function GetSanitizedDoubleFromControl(control As TextBox, fieldName As String,
                                                    Optional minValue As Double = Double.MinValue,
                                                    Optional maxValue As Double = Double.MaxValue) As Double
        If control Is Nothing Then
            Throw New InvalidDrawerParametersException($"{fieldName} control is not available")
        End If

        ' Remove extra whitespace and normalize input
        Dim cleanInput As String = control.Text.Trim()

        If String.IsNullOrWhiteSpace(cleanInput) Then
            Throw New InvalidDrawerParametersException($"{fieldName} is required")
        End If

        ' Handle common input variations
        cleanInput = cleanInput.Replace(",", ".") ' Handle European decimal separator
        cleanInput = System.Text.RegularExpressions.Regex.Replace(cleanInput, "[^\d\.\-]", "") ' Remove non-numeric chars except decimal and minus

        Dim result As Double
        If Not Double.TryParse(cleanInput, NumberStyles.Float, CultureInfo.InvariantCulture, result) Then
            Throw New InvalidDrawerParametersException($"{fieldName} must be a valid decimal number")
        End If

        ' Range validation
        If result < minValue Then
            Throw New InvalidDrawerParametersException($"{fieldName} must be at least {minValue}")
        End If

        If result > maxValue Then
            Throw New InvalidDrawerParametersException($"{fieldName} cannot exceed {maxValue}")
        End If

        Return result
    End Function

End Class