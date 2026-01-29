' ============================================================================
' Last Updated: January 29, 2026
' Changes: Initial creation - Shelf sag calculator data models
'          Added ShelfSupportType enum for dado vs bracket support
' ============================================================================

''' <summary>
''' Supported material types for shelf calculations
''' </summary>
Public Enum ShelfMaterialType
    Plywood
    MDF
    ParticleBoard
    SolidWoodSYPine
    SolidWoodWPine
    SolidWoodPoplar
    SolidWoodOak
    SolidWoodMaple
    SolidWoodWalnut
    SolidWoodCherry
    SolidWoodMahogany
    MelamineBoard
    OSB
    Bamboo
End Enum

''' <summary>
''' Shelf support type - affects how span and fixity are calculated
''' </summary>
Public Enum ShelfSupportType
    ''' <summary>Bracket or cleat support - simple support with free rotation</summary>
    Bracket
    ''' <summary>Pin support - small cylindrical supports, simple support</summary>
    Pin
    ''' <summary>Dado or groove support - partial fixity at ends</summary>
    Dado
End Enum

''' <summary>
''' Input parameters for shelf sag calculation
''' </summary>
Public Class ShelfSagInput
    ''' <summary>Span length in inches (total shelf length)</summary>
    Public Property SpanLength As Double

    ''' <summary>Load on shelf in pounds</summary>
    Public Property Load As Double

    ''' <summary>Material type</summary>
    Public Property MaterialType As ShelfMaterialType

    ''' <summary>Thickness of shelf in inches</summary>
    Public Property Thickness As Double

    ''' <summary>Width (depth) of shelf in inches</summary>
    Public Property Width As Double

    ''' <summary>Has front edge stiffener</summary>
    Public Property HasFrontStiffener As Boolean

    ''' <summary>Has back edge stiffener</summary>
    Public Property HasBackStiffener As Boolean

    ''' <summary>Height of stiffener(s) in inches</summary>
    Public Property StiffenerHeight As Double

    ''' <summary>Thickness of stiffener(s) in inches</summary>
    Public Property StiffenerThickness As Double

    ''' <summary>Material type for stiffener(s)</summary>
    Public Property StiffenerMaterial As ShelfMaterialType

    ''' <summary>Type of shelf support (Bracket, Pin, or Dado)</summary>
    Public Property SupportType As ShelfSupportType

    ''' <summary>Width of ONE bracket support in inches - app multiplies by 2 for both sides</summary>
    Public Property BracketWidth As Double

    ''' <summary>Diameter/width of ONE pin support in inches - app multiplies by 2 for both sides</summary>
    Public Property PinWidth As Double

    ''' <summary>Dado depth (inches) - depth of groove cut into side panels</summary>
    Public Property DadoDepth As Double
End Class

''' <summary>
''' Results from shelf sag calculation
''' </summary>
Public Class ShelfSagResult

    ''' <summary>Expected sag in inches</summary>
    Public Property SagInches As Double

    ''' <summary>Expected sag in millimeters</summary>
    Public Property SagMillimeters As Double

    ''' <summary>Expected sag as fraction string</summary>
    Public Property SagFraction As String

    ''' <summary>Safe load in pounds</summary>
    Public Property SafeLoad As Double

    ''' <summary>Maximum recommended load in pounds</summary>
    Public Property MaxLoad As Double

    ''' <summary>Is the current load safe?</summary>
    Public Property IsSafe As Boolean

    ''' <summary>Safety factor (ratio of safe load to actual load)</summary>
    Public Property SafetyFactor As Double

    ''' <summary>Material modulus of elasticity (psi)</summary>
    Public Property ModulusOfElasticity As Double

    ''' <summary>Material moment of inertia (in^4)</summary>
    Public Property MomentOfInertia As Double

    ''' <summary>Recommended max span for this configuration (inches)</summary>
    Public Property RecommendedMaxSpan As Double

    ''' <summary>Warning message if applicable</summary>
    Public Property WarningMessage As String

End Class

''' <summary>
''' Material properties for structural calculations
''' </summary>
Public Class MaterialProperties

    ''' <summary>Material name</summary>
    Public Property Name As String

    ''' <summary>Modulus of Elasticity (E) in psi</summary>
    Public Property ModulusOfElasticity As Double

    ''' <summary>Density in pounds per cubic foot</summary>
    Public Property Density As Double

    ''' <summary>Typical grade/quality notes</summary>
    Public Property Notes As String

End Class
