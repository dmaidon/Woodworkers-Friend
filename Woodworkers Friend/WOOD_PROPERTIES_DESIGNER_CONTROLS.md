# Wood Properties Reference - Designer Controls

## Date: January 29, 2026
## TabPage: TpWoodProperties (formerly TabPage1)

---

## ✅ Complete Control List - Only 12 Controls!

### **1. Filter Panel (Top Section)**
```visualbasic
' Container panel for filter controls
Friend WithEvents PnlWoodFilters As Panel
' Location: Top of TpWoodProperties
' Height: 40
' Dock: Top

' Filter radio buttons
Friend WithEvents RbWoodAll As RadioButton
' Text: "All"
' Checked: True (default)
' Location: (10, 10) within PnlWoodFilters

Friend WithEvents RbWoodHardwoods As RadioButton
' Text: "Hardwoods"
' Location: (80, 10) within PnlWoodFilters

Friend WithEvents RbWoodSoftwoods As RadioButton
' Text: "Softwoods"
' Location: (200, 10) within PnlWoodFilters

' Search textbox
Friend WithEvents TxtWoodSearch As TextBox
' Location: (350, 8) within PnlWoodFilters
' Width: 200
' PlaceholderText: "Search species..."

' Clear search button
Friend WithEvents BtnWoodClearSearch As Button
' Text: "Clear"
' Location: (555, 7) within PnlWoodFilters
' Size: (60, 25)
```

### **2. Main Data Grid**
```visualbasic
Friend WithEvents DgvWoodProperties As DataGridView
' Location: Below PnlWoodFilters
' Dock: Fill (or Top with specific height like 400)
' Columns configured in code (7 columns + 1 hidden)
```

### **3. Details Panel (Bottom Section)**
```visualbasic
' Container panel
Friend WithEvents PnlWoodDetails As Panel
' Location: Below DgvWoodProperties
' Height: 180
' Dock: Bottom

' Details header label
Friend WithEvents LblWoodDetailsHeader As Label
' Text: "Details for: (Select a wood species)"
' Font: Bold, 10pt
' Location: (10, 10) within PnlWoodDetails
' AutoSize: True

' Details rich textbox
Friend WithEvents RtbWoodDetails As RichTextBox
' Location: (10, 35) within PnlWoodDetails
' Size: Fill remaining space (Width: PnlWidth - 20, Height: 100)
' ReadOnly: True
' BorderStyle: FixedSingle
' Font: Segoe UI, 9pt
```

### **4. Action Buttons**
```visualbasic
' Compare button
Friend WithEvents BtnCompareWoods As Button
' Text: "Compare Selected..."
' Location: Bottom-right of PnlWoodDetails
' Size: (140, 30)

' Export button
Friend WithEvents BtnExportWoodData As Button
' Text: "Export to CSV..."
' Location: Next to BtnCompareWoods
' Size: (120, 30)

' Print button
Friend WithEvents BtnPrintWoodData As Button
' Text: "Print..."
' Location: Next to BtnExportWoodData
' Size: (80, 30)
```

---

## 🎨 Layout Diagram

```
┌────────────────────────────────────────────────────────────────┐
│ TpWoodProperties (TabPage)                                      │
├────────────────────────────────────────────────────────────────┤
│ ┌────────────────────────────────────────────────────────────┐ │
│ │ PnlWoodFilters (Dock: Top, Height: 40)                     │ │
│ │  ○ All  ○ Hardwoods  ○ Softwoods   Search:[____] [Clear]  │ │
│ └────────────────────────────────────────────────────────────┘ │
│ ┌────────────────────────────────────────────────────────────┐ │
│ │ DgvWoodProperties (Dock: Fill or Top with Height: 400)    │ │
│ │ Species     │Janka│Sp.Gr│Moist%│Density│Shrink R│Shrink T││ │
│ │ ───────────┼─────┼─────┼──────┼───────┼────────┼────────││ │
│ │ Ash         │1,320│ 0.60│  12% │ 41    │  4.9%  │  7.8%  ││ │
│ │ Basswood    │  410│ 0.37│  12% │ 26    │  6.6%  │  9.3%  ││ │
│ │ Oak (Red)   │1,290│ 0.63│  12% │ 44    │  4.0%  │  8.6%  ││ │
│ │ ...         │     │     │      │       │        │        ││ │
│ └────────────────────────────────────────────────────────────┘ │
│ ┌────────────────────────────────────────────────────────────┐ │
│ │ PnlWoodDetails (Dock: Bottom, Height: 180)                 │ │
│ │ Details for: Oak (Red)                                     │ │
│ │ ┌────────────────────────────────────────────────────────┐ │ │
│ │ │ RtbWoodDetails (ReadOnly, multi-line)                  │ │ │
│ │ │ ═══ TYPICAL USES ═══                                   │ │ │
│ │ │ Furniture, cabinetry, flooring...                      │ │ │
│ │ │                                                        │ │ │
│ │ │ ═══ WORKABILITY ═══                                    │ │ │
│ │ │ Moderate difficulty. Machines well...                  │ │ │
│ │ └────────────────────────────────────────────────────────┘ │ │
│ │ [Compare Selected...] [Export to CSV...] [Print...]       │ │
│ └────────────────────────────────────────────────────────────┘ │
└────────────────────────────────────────────────────────────────┘
```

---

## 📋 Control Properties Quick Reference

| Control | Type | Properties |
|---------|------|------------|
| **PnlWoodFilters** | Panel | Dock: Top, Height: 40, BackColor: Control |
| **RbWoodAll** | RadioButton | Text: "All", Checked: True, AutoSize: True |
| **RbWoodHardwoods** | RadioButton | Text: "Hardwoods", AutoSize: True |
| **RbWoodSoftwoods** | RadioButton | Text: "Softwoods", AutoSize: True |
| **TxtWoodSearch** | TextBox | Width: 200, PlaceholderText: "Search species..." |
| **BtnWoodClearSearch** | Button | Text: "Clear", Size: (60, 25) |
| **DgvWoodProperties** | DataGridView | Dock: Fill, ReadOnly: True, SelectionMode: FullRowSelect |
| **PnlWoodDetails** | Panel | Dock: Bottom, Height: 180, BackColor: Control |
| **LblWoodDetailsHeader** | Label | Font: Bold 10pt, Text: "Details for: ..." |
| **RtbWoodDetails** | RichTextBox | ReadOnly: True, Multiline: True, ScrollBars: Vertical |
| **BtnCompareWoods** | Button | Text: "Compare Selected...", Size: (140, 30) |
| **BtnExportWoodData** | Button | Text: "Export to CSV...", Size: (120, 30) |
| **BtnPrintWoodData** | Button | Text: "Print...", Size: (80, 30) |

---

## 🔧 DataGridView Column Configuration

*Columns are auto-configured in code by `InitializeWoodPropertiesGrid()`*

1. **Species** - CommonName (30% width)
2. **Janka** - JankaHardness, Format: N0, Align: Right (12%)
3. **Sp.Gr** - SpecificGravity, Format: 0.00, Align: Right (10%)
4. **Moist%** - MoistureContent, Format: 0%, Align: Right (10%)
5. **Density** - Density, Format: N0, Align: Right (12%)
6. **Shrink R** - ShrinkageRadial, Format: 0.0%, Align: Right (13%)
7. **Shrink T** - ShrinkageTangential, Format: 0.0%, Align: Right (13%)
8. **WoodType** - Hidden column for filtering

---

## 🚀 Initialization Steps

### 1. Add to FrmMain Constructor/Load
```visualbasic
Private Sub FrmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    Try
        _loading = True
        
        ' ... existing initialization code ...
        
        ' Initialize wood properties reference
        InitializeWoodPropertiesReference()
        
        _loading = False
    Catch ex As Exception
        ErrorHandler.HandleError(ex, "FrmMain_Load", showToUser:=True)
    End Try
End Sub
```

### 2. TabPage Properties
```
Name: TpWoodProperties
Text: "Wood Properties"
TabIndex: As appropriate in your tab control
```

---

## 📚 Data Included

### Hardwoods (13 species):
- Ash (White)
- Basswood
- Beech (American)
- Birch (Yellow)
- Cherry (Black)
- Hickory
- Mahogany (Genuine)
- Maple (Hard/Sugar)
- Oak (Red)
- Oak (White)
- Poplar (Yellow)
- Walnut (Black)

### Softwoods (4 species):
- Cedar (Western Red)
- Cypress (Bald)
- Douglas Fir
- Pine (Eastern White)
- Pine (Southern Yellow)

*Each species includes: Janka hardness, specific gravity, moisture content, density, shrinkage rates, typical uses, workability notes, and cautions.*

---

## ✅ Features Implemented

1. ✅ **Real-time Search** - Filter by species name as you type
2. ✅ **Wood Type Filter** - Show all, hardwoods only, or softwoods only
3. ✅ **Sortable Columns** - Click headers to sort (automatic)
4. ✅ **Detailed Information** - Click any species to see full details
5. ✅ **Export to CSV** - Save data to spreadsheet
6. ✅ **Tooltips** - Helpful explanations on all controls
7. ✅ **Professional Appearance** - Alternating row colors, proper formatting

---

## 🎯 Usage Example

```vb
' User types "oak" in search box
→ Only Oak (Red) and Oak (White) displayed

' User clicks Oak (Red) row
→ Details panel shows:
  - Typical Uses: Furniture, cabinetry...
  - Workability: Moderate difficulty...
  - Cautions: Large open pores require filler...

' User clicks "Export to CSV..."
→ SaveFileDialog opens
→ All displayed data exported to CSV format
```

---

## 📝 Files Created

1. ✅ `Modules\References\WoodPropertiesModels.vb` - Data model
2. ✅ `Modules\References\WoodPropertiesDatabase.vb` - Wood species data (17 species)
3. ✅ `Partials\FrmMain.WoodProperties.vb` - UI logic and event handlers
4. ✅ `WOOD_PROPERTIES_DESIGNER_CONTROLS.md` - This document

---

## 🔍 Quick Testing Steps

1. Add all 12 controls to TpWoodProperties
2. Build project (should compile without errors)
3. Run application and navigate to Wood Properties tab
4. Verify data grid displays 17 wood species
5. Click different species - details should update
6. Type in search box - list should filter
7. Toggle filter radio buttons - list should update
8. Click Export - CSV should save successfully
9. Verify tooltips appear on hover

---

## 💡 Benefits of This Design

✅ **Minimal Controls** - Only 12 instead of 30+  
✅ **Easy to Maintain** - Add species by editing data list  
✅ **Professional Look** - Clean table with details panel  
✅ **Built-in Features** - Sorting, filtering, export  
✅ **Comprehensive Data** - 17 species with full details  
✅ **User-Friendly** - Intuitive interface, helpful tooltips  
✅ **Extensible** - Easy to add more species or properties  

Ready to implement! Just add the 12 controls to the Designer and it's done! 🎉
