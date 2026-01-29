' ============================================================================
' Last Updated: January 29, 2026
' Changes: Initial creation - Wood properties reference data models
' ============================================================================

''' <summary>
''' Data model for wood species properties (reference section)
''' </summary>
Public Class WoodPropertiesData

    ''' <summary>Common name of the wood species</summary>
    Public Property CommonName As String

    ''' <summary>Janka hardness rating in pounds-force (lbf)</summary>
    Public Property JankaHardness As Integer

    ''' <summary>Specific gravity (oven-dry weight / volume)</summary>
    Public Property SpecificGravity As Double

    ''' <summary>Typical moisture content as decimal (e.g., 0.12 = 12%)</summary>
    Public Property MoistureContent As Double

    ''' <summary>Density in pounds per cubic foot</summary>
    Public Property Density As Integer

    ''' <summary>Radial shrinkage as decimal (e.g., 0.04 = 4%)</summary>
    Public Property ShrinkageRadial As Double

    ''' <summary>Tangential shrinkage as decimal (e.g., 0.086 = 8.6%)</summary>
    Public Property ShrinkageTangential As Double

    ''' <summary>Wood type: "Hardwood" or "Softwood"</summary>
    Public Property WoodType As String

    ''' <summary>Typical uses and applications</summary>
    Public Property TypicalUses As String

    ''' <summary>Workability information</summary>
    Public Property Workability As String

    ''' <summary>Cautions and notes</summary>
    Public Property Cautions As String

    ''' <summary>Scientific name (optional)</summary>
    Public Property ScientificName As String

    ''' <summary>Additional notes</summary>
    Public Property Notes As String

End Class

''' <summary>
''' Wood type enumeration for reference section
''' </summary>
Public Enum WoodPropertyType
    Hardwood
    Softwood
End Enum
