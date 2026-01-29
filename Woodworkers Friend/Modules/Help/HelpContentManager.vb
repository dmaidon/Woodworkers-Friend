' ============================================================================
' Last Updated: January 29, 2026
' Changes: Initial creation - Resource-based help content manager with
'          Markdown to RTF conversion for embedded help documentation
' ============================================================================

Imports System.IO
Imports System.Reflection
Imports System.Text.RegularExpressions

''' <summary>
''' Manages loading and formatting of help content from embedded Markdown resources
''' </summary>
Public Class HelpContentManager

    Private Const RESOURCE_NAMESPACE As String = "WwFriend.Resources.Help"

    ''' <summary>
    ''' Loads help content from embedded Markdown resource and displays in RichTextBox
    ''' </summary>
    Public Shared Sub LoadHelpTopic(rtb As RichTextBox, topic As String)
        Try
            ' Clear existing content
            rtb.Clear()

            ' Load markdown content
            Dim markdown = LoadMarkdownResource(topic)

            If String.IsNullOrEmpty(markdown) Then
                ShowResourceNotFound(rtb, topic)
                Return
            End If

            ' Convert Markdown to formatted RTF and display
            ConvertMarkdownToRichText(rtb, markdown)
        Catch ex As Exception
            ShowErrorMessage(rtb, topic, ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' Tries to load help content from embedded Markdown resource.
    ''' Returns True if successful, False if resource not found (for fallback handling)
    ''' </summary>
    Public Shared Function TryLoadHelpTopic(rtb As RichTextBox, topic As String) As Boolean
        Try
            ' Load markdown content
            Dim markdown = LoadMarkdownResource(topic)

            If String.IsNullOrEmpty(markdown) Then
                Return False
            End If

            ' Clear existing content
            rtb.Clear()

            ' Convert Markdown to formatted RTF and display
            ConvertMarkdownToRichText(rtb, markdown)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Loads Markdown content from embedded resource
    ''' </summary>
    Private Shared Function LoadMarkdownResource(topic As String) As String
        Try
            Dim assembly As Assembly = Assembly.GetExecutingAssembly()
            Dim resourceName = $"{RESOURCE_NAMESPACE}.{topic}.md"

            Using stream = assembly.GetManifestResourceStream(resourceName)
                If stream Is Nothing Then
                    Return Nothing
                End If

                Using reader As New StreamReader(stream)
                    Return reader.ReadToEnd()
                End Using
            End Using
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Converts Markdown to formatted RichTextBox content
    ''' Simple converter supporting: headers, bold, italic, bullets, code blocks
    ''' </summary>
    Private Shared Sub ConvertMarkdownToRichText(rtb As RichTextBox, markdown As String)
        rtb.Clear()
        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)

        Dim lines = markdown.Split({vbCrLf, vbLf}, StringSplitOptions.None)
        Dim inCodeBlock = False

        For Each line In lines
            ' Handle code blocks (```)
            If line.Trim().StartsWith("```") Then
                inCodeBlock = Not inCodeBlock
                Continue For
            End If

            If inCodeBlock Then
                ' Code block formatting
                rtb.SelectionFont = New Font("Consolas", 9, FontStyle.Regular)
                rtb.SelectionColor = Color.DarkBlue
                rtb.SelectionBackColor = Color.FromArgb(240, 240, 240)
                rtb.AppendText("  " & line & vbCrLf)
                Continue For
            End If

            ' Reset formatting for each line
            rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
            rtb.SelectionColor = Color.Black
            rtb.SelectionBackColor = Color.White

            ' H1 Headers (# )
            If line.StartsWith("# ") Then
                rtb.SelectionFont = New Font("Georgia", 16, FontStyle.Bold)
                rtb.SelectionColor = Color.DarkBlue
                rtb.AppendText(String.Concat(line.AsSpan(2), vbCrLf, vbCrLf))
                Continue For
            End If

            ' H2 Headers (## )
            If line.StartsWith("## ") Then
                rtb.SelectionFont = New Font("Georgia", 13, FontStyle.Bold)
                rtb.SelectionColor = Color.DarkGreen
                rtb.AppendText(String.Concat(line.AsSpan(3), vbCrLf, vbCrLf))
                Continue For
            End If

            ' H3 Headers (### )
            If line.StartsWith("### ") Then
                rtb.SelectionFont = New Font("Georgia", 11, FontStyle.Bold)
                rtb.SelectionColor = Color.DarkOliveGreen
                rtb.AppendText(String.Concat(line.AsSpan(4), vbCrLf))
                Continue For
            End If

            ' Bullet points (- or *)
            If line.TrimStart().StartsWith("- ") OrElse line.TrimStart().StartsWith("* ") Then
                rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
                rtb.AppendText(String.Concat("  • ", line.TrimStart().AsSpan(2), vbCrLf))
                Continue For
            End If

            ' Numbered lists (1. )
            If Regex.IsMatch(line.TrimStart(), "^\d+\.\s") Then
                rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
                rtb.AppendText("  " & line.TrimStart() & vbCrLf)
                Continue For
            End If

            ' Empty lines
            If String.IsNullOrWhiteSpace(line) Then
                rtb.AppendText(vbCrLf)
                Continue For
            End If

            ' Process inline formatting (bold, italic, code)
            ProcessInlineFormatting(rtb, line)
            rtb.AppendText(vbCrLf)
        Next

        ' Scroll to top
        rtb.SelectionStart = 0
        rtb.ScrollToCaret()
    End Sub

    ''' <summary>
    ''' Processes inline Markdown formatting (bold, italic, inline code)
    ''' </summary>
    Private Shared Sub ProcessInlineFormatting(rtb As RichTextBox, line As String)
        'Dim position As Integer
        Dim currentText = line

        ' Simple regex patterns for inline formatting
        ' **bold**, *italic*, `code`
        Dim patterns As New Dictionary(Of String, (Style As FontStyle, Color As Color)) From {
            {"\*\*(.+?)\*\*", (FontStyle.Bold, Color.Black)},
            {"\*(.+?)\*", (FontStyle.Italic, Color.Black)},
            {"`(.+?)`", (FontStyle.Regular, Color.DarkBlue)}
        }

        ' For simplicity, just append the line with basic formatting
        ' A full implementation would parse and apply formatting segments
        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
        rtb.SelectionColor = Color.Black

        ' Simple bold handling
        currentText = Regex.Replace(currentText, "\*\*(.+?)\*\*", "$1")
        ' Simple italic handling
        currentText = Regex.Replace(currentText, "\*(.+?)\*", "$1")
        ' Simple code handling
        currentText = Regex.Replace(currentText, "`(.+?)`", "$1")

        rtb.AppendText(currentText)
    End Sub

    ''' <summary>
    ''' Shows error when resource not found
    ''' </summary>
    Private Shared Sub ShowResourceNotFound(rtb As RichTextBox, topic As String)
        rtb.SelectionFont = New Font("Segoe UI", 12, FontStyle.Bold)
        rtb.SelectionColor = Color.DarkRed
        rtb.AppendText("⚠ Help Topic Not Found" & vbCrLf & vbCrLf)

        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
        rtb.SelectionColor = Color.Black
        rtb.AppendText($"The help topic '{topic}' could not be loaded." & vbCrLf & vbCrLf)
        rtb.AppendText("This may indicate:" & vbCrLf)
        rtb.AppendText("  • The help file is missing from resources" & vbCrLf)
        rtb.AppendText("  • The resource name doesn't match" & vbCrLf & vbCrLf)
        rtb.AppendText($"Expected resource: {RESOURCE_NAMESPACE}.{topic}.md")
    End Sub

    ''' <summary>
    ''' Shows error message
    ''' </summary>
    Private Shared Sub ShowErrorMessage(rtb As RichTextBox, topic As String, errorMsg As String)
        rtb.SelectionFont = New Font("Segoe UI", 12, FontStyle.Bold)
        rtb.SelectionColor = Color.DarkRed
        rtb.AppendText("Error Loading Help" & vbCrLf & vbCrLf)

        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
        rtb.SelectionColor = Color.Black
        rtb.AppendText($"Topic: {topic}" & vbCrLf)
        rtb.AppendText($"Error: {errorMsg}")
    End Sub

    ''' <summary>
    ''' Gets list of available help topics from embedded resources
    ''' </summary>
    Public Shared Function GetAvailableTopics() As List(Of String)
        Dim topics As New List(Of String)
        Try
            Dim assembly As Assembly = Assembly.GetExecutingAssembly()
            Dim resources As String() = assembly.GetManifestResourceNames()

            For Each resource As String In resources
                If resource.StartsWith(RESOURCE_NAMESPACE) AndAlso resource.EndsWith(".md") Then
                    ' Extract topic name (remove namespace and .md extension)
                    Dim topic = resource.Substring(RESOURCE_NAMESPACE.Length + 1)
                    topic = topic.Substring(0, topic.Length - 3) ' Remove .md
                    topics.Add(topic)
                End If
            Next
        Catch ex As Exception
            ' Return empty list on error
        End Try
        Return topics
    End Function

    ''' <summary>
    ''' Debugging method to list embedded resources
    ''' </summary>
    Public Shared Sub DebugListResources()
        Dim assembly As Assembly = Assembly.GetExecutingAssembly()
        Dim resources As String() = assembly.GetManifestResourceNames()
        Debug.WriteLine("=== Embedded Resources ===")
        For Each resource As String In resources
            If resource.Contains(".Help.") Then
                Debug.WriteLine(resource)
            End If
        Next
    End Sub

End Class
