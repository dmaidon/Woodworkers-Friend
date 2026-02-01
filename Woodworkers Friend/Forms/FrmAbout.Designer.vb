<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAbout
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAbout))
        Me.PictureBox1 = New PictureBox()
        Me.LblAppName = New Label()
        Me.LblVersion = New Label()
        Me.LblCopyright = New Label()
        Me.TxtDescription = New TextBox()
        Me.LnkGitHub = New LinkLabel()
        Me.LnkLicense = New LinkLabel()
        Me.LnkEmail = New LinkLabel()
        Me.BtnOK = New Button()
        Me.Label1 = New Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        Me.PictureBox1.Location = New Point(12, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New Size(64, 64)
        Me.PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 0
        Me.PictureBox1.TabStop = False
        '
        'LblAppName
        '
        Me.LblAppName.AutoSize = True
        Me.LblAppName.Font = New Font("Segoe UI", 16.0F, FontStyle.Bold, GraphicsUnit.Point)
        Me.LblAppName.Location = New Point(82, 12)
        Me.LblAppName.Name = "LblAppName"
        Me.LblAppName.Size = New Size(232, 30)
        Me.LblAppName.TabIndex = 1
        Me.LblAppName.Text = "Woodworker's Friend"
        '
        'LblVersion
        '
        Me.LblVersion.AutoSize = True
        Me.LblVersion.Font = New Font("Segoe UI", 10.0F, FontStyle.Regular, GraphicsUnit.Point)
        Me.LblVersion.Location = New Point(82, 46)
        Me.LblVersion.Name = "LblVersion"
        Me.LblVersion.Size = New Size(95, 19)
        Me.LblVersion.TabIndex = 2
        Me.LblVersion.Text = "Version 1.0.0"
        '
        'LblCopyright
        '
        Me.LblCopyright.AutoSize = True
        Me.LblCopyright.Location = New Point(12, 92)
        Me.LblCopyright.Name = "LblCopyright"
        Me.LblCopyright.Size = New Size(249, 15)
        Me.LblCopyright.TabIndex = 3
        Me.LblCopyright.Text = "Copyright © 2026 Dennis Maidon. MIT License."
        '
        'TxtDescription
        '
        Me.TxtDescription.BackColor = SystemColors.Control
        Me.TxtDescription.BorderStyle = BorderStyle.None
        Me.TxtDescription.Location = New Point(12, 120)
        Me.TxtDescription.Multiline = True
        Me.TxtDescription.Name = "TxtDescription"
        Me.TxtDescription.ReadOnly = True
        Me.TxtDescription.ScrollBars = ScrollBars.Vertical
        Me.TxtDescription.Size = New Size(460, 140)
        Me.TxtDescription.TabIndex = 4
        Me.TxtDescription.Text = "A comprehensive woodworking calculator and planning tool for professional woodworkers and enthusiasts. Features 18+ specialized calculators including:" & vbCrLf & vbCrLf & _
            "• Drawer & Door Calculators" & vbCrLf & _
            "• Board Feet & Epoxy Pour Calculators" & vbCrLf & _
            "• Joinery Suite (Mortise/Tenon, Dovetails, Box Joints, Biscuits)" & vbCrLf & _
            "• Materials & Finishing (Veneer, Glue, Finish Coverage)" & vbCrLf & _
            "• Wood Movement & Shelf Sag Calculators" & vbCrLf & _
            "• Miter Angle & Polygon Calculators" & vbCrLf & _
            "• Safety Tools (Router Speed, Blade Height, Dust Collection)" & vbCrLf & _
            "• Cut List Optimizer & Sanding Grit Progression" & vbCrLf & vbCrLf & _
            "Plus 50+ wood species database, hardware standards, and comprehensive help system."
        '
        'LnkGitHub
        '
        Me.LnkGitHub.AutoSize = True
        Me.LnkGitHub.Location = New Point(12, 280)
        Me.LnkGitHub.Name = "LnkGitHub"
        Me.LnkGitHub.Size = New Size(106, 15)
        Me.LnkGitHub.TabIndex = 5
        Me.LnkGitHub.TabStop = True
        Me.LnkGitHub.Text = "View on GitHub"
        '
        'LnkLicense
        '
        Me.LnkLicense.AutoSize = True
        Me.LnkLicense.Location = New Point(130, 280)
        Me.LnkLicense.Name = "LnkLicense"
        Me.LnkLicense.Size = New Size(83, 15)
        Me.LnkLicense.TabIndex = 6
        Me.LnkLicense.TabStop = True
        Me.LnkLicense.Text = "View License"
        '
        'LnkEmail
        '
        Me.LnkEmail.AutoSize = True
        Me.LnkEmail.Location = New Point(230, 280)
        Me.LnkEmail.Name = "LnkEmail"
        Me.LnkEmail.Size = New Size(95, 15)
        Me.LnkEmail.TabIndex = 9
        Me.LnkEmail.TabStop = True
        Me.LnkEmail.Text = "Email Support"
        '
        'BtnOK
        '
        Me.BtnOK.Location = New Point(397, 270)
        Me.BtnOK.Name = "BtnOK"
        Me.BtnOK.Size = New Size(75, 30)
        Me.BtnOK.TabIndex = 7
        Me.BtnOK.Text = "OK"
        Me.BtnOK.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New Font("Segoe UI", 9.0F, FontStyle.Italic, GraphicsUnit.Point)
        Me.Label1.ForeColor = SystemColors.GrayText
        Me.Label1.Location = New Point(12, 300)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New Size(373, 15)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "Built with .NET 10.0 | SQLite | Windows Forms | Made with ❤️ for woodworkers"
        '
        'FrmAbout
        '
        Me.AcceptButton = Me.BtnOK
        Me.AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        Me.AutoScaleMode = AutoScaleMode.Font
        Me.ClientSize = New Size(484, 326)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnOK)
        Me.Controls.Add(Me.LnkEmail)
        Me.Controls.Add(Me.LnkLicense)
        Me.Controls.Add(Me.LnkGitHub)
        Me.Controls.Add(Me.TxtDescription)
        Me.Controls.Add(Me.LblCopyright)
        Me.Controls.Add(Me.LblVersion)
        Me.Controls.Add(Me.LblAppName)
        Me.Controls.Add(Me.PictureBox1)
        Me.FormBorderStyle = FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAbout"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = FormStartPosition.CenterParent
        Me.Text = "About Woodworker's Friend"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()
    End Sub

    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents LblAppName As Label
    Friend WithEvents LblVersion As Label
    Friend WithEvents LblCopyright As Label
    Friend WithEvents TxtDescription As TextBox
    Friend WithEvents LnkGitHub As LinkLabel
    Friend WithEvents LnkLicense As LinkLabel
    Friend WithEvents LnkEmail As LinkLabel
    Friend WithEvents BtnOK As Button
    Friend WithEvents Label1 As Label
End Class
