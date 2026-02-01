# 🪚 Woodworker's Friend - Installation Guide

## System Requirements

### Minimum Requirements
- **Operating System:** Windows 10 (version 1809 or later) or Windows 11
- **.NET Runtime:** .NET 10.0 Desktop Runtime
- **RAM:** 512 MB minimum, 1 GB recommended
- **Disk Space:** 100 MB for application and databases
- **Display:** 1024 x 768 minimum resolution, 1920 x 1080 recommended

### Recommended Requirements
- **Operating System:** Windows 11
- **RAM:** 2 GB or more
- **Display:** 1920 x 1080 or higher
- **Mouse:** For interactive diagrams and 3D visualizations

---

## Installation Methods

### Method 1: Installer (Recommended)

1. **Download the Installer**
   - Download `WoodworkersFriend-Setup-v1.0.0.exe` from the [Releases page](https://github.com/dmaidon/Woodworkers-Friend/releases)
   - File size: ~15 MB

2. **Run the Installer**
   - Double-click the downloaded `.exe` file
   - If Windows SmartScreen appears, click "More info" → "Run anyway"
   - Follow the installation wizard:
     - Accept the license agreement
     - Choose installation location (default: `C:\Program Files\WoodworkersFriend`)
     - Select start menu folder
     - Choose desktop shortcut option

3. **Launch the Application**
   - Start from Desktop shortcut or Start Menu
   - First launch will create user database at `%APPDATA%\WoodworkersFriend`

### Method 2: Portable Version

1. **Download the Portable ZIP**
   - Download `WoodworkersFriend-Portable-v1.0.0.zip`
   - File size: ~12 MB

2. **Extract Files**
   - Extract ZIP to your preferred location
   - No installation required
   - Run `Woodworkers Friend.exe` directly

3. **Portable Mode**
   - All settings and databases stored in application folder
   - Can run from USB drive
   - No registry entries created

### Method 3: Build from Source

1. **Prerequisites**
   - Visual Studio 2022 or later
   - .NET 10.0 SDK
   - Git

2. **Clone Repository**
   ```bash
   git clone https://github.com/dmaidon/Woodworkers-Friend.git
   cd Woodworkers-Friend
   ```

3. **Build Project**
   ```bash
   dotnet restore
   dotnet build --configuration Release
   ```

4. **Run Application**
   ```bash
   cd "Woodworkers Friend\bin\Release\net10.0-windows"
   "Woodworkers Friend.exe"
   ```

---

## .NET Runtime Installation

If you don't have .NET 10.0 Desktop Runtime installed:

### Automatic Installation
- The installer will prompt you to download .NET 10.0 if missing
- Click "Download .NET" when prompted
- Installer will redirect to Microsoft's official download page

### Manual Installation
1. Visit [.NET Download Page](https://dotnet.microsoft.com/download/dotnet/10.0)
2. Download **.NET Desktop Runtime 10.0.x** (x86 or x64 based on your system)
3. Run the installer
4. Restart Woodworker's Friend

**Check if .NET is installed:**
```powershell
dotnet --list-runtimes
```
Look for `Microsoft.WindowsDesktop.App 10.0.x`

---

## First Launch Setup

### Initial Configuration

1. **Welcome Screen**
   - Choose theme (Light/Dark)
   - Select unit system (Imperial/Metric)
   - Set default wood species

2. **Database Initialization**
   - Application creates SQLite databases automatically
   - Location: `%APPDATA%\WoodworkersFriend\`
   - Includes:
     - `WoodworkersMain.db` - User data and projects
     - `Help.db` - Help content and documentation
     - `Resources.db` - Wood species, joinery, hardware data

3. **Cost Database (Optional)**
   - Navigate to **Settings** → **Cost Management**
   - Import wood costs CSV or enter manually
   - Import epoxy costs or use defaults

---

## Troubleshooting

### Issue: Application won't start

**Solution 1:** Install/Repair .NET Runtime
```powershell
# Check .NET version
dotnet --version

# Reinstall .NET 10.0 Desktop Runtime from Microsoft
```

**Solution 2:** Check Windows version
- Open `Settings` → `System` → `About`
- Ensure Windows 10 version 1809+ or Windows 11

**Solution 3:** Run as Administrator
- Right-click `Woodworkers Friend.exe`
- Select "Run as administrator"

### Issue: Database errors on launch

**Solution:** Reset user database
```powershell
# Close application first
# Delete user database (will be recreated on next launch)
Remove-Item "$env:APPDATA\WoodworkersFriend\WoodworkersMain.db"
```

### Issue: Missing calculations or features

**Solution:** Force database migration
1. Close application
2. Delete `ErrorLog.txt` from `%APPDATA%\WoodworkersFriend\`
3. Restart application (forces clean migration)

### Issue: Display issues or scaling problems

**Solution:** Adjust DPI settings
1. Right-click `Woodworkers Friend.exe` → **Properties**
2. Go to **Compatibility** tab
3. Click "Change high DPI settings"
4. Check "Override high DPI scaling behavior"
5. Select "System (Enhanced)"

---

## Uninstallation

### From Installer
1. **Windows 10:** `Settings` → `Apps` → `Woodworker's Friend` → `Uninstall`
2. **Windows 11:** `Settings` → `Apps` → `Installed apps` → `Woodworker's Friend` → `⋮` → `Uninstall`

### Portable Version
- Simply delete the application folder

### Clean Removal (All Data)
To remove all user data and settings:
```powershell
# Remove application data
Remove-Item -Recurse "$env:APPDATA\WoodworkersFriend"

# Remove local data (if exists)
Remove-Item -Recurse "$env:LOCALAPPDATA\WoodworkersFriend"
```

---

## File Locations

### Application Files
- **Installed:** `C:\Program Files\WoodworkersFriend\`
- **Portable:** User-selected folder

### User Data
- **Databases:** `%APPDATA%\WoodworkersFriend\`
  - `WoodworkersMain.db` - Projects, preferences, cost data
  - `Help.db` - Help content
  - `Resources.db` - Wood species, joinery, hardware
- **Logs:** `%APPDATA%\WoodworkersFriend\ErrorLog.txt`
- **Exports:** `%USERPROFILE%\Documents\WoodworkersFriend\`

---

## Upgrading

### From Older Version
1. **Backup your data** (recommended):
   ```powershell
   Copy-Item -Recurse "$env:APPDATA\WoodworkersFriend" "$env:USERPROFILE\Desktop\WwF_Backup"
   ```

2. **Install new version:**
   - Installer will detect and preserve user data
   - Database auto-migration runs on first launch

3. **Verify upgrade:**
   - Check `Help` → `About` for new version number
   - Review CHANGELOG.md for new features

### Rollback
If upgrade causes issues:
1. Uninstall current version
2. Install previous version
3. Restore backed-up database files

---

## Getting Help

- **In-App Help:** Press `F1` or navigate to **Help** tab
- **Documentation:** [GitHub Wiki](https://github.com/dmaidon/Woodworkers-Friend/wiki)
- **Report Issues:** [GitHub Issues](https://github.com/dmaidon/Woodworkers-Friend/issues)
- **Discussions:** [GitHub Discussions](https://github.com/dmaidon/Woodworkers-Friend/discussions)

---

## License

This software is licensed under the MIT License. See [LICENSE](LICENSE) file for details.

**Disclaimer:** This software provides calculations based on industry-standard formulas. Always verify critical measurements and follow proper safety procedures when working with woodworking tools and machinery.
