# 🪚 Woodworker's Friend

**A comprehensive woodworking calculator and planning tool for professional woodworkers and enthusiasts.**

[![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet)](https://dotnet.microsoft.com/)
[![VB.NET](https://img.shields.io/badge/VB.NET-Windows%20Forms-blue)](https://docs.microsoft.com/en-us/dotnet/visual-basic/)
[![SQLite](https://img.shields.io/badge/SQLite-3.0-003B57?logo=sqlite)](https://www.sqlite.org/)
[![License](https://img.shields.io/badge/license-MIT-green)](LICENSE)

---

## 📋 Table of Contents

- [Overview](#overview)
- [Features](#features)
- [Screenshots](#screenshots)
- [Installation](#installation)
- [Usage](#usage)
- [Technology Stack](#technology-stack)
- [Project Structure](#project-structure)
- [Development](#development)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)

---

## 🎯 Overview

**Woodworker's Friend** is a feature-rich desktop application designed to simplify complex woodworking calculations and provide quick access to essential reference data. Whether you're building cabinets, designing furniture, or planning your next project, this tool helps you calculate precise measurements, estimate materials, and make informed decisions.

### **Why Woodworker's Friend?**

- ✅ **Save Time** - Instant calculations for common woodworking tasks
- ✅ **Increase Accuracy** - Mathematical precision reduces waste and errors
- ✅ **Learn Best Practices** - Built-in reference guides for joinery and materials
- ✅ **Plan Efficiently** - Optimize cut lists and material usage
- ✅ **Professional Results** - Industry-standard calculations and formulas

---

## ✨ Features

### **🔧 Calculators**

#### **Drawer Height Calculator**
- **10 calculation methods** including Hambridge Ratio, Golden Ratio, Fibonacci, Geometric, Arithmetic progressions
- Quick presets for kitchen, office, and bathroom cabinets
- Visual representation of drawer configurations
- Export results to CSV, text, or HTML

#### **Door Calculator**
- Cabinet door calculations for **inset** and **overlay** styles
- Precise rail, stile, and panel dimensions
- Single and double door configurations
- Accounts for panel expansion gaps and groove depths

#### **Board Feet Calculator**
- Calculate lumber requirements with waste factor
- Grid-based multi-board calculations
- Cost estimation
- Project save/load with history tracking

#### **Epoxy Pour Calculator**
- Calculate resin volumes for river tables and bar tops
- Rectangular, circular, and custom area support
- Multiple waste percentage options
- Results in ounces, gallons, quarts, pints, liters, and milliliters
- **Bonus:** Stone Coat top coat calculator

#### **Joinery Calculator**
- **Mortise & Tenon** (Standard, Haunched, Through)
- **Dovetails** (Through and Half-Blind) with automatic angle calculation
- **Box Joints** (Finger Joints)
- **Dado Joints** for shelf housing
- Visual diagrams for each joint type

#### **Wood Movement Calculator**
- Predict expansion and contraction based on humidity changes
- **50+ wood species** with accurate movement coefficients
- Tangential (flat sawn) and Radial (quarter sawn) calculations
- Recommended panel gap sizing
- Humidity presets for different environments

#### **Shelf Sag Calculator**
- Calculate shelf deflection under load
- **14 material types** including plywood, MDF, hardwoods, softwoods
- Support type options: Bracket, Dado, Pin
- Edge stiffener calculations
- Safety factor analysis with industry standards (1/360 deflection limit)
- Visual shelf diagram

#### **Cut List Optimizer**
- Optimize cutting patterns to minimize waste
- Visual cutting diagrams for each board
- Supports standard sheet sizes (4x8, 4x4, 4x10, custom)
- Accounts for saw kerf (blade width)
- Material cost estimation
- Export cutting plans

#### **Miter Angle Calculator** 🆕
- **Calculate precise miter and bevel angles** for perfect joints
- **Frame types supported:**
  - **Flat frames** (picture frames, table tops, mirrors)
  - **Tilted frames** (crown molding, cove molding, compound angles)
- **Polygon support:** 3-24 sides for any polygonal project
- **Common applications:**
  - Picture frames (4 sides, 45° miters)
  - Hexagon tables (6 sides, 30° miters)
  - Octagon frames (8 sides, 22.5° miters)
  - Crown molding (compound cuts with spring angle)
- **Results displayed:**
  - **Miter (Saw) Angle** - Set your miter saw to this angle
  - **Bevel Angle** - Set blade tilt for compound cuts (tilted frames only)
  - **Complementary Angle** - Reference angle for verification
  - **Interior Angle** - Polygon's interior angle at each vertex
- **Tilt angle presets:**
  - 38° - Standard crown molding (52/38 spring angle)
  - 45° - Common for 45/45 crown molding
  - Custom angles 0-90°
- **Visual diagram** shows miter joint configuration
- **Real-time calculation** as you type
- **Tooltips with usage examples** for quick reference
- **Perfect for:** Crown molding installation, picture frame building, polygon-shaped furniture, box lids

#### **Polygon Calculator**
- Calculate dimensions for **regular polygons (3-25 sides)**
- **Dual input modes:**
  - Enter side length → calculates radius, apothem, perimeter, area
  - Enter radius → calculates side length, apothem, perimeter, area
- **Comprehensive calculations:**
  - Interior angle (angle at each vertex)
  - Exterior angle (central angle per side)
  - Miter angle (cut angle for segments)
  - Apothem (inradius - perpendicular distance to side)
  - Perimeter (total distance around polygon)
  - Area (surface area enclosed)
- **Unit support:**
  - Imperial (inches, sq.in) and Metric (millimeters, sq.cm)
  - Automatic conversion when switching units
- **Quick presets** for common polygons:
  - Triangle (3), Square (4), Hexagon (6), Octagon (8)
- **Rotating 3D visual display** with:
  - Vertex markers, side labels, angle arc indicators
  - Shadow and lighting effects
  - Click to pause/resume animation
- **Copy results to clipboard** - Ready for spreadsheets or documentation
- **Perfect for:** Segmented bowls, polygon frames, faceted boxes, geometric panels

#### **Table Tipping Force Calculator**
- Safety calculator for furniture stability
- Critical for pieces used around children
- Calculates force required to tip furniture

### **📚 Reference Databases**

#### **Wood Properties Reference**
- **25+ wood species** with comprehensive data
- Janka hardness ratings
- Specific gravity and density
- Shrinkage coefficients (radial and tangential)
- Typical uses, workability, and cautions
- Searchable and filterable (hardwoods, softwoods, all)
- **User-expandable** - Add your own custom species

#### **Joinery Reference Guide**
- **12 traditional joint types** with detailed specifications
- Strength ratings (1-5 scale)
- Difficulty levels (Beginner, Intermediate, Advanced)
- Required tools and equipment
- Typical applications and historical context
- Reinforcement options
- Filter by category: Frame, Box, Edge, Modern

#### **Hardware Standards Database**
- **16 common hardware items** with precise specifications
- Categories: Hinges, Slides, Shelf Support, Fasteners, Brackets, Pulls/Knobs, Legs, Casters
- Brand recommendations and part numbers
- Mounting requirements and dimensions
- Weight capacity ratings
- Installation notes and tips

#### **Cost Management System** 🆕
- **Database-backed cost tracking** for wood and epoxy
- **66 wood species** with thickness and cost per board foot
- **8 epoxy products** with brand, type, and cost per gallon
- User-friendly CRUD interface with sortable grids
- Soft delete pattern (mark inactive, preserve history)
- Seamless integration with Board Feet and Epoxy calculators
- CSV fallback for reliability
- Track user-added vs system entries
- Audit trail with date tracking

#### **Safety Calculators** 🆕
- **Router Bit Speed Calculator**
  - Calculate safe RPM based on bit diameter and surface speed
  - Industry-standard formula: RPM = (Surface Speed × 12) / (π × Diameter)
  - Safety warnings for dangerous speeds
  - Rim speed calculation in MPH
  - Recommended range: 9,000-12,000 ft/min surface speed
  
- **Blade Height Recommendations**
  - Safe blade height for table saw operations
  - Operation-specific recommendations (Ripping, Crosscutting, Dado/Groove, Thin Stock)
  - Detailed safety notes for thin and thick stock
  - Critical safety warnings and best practices
  
- **Push Stick Requirements Evaluator**
  - Risk assessment based on stock dimensions
  - 4-level risk analysis: LOW, MODERATE, HIGH, CRITICAL
  - Safety device requirements (push sticks, featherboards, guards)
  - Comprehensive safety checklists
  - Adjusts recommendations based on guard and featherboard usage
  
- **Dust Collection CFM Calculator**
  - Calculate required CFM (Cubic Feet per Minute) for effective dust collection
  - Tool-specific requirements for 8 common tools:
    - Table Saw, Router Table, Miter Saw, Planer, Jointer, Bandsaw, Drum Sander, Thickness Sander
  - Port diameter and duct length calculations
  - Static pressure loss compensation
  - Health and safety warnings for wood dust exposure
  - General dust collection system tips and best practices

#### **Sanding Grit Progression Calculator** 🆕
- **Optimal grit sequence planning** for professional-quality finishes
- **Wood-specific recommendations** (Softwood vs Hardwood)
- **Two progression methods:**
  - Sequential (Thorough) - Every grit for best finish quality
  - Skip-Grit (Fast) - Every other grit for painted surfaces
- **Standard grit range:** 40 → 60 → 80 → 100 → 120 → 150 → 180 → 220 → 320 → 400 → 600
- **Detailed results include:**
  - Visual grit progression sequence
  - Time estimates (3-5 minutes per grit)
  - Method-specific recommendations
  - Individual grit descriptions and purposes
  - Wood-specific sanding tips
  - Safety reminders (dust mask requirements)
- **Common finish sequences** for different project types
- **Comprehensive help documentation** with sanding science and best practices

#### **Clamp Spacing Calculator** 🆕
- **Optimal clamp placement** for panel glue-ups
- **Wood type adjustments** (Hardwood vs Softwood)
- **Glue type optimization:**
  - PVA (White/Yellow Glue) - Standard spacing
  - Polyurethane - Tighter spacing for better pressure
  - Epoxy - Slightly wider spacing acceptable
- **Automatic calculations:**
  - Recommended spacing (8-15" range following 12x thickness rule)
  - Number of clamps needed
  - Clamp pressure (PSI) recommendations
- **Practical tips:**
  - Alternate clamps top/bottom to prevent bowing
  - Clamping time by glue type
  - Caul (clamping board) recommendations
- **Units:** Imperial (inches) or Metric (mm)

#### **Biscuit/Domino Joinery Calculator** 🆕
- **Precise biscuit and domino placement** for professional edge joinery
- **Joinery types supported:**
  - Biscuit Joinery (#0, #10, #20, #FF, #H9 sizes)
  - Festool Domino (4x20, 5x30, 6x40, 8x50, 10x50 sizes)
- **Strength-based spacing:**
  - Light duty - Wider spacing (face frames, panels)
  - Medium duty - Standard spacing (edge-to-edge joints)
  - Heavy duty - Tight spacing (structural joints)
- **Smart edge distance calculation:**
  - Based on actual biscuit/domino length (prevents exposure)
  - Includes 5/8" padding for trimming and error tolerance
  - Prevents joinery exposure at joint ends
- **Automatic center mark positions:**
  - Numbered list of exact marking locations
  - Displayed in fractional inches for shop use
  - Accounts for joint length and edge distances
- **Visual results:**
  - Total number of biscuits/dominos needed
  - Edge distance from ends
  - Recommended spacing between joinery
  - Complete center mark list for layout
- **Units:** Imperial (inches) or Metric (mm)

#### **Dado Stack Calculator** 🆕
- **Optimal blade combination calculator** for dado stack setups
- **Smart algorithm finds best blade stack** to achieve target width
- **Features:**
  - Dynamic programming algorithm for optimal combinations
  - Supports custom blade sizes and manufacturers
  - Preset configurations (Standard 8" set, Premium with shims)
  - Save and load custom dado set configurations
  - Error calculation with color-coded accuracy indicators
  - Alternative blade combinations (up to 3 options)
- **Blade types supported:**
  - Outer blades (required 2x 1/8" or custom)
  - Chippers: 1/16", 1/8", 3/16", 1/4"
  - Precision shims: 0.004", 0.008"
  - Custom sizes via dialog
- **Advanced features:**
  - Context menu with copy, detailed results, presets
  - Export configurations to JSON
  - Import/load saved configurations
  - Comprehensive tooltips on all inputs
  - Error provider for validation feedback
- **Results display:**
  - Blade assembly order (numbered list)
  - Total width calculation
  - Error from target (±0.001" precision)
  - Monospace formatting for shop printout
  - Alternative solutions if available
- **Units:** Imperial (inches) or Metric (mm)
- **Tolerance:** ±0.005" default (adjustable in algorithm)

#### **Materials & Finishing Calculators** 🆕
Three integrated calculators sharing a common area input for efficient material planning:

##### **Veneer & Inlay Calculator**
- **Calculate veneer sheets needed** for any project
- **Pattern-specific waste factors:**
  - Book Match (20%) - Mirror image pairs
  - Slip Match (15%) - Repeat pattern
  - Random (10%) - No matching required
  - Radial (25%) - Sunburst patterns
  - Diamond (30%) - Complex matching
- **Inputs:**
  - Project area (Length × Width or direct sq ft/sq m entry)
  - Veneer sheet dimensions (default 96" × 48")
  - Pattern type (auto-adjusts waste factor)
- **Results:**
  - Number of sheets needed
  - Total area including waste allowance

##### **Finishing Materials Calculator**
- **Calculate finish quantities** for any surface area
- **8 finish types** with auto-filled coverage rates:
  - Stain (200 sq ft/gal)
  - Polyurethane (125 sq ft/qt)
  - Lacquer (150 sq ft/qt)
  - Danish Oil (200 sq ft/qt)
  - Tung Oil (150 sq ft/qt)
  - Wax (300 sq ft/lb)
  - Shellac (175 sq ft/qt)
  - Varnish (100 sq ft/qt)
- **Comprehensive calculations:**
  - Quantity needed (oz, quarts, gallons, or ml, liters)
  - Total finishing time (including dry time between coats)
  - Estimated cost
- **Options:**
  - Number of coats (1-10)
  - Dry time between coats
  - Sanding between coats (adds time estimate)
- **Application tips** for each finish type

##### **Glue Coverage Calculator**
- **Calculate glue quantities** for any joint area
- **7 glue types** with open/clamp times:
  - PVA White (10 min open, 1 hr clamp)
  - Yellow/Titebond (8 min open, 45 min clamp)
  - Titebond III (10 min open, 1 hr clamp)
  - Polyurethane (15 min open, 4 hr clamp)
  - Epoxy (30 min open, 24 hr clamp)
  - Hide Glue (5 min open, 2 hr clamp)
  - CA/Super Glue (1 min open, instant clamp)
- **Joint type multipliers:**
  - Edge-to-Edge (1.0×) - Standard consumption
  - Face-to-Face (1.0×) - Standard consumption
  - End Grain (2.0×) - Double coverage needed
  - Mortise & Tenon (1.5×) - Extra for multiple surfaces
  - Dovetail (1.3×) - Complex surfaces
  - Biscuit/Domino (0.8×) - Less area exposed
- **Results:**
  - Amount needed (oz and ml)
  - Open time (working time)
  - Clamp time requirement
  - Application tips per glue/joint combination

##### **Shared Features**
- **Two input methods:**
  - Length × Width for calculated area
  - Direct area entry (if already known)
- **Full metric support:**
  - Inches, Feet, Millimeters, Centimeters
  - All results display in appropriate units
  - Labels update dynamically (sq ft/qt vs sq m/L)
- **"Calculate All" button** runs all three calculators at once
- **Real-time updates** as you change inputs

### **🛠️ Utilities**

- **Unit Conversions** - Inches ↔ Millimeters, Fractions ↔ Decimals
- **Conversion Tables** - Quick reference for common fractions
- **Project Management** - Save and load calculations for repeat projects
- **Export Capabilities** - CSV, Text, HTML formats
- **Print Support** - Print calculations and cutting diagrams
- **Dark/Light Themes** - Reduce eye strain with theme switching
- **User Preferences** - Persistent settings for theme, scale, window state
- **Comprehensive Help System** - Searchable in-app documentation
- **Error Logging** - Automatic logging for troubleshooting

---

## 📸 Screenshots

> **Coming Soon:** Screenshots showcasing the main calculators and reference databases.

### **Main Interface**
- Tabbed interface with organized calculator sections
- Clean, professional layout
- Responsive split-pane design

### **Calculators**
- Drawer height progressions with visual grid
- Door calculations with material breakdown
- Joinery diagrams with dimensional callouts
- Shelf sag analysis with safety indicators

### **Reference Databases**
- Sortable, filterable data grids
- Detailed information panels
- Search capabilities

---

## 💻 Installation

### **Requirements**

- **Operating System:** Windows 10 or later
- **.NET Runtime:** .NET 10.0 or later
- **Display:** 1200x900 minimum resolution (1920x1080 recommended)
- **Storage:** ~50 MB for application and database

### **Installation Steps**

1. **Download the latest release**
   ```
   Download from GitHub Releases page
   ```

2. **Extract the ZIP file**
   ```
   Extract to a folder of your choice (e.g., C:\Program Files\Woodworkers Friend\)
   ```

3. **Run the application**
   ```
   Double-click "Woodworkers Friend.exe"
   ```

4. **First Run**
   - Application creates database automatically
   - Default preferences are set
   - Initial data migration occurs
   - Splash screen displays during initialization

### **Building from Source**

1. **Clone the repository**
   ```bash
   git clone https://github.com/dmaidon/Woodworkers-Friend.git
   cd Woodworkers-Friend
   ```

2. **Open in Visual Studio**
   ```
   Open "Woodworkers Friend.sln" in Visual Studio 2022 or later
   ```

3. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

4. **Build the solution**
   ```bash
   dotnet build --configuration Release
   ```

5. **Run the application**
   ```bash
   dotnet run --project "Woodworkers Friend"
   ```

---

## 🚀 Usage

### **Quick Start**

1. **Launch the application**
2. **Select a calculator tab** (Drawers, Doors, Joinery, etc.)
3. **Enter your measurements** in the input fields
4. **Click Calculate** to see results
5. **Export or print** your results for shop use

### **Common Workflows**

#### **Cabinet Drawer Design**
1. Navigate to **Drawers** tab
2. Select a preset (Kitchen Standard, Office Desk, etc.)
3. Adjust parameters as needed
4. Click **Calculate**
5. Review the grid of drawer heights
6. Export results or save project

#### **Wood Movement Planning**
1. Navigate to **Wood Movement** tab
2. Select wood species from dropdown (50+ species)
3. Enter board width
4. Set initial and final humidity levels (or use presets)
5. Select grain direction (Tangential/Radial)
6. Click **Calculate Movement**
7. Review recommended panel gaps

#### **Shelf Design with Sag Analysis**
1. Navigate to **Wood Movement** → **Shelf Sag** tab
2. Select shelf material (14 options)
3. Enter span, thickness, width, and expected load
4. Select support type (Bracket, Dado, Pin)
5. Add edge stiffeners if needed
6. Click **Calculate Shelf Sag**
7. Check safety status and recommendations

#### **Cut List Optimization**
1. Navigate to **Cut List** tab
2. Add pieces with labels, lengths, widths, and quantities
3. Select stock board size
4. Set saw kerf width
5. Click **Optimize**
6. Review cutting diagrams
7. Navigate through multiple board patterns
8. Export cutting plan

### **Tips & Tricks**

- 💡 **Hover over input fields** to see tooltips with valid ranges
- 💡 **Use presets** as starting points and customize
- 💡 **Click theme toggle** in status bar to switch Dark/Light mode
- 💡 **Click scale indicator** to toggle Imperial/Metric
- 💡 **Text fields auto-select** when clicked for quick editing
- 💡 **Right-click log viewer** for context menu options
- 💡 **Save projects** for repeat builds

---

## 🏗️ Technology Stack

### **Core Technologies**

| Technology | Version | Purpose |
|------------|---------|---------|
| **Visual Basic .NET** | VB 16+ | Primary programming language |
| **.NET Framework** | .NET 10.0 | Application framework |
| **Windows Forms** | - | User interface framework |
| **SQLite** | 3.x | Embedded database |
| **System.Data.SQLite** | Latest | SQLite ADO.NET provider |

### **Design Patterns**

- **Partial Classes** - Modular code organization
- **Manager Pattern** - Centralized business logic
- **Repository Pattern** - Database abstraction
- **Event Coordinator** - Centralized event handling
- **Singleton Pattern** - DatabaseManager, ErrorHandler

### **Key Libraries**

- `System.Drawing` - Graphics rendering for diagrams
- `System.Data.SQLite` - Database operations
- `System.ComponentModel` - Data binding

---

## 📁 Project Structure

```
Woodworkers Friend/
│
├── Forms/                          # Modal dialogs and secondary forms
│   ├── FrmSplash.vb               # Splash screen
│   ├── FrmAddWoodSpecies.vb       # Add custom wood species
│   └── FrmCalculationHistory.vb   # Board feet history viewer
│
├── Partials/                       # FrmMain partial class files
│   ├── FrmMain.Boardfoot.vb       # Board feet calculator logic
│   ├── FrmMain.Drawers.vb         # Drawer calculator logic
│   ├── FrmMain.Doors.vb           # Door calculator logic
│   ├── FrmMain.Epoxy.vb           # Epoxy calculator logic
│   ├── FrmMain.Joinery.vb         # Joinery calculator logic
│   ├── FrmMain.WoodMovement.vb    # Wood movement calculator logic
│   ├── FrmMain.ShelfSag.vb        # Shelf sag calculator logic
│   ├── FrmMain.CutList.vb         # Cut list optimizer logic
│   ├── FrmMain.WoodProperties.vb  # Wood properties reference logic
│   ├── FrmMain.JoineryReference.vb # Joinery reference logic
│   ├── FrmMain.HardwareReference.vb # Hardware reference logic
│   ├── FrmMain.Help.vb            # Help system logic
│   └── FrmMain.About.vb           # About tab and logging
│
├── Modules/                        # Business logic modules
│   ├── Calculators/               # Calculator implementations
│   │   ├── BoardFeetCalculator.vb
│   │   ├── DrawerCalculator.vb
│   │   ├── DoorCalculator.vb
│   │   ├── EpoxyCalculator.vb
│   │   ├── JoineryCalculator.vb
│   │   ├── WoodMovementCalculator.vb
│   │   ├── ShelfSagCalculator.vb
│   │   └── CutListOptimizer.vb
│   │
│   ├── Database/                  # Database layer
│   │   ├── DatabaseManager.vb     # SQLite database operations
│   │   ├── DataMigration.vb       # Data seeding and migration
│   │   └── CalculationHistoryModels.vb
│   │
│   ├── References/                # Reference data models
│   │   ├── WoodPropertiesDatabase.vb
│   │   ├── WoodPropertiesModels.vb
│   │   ├── JoineryModels.vb
│   │   └── HardwareModels.vb
│   │
│   ├── Utils/                     # Utility classes
│   │   ├── ErrorHandler.vb        # Logging and error management
│   │   ├── ThemeManager.vb        # Dark/Light theme switching
│   │   ├── ValidationManager.vb   # Input validation
│   │   └── ScaleManager.vb        # Imperial/Metric scale management
│   │
│   └── Help/                      # Help system
│       └── HelpContentManager.vb  # Help content rendering
│
├── Resources/                      # Embedded resources
│   ├── Help/                      # User-facing help markdown files
│   │   ├── GettingStarted.md
│   │   ├── DrawerCalculator.md
│   │   ├── ShelfSag.md
│   │   ├── WoodMovement.md
│   │   └── version.md
│   │
│   └── Icons/                     # Application icons and images
│
├── Docs/                          # Development documentation
│   ├── README.md                  # Documentation index
│   └── [Phase and feature docs]  # Implementation guides
│
├── FrmMain.vb                     # Main form
├── FrmMain.Designer.vb            # Main form designer code
├── Globals.vb                     # Global constants and enums
└── My Project/                    # Project settings and resources

```

---

## 🎨 Features in Detail

### **Calculator Suite**

#### **1. Drawer Height Calculator**
Calculate aesthetically pleasing drawer height progressions using mathematical sequences:

- **Hambridge Ratio** - Classical proportions based on mathematical harmony
- **Golden Ratio** - φ (phi) = 1.618 for natural beauty
- **Fibonacci Sequence** - Nature's pattern (1, 1, 2, 3, 5, 8...)
- **Geometric Progression** - Fixed multiplier (e.g., each 1.2× previous)
- **Arithmetic Progression** - Fixed increment (e.g., +0.5" each)
- **Logarithmic/Exponential** - Advanced mathematical curves
- **Reverse Sequences** - Larger drawers at top
- **Uniform** - All equal heights
- **Custom Ratio** - Define your own sequence

**Output:** Grid of drawer heights, total height, material needed, visual representation

---

#### **2. Cabinet Door Calculator**
Design precision cabinet doors with accurate component sizing:

- **Inset Doors** - Fit inside cabinet opening with gap allowance
- **Overlay Doors** - Cover face frame with overlay amount
- **Single or Double Doors** - Automatic width division for pairs
- **Panel Sizing** - Accounts for groove depth and expansion gaps
- **Material Breakdown** - Rail lengths, stile lengths, panel dimensions

**Output:** Complete cutting list for all door components

---

#### **3. Board Feet Calculator**
Estimate lumber requirements and costs:

- **Grid-based entry** - Multiple board sizes in one calculation
- **Automatic totals** - Calculates total board feet
- **Waste factors** - Add 10%, 15%, or 20% waste
- **Cost estimation** - Total project cost
- **Project save/load** - Store calculations for future reference
- **History tracking** - Review past calculations

**Formula:** `Board Feet = (Thickness × Width × Length) / 144`

---

#### **4. Epoxy Pour Volume Calculator**
Never run out (or waste) expensive epoxy again:

- **Rectangular Pours** - Length × Width × Depth
- **Circular Pours** - Diameter-based calculations
- **Custom Area** - Enter your own square footage
- **Multiple units** - Results in oz, gal, qt, pt, L, mL
- **Waste factors** - 0%, 10%, 15%, 20%
- **Cost estimation** - Track epoxy expenses

**Bonus Feature:** Stone Coat top coat calculator with water ratios

---

#### **5. Joinery Calculator**
Design traditional woodworking joints with precision:

**Mortise & Tenon:**
- Tenon thickness = 1/3 stock thickness
- Tenon length = 1"-2" (typical)
- Mortise depth = tenon length + 1/16"
- Shoulder offset for balance

**Dovetails:**
- Automatic angle calculation (1:6 softwood, 1:8 hardwood)
- Pin and tail width calculations
- Half-pin sizing for corners
- Optimal spacing for board width

**Box Joints:**
- Pin width = stock thickness
- Automatic pin count calculation
- Perfect for router jig setup

**Dados:**
- Depth = 1/3 to 1/2 stock thickness
- Width = shelf thickness + clearance

**Output:** Precise dimensions, visual diagrams, cutting instructions

---

#### **6. Wood Movement Calculator**
The #1 cause of failed projects is ignoring wood movement. This calculator prevents that:

- **50+ wood species** with accurate movement data
- **Humidity-based calculations** - From/To moisture content
- **Grain direction** - Tangential (flat sawn) vs Radial (quarter sawn)
- **Panel gap recommendations** - Min/Max gaps for frame & panel doors
- **Humidity presets** - Indoor Winter, Indoor Summer, Shop Storage, Kiln Dried

**Critical for:**
- Frame and panel doors
- Table tops
- Wide glue-ups
- Breadboard ends
- Any cross-grain construction

**Formula:** Accounts for tangential/radial shrinkage coefficients and humidity differential

---

#### **7. Shelf Sag Calculator**
Design shelves that won't sag:

- **14 material types** - Plywood (various), MDF, Particleboard, Hardwoods, Softwoods, Bamboo
- **Deflection calculations** - Industry standard: max 1/360 of span
- **Load capacity** - Safe load vs maximum load
- **Support types** - Bracket, Dado, Pin support
- **Edge stiffeners** - Front/back stiffeners significantly reduce sag
- **Safety analysis** - Pass/Fail based on deflection limits
- **Visual diagram** - See your shelf under load

**Example:** A 36" oak shelf, 3/4" thick, 10" wide can safely hold ~50 lbs without exceeding sag limits.

---

#### **8. Cut List Optimizer**
Minimize waste and save money:

- **Visual cutting diagrams** - See exactly where to cut
- **Multiple board optimization** - Arrange pieces across several boards
- **Kerf accounting** - Accounts for saw blade width
- **Standard sheet sizes** - 4×8, 4×4, 4×10, or custom
- **Efficiency metrics** - Waste %, efficiency %, boards needed, total cost
- **Navigation** - Browse through each board's cutting pattern
- **Export diagrams** - Print for shop use

**Algorithm:** Best-fit rectangle packing with waste minimization

---

### **Reference System**

#### **Wood Properties Database**
Comprehensive reference for 25+ wood species:

**Data Included:**
- Common name and scientific name
- Wood type (Hardwood/Softwood)
- Janka hardness rating (lbf)
- Specific gravity
- Density (lb/ft³)
- Moisture content (%)
- Shrinkage coefficients (radial and tangential)
- Typical uses and applications
- Workability characteristics
- Cautions and safety notes
- Special notes

**Features:**
- Search by name
- Filter by wood type
- Sort by any column
- Compare multiple species
- Export to CSV
- Print data sheets
- **Add custom species** - Expand database with your own data

---

#### **Joinery Reference**
Learn about 12 classic and modern joints:

**Joint Types:**

| Joint | Category | Strength | Difficulty |
|-------|----------|----------|------------|
| Mortise & Tenon | Frame | ⭐⭐⭐⭐⭐ | Intermediate |
| Dovetail (Through) | Box | ⭐⭐⭐⭐⭐ | Advanced |
| Dovetail (Half-Blind) | Box | ⭐⭐⭐⭐ | Advanced |
| Box Joint | Box | ⭐⭐⭐⭐ | Beginner |
| Dado | Carcass | ⭐⭐⭐ | Beginner |
| Rabbet | Edge | ⭐⭐ | Beginner |
| Lap Joint | Frame | ⭐⭐⭐ | Beginner |
| Bridle Joint | Frame | ⭐⭐⭐⭐ | Intermediate |
| Biscuit Joint | Edge | ⭐⭐⭐ | Beginner |
| Dowel Joint | Frame | ⭐⭐⭐ | Intermediate |
| Pocket Hole | Modern | ⭐⭐⭐ | Beginner |
| Spline Joint | Edge | ⭐⭐⭐ | Intermediate |

**Each entry includes:**
- Detailed description and history
- Required tools
- Typical applications
- Strength characteristics
- Glue requirements
- Reinforcement options

---

#### **Hardware Standards Reference**
Specifications for 16 common hardware items:

**Categories:**
- **Hinges** - Euro hinges (35mm), butt hinges, overlay hinges
- **Drawer Slides** - Ball-bearing, undermount soft-close
- **Shelf Support** - 5mm pins, 1/4" pins
- **Brackets** - Corner braces, table leg brackets
- **Fasteners** - Wood screws, Confirmat screws
- **Pulls & Knobs** - Bar pulls (3" c-c), knobs (1.25" dia)
- **Legs** - Tapered table legs (29")
- **Casters** - Swivel casters (3" wheel)

**Data Included:**
- Brand recommendations (Blum, Accuride, Grass, etc.)
- Part numbers (when applicable)
- Precise dimensions
- Mounting requirements
- Weight capacity
- Installation notes and tips

---

### **Application Features**

#### **Theme Support**
- **Light Theme** - Traditional appearance, best for bright environments
- **Dark Theme** - Reduces eye strain in dim lighting, modern aesthetic
- **Persistent preference** - Your choice is saved

#### **Scale System**
- **Imperial** - Inches, feet, pounds (default for North America)
- **Metric** - Millimeters, meters, kilograms
- **Toggle easily** - Click status bar indicator
- **Persistent preference** - Auto-loads your preference

#### **User Preferences (Phase 5)**
All preferences saved to database:
- Theme selection (Dark/Light)
- Scale selection (Imperial/Metric)
- Last active tab
- Window size and state (Normal/Maximized)
- Calculator defaults (waste %, kerf width)

#### **Help System**
- **Searchable help** - Find topics quickly
- **Context-sensitive** - Help for each calculator
- **Markdown-based** - Clean, readable formatting
- **Embedded in app** - No internet required

#### **Error Logging**
- **Automatic logging** - All errors logged to files
- **Log viewer** - Built-in log browser on About tab
- **Log rotation** - Old logs automatically cleaned up
- **Diagnostic support** - Easy troubleshooting

---

## 🗃️ Database Schema

### **SQLite Database Location**
```
%APPDATA%\Woodworkers Friend\WoodworkersFriend.db
```

### **Tables**

- **WoodSpecies** - Wood properties and movement data
- **UserPreferences** - Application settings
- **CalculationHistory** - Saved calculations (board feet, etc.)
- **HelpContent** - Searchable help system content
- **JoineryTypes** - Joinery reference data (Phase 7.1)
- **HardwareStandards** - Hardware reference data (Phase 7.2)

### **Data Migration**
First run automatically:
1. Creates database schema
2. Seeds wood species data (25+ species)
3. Seeds joinery types (12 types)
4. Seeds hardware standards (16 items)
5. Seeds help content (14 topics)
6. Creates default user preferences

---

## 👨‍💻 Development

### **Prerequisites**

- Visual Studio 2022 or later
- .NET 10.0 SDK
- Windows 10 SDK (10.0.22000.0 or later)

### **Development Setup**

1. Clone the repository
2. Open `Woodworkers Friend.sln` in Visual Studio
3. Restore NuGet packages
4. Build solution (F6)
5. Run application (F5)

### **Code Style**

- **VB.NET Conventions** - Follow VB.NET naming conventions
- **Partial Classes** - Feature-specific logic in separate files
- **XML Comments** - Document public APIs
- **Error Handling** - All exceptions logged via ErrorHandler
- **Null Checking** - `ArgumentNullException.ThrowIfNull()` for parameters

### **Adding New Features**

**To add a new calculator:**
1. Create partial class file: `FrmMain.[Feature].vb`
2. Add tab page in Designer
3. Implement calculator logic in module
4. Wire up events in partial class
5. Add help content to database
6. Update this README

---

## 📦 Release Checklist

- [ ] Update version number in `AssemblyInfo.vb`
- [ ] Build in Release configuration
- [ ] Test all calculators
- [ ] Verify database migration
- [ ] Test on clean Windows installation
- [ ] Update help content if needed
- [ ] Create installer package
- [ ] Tag release in Git
- [ ] Update CHANGELOG.md
- [ ] Publish to GitHub Releases

---

## 🤝 Contributing

Contributions are welcome! Please follow these guidelines:

### **Bug Reports**
1. Check existing issues first
2. Provide detailed description
3. Include steps to reproduce
4. Attach error logs if applicable
5. Specify Windows version and .NET version

### **Feature Requests**
1. Describe the feature clearly
2. Explain the use case
3. Provide examples if possible
4. Discuss impact on existing features

### **Pull Requests**
1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Follow existing code style and patterns
4. Add XML comments to public APIs
5. Test thoroughly
6. Update documentation
7. Commit changes (`git commit -m 'Add amazing feature'`)
8. Push to branch (`git push origin feature/amazing-feature`)
9. Open a Pull Request

---

## 📄 License

This project is licensed under the **MIT License** - see the [LICENSE](LICENSE) file for details.

---

## 🙏 Acknowledgments

### **Woodworking Formulas & Standards**
- Drawer progression formulas based on classical design principles
- Wood movement data compiled from Forest Products Laboratory publications
- Shelf sag calculations based on engineering beam deflection formulas
- Joinery standards from traditional woodworking texts

### **Technologies**
- .NET Framework and Windows Forms - Microsoft
- SQLite - D. Richard Hipp
- System.Data.SQLite - SQLite Development Team

### **Inspiration**
Built with passion for woodworking and software craftsmanship. This tool combines years of woodworking experience with modern software engineering.

---

## 📧 Contact

**Developer:** Dan Maidon  
**GitHub:** [@dmaidon](https://github.com/dmaidon)  
**Repository:** [Woodworkers-Friend](https://github.com/dmaidon/Woodworkers-Friend)  
**Issues:** [GitHub Issues](https://github.com/dmaidon/Woodworkers-Friend/issues)

---

## 🚧 Roadmap

### **Planned Features**

- [ ] **Cloud Sync** - Sync projects across devices
- [ ] **Mobile Companion App** - iOS/Android calculator subset
- [ ] **3D Visualizations** - Render cabinet and furniture designs
- [ ] **Material Database** - Expand to include plywood, veneers, finishes
- [ ] **Project Templates** - Complete project plans (workbench, bookshelf, etc.)
- [ ] **Cut List Integration** - Link to major lumber suppliers
- [ ] **Advanced Analytics** - Track material usage over time
- [ ] **Multi-language Support** - Internationalization
- [ ] **Plugin System** - Community-contributed calculators

### **Recent Updates**

#### **Version 1.0 (Current)**
- ✅ Complete calculator suite (8 calculators)
- ✅ SQLite database migration
- ✅ User preferences persistence
- ✅ Dark/Light theme support
- ✅ Wood properties reference (25 species)
- ✅ Joinery reference (12 joint types)
- ✅ Hardware reference (16 items)
- ✅ Comprehensive help system
- ✅ Error logging system
- ✅ Export capabilities

---

## 📊 Statistics

- **25+ Wood Species** - Comprehensive movement and property data
- **12 Joint Types** - Traditional to modern joinery
- **16 Hardware Items** - Common cabinet and furniture hardware
- **10 Drawer Methods** - Mathematical progressions for aesthetic design
- **14 Shelf Materials** - Engineered and solid wood options
- **50+ Help Topics** - Searchable in-app documentation

---

## 🎓 Educational Value

**Woodworker's Friend** is not just a calculator—it's a learning tool:

- 📖 **Learn joinery** - Understand joint strength and appropriate uses
- 📖 **Master wood movement** - Prevent common project failures
- 📖 **Optimize materials** - Reduce waste, save money
- 📖 **Discover species** - Explore properties of different woods
- 📖 **Industry standards** - Hardware specs and mounting requirements

---

## ⚠️ Disclaimer

**Woodworker's Friend** provides calculations based on industry-standard formulas and best practices. However:

- ✋ Always verify structural calculations with local building codes
- ✋ Test joints and techniques on scrap wood first
- ✋ Use appropriate safety equipment
- ✋ The calculator is a tool—your experience and judgment are essential
- ✋ Author assumes no liability for project outcomes

**Safety first, accuracy second, craftsmanship always!** 🪚

---

## 🌟 Star History

If you find **Woodworker's Friend** useful, please consider giving it a star ⭐ on GitHub!

---

## 📝 Changelog

See [CHANGELOG.md](CHANGELOG.md) for a detailed history of changes.

---

**Made with ❤️ by woodworkers, for woodworkers.**

**Happy woodworking!** 🪵🔨🪚

---

*Last Updated: January 30, 2026*
