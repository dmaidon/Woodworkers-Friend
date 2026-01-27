' ============================================================================
' Last Updated: {Current Date}
' Changes: Initial creation - Defined validation ranges for all calculators
'          (epoxy, polygon, doors, drawers) with descriptive error messages
' ============================================================================

''' <summary>
''' Defines validation range for numeric inputs
''' </summary>
Public Structure ValidationRange
    Public Property Min As Double
    Public Property Max As Double
    Public Property Name As String

    Public Sub New(min As Double, max As Double, name As String)
        Me.Min = min
        Me.Max = max
        Me.Name = name
    End Sub

    ''' <summary>
    ''' Checks if a value is within the valid range
    ''' </summary>
    Public Function IsValid(value As Double) As Boolean
        Return value >= Min AndAlso value <= Max
    End Function

    ''' <summary>
    ''' Gets a user-friendly error message for out-of-range values
    ''' </summary>
    Public Function GetErrorMessage(value As Double) As String
        If value < Min Then
            Return $"{Name} must be at least {Min}"
        ElseIf value > Max Then
            Return $"{Name} must be no more than {Max}"
        Else
            Return String.Empty
        End If
    End Function

End Structure

''' <summary>
''' Contains validation rules and ranges for all application inputs
''' </summary>
Public Module ValidationRules

    ' Epoxy Pour Validation Ranges
    Public ReadOnly EpoxyDepthRange As New ValidationRange(0.0625, 6.0, "Epoxy depth") ' 1/16" to 6"

    Public ReadOnly EpoxyLengthRange As New ValidationRange(0.1, 1000.0, "Length") ' 0.1" to 1000"
    Public ReadOnly EpoxyWidthRange As New ValidationRange(0.1, 1000.0, "Width") ' 0.1" to 1000"
    Public ReadOnly EpoxyDiameterRange As New ValidationRange(0.1, 1000.0, "Diameter") ' 0.1" to 1000"
    Public ReadOnly EpoxyAreaRange As New ValidationRange(0.01, 10000.0, "Area") ' 0.01 to 10000 sq ft

    ' Polygon Validation Ranges
    Public ReadOnly PolygonSidesRange As New ValidationRange(3, 25, "Number of sides")

    ' Door Calculation Ranges
    Public ReadOnly DoorHeightRange As New ValidationRange(6.0, 120.0, "Door height") ' 6" to 120"

    Public ReadOnly DoorWidthRange As New ValidationRange(6.0, 60.0, "Door width") ' 6" to 60"
    Public ReadOnly StileWidthRange As New ValidationRange(0.5, 6.0, "Stile width") ' 0.5" to 6"
    Public ReadOnly RailWidthRange As New ValidationRange(0.5, 6.0, "Rail width") ' 0.5" to 6"

    ' Drawer Calculation Ranges
    Public ReadOnly DrawerCountRange As New ValidationRange(1, 20, "Number of drawers")

    Public ReadOnly DrawerHeightRange As New ValidationRange(2.0, 36.0, "Drawer height") ' 2" to 36"
    Public ReadOnly DrawerWidthRange As New ValidationRange(6.0, 48.0, "Drawer width") ' 6" to 48"
    Public ReadOnly DrawerSpacingRange As New ValidationRange(0.0, 2.0, "Drawer spacing") ' 0" to 2"

    ' General Ranges
    Public ReadOnly PositiveNumberRange As New ValidationRange(0.0001, Double.MaxValue, "Value")

    Public ReadOnly PercentageRange As New ValidationRange(0.0, 1.0, "Percentage")
    Public ReadOnly AngleRange As New ValidationRange(0.0, 360.0, "Angle")

End Module
