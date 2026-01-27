' ============================================================================
' Last Updated: {Current Date}
' Changes: Initial creation - Utility for formatting labels using Tag-based
'          format strings with automatic error handling and fallback support
' ============================================================================

''' <summary>
''' Provides utility methods for formatting label text using Tag-based format strings
''' </summary>
Public Class LabelFormatter

    ''' <summary>
    ''' Updates a label's text using its Tag property as a format string
    ''' </summary>
    ''' <param name="label">The label to update</param>
    ''' <param name="values">The values to format</param>
    Public Shared Sub UpdateLabel(label As Label, ParamArray values() As Object)
        If label Is Nothing Then Return

        If label.Tag IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(CStr(label.Tag)) Then
            Try
                label.Text = String.Format(CStr(label.Tag), values)
            Catch ex As FormatException
                ' If format fails, show error message
                label.Text = "Format Error"
                ErrorHandler.LogError(ex, $"LabelFormatter.UpdateLabel for {label.Name}")
            End Try
        End If
    End Sub

    ''' <summary>
    ''' Updates a label's text using its Tag property as a format string, or uses fallback text
    ''' </summary>
    ''' <param name="label">The label to update</param>
    ''' <param name="fallbackText">Text to use if Tag is not set</param>
    ''' <param name="values">The values to format</param>
    Public Shared Sub UpdateLabelWithFallback(label As Label, fallbackText As String, ParamArray values() As Object)
        If label Is Nothing Then Return

        If label.Tag IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(CStr(label.Tag)) Then
            Try
                label.Text = String.Format(CStr(label.Tag), values)
            Catch ex As FormatException
                label.Text = fallbackText
                ErrorHandler.LogError(ex, $"LabelFormatter.UpdateLabelWithFallback for {label.Name}")
            End Try
        Else
            label.Text = fallbackText
        End If
    End Sub

    ''' <summary>
    ''' Safely gets the format string from a label's Tag property
    ''' </summary>
    Public Shared Function GetFormatString(label As Label) As String
        If label?.Tag IsNot Nothing Then
            Return CStr(label.Tag)
        End If
        Return String.Empty
    End Function

End Class
