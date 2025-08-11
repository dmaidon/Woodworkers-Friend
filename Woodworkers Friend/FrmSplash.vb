Public Class FrmSplash
    Inherits Form

    Private splashImage As PictureBox
    Private lblTitle As Label
    Private lblInstructions As Label

    Public Sub New()
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.White
        Me.Width = 500
        Me.Height = 350

        splashImage = New PictureBox With {
            .Image = My.Resources.ps_logo, ' Corrected: Remove ".png" extension
            .SizeMode = PictureBoxSizeMode.Zoom,
            .Location = New Point(120, 20),
            .Size = New Size(260, 120)
        }

        lblTitle = New Label With {
            .Text = "Woodworker's Friend",
            .Font = New Font("Georgia", 20, FontStyle.Bold),
            .ForeColor = Color.DarkGreen,
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(50, 150),
            .Size = New Size(400, 40)
        }

        lblInstructions = New Label With {
            .Text = "Welcome! Please follow these steps:" & vbCrLf &
                     "1. Select a project type from the menu." & vbCrLf &
                     "2. Enter your measurements and options." & vbCrLf &
                     "3. Click 'Calculate' to view results." & vbCrLf &
                     "4. Use the Print or Save options as needed.",
            .Font = New Font("Georgia", 12),
            .ForeColor = Color.Black,
            .AutoSize = False,
            .TextAlign = ContentAlignment.TopLeft,
            .Location = New Point(50, 200),
            .Size = New Size(400, 120)
        }

        Me.Controls.Add(splashImage)
        Me.Controls.Add(lblTitle)
        Me.Controls.Add(lblInstructions)
    End Sub

    Public Sub FadeOut(Optional fadeDuration As Integer = 1000)
        Dim sw As New Stopwatch()
        sw.Start()
        '   Dim initialOpacity As Double = Me.Opacity
        While Me.Opacity > 0
            Me.Opacity -= 0.05
            Application.DoEvents()
            Threading.Thread.Sleep(fadeDuration \ 20)
        End While
        sw.Stop()
    End Sub

End Class