Public Enum AppTheme
    Light
    Dark
    System
End Enum

Public Class ThemeManager
    Implements IDisposable

    Private _disposed As Boolean = False
    Private Shared _currentTheme As AppTheme = AppTheme.Light

    ''' <summary>
    ''' Gets the current application theme
    ''' </summary>
    Public Shared ReadOnly Property CurrentTheme As AppTheme
        Get
            Return _currentTheme
        End Get
    End Property

    ''' <summary>
    ''' Applies a theme to the main form
    ''' </summary>
    Public Shared Sub ApplyTheme(form As Form, theme As AppTheme)
        _currentTheme = theme

        Select Case theme
            Case AppTheme.Light
                ApplyLightTheme(form)
            Case AppTheme.Dark
                ApplyDarkTheme(form)
            Case AppTheme.System
                ApplySystemTheme(form)
        End Select
    End Sub

    ''' <summary>
    ''' Applies light theme colors
    ''' </summary>
    Private Shared Sub ApplyLightTheme(form As Form)
        form.BackColor = Color.White
        form.ForeColor = Color.Black

        ApplyThemeToControls(form.Controls, Color.White, Color.Black, Color.LightGray)
    End Sub

    ''' <summary>
    ''' Applies dark theme colors
    ''' </summary>
    Private Shared Sub ApplyDarkTheme(form As Form)
        form.BackColor = Color.FromArgb(45, 45, 48)
        form.ForeColor = Color.White

        ApplyThemeToControls(form.Controls, Color.FromArgb(45, 45, 48), Color.White, Color.FromArgb(60, 60, 60))
    End Sub

    ''' <summary>
    ''' Applies system theme (follows Windows setting)
    ''' </summary>
    Private Shared Sub ApplySystemTheme(form As Form)
        form.BackColor = SystemColors.Control
        form.ForeColor = SystemColors.ControlText

        ApplyThemeToControls(form.Controls, SystemColors.Control, SystemColors.ControlText, SystemColors.ControlDark)
    End Sub

    ''' <summary>
    ''' Recursively applies theme to controls
    ''' </summary>
    Private Shared Sub ApplyThemeToControls(controls As Control.ControlCollection, backColor As Color, foreColor As Color, borderColor As Color)
        For Each control As Control In controls
            ' Apply colors based on control type
            Select Case True
                Case TypeOf control Is Button, TypeOf control Is TextBox, TypeOf control Is RichTextBox, TypeOf control Is GroupBox, TypeOf control Is Panel
                    control.BackColor = backColor
                    control.ForeColor = foreColor
                Case TypeOf control Is Label
                    control.ForeColor = foreColor
                    If control.BackColor <> Color.Transparent Then
                        control.BackColor = backColor
                    End If
                Case TypeOf control Is DataGridView
                    Dim dgv As DataGridView = DirectCast(control, DataGridView)
                    dgv.BackgroundColor = backColor
                    dgv.DefaultCellStyle.BackColor = backColor
                    dgv.DefaultCellStyle.ForeColor = foreColor
                    dgv.ColumnHeadersDefaultCellStyle.BackColor = borderColor
                    dgv.ColumnHeadersDefaultCellStyle.ForeColor = foreColor
            End Select

            ' Recursively apply to child controls
            If control.HasChildren Then
                ApplyThemeToControls(control.Controls, backColor, foreColor, borderColor)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Gets theme-appropriate colors for status indicators
    ''' </summary>
    Public Shared Function GetStatusColor(isSuccess As Boolean) As Color
        If isSuccess Then
            Return If(_currentTheme = AppTheme.Dark, Color.LightGreen, Color.Green)
        Else
            Return If(_currentTheme = AppTheme.Dark, Color.LightCoral, Color.Red)
        End If
    End Function

    ''' <summary>
    ''' Dispose method for IDisposable
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not _disposed Then
            If disposing Then
                ' Dispose managed resources here if needed
            End If
            _disposed = True
        End If
    End Sub

End Class