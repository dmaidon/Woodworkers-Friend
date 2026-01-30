' ============================================================================
' Last Updated: January 30, 2026
' Changes: Phase 4 - Migrated to unified SQLite database via DatabaseManager.
'          Now loads help content from HelpContent table instead of embedded
'          Markdown resources. Renders custom markup tags to RTF formatting.
'          Added search support via DatabaseManager.SearchHelpContent().
'          Original embedded-resource loading kept as fallback.
' ============================================================================

Imports System.IO
Imports System.Reflection
Imports System.Text.RegularExpressions

''' <summary>
''' Manages loading and formatting of help content from unified database.
''' Falls back to embedded Markdown resources if database unavailable.
'''
''' Markup format stored in database:
'''   #TITLE:text          - Main title (16pt bold, dark blue)
'''   ##SECTION:title|desc - Section with title and description
'''   ###SUBTITLE:text     - Subtitle (11pt bold)
'''   *BULLET:text         - Bullet point
'''   #NUM:n:text          - Numbered item
'''   !WARNING:text        - Red warning box
'''   ?NOTE:text           - Green note box
'''   =FORMULA:text        - Monospace formula box
'''   @METHOD:title|desc   - Method/feature box with background color
'''   ^SHORTCUT:keys|desc  - Keyboard shortcut display
'''   &amp;PROBLEM:text        - Problem statement (red)
'''   &amp;SOLUTION:text       - Solution text (green)
'''   (plain text)         - Normal paragraph text
'''   (empty line)         - Blank line separator
''' </summary>
Public Class HelpContentManager

    Private Const RESOURCE_NAMESPACE As String = "WwFriend.Resources.Help"

    ''' <summary>
    ''' Tries to load help content from database first, then embedded resources.
    ''' Returns True if content was loaded successfully.
    ''' </summary>
    Public Shared Function TryLoadHelpTopic(rtb As RichTextBox, topic As String) As Boolean
        Try
            ' Try database first (Phase 4)
            Dim helpData = DatabaseManager.Instance.GetHelpContent(topic)
            If helpData IsNot Nothing AndAlso Not String.IsNullOrEmpty(helpData.Content) Then
                rtb.Clear()
                RenderMarkupContent(rtb, helpData.Content)
                Return True
            End If

            ' Fallback: Try embedded Markdown resources
            Dim markdown = LoadMarkdownResource(topic)
            If Not String.IsNullOrEmpty(markdown) Then
                rtb.Clear()
                ConvertMarkdownToRichText(rtb, markdown)
                Return True
            End If

            Return False
        Catch ex As Exception
            ErrorHandler.LogError(ex, $"TryLoadHelpTopic - {topic}")
            Return False
        End Try
    End Function

    ''' <summary>
    ''' Loads help content from database and displays in RichTextBox
    ''' </summary>
    Public Shared Sub LoadHelpTopic(rtb As RichTextBox, topic As String)
        If Not TryLoadHelpTopic(rtb, topic) Then
            ShowResourceNotFound(rtb, topic)
        End If
    End Sub

    ''' <summary>
    ''' Searches help content and returns matching topics
    ''' </summary>
    Public Shared Function SearchHelp(searchTerm As String) As List(Of DatabaseManager.HelpContentData)
        Try
            Return DatabaseManager.Instance.SearchHelpContent(searchTerm)
        Catch ex As Exception
            ErrorHandler.LogError(ex, "SearchHelp")
            Return New List(Of DatabaseManager.HelpContentData)
        End Try
    End Function

    ''' <summary>
    ''' Gets all help topics from database (lightweight, no content body)
    ''' </summary>
    Public Shared Function GetAllTopics() As List(Of DatabaseManager.HelpContentData)
        Try
            Return DatabaseManager.Instance.GetHelpTopics()
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetAllTopics")
            Return New List(Of DatabaseManager.HelpContentData)
        End Try
    End Function

#Region "Markup Renderer"

    ''' <summary>
    ''' Renders custom markup content into a RichTextBox with formatted RTF
    ''' </summary>
    Public Shared Sub RenderMarkupContent(rtb As RichTextBox, content As String)
        rtb.Clear()

        Dim lines = content.Split({vbLf}, StringSplitOptions.None)
        Dim methodColorIndex = 0
        Dim methodColors() As Color = {
            Color.FromArgb(230, 230, 250),
            Color.FromArgb(240, 255, 240),
            Color.FromArgb(255, 250, 205),
            Color.FromArgb(255, 228, 225),
            Color.FromArgb(245, 245, 245),
            Color.FromArgb(230, 250, 250)
        }

        For Each line In lines
            Dim trimmed = line.Trim()

            ' Empty line = blank separator
            If String.IsNullOrEmpty(trimmed) Then
                rtb.AppendText(vbCrLf)
                Continue For
            End If

            ' #TITLE:text
            If trimmed.StartsWith("#TITLE:") Then
                Dim text = trimmed.Substring(7)
                rtb.SelectionFont = New Font("Segoe UI", 16, FontStyle.Bold)
                rtb.SelectionColor = Color.DarkBlue
                rtb.AppendText(text & vbCrLf)
                rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
                rtb.SelectionColor = Color.Black
                rtb.AppendText(New String(ChrW(&H2500), 60) & vbCrLf & vbCrLf)
                Continue For
            End If

            ' ##SECTION:title|description
            If trimmed.StartsWith("##SECTION:") Then
                Dim parts = trimmed.Substring(10).Split("|"c)
                rtb.SelectionFont = New Font("Segoe UI", 12, FontStyle.Bold)
                rtb.SelectionColor = Color.DarkGreen
                rtb.AppendText(parts(0) & vbCrLf)
                If parts.Length > 1 Then
                    rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
                    rtb.SelectionColor = Color.Black
                    rtb.AppendText(parts(1) & vbCrLf)
                End If
                rtb.AppendText(vbCrLf)
                Continue For
            End If

            ' ###SUBTITLE:text
            If trimmed.StartsWith("###SUBTITLE:") Then
                Dim text = trimmed.Substring(12)
                rtb.SelectionFont = New Font("Segoe UI", 11, FontStyle.Bold)
                rtb.SelectionColor = Color.DarkOliveGreen
                rtb.AppendText(text & vbCrLf)
                rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
                rtb.SelectionColor = Color.Black
                Continue For
            End If

            ' *BULLET:text
            If trimmed.StartsWith("*BULLET:") Then
                Dim text = trimmed.Substring(8)
                rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
                rtb.SelectionColor = Color.Black
                rtb.AppendText("  " & ChrW(&H2022) & " " & text & vbCrLf)
                Continue For
            End If

            ' #NUM:n:text
            If trimmed.StartsWith("#NUM:") Then
                Dim rest = trimmed.Substring(5)
                Dim colonIdx = rest.IndexOf(":"c)
                If colonIdx > 0 Then
                    Dim num = rest.Substring(0, colonIdx)
                    Dim text = rest.Substring(colonIdx + 1)
                    rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Bold)
                    rtb.SelectionColor = Color.DarkBlue
                    rtb.AppendText($"  {num}. ")
                    rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
                    rtb.SelectionColor = Color.Black
                    rtb.AppendText(text & vbCrLf)
                End If
                Continue For
            End If

            ' !WARNING:text
            If trimmed.StartsWith("!WARNING:") Then
                Dim text = trimmed.Substring(9)
                Dim startPos = rtb.TextLength
                rtb.SelectionFont = New Font("Segoe UI", 9, FontStyle.Bold)
                rtb.SelectionColor = Color.DarkRed
                rtb.AppendText(ChrW(&H26A0) & " " & text & vbCrLf)
                rtb.Select(startPos, rtb.TextLength - startPos)
                rtb.SelectionBackColor = Color.FromArgb(255, 240, 240)
                rtb.SelectionLength = 0
                rtb.SelectionBackColor = Color.White
                Continue For
            End If

            ' ?NOTE:text
            If trimmed.StartsWith("?NOTE:") Then
                Dim text = trimmed.Substring(6)
                Dim startPos = rtb.TextLength
                rtb.SelectionFont = New Font("Segoe UI", 9, FontStyle.Italic)
                rtb.SelectionColor = Color.DarkGreen
                rtb.AppendText(ChrW(&H2139) & " " & text & vbCrLf)
                rtb.Select(startPos, rtb.TextLength - startPos)
                rtb.SelectionBackColor = Color.FromArgb(240, 255, 240)
                rtb.SelectionLength = 0
                rtb.SelectionBackColor = Color.White
                Continue For
            End If

            ' =FORMULA:text
            If trimmed.StartsWith("=FORMULA:") Then
                Dim text = trimmed.Substring(9)
                Dim startPos = rtb.TextLength
                rtb.SelectionFont = New Font("Courier New", 10, FontStyle.Bold)
                rtb.SelectionColor = Color.DarkBlue
                rtb.AppendText("  " & text & vbCrLf)
                rtb.Select(startPos, rtb.TextLength - startPos)
                rtb.SelectionBackColor = Color.FromArgb(245, 245, 250)
                rtb.SelectionLength = 0
                rtb.SelectionBackColor = Color.White
                Continue For
            End If

            ' @METHOD:title|description
            If trimmed.StartsWith("@METHOD:") Then
                Dim parts = trimmed.Substring(8).Split("|"c)
                Dim title = parts(0)
                Dim desc = If(parts.Length > 1, parts(1), "")
                Dim bgColor = methodColors(methodColorIndex Mod methodColors.Length)
                methodColorIndex += 1

                Dim startPos = rtb.TextLength
                rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Bold)
                rtb.SelectionColor = Color.DarkBlue
                rtb.AppendText(ChrW(&H25B6) & " " & title & vbCrLf)
                If Not String.IsNullOrEmpty(desc) Then
                    rtb.SelectionFont = New Font("Segoe UI", 9, FontStyle.Regular)
                    rtb.SelectionColor = Color.DimGray
                    rtb.AppendText("  " & desc & vbCrLf)
                End If
                rtb.Select(startPos, rtb.TextLength - startPos)
                rtb.SelectionBackColor = bgColor
                rtb.SelectionLength = 0
                rtb.SelectionBackColor = Color.White
                rtb.AppendText(vbCrLf)
                Continue For
            End If

            ' ^SHORTCUT:keys|description
            If trimmed.StartsWith("^SHORTCUT:") Then
                Dim parts = trimmed.Substring(10).Split("|"c)
                Dim keys = parts(0)
                Dim desc = If(parts.Length > 1, parts(1), "")
                rtb.SelectionFont = New Font("Segoe UI", 9, FontStyle.Bold)
                rtb.SelectionColor = Color.White
                rtb.SelectionBackColor = Color.DarkBlue
                rtb.AppendText(" " & keys & " ")
                rtb.SelectionBackColor = Color.White
                rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
                rtb.SelectionColor = Color.Black
                rtb.AppendText("  " & desc & vbCrLf)
                Continue For
            End If

            ' &PROBLEM:text
            If trimmed.StartsWith("&PROBLEM:") Then
                Dim text = trimmed.Substring(9)
                rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Bold)
                rtb.SelectionColor = Color.DarkRed
                rtb.AppendText("Problem: " & text & vbCrLf)
                Continue For
            End If

            ' &SOLUTION:text
            If trimmed.StartsWith("&SOLUTION:") Then
                Dim text = trimmed.Substring(10)
                rtb.SelectionFont = New Font("Segoe UI", 9, FontStyle.Regular)
                rtb.SelectionColor = Color.DarkGreen
                rtb.AppendText(text & vbCrLf & vbCrLf)
                Continue For
            End If

            ' Default: plain text
            rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
            rtb.SelectionColor = Color.Black
            rtb.AppendText(trimmed & vbCrLf)
        Next

        ' Scroll to top
        rtb.SelectionStart = 0
        rtb.ScrollToCaret()
    End Sub

#End Region

#Region "Embedded Resource Fallback"

    ''' <summary>
    ''' Loads Markdown content from embedded resource (fallback)
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
    ''' Converts Markdown to formatted RichTextBox content (fallback)
    ''' </summary>
    Private Shared Sub ConvertMarkdownToRichText(rtb As RichTextBox, markdown As String)
        rtb.Clear()
        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)

        Dim lines = markdown.Split({vbCrLf, vbLf}, StringSplitOptions.None)
        Dim inCodeBlock = False

        For Each line In lines
            If line.Trim().StartsWith("```") Then
                inCodeBlock = Not inCodeBlock
                Continue For
            End If

            If inCodeBlock Then
                rtb.SelectionFont = New Font("Consolas", 9, FontStyle.Regular)
                rtb.SelectionColor = Color.DarkBlue
                rtb.SelectionBackColor = Color.FromArgb(240, 240, 240)
                rtb.AppendText("  " & line & vbCrLf)
                Continue For
            End If

            rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
            rtb.SelectionColor = Color.Black
            rtb.SelectionBackColor = Color.White

            If line.StartsWith("# ") Then
                rtb.SelectionFont = New Font("Georgia", 16, FontStyle.Bold)
                rtb.SelectionColor = Color.DarkBlue
                rtb.AppendText(String.Concat(line.AsSpan(2), vbCrLf, vbCrLf))
                Continue For
            End If

            If line.StartsWith("## ") Then
                rtb.SelectionFont = New Font("Georgia", 13, FontStyle.Bold)
                rtb.SelectionColor = Color.DarkGreen
                rtb.AppendText(String.Concat(line.AsSpan(3), vbCrLf, vbCrLf))
                Continue For
            End If

            If line.StartsWith("### ") Then
                rtb.SelectionFont = New Font("Georgia", 11, FontStyle.Bold)
                rtb.SelectionColor = Color.DarkOliveGreen
                rtb.AppendText(String.Concat(line.AsSpan(4), vbCrLf))
                Continue For
            End If

            If line.TrimStart().StartsWith("- ") OrElse line.TrimStart().StartsWith("* ") Then
                rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
                rtb.AppendText(String.Concat("  " & ChrW(&H2022) & " ", line.TrimStart().AsSpan(2), vbCrLf))
                Continue For
            End If

            If Regex.IsMatch(line.TrimStart(), "^\d+\.\s") Then
                rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
                rtb.AppendText("  " & line.TrimStart() & vbCrLf)
                Continue For
            End If

            If String.IsNullOrWhiteSpace(line) Then
                rtb.AppendText(vbCrLf)
                Continue For
            End If

            rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
            rtb.SelectionColor = Color.Black
            rtb.AppendText(line & vbCrLf)
        Next

        rtb.SelectionStart = 0
        rtb.ScrollToCaret()
    End Sub

#End Region

    ''' <summary>
    ''' Shows error when resource not found
    ''' </summary>
    Private Shared Sub ShowResourceNotFound(rtb As RichTextBox, topic As String)
        rtb.SelectionFont = New Font("Segoe UI", 12, FontStyle.Bold)
        rtb.SelectionColor = Color.DarkRed
        rtb.AppendText(ChrW(&H26A0) & " Help Topic Not Found" & vbCrLf & vbCrLf)

        rtb.SelectionFont = New Font("Segoe UI", 10, FontStyle.Regular)
        rtb.SelectionColor = Color.Black
        rtb.AppendText($"The help topic '{topic}' could not be loaded." & vbCrLf & vbCrLf)
        rtb.AppendText("Please select a different topic from the navigation tree on the left.")
    End Sub

    ''' <summary>
    ''' Gets list of available help topics from database and embedded resources
    ''' </summary>
    Public Shared Function GetAvailableTopics() As List(Of String)
        Dim topics As New List(Of String)
        Try
            ' Get from database first
            Dim dbTopics = DatabaseManager.Instance.GetHelpTopics()
            For Each t In dbTopics
                topics.Add(t.ModuleName)
            Next

            ' Also check embedded resources
            Dim assembly As Assembly = Assembly.GetExecutingAssembly()
            Dim resources As String() = assembly.GetManifestResourceNames()

            For Each resource As String In resources
                If resource.StartsWith(RESOURCE_NAMESPACE) AndAlso resource.EndsWith(".md") Then
                    Dim topic = resource.Substring(RESOURCE_NAMESPACE.Length + 1)
                    topic = topic.Substring(0, topic.Length - 3)
                    If Not topics.Contains(topic) Then
                        topics.Add(topic)
                    End If
                End If
            Next
        Catch ex As Exception
            ErrorHandler.LogError(ex, "GetAvailableTopics")
        End Try
        Return topics
    End Function

End Class
