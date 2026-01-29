# Wood Movement Calculator

The Wood Movement Calculator predicts how much solid wood will expand or contract with changes in relative humidity, helping you plan appropriate gaps and avoid structural problems.

## Why Wood Moves

Wood is **hygroscopic** - it absorbs and releases moisture from the air:
- High humidity → wood swells
- Low humidity → wood shrinks
- Movement is primarily **across the grain** (width changes)
- Movement along the grain is negligible (~1%)

### Real-World Impact
- Cabinet doors binding or showing gaps
- Table tops cracking when restrained
- Panel assemblies pushing joints apart
- Veneer bubbling or cracking

**Ignoring wood movement is the #1 cause of woodworking project failures!**

## How to Use

### 1. Select Wood Species
Choose from 20+ common species - each has different movement characteristics:

**High Movement (unstable):**
- Beech, Sycamore - move significantly
- Avoid for wide panels in frame & panel

**Moderate Movement:**
- Oak, Walnut, Cherry - most hardwoods
- Standard for furniture work

**Low Movement (stable):**
- Mahogany, Teak, Cedar - excellent stability
- Premium choice for wide panels

**Quarter-sawn vs Flat-sawn:**
- Grain orientation affects movement rate
- Calculator accounts for both

### 2. Enter Dimensions & Humidity

**Width** - Panel width across the grain (inches)
- Wider panels = more total movement
- Example: 12" wide table top

**Initial Humidity** - Current relative humidity (%)
- Where wood was acclimated
- Shop conditions: typically 40-50%

**Final Humidity** - Expected service humidity (%)
- Where piece will live
- Indoor heated: 25-35% (winter)
- Outdoor/humid: 60-80% (summer)

### 3. Select Grain Orientation

**Tangential (Flat-sawn)** - Growth rings roughly parallel to face
- Most common cut
- **Moves about 2x more than radial**
- Typical movement: 6-9% moisture content change

**Radial (Quarter-sawn)** - Growth rings perpendicular to face
- More stable
- About 50% less movement than tangential
- Premium appearance (ray fleck in oak)

## Understanding Results

### Movement Amount
- Shown in inches, millimeters, and fractional inches
- **Positive** = expansion (swelling)
- **Negative** = shrinkage (contraction)

### Movement Categories
- **Minimal** (<0.03") - Negligible
- **Low** (0.03"-0.06") - Minor, manageable
- **Moderate** (0.06"-0.12") - Must plan for
- **High** (>0.12") - Significant design consideration

### Panel Gap Recommendations
**Minimum Gap** - For frame & panel construction
- Allows for seasonal expansion
- Per side of panel (double for total)
- Assumes panel at equilibrium

**Maximum Gap** - Largest expected gap
- Panel fully contracted
- Visible gap in winter
- Balance function with appearance

### Wood Properties Display
- **Density** - Weight per cubic foot
- **Type** - Hardwood or Softwood
- **Movement coefficients** - Technical data

## Design Strategies

### Frame & Panel Construction
**Purpose:** Allow panels to move freely without stressing joints

**Best Practices:**
- Leave gap in groove (don't glue panel)
- Size panel for mid-season humidity
- Minimum gap = half of max movement
- Center panel in frame (equal gaps both sides)

Example: 12" oak panel, tangential
- Movement: ±0.10"
- Groove depth: 3/8"
- Panel width: 11.75" (leaves 1/8" per side)

### Breadboard Ends
**Problem:** End grain perpendicular to main panel

**Solution:**
- Elongate peg holes (allow movement)
- Only glue center 2-3"
- Proud ends acceptable (traditional)

### Table Tops
**Strategies:**
1. **Figure-8 fasteners** - Allow movement
2. **Slotted screw holes** - Screws slide
3. **Buttons in grooves** - Traditional method
4. **Avoid:** Edge-gluing to apron (will crack!)

### Avoiding Problems
- ✅ Account for **seasonal extremes** (not current conditions)
- ✅ Let wood **acclimate** to shop before working
- ✅ **Finish both sides equally** (prevents cupping)
- ✅ Use **stable species** for critical applications
- ❌ Never **constrain wide solid wood** panels
- ❌ Don't trust **theoretical calculations** over experience

## Humidity Presets

Quick selections for common scenarios:

- **Shop (Winter)** - 30-40% RH
- **Shop (Summer)** - 50-60% RH
- **Indoor Heated** - 25-35% RH
- **Indoor AC** - 45-55% RH
- **Outdoor Covered** - 60-70% RH
- **Outdoor Exposed** - 70-85% RH

## Tips & Best Practices

### Moisture Content vs Relative Humidity
- Wood reaches **equilibrium moisture content (EMC)** for given RH
- Calculator converts RH → EMC → movement
- 30% RH ≈ 6% MC
- 50% RH ≈ 9% MC
- 80% RH ≈ 16% MC

### Geographic Considerations
- **Desert climates** - Very dry winters (20-30% RH)
- **Humid climates** - High summer humidity (70%+)
- **Seasonal swing** - Plan for worst case
- **Heated vs unheated** spaces differ dramatically

### Species Selection
**Stable Species for Wide Panels:**
- Mahogany (excellent)
- Teak (excellent)
- Cedar (good)
- Walnut (good)

**Avoid for Wide Panels:**
- Beech (terrible)
- Hickory (very bad)
- Sycamore (bad)

### Construction Techniques
**Quartersawn Advantages:**
- 40-50% less movement
- More stable
- Premium lumber (more expensive)
- Ray fleck figure (especially oak)

**When Stability Matters Most:**
- Table tops >30" wide
- Frame & panel doors
- Drawer fronts
- Musical instruments
- Fine furniture

## Common Mistakes

### ❌ Mistake #1: Constraining Panels
**Problem:** Gluing panel into groove or to apron
**Result:** Panel cracks or pushes joints apart
**Fix:** Leave gap, use proper fasteners

### ❌ Mistake #2: Ignoring Seasonal Extremes
**Problem:** Building in summer, piece goes to heated home
**Result:** Winter shrinkage causes gaps/cracks
**Fix:** Plan for destination climate, not shop

### ❌ Mistake #3: Mixed Grain Orientation
**Problem:** Alternating growth rings in glue-up
**Result:** Cupping, uneven surface
**Fix:** Orient growth rings same direction

### ❌ Mistake #4: Uneven Finishing
**Problem:** Finishing only show surface
**Result:** Differential moisture exchange, cupping
**Fix:** Seal all surfaces equally

## Related Topics

- **Shelf Sag Calculator** - Structural calculations
- **Joinery Calculator** - Joint sizing for panels
- **Cut List** - Material planning

## Advanced: The Math

**Movement Formula:**
```
ΔW = W × (MC₂ - MC₁) × CMC

Where:
ΔW = change in width
W = current width
MC₁, MC₂ = moisture content at conditions 1 & 2
CMC = coefficient of movement (tangential or radial)
```

**Equilibrium Moisture Content (EMC):**
Complex function of temperature and relative humidity
- Approximation: EMC ≈ (RH / 5) - 1
- More accurate: Use published EMC tables

---
*Woodworker's Friend - Calculate with Confidence* 🪵
