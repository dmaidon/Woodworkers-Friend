' ============================================================================
' Last Updated: January 30, 2026
' Changes: Initial creation - Calculation history data models (Phase 6)
' ============================================================================

Imports System.Text.Json

''' <summary>
''' Represents a saved calculation from any calculator
''' </summary>
Public Class CalculationHistory
    Public Property HistoryID As Integer
    Public Property CalculatorType As String
    Public Property CalculationName As String
    Public Property InputParameters As String ' JSON
    Public Property Results As String ' JSON
    Public Property DateCalculated As DateTime
    Public Property IsFavorite As Boolean
    Public Property Notes As String

    ''' <summary>
    ''' Deserializes input parameters from JSON
    ''' </summary>
    Public Function GetInputs(Of T)() As T
        Try
            Return JsonSerializer.Deserialize(Of T)(InputParameters)
        Catch
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Deserializes results from JSON
    ''' </summary>
    Public Function GetResults(Of T)() As T
        Try
            Return JsonSerializer.Deserialize(Of T)(Results)
        Catch
            Return Nothing
        End Try
    End Function
End Class

''' <summary>
''' Calculator type constants
''' </summary>
Public Class CalculatorTypes
    Public Const BoardFeet As String = "BoardFeet"
    Public Const ShelfSag As String = "ShelfSag"
    Public Const WoodMovement As String = "WoodMovement"
    Public Const EpoxyPour As String = "EpoxyPour"
    Public Const DrawerCalculator As String = "DrawerCalculator"
    Public Const DoorCalculator As String = "DoorCalculator"
    Public Const JoineryCalculator As String = "JoineryCalculator"
    Public Const CutListOptimizer As String = "CutListOptimizer"
End Class

''' <summary>
''' Example input/result classes for Board Feet calculator
''' </summary>
Public Class BoardFeetInputs
    Public Property Thickness As Double
    Public Property Width As Double
    Public Property Length As Double
    Public Property Quantity As Integer
End Class

Public Class BoardFeetResults
    Public Property BoardFeet As Double
    Public Property CubicInches As Double
    Public Property CubicFeet As Double
End Class

''' <summary>
''' Example input/result classes for Shelf Sag calculator
''' </summary>
Public Class ShelfSagInputs
    Public Property Length As Double
    Public Property Width As Double
    Public Property Thickness As Double
    Public Property LoadType As String
    Public Property LoadAmount As Double
    Public Property WoodSpecies As String
End Class

Public Class ShelfSagResults
    Public Property Sag As Double
    Public Property MaxSafeLoad As Double
    Public Property Recommendation As String
End Class

''' <summary>
''' Example input/result classes for Wood Movement calculator
''' </summary>
Public Class WoodMovementInputs
    Public Property WoodSpecies As String
    Public Property Width As Double
    Public Property InitialMoisture As Double
    Public Property FinalMoisture As Double
    Public Property Orientation As String
End Class

Public Class WoodMovementResults
    Public Property Movement As Double
    Public Property FinalWidth As Double
    Public Property MovementPercent As Double
End Class
