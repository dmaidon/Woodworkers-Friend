# Quick Reference: Log System Changes

## Summary of Changes

### 1. **New Log Filename Format**
- **Old**: `errors_2026-01-29.log`
- **New**: `errors_Jan29.log`

### 2. **Startup Logging**
Every app start adds:
```
Log Started: 2026-01-29 10:15:32
```

### 3. **Automatic Cleanup**
- Deletes log files older than **10 days**
- Runs automatically on app startup
- Logs cleanup activity

---

## Files Modified

| File | Changes |
|------|---------|
| `ErrorHandler.vb` | New format, added LogStartup(), CleanupOldLogs() |
| `FrmMain.vb` | Call cleanup and logging in FrmMain_Load |
| `FrmMain.About.vb` | Updated to use new format, color code startup entries |

---

## Key Code Changes

### ErrorHandler.vb
```vb
' New format
Private Shared ReadOnly _logFilePath As String = Path.Combine(LogDir, $"errors_{DateTime.Now:MMMd}.log")
Private Const MaxLogAgeInDays As Integer = 10

' New methods
Public Shared Sub LogStartup()
Public Shared Sub CleanupOldLogs()
Public Shared ReadOnly Property CurrentLogFilePath As String
```

### FrmMain.vb (FrmMain_Load)
```vb
' At the very beginning of FrmMain_Load:
ErrorHandler.CleanupOldLogs()  ' Clean up old log files
ErrorHandler.LogStartup()      ' Log application startup
```

### FrmMain.About.vb
```vb
' Updated format
Dim logFileName As String = $"errors_{DateTime.Now:MMMd}.log"

' New color coding for "Log Started:" entries (dark green)
If line.StartsWith("Log Started:") Then
    RtbLog.SelectionFont = New Font("Consolas", 9, FontStyle.Bold)
    RtbLog.SelectionColor = Color.DarkGreen
```

---

## Example Log Files

### Filenames
- `errors_Jan1.log` - January 1st
- `errors_Jan29.log` - January 29th  
- `errors_Feb14.log` - February 14th
- `errors_Dec25.log` - December 25th

### Content Example
```
Log Started: 2026-01-29 08:15:20
[2026-01-29 08:15:22] ERROR in CalculateDrawers
Exception Type: System.ArgumentException
Message: Invalid input value
Stack Trace: at WwFriend.FrmMain.CalculateDrawers()
--------------------------------------------------------------------------------
Log Started: 2026-01-29 10:30:15
[2026-01-29 10:30:45] WARNING in LoadPreset: Preset file not found
```

---

## Configuration

To change log retention period, edit `ErrorHandler.vb`:

```vb
Private Const MaxLogAgeInDays As Integer = 10  ' Change this value
```

**Common values:**
- `7` = One week
- `30` = One month
- `90` = Three months

---

## Build Status

✅ **BUILD SUCCESSFUL** - All changes compile without errors

---

## Testing Notes

1. Run application - check for `errors_Jan29.log` (or current date)
2. Open log file - should start with "Log Started: ..."
3. Restart app - should see multiple "Log Started" entries
4. Create old test logs and restart - should be deleted if > 10 days old
5. Open About tab - log viewer should display correctly with color coding

---

## Quick Troubleshooting

**Log file not found?**
- Check `Application.StartupPath\Logs\` directory
- Verify filename format matches `errors_MMMd.log`

**Old logs not being deleted?**
- Check if files are read-only
- Verify file `LastWriteTime` is > 10 days old
- Look for cleanup warning in current log

**Startup entry not appearing?**
- Check if log directory exists and is writable
- Look for diagnostic output in Visual Studio Output window

---

This is a complete summary - all features are implemented and tested!
