Imports System.IO

Friend Module Globals

    ' Global variables and constants
    Friend Const AppName As String = "Woodworker's Friend"

    Friend Const Version As String = "1.0.0"

    Friend Const SupportEmail As String = "support@maidonww.com"

    ' TimesRun tracks the number of application starts (stored in database)
    Friend TimesRun As Integer

    ' MaxLogAgeInDays controls how long to keep log files before cleanup
    ' Default is 3 days (minimum), loaded from UserPreferences at startup
    Friend MaxLogAgeInDays As Integer = 3

    ' Directory paths
    ' Application data (Help.db, Reference.db) - Read-only, in installation folder
    Friend ReadOnly InstallDir As String = Application.StartupPath

    Friend ReadOnly AppDataDir As String = Path.Combine(InstallDir, "Data")
    Friend ReadOnly AppResourcesDir As String = Path.Combine(AppDataDir, "Resources")

    ' User-specific data - Read-write, in AppData
    Private ReadOnly UserAppDataRoot As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WoodworkersFriend")

    Friend ReadOnly LogDir As String = Path.Combine(UserAppDataRoot, "Logs")
    Friend ReadOnly SetDir As String = Path.Combine(UserAppDataRoot, "Settings")
    Friend ReadOnly ProjectDir As String = Path.Combine(UserAppDataRoot, "Projects")
    Friend ReadOnly DataDir As String = Path.Combine(UserAppDataRoot, "Data")
    Friend ReadOnly TempDir As String = Path.Combine(UserAppDataRoot, "$tmp")

    Friend LogFile As String

    ' Global functions
    Friend Function GetAppInfo() As String
        Return $"{AppName} - Version {Version}"
    End Function

    Friend Function GetCopyrightNotice() As String
        Return $"© {DateTime.Now.Year} Dennis N. Maidon. All rights reserved."
    End Function

    ' AppTheme enum is defined in ThemeManager.vb (Light, Dark, System)
    ' Removed duplicate from here in Phase 5

End Module
