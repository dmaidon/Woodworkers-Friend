' ============================================================================
' Last Updated: January 27, 2026
' Changes: Initial creation - Multi-format export system for CSV, Text, HTML
'          with DataTable, Dictionary, and DataGridView support
' ============================================================================

Imports System.IO
Imports System.Text

''' <summary>
''' Provides export functionality for calculation results and reports
''' </summary>
Public Class ReportExporter

    ''' <summary>
    ''' Exports data to CSV format
    ''' </summary>
    Public Shared Sub ExportToCsv(data As DataTable, filePath As String)
        Try
            Dim csv As New StringBuilder()

            ' Add column headers
            Dim headers = data.Columns.Cast(Of DataColumn)().
                              Select(Function(col) EscapeCsvValue(col.ColumnName))
            csv.AppendLine(String.Join(",", headers))

            ' Add rows
            For Each row As DataRow In data.Rows
                Dim values = row.ItemArray.Select(Function(val) EscapeCsvValue(val?.ToString()))
                csv.AppendLine(String.Join(",", values))
            Next

            File.WriteAllText(filePath, csv.ToString())
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "ReportExporter.ExportToCsv", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Exports a dictionary of key-value pairs to CSV
    ''' </summary>
    Public Shared Sub ExportToCsv(data As Dictionary(Of String, String), filePath As String)
        Try
            Dim csv As New StringBuilder()
            csv.AppendLine("Property,Value")

            For Each kvp In data
                csv.AppendLine($"{EscapeCsvValue(kvp.Key)},{EscapeCsvValue(kvp.Value)}")
            Next

            File.WriteAllText(filePath, csv.ToString())
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "ReportExporter.ExportToCsv", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Exports calculation results to a text file
    ''' </summary>
    Public Shared Sub ExportToText(title As String,
                                   data As Dictionary(Of String, String),
                                   filePath As String)
        Try
            Dim sb As New StringBuilder()

            ' Add header
            sb.AppendLine(New String("="c, 60))
            sb.AppendLine(title.PadLeft((60 + title.Length) \ 2))
            sb.AppendLine(New String("="c, 60))
            sb.AppendLine()

            ' Add timestamp
            sb.AppendLine($"Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}")
            sb.AppendLine()

            ' Add data
            Dim maxKeyLength = data.Keys.Max(Function(k) k.Length)
            For Each kvp In data
                sb.AppendLine($"{kvp.Key.PadRight(maxKeyLength)} : {kvp.Value}")
            Next

            ' Add footer
            sb.AppendLine()
            sb.AppendLine(New String("="c, 60))
            sb.AppendLine($"Woodworker's Friend v{Version}")
            sb.AppendLine(New String("="c, 60))

            File.WriteAllText(filePath, sb.ToString())
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "ReportExporter.ExportToText", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Exports DataGridView contents to CSV
    ''' </summary>
    Public Shared Sub ExportDataGridViewToCsv(dgv As DataGridView, filePath As String)
        Try
            Dim csv As New StringBuilder()

            ' Add headers
            Dim headers = dgv.Columns.Cast(Of DataGridViewColumn)().
                             Where(Function(col) col.Visible).
                             Select(Function(col) EscapeCsvValue(col.HeaderText))
            csv.AppendLine(String.Join(",", headers))

            ' Add rows
            For Each row As DataGridViewRow In dgv.Rows
                If Not row.IsNewRow Then
                    Dim values As New List(Of String)
                    For Each col As DataGridViewColumn In dgv.Columns
                        If col.Visible Then
                            Dim cellValue As String = If(row.Cells(col.Index).Value?.ToString(), String.Empty)
                            values.Add(EscapeCsvValue(cellValue))
                        End If
                    Next
                    csv.AppendLine(String.Join(",", values))
                End If
            Next

            File.WriteAllText(filePath, csv.ToString())
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "ReportExporter.ExportDataGridViewToCsv", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Exports calculation results to HTML format
    ''' </summary>
    Public Shared Sub ExportToHtml(title As String,
                                  data As Dictionary(Of String, String),
                                  filePath As String)
        Try
            Dim html As New StringBuilder()

            html.AppendLine("<!DOCTYPE html>")
            html.AppendLine("<html>")
            html.AppendLine("<head>")
            html.AppendLine($"    <title>{title}</title>")
            html.AppendLine("    <style>")
            html.AppendLine("        body { font-family: Arial, sans-serif; margin: 20px; }")
            html.AppendLine("        h1 { color: #2c3e50; border-bottom: 2px solid #3498db; }")
            html.AppendLine("        table { border-collapse: collapse; width: 100%; margin-top: 20px; }")
            html.AppendLine("        th, td { border: 1px solid #ddd; padding: 12px; text-align: left; }")
            html.AppendLine("        th { background-color: #3498db; color: white; }")
            html.AppendLine("        tr:nth-child(even) { background-color: #f2f2f2; }")
            html.AppendLine("        .footer { margin-top: 20px; color: #7f8c8d; font-size: 0.9em; }")
            html.AppendLine("    </style>")
            html.AppendLine("</head>")
            html.AppendLine("<body>")
            html.AppendLine($"    <h1>{title}</h1>")
            html.AppendLine($"    <p>Generated: {DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>")
            html.AppendLine("    <table>")
            html.AppendLine("        <tr><th>Property</th><th>Value</th></tr>")

            For Each kvp In data
                html.AppendLine($"        <tr><td>{HtmlEncode(kvp.Key)}</td><td>{HtmlEncode(kvp.Value)}</td></tr>")
            Next

            html.AppendLine("    </table>")
            html.AppendLine($"    <div class='footer'>Woodworker's Friend v{Version}</div>")
            html.AppendLine("</body>")
            html.AppendLine("</html>")

            File.WriteAllText(filePath, html.ToString())
        Catch ex As Exception
            ErrorHandler.HandleError(ex, "ReportExporter.ExportToHtml", showToUser:=True)
        End Try
    End Sub

    ''' <summary>
    ''' Escapes special characters for CSV format
    ''' </summary>
    Private Shared Function EscapeCsvValue(value As String) As String
        If String.IsNullOrEmpty(value) Then Return String.Empty

        ' If value contains comma, quote, or newline, wrap in quotes and escape quotes
        If value.Contains(","c) OrElse value.Contains(""""c) OrElse value.Contains(vbCrLf) Then
            Return """" & value.Replace("""", """""") & """"
        End If

        Return value
    End Function

    ''' <summary>
    ''' Encodes HTML special characters
    ''' </summary>
    Private Shared Function HtmlEncode(value As String) As String
        If String.IsNullOrEmpty(value) Then Return String.Empty

        Return value.Replace("&", "&amp;").
                    Replace("<", "&lt;").
                    Replace(">", "&gt;").
                    Replace("""", "&quot;").
                    Replace("'", "&#39;")
    End Function

End Class
