Imports System.Diagnostics

Public Class FrmSplash
    Inherits Form

    Private splashImage As PictureBox
    Private lblTitle As Label
    Private lblVersion As Label
    Private lblSubtitle As Label
    Private lblCompany As Label
    Private lblRunInfo As Label
    Private lblCopyright As Label

    Public Sub New()
        InitializeComponent()

        ' Form setup
        Me.FormBorderStyle = FormBorderStyle.None
        Me.StartPosition = FormStartPosition.CenterScreen
        Me.BackColor = Color.White
        Me.Width = 500
        Me.Height = 400

        ' Get assembly info for company name
        Dim assembly = Reflection.Assembly.GetExecutingAssembly()
        Dim fileVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location)
        Dim companyName As String = If(String.IsNullOrEmpty(fileVersionInfo.CompanyName), "Woodworker's Friend Software", fileVersionInfo.CompanyName)

        ' Logo
        splashImage = New PictureBox With {
            .Image = My.Resources.ps_logo,
            .SizeMode = PictureBoxSizeMode.Zoom,
            .Location = New Point(120, 20),
            .Size = New Size(260, 120)
        }

        ' Application Title
        lblTitle = New Label With {
            .Text = AppName,
            .Font = New Font("Georgia", 24, FontStyle.Bold),
            .ForeColor = Color.DarkGreen,
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(50, 150),
            .Size = New Size(400, 40)
        }

        ' Version
        lblVersion = New Label With {
            .Text = $"Version {Version}",
            .Font = New Font("Georgia", 11, FontStyle.Regular),
            .ForeColor = Color.Gray,
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(50, 195),
            .Size = New Size(400, 25)
        }

        ' Subtitle/Tagline
        lblSubtitle = New Label With {
            .Text = "Professional Woodworking Calculator Suite",
            .Font = New Font("Georgia", 12, FontStyle.Italic),
            .ForeColor = Color.DarkSlateGray,
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(50, 230),
            .Size = New Size(400, 30)
        }

        ' Feature highlights
        Dim lblFeatures As New Label With {
            .Text = "15+ Calculators • Dark Mode • Cost Management" & vbCrLf &
                    "Wood Properties • Joinery Reference • Help System",
            .Font = New Font("Georgia", 9),
            .ForeColor = Color.SlateGray,
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(50, 270),
            .Size = New Size(400, 40)
        }

        ' Company name
        lblCompany = New Label With {
            .Text = companyName,
            .Font = New Font("Georgia", 10, FontStyle.Regular),
            .ForeColor = Color.DarkGray,
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(50, 320),
            .Size = New Size(400, 20)
        }

        ' Copyright
        lblCopyright = New Label With {
            .Text = GetCopyrightNotice(),
            .Font = New Font("Georgia", 8),
            .ForeColor = Color.LightGray,
            .AutoSize = False,
            .TextAlign = ContentAlignment.MiddleCenter,
            .Location = New Point(50, 345),
            .Size = New Size(400, 20)
        }

        ' Run counter (bottom right corner)
        lblRunInfo = New Label With {
            .Text = $"Run #{TimesRun}",
            .Font = New Font("Consolas", 8),
            .ForeColor = Color.LightGray,
            .AutoSize = False,
            .TextAlign = ContentAlignment.BottomRight,
            .Location = New Point(420, 375),
            .Size = New Size(70, 20)
        }

        ' Add all controls
        Me.Controls.Add(splashImage)
        Me.Controls.Add(lblTitle)
        Me.Controls.Add(lblVersion)
        Me.Controls.Add(lblSubtitle)
        Me.Controls.Add(lblFeatures)
        Me.Controls.Add(lblCompany)
        Me.Controls.Add(lblCopyright)
        Me.Controls.Add(lblRunInfo)
    End Sub

    Public Sub FadeOut(Optional fadeDuration As Integer = 1000)
        Dim sw As New Stopwatch()
        sw.Start()
        While Me.Opacity > 0
            Me.Opacity -= 0.05
            Application.DoEvents()
            Threading.Thread.Sleep(fadeDuration \ 20)
        End While
        sw.Stop()
    End Sub

End Class
