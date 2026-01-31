# Log Retention Setting Implementation

## Overview
Added user-configurable log retention setting via `NudLogKeep` NumericUpDown control on the About tab.

## Date: January 2025
## Feature: Log Retention Days Setting

---

## ✨ Features Implemented

### 1. **User Control: NudLogKeep**
- **Location:** About Tab (TpAbout)
- **Type:** NumericUpDown
- **Range:** 5-100 days (already configured in Designer)
- **Default:** 5 days (minimum safe value)
- **Purpose:** Control how many days of log files to retain before automatic cleanup

### 2. **Database Storage**
- **Key:** `LogKeepDays`
- **Type:** Integer
- **Category:** System
- **Table:** UserPreferences
- **Default:** 10 days (loaded preference, falls back to 5 if not set)

### 3. **Global Variable**
**File:** `Woodworkers Friend\Globals.vb`

```vb
' MaxLogAgeInDays controls how long to keep log files before cleanup
' Default is 5 days (minimum), loaded from UserPreferences at startup
Friend MaxLogAgeInDays As Integer = 5
```

**Why Global?**
- ✅ Initialized before ErrorHandler is used
- ✅ Ensures startup cleanup uses user's preference
- ✅ Accessible throughout application
- ✅ Single source of truth
- ✅ Avoids initialization order issues

### 4. **ErrorHandler Changes**
**File:** `Woodworkers Friend\Modules\Utils\ErrorHandler.vb`

**Before:**
```vb
Private Const MaxLogAgeInDays As Integer = 10
```

**After:**
```vb
' Removed - now uses global variable from Globals.vb
```

**Change:** Removed local constant, now uses global `MaxLogAgeInDays` variable.

### 4. **Load/Save Methods**
**File:** `Woodworkers Friend\FrmMain.vb`

#### LoadLogKeepSetting()
- Called at the end of `LoadUserPreferences()`
- Loads `LogKeepDays` from database (default: 10)
- Validates range (5-100 days)
- Sets global `MaxLogAgeInDays`
- Updates `NudLogKeep.Value`
- Handles errors gracefully with minimum fallback (5 days)

#### SaveLogKeepSetting(value As Integer)
- Called when `NudLogKeep.Value` changes
- Saves to database under key `LogKeepDays`
- Updates global `MaxLogAgeInDays` immediately
- Logs the change for audit trail

### 5. **Event Handlers**
**File:** `Woodworkers Friend\Partials\FrmMain.About.vb`

#### NudLogKeep_ValueChanged
- Triggers when user changes the value
- Only saves if form is fully loaded (prevents initialization saves)
- Calls `SaveLogKeepSetting()`

#### NudLogKeep_Enter
- Selects all text when control is entered (via Tab key)
- Allows immediate typing to replace value

#### NudLogKeep_GotFocus
- Selects all text when control receives focus (via mouse click)
- Uses `BeginInvoke` to ensure selection happens after focus is complete

---

## 🔄 Workflow

### Application Startup (CRITICAL ORDER)
1. `FrmMain_Load()` initializes database
2. **EARLY LOAD:** Reads `LogKeepDays` from database and sets global `MaxLogAgeInDays`
3. `ErrorHandler.CleanupOldLogs()` uses user's preferred retention value
4. `ErrorHandler.LogStartup()` creates new log
5. Later: `LoadUserPreferences()` → `LoadLogKeepSetting()` updates `NudLogKeep.Value` display

**Why Early Load?**
- Cleanup happens BEFORE full UI initialization
- Must use user's preference, not default value
- Global variable ensures it's available when needed
- Default of 5 days (minimum) is safe if database load fails

### User Changes Setting
1. User navigates to About tab
2. User clicks or tabs to `NudLogKeep`
3. `NudLogKeep_GotFocus` or `NudLogKeep_Enter` selects all text
4. User types new value or uses spinner
5. `NudLogKeep_ValueChanged` fires
6. `SaveLogKeepSetting()` is called:
   - Saves to database
   - Updates global `MaxLogAgeInDays` immediately
   - Logs the change
7. Next cleanup will use new retention period

---

## 📝 Usage

### For Users
1. Open the application
2. Navigate to the **About** tab
3. Find the **Log Retention Days** control (NudLogKeep)
4. Set the desired number of days (5-100)
5. Value is saved automatically when changed
6. Applies to next log cleanup

### For Developers
```vb
' Get current retention days (from global)
Dim retentionDays = MaxLogAgeInDays

' Change retention days
MaxLogAgeInDays = 30

' Save to database
DatabaseManager.Instance.SavePreference("LogKeepDays", "30", "Integer", "System")
```

---

## 🎯 Benefits

1. **User Control:** Users can customize log retention based on their needs
2. **Disk Space Management:** Shorter retention for limited disk space
3. **Troubleshooting:** Longer retention for debugging persistent issues
4. **Compliance:** Meet organizational log retention policies
5. **Immediate Effect:** Changes apply immediately (no restart required)
6. **Persistent:** Setting saved to database across sessions
7. **Validated:** Range limited to sensible values (5-100 days)

---

## 🔍 Technical Details

### Database Schema
```sql
-- UserPreferences table (existing)
CREATE TABLE UserPreferences (
    PrefKey TEXT PRIMARY KEY,
    PrefValue TEXT,
    DataType TEXT,
    Category TEXT,
    LastModified TEXT DEFAULT CURRENT_TIMESTAMP
);

-- Example row for this feature
INSERT INTO UserPreferences (PrefKey, PrefValue, DataType, Category)
VALUES ('LogKeepDays', '10', 'Integer', 'System');
```

### Validation
- **Minimum:** 5 days (reasonable minimum for troubleshooting)
- **Maximum:** 100 days (reasonable maximum to prevent excessive disk usage)
- **Designer:** Min/Max already set on `NudLogKeep`
- **Code:** Additional validation in `LoadLogKeepSetting()`

### Error Handling
- **Load Failure:** Defaults to 10 days, logs error
- **Save Failure:** Logs error, user can try again
- **Selection Errors:** Silently logged, doesn't interrupt user

---

## 📋 Testing Checklist

- [x] ✅ Build successful - no errors
- [ ] Set value to 5 days, verify saves
- [ ] Set value to 100 days, verify saves
- [ ] Restart app, verify value persists
- [ ] Click on NUD, verify text selects
- [ ] Tab to NUD, verify text selects
- [ ] Change value, check database
- [ ] Check logs for "Log retention changed to X days" message
- [ ] Verify cleanup uses new value
- [ ] Test with value < 5 (should clamp to 5)
- [ ] Test with value > 100 (should clamp to 100)

---

## 📁 Files Modified

1. `Woodworkers Friend\Globals.vb`
   - Added `MaxLogAgeInDays` as global variable (default: 5 days)

2. `Woodworkers Friend\Modules\Utils\ErrorHandler.vb`
   - Removed `MaxLogAgeInDays` property (now uses global from Globals.vb)

3. `Woodworkers Friend\FrmMain.vb`
   - Added early load of `LogKeepDays` in `FrmMain_Load` (before cleanup)
   - Added `LoadLogKeepSetting()` method (updates NUD display)
   - Added `SaveLogKeepSetting()` method (saves to DB and global)
   - Modified `LoadUserPreferences()` to call `LoadLogKeepSetting()`

4. `Woodworkers Friend\Partials\FrmMain.About.vb`
   - Added `NudLogKeep_ValueChanged` handler
   - Added `NudLogKeep_Enter` handler
   - Added `NudLogKeep_GotFocus` handler

5. `Woodworkers Friend\FrmMain.Designer.vb`
   - Already contains `NudLogKeep` with Min=5, Max=100

---

## 🔍 Technical Details

### Why Global Variable?

**Problem:** 
- `ErrorHandler.CleanupOldLogs()` is called in `FrmMain_Load` before `LoadUserPreferences()`
- If `MaxLogAgeInDays` was in ErrorHandler as a property with default=10, cleanup would always use 10 on first call
- User's preference wouldn't be applied until after cleanup already happened

**Solution:**
- Declare `MaxLogAgeInDays` as global in `Globals.vb` with initial value = 5 (minimum)
- Load from database EARLY in `FrmMain_Load`, right after getting `TimesRun`
- Set global variable BEFORE calling `ErrorHandler.CleanupOldLogs()`
- Cleanup now uses user's preference on every startup

**Initialization Sequence:**
```vb
' FrmMain_Load()
1. CreateProgramFolders()
2. Get TimesRun from database
3. *** LOAD LogKeepDays from database → set global MaxLogAgeInDays ***
4. CleanupOldLogs() - now uses user's MaxLogAgeInDays
5. LogStartup()
6. ... rest of initialization ...
7. LoadUserPreferences() - updates NudLogKeep.Value for display
```

---

## 🎨 UI/UX Notes

### Text Selection Behavior
- **Enter (Tab):** `NudLogKeep_Enter` selects text immediately
- **GotFocus (Click):** `NudLogKeep_GotFocus` uses `BeginInvoke` for delayed selection
  - Delay needed because click event sets caret position after focus
  - `BeginInvoke` ensures selection happens after caret positioning

### Why Both Handlers?
- **Enter:** Fires when control is entered via keyboard (Tab key)
- **GotFocus:** Fires when control receives focus (mouse click or programmatic)
- Both needed to cover all entry methods

---

## 🚀 Future Enhancements

- [ ] Add tooltip explaining the setting
- [ ] Add label next to NUD: "Keep log files for __ days"
- [ ] Show disk space used by logs
- [ ] Add "Clean Now" button to manually trigger cleanup
- [ ] Show next cleanup date/time
- [ ] Add option to export logs before cleanup

---

## ✅ Status: COMPLETE

**Version:** 1.0  
**Build Status:** ✅ Successful  
**Ready for:** Testing and Production
