Friend Module FolderRoutines

    Friend Sub CreateprogramFolders()

        Dim FolderList As New List(Of String) From {
            LogDir,
            SetDir,
            ProjectDir,
            DataDir,
            TempDir
        }

        Try

            For Each folderPath As String In FolderList
                If Not IO.Directory.Exists(folderPath) Then
                    IO.Directory.CreateDirectory(folderPath)
                End If
            Next
        Catch ex As Exception
            MsgBox($"Error creating program folders: {ex.Message}", MsgBoxStyle.Critical, "Folder Creation Error")

        End Try

    End Sub

End Module