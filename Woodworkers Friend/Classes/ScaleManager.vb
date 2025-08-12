Public Class ScaleManager

    Public Enum ScaleType
        Imperial
        Metric
    End Enum

    Public Property CurrentScale As ScaleType = ScaleType.Imperial

    ' Conversion factors
    Public Const InchToMillimeter As Double = 25.4

    Public Const FootToMeter As Double = 0.3048
    Public Const GallonToLiter As Double = 3.78541

    ' Conversion methods
    Public Shared Function ToMetricInches(inches As Double) As Double
        Return inches * InchToMillimeter
    End Function

    Public Shared Function ToImperialMillimeters(mm As Double) As Double
        Return mm / InchToMillimeter
    End Function

    ' Add more conversion methods as needed...

    ' Set scale and notify
    Public Sub SetScale(scale As ScaleType)
        CurrentScale = scale
        ' Optionally raise an event or callback here
    End Sub

End Class