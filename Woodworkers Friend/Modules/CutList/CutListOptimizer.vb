' ============================================================================
' Last Updated: January 27, 2026
' Changes: Initial creation - Cut list optimization using First Fit Decreasing
'          algorithm with rotation support
' ============================================================================

''' <summary>
''' Optimizes cutting patterns to minimize waste
''' </summary>
Public Class CutListOptimizer

    Private _kerfWidth As Double = 0.125  ' Default 1/8" kerf

    Public Property KerfWidth As Double
        Get
            Return _kerfWidth
        End Get
        Set(value As Double)
            _kerfWidth = value
        End Set
    End Property

    ''' <summary>
    ''' Optimizes a cut list using First Fit Decreasing algorithm
    ''' </summary>
    Public Function Optimize(items As List(Of CutListItem),
                            stockBoard As BoardStock) As CutListOptimizationResult

        Dim result As New CutListOptimizationResult()

        ' Expand items by quantity
        Dim allPieces As New List(Of CutListItem)
        For Each item In items
            For i As Integer = 1 To item.Quantity
                allPieces.Add(New CutListItem With {
                    .Length = item.Length,
                    .Width = item.Width,
                    .Label = item.Label,
                    .MaterialType = item.MaterialType,
                    .Quantity = 1,
                    .Id = item.Id
                })
            Next
        Next

        ' Sort by area (largest first)
        allPieces = allPieces.OrderByDescending(Function(p) p.Area).ToList()

        ' Place pieces
        Dim currentPattern As CuttingPattern = Nothing

        For Each piece In allPieces
            Dim placed As Boolean = False

            ' Try to place on existing boards
            For Each pattern In result.Patterns
                If TryPlacePiece(piece, pattern, stockBoard) Then
                    placed = True
                    Exit For
                End If
            Next

            ' Need new board
            If Not placed Then
                currentPattern = New CuttingPattern With {
                    .Board = stockBoard,
                    .BoardNumber = result.Patterns.Count + 1
                }
                result.Patterns.Add(currentPattern)

                If Not TryPlacePiece(piece, currentPattern, stockBoard) Then
                    ' Piece doesn't fit even on empty board
                    result.UnplacedItems.Add(piece)
                End If
            End If
        Next

        ' Calculate totals
        result.TotalBoards = result.Patterns.Count
        result.TotalCost = result.TotalBoards * stockBoard.Cost
        result.TotalWaste = result.Patterns.Sum(Function(p) p.WastedArea)
        If result.Patterns.Count > 0 Then
            result.AverageEfficiency = result.Patterns.Average(Function(p) p.Efficiency)
        End If

        Return result
    End Function

    ''' <summary>
    ''' Tries to place a piece on a cutting pattern
    ''' </summary>
    Private Function TryPlacePiece(piece As CutListItem,
                                   pattern As CuttingPattern,
                                   board As BoardStock) As Boolean

        ' Try both orientations
        Dim orientations = {False, True}  ' normal, rotated

        For Each rotated In orientations
            Dim pieceLength = If(rotated, piece.Width, piece.Length)
            Dim pieceWidth = If(rotated, piece.Length, piece.Width)

            ' Account for kerf
            Dim actualLength = pieceLength + _kerfWidth
            Dim actualWidth = pieceWidth + _kerfWidth

            ' Check if piece fits on board at all
            If actualLength > board.Length OrElse actualWidth > board.Width Then
                Continue For
            End If

            ' Try to find a position using simple shelf algorithm
            Dim position = FindPosition(pattern, actualLength, actualWidth, board)

            If position.Found Then
                pattern.Pieces.Add(New PlacedPiece With {
                    .Item = piece,
                    .X = position.X,
                    .Y = position.Y,
                    .IsRotated = rotated
                })
                Return True
            End If
        Next

        Return False
    End Function

    ''' <summary>
    ''' Finds a position for a piece using simple shelf packing
    ''' </summary>
    Private Function FindPosition(pattern As CuttingPattern,
                                  length As Double,
                                  width As Double,
                                  board As BoardStock) As (Found As Boolean, X As Double, Y As Double)

        If pattern.Pieces.Count = 0 Then
            ' First piece - place at origin
            Return (True, 0, 0)
        End If

        ' Simple shelf algorithm: try rows
        Dim currentY As Double = 0

        While currentY + width <= board.Width
            Dim currentX As Double = 0
            Dim rowHeight As Double = 0

            ' Find pieces in this row - Fixed: Multi-line lambda must end with End Function
            Dim rowPieces = pattern.Pieces.Where(Function(p) Math.Abs(p.Y - currentY) < 0.01).OrderBy(Function(p) p.X).ToList()

            If rowPieces.Count > 0 Then
                ' Try to fit after last piece in row
                Dim lastPiece = rowPieces.Last()
                currentX = lastPiece.X + lastPiece.ActualLength + _kerfWidth
                rowHeight = rowPieces.Max(Function(p) p.ActualWidth)

                If currentX + length <= board.Length Then
                    Return (True, currentX, currentY)
                End If

                ' Move to next row
                currentY += rowHeight + _kerfWidth
            Else
                ' Empty row
                Return (True, 0, currentY)
            End If
        End While

        Return (False, 0, 0)
    End Function

    ''' <summary>
    ''' Gets standard board sizes
    ''' </summary>
    Public Shared Function GetStandardBoards() As List(Of BoardStock)
        Return New List(Of BoardStock) From {
            New BoardStock With {.Name = "4×8 Sheet", .Length = 96, .Width = 48, .Cost = 50, .MaterialType = "Plywood"},
            New BoardStock With {.Name = "4×4 Sheet", .Length = 48, .Width = 48, .Cost = 30, .MaterialType = "Plywood"},
            New BoardStock With {.Name = "2×8 Sheet", .Length = 96, .Width = 24, .Cost = 28, .MaterialType = "Plywood"},
            New BoardStock With {.Name = "8ft 1×12", .Length = 96, .Width = 11.25, .Cost = 25, .MaterialType = "Hardwood"},
            New BoardStock With {.Name = "8ft 1×8", .Length = 96, .Width = 7.25, .Cost = 18, .MaterialType = "Hardwood"},
            New BoardStock With {.Name = "8ft 1×6", .Length = 96, .Width = 5.5, .Cost = 14, .MaterialType = "Hardwood"},
            New BoardStock With {.Name = "6ft 1×12", .Length = 72, .Width = 11.25, .Cost = 20, .MaterialType = "Hardwood"},
            New BoardStock With {.Name = "6ft 1×8", .Length = 72, .Width = 7.25, .Cost = 14, .MaterialType = "Hardwood"}
        }
    End Function

End Class
