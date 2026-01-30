# Phase 7.1 - Joinery Reference UI Setup Guide

## Status: Code Complete, Awaiting Designer Implementation

---

## 📋 Controls Needed in TabPage2 (TpReferences)

### **Required Controls List:**

#### **1. DataGridView**
- **(Name):** `DgvJoineryTypes`
- **WithEvents:** Yes (Friend WithEvents)
- **Location:** Left side of TabPage2
- **Size:** ~500 width x full height
- **Dock:** Left or use SplitContainer

#### **2. Filter Radio Buttons**
- **(Name):** `RbJoineryAll` - Text: "All Types"
- **(Name):** `RbJoineryFrame` - Text: "Frame"
- **(Name):** `RbJoineryBox` - Text: "Box"
- **(Name):** `RbJoineryEdge` - Text: "Edge"
- **(Name):** `RbJoineryBeginner` - Text: "Beginner Friendly"
- **WithEvents:** Yes (each one)
- **Location:** Top of TabPage2 or in a GroupBox
- **GroupName:** "JoineryFilter" (so they're mutually exclusive)

#### **3. Count Label**
- **(Name):** `LblJoineryCount`
- **Text:** "0 joinery types"
- **Location:** Near filter buttons or bottom of grid

#### **4. Details Panel Labels** (Summary Info)
- **(Name):** `LblJoineryName` - Displays joint name
- **(Name):** `LblJoineryCategory` - Displays category
- **(Name):** `LblJoineryDifficulty` - Displays difficulty level (color-coded)
- **(Name):** `LblJoineryStrength` - Displays strength rating
- **(Name):** `LblJoineryGlue` - Displays "Yes" or "No"

#### **5. Details TextBoxes** (Detailed Info)
- **(Name):** `TxtJoineryDescription` - Multiline, ReadOnly, ScrollBars
- **(Name):** `TxtJoineryUses` - Multiline, ReadOnly, ScrollBars
- **(Name):** `TxtJoineryTools` - Multiline, ReadOnly, ScrollBars
- **(Name):** `TxtJoineryStrengthChar` - Multiline, ReadOnly, ScrollBars
- **(Name):** `TxtJoineryReinforcement` - Multiline, ReadOnly, ScrollBars
- **(Name):** `TxtJoineryHistory` - Multiline, ReadOnly, ScrollBars

---

## 🎨 Recommended Layout

```
┌─────────────────────────────────────────────────────────────────┐
│ TabPage2 (Joinery Reference)                                     │
├─────────────────────────────────────────────────────────────────┤
│ Filter: (○) All  (○) Frame  (○) Box  (○) Edge  (○) Beginner    │
│ Count: LblJoineryCount                                           │
├──────────────────────┬──────────────────────────────────────────┤
│                      │                                          │
│                      │ **Summary:**                             │
│                      │ Name: LblJoineryName                     │
│  DgvJoineryTypes     │ Category: LblJoineryCategory             │
│  (Grid of joints)    │ Difficulty: LblJoineryDifficulty         │
│                      │ Strength: LblJoineryStrength             │
│                      │ Glue Required: LblJoineryGlue            │
│                      │                                          │
│                      │ **Description:**                         │
│                      │ TxtJoineryDescription                    │
│                      │                                          │
│                      │ **Typical Uses:**                        │
│                      │ TxtJoineryUses                           │
│                      │                                          │
│                      │ **Required Tools:**                      │
│                      │ TxtJoineryTools                          │
│                      │                                          │
│                      │ **Strength Characteristics:**            │
│                      │ TxtJoineryStrengthChar                   │
│                      │                                          │
│                      │ **Reinforcement Options:**               │
│                      │ TxtJoineryReinforcement                  │
│                      │                                          │
│                      │ **Historical Notes:**                    │
│                      │ TxtJoineryHistory                        │
│                      │                                          │
└──────────────────────┴──────────────────────────────────────────┘
```

---

## 📝 Step-by-Step Designer Implementation

### **Step 1: Locate TabPage2**
1. Open `FrmMain` in Designer (View → Designer or Shift+F7)
2. Find `TcReferences` (References TabControl)
3. Click on `TabPage2`
4. Right-click → Properties
5. Set **(Name):** `TpJoineryReference`
6. Set **Text:** `Joinery Types`

### **Step 2: Add SplitContainer (Recommended)**
1. Drag **SplitContainer** onto TabPage2
2. **(Name):** `ScJoineryMain`
3. **Dock:** Fill
4. **Orientation:** Vertical
5. **SplitterDistance:** 500

### **Step 3: Add Left Panel (Grid & Filters)**
1. In **Panel1** of SplitContainer, add:
   - **GroupBox** for filters at top
   - **DgvJoineryTypes** (DataGridView) below
   - **LblJoineryCount** at bottom

#### **Filter GroupBox:**
- **(Name):** `GbJoineryFilter`
- **Text:** "Filter by Category/Difficulty"
- **Dock:** Top
- **Height:** ~80

**Inside GroupBox, add 5 RadioButtons:**
```vb
' RbJoineryAll
.Name = "RbJoineryAll"
.Text = "All Types"
.Checked = True
.Location = New Point(10, 20)

' RbJoineryFrame
.Name = "RbJoineryFrame"
.Text = "Frame"
.Location = New Point(90, 20)

' RbJoineryBox
.Name = "RbJoineryBox"
.Text = "Box"
.Location = New Point(170, 20)

' RbJoineryEdge
.Name = "RbJoineryEdge"
.Text = "Edge"
.Location = New Point(230, 20)

' RbJoineryBeginner
.Name = "RbJoineryBeginner"
.Text = "Beginner Friendly"
.Location = New Point(10, 45)
```

#### **DataGridView:**
- **(Name):** `DgvJoineryTypes`
- **Dock:** Fill
- **Location:** Below GroupBox

#### **Count Label:**
- **(Name):** `LblJoineryCount`
- **Text:** "0 joinery types"
- **Dock:** Bottom

### **Step 4: Add Right Panel (Details)**
In **Panel2** of SplitContainer:

1. Add **Panel** for summary info at top:
   - **Dock:** Top
   - **Height:** ~150
   - Contains 5 labels with static labels:

```
[Static Label: "Name:"]     [LblJoineryName]
[Static Label: "Category:"] [LblJoineryCategory]
[Static Label: "Difficulty:"] [LblJoineryDifficulty]
[Static Label: "Strength:"] [LblJoineryStrength]
[Static Label: "Glue Required:"] [LblJoineryGlue]
```

2. Add **TabControl** for detailed info:
   - **Dock:** Fill
   - Tabs: Description, Uses, Tools, Strength, Reinforcement, History

**OR simpler: Use Panel with Labels and TextBoxes:**

```vb
' Add these vertically in Panel2:

Label: "Description:"
TxtJoineryDescription (Multiline, ReadOnly, Height: 80, Dock: None)

Label: "Typical Uses:"
TxtJoineryUses (Multiline, ReadOnly, Height: 60, Dock: None)

Label: "Required Tools:"
TxtJoineryTools (Multiline, ReadOnly, Height: 60, Dock: None)

Label: "Strength Characteristics:"
TxtJoineryStrengthChar (Multiline, ReadOnly, Height: 60, Dock: None)

Label: "Reinforcement Options:"
TxtJoineryReinforcement (Multiline, ReadOnly, Height: 50, Dock: None)

Label: "Historical Notes:"
TxtJoineryHistory (Multiline, ReadOnly, Height: 60, Dock: None)
```

**Make all TextBoxes:**
- **ReadOnly:** True
- **Multiline:** True
- **ScrollBars:** Vertical
- **BackColor:** SystemColors.Control

### **Step 5: Add WithEvents Declarations**
In `FrmMain.Designer.vb`, add Friend WithEvents declarations:

```vb
Friend WithEvents DgvJoineryTypes As DataGridView
Friend WithEvents RbJoineryAll As RadioButton
Friend WithEvents RbJoineryFrame As RadioButton
Friend WithEvents RbJoineryBox As RadioButton
Friend WithEvents RbJoineryEdge As RadioButton
Friend WithEvents RbJoineryBeginner As RadioButton
Friend WithEvents LblJoineryCount As Label
Friend WithEvents LblJoineryName As Label
Friend WithEvents LblJoineryCategory As Label
Friend WithEvents LblJoineryDifficulty As Label
Friend WithEvents LblJoineryStrength As Label
Friend WithEvents LblJoineryGlue As Label
Friend WithEvents TxtJoineryDescription As TextBox
Friend WithEvents TxtJoineryUses As TextBox
Friend WithEvents TxtJoineryTools As TextBox
Friend WithEvents TxtJoineryStrengthChar As TextBox
Friend WithEvents TxtJoineryReinforcement As TextBox
Friend WithEvents TxtJoineryHistory As TextBox
```

---

## ✅ Testing Checklist

After adding all controls:

1. **Build** (F6) - Should succeed with no errors
2. **Run** - Application should start
3. **Navigate** to References tab → Joinery Types tab
4. **Verify:**
   - [ ] Grid shows 12 joinery types
   - [ ] Filter buttons work (All, Frame, Box, Edge, Beginner)
   - [ ] Clicking a joint shows details on right
   - [ ] Sorting works (click column headers)
   - [ ] Count label updates
   - [ ] All details display correctly

---

## 📊 Expected Data (12 Joinery Types)

After implementation, you should see:

1. **Mortise & Tenon** (Frame, Intermediate, Strength: 10)
2. **Dovetail (Through)** (Box, Advanced, Strength: 10)
3. **Dovetail (Half-Blind)** (Box, Advanced, Strength: 9)
4. **Box Joint (Finger Joint)** (Box, Beginner, Strength: 8)
5. **Dado** (Carcass, Beginner, Strength: 7)
6. **Rabbet** (Edge, Beginner, Strength: 5)
7. **Lap Joint (Half-Lap)** (Frame, Beginner, Strength: 6)
8. **Bridle Joint** (Frame, Intermediate, Strength: 8)
9. **Biscuit Joint** (Edge, Beginner, Strength: 6)
10. **Dowel Joint** (Frame, Intermediate, Strength: 7)
11. **Pocket Hole** (Modern, Beginner, Strength: 6)
12. **Spline Joint** (Edge, Intermediate, Strength: 7)

---

## 🎨 Design Tips

### **Colors:**
- Difficulty labels change color based on level:
  - Beginner: Green
  - Intermediate: Orange
  - Advanced: Red

### **Grid:**
- Alternating row colors for readability
- Column headers are bold
- Clicking headers sorts data

### **Details:**
- ReadOnly TextBoxes for better appearance
- ScrollBars for long content
- Summary at top, detailed info below

---

## 🔧 Troubleshooting

### **"Control not declared"**
- Make sure WithEvents declarations are in FrmMain.Designer.vb
- Make sure controls are actually added in Designer

### **"Handles clause error"**
- Verify WithEvents in Designer.vb
- Rebuild solution

### **Grid is empty**
- Check database has data (run app once to seed)
- Check `InitializeJoineryReference()` is called
- Check error log for database issues

### **Details don't display**
- Verify control names match exactly
- Check SelectionChanged event is wired up
- Make sure TextBoxes aren't disabled

---

## 🎉 Success!

Once all controls are added, Phase 7.1 will be **100% complete!** You'll have:
- ✅ JoineryTypes database table
- ✅ 12 joinery types seeded
- ✅ Full CRUD operations
- ✅ Beautiful reference UI
- ✅ Filtering and sorting
- ✅ Detailed information display

**Time to implement:** ~20 minutes in Designer

**Status:** Ready for implementation! 🚀
