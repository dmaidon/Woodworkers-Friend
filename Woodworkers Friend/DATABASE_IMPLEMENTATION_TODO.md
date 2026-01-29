# Woodworkers Friend - Unified Database Implementation TODO

## Date Created: January 29, 2026
## Status: Planning Phase - Ready to Begin Implementation

---

## 📋 EXECUTIVE SUMMARY

**Goal:** Migrate from in-code databases to unified SQLite database for application-wide data management.

**Benefits:**
- Eliminate duplicate wood species data (currently in 2 separate modules)
- Enable user customization (add custom wood species, save calculations)
- Revolutionize help system (searchable, context-sensitive)
- Enable cross-module intelligence (one species = all properties available everywhere)
- Foundation for future features (calculation history, project templates, material presets)

**Technology:** SQLite (lightweight, file-based, no server, 1.5MB footprint)

---

## ✅ COMPLETED TODAY (January 29, 2026)

- [x] Wood Properties Reference UI implemented (DataGridView-based)
- [x] 17 wood species with comprehensive data
- [x] Column sorting functionality (click headers to sort)
- [x] Search and filter capabilities (All/Hardwoods/Softwoods)
- [x] Detailed species information display
- [x] Export to CSV functionality
- [x] Tooltips for all controls
- [x] **DATABASE CORRUPTED** - WoodPropertiesDatabase.vb has syntax errors from PowerShell escaping issue
  - File has literal `\n` strings instead of line breaks
  - Need to recreate clean version with all 23 species (17 original + 6 user-requested)

---

## 🚨 IMMEDIATE PRIORITY - FIX DATABASE FILE

### Step 0: Fix Corrupted WoodPropertiesDatabase.vb (Do This First!)

**Problem:** File has literal `\n\n` strings from failed PowerShell replacement.

**Solution Options:**

#### Option A: Recreate from Scratch (Recommended - 30 minutes)
1. Delete current `WoodPropertiesDatabase.vb`
2. Create new clean file with proper `woodList.Add()` syntax
3. Include all 23 species:
   - Original 17: Ash, Basswood, Beech, Birch, Cedar, Cherry, Cypress, Douglas Fir, Hickory, Mahogany, Maple (Hard), Oak (Red), Oak (White), Pine (Eastern White), Pine (Southern Yellow), Poplar, Walnut
   - User-requested 6: Purpleheart, Padauk, Yellowheart, Tigerwood, Maple (Soft/Red), Birch (Paper)
4. Test compilation
5. Test grid population

#### Option B: Manual Fix (Time-consuming)
1. Find/Replace all `})\n\n            woodList.Add(` with proper line breaks
2. Verify indentation
3. Test compilation

**Location:** `Woodworkers Friend\Modules\References\WoodPropertiesDatabase.vb`

**Testing:**
- [ ] File compiles without errors
- [ ] Grid shows all 23 species on app launch
- [ ] Sorting works on all columns
- [ ] Filtering works (All/Hardwoods/Softwoods)
- [ ] Details display when clicking species

---

## 📅 PHASE 1: DATABASE FOUNDATION (Estimated: 4-6 hours)

### Goal: Install SQLite and create unified database schema

### Step 1.1: Install SQLite (15 minutes)
- [ ] Open NuGet Package Manager in Visual Studio
- [ ] Search for "System.Data.SQLite"
- [ ] Install latest stable version
- [ ] Verify installation (check References)
- [ ] Add `Imports System.Data.SQLite` to relevant files

**References:**
- NuGet Package: `System.Data.SQLite` or `System.Data.SQLite.Core`
- Documentation: https://system.data.sqlite.org/

### Step 1.2: Design Database Schema (1 hour)
- [ ] Review existing data structures:
  - `WoodPropertiesData` class (References\WoodPropertiesModels.vb)
  - `WoodSpecies` class (WoodMovement\WoodSpeciesDatabase.vb)
- [ ] Create unified `WoodSpecies` table schema combining both
- [ ] Create `HelpContent` table schema
- [ ] Create `UserPreferences` table schema
- [ ] Document schema in `DATABASE_SCHEMA.md`

**Schema File Location:** `Woodworkers Friend\DATABASE_SCHEMA.md`

**Key Tables for Phase 1:**
```sql
-- Consolidate both wood databases
CREATE TABLE WoodSpecies (
    SpeciesID INTEGER PRIMARY KEY AUTOINCREMENT,
    CommonName TEXT NOT NULL,
    ScientificName TEXT,
    WoodType TEXT CHECK(WoodType IN ('Hardwood', 'Softwood')),
    
    -- Physical Properties (from Wood Properties module)
    JankaHardness INTEGER,
    SpecificGravity REAL,
    Density INTEGER,
    MoistureContent REAL,
    
    -- Movement Properties (from Wood Movement module)
    ShrinkageRadial REAL,
    ShrinkageTangential REAL,
    
    -- Reference Data
    TypicalUses TEXT,
    Workability TEXT,
    Cautions TEXT,
    Notes TEXT,
    
    -- Metadata
    IsUserAdded BOOLEAN DEFAULT 0,
    DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
    LastModified DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Index for common queries
CREATE INDEX idx_woodtype ON WoodSpecies(WoodType);
CREATE INDEX idx_commonname ON WoodSpecies(CommonName);
```

### Step 1.3: Create DatabaseManager Class (2 hours)
- [ ] Create new file: `Modules\Database\DatabaseManager.vb`
- [ ] Implement Singleton pattern
- [ ] Add connection management
- [ ] Add error handling
- [ ] Create basic CRUD methods:
  - `GetAllWoodSpecies()` → List(Of WoodSpecies)
  - `GetWoodSpeciesByType(woodType As String)` → List(Of WoodSpecies)
  - `GetWoodSpeciesByName(name As String)` → WoodSpecies
  - `SearchWoodSpecies(searchTerm As String)` → List(Of WoodSpecies)
  - `AddWoodSpecies(species As WoodSpecies)` → Boolean
  - `UpdateWoodSpecies(species As WoodSpecies)` → Boolean
  - `DeleteWoodSpecies(speciesId As Integer)` → Boolean

**File:** `Woodworkers Friend\Modules\Database\DatabaseManager.vb`

**Singleton Pattern Example:**
```visualbasic
Public Class DatabaseManager
    Private Shared _instance As DatabaseManager
    Private ReadOnly _connectionString As String
    Private ReadOnly _dbPath As String
    
    Private Sub New()
        ' Database in user's AppData folder
        Dim appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
        Dim appFolder = Path.Combine(appData, "WoodworkersFriend")
        Directory.CreateDirectory(appFolder)
        _dbPath = Path.Combine(appFolder, "WoodworkersFriend.db")
        _connectionString = $"Data Source={_dbPath};Version=3;"
        InitializeDatabase()
    End Sub
    
    Public Shared ReadOnly Property Instance As DatabaseManager
        Get
            If _instance Is Nothing Then
                _instance = New DatabaseManager()
            End If
            Return _instance
        End Get
    End Property
End Class
```

### Step 1.4: Create Database Initialization (1 hour)
- [ ] Add `InitializeDatabase()` method to DatabaseManager
- [ ] Create tables if they don't exist
- [ ] Add database version tracking
- [ ] Handle first-run scenario
- [ ] Test database creation

**Key Considerations:**
- Database location: `%AppData%\WoodworkersFriend\WoodworkersFriend.db`
- Check if exists before creating
- Create backup of old database on schema updates

### Step 1.5: Data Migration Utility (1 hour)
- [ ] Create `Modules\Database\DataMigration.vb`
- [ ] Write method to import from WoodPropertiesDatabase
- [ ] Write method to import from WoodSpeciesDatabase (WoodMovement)
- [ ] Merge duplicate species (Oak, Maple, etc.)
- [ ] Preserve all unique properties
- [ ] Test migration with current data

**Migration Strategy:**
1. Load all species from WoodPropertiesDatabase (23 species)
2. Load all species from WoodSpeciesDatabase (~15 species)
3. Merge by CommonName (case-insensitive)
4. Combine properties (movement + strength data)
5. Insert into SQLite database
6. Verify count and data integrity

---

## 📅 PHASE 2: WOOD PROPERTIES MODULE MIGRATION (Estimated: 3-4 hours)

### Goal: Convert Wood Properties tab to use SQLite database

### Step 2.1: Update Data Models (30 minutes)
- [ ] Keep `WoodPropertiesData` class or rename to `WoodSpecies`
- [ ] Add `SpeciesID` property
- [ ] Add `IsUserAdded` property
- [ ] Add `DateAdded` and `LastModified` properties
- [ ] Match database schema exactly

**File:** `Woodworkers Friend\Modules\References\WoodPropertiesModels.vb`

### Step 2.2: Update FrmMain.WoodProperties.vb (2 hours)
- [ ] Replace `_allWoodPropertiesData = WoodPropertiesDatabase.GetWoodSpeciesList()`
- [ ] With: `_allWoodPropertiesData = DatabaseManager.Instance.GetAllWoodSpecies()`
- [ ] Update `ApplyWoodFilter()` to use database queries (optional optimization)
- [ ] Test all existing functionality:
  - Grid population
  - Sorting
  - Filtering
  - Search
  - Details display
  - Export

**Changes in InitializeWoodPropertiesReference():**
```visualbasic
' OLD
_allWoodPropertiesData = WoodPropertiesDatabase.GetWoodSpeciesList()

' NEW
_allWoodPropertiesData = DatabaseManager.Instance.GetAllWoodSpecies()
```

### Step 2.3: Add User Customization (1 hour)
- [ ] Add "Add Species" button
- [ ] Create dialog for adding custom wood species
- [ ] Implement `AddWoodSpecies()` method
- [ ] Mark user-added species with icon or color
- [ ] Test adding/editing custom species

**New Feature:** Users can add their own exotic woods!

### Step 2.4: Testing (30 minutes)
- [ ] Verify all 23 species load correctly
- [ ] Test sorting on all columns
- [ ] Test filtering (All/Hardwoods/Softwoods)
- [ ] Test search functionality
- [ ] Test adding custom species
- [ ] Export to CSV includes custom species
- [ ] Test with empty database (first run)

### Step 2.5: Deprecate Old Code (15 minutes)
- [ ] Mark `WoodPropertiesDatabase.vb` as obsolete (add comment)
- [ ] Do NOT delete yet (keep as reference)
- [ ] Update documentation

---

## 📅 PHASE 3: WOOD MOVEMENT MODULE MIGRATION (Estimated: 2-3 hours)

### Goal: Convert Wood Movement calculator to use unified database

### Step 3.1: Analyze Current Implementation (30 minutes)
- [ ] Review `Modules\WoodMovement\WoodSpeciesDatabase.vb`
- [ ] Note any properties NOT in Wood Properties module
- [ ] Ensure unified schema has all properties
- [ ] Check for calculation-specific data

### Step 3.2: Update Wood Movement Module (1.5 hours)
- [ ] Find where wood species are loaded
- [ ] Replace with `DatabaseManager.Instance.GetAllWoodSpecies()`
- [ ] Update any wood selection dropdowns
- [ ] Test calculations with new data source
- [ ] Verify results match previous calculations

### Step 3.3: Cross-Module Benefits (30 minutes)
- [ ] User selects Oak in Wood Movement → Gets Janka hardness automatically
- [ ] User selects Oak in Shelf Sag → Gets shrinkage data automatically
- [ ] Demonstrate single source of truth

### Step 3.4: Testing (30 minutes)
- [ ] Wood Movement calculations work correctly
- [ ] All wood species available in dropdown
- [ ] Custom user species appear in Wood Movement
- [ ] Calculations produce same results as before

### Step 3.5: Deprecate Old Code (15 minutes)
- [ ] Mark `WoodMovement\WoodSpeciesDatabase.vb` as obsolete
- [ ] Do NOT delete yet
- [ ] Update documentation

---

## 📅 PHASE 4: HELP SYSTEM DATABASE (Estimated: 4-5 hours)

### Goal: Convert file-based help to searchable database

### Step 4.1: Analyze Current Help System (30 minutes)
- [ ] Review `Modules\Help\HelpContentManager.vb`
- [ ] Document current help file structure
- [ ] List all help topics
- [ ] Note any embedded resources

**Current State:**
- File: `HelpContentManager.vb` 
- Likely uses embedded resources or external files
- Need to check how help is currently loaded

### Step 4.2: Design Help Database Schema (30 minutes)
- [ ] Create `HelpContent` table design
- [ ] Add full-text search index
- [ ] Support categories and keywords
- [ ] Version tracking for updates

```sql
CREATE TABLE HelpContent (
    HelpID INTEGER PRIMARY KEY AUTOINCREMENT,
    ModuleName TEXT NOT NULL, -- "BoardFeet", "ShelfSag", etc.
    Title TEXT NOT NULL,
    Content TEXT NOT NULL, -- Markdown or HTML
    Keywords TEXT, -- Comma-separated for search
    Category TEXT, -- "GettingStarted", "Advanced", "Troubleshooting"
    SortOrder INTEGER DEFAULT 0,
    Version TEXT DEFAULT '1.0',
    LastUpdated DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Enable full-text search
CREATE VIRTUAL TABLE HelpContent_FTS USING fts5(
    Title, Content, Keywords, 
    content='HelpContent'
);
```

### Step 4.3: Migrate Help Content (2 hours)
- [ ] Create utility to import current help files
- [ ] Convert to database format
- [ ] Add keywords for better search
- [ ] Organize by module and category
- [ ] Test import process

### Step 4.4: Update HelpContentManager (1.5 hours)
- [ ] Add database methods:
  - `GetHelpContent(moduleName As String)` → String
  - `SearchHelp(searchTerm As String)` → List(Of HelpContent)
  - `GetHelpByCategory(category As String)` → List(Of HelpContent)
- [ ] Update `FrmMain.Help.vb` to use new methods
- [ ] Implement search functionality
- [ ] Add context-sensitive help

### Step 4.5: Enhanced Help UI (Optional - 1 hour)
- [ ] Add search box to help tab
- [ ] Show search results as you type
- [ ] Highlight search terms in content
- [ ] Add "Related Topics" section
- [ ] Breadcrumb navigation

### Step 4.6: Testing (30 minutes)
- [ ] All help content loads correctly
- [ ] Search finds relevant topics
- [ ] Context-sensitive help works
- [ ] Performance is acceptable

---

## 📅 PHASE 5: USER PREFERENCES DATABASE (Estimated: 2 hours)

### Goal: Store user settings in database instead of registry/config files

### Step 5.1: Identify Current Settings (30 minutes)
- [ ] Find where user preferences are currently stored
- [ ] List all settings (theme, defaults, recent files, etc.)
- [ ] Document current loading/saving mechanism

### Step 5.2: Create Preferences Schema (15 minutes)
```sql
CREATE TABLE UserPreferences (
    PrefKey TEXT PRIMARY KEY,
    PrefValue TEXT,
    DataType TEXT CHECK(DataType IN ('String', 'Integer', 'Boolean', 'Double', 'JSON')),
    Category TEXT, -- "UI", "Defaults", "Advanced", "Calculation"
    Description TEXT,
    LastModified DATETIME DEFAULT CURRENT_TIMESTAMP
);
```

### Step 5.3: Implement Preferences Manager (1 hour)
- [ ] Add methods to DatabaseManager:
  - `GetPreference(key As String, defaultValue As String)` → String
  - `SavePreference(key As String, value As String, dataType As String)`
  - `GetPreferencesByCategory(category As String)` → Dictionary
- [ ] Add type-safe wrappers:
  - `GetBoolPreference()`, `GetIntPreference()`, etc.
- [ ] Test reading/writing

### Step 5.4: Migrate Existing Preferences (30 minutes)
- [ ] Load current preferences from registry/config
- [ ] Save to database
- [ ] Update all preference reads to use database
- [ ] Test application behavior

### Step 5.5: Testing (15 minutes)
- [ ] Settings persist across app restarts
- [ ] Default values work correctly
- [ ] Settings UI updates database

---

## 📅 PHASE 6: CALCULATION HISTORY (Estimated: 3 hours)

### Goal: Save user calculations for quick recall

### Step 6.1: Design History Schema (30 minutes)
```sql
CREATE TABLE CalculationHistory (
    HistoryID INTEGER PRIMARY KEY AUTOINCREMENT,
    CalculatorType TEXT NOT NULL, -- "BoardFeet", "ShelfSag", etc.
    CalculationName TEXT, -- User-provided name (optional)
    InputParameters TEXT NOT NULL, -- JSON: {"length":36,"width":12,...}
    Results TEXT NOT NULL, -- JSON: {"volume":27,"boardFeet":3.5,...}
    DateCalculated DATETIME DEFAULT CURRENT_TIMESTAMP,
    IsFavorite BOOLEAN DEFAULT 0,
    Notes TEXT
);

CREATE INDEX idx_calculator_type ON CalculationHistory(CalculatorType);
CREATE INDEX idx_favorites ON CalculationHistory(IsFavorite);
```

### Step 6.2: Implement History Manager (1.5 hours)
- [ ] Add methods to DatabaseManager:
  - `SaveCalculation(type, name, inputs, results)` → Integer (HistoryID)
  - `GetCalculationHistory(type, limit)` → List
  - `GetFavorites()` → List
  - `DeleteCalculation(historyId)` → Boolean
  - `ToggleFavorite(historyId)` → Boolean
- [ ] Create helper class for serializing inputs/results to JSON

### Step 6.3: Add UI for History (1 hour)
- [ ] Add "History" button to each calculator
- [ ] Create history dialog showing recent calculations
- [ ] Double-click to load calculation
- [ ] Right-click menu: Favorite, Delete, Rename
- [ ] Show favorites prominently

### Step 6.4: Testing (30 minutes)
- [ ] Calculations save correctly
- [ ] Loading history restores inputs
- [ ] Favorites persist
- [ ] Deleting works
- [ ] Test across multiple calculators

---

## 📅 PHASE 7: ADVANCED FEATURES (Future - 8+ hours)

### Optional enhancements when time permits:

### Joinery Reference Database
- [ ] Create `JoineryTypes` table
- [ ] Add common joinery methods (Mortise/Tenon, Dovetail, etc.)
- [ ] Include strength ratings, difficulty, typical uses
- [ ] Add reference tab in UI

### Hardware Standards Database
- [ ] Create `HardwareStandards` table
- [ ] Add common hardware (hinges, slides, brackets)
- [ ] Include dimensions and mounting requirements
- [ ] Add reference tab in UI

### Material Presets Database
- [ ] Create `MaterialPresets` table
- [ ] Store common material dimensions and properties
- [ ] Allow user-defined presets
- [ ] Quick-load presets in calculators

### Formula Library Database
- [ ] Create `Formulas` table
- [ ] Document all formulas used in app
- [ ] Show formulas to users (educational)
- [ ] Allow formula notes and examples

### Project Templates
- [ ] Create `ProjectTemplates` table
- [ ] Store common projects (bookshelf, table, etc.)
- [ ] Include cut lists and materials
- [ ] Export to cut list optimizer

---

## 🧪 TESTING CHECKLIST

### After Each Phase:
- [ ] All existing functionality still works
- [ ] No performance degradation
- [ ] Database file size is reasonable
- [ ] No memory leaks
- [ ] Error handling works correctly
- [ ] User data is preserved on app restart
- [ ] Backup/restore functionality works

### Final Integration Testing:
- [ ] Test all modules with database
- [ ] Test with empty database (first run)
- [ ] Test with corrupted database (recovery)
- [ ] Test with large dataset (performance)
- [ ] Test concurrent access (if needed)
- [ ] Test database migration/upgrade path

---

## 📚 DOCUMENTATION TASKS

### As You Go:
- [ ] Document database schema in `DATABASE_SCHEMA.md`
- [ ] Update `README.md` with database information
- [ ] Add inline code comments for database operations
- [ ] Create user guide for custom species
- [ ] Document backup/restore procedures

### Files to Create:
1. `DATABASE_SCHEMA.md` - Complete schema documentation
2. `DATABASE_MIGRATION_GUIDE.md` - How to migrate from old versions
3. `USER_DATA_GUIDE.md` - Where user data is stored, how to backup
4. `DEVELOPER_NOTES_DATABASE.md` - Implementation notes

---

## 🎯 QUICK WIN MILESTONES

### Milestone 1: Database Working (Phase 1 Complete)
**Deliverable:** SQLite database created, DatabaseManager working, data migrated.
**Test:** Can query wood species from database.
**Time:** ~6 hours

### Milestone 2: Wood Properties Using Database (Phase 2 Complete)
**Deliverable:** Wood Properties tab uses database, sorting/filtering works.
**Test:** All 23 species display correctly, can add custom species.
**Time:** ~4 hours (cumulative: 10 hours)

### Milestone 3: Unified Wood Data (Phase 3 Complete)
**Deliverable:** Both wood modules use same database.
**Test:** Select oak in one module, has all properties in other module.
**Time:** ~3 hours (cumulative: 13 hours)

### Milestone 4: Searchable Help (Phase 4 Complete)
**Deliverable:** Help content in database with full-text search.
**Test:** Search for "sag" finds relevant help topics.
**Time:** ~5 hours (cumulative: 18 hours)

### Milestone 5: User Preferences (Phase 5 Complete)
**Deliverable:** Settings stored in database.
**Test:** Settings persist across restarts.
**Time:** ~2 hours (cumulative: 20 hours)

### Milestone 6: Calculation History (Phase 6 Complete)
**Deliverable:** Calculations can be saved and recalled.
**Test:** Save favorite shelf calculation, reload it later.
**Time:** ~3 hours (cumulative: 23 hours)

---

## ⚠️ IMPORTANT NOTES

### Database Location:
- **Development:** `%AppData%\WoodworkersFriend\WoodworkersFriend.db`
- **Production:** Same location (user-specific data)
- **Backup folder:** `%AppData%\WoodworkersFriend\Backups\`

### Version Control:
- **DO NOT** commit database file to Git (.gitignore it)
- **DO** commit database schema SQL files
- **DO** commit migration scripts

### Backward Compatibility:
- Keep old in-code databases for 2-3 versions
- Automatic migration on first run
- Fallback if database unavailable

### Performance Considerations:
- Use connection pooling (SQLite handles automatically)
- Use transactions for batch operations
- Index frequently-queried columns
- Consider caching for read-heavy operations

### Error Handling:
- Graceful degradation if database unavailable
- Automatic backup before schema changes
- Clear error messages for users
- Detailed logging for debugging

---

## 🔧 DEVELOPMENT ENVIRONMENT SETUP

### Before Starting:
1. [ ] Commit current work to Git
2. [ ] Create feature branch: `feature/unified-database`
3. [ ] Install SQLite NuGet package
4. [ ] Create `Modules\Database\` folder
5. [ ] Have SQLite DB Browser installed (for debugging)

### Recommended Tools:
- **DB Browser for SQLite** - https://sqlitebrowser.org/
- **Visual Studio SQLite/SQL Server Compact Toolbox** - Extension
- **LINQPad** - For testing LINQ queries (optional)

---

## 📞 DECISION POINTS

### To Decide Before Starting:
1. [ ] Database naming convention: `WoodworkersFriend.db` or `WoodworkersFriend.sqlite`?
2. [ ] Keep old code files or delete after migration?
3. [ ] Auto-backup frequency (daily, weekly, on-close)?
4. [ ] Maximum history entries to keep (100, 500, unlimited)?
5. [ ] Allow users to export/import their database?

---

## 🚀 READY TO START CHECKLIST

Before beginning Phase 1:
- [ ] WoodPropertiesDatabase.vb is fixed and compiling
- [ ] Current application is stable and tested
- [ ] All work committed to Git
- [ ] Created feature branch
- [ ] Read through all phases to understand scope
- [ ] Estimated personal time commitment
- [ ] Have SQLite tools ready

---

## 📝 NOTES & REMINDERS

### Database Advantages You'll Gain:
✅ **No more duplicate data** - One source of truth
✅ **User customization** - Add custom species, save preferences
✅ **Powerful search** - Find species by properties
✅ **Cross-module intelligence** - Data shared everywhere
✅ **Calculation history** - Never lose a calculation
✅ **Searchable help** - Find answers fast
✅ **Professional architecture** - Scalable and maintainable
✅ **Future-proof** - Easy to add new features

### Current File Status:
- ✅ Wood Properties UI: Working
- ⚠️ WoodPropertiesDatabase.vb: CORRUPTED - Fix before starting Phase 1!
- ✅ Sorting: Working
- ✅ Filtering: Working
- ✅ Search: Working
- ✅ Export: Working

### Remember:
- Take breaks between phases
- Test thoroughly after each change
- Commit frequently
- Ask for help if stuck
- This is a marathon, not a sprint!

---

## 🎉 SUCCESS METRICS

You'll know you're successful when:
1. ✅ Wood Properties grid shows all species from database
2. ✅ Users can add their own custom wood species
3. ✅ Wood Movement and Wood Properties share the same species data
4. ✅ Help system is searchable
5. ✅ Application is faster and more responsive
6. ✅ No duplicate code for wood species
7. ✅ Users can save and recall calculations
8. ✅ Settings persist properly
9. ✅ Single `WoodworkersFriend.db` file contains all data
10. ✅ Old code is removed and codebase is cleaner

---

## 📅 ESTIMATED TIMELINE

**Conservative Estimate:** 25-30 hours total work
- Phase 1: 6 hours
- Phase 2: 4 hours
- Phase 3: 3 hours
- Phase 4: 5 hours
- Phase 5: 2 hours
- Phase 6: 3 hours
- Testing & Documentation: 2-5 hours

**Working 2-3 hours per day:** 2-3 weeks
**Working weekends (8 hours/day):** 3-4 weekends

---

## 🎯 NEXT SESSION START HERE

1. **Fix WoodPropertiesDatabase.vb** (Option A: Recreate clean file)
2. **Verify application builds and runs**
3. **Test Wood Properties grid with all 23 species**
4. **Commit working code to Git**
5. **Create feature branch: `feature/unified-database`**
6. **Begin Phase 1: Install SQLite**

**Good luck! You're building something awesome! 🚀**

---

## 📖 REFERENCE LINKS

- SQLite Documentation: https://www.sqlite.org/docs.html
- System.Data.SQLite: https://system.data.sqlite.org/
- SQLite Tutorial: https://www.sqlitetutorial.net/
- DB Browser for SQLite: https://sqlitebrowser.org/

---

## ✍️ JOURNAL ENTRIES (Keep Notes Here)

### Session 1: [Date]
- Started: [Time]
- Completed: [What you finished]
- Issues: [Any problems]
- Next: [What to do next]
- Ended: [Time]

### Session 2: [Date]
- [Your notes...]

---

**File Created:** January 29, 2026  
**Last Updated:** January 29, 2026  
**Status:** Ready to Begin  
**Priority:** Fix database file first, then Phase 1
