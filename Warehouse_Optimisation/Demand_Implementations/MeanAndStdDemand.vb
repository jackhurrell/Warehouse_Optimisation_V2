Public Class MeandAndSTDDemand
    Implements IDemandGenerator

    Public mean As Double
    Public std As Double

    Public Sub New(mean As Double, std As Double)
        Me.mean = mean
        Me.std = std
    End Sub

    Public Function generate_demand(NumDays As Integer) As Integer() Implements IDemandGenerator.generate_demand
        Return Utils.generate_normal_random_ints(Me.mean, Me.std, NumDays + 1)
    End Function

    Public Function returnmean() As Double Implements IDemandGenerator.returnmean
        Return Me.mean
    End Function

    Public Function returnstd() As Double Implements IDemandGenerator.returnstd
        Return Me.std
    End Function
End Class
