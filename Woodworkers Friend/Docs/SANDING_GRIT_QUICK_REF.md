# Sanding Grit Progression Calculator - Quick Reference

## Location
**Tab:** References → Sanding

## Purpose
Calculate optimal sanding grit sequence for achieving smooth woodworking finishes.

## Controls Summary

### Input Controls (Panel1 - Left)
```
GbxSandingGrit (GroupBox - Silver background)
├─ LblOptimalGritSequence (Description label - Dock Top)
├─ LblSgpWoodType + CmbWoodType
├─ LblSgpStartingGrit + CmbStartGrit
├─ LblSgpFinalGrit + CmbFinalGrit
├─ LblSgpProgressType
├─ PnlSgpRadioButtons
│  ├─ RbSkipGrit (Skip Grits - Fast)
│  └─ RbSequential (Sequential - Thorough)
└─ BtnCalculateGrit (MistyRose button)
```

### Output Controls (Panel2 - Right)
```
├─ LblGritResult (Bold Green label - progression sequence)
└─ LblGritNotes (WhiteSmoke background - detailed notes)
```

## Input Options

### Wood Type
- **Softwood (Pine, Fir, Cedar):** More susceptible to scratches, requires careful progression
- **Hardwood (Oak, Maple, Walnut):** More forgiving, can skip grits if needed

### Starting Grits
- **40:** Extra coarse - Heavy stock removal
- **60:** Coarse - Remove mill marks
- **80:** Medium-coarse - Common starting point
- **100-120:** Medium - General smoothing

### Final Grits
- **150:** Fine - Basic finish prep
- **180:** Fine - Standard for stain/paint
- **220:** Very fine - Common for clear finish
- **320-600:** Extra fine - Between coats, high-gloss

### Progression Types
- **Skip-Grit (Fast):** Skips every other grit; faster but may leave scratches
- **Sequential (Thorough):** Every grit in sequence; best finish quality

## Calculation Logic

### Standard Grit Sequence
```
40 → 60 → 80 → 100 → 120 → 150 → 180 → 220 → 320 → 400 → 600
```

### Sequential Method
Includes all grits between start and finish:
- Example: 80 → 100 → 120 → 150 → 180 → 220

### Skip-Grit Method
Skips every other grit (keeps first and last):
- Example: 80 → 120 → 180 → 220

## Results Display

### Progression Line
```
Progression: 80 → 120 → 180 → 220
```

### Detailed Notes Include:
1. **Method Information**
   - Method type (Skip-Grit or Sequential)
   - Wood type
   - Total steps
   - Time estimate (3-5 min per grit)

2. **Method-Specific Tips**
   - Skip-Grit: Speed vs. quality tradeoffs
   - Sequential: Quality recommendations

3. **Wood-Specific Recommendations**
   - Softwood: Don't skip grits, use light pressure
   - Hardwood: Can skip if needed, higher grit = better clarity

4. **Grit Descriptions**
   - Purpose and use for each grit in sequence

5. **Safety Reminders**
   - Dust mask/respirator required
   - Dust collection importance
   - Surface preparation tips

## Grit Reference Guide

### Coarse (40-60)
- **Purpose:** Heavy stock removal, shaping
- **Use:** Remove mill marks, flatten rough surfaces
- **Note:** Creates deep scratches that must be removed

### Medium (80-120)
- **Purpose:** General smoothing, preparation
- **Use:** Standard starting point for most projects
- **Note:** Most common range for initial sanding

### Fine (150-220)
- **Purpose:** Final preparation before finishing
- **Use:** Pre-stain, pre-paint, or pre-clear coat
- **Note:** 220 is typical final grit for clear finish

### Extra Fine (320-600)
- **Purpose:** Between finish coats, high-gloss prep
- **Use:** Smooth finish coats, wet sanding
- **Note:** Not typically used on bare wood

## Best Practices

### General Tips
1. ✓ Always sand with the grain direction
2. ✓ Clean surface between grits (vacuum + tack cloth)
3. ✓ Check for cross-grain scratches
4. ✓ Test on scrap wood first
5. ✓ Use proper dust collection

### Softwood Specific
- DON'T skip grits (shows scratches easily)
- DO use light pressure (avoid dishing)
- DO watch for raised grain after first grit
- DO consider wet-sanding between coats

### Hardwood Specific
- CAN skip grits if needed
- DO use higher final grit for better clarity
- DO check for mill marks before starting
- DO use card scraper for difficult grain

## Common Sequences

### Standard Clear Finish
```
80 → 100 → 120 → 150 → 180 → 220
Time: 18-30 minutes
```

### Quick Painted Finish
```
80 → 120 → 180
Time: 9-15 minutes
```

### Premium Clear Finish
```
80 → 100 → 120 → 150 → 180 → 220 → 320
Time: 21-35 minutes
```

### Restoration Work
```
40 → 60 → 80 → 100 → 120 → 150 → 180 → 220
Time: 24-40 minutes
```

## Implementation Details

### File Location
```
Woodworkers Friend\Partials\FrmMain.Sanding.vb
```

### Key Methods
- `InitializeSandingGritCalculator()` - Initialize controls and defaults
- `BtnCalculateGrit_Click()` - Main calculation handler
- `CalculateGritProgression()` - Determine grit sequence
- `DisplayGritResults()` - Format and display results
- `GetGritDescription()` - Get description for each grit

### Error Handling
- Input validation for all fields
- Grit range validation (start < finish)
- Comprehensive error logging via ErrorHandler

## Safety Features

### Validation
- All inputs must be selected before calculating
- Starting grit must be coarser than final grit
- Clear error messages for invalid inputs

### Warnings
- Dust mask/respirator reminder
- Dust collection importance
- Surface preparation tips
- Cross-grain scratch warnings

## Future Enhancements (Optional)

### Potential Features
- [ ] Auto-calculate on selection change
- [ ] Save/load favorite sequences
- [ ] Custom grit sequences
- [ ] Time tracking per grit
- [ ] Sandpaper cost estimator
- [ ] Project history
- [ ] Export recommendations to PDF

## Testing Checklist

- [ ] All dropdowns populate correctly
- [ ] Default values set properly
- [ ] Calculate button works
- [ ] Results display correctly
- [ ] Sequential method works
- [ ] Skip-grit method works
- [ ] Input validation works
- [ ] Error messages display
- [ ] Softwood recommendations show
- [ ] Hardwood recommendations show
- [ ] All grit descriptions display
- [ ] Safety warnings display

---

**Version:** 1.0  
**Last Updated:** January 2026  
**Status:** ✅ Complete and functional
