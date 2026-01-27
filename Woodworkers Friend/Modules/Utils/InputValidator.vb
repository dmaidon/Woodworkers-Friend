' ============================================================================
' Last Updated: {Current Date}
' Changes: Initial creation and enhancements - Input validation, sanitization,
'          clamping, fraction parsing (e.g., "1 1/2"), and numeric checks
' ============================================================================

''' <summary>
''' Provides validation methods for user input
''' </summary>
Public Class InputValidator

    ''' <summary>
    ''' Attempts to parse a double value with a default fallback
    ''' </summary>
    Public Shared Function TryParseDoubleWithDefault(input As String, defaultValue As Double) As Double
        If String.IsNullOrWhiteSpace(input) Then Return defaultValue

        Dim result As Double
        Return If(Double.TryParse(input, result), result, defaultValue)
    End Function

    ''' <summary>
    ''' Attempts to parse an integer value with range validation
    ''' </summary>
    Public Shared Function TryParseIntegerInRange(input As String, min As Integer, max As Integer, defaultValue As Integer) As Integer
        If String.IsNullOrWhiteSpace(input) Then Return defaultValue

        Dim result As Integer
        If Integer.TryParse(input, result) Then
            Return Math.Max(min, Math.Min(max, result))
        End If
        Return defaultValue
    End Function

    ''' <summary>
    ''' Validates if a double value is within a specified range
    ''' </summary>
    Public Shared Function IsInRange(value As Double, min As Double, max As Double) As Boolean
        Return value >= min AndAlso value <= max
    End Function

    ''' <summary>
    ''' Attempts to parse multiple text inputs as doubles
    ''' </summary>
    Public Shared Function TryParseMultipleDoubles(values() As (Input As String, Output As Double), defaultValue As Double) As Boolean
        Dim allValid As Boolean = True

        For i As Integer = 0 To values.Length - 1
            Dim result As Double
            If Double.TryParse(values(i).Input, result) Then
                values(i).Output = result
            Else
                values(i).Output = defaultValue
                allValid = False
            End If
        Next

        Return allValid
    End Function

    ''' <summary>
    ''' Sanitizes numeric input by removing invalid characters
    ''' </summary>
    Public Shared Function SanitizeNumericInput(input As String, allowNegative As Boolean) As String
        If String.IsNullOrEmpty(input) Then Return String.Empty

        Dim validChars As New List(Of Char)()
        Dim decimalPointSeen As Boolean = False

        For Each c As Char In input
            If Char.IsDigit(c) Then
                validChars.Add(c)
            ElseIf c = "."c AndAlso Not decimalPointSeen Then
                validChars.Add(c)
                decimalPointSeen = True
            ElseIf c = "-"c AndAlso allowNegative AndAlso validChars.Count = 0 Then
                validChars.Add(c)
            End If
        Next

        Return New String(validChars.ToArray())
    End Function

    ''' <summary>
    ''' Sanitizes numeric input and returns parsed value with default
    ''' </summary>
    Public Shared Function SanitizeAndParse(input As String,
                                           allowNegative As Boolean,
                                           defaultValue As Double) As Double
        Dim sanitized = SanitizeNumericInput(input, allowNegative)
        Return TryParseDoubleWithDefault(sanitized, defaultValue)
    End Function

    ''' <summary>
    ''' Validates that input contains only numeric characters
    ''' </summary>
    Public Shared Function IsNumericInput(input As String, allowNegative As Boolean, allowDecimal As Boolean) As Boolean
        If String.IsNullOrEmpty(input) Then Return False

        Dim decimalPointSeen As Boolean = False

        For i As Integer = 0 To input.Length - 1
            Dim c As Char = input(i)

            If Char.IsDigit(c) Then
                Continue For
            ElseIf c = "."c AndAlso allowDecimal AndAlso Not decimalPointSeen Then
                decimalPointSeen = True
                Continue For
            ElseIf c = "-"c AndAlso allowNegative AndAlso i = 0 Then
                Continue For
            End If

            Return False
        Next

        Return True
    End Function

    ''' <summary>
    ''' Clamps a value to a specified range
    ''' </summary>
    Public Shared Function Clamp(value As Double, min As Double, max As Double) As Double
        Return Math.Max(min, Math.Min(max, value))
    End Function

    ''' <summary>
    ''' Attempts to parse a fraction string (e.g., "3/4" or "1 1/2")
    ''' </summary>
    Public Shared Function TryParseFraction(input As String, ByRef value As Double) As Boolean
        Try
            If String.IsNullOrWhiteSpace(input) Then
                value = 0
                Return False
            End If

            input = input.Trim()

            ' Check for mixed fraction (e.g., "1 1/2")
            Dim spaceParts = input.Split(" "c)
            If spaceParts.Length = 2 Then
                Dim wholePart As Double
                Dim fractionPart As Double

                If Double.TryParse(spaceParts(0), wholePart) AndAlso
                   TryParseFraction(spaceParts(1), fractionPart) Then
                    value = wholePart + fractionPart
                    Return True
                End If
            End If

            ' Check for simple fraction (e.g., "3/4")
            Dim slashParts = input.Split("/"c)
            If slashParts.Length = 2 Then
                Dim numerator, denominator As Double
                If Double.TryParse(slashParts(0), numerator) AndAlso
                   Double.TryParse(slashParts(1), denominator) AndAlso
                   denominator <> 0 Then
                    value = numerator / denominator
                    Return True
                End If
            End If

            ' Try parsing as decimal
            Return Double.TryParse(input, value)
        Catch ex As Exception
            value = 0
            Return False
        End Try
    End Function

End Class
