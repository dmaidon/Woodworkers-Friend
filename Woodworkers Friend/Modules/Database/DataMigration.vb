' ============================================================================
' Last Updated: January 30, 2026
' Changes: Phase 5 - Added SeedDefaultPreferences() for user preferences persistence
'          Phase 4 - Added MigrateHelpContent() for help system database migration
' ============================================================================

Imports System.Data.SQLite

''' <summary>
''' Handles migration of data from in-code databases to SQLite
''' </summary>
Public Class DataMigration

    ''' <summary>
    ''' Migrates all wood species from WoodPropertiesDatabase to SQLite
    ''' </summary>
    Public Shared Function MigrateWoodSpecies() As Boolean
        Try
            ErrorHandler.LogError(New Exception("Starting wood species migration..."), "MigrateWoodSpecies")

            ' Get all species from in-code database
#Disable Warning BC40000
            Dim allSpecies = WoodPropertiesDatabase.GetWoodSpeciesList()
#Enable Warning BC40000

            ErrorHandler.LogError(New Exception($"Found {allSpecies.Count} species to migrate"), "MigrateWoodSpecies")

            Dim successCount = 0
            Dim failCount = 0

            ' Get database connection
            Using conn As New SQLiteConnection($"Data Source={DatabaseManager.Instance.DatabasePath};Version=3;")
                conn.Open()

                Using transaction = conn.BeginTransaction()
                    Try
                        For Each species In allSpecies
                            Try
                                Using cmd As New SQLiteCommand("
                                    INSERT OR REPLACE INTO WoodSpecies (
                                        CommonName, ScientificName, WoodType,
                                        JankaHardness, SpecificGravity, Density, MoistureContent,
                                        ShrinkageRadial, ShrinkageTangential,
                                        TypicalUses, Workability, Cautions, Notes,
                                        IsUserAdded
                                    ) VALUES (
                                        @CommonName, @ScientificName, @WoodType,
                                        @JankaHardness, @SpecificGravity, @Density, @MoistureContent,
                                        @ShrinkageRadial, @ShrinkageTangential,
                                        @TypicalUses, @Workability, @Cautions, @Notes,
                                        0
                                    )", conn, transaction)

                                    cmd.Parameters.AddWithValue("@CommonName", species.CommonName)
                                    cmd.Parameters.AddWithValue("@ScientificName", If(String.IsNullOrEmpty(species.ScientificName), CObj(DBNull.Value), CObj(species.ScientificName)))
                                    cmd.Parameters.AddWithValue("@WoodType", species.WoodType)
                                    cmd.Parameters.AddWithValue("@JankaHardness", species.JankaHardness)
                                    cmd.Parameters.AddWithValue("@SpecificGravity", species.SpecificGravity)
                                    cmd.Parameters.AddWithValue("@Density", species.Density)
                                    cmd.Parameters.AddWithValue("@MoistureContent", species.MoistureContent)
                                    cmd.Parameters.AddWithValue("@ShrinkageRadial", species.ShrinkageRadial)
                                    cmd.Parameters.AddWithValue("@ShrinkageTangential", species.ShrinkageTangential)
                                    cmd.Parameters.AddWithValue("@TypicalUses", If(String.IsNullOrEmpty(species.TypicalUses), CObj(DBNull.Value), CObj(species.TypicalUses)))
                                    cmd.Parameters.AddWithValue("@Workability", If(String.IsNullOrEmpty(species.Workability), CObj(DBNull.Value), CObj(species.Workability)))
                                    cmd.Parameters.AddWithValue("@Cautions", If(String.IsNullOrEmpty(species.Cautions), CObj(DBNull.Value), CObj(species.Cautions)))
                                    cmd.Parameters.AddWithValue("@Notes", If(String.IsNullOrEmpty(species.Notes), CObj(DBNull.Value), CObj(species.Notes)))

                                    cmd.ExecuteNonQuery()
                                    successCount += 1
                                End Using
                            Catch ex As Exception
                                failCount += 1
                                ErrorHandler.LogError(ex, $"MigrateWoodSpecies - Failed to migrate: {species.CommonName}")
                            End Try
                        Next

                        transaction.Commit()

                        ErrorHandler.LogError(
                            New Exception($"Migration complete: {successCount} succeeded, {failCount} failed"),
                            "MigrateWoodSpecies")

                        Return failCount = 0
                    Catch ex As Exception
                        transaction.Rollback()
                        ErrorHandler.LogError(ex, "MigrateWoodSpecies - Transaction failed")
                        Return False
                    End Try
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "MigrateWoodSpecies")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Checks if wood species data has been migrated
    ''' </summary>
    Public Shared Function IsWoodSpeciesMigrated() As Boolean
        Try
            Using conn As New SQLiteConnection($"Data Source={DatabaseManager.Instance.DatabasePath};Version=3;")
                conn.Open()
                Using cmd As New SQLiteCommand("SELECT COUNT(*) FROM WoodSpecies", conn)
                    Dim count = Convert.ToInt32(cmd.ExecuteScalar())
                    Return count > 0
                End Using
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "IsWoodSpeciesMigrated")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Performs initial data migration on first run
    ''' </summary>
    Public Shared Sub PerformInitialMigration()
        Try
            ' Check if wood species migration is needed
            If Not IsWoodSpeciesMigrated() Then
                ErrorHandler.LogError(New Exception("First run detected - starting initial data migration"), "PerformInitialMigration")

                ' Migrate wood species
                If MigrateWoodSpecies() Then
                    ErrorHandler.LogError(New Exception("Wood species migration completed successfully"), "PerformInitialMigration")
                Else
                    ErrorHandler.LogError(New Exception("Wood species migration completed with errors"), "PerformInitialMigration")
                End If
            End If

            ' Check if help content migration is needed
            If Not DatabaseManager.Instance.IsHelpContentSeeded() Then
                ErrorHandler.LogError(New Exception("Help content not found - seeding help database"), "PerformInitialMigration")
                Dim helpCount = MigrateHelpContent()
                ErrorHandler.LogError(New Exception($"Help content seeded: {helpCount} topics"), "PerformInitialMigration")
            Else
                ' Check for missing Definitions topic (added in later version)
                AddMissingHelpTopics()
            End If

            ' Phase 5: Seed default user preferences if not yet set
            If Not DatabaseManager.Instance.HasPreferences() Then
                ErrorHandler.LogError(New Exception("No user preferences found - seeding defaults"), "PerformInitialMigration")
                SeedDefaultPreferences()
            End If

            ' Phase 7.1: Migrate joinery types reference data
            Dim joineryCount = MigrateJoineryTypes()
            If joineryCount > 0 Then
                ErrorHandler.LogError(New Exception($"Joinery types seeded: {joineryCount} types"), "PerformInitialMigration")
            End If

            ' Phase 7.2: Migrate hardware standards reference data
            Dim hardwareCount = MigrateHardwareStandards()
            If hardwareCount > 0 Then
                ErrorHandler.LogError(New Exception($"Hardware standards seeded: {hardwareCount} items"), "PerformInitialMigration")
            End If

            ' Phase 7.3: Migrate cost data from CSV files
            If Not IsWoodCostsMigrated() Then
                ErrorHandler.LogError(New Exception("Wood costs not found - migrating from CSV"), "PerformInitialMigration")
                Dim woodCostCount = MigrateWoodCosts()
                If woodCostCount > 0 Then
                    ErrorHandler.LogError(New Exception($"Wood costs migrated: {woodCostCount} items"), "PerformInitialMigration")
                End If
            Else
                ' One-time conversion: Convert existing UPPERCASE names to Title Case
                Dim convertedCount = ConvertWoodCostsToTitleCase()
                If convertedCount > 0 Then
                    ErrorHandler.LogError(New Exception($"Converted {convertedCount} wood cost names to Title Case"), "PerformInitialMigration")
                End If
            End If

            If Not IsEpoxyCostsMigrated() Then
                ErrorHandler.LogError(New Exception("Epoxy costs not found - migrating from CSV"), "PerformInitialMigration")
                Dim epoxyCostCount = MigrateEpoxyCosts()
                If epoxyCostCount > 0 Then
                    ErrorHandler.LogError(New Exception($"Epoxy costs migrated: {epoxyCostCount} items"), "PerformInitialMigration")
                End If
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "PerformInitialMigration")
        End Try
    End Sub

    ''' <summary>
    ''' Adds help topics that were added in later versions (for existing databases)
    ''' </summary>
    Private Shared Sub AddMissingHelpTopics()
        Try
            ' Check if Definitions topic exists
            Dim existing = DatabaseManager.Instance.GetHelpContent("definitions")
            If existing Is Nothing Then
                ErrorHandler.LogError(New Exception("Adding missing Definitions help topic"), "AddMissingHelpTopics")
                ' Re-run migration to add new topics
                MigrateHelpContent()
            End If

            ' Check if Miter Angle Calculator help exists (added January 31, 2026)
            Dim miterHelp = DatabaseManager.Instance.GetHelpContent("MiterAngle")
            ErrorHandler.LogError(New Exception($"AddMissingHelpTopics: MiterAngle help check - IsNull={miterHelp Is Nothing}"), "MiterAngle Debug")
            
            If miterHelp Is Nothing Then
                ErrorHandler.LogError(New Exception("Adding Miter Angle Calculator help topic"), "AddMissingHelpTopics")
                AddMiterAngleHelp()
            Else
                ErrorHandler.LogError(New Exception($"Miter Angle help already exists: Title='{miterHelp.Title}', Category='{miterHelp.Category}'"), "MiterAngle Debug")
            End If

            ' Check if Materials & Finishing help exists (added February 1, 2026)
            Dim materialsHelp = DatabaseManager.Instance.GetHelpContent("MaterialsFinishing")
            If materialsHelp Is Nothing Then
                ErrorHandler.LogError(New Exception("Adding Materials & Finishing Calculator help topic"), "AddMissingHelpTopics")
                AddMaterialsFinishingHelp()
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "AddMissingHelpTopics")
        End Try
    End Sub

    ''' <summary>
    ''' Adds Miter Angle Calculator help content to the database
    ''' Added: January 31, 2026
    ''' </summary>
    Private Shared Sub AddMiterAngleHelp()
        Try
            Dim helpContent As New List(Of DatabaseManager.HelpContentData) From {
                New DatabaseManager.HelpContentData With {
                    .ModuleName = "MiterAngle",
                    .Title = "Miter Angle Calculator",
                    .Category = "Calculators",
                    .SortOrder = 140,
                    .Version = "1.0",
                    .Keywords = "miter,angle,bevel,compound,crown,molding,frame,picture,polygon,hexagon,octagon,cut,saw",
                    .Content = "
# Miter Angle Calculator

## Overview
The Miter Angle Calculator helps you determine the precise miter and bevel angles needed for perfect joints in polygonal projects. Whether you're building picture frames, installing crown molding, or creating hexagonal tables, this calculator ensures accurate cuts every time.

## When to Use This Calculator
- **Picture frames** (4-sided, 45° miters)
- **Polygon furniture** (hexagon tables, octagon mirrors)
- **Crown molding** (compound angle cuts)
- **Decorative trim** (cove molding, corner trim)
- **Box lids** (any polygon shape)
- **Segmented turning** (bowls, vases with multiple sides)

## Input Fields

### Number of Sides
Enter the number of sides for your polygon (3-24 sides supported).

**Common polygons:**
- 3 sides = Triangle
- 4 sides = Square/Rectangle
- 5 sides = Pentagon
- 6 sides = Hexagon
- 8 sides = Octagon
- 12 sides = Dodecagon

### Frame Type

#### Flat Frame
Select this for standard flat projects where pieces meet at 90° to the surface:
- Picture frames
- Table tops
- Box lids
- Mirror frames

**Result:** Only miter angle is calculated (bevel = 0°)

#### Tilted Frame
Select this for projects where pieces are installed at an angle:
- Crown molding
- Cove molding
- Angled trim

**Result:** Both miter AND bevel angles are calculated (compound cut)

### Tilt Angle (Tilted Frames Only)
For tilted frames, enter the spring angle of your molding:
- **38°** - Standard crown molding (52/38 spring angle)
- **45°** - Common for 45/45 crown molding
- **Custom** - Any angle from 0° to 90°

**What is spring angle?**
The spring angle is the angle the molding makes with the wall when properly installed. It's marked on most crown molding packaging.

### Material Thickness (Optional)
Enter the thickness of your material for reference. This doesn't affect the angle calculations but is useful for project documentation.

## Understanding the Results

### Miter (Saw) Angle
**This is your primary cutting angle.**
- Set your miter saw to this angle
- For 4-sided frames: typically 45°
- For 6-sided frames: typically 30°
- For 8-sided frames: typically 22.5°

**Formula:** Miter Angle = (180° - Interior Angle) / 2

### Bevel Angle (Tilted Frames Only)
**This is your blade tilt angle for compound cuts.**
- Set your saw blade to tilt this many degrees
- Only applies to tilted frames (crown molding, etc.)
- For flat frames, bevel = 0° (no tilt needed)

**Formula:** Uses compound miter trigonometry based on tilt angle

### Complementary Angle
The complementary angle (90° - Miter Angle) is provided for reference and verification.

### Interior Angle
The interior angle at each vertex of the polygon. Useful for layout and design verification.

## Step-by-Step: Picture Frame

**Example: 16"" x 20"" rectangular picture frame**

1. Enter **4** sides (rectangle)
2. Select **Flat Frame**
3. Click Calculate

**Results:**
- Miter Angle: 45°
- Bevel Angle: 0° (flat frame)
- Set your miter saw to 45° and make straight cuts

## Step-by-Step: Crown Molding

**Example: Installing 52/38 crown molding in a rectangular room**

1. Enter **4** sides (rectangle)
2. Select **Tilted Frame**
3. Enter **38°** for tilt angle (52/38 molding)
4. Click Calculate

**Results:**
- Miter Angle: ~31.6°
- Bevel Angle: ~33.9°
- Set miter saw to 31.6° AND tilt blade to 33.9°

**Important:** For crown molding, some woodworkers prefer to lay the molding flat on the saw and use nested cuts. This calculator gives you angles for vertical cuts with the molding standing against the fence.

## Step-by-Step: Hexagon Table

**Example: 24"" diameter hexagonal table top**

1. Enter **6** sides (hexagon)
2. Select **Flat Frame**
3. Click Calculate

**Results:**
- Miter Angle: 30°
- Bevel Angle: 0°
- Interior Angle: 120°
- Cut each board end at 30° for perfect hexagon

## Tips and Tricks

### Accuracy is Critical
- **±0.1°** error can create visible gaps in large projects
- **Test cuts first** - Make test pieces before cutting expensive material
- **Verify saw calibration** - Check your miter saw zero position regularly

### Flat Frame vs Tilted Frame
- **Flat Frame:** Pieces lay flat (picture frames, box lids)
- **Tilted Frame:** Pieces installed at an angle (crown molding)
- **Wrong selection = Wrong angles!**

### Multi-Sided Projects
The more sides you have:
- Smaller miter angles
- Less visible gaps from small errors
- More forgiving overall
- Example: 12-sided clock is more forgiving than 4-sided frame

### Crown Molding Tips
1. **Know your spring angle** - Check the molding package
2. **Most common:** 52/38 (38° spring angle) or 45/45 (45° spring angle)
3. **Cutting options:**
   - **Compound cut (this calculator):** Molding vertical against fence
   - **Nested cut:** Molding flat on saw bed at spring angle
4. **Test first:** Crown molding is expensive!

### Verification
- Interior Angle × Number of Sides should = (Sides - 2) × 180°
- Example: Hexagon (6 sides): 120° × 6 = 720° = (6-2) × 180° ✓

## Common Angles Quick Reference

| Sides | Shape | Miter Angle (Flat) | Each Interior Angle |
|-------|-------|-------------------|---------------------|
| 3 | Triangle | 60° | 60° |
| 4 | Square | 45° | 90° |
| 5 | Pentagon | 36° | 108° |
| 6 | Hexagon | 30° | 120° |
| 8 | Octagon | 22.5° | 135° |
| 10 | Decagon | 18° | 144° |
| 12 | Dodecagon | 15° | 150° |

## Troubleshooting

### Gaps at Joints
- Check miter saw calibration
- Verify you entered correct number of sides
- Ensure blade is sharp and not deflecting
- Material may not be perfectly straight

### Crown Molding Doesn't Fit
- Verify spring angle is correct (check package)
- Confirm frame type is set to ""Tilted""
- Room corners may not be perfectly 90°
- Consider scribing technique for out-of-square corners

### Compound Cuts Not Matching
- Both miter AND bevel must be set correctly
- Some saws require complementary angles (90° - calculated angle)
- Consult your saw manual for angle direction

## Formula Reference

### Flat Frame (Simple Miter)
```
Interior Angle = (n - 2) × 180° / n
Miter Angle = (180° - Interior Angle) / 2
```

### Tilted Frame (Compound Miter)
```
Compound Miter = arctan(cos(tilt) × tan(simple_miter))
Bevel Angle = arcsin(sin(tilt) × sin(simple_miter))
```

Where:
- n = number of sides
- tilt = spring angle in radians
- simple_miter = flat frame miter angle in radians

## Safety Notes
⚠️ **Always wear safety glasses when using power tools**
⚠️ **Test cuts on scrap material first**
⚠️ **Keep hands clear of blade path**
⚠️ **Use proper push sticks for small pieces**
⚠️ **Ensure workpiece is firmly secured**

## Related Calculators
- **Polygon Calculator** - Calculate polygon dimensions
- **Dado Stack Calculator** - For groove cuts
- **Table Tipping Force** - Furniture safety calculations

## Additional Resources
- Crown molding installation guides
- Compound miter cut tables
- Picture frame building tutorials

---

**Need more help?** Contact support or visit the documentation wiki.
"
                }
            }

            Dim count = DatabaseManager.Instance.BulkInsertHelpContent(helpContent)
            ErrorHandler.LogError(New Exception($"Miter Angle Calculator help added: {count} topic"), "AddMiterAngleHelp")
        Catch ex As Exception
            ErrorHandler.LogError(ex, "AddMiterAngleHelp")
        End Try
    End Sub

    ''' <summary>
    ''' Adds Materials and Finishing Calculator help content to the database
    ''' Added: February 1, 2026
    ''' </summary>
    Private Shared Sub AddMaterialsFinishingHelp()
        Try
            Dim helpContent As New List(Of DatabaseManager.HelpContentData) From {
                New DatabaseManager.HelpContentData With {
                    .ModuleName = "MaterialsFinishing",
                    .Title = "Materials and Finishing Calculators",
                    .Category = "Calculators",
                    .SortOrder = 145,
                    .Version = "1.0",
                    .Keywords = "veneer,inlay,finish,finishing,stain,polyurethane,lacquer,oil,wax,shellac,varnish,glue,adhesive,titebond,epoxy,coverage,coat,dry,clamp",
                    .Content = "
# Materials and Finishing Calculators

## Overview
The Materials and Finishing tab provides three integrated calculators that share a common area input, making it easy to plan all your material needs for a project at once.

## Shared Area Input
Enter your project dimensions once, and all three calculators use the same area.

### Two Input Methods
**Method 1: Length x Width**
- Enter length and width separately
- Calculator computes the area automatically

**Method 2: Known Area**
- If you already know the total area, enter it directly
- Useful when you have irregular shapes or multiple surfaces

### Unit Support
- **Imperial:** Inches, Feet
- **Metric:** Millimeters, Centimeters
- All results display in appropriate units for your selection

---

## Veneer and Inlay Calculator

### Purpose
Calculate how many veneer sheets you need for your project, accounting for pattern matching waste.

### Pattern Types and Waste Factors
| Pattern | Waste % | Description |
|---------|---------|-------------|
| Book Match | 20% | Mirror image pairs, requires careful matching |
| Slip Match | 15% | Repeating pattern, moderate waste |
| Random | 10% | No matching needed, minimal waste |
| Radial | 25% | Sunburst patterns, high waste |
| Diamond | 30% | Complex matching, highest waste |

### Inputs
- **Sheet Length:** Standard veneer sheet length (default 96 inches)
- **Sheet Width:** Standard veneer sheet width (default 48 inches)
- **Pattern Type:** Automatically adjusts waste factor
- **Waste %:** Can be manually adjusted

### Results
- **Sheets Needed:** Total veneer sheets to purchase
- **Total Area with Waste:** Actual coverage including waste allowance

---

## Finishing Materials Calculator

### Purpose
Calculate how much finish you need and estimate the total finishing time.

### Finish Types
| Finish | Coverage | Dry Time | Cost/Qt | Tips |
|--------|----------|----------|---------|------|
| Stain | 200 sq ft/gal | 4 hrs | $12 | Wipe off excess, test on scrap |
| Polyurethane | 125 sq ft/qt | 6 hrs | $16 | Thin first coat 10%, sand with 320 |
| Lacquer | 150 sq ft/qt | 30 min | $20 | Thin coats, good ventilation! |
| Danish Oil | 200 sq ft/qt | 8 hrs | $14 | Wipe on, wait 15 min, wipe off |
| Tung Oil | 150 sq ft/qt | 24 hrs | $18 | Pure tung takes 5-7 coats |
| Wax | 300 sq ft/lb | 15 min | $10 | Apply thin, buff when dry |
| Shellac | 175 sq ft/qt | 2 hrs | $15 | Dewaxed for topcoat compatibility |
| Varnish | 100 sq ft/qt | 8 hrs | $18 | Thin coats, best for exterior |

### Inputs
- **Finish Type:** Select from dropdown (auto-fills coverage and dry time)
- **Coverage:** Can be manually adjusted
- **Number of Coats:** 1-10 coats
- **Dry Time:** Hours between coats
- **Sand Between Coats:** Adds sanding time to total

### Results
- **Quantity Needed:** In oz/quarts/gallons (or ml/liters for metric)
- **Total Time:** Including application and drying time
- **Estimated Cost:** Based on typical prices

---

## Glue Coverage Calculator

### Purpose
Calculate how much glue you need and understand working times.

### Glue Types
| Glue | Open Time | Clamp Time | Best For |
|------|-----------|------------|----------|
| PVA (White) | 10 min | 1 hr | Interior, general purpose |
| Yellow (Titebond) | 8 min | 45 min | Furniture, stronger than wood |
| Titebond III | 10 min | 1 hr | Waterproof, cutting boards |
| Polyurethane | 15 min | 4 hrs | Gap-filling, waterproof |
| Epoxy | 30 min | 24 hrs | Structural, gap-filling |
| Hide Glue | 5 min | 2 hrs | Traditional, reversible |
| CA (Super Glue) | 1 min | Instant | Quick repairs, turnings |

### Joint Type Multipliers
Different joints need different amounts of glue:
- **Edge-to-Edge (1.0x):** Standard panel glue-ups
- **Face-to-Face (1.0x):** Laminations, veneering
- **End Grain (2.0x):** Requires double coverage (pre-seal recommended)
- **Mortise and Tenon (1.5x):** Multiple surfaces to coat
- **Dovetail (1.3x):** Complex mating surfaces
- **Biscuit/Domino (0.8x):** Less exposed surface area

### Application Methods
- Brush, Roller, Squeeze Bottle, Spreader

### Results
- **Amount Needed:** In oz and ml
- **Open Time:** Working time before glue starts to set
- **Clamp Time:** Minimum time to keep clamps on
- **Tips:** Joint and glue-specific advice

---

## Tips for Best Results

### Veneer Work
- Always order 10-30% extra depending on pattern
- Book match requires the most careful planning
- Keep veneer flat and avoid moisture

### Finishing
- Thin first coats for better penetration
- Sand between coats with 320 grit
- Allow full dry time for best results
- Work in a dust-free environment

### Gluing
- Pre-seal end grain before gluing
- Apply glue to both surfaces for best bond
- Alternate clamps top and bottom to prevent bowing
- Remove squeeze-out before it dries completely
"
                }
            }

            Dim count = DatabaseManager.Instance.BulkInsertHelpContent(helpContent)
            ErrorHandler.LogError(New Exception($"Materials and Finishing help added: {count} topic"), "AddMaterialsFinishingHelp")
        Catch ex As Exception
            ErrorHandler.LogError(ex, "AddMaterialsFinishingHelp")
        End Try
    End Sub

    ''' <summary>
    ''' Migrates all help content from hardcoded methods to SQLite database.
    ''' Content uses simple markup: lines prefixed with tags for rendering.
    ''' Tags: #TITLE:, ##SECTION:, ###SUBTITLE:, *BULLET:, #NUM:n:, !WARNING:, ?NOTE:,
    '''        =FORMULA:, ~STEP:n:title:, @METHOD:title:desc:, %COLOR:label:desc:,
    '''        ^SHORTCUT:keys:, &amp;PROBLEM:text:, &amp;SOLUTION:text:, +CATEGORY:
    ''' </summary>
    Public Shared Function MigrateHelpContent() As Integer
        Try
            ' ===== GETTING STARTED =====
            ' ===== INTERFACE =====
            ' ===== DRAWER CALCULATOR =====
            ' ===== DOOR CALCULATOR =====
            ' ===== BOARD FEET =====
            ' ===== EPOXY =====
            ' ===== POLYGON =====
            ' ===== JOINERY =====
            ' ===== WOOD MOVEMENT =====
            ' ===== SHELF SAG =====
            ' ===== CUT LIST =====
            ' ===== UNIT CONVERSIONS =====
            ' ===== FRACTIONS =====
            ' ===== TABLE TIP =====
            ' ===== SHORTCUTS =====
            ' ===== THEMES =====
            ' ===== BEST PRACTICES =====
            ' ===== TROUBLESHOOTING =====
            ' ===== VERSION =====
            ' ===== EXPORT =====
            ' ===== PRESETS =====
            ' ===== VALIDATION =====
            Dim helpItems As New List(Of DatabaseManager.HelpContentData) From {
                New DatabaseManager.HelpContentData With {
.ModuleName = "GettingStarted",
.Title = "Getting Started with Woodworker's Friend",
.Category = "GettingStarted",
.SortOrder = 1,
.Keywords = "start,welcome,overview,introduction,navigation,tips",
.Content =
"#TITLE:Getting Started with Woodworker's Friend" & vbLf &
"##SECTION:Welcome!|Woodworker's Friend is your comprehensive woodworking calculator and planning tool. This application helps you calculate dimensions, materials, and perform conversions for various woodworking projects." & vbLf &
"###SUBTITLE:What Can You Do?" & vbLf &
"*BULLET:Calculate drawer heights using various mathematical progressions" & vbLf &
"*BULLET:Design cabinet doors with precise rail and stile dimensions" & vbLf &
"*BULLET:Calculate board feet for material estimation" & vbLf &
"*BULLET:Determine epoxy pour volumes for river tables and projects" & vbLf &
"*BULLET:Calculate polygon dimensions and angles" & vbLf &
"*BULLET:Design mortise & tenon, dovetail, box, and dado joints" & vbLf &
"*BULLET:Predict wood movement and plan for expansion gaps" & vbLf &
"*BULLET:Optimize cut lists to minimize material waste" & vbLf &
"*BULLET:Convert between imperial and metric units" & vbLf &
"*BULLET:Convert fractions to decimals and vice versa" & vbLf &
"" & vbLf &
"###SUBTITLE:Navigation" & vbLf &
"Use the tabs at the top of the window to access different calculators and tools. Each tab contains related functionality with easy-to-understand input fields." & vbLf &
"" & vbLf &
"?NOTE:Tip: Hover your mouse over input fields to see helpful tooltips with valid ranges and examples!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "interface",
.Title = "Understanding the Interface",
.Category = "GettingStarted",
.SortOrder = 2,
.Keywords = "interface,layout,tabs,status bar,color coding,navigation",
.Content =
"#TITLE:Understanding the Interface" & vbLf &
"###SUBTITLE:Main Window Layout" & vbLf &
"The application uses a tabbed interface with the following main sections:" & vbLf &
"" & vbLf &
"*BULLET:Drawers Tab - Calculate drawer heights and spacing" & vbLf &
"*BULLET:Doors Tab - Design cabinet doors with precise measurements" & vbLf &
"*BULLET:Board Feet Tab - Calculate lumber requirements" & vbLf &
"*BULLET:Calculations Tab - Unit conversions and misc calculations" & vbLf &
"*BULLET:Epoxy Tab - Calculate epoxy pour volumes" & vbLf &
"*BULLET:Conversions Tab - Quick unit conversions" & vbLf &
"*BULLET:Calculators Tab - Polygon and geometric calculations" & vbLf &
"*BULLET:Joinery Tab - Mortise & tenon, dovetails, box joints, dados" & vbLf &
"*BULLET:Wood Movement Tab - Calculate wood expansion/contraction" & vbLf &
"*BULLET:Cut List Tab - Optimize board cutting patterns" & vbLf &
"*BULLET:References Tab - Wood species, joinery types, hardware standards" & vbLf &
"*BULLET:Drawings Tab - Visual representations of calculations" & vbLf &
"*BULLET:Help Tab - This help system" & vbLf &
"" & vbLf &
"###SUBTITLE:Status Bar" & vbLf &
"The status bar at the bottom shows:" & vbLf &
"*BULLET:Application version" & vbLf &
"*BULLET:Copyright information" & vbLf &
"*BULLET:Current theme (click to toggle Dark/Light)" & vbLf &
"*BULLET:Current scale setting"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "DrawerCalculator",
.Title = "Drawer Calculator",
.Category = "Calculators",
.SortOrder = 10,
.Keywords = "drawer,heights,progression,geometric,arithmetic,golden ratio,fibonacci,cabinet",
.Content =
"#TITLE:Drawer Calculator" & vbLf &
"##SECTION:Purpose|Calculate optimal drawer heights for cabinets using various mathematical progressions. Perfect for creating aesthetically pleasing and functional drawer configurations." & vbLf &
"###SUBTITLE:Calculation Methods" & vbLf &
"@METHOD:Geometric|Each drawer is proportionally taller than the previous one" & vbLf &
"@METHOD:Arithmetic|Each drawer increases by a fixed amount" & vbLf &
"@METHOD:Golden Ratio|Uses the golden ratio (1.618) for pleasing proportions" & vbLf &
"@METHOD:Fibonacci|Based on the Fibonacci sequence" & vbLf &
"@METHOD:Uniform|All drawers the same height" & vbLf &
"@METHOD:Custom Ratio|Define your own progression ratio" & vbLf &
"" & vbLf &
"###SUBTITLE:Required Inputs" & vbLf &
"*BULLET:Number of Drawers (1-20)" & vbLf &
"*BULLET:Drawer Width (6-48 inches)" & vbLf &
"*BULLET:Drawer Spacing (0-2 inches)" & vbLf &
"*BULLET:First Drawer Height (method-specific)" & vbLf &
"*BULLET:Multiplier or Increment (method-specific)" & vbLf &
"" & vbLf &
"###SUBTITLE:Presets" & vbLf &
"Quick-start presets are available for common scenarios:" & vbLf &
"*BULLET:Kitchen Standard - Typical kitchen base cabinet" & vbLf &
"*BULLET:Office Desk - Standard desk drawer configuration" & vbLf &
"*BULLET:Bathroom Vanity - Bathroom cabinet dimensions" & vbLf &
"*BULLET:Custom Cabinet - Your saved configurations" & vbLf &
"" & vbLf &
"?NOTE:Tip: Use the 'Draw' button to see a visual representation of your drawer configuration!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "doors",
.Title = "Door Calculator",
.Category = "Calculators",
.SortOrder = 11,
.Keywords = "door,cabinet,rail,stile,panel,inset,overlay,groove",
.Content =
"#TITLE:Door Calculator" & vbLf &
"##SECTION:Purpose|Calculate precise dimensions for cabinet doors including rails, stiles, and panels. Supports both inset and overlay door configurations." & vbLf &
"###SUBTITLE:Door Types" & vbLf &
"@METHOD:Inset Doors|Door fits inside the cabinet opening" & vbLf &
"@METHOD:Overlay Doors|Door overlaps the cabinet face frame" & vbLf &
"" & vbLf &
"###SUBTITLE:Required Inputs" & vbLf &
"*BULLET:Cabinet Opening Height (6-120 inches)" & vbLf &
"*BULLET:Cabinet Opening Width (6-60 inches)" & vbLf &
"*BULLET:Stile Width (0.5-6 inches)" & vbLf &
"*BULLET:Rail Width (0.5-6 inches)" & vbLf &
"*BULLET:Panel Groove Depth (typically 0.25-0.5 inches)" & vbLf &
"*BULLET:Gap Size (for inset doors)" & vbLf &
"*BULLET:Overlay Amount (for overlay doors)" & vbLf &
"" & vbLf &
"###SUBTITLE:Calculated Results" & vbLf &
"*BULLET:Exact door width and height" & vbLf &
"*BULLET:Rail lengths (top and bottom)" & vbLf &
"*BULLET:Stile lengths (left and right)" & vbLf &
"*BULLET:Panel dimensions" & vbLf &
"*BULLET:Material requirements" & vbLf &
"" & vbLf &
"!WARNING:Remember to account for wood movement when sizing panels!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "boardfeet",
.Title = "Board Feet Calculator",
.Category = "Calculators",
.SortOrder = 12,
.Keywords = "board feet,lumber,thickness,width,length,waste,material,estimate",
.Content =
"#TITLE:Board Feet Calculator" & vbLf &
"##SECTION:What is a Board Foot?|A board foot is a unit of measurement for lumber. One board foot equals 144 cubic inches (1"" x 12"" x 12"")." & vbLf &
"###SUBTITLE:Formula" & vbLf &
"=FORMULA:Board Feet = (Thickness x Width x Length) / 144" & vbLf &
"Where all dimensions are in inches" & vbLf &
"" & vbLf &
"###SUBTITLE:Using the Calculator" & vbLf &
"#NUM:1:Enter board thickness in inches (e.g., 0.75 for 3/4"")" & vbLf &
"#NUM:2:Enter board width in inches" & vbLf &
"#NUM:3:Enter board length in inches" & vbLf &
"#NUM:4:Enter quantity needed" & vbLf &
"#NUM:5:Add waste percentage (typically 10-20%)" & vbLf &
"" & vbLf &
"###SUBTITLE:Multiple Boards" & vbLf &
"Use the grid to calculate total board feet for multiple board sizes. The calculator automatically sums all entries and applies waste percentage." & vbLf &
"" & vbLf &
"?NOTE:Tip: Always add 10-20% waste factor for cuts, mistakes, and matching grain!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "epoxy",
.Title = "Epoxy Pour Calculator",
.Category = "Calculators",
.SortOrder = 13,
.Keywords = "epoxy,resin,river table,pour,volume,ounces,gallons,bar top",
.Content =
"#TITLE:Epoxy Pour Calculator" & vbLf &
"##SECTION:Purpose|Calculate the exact amount of epoxy resin needed for river tables, bar tops, and other epoxy projects." & vbLf &
"###SUBTITLE:Calculation Methods" & vbLf &
"@METHOD:Rectangular Pour|For straight-sided projects (Length x Width x Depth)" & vbLf &
"@METHOD:Circular Pour|For round projects (Pi x Radius-squared x Depth)" & vbLf &
"@METHOD:Custom Area|Enter your own calculated area" & vbLf &
"" & vbLf &
"###SUBTITLE:Required Inputs" & vbLf &
"*BULLET:Length (inches) - for rectangular pours" & vbLf &
"*BULLET:Width (inches) - for rectangular pours" & vbLf &
"*BULLET:Diameter (inches) - for circular pours" & vbLf &
"*BULLET:Depth (1/16"" to 6"" typical)" & vbLf &
"*BULLET:Waste percentage (10-20% recommended)" & vbLf &
"" & vbLf &
"###SUBTITLE:Results Provided" & vbLf &
"*BULLET:Total ounces needed" & vbLf &
"*BULLET:Gallons, quarts, and pints" & vbLf &
"*BULLET:Milliliters and liters" & vbLf &
"*BULLET:Estimated cost (if epoxy price is set)" & vbLf &
"" & vbLf &
"!WARNING:Important: Always mix resin in small batches to avoid excessive heat buildup!" & vbLf &
"?NOTE:Tip: For deep pours, consider multiple thin layers instead of one thick pour!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "polygon",
.Title = "Polygon Calculator",
.Category = "Calculators",
.SortOrder = 14,
.Keywords = "polygon,hexagon,octagon,angles,sides,radius,apothem,perimeter,area",
.Content =
"#TITLE:Polygon Calculator" & vbLf &
"##SECTION:Purpose|Calculate dimensions and angles for regular polygons. Useful for hexagonal tables, octagonal windows, and decorative inlays." & vbLf &
"###SUBTITLE:Calculations" & vbLf &
"*BULLET:Side length from radius" & vbLf &
"*BULLET:Interior angles" & vbLf &
"*BULLET:Exterior angles" & vbLf &
"*BULLET:Total perimeter" & vbLf &
"*BULLET:Area" & vbLf &
"*BULLET:Apothem (distance from center to mid-side)" & vbLf &
"" & vbLf &
"###SUBTITLE:Inputs" & vbLf &
"*BULLET:Number of sides (3-25)" & vbLf &
"*BULLET:Radius or side length" & vbLf &
"" & vbLf &
"###SUBTITLE:Common Polygons" & vbLf &
"Triangle (3), Square (4), Pentagon (5), Hexagon (6), Octagon (8)" & vbLf &
"" & vbLf &
"?NOTE:The visual display rotates to show your polygon from all angles!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "joinery",
.Title = "Joinery Calculator",
.Category = "Joinery",
.SortOrder = 20,
.Keywords = "joinery,mortise,tenon,dovetail,box joint,dado,groove,joint",
.Content =
"#TITLE:Joinery Calculator" & vbLf &
"##SECTION:Purpose|Calculate precise dimensions for traditional woodworking joints including mortise & tenon, dovetails, box joints, and dados. Ensures strong, accurate joints every time." & vbLf &
"###SUBTITLE:Available Joint Types" & vbLf &
"@METHOD:Mortise & Tenon|Traditional frame joint - strong and versatile (Standard, Haunched, Through)" & vbLf &
"@METHOD:Dovetails|Beautiful, interlocking drawer joint for hardwood and softwood" & vbLf &
"@METHOD:Box Joints|Finger joints for boxes - easier than dovetails, very strong" & vbLf &
"@METHOD:Dado & Groove|Housed joints for shelves and panels" & vbLf &
"" & vbLf &
"###SUBTITLE:Mortise & Tenon" & vbLf &
"Input your stock dimensions and the calculator determines:" & vbLf &
"*BULLET:Tenon thickness (typically 1/3 of stock thickness)" & vbLf &
"*BULLET:Tenon length (typically 1"" to 2"")" & vbLf &
"*BULLET:Tenon width (typically 2/3 of stock width)" & vbLf &
"*BULLET:Mortise depth (tenon length + 1/16"")" & vbLf &
"*BULLET:Shoulder offsets for aesthetic balance" & vbLf &
"" & vbLf &
"###SUBTITLE:Dovetails" & vbLf &
"Perfect for drawer fronts and backs. Calculator provides:" & vbLf &
"*BULLET:Dovetail angle (1:6 for softwood, 1:8 for hardwood)" & vbLf &
"*BULLET:Pin width (typically half-pin at corners)" & vbLf &
"*BULLET:Tail width (proportional to pin spacing)" & vbLf &
"*BULLET:Number of tails needed for board width" & vbLf &
"" & vbLf &
"###SUBTITLE:Box Joints" & vbLf &
"Simple, strong corner joints for boxes and drawers:" & vbLf &
"*BULLET:Pin width matches stock thickness" & vbLf &
"*BULLET:Alternating pins and sockets" & vbLf &
"*BULLET:Perfect for router table jig" & vbLf &
"" & vbLf &
"###SUBTITLE:Dados & Grooves" & vbLf &
"For shelf housing and panel installations:" & vbLf &
"*BULLET:Dado depth (typically 1/3 to 1/2 of stock thickness)" & vbLf &
"*BULLET:Dado width (matches shelf thickness + slight clearance)" & vbLf &
"*BULLET:Stopped or through dados" & vbLf &
"" & vbLf &
"?NOTE:Tip: Visual diagrams show you exactly how to cut each joint!" & vbLf &
"!WARNING:Always test joints with scrap wood before cutting your final pieces!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "WoodMovement",
.Title = "Wood Movement Calculator",
.Category = "Joinery",
.SortOrder = 21,
.Keywords = "wood movement,expansion,contraction,humidity,shrinkage,tangential,radial,grain,panel gap",
.Content =
"#TITLE:Wood Movement Calculator" & vbLf &
"##SECTION:Purpose|Predict how much wood will expand or contract with changes in humidity. Critical for avoiding cracked panels, stuck drawers, and failed joints." & vbLf &
"###SUBTITLE:Why Wood Moves" & vbLf &
"Wood absorbs and releases moisture with changing humidity. This causes:" & vbLf &
"*BULLET:Width changes (significant across the grain)" & vbLf &
"*BULLET:Minimal length changes (along the grain)" & vbLf &
"*BULLET:Seasonal expansion and contraction cycles" & vbLf &
"" & vbLf &
"###SUBTITLE:Calculation Inputs" & vbLf &
"*BULLET:Wood Species (50+ species in database)" & vbLf &
"*BULLET:Board Width (inches)" & vbLf &
"*BULLET:Initial Humidity % (where wood is now)" & vbLf &
"*BULLET:Final Humidity % (where it will be)" & vbLf &
"*BULLET:Grain Direction (Tangential/Radial)" & vbLf &
"" & vbLf &
"###SUBTITLE:Humidity Presets" & vbLf &
"@METHOD:Indoor Winter|6-8% RH - Dry heated air" & vbLf &
"@METHOD:Indoor Summer|10-12% RH - Higher humidity" & vbLf &
"@METHOD:Shop Storage|8-10% RH - Typical workshop" & vbLf &
"@METHOD:Kiln Dried|6-8% RH - Fresh from kiln" & vbLf &
"" & vbLf &
"###SUBTITLE:Grain Direction" & vbLf &
"Movement depends on how the board was cut:" & vbLf &
"*BULLET:Tangential (Flatsawn) - More movement, 2x radial" & vbLf &
"*BULLET:Radial (Quartersawn) - Less movement, more stable" & vbLf &
"" & vbLf &
"###SUBTITLE:Results Provided" & vbLf &
"*BULLET:Total movement in inches and fractions" & vbLf &
"*BULLET:Direction (expansion or contraction)" & vbLf &
"*BULLET:Recommended panel gaps (min/max)" & vbLf &
"*BULLET:Wood properties (density, movement coefficient)" & vbLf &
"" & vbLf &
"###SUBTITLE:Design Guidelines" & vbLf &
"*BULLET:Allow 1/16"" gap per 12"" of width for panel doors" & vbLf &
"*BULLET:Use elongated screw holes for table tops" & vbLf &
"*BULLET:Never glue wide boards edge-to-edge across grain" & vbLf &
"*BULLET:Account for movement in all cross-grain joints" & vbLf &
"" & vbLf &
"!WARNING:CRITICAL: Failure to account for wood movement is the #1 cause of failed projects!" & vbLf &
"?NOTE:Tip: Always use quartersawn lumber for the most stable projects!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "ShelfSag",
.Title = "Shelf Sag Calculator",
.Category = "Joinery",
.SortOrder = 22,
.Keywords = "shelf,sag,deflection,load,span,stiffener,plywood,MDF,oak,maple",
.Content =
"#TITLE:Shelf Sag Calculator" & vbLf &
"##SECTION:Purpose|Calculate shelf deflection (sag) and load capacity to design safe, sturdy shelves. Accounts for material properties, dimensions, and optional edge stiffeners." & vbLf &
"###SUBTITLE:Why This Matters" & vbLf &
"Shelves that sag too much are:" & vbLf &
"*BULLET:Aesthetically unpleasing - visible droop looks unprofessional" & vbLf &
"*BULLET:Functionally poor - items slide to center, doors don't close" & vbLf &
"*BULLET:Potentially unsafe - risk of failure under heavy loads" & vbLf &
"" & vbLf &
"###SUBTITLE:Industry Standard" & vbLf &
"Maximum acceptable sag is 1/360 of span" & vbLf &
"Example: For a 36"" shelf, maximum sag = 0.10""" & vbLf &
"" & vbLf &
"###SUBTITLE:Shelf Material Options" & vbLf &
"Choose from 14 material types with different stiffness:" & vbLf &
"*BULLET:Engineered: Plywood, MDF, Particleboard, Melamine, OSB" & vbLf &
"*BULLET:Softwoods: SYP, White Pine" & vbLf &
"*BULLET:Hardwoods: Oak, Maple, Walnut, Cherry, Mahogany" & vbLf &
"*BULLET:Other: Bamboo" & vbLf &
"" & vbLf &
"###SUBTITLE:Key Inputs" & vbLf &
"*BULLET:Span - Distance between supports (typical: 36"")" & vbLf &
"*BULLET:Thickness - Shelf material thickness (typical: 0.75"")" & vbLf &
"*BULLET:Width - Shelf depth front to back (typical: 10-12"")" & vbLf &
"*BULLET:Load - Total weight on shelf (lbs)" & vbLf &
"" & vbLf &
"###SUBTITLE:Edge Stiffeners" & vbLf &
"Add stiffeners to reduce sag:" & vbLf &
"*BULLET:Front edge band - solid wood strip glued to front edge" & vbLf &
"*BULLET:Back edge band - additional support at rear" & vbLf &
"*BULLET:Significantly increases stiffness without changing shelf thickness" & vbLf &
"" & vbLf &
"!WARNING:Always test shelves with actual weight before use!" & vbLf &
"?NOTE:Tip: Doubling thickness increases stiffness by 8x!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "cut_list",
.Title = "Cut List Optimizer",
.Category = "Optimization",
.SortOrder = 30,
.Keywords = "cut list,optimizer,waste,sheet goods,plywood,cutting diagram,kerf,pattern",
.Content =
"#TITLE:Cut List Optimizer" & vbLf &
"##SECTION:Purpose|Optimize how you cut parts from sheet goods or boards to minimize waste and save money. Generates cutting diagrams showing exactly where to make each cut." & vbLf &
"###SUBTITLE:How It Works" & vbLf &
"Enter all the pieces you need, and the optimizer:" & vbLf &
"*BULLET:Arranges pieces to minimize waste" & vbLf &
"*BULLET:Accounts for saw kerf (blade width)" & vbLf &
"*BULLET:Calculates total boards needed" & vbLf &
"*BULLET:Shows material cost" & vbLf &
"*BULLET:Displays cutting patterns visually" & vbLf &
"" & vbLf &
"###SUBTITLE:Adding Pieces" & vbLf &
"#NUM:1:Enter a label/name for each piece (e.g., ""Shelf A"")" & vbLf &
"#NUM:2:Enter length in inches" & vbLf &
"#NUM:3:Enter width in inches" & vbLf &
"#NUM:4:Enter quantity needed" & vbLf &
"#NUM:5:Click 'Add Row' for additional pieces" & vbLf &
"" & vbLf &
"###SUBTITLE:Stock Board Selection" & vbLf &
"Choose from standard sheet sizes:" & vbLf &
"*BULLET:4x8 Sheet (48"" x 96"") - Standard plywood" & vbLf &
"*BULLET:4x4 Sheet (48"" x 48"") - Half sheet" & vbLf &
"*BULLET:4x10 Sheet (48"" x 120"") - Oversized" & vbLf &
"*BULLET:Custom sizes supported" & vbLf &
"" & vbLf &
"###SUBTITLE:Kerf Width" & vbLf &
"Set your saw blade width (kerf) for accurate calculations:" & vbLf &
"*BULLET:Table saw: 1/8"" (0.125"")" & vbLf &
"*BULLET:Circular saw: 3/32"" to 1/8""" & vbLf &
"*BULLET:Track saw: 1/16"" to 3/32""" & vbLf &
"" & vbLf &
"###SUBTITLE:Optimization Results" & vbLf &
"After clicking 'Optimize', you'll see:" & vbLf &
"*BULLET:Boards Needed - Total sheets/boards required" & vbLf &
"*BULLET:Total Cost - Based on board price" & vbLf &
"*BULLET:Waste % - Percentage of material wasted" & vbLf &
"*BULLET:Efficiency % - How well pieces were packed" & vbLf &
"" & vbLf &
"?NOTE:Tip: Print cutting diagrams and tape them to your boards in the shop!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "units",
.Title = "Unit Conversions",
.Category = "Conversions",
.SortOrder = 40,
.Keywords = "units,conversion,inches,millimeters,feet,meters,imperial,metric",
.Content =
"#TITLE:Unit Conversions" & vbLf &
"##SECTION:Available Conversions|Quick conversion between imperial and metric units commonly used in woodworking." & vbLf &
"###SUBTITLE:Length Conversions" & vbLf &
"*BULLET:Inches to Millimeters (1"" = 25.4mm)" & vbLf &
"*BULLET:Millimeters to Inches" & vbLf &
"*BULLET:Feet to Meters" & vbLf &
"" & vbLf &
"###SUBTITLE:Fraction Conversions" & vbLf &
"*BULLET:Decimal to Fraction (e.g., 0.375 = 3/8)" & vbLf &
"*BULLET:Fraction to Decimal (e.g., 3/4 = 0.75)" & vbLf &
"*BULLET:Mixed fractions supported (e.g., 1 1/2 = 1.5)" & vbLf &
"" & vbLf &
"###SUBTITLE:Quick Reference Tables" & vbLf &
"The Calculations tab includes comprehensive conversion tables for common fractions (1/64"" through 1"") in both decimal and metric." & vbLf &
"" & vbLf &
"?NOTE:Tip: Conversion constants are centralized and can be found in UnitConversionConstants module!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "fractions",
.Title = "Fraction Conversions",
.Category = "Conversions",
.SortOrder = 41,
.Keywords = "fraction,decimal,conversion,mixed,reduce,simplify",
.Content =
"#TITLE:Fraction Conversions" & vbLf &
"##SECTION:Working with Fractions|Woodworking often requires converting between fractional and decimal measurements." & vbLf &
"###SUBTITLE:Fraction to Decimal" & vbLf &
"Enter fractions in any of these formats:" & vbLf &
"*BULLET:Simple: 3/4" & vbLf &
"*BULLET:Mixed: 1 1/2" & vbLf &
"*BULLET:Already decimal: 0.75" & vbLf &
"" & vbLf &
"###SUBTITLE:Decimal to Fraction" & vbLf &
"Converts decimal values to the nearest 1/64"" fraction. The calculator automatically reduces fractions (e.g., 32/64 becomes 1/2)." & vbLf &
"" & vbLf &
"###SUBTITLE:Common Conversions" & vbLf &
"Quick reference for common woodworking measurements:" & vbLf &
"*BULLET:1/4"" = 0.25 (Quarter inch)" & vbLf &
"*BULLET:3/8"" = 0.375 (Common dado width)" & vbLf &
"*BULLET:1/2"" = 0.5 (Half inch)" & vbLf &
"*BULLET:5/8"" = 0.625 (Common drawer bottom)" & vbLf &
"*BULLET:3/4"" = 0.75 (Standard plywood thickness)"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "table_tip",
.Title = "Table Tipping Force Calculator",
.Category = "Conversions",
.SortOrder = 42,
.Keywords = "table,tipping,force,safety,children,weight,base,stability",
.Content =
"#TITLE:Table Tipping Force Calculator" & vbLf &
"##SECTION:Purpose|Calculate the force required to tip over a table. Important for safety in furniture design, especially with children." & vbLf &
"###SUBTITLE:How It Works" & vbLf &
"The calculator determines the force needed at the edge of the table top to cause tipping, based on:" & vbLf &
"*BULLET:Table top weight and length" & vbLf &
"*BULLET:Base weight and length" & vbLf &
"*BULLET:Lever arm principles" & vbLf &
"" & vbLf &
"###SUBTITLE:Required Inputs" & vbLf &
"*BULLET:Table Top Length (inches or mm)" & vbLf &
"*BULLET:Table Top Weight (lbs or kg)" & vbLf &
"*BULLET:Base Length (inches or mm)" & vbLf &
"*BULLET:Base Weight (lbs or kg)" & vbLf &
"" & vbLf &
"###SUBTITLE:Safety Guidelines" & vbLf &
"!WARNING:IMPORTANT: Tipping force should be AT LEAST 50 lbs (23 kg) for tables used around children!" & vbLf &
"!WARNING:Consider: A heavy table is safer than a light one with the same proportions" & vbLf &
"!WARNING:Wider bases significantly increase tipping resistance" & vbLf &
"" & vbLf &
"###SUBTITLE:Design Tips" & vbLf &
"*BULLET:Increase base width/length" & vbLf &
"*BULLET:Add weight to the base" & vbLf &
"*BULLET:Reduce table top overhang" & vbLf &
"*BULLET:Consider attaching to wall for very tall pieces"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "shortcuts",
.Title = "Keyboard Shortcuts",
.Category = "Features",
.SortOrder = 50,
.Keywords = "keyboard,shortcuts,hotkeys,ctrl,tab,navigation,copy,paste",
.Content =
"#TITLE:Keyboard Shortcuts" & vbLf &
"##SECTION:Work Faster|Use keyboard shortcuts to navigate and perform calculations more efficiently." & vbLf &
"###SUBTITLE:Navigation Shortcuts" & vbLf &
"^SHORTCUT:Ctrl+Tab|Next tab" & vbLf &
"^SHORTCUT:Ctrl+Shift+Tab|Previous tab" & vbLf &
"^SHORTCUT:Tab|Next field" & vbLf &
"^SHORTCUT:Shift+Tab|Previous field" & vbLf &
"" & vbLf &
"###SUBTITLE:Action Shortcuts" & vbLf &
"^SHORTCUT:Enter|Calculate (when in input field)" & vbLf &
"^SHORTCUT:Ctrl+C|Copy results" & vbLf &
"^SHORTCUT:Ctrl+V|Paste values" & vbLf &
"^SHORTCUT:Ctrl+A|Select all (in text fields)" & vbLf &
"^SHORTCUT:Escape|Clear current field" & vbLf &
"" & vbLf &
"?NOTE:Tip: Text fields auto-select all text when you click them for quick replacement!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "themes",
.Title = "Dark & Light Themes",
.Category = "Features",
.SortOrder = 51,
.Keywords = "theme,dark,light,appearance,toggle,eye strain",
.Content =
"#TITLE:Dark & Light Themes" & vbLf &
"##SECTION:Visual Themes|Choose between Light and Dark themes to match your preference and working environment." & vbLf &
"###SUBTITLE:Switching Themes" & vbLf &
"Click the theme toggle in the status bar at the bottom of the window." & vbLf &
"" & vbLf &
"###SUBTITLE:Light Theme" & vbLf &
"*BULLET:Traditional light background" & vbLf &
"*BULLET:Black text on white" & vbLf &
"*BULLET:Better for bright environments" & vbLf &
"*BULLET:Better for printing screenshots" & vbLf &
"" & vbLf &
"###SUBTITLE:Dark Theme" & vbLf &
"*BULLET:Dark background with white text" & vbLf &
"*BULLET:Reduces eye strain in dim lighting" & vbLf &
"*BULLET:Modern appearance" & vbLf &
"*BULLET:Reduces screen brightness" & vbLf &
"" & vbLf &
"?NOTE:Your theme preference is remembered between sessions!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "best_practices",
.Title = "Best Practices",
.Category = "Features",
.SortOrder = 52,
.Keywords = "best practices,tips,measurement,safety,material,accuracy",
.Content =
"#TITLE:Best Practices" & vbLf &
"##SECTION:Tips for Success|Follow these best practices to get the most accurate and useful results from Woodworker's Friend." & vbLf &
"###SUBTITLE:Measurement Best Practices" & vbLf &
"*BULLET:Always measure twice, calculate once" & vbLf &
"*BULLET:Use consistent units throughout a project" & vbLf &
"*BULLET:Account for wood movement in panel calculations" & vbLf &
"*BULLET:Add waste factor to material estimates (10-20%)" & vbLf &
"*BULLET:Consider grain direction in your calculations" & vbLf &
"" & vbLf &
"###SUBTITLE:Calculator Usage" & vbLf &
"*BULLET:Use presets as starting points" & vbLf &
"*BULLET:Export results before starting a new calculation" & vbLf &
"*BULLET:Verify results make sense for your project" & vbLf &
"*BULLET:Save custom presets for repeat projects" & vbLf &
"" & vbLf &
"###SUBTITLE:Safety First" & vbLf &
"!WARNING:Always verify structural calculations with local building codes" & vbLf &
"!WARNING:Test joinery on scrap wood before final pieces" & vbLf &
"!WARNING:Use appropriate safety equipment when working" & vbLf &
"" & vbLf &
"?NOTE:The calculator is a tool - your experience and judgment are irreplaceable!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "joinery_reference",
.Title = "Joinery Reference Guide",
.Category = "References",
.SortOrder = 53,
.Keywords = "joinery,reference,mortise,tenon,dovetail,box joint,dado,rabbet,lap joint,biscuit,dowel,pocket hole,spline,strength,difficulty",
.Content =
"#TITLE:Joinery Reference Guide" & vbLf &
"##SECTION:Woodworking Joinery Database|Comprehensive reference for traditional and modern woodworking joints." & vbLf &
"###SUBTITLE:What is the Joinery Reference?" & vbLf &
"The Joinery Reference tab provides a searchable database of 12 common woodworking joint types. Each entry includes:" & vbLf &
"*BULLET:Joint name and category (Frame, Box, Edge)" & vbLf &
"*BULLET:Strength rating (1-5 scale)" & vbLf &
"*BULLET:Difficulty level (Beginner, Intermediate, Advanced)" & vbLf &
"*BULLET:Detailed description and historical context" & vbLf &
"*BULLET:Typical uses and applications" & vbLf &
"*BULLET:Required tools and equipment" & vbLf &
"*BULLET:Strength characteristics" & vbLf &
"*BULLET:Glue requirements" & vbLf &
"*BULLET:Reinforcement options" & vbLf &
"" & vbLf &
"###SUBTITLE:Using the Joinery Reference" & vbLf &
"*BULLET:Click on the References tab, then Joinery Types tab" & vbLf &
"*BULLET:Filter joints by category (Frame, Box, Edge) or difficulty" & vbLf &
"*BULLET:Click any joint type to view detailed specifications" & vbLf &
"*BULLET:Sort by name, strength, or difficulty by clicking column headers" & vbLf &
"*BULLET:Review the description, tools needed, and typical uses" & vbLf &
"" & vbLf &
"###SUBTITLE:Joint Categories" & vbLf &
"&TOPIC:Frame Joinery" & vbLf &
"Connects pieces at angles (legs to aprons, chair rails):" & vbLf &
"*BULLET:Mortise & Tenon - strongest traditional joint" & vbLf &
"*BULLET:Bridle Joint - good for leg-to-rail connections" & vbLf &
"*BULLET:Biscuit Joint - fast alignment for frames" & vbLf &
"*BULLET:Dowel Joint - simple alternative to mortise & tenon" & vbLf &
"*BULLET:Pocket Hole - fastest method, requires jig" & vbLf &
"" & vbLf &
"&TOPIC:Box Joinery" & vbLf &
"Joins boards edge-to-edge for boxes and drawers:" & vbLf &
"*BULLET:Dovetail (Through) - strongest and most decorative" & vbLf &
"*BULLET:Dovetail (Half-Blind) - strong with hidden joint on one face" & vbLf &
"*BULLET:Box Joint (Finger Joint) - strong and decorative" & vbLf &
"*BULLET:Rabbet Joint - simple for box backs and bottoms" & vbLf &
"" & vbLf &
"&TOPIC:Edge Joinery" & vbLf &
"Joins boards face-to-face or edge-to-edge:" & vbLf &
"*BULLET:Spline Joint - reinforces edge glue-ups" & vbLf &
"*BULLET:Dado - creates strong housing for shelves" & vbLf &
"*BULLET:Lap Joint - creates flush surfaces" & vbLf &
"" & vbLf &
"###SUBTITLE:Strength Ratings Explained" & vbLf &
"Joint strength is rated 1-5 stars:" & vbLf &
"*BULLET:⭐ - Lightweight applications only" & vbLf &
"*BULLET:⭐⭐ - Light-duty furniture" & vbLf &
"*BULLET:⭐⭐⭐ - Medium-strength for most furniture" & vbLf &
"*BULLET:⭐⭐⭐⭐ - Heavy-duty furniture and structures" & vbLf &
"*BULLET:⭐⭐⭐⭐⭐ - Maximum strength for demanding applications" & vbLf &
"" & vbLf &
"?NOTE:Click any joint in the list to see full details, historical notes, and recommended reinforcement options!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "hardware_standards",
.Title = "Hardware Standards Reference",
.Category = "References",
.SortOrder = 54,
.Keywords = "hardware,hinges,slides,shelf,fasteners,brackets,pulls,knobs,euro hinge,drawer slide,specifications,dimensions,mounting",
.Content =
"#TITLE:Hardware Standards Reference" & vbLf &
"##SECTION:Woodworking Hardware Database|Comprehensive specifications for common cabinet and furniture hardware." & vbLf &
"###SUBTITLE:What is the Hardware Reference?" & vbLf &
"The Hardware Standards tab provides detailed specifications for 16 common hardware items used in woodworking. Each entry includes:" & vbLf &
"*BULLET:Hardware type and category" & vbLf &
"*BULLET:Brand recommendations" & vbLf &
"*BULLET:Part numbers (when applicable)" & vbLf &
"*BULLET:Precise dimensions and specifications" & vbLf &
"*BULLET:Mounting requirements" & vbLf &
"*BULLET:Weight capacity ratings" & vbLf &
"*BULLET:Typical uses and applications" & vbLf &
"*BULLET:Installation notes and tips" & vbLf &
"" & vbLf &
"###SUBTITLE:Using the Hardware Reference" & vbLf &
"*BULLET:Click on the References tab, then Hardware tab" & vbLf &
"*BULLET:Filter by category (Hinges, Slides, Shelf Support, Fasteners)" & vbLf &
"*BULLET:Click any hardware item to view complete specifications" & vbLf &
"*BULLET:Sort by type, category, brand, or dimensions" & vbLf &
"*BULLET:Review mounting requirements before purchasing" & vbLf &
"*BULLET:Check weight capacity for your application" & vbLf &
"" & vbLf &
"###SUBTITLE:Hardware Categories" & vbLf &
"&TOPIC:Hinges" & vbLf &
"Cabinet and door hardware:" & vbLf &
"*BULLET:European (Euro) Hinges - 35mm concealed, most common" & vbLf &
"*BULLET:Butt Hinges - traditional surface-mounted" & vbLf &
"*BULLET:Overlay Hinges - no mortising required" & vbLf &
"" & vbLf &
"&TOPIC:Drawer Slides" & vbLf &
"Full-extension and soft-close options:" & vbLf &
"*BULLET:Ball-Bearing Slides - side-mount, 75-100 lbs capacity" & vbLf &
"*BULLET:Undermount Soft-Close - hidden installation, premium option" & vbLf &
"" & vbLf &
"&TOPIC:Shelf Support" & vbLf &
"Adjustable and fixed shelf hardware:" & vbLf &
"*BULLET:5mm Shelf Pins - metric standard" & vbLf &
"*BULLET:1/4"" Shelf Pins - imperial standard" & vbLf &
"" & vbLf &
"&TOPIC:Brackets & Fasteners" & vbLf &
"Structural and mounting hardware:" & vbLf &
"*BULLET:Corner Braces - reinforces joints" & vbLf &
"*BULLET:Table Leg Brackets - secure leg attachment" & vbLf &
"*BULLET:Wood Screws - standard #8 size" & vbLf &
"*BULLET:Confirmat Screws - European cabinet fasteners" & vbLf &
"" & vbLf &
"&TOPIC:Pulls & Knobs" & vbLf &
"Cabinet door and drawer handles:" & vbLf &
"*BULLET:Bar Pulls - 3"" center-to-center most common" & vbLf &
"*BULLET:Knobs - 1.25"" diameter standard" & vbLf &
"" & vbLf &
"###SUBTITLE:Key Specifications" & vbLf &
"When selecting hardware, pay attention to:" & vbLf &
"*BULLET:Dimensions - Ensure proper fit for your project" & vbLf &
"*BULLET:Mounting Requirements - Drill sizes, clearances, depths" & vbLf &
"*BULLET:Weight Capacity - Match to your application loads" & vbLf &
"*BULLET:Installation Notes - Special tools or techniques required" & vbLf &
"" & vbLf &
"###SUBTITLE:Installation Tips" & vbLf &
"!WARNING:Euro hinges require precise 35mm boring" & vbLf &
"!WARNING:Drawer slides need exact 1/2"" clearance per side" & vbLf &
"!WARNING:Use pilot holes for all screws to prevent splitting" & vbLf &
"" & vbLf &
"?NOTE:Click any hardware item to see detailed mounting requirements, brand recommendations, and part numbers!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "troubleshooting",
.Title = "Troubleshooting",
.Category = "Support",
.SortOrder = 60,
.Keywords = "troubleshooting,problem,error,fix,invalid input,display,performance,help",
.Content =
"#TITLE:Troubleshooting" & vbLf &
"##SECTION:Common Issues & Solutions|Having problems? Check these common issues and their solutions." & vbLf &
"###SUBTITLE:Calculation Issues" & vbLf &
"&PROBLEM:""Invalid Input"" error" & vbLf &
"&SOLUTION:Check that all required fields are filled in. Verify values are within valid ranges (hover for tooltip). Ensure you're using numbers, not text." & vbLf &
"" & vbLf &
"&PROBLEM:Results seem incorrect" & vbLf &
"&SOLUTION:Verify you're using consistent units (all inches or all mm). Double-check input values for typos. Ensure correct calculation method is selected." & vbLf &
"" & vbLf &
"&PROBLEM:Can't see all results" & vbLf &
"&SOLUTION:Scroll down in the results panel. Resize the window or splitter. Export results to view in external program." & vbLf &
"" & vbLf &
"###SUBTITLE:Display Issues" & vbLf &
"&PROBLEM:Text is too small/large" & vbLf &
"&SOLUTION:Use Windows display scaling settings. Try different theme (Light/Dark). Maximize window for more space." & vbLf &
"" & vbLf &
"###SUBTITLE:Getting Help" & vbLf &
"If problems persist:" & vbLf &
"*BULLET:Check error log file in application folder" & vbLf &
"*BULLET:Note exact error messages" & vbLf &
"*BULLET:Try restarting the application" & vbLf &
"*BULLET:Contact support with screenshots and error logs" & vbLf &
"" & vbLf &
"?NOTE:Error logs are automatically created in the Logs folder for debugging!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "version",
.Title = "Version Information",
.Category = "About",
.SortOrder = 70,
.Keywords = "version,about,features,system,build,.NET,credits",
.Content =
"#TITLE:Version Information" & vbLf &
"##SECTION:Woodworker's Friend|A comprehensive woodworking calculator and planning tool." & vbLf &
"###SUBTITLE:Features in This Version" & vbLf &
"*BULLET:Drawer height calculator with 10 calculation methods" & vbLf &
"*BULLET:Cabinet door calculator for inset and overlay" & vbLf &
"*BULLET:Board feet calculator with grid support" & vbLf &
"*BULLET:Epoxy pour volume calculator" & vbLf &
"*BULLET:Polygon calculator with visual display" & vbLf &
"*BULLET:Joinery calculator (mortise & tenon, dovetails, box joints, dados)" & vbLf &
"*BULLET:Wood movement calculator with 50+ species" & vbLf &
"*BULLET:Shelf sag calculator with material database" & vbLf &
"*BULLET:Cut list optimizer with visual cutting diagrams" & vbLf &
"*BULLET:Wood properties reference database (25 species, searchable)" & vbLf &
"*BULLET:Joinery reference guide (12 joint types with specifications)" & vbLf &
"*BULLET:Hardware standards database (16 items with dimensions)" & vbLf &
"*BULLET:Unit conversion tools" & vbLf &
"*BULLET:Table tipping force calculator" & vbLf &
"*BULLET:Dark/Light theme support" & vbLf &
"*BULLET:Export to CSV, Text, HTML" & vbLf &
"*BULLET:Unified SQLite database" & vbLf &
"*BULLET:Searchable help system" & vbLf &
"" & vbLf &
"###SUBTITLE:Technical Details" & vbLf &
"*BULLET:Language: Visual Basic .NET" & vbLf &
"*BULLET:UI Framework: Windows Forms" & vbLf &
"*BULLET:Database: SQLite" & vbLf &
"*BULLET:Architecture: Modular with partial classes" & vbLf &
"" & vbLf &
"###SUBTITLE:Credits" & vbLf &
"Developed with passion for woodworking and software craftsmanship." & vbLf &
"*BULLET:GitHub: https://github.com/dmaidon/Woodworkers-Friend" & vbLf &
"*BULLET:Report issues on GitHub Issues page" & vbLf &
"" & vbLf &
"?NOTE:Thank you for using Woodworker's Friend! Happy woodworking!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "export",
.Title = "Exporting Results",
.Category = "Features",
.SortOrder = 53,
.Keywords = "export,CSV,text,HTML,save,share,print",
.Content =
"#TITLE:Exporting Results" & vbLf &
"##SECTION:Export Capabilities|Save and share your calculations in multiple formats for documentation, client quotes, and project planning." & vbLf &
"###SUBTITLE:Available Export Formats" & vbLf &
"@METHOD:CSV (Comma-Separated Values)|Opens in Excel, Google Sheets. Best for data analysis." & vbLf &
"@METHOD:Text (Plain Text)|Simple format readable in any text editor. Good for printing." & vbLf &
"@METHOD:HTML (Web Page)|Formatted web page with styling. Perfect for emailing to clients." & vbLf &
"" & vbLf &
"###SUBTITLE:How to Export" & vbLf &
"#NUM:1:Complete your calculations" & vbLf &
"#NUM:2:Right-click on results area" & vbLf &
"#NUM:3:Choose 'Export Results'" & vbLf &
"#NUM:4:Select format (CSV, Text, or HTML)" & vbLf &
"#NUM:5:Choose save location" & vbLf &
"#NUM:6:Click Save" & vbLf &
"" & vbLf &
"?NOTE:Tip: CSV exports can be imported back into Excel for further calculations!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "presets",
.Title = "Using Presets",
.Category = "Features",
.SortOrder = 54,
.Keywords = "presets,kitchen,office,bathroom,custom,save,load,configuration",
.Content =
"#TITLE:Using Presets" & vbLf &
"##SECTION:What are Presets?|Presets are pre-configured settings for common woodworking scenarios. They save time and ensure you start with industry-standard dimensions." & vbLf &
"###SUBTITLE:Available Presets" & vbLf &
"@METHOD:Kitchen Standard|Typical kitchen base cabinet: 5 drawers with graduated heights" & vbLf &
"@METHOD:Office Desk|Standard office desk drawer configuration" & vbLf &
"@METHOD:Bathroom Vanity|Typical bathroom vanity dimensions" & vbLf &
"@METHOD:Custom Cabinet|Your saved custom configurations" & vbLf &
"" & vbLf &
"###SUBTITLE:Using Presets" & vbLf &
"#NUM:1:Navigate to the calculator (e.g., Drawers)" & vbLf &
"#NUM:2:Click on a preset button" & vbLf &
"#NUM:3:All fields are populated automatically" & vbLf &
"#NUM:4:Modify values as needed for your project" & vbLf &
"#NUM:5:Calculate to see results" & vbLf &
"" & vbLf &
"?NOTE:Tip: Save presets for your frequently-built pieces to speed up future projects!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "validation",
.Title = "Input Validation",
.Category = "Features",
.SortOrder = 55,
.Keywords = "validation,input,error,range,feedback,visual,rules",
.Content =
"#TITLE:Input Validation" & vbLf &
"##SECTION:Smart Input Validation|Woodworker's Friend includes intelligent validation to prevent errors and guide you to successful calculations." & vbLf &
"###SUBTITLE:Validation Features" & vbLf &
"*BULLET:Real-time feedback as you type" & vbLf &
"*BULLET:Clear error messages explaining what's wrong" & vbLf &
"*BULLET:Suggested valid ranges for each field" & vbLf &
"*BULLET:Automatic fixing of common mistakes" & vbLf &
"*BULLET:Prevention of impossible calculations" & vbLf &
"" & vbLf &
"###SUBTITLE:Common Validation Rules" & vbLf &
"Drawer Calculator:" & vbLf &
"*BULLET:Drawer count: 1-20 drawers" & vbLf &
"*BULLET:Width: 6-48 inches" & vbLf &
"*BULLET:Spacing: 0-2 inches" & vbLf &
"" & vbLf &
"Door Calculator:" & vbLf &
"*BULLET:Height: 6-120 inches" & vbLf &
"*BULLET:Width: 6-60 inches" & vbLf &
"*BULLET:Stile/Rail: 0.5-6 inches" & vbLf &
"" & vbLf &
"?NOTE:Tip: Hover over any input field to see its valid range and examples!" & vbLf &
"!WARNING:Validation prevents mistakes, but always double-check critical measurements!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "safety",
.Title = "Safety Calculators",
.Category = "Calculators",
.SortOrder = 56,
.Keywords = "safety,router,rpm,blade height,push stick,dust collection,cfm,speed,protection",
.Content =
"#TITLE:Safety Calculators" & vbLf &
"##SECTION:Safety-Focused Tools|Four essential calculators to help you work more safely by providing science-based recommendations." & vbLf &
"!WARNING:These calculators provide guidance based on industry standards. Always follow manufacturer specifications and use appropriate PPE!" & vbLf &
"" & vbLf &
"###SUBTITLE:Router Bit Speed Calculator" & vbLf &
"Calculate maximum safe RPM based on bit diameter and desired surface speed." & vbLf &
"" & vbLf &
"=FORMULA:RPM = (Surface Speed × 12) / (π × Diameter)" & vbLf &
"" & vbLf &
"*BULLET:Safe surface speed range: 9,000-12,000 ft/min" & vbLf &
"*BULLET:Large bits (>2 inches) require lower RPMs" & vbLf &
"*BULLET:Small bits (<1/2 inch) can run faster" & vbLf &
"*BULLET:Variable speed router essential for large bits" & vbLf &
"!WARNING:Large bits at high RPM can explode - always check before routing!" & vbLf &
"" & vbLf &
"###SUBTITLE:Blade Height Recommendations" & vbLf &
"Get safe blade height for table saw operations:" & vbLf &
"" & vbLf &
"*BULLET:Ripping: 1/8 to 1/4 inch above material (minimizes kickback)" & vbLf &
"*BULLET:Crosscutting: Full tooth (≈1/4 inch) above material (cleaner cut)" & vbLf &
"*BULLET:Dado/Groove: Just breaking through (+1/32 inch) (precise depth)" & vbLf &
"*BULLET:Thin Stock: 1/16 inch above material (extra safety)" & vbLf &
"" & vbLf &
"?NOTE:Blade height affects both safety and cut quality - follow recommendations!" & vbLf &
"" & vbLf &
"###SUBTITLE:Push Stick Requirements" & vbLf &
"Evaluates risk level and required safety devices:" & vbLf &
"" & vbLf &
"@METHOD:Risk Levels|Four-level assessment: LOW, MODERATE, HIGH, CRITICAL" & vbLf &
"" & vbLf &
"*BULLET:Stock < 3 inches wide = CRITICAL risk" & vbLf &
"*BULLET:Stock < 0.5 inches thick = CRITICAL risk" & vbLf &
"*BULLET:Stock 3-6 inches wide = HIGH risk" & vbLf &
"*BULLET:Stock 6-12 inches wide = MODERATE risk" & vbLf &
"*BULLET:Stock > 12 inches wide = LOW risk" & vbLf &
"" & vbLf &
"!WARNING:The 6-inch rule: NEVER let hands come within 6 inches of running blade!" & vbLf &
"" & vbLf &
"###SUBTITLE:Dust Collection CFM Calculator" & vbLf &
"Calculate required CFM (Cubic Feet per Minute) for effective dust collection:" & vbLf &
"" & vbLf &
"*BULLET:Tool-specific requirements for 8 common tools" & vbLf &
"*BULLET:Port diameter and duct length calculations" & vbLf &
"*BULLET:Static pressure loss compensation" & vbLf &
"*BULLET:Table Saw: 450-650 CFM recommended" & vbLf &
"*BULLET:Planer: 600-1000 CFM recommended" & vbLf &
"*BULLET:Drum Sander: 800-1200 CFM recommended" & vbLf &
"" & vbLf &
"!WARNING:Wood dust is carcinogenic! Fine dust (<10 microns) is most dangerous!" & vbLf &
"" & vbLf &
"###SUBTITLE:General Safety Rules" & vbLf &
"*BULLET:Always wear safety glasses, hearing protection, and dust mask" & vbLf &
"*BULLET:No gloves around rotating tools" & vbLf &
"*BULLET:Remove jewelry and tie back long hair" & vbLf &
"*BULLET:Never reach over or behind blade" & vbLf &
"*BULLET:Stand to the side, not behind stock" & vbLf &
"*BULLET:If it feels unsafe, find another way" & vbLf &
"" & vbLf &
"?NOTE:For detailed safety information, see the Safety Calculator Help document!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "sanding_grit",
.Title = "Sanding Grit Progression Calculator",
.Category = "Calculators",
.SortOrder = 57,
.Keywords = "sanding,grit,progression,sandpaper,finish,smooth,sequential,skip grit,softwood,hardwood",
.Content =
"#TITLE:Sanding Grit Progression Calculator" & vbLf &
"##SECTION:Purpose|Calculate optimal sanding grit sequence for achieving professional-quality smooth finishes on woodworking projects." & vbLf &
"###SUBTITLE:Why Grit Progression Matters" & vbLf &
"Each sandpaper grit creates scratches in wood. Progressive sanding replaces deep scratches with finer ones until invisible to the eye." & vbLf &
"" & vbLf &
"*BULLET:Skipping grits leaves visible scratches" & vbLf &
"*BULLET:Starting too fine wastes time" & vbLf &
"*BULLET:Ending too coarse gives rough finish" & vbLf &
"*BULLET:Proper progression = professional results" & vbLf &
"" & vbLf &
"###SUBTITLE:Input Parameters" & vbLf &
"@METHOD:Wood Type|Softwood (Pine, Fir) or Hardwood (Oak, Maple, Walnut)" & vbLf &
"@METHOD:Starting Grit|40-150 based on surface condition (80 most common)" & vbLf &
"@METHOD:Final Grit|150-600 based on desired finish (220 standard)" & vbLf &
"@METHOD:Progression Type|Sequential (thorough) or Skip-Grit (fast)" & vbLf &
"" & vbLf &
"###SUBTITLE:Progression Methods" & vbLf &
"~STEP:1:Sequential Method:Uses every grit in sequence for best quality" & vbLf &
"~STEP:2:Skip-Grit Method:Skips every other grit for faster results" & vbLf &
"" & vbLf &
"###SUBTITLE:Starting Grits Guide" & vbLf &
"*BULLET:40 - Extra coarse: Heavy stock removal, major flattening" & vbLf &
"*BULLET:60 - Coarse: Remove mill marks, rough shaping" & vbLf &
"*BULLET:80 - Medium-coarse: Standard starting point (most common)" & vbLf &
"*BULLET:100 - Medium: Already smooth surfaces" & vbLf &
"*BULLET:120 - Medium-fine: Very smooth starting surfaces" & vbLf &
"" & vbLf &
"###SUBTITLE:Final Grits Guide" & vbLf &
"*BULLET:150 - Fine: Basic smooth finish, acceptable for paint" & vbLf &
"*BULLET:180 - Fine: Standard for stain and paint" & vbLf &
"*BULLET:220 - Very fine: Optimal for clear finish (most common)" & vbLf &
"*BULLET:320 - Extra fine: Between finish coats" & vbLf &
"*BULLET:400-600 - Ultra fine: High-gloss prep, wet-sanding only" & vbLf &
"" & vbLf &
"###SUBTITLE:Sequential Method (Thorough)" & vbLf &
"Uses every grit in the standard sequence." & vbLf &
"" & vbLf &
"Example: 80 → 100 → 120 → 150 → 180 → 220" & vbLf &
"" & vbLf &
"*BULLET:Best finish quality" & vbLf &
"*BULLET:Each grit removes previous scratches" & vbLf &
"*BULLET:Recommended for clear finishes" & vbLf &
"*BULLET:Takes more time but better results" & vbLf &
"*BULLET:Reveals wood grain figure best" & vbLf &
"" & vbLf &
"###SUBTITLE:Skip-Grit Method (Fast)" & vbLf &
"Skips every other grit (keeps first and last)." & vbLf &
"" & vbLf &
"Example: 80 → 120 → 180 → 220" & vbLf &
"" & vbLf &
"*BULLET:Faster process" & vbLf &
"*BULLET:Uses less sandpaper" & vbLf &
"*BULLET:May leave visible scratches" & vbLf &
"*BULLET:Good for painted surfaces" & vbLf &
"!WARNING:Check for cross-grain scratches between grits!" & vbLf &
"" & vbLf &
"###SUBTITLE:Softwood Tips" & vbLf &
"Pine, Fir, Cedar, Spruce require careful sanding:" & vbLf &
"" & vbLf &
"*BULLET:Don't skip grits - shows scratches easily" & vbLf &
"*BULLET:Use light pressure to avoid dishing" & vbLf &
"*BULLET:Sand with the grain direction" & vbLf &
"*BULLET:Watch for raised grain after first grit" & vbLf &
"*BULLET:Consider wet-sanding between coats" & vbLf &
"" & vbLf &
"###SUBTITLE:Hardwood Tips" & vbLf &
"Oak, Maple, Walnut, Cherry are more forgiving:" & vbLf &
"" & vbLf &
"*BULLET:Can skip grits if needed" & vbLf &
"*BULLET:Higher final grit = better grain clarity" & vbLf &
"*BULLET:Check for mill marks before sanding" & vbLf &
"*BULLET:Use card scraper for difficult grain" & vbLf &
"*BULLET:Final grit shows grain figure best" & vbLf &
"" & vbLf &
"###SUBTITLE:Results Provided" & vbLf &
"*BULLET:Grit sequence (e.g., 80 → 120 → 180 → 220)" & vbLf &
"*BULLET:Total steps required" & vbLf &
"*BULLET:Estimated time (3-5 minutes per grit)" & vbLf &
"*BULLET:Method-specific recommendations" & vbLf &
"*BULLET:Wood-specific tips" & vbLf &
"*BULLET:Description of each grit's purpose" & vbLf &
"*BULLET:Safety reminders" & vbLf &
"" & vbLf &
"###SUBTITLE:Common Sequences" & vbLf &
"Fine Furniture (Hardwood, Clear): 80 → 100 → 120 → 150 → 180 → 220" & vbLf &
"Painted Cabinet: 80 → 120 → 180" & vbLf &
"Stained Project: 80 → 100 → 120 → 150 → 180 → 220" & vbLf &
"Premium Table: 60 → 80 → 100 → 120 → 150 → 180 → 220 → 320" & vbLf &
"" & vbLf &
"!WARNING:SAFETY: Always wear dust mask! Wood dust is carcinogenic!" & vbLf &
"!WARNING:Use dust collection or work outdoors when possible!" & vbLf &
"" & vbLf &
"?NOTE:Tip: Test your finish on scrap wood after final sanding!"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "clamp_spacing",
.Title = "Clamp Spacing Calculator",
.Category = "Calculators",
.SortOrder = 58,
.Keywords = "clamp,spacing,glue-up,panel,pressure,psi,pva,polyurethane,epoxy,edge glue",
.Content =
"#TITLE:Clamp Spacing Calculator" & vbLf &
"##SECTION:Purpose|Calculate optimal clamp placement for panel glue-ups to ensure even pressure distribution and flat, strong joints." & vbLf &
"###SUBTITLE:Why Clamp Spacing Matters" & vbLf &
"Proper clamp spacing is critical for successful glue-ups:" & vbLf &
"" & vbLf &
"*BULLET:Too wide = dry spots, weak joint, panel bowing" & vbLf &
"*BULLET:Too narrow = excessive squeeze-out, wasted time" & vbLf &
"*BULLET:Proper spacing = even pressure, flat panels" & vbLf &
"*BULLET:Industry standard: 8-15 inches maximum spacing" & vbLf &
"" & vbLf &
"###SUBTITLE:Input Parameters" & vbLf &
"@METHOD:Panel Width|Width of panel being glued (inches or mm)" & vbLf &
"@METHOD:Panel Thickness|Thickness affects spacing (Rule: 12x thickness)" & vbLf &
"@METHOD:Wood Type|Hardwood or Softwood (affects pressure needs)" & vbLf &
"@METHOD:Glue Type|PVA, Polyurethane, or Epoxy (affects spacing)" & vbLf &
"" & vbLf &
"###SUBTITLE:Calculation Formula" & vbLf &
"Base Spacing = Panel Thickness × 12" & vbLf &
"" & vbLf &
"=FORMULA:Adjusted Spacing = Base × Wood Factor × Glue Factor" & vbLf &
"" & vbLf &
"*BULLET:Hardwood Factor: 1.0 (needs more pressure)" & vbLf &
"*BULLET:Softwood Factor: 1.2 (can space slightly wider)" & vbLf &
"" & vbLf &
"###SUBTITLE:Glue Type Adjustments" & vbLf &
"@METHOD:PVA (White/Yellow Glue)|Standard spacing, 30-45 min clamp time" & vbLf &
"@METHOD:Polyurethane|0.9x spacing (needs more pressure), 2-4 hour clamp time" & vbLf &
"@METHOD:Epoxy|1.1x spacing (less pressure needed), 4-6 hour clamp time" & vbLf &
"" & vbLf &
"###SUBTITLE:Results Provided" & vbLf &
"*BULLET:Recommended spacing between clamps" & vbLf &
"*BULLET:Number of clamps needed" & vbLf &
"*BULLET:Clamp pressure (PSI) recommendation" & vbLf &
"*BULLET:Detailed clamping tips and best practices" & vbLf &
"" & vbLf &
"###SUBTITLE:Best Practices" & vbLf &
"*BULLET:Alternate clamps top/bottom to prevent panel bowing" & vbLf &
"*BULLET:Use cauls (clamping boards) for perfectly flat panels" & vbLf &
"*BULLET:Check for square before glue sets" & vbLf &
"*BULLET:Don't over-tighten - minimal squeeze-out is ideal" & vbLf &
"*BULLET:Use wax paper under cauls to prevent sticking" & vbLf &
"" & vbLf &
"!WARNING:Over-clamping can cause sunken joints and glue starvation!" & vbLf &
"?NOTE:Rule of thumb: You should see a thin, even bead of glue along entire joint"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "biscuit_domino",
.Title = "Biscuit and Domino Joinery Calculator",
.Category = "Calculators",
.SortOrder = 59,
.Keywords = "biscuit,domino,joinery,edge joint,alignment,spacing,center marks,festool,plate joiner",
.Content =
"#TITLE:Biscuit and Domino Joinery Calculator" & vbLf &
"##SECTION:Purpose|Calculate precise placement of biscuits or dominos for professional edge-to-edge joints with perfect alignment." & vbLf &
"###SUBTITLE:What Are Biscuits and Dominos?" & vbLf &
"Compressed wood wafers (biscuits) or loose tenons (dominos) used for alignment and strength in edge joints." & vbLf &
"" & vbLf &
"*BULLET:Biscuits: Oval compressed beech wafers, expand when wet with glue" & vbLf &
"*BULLET:Dominos: Rectangular tenons, used with Festool Domino joiner" & vbLf &
"*BULLET:Primary purpose: ALIGNMENT (strength is secondary)" & vbLf &
"*BULLET:Common uses: Tabletops, panels, edge-to-edge boards" & vbLf &
"" & vbLf &
"###SUBTITLE:Biscuit Sizes" & vbLf &
"Standard sizes with dimensions:" & vbLf &
"" & vbLf &
"@METHOD:#0 Biscuit|1.75 inches long × 0.55 inches wide (small projects)" & vbLf &
"@METHOD:#10 Biscuit|2.125 inches long × 0.55 inches wide (standard)" & vbLf &
"@METHOD:#20 Biscuit|2.375 inches long × 0.68 inches wide (most common)" & vbLf &
"@METHOD:#FF Biscuit|2.75 inches long × 0.68 inches wide (large panels)" & vbLf &
"@METHOD:#H9 Biscuit|2.75 inches long × 0.68 inches wide (heavy duty)" & vbLf &
"" & vbLf &
"###SUBTITLE:Festool Domino Sizes" & vbLf &
"@METHOD:4mm × 20mm|0.79 inches long (small face frames)" & vbLf &
"@METHOD:5mm × 30mm|1.18 inches long (face frames)" & vbLf &
"@METHOD:6mm × 40mm|1.57 inches long (cabinet work)" & vbLf &
"@METHOD:8mm × 50mm|1.97 inches long (furniture, panels)" & vbLf &
"@METHOD:10mm × 50mm|1.97 inches long (heavy duty)" & vbLf &
"" & vbLf &
"###SUBTITLE:Input Parameters" & vbLf &
"@METHOD:Joint Length|Total length of edge joint being connected" & vbLf &
"@METHOD:Joinery Type|Biscuit or Domino" & vbLf &
"@METHOD:Size|Select from standard sizes above" & vbLf &
"@METHOD:Joint Strength|Light, Medium, or Heavy (affects spacing)" & vbLf &
"@METHOD:Stock Thickness|Thickness of boards being joined" & vbLf &
"" & vbLf &
"###SUBTITLE:Spacing Recommendations" & vbLf &
"Base spacing varies by size and strength requirement:" & vbLf &
"" & vbLf &
"*BULLET:Light Duty: 1.25x base spacing (wider)" & vbLf &
"*BULLET:Medium Duty: Standard base spacing" & vbLf &
"*BULLET:Heavy Duty: 0.75x base spacing (tighter)" & vbLf &
"" & vbLf &
"Typical base spacing:" & vbLf &
"*BULLET:#0 / 4mm Domino: 4 inches" & vbLf &
"*BULLET:#10 / 5mm Domino: 5 inches" & vbLf &
"*BULLET:#20 / 6mm Domino: 6 inches" & vbLf &
"*BULLET:#FF / 8-10mm Domino: 8-10 inches" & vbLf &
"" & vbLf &
"###SUBTITLE:Edge Distance Calculation" & vbLf &
"Edge distance prevents biscuit/domino exposure at joint ends." & vbLf &
"" & vbLf &
"=FORMULA:Edge Distance = (Joinery Length ÷ 2) + 0.625 inches" & vbLf &
"" & vbLf &
"*BULLET:Half-length prevents exposure at end" & vbLf &
"*BULLET:5/8 inch padding allows trimming and error tolerance" & vbLf &
"*BULLET:Example #20: (2.375 ÷ 2) + 0.625 = 1.8125 inches" & vbLf &
"*BULLET:Practical for real-world shop work" & vbLf &
"" & vbLf &
"###SUBTITLE:Results Provided" & vbLf &
"*BULLET:Number of biscuits/dominos needed" & vbLf &
"*BULLET:Edge distance from each end (prevents exposure)" & vbLf &
"*BULLET:Recommended spacing between joinery" & vbLf &
"*BULLET:Complete list of center mark positions" & vbLf &
"*BULLET:Fractional measurements for shop layout" & vbLf &
"" & vbLf &
"###SUBTITLE:Using Center Mark Positions" & vbLf &
"The calculator provides numbered center mark positions:" & vbLf &
"" & vbLf &
"~STEP:1:Mark Reference Edge:Choose one edge as reference (usually left/bottom)" & vbLf &
"~STEP:2:Measure from Edge:Use tape measure from reference edge" & vbLf &
"~STEP:3:Mark Centers:Mark each position from the list" & vbLf &
"~STEP:4:Transfer to Mating Board:Use reference marks to align both pieces" & vbLf &
"~STEP:5:Cut Slots:Cut biscuit/domino slots at marked positions" & vbLf &
"" & vbLf &
"###SUBTITLE:Best Practices" & vbLf &
"*BULLET:Always dry-fit before gluing (check alignment)" & vbLf &
"*BULLET:Mark face sides to avoid reversed boards" & vbLf &
"*BULLET:Use alignment method (spacer blocks or jig)" & vbLf &
"*BULLET:Glue both sides of joint AND inside slots" & vbLf &
"*BULLET:Biscuits expand - don't let glue dry before assembly" & vbLf &
"*BULLET:Work quickly once glue is applied (5-10 min max)" & vbLf &
"" & vbLf &
"###SUBTITLE:Stock Thickness Requirements" & vbLf &
"Minimum thickness for each size:" & vbLf &
"" & vbLf &
"*BULLET:#0: 1/2 inch minimum" & vbLf &
"*BULLET:#10: 5/8 inch minimum" & vbLf &
"*BULLET:#20: 3/4 inch minimum (most common)" & vbLf &
"*BULLET:#FF / #H9: 7/8 inch minimum" & vbLf &
"" & vbLf &
"!WARNING:Using biscuits in thin stock will cause blow-out!" & vbLf &
"!WARNING:Always test biscuit depth setting on scrap first!" & vbLf &
"" & vbLf &
"?NOTE:Pro Tip: Biscuits are for alignment - the glue joint provides the strength!" & vbLf &
"?NOTE:For long joints (>36 inches), consider adding more biscuits near the middle"
},
                New DatabaseManager.HelpContentData With {
.ModuleName = "definitions",
.Title = "Woodworking Definitions & Glossary",
.Category = "Reference",
.SortOrder = 60,
.Keywords = "definitions,glossary,terms,apothem,circumradius,radius,interior angle,exterior angle,miter angle,kerf,dado,rabbet,moisture content,board feet,joinery,safety",
.Content =
"#TITLE:Woodworking Definitions & Glossary" & vbLf &
"##SECTION:Purpose|Quick reference for woodworking terms, geometry concepts, and technical definitions used throughout Woodworker's Friend." & vbLf &
"" & vbLf &
"###SUBTITLE:Geometry Terms" & vbLf &
"" & vbLf &
"@METHOD:Apothem|The perpendicular distance from the center of a regular polygon to the midpoint of any side (inradius)." & vbLf &
"=FORMULA:From side length: apothem = side_length / (2 × tan(π/n))" & vbLf &
"=FORMULA:From radius: apothem = radius × cos(π/n)" & vbLf &
"?NOTE:The apothem determines the 'flat-to-flat' measurement when building polygon shapes like hexagonal planters or octagonal frames." & vbLf &
"" & vbLf &
"@METHOD:Circumradius (Radius)|Distance from the center of a regular polygon to any vertex (corner)." & vbLf &
"=FORMULA:From side length: radius = side_length / (2 × sin(π/n))" & vbLf &
"?NOTE:This is the radius of the circle that passes through all vertices. Important for determining minimum board width needed." & vbLf &
"" & vbLf &
"@METHOD:Interior Angle|Angle formed inside a polygon where two adjacent sides meet at a vertex." & vbLf &
"=FORMULA:Interior Angle = (n - 2) × 180° / n" & vbLf &
"*BULLET:Triangle (3): 60°" & vbLf &
"*BULLET:Square (4): 90°" & vbLf &
"*BULLET:Hexagon (6): 120°" & vbLf &
"*BULLET:Octagon (8): 135°" & vbLf &
"" & vbLf &
"@METHOD:Exterior Angle|Central angle subtended by one side. Also the angle formed outside the polygon." & vbLf &
"=FORMULA:Exterior Angle = 360° / n" & vbLf &
"?NOTE:This is the angle you turn at each corner when walking around the polygon perimeter." & vbLf &
"" & vbLf &
"@METHOD:Miter Angle (Cut Angle)|The angle to cut each end of a segment so adjacent pieces meet perfectly." & vbLf &
"=FORMULA:Miter Angle = 180° / n = (Exterior Angle / 2)" & vbLf &
"*BULLET:Square (4): 45° miter" & vbLf &
"*BULLET:Hexagon (6): 30° miter" & vbLf &
"*BULLET:Octagon (8): 22.5° miter" & vbLf &
"!WARNING:Most miter saws display angles as deviation from 90°. For a hexagon, set saw to 30° (not 60°)." & vbLf &
"" & vbLf &
"@METHOD:Perimeter|Total distance around the outside of a polygon." & vbLf &
"=FORMULA:Perimeter = n × side_length" & vbLf &
"?NOTE:Tells you total linear footage needed. Add 10-20% waste for saw kerfs and mistakes." & vbLf &
"" & vbLf &
"@METHOD:Area|Total surface area enclosed by a polygon." & vbLf &
"=FORMULA:Area = (n × side²) / (4 × tan(π/n))" & vbLf &
"=FORMULA:Alternative: Area = (Perimeter × Apothem) / 2" & vbLf &
"" & vbLf &
"###SUBTITLE:Joinery Terms" & vbLf &
"" & vbLf &
"@METHOD:Board Feet|Unit of lumber volume equal to 144 cubic inches." & vbLf &
"=FORMULA:Board Feet = (Thickness × Width × Length) / 144" & vbLf &
"?NOTE:Standard unit for purchasing hardwood lumber. All dimensions in inches." & vbLf &
"" & vbLf &
"@METHOD:Kerf|Width of material removed by a saw blade during cutting." & vbLf &
"*BULLET:Table saw blade: 1/8 inch (0.125 in)" & vbLf &
"*BULLET:Thin-kerf blade: 3/32 inch (0.094 in)" & vbLf &
"?NOTE:Account for kerf when calculating how many pieces you can get from a board." & vbLf &
"" & vbLf &
"@METHOD:Dado|Rectangular groove cut across the grain, typically for shelving." & vbLf &
"?NOTE:Dado width should match the thickness of piece being inserted." & vbLf &
"" & vbLf &
"@METHOD:Rabbet|L-shaped step cut along edge or end of a board." & vbLf &
"?NOTE:Common for cabinet back panels and picture frames." & vbLf &
"" & vbLf &
"@METHOD:Biscuit|Oval compressed wood piece used to align and strengthen butt joints." & vbLf &
"*BULLET:#0: 5/8 in × 1-3/4 in" & vbLf &
"*BULLET:#10: 3/4 in × 2-1/8 in" & vbLf &
"*BULLET:#20: 1 in × 2-3/8 in (most common)" & vbLf &
"" & vbLf &
"###SUBTITLE:Wood Movement Terms" & vbLf &
"" & vbLf &
"@METHOD:Moisture Content (MC)|Percentage of water weight compared to oven-dry weight." & vbLf &
"=FORMULA:MC = ((Wet Weight - Dry Weight) / Dry Weight) × 100%" & vbLf &
"*BULLET:Furniture: 6-8% MC" & vbLf &
"*BULLET:Outdoor: 12-15% MC" & vbLf &
"*BULLET:Construction: 15-19% MC" & vbLf &
"" & vbLf &
"@METHOD:Equilibrium Moisture Content (EMC)|Moisture content where wood neither gains nor loses moisture to surrounding air." & vbLf &
"?NOTE:Allow wood to acclimate to your shop's EMC before final dimensioning to minimize movement after construction." & vbLf &
"" & vbLf &
"@METHOD:Tangential Movement|Wood movement parallel to growth rings (typically most significant)." & vbLf &
"?NOTE:Tangential movement is usually 1.5-2× greater than radial movement. This is why flat-sawn boards cup more." & vbLf &
"" & vbLf &
"@METHOD:Radial Movement|Wood movement perpendicular to growth rings." & vbLf &
"?NOTE:Quarter-sawn lumber experiences primarily radial movement, which is more stable and consistent." & vbLf &
"" & vbLf &
"###SUBTITLE:Safety Terms" & vbLf &
"" & vbLf &
"@METHOD:Kickback|Dangerous condition where workpiece is violently thrown back toward operator by spinning blade." & vbLf &
"!WARNING:Prevention: Use riving knife, never stand behind blade, use proper blade guard, maintain sharp blades." & vbLf &
"" & vbLf &
"@METHOD:Push Stick|Safety device to push material through blade while keeping hands at safe distance." & vbLf &
"*BULLET:Use when ripping material less than 6 inches wide" & vbLf &
"*BULLET:Use when material is less than 3 inches from blade" & vbLf &
"*BULLET:Use anytime hands would be uncomfortably close to blade" & vbLf &
"" & vbLf &
"###SUBTITLE:Finishing Terms" & vbLf &
"" & vbLf &
"@METHOD:Grit Progression|Sequence of sandpaper grits used when smoothing wood, typically doubling or increasing by 50% between grits." & vbLf &
"*BULLET:Typical progression: 80 → 120 → 180 → 220 → 320" & vbLf &
"!WARNING:Skipping grits leaves scratches from coarser paper that show through finish!" & vbLf &
"?NOTE:Each grit should remove the scratches from previous grit." & vbLf &
"" & vbLf &
"@METHOD:Open Time|Working time available after applying glue before it begins to set." & vbLf &
"*BULLET:PVA/Yellow Glue: 5-10 minutes" & vbLf &
"*BULLET:Polyurethane: 15-20 minutes" & vbLf &
"*BULLET:Epoxy: 5-60 minutes (varies by formula)" & vbLf &
"?NOTE:Temperature affects open time - cold extends it, heat shortens it." & vbLf &
"" & vbLf &
"@METHOD:Clamp Time|Minimum time joints should remain clamped for glue to achieve handling strength." & vbLf &
"*BULLET:PVA Glue: 30-60 minutes" & vbLf &
"*BULLET:Polyurethane: 4-6 hours" & vbLf &
"*BULLET:Epoxy: Varies (check manufacturer)" & vbLf &
"!WARNING:Full cure strength (24-72 hours) is much longer than clamp time!" & vbLf &
"" & vbLf &
"@METHOD:Book Match|Veneer matching technique where consecutive sheets are opened like a book to create mirror-image patterns." & vbLf &
"?NOTE:Creates symmetrical grain patterns, commonly used for tabletops and door panels." & vbLf &
"" & vbLf &
"@METHOD:Slip Match|Veneer matching technique where consecutive sheets are laid side-by-side without flipping." & vbLf &
"?NOTE:Creates repeating patterns, useful when you want grain to flow in same direction." & vbLf &
"" & vbLf &
"@METHOD:Coverage Rate|Amount of surface area a finish will cover per unit volume, typically sq ft per quart or gallon." & vbLf &
"*BULLET:Polyurethane: ~125 sq ft/qt" & vbLf &
"*BULLET:Stain: ~200 sq ft/gal" & vbLf &
"?NOTE:Porous woods absorb more finish, reducing effective coverage."
}
            }

            ' Bulk insert all help content
            Dim insertedCount = DatabaseManager.Instance.BulkInsertHelpContent(helpItems)
            ErrorHandler.LogError(New Exception($"Help content migration: {insertedCount}/{helpItems.Count} topics inserted"), "MigrateHelpContent")
            Return insertedCount
        Catch ex As Exception
            ErrorHandler.LogError(ex, "MigrateHelpContent")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Seeds default user preferences into the database (Phase 5).
    ''' These are the initial values for a first-time user.
    ''' </summary>
    Public Shared Sub SeedDefaultPreferences()
        Try
            Dim db = DatabaseManager.Instance

            ' UI Preferences
            db.SavePreference("Theme", "Light", "String", "UI")
            db.SavePreference("Scale", "Imperial", "String", "UI")

            ' Window state (will be updated on close)
            db.SavePreference("WindowState", "Normal", "String", "UI")
            db.SavePreference("WindowWidth", "1200", "Integer", "UI")
            db.SavePreference("WindowHeight", "800", "Integer", "UI")

            ' Calculator defaults
            db.SavePreference("DefaultWastePercent", "10", "Integer", "Calculation")
            db.SavePreference("DefaultKerfWidth", "0.125", "Double", "Calculation")

            ' General
            db.SavePreference("LastActiveTab", "0", "Integer", "General")

            ErrorHandler.LogError(New Exception("Default preferences seeded successfully"), "SeedDefaultPreferences")
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SeedDefaultPreferences")
        End Try
    End Sub

    ''' <summary>
    ''' Migrates joinery types reference data to database (Phase 7.1)
    ''' </summary>
    Public Shared Function MigrateJoineryTypes() As Integer
        Try
            ErrorHandler.LogError(New Exception("Starting joinery types migration..."), "MigrateJoineryTypes")

            ' Check if already seeded
            Dim existing = DatabaseManager.Instance.GetAllJoineryTypes()
            If existing.Count > 0 Then
                ErrorHandler.LogError(New Exception($"Joinery types already migrated ({existing.Count} types found)"), "MigrateJoineryTypes")
                Return existing.Count
            End If

            ' Create joinery types list
            ' ===== MORTISE & TENON =====
            ' ===== DOVETAIL - THROUGH =====
            ' ===== DOVETAIL - HALF-BLIND =====
            ' ===== BOX JOINT (FINGER JOINT) =====
            ' ===== DADO JOINT =====
            ' ===== RABBET JOINT =====
            ' ===== LAP JOINT =====
            ' ===== BRIDLE JOINT =====
            ' ===== BISCUIT JOINT =====
            ' ===== DOWEL JOINT =====
            ' ===== POCKET HOLE =====
            ' ===== SPLINE JOINT =====
            Dim joineryList As New List(Of JoineryType) From {
                New JoineryType With {
.Name = "Mortise & Tenon",
.Category = JoineryCategory.Frame,
.StrengthRating = 10,
.DifficultyLevel = JoineryDifficulty.Intermediate,
.Description = "One of the strongest and most versatile joints in woodworking. A rectangular tenon fits into a matching mortise.",
.TypicalUses = "Frame and panel construction, table legs, chair frames, door frames, mission-style furniture",
.RequiredTools = "Mortiser, chisel, router, table saw, tenon saw",
.StrengthCharacteristics = "Excellent in tension, compression, and shear. Large glue surface area provides maximum strength.",
.GlueRequired = True,
.ReinforcementOptions = "Through wedges, drawbore pegs, pins",
.HistoricalNotes = "Used for thousands of years. Found in ancient Egyptian furniture and medieval timber framing."
},
                New JoineryType With {
.Name = "Dovetail (Through)",
.Category = JoineryCategory.Box,
.StrengthRating = 10,
.DifficultyLevel = JoineryDifficulty.Advanced,
.Description = "Interlocking wedge-shaped pins and tails. Visible from both sides. The joint gets stronger under tension.",
.TypicalUses = "Drawer boxes, jewelry boxes, fine furniture carcasses, decorative boxes",
.RequiredTools = "Dovetail saw, coping saw, chisel, marking gauge, dovetail jig (optional)",
.StrengthCharacteristics = "Exceptional mechanical strength. Resists pulling apart without glue. Self-tightening under load.",
.GlueRequired = False,
.ReinforcementOptions = "Typically used alone. Sometimes pinned in large timbers.",
.HistoricalNotes = "Dates to ancient Egypt. Became signature of fine craftsmanship in 18th century furniture."
},
                New JoineryType With {
.Name = "Dovetail (Half-Blind)",
.Category = JoineryCategory.Box,
.StrengthRating = 9,
.DifficultyLevel = JoineryDifficulty.Advanced,
.Description = "Dovetails hidden on one face. Pins are cut only partway through the tail board.",
.TypicalUses = "Drawer fronts where you don't want visible joinery, fine cabinet work",
.RequiredTools = "Dovetail saw, coping saw, chisel, marking gauge, router with dovetail jig",
.StrengthCharacteristics = "Nearly as strong as through dovetails. Slightly less glue surface.",
.GlueRequired = True,
.ReinforcementOptions = "Rarely reinforced due to complexity",
.HistoricalNotes = "Developed to hide joinery on drawer fronts. Became standard for fine furniture drawers."
},
                New JoineryType With {
.Name = "Box Joint (Finger Joint)",
.Category = JoineryCategory.Box,
.StrengthRating = 8,
.DifficultyLevel = JoineryDifficulty.Beginner,
.Description = "Square interlocking fingers. Machine-cut precision provides large glue surface area.",
.TypicalUses = "Shop-made boxes, tool boxes, drawers, small cabinets",
.RequiredTools = "Table saw with box joint jig, router with template",
.StrengthCharacteristics = "Very strong due to large glue surface. Not as decorative as dovetails but easier to cut.",
.GlueRequired = True,
.ReinforcementOptions = "Pins through fingers (decorative)",
.HistoricalNotes = "Popularized with power tools in 20th century as a simpler alternative to dovetails."
},
                New JoineryType With {
.Name = "Dado",
.Category = JoineryCategory.Carcass,
.StrengthRating = 7,
.DifficultyLevel = JoineryDifficulty.Beginner,
.Description = "A groove cut across the grain to receive another board. Provides excellent alignment and strength.",
.TypicalUses = "Bookshelf shelves, cabinet dividers, drawer bottoms",
.RequiredTools = "Router, dado stack on table saw, straight bit",
.StrengthCharacteristics = "Good strength in compression. Prevents racking. Provides positive alignment.",
.GlueRequired = True,
.ReinforcementOptions = "Screws, nails, pins through the dado",
.HistoricalNotes = "One of the oldest joinery methods. Simple and effective for shelf support."
},
                New JoineryType With {
.Name = "Rabbet",
.Category = JoineryCategory.Edge,
.StrengthRating = 5,
.DifficultyLevel = JoineryDifficulty.Beginner,
.Description = "L-shaped cut along the edge of a board. Simple joint with good glue surface.",
.TypicalUses = "Cabinet backs, drawer backs, picture frames, panel inserts",
.RequiredTools = "Router, table saw, rabbet plane",
.StrengthCharacteristics = "Moderate strength. Primarily relies on glue. Good for alignment.",
.GlueRequired = True,
.ReinforcementOptions = "Brad nails, screws, staples",
.HistoricalNotes = "Used in panel construction for centuries. Common in traditional cabinetry."
},
                New JoineryType With {
.Name = "Lap Joint (Half-Lap)",
.Category = JoineryCategory.Frame,
.StrengthRating = 6,
.DifficultyLevel = JoineryDifficulty.Beginner,
.Description = "Two boards overlap with half the thickness removed from each. Creates flush surface.",
.TypicalUses = "Face frames, stretchers, cross-bracing, Japanese joinery",
.RequiredTools = "Table saw, router, dado stack, chisel",
.StrengthCharacteristics = "Good for frames. Better with mechanical fasteners. Large glue surface.",
.GlueRequired = True,
.ReinforcementOptions = "Screws, bolts, pins, pegs",
.HistoricalNotes = "Traditional timber framing joint. Used in both Western and Japanese carpentry."
},
                New JoineryType With {
.Name = "Bridle Joint",
.Category = JoineryCategory.Frame,
.StrengthRating = 8,
.DifficultyLevel = JoineryDifficulty.Intermediate,
.Description = "Open mortise and tenon. Fork-like joint where tenon sits between two prongs.",
.TypicalUses = "Leg-to-rail connections, chair frames, table aprons",
.RequiredTools = "Table saw, band saw, chisel, router",
.StrengthCharacteristics = "Strong in compression and tension. Large glue surface. Resists racking.",
.GlueRequired = True,
.ReinforcementOptions = "Wedges, pins",
.HistoricalNotes = "Common in Arts & Crafts furniture. Visible joinery is part of the aesthetic."
},
                New JoineryType With {
.Name = "Biscuit Joint",
.Category = JoineryCategory.Edge,
.StrengthRating = 6,
.DifficultyLevel = JoineryDifficulty.Beginner,
.Description = "Compressed wood wafers expand in glue-filled slots. Provides alignment and some strength.",
.TypicalUses = "Edge-to-edge panels, miter joints, case construction",
.RequiredTools = "Biscuit joiner (plate joiner)",
.StrengthCharacteristics = "Primarily for alignment. Adds moderate strength. Not structural on its own.",
.GlueRequired = True,
.ReinforcementOptions = "Used with glue joints, rarely reinforced further",
.HistoricalNotes = "Invented in Switzerland in 1950s. Revolutionized cabinet making."
},
                New JoineryType With {
.Name = "Dowel Joint",
.Category = JoineryCategory.Frame,
.StrengthRating = 7,
.DifficultyLevel = JoineryDifficulty.Intermediate,
.Description = "Round wooden pegs fit into matching holes. Provides alignment and mechanical strength.",
.TypicalUses = "Chair joints, frame corners, edge gluing, cabinet face frames",
.RequiredTools = "Drill, doweling jig, dowel centers, clamps",
.StrengthCharacteristics = "Good strength if aligned properly. Multiple dowels distribute load. Relies on tight fit and glue.",
.GlueRequired = True,
.ReinforcementOptions = "Multiple dowels, larger diameter",
.HistoricalNotes = "Ancient technique. Mass-produced furniture often uses dowels instead of mortise & tenon."
},
                New JoineryType With {
.Name = "Pocket Hole",
.Category = JoineryCategory.Modern,
.StrengthRating = 6,
.DifficultyLevel = JoineryDifficulty.Beginner,
.Description = "Angled screw driven through pocket hole. Fast and strong. Hidden on back face.",
.TypicalUses = "Face frames, cabinet construction, quick assemblies",
.RequiredTools = "Pocket hole jig (Kreg), drill, special screws, clamps",
.StrengthCharacteristics = "Good for face frames. Not ideal for high-stress joints. Quick and repeatable.",
.GlueRequired = False,
.ReinforcementOptions = "Glue added for extra strength",
.HistoricalNotes = "Popularized by Kreg Tool in 1980s. Now standard in production cabinetry."
},
                New JoineryType With {
.Name = "Spline Joint",
.Category = JoineryCategory.Edge,
.StrengthRating = 7,
.DifficultyLevel = JoineryDifficulty.Intermediate,
.Description = "Thin strip of wood fits into matching grooves. Reinforces miter joints and edge glue-ups.",
.TypicalUses = "Miter joints on boxes, breadboard ends, edge-to-edge panels",
.RequiredTools = "Table saw, router, biscuit joiner",
.StrengthCharacteristics = "Strengthens weak miter joints. Provides cross-grain support. Aids alignment.",
.GlueRequired = True,
.ReinforcementOptions = "Multiple splines, keys on miters",
.HistoricalNotes = "Traditional method for strengthening miter joints and breadboard ends."
}
            }

            ' Insert all into database
            Dim inserted = 0
            For Each joinery In joineryList
                If DatabaseManager.Instance.AddJoineryType(joinery) Then
                    inserted += 1
                End If
            Next

            ErrorHandler.LogError(New Exception($"Joinery migration complete: {inserted}/{joineryList.Count} types inserted"), "MigrateJoineryTypes")
            Return inserted
        Catch ex As Exception
            ErrorHandler.LogError(ex, "MigrateJoineryTypes")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Migrates hardware standards reference data to database (Phase 7.2)
    ''' </summary>
    Public Shared Function MigrateHardwareStandards() As Integer
        Try
            ErrorHandler.LogError(New Exception("Starting hardware standards migration..."), "MigrateHardwareStandards")

            ' Check if already seeded
            Dim existing = DatabaseManager.Instance.GetAllHardwareStandards()
            If existing.Count > 0 Then
                ErrorHandler.LogError(New Exception($"Hardware already migrated ({existing.Count} items found)"), "MigrateHardwareStandards")
                Return existing.Count
            End If

            ' Create hardware list with common items
            ' ===== HINGES =====
            ' ===== DRAWER SLIDES =====
            ' ===== SHELF SUPPORT =====
            ' ===== BRACKETS =====
            ' ===== FASTENERS =====
            ' ===== PULLS & KNOBS =====
            ' ===== TABLE LEGS =====
            ' ===== CASTERS =====
            Dim hardwareList As New List(Of HardwareStandard) From {
                New HardwareStandard With {
.Category = HardwareCategory.Hinges,
.Type = "European (Euro) Hinge - 107°",
.Brand = "Blum",
.Description = "Concealed cabinet hinge with 107° opening angle. Most common cabinet hinge type.",
.Dimensions = "35mm cup diameter, various mounting plates",
.MountingRequirements = "35mm bore hole, 3-4mm depth. Requires mounting plate on cabinet side.",
.TypicalUses = "Face frame and frameless cabinets, overlay doors, kitchen cabinets",
.InstallationNotes = "Use 35mm Forstner bit for cup hole. Allows 3-way adjustment (in/out, up/down, left/right)."
},
                New HardwareStandard With {
.Category = HardwareCategory.Hinges,
.Type = "Butt Hinge - 2"" x 1.5""",
.Description = "Traditional surface-mounted hinge. Visible on face frame cabinets.",
.Dimensions = "2"" height x 1.5"" width (when open)",
.MountingRequirements = "Mortise into door and frame. Typically 1/16"" to 1/8"" deep.",
.TypicalUses = "Face frame cabinets, inset doors, traditional furniture",
.InstallationNotes = "Use chisel to create mortise. Three hinges for doors over 60"" tall."
},
                New HardwareStandard With {
.Category = HardwareCategory.Hinges,
.Type = "Overlay Hinge - Non-Mortise",
.Description = "Surface-mounted overlay hinge. No mortising required.",
.Dimensions = "Various sizes, typically 3/8"" or 1/2"" overlay",
.MountingRequirements = "Screws directly to door and face frame. No mortise needed.",
.TypicalUses = "Utility cabinets, shop cabinets, quick builds",
.InstallationNotes = "Easier to install than mortised hinges. Less adjustment range."
},
                New HardwareStandard With {
.Category = HardwareCategory.Slides,
.Type = "Full Extension Ball-Bearing Slide - Side Mount",
.Brand = "Blum, Accuride",
.Description = "Drawer extends fully for complete access. Ball-bearing rollers for smooth operation.",
.Dimensions = "Lengths: 12"", 14"", 16"", 18"", 20"", 22"", 24"". Width: 1/2"" per side.",
.WeightCapacity = "75-100 lbs per pair (varies by length)",
.MountingRequirements = "1/2"" clearance each side. Screws to drawer side and cabinet.",
.TypicalUses = "Kitchen drawers, tool drawers, file cabinets",
.InstallationNotes = "Drawer width = opening width - 1"". Use spacers for exact alignment."
},
                New HardwareStandard With {
.Category = HardwareCategory.Slides,
.Type = "Undermount Soft-Close Slide",
.Brand = "Blum Tandem, Grass",
.Description = "Hidden slides mount under drawer. Soft-close mechanism prevents slamming.",
.Dimensions = "Lengths: 12""-24"". Requires specific drawer box height.",
.WeightCapacity = "75-100 lbs per pair",
.MountingRequirements = "Mounts to drawer bottom. Requires specific drawer box dimensions per manufacturer.",
.TypicalUses = "High-end kitchen cabinets, furniture, clean aesthetic installations",
.InstallationNotes = "More complex installation. Follow manufacturer templates exactly."
},
                New HardwareStandard With {
.Category = HardwareCategory.Shelf,
.Type = "Shelf Pin - 5mm",
.Description = "Standard shelf support pin. Fits into drilled holes for adjustable shelving.",
.Dimensions = "5mm diameter x 16-20mm length",
.MountingRequirements = "5mm diameter holes, typically 1-2"" from edges, 32mm or 50mm spacing",
.TypicalUses = "Adjustable shelves in cabinets and bookcases",
.InstallationNotes = "Use shelf pin jig for accurate hole spacing. 4 pins per shelf minimum."
},
                New HardwareStandard With {
.Category = HardwareCategory.Shelf,
.Type = "Shelf Pin - 1/4""",
.Description = "Imperial shelf support pin. Common in North America.",
.Dimensions = "1/4"" diameter x 5/8"" to 3/4"" length",
.MountingRequirements = "1/4"" diameter holes, 1-2"" from edges",
.TypicalUses = "Adjustable shelves, bookcase standards",
.InstallationNotes = "Drill holes perpendicular to surface. Use brad point bit for clean holes."
},
                New HardwareStandard With {
.Category = HardwareCategory.Brackets,
.Type = "Corner Brace - 2"" x 2""",
.Description = "L-shaped metal bracket for reinforcing corners.",
.Dimensions = "2"" x 2"" with screw holes",
.TypicalUses = "Reinforcing case corners, securing table legs, picture frames",
.InstallationNotes = "Use on inside corners. Countersink screws if visible."
},
                New HardwareStandard With {
.Category = HardwareCategory.Brackets,
.Type = "Table Leg Bracket - Angled",
.Description = "Steel bracket for attaching table legs. Allows angle adjustment.",
.Dimensions = "4"" mounting surface, accommodates 1.5""-3"" legs",
.MountingRequirements = "4 screws to underside of top. Center on leg position.",
.TypicalUses = "Table leg attachment, workbench legs",
.InstallationNotes = "Ensure bracket is square to top. Use lag screws into leg."
},
                New HardwareStandard With {
.Category = HardwareCategory.Fasteners,
.Type = "Wood Screw - #8 x 1.5""",
.Description = "General purpose wood screw. Tapered head for countersinking.",
.Dimensions = "#8 diameter (0.164""), 1.5"" length",
.TypicalUses = "Face frame attachment, hinge mounting, general assembly",
.InstallationNotes = "Pilot hole: 1/8"" for hardwood, 3/32"" for softwood."
},
                New HardwareStandard With {
.Category = HardwareCategory.Fasteners,
.Type = "Confirmat Screw - 5mm x 50mm",
.Description = "European connector screw for cabinet construction. Coarse thread, large head.",
.Dimensions = "5mm diameter, 50mm length (most common)",
.MountingRequirements = "Requires specific drill bit (5mm shank, 7mm counterbore)",
.TypicalUses = "Frameless cabinet construction, connecting cabinet panels",
.InstallationNotes = "Use Confirmat drill bit. Screws into 8mm pilot hole."
},
                New HardwareStandard With {
.Category = HardwareCategory.Pulls,
.Type = "Bar Pull - 3"" Center-to-Center",
.Description = "Cabinet drawer pull. Modern straight bar design.",
.Dimensions = "3"" hole spacing (76mm), 4-5"" overall length",
.MountingRequirements = "Two 3/32"" or 1/8"" holes, 3"" apart",
.TypicalUses = "Kitchen cabinets, drawer fronts, modern furniture",
.InstallationNotes = "Use template for consistent placement. 2.5""-3"" from bottom edge is standard."
},
                New HardwareStandard With {
.Category = HardwareCategory.Pulls,
.Type = "Knob - 1.25"" Diameter",
.Description = "Round cabinet knob. Single-screw mounting.",
.Dimensions = "1.25"" diameter, 1"" projection",
.MountingRequirements = "Single 1/8"" hole, centered on door/drawer",
.TypicalUses = "Traditional cabinets, doors, drawers",
.InstallationNotes = "Center on door stile or drawer front. Use backing washer for thin fronts."
},
                New HardwareStandard With {
.Category = HardwareCategory.Legs,
.Type = "Tapered Table Leg - 29""",
.Description = "Wooden table leg with taper. Standard dining table height.",
.Dimensions = "29"" length, 2"" square at top, tapers to 1.5"" at bottom",
.MountingRequirements = "Apron rail attachment or leg mounting plate",
.TypicalUses = "Dining tables, desks, workbenches",
.InstallationNotes = "Add glides to bottom for floor protection. Account for top thickness."
},
                New HardwareStandard With {
.Category = HardwareCategory.Casters,
.Type = "Swivel Caster - 3"" Wheel",
.Description = "Rotating wheel for shop furniture. Lockable versions available.",
.Dimensions = "3"" wheel diameter, 3.5"" overall height, 2"" x 2"" mounting plate",
.WeightCapacity = "75-150 lbs per caster",
.MountingRequirements = "4 screws through mounting plate",
.TypicalUses = "Mobile workbenches, tool carts, assembly tables",
.InstallationNotes = "Use locking casters on 2 wheels minimum. Add threaded inserts for easier replacement."
}
            }

            ' Insert all into database
            Dim inserted = 0
            For Each hardware In hardwareList
                If DatabaseManager.Instance.AddHardwareStandard(hardware) Then
                    inserted += 1
                End If
            Next

            ErrorHandler.LogError(New Exception($"Hardware migration complete: {inserted}/{hardwareList.Count} items inserted"), "MigrateHardwareStandards")
            Return inserted
        Catch ex As Exception
            ErrorHandler.LogError(ex, "MigrateHardwareStandards")
            Return 0
        End Try
    End Function

#Region "Cost Data Migration (Phase 7.3)"

    ''' <summary>
    ''' Migrates wood cost data from bfCost.csv to database
    ''' </summary>
    Public Shared Function MigrateWoodCosts() As Integer
        Try
            ErrorHandler.LogError(New Exception("Starting wood costs migration from CSV..."), "MigrateWoodCosts")

            Dim csvPath As String = IO.Path.Combine(SetDir, "bfCost.csv")
            If Not IO.File.Exists(csvPath) Then
                ErrorHandler.LogError(New Exception($"CSV file not found: {csvPath}"), "MigrateWoodCosts")
                Return 0
            End If

            Dim woodCosts As New List(Of WoodCost)()
            Dim lines = IO.File.ReadAllLines(csvPath)

            ' Parse CSV
            For Each line In lines
                If String.IsNullOrWhiteSpace(line) Then Continue For

                Try
                    ' Parse CSV line with quoted fields
                    Dim parts = ParseCsvLine(line)
                    If parts.Length >= 3 Then
                        Dim thickness = parts(0).Replace("""", "").Trim()
                        Dim woodName = parts(1).Replace("""", "").Trim()
                        ' Convert wood name to Title Case (CamelCase)
                        woodName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(woodName.ToLower())
                        Dim costString = parts(2).Replace("$", "").Replace("""", "").Trim()

                        Dim cost As Double
                        If Double.TryParse(costString, cost) Then
                            woodCosts.Add(New WoodCost With {
                                .Thickness = thickness,
                                .WoodName = woodName,
                                .CostPerBoardFoot = cost,
                                .Active = True,
                                .IsUserAdded = False
                            })
                        End If
                    End If
                Catch ex As Exception
                    ErrorHandler.LogError(ex, $"MigrateWoodCosts - Failed to parse line: {line}")
                End Try
            Next

            ' Insert into database
            Dim inserted = 0
            For Each woodCost In woodCosts
                If DatabaseManager.Instance.AddWoodCost(woodCost) Then
                    inserted += 1
                End If
            Next

            ErrorHandler.LogError(New Exception($"Wood costs migration complete: {inserted}/{woodCosts.Count} items inserted"), "MigrateWoodCosts")
            Return inserted
        Catch ex As Exception
            ErrorHandler.LogError(ex, "MigrateWoodCosts")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Migrates epoxy cost data from epoxyCost.csv to database
    ''' </summary>
    Public Shared Function MigrateEpoxyCosts() As Integer
        Try
            ErrorHandler.LogError(New Exception("Starting epoxy costs migration from CSV..."), "MigrateEpoxyCosts")

            Dim csvPath As String = IO.Path.Combine(SetDir, "epoxyCost.csv")
            If Not IO.File.Exists(csvPath) Then
                ErrorHandler.LogError(New Exception($"CSV file not found: {csvPath}"), "MigrateEpoxyCosts")
                Return 0
            End If

            Dim epoxyCosts As New List(Of EpoxyCost)()
            Dim lines = IO.File.ReadAllLines(csvPath)

            ' Parse CSV
            For Each line In lines
                If String.IsNullOrWhiteSpace(line) Then Continue For

                Try
                    Dim parts = line.Split(","c)
                    If parts.Length >= 3 Then
                        Dim brand = parts(0).Trim()
                        Dim type = parts(1).Trim()
                        Dim costString = parts(2).Replace("$", "").Trim()

                        Dim cost As Double
                        If Double.TryParse(costString, cost) Then
                            Dim displayText = $"{brand} {type} - ${cost:F2}/gal"
                            epoxyCosts.Add(New EpoxyCost With {
                                .Brand = brand,
                                .Type = type,
                                .CostPerGallon = cost,
                                .DisplayText = displayText,
                                .Active = True,
                                .IsUserAdded = False
                            })
                        End If
                    End If
                Catch ex As Exception
                    ErrorHandler.LogError(ex, $"MigrateEpoxyCosts - Failed to parse line: {line}")
                End Try
            Next

            ' Insert into database
            Dim inserted = 0
            For Each epoxyCost In epoxyCosts
                If DatabaseManager.Instance.AddEpoxyCost(epoxyCost) Then
                    inserted += 1
                End If
            Next

            ErrorHandler.LogError(New Exception($"Epoxy costs migration complete: {inserted}/{epoxyCosts.Count} items inserted"), "MigrateEpoxyCosts")
            Return inserted
        Catch ex As Exception
            ErrorHandler.LogError(ex, "MigrateEpoxyCosts")
            Return 0
        End Try
    End Function

    ''' <summary>
    ''' Helper method to parse CSV lines with quoted fields
    ''' </summary>
    Private Shared Function ParseCsvLine(line As String) As String()
        Dim result As New List(Of String)
        Dim currentField As New Text.StringBuilder()
        Dim inQuotes As Boolean = False

        For i As Integer = 0 To line.Length - 1
            Dim c = line(i)

            If c = """"c Then
                inQuotes = Not inQuotes
            ElseIf c = ","c AndAlso Not inQuotes Then
                result.Add(currentField.ToString())
                currentField.Clear()
            Else
                currentField.Append(c)
            End If
        Next

        ' Add the last field
        result.Add(currentField.ToString())

        Return result.ToArray()
    End Function

    ''' <summary>
    ''' Checks if wood costs have been migrated to database
    ''' </summary>
    Public Shared Function IsWoodCostsMigrated() As Boolean
        Try
            Dim costs = DatabaseManager.Instance.GetAllWoodCosts()
            Return costs.Count > 0
        Catch ex As Exception
            ErrorHandler.LogError(ex, "IsWoodCostsMigrated")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Checks if epoxy costs have been migrated to database
    ''' </summary>
    Public Shared Function IsEpoxyCostsMigrated() As Boolean
        Try
            Dim costs = DatabaseManager.Instance.GetAllEpoxyCosts()
            Return costs.Count > 0
        Catch ex As Exception
            ErrorHandler.LogError(ex, "IsEpoxyCostsMigrated")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Converts all existing wood cost names from UPPERCASE to Title Case
    ''' Phase 7.3 - One-time conversion utility
    ''' </summary>
    Public Shared Function ConvertWoodCostsToTitleCase() As Integer
        Try
            ErrorHandler.LogError(New Exception("Converting wood cost names to Title Case..."), "ConvertWoodCostsToTitleCase")

            Dim allCosts = DatabaseManager.Instance.GetAllWoodCosts()
            Dim convertedCount = 0

            For Each cost In allCosts
                ' Check if name is all uppercase or has multiple uppercase letters
                If cost.WoodName.Equals(cost.WoodName, StringComparison.CurrentCultureIgnoreCase) OrElse cost.WoodName.Count(Function(c) Char.IsUpper(c)) > 1 Then
                    ' Convert to Title Case
                    cost.WoodName = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(cost.WoodName.ToLower())

                    ' Update in database
                    If DatabaseManager.Instance.UpdateWoodCost(cost) Then
                        convertedCount += 1
                    End If
                End If
            Next

            ErrorHandler.LogError(New Exception($"Wood cost name conversion complete: {convertedCount}/{allCosts.Count} items converted"), "ConvertWoodCostsToTitleCase")
            Return convertedCount
        Catch ex As Exception
            ErrorHandler.LogError(ex, "ConvertWoodCostsToTitleCase")
            Return 0
        End Try
    End Function

#End Region

End Class
