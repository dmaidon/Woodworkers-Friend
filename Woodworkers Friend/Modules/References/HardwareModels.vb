' ============================================================================
' Last Updated: January 30, 2026
' Changes: Phase 7.2 - Hardware standards data models
' ============================================================================

''' <summary>
''' Represents a woodworking hardware standard with specifications
''' </summary>
Public Class HardwareStandard
    Public Property HardwareID As Integer
    Public Property Category As String
    Public Property Type As String
    Public Property Brand As String
    Public Property PartNumber As String
    Public Property Description As String
    Public Property Dimensions As String
    Public Property MountingRequirements As String
    Public Property WeightCapacity As String
    Public Property TypicalUses As String
    Public Property InstallationNotes As String
    Public Property PurchaseLink As String
    Public Property IsUserAdded As Boolean
    Public Property DateAdded As DateTime

    ''' <summary>
    ''' Gets a formatted display string for grid/list views
    ''' </summary>
    Public ReadOnly Property DisplayName As String
        Get
            Return $"{Type} - {Category}"
        End Get
    End Property

    ''' <summary>
    ''' Gets short description for list views
    ''' </summary>
    Public ReadOnly Property ShortDescription As String
        Get
            If String.IsNullOrEmpty(Description) Then Return ""
            Return If(Description.Length > 50, String.Concat(Description.AsSpan(0, 47), "..."), Description)
        End Get
    End Property

End Class

''' <summary>
''' Hardware category constants
''' </summary>
Public Class HardwareCategory
    Public Const Hinges As String = "Hinges"
    Public Const Slides As String = "Slides"
    Public Const Brackets As String = "Brackets"
    Public Const Fasteners As String = "Fasteners"
    Public Const Pulls As String = "Pulls & Knobs"
    Public Const Shelf As String = "Shelf Support"
    Public Const Legs As String = "Table Legs"
    Public Const Casters As String = "Casters & Wheels"
End Class
