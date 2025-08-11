Public Class ControlUtility

    ''' <summary>
    ''' Clears the text of multiple TextBox controls
    ''' </summary>
    Public Shared Sub ClearTextControls(ParamArray controls() As Control)
        For Each control In controls
            If control IsNot Nothing Then
                If TypeOf control Is TextBox Then
                    DirectCast(control, TextBox).Clear()
                ElseIf TypeOf control Is RichTextBox Then
                    DirectCast(control, RichTextBox).Clear()
                ElseIf TypeOf control Is Label Then
                    DirectCast(control, Label).Text = ""
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' Sets the enabled state of multiple controls
    ''' </summary>
    Public Shared Sub SetControlsEnabled(enabled As Boolean, ParamArray controls() As Control)
        For Each control In controls
            If control IsNot Nothing Then
                control.Enabled = enabled
            End If
        Next
    End Sub

    ''' <summary>
    ''' Sets the visible state of multiple controls
    ''' </summary>
    Public Shared Sub SetControlsVisible(visible As Boolean, ParamArray controls() As Control)
        For Each control In controls
            If control IsNot Nothing Then
                control.Visible = visible
            End If
        Next
    End Sub

    ''' <summary>
    ''' Gets the text value from a control safely
    ''' </summary>
    Public Shared Function GetControlText(control As Control) As String
        If control Is Nothing Then Return ""

        If TypeOf control Is TextBox Then
            Return DirectCast(control, TextBox).Text
        ElseIf TypeOf control Is RichTextBox Then
            Return DirectCast(control, RichTextBox).Text
        ElseIf TypeOf control Is Label Then
            Return DirectCast(control, Label).Text
        Else
            Return ""
        End If
    End Function

    ''' <summary>
    ''' Sets the text value of a control safely
    ''' </summary>
    Public Shared Sub SetControlText(control As Control, text As String)
        If control Is Nothing Then Return

        If TypeOf control Is TextBox Then
            DirectCast(control, TextBox).Text = text
        ElseIf TypeOf control Is RichTextBox Then
            DirectCast(control, RichTextBox).Text = text
        ElseIf TypeOf control Is Label Then
            DirectCast(control, Label).Text = text
        End If
    End Sub

    ''' <summary>
    ''' Validates that all required controls are not empty
    ''' </summary>
    Public Shared Function ValidateRequiredControls(ParamArray controls() As Control) As String
        For Each control In controls
            If control IsNot Nothing Then
                Dim text As String = GetControlText(control)
                If String.IsNullOrWhiteSpace(text) Then
                    Return $"Please fill in the {control.Name} field"
                End If
            End If
        Next
        Return ""
    End Function

End Class