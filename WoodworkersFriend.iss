; Woodworker's Friend Installer Script
; Requires Inno Setup 6.0 or later (https://jrsoftware.org/isinfo.php)

#define MyAppName "Woodworker's Friend"
#define MyAppVersion "1.0.0"
#define MyAppPublisher "Dennis N. Maidon"
#define MyAppURL "https://www.maidonww.com"
#define MyAppSupportEmail "support@maidonww.com"
#define MyAppExeName "Woodworkers Friend.exe"

[Setup]
; NOTE: The value of AppId uniquely identifies this application.
; Do not use the same AppId value in installers for other applications.
AppId={{8F3D4A2B-1C5E-4F9A-B2D1-3E7C8A9F4B1D}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}/issues
AppUpdatesURL={#MyAppURL}/releases
AppContact={#MyAppSupportEmail}
DefaultDirName={pf}\{#MyAppName}
DefaultGroupName={#MyAppName}
LicenseFile=LICENSE
InfoBeforeFile=README.md
OutputDir=Installer
OutputBaseFilename=WoodworkersFriend-Setup-v{#MyAppVersion}
SetupIconFile="Woodworkers Friend\Resources\wood.ico"
Compression=lzma2/max
SolidCompression=yes
WizardStyle=modern
PrivilegesRequired=lowest
ArchitecturesInstallIn64BitMode=x64

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
; Application files
Source: "C:\VB18\Release\WwFriend\net10.0-windows10.0.26100.0"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs
; Documentation
Source: "README.md"; DestDir: "{app}"; Flags: ignoreversion
Source: "LICENSE"; DestDir: "{app}"; Flags: ignoreversion
Source: "CHANGELOG.md"; DestDir: "{app}"; Flags: ignoreversion
Source: "INSTALL.md"; DestDir: "{app}"; Flags: ignoreversion
Source: "RELEASE_NOTES.md"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"
Name: "{group}\User Guide"; Filename: "{app}\README.md"
Name: "{group}\Release Notes"; Filename: "{app}\RELEASE_NOTES.md"
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{autodesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[Run]
Filename: "{app}\{#MyAppExeName}"; Description: "{cm:LaunchProgram,{#StringChange(MyAppName, '&', '&&')}}"; Flags: nowait postinstall skipifsilent

[Code]
function IsDotNetInstalled(): Boolean;
var
  ResultCode: Integer;
begin
  // Check if .NET 10.0 Desktop Runtime is installed
  Result := Exec('cmd.exe', '/c dotnet --list-runtimes | findstr "Microsoft.WindowsDesktop.App 10.0"', '', SW_HIDE, ewWaitUntilTerminated, ResultCode) and (ResultCode = 0);
end;

function InitializeSetup(): Boolean;
var
  ResultCode: Integer;
begin
  Result := True;
  
  if not IsDotNetInstalled() then
  begin
    if MsgBox('.NET 10.0 Desktop Runtime is required but not installed.' + #13#10 + #13#10 + 
              'Would you like to download it now?' + #13#10 + #13#10 +
              'After installing .NET, please run this installer again.', 
              mbConfirmation, MB_YESNO) = IDYES then
    begin
      ShellExec('open', 'https://dotnet.microsoft.com/download/dotnet/10.0', '', '', SW_SHOW, ewNoWait, ResultCode);
    end;
    Result := False;
  end;
end;

[UninstallDelete]
Type: filesandordirs; Name: "{userappdata}\WoodworkersFriend"
