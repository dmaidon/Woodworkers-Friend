# Miter Angle Calculator - Debugging Guide

## Issue 1: Calculator Not Calculating

### Steps to Debug:

1. **Build and Run the Application**
   - Build successful ✅
   - Diagnostic logging has been added

2. **Open the Angles Tab**
   - Navigate to the Angles tab in the application
   - Check the RtbLog (Log tab) for diagnostic messages

3. **Expected Log Messages:**
   ```
   TpAngles_Enter: Initialized=False
   CalculateMiterAngles: Starting
   CalculateMiterAngles: NumSides=4, IsFlat=True
   CalculateMiterAngles: Result - Miter=45.00°, Bevel=0.00°
   CalculateMiterAngles: Completed successfully
   ```

4. **If You See "Controls null" Message:**
   - The controls don't exist or have wrong names
   - Check FrmMain.Designer.vb for control declarations
   - Verify names match exactly:
     - `TxtMiterNumSides`
     - `RbMiterFrameFlat`
     - `RbMiterFrameTilted`
     - `TxtMiterTiltAngle`
     - `LblMiterSawAngle`
     - `LblMiterBevelAngle`
     - `LblComplementaryAngle`

5. **If You See "Suppressed" Message:**
   - The `_suppressMiterCalculation` flag is still True
   - This shouldn't happen but indicates initialization issue

6. **If You See No Messages:**
   - The TpAngles_Enter event isn't firing
   - Check Designer: Does `TpAngles` have `Handles TpAngles.Enter`?
   - Verify in FrmMain.Designer.vb: `Friend WithEvents TpAngles As TabPage`

### Manual Test:

Try typing in the TxtMiterNumSides textbox:
- Type "6" → Should show 30° miter angle
- Type "8" → Should show 22.5° miter angle
- If nothing happens, check the log for "TextChanged" events

---

## Issue 2: Help Content Not Appearing

### Steps to Debug:

1. **Force Help Database Refresh**
   
   Option A - Delete and Recreate:
   ```powershell
   # Close the application first!
   cd "$env:APPDATA\WoodworkersFriend\Resources"
   Remove-Item "Help.db" -Force
   # Now restart the application - it will recreate the database
   ```

   Option B - Manual SQL Check:
   ```powershell
   # Check if help content exists
   cd "$env:APPDATA\WoodworkersFriend\Resources"
   sqlite3 Help.db "SELECT ModuleName, Title, Category FROM HelpContent WHERE ModuleName='MiterAngle';"
   ```

2. **Check Migration Logs**
   - Run the application
   - Check RtbLog for messages like:
     ```
     "Adding Miter Angle Calculator help topic"
     "Miter Angle Calculator help added: 1 topic"
     ```

3. **Verify Help System**
   - Open Help tab in the application
   - Look for category dropdown/combo box
   - Select "Calculators" category
   - Search for "Miter" or "Angle"

4. **Expected Help Entry:**
   ```
   ModuleName: MiterAngle
   Title: Miter Angle Calculator
   Category: Calculators
   SortOrder: 140
   ```

### Manual Database Check:

If you have SQLite Browser installed:
1. Open `%APPDATA%\WoodworkersFriend\Resources\Help.db`
2. Browse the `HelpContent` table
3. Look for row with `ModuleName = 'MiterAngle'`
4. Verify `Category = 'Calculators'`

### If Help Still Doesn't Appear:

**Check Help UI Code:**
- Open `Woodworkers Friend\Partials\FrmMain.Help.vb`
- Find the category filter method
- Verify it includes "Calculators" category
- Check if there's a hardcoded filter excluding new entries

**Check Database Permissions:**
```powershell
# Verify Help.db is not marked read-only during migration
$path = "$env:APPDATA\WoodworkersFriend\Resources\Help.db"
if (Test-Path $path) {
    Get-ItemProperty $path | Select-Object FullName, IsReadOnly, Length
}
```

---

## Quick Fixes

### Fix 1: Force Recalculation
Add a button to manually trigger calculation (temporary debugging):
```vb
Private Sub BtnDebugCalc_Click(sender As Object, e As EventArgs)
    _suppressMiterCalculation = False
    CalculateMiterAngles()
End Sub
```

### Fix 2: Bypass BeginInvoke
If initialization timing is the issue, try direct call:
```vb
' In InitializeMiterAngleCalculator, replace:
BeginInvoke(Sub() CalculateMiterAngles())

' With direct call:
CalculateMiterAngles()
```

### Fix 3: Force Help Migration
Add to FrmMain_Load:
```vb
' Force help migration on every start (debugging only)
DataMigration.AddMiterAngleHelp()
```

---

## Common Issues

### Calculator:
- ❌ Controls have different names in Designer
- ❌ Event handlers not wired up (`Handles` clause missing)
- ❌ Tab control prevents initialization
- ❌ Timing issue with BeginInvoke

### Help:
- ❌ Help.db is read-only during migration
- ❌ Migration check fails (`GetHelpContent` returns empty, not Nothing)
- ❌ Category filter in UI doesn't include "Calculators"
- ❌ Sort order puts it outside visible range

---

## Contact Points for Debugging

1. **View Error Log:**
   - Application → Log tab
   - Or: `%APPDATA%\WoodworkersFriend\ErrorLog.txt`

2. **Check Databases:**
   - Help: `%APPDATA%\WoodworkersFriend\Resources\Help.db`
   - User Data: `%APPDATA%\WoodworkersFriend\WoodworkersFriend.db`

3. **Verify Installation:**
   - Check all DLL files present
   - Verify .NET 10 runtime installed
   - Check Windows event log for crashes

---

## Next Steps After Running Diagnostics

1. **Run the application**
2. **Open Angles tab**
3. **Check Log tab for diagnostic messages**
4. **Copy and share the log output**
5. **Check Help tab → Calculators category**
6. **Report findings**

With the diagnostic logging in place, we'll see exactly where the issue is occurring!
