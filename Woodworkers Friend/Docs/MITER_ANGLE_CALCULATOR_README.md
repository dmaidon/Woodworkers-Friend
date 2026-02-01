# Miter Angle Calculator

## Quick Start

The Miter Angle Calculator helps woodworkers determine precise miter and bevel angles for various frame types.

## Supported Projects

### 1️⃣ Flat Frames (Picture Frames)
Simple frames with flat mitered corners.
- **Input:** Corner angle (typically 90°)
- **Output:** Miter angle, no bevel
- **Example:** 90° corner → 45° miter

### 2️⃣ Polygonal Projects (Multi-Sided Frames)
Hexagonal planters, octagonal mirrors, etc.
- **Input:** Number of sides (3-25)
- **Output:** Miter angle for each piece
- **Example:** Hexagon (6 sides) → 30° miter

### 3️⃣ Tilted Frames (Shadow Boxes)
Frames with slanted sides requiring compound miters.
- **Input:** Corner angle + Tilt angle
- **Output:** Miter angle + Bevel angle
- **Example:** 90° corner + 15° tilt → 44.5° miter + 10.8° bevel

### 4️⃣ Crown Molding
Crown molding installation with spring angles.
- **Input:** Corner angle + Spring angle + Inside/Outside
- **Output:** Miter angle + Bevel angle
- **Example:** 90° corner + 45° spring → 35.3° miter + 30.0° bevel

## Features

✅ **4 Frame Types** - Flat, Polygonal, Tilted, Crown Molding  
✅ **Preset Values** - Common corner and spring angles  
✅ **Saw Validation** - Checks if your saw can make the cuts  
✅ **Cutting Instructions** - Step-by-step guidance  
✅ **Auto-Calculate** - Updates as you type  
✅ **Save Projects** - Store your calculations  

## Formulas

### Flat Frames
```
Miter = Corner Angle ÷ 2
```

### Polygonal Frames
```
Interior Angle = (Sides - 2) × 180 ÷ Sides
Miter = 90 - (Interior Angle ÷ 2)
```

### Compound Miters (Tilted & Crown)
```
tan(Miter) = cos(Angle) × tan(Corner ÷ 2)
sin(Bevel) = sin(Angle) × sin(Corner ÷ 2)
```

## Tips

### Common Projects

**Square Picture Frame**
- Corner: 90°
- Miter: 45°
- Bevel: 0°

**Hexagonal Planter**
- Sides: 6
- Miter: 30°
- Interior: 120°

**Crown Molding (Standard)**
- Corner: 90°
- Spring: 45°
- Inside corner: Miter 35.3°, Bevel 30.0°

**Shadow Box (Shallow)**
- Corner: 90°
- Tilt: 10°
- Miter: 44.7°, Bevel 7.2°

### Saw Limitations

Most miter saws can cut:
- **Miter:** 0° to 50°
- **Bevel:** 0° to 45°

The calculator will warn you if your project exceeds these limits.

### Cutting Instructions

The calculator provides step-by-step cutting instructions:

1. Set miter saw to calculated horizontal angle
2. Set bevel to calculated vertical angle (if needed)
3. Make test cuts on scrap wood
4. Verify fit before cutting final pieces

## Presets

### Corner Angles
- 90° (Square corner) - Most common
- 120° (Hexagon corner)
- 135° (Octagon corner)
- 108° (Pentagon corner)

### Spring Angles (Crown Molding)
- 38° (Common crown molding)
- 45° (Standard crown molding)
- 52° (Steep crown molding)

## Examples

### Example 1: Picture Frame
**Project:** 16" × 20" picture frame  
**Input:** 90° corner (square)  
**Output:** 45° miter, 0° bevel  
**Instruction:** Simple miter cut at 45°

### Example 2: Hexagonal Mirror
**Project:** Hexagonal bathroom mirror  
**Input:** 6 sides  
**Output:** 30° miter, 120° interior angle  
**Instruction:** Cut each piece at 30° miter

### Example 3: Shadow Box
**Project:** Display case with slanted front  
**Input:** 90° corner, 20° tilt  
**Output:** 43.9° miter, 14.3° bevel  
**Instruction:** Compound miter cut

### Example 4: Crown Molding
**Project:** Living room crown molding  
**Input:** 90° corner, 45° spring, inside  
**Output:** 35.3° miter, 30.0° bevel  
**Instruction:** Compound miter for inside corner

## Accuracy

All calculations use industry-standard formulas:
- Flat frames: Simple geometry
- Polygonal: Regular polygon formulas
- Compound miters: Trigonometric formulas from compound miter tables

Always make test cuts on scrap wood before cutting final pieces.

## Documentation

- **Implementation Guide:** `Docs/MITER_ANGLE_IMPLEMENTATION_GUIDE.md`
- **UI Specifications:** `Docs/MITER_ANGLE_UI_CONTROLS.md`
- **Summary:** `IMPLEMENTATION_SUMMARY.md`

## Version

**v1.0** - February 1, 2026

---

*Part of Woodworker's Friend - Professional Woodworking Calculator Suite*
