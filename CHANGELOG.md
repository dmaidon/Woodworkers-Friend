# Changelog

All notable changes to **Woodworker's Friend** will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

---

## [Unreleased]

---

## [1.0.1] - 2026-02-03

### Fixed
- **Database Migration Critical Fix**: Resolved dual issues preventing reference data from loading on first run:
  - **Issue 1A**: Removed Windows read-only file attribute from Reference.db that persisted from previous runs
  - **Issue 1B**: Fixed lazy initialization race condition where schema was created mid-migration
  - Solution: Force ReferenceDataManager initialization BEFORE any migration code executes
  - **Result**: All 127 rows now migrate successfully (25 wood species, 12 joinery types, 15 hardware items, 68 wood costs, 7 epoxy costs)
- **About Tab Logo**: Fixed logo image not displaying in PbMwwLogo control on About tab
- **Form Opening Size**: Corrected default form size from 1178x958 to 1200x1014

### Changed
- Database initialization now occurs at start of `PerformInitialMigration()` to prevent schema creation race conditions
- Added schema verification step after creation to catch failures early
- Added comprehensive error logging and verification for migration process
- Database file attribute management: removed at migration start, reapplied in Finally block after all migrations complete
- Improved error messages for database migration failures

---

## [1.0.0] - 2026-01-30

### Fixed
- **Critical Startup Bug**: Added missing `CreateProgramFolders()` method that prevented app from initializing
- **Database Schema Issues**:
  - Fixed Reference.db schema creation (SQLite.NET doesn't support multi-statement commands)
  - Fixed Reference.db being set to read-only before data migration
  - Added schema validation and automatic recreation for invalid databases
  - Separate ExecuteNonQuery() calls for each CREATE TABLE statement
- **Path Configuration**: All data paths correctly use Application.StartupPath for single-folder architecture

### Changed
- Installer now installs to `C:\Woodworkers Friend\` instead of Program Files for full write permissions
- All databases (Help.db, Reference.db, UserData.db) created in installation folder
- Logs, settings, and projects stored in installation folder
- Classic single-folder architecture maintained

### Infrastructure
- Updated installer script for C:\ root installation
- Renamed executable from `Woodworkers Friend.exe` to `WwFriend.exe` for cleaner file naming
- Updated installer to request administrator privileges for C:\ root installation

### Added - Help System Enhancements
- **Help System Enhancements**
  - Added "Epoxy Area Calculator" help topic with comprehensive calculator guide
  - Added "Stone Coat Top Coat Calculator" help topic with detailed instructions
  - Topics integrated into navigation tree under Calculators section
  - Full search support for new topics

---

## [1.0.0] - 2026-01-30 (Initial Release)

### Added - Phase 7: Reference & Cost Management System Complete

- **Cost Management System** (Phase 7.3) 🆕
  - Database-backed wood and epoxy cost management
  - User-friendly CRUD interface (FrmManageCosts)
  - 66 wood species costs migrated from CSV
  - 8 epoxy products migrated from CSV
  - Sortable data grids with visual indicators
  - Soft delete pattern (mark inactive)
  - Tracks user-added vs system entries
  - Date tracking for audit trail
  - Seamless integration with Board Feet and Epoxy calculators
  - CSV fallback for reliability
  - Automatic Title Case conversion for wood names
  - Access via About tab → [Manage Costs] button

- **Joinery Reference Database** (Phase 7.1)
  - 12 traditional and modern joint types
  - Detailed specifications with historical context
  - Strength ratings and difficulty levels
  - Required tools and reinforcement options
  - Filter by category (Frame, Box, Edge, Modern)
  - Beginner-friendly filter option
  
- **Hardware Standards Reference** (Phase 7.2)
  - 16 common hardware items with specifications
  - Categories: Hinges, Slides, Shelf Support, Fasteners, Brackets, Pulls/Knobs, Legs, Casters
  - Brand recommendations (Blum, Accuride, Grass)
  - Part numbers and mounting requirements
  - Weight capacity ratings
  - Installation notes and tips
  - Filter by hardware category

### Added - Phase 6: Help System Complete
- Comprehensive in-app help system
- 14 searchable help topics
- Markdown-based content with custom rendering
- Context-sensitive help for each calculator
- Help content stored in SQLite database
- Rich formatting support with custom tags

### Added - Phase 5: User Preferences Persistence
- Persistent user preferences in SQLite database
- Theme preference (Dark/Light) saved
- Scale preference (Imperial/Metric) saved
- Last active tab remembered
- Window size and state persistence
- Default calculator settings saved
- Auto-load preferences on startup

### Added - Phase 4: Shelf Sag Calculator & Enhancements
- **Shelf Sag Calculator**
  - 14 material types (plywood, MDF, hardwoods, softwoods, bamboo)
  - Deflection calculations with industry standards (1/360 rule)
  - Load capacity analysis (safe load vs maximum load)
  - Support type options (Bracket, Dado, Pin)
  - Edge stiffener calculations (front/back)
  - Safety factor analysis and warnings
  - Visual shelf diagram showing deflection
  - Material property database
  
- **Board Feet Enhancements**
  - Calculation history save/load
  - Project naming and tracking
  - History browser dialog
  - Print project capability

### Added - Phase 3: Wood Movement Calculator
- **Wood Movement Calculator**
  - 50+ wood species with movement coefficients
  - Humidity-based expansion/contraction calculations
  - Tangential (flat sawn) vs Radial (quarter sawn) grain options
  - Recommended panel gap sizing
  - Humidity presets (Indoor Winter/Summer, Shop, Kiln Dried)
  - Wood property display (density, type, movement rates)
  
### Added - Phase 2: Database Migration
- SQLite database implementation
- Migrated wood species data from in-code to database
- Database location: `%APPDATA%\Woodworkers Friend\WoodworkersFriend.db`
- Automatic database creation and schema initialization
- Data migration on first run
- Fallback to in-code data if database unavailable
- User-expandable wood species database

### Added - Phase 1: Core Calculators
- **Drawer Height Calculator**
  - 10 calculation methods (Hambridge, Golden Ratio, Fibonacci, Geometric, Arithmetic, Logarithmic, Exponential, Reverse, Uniform, Custom)
  - Quick presets (Kitchen, Office, Bathroom, Custom)
  - Visual grid of drawer heights
  - Save/load drawer projects
  
- **Door Calculator**
  - Inset and Overlay door types
  - Single and double door configurations
  - Rail, stile, and panel dimension calculations
  - Panel expansion gap and groove depth
  - Material area calculations
  - Save/load door projects
  
- **Board Feet Calculator**
  - Grid-based multi-board calculations
  - Waste factor options (0%, 10%, 15%, 20%)
  - Cost estimation
  - Project naming and saving
  
- **Epoxy Pour Calculator**
  - Rectangular and circular pour calculations
  - Custom area input
  - Results in multiple units (oz, gal, qt, pt, L, mL)
  - Waste factor options
  - Cost estimation
  - **Bonus:** Stone Coat top coat calculator
  
- **Joinery Calculator**
  - Mortise & Tenon (Standard, Haunched, Through)
  - Dovetails with automatic angle calculation
  - Box joints with pin counting
  - Dado joints for shelving
  - Visual joint diagrams
  
- **Cut List Optimizer**
  - Visual cutting diagram generation
  - Multiple board optimization
  - Kerf width accounting
  - Standard sheet sizes (4×8, 4×4, 4×10, custom)
  - Efficiency and waste metrics
  - Export cutting plans
  
- **Polygon Calculator**
  - Regular polygons (3-25 sides)
  - Angle calculations (interior/exterior, cutting angles)
  - Perimeter and area calculations
  - Rotating visual display
  
- **Miter Angle Calculator**
  - Miter (saw) angle calculations for polygonal frames
  - Bevel angle for tilted frames
  - Flat and tilted frame support
  - Material thickness compensation
  - Complementary and interior angle display
  
- **Dado Stack Calculator**
  - Blade combination calculator for precise dado widths
  - Standard and custom chipper sizes
  - Kerf width compensation
  - Multiple alternative configurations
  - Shim calculation support
  
- **Table Tipping Force Calculator**
  - Safety calculator for furniture stability
  - Force required to tip calculation
  - Important for child safety

- **Clamp Spacing Calculator**
  - Recommended clamp spacing for glue-ups
  - Wood type considerations (hardwood/softwood)
  - Glue type factors (PVA, Polyurethane, Epoxy)
  - Panel thickness and width inputs
  - Clamp pressure calculations
  - Number of clamps needed
  
- **Biscuit/Domino Spacing Calculator**
  - Spacing calculations for biscuit and domino joinery
  - Multiple biscuit sizes (#0, #10, #20, #FF, #H9)
  - Joint strength settings (Light, Medium, Heavy)
  - Edge distance and center mark positions
  - Stock thickness considerations
  
- **Finishing Materials Calculator**
  - Coverage calculations for stains, oils, and finishes
  - Multiple finish types (Polyurethane, Lacquer, Danish Oil, Tung Oil, Wax, Shellac, Varnish)
  - Multi-coat coverage and quantity needed
  - Dry time between coats calculator
  - Total project time estimation
  - Sand-between-coats option
  - Cost estimation
  
- **Glue Coverage Calculator**
  - Glue quantity needed for various joint types
  - Multiple glue types (PVA, Yellow, TiteBond III, Polyurethane, Epoxy, Hide Glue, CA)
  - Joint type considerations (Edge-to-Edge, Face-to-Face, End Grain, M&T, Dovetail, Biscuit/Domino)
  - Application method factors (Brush, Roller, Squeeze Bottle, Spreader)
  - Waste factor settings
  - Open time and clamp time recommendations
  - Application tips
  
- **Veneer & Inlay Calculator**
  - Sheet count calculations for veneer projects
  - Pattern type support (Book Match, Slip Match, Random, Radial, Diamond)
  - Custom sheet dimensions
  - Waste factor (5-50%)
  - Total area calculations with waste
  - Pattern-specific matching notes

- **Sanding Grit Progression Calculator**
  - Optimal sanding grit sequences
  - Wood type-specific recommendations
  - Sequential vs skip-grit progressions
  - Custom starting and ending grits
  - Detailed progression notes
  
- **Safety Calculators**
  - **Router Speed Calculator**: Maximum safe RPM for router bits based on diameter and surface speed
  - **Blade Height Calculator**: Recommended table saw blade height for different operations (ripping, crosscutting, dado, thin stock)
  - **Push Stick Requirements**: Risk evaluation for when push sticks are required
  - **Dust Collection CFM Calculator**: Required CFM for various tools with duct length compensation

### Added - Core Features
- **Wood Properties Reference**
  - 25 wood species with comprehensive data
  - Janka hardness, density, specific gravity
  - Shrinkage coefficients
  - Typical uses and workability
  - Search and filter capabilities
  - Export to CSV
  - Add custom species
  
- **Unit Conversions**
  - Inches ↔ Millimeters
  - Fractions ↔ Decimals
  - Conversion reference tables
  
- **Theme System**
  - Dark and Light themes
  - Status bar theme toggle
  - Persistent theme preference
  
- **Scale System**
  - Imperial and Metric units
  - Status bar scale toggle
  - Persistent scale preference
  
- **Export Capabilities**
  - Export to CSV
  - Export to Text
  - Export to HTML
  - Print support
  
- **Error Handling**
  - Comprehensive error logging
  - Log file viewer
  - Automatic log cleanup
  - Diagnostic information

### Added - User Interface
- Tabbed interface for organized access
- Split-pane layouts for input/results
- Status bars with version, theme, scale indicators
- Context menus for common operations
- Tooltips on all input fields
- Auto-select text in input fields
- Keyboard shortcuts support
- Responsive window sizing

### Technical Infrastructure
- Partial class architecture for maintainability
- Manager pattern for business logic separation
- Event coordinator for centralized event handling
- Validation manager for input validation
- Theme manager for consistent theming
- Database manager with singleton pattern
- Error handler with file logging
- Resource management with proper disposal

---

## [0.9.0] - 2025-12-15 (Beta)

### Added
- Initial beta release
- Core drawer calculator functionality
- Basic door calculator
- Board feet grid calculator
- Epoxy volume calculator
- Unit conversion tools
- Dark theme prototype

### Changed
- Migrated from WinForms .NET Framework 4.8 to .NET 10
- Updated to modern VB.NET syntax
- Implemented file-scoped namespaces

### Fixed
- High DPI scaling issues
- Control layout inconsistencies
- Calculation precision errors

---

## Version History

| Version | Date | Milestone |
|---------|------|-----------|
| 1.0.1 | 2026-02-03 | **Critical Fix** - Database migration fixes |
| 1.0.0 | 2026-01-30 | **Full Release** - Complete feature set |
| 0.9.0 | 2025-12-15 | Beta - Core calculators |
| 0.5.0 | 2025-11-01 | Alpha - Prototype testing |

---

## Migration Phases

### **Phase 1: Core Calculators** ✅
- Drawer, Door, Board Feet, Epoxy, Joinery, Cut List, Polygon calculators
- Basic reference data (in-code)

### **Phase 2: Database Migration** ✅
- SQLite database implementation
- Wood species migration to database
- User-expandable data

### **Phase 3: Wood Movement** ✅
- Wood movement calculator with 50+ species
- Humidity-based calculations
- Panel gap recommendations

### **Phase 4: Shelf Sag & History** ✅
- Shelf sag calculator with 14 materials
- Edge stiffener support
- Board feet calculation history

### **Phase 5: User Preferences** ✅
- Persistent theme and scale preferences
- Window state persistence
- Last tab memory
- Calculator defaults

### **Phase 6: Help System** ✅
- Database-driven help content
- Searchable help topics
- Markdown rendering

### **Phase 7: Reference System** ✅
- **7.1:** Joinery reference database (12 types)
- **7.2:** Hardware standards database (16 items)
- **7.3:** Cost Management System (66 wood costs, 8 epoxy costs)

---

## 🐛 Known Issues

### **Minor**
- None currently reported

### **Enhancement Requests**
- See [GitHub Issues](https://github.com/dmaidon/Woodworkers-Friend/issues) for feature requests

---

## 📝 Notes

- All measurements default to Imperial (inches, feet)
- Metric support available via scale toggle
- Database auto-creates on first run
- Logs stored in `%APPDATA%\Woodworkers Friend\Logs\`
- Old log files cleaned up automatically (>30 days)

---

**For detailed feature documentation, see the in-app Help tab or visit the [Wiki](https://github.com/dmaidon/Woodworkers-Friend/wiki).**

---

*This changelog is maintained as part of the Woodworker's Friend project.*
*Last Updated: February 3, 2026*
