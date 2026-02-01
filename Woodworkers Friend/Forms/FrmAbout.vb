Imports System.Diagnostics
Imports System.Reflection

Public Class FrmAbout

    Private Sub FrmAbout_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            ' Get version info from assembly
            Dim assembly As Assembly = Assembly.GetExecutingAssembly()
            Dim version As Version = assembly.GetName().Version
            Dim fileVersionInfo As FileVersionInfo = FileVersionInfo.GetVersionInfo(assembly.Location)

            ' Set version label
            LblVersion.Text = $"Version {version.Major}.{version.Minor}.{version.Build}"

            ' Set copyright
            Dim copyrightAttr = assembly.GetCustomAttribute(Of AssemblyCopyrightAttribute)()
            If copyrightAttr IsNot Nothing Then
                LblCopyright.Text = copyrightAttr.Copyright
            End If

            ' Set description
            Dim descAttr = assembly.GetCustomAttribute(Of AssemblyDescriptionAttribute)()
            If descAttr IsNot Nothing Then
                TxtDescription.Text = descAttr.Description
            End If
        Catch ex As Exception
            ErrorHandler.LogError(ex, "FrmAbout_Load")
        End Try
    End Sub

    Private Sub LnkGitHub_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LnkGitHub.LinkClicked
        Try
            Process.Start(New ProcessStartInfo("https://github.com/dmaidon/Woodworkers-Friend") With {.UseShellExecute = True})
        Catch ex As Exception
            ErrorHandler.LogError(ex, "LnkGitHub_LinkClicked")
        End Try
    End Sub

    Private Sub LnkLicense_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LnkLicense.LinkClicked
        Try
            Process.Start(New ProcessStartInfo("https://github.com/dmaidon/Woodworkers-Friend/blob/master/LICENSE") With {.UseShellExecute = True})
        Catch ex As Exception
            ErrorHandler.LogError(ex, "LnkLicense_LinkClicked")
        End Try
    End Sub

    Private Sub LnkEmail_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LnkEmail.LinkClicked
        Try
            Process.Start(New ProcessStartInfo($"mailto:{SupportEmail}?subject=Woodworker's Friend Support") With {.UseShellExecute = True})
        Catch ex As Exception
            ErrorHandler.LogError(ex, "LnkEmail_LinkClicked")
            MessageBox.Show($"Please send support inquiries to:{vbCrLf}{SupportEmail}", "Email Support", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
    End Sub

    Private Sub BtnOK_Click(sender As Object, e As EventArgs) Handles BtnOK.Click
        Me.Close()
    End Sub

End Class
