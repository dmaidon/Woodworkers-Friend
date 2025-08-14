Imports System.Drawing

Namespace WwFriend.Modules.Doors

    Public NotInheritable Class DoorUiStateController

        Private Sub New()
        End Sub

        Public Shared Sub Apply(
            isInset As Boolean,
            isSingleDoor As Boolean,
            txtDoorOverlay As TextBox,
            txtGapSize As TextBox,
            Optional updatePresetButtons As Action = Nothing
        )
            If txtDoorOverlay IsNot Nothing Then
                If isInset Then
                    txtDoorOverlay.Enabled = False
                    txtDoorOverlay.ReadOnly = True
                    txtDoorOverlay.BackColor = SystemColors.Control
                    txtDoorOverlay.Text = "0"
                Else
                    txtDoorOverlay.Enabled = True
                    txtDoorOverlay.ReadOnly = False
                    txtDoorOverlay.BackColor = SystemColors.Window
                    If String.IsNullOrWhiteSpace(txtDoorOverlay.Text) OrElse txtDoorOverlay.Text = "0" Then
                        txtDoorOverlay.Text = "0.5"
                    End If
                End If
            End If

            If txtGapSize IsNot Nothing Then
                If isSingleDoor Then
                    txtGapSize.Enabled = False
                    txtGapSize.ReadOnly = True
                    txtGapSize.BackColor = SystemColors.Control
                    txtGapSize.Text = "0"
                Else
                    txtGapSize.Enabled = True
                    txtGapSize.ReadOnly = False
                    txtGapSize.BackColor = SystemColors.Window
                    If String.IsNullOrWhiteSpace(txtGapSize.Text) OrElse txtGapSize.Text = "0" Then
                        txtGapSize.Text = "0.125"
                    End If
                End If
            End If

            updatePresetButtons?.Invoke()
        End Sub

    End Class

End Namespace