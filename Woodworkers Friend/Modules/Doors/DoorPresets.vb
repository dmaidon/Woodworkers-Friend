Namespace WwFriend.Modules.Doors

    Public Enum DoorPresetType
        Kitchen
        Bathroom
        Office
    End Enum

    Public Structure DoorPresetData
        Public OpeningHeight As String
        Public OpeningWidth As String
        Public StileWidth As String
        Public RailWidth As String
        Public GrooveDepth As String
        Public ExpansionGap As String
        Public IsOverlay As Boolean
        Public IsSingleDoor As Boolean
    End Structure

    Public NotInheritable Class DoorPresets

        Private Sub New()
        End Sub

        Public Shared Function GetPreset(presetType As DoorPresetType) As DoorPresetData
            Select Case presetType
                Case DoorPresetType.Kitchen
                    Return New DoorPresetData With {
                        .OpeningHeight = "30",
                        .OpeningWidth = "24",
                        .StileWidth = "2.25",
                        .RailWidth = "2.25",
                        .GrooveDepth = "0.375",
                        .ExpansionGap = "0.0625",
                        .IsOverlay = True,
                        .IsSingleDoor = True
                    }
                Case DoorPresetType.Bathroom
                    Return New DoorPresetData With {
                        .OpeningHeight = "24",
                        .OpeningWidth = "18",
                        .StileWidth = "2",
                        .RailWidth = "2",
                        .GrooveDepth = "0.375",
                        .ExpansionGap = "0.0625",
                        .IsOverlay = True,
                        .IsSingleDoor = True
                    }
                Case DoorPresetType.Office
                    Return New DoorPresetData With {
                        .OpeningHeight = "28",
                        .OpeningWidth = "32",
                        .StileWidth = "2",
                        .RailWidth = "2.5",
                        .GrooveDepth = "0.375",
                        .ExpansionGap = "0.0625",
                        .IsOverlay = True,
                        .IsSingleDoor = False
                    }
                Case Else
                    Return New DoorPresetData()
            End Select
        End Function

    End Class

End Namespace