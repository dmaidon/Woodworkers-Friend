' ============================================================================
' Last Updated: {Current Date}
' Changes: Initial creation - Reentrancy prevention utilities with TryEnter/Exit
'          pattern for preventing recursive calls in event handlers
' ============================================================================

''' <summary>
''' Note: Due to VB.NET limitations with ByRef in closures,
''' it's recommended to use ReentrancyGuardHelper instead
''' </summary>
Public Class ReentrancyGuard
    Implements IDisposable

    ' This class is provided for API compatibility but has limitations in VB.NET
    ' Use ReentrancyGuardHelper.TryEnter/Exit pattern instead

    Private _disposed As Boolean = False

    Public Sub New()
        ' Simplified constructor - for VB.NET use ReentrancyGuardHelper instead
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        If Not _disposed Then
            _disposed = True
        End If
        GC.SuppressFinalize(Me)
    End Sub

End Class

''' <summary>
''' Provides a silent reentrancy guard that returns early instead of throwing
''' Useful when reentrancy is expected and should be silently ignored
''' </summary>
''' <example>
''' If Not ReentrancyGuardHelper.TryEnter(_isUpdating) Then Return
''' Try
'''     ' Your code here
''' Finally
'''     ReentrancyGuardHelper.Exit(_isUpdating)
''' End Try
''' </example>
Public Class ReentrancyGuardHelper

    ''' <summary>
    ''' Attempts to enter a guarded section
    ''' </summary>
    ''' <returns>True if entry was successful, False if already in use</returns>
    Public Shared Function TryEnter(ByRef flag As Boolean) As Boolean
        If flag Then
            Return False
        End If
        flag = True
        Return True
    End Function

    ''' <summary>
    ''' Exits a guarded section
    ''' </summary>
    Public Shared Sub [Exit](ByRef flag As Boolean)
        flag = False
    End Sub

End Class