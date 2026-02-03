# 🪚 Woodworker's Friend v1.0.1 - Release Notes

**Release Date:** February 2, 2026

## 🎉 Welcome to Woodworker's Friend!

We're excited to announce the first public release of Woodworker's Friend - a comprehensive woodworking calculator and planning tool designed to make your woodworking projects easier, more accurate, and more efficient.

---

## 🔥 Critical Updates (February 2, 2026)

### Fixed - Critical Startup Issues
- **Fixed missing `CreateProgramFolders()` method** that prevented application startup
- **Fixed database schema creation** - SQLite.NET multi-statement command issue resolved
- **Fixed database migration timing** - Reference.db no longer set to read-only before data insertion
- **Added automatic schema validation** - Invalid databases are now automatically recreated

### Enhanced - Help System
- Added **Epoxy Area Calculator** help topic with comprehensive guide
- Added **Stone Coat Top Coat Calculator** help topic with detailed instructions
- New topics integrated into navigation tree and search

### Changed - Installation
- Application now installs to `C:\Woodworkers Friend\` for full write permissions
- All data (logs, databases, settings) stored in installation folder
- Single-folder architecture for easy backup and portability

---

## ✨ What's Included

### 🔧 18 Specialized Calculators
From drawer heights to miter angles, from wood movement to finish coverage - we've got you covered!

### 📚 Comprehensive Databases
- 50+ wood species with movement characteristics
- 12 traditional joinery types
- 16 hardware standards
- Cost tracking for wood and epoxy

### 🛠️ Utilities & Tools
- Cut list optimizer with visual diagrams
- Safety calculators for router speeds, blade heights, dust collection
- Sanding grit progression planner
- Unit conversions and project management

### 📖 Built-in Help System
Searchable documentation with 31 help topics including calculator guides, definitions, formulas, and woodworking tips.

---

## 📦 Download Options

### Installer (Recommended)
**File:** `WoodworkersFriend-Setup-v1.0.0.exe`  
**Size:** ~15 MB  
**Includes:** .NET runtime check, Start Menu shortcuts, auto-updater

**Download:** [Windows Installer](https://github.com/dmaidon/Woodworkers-Friend/releases/download/v1.0.0/WoodworkersFriend-Setup-v1.0.0.exe)

### Portable Version
**File:** `WoodworkersFriend-Portable-v1.0.0.zip`  
**Size:** ~12 MB  
**Benefits:** No installation, run from USB, portable settings

**Download:** [Portable ZIP](https://github.com/dmaidon/Woodworkers-Friend/releases/download/v1.0.0/WoodworkersFriend-Portable-v1.0.0.zip)

### Source Code
**Download:** [Source Code (zip)](https://github.com/dmaidon/Woodworkers-Friend/archive/refs/tags/v1.0.0.zip)  
**Build Requirements:** Visual Studio 2022, .NET 10.0 SDK

---

## 🆕 Key Features

### Calculator Highlights

#### Drawer Heights
- 10 progression methods (Golden Ratio, Fibonacci, Hambridge, etc.)
- Visual grid display
- Quick presets for kitchen, office, bathroom

#### Joinery Suite
- Mortise & Tenon (Standard, Haunched, Through)
- Dovetails (Through, Half-Blind) with angle calculator
- Box Joints with spacing optimization
- Biscuit/Domino placement calculator

#### Materials & Finishing
- Veneer calculator with pattern-specific waste (Book Match, Slip Match, etc.)
- Finishing materials: 8 finish types with coverage, time, cost
- Glue coverage: 7 glue types with open/clamp times

#### Wood Movement
- 50+ species database
- Tangential and Radial calculations
- Panel gap recommendations
- Humidity presets

#### Safety Tools
- Router bit speed calculator with safety warnings
- Blade height recommendations by operation
- Push stick requirement evaluator
- Dust collection CFM calculator

---

## 💻 System Requirements

- **OS:** Windows 10 (1809+) or Windows 11
- **.NET:** 10.0 Desktop Runtime (auto-prompted if missing)
- **RAM:** 512 MB minimum, 1 GB recommended
- **Display:** 1024x768 minimum, 1920x1080 recommended

---

## 🚀 Getting Started

1. **Install the application**
   - Run installer or extract portable version
   - .NET runtime installed automatically if needed

2. **First launch**
   - Choose theme (Light/Dark)
   - Select unit system (Imperial/Metric)
   - Explore calculators via tabs

3. **Quick wins**
   - Try **Drawer Calculator** → Select "Golden Ratio"
   - Try **Miter Angle** → Enter 6 sides for hexagon
   - Try **Materials & Finishing** → Calculate finish for your project

4. **Get help**
   - Press `F1` or click **Help** tab
   - Hover over inputs for tooltips
   - Check **Definitions** for glossary

---

## 📝 Known Issues

### Current Limitations
- **Windows only** - No macOS or Linux support yet
- **English only** - No localization in v1.0
- **No cloud sync** - All data stored locally

### Workarounds
- Export projects as CSV for backup/sharing
- Use portable version for multi-computer setup

---

## 🔮 Coming Soon

Based on user feedback, we're considering:
- Lumber Yield Calculator
- PDF export for cutting diagrams
- Mobile companion app
- Cloud project sync
- Additional wood species
- More joinery types

**Want to request a feature?** [Open an issue!](https://github.com/dmaidon/Woodworkers-Friend/issues/new?labels=enhancement&template=feature_request.md)

---

## 🐛 Reporting Bugs

Found a bug? We want to hear about it!

1. Check [existing issues](https://github.com/dmaidon/Woodworkers-Friend/issues)
2. Gather error log from `%APPDATA%\WoodworkersFriend\ErrorLog.txt`
3. [Report the bug](https://github.com/dmaidon/Woodworkers-Friend/issues/new?labels=bug&template=bug_report.md)

---

## 🤝 Contributing

Woodworker's Friend is open source! Contributions welcome:
- 📝 [Report bugs](https://github.com/dmaidon/Woodworkers-Friend/issues/new?template=bug_report.md)
- 💡 [Suggest features](https://github.com/dmaidon/Woodworkers-Friend/issues/new?template=feature_request.md)
- 🔧 [Submit pull requests](https://github.com/dmaidon/Woodworkers-Friend/pulls)
- 📚 [Improve documentation](https://github.com/dmaidon/Woodworkers-Friend/wiki)

Read [CONTRIBUTING.md](CONTRIBUTING.md) for guidelines.

---

## 📄 License

Woodworker's Friend is licensed under the **MIT License**.

**You are free to:**
- ✅ Use commercially
- ✅ Modify
- ✅ Distribute
- ✅ Private use

See [LICENSE](LICENSE) for full terms.

---

## 🙏 Credits

### Built With
- **.NET 10.0** - Application framework
- **VB.NET** - Programming language
- **Windows Forms** - UI framework
- **SQLite** - Database engine

### Special Thanks
- Woodworking community for formulas and best practices
- Beta testers for feedback
- Open source contributors

---

## 📧 Contact

- **Issues:** [GitHub Issues](https://github.com/dmaidon/Woodworkers-Friend/issues)
- **Discussions:** [GitHub Discussions](https://github.com/dmaidon/Woodworkers-Friend/discussions)
- **Email:** [Your email if you want to include it]

---

## ⚠️ Safety Disclaimer

This software provides calculations based on industry-standard formulas. However:

- Always verify critical measurements
- Follow proper safety procedures
- Use appropriate safety equipment
- Consult with professionals for structural projects
- Author assumes no liability for injuries or damages

**Stay safe in the shop!** 🪚

---

**Happy Woodworking!**  
🪚🎨✨
