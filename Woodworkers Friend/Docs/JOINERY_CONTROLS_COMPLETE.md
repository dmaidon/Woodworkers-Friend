# Complete Joinery Calculator Control Reference

## Visual Layout for 1200×900 Screen

```
┌──────────────────────────────────────────────────────────────────────────────────────────┐
│ JOINERY CALCULATOR TAB                                                                    │
├────────────────────────────────────────────┬─────────────────────────────────────────────┤
│ LEFT PANEL (400px wide, scrollable)       │ RIGHT PANEL (Fill remaining space)          │
│                                            │                                             │
│ ┌─ MORTISE & TENON ──────────────────────┐│ ┌─ MORTISE & TENON RESULTS ──────────────┐ │
│ │                                         ││ │                                         │ │
│ │ Stock Thickness (in): [0.75    ]       ││ │ Tenon Thickness:  0.250"               │ │
│ │ Stock Width (in):     [3.00    ]       ││ │ Tenon Length:     2.188"               │ │
│ │                                         ││ │ Tenon Width:      2.750"               │ │
│ │ Tenon Type:                             ││ │ Mortise Depth:    2.250"               │ │
│ │   ● Standard                            ││ │ Mortise Width:    0.250"               │ │
│ │   ○ Haunched                            ││ │ Shoulder Offset:  0.125"               │ │
│ │   ○ Through                             ││ │                                         │ │
│ │                                         ││ └─────────────────────────────────────────┘ │
│ └─────────────────────────────────────────┘│                                             │
│                                            │ ┌─ DOVETAIL RESULTS ──────────────────────┐ │
│ ┌─ DOVETAILS ────────────────────────────┐│ │                                         │ │
│ │                                         ││ │ Angle:        8.0° (1:8)               │ │
│ │ Board Thickness (in): [0.75    ]       ││ │ Pin Width:    0.500"                   │ │
│ │ Board Width (in):     [6.00    ]       ││ │ Tail Width:   1.250"                   │ │
│ │ Pin Spacing (in):     [0.50    ]       ││ │ Number of Tails: 3                     │ │
│ │                                         ││ │                                         │ │
│ │ ☑ Hardwood (1:8 angle)                 ││ └─────────────────────────────────────────┘ │
│ │   (Softwood uses 1:7 angle if unchecked)││                                             │
│ │                                         ││ ┌─ BOX JOINT RESULTS ─────────────────────┐ │
│ └─────────────────────────────────────────┘│ │                                         │ │
│                                            │ │ Pin Width:   0.250"                    │ │
│ ┌─ BOX JOINTS ───────────────────────────┐│ │ Pin Count:   25                        │ │
│ │                                         ││ │                                         │ │
│ │ Stock Thickness (in): [0.50    ]       ││ └─────────────────────────────────────────┘ │
│ │ Board Width (in):     [6.00    ]       ││                                             │
│ │                                         ││ ┌─ DADO/GROOVE RESULTS ───────────────────┐ │
│ └─────────────────────────────────────────┘│ │                                         │ │
│                                            │ │ Dado Depth:  0.375"                    │ │
│ ┌─ DADO / GROOVE ────────────────────────┐│ │ Dado Width:  0.500"                    │ │
│ │                                         ││ │                                         │ │
│ │ Stock Thickness (in): [0.75    ]       ││ └─────────────────────────────────────────┘ │
│ │ Shelf Thickness (in): [0.50    ]       ││                                             │
│ │                                         ││ ┌─────────────────────────────────────────┐ │
│ └─────────────────────────────────────────┘│ │                                         │ │
│                                            │ │    [Joint Diagram Visual]               │ │
│ [ Calculate All Joints ]                   │ │                                         │ │
│                                            │ │    500px × 350px                        │ │
│                                            │ │    PictureBox                           │ │
│                                            │ │                                         │ │
│                                            │ └─────────────────────────────────────────┘ │
│                                            │                                             │
└────────────────────────────────────────────┴─────────────────────────────────────────────┘
```

---

## Complete Control List (52 controls total)

### TabPage & Containers
1. `TpJoinery` - TabPage ("Joinery")
2. `SplitContainer` - Split left/right panels
3. `PnlJoineryLeft` - Panel (left side, 400px, scrollable)
4. `PnlJoineryRight` - Panel (right side, fill)

### MORTISE & TENON Section (14 controls)

**GroupBox:**
5. `GbMortiseTenon` - GroupBox ("Mortise & Tenon")

**Input Labels:**
6. `LblMTStockThickness` - Label ("Stock Thickness (in):")
7. `LblMTStockWidth` - Label ("Stock Width (in):")
8. `LblTenonType` - Label ("Tenon Type:")

**Input Controls:**
9. `TxtJointStockThickness` - TextBox (default: "0.75")
10. `TxtJointStockWidth` - TextBox (default: "3.00")
11. `RbTenonStandard` - RadioButton ("Standard") - Checked
12. `RbTenonHaunched` - RadioButton ("Haunched")
13. `RbTenonThrough` - RadioButton ("Through")

**Result GroupBox:**
14. `GbMortiseTenonResults` - GroupBox ("Mortise & Tenon Results")

**Result Display (with descriptive labels):**
15. `LblMTTenonThicknessText` - Label ("Tenon Thickness:")
16. `LblTenonThickness` - Label ("0.000") - Bold
17. `LblMTTenonLengthText` - Label ("Tenon Length:")
18. `LblTenonLength` - Label ("0.000") - Bold
19. `LblMTTenonWidthText` - Label ("Tenon Width:")
20. `LblTenonWidth` - Label ("0.000") - Bold
21. `LblMTMortiseDepthText` - Label ("Mortise Depth:")
22. `LblMortiseDepth` - Label ("0.000") - Bold
23. `LblMTMortiseWidthText` - Label ("Mortise Width:")
24. `LblMortiseWidth` - Label ("0.000") - Bold
25. `LblMTShoulderOffsetText` - Label ("Shoulder Offset:")
26. `LblShoulderOffset` - Label ("0.000") - Bold

### DOVETAILS Section (13 controls)

**GroupBox:**
27. `GbDovetails` - GroupBox ("Dovetails")

**Input Labels:**
28. `LblDTBoardThickness` - Label ("Board Thickness (in):")
29. `LblDTBoardWidth` - Label ("Board Width (in):")
30. `LblDTSpacing` - Label ("Pin Spacing (in):")

**Input Controls:**
31. `TxtDovetailThickness` - TextBox (default: "0.75")
32. `TxtDovetailWidth` - TextBox (default: "6.00")
33. `TxtDovetailSpacing` - TextBox (default: "0.50")
34. `ChkDovetailHardwood` - CheckBox ("Hardwood (1:8 angle)") - Checked

**Result GroupBox:**
35. `GbDovetailResults` - GroupBox ("Dovetail Results")

**Result Display:**
36. `LblDTAngleText` - Label ("Angle:")
37. `LblDovetailAngle` - Label ("0.0°") - Bold
38. `LblDTPinWidthText` - Label ("Pin Width:")
39. `LblDovetailPinWidth` - Label ("0.000") - Bold
40. `LblDTTailWidthText` - Label ("Tail Width:")
41. `LblDovetailTailWidth` - Label ("0.000") - Bold
42. `LblDTCountText` - Label ("Number of Tails:")
43. `LblDovetailCount` - Label ("0") - Bold

### BOX JOINTS Section (9 controls)

**GroupBox:**
44. `GbBoxJoint` - GroupBox ("Box Joints")

**Input Labels:**
45. `LblBJStockThickness` - Label ("Stock Thickness (in):")
46. `LblBJBoardWidth` - Label ("Board Width (in):")

**Input Controls:**
47. `TxtBoxJointThickness` - TextBox (default: "0.50")
48. `TxtBoxJointWidth` - TextBox (default: "6.00")

**Result GroupBox:**
49. `GbBoxJointResults` - GroupBox ("Box Joint Results")

**Result Display:**
50. `LblBJPinWidthText` - Label ("Pin Width:")
51. `LblBoxJointPinWidth` - Label ("0.000") - Bold
52. `LblBJCountText` - Label ("Pin Count:")
53. `LblBoxJointCount` - Label ("0") - Bold

### DADO/GROOVE Section (9 controls)

**GroupBox:**
54. `GbDado` - GroupBox ("Dado / Groove")

**Input Labels:**
55. `LblDadoStockThickness` - Label ("Stock Thickness (in):")
56. `LblDadoShelfThickness` - Label ("Shelf Thickness (in):")

**Input Controls:**
57. `TxtDadoStockThickness` - TextBox (default: "0.75")
58. `TxtDadoShelfThickness` - TextBox (default: "0.50")

**Result GroupBox:**
59. `GbDadoResults` - GroupBox ("Dado/Groove Results")

**Result Display:**
60. `LblDadoDepthText` - Label ("Dado Depth:")
61. `LblDadoDepth` - Label ("0.000") - Bold
62. `LblDadoWidthText` - Label ("Dado Width:")
63. `LblDadoWidth` - Label ("0.000") - Bold

### DIAGRAM & ACTIONS (2 controls)

64. `PbJointDiagram` - PictureBox (500×350, white background)
65. `BtnCalculateJoinery` - Button ("Calculate All Joints") - Optional, auto-calc on change

---

## Recommended Control Sizes

### TextBoxes:
- Width: 100px
- Height: 23px
- Font: Segoe UI, 9pt

### Labels (descriptive text):
- Width: 150px
- Height: 20px
- Font: Segoe UI, 9pt
- TextAlign: MiddleRight

### Labels (result values):
- Width: 100px
- Height: 20px
- Font: Segoe UI, 10pt, Bold
- ForeColor: DarkBlue
- TextAlign: MiddleLeft

### GroupBoxes:
- Padding: 10px
- Font: Segoe UI, 10pt, Bold
- ForeColor: DarkGreen

### RadioButtons:
- Height: 20px
- Font: Segoe UI, 9pt

### CheckBox:
- Height: 20px
- Font: Segoe UI, 9pt

### PictureBox:
- Size: 500×350
- BorderStyle: FixedSingle
- BackColor: White
- Dock: Bottom or Fill

---

## Layout Tips for Designer

### Left Panel (ScrollablePanel):
```
Dock = Left
Width = 400
AutoScroll = True
Padding = 10

Stack GroupBoxes vertically:
  - GbMortiseTenon (top: 10)
  - GbDovetails (top: 190)
  - GbBoxJoint (top: 370)
  - GbDado (top: 500)
  - BtnCalculateJoinery (top: 630)
```

### Right Panel:
```
Dock = Fill
Padding = 10

Stack Result GroupBoxes:
  - GbMortiseTenonResults (top: 10)
  - GbDovetailResults (top: 180)
  - GbBoxJointResults (top: 300)
  - GbDadoResults (top: 380)
  - PbJointDiagram (Dock: Bottom or Fill remaining)
```

---

## Alternative: Tabbed Joinery Interface

If space is too tight, consider sub-tabs:

```
TpJoinery
  ├─ TcJoineryTypes (TabControl)
      ├─ TpMortiseTenon
      ├─ TpDovetails
      ├─ TpBoxJoints
      └─ TpDados
```

This would reduce controls visible at once but adds navigation depth.

---

## Color Scheme

**Input Sections:**
- Background: Color.FromArgb(240, 240, 245) (very light blue-gray)
- Border: DarkGray

**Result Sections:**
- Background: Color.FromArgb(245, 255, 245) (very light green)
- Border: DarkGreen

**Diagram:**
- Background: White
- Grid: LightGray
- Pieces: Various pastel colors

---

## Quick Copy-Paste Control Names

For quick reference when adding to Designer:

```
TpJoinery
GbMortiseTenon, TxtJointStockThickness, TxtJointStockWidth
RbTenonStandard, RbTenonHaunched, RbTenonThrough
LblTenonThickness, LblTenonLength, LblTenonWidth
LblMortiseDepth, LblMortiseWidth, LblShoulderOffset

GbDovetails, TxtDovetailThickness, TxtDovetailWidth, TxtDovetailSpacing
ChkDovetailHardwood
LblDovetailAngle, LblDovetailPinWidth, LblDovetailTailWidth, LblDovetailCount

GbBoxJoint, TxtBoxJointThickness, TxtBoxJointWidth
LblBoxJointPinWidth, LblBoxJointCount

GbDado, TxtDadoStockThickness, TxtDadoShelfThickness
LblDadoDepth, LblDadoWidth

PbJointDiagram
```

---

**Total: 65 controls for complete Joinery Calculator**

This provides professional-grade joinery calculations for all common woodworking joints!
