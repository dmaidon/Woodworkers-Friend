' ============================================================================
' Last Updated: January 30, 2026
' Changes: DEPRECATED - Data now loaded from SQLite database (Phase 2 complete)
'          This file kept for reference only. Will be removed in future version.
'          Use DatabaseManager.Instance.GetAllWoodSpecies() instead.
' ============================================================================

' *** OBSOLETE - DO NOT USE ***
' This class is deprecated as of Phase 2 migration to SQLite database.
' All wood species data now stored in WoodworkersFriend.db
' Use: DatabaseManager.Instance.GetAllWoodSpecies()

Imports System.ComponentModel

''' <summary>
''' Provides wood species properties data
''' **OBSOLETE: Use DatabaseManager.Instance.GetAllWoodSpecies() instead**
''' </summary>
<Obsolete("This class is deprecated. Use DatabaseManager.Instance.GetAllWoodSpecies() instead.")>
Public Class WoodPropertiesDatabase

    ''' <summary>
    ''' Gets the complete list of wood species with properties
    ''' </summary>
    Public Shared Function GetWoodSpeciesList() As List(Of WoodPropertiesData)
        ' HARDWOODS
        ' EXOTIC HARDWOODS - Popular for fine woodworking
        Dim woodList As New List(Of WoodPropertiesData) From {
            New WoodPropertiesData With {
.CommonName = "Ash (White)",
.ScientificName = "Fraxinus americana",
.JankaHardness = 1320,
.SpecificGravity = 0.6,
.MoistureContent = 0.12,
.Density = 41,
.ShrinkageRadial = 0.049,
.ShrinkageTangential = 0.078,
.WoodType = "Hardwood",
.TypicalUses = "Baseball bats, tool handles, furniture, flooring, cabinets, millwork, sporting goods. Excellent shock resistance makes it ideal for tool handles and sporting equipment.",
.Workability = "Moderate difficulty. Machines well with sharp tools. Glues, stains, and finishes satisfactorily. Nailing and screwing may require pre-drilling. Good steam-bending properties.",
.Cautions = "Large pores may require grain filler. Can be brittle if dried too quickly. Moderately durable but not suitable for ground contact. Susceptible to insect attack."
},
            New WoodPropertiesData With {
.CommonName = "Basswood",
.ScientificName = "Tilia americana",
.JankaHardness = 410,
.SpecificGravity = 0.37,
.MoistureContent = 0.12,
.Density = 26,
.ShrinkageRadial = 0.066,
.ShrinkageTangential = 0.093,
.WoodType = "Hardwood",
.TypicalUses = "Carving, turning, pattern making, venetian blinds, musical instruments, interior woodwork. Excellent for hand carving due to its softness and fine grain.",
.Workability = "Very easy to work. Soft and lightweight. Machines, glues, and finishes well. Excellent for hand tools and carving. Takes paint beautifully. Popular choice for beginners.",
.Cautions = "Very soft - dents and scratches extremely easily. Not suitable for structural applications. Low decay resistance. Must be painted or sealed for exterior use."
},
            New WoodPropertiesData With {
.CommonName = "Beech (American)",
.ScientificName = "Fagus grandifolia",
.JankaHardness = 1300,
.SpecificGravity = 0.64,
.MoistureContent = 0.12,
.Density = 45,
.ShrinkageRadial = 0.055,
.ShrinkageTangential = 0.119,
.WoodType = "Hardwood",
.TypicalUses = "Furniture, flooring, woodenware, tool handles, railroad ties, veneer, plywood. Frequently used in furniture that will be painted. Excellent for food-contact surfaces.",
.Workability = "Moderate to difficult. Machines well but hard on cutting edges. Glues and finishes well. Excellent steam-bending properties. Pre-drill for nails and screws.",
.Cautions = "High shrinkage - prone to movement with humidity changes. Not durable outdoors. Can develop color variations. Susceptible to fungal attack if not properly dried."
},
            New WoodPropertiesData With {
.CommonName = "Birch (Yellow)",
.ScientificName = "Betula alleghaniensis",
.JankaHardness = 1260,
.SpecificGravity = 0.62,
.MoistureContent = 0.12,
.Density = 43,
.ShrinkageRadial = 0.073,
.ShrinkageTangential = 0.095,
.WoodType = "Hardwood",
.TypicalUses = "Furniture, cabinets, doors, flooring, veneer, plywood, turned objects, interior trim. Often used as a substitute for more expensive hardwoods.",
.Workability = "Moderate difficulty. Machines well with sharp tools. Glues and finishes well but can be blotchy when stained. Pre-drill recommended. Good for turning.",
.Cautions = "Can be difficult to stain evenly - use wood conditioner. High shrinkage. Not suitable for outdoor use. May contain pitch pockets or mineral streaks."
},
            New WoodPropertiesData With {
.CommonName = "Cedar (Western Red)",
.ScientificName = "Thuja plicata",
.JankaHardness = 350,
.SpecificGravity = 0.32,
.MoistureContent = 0.12,
.Density = 23,
.ShrinkageRadial = 0.024,
.ShrinkageTangential = 0.05,
.WoodType = "Softwood",
.TypicalUses = "Outdoor furniture, decking, siding, shingles, fencing, closet linings, chests. Excellent natural decay resistance. Aromatic properties repel moths.",
.Workability = "Very easy to work. Soft and lightweight. Machines, glues, and finishes well. May need sharp tools to prevent fuzzing. Excellent dimensional stability.",
.Cautions = "Very soft - dents easily. Contains natural oils that may affect some finishes. Aromatic odor may be strong initially. Can cause allergic reactions in some people."
},
            New WoodPropertiesData With {
.CommonName = "Cherry (Black)",
.ScientificName = "Prunus serotina",
.JankaHardness = 950,
.SpecificGravity = 0.5,
.MoistureContent = 0.12,
.Density = 35,
.ShrinkageRadial = 0.037,
.ShrinkageTangential = 0.071,
.WoodType = "Hardwood",
.TypicalUses = "Fine furniture, cabinets, architectural millwork, musical instruments, turned objects, interior trim. Prized for its rich color that deepens with age.",
.Workability = "Easy to moderate. Machines, glues, stains, and finishes very well. Excellent for hand tools. Good bending properties. One of the best woods for finishing.",
.Cautions = "Color darkens significantly with UV exposure. Can have gum pockets or pitch. Not suitable for outdoor use. May develop small pin knots."
},
            New WoodPropertiesData With {
.CommonName = "Cypress (Bald)",
.ScientificName = "Taxodium distichum",
.JankaHardness = 510,
.SpecificGravity = 0.46,
.MoistureContent = 0.12,
.Density = 32,
.ShrinkageRadial = 0.037,
.ShrinkageTangential = 0.062,
.WoodType = "Softwood",
.TypicalUses = "Siding, outdoor furniture, decking, boat building, caskets, trim. Excellent decay resistance makes it ideal for outdoor applications.",
.Workability = "Easy to work. Machines, glues, and finishes well. May contain pockets of oil/resin. Good dimensional stability. Aromatic scent when cut.",
.Cautions = "Can vary widely in density and color. Resin pockets may bleed through finish. Odor may be unpleasant to some. Check local availability as old-growth is rare."
},
            New WoodPropertiesData With {
.CommonName = "Douglas Fir",
.ScientificName = "Pseudotsuga menziesii",
.JankaHardness = 660,
.SpecificGravity = 0.48,
.MoistureContent = 0.12,
.Density = 34,
.ShrinkageRadial = 0.045,
.ShrinkageTangential = 0.075,
.WoodType = "Softwood",
.TypicalUses = "Construction lumber, plywood, veneer, flooring, timbers, poles. One of the most important structural woods in North America.",
.Workability = "Moderate. Machines well but can be hard on cutting tools. Resinous - clean blades frequently. Glues and finishes well with proper surface prep. Pre-drill recommended.",
.Cautions = "Very resinous - can gum up tools. Prone to splitting when nailing. Not naturally decay resistant. Strong grain can make sanding difficult."
},
            New WoodPropertiesData With {
.CommonName = "Hickory",
.ScientificName = "Carya spp.",
.JankaHardness = 1820,
.SpecificGravity = 0.72,
.MoistureContent = 0.12,
.Density = 51,
.ShrinkageRadial = 0.07,
.ShrinkageTangential = 0.108,
.WoodType = "Hardwood",
.TypicalUses = "Tool handles, ladder rungs, sporting goods, flooring, drumsticks. Extremely strong and shock-resistant. Traditional choice for axe and hammer handles.",
.Workability = "Difficult to work. Very hard and dense. Dulls cutting tools quickly. Difficult to machine and glue. Excellent for steam bending. Pre-drill essential.",
.Cautions = "Extremely high shrinkage - expect significant movement. Very hard on tools. Can be difficult to dry without checking. Not durable in ground contact."
},
            New WoodPropertiesData With {
.CommonName = "Mahogany (Genuine)",
.ScientificName = "Swietenia macrophylla",
.JankaHardness = 800,
.SpecificGravity = 0.5,
.MoistureContent = 0.12,
.Density = 35,
.ShrinkageRadial = 0.031,
.ShrinkageTangential = 0.045,
.WoodType = "Hardwood",
.TypicalUses = "Fine furniture, boat building, musical instruments, carving, cabinetry, veneer. Prized for dimensional stability and beautiful grain patterns.",
.Workability = "Very easy to work. Machines, glues, stains, and finishes excellently. Great for hand tools and carving. Excellent dimensional stability.",
.Cautions = "True mahogany is expensive and availability is limited. Many substitutes sold as 'mahogany'. Check CITES regulations for imported species. Can have interlocked grain."
},
            New WoodPropertiesData With {
.CommonName = "Maple (Hard/Sugar)",
.ScientificName = "Acer saccharum",
.JankaHardness = 1450,
.SpecificGravity = 0.63,
.MoistureContent = 0.12,
.Density = 44,
.ShrinkageRadial = 0.048,
.ShrinkageTangential = 0.099,
.WoodType = "Hardwood",
.TypicalUses = "Flooring, furniture, cabinetry, butcher blocks, cutting boards, musical instruments, bowling pins, bowling alley lanes. Excellent for high-traffic areas.",
.Workability = "Difficult to work. Hard and dense. Dulls tools quickly. Can burn when routing or drilling. Glues well. Difficult to stain evenly - use conditioner.",
.Cautions = "Very hard - use sharp tools and slow feed rates. Can develop bird's eye or curly figure (highly prized). Difficult to stain evenly. May develop mineral streaks."
},
            New WoodPropertiesData With {
.CommonName = "Oak (Red)",
.ScientificName = "Quercus rubra",
.JankaHardness = 1290,
.SpecificGravity = 0.63,
.MoistureContent = 0.12,
.Density = 44,
.ShrinkageRadial = 0.04,
.ShrinkageTangential = 0.086,
.WoodType = "Hardwood",
.TypicalUses = "Furniture, cabinetry, flooring, interior trim, veneer, plywood, barrels, caskets. Popular for its strength and attractive open grain pattern.",
.Workability = "Moderate difficulty. Machines well with sharp tools. Glues, stains, and finishes well. Large pores may require grain filler. Good bending properties. Pre-drill for nails.",
.Cautions = "Large open pores require grain filler for smooth finish. Can be difficult to stain evenly. Not suitable for outdoor use without treatment. Moderately resistant to decay."
},
            New WoodPropertiesData With {
.CommonName = "Oak (White)",
.ScientificName = "Quercus alba",
.JankaHardness = 1360,
.SpecificGravity = 0.68,
.MoistureContent = 0.12,
.Density = 47,
.ShrinkageRadial = 0.056,
.ShrinkageTangential = 0.105,
.WoodType = "Hardwood",
.TypicalUses = "Furniture, flooring, cabinets, boat building, barrels (whiskey), veneer, fence posts. More decay-resistant than red oak. Traditional choice for barrel making.",
.Workability = "Moderate difficulty. Machines well but hard on cutting edges. Glues and finishes well. Excellent for steam bending. Large pores require filler for smooth finish.",
.Cautions = "Tyloses in pores make it liquid-tight (good for barrels). High tannin content can corrode some metals. Requires grain filler. Higher shrinkage than red oak."
},
            New WoodPropertiesData With {
.CommonName = "Pine (Eastern White)",
.ScientificName = "Pinus strobus",
.JankaHardness = 380,
.SpecificGravity = 0.35,
.MoistureContent = 0.12,
.Density = 25,
.ShrinkageRadial = 0.021,
.ShrinkageTangential = 0.061,
.WoodType = "Softwood",
.TypicalUses = "Construction lumber, trim, mouldings, paneling, furniture, carving, pattern making. Excellent for painted projects and general woodworking. Great for learning.",
.Workability = "Very easy to work. Soft and lightweight. Machines, glues, and paints excellently. Great for hand tools and carving. Excellent for beginners.",
.Cautions = "Very soft - dents and scratches easily. Resinous - can bleed through paint if not sealed. Not suitable for high-wear surfaces or structural use. Low decay resistance."
},
            New WoodPropertiesData With {
.CommonName = "Pine (Southern Yellow)",
.ScientificName = "Pinus spp.",
.JankaHardness = 690,
.SpecificGravity = 0.55,
.MoistureContent = 0.12,
.Density = 36,
.ShrinkageRadial = 0.053,
.ShrinkageTangential = 0.074,
.WoodType = "Softwood",
.TypicalUses = "Construction lumber, flooring, plywood, millwork, poles, railroad ties. One of the strongest softwoods. Excellent for structural applications.",
.Workability = "Moderate. Harder than most softwoods. Resinous - requires frequent blade cleaning. Machines well but can splinter. Pre-drill for nailing. Glues well with proper prep.",
.Cautions = "Very resinous - pitch can gum up tools. Dense lite wood can be difficult to nail. Not decay resistant unless treated. Prone to warping if not properly dried."
},
            New WoodPropertiesData With {
.CommonName = "Poplar (Yellow)",
.ScientificName = "Liriodendron tulipifera",
.JankaHardness = 540,
.SpecificGravity = 0.42,
.MoistureContent = 0.12,
.Density = 28,
.ShrinkageRadial = 0.044,
.ShrinkageTangential = 0.082,
.WoodType = "Hardwood",
.TypicalUses = "Furniture frames, interior trim, mouldings, painted cabinets, boxes, crates, pallets. Excellent for painted projects. Often used where strength isn't critical.",
.Workability = "Very easy to work. Soft for a hardwood. Machines, glues, and paints excellently. Takes nails and screws well. Good dimensional stability.",
.Cautions = "Color can vary from white to green to purple. Not suitable for staining - best painted. Soft - dents easily. Not durable outdoors. May have mineral streaks."
},
            New WoodPropertiesData With {
.CommonName = "Walnut (Black)",
.ScientificName = "Juglans nigra",
.JankaHardness = 1010,
.SpecificGravity = 0.55,
.MoistureContent = 0.12,
.Density = 38,
.ShrinkageRadial = 0.055,
.ShrinkageTangential = 0.078,
.WoodType = "Hardwood",
.TypicalUses = "Fine furniture, cabinetry, gunstocks, musical instruments, flooring, veneer, turned objects. Prized for its rich dark color and excellent working properties.",
.Workability = "Easy to moderate. Machines, glues, stains, and finishes beautifully. Excellent for hand tools and carving. Good dimensional stability. One of the best woods to work.",
.Cautions = "Expensive and availability may be limited. Sapwood is light colored (sometimes mixed for contrast). Dust can be irritating. Natural oils may affect some finishes."
},
            New WoodPropertiesData With {
.CommonName = "Purpleheart",
.ScientificName = "Peltogyne spp.",
.JankaHardness = 2520,
.SpecificGravity = 0.86,
.MoistureContent = 0.12,
.Density = 56,
.ShrinkageRadial = 0.032,
.ShrinkageTangential = 0.064,
.WoodType = "Hardwood",
.TypicalUses = "Fine furniture, turnings, flooring, musical instruments, inlays, accent pieces. Highly prized for its distinctive purple color that darkens with age. Excellent for decorative work.",
.Workability = "Difficult to work. Very hard and dense. Dulls cutting tools quickly. Can be difficult to glue due to high density. Takes a beautiful finish. Pre-drilling essential. Turns and polishes excellently.",
.Cautions = "Extremely hard - use sharp carbide tools. Color fades from bright purple to dark brown if not UV protected. Can cause respiratory irritation. Difficult to glue - use epoxy. Blunts tools rapidly."
},
            New WoodPropertiesData With {
.CommonName = "Padauk (African)",
.ScientificName = "Pterocarpus soyauxii",
.JankaHardness = 1725,
.SpecificGravity = 0.72,
.MoistureContent = 0.12,
.Density = 48,
.ShrinkageRadial = 0.026,
.ShrinkageTangential = 0.042,
.WoodType = "Hardwood",
.TypicalUses = "Fine furniture, flooring, turned objects, musical instruments, inlays, decorative veneer. Popular for its vibrant orange-red color. Excellent for accent pieces and contrasting elements.",
.Workability = "Moderate difficulty. Machines well with sharp tools. Glues satisfactorily. Can be difficult to work due to interlocked grain. Takes an excellent finish. Good dimensional stability.",
.Cautions = "Color fades from bright orange-red to darker reddish-brown over time. Dust and splinters can cause skin irritation. Contains yellow dye that can stain clothing and workbench. May have interlocked grain."
},
            New WoodPropertiesData With {
.CommonName = "Yellowheart",
.ScientificName = "Euxylophora paraensis",
.JankaHardness = 1860,
.SpecificGravity = 0.76,
.MoistureContent = 0.12,
.Density = 51,
.ShrinkageRadial = 0.038,
.ShrinkageTangential = 0.065,
.WoodType = "Hardwood",
.TypicalUses = "Fine furniture, turnings, inlays, musical instruments, decorative items, accent pieces. Prized for its bright golden yellow color. Popular for contrasting with darker woods.",
.Workability = "Moderate difficulty. Machines well with sharp tools. Can have interlocked grain that affects planing. Glues and finishes well. Takes an excellent polish. Good for turning.",
.Cautions = "Color darkens from bright yellow to golden brown with age and UV exposure. Can cause skin irritation. Interlocked grain may cause tearout. Moderately expensive and limited availability."
},
            New WoodPropertiesData With {
.CommonName = "Tigerwood (Goncalo Alves)",
.ScientificName = "Astronium graveolens",
.JankaHardness = 1850,
.SpecificGravity = 0.9,
.MoistureContent = 0.12,
.Density = 58,
.ShrinkageRadial = 0.047,
.ShrinkageTangential = 0.078,
.WoodType = "Hardwood",
.TypicalUses = "Fine furniture, flooring, turnings, knife handles, musical instruments, inlays. Highly prized for dramatic striped figure. Excellent for decorative applications and accent pieces.",
.Workability = "Difficult to work. Very hard and dense. Interlocked grain can cause tearout. Dulls tools quickly. Glues well with proper surface preparation. Takes an excellent finish. Good for turning.",
.Cautions = "Very hard - use sharp carbide tools. Irregular grain can make planing difficult. Can be oily and affect gluing. Dust may cause skin and respiratory irritation. Expensive. High shrinkage."
},
            New WoodPropertiesData With {
.CommonName = "Maple (Soft/Red)",
.ScientificName = "Acer rubrum",
.JankaHardness = 950,
.SpecificGravity = 0.54,
.MoistureContent = 0.12,
.Density = 38,
.ShrinkageRadial = 0.037,
.ShrinkageTangential = 0.072,
.WoodType = "Hardwood",
.TypicalUses = "Furniture, cabinets, boxes, crates, musical instruments, veneer, plywood. More affordable alternative to hard maple. Good for painted projects and general woodworking.",
.Workability = "Easy to moderate. Softer than hard maple - easier on tools. Machines, glues, and finishes well. Can be blotchy when stained - use wood conditioner. Good for general woodworking.",
.Cautions = "Softer than hard maple - not suitable for high-wear surfaces like flooring. Can be difficult to stain evenly. May develop figure (curly, quilted) which is highly prized but harder to work. Burns easily when routing."
},
            New WoodPropertiesData With {
.CommonName = "Birch (Paper)",
.ScientificName = "Betula papyrifera",
.JankaHardness = 910,
.SpecificGravity = 0.55,
.MoistureContent = 0.12,
.Density = 38,
.ShrinkageRadial = 0.063,
.ShrinkageTangential = 0.086,
.WoodType = "Hardwood",
.TypicalUses = "Plywood, furniture, cabinets, turned objects, toothpicks, interior trim. Often used as a paint-grade wood. Less expensive than yellow birch but with similar working properties.",
.Workability = "Easy to moderate. Machines, glues, and finishes well. Can be blotchy when stained - use conditioner. Good for turning. Takes paint excellently. Softer than yellow birch.",
.Cautions = "High shrinkage - expect significant movement. Not suitable for outdoor use. Can be difficult to stain evenly. Sapwood may contain mineral streaks. Not as strong as yellow birch."
},
            New WoodPropertiesData With {
.CommonName = "Sapele",
.ScientificName = "Entandrophragma cylindricum",
.JankaHardness = 1410,
.SpecificGravity = 0.62,
.MoistureContent = 0.12,
.Density = 42,
.ShrinkageRadial = 0.048,
.ShrinkageTangential = 0.072,
.WoodType = "Hardwood",
.TypicalUses = "Fine furniture, cabinetry, boat building, musical instruments (guitars), veneer, interior trim, flooring. Popular mahogany substitute with beautiful ribbon figure. Excellent for high-end applications.",
.Workability = "Moderate difficulty. Interlocked grain can be challenging to plane - may cause tearout. Machines reasonably well with sharp tools. Glues and finishes well. Beautiful ribbon stripe figure when quartersawn.",
.Cautions = "Interlocked grain requires care when planing - work at slight angle. Can cause respiratory irritation and skin sensitization. Blunts tools moderately. Ribbon figure beautiful but tricky to work. Check CITES regulations."
},
            New WoodPropertiesData With {
.CommonName = "Wenge",
.ScientificName = "Millettia laurentii",
.JankaHardness = 1630,
.SpecificGravity = 0.88,
.MoistureContent = 0.12,
.Density = 58,
.ShrinkageRadial = 0.038,
.ShrinkageTangential = 0.067,
.WoodType = "Hardwood",
.TypicalUses = "Flooring, furniture, knife handles, musical instruments, decorative veneer, turned objects, accent pieces. Prized for its very dark color with black veining. Popular for contemporary furniture and contrasts.",
.Workability = "Difficult to work. Very dense and hard. Dulls cutting tools quickly. Splinters easily - wear gloves. Can be difficult to glue. Pre-drilling essential. Takes excellent finish despite open grain. Good dimensional stability.",
.Cautions = "TOXIC DUST - wear respirator! Can cause severe respiratory issues and skin sensitization. Very splintery - handle with care. Extremely hard on tools - use carbide. Check CITES - may be restricted. Expensive and limited availability."
}
        }

        Return woodList
    End Function

    ''' <summary>
    ''' Gets a binding list for DataGridView binding
    ''' </summary>
    Public Shared Function GetBindingList() As BindingList(Of WoodPropertiesData)
        Return New BindingList(Of WoodPropertiesData)(GetWoodSpeciesList())
    End Function

End Class
