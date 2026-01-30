# Phase 7.3: CSV Cost Data Migration to Database

## Date: January 30, 2026
## Status: 🚧 **IN PROGRESS** - Core infrastructure complete, needs UI wiring

---

## ✅ **Completed:**

### **1. Database Schema** ✅
Created two new tables in SQLite database:

#### **WoodCosts Table**
```sql
CREATE TABLE WoodCosts (
    WoodCostID INTEGER PRIMARY KEY AUTOINCREMENT,
    Thickness TEXT NOT NULL,
    WoodName TEXT NOT NULL,
    CostPerBoardFoot REAL NOT NULL,
    Active BOOLEAN DEFAULT 1,
    IsUserAdded BOOLEAN DEFAULT 0,
    DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
    LastModified DATETIME DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(Thickness, WoodName)
);
```

**Indexes:**
- `idx_woodcost_active` on Active
- `idx_woodcost_name` on WoodName

#### **EpoxyCosts Table**
```sql
CREATE TABLE EpoxyCosts (
    EpoxyCostID INTEGER PRIMARY KEY AUTOINCREMENT,
    Brand TEXT NOT NULL,
    Type TEXT NOT NULL,
    CostPerGallon REAL NOT NULL,
    DisplayText TEXT,
    Active BOOLEAN DEFAULT 1,
    IsUserAdded BOOLEAN DEFAULT 0,
    DateAdded DATETIME DEFAULT CURRENT_TIMESTAMP,
    LastModified DATETIME DEFAULT CURRENT_TIMESTAMP,
    UNIQUE(Brand, Type)
);
```

**Indexes:**
- `idx_epoxycost_active` on Active
- `idx_epoxycost_brand` on Brand

---

### **2. Model Classes** ✅
**File:** `Woodworkers Friend\Modules\Database\CostDataModels.vb`

```vb
Public Class WoodCost
    Public Property WoodCostID As Integer
    Public Property Thickness As String
    Public Property WoodName As String
    Public Property CostPerBoardFoot As Double
    Public Property Active As Boolean
    Public Property IsUserAdded As Boolean
    Public Property DateAdded As DateTime
    Public Property LastModified As DateTime
    
    Public ReadOnly Property DisplayName As String
        Get
            Return $"{Thickness} {WoodName} - ${CostPerBoardFoot:F2}/bf"
        End Get
    End Property
End Class

Public Class EpoxyCost
    Public Property EpoxyCostID As Integer
    Public Property Brand As String
    Public Property Type As String
    Public Property CostPerGallon As Double
    Public Property DisplayText As String
    Public Property Active As Boolean
    Public Property IsUserAdded As Boolean
    Public Property DateAdded As DateTime
    Public Property LastModified As DateTime
    
    Public ReadOnly Property DisplayName As String
        Get
            Return $"{Brand} {Type} - ${CostPerGallon:F2}/gal"
        End Get
    End Property
End Class
```

---

### **3. Database Manager CRUD Methods** ✅
**File:** `Woodworkers Friend\Modules\Database\DatabaseManager.vb`

**Wood Costs:**
- `GetAllWoodCosts()` - Retrieves all active wood costs
- `AddWoodCost(woodCost)` - Adds new wood cost entry
- `UpdateWoodCost(woodCost)` - Updates existing entry
- `DeleteWoodCost(woodCostID)` - Soft delete (sets Active = 0)

**Epoxy Costs:**
- `GetAllEpoxyCosts()` - Retrieves all active epoxy costs
- `AddEpoxyCost(epoxyCost)` - Adds new epoxy cost entry
- `UpdateEpoxyCost(epoxyCost)` - Updates existing entry
- `DeleteEpoxyCost(epoxyCostID)` - Soft delete (sets Active = 0)

---

### **4. Data Migration** ✅
**File:** `Woodworkers Friend\Modules\Database\DataMigration.vb`

**Added Methods:**
- `MigrateWoodCosts()` - Migrates bfCost.csv to database
- `MigrateEpoxyCosts()` - Migrates epoxyCost.csv to database
- `IsWoodCostsMigrated()` - Checks if migration complete
- `IsEpoxyCostsMigrated()` - Checks if migration complete
- `ParseCsvLine()` - Helper for parsing quoted CSV fields

**Migration integrated into `PerformInitialMigration()`:**
```vb
' Phase 7.3: Migrate cost data from CSV files
If Not IsWoodCostsMigrated() Then
    Dim woodCostCount = MigrateWoodCosts()
    If woodCostCount > 0 Then
        ErrorHandler.LogError(New Exception($"Wood costs migrated: {woodCostCount} items"))
    End If
End If

If Not IsEpoxyCostsMigrated() Then
    Dim epoxyCostCount = MigrateEpoxyCosts()
    If epoxyCostCount > 0 Then
        ErrorHandler.LogError(New Exception($"Epoxy costs migrated: {epoxyCostCount} items"))
    End If
End If
```

**On first run after this update:**
- Reads `Settings\bfCost.csv` (66 entries)
- Reads `Settings\epoxyCost.csv` (8 entries)
- Inserts all into database
- Future runs skip migration (data exists)

---

### **5. Management Form** ✅
**File:** `Woodworkers Friend\Forms\FrmManageCosts.vb`

**Features:**
- Tabbed interface (Wood Costs / Epoxy Costs)
- DataGridView for each type
- **Add** buttons with InputBox prompts
- **Save Changes** - batch update all modified rows
- **Delete** - soft delete (marks inactive)
- Auto-refresh after operations
- Full CRUD operations

**Wood Costs Tab:**
- Columns: Thickness, Wood Name, Cost/BF, Active, Custom, Date Added
- Editable: Thickness, Wood Name, Cost, Active checkbox
- Read-only: Custom flag, Date Added

**Epoxy Costs Tab:**
- Columns: Brand, Type, Cost/Gal, Active, Custom, Date Added
- Editable: Brand, Type, Cost, Active checkbox
- Read-only: Custom flag, Date Added

---

## 🚧 **TODO: Remaining Steps**

### **Step 6: Create Designer File for FrmManageCosts**

You need to create `FrmManageCosts.Designer.vb` with these controls:

**Form Layout:**
```
┌─ Manage Costs ───────────────────────────────────┐
│  ┌─ TabControl (TcCosts) ──────────────────────┐ │
│  │ ┌─ Wood Costs Tab ──────────────────────┐ │ │
│  │ │ ┌─ DataGridView (DgvWoodCosts) ─────┐│ │ │
│  │ │ │                                     ││ │ │
│  │ │ │  (Grid showing wood costs)          ││ │ │
│  │ │ │                                     ││ │ │
│  │ │ └─────────────────────────────────────┘│ │ │
│  │ │ [Add] [Save Changes] [Delete]          │ │ │
│  │ └────────────────────────────────────────┘ │ │
│  │ ┌─ Epoxy Costs Tab ─────────────────────┐ │ │
│  │ │ ┌─ DataGridView (DgvEpoxyCosts) ────┐│ │ │
│  │ │ │                                     ││ │ │
│  │ │ │  (Grid showing epoxy costs)         ││ │ │
│  │ │ │                                     ││ │ │
│  │ │ └─────────────────────────────────────┘│ │ │
│  │ │ [Add] [Save Changes] [Delete]          │ │ │
│  │ └────────────────────────────────────────┘ │ │
│  └──────────────────────────────────────────────┘ │
│                                    [Close]          │
└──────────────────────────────────────────────────┘
```

**Required Controls:**

**Form:**
- Name: `FrmManageCosts`
- Text: "Manage Costs - Wood & Epoxy"
- Size: 900, 600
- MinimumSize: 800, 500

**TabControl:**
- Name: `TcCosts`
- Dock: Fill
- Two TabPages: "Wood Costs" and "Epoxy Costs"

**Wood Costs Tab (TpWoodCosts):**
- `DgvWoodCosts` (DataGridView) - Dock: Fill in top panel
- `PnlWoodButtons` (Panel) - Dock: Bottom, Height: 50
  - `BtnAddWoodCost` - Text: "Add Wood Cost"
  - `BtnSaveWoodChanges` - Text: "Save Changes"
  - `BtnDeleteWoodCost` - Text: "Delete"

**Epoxy Costs Tab (TpEpoxyCosts):**
- `DgvEpoxyCosts` (DataGridView) - Dock: Fill in top panel
- `PnlEpoxyButtons` (Panel) - Dock: Bottom, Height: 50
  - `BtnAddEpoxyCost` - Text: "Add Epoxy Cost"
  - `BtnSaveEpoxyChanges` - Text: "Save Changes"
  - `BtnDeleteEpoxyCost` - Text: "Delete"

**Bottom Panel:**
- `PnlBottom` (Panel) - Dock: Bottom, Height: 50
  - `BtnClose` - Text: "Close", DialogResult: Cancel

---

### **Step 7: Update Board Feet Loading**

**File:** `Woodworkers Friend\Partials\FrmMain.Boardfoot.vb`

Replace the `LoadWoodCosts()` method:

```vb
Friend Sub LoadWoodCosts()
    ' Phase 7.3: Load from database with CSV fallback
    WoodCostList.Clear()

    Try
        ' Try database first
        Dim dbCosts = DatabaseManager.Instance.GetAllWoodCosts()
        
        If dbCosts.Count > 0 Then
            ' Convert WoodCost to WoodCostInfo
            For Each cost In dbCosts
                WoodCostList.Add(New WoodCostInfo With {
                    .Name = cost.WoodName,
                    .Thickness = cost.Thickness,
                    .CostPerBoardFoot = cost.CostPerBoardFoot
                })
            Next
            ErrorHandler.LogError(New Exception($"Loaded {WoodCostList.Count} wood costs from database"), "LoadWoodCosts")
            Return
        End If
    Catch ex As Exception
        ErrorHandler.LogError(ex, "LoadWoodCosts - Database failed, trying CSV fallback")
    End Try

    ' Fallback to CSV if database empty or fails
    Dim filePath As String = Path.Combine(SetDir, "bfCost.csv")
    If Not File.Exists(filePath) Then Return

    Try
        For Each line In File.ReadAllLines(filePath)
            If String.IsNullOrWhiteSpace(line) Then Continue For

            Dim parts = ParseCsvLine(line)
            If parts.Length >= 3 Then
                Dim thickness = parts(0).Replace("""", "").Trim()
                Dim name = parts(1).Replace("""", "").Trim()
                Dim costString = parts(2).Replace("$", "").Replace("""", "").Trim()
                Dim cost As Double

                If Double.TryParse(costString, cost) Then
                    WoodCostList.Add(New WoodCostInfo With {
                        .Name = name,
                        .Thickness = thickness,
                        .CostPerBoardFoot = cost
                    })
                End If
            End If
        Next
    Catch ex As Exception
        MessageBox.Show($"Error loading wood costs from CSV: {ex.Message}", "Load Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Try
End Sub
```

---

### **Step 8: Update Epoxy Pour Loading**

**File:** `Woodworkers Friend\Partials\FrmMain.EpoxyPour.vb`

Replace the `LoadEpoxyCostData()` method:

```vb
Private Sub LoadEpoxyCostData()
    ' Phase 7.3: Load from database with CSV fallback
    Try
        CmbEpoxyCost.Items.Clear()

        ' Try database first
        Dim dbCosts = DatabaseManager.Instance.GetAllEpoxyCosts()
        
        If dbCosts.Count > 0 Then
            For Each cost In dbCosts
                Dim item As New EpoxyCostItem(cost.Brand, cost.Type, cost.CostPerGallon, cost.DisplayName)
                CmbEpoxyCost.Items.Add(item)
            Next
            
            If CmbEpoxyCost.Items.Count > 0 Then
                CmbEpoxyCost.SelectedIndex = 0
            End If
            Return
        End If
    Catch ex As Exception
        ErrorHandler.LogError(ex, "LoadEpoxyCostData - Database failed, trying CSV fallback")
    End Try

    ' Fallback to CSV if database empty or fails
    Dim csvPath As String = Path.Combine(Application.StartupPath, "Settings", "epoxyCost.csv")
    If File.Exists(csvPath) Then
        Dim lines() As String = File.ReadAllLines(csvPath)

        For Each line In lines
            If Not String.IsNullOrWhiteSpace(line) Then
                Dim parts() As String = line.Split(","c)
                If parts.Length >= 3 Then
                    Dim brand As String = parts(0).Trim()
                    Dim type As String = parts(1).Trim()
                    Dim costText As String = parts(2).Trim()

                    If costText.StartsWith("$"c) Then
                        costText = costText.Substring(1)
                    End If

                    Dim cost As Double
                    If Double.TryParse(costText, cost) Then
                        Dim displayText As String = $"{brand} {type} - ${cost}/gal"
                        CmbEpoxyCost.Items.Add(New EpoxyCostItem(brand, type, cost, displayText))
                    End If
                End If
            End If
        Next

        If CmbEpoxyCost.Items.Count > 0 Then
            CmbEpoxyCost.SelectedIndex = 0
        End If
    End If
End Sub
```

---

### **Step 9: Add Menu Item to Open Form**

**Option A: Add to Tools Menu (Recommended)**

1. Open `FrmMain.Designer.vb`
2. Find or create `MenuStrip` for Tools
3. Add menu item:

```vb
Friend WithEvents TsmiManageCosts As ToolStripMenuItem

' In InitializeComponent:
TsmiManageCosts = New ToolStripMenuItem With {
    .Name = "TsmiManageCosts",
    .Text = "Manage Costs...",
    .ToolTipText = "Manage wood and epoxy cost data"
}
ToolsMenu.DropDownItems.Add(TsmiManageCosts)
```

4. Add handler in `FrmMain.vb`:

```vb
Private Sub TsmiManageCosts_Click(sender As Object, e As EventArgs) Handles TsmiManageCosts.Click
    Try
        Using frm As New FrmManageCosts()
            frm.ShowDialog(Me)
        End Using
        
        ' Reload cost lists after form closes
        LoadWoodCosts()
        LoadEpoxyCostData()
    Catch ex As Exception
        ErrorHandler.LogError(ex, "TsmiManageCosts_Click")
        MessageBox.Show($"Error opening cost management: {ex.Message}", 
                       "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    End Try
End Sub
```

**Option B: Add Button to Settings/Preferences Area**

If you have a settings panel, add:
```vb
Friend WithEvents BtnManageCosts As Button

BtnManageCosts = New Button With {
    .Text = "Manage Costs",
    .Size = New Size(120, 30),
    .Location = New Point(10, 10)
}
```

---

### **Step 10: Testing Checklist**

#### **Database Migration Testing:**
- [ ] Delete database file: `%APPDATA%\Woodworkers Friend\WoodworkersFriend.db`
- [ ] Run application
- [ ] Check logs: should see "Wood costs migrated: 66 items"
- [ ] Check logs: should see "Epoxy costs migrated: 8 items"
- [ ] Verify Board Feet combobox populates correctly
- [ ] Verify Epoxy Cost combobox populates correctly

#### **Management Form Testing:**
- [ ] Open "Manage Costs" form
- [ ] **Wood Costs Tab:**
  - [ ] Grid shows 66 entries
  - [ ] Click "Add Wood Cost" - enter test data
  - [ ] Edit existing row - change cost
  - [ ] Click "Save Changes" - verify success
  - [ ] Select row, click "Delete" - verify soft delete
  - [ ] Close and reopen - changes persist
- [ ] **Epoxy Costs Tab:**
  - [ ] Grid shows 8 entries
  - [ ] Click "Add Epoxy Cost" - enter test data
  - [ ] Edit existing row - change cost
  - [ ] Click "Save Changes" - verify success
  - [ ] Select row, click "Delete" - verify soft delete
  - [ ] Close and reopen - changes persist

#### **Integration Testing:**
- [ ] Add new wood cost in management form
- [ ] Close management form
- [ ] Go to Board Feet tab
- [ ] New wood cost appears in dropdown
- [ ] Add new epoxy cost in management form
- [ ] Close management form
- [ ] Go to Epoxy Pour tab
- [ ] New epoxy cost appears in dropdown

---

## 📊 **Benefits of This Implementation:**

### **Before (CSV Files):**
❌ Must edit CSV files manually
❌ No validation
❌ Easy to corrupt file format
❌ No user-friendly editing
❌ No audit trail
❌ Can't soft delete entries

### **After (Database + UI):**
✅ User-friendly edit form
✅ Validation built-in
✅ Safe CRUD operations
✅ Soft delete (mark inactive)
✅ Tracks who added (user vs system)
✅ Tracks date added/modified
✅ Searchable and sortable
✅ Export/import possible (future)
✅ Follows existing patterns (wood species, joinery, hardware)

---

## 🏗️ **Database Architecture:**

### **Table Relationships:**
```
WoodCosts (66 entries from CSV)
    ├─ Used by: FrmMain.Boardfoot.vb (DgvBoardfeet ComboBox)
    └─ Managed by: FrmManageCosts (Wood Costs tab)

EpoxyCosts (8 entries from CSV)
    ├─ Used by: FrmMain.EpoxyPour.vb (CmbEpoxyCost)
    └─ Managed by: FrmManageCosts (Epoxy Costs tab)
```

### **Soft Delete Pattern:**
- Setting `Active = 0` hides entry from dropdowns
- Data preserved for historical calculations
- Can be "un-deleted" by setting `Active = 1`

### **User-Added Flag:**
- `IsUserAdded = 0` - Original CSV data
- `IsUserAdded = 1` - User added custom entry
- Helpful for debugging and support

---

## 📝 **Migration Log Example:**

```
2026-01-30 11:00:00 - Starting wood costs migration from CSV...
2026-01-30 11:00:00 - Wood costs migration complete: 66/66 items inserted
2026-01-30 11:00:00 - Starting epoxy costs migration from CSV...
2026-01-30 11:00:00 - Epoxy costs migration complete: 8/8 items inserted
```

---

## 🔄 **Fallback Strategy:**

Both `LoadWoodCosts()` and `LoadEpoxyCostData()` implement **defensive programming**:

1. ✅ Try database first
2. ⚠️ If database empty or fails → Log error
3. ✅ Fall back to CSV file
4. ⚠️ If CSV fails → Show error message
5. 📝 Log all attempts for troubleshooting

---

## 🎯 **Future Enhancements (Optional):**

### **Export/Import:**
- Export to CSV button
- Import from CSV to add bulk data
- Merge functionality

### **Search/Filter:**
- Search box on management form
- Filter by active/inactive
- Filter by user-added vs system

### **History:**
- Track price changes over time
- View price history for species
- Compare historical costs

### **Reporting:**
- Cost trends report
- Most expensive woods
- Price change alerts

---

## ✅ **Summary:**

**What's Complete:**
- ✅ Database schema
- ✅ Model classes
- ✅ CRUD methods
- ✅ CSV migration
- ✅ Management form logic

**What Needs Completion:**
- 🚧 FrmManageCosts.Designer.vb (create controls)
- 🚧 Update LoadWoodCosts (Board Feet)
- 🚧 Update LoadEpoxyCostData (Epoxy Pour)
- 🚧 Add menu item or button
- 🚧 Testing

**Estimated Time to Complete:** 30-60 minutes

---

**Author:** AI Assistant  
**Date:** January 30, 2026  
**Phase:** 7.3 - CSV Cost Data Migration  
**Status:** Core infrastructure complete, UI wiring needed
