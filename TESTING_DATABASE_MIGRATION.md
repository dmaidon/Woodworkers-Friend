# Testing Guide - Database Migration Fix (v1.0.1)

## Critical Issue Fixed
**"attempt to write a readonly database" Error** - Prevented all 25 wood species from migrating on first run

---

## Root Cause Analysis

### The Problem
Reference.db file was being marked with Windows read-only file attribute during `FinalizeDatabase()`. On subsequent runs:

1. App starts
2. `ReferenceDataManager` initializes and finds existing Reference.db file
3. File ALREADY has read-only attribute set from previous run
4. `PerformInitialMigration()` runs and tries to write to database
5. ❌ SQLite fails with "attempt to write a readonly database"
6. All 25 wood species fail to migrate
7. App falls back to in-code database

### The Fix
**Two-part solution:**

**Part 1 - Start of Migration:**
```vb
' Remove read-only attribute BEFORE any migrations
Dim refDbPath = IO.Path.Combine(AppResourcesDir, "Reference.db")
If IO.File.Exists(refDbPath) Then
    Dim fileInfo As New IO.FileInfo(refDbPath)
    If fileInfo.IsReadOnly Then
        fileInfo.IsReadOnly = False
        ' Log the removal
    End If
End If
```

**Part 2 - End of Migration:**
```vb
Finally
    ' Reapply read-only attribute after all migrations complete
    Try
        DatabaseManager.Instance.Reference.FinalizeDatabase()
    Catch finalizeEx As Exception
        ' Log error
    End Try
End Try
```

---

## Test Plan

### Test #1: Fresh Install with Clean Database
**Objective:** Verify clean migration from scratch

**Steps:**
1. Close Woodworker's Friend application
2. Delete `C:\VB18\Release\WwFriend\net10.0-windows10.0.26100.0\Data\Resources\Reference.db`
3. Launch application
4. Check log file (latest in Logs folder)

**Expected Results:**
✅ "Starting wood species migration..."
✅ "Found 25 species to migrate"
✅ "Migration complete: 25 succeeded, 0 failed"
✅ NO "attempt to write a readonly database" errors
✅ "Reference.db finalized as read-only"

**Verification:**
```powershell
# Check database exists and is read-only
Get-Item "..\Release\WwFriend\net10.0-windows10.0.26100.0\Data\Resources\Reference.db" | 
  Select-Object Name, IsReadOnly, Length
```
Should show: `IsReadOnly: True`

---

### Test #2: Second Run with Existing Read-Only Database
**Objective:** Verify read-only attribute removal works

**Steps:**
1. Run Test #1 first (creates read-only Reference.db)
2. Verify file is read-only:
   ```powershell
   (Get-Item "..\Release\WwFriend\net10.0-windows10.0.26100.0\Data\Resources\Reference.db").IsReadOnly
   # Should return: True
   ```
3. Close and restart application
4. Check log file

**Expected Results:**
✅ "Removed read-only attribute from Reference.db for migration"
✅ NO migration errors
✅ "Reference.db finalized as read-only"

---

### Test #3: Wood Species Query
**Objective:** Verify data is actually in database

**Steps:**
1. Run application
2. Navigate to References tab → Wood Properties tab
3. Click "All" filter
4. Verify species list populates

**Expected Results:**
✅ 25 wood species displayed in DataGridView
✅ Species sorted alphabetically
✅ Click any species shows detailed information

**SQL Verification:**
```sql
SELECT COUNT(*) FROM WoodSpecies;
-- Should return: 25

SELECT CommonName FROM WoodSpecies ORDER BY CommonName LIMIT 5;
-- Should return: Ash (White), Bamboo, Basswood, Beech (American), Birch (Paper)
```

---

### Test #4: Joinery & Hardware Migration
**Objective:** Verify all reference data migrated

**Steps:**
1. Navigate to References tab → Joinery Types tab
2. Verify 12 joinery types displayed
3. Navigate to References tab → Hardware Standards tab  
4. Verify 15 hardware items displayed

**Expected Results:**
✅ Joinery: Mortise & Tenon, Dovetails, Box Joint, Dado, etc.
✅ Hardware: Euro Hinges, Drawer Slides, Shelf Pins, etc.

---

### Test #5: Cost Data Migration
**Objective:** Verify CSV cost data migrated

**Steps:**
1. Navigate to Board Feet tab
2. Click "Manage Costs" button (if exists)
3. Verify wood costs displayed

**Expected Results:**
✅ 68 wood cost entries (from bfCost.csv)
✅ Wood names in Title Case (not UPPERCASE)
✅ 7 epoxy cost entries (from epoxyCost.csv)

---

## Log File Analysis

### Success Indicators
Look for these log entries in sequence:

```
[timestamp] Starting wood species migration...
[timestamp] Found 25 species to migrate
[timestamp] Migration complete: 25 succeeded, 0 failed
[timestamp] Wood species migration completed successfully
[timestamp] Joinery types seeded: 12 types
[timestamp] Hardware standards seeded: 15 items
[timestamp] Wood costs migrated: 68 items
[timestamp] Epoxy costs migrated: 7 items
[timestamp] Reference.db finalized as read-only
```

### Failure Indicators (Should NOT appear)
❌ "attempt to write a readonly database"
❌ "Migration complete: 0 succeeded, 25 failed"
❌ "Database returned empty! Falling back to in-code database..."

---

## Manual Database Inspection

### Using DB Browser for SQLite
1. Install [DB Browser for SQLite](https://sqlitebrowser.org/)
2. Open `C:\VB18\Release\WwFriend\net10.0-windows10.0.26100.0\Data\Resources\Reference.db`
3. **IMPORTANT:** File is read-only - open in read-only mode

**Expected Tables:**
- WoodSpecies (25 rows)
- JoineryTypes (12 rows)
- HardwareStandards (15 rows)
- WoodCosts (68 rows)
- EpoxyCosts (7 rows)
- DatabaseVersion (1 row)

**Quick Queries:**
```sql
-- Count all tables
SELECT name FROM sqlite_master WHERE type='table';

-- Count wood species
SELECT COUNT(*) AS species_count FROM WoodSpecies;

-- Verify first few species
SELECT CommonName, WoodType, JankaHardness 
FROM WoodSpecies 
ORDER BY CommonName 
LIMIT 5;

-- Check joinery types
SELECT Name, Category, StrengthRating, DifficultyLevel 
FROM JoineryTypes 
ORDER BY Name;
```

---

## Rollback Plan (If Issues Persist)

If migration still fails:

1. **Delete databases and let app recreate:**
   ```powershell
   Remove-Item "..\Release\WwFriend\net10.0-windows10.0.26100.0\Data\Resources\Reference.db" -Force
   Remove-Item "..\Release\WwFriend\net10.0-windows10.0.26100.0\Data\WoodworkersFriend.db" -Force
   ```

2. **Clear file attributes manually:**
   ```powershell
   Set-ItemProperty "..\Release\WwFriend\net10.0-windows10.0.26100.0\Data\Resources\Reference.db" -Name IsReadOnly -Value $false
   ```

3. **Check Windows file permissions:**
   - Right-click Reference.db → Properties → Security
   - Ensure your user account has "Modify" permission

---

## Performance Metrics

Migration should complete in **< 1 second** for all operations:

- Wood Species (25): ~150ms
- Joinery Types (12): ~50ms
- Hardware (15): ~60ms
- Wood Costs (68): ~200ms
- Epoxy Costs (7): ~30ms
- **Total: ~500ms**

If migration takes > 2 seconds, check for disk issues or antivirus interference.

---

## Known Good State

After successful migration, you should have:

### Reference.db
- **Size:** ~50-70 KB
- **Attribute:** Read-Only
- **Location:** `Data\Resources\Reference.db`
- **Tables:** 6 tables with 127 total rows

### Help.db
- **Size:** ~80-100 KB  
- **Attribute:** Read-Only
- **Location:** `Data\Resources\Help.db`
- **Tables:** HelpContent, HelpCategories with 20+ topics

### WoodworkersFriend.db (UserData.db)
- **Size:** ~10-30 KB
- **Attribute:** Writable
- **Location:** `Data\WoodworkersFriend.db`
- **Tables:** UserPreferences, Projects, etc.

---

## Success Criteria

✅ **All 25 wood species** migrate successfully
✅ **All 12 joinery types** seeded
✅ **All 15 hardware items** seeded
✅ **All 68 wood costs** migrated from CSV
✅ **All 7 epoxy costs** migrated from CSV
✅ **No SQLite errors** in log file
✅ **Reference.db is read-only** after migration
✅ **App functions normally** with database-backed reference data

---

## Post-Fix Validation Checklist

- [ ] Test #1 Complete (fresh install)
- [ ] Test #2 Complete (second run)
- [ ] Test #3 Complete (wood species query)
- [ ] Test #4 Complete (joinery & hardware)
- [ ] Test #5 Complete (cost data)
- [ ] Log file shows no errors
- [ ] Database file is read-only after migration
- [ ] All 127 rows present in Reference.db

---

**If all tests pass:** ✅ Ready to package version 1.0.1
**If any test fails:** ⚠️ Check log file and report error details

---

**Updated:** February 3, 2026  
**Version:** 1.0.1 (Patch Release)  
**Status:** Ready for Testing
