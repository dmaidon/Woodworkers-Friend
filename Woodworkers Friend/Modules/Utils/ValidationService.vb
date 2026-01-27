' ============================================================================
' Last Updated: January 27, 2026
' Changes: Initial creation - Centralized validation service with methods for
'          validating door, epoxy, polygon, and drawer parameters
' ============================================================================

''' <summary>
''' Centralized validation service for all application inputs
''' Provides consistent validation logic and error messages
''' </summary>
Public Class ValidationService

    ''' <summary>
    ''' Validates a numeric input against a validation range
    ''' </summary>
    ''' <param name="input">The input text to validate</param>
    ''' <param name="range">The validation range to check against</param>
    ''' <param name="value">The parsed value if valid</param>
    ''' <param name="errorMessage">The error message if invalid</param>
    ''' <returns>True if validation passed, False otherwise</returns>
    Public Shared Function ValidateNumericInput(input As String,
                                                range As ValidationRange,
                                                ByRef value As Double,
                                                ByRef errorMessage As String) As Boolean
        ' Check if empty
        If String.IsNullOrWhiteSpace(input) Then
            errorMessage = $"{range.Name} is required"
            value = 0
            Return False
        End If

        ' Try to parse
        If Not Double.TryParse(input, value) Then
            errorMessage = $"{range.Name} must be a valid number"
            Return False
        End If

        ' Check range
        If Not range.IsValid(value) Then
            errorMessage = range.GetErrorMessage(value)
            Return False
        End If

        errorMessage = String.Empty
        Return True
    End Function

    ''' <summary>
    ''' Validates door calculation parameters
    ''' </summary>
    Public Shared Function ValidateDoorParameters(height As Double,
                                                  width As Double,
                                                  stileWidth As Double,
                                                  railWidth As Double) As (IsValid As Boolean, ErrorMessage As String)

        If Not ValidationRules.DoorHeightRange.IsValid(height) Then
            Return (False, ValidationRules.DoorHeightRange.GetErrorMessage(height))
        End If

        If Not ValidationRules.DoorWidthRange.IsValid(width) Then
            Return (False, ValidationRules.DoorWidthRange.GetErrorMessage(width))
        End If

        If Not ValidationRules.StileWidthRange.IsValid(stileWidth) Then
            Return (False, ValidationRules.StileWidthRange.GetErrorMessage(stileWidth))
        End If

        If Not ValidationRules.RailWidthRange.IsValid(railWidth) Then
            Return (False, ValidationRules.RailWidthRange.GetErrorMessage(railWidth))
        End If

        ' Check if stiles/rails are too large for door
        If stileWidth * 2 >= width Then
            Return (False, "Stile width is too large for the door width")
        End If

        If railWidth * 2 >= height Then
            Return (False, "Rail width is too large for the door height")
        End If

        Return (True, String.Empty)
    End Function

    ''' <summary>
    ''' Validates epoxy pour parameters
    ''' </summary>
    Public Shared Function ValidateEpoxyParameters(length As Double,
                                                   width As Double,
                                                   depth As Double) As (IsValid As Boolean, ErrorMessage As String)

        If depth > 0 AndAlso Not ValidationRules.EpoxyDepthRange.IsValid(depth) Then
            Return (False, ValidationRules.EpoxyDepthRange.GetErrorMessage(depth))
        End If

        If length > 0 AndAlso Not ValidationRules.EpoxyLengthRange.IsValid(length) Then
            Return (False, ValidationRules.EpoxyLengthRange.GetErrorMessage(length))
        End If

        If width > 0 AndAlso Not ValidationRules.EpoxyWidthRange.IsValid(width) Then
            Return (False, ValidationRules.EpoxyWidthRange.GetErrorMessage(width))
        End If

        Return (True, String.Empty)
    End Function

    ''' <summary>
    ''' Validates polygon sides parameter
    ''' </summary>
    Public Shared Function ValidatePolygonSides(sides As Integer) As (IsValid As Boolean, ErrorMessage As String)
        If sides < ValidationRules.PolygonSidesRange.Min Then
            Return (False, $"Polygon must have at least {ValidationRules.PolygonSidesRange.Min} sides")
        End If

        If sides > ValidationRules.PolygonSidesRange.Max Then
            Return (False, $"Polygon cannot have more than {ValidationRules.PolygonSidesRange.Max} sides")
        End If

        Return (True, String.Empty)
    End Function

    ''' <summary>
    ''' Validates drawer calculation parameters
    ''' </summary>
    Public Shared Function ValidateDrawerParameters(count As Integer,
                                                    height As Double,
                                                    width As Double) As (IsValid As Boolean, ErrorMessage As String)

        If count < ValidationRules.DrawerCountRange.Min OrElse count > ValidationRules.DrawerCountRange.Max Then
            Return (False, $"Number of drawers must be between {ValidationRules.DrawerCountRange.Min} and {ValidationRules.DrawerCountRange.Max}")
        End If

        If height > 0 AndAlso Not ValidationRules.DrawerHeightRange.IsValid(height) Then
            Return (False, ValidationRules.DrawerHeightRange.GetErrorMessage(height))
        End If

        If width > 0 AndAlso Not ValidationRules.DrawerWidthRange.IsValid(width) Then
            Return (False, ValidationRules.DrawerWidthRange.GetErrorMessage(width))
        End If

        Return (True, String.Empty)
    End Function

End Class
