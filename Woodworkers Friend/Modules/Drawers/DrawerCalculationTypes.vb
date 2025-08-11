' Enum for calculation methods
Imports System.Text

Public Enum DrawerCalculationMethod
    Hambridge
    Geometric
    Fibonacci
    Arithmetic
    Logarithmic
    Exponential
    CustomRatio
    Uniform
    ReverseArithmetic
    GoldenRatio
End Enum

' Enum for measurement scale
Public Enum MeasurementScale
    Imperial
    Metric
End Enum

' Parameters for calculation
Public Class DrawerCalculationParameters
    Public Property DrawerCount As Integer
    Public Property DrawerSpacing As Double
    Public Property DrawerWidth As Double
    Public Property FirstDrawerHeight As Double
    Public Property Multiplier As Double
    Public Property ArithmeticIncrement As Double
    Public Property LogarithmicBase As Double = Math.E
    Public Property ExponentialBase As Double = 2.0
    Public Property CustomRatios As Double() ' <-- Fixed property name (was CustomRatio)
    Public Property CalculationMethod As DrawerCalculationMethod
    Public Property Scale As MeasurementScale
End Class

' Result of calculation
Public Class DrawerCalculationResult
    Public Property IsValid As Boolean = True
    Public Property ErrorMessage As String = ""
    Public Property DrawerHeights As Double()
    Public Property TotalDrawerHeight As Double
    Public Property TotalHeight As Double
    Public Property AverageDrawerHeight As Double
    Public Property HeightRatio As Double
    Public Property TotalMaterialArea As Double
    Public Property Unit As String
    Public Property AreaUnit As String
    Public Property Details As String
End Class

' Enhanced validation result with warnings and suggestions
Public Class ValidationResult
    Public Property IsValid As Boolean = True
    Public Property HasWarnings As Boolean = False
    Public Property ErrorMessage As String = ""
    Public Property Warnings As New List(Of String)()
    Public Property Suggestions As New List(Of String)()

    Public Sub AddError(msg As String)
        IsValid = False
        If ErrorMessage <> "" Then
            ErrorMessage &= vbCrLf
        End If
        ErrorMessage &= msg
    End Sub

    Public Sub AddWarning(msg As String)
        HasWarnings = True
        Warnings.Add(msg)
    End Sub

    Public Sub AddSuggestion(msg As String)
        Suggestions.Add(msg)
    End Sub

    Public Function GetFullReport() As String
        Dim report As New StringBuilder()

        If Not IsValid Then
            report.AppendLine("Errors:")
            report.AppendLine(ErrorMessage)
        End If

        If HasWarnings Then
            report.AppendLine("Warnings:")
            For Each warning In Warnings
                report.AppendLine($"• {warning}")
            Next
        End If

        If Suggestions.Count > 0 Then
            report.AppendLine("Suggestions:")
            For Each suggestion In Suggestions
                report.AppendLine($"• {suggestion}")
            Next
        End If

        Return report.ToString()
    End Function

End Class

' Custom exception for invalid parameters
Public Class InvalidDrawerParametersException
    Inherits Exception

    Public Sub New(message As String)
        MyBase.New(message)
    End Sub

End Class