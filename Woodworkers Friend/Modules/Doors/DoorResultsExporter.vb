Imports System.IO
Imports System.Windows.Forms

Namespace WwFriend.Modules.Doors

    Public NotInheritable Class DoorResultsExporter

        Private Sub New()
        End Sub

        Public Shared Sub Export(owner As IWin32Window, rtb As RichTextBox, statusSink As Action(Of String, Drawing.Color))
            If rtb Is Nothing OrElse String.IsNullOrWhiteSpace(rtb.Text) Then
                MessageBox.Show(owner, "No calculation results to export. Please calculate door dimensions first.", "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
                Return
            End If

            Using dlg As New SaveFileDialog()
                dlg.Filter = "Text Files (*.txt)|*.txt|Rich Text Format (*.rtf)|*.rtf|All Files (*.*)|*.*"
                dlg.DefaultExt = "txt"
                dlg.FileName = $"Door_Calculation_{DateTime.Now:yyyyMMdd_HHmmss}"

                If dlg.ShowDialog(owner) = DialogResult.OK Then
                    If dlg.FilterIndex = 2 Then
                        File.WriteAllText(dlg.FileName, rtb.Rtf)
                    Else
                        File.WriteAllText(dlg.FileName, rtb.Text)
                    End If
                    statusSink?.Invoke($"Results exported to {Path.GetFileName(dlg.FileName)}", Drawing.Color.Green)
                    MessageBox.Show(owner, "Export completed successfully.", "Export", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End Using
        End Sub

    End Class

End Namespace