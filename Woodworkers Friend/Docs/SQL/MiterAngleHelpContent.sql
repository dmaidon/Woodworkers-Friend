-- ============================================================================
-- Miter Angle Calculator Help Content
-- Created: January 31, 2026
-- Purpose: Help documentation for the Miter Angle Calculator feature
-- ============================================================================

INSERT OR REPLACE INTO HelpContent (ModuleName, Title, Content, Keywords, Category, SortOrder, Version, LastUpdated)
VALUES ('MiterAngle', 'Miter Angle Calculator', '
# Miter Angle Calculator

## Overview
The Miter Angle Calculator helps you determine the precise miter and bevel angles needed for perfect joints in polygonal projects. Whether you''re building picture frames, installing crown molding, or creating hexagonal tables, this calculator ensures accurate cuts every time.

## When to Use This Calculator
- **Picture frames** (4-sided, 45° miters)
- **Polygon furniture** (hexagon tables, octagon mirrors)
- **Crown molding** (compound angle cuts)
- **Decorative trim** (cove molding, corner trim)
- **Box lids** (any polygon shape)
- **Segmented turning** (bowls, vases with multiple sides)

## Input Fields

### Number of Sides
Enter the number of sides for your polygon (3-24 sides supported).

**Common polygons:**
- 3 sides = Triangle
- 4 sides = Square/Rectangle
- 5 sides = Pentagon
- 6 sides = Hexagon
- 8 sides = Octagon
- 12 sides = Dodecagon

### Frame Type

#### Flat Frame
Select this for standard flat projects where pieces meet at 90° to the surface:
- Picture frames
- Table tops
- Box lids
- Mirror frames

**Result:** Only miter angle is calculated (bevel = 0°)

#### Tilted Frame
Select this for projects where pieces are installed at an angle:
- Crown molding
- Cove molding
- Angled trim

**Result:** Both miter AND bevel angles are calculated (compound cut)

### Tilt Angle (Tilted Frames Only)
For tilted frames, enter the spring angle of your molding:
- **38°** - Standard crown molding (52/38 spring angle)
- **45°** - Common for 45/45 crown molding
- **Custom** - Any angle from 0° to 90°

**What is spring angle?**
The spring angle is the angle the molding makes with the wall when properly installed. It''s marked on most crown molding packaging.

### Material Thickness (Optional)
Enter the thickness of your material for reference. This doesn''t affect the angle calculations but is useful for project documentation.

## Understanding the Results

### Miter (Saw) Angle
**This is your primary cutting angle.**
- Set your miter saw to this angle
- For 4-sided frames: typically 45°
- For 6-sided frames: typically 30°
- For 8-sided frames: typically 22.5°

**Formula:** Miter Angle = (180° - Interior Angle) / 2

### Bevel Angle (Tilted Frames Only)
**This is your blade tilt angle for compound cuts.**
- Set your saw blade to tilt this many degrees
- Only applies to tilted frames (crown molding, etc.)
- For flat frames, bevel = 0° (no tilt needed)

**Formula:** Uses compound miter trigonometry based on tilt angle

### Complementary Angle
The complementary angle (90° - Miter Angle) is provided for reference and verification.

### Interior Angle
The interior angle at each vertex of the polygon. Useful for layout and design verification.

## Step-by-Step: Picture Frame

**Example: 16" x 20" rectangular picture frame**

1. Enter **4** sides (rectangle)
2. Select **Flat Frame**
3. Click Calculate

**Results:**
- Miter Angle: 45°
- Bevel Angle: 0° (flat frame)
- Set your miter saw to 45° and make straight cuts

## Step-by-Step: Crown Molding

**Example: Installing 52/38 crown molding in a rectangular room**

1. Enter **4** sides (rectangle)
2. Select **Tilted Frame**
3. Enter **38°** for tilt angle (52/38 molding)
4. Click Calculate

**Results:**
- Miter Angle: ~31.6°
- Bevel Angle: ~33.9°
- Set miter saw to 31.6° AND tilt blade to 33.9°

**Important:** For crown molding, some woodworkers prefer to lay the molding flat on the saw and use nested cuts. This calculator gives you angles for vertical cuts with the molding standing against the fence.

## Step-by-Step: Hexagon Table

**Example: 24" diameter hexagonal table top**

1. Enter **6** sides (hexagon)
2. Select **Flat Frame**
3. Click Calculate

**Results:**
- Miter Angle: 30°
- Bevel Angle: 0°
- Interior Angle: 120°
- Cut each board end at 30° for perfect hexagon

## Tips and Tricks

### Accuracy is Critical
- **±0.1°** error can create visible gaps in large projects
- **Test cuts first** - Make test pieces before cutting expensive material
- **Verify saw calibration** - Check your miter saw zero position regularly

### Flat Frame vs Tilted Frame
- **Flat Frame:** Pieces lay flat (picture frames, box lids)
- **Tilted Frame:** Pieces installed at an angle (crown molding)
- **Wrong selection = Wrong angles!**

### Multi-Sided Projects
The more sides you have:
- Smaller miter angles
- Less visible gaps from small errors
- More forgiving overall
- Example: 12-sided clock is more forgiving than 4-sided frame

### Crown Molding Tips
1. **Know your spring angle** - Check the molding package
2. **Most common:** 52/38 (38° spring angle) or 45/45 (45° spring angle)
3. **Cutting options:**
   - **Compound cut (this calculator):** Molding vertical against fence
   - **Nested cut:** Molding flat on saw bed at spring angle
4. **Test first:** Crown molding is expensive!

### Verification
- Interior Angle × Number of Sides should = (Sides - 2) × 180°
- Example: Hexagon (6 sides): 120° × 6 = 720° = (6-2) × 180° ✓

## Common Angles Quick Reference

| Sides | Shape | Miter Angle (Flat) | Each Interior Angle |
|-------|-------|-------------------|---------------------|
| 3 | Triangle | 60° | 60° |
| 4 | Square | 45° | 90° |
| 5 | Pentagon | 36° | 108° |
| 6 | Hexagon | 30° | 120° |
| 8 | Octagon | 22.5° | 135° |
| 10 | Decagon | 18° | 144° |
| 12 | Dodecagon | 15° | 150° |

## Troubleshooting

### Gaps at Joints
- Check miter saw calibration
- Verify you entered correct number of sides
- Ensure blade is sharp and not deflecting
- Material may not be perfectly straight

### Crown Molding Doesn''t Fit
- Verify spring angle is correct (check package)
- Confirm frame type is set to "Tilted"
- Room corners may not be perfectly 90°
- Consider scribing technique for out-of-square corners

### Compound Cuts Not Matching
- Both miter AND bevel must be set correctly
- Some saws require complementary angles (90° - calculated angle)
- Consult your saw manual for angle direction

## Formula Reference

### Flat Frame (Simple Miter)
```
Interior Angle = (n - 2) × 180° / n
Miter Angle = (180° - Interior Angle) / 2
```

### Tilted Frame (Compound Miter)
```
Compound Miter = arctan(cos(tilt) × tan(simple_miter))
Bevel Angle = arcsin(sin(tilt) × sin(simple_miter))
```

Where:
- n = number of sides
- tilt = spring angle in radians
- simple_miter = flat frame miter angle in radians

## Safety Notes
⚠️ **Always wear safety glasses when using power tools**
⚠️ **Test cuts on scrap material first**
⚠️ **Keep hands clear of blade path**
⚠️ **Use proper push sticks for small pieces**
⚠️ **Ensure workpiece is firmly secured**

## Related Calculators
- **Polygon Calculator** - Calculate polygon dimensions
- **Dado Stack Calculator** - For groove cuts
- **Table Tipping Force** - Furniture safety calculations

## Additional Resources
- Crown molding installation guides
- Compound miter cut tables
- Picture frame building tutorials

---

**Need more help?** Contact support or visit the documentation wiki.
', 'miter,angle,bevel,compound,crown,molding,frame,picture,polygon,hexagon,octagon,cut,saw', 'Calculators', 140, '1.0', CURRENT_TIMESTAMP);
