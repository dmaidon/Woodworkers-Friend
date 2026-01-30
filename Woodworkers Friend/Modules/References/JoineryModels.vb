' ============================================================================
' Last Updated: January 30, 2026
' Changes: Phase 7.1 - Joinery reference data models
' ============================================================================

''' <summary>
''' Represents a woodworking joinery type with properties and characteristics
''' </summary>
Public Class JoineryType
    Public Property JoineryID As Integer
    Public Property Name As String
    Public Property Category As String
    Public Property StrengthRating As Integer ' 1-10 scale
    Public Property DifficultyLevel As String ' Beginner, Intermediate, Advanced
    Public Property Description As String
    Public Property TypicalUses As String
    Public Property RequiredTools As String
    Public Property StrengthCharacteristics As String
    Public Property GlueRequired As Boolean
    Public Property ReinforcementOptions As String
    Public Property HistoricalNotes As String
    Public Property DiagramFileName As String
    Public Property IsUserAdded As Boolean
    Public Property DateAdded As DateTime

    ''' <summary>
    ''' Gets a formatted display string for grid/list views
    ''' </summary>
    Public ReadOnly Property DisplayName As String
        Get
            Return $"{Name} ({Category})"
        End Get
    End Property

    ''' <summary>
    ''' Gets difficulty color for UI display
    ''' </summary>
    Public ReadOnly Property DifficultyColor As Color
        Get
            If String.IsNullOrEmpty(DifficultyLevel) Then Return Color.Gray

            Select Case DifficultyLevel.ToLower()
                Case "beginner"
                    Return Color.Green
                Case "intermediate"
                    Return Color.Orange
                Case "advanced"
                    Return Color.Red
                Case Else
                    Return Color.Gray
            End Select
        End Get
    End Property

    ''' <summary>
    ''' Gets strength rating as stars (★★★☆☆)
    ''' </summary>
    Public ReadOnly Property StrengthStars As String
        Get
            If StrengthRating <= 0 OrElse StrengthRating > 10 Then Return "N/A"
            Dim filled = New String("★"c, StrengthRating)
            Dim empty = New String("☆"c, 10 - StrengthRating)
            Return filled & empty
        End Get
    End Property
End Class

''' <summary>
''' Joinery category constants
''' </summary>
Public Class JoineryCategory
    Public Const Frame As String = "Frame"
    Public Const Carcass As String = "Carcass"
    Public Const Box As String = "Box"
    Public Const Edge As String = "Edge"
    Public Const Corner As String = "Corner"
    Public Const Reinforcement As String = "Reinforcement"
    Public Const Traditional As String = "Traditional"
    Public Const Modern As String = "Modern"
End Class

''' <summary>
''' Difficulty level constants
''' </summary>
Public Class JoineryDifficulty
    Public Const Beginner As String = "Beginner"
    Public Const Intermediate As String = "Intermediate"
    Public Const Advanced As String = "Advanced"
End Class
