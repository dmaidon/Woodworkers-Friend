# Database Architecture Split - Multi-Database Design

**Created:** January 2026  
**Status:** Design Phase  
**Goal:** Split monolithic database into specialized, purpose-driven databases

---

## 🎯 Executive Summary

**Current State:** Single `WoodworkersFriend.db` contains all data (help, reference, user data)  
**Target State:** Three specialized databases with clear ownership and lifecycle management

**Motivation:**
- ✅ **Isolation** - Corruption in one database doesn't affect others
- ✅ **Versioning** - Update reference data independently of user data
- ✅ **Distribution** - Ship read-only databases as application resources
- ✅ **Backup Strategy** - Users only backup their data, not system data
- ✅ **Performance** - Smaller databases, faster queries, better indexes
- ✅ **Security** - Reference data can be read-only, preventing user modification
- ✅ **Maintenance** - Clear separation of concerns

---

## 📊 Database Architecture

### **Database 1: Help.db** (Read-Only Reference)

**Purpose:** Application help content, tutorials, definitions  
**Location:** `Resources\Data\Help.db` (shipped with application)  
**Access:** Read-only (opened in shared mode)  
**Update Frequency:** Only with application updates  
**Size Estimate:** ~500KB - 2MB

**Tables:**
```sql
CREATE TABLE HelpContent (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    ModuleName TEXT NOT NULL UNIQUE,
    Title TEXT NOT NULL,
    Content TEXT NOT NULL,
    Keywords TEXT,
    Category TEXT,
    SortOrder INTEGER DEFAULT 0,
    Version TEXT DEFAULT '1.0',
    LastUpdated DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_help_module ON HelpContent(ModuleName);
CREATE INDEX idx_help_category ON HelpContent(Category);
CREATE INDEX idx_help_keywords ON HelpContent(Keywords);
```

**Distribution Strategy:**
- Ship as embedded resource in application
- Extract to `AppData\WoodworkersFriend\Resources\` on first run
- Check version on startup, update if newer version available
- NEVER allow user modification (read-only file attributes)

---

### **Database 2: Reference.db** (Read-Only Reference)

**Purpose:** Wood species, joinery types, hardware standards  
**Location:** `Resources\Data\Reference.db` (shipped with application)  
**Access:** Read-only (can be refreshed from app resources)  
**Update Frequency:** Updated with application releases  
**Size Estimate:** ~1-2MB

**Tables:**
```sql
-- Wood Species (25+ species)
CREATE TABLE WoodSpecies (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    CommonName TEXT NOT NULL,
    ScientificName TEXT,
    WoodType TEXT NOT NULL,
    JankaHardness INTEGER,
    SpecificGravity REAL,
    Density REAL,
    MoistureContent REAL,
    ShrinkageRadial REAL,
    ShrinkageTangential REAL,
    TypicalUses TEXT,
    Workability TEXT,
    Cautions TEXT,
    Notes TEXT,
    IsUserAdded INTEGER DEFAULT 0,
    DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Joinery Types (12 types)
CREATE TABLE JoineryTypes (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL UNIQUE,
    Category TEXT NOT NULL,
    StrengthRating INTEGER,
    DifficultyLevel TEXT,
    Description TEXT,
    TypicalUses TEXT,
    RequiredTools TEXT,
    StrengthCharacteristics TEXT,
    GlueRequired INTEGER,
    ReinforcementOptions TEXT,
    HistoricalNotes TEXT
);

-- Hardware Standards (16 items)
CREATE TABLE HardwareStandards (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Category TEXT NOT NULL,
    Specifications TEXT,
    Dimensions TEXT,
    MountingRequirements TEXT,
    WeightCapacity TEXT,
    CommonBrands TEXT,
    PartNumbers TEXT,
    Notes TEXT,
    InstallationTips TEXT
);
```

**Distribution Strategy:**
- Ship as embedded resource
- Extract to `AppData\WoodworkersFriend\Resources\`
- Allow "Refresh Reference Data" feature to restore from app resources
- Users CAN add custom wood species (stored separately in UserData.db)

---

### **Database 3: UserData.db** (User-Modifiable)

**Purpose:** User costs, preferences, calculation history, custom entries  
**Location:** `AppData\WoodworkersFriend\UserData.db`  
**Access:** Read-write  
**Update Frequency:** Every time user makes changes  
**Size Estimate:** Grows over time (10KB - 50MB)

**Tables:**
```sql
-- Wood Costs (user pricing data)
CREATE TABLE WoodCosts (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    WoodName TEXT NOT NULL,
    Thickness TEXT NOT NULL,
    CostPerBoardFoot REAL NOT NULL,
    IsUserAdded INTEGER DEFAULT 0,
    IsActive INTEGER DEFAULT 1,
    DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
    LastModified DATETIME DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(WoodName, Thickness)
);

-- Epoxy Costs (user pricing data)
CREATE TABLE EpoxyCosts (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Brand TEXT NOT NULL,
    Type TEXT NOT NULL,
    CostPerGallon REAL NOT NULL,
    IsUserAdded INTEGER DEFAULT 0,
    IsActive INTEGER DEFAULT 1,
    DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
    LastModified DATETIME DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(Brand, Type)
);

-- User Preferences
CREATE TABLE Preferences (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Key TEXT NOT NULL UNIQUE,
    Value TEXT NOT NULL,
    DataType TEXT DEFAULT 'String',
    Category TEXT,
    LastModified DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- Calculation History (Board Feet, Epoxy, etc.)
CREATE TABLE CalculationHistory (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    CalculatorType TEXT NOT NULL,
    InputData TEXT NOT NULL,
    ResultData TEXT NOT NULL,
    TimesRun INTEGER DEFAULT 1,
    DateCreated DATETIME DEFAULT CURRENT_TIMESTAMP,
    LastRun DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- User-Added Wood Species (extends Reference.db)
CREATE TABLE CustomWoodSpecies (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    CommonName TEXT NOT NULL UNIQUE,
    ScientificName TEXT,
    WoodType TEXT NOT NULL,
    JankaHardness INTEGER,
    SpecificGravity REAL,
    Density REAL,
    MoistureContent REAL,
    ShrinkageRadial REAL,
    ShrinkageTangential REAL,
    TypicalUses TEXT,
    Workability TEXT,
    Cautions TEXT,
    Notes TEXT,
    DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP
);

-- User Notes (future feature)
CREATE TABLE ProjectNotes (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Content TEXT,
    Tags TEXT,
    DateCreated DATETIME DEFAULT CURRENT_TIMESTAMP,
    LastModified DATETIME DEFAULT CURRENT_TIMESTAMP
);
```

**Backup Strategy:**
- Export to JSON for backup
- Automatic backup on app close (keep last 5)
- User-triggered export/import
- Clear separation: user can backup just this file

---

## 🏗️ Implementation Architecture

### **Class Structure**

```
DatabaseManager (Coordinator/Facade)
├── HelpDataManager (Help.db) - Read-only
├── ReferenceDataManager (Reference.db) - Read-only
└── UserDataManager (UserData.db) - Read-write
```

### **DatabaseManager.vb** (Singleton Coordinator)

**Responsibilities:**
- Initialize all sub-managers
- Provide unified API for data access
- Route queries to appropriate database
- Handle cross-database queries (if needed)

```vb
Public Class DatabaseManager
    Private Shared _instance As DatabaseManager
    
    ' Sub-managers
    Public ReadOnly Property Help As HelpDataManager
    Public ReadOnly Property Reference As ReferenceDataManager
    Public ReadOnly Property UserData As UserDataManager
    
    ' Unified API (routes to appropriate manager)
    Public Function GetHelpContent(topic As String) As HelpContentData
        Return Help.GetContent(topic)
    End Function
    
    Public Function GetWoodSpecies(name As String) As WoodSpecies
        ' Check user-added first, then reference
        Dim custom = UserData.GetCustomWoodSpecies(name)
        If custom IsNot Nothing Then Return custom
        Return Reference.GetWoodSpecies(name)
    End Function
    
    Public Function GetAllWoodSpecies() As List(Of WoodSpecies)
        ' Merge reference + user-added
        Dim result = Reference.GetAllWoodSpecies()
        result.AddRange(UserData.GetCustomWoodSpecies())
        Return result
    End Function
End Class
```

---

### **HelpDataManager.vb** (Help.db)

**Responsibilities:**
- Load and search help content
- Manage help content updates from app resources
- Read-only operations only

```vb
Public Class HelpDataManager
    Private ReadOnly _connectionString As String
    
    Public Function GetContent(moduleName As String) As HelpContentData
    Public Function SearchContent(searchTerm As String) As List(Of HelpContentData)
    Public Function GetAllTopics() As List(Of HelpContentData)
    Public Function GetContentByCategory(category As String) As List(Of HelpContentData)
    
    ' Update method (only called during app startup if version changed)
    Friend Sub RefreshFromResources(helpData As List(Of HelpContentData))
End Class
```

---

### **ReferenceDataManager.vb** (Reference.db)

**Responsibilities:**
- Wood species lookup
- Joinery types reference
- Hardware standards reference
- Read-only (except for version updates)

```vb
Public Class ReferenceDataManager
    Private ReadOnly _connectionString As String
    
    ' Wood Species
    Public Function GetWoodSpecies(name As String) As WoodSpecies
    Public Function GetAllWoodSpecies() As List(Of WoodSpecies)
    Public Function SearchWoodSpecies(filter As String) As List(Of WoodSpecies)
    
    ' Joinery Types
    Public Function GetJoineryType(name As String) As JoineryType
    Public Function GetAllJoineryTypes() As List(Of JoineryType)
    Public Function GetJoineryByCategory(category As String) As List(Of JoineryType)
    
    ' Hardware Standards
    Public Function GetHardwareStandard(name As String) As HardwareStandard
    Public Function GetAllHardwareStandards() As List(Of HardwareStandard)
    Public Function GetHardwareByCategory(category As String) As List(Of HardwareStandard)
    
    ' Refresh method for app updates
    Friend Sub RefreshFromResources()
End Class
```

---

### **UserDataManager.vb** (UserData.db)

**Responsibilities:**
- Wood/epoxy costs (user pricing)
- User preferences
- Calculation history
- Custom wood species (user-added)
- Project notes (future)
- Read-write operations

```vb
Public Class UserDataManager
    Private ReadOnly _connectionString As String
    
    ' Costs
    Public Function GetWoodCost(name As String, thickness As String) As WoodCost
    Public Function SaveWoodCost(cost As WoodCost) As Boolean
    Public Function GetAllWoodCosts() As List(Of WoodCost)
    Public Function DeleteWoodCost(id As Integer) As Boolean
    
    Public Function GetEpoxyCost(brand As String, type As String) As EpoxyCost
    Public Function SaveEpoxyCost(cost As EpoxyCost) As Boolean
    Public Function GetAllEpoxyCosts() As List(Of EpoxyCost)
    
    ' Preferences
    Public Function GetPreference(key As String) As String
    Public Function SavePreference(key As String, value As String, dataType As String, category As String) As Boolean
    Public Function GetAllPreferences() As Dictionary(Of String, String)
    
    ' Calculation History
    Public Function SaveCalculation(calc As CalculationHistory) As Boolean
    Public Function GetTopCalculations(count As Integer) As List(Of CalculationHistory)
    
    ' Custom Wood Species
    Public Function AddCustomWoodSpecies(species As WoodSpecies) As Boolean
    Public Function GetCustomWoodSpecies() As List(Of WoodSpecies)
    
    ' Backup/Restore
    Public Function ExportToJson(filePath As String) As Boolean
    Public Function ImportFromJson(filePath As String) As Boolean
End Class
```

---

## 📁 File Locations

### **Development (Source Control)**
```
WoodworkersFriend/
├── Resources/
│   └── Data/
│       ├── Help.db (shipped with app, version controlled)
│       └── Reference.db (shipped with app, version controlled)
```

### **Runtime (User's Machine)**
```
%AppData%/WoodworkersFriend/
├── Resources/
│   ├── Help.db (extracted from app resources, read-only)
│   └── Reference.db (extracted from app resources, read-only)
├── UserData.db (user-modifiable)
└── Backups/
    ├── UserData_2026-01-30.db
    ├── UserData_2026-01-29.db
    └── ... (keep last 5)
```

---

## 🔄 Migration Strategy for Existing Users

### **Phase 1: Detection**
```vb
Public Function NeedsDatabaseSplit() As Boolean
    ' Check if old monolithic database exists
    Dim oldDb = Path.Combine(AppData, "WoodworkersFriend.db")
    Return File.Exists(oldDb)
End Function
```

### **Phase 2: Extract and Split**
```vb
Public Function SplitMonolithicDatabase() As Boolean
    ' 1. Create new databases
    CreateHelpDatabase()
    CreateReferenceDatabase()
    CreateUserDatabase()
    
    ' 2. Copy data to appropriate databases
    '    - HelpContent → Help.db
    '    - WoodSpecies, JoineryTypes, HardwareStandards → Reference.db
    '    - WoodCosts, EpoxyCosts, Preferences, CalculationHistory → UserData.db
    
    ' 3. Backup old database
    BackupMonolithicDatabase()
    
    ' 4. Rename old database to .old (keep for safety)
    RenameToOldDatabase()
    
    Return True
End Function
```

### **Phase 3: Verification**
- Query each database to verify data integrity
- Count records in each table
- Run smoke tests on critical queries
- Log success/failure to error log

---

## 🔐 Security & Access Control

| Database | File Attributes | Access Mode | User Can Modify |
|----------|----------------|-------------|-----------------|
| Help.db | Read-only | Shared Read | ❌ No |
| Reference.db | Read-only | Shared Read | ❌ No (can add to UserData.db) |
| UserData.db | Normal | Exclusive Read-Write | ✅ Yes |

---

## 🚀 Deployment & Updates

### **First Install:**
1. Extract `Help.db` from app resources → `%AppData%\WoodworkersFriend\Resources\`
2. Extract `Reference.db` from app resources → `%AppData%\WoodworkersFriend\Resources\`
3. Create empty `UserData.db` with schema
4. Seed default preferences
5. Set file attributes (Help/Reference = read-only)

### **App Update (New Version):**
1. Check version of `Help.db` (stored in metadata table)
2. If app version > database version:
   - Replace `Help.db` from app resources
   - Replace `Reference.db` from app resources
3. `UserData.db` is NEVER touched by updates

### **User Requests "Refresh Reference Data":**
1. Backup current `Reference.db`
2. Extract fresh copy from app resources
3. Re-apply any user-added species from `UserData.CustomWoodSpecies`

---

## 📦 Benefits Breakdown

### **For Development:**
- ✅ Easy to version control reference databases (ship with app)
- ✅ Clear separation: reference data vs user data
- ✅ Faster CI/CD (only rebuild changed databases)
- ✅ Easier testing (swap reference databases for test data)

### **For Distribution:**
- ✅ Update help content without touching user data
- ✅ Reference data corruption? Just re-extract from app
- ✅ Smaller update packages (only changed databases)

### **For Users:**
- ✅ Easy backup: just copy `UserData.db`
- ✅ Safe experimentation: reset UserData.db without losing reference data
- ✅ Corruption recovery: app can restore reference databases
- ✅ Clear data ownership: "This is MY data, this is SYSTEM data"

### **For Support:**
- ✅ "Help content corrupted?" → Re-extract Help.db
- ✅ "Wrong wood species data?" → Refresh Reference.db
- ✅ "Lost my costs?" → Restore UserData.db from backup

---

## 🎯 Migration Checklist

### **Phase A: Create New Infrastructure**
- [ ] Create `HelpDataManager.vb`
- [ ] Create `ReferenceDataManager.vb`
- [ ] Create `UserDataManager.vb`
- [ ] Update `DatabaseManager.vb` to coordinate sub-managers
- [ ] Create database schemas in SQL

### **Phase B: Update Data Access**
- [ ] Update `HelpContentManager` to use `HelpDataManager`
- [ ] Update wood species queries to use `ReferenceDataManager`
- [ ] Update joinery queries to use `ReferenceDataManager`
- [ ] Update cost queries to use `UserDataManager`
- [ ] Update preference queries to use `UserDataManager`

### **Phase C: Migration Utilities**
- [ ] Create `SplitMonolithicDatabase()` method
- [ ] Create detection logic for existing users
- [ ] Create backup/restore utilities
- [ ] Create version checking logic

### **Phase D: Testing**
- [ ] Test fresh install (no existing database)
- [ ] Test migration (existing monolithic database)
- [ ] Test all CRUD operations on each database
- [ ] Test concurrent access (Help/Reference in shared mode)
- [ ] Test backup/restore for UserData.db

### **Phase E: Documentation**
- [ ] Update README.md with new architecture
- [ ] Create MIGRATION_GUIDE for existing users
- [ ] Update CONTRIBUTING.md for developers
- [ ] Add architecture diagrams

---

## ⚠️ Risks & Mitigation

| Risk | Probability | Impact | Mitigation |
|------|-------------|--------|------------|
| Migration fails, data lost | Low | HIGH | Backup old database before split |
| Corruption during migration | Low | Medium | Transactional migration, rollback on error |
| Performance regression | Very Low | Low | Smaller databases = faster queries |
| Code complexity increases | Medium | Low | Clear manager separation, good documentation |
| User confusion | Low | Low | Automatic migration, transparent to user |

---

## 📈 Performance Expectations

| Operation | Before (Single DB) | After (Split DBs) | Improvement |
|-----------|-------------------|-------------------|-------------|
| Load help topic | 5-10ms | 2-5ms | 2x faster (smaller index) |
| Search help | 15-25ms | 8-12ms | 2x faster |
| Get wood species | 3-8ms | 2-5ms | 1.5x faster |
| Save user cost | 10-20ms | 8-15ms | Similar (write operations) |
| Concurrent reads | ❌ Locked | ✅ Parallel | 10x+ faster |

**Key Win:** Help and Reference databases can be queried in parallel since they're separate files!

---

## 🎉 Success Criteria

- [ ] All existing functionality works identically
- [ ] No data loss during migration
- [ ] Users unaware of internal change (transparent)
- [ ] Reference data can be updated independently
- [ ] User data backed up automatically
- [ ] No performance regressions
- [ ] All unit tests pass
- [ ] Documentation complete

---

## 📝 Implementation Timeline

| Phase | Tasks | Estimated Time |
|-------|-------|----------------|
| Design (this doc) | Architecture, schemas | ✅ Complete |
| Infrastructure | Create managers, update DatabaseManager | 2-3 hours |
| Data Access Updates | Update all queries | 2-3 hours |
| Migration Logic | Split existing databases | 1-2 hours |
| Testing | End-to-end testing | 2-3 hours |
| Documentation | README, guides | 1 hour |
| **TOTAL** | | **8-12 hours** |

---

## 🔮 Future Enhancements

Once split is complete, enables:
1. **Cloud Sync** - Sync UserData.db to OneDrive/Dropbox
2. **Multi-User** - Share Reference.db, separate UserData per user
3. **Plugins** - Additional reference databases (exotic woods, specialty hardware)
4. **Offline Help** - Help.db works even without internet
5. **Version Rollback** - Keep old Reference.db versions for compatibility

---

## 📚 Related Documents

- `DATABASE_IMPLEMENTATION_TODO.md` - Original single-database design
- `MIGRATION_GUIDE_DATABASE_SPLIT.md` - User-facing migration guide (TBD)
- `CONTRIBUTING.md` - Developer guide (needs update)

---

**Next Steps:**
1. Review and approve this architecture
2. Create `HelpDataManager.vb`
3. Create `ReferenceDataManager.vb`
4. Create `UserDataManager.vb`
5. Begin systematic refactoring

---

**Approved By:** _____________  
**Date:** _____________
