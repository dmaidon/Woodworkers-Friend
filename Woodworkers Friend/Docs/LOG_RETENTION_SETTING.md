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
- **Default:** 10 days
- **Purpose:** Control how many days of log files to retain before automatic cleanup

### 2. **Database Storage**
- **Key:** `LogKeepDays`
- **Type:** Integer
- **Category:** System
- **Table:** UserPreferences
- **Default:** 10 days

### 3. **ErrorHandler Changes**
**File:** `Woodworkers Friend\Modules\Utils\ErrorHandler.vb`

**Before:**
```vb
Private Const MaxLogAgeInDays As Integer = 10
```

**After:**
```vb
''' <summary>
''' Maximum age in days before log files are deleted (default: 10 days)
''' This value is loaded from user preferences at startup
''' </summary>
Public Shared Property MaxLogAgeInDays As Integer = 10
```

**Change:** Converted from constant to public shared property so it can be set from user preferences.

### 4. **Load/Save Methods**
**File:** `Woodworkers Friend\FrmMain.vb`

#### LoadLogKeepSetting()
- Called at the end of `LoadUserPreferences()`
- Loads `LogKeepDays` from database (default: 10)
- Validates range (5-100 days)
- Sets `ErrorHandler.MaxLogAgeInDays`
- Updates `NudLogKeep.Value`
- Handles errors gracefully with default fallback

#### SaveLogKeepSetting(value As Integer)
- Called when `NudLogKeep.Value` changes
- Saves to database under key `LogKeepDays`
- Updates `ErrorHandler.MaxLogAgeInDays` immediately
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

### Application Startup
1. `FrmMain_Load()` calls `LoadUserPreferences()`
2. `LoadUserPreferences()` calls `LoadLogKeepSetting()`
3. `LoadLogKeepSetting()`:
   - Reads `LogKeepDays` from database (default: 10)
   - Validates range (5-100)
   - Sets `ErrorHandler.MaxLogAgeInDays`
   - Updates `NudLogKeep.Value`
4. `ErrorHandler.CleanupOldLogs()` uses current `MaxLogAgeInDays` value

### User Changes Setting
1. User navigates to About tab
2. User clicks or tabs to `NudLogKeep`
3. `NudLogKeep_GotFocus` or `NudLogKeep_Enter` selects all text
4. User types new value or uses spinner
5. `NudLogKeep_ValueChanged` fires
6. `SaveLogKeepSetting()` is called:
   - Saves to database
   - Updates `ErrorHandler.MaxLogAgeInDays` immediately
   - Logs the change
7. Next cleanup will use new retention period

---

## 📝 Usage

### For Users
1. Open the application
2. Navigate to the **About** tab
3. Find the **Log Retention Days** control
4. Set the desired number of days (5-100)
5. Value is saved automatically when changed
6. Applies to next log cleanup

### For Developers
```vb
' Get current retention days
Dim retentionDays = ErrorHandler.MaxLogAgeInDays

' Change retention days (typically done via NUD)
ErrorHandler.MaxLogAgeInDays = 30

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

1. `Woodworkers Friend\Modules\Utils\ErrorHandler.vb`
   - Changed `MaxLogAgeInDays` from constant to property

2. `Woodworkers Friend\FrmMain.vb`
   - Added `LoadLogKeepSetting()` method
   - Added `SaveLogKeepSetting()` method
   - Modified `LoadUserPreferences()` to call `LoadLogKeepSetting()`

3. `Woodworkers Friend\Partials\FrmMain.About.vb`
   - Added `NudLogKeep_ValueChanged` handler
   - Added `NudLogKeep_Enter` handler
   - Added `NudLogKeep_GotFocus` handler

4. `Woodworkers Friend\FrmMain.Designer.vb`
   - Already contains `NudLogKeep` with Min=5, Max=100

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
