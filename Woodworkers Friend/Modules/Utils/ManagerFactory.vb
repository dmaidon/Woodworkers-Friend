' ============================================================================
' Last Updated: {Current Date}
' Changes: Initial creation - Simple dependency injection factory with service
'          locator pattern, automatic disposal, and type-safe retrieval
' ============================================================================

''' <summary>
''' Simple factory for creating and managing application service instances
''' Provides centralized service creation and lifecycle management
''' </summary>
Public Class ManagerFactory

    Private Shared _instance As ManagerFactory
    Private ReadOnly _managers As New Dictionary(Of Type, Object)
    Private ReadOnly _lock As New Object()

    Private Sub New()
        ' Private constructor for singleton
    End Sub

    ''' <summary>
    ''' Gets the singleton instance of the factory
    ''' </summary>
    Public Shared ReadOnly Property Instance As ManagerFactory
        Get
            If _instance Is Nothing Then
                _instance = New ManagerFactory()
            End If
            Return _instance
        End Get
    End Property

    ''' <summary>
    ''' Registers a manager instance
    ''' </summary>
    Public Sub Register(Of T)(instance As T)
        SyncLock _lock
            _managers(GetType(T)) = instance
        End SyncLock
    End Sub

    ''' <summary>
    ''' Gets or creates a manager instance
    ''' </summary>
    Public Function [Get](Of T)() As T
        SyncLock _lock
            Dim managerType = GetType(T)

            Dim value As Object = Nothing
            If _managers.TryGetValue(managerType, value) Then
                Return CType(value, T)
            End If

            ' Try to create instance with parameterless constructor
            Try
                Dim instance = Activator.CreateInstance(Of T)()
                _managers(managerType) = instance
                Return instance
            Catch ex As Exception
                Throw New InvalidOperationException(
                    $"Cannot create instance of {managerType.Name}. " &
                    "Please register an instance manually.", ex)
            End Try
        End SyncLock
    End Function

    ''' <summary>
    ''' Checks if a manager is registered
    ''' </summary>
    Public Function IsRegistered(Of T)() As Boolean
        SyncLock _lock
            Return _managers.ContainsKey(GetType(T))
        End SyncLock
    End Function

    ''' <summary>
    ''' Clears all registered managers
    ''' </summary>
    Public Sub Clear()
        SyncLock _lock
            ' Dispose any IDisposable managers
            For Each manager In _managers.Values
                Dim disposable = TryCast(manager, IDisposable)
                If disposable IsNot Nothing Then
                    Try
                        disposable.Dispose()
                    Catch ex As Exception
                        ErrorHandler.LogError(ex, "ManagerFactory.Clear - disposing manager")
                    End Try
                End If
            Next

            _managers.Clear()
        End SyncLock
    End Sub

    ''' <summary>
    ''' Gets all registered manager types
    ''' </summary>
    Public Function GetRegisteredTypes() As Type()
        SyncLock _lock
            Return _managers.Keys.ToArray()
        End SyncLock
    End Function

End Class

''' <summary>
''' Service locator for quick access to common managers
''' </summary>
Public Module ServiceLocator

    ''' <summary>
    ''' Gets the ValidationService instance
    ''' </summary>
    Public Function GetValidationService() As ValidationService
        Return ManagerFactory.Instance.Get(Of ValidationService)()
    End Function

    ''' <summary>
    ''' Gets the ErrorHandler instance
    ''' </summary>
    Public Function GetErrorHandler() As ErrorHandler
        Return ManagerFactory.Instance.Get(Of ErrorHandler)()
    End Function

End Module