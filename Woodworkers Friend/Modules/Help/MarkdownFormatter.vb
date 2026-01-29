''' ============================================================================
''' Last Updated: January 29, 2026
''' Changes: Initial creation of MarkdownFormatter for rendering markdown in RichTextBox
''' ============================================================================

Imports System.Text.RegularExpressions

''' <summary>
''' Formats markdown content for display in a RichTextBox
''' </summary>
Public Class MarkdownFormatter

    ''' <summary>
    ''' Converts markdown text to formatted RichTextBox content
    ''' </summary>
    Public Shared Sub FormatMarkdown(rtb As RichTextBox, markdownText As String)
        If rtb Is Nothing OrElse String.IsNullOrWhiteSpace(markdownText) Then
            Return
        End If

        rtb.Clear()
        rtb.SelectionStart = 0

        Dim lines As String() = markdownText.Split(New String() {vbCrLf, vbLf}, StringSplitOptions.None)

        For Each line As String In lines
            ProcessMarkdownLine(rtb, line)
        Next

        ' Scroll to top
        rtb.SelectionStart = 0
        rtb.ScrollToCaret()
    End Sub

    ''' <summary>
    ''' Processes a single markdown line
    ''' </summary>
    Private Shared Sub ProcessMarkdownLine(rtb As RichTextBox, line As String)
        ' H1 - Large bold title
        If line.StartsWith("# ") Then
            AddFormattedText(rtb, line.Substring(2), 16, FontStyle.Bold, Color.DarkBlue)
            rtb.AppendText(vbCrLf)
            Return
        End If

        ' H2 - Medium bold section header
        If line.StartsWith("## ") Then
            If rtb.TextLength > 0 Then rtb.AppendText(vbCrLf)
            AddFormattedText(rtb, line.Substring(3), 14, FontStyle.Bold, Color.DarkGreen)
            rtb.AppendText(vbCrLf)
            Return
        End If

        ' H3 - Small bold subsection
        If line.StartsWith("### ") Then
            AddFormattedText(rtb, line.Substring(4), 12, FontStyle.Bold, Color.DarkOliveGreen)
            rtb.AppendText(vbCrLf)
            Return
        End If

        ' Bullet points
        If line.TrimStart().StartsWith("- ") Then
            Dim indent As Integer = line.Length - line.TrimStart().Length
            Dim bulletText As String = line.TrimStart().Substring(2)
            
            ' Add indentation
            If indent > 0 Then
                rtb.AppendText(New String(" "c, indent))
            End If
            
            ' Add bullet and process inline formatting
            rtb.AppendText("• ")
            ProcessInlineFormatting(rtb, bulletText)
            rtb.AppendText(vbCrLf)
            Return
        End If

        ' Empty line
        If String.IsNullOrWhiteSpace(line) Then
            rtb.AppendText(vbCrLf)
            Return
        End If

        ' Regular paragraph with inline formatting
        ProcessInlineFormatting(rtb, line)
        rtb.AppendText(vbCrLf)
    End Sub

    ''' <summary>
    ''' Processes inline markdown formatting (bold, italic, code)
    ''' </summary>
    Private Shared Sub ProcessInlineFormatting(rtb As RichTextBox, text As String)
        Dim currentPos As Integer = 0
        Dim boldRegex As New Regex("\*\*(.+?)\*\*")
        Dim matches As MatchCollection = boldRegex.Matches(text)

        If matches.Count = 0 Then
            ' No formatting, just add plain text
            AddFormattedText(rtb, text, 10, FontStyle.Regular, Color.Black)
            Return
        End If

        ' Process text with bold sections
        For Each match As Match In matches
            ' Add text before bold
            If match.Index > currentPos Then
                Dim beforeText As String = text.Substring(currentPos, match.Index - currentPos)
                AddFormattedText(rtb, beforeText, 10, FontStyle.Regular, Color.Black)
            End If

            ' Add bold text
            AddFormattedText(rtb, match.Groups(1).Value, 10, FontStyle.Bold, Color.Black)
            currentPos = match.Index + match.Length
        Next

        ' Add remaining text
        If currentPos < text.Length Then
            Dim remainingText As String = text.Substring(currentPos)
            AddFormattedText(rtb, remainingText, 10, FontStyle.Regular, Color.Black)
        End If
    End Sub

    ''' <summary>
    ''' Adds formatted text to the RichTextBox
    ''' </summary>
    Private Shared Sub AddFormattedText(rtb As RichTextBox, text As String, fontSize As Integer, style As FontStyle, color As Color)
        Dim startPos As Integer = rtb.TextLength
        rtb.AppendText(text)
        rtb.Select(startPos, text.Length)
        rtb.SelectionFont = New Font("Segoe UI", fontSize, style)
        rtb.SelectionColor = color
        rtb.Select(rtb.TextLength, 0)
    End Sub

End Class
