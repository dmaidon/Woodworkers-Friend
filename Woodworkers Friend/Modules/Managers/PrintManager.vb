Imports System.Drawing.Printing

''' <summary>
''' Manages all printing functionality for calculation results
''' </summary>
Public Class PrintManager
    Implements IDisposable

#Region "Private Fields"

    Private printDocument As PrintDocument
    Private printContent As String
    Private printFont As Font
    Private printHeaderFont As Font
    Private printTitleFont As Font
    Private currentPrintPage As Integer = 0
    Private totalPrintPages As Integer = 0
    Private printLines() As String
    Private linesPerPage As Integer = 0
    Private _disposed As Boolean = False

#End Region

#Region "Public Methods"

    ''' <summary>
    ''' Constructor
    ''' </summary>
    Public Sub New()
        InitializePrintComponents()
    End Sub

    ''' <summary>
    ''' Shows print dialog with preview option
    ''' </summary>
    Public Sub ShowPrintDialog(content As String)
        Try
            If String.IsNullOrWhiteSpace(content) Then
                MessageBox.Show("No content to print.",
                               "Print Error",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information)
                Return
            End If

            ' Set content
            printContent = content

            ' Ask user for print options
            Dim result As DialogResult = MessageBox.Show(
                "Would you like to preview before printing?" & vbCrLf & vbCrLf &
                "Yes = Print Preview" & vbCrLf &
                "No = Print directly" & vbCrLf &
                "Cancel = Cancel printing",
                "Print Options",
                MessageBoxButtons.YesNoCancel,
                MessageBoxIcon.Question)

            Select Case result
                Case DialogResult.Yes
                    ShowPrintPreview()

                Case DialogResult.No
                    ShowPrinterDialog()

                Case DialogResult.Cancel
                    Return
            End Select
        Catch ex As Exception
            MessageBox.Show($"Print dialog error:{vbCrLf}{vbCrLf}{ex.Message}",
                           "Print Error",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Shows print preview dialog
    ''' </summary>
    Public Sub ShowPrintPreview()
        Try
            Using previewDialog As New PrintPreviewDialog()
                previewDialog.Document = printDocument
                previewDialog.Width = 800
                previewDialog.Height = 600
                previewDialog.UseAntiAlias = True
                previewDialog.WindowState = FormWindowState.Maximized
                previewDialog.Text = "Print Preview - Woodworkers Calculator"

                ' Set icon if available
                Try
                    ' previewDialog.Icon = My.Resources.appIcon
                Catch
                    ' Ignore if icon not available
                End Try

                previewDialog.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show($"Print preview error:{vbCrLf}{vbCrLf}{ex.Message}",
                           "Print Preview Error",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Shows printer selection dialog and prints
    ''' </summary>
    Public Sub ShowPrinterDialog()
        Try
            Using printDialog As New PrintDialog()
                printDialog.Document = printDocument
                printDialog.UseEXDialog = True
                printDialog.AllowPrintToFile = True
                printDialog.AllowSelection = False
                printDialog.AllowSomePages = True
                printDialog.PrinterSettings = printDocument.PrinterSettings

                If printDialog.ShowDialog() = DialogResult.OK Then
                    Print()
                    MessageBox.Show("Print job sent to printer successfully.",
                                   "Print Complete",
                                   MessageBoxButtons.OK,
                                   MessageBoxIcon.Information)
                End If
            End Using
        Catch ex As Exception
            MessageBox.Show($"Printer dialog error:{vbCrLf}{vbCrLf}{ex.Message}",
                           "Print Error",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Prints the document directly
    ''' </summary>
    Public Sub Print()
        Try
            printDocument?.Print()
        Catch ex As Exception
            MessageBox.Show($"Printing failed:{vbCrLf}{vbCrLf}{ex.Message}",
                           "Print Error",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error)
        End Try
    End Sub

    ''' <summary>
    ''' Configures print settings
    ''' </summary>
    Public Sub ConfigurePrintSettings(Optional documentName As String = "Woodworkers Calculator Results",
                                      Optional orientation As Orientation = Orientation.Portrait,
                                      Optional margins As Margins = Nothing)
        If printDocument IsNot Nothing Then
            printDocument.DocumentName = documentName

            If margins IsNot Nothing Then
                printDocument.DefaultPageSettings.Margins = margins
            Else
                ' Set default margins (1 inch on all sides)
                printDocument.DefaultPageSettings.Margins = New Margins(100, 100, 100, 100)
            End If

            printDocument.DefaultPageSettings.Landscape = orientation = Orientation.Landscape
        End If
    End Sub

    ''' <summary>
    ''' Gets available printers
    ''' </summary>
    Public Shared Function GetAvailablePrinters() As List(Of String)
        Dim printers As New List(Of String)()

        Try
            For Each printerName As String In PrinterSettings.InstalledPrinters
                printers.Add(printerName)
            Next
        Catch ex As Exception
            ' Return empty list if can't enumerate printers
        End Try

        Return printers
    End Function

    ''' <summary>
    ''' Gets default printer name
    ''' </summary>
    Public Shared Function GetDefaultPrinter() As String
        Try
            Dim settings As New PrinterSettings()
            Return settings.PrinterName
        Catch
            Return "No default printer"
        End Try
    End Function

#End Region

#Region "Private Methods"

    Private Sub InitializePrintComponents()
        ' Create print document
        printDocument = New PrintDocument()

        ' Set up fonts
        printTitleFont = New Font("Arial", 16, FontStyle.Bold)
        printHeaderFont = New Font("Arial", 12, FontStyle.Bold)
        printFont = New Font("Arial", 10, FontStyle.Regular)

        ' Set default document properties
        ConfigurePrintSettings()

        ' Add event handlers
        AddHandler printDocument.PrintPage, AddressOf PrintDocument_PrintPage
        AddHandler printDocument.BeginPrint, AddressOf PrintDocument_BeginPrint
        AddHandler printDocument.EndPrint, AddressOf PrintDocument_EndPrint
    End Sub

    Private Sub PrintDocument_BeginPrint(sender As Object, e As PrintEventArgs)
        Try
            currentPrintPage = 0
            printLines = printContent.Split({vbCrLf, vbLf}, StringSplitOptions.None)

            ' Calculate lines per page
            Dim pageHeight As Integer = CInt(printDocument.DefaultPageSettings.PrintableArea.Height)
            Dim marginTop As Integer = printDocument.DefaultPageSettings.Margins.Top
            Dim marginBottom As Integer = printDocument.DefaultPageSettings.Margins.Bottom
            Dim availableHeight As Integer = pageHeight - marginTop - marginBottom

            ' Reserve space for header and footer
            availableHeight -= CInt(printTitleFont.Height * 3) ' Header space
            availableHeight -= CInt(printFont.Height * 2) ' Footer space

            linesPerPage = CInt(availableHeight / printFont.Height) - 2 ' Small buffer
            totalPrintPages = CInt(Math.Ceiling(printLines.Length / CDbl(linesPerPage)))

            ' Ensure at least 1 page
            If totalPrintPages = 0 Then totalPrintPages = 1
        Catch ex As Exception
            ' Set defaults if calculation fails
            linesPerPage = 50
            totalPrintPages = CInt(Math.Ceiling(printLines.Length / 50.0))
        End Try
    End Sub

    Private Sub PrintDocument_PrintPage(sender As Object, e As PrintPageEventArgs)
        Try
            Dim yPosition As Single = e.MarginBounds.Top
            Dim leftMargin As Single = e.MarginBounds.Left
            Dim rightMargin As Single = e.MarginBounds.Right
            Dim bottomMargin As Single = e.MarginBounds.Bottom

            ' Calculate line range for this page
            Dim startLine As Integer = currentPrintPage * linesPerPage
            Dim endLine As Integer = Math.Min(startLine + linesPerPage, printLines.Length)

            ' Print header
            yPosition = PrintPageHeader(e.Graphics, leftMargin, yPosition, rightMargin)

            ' Print content lines
            For lineIndex As Integer = startLine To endLine - 1
                If yPosition >= bottomMargin - (printFont.Height * 3) Then
                    Exit For ' Leave space for footer
                End If

                Dim line As String = If(printLines(lineIndex), "")
                yPosition = PrintLine(e.Graphics, line, leftMargin, yPosition, rightMargin)
            Next

            ' Print footer
            PrintPageFooter(e.Graphics, leftMargin, bottomMargin, rightMargin)

            ' Check if more pages needed
            currentPrintPage += 1
            e.HasMorePages = currentPrintPage * linesPerPage < printLines.Length
        Catch ex As Exception
            ' Stop printing on error
            e.HasMorePages = False
            MessageBox.Show($"Error during printing:{vbCrLf}{ex.Message}",
                           "Print Error",
                           MessageBoxButtons.OK,
                           MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub PrintDocument_EndPrint(sender As Object, e As PrintEventArgs)
        ' Reset state
        currentPrintPage = 0
        totalPrintPages = 0
        printLines = Nothing
    End Sub

    Private Function PrintPageHeader(graphics As Graphics, leftMargin As Single, yPosition As Single, rightMargin As Single) As Single
        If currentPrintPage = 0 Then
            ' Title on first page
            graphics.DrawString("WOODWORKERS CALCULATOR", printTitleFont, Brushes.Black, leftMargin, yPosition)
            yPosition += printTitleFont.Height + 5
            graphics.DrawString("Calculation Results", printHeaderFont, Brushes.Black, leftMargin, yPosition)
            yPosition += printHeaderFont.Height + 15
        Else
            ' Header on subsequent pages
            graphics.DrawString($"Woodworkers Calculator Results (Page {currentPrintPage + 1})",
                               printHeaderFont, Brushes.Black, leftMargin, yPosition)
            yPosition += printHeaderFont.Height + 10
        End If

        ' Draw separator line
        graphics.DrawLine(Pens.Black, leftMargin, yPosition, rightMargin, yPosition)
        yPosition += 10

        Return yPosition
    End Function

    Private Function PrintLine(graphics As Graphics, line As String, leftMargin As Single, yPosition As Single, rightMargin As Single) As Single
        If String.IsNullOrEmpty(line) Then
            ' Empty line
            Return yPosition + printFont.Height
        End If

        ' Determine font based on line content
        Dim currentFont As Font = printFont
        If line.StartsWith("WOODWORKERS CALCULATOR") OrElse line.StartsWith("CALCULATION RESULTS") Then
            currentFont = printTitleFont
        ElseIf line.EndsWith(":"c) OrElse line.Contains("===") OrElse line.Contains("---") OrElse
               String.Equals(line, line.ToUpper(), StringComparison.Ordinal) AndAlso line.Length > 10 Then
            currentFont = printHeaderFont
        End If

        ' Handle word wrapping for long lines
        Dim availableWidth As Single = rightMargin - leftMargin
        If graphics.MeasureString(line, currentFont).Width > availableWidth Then
            Return PrintWrappedLine(graphics, line, currentFont, leftMargin, yPosition, availableWidth)
        Else
            graphics.DrawString(line, currentFont, Brushes.Black, leftMargin, yPosition)
            Return yPosition + currentFont.Height
        End If
    End Function

    Private Shared Function PrintWrappedLine(graphics As Graphics, line As String, font As Font, leftMargin As Single, yPosition As Single, availableWidth As Single) As Single
        ArgumentNullException.ThrowIfNull(graphics)
        ArgumentException.ThrowIfNullOrEmpty(line)
        ArgumentNullException.ThrowIfNull(font)
        Dim words() As String = line.Split(" "c)
        Dim currentLine As String = ""

        For Each word As String In words
            Dim testLine As String = If(currentLine = "", word, currentLine & " " & word)

            If graphics.MeasureString(testLine, font).Width <= availableWidth Then
                currentLine = testLine
            Else
                If currentLine <> "" Then
                    graphics.DrawString(currentLine, font, Brushes.Black, leftMargin, yPosition)
                    yPosition += font.Height
                End If
                currentLine = word
            End If
        Next

        If currentLine <> "" Then
            graphics.DrawString(currentLine, font, Brushes.Black, leftMargin, yPosition)
            yPosition += font.Height
        End If

        Return yPosition
    End Function

    Private Sub PrintPageFooter(graphics As Graphics, leftMargin As Single, bottomMargin As Single, rightMargin As Single)
        ArgumentNullException.ThrowIfNull(graphics)
        Dim footerY As Single = bottomMargin - printFont.Height

        ' Page number
        Dim pageText As String = $"Page {currentPrintPage + 1} of {totalPrintPages}"
        Dim pageSize As SizeF = graphics.MeasureString(pageText, printFont)
        graphics.DrawString(pageText, printFont, Brushes.Gray, rightMargin - pageSize.Width, footerY)

        ' Date/time
        Dim dateText As String = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
        graphics.DrawString(dateText, printFont, Brushes.Gray, leftMargin, footerY)

        ' Draw separator line above footer
        graphics.DrawLine(Pens.LightGray, leftMargin, footerY - 5, rightMargin, footerY - 5)
    End Sub

#End Region

#Region "IDisposable Implementation"

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not _disposed Then
            If disposing Then
                ' Dispose managed resources
                printDocument?.Dispose()
                printTitleFont?.Dispose()
                printHeaderFont?.Dispose()
                printFont?.Dispose()
            End If

            ' Set objects to Nothing
            printDocument = Nothing
            printTitleFont = Nothing
            printHeaderFont = Nothing
            printFont = Nothing
            printLines = Nothing

            _disposed = True
        End If
    End Sub

    Protected Overrides Sub Finalize()
        Dispose(False)
        MyBase.Finalize()
    End Sub

#End Region

End Class

''' <summary>
''' Print orientation enumeration
''' </summary>
Public Enum Orientation
    Portrait
    Landscape
End Enum