# 📦 Packaging Guide for Woodworker's Friend

This guide explains how to create distributable packages for Woodworker's Friend.

---

## Prerequisites

### Required Software
1. **Visual Studio 2022** (Community Edition or better)
2. **.NET 10.0 SDK** ([Download](https://dotnet.microsoft.com/download/dotnet/10.0))
3. **Git** (for version management)

### Optional Tools
- **Inno Setup 6.0+** ([Download](https://jrsoftware.org/isinfo.php)) - For Windows installer
- **7-Zip** - For creating portable ZIP archives

---

## 📋 Pre-Release Checklist

Before creating release packages:

- [ ] All tests pass
- [ ] No build warnings
- [ ] CHANGELOG.md updated with release notes
- [ ] Version number updated in `AssemblyInfo.vb`
- [ ] README.md reflects current features
- [ ] Help content is up-to-date
- [ ] Database migrations tested on clean install
- [ ] Tested on Windows 10 and Windows 11

---

## 🔨 Building the Application

### 1. Clean Build

```powershell
# Navigate to solution directory
cd "C:\VB18\WwFriend"

# Clean previous builds
dotnet clean --configuration Release

# Restore NuGet packages
dotnet restore

# Build release version
dotnet build --configuration Release
```

### 2. Verify Build Output

Check that build succeeded:
```powershell
cd "Woodworkers Friend\bin\Release\net10.0-windows"
dir
```

You should see:
- `Woodworkers Friend.exe`
- `*.dll` files (dependencies)
- `Resources\` folder
- `Help.db`, `Resources.db` (if embedded)

---

## 📦 Creating Distribution Packages

### Option 1: Windows Installer (Inno Setup)

#### Step 1: Install Inno Setup
- Download from [https://jrsoftware.org/isinfo.php](https://jrsoftware.org/isinfo.php)
- Run installer, accept defaults

#### Step 2: Compile Installer Script
```powershell
# From project root directory
& "C:\Program Files (x86)\Inno Setup 6\ISCC.exe" WoodworkersFriend.iss
```

Or use Inno Setup GUI:
1. Open `WoodworkersFriend.iss` in Inno Setup
2. Click **Build** → **Compile**
3. Wait for completion

#### Step 3: Find Installer
Output location: `Installer\WoodworkersFriend-Setup-v1.0.0.exe` (~15 MB)

**Test the installer:**
1. Run on clean Windows VM
2. Verify .NET check works
3. Install to custom location
4. Test application launch
5. Uninstall cleanly

---

### Option 2: Portable ZIP Archive

#### Step 1: Build Release
```powershell
dotnet build --configuration Release
```

#### Step 2: Copy Files to Staging
```powershell
# Create staging directory
New-Item -ItemType Directory -Force -Path "Package\Portable"

# Copy binaries
Copy-Item -Recurse "Woodworkers Friend\bin\Release\net10.0-windows\*" "Package\Portable\"

# Copy documentation
Copy-Item "README.md" "Package\Portable\"
Copy-Item "LICENSE" "Package\Portable\"
Copy-Item "CHANGELOG.md" "Package\Portable\"
Copy-Item "INSTALL.md" "Package\Portable\"
Copy-Item "RELEASE_NOTES.md" "Package\Portable\"
```

#### Step 3: Create ZIP Archive
```powershell
# Using PowerShell
Compress-Archive -Path "Package\Portable\*" -DestinationPath "Package\WoodworkersFriend-Portable-v1.0.0.zip"

# Or using 7-Zip
& "C:\Program Files\7-Zip\7z.exe" a -tzip "Package\WoodworkersFriend-Portable-v1.0.0.zip" "Package\Portable\*"
```

**Test portable version:**
1. Extract to clean folder
2. Run without installing
3. Verify all features work
4. Check databases created in app folder (portable mode)

---

### Option 3: Source Code Package

#### Step 1: Create Git Archive
```powershell
# Create tagged release
git tag -a v1.0.0 -m "Version 1.0.0 - Initial Release"
git push origin v1.0.0

# Create source archive
git archive --format=zip --output=WoodworkersFriend-Source-v1.0.0.zip v1.0.0
```

Or manually:
1. Go to GitHub Releases page
2. Click "Draft a new release"
3. Choose tag `v1.0.0`
4. GitHub auto-generates source archives

---

## 🚀 Publishing Release on GitHub

### Step 1: Create Release on GitHub

1. Navigate to [https://github.com/dmaidon/Woodworkers-Friend/releases](https://github.com/dmaidon/Woodworkers-Friend/releases)
2. Click **"Draft a new release"**
3. Fill in release details:
   - **Tag version:** `v1.0.0`
   - **Release title:** `Woodworker's Friend v1.0.0`
   - **Description:** Copy content from `RELEASE_NOTES.md`

### Step 2: Upload Assets

Drag and drop these files to the release:
- `WoodworkersFriend-Setup-v1.0.0.exe` (Installer)
- `WoodworkersFriend-Portable-v1.0.0.zip` (Portable)
- *(GitHub auto-generates source code archives)*

### Step 3: Publish

- Check "This is a pre-release" if beta
- Click **"Publish release"**

---

## 📝 Version Numbering

We follow [Semantic Versioning](https://semver.org/):

**Format:** `MAJOR.MINOR.PATCH`

- **MAJOR:** Breaking changes (v2.0.0)
- **MINOR:** New features, backward-compatible (v1.1.0)
- **PATCH:** Bug fixes, backward-compatible (v1.0.1)

### Update Version Numbers

**1. AssemblyInfo.vb:**
```vb
<Assembly: AssemblyVersion("1.0.0.0")>
<Assembly: AssemblyFileVersion("1.0.0.0")>
```

**2. Inno Setup Script (WoodworkersFriend.iss):**
```ini
#define MyAppVersion "1.0.0"
```

**3. CHANGELOG.md:**
```markdown
## [1.0.0] - 2026-02-01
```

---

## 🧪 Testing Checklist

Before releasing, test on:

### Environments
- [ ] Windows 10 (fresh install)
- [ ] Windows 11 (fresh install)
- [ ] VM without .NET installed
- [ ] Low-res display (1024x768)
- [ ] High-DPI display (4K)

### Installation
- [ ] Installer runs without admin
- [ ] .NET check works correctly
- [ ] Start Menu shortcuts created
- [ ] Desktop shortcut (if selected)
- [ ] Uninstaller works cleanly

### Portable
- [ ] Runs without installation
- [ ] Databases created in app folder
- [ ] Works from USB drive

### Functionality
- [ ] All calculators work
- [ ] Help system loads
- [ ] Theme switching works
- [ ] Export features work (CSV, HTML)
- [ ] Database migrations run
- [ ] Error logging functional

---

## 📂 Package Contents Summary

### Installer Package Includes:
```
WoodworkersFriend-Setup-v1.0.0.exe
├── Application Files
│   ├── Woodworkers Friend.exe
│   ├── *.dll (dependencies)
│   ├── Resources/
│   └── Help.db, Resources.db
├── Documentation
│   ├── README.md
│   ├── LICENSE
│   ├── CHANGELOG.md
│   ├── INSTALL.md
│   └── RELEASE_NOTES.md
└── Installer Logic
    ├── .NET check
    ├── Registry entries
    └── Uninstaller
```

### Portable Package Includes:
```
WoodworkersFriend-Portable-v1.0.0.zip
├── Woodworkers Friend.exe
├── *.dll (dependencies)
├── Resources/
├── Help.db
├── Resources.db
├── README.md
├── LICENSE
├── CHANGELOG.md
├── INSTALL.md
└── RELEASE_NOTES.md
```

---

## 🔒 Code Signing (Optional)

For production releases, consider code signing:

1. **Obtain Code Signing Certificate**
   - Purchase from CA (DigiCert, Sectigo, etc.)
   - Or use self-signed for internal distribution

2. **Sign Executable**
   ```powershell
   # Using signtool.exe (from Windows SDK)
   signtool sign /f "certificate.pfx" /p "password" /t "http://timestamp.digicert.com" "Woodworkers Friend.exe"
   ```

3. **Sign Installer**
   ```powershell
   signtool sign /f "certificate.pfx" /p "password" /t "http://timestamp.digicert.com" "WoodworkersFriend-Setup-v1.0.0.exe"
   ```

**Benefits:**
- Removes SmartScreen warnings
- Shows publisher name
- Builds trust with users

---

## 📊 File Size Estimates

| Package | Size | Contents |
|---------|------|----------|
| Installer | ~15 MB | Compressed with LZMA2 |
| Portable ZIP | ~12 MB | Standard ZIP compression |
| Source Code | ~2 MB | No binaries, text only |

---

## 🆘 Troubleshooting Package Creation

### Issue: Inno Setup can't find files

**Solution:** Check paths in `WoodworkersFriend.iss`:
```ini
Source: "Woodworkers Friend\bin\Release\net10.0-windows\*";
```
Ensure you've built Release configuration first.

### Issue: Installer created but .NET check fails

**Solution:** Update .NET version check in script:
```pascal
findstr "Microsoft.WindowsDesktop.App 10.0"
```

### Issue: Portable version doesn't create databases

**Solution:** Ensure write permissions on folder. Run as regular user, not admin.

---

## 📧 Distribution Channels

Once packages are ready:

1. **GitHub Releases** (Primary)
   - Automatic download counting
   - Version history
   - Direct links

2. **Project Website** (Optional)
   - Custom landing page
   - Documentation
   - Community forums

3. **Software Directories** (Future)
   - SourceForge
   - Microsoft Store (requires UWP conversion)
   - Chocolatey package manager

---

## ✅ Post-Release Tasks

After publishing:

- [ ] Announce on GitHub Discussions
- [ ] Update project website (if applicable)
- [ ] Share on woodworking forums/communities
- [ ] Monitor for issues/bug reports
- [ ] Plan next release based on feedback

---

**Happy Packaging!** 🚀📦
