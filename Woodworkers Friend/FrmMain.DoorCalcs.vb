Partial Public Class FrmMain

    Public Structure DoorCalculationInput
        Public CabinetWidth As Double
        Public CabinetHeight As Double
        Public StileWidth As Double
        Public RailWidth As Double
        Public OverlayAmount As Double
        Public GrooveDepth As Double
        Public ExpansionGap As Double
        Public DoorGap As Double
        Public IsDualDoor As Boolean
        Public IsInsetDoor As Boolean
        Public MeasurementScale As String ' "Imperial" or "Metric"
    End Structure

    ''' <summary>
    ''' Represents the complete calculation results
    ''' </summary>
    Public Structure DoorCalculationResult
        Public PanelHeight As Double
        Public PanelWidth As Double
        Public RailLength As Double
        Public StileLength As Double
        Public BoardFeet As Double
        Public TotalMaterialArea As Double
        Public DoorWidth As Double
        Public DoorHeight As Double
        Public IsValid As Boolean
        Public ErrorMessage As String
        Public Warnings As List(Of String)
    End Structure

End Class