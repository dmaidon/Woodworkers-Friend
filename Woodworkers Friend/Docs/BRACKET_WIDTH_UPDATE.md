# Bracket Width Default Value Update - CORRECTED

## Change Made: January 29, 2026

### Issue
Initially set default to 0.75" thinking that was the combined width, but brackets are typically 0.75" **per side**.

### Solution
Updated default to **1.5"** which represents:
- **0.75" per bracket × 2 brackets = 1.5" total combined width**
- This is the TOTAL width that gets subtracted from span
- Typical bracket size for standard shelf hardware

### Clarification: What is "Bracket Width"?

The `BracketWidth` property represents the **TOTAL COMBINED WIDTH** of both support brackets:
- If each bracket is 0.75" wide → Total = 1.5"
- If each bracket is 0.5" wide → Total = 1.0"
- If each bracket is 1.0" wide → Total = 2.0"

**Calculation:**
```
Effective Span = Total Shelf Length - Total Bracket Width
Example: 36" shelf - 1.5" brackets = 34.5" effective span
```

### Files Updated

1. **FrmMain.ShelfSag.vb** - Changed initialization default from "3.0" to "0.75"
2. **FrmMain.ShelfSag.vb** - Changed fallback default in calculation from 3.0 to 0.75
3. **SHELF_SAG_DESIGNER_STEPS.md** - Updated Designer property and visual examples
4. **SHELF_SAG_UPDATES.md** - Updated control properties table and visual layout
5. **SHELF_SAG_QUICK_REF.md** - Updated default property value
6. **SHELF_SAG_SUPPORT_TYPE_CONTROLS.md** - Updated TextBox default and example calculation
7. **SHELF_SAG_READONLY_UX.md** - Updated all visual examples and property defaults
8. **ShelfSag.md** - Updated typical bracket widths and example calculations

### Realistic Bracket Widths (TOTAL for both sides)

| Support Type | Per Bracket | Total Combined | Use Case |
|--------------|-------------|----------------|----------|
| Shelf pins | 0.25"-0.5" | 0.5"-1.0" | Adjustable shelving |
| Small brackets | 0.5"-0.75" | 1.0"-1.5" | Light duty (DEFAULT) |
| Medium brackets | 0.75"-1.0" | 1.5"-2.0" | Standard use |
| Large brackets | 1.0"-1.5" | 2.0"-3.0" | Heavy duty |

### Updated Example Calculations

**36" span, 3/4" plywood, 100 lbs:**
- **Default:** Bracket (1.5" total = 0.75" each) = 34.5" effective span ✓ Realistic

**Key Point:** 
- User inputs: **TOTAL combined width** (what gets subtracted from span)
- Most common bracket: **0.75" per side = 1.5" total**

✅ **All code and documentation updated to 1.5" default**
