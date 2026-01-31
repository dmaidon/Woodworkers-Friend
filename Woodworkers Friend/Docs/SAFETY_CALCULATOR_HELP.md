# Safety Calculators - User Guide

## Overview

The **Safety** tab provides four essential calculators designed to help woodworkers work more safely by providing science-based recommendations for tool speeds, blade heights, and dust collection requirements.

**⚠️ IMPORTANT SAFETY DISCLAIMER:**
These calculators provide guidance based on industry standards and best practices. Always:
- Follow manufacturer specifications for your specific tools
- Wear appropriate personal protective equipment (PPE)
- Read and understand your tool's manual
- Never bypass or remove safety devices unless absolutely necessary
- Use common sense and prioritize safety over speed

---

## 1. Router Bit Speed Calculator

### Purpose
Calculates the maximum safe RPM (Revolutions Per Minute) for router bits based on diameter and desired surface speed.

### Why It Matters
- **Large diameter bits at high RPM can explode** causing catastrophic injury
- Surface speed (rim speed) is what matters for safe operation, not just RPM
- Industry standard safe range: 9,000-12,000 feet per minute (ft/min)
- Exceeding safe speeds can cause bit failure, excessive heat, and poor cuts

### How to Use

1. **Enter Router Bit Diameter (inches)**
   - Measure the cutting diameter of your bit
   - Example: A 1" straight bit, 2.5" panel raiser

2. **Enter Desired Surface Speed (ft/min)**
   - Default: 9,000 ft/min (conservative, safe for most bits)
   - Range: 8,000-12,000 ft/min for wood
   - Higher speeds for small bits, lower for large bits

3. **Click Calculate** (or values update automatically as you type)

### Understanding Results

**Maximum Safe RPM:** The calculated speed your router should run at
- Green = Safe range
- Orange = Caution range (22,000-24,000 RPM)
- Red = Danger range (>24,000 RPM) - **DO NOT USE**

**Rim Speed:** The actual speed at the bit's cutting edge in MPH

**Warnings:**
- ⚠️ Very low speed warnings (< 8,000 RPM)
- ⚠️ High speed cautions (> 22,000 RPM)
- ⚠️ DANGER alerts (> 24,000 RPM)
- ⚠️ Large bit warnings (> 3.5" diameter)

### Formula
```
RPM = (Surface Speed × 12) / (π × Diameter)

Where:
- Surface Speed is in feet per minute
- Diameter is in inches
- 12 converts feet to inches
- π (pi) = 3.14159...
```

### Safety Tips
- **Large bits (> 2") require lower RPMs** - Often 10,000-16,000 RPM max
- **Small bits (< 1/2") can run faster** - Up to 20,000-24,000 RPM
- **Panel raising bits** - Keep under 18,000 RPM for bits larger than 2.5"
- **Variable speed routers** - Essential for using large diameter bits safely
- **Test cuts** - Always start with a test cut on scrap wood
- **Bit condition** - Inspect bits for cracks or damage before use

---

## 2. Blade Height Recommendations

### Purpose
Provides science-based recommendations for table saw blade height for different cutting operations.

### Why It Matters
- **Blade height affects safety** - Too high increases kickback risk and blade exposure
- **Blade height affects cut quality** - Too low can burn wood, too high causes tearout
- **Operation-specific heights** - Ripping, crosscutting, and dados require different heights
- **Tooth engagement** - Proper height ensures clean, efficient cutting

### How to Use

1. **Enter Material Thickness (inches)**
   - Measure the actual thickness of your stock
   - Example: 0.75" for standard ¾" lumber, 1.5" for 2x4, 0.125" for ⅛" hardboard

2. **Select Operation Type**
   - **Ripping** - Cutting with the grain (lengthwise)
   - **Crosscutting** - Cutting across the grain (widthwise)
   - **Dado/Groove** - Making grooves or housings for shelves
   - **Thin Stock (< ¼")** - Special handling for very thin material

3. **Click Calculate** (or values update automatically as you change inputs)

### Understanding Results

**Recommended Height:** The blade height in inches and millimeters

**Operation-Specific Notes:**

- **Ripping (1/8" to 1/4" above material)**
  - Minimizes kickback risk
  - Reduces blade exposure
  - Adequate tooth engagement
  - Standard for most ripping operations

- **Crosscutting (Full tooth ≈1/4" above material)**
  - Cleaner cut on crossgrain
  - Reduces tearout on exit side
  - Better tooth engagement
  - Use with miter gauge or crosscut sled

- **Dado/Groove (Just breaking through +1/32")**
  - Precise depth control
  - Test cut on scrap first
  - Multiple passes for deep cuts
  - Prevents cutting too deep

- **Thin Stock (1/16" above material)**
  - Use zero-clearance insert
  - Featherboards recommended
  - Push stick mandatory
  - Extra caution required

### Safety Warnings

**Thin Stock Warnings (< 1/4"):**
- Use push sticks and push blocks
- Zero-clearance insert required
- Featherboards highly recommended
- Never use fingers near blade
- Consider a sled or jig for added safety

**Thick Stock Warnings (> 2"):**
- Check blade capacity
- Multiple passes may be safer
- Use proper support/outfeed
- Reduced blade speed may help
- Watch for blade binding

### Safety Tips
- **Never freehand cut** - Always use fence or miter gauge
- **Zero-clearance inserts** - Prevent thin stock from diving under blade
- **Outfeed support** - Essential for long boards
- **Blade guard** - Keep in place unless operation requires removal
- **Splitter/riving knife** - Prevents kickback by keeping kerf open
- **Push sticks** - Keep hands 6" away from blade
- **Hearing and eye protection** - Always mandatory

---

## 3. Push Stick Requirements Evaluator

### Purpose
Determines when push sticks and other safety devices are required based on stock dimensions and operation details.

### Why It Matters
- **Hand injuries are the most common woodworking accident**
- **The 6-inch rule** - Hands should never be within 6" of the blade
- **Risk increases exponentially** with narrow or thin stock
- **Multiple safety devices** work together to keep you safe
- **Industry standards** exist for good reason - they save lives and fingers

### How to Use

1. **Enter Stock Width (inches)**
   - The width of the board being cut
   - Example: 2.5" for narrow strips, 12" for wide panels

2. **Enter Stock Thickness (inches)**
   - The thickness of the board being cut
   - Example: 0.75" for ¾" lumber, 0.25" for ¼" thin stock

3. **Check Blade Guard in Place**
   - ✓ Checked = Guard is being used
   - ☐ Unchecked = Guard removed (increases risk level)

4. **Check Using Featherboards**
   - ✓ Checked = Featherboard helping guide stock
   - ☐ Unchecked = No featherboard (increases risk for thin/narrow stock)

5. **Click Evaluate** (or updates automatically as you change inputs)

### Understanding Results

**Risk Levels:**

- **LOW RISK (Green)**
  - Stock > 12" wide
  - Standard safety precautions apply
  - Push stick recommended for final pass
  - Blade guard in place

- **MODERATE RISK (Dark Orange)**
  - Stock 6"-12" wide
  - Push stick recommended for last 12"
  - Blade guard should be in place
  - Featherboard helpful for consistency
  - Maintain 6" clearance from blade

- **HIGH RISK (Orange)**
  - Stock 3"-6" wide OR < ¾" thick
  - **Push stick MANDATORY**
  - Push block recommended
  - Featherboard highly recommended
  - Blade guard must be in place
  - Hands at least 6" from blade
  - Use outfeed support

- **CRITICAL RISK (Red)**
  - Stock < 3" wide OR < ½" thick
  - 🚨 **CRITICAL DANGER ZONE** 🚨
  - **TWO push sticks recommended**
  - Push block for downward pressure
  - Featherboards (front and side) - **MANDATORY**
  - Blade guard MUST be in place
  - Zero-clearance insert required
  - ⛔ **NEVER use hands within 6" of blade**
  - ⛔ **Consider using a sled or jig instead**

### Safety Device Requirements

**Push Sticks:**
- Keep hands 6"+ away from blade
- Apply horizontal pushing force
- Use two for very narrow stock
- Make multiple - they're expendable

**Push Blocks:**
- Provide downward pressure
- Prevent stock from lifting
- Use with push stick for narrow stock
- Essential for thin stock

**Featherboards:**
- Apply consistent side pressure
- Keep stock against fence
- Prevent kickback
- Use on infeed side, never outfeed

**Blade Guard:**
- Primary protection against blade contact
- Reduces noise and debris
- Should always be in place unless operation specifically requires removal
- Modern guards don't interfere with most cuts

**Zero-Clearance Insert:**
- Prevents thin stock from diving under blade
- Reduces tearout
- Essential for thin stock (< ¼")
- Easy to make from MDF or hardboard

### General Safety Rules
- Never reach over or behind blade
- Stand to the side, not directly behind stock
- Remove jewelry and tie back long hair
- No gloves when operating power tools
- Eye and hearing protection mandatory
- Dust collection/mask for health
- Never force the cut - let the saw do the work
- If it feels unsafe, it probably is - find another way

---

## 4. Dust Collection CFM Calculator

### Purpose
Calculates the required CFM (Cubic Feet per Minute) for effective dust collection based on tool type, port size, and duct length.

### Why It Matters
- **Wood dust is carcinogenic** - Especially hardwoods, MDF, and plywood
- **Fine dust (< 10 microns) is most dangerous** - Penetrates deep into lungs
- **Inadequate dust collection = health hazard** - Long-term exposure causes respiratory disease
- **Tool performance** - Inadequate CFM can damage tools (especially sanders)
- **Shop cleanliness** - Good collection means less cleanup

### How to Use

1. **Enter Tool Port Diameter (inches)**
   - Measure the dust port on your tool
   - Common sizes: 2.5", 4", 6"
   - Example: Most table saws have 4" ports

2. **Select Tool Type**
   - **Table Saw** - 450-650 CFM recommended
   - **Router Table** - 300-400 CFM recommended
   - **Miter Saw** - 500-700 CFM recommended
   - **Planer** - 600-1000 CFM recommended
   - **Jointer** - 500-750 CFM recommended
   - **Bandsaw** - 300-450 CFM recommended
   - **Drum Sander** - 800-1200 CFM recommended
   - **Thickness Sander** - 1000-1500 CFM recommended

3. **Enter Duct Length (feet)**
   - Measure total run from collector to tool
   - Include equivalent length for fittings
   - Each 90° elbow ≈ 5 feet of straight duct loss
   - Example: 10' straight + two 90° elbows = 20' total

4. **Click Calculate** (or updates automatically as you change inputs)

### Understanding Results

**Required CFM:** The minimum CFM needed at the tool

**Calculation Details:**
- Port diameter and area
- Air velocity (4,000 FPM - industry standard)
- Base CFM calculation
- Duct length loss factor
- Tool-specific multiplier
- Adjusted CFM with losses

**Tool-Specific Requirements:**
Each tool type shows:
- Minimum CFM (barely adequate)
- Recommended CFM (ideal range)
- Special considerations
- Performance notes

### General Dust Collection Tips

**System Design:**
- **Shorter, straighter runs** = better performance
- **Each 90° elbow** = ~5 feet of straight duct loss
- **Smooth interior ducts** perform better (metal vs. flexible)
- **Static pressure** matters as much as CFM
- **Ground metal ducts** to prevent static electricity
- **Blast gates** to direct full airflow to active tool

**Filtration:**
- **Fine dust requires 1-micron filtration** minimum
- **HEPA filters (0.3 micron)** for ultimate protection
- **Cyclone separator** extends filter life dramatically
- **2-stage collection** (cyclone + filter) is ideal

**Health & Safety:**
- ⚠️ Wood dust is carcinogenic (especially hardwoods)
- ⚠️ Inadequate collection = health hazard
- ⚠️ Wear respirator even with good collection
- ⚠️ Fine dust (< 10 microns) is most dangerous
- ⚠️ MDF, plywood dust particularly hazardous
- ⚠️ Long-term exposure causes respiratory disease

### CFM Requirements by Tool

**Table Saw (350-650 CFM)**
- Below-table collection most important
- Blade guard collection helps with finer dust
- Larger blades generate more dust

**Router Table (200-400 CFM)**
- Port near bit is essential
- Produces very fine dust
- Consider overhead collection too

**Miter Saw (400-700 CFM)**
- Dust is thrown with cutting action
- Dust shroud/hood improves collection
- Need higher CFM due to throwing action

**Planer (400-1000 CFM)**
- Portable: 400 CFM minimum
- Stationary: 600-1000 CFM
- Produces high volume of chips
- Larger ports perform much better

**Jointer (350-750 CFM)**
- 6" jointer: 350 CFM minimum
- 8" jointer: 500 CFM minimum
- 8"+ jointers need substantially more
- Chips can clog smaller ports

**Bandsaw (200-450 CFM)**
- Relatively low dust production
- Dual ports (upper/lower) recommended
- Fine dust accumulates inside cabinet
- Easy to collect from

**Drum Sander (600-1200 CFM)**
- Produces very fine hazardous dust
- Inadequate CFM = health hazard
- Cyclone pre-separator highly recommended
- Most dangerous dust to breathe

**Thickness Sander (800-1500 CFM)**
- Extremely high dust production
- Inadequate CFM can damage machine
- Premium filtration essential
- Professional-grade collection needed

### Sizing Your Dust Collector

**Single-Stage (Bag/Filter Only):**
- Good for: 1-2 HP, small shops
- CFM: 400-650 CFM typical
- Limitations: Filter clogs quickly
- Best for: Occasional use

**Two-Stage (Cyclone + Filter):**
- Good for: Any shop size
- CFM: 650-1500 CFM typical
- Advantages: 95%+ debris separated before filter
- Best for: Serious woodworking

**Central System:**
- Good for: Large shops, multiple tools
- CFM: 1500-3000+ CFM
- Features: Blast gates, large ducting
- Best for: Professional shops

---

## General Safety Principles

### The Safety Hierarchy
1. **Elimination** - Can you avoid the hazard entirely?
2. **Substitution** - Can you use a safer method?
3. **Engineering Controls** - Guards, jigs, fixtures
4. **Administrative Controls** - Training, procedures
5. **PPE (Personal Protective Equipment)** - Last line of defense

### Essential PPE
- ☑ **Safety glasses** - ALWAYS, even for "quick" cuts
- ☑ **Hearing protection** - Prolonged exposure causes permanent damage
- ☑ **Dust mask/respirator** - Wood dust is a carcinogen
- ☑ **Face shield** - For lathes and other high-speed operations
- ☐ **NO gloves** - Never wear gloves around rotating tools

### Safety Mindset
- **If it feels wrong, it probably is** - Trust your instincts
- **There are no emergencies in woodworking** - Take your time
- **Fatigue kills** - Stop when you're tired
- **Distraction kills** - Stay focused, no interruptions
- **Complacency kills** - Respect every cut, every time
- **Adequate lighting** - You can't cut safely what you can't see clearly
- **Shop organization** - Trip hazards and clutter cause accidents

---

## Additional Resources

### Recommended Reading
- "The Tablesaw Book" by Kelly Mehler
- "Working Safely in the Woodshop" (FineWoodworking)
- OSHA Woodworking Safety Guidelines

### Online Resources
- WoodWorkWeb Safety Videos (YouTube)
- Highland Woodworking Safety Articles
- Woodworker's Journal Safety Tips

### Organizations
- Occupational Safety and Health Administration (OSHA)
- National Institute for Occupational Safety and Health (NIOSH)
- American Woodworking Federation

---

## Feedback and Updates

These calculators are based on industry standards and best practices as of 2026. If you notice any errors or have suggestions for improvements, please contact the developer.

**Version:** 1.0  
**Last Updated:** 2026-01-30  
**Part of:** Woodworker's Friend v10.0

---

**Remember:** These are tools to help you work more safely. They don't replace common sense, manufacturer guidelines, or professional training. Stay safe and happy woodworking! 🪚
