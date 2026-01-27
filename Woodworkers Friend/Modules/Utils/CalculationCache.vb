' ============================================================================
' Last Updated: January 27, 2026
' Changes: Initial creation - Generic calculation cache with LRU eviction,
'          expiration support, and specialized polygon cache for performance
' ============================================================================

''' <summary>
''' Generic cache for calculation results to avoid redundant computations
''' Thread-safe implementation with configurable expiration
''' </summary>
Public Class CalculationCache(Of TKey, TValue)

    Private Class CacheEntry
        Public Property Value As TValue
        Public Property Timestamp As DateTime
        Public Property HitCount As Integer
    End Class

    Private ReadOnly _cache As New Dictionary(Of TKey, CacheEntry)
    Private ReadOnly _maxSize As Integer
    Private ReadOnly _expirationMinutes As Double
    Private ReadOnly _lock As New Object()

    ''' <summary>
    ''' Creates a new calculation cache
    ''' </summary>
    ''' <param name="maxSize">Maximum number of entries to cache</param>
    ''' <param name="expirationMinutes">Minutes before cache entries expire (0 = no expiration)</param>
    Public Sub New(Optional maxSize As Integer = 100, Optional expirationMinutes As Double = 5.0)
        _maxSize = maxSize
        _expirationMinutes = expirationMinutes
    End Sub

    ''' <summary>
    ''' Attempts to get a cached value
    ''' </summary>
    Public Function TryGetValue(key As TKey, ByRef value As TValue) As Boolean
        SyncLock _lock

            Dim entry As CacheEntry = Nothing

            If _cache.TryGetValue(key, entry) Then

                ' Check if expired
                If _expirationMinutes > 0 AndAlso
                   DateTime.Now.Subtract(entry.Timestamp).TotalMinutes > _expirationMinutes Then
                    _cache.Remove(key)
                    Return False
                End If

                entry.HitCount += 1
                value = entry.Value
                Return True
            End If

            Return False
        End SyncLock
    End Function

    ''' <summary>
    ''' Adds or updates a cache entry
    ''' </summary>
    Public Sub Add(key As TKey, val As TValue)
        SyncLock _lock
            ' If cache is full, remove least recently used item
            If _cache.Count >= _maxSize AndAlso Not _cache.ContainsKey(key) Then
                Dim lruKey = _cache.OrderBy(Function(x) x.Value.HitCount).First().Key
                _cache.Remove(lruKey)
            End If

            _cache(key) = New CacheEntry With {
                .Value = val,
                .Timestamp = DateTime.Now,
                .HitCount = 0
            }
        End SyncLock
    End Sub

    ''' <summary>
    ''' Clears all cache entries
    ''' </summary>
    Public Sub Clear()
        SyncLock _lock
            _cache.Clear()
        End SyncLock
    End Sub

    ''' <summary>
    ''' Gets cache statistics
    ''' </summary>
    Public Function GetStatistics() As (Count As Integer, TotalHits As Integer)
        SyncLock _lock
            Return (_cache.Count, _cache.Values.Sum(Function(x) x.HitCount))
        End SyncLock
    End Function

End Class

''' <summary>
''' Specialized cache for polygon calculations
''' </summary>
Public Class PolygonCache

    Private Structure PolygonKey
        Public NSides As Integer
        Public Radius As Single
        Public CenterX As Single
        Public CenterY As Single

        Public Overrides Function GetHashCode() As Integer
            Return NSides.GetHashCode() Xor
                   Radius.GetHashCode() Xor
                   CenterX.GetHashCode() Xor
                   CenterY.GetHashCode()
        End Function

    End Structure

    Private _cachedKey As PolygonKey
    Private _cachedPoints() As PointF
    Private _cacheValid As Boolean = False

    ''' <summary>
    ''' Gets polygon points from cache or calculates if needed
    ''' </summary>
    Public Function GetPoints(nSides As Integer,
                             radius As Single,
                             centerX As Single,
                             centerY As Single,
                             calculator As Func(Of Integer, Single, Single, Single, PointF())) As PointF()

        Dim key As New PolygonKey With {
            .NSides = nSides,
            .Radius = radius,
            .CenterX = centerX,
            .CenterY = centerY
        }

        ' Check if cached
        If _cacheValid AndAlso
           _cachedKey.NSides = key.NSides AndAlso
           Math.Abs(_cachedKey.Radius - key.Radius) < 0.01F AndAlso
           Math.Abs(_cachedKey.CenterX - key.CenterX) < 0.01F AndAlso
           Math.Abs(_cachedKey.CenterY - key.CenterY) < 0.01F Then
            Return _cachedPoints
        End If

        ' Calculate and cache
        _cachedPoints = calculator(nSides, radius, centerX, centerY)
        _cachedKey = key
        _cacheValid = True

        Return _cachedPoints
    End Function

    ''' <summary>
    ''' Invalidates the cache
    ''' </summary>
    Public Sub Invalidate()
        _cacheValid = False
    End Sub

End Class
