Imports System.Text

''' <summary>
''' Parameters for door calculations
''' </summary>
Public Class DoorCalculationParameters
    Public Property CabinetOpeningHeight As Double
    Public Property CabinetOpeningWidth As Double
    Public Property StileWidth As Double
    Public Property RailWidth As Double
    Public Property IsTwoDoor As Boolean ' True for 2-door, False for 1-door
    Public Property GapSize As Double ' Gap between doors (for 2-door config)
    Public Property DoorOverlay As Double ' Amount door overlays cabinet opening
    Public Property IsOverlay As Boolean ' True for overlay doors, False for inset
    Public Property PanelGrooveDepth As Double
    Public Property PanelExpansionGap As Double
    Public Property Scale As MeasurementScale
End Class

''' <summary>
''' Enhanced results of door calculations
''' </summary>
Public Class DoorCalculationResult
    Public Property IsValid As Boolean = True
    Public Property ErrorMessage As String = ""
    Public Property RailLength As Double
    Public Property StileLength As Double
    Public Property PanelHeight As Double
    Public Property PanelWidth As Double
    Public Property DoorWidth As Double
    Public Property DoorHeight As Double
    Public Property NumberOfDoors As Integer
    Public Property Unit As String
    Public Property Details As String

    ' Enhanced calculations
    Public Property TotalMaterialNeeded As Double

    Public Property AreaUnit As String
    Public Property CutList As List(Of DoorCutListItem)
    Public Property HingePositions As List(Of Double)
    Public Property HandlePosition As DoorHandlePosition
    Public Property WeightEstimate As Double
End Class

''' <summary>
''' Cut list item for door material requirements
''' </summary>
Public Class DoorCutListItem
    Public Property ComponentName As String
    Public Property Quantity As Integer
    Public Property Length As Double
    Public Property Width As Double
    Public Property Thickness As Double
    Public Property MaterialType As String
End Class

''' <summary>
''' Door handle position calculation
''' </summary>
Public Class DoorHandlePosition
    Public Property FromTopRail As Double
    Public Property FromStileEdge As Double
    Public Property CenterlineHeight As Double
End Class

''' <summary>
''' Door project for saving/loading
''' </summary>
Public Class DoorProject
    Public Property ProjectId As Guid = Guid.NewGuid() ' Add GUID support
    Public Property ProjectName As String
    Public Property CabinetOpeningHeight As Double
    Public Property CabinetOpeningWidth As Double
    Public Property StileWidth As Double
    Public Property RailWidth As Double
    Public Property IsTwoDoor As Boolean
    Public Property GapSize As Double
    Public Property DoorOverlay As Double
    Public Property IsOverlay As Boolean
    Public Property PanelGrooveDepth As Double
    Public Property PanelExpansionGap As Double
    Public Property Scale As MeasurementScale
    Public Property SavedDate As DateTime
    Public Property Notes As String
End Class