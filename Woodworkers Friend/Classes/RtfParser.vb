Imports System.IO
Imports System.Text.RegularExpressions
Imports System.Windows.Forms

Public Class RtfParser

    Public Shared Sub ParseRtfTableToCsv(rtfPath As String, csvPath As String)
        ' Load RTF as plain text
        Dim rtfText As String
        Using rtfBox As New RichTextBox()
            rtfBox.LoadFile(rtfPath)
            rtfText = rtfBox.Text
        End Using

        ' Find the table header line
        Dim lines = rtfText.Split({vbCrLf, vbLf}, StringSplitOptions.RemoveEmptyEntries)
        Dim tableStart = lines.ToList().FindIndex(Function(l) l.Contains("THICKNESS") AndAlso (l.Contains("3"c) OrElse l.Contains("6"c) OrElse l.Contains("9"c) OrElse l.Contains("12")))
        If tableStart = -1 Then
            MessageBox.Show("Table header not found.")
            Return
        End If

        Using writer As New StreamWriter(csvPath)
            ' Write header
            writer.WriteLine("THICKNESS,DESCRIPTION,COST")

            ' Parse each table row
            For i = tableStart + 1 To lines.Length - 1
                Dim line = lines(i).Trim()
                If String.IsNullOrWhiteSpace(line) Then Continue For

                ' Handle YEAR END CLOSE OUT lines
                If line.Contains("YEAR END CLOSE OUT", StringComparison.OrdinalIgnoreCase) Then
                    Dim m = Regex.Match(line, "\$[\d,]+(\.\d{2})?")
                    Dim cost = If(m.Success, m.Value.Replace("$"c, "").Replace(","c, ""), "")
                    writer.WriteLine($",,{cost}")
                    Continue For
                End If

                ' Skip lines that don't look like data rows
                If Not Regex.IsMatch(line, "^\d+") Then Continue For

                ' Split the line using multiple delimiters (tabs, multiple spaces)
                Dim parts = Regex.Split(line.Trim(), "\t+|\s{2,}").Where(Function(p) Not String.IsNullOrWhiteSpace(p)).ToArray()

                If parts.Length < 6 Then Continue For ' Need at least: SKU, Thickness, Description, 4 prices

                ' Extract components (skip SKU at index 0)
                Dim thickness = parts(1).Trim()
                Dim description = parts(2).Trim()

                ' Clean description
                description = description.Replace("S2S", "", StringComparison.OrdinalIgnoreCase) _
                                        .Replace("*"c, "") _
                                        .Replace("YEAR END CLOSE OUT", "", StringComparison.OrdinalIgnoreCase) _
                                        .Trim()

                ' Extract price columns (should be the last 4 elements)
                Dim priceStartIndex = Math.Max(3, parts.Length - 4)
                Dim prices As New List(Of Double)

                For j = priceStartIndex To parts.Length - 1
                    Dim priceText = parts(j).Replace("$"c, "").Replace(",", "").Trim()
                    Dim priceVal As Double
                    If Double.TryParse(priceText, priceVal) AndAlso priceVal > 0 Then
                        prices.Add(priceVal)
                    End If
                Next

                ' Calculate average cost
                Dim avgCost As String = ""
                If prices.Count > 0 Then
                    avgCost = (prices.Sum() / prices.Count).ToString("F2")
                End If

                ' Write to CSV: THICKNESS, DESCRIPTION, COST
                writer.WriteLine($"{thickness},{description},{avgCost}")
            Next
        End Using

        MessageBox.Show("CSV file created: " & csvPath)
    End Sub

End Class