# Woodworking Definitions & Glossary

A reference guide for woodworking terms, geometry concepts, and technical definitions used throughout the Woodworkers Friend application.

---

## Geometry Terms

### Apothem
**Definition:** The perpendicular distance from the center of a regular polygon to the midpoint of any of its sides.

**Also Known As:** Inradius (for the inscribed circle)

**Formula:** 
- From side length: `apothem = side_length / (2 × tan(π/n))`
- From radius: `apothem = radius × cos(π/n)`

Where `n` = number of sides

**Woodworking Application:** 
The apothem is crucial when building polygon-shaped projects like:
- Hexagonal planters or boxes
- Octagonal picture frames
- Circular tables with polygon bases
- Segmented turning blanks

When cutting mitered segments, the apothem determines the "flat-to-flat" measurement (the distance across the polygon measured perpendicular to the sides), while the radius determines the "point-to-point" measurement (corner to corner through center).

**Example:** For a hexagon with 5-inch sides:
- Apothem = 5 / (2 × tan(30°)) = 4.33 inches
- This is the distance from center to the middle of any flat side

**Visual:** Imagine a circle that fits perfectly inside the polygon, touching each side at exactly one point. The apothem is the radius of that inscribed circle.

---

### Circumradius (Radius)
**Definition:** The distance from the center of a regular polygon to any of its vertices (corners).

**Also Known As:** Circumscribed radius, outer radius

**Formula:**
- From side length: `radius = side_length / (2 × sin(π/n))`
- From apothem: `radius = apothem / cos(π/n)`

**Woodworking Application:**
The radius determines the size of the circle that would pass through all vertices of the polygon. This is important for:
- Determining the minimum board width needed for a polygon shape
- Setting up a router jig for circular cuts
- Calculating the swing radius for segmented bowl blanks

---

### Interior Angle
**Definition:** The angle formed inside a polygon where two adjacent sides meet at a vertex.

**Formula:** `Interior Angle = (n - 2) × 180° / n`

Where `n` = number of sides

**Common Values:**
- Triangle (3 sides): 60°
- Square (4 sides): 90°
- Pentagon (5 sides): 108°
- Hexagon (6 sides): 120°
- Octagon (8 sides): 135°

**Woodworking Application:**
Knowing the interior angle helps when joining polygon segments at corners, such as building hexagonal frames or octagonal boxes.

---

### Exterior Angle
**Definition:** The angle formed outside a polygon between one side and the extension of an adjacent side. Also the central angle subtended by one side.

**Formula:** `Exterior Angle = 360° / n`

**Relationship:** Interior Angle + Exterior Angle = 180°

**Woodworking Application:**
The exterior angle is the angle you need to turn at each corner when walking around the perimeter of the polygon.

---

### Miter Angle (Cut Angle)
**Definition:** The angle at which to cut each end of a polygon segment so that adjacent pieces meet perfectly.

**Formula:** `Miter Angle = (360° / n) / 2 = 180° / n`

**Also Known As:** Half the exterior angle, segment cut angle

**Common Values:**
- Triangle (3 sides): 60° miter
- Square (4 sides): 45° miter
- Pentagon (5 sides): 36° miter
- Hexagon (6 sides): 30° miter
- Octagon (8 sides): 22.5° miter
- Dodecagon (12 sides): 15° miter

**Woodworking Application:**
This is the angle you set your miter saw or table saw blade to when cutting segments for a polygon frame, segmented bowl, or any multi-sided assembly.

**Important:** Most miter saws display angles as deviation from 90°. For a hexagon, you would set the saw to 30° (not 60°).

---

### Perimeter
**Definition:** The total distance around the outside of a polygon.

**Formula:** `Perimeter = n × side_length`

**Woodworking Application:**
The perimeter tells you the total linear footage of material needed for the sides of a polygon shape. Add waste allowance (typically 10-20%) for saw kerfs and mistakes.

---

### Area
**Definition:** The total surface area enclosed by a polygon.

**Formula:** `Area = (n × side² ) / (4 × tan(π/n))`

**Alternative:** `Area = (Perimeter × Apothem) / 2`

**Woodworking Application:**
Useful for calculating material needed for polygon tabletops, box bottoms, or any flat polygon surface.

---

## Joinery Terms

### Board Feet
**Definition:** A unit of lumber volume equal to 144 cubic inches, or a board that is 1 inch thick, 12 inches wide, and 12 inches long.

**Formula:** `Board Feet = (Thickness × Width × Length) / 144`

(All dimensions in inches)

**Woodworking Application:**
Board feet is the standard unit for purchasing hardwood lumber. Softwoods are typically sold by linear foot.

---

### Kerf
**Definition:** The width of material removed by a saw blade during cutting.

**Typical Values:**
- Table saw blade: 1/8" (0.125")
- Thin-kerf blade: 3/32" (0.094")
- Hand saw: varies by set

**Woodworking Application:**
Account for kerf when calculating how many pieces you can get from a board. Each cut removes material equal to the kerf width.

---

### Dado
**Definition:** A rectangular groove cut across the grain of a board, typically used to receive the end or edge of another board.

**Woodworking Application:**
Used for shelving, drawer construction, and case work. The dado width should match the thickness of the piece being inserted.

---

### Rabbet
**Definition:** An L-shaped step cut along the edge or end of a board.

**Woodworking Application:**
Used for back panels in cabinets, picture frames, and box construction.

---

### Biscuit
**Definition:** An oval-shaped piece of compressed wood used to align and strengthen butt joints.

**Common Sizes:**
- #0: 5/8" × 1-3/4"
- #10: 3/4" × 2-1/8"
- #20: 1" × 2-3/8"

---

## Wood Movement Terms

### Moisture Content (MC)
**Definition:** The percentage of water weight compared to the oven-dry weight of wood.

**Formula:** `MC = ((Wet Weight - Dry Weight) / Dry Weight) × 100%`

**Target Values:**
- Furniture: 6-8% MC
- Outdoor use: 12-15% MC
- Construction lumber: 15-19% MC

---

### Equilibrium Moisture Content (EMC)
**Definition:** The moisture content at which wood neither gains nor loses moisture to the surrounding air.

**Woodworking Application:**
Allow wood to acclimate to your shop's EMC before final dimensioning. This minimizes movement after construction.

---

### Tangential Movement
**Definition:** Wood movement parallel to the growth rings (typically the most significant movement).

**Woodworking Application:**
Tangential movement is usually 1.5-2× greater than radial movement. This is why flat-sawn boards cup more than quarter-sawn boards.

---

### Radial Movement
**Definition:** Wood movement perpendicular to the growth rings.

**Woodworking Application:**
Quarter-sawn lumber experiences primarily radial movement, which is more stable and consistent.

---

## Safety Terms

### Kickback
**Definition:** The dangerous condition where a workpiece is violently thrown back toward the operator by a spinning blade.

**Prevention:**
- Use riving knife and splitter
- Never stand directly behind the blade
- Use proper blade guard
- Maintain sharp blades

---

### Push Stick
**Definition:** A safety device used to push material through a saw blade while keeping hands at a safe distance.

**When to Use:**
- When ripping material less than 6" wide
- When material is less than 3" from the blade
- Any time hands would be uncomfortably close to the blade

---

## Finishing Terms

### Grit Progression
**Definition:** The sequence of sandpaper grits used when smoothing wood, typically doubling or increasing by 50% between grits.

**Typical Progression:** 80 → 120 → 180 → 220 → 320

**Woodworking Application:**
Skipping grits leaves scratches from the coarser paper that show through the finish. Each grit should remove the scratches from the previous grit.

---

*This glossary is continuously updated. Suggestions for new terms can be added through the application's help system.*

---

**Version:** 1.0
**Last Updated:** January 2026
