# Input Validation

## Overview
Input validation ensures your calculations are accurate by checking that all values are within acceptable ranges and properly formatted.

## How Validation Works

### Automatic Checking
- Validation occurs as you type
- Immediate visual feedback
- Error messages displayed
- Calculate button disabled until valid

### Visual Indicators

**Valid Input:**
- Normal text field appearance
- No highlighting
- Calculate button enabled

**Invalid Input:**
- Red highlight on field
- Error message in tooltip
- Status bar shows error count
- Calculate button disabled

## Common Validation Rules

### Numeric Values
- Must be numbers (not text)
- No letters or special characters
- Decimal point allowed (use period, not comma)
- Negative numbers only where applicable

### Range Checks
- Values must be within realistic limits
- Minimum values enforced (often > 0)
- Maximum values prevent unrealistic inputs
- Context-specific ranges per calculator

### Required Fields
- All marked fields must have values
- Empty fields highlighted
- Cannot calculate with missing data

## Validation by Calculator

### Drawer Calculator
**Width:** 1-100 inches
**Height:** 1-50 inches
**Depth:** 1-50 inches
**Thickness:** 0.25-2 inches

**Why:** Typical cabinet dimensions

### Door Calculator
**Width:** 1-100 inches
**Height:** 1-100 inches
**Reveal:** 0-0.5 inches
**Overlay:** 0-2 inches

**Why:** Standard door proportions

### Board Feet
**Thickness:** 0.25-12 inches
**Width:** 1-100 inches
**Length:** 1-240 inches
**Quantity:** 1-999

**Why:** Common lumber dimensions

### Wood Movement
**Width:** 1-100 inches
**Initial MC:** 0-30%
**Final MC:** 0-30%

**Why:** Typical moisture content ranges

## Error Messages

### Format Errors
**"Must be a valid number"**
- Contains letters or symbols
- Fix: Enter numbers only

**"Use decimal point, not comma"**
- Used comma for decimal
- Fix: Change 12,5 to 12.5

### Range Errors
**"Value must be between X and Y"**
- Outside acceptable range
- Fix: Enter value within limits

**"Value must be positive"**
- Negative where not allowed
- Fix: Enter positive number

**"Value must be greater than zero"**
- Zero entered where not valid
- Fix: Enter positive, non-zero value

### Required Field Errors
**"This field is required"**
- Left empty
- Fix: Enter a value

## Best Practices

### Data Entry
1. Use Tab to move between fields
2. Enter values carefully
3. Watch for red highlights
4. Fix errors before calculating

### Checking Your Work
- Review all values before calculating
- Hover over highlighted fields
- Read error messages carefully
- Double-check units

### Common Mistakes
- Mixing units (inches and feet)
- Decimal comma instead of period
- Leaving required fields empty
- Entering unrealistic values

## Validation Feedback

### Status Bar
Shows overall validation state:
- "Ready to calculate" - All valid
- "2 errors found" - Fix before calculating
- Specific error summary

### Tooltips
Hover over invalid field for:
- Specific error message
- Acceptable range
- Example valid value
- Why it's invalid

### Field Highlighting
- **No highlight**: Valid
- **Red highlight**: Invalid
- **Yellow**: Warning (valid but unusual)

## Override Options

### Warnings vs. Errors
**Warnings** (Yellow):
- Unusual but valid values
- Can calculate anyway
- Consider if intentional
- Example: Very thick material

**Errors** (Red):
- Invalid or out of range
- Must fix before calculating
- Prevents incorrect results
- Example: Negative dimension

### No Override for Errors
- Errors must be fixed
- Cannot force calculation
- Prevents bad results
- Protects data integrity

## Troubleshooting Validation

### "Value seems valid but rejected"
- Check decimal point (not comma)
- Verify within valid range
- Ensure no extra spaces
- Try clearing and re-entering

### "Can't find the error"
- Check status bar for count
- Hover over each field
- Look for red highlights
- Scroll if fields below fold

### "Error persists after fixing"
- Tab to next field
- Click elsewhere
- Press Enter
- Restart if necessary

## Validation and Safety

### Why Validation Matters
- Prevents calculation errors
- Catches data entry mistakes
- Ensures realistic results
- Protects against invalid dimensions

### Safety Considerations
- Validation is first line of defense
- Still verify results make sense
- Consider real-world tolerances
- Add safety factors to calculations

## Performance Impact

### Validation Speed
- Instant feedback
- No delay in typing
- Efficient checking
- Minimal CPU use

### When It Happens
- As you type (real-time)
- When leaving field (on blur)
- Before calculation
- On form submission

## Related Topics
- **Interface**: Understanding visual feedback
- **Best Practices**: Data entry tips
- **Troubleshooting**: Fixing common problems
- **Calculators**: Specific validation rules

Validation helps ensure accurate calculations. Pay attention to feedback and fix errors before calculating for best results!
