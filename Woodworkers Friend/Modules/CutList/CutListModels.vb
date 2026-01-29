' ============================================================================
' Last Updated: January 27, 2026
' Changes: Initial creation - Cut list data structures for piece tracking
'          and board stock management
' ============================================================================

''' <summary>
''' Represents a piece to be cut from stock
''' </summary>
Public Class CutListItem
    Public Property Length As Double
    Public Property Width As Double
    Public Property Quantity As Integer
    Public Property Label As String
    Public Property MaterialType As String
    Public Property Id As Integer
    
    Public Sub New()
        MaterialType = "Plywood"
        Quantity = 1
        Label = ""
    End Sub
    
    Public ReadOnly Property Area As Double
        Get
            Return Length * Width
        End Get
    End Property
    
    Public Overrides Function ToString() As String
        Return $"{Label}: {Length}""×{Width}"" ({Quantity})"
    End Function
End Class

''' <summary>
''' Represents a board or sheet of stock material
''' </summary>
Public Class BoardStock
    Public Property Length As Double
    Public Property Width As Double
    Public Property Cost As Decimal
    Public Property MaterialType As String
    Public Property Name As String
    
    Public Sub New()
        MaterialType = "Plywood"
        Name = "4×8 Sheet"
        Length = 96
        Width = 48
        Cost = 50
    End Sub
    
    Public ReadOnly Property Area As Double
        Get
            Return Length * Width
        End Get
    End Property
    
    Public Overrides Function ToString() As String
        Return $"{Name} ({Length}""×{Width}"") - {Cost:C}"
    End Function
End Class

''' <summary>
''' Represents a placed piece on a board
''' </summary>
Public Class PlacedPiece
    Public Property Item As CutListItem
    Public Property X As Double
    Public Property Y As Double
    Public Property IsRotated As Boolean
    
    Public ReadOnly Property ActualLength As Double
        Get
            Return If(IsRotated, Item.Width, Item.Length)
        End Get
    End Property
    
    Public ReadOnly Property ActualWidth As Double
        Get
            Return If(IsRotated, Item.Length, Item.Width)
        End Get
    End Property
End Class

''' <summary>
''' Represents a cutting pattern on one board
''' </summary>
Public Class CuttingPattern
    Public Property Board As BoardStock
    Public Property Pieces As New List(Of PlacedPiece)
    Public Property BoardNumber As Integer
    
    Public ReadOnly Property UsedArea As Double
        Get
            Return Pieces.Sum(Function(p) p.Item.Area)
        End Get
    End Property
    
    Public ReadOnly Property WastedArea As Double
        Get
            Return Board.Area - UsedArea
        End Get
    End Property
    
    Public ReadOnly Property Efficiency As Double
        Get
            If Board.Area = 0 Then Return 0
            Return (UsedArea / Board.Area) * 100
        End Get
    End Property
End Class

''' <summary>
''' Result of cut list optimization
''' </summary>
Public Class CutListOptimizationResult
    Public Property Patterns As New List(Of CuttingPattern)
    Public Property UnplacedItems As New List(Of CutListItem)
    Public Property TotalBoards As Integer
    Public Property TotalCost As Decimal
    Public Property TotalWaste As Double
    Public Property AverageEfficiency As Double
    
    Public ReadOnly Property WastePercentage As Double
        Get
            Dim totalArea = Patterns.Sum(Function(p) p.Board.Area)
            If totalArea = 0 Then Return 0
            Return (TotalWaste / totalArea) * 100
        End Get
    End Property
End Class
