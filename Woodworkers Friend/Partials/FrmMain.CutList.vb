' ============================================================================
' Last Updated: January 27, 2026
' Changes: Fixed syntax errors - removed C# operators, fixed string formatting
' ============================================================================

Partial Public Class FrmMain

#Region "Cut List Optimizer"

    Private _cutListItems As New List(Of CutListItem)
    Private _optimizationResult As CutListOptimizationResult
    Private _currentPattern As Integer = 0

    ''' <summary>
    ''' Initializes cut list optimizer
    ''' </summary>
    Private Sub InitializeCutListOptimizer()
        Try
            ' Setup DataGridView
            If DgvCutList IsNot Nothing Then
                DgvCutList.AllowUserToAddRows = True
                DgvCutList.AllowUserToDeleteRows = True
                DgvCutList.AutoGenerateColumns = False

                ' Add columns if not already present
                If DgvCutList.Columns.Count = 0 Then
                    DgvCutList.Columns.Add(New DataGridViewTextBoxColumn With {
                        .Name = "Label",
                        .HeaderText = "Label",
                        .Width = 120
                    })
                    DgvCutList.Columns.Add(New DataGridViewTextBoxColumn With {
                        .Name = "Length",
                        .HeaderText = "Length (in)",
                        .Width = 80
                    })
                    DgvCutList.Columns.Add(New DataGridViewTextBoxColumn With {
                        .Name = "Width",
                        .HeaderText = "Width (in)",
                        .Width = 80
                    })
                    DgvCutList.Columns.Add(New DataGridViewTextBoxColumn With {
                        .Name = "Quantity",
                        .HeaderText = "Qty",
                        .Width = 50
                    })
                End If
            End If

            ' Populate stock board dropdown
            If CmbStockBoard IsNot Nothing Then
                CmbStockBoard.Items.Clear()
                For Each board In CutListOptimizer.GetStandardBoards()
                    CmbStockBoard.Items.Add(board.ToString())
                Next
                If CmbStockBoard.Items.Count > 0 Then
                    CmbStockBoard.SelectedIndex = 0  ' Default to 4×8 sheet
                End If
            End If

            ' Set default kerf
            If TxtKerf IsNot Nothing Then TxtKerf.Text = "0.125"
        Catch ex As Exception
            ErrorHandler.LogError(ex, "InitializeCutListOptimizer")
        End Try
    End Sub

    ''' <summary>
    ''' Optimizes the cut list
    ''' </summary>
    Private Sub OptimizeCutList()
        Try
            ' Parse items from grid
            _cutListItems.Clear()
            Dim itemId = 1

            For Each row As DataGridViewRow In DgvCutList.Rows
                If row.IsNewRow Then Continue For

                ' Fixed: VB.NET doesn't support ?? operator - use If() instead
                Dim labelValue = row.Cells("Label").Value
                Dim label As String = If(labelValue IsNot Nothing, labelValue.ToString(), "Piece " & itemId.ToString())

                Dim lengthValue = row.Cells("Length").Value
                Dim lengthStr As String = If(lengthValue IsNot Nothing, lengthValue.ToString(), "")
                Dim length = InputValidator.TryParseDoubleWithDefault(lengthStr, 0)

                Dim widthValue = row.Cells("Width").Value
                Dim widthStr As String = If(widthValue IsNot Nothing, widthValue.ToString(), "")
                Dim width = InputValidator.TryParseDoubleWithDefault(widthStr, 0)

                Dim qtyValue = row.Cells("Quantity").Value
                Dim qtyStr As String = If(qtyValue IsNot Nothing, qtyValue.ToString(), "")
                Dim qty = InputValidator.TryParseIntegerInRange(qtyStr, 1, 999, 1)

                If length > 0 AndAlso width > 0 Then
                    _cutListItems.Add(New CutListItem With {
                        .Id = itemId,
                        .Label = label,
                        .Length = length,
                        .Width = width,
                        .Quantity = qty
                    })
                    itemId += 1
                End If
            Next

            If _cutListItems.Count = 0 Then
                MessageBox.Show("Please add at least one piece to the cut list",
                              "No Pieces", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Return
            End If

            ' Get selected stock board
            Dim boards = CutListOptimizer.GetStandardBoards()
            Dim selectedBoard = boards(Math.Max(0, CmbStockBoard.SelectedIndex))

            ' Get kerf width
            Dim kerf = InputValidator.TryParseDoubleWithDefault(TxtKerf.Text, 0.125)

            ' Run optimization
            Dim optimizer As New CutListOptimizer With {.KerfWidth = kerf}
            _optimizationResult = optimizer.Optimize(_cutListItems, selectedBoard)
            _currentPattern = 0

            ' Display results
            DisplayOptimizationResults()
            DrawCurrentPattern()
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "OptimizeCutList", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Displays optimization results summary
    ''' </summary>
    Private Sub DisplayOptimizationResults()
        If _optimizationResult Is Nothing Then Return

        Try
            ' Update summary labels - Fixed: Use String.Format instead of $""
            If LblBoardsNeeded IsNot Nothing Then
                LblBoardsNeeded.Text = String.Format("Boards Needed: {0}", _optimizationResult.TotalBoards)
            End If

            If LblTotalCost IsNot Nothing Then
                LblTotalCost.Text = String.Format("Total Cost: {0:C2}", _optimizationResult.TotalCost)
            End If

            If LblWastePercent IsNot Nothing Then
                LblWastePercent.Text = String.Format("Waste: {0:N1}%", _optimizationResult.WastePercentage)
            End If

            If LblAvgEfficiency IsNot Nothing Then
                LblAvgEfficiency.Text = String.Format("Efficiency: {0:N1}%", _optimizationResult.AverageEfficiency)
            End If

            ' Enable navigation if multiple boards
            If BtnPrevPattern IsNot Nothing Then
                BtnPrevPattern.Enabled = _optimizationResult.TotalBoards > 1
            End If
            If BtnNextPattern IsNot Nothing Then
                BtnNextPattern.Enabled = _optimizationResult.TotalBoards > 1
            End If

            ' Show unplaced items warning
            If _optimizationResult.UnplacedItems.Count > 0 Then
                Dim msg = String.Format("Warning: {0} piece(s) too large to fit on selected board stock!",
                                       _optimizationResult.UnplacedItems.Count)
                MessageBox.Show(msg, "Pieces Don't Fit", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DisplayOptimizationResults")
        End Try
    End Sub

    ''' <summary>
    ''' Draws the current cutting pattern
    ''' </summary>
    Private Sub DrawCurrentPattern()
        If PbCuttingDiagram Is Nothing OrElse _optimizationResult Is Nothing Then Return
        If _optimizationResult.Patterns.Count = 0 Then Return

        Try
            ' Get current pattern
            _currentPattern = Math.Max(0, Math.Min(_currentPattern, _optimizationResult.Patterns.Count - 1))
            Dim pattern = _optimizationResult.Patterns(_currentPattern)

            ' Create bitmap
            Dim bmp As New Bitmap(PbCuttingDiagram.Width, PbCuttingDiagram.Height)
            Using g As Graphics = Graphics.FromImage(bmp)
                g.Clear(Color.White)
                g.SmoothingMode = Drawing2D.SmoothingMode.AntiAlias

                ' Calculate scale to fit board
                Dim margin = 40
                Dim availWidth = PbCuttingDiagram.Width - (2 * margin)
                Dim availHeight = PbCuttingDiagram.Height - (2 * margin)
                Dim scale = Math.Min(availWidth / pattern.Board.Length, availHeight / pattern.Board.Width)

                ' Draw board outline
                Dim boardRect As New RectangleF(margin, margin,
                    CSng(pattern.Board.Length * scale), CSng(pattern.Board.Width * scale))
                g.FillRectangle(Brushes.Beige, boardRect)
                g.DrawRectangle(Pens.Black, Rectangle.Round(boardRect))

                ' Draw each piece
                Dim colorIndex = 0
                Dim colors() As Color = {Color.LightBlue, Color.LightGreen, Color.LightCoral,
                                        Color.LightGoldenrodYellow, Color.LightPink, Color.LightSteelBlue}

                For Each piece In pattern.Pieces
                    Dim pieceRect As New RectangleF(
                        CSng(margin + piece.X * scale),
                        CSng(margin + piece.Y * scale),
                        CSng(piece.ActualLength * scale),
                        CSng(piece.ActualWidth * scale))

                    ' Draw piece
                    Dim pieceColor = colors(colorIndex Mod colors.Length)
                    g.FillRectangle(New SolidBrush(pieceColor), pieceRect)
                    g.DrawRectangle(Pens.Black, Rectangle.Round(pieceRect))

                    ' Draw label
                    Dim labelFont As New Font("Arial", 7, FontStyle.Bold)
                    Dim label = $"{piece.Item.Label}{vbCrLf}{piece.Item.Length:N1}×{piece.Item.Width:N1}"
                    If piece.IsRotated Then label &= vbCrLf & "(R)"

                    Dim labelFormat As New StringFormat With {
                        .Alignment = StringAlignment.Center,
                        .LineAlignment = StringAlignment.Center
                    }
                    g.DrawString(label, labelFont, Brushes.Black, pieceRect, labelFormat)

                    colorIndex += 1
                Next

                ' Draw title
                g.DrawString($"Board {_currentPattern + 1} of {_optimizationResult.TotalBoards} - Efficiency: {pattern.Efficiency:N1}%",
                    New Font("Arial", 10, FontStyle.Bold), Brushes.Black, margin, 5)

            End Using

            PbCuttingDiagram.Image?.Dispose()
            PbCuttingDiagram.Image = bmp
        Catch ex As Exception
            ErrorHandler.LogError(ex, "DrawCurrentPattern")
        End Try
    End Sub

    ''' <summary>
    ''' Navigate to previous pattern
    ''' </summary>
    Private Sub BtnPrevPattern_Click(sender As Object, e As EventArgs) Handles BtnPrevPattern.Click
        If _optimizationResult IsNot Nothing AndAlso _currentPattern > 0 Then
            _currentPattern -= 1
            DrawCurrentPattern()
        End If
    End Sub

    ''' <summary>
    ''' Navigate to next pattern
    ''' </summary>
    Private Sub BtnNextPattern_Click(sender As Object, e As EventArgs) Handles BtnNextPattern.Click
        If _optimizationResult IsNot Nothing AndAlso
           _currentPattern < _optimizationResult.Patterns.Count - 1 Then
            _currentPattern += 1
            DrawCurrentPattern()
        End If
    End Sub

    ''' <summary>
    ''' Exports cut list to CSV
    ''' </summary>
    Private Sub ExportCutList()
        Try
            If _optimizationResult Is Nothing Then Return

            Dim sfd As New SaveFileDialog With {
                .Filter = "CSV Files (*.csv)|*.csv|All Files (*.*)|*.*",
                .DefaultExt = "csv",
                .FileName = "CutList.csv"
            }

            If sfd.ShowDialog() = DialogResult.OK Then
                Dim data As New Dictionary(Of String, String) From {
                    {"Total Boards", _optimizationResult.TotalBoards.ToString()},
                    {"Total Cost", _optimizationResult.TotalCost.ToString("C2")},
                    {"Waste Percentage", $"{_optimizationResult.WastePercentage:N1}%"},
                    {"Average Efficiency", $"{_optimizationResult.AverageEfficiency:N1}%"}
                }

                ReportExporter.ExportToCsv(data, sfd.FileName)
                MessageBox.Show("Cut list exported successfully!", "Export Complete",
                              MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "ExportCutList", showToUser:=True)
        End Try
    End Sub

    Private Sub BtnOptimize_Click(sender As Object, e As EventArgs) Handles BtnOptimize.Click
        OptimizeCutList()
    End Sub

#End Region

End Class
