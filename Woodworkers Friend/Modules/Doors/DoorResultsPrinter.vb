Imports System.Drawing
Imports System.Drawing.Printing
Imports System.Windows.Forms

Namespace WwFriend.Modules.Doors

    Public Module DoorResultsPrinter

        Public Sub Print(owner As IWin32Window, rtb As RichTextBox, statusSink As Action(Of String, Color))
            If rtb Is Nothing OrElse String.IsNullOrWhiteSpace(rtb.Text) Then
                MessageBox.Show(owner, "No calculation results to print. Please calculate door dimensions first.", "Print Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Using printDoc As New DoorPrintDocument(rtb.Text)
                printDoc.DocumentName = "Door Calculation Results"

                Using dlg As New PrintDialog()
                    dlg.Document = printDoc
                    If dlg.ShowDialog(owner) = DialogResult.OK Then
                        printDoc.Print()
                        statusSink?.Invoke("Results sent to printer", Color.Green)
                    End If
                End Using
            End Using
        End Sub

        Private NotInheritable Class DoorPrintDocument
            Inherits PrintDocument

            Private ReadOnly _content As String
            Private _lineIndex As Integer
            Private ReadOnly _lines As String()

            Public Sub New(content As String)
                _content = If(content, String.Empty)
                _lines = _content.Split({vbCrLf, vbLf}, StringSplitOptions.None)
                _lineIndex = 0
            End Sub

            Protected Overrides Sub OnPrintPage(e As PrintPageEventArgs)
                MyBase.OnPrintPage(e)
                Dim font As New Font("Courier New", 10)
                Dim headerFont As New Font("Arial", 8, FontStyle.Bold)
                Dim brush As New SolidBrush(Color.Black)

                Dim leftMargin As Single = e.MarginBounds.Left
                Dim topMargin As Single = e.MarginBounds.Top
                Dim bottomMargin As Single = e.MarginBounds.Bottom
                Dim lineHeight As Single = font.GetHeight(e.Graphics)
                Dim linesPerPage As Integer = Math.Max(1, CInt((bottomMargin - topMargin) / lineHeight))

                Dim y As Single = topMargin
                Dim printed As Integer = 0

                While _lineIndex < _lines.Length AndAlso printed < linesPerPage
                    Dim line As String = _lines(_lineIndex)
                    Dim wrapped As List(Of String) = WrapText(line, font, e.Graphics, e.MarginBounds.Width)

                    For Each w As String In wrapped
                        If printed >= linesPerPage Then Exit For
                        e.Graphics.DrawString(w, font, brush, leftMargin, y)
                        y += lineHeight
                        printed += 1
                    Next

                    _lineIndex += 1
                End While

                Dim headerText As String = $"Door Calculation Results - {DateTime.Now:yyyy-MM-dd HH:mm:ss}"
                e.Graphics.DrawString(headerText, headerFont, brush, leftMargin, topMargin - 20)

                e.HasMorePages = _lineIndex < _lines.Length

                font.Dispose()
                headerFont.Dispose()
                brush.Dispose()
            End Sub

            Private Shared Function WrapText(text As String, font As Font, g As Graphics, maxWidth As Single) As List(Of String)
                Dim safeText As String = If(text, String.Empty)
                Dim words As String() = safeText.Split(" "c)
                Dim lines As New List(Of String)
                Dim current As String = String.Empty

                For Each word As String In words
                    Dim test As String = If(String.IsNullOrEmpty(current), word, current & " " & word)
                    Dim size As SizeF = g.MeasureString(test, font)
                    If size.Width <= maxWidth Then
                        current = test
                    Else
                        If Not String.IsNullOrEmpty(current) Then lines.Add(current)
                        current = word
                    End If
                Next

                If Not String.IsNullOrEmpty(current) Then lines.Add(current)
                Return lines
            End Function

        End Class

    End Module

End Namespace