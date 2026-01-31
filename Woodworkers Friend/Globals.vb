Imports System.IO

Friend Module Globals

    ' Global variables and constants
    Friend Const AppName As String = "Woodworker's Friend"

    Friend Const Version As String = "1.0.0"

    ' TimesRun tracks the number of application starts (stored in database)
    Friend TimesRun As Integer

    ' MaxLogAgeInDays controls how long to keep log files before cleanup
    ' Default is 5 days (minimum), loaded from UserPreferences at startup
    Friend MaxLogAgeInDays As Integer = 5

    ' Directory paths
    Friend ReadOnly LogDir As String = Path.Combine(Application.StartupPath, "Logs")

    Friend ReadOnly SetDir As String = Path.Combine(Application.StartupPath, "Settings")
    Friend ReadOnly ProjectDir As String = Path.Combine(Application.StartupPath, "Projects")
    Friend ReadOnly DataDir As String = Path.Combine(Application.StartupPath, "Data")
    Friend ReadOnly TempDir As String = Path.Combine(Application.StartupPath, "$tmp")

    Friend LogFile As String

    ' Global functions
    Friend Function GetAppInfo() As String
        Return $"{AppName} - Version {Version}"
    End Function

    Friend Function GetCopyrightNotice() As String
        Return $"© {DateTime.Now.Year} Woodworker's Friend. All rights reserved."
    End Function

    ' AppTheme enum is defined in ThemeManager.vb (Light, Dark, System)
    ' Removed duplicate from here in Phase 5

End Module
