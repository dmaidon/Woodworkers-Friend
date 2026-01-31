' ============================================================================
' Last Updated: January 30, 2026
' Changes: OBSOLETE - Wood Movement now uses unified SQLite database via
'          DatabaseManager.Instance.GetAllWoodSpecies(). This file is kept
'          as a reference and for the WoodSpecies class used by
'          WoodMovementCalculator. Do NOT add new species here.
' ============================================================================

''' <summary>
''' OBSOLETE: Old wood species class for WoodMovementCalculator.
''' Use the unified WoodSpecies from DatabaseModels.vb instead.
''' This class is kept ONLY for legacy hardcoded fallback data.
''' The new WoodSpecies has all these properties via backward-compatibility.
''' </summary>
<Obsolete("Use unified WoodSpecies from DatabaseModels.vb via DatabaseManager.Instance.GetWoodSpeciesList()")>
Public Class WoodSpeciesLegacy
    Public Property Name As String
    Public Property TangentialShrinkage As Double  ' % per 1% MC change (flat sawn)
    Public Property RadialShrinkage As Double      ' % per 1% MC change (quarter sawn)
    Public Property Density As Double              ' lbs/ft³
    Public Property IsHardwood As Boolean
End Class

''' <summary>
''' OBSOLETE: Database of common wood species and their properties.
''' Wood Movement now loads species from unified SQLite database via DatabaseManager.
''' This module is kept as fallback only. Do NOT add new species here.
''' </summary>
<Obsolete("Use DatabaseManager.Instance.GetAllWoodSpecies() instead")>
Public Module WoodSpeciesDatabase
    
    Private _species As List(Of WoodSpeciesLegacy)
    
    ''' <summary>
    ''' Gets all available wood species
    ''' </summary>
    Public ReadOnly Property AllSpecies As List(Of WoodSpeciesLegacy)
        Get
            If _species Is Nothing Then
                InitializeSpecies()
            End If
            Return _species
        End Get
    End Property
    
    ''' <summary>
    ''' Initializes the wood species database
    ''' </summary>
    Private Sub InitializeSpecies()
        _species = New List(Of WoodSpeciesLegacy) From {
            New WoodSpeciesLegacy With {.Name = "White Oak", .TangentialShrinkage = 10.8, .RadialShrinkage = 5.6, .Density = 47, .IsHardwood = True},
            New WoodSpeciesLegacy With {.Name = "Red Oak", .TangentialShrinkage = 8.6, .RadialShrinkage = 4.0, .Density = 44, .IsHardwood = True},
            New WoodSpeciesLegacy With {.Name = "Hard Maple", .TangentialShrinkage = 9.9, .RadialShrinkage = 4.8, .Density = 44, .IsHardwood = True},
            New WoodSpeciesLegacy With {.Name = "Soft Maple", .TangentialShrinkage = 7.1, .RadialShrinkage = 3.7, .Density = 38, .IsHardwood = True},
            New WoodSpeciesLegacy With {.Name = "Cherry", .TangentialShrinkage = 7.1, .RadialShrinkage = 3.7, .Density = 35, .IsHardwood = True},
            New WoodSpeciesLegacy With {.Name = "Black Walnut", .TangentialShrinkage = 7.8, .RadialShrinkage = 5.5, .Density = 38, .IsHardwood = True},
            New WoodSpeciesLegacy With {.Name = "Ash", .TangentialShrinkage = 7.8, .RadialShrinkage = 4.9, .Density = 42, .IsHardwood = True},
            New WoodSpeciesLegacy With {.Name = "Birch (Yellow)", .TangentialShrinkage = 9.5, .RadialShrinkage = 7.3, .Density = 43, .IsHardwood = True},
            New WoodSpeciesLegacy With {.Name = "Hickory", .TangentialShrinkage = 10.5, .RadialShrinkage = 7.0, .Density = 51, .IsHardwood = True},
            New WoodSpeciesLegacy With {.Name = "Mahogany", .TangentialShrinkage = 4.1, .RadialShrinkage = 2.6, .Density = 31, .IsHardwood = True},
            New WoodSpeciesLegacy With {.Name = "Poplar", .TangentialShrinkage = 8.2, .RadialShrinkage = 4.6, .Density = 28, .IsHardwood = True},
            New WoodSpeciesLegacy With {.Name = "Beech", .TangentialShrinkage = 11.9, .RadialShrinkage = 5.5, .Density = 45, .IsHardwood = True},
            New WoodSpeciesLegacy With {.Name = "Pine (Eastern White)", .TangentialShrinkage = 6.1, .RadialShrinkage = 2.1, .Density = 25, .IsHardwood = False},
            New WoodSpeciesLegacy With {.Name = "Pine (Southern Yellow)", .TangentialShrinkage = 7.7, .RadialShrinkage = 4.8, .Density = 36, .IsHardwood = False},
            New WoodSpeciesLegacy With {.Name = "Douglas Fir", .TangentialShrinkage = 7.6, .RadialShrinkage = 4.8, .Density = 34, .IsHardwood = False},
            New WoodSpeciesLegacy With {.Name = "Cedar (Western Red)", .TangentialShrinkage = 5.0, .RadialShrinkage = 2.4, .Density = 23, .IsHardwood = False},
            New WoodSpeciesLegacy With {.Name = "Spruce", .TangentialShrinkage = 7.8, .RadialShrinkage = 3.8, .Density = 25, .IsHardwood = False},
            New WoodSpeciesLegacy With {.Name = "Redwood", .TangentialShrinkage = 4.4, .RadialShrinkage = 2.6, .Density = 28, .IsHardwood = False}
        }
    End Sub
    
    ''' <summary>
    ''' Gets a species by name
    ''' </summary>
    Public Function GetSpeciesByName(name As String) As WoodSpeciesLegacy
        Return AllSpecies.FirstOrDefault(Function(s) s.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
    End Function
    
End Module
