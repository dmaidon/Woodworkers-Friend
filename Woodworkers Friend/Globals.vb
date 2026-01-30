Imports System.IO

Public Module Globals

    ' Global variables and constants can be defined here
    Public Const AppName As String = "Woodworker's Friend"

    Public Const Version As String = "1.0.0"

    ' Example of a global function
    Public Function GetAppInfo() As String
        Return $"{AppName} - Version {Version}"
    End Function

    ' You can add more global variables, functions, or constants as needed

    Public Function GetCopyrightNotice() As String
        Return $"© {DateTime.Now.Year} Woodworker's Friend. All rights reserved."
    End Function

    Public ReadOnly LogDir As String = Path.Combine(Application.StartupPath, "Logs")
    Public ReadOnly SetDir As String = Path.Combine(Application.StartupPath, "Settings")
    Public ReadOnly ProjectDir As String = Path.Combine(Application.StartupPath, "Projects")
    Public ReadOnly DataDir As String = Path.Combine(Application.StartupPath, "Data")
    Public ReadOnly TempDir As String = Path.Combine(Application.StartupPath, "$tmp")

    ' AppTheme enum is defined in ThemeManager.vb (Light, Dark, System)
    ' Removed duplicate from here in Phase 5

End Module