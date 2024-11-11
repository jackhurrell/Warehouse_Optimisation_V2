''' <summary>
''' This interface is used a large amount of different ways to generate demand to exist.
''' All each option has to do is be able to generate a demand array of integers based of the number of demand days
''' requested.
''' </summary>

Public Interface IDemandGenerator

    Function generate_demand(NumDays As Integer) As Integer()

    Function returnmean() As Double

    Function returnstd() As Double


End Interface
