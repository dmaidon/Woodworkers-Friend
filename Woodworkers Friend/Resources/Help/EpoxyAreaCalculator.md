# Epoxy Area Calculator

## Purpose
Quickly calculate the total surface area for epoxy projects involving multiple surfaces or zones. Automatically sums areas and integrates with both Epoxy Pour and Stone Coat Top Coat calculators.

## What It Does

The Area Calculator is a **shared workspace table** that helps you:
- Calculate area for rectangular surfaces
- Track multiple zones in a single project
- Separate base pour quantities from top coat quantities
- Get automatic totals across all entries

## How It Works

### Grid-Based Entry
The calculator uses a simple spreadsheet-style grid where you can:
1. Add multiple surfaces/zones
2. Enter length and width for each
3. Select calculation type (Pour, TopCoat, or Both)
4. See area auto-calculate for each row
5. View total area at bottom

### Calculation Types

The three radio buttons control what the area is used for:

#### Pour (Base Epoxy)
- Calculates area for **base flood coat** or **river table pour**
- Total feeds into **Epoxy Pour Calculator** (left panel)
- Used when you need volume of epoxy resin

#### TopCoat (Stone Coat)
- Calculates area for **Stone Coat top coat** surface coverage
- Total feeds into **Stone Coat Calculator** (right panel)
- Used when applying protective finish coat

#### Both
- Calculates **combined area** for projects needing both layers
- Shows total for complete project planning
- Useful for budgeting and material ordering

## Using the Calculator

### Adding Surfaces

**Method 1: Direct Grid Entry**
1. Click in any empty row
2. Enter surface name/description (optional)
3. Enter **Length** in inches
4. Enter **Width** in inches
5. Area automatically calculates

**Method 2: Named Zones**
For complex projects, label each zone:
- "Kitchen Counter - Main"
- "Kitchen Counter - Island"  
- "Bar Top - Front Section"
- "Bar Top - Side Return"

### Selecting Calculation Type

**Use the radio buttons** to control what totals:

- Select **Pour** when calculating:
  - River table epoxy volume
  - Flood coat base layer
  - Deep pour quantities

- Select **TopCoat** when calculating:
  - Stone Coat top coat coverage
  - Protective finish layer
  - Final sealing coat

- Select **Both** when planning:
  - Complete project material needs
  - Budget estimation
  - Material ordering

### Reading the Total

The **Total Area** displays:
- Automatically sums all grid rows
- Updates in real-time as you type
- Shows in **square feet**
- Feeds selected calculator(s)

## Integration with Calculators

### With Epoxy Pour Calculator
When **Pour** or **Both** is selected:
1. Total area flows to Epoxy Pour Calculator
2. Enter depth in the Pour panel
3. Volume auto-calculates based on area × depth
4. Results show ounces, gallons, liters needed

### With Stone Coat Calculator  
When **TopCoat** or **Both** is selected:
1. Total area flows to Stone Coat panel
2. Calculator applies coverage rate (0.7 oz/sq ft)
3. Displays Part A, Part B, Water amounts
4. Shows both Matte and Gloss mixing instructions

## Practical Examples

### Example 1: Simple Kitchen Counter

**Single Counter - Pour Only**
| Description | Length | Width | Area |
|-------------|--------|-------|------|
| Main Counter | 72" | 25.5" | 12.75 sq ft |

- Select: **Pour**
- Enter depth (e.g., 0.125" for flood coat)
- Epoxy Pour Calculator shows: ~0.09 gallons needed

### Example 2: Kitchen with Island - TopCoat Only

**Multiple Surfaces - TopCoat**
| Description | Length | Width | Area |
|-------------|--------|-------|------|
| Main Counter | 96" | 26" | 17.3 sq ft |
| Island Top | 48" | 36" | 12 sq ft |
| **TOTAL** | | | **29.3 sq ft** |

- Select: **TopCoat**
- Stone Coat Calculator shows: ~20 oz Part A, 10 oz Part B, 5 oz Water (Matte)

### Example 3: Complete Bar Top - Both Layers

**Bar with Base Pour + TopCoat**
| Description | Length | Width | Area |
|-------------|--------|-------|------|
| Bar Front | 120" | 24" | 20 sq ft |
| Bar Back Return | 84" | 18" | 10.5 sq ft |
| **TOTAL** | | | **30.5 sq ft** |

- Select: **Both**
- **Epoxy Pour** (at 1" depth): ~1.1 gallons base resin
- **Stone Coat TopCoat**: ~21 oz Part A, 11 oz Part B, 5.5 oz Water

## Tips & Best Practices

### Accurate Measurements
- **Measure installed dimensions** - not cabinet opening
- **Account for overhangs** - add extra for waterfall edges
- **Irregular shapes**: Break into rectangular sections
- **Curved edges**: Use rectangle that encompasses curve, add 5% waste

### Grid Organization
- **Label clearly** - helps when reviewing later
- **Group by area type** - all counters together, all islands together
- **Update as project changes** - recalculates instantly
- **Save screenshots** - for material ordering reference

### Material Planning
- **Run calculator for each layer** separately first
- **Then run Both** for total project cost
- **Add 10-15%** for safety margin
- **Round up** to next kit size when ordering

### Waste Factors by Complexity
- **Simple rectangle**: 10% waste sufficient
- **L-shaped counter**: 15% waste  
- **Complex multi-section**: 20% waste
- **First Stone Coat project**: 20% waste minimum

## Common Project Scenarios

### Scenario 1: Two-Layer Counter System
1. Enter all surface dimensions in grid
2. Select **Pour** → Calculate base resin volume (with depth)
3. Select **TopCoat** → Calculate Stone Coat amounts
4. Select **Both** → Get total material cost estimate

### Scenario 2: Top Coat Only (Pre-existing Surface)
1. Enter surface areas
2. Select **TopCoat** only
3. Get Stone Coat mixing instructions
4. No need to use Pour calculator

### Scenario 3: River Table (Pour Only)
1. Enter table top dimensions
2. Select **Pour**
3. Enter pour depth in Epoxy Pour Calculator
4. Skip TopCoat if using different sealer

## Troubleshooting

### Total Seems Wrong
- **Check units**: All inputs should be in **inches**
- **Verify grid entries**: Scroll to ensure no duplicate rows
- **Clear old data**: Delete unused rows
- **Recalculate**: Click in/out of cells to refresh

### Calculator Not Updating
- **Radio button selection**: Ensure correct type selected (Pour/TopCoat/Both)
- **Click Apply**: Some fields may need manual update trigger
- **Refresh grid**: Click in calculation cell to force update

### Integration Not Working
- Ensure radio button selected BEFORE entering area
- Try toggling radio buttons to refresh connection
- Manual entry works in both calculators if auto-feed fails

## Related Features

### Epoxy Pour Calculator
Left panel calculates:
- Volume for deep pours
- River table resin quantities
- Flood coat amounts

### Stone Coat Top Coat
Right panel calculates:
- Part A + Part B + Water amounts
- Matte vs. Gloss formulations
- Coverage for finish coats

### Cost Management
Configure material pricing:
- **About Tab** → **Manage Costs**
- Set epoxy brands and prices
- Set Stone Coat kit prices
- Get automatic cost estimates

## Formula Reference

**Area Calculation**:
```
Area (sq ft) = (Length in inches × Width in inches) ÷ 144
```

**Grid Total**:
```
Total Area = Sum of all row areas (filtered by radio button selection)
```

**Pour Volume from Area**:
```
Volume (gallons) = Area (sq ft) × Depth (in) ÷ 231
```

**TopCoat Amount from Area**:
```
Matte:
- Part A (oz) = Area (sq ft) × 0.7 × 0.571
- Part B (oz) = Area (sq ft) × 0.7 × 0.286
- Water (oz)  = Area (sq ft) × 0.7 × 0.143

Gloss:
- Part A (oz) = Area (sq ft) × 0.7 × 0.595
- Part B (oz) = Area (sq ft) × 0.7 × 0.298
- Water (oz)  = Area (sq ft) × 0.7 × 0.107
```

## Best Practices Summary

✅ **DO:**
- Measure all surfaces carefully
- Use grid to organize complex projects  
- Select appropriate calculation type
- Add 10-20% waste factor
- Clear grid between different projects

❌ **DON'T:**
- Mix length/width units (always use inches)
- Forget to select calculation type radio button
- Leave old project data in grid
- Estimate area for irregular shapes (break into rectangles)
- Rely on area calculator alone (verify with tape measure)

---

**Workflow Integration**: 
Area Calculator → Epoxy Pour Calculator (for volume) 
Area Calculator → Stone Coat Calculator (for top coat mixing)

---

*Last Updated: January 2025*  
*Version: 1.0*
