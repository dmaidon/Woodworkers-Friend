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
            If String.IsNullOrEmpty(WoodType) Then
                Return False
            End If
            Return WoodType.Equals("Hardwood", StringComparison.OrdinalIgnoreCase)
        End Get
    End Property
End Class


