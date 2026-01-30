# Shelf Sag Support Type - Quick Reference

## Controls to Add to FrmMain.Designer.vb

### Backing Field Declarations (Add at end of Designer file):
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

### Default Property Values:
| Control | Property | Value |
|---------|----------|-------|
| GbShelfSupportType | Text | "Shelf Support Method" |
| RbSupportBracket | Text | "Bracket/Cleat Support" |
| RbSupportBracket | Checked | **True** (default) |
| RbSupportDado | Text | "Dado/Groove Support" |
| RbSupportDado | Checked | False |
| LblShelfBracketWidth | Text | "  Bracket Width:" |
| LblShelfBracketWidth | Visible | True |
| TxtShelfBracketWidth | Text | "1.5" |
| TxtShelfBracketWidth | Visible | True |
| LblShelfBracketWidthUnit | Text | "inches total" |
| LblShelfBracketWidthUnit | Visible | True |
| LblDadoDepth1 | Text | "  Dado Depth:" |
| LblDadoDepth1 | Visible | False |
| TxtDadoDepth1 | Text | "0.375" |
| TxtDadoDepth1 | Visible | False |
| LblDadoDepthUnit | Text | "inches" |
| LblDadoDepthUnit | Visible | False |

## What Each Control Does

| Control | Purpose |
|---------|---------|
| **GbShelfSupportType** | Groups support type options |
| **RbSupportBracket** | Select bracket/cleat support (reduces effective span) - **DEFAULT** |
| **RbSupportDado** | Select dado/groove support (adds partial fixity) |
| **LblShelfBracketWidth** | Label for bracket width input |
| **TxtShelfBracketWidth** | Total combined width of both brackets (inches) |
| **LblShelfBracketWidthUnit** | Unit label ("inches total") |
| **LblDadoDepth1** | Label for dado depth input |
| **TxtDadoDepth1** | Depth of dado groove (inches) |
| **LblDadoDepthUnit** | Unit label ("inches") |

## Calculation Logic

### Bracket Support:
```
Effective Span = Total Shelf Length - Bracket Width
Deflection = Standard beam formula using effective span
```

### Dado Support:
```
Effective Span = Full shelf length
Fixity Factor = f(dado depth / shelf thickness)
Deflection = Blended between simple and fixed beam formulas
```

### Fixity Factors:
- Dado depth 25% of thickness → 10% fixity
- Dado depth 37.5% of thickness → 25% fixity  
- Dado depth 50%+ of thickness → 40% fixity

## Files Modified

1. ✅ `Modules\ShelfSag\ShelfSagModels.vb` - Added enum and properties
2. ✅ `Modules\ShelfSag\ShelfSagCalculator.vb` - Added calculation logic
3. ✅ `Partials\FrmMain.ShelfSag.vb` - Added UI logic and event handlers
4. ✅ `Resources\Help\ShelfSag.md` - Added documentation
5. ⏳ `FrmMain.Designer.vb` - **NEEDS CONTROLS ADDED**

## After Controls Added - Testing

1. Build solution
2. Run application
3. Navigate to Shelf Sag tab
4. Test bracket support with 36" span, 3" bracket width
5. Test dado support with same span, 0.375" dado
6. Verify dado shows less sag than bracket
7. Toggle between support types - visibility should update
8. Verify validation errors for invalid inputs
