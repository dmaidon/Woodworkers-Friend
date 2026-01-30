# Phase 7.2 - Hardware Standards UI Setup Guide

## Status: Code Complete, Awaiting Designer Implementation

---

## 📋 Controls Needed in TabPage3 (TcReferences)

### **Required Controls List:**

#### **1. DataGridView**
- **(Name):** `DgvHardware`
- **WithEvents:** Yes (Friend WithEvents)
- **Location:** Left side of TabPage3
- **Size:** ~500 width x full height
- **Dock:** Left or use SplitContainer

#### **2. Filter Radio Buttons**
- **(Name):** `RbHardwareAll` - Text: "All Types"
- **(Name):** `RbHardwareHinges` - Text: "Hinges"
- **(Name):** `RbHardwareSlides` - Text: "Slides"
- **(Name):** `RbHardwareShelf` - Text: "Shelf Support"
- **(Name):** `RbHardwareFasteners` - Text: "Fasteners"
- **WithEvents:** Yes (each one)
- **Location:** Top of TabPage3 or in a GroupBox
- **GroupName:** "HardwareFilter" (so they're mutually exclusive)

#### **3. Count Label**
- **(Name):** `LblHardwareCount`
- **Text:** "0 hardware items"
- **Location:** Near filter buttons or bottom of grid

#### **4. Details Panel Labels** (Summary Info)
- **(Name):** `LblHardwareType` - Displays hardware type
- **(Name):** `LblHardwareCategory` - Displays category
- **(Name):** `LblHardwareBrand` - Displays brand
- **(Name):** `LblHardwareDimensions` - Displays dimensions
- **(Name):** `LblHardwareWeight` - Displays weight capacity

#### **5. Details TextBoxes** (Detailed Info)
- **(Name):** `TxtHardwareDescription` - Multiline, ReadOnly, ScrollBars
- **(Name):** `TxtHardwareUses` - Multiline, ReadOnly, ScrollBars
- **(Name):** `TxtHardwareMounting` - Multiline, ReadOnly, ScrollBars
- **(Name):** `TxtHardwareInstallation` - Multiline, ReadOnly, ScrollBars
- **(Name):** `TxtHardwarePartNumber` - Single line, ReadOnly

---

## 🎨 Recommended Layout

```
┌─────────────────────────────────────────────────────────────────┐
│ TabPage3 (Hardware Standards)                                    │
├─────────────────────────────────────────────────────────────────┤
│ Filter: (○) All  (○) Hinges  (○) Slides  (○) Shelf  (○) Fasteners│
│ Count: LblHardwareCount                                          │
├──────────────────────┬──────────────────────────────────────────┤
│                      │                                          │
│                      │ **Summary:**                             │
│                      │ Type: LblHardwareType                    │
│  DgvHardware        │ Category: LblHardwareCategory            │
│  (Grid of hardware) │ Brand: LblHardwareBrand                  │
│                      │ Dimensions: LblHardwareDimensions        │
│                      │ Weight Capacity: LblHardwareWeight       │
│                      │                                          │
│                      │ **Description:**                         │
│                      │ TxtHardwareDescription                   │
│                      │                                          │
│                      │ **Typical Uses:**                        │
│                      │ TxtHardwareUses                          │
│                      │                                          │
│                      │ **Mounting Requirements:**               │
│                      │ TxtHardwareMounting                      │
│                      │                                          │
│                      │ **Installation Notes:**                  │
│                      │ TxtHardwareInstallation                  │
│                      │                                          │
│                      │ **Part Number:**                         │
│                      │ TxtHardwarePartNumber                    │
│                      │                                          │
└──────────────────────┴──────────────────────────────────────────┘
```

---

## 📝 Step-by-Step Designer Implementation

### **Step 1: Locate TabPage3**
1. Open `FrmMain` in Designer (View → Designer or Shift+F7)
2. Find `TcReferences` (References TabControl)
3. Click on `TabPage3`
4. Right-click → Properties
5. Set **(Name):** `TpHardwareStandards`
6. Set **Text:** `Hardware`

### **Step 2: Add SplitContainer (Recommended)**
1. Drag **SplitContainer** onto TabPage3
2. **(Name):** `ScHardwareMain`
3. **Dock:** Fill
4. **Orientation:** Vertical
5. **SplitterDistance:** 500

### **Step 3: Add Left Panel (Grid & Filters)**

#### **Filter GroupBox:**
- **(Name):** `GbHardwareFilter`
- **Text:** "Filter by Category"
- **Dock:** Top
- **Height:** ~80

**Inside GroupBox, add 5 RadioButtons:**
```vb
' RbHardwareAll
.Name = "RbHardwareAll"
.Text = "All Types"
.Checked = True
.Location = New Point(10, 20)

' RbHardwareHinges
.Name = "RbHardwareHinges"
.Text = "Hinges"
.Location = New Point(90, 20)

' RbHardwareSlides
.Name = "RbHardwareSlides"
.Text = "Slides"
.Location = New Point(160, 20)

' RbHardwareShelf
.Name = "RbHardwareShelf"
.Text = "Shelf Support"
.Location = New Point(230, 20)

' RbHardwareFasteners
.Name = "RbHardwareFasteners"
.Text = "Fasteners"
.Location = New Point(340, 20)
```

#### **DataGridView:**
- **(Name):** `DgvHardware`
- **Dock:** Fill

#### **Count Label:**
- **(Name):** `LblHardwareCount`
- **Text:** "0 hardware items"
- **Dock:** Bottom

### **Step 4: Add Right Panel (Details)**

1. Add **Panel** for summary info at top:
   - **Dock:** Top
   - **Height:** ~150

```
[Static Label: "Type:"]             [LblHardwareType]
[Static Label: "Category:"]         [LblHardwareCategory]
[Static Label: "Brand:"]            [LblHardwareBrand]
[Static Label: "Dimensions:"]       [LblHardwareDimensions]
[Static Label: "Weight Capacity:"]  [LblHardwareWeight]
```

2. Add TextBoxes below:

```vb
Label: "Description:"
TxtHardwareDescription (Multiline, ReadOnly, Height: 80, Dock: None)

Label: "Typical Uses:"
TxtHardwareUses (Multiline, ReadOnly, Height: 60, Dock: None)

Label: "Mounting Requirements:"
TxtHardwareMounting (Multiline, ReadOnly, Height: 60, Dock: None)

Label: "Installation Notes:"
TxtHardwareInstallation (Multiline, ReadOnly, Height: 60, Dock: None)

Label: "Part Number:"
TxtHardwarePartNumber (Single line, ReadOnly, Height: 25, Dock: None)
```

**Make all TextBoxes:**
- **ReadOnly:** True
- **Multiline:** True (except PartNumber)
- **ScrollBars:** Vertical
- **BackColor:** SystemColors.Control

### **Step 5: Add WithEvents Declarations**

In `FrmMain.Designer.vb`, add Friend WithEvents declarations:

```vb
Friend WithEvents DgvHardware As DataGridView
Friend WithEvents RbHardwareAll As RadioButton
Friend WithEvents RbHardwareHinges As RadioButton
Friend WithEvents RbHardwareSlides As RadioButton
Friend WithEvents RbHardwareShelf As RadioButton
Friend WithEvents RbHardwareFasteners As RadioButton
Friend WithEvents LblHardwareCount As Label
Friend WithEvents LblHardwareType As Label
Friend WithEvents LblHardwareCategory As Label
Friend WithEvents LblHardwareBrand As Label
Friend WithEvents LblHardwareDimensions As Label
Friend WithEvents LblHardwareWeight As Label
Friend WithEvents TxtHardwareDescription As TextBox
Friend WithEvents TxtHardwareUses As TextBox
Friend WithEvents TxtHardwareMounting As TextBox
Friend WithEvents TxtHardwareInstallation As TextBox
Friend WithEvents TxtHardwarePartNumber As TextBox
```

---

## ✅ Testing Checklist

After adding all controls:

1. **Build** (F6) - Should succeed with no errors
2. **Run** - Application should start
3. **Navigate** to References tab → Hardware tab
4. **Verify:**
   - [ ] Grid shows 16 hardware items
   - [ ] Filter buttons work (All, Hinges, Slides, Shelf, Fasteners)
   - [ ] Clicking a hardware item shows details on right
   - [ ] Sorting works (click column headers)
   - [ ] Count label updates
   - [ ] All details display correctly

---

## 📊 Expected Data (16 Hardware Items)

After implementation, you should see:

**Hinges (3):**
1. European (Euro) Hinge - 107°
2. Butt Hinge - 2" x 1.5"
3. Overlay Hinge - Non-Mortise

**Slides (2):**
4. Full Extension Ball-Bearing Slide - Side Mount
5. Undermount Soft-Close Slide

**Shelf Support (2):**
6. Shelf Pin - 5mm
7. Shelf Pin - 1/4"

**Brackets (2):**
8. Corner Brace - 2" x 2"
9. Table Leg Bracket - Angled

**Fasteners (2):**
10. Wood Screw - #8 x 1.5"
11. Confirmat Screw - 5mm x 50mm

**Pulls & Knobs (2):**
12. Bar Pull - 3" Center-to-Center
13. Knob - 1.25" Diameter

**Table Legs (1):**
14. Tapered Table Leg - 29"

**Casters (1):**
15. Swivel Caster - 3" Wheel

---

## 🎉 Success!

Once all controls are added, Phase 7.2 will be **100% complete!** You'll have:
- ✅ HardwareStandards database table
- ✅ 16 hardware items seeded
- ✅ Full CRUD operations
- ✅ Beautiful reference UI
- ✅ Filtering and sorting
- ✅ Detailed specifications display

**Time to implement:** ~20 minutes in Designer

**Status:** Ready for implementation! 🚀
