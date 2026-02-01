# Miter Angle Calculator UI Controls

This document describes the UI controls that need to be added to `FrmMain.Designer.vb` for the Miter Angle Calculator feature.

## Location

The controls should be added to the existing `TpAngles` tab (Angles tab) in the `ScAngles` SplitContainer's Panel2. The Panel1 already contains the Polygon Calculator.

## Required Controls

### Container
- **GbxMiterCalculator** (GroupBox)
  - Text: "Miter Angle Calculator"
  - Dock: Fill
  - Contains all the controls below

### Frame Type Selection
- **CmbMiterFrameType** (ComboBox)
  - Items: 
    - "Flat Frame (Picture Frame)"
    - "Tilted Frame (Shadow Box)"
    - "Crown Molding"
    - "Polygonal Project"
  - DropDownStyle: DropDownList

### Input Controls - Common
- **Label** - "Corner Angle:"
- **TxtMiterCornerAngle** (TextBox)
  - Default: "90"
- **CmbMiterCornerPreset** (ComboBox)
  - Preset corner angles (90°, 120°, 135°, 108°)

### Input Controls - Polygonal Project
- **Label** - "Number of Sides:"
- **TxtMiterSides** (TextBox)
  - Default: "6"
  - Valid range: 3-25

### Input Controls - Tilted Frame
- **Label** - "Tilt Angle:"
- **TxtMiterTiltAngle** (TextBox)
  - Default: "15"
  - Valid range: 0-90°

### Input Controls - Crown Molding
- **Label** - "Spring Angle:"
- **TxtMiterSpringAngle** (TextBox)
  - Default: "45"
- **CmbMiterSpringAngle** (ComboBox)
  - Presets: 38°, 45°, 52°
- **ChkMiterInsideCorner** (CheckBox)
  - Text: "Inside Corner"
  - Default: Checked

### Action Buttons
- **BtnCalculateMiter** (Button)
  - Text: "Calculate"
  - Handles: BtnCalculateMiter.Click

### Result Display
- **LblMiterAngleResult** (Label)
  - Text: "Miter Angle: --"
  - Font: Bold, larger size

- **LblBevelAngleResult** (Label)
  - Text: "Bevel Angle: --"
  - Font: Bold, larger size

- **LblMiterDescription** (Label)
  - Text: ""
  - Font: Regular

- **LblMiterSawCapability** (Label)
  - Text: ""
  - ForeColor: Changes based on validation

- **RtbMiterResults** (RichTextBox)
  - ReadOnly: True
  - Shows detailed results and cutting instructions

## Layout Suggestions

Use a TableLayoutPanel or FlowLayoutPanel for organized layout:

```
┌─────────────────────────────────────┐
│ Miter Angle Calculator              │
├─────────────────────────────────────┤
│ Frame Type: [Dropdown▼]             │
│                                     │
│ Corner Angle: [90   ] [Preset▼]    │
│                                     │
│ [Conditional controls based on type]│
│                                     │
│ [Calculate Button]                  │
│                                     │
│ Results:                            │
│ Miter Angle: 45.00°                 │
│ Bevel Angle: 0.00°                  │
│ Description: Flat frame...          │
│ Saw Capability: ✓ Within range     │
│                                     │
│ ┌─────────────────────────────┐    │
│ │ Detailed Results:           │    │
│ │                             │    │
│ │ (RichTextBox with           │    │
│ │  cutting instructions)      │    │
│ │                             │    │
│ └─────────────────────────────┘    │
└─────────────────────────────────────┘
```

## Control Visibility Logic

The visibility of input controls should change based on the selected frame type:

| Frame Type | Visible Controls |
|------------|------------------|
| Flat Frame | Corner Angle, Corner Preset |
| Tilted Frame | Corner Angle, Corner Preset, Tilt Angle |
| Crown Molding | Corner Angle, Corner Preset, Spring Angle, Spring Preset, Inside/Outside checkbox |
| Polygonal Project | Number of Sides |

## Implementation Notes

1. All controls have defensive null checks in the code (`If control IsNot Nothing`)
2. The code auto-calculates on input changes
3. Error handling is built-in
4. The Calculator button is optional (calculations happen automatically)
5. Results display in both labels and RichTextBox for different detail levels

## Visual Design

- Use consistent spacing (5-10px margins)
- Group related controls (e.g., corner angle + preset together)
- Use panels or group boxes to visually separate sections
- Match the styling of other calculators in the application
- Consider dark/light theme support (use ThemeManager colors)
