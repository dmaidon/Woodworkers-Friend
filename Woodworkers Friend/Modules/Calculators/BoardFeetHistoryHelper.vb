' ============================================================================
' Last Updated: January 30, 2026
' Changes: Phase 6 - Board Feet calculation history helper
' ============================================================================

Imports System.Text.Json

''' <summary>
''' Helper class for Board Feet calculator history
''' </summary>
Public Class BoardFeetHistoryHelper

    ''' <summary>
    ''' Saves current board feet calculation to history
    ''' </summary>
    Public Shared Function SaveCalculation(thickness As Double, width As Double, length As Double, quantity As Integer,
                                          boardFeet As Double, cubicInches As Double, cubicFeet As Double,
                                          Optional name As String = "") As Boolean
        Try
            ' Create inputs JSON
            Dim inputs As New BoardFeetInputs With {
                .Thickness = thickness,
                .Width = width,
                .Length = length,
                .Quantity = quantity
            }

            ' Create results JSON
            Dim results As New BoardFeetResults With {
                .BoardFeet = boardFeet,
                .CubicInches = cubicInches,
                .CubicFeet = cubicFeet
            }

            ' Serialize to JSON
            Dim inputsJson = JsonSerializer.Serialize(inputs)
            Dim resultsJson = JsonSerializer.Serialize(results)

            ' Save to database
            Dim historyId = DatabaseManager.Instance.SaveCalculation(
                CalculatorTypes.BoardFeet,
                name,
                inputsJson,
                resultsJson
            )

            Return historyId > 0

        Catch ex As Exception
            ErrorHandler.LogError(ex, "BoardFeetHistoryHelper.SaveCalculation")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Loads a board feet calculation from history
    ''' </summary>
    Public Shared Function LoadCalculation(history As CalculationHistory, ByRef thickness As Double, ByRef width As Double,
                                          ByRef length As Double, ByRef quantity As Integer) As Boolean
        Try
            ' Deserialize inputs
            Dim inputs = history.GetInputs(Of BoardFeetInputs)()
            If inputs Is Nothing Then Return False

            ' Set output values
            thickness = inputs.Thickness
            width = inputs.Width
            length = inputs.Length
            quantity = inputs.Quantity

            Return True

        Catch ex As Exception
            ErrorHandler.LogError(ex, "BoardFeetHistoryHelper.LoadCalculation")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Shows history dialog and returns selected calculation
    ''' </summary>
    Public Shared Function ShowHistoryDialog() As CalculationHistory
        Try
            Using dlg As New FrmCalculationHistory(CalculatorTypes.BoardFeet)
                If dlg.ShowDialog() = DialogResult.OK Then
                    Return dlg.SelectedHistory
                End If
            End Using
        Catch ex As Exception
            ErrorHandler.LogError(ex, "BoardFeetHistoryHelper.ShowHistoryDialog")
        End Try

        Return Nothing
    End Function

End Class
