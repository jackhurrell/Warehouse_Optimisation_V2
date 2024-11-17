Imports System
Imports System.Linq

Public Class HistoricalDemand
    Implements IDemandGenerator

    Public Demand As Integer()
    Private connectionString As String

    Public Sub New(Demand As Integer())
        Me.Demand = Demand
    End Sub

    Public Function returnmean() As Double Implements IDemandGenerator.returnmean
        If Demand Is Nothing Or Demand.Length = 0 Then
            Return 0
        End If

        Return Me.Demand.Sum / Me.Demand.Length
    End Function

    Public Function returnstd() As Double Implements IDemandGenerator.returnstd
        If Demand Is Nothing Or Demand.Length < 2 Then
            Return 0
        End If

        Dim Mean As Double = Me.returnmean()
        Dim SumOfSquares As Double = Me.Demand.Sum(Function(num) Math.Pow(num - Mean, 2))
        Return Math.Sqrt(SumOfSquares / (Demand.Length - 1))
    End Function


    Private Function IDemandGenerator_generate_demand(NumDays As Integer) As Integer() Implements IDemandGenerator.generate_demand

        Dim NewDemand = New Integer(NumDays) {}
        For i As Integer = 0 To NumDays
            NewDemand(i) = Demand(i Mod Demand.Length)
        Next

        Return NewDemand
    End Function



End Class