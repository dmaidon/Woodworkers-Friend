' ============================================================================
' Created: January 2026
' Purpose: Unified data models for multi-database architecture
' Used by: HelpDataManager, ReferenceDataManager, UserDataManager
' Design: Single comprehensive models with backward-compatibility properties
' ============================================================================

''' <summary>
''' Comprehensive wood species model - SINGLE SOURCE OF TRUTH
''' Used across all features:
'''   - Reference.db: Read-only reference species
'''   - UserData.db: User-added custom species
'''   - WoodMovementCalculator: Uses TangentialShrinkage, RadialShrinkage, Density
'''   - Reference Browser: Uses ALL properties
'''   - Cost Calculators: Uses CommonName, Density
''' </summary>
Public Class WoodSpecies
    ' Primary Identity (Database)
    Public Property CommonName As String
    Public Property ScientificName As String
    Public Property WoodType As String ' "Hardwood" or "Softwood"
    
    ' Physical Properties
    Public Property JankaHardness As Integer
    Public Property SpecificGravity As Double
    Public Property Density As Double ' lbs/ft³
    Public Property MoistureContent As Double
    
    ' Movement Properties (Database naming convention)
    Public Property ShrinkageRadial As Double ' % per 1% MC change (quarter sawn)
    Public Property ShrinkageTangential As Double ' % per 1% MC change (flat sawn)
    
    ' Reference Information
    Public Property TypicalUses As String
    Public Property Workability As String
    Public Property Cautions As String
    Public Property Notes As String
    
    ' ============================================================================
    ' BACKWARD COMPATIBILITY PROPERTIES
    ' For legacy code that uses old WoodSpeciesDatabase.WoodSpecies
    ' ============================================================================
    
    ''' <summary>
    ''' Legacy property - maps to CommonName
    ''' Used by: WoodMovementCalculator, legacy UI code
    ''' </summary>
    Public ReadOnly Property Name As String
        Get
            Return CommonName
        End Get
    End Property
    
    ''' <summary>
    ''' Legacy property - maps to ShrinkageRadial
    ''' Used by: WoodMovementCalculator
    ''' </summary>
    Public ReadOnly Property RadialShrinkage As Double
        Get
            Return ShrinkageRadial
        End Get
    End Property
    
    ''' <summary>
    ''' Legacy property - maps to ShrinkageTangential
    ''' Used by: WoodMovementCalculator
    ''' </summary>
    Public ReadOnly Property TangentialShrinkage As Double
        Get
            Return ShrinkageTangential
        End Get
    End Property
    
    ''' <summary>
    ''' Legacy property - computed from WoodType
    ''' Used by: WoodMovementCalculator, legacy filters
    ''' </summary>
    Public ReadOnly Property IsHardwood As Boolean
        Get
            Return WoodType?.Equals("Hardwood", StringComparison.OrdinalIgnoreCase) = True
        End Get
    End Property
End Class

''' <summary>
''' Hardware standard/specification model - unified from DatabaseManager schema
''' Used by Reference.db for hardware reference data
''' Maps to HardwareStandards table in database
''' </summary>
Public Class HardwareStandard
    ' Database columns (primary)
    Public Property HardwareID As Integer
    Public Property Category As String
    Public Property Type As String
    Public Property Brand As String
    Public Property PartNumber As String
    Public Property Description As String
    Public Property Dimensions As String
    Public Property MountingRequirements As String
    Public Property WeightCapacity As String
    Public Property TypicalUses As String
    Public Property InstallationNotes As String
    Public Property PurchaseLink As String
    Public Property IsUserAdded As Boolean
    Public Property DateAdded As DateTime
    
    ' Extended properties (for ReferenceDataManager compatibility)
    Public Property Name As String ' Maps to Type for backward compatibility
    Public Property Specifications As String ' Maps to Description
    Public Property CommonBrands As String ' CSV list of brands
    Public Property PartNumbers As String ' CSV list of part numbers
    Public Property Notes As String ' Additional notes
    Public Property InstallationTips As String ' Maps to InstallationNotes
End Class

''' <summary>
''' Joinery category constants (matches JoineryModels.vb)
''' Database stores as TEXT, not enum values
''' </summary>
Public Class JoineryCategory
    Public Const Frame As String = "Frame"
    Public Const Carcass As String = "Carcass"
    Public Const Box As String = "Box"
    Public Const Edge As String = "Edge"
    Public Const Corner As String = "Corner"
    Public Const Reinforcement As String = "Reinforcement"
    Public Const Traditional As String = "Traditional"
    Public Const Modern As String = "Modern"
End Class

''' <summary>
''' Joinery difficulty constants (matches JoineryModels.vb)
''' Database stores as TEXT, not enum values
''' </summary>
Public Class JoineryDifficulty
    Public Const Beginner As String = "Beginner"
    Public Const Intermediate As String = "Intermediate"
    Public Const Advanced As String = "Advanced"
End Class
