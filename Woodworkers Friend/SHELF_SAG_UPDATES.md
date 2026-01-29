# Shelf Sag Support Type - Final Updates

## Date: January 29, 2026

---

## ✅ Updates Completed

### 1. **Control Naming Standardization**
Changed from `TxtDadoDepth2` to `TxtDadoDepth1` for consistency, with backward compatibility maintained.

### 2. **Better UX: ReadOnly Instead of Hidden**
**Changed approach from hiding controls to using ReadOnly state:**
- All controls remain visible at all times
- Inactive textboxes are set to ReadOnly with gray background
- Active textboxes are editable with white background
- Users can see all options and configured values simultaneously
- More intuitive and less jarring than hiding/showing controls

### 3. **Default Selection**
- `RbSupportBracket` is now explicitly set as **default checked**
- Ensures bracket support is selected on first load

### 4. **Enhanced State Management**
Updated `UpdateSupportTypeVisibility()` to manage ReadOnly state and background colors:

**Bracket Support (selected - active):**
- `TxtShelfBracketWidth.ReadOnly = False` (white background)
- `TxtDadoDepth1.ReadOnly = True` (gray background)

**Dado Support (selected - active):**
- `TxtDadoDepth1.ReadOnly = False` (white background)
- `TxtShelfBracketWidth.ReadOnly = True` (gray background)

### 5. **Event Handlers**
Added event handler for `TxtDadoDepth1.TextChanged` to trigger recalculation.

### 6. **Input Reading Logic**
Updated to check both `TxtDadoDepth1` and `TxtDadoDepth2` for dado depth value:
```vb
If TxtDadoDepth1 IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(TxtDadoDepth1.Text) Then
    dadoDepth = InputValidator.TryParseDoubleWithDefault(TxtDadoDepth1.Text, 0.375)
ElseIf TxtDadoDepth2 IsNot Nothing AndAlso Not String.IsNullOrWhiteSpace(TxtDadoDepth2.Text) Then
    dadoDepth = InputValidator.TryParseDoubleWithDefault(TxtDadoDepth2.Text, 0.375)
Else
    dadoDepth = 0.375
End If
```

---

## 📋 Complete Control List for Designer

### Required Controls (9 total):

```visualbasic
Friend WithEvents GbShelfSupportType As GroupBox
Friend WithEvents RbSupportBracket As RadioButton
Friend WithEvents RbSupportDado As RadioButton
Friend WithEvents LblShelfBracketWidth As Label
Friend WithEvents TxtShelfBracketWidth As TextBox
Friend WithEvents LblShelfBracketWidthUnit As Label
Friend WithEvents LblDadoDepth1 As Label
Friend WithEvents TxtDadoDepth1 As TextBox
Friend WithEvents LblDadoDepthUnit As Label
```

### Control Properties Summary:

| Control | Text | Checked | ReadOnly | BackColor | Default Value |
|---------|------|---------|----------|-----------|---------------|
| GbShelfSupportType | "Shelf Support Method" | - | - | - | - |
| RbSupportBracket | "Bracket/Cleat Support" | **True** | - | - | - |
| RbSupportDado | "Dado/Groove Support" | False | - | - | - |
| LblShelfBracketWidth | "  Bracket Width:" | - | - | - | - |
| TxtShelfBracketWidth | - | - | False | Window | "1.5" |
| LblShelfBracketWidthUnit | "inches total" | - | - | - | - |
| LblDadoDepth1 | "  Dado Depth:" | - | - | - | - |
| TxtDadoDepth1 | - | - | True | Control | "0.375" |
| LblDadoDepthUnit | "inches" | - | - | - | - |

---

## 🎨 Visual Layout

```
┌─ Shelf Support Method ────────────────────────────────────┐
│ ● Bracket/Cleat Support                [DEFAULT SELECTED] │
│   Bracket Width: [1.5___] inches total                    │
│   (Combined width of both supports)                        │
│                                                            │
│ ○ Dado/Groove Support                                     │
│   Dado Depth: [0.375_] inches                             │
│   (Depth of groove in side panel)                         │
└────────────────────────────────────────────────────────────┘
```

---

## 🔄 Control State Behavior

### When Bracket Selected (default):
✅ **Active (editable):**
- `TxtShelfBracketWidth` - ReadOnly = False, white background

❌ **Inactive (readonly):**
- `TxtDadoDepth1` - ReadOnly = True, gray background

### When Dado Selected:
✅ **Active (editable):**
- `TxtDadoDepth1` - ReadOnly = False, white background

❌ **Inactive (readonly):**
- `TxtShelfBracketWidth` - ReadOnly = True, gray background

### Benefits of ReadOnly Approach:
- ✅ All controls always visible - no jarring hide/show
- ✅ Users see all options and their values at once
- ✅ Clear visual indication (color) of which input is active
- ✅ Better discoverability - users know what each support type needs
- ✅ Simpler control management - no complex visibility logic

---

## 🧪 Testing Checklist

- [ ] Add all 9 controls to FrmMain.Designer.vb
- [ ] Build solution - should compile without errors
- [ ] Run application and navigate to Shelf Sag tab
- [ ] Verify bracket support is selected by default
- [ ] Verify TxtShelfBracketWidth is editable (white) and TxtDadoDepth1 is readonly (gray)
- [ ] Click dado radio button - TxtDadoDepth1 should become editable, TxtShelfBracketWidth readonly
- [ ] Click bracket radio button - TxtShelfBracketWidth editable again
- [ ] Enter values in TxtShelfBracketWidth - calculation should update
- [ ] Switch to dado and enter values in TxtDadoDepth1 - calculation should update
- [ ] Compare results: dado should show less sag than bracket for same dimensions
- [ ] Verify readonly textboxes have gray background and cannot be edited
- [ ] Verify all labels and controls remain visible at all times

---

## 📝 Key Features

1. **Smart Default**: Bracket support selected on startup (most common use case)
2. **ReadOnly State Management**: Inactive inputs shown in gray, active in white
3. **Always Visible**: All controls visible at all times - no hiding/showing
4. **Unit Labels**: Clear indication of measurement units
5. **Auto-calculation**: Updates results as soon as values change
6. **Backward Compatible**: Supports both TxtDadoDepth1 and TxtDadoDepth2
7. **Comprehensive Validation**: Checks all support-specific inputs
8. **Visual Feedback**: Color indicates which input is currently active

---

## 📖 Documentation Updated

- ✅ SHELF_SAG_SUPPORT_TYPE_CONTROLS.md - Complete implementation guide
- ✅ SHELF_SAG_QUICK_REF.md - Quick reference with control list
- ✅ ShelfSag.md - Help documentation with support type explanations

---

## 🎯 Next Steps

1. Add the 9 controls to FrmMain.Designer.vb
2. Set properties as specified above
3. Build and test
4. Verify all functionality works as expected

Once completed, the feature will be fully operational! 🚀
