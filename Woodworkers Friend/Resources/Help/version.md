# Version Information

## Woodworker's Friend

**Current Version:** 1.0.1 (Build 26.2.3.15)  
**Release Date:** February 3, 2026  
**Platform:** .NET 10.0 (Windows 10/11)

## What's New in Version 1.0.1

### Critical Fixes

#### Database Migration System ✅
- **Fixed**: Reference data now loads correctly on first run
- **Fixed**: "attempt to write a readonly database" error
- **Fixed**: "no such table: WoodSpecies" race condition
- **Result**: All 127 reference rows migrate successfully
  - 25 wood species
  - 12 joinery types
  - 15 hardware standards
  - 68 wood costs
  - 7 epoxy costs

#### UI Fixes
- **Fixed**: About tab logo now displays correctly
- **Fixed**: Main form opens at correct size (1200x1014)

#### Startup Fixes
- **Fixed**: Missing `CreateProgramFolders()` method that prevented app initialization
- **Fixed**: Reference.db schema creation issues (SQLite.NET multi-statement compatibility)

### Technical Improvements
- Early database initialization prevents race conditions
- Schema verification after creation
- Comprehensive error logging for troubleshooting
- Proper file attribute management (read-only handling)
- Installer configuration for single-folder architecture

### Help System Enhancements
- Added "Epoxy Area Calculator" help topic with comprehensive guide
- Added "Stone Coat Top Coat Calculator" help topic with detailed instructions
- Topics integrated into navigation tree under Calculators section
- Full search support for new topics

---

## Version 1.0.0 (January 30, 2026) - Initial Release

### Major Features Added

#### Shelf Sag Calculator 🆕
- Calculate shelf deflection based on material, span, load
- Support for 14 material types
- **Edge stiffener analysis** - T-beam and I-beam calculations
- Composite beam calculations for mixed materials
- Visual diagram with realistic sag curves
- Safe load limits and safety factors
- Real-time calculation updates

#### Wood Movement Calculator Enhancements
- Added sub-tab organization
- Improved navigation with tab control
- Better integration with structural calculators

#### Help System Overhaul
- Resource-based help system
- Markdown documentation
- Faster loading
- Easier to maintain and extend
- Comprehensive documentation for all features

### Additional Materials
- Southern Yellow Pine (SYP) - Structural grade
- White Pine - Economy softwood
- Poplar - Paint-grade hardwood

### Technical Improvements
- Reduced code size by ~95% in help system
- Embedded Markdown resources
- Better Option Strict compliance
- Improved error handling
- Enhanced input validation

## System Requirements

- **Operating System:** Windows 10/11 (version 22000+)
- **Framework:** .NET 10.0 Runtime
- **Display:** 1280x900 minimum resolution
- **Memory:** 512 MB RAM minimum
- **Disk Space:** 50 MB

## Previous Versions

### Version 2.x
- Joinery calculator (mortise & tenon, dovetails, box joints, dados)
- Wood movement calculator
- Cut list optimizer
- Enhanced door calculator
- Theme support (dark/light modes)

### Version 1.x
- Drawer calculator with mathematical progressions
- Door calculator
- Board feet calculator
- Epoxy pour calculator
- Unit conversions
- Fraction/decimal conversions

## Credits

**Development:** Woodworker's Friend Team  
**Engineering Calculations:** Based on industry-standard formulas  
**Material Data:** Wood Database and engineering references

## License

Woodworker's Friend is proprietary software.  
© 2024-2026 All rights reserved.

## Support

For questions, bug reports, or feature requests:
- Check this help system first
- Review troubleshooting section
- Contact support via project repository

## Acknowledgments

Special thanks to:
- The woodworking community for feedback
- Beta testers for valuable input
- Contributors to wood movement and structural databases

---

**Build with confidence. Calculate with precision.** 🪵

*Last Updated: January 29, 2026*
