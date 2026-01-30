''' <summary>
''' Cost data models for wood and epoxy
''' Phase 7.3 - CSV Migration to Database
''' </summary>
Public Module CostDataModels

    ''' <summary>
    ''' Represents a wood cost entry (thickness, species, price)
    ''' </summary>
    Public Class WoodCost
        Public Property WoodCostID As Integer
        Public Property Thickness As String
        Public Property WoodName As String
        Public Property CostPerBoardFoot As Double
        Public Property Active As Boolean
        Public Property IsUserAdded As Boolean
        Public Property DateAdded As DateTime
        Public Property LastModified As DateTime

        ''' <summary>
        ''' Display name for combo boxes: "4/4" CHERRY - $5.60"
        ''' </summary>
        Public ReadOnly Property DisplayName As String
            Get
                Return $"{Thickness} {WoodName} - ${CostPerBoardFoot:F2}/bf"
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return DisplayName
        End Function
    End Class

    ''' <summary>
    ''' Represents an epoxy cost entry (brand, type, price)
    ''' </summary>
    Public Class EpoxyCost
        Public Property EpoxyCostID As Integer
        Public Property Brand As String
        Public Property Type As String
        Public Property CostPerGallon As Double
        Public Property DisplayText As String
        Public Property Active As Boolean
        Public Property IsUserAdded As Boolean
        Public Property DateAdded As DateTime
        Public Property LastModified As DateTime

        ''' <summary>
        ''' Display name for combo boxes: "TotalBoat Table Top - $59.99/gal"
        ''' </summary>
        Public ReadOnly Property DisplayName As String
            Get
                If Not String.IsNullOrEmpty(DisplayText) Then
                    Return DisplayText
                Else
                    Return $"{Brand} {Type} - ${CostPerGallon:F2}/gal"
                End If
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return DisplayName
        End Function
    End Class

End Module
