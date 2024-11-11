Public Class HistoricalDemand
    Implements IDemandGenerator

    Public NetworkID As String
    Public SKU As String
    Public WarehouseID As Integer
    Public Demand As Integer()
    Private connectionString As String

    Public Sub New(NetworkID As String, SKU As String, WarehouseID As String)

        '''PRIVATE INFORMATION TO BE REMOVED WHEN ADDED TO THE REPOSITORY
        Dim DatabasePassword As String = "###"
        Me.connectionString = "Server=180.150.46.156;Database=prophit_FM_DEMO;User Id=Prophit;Password=" & DatabasePassword & ";Encrypt=False"


    End Sub

    Public Function returnmean() As Double Implements IDemandGenerator.returnmean
        Return Me.Demand.Sum / Me.Demand.Length
    End Function

    Public Function returnstd() As Double Implements IDemandGenerator.returnstd
        Throw New NotImplementedException()
    End Function

    Private Function IDemandGenerator_generate_demand(NumDays As Integer) As Integer() Implements IDemandGenerator.generate_demand
        Throw New NotImplementedException()
    End Function



End Class
