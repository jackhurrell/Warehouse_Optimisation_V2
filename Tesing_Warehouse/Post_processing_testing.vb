Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Warehouse_Optimisation

<TestClass>
Public Class Post_processing_testing
    <TestMethod>
    Sub test_calculate_averages_by_warehouse()
        Dim twice_indexed_list As New List(Of List(Of Double))
        twice_indexed_list.Add(New List(Of Double) From {1, 2, 3, 4, 5})
        twice_indexed_list.Add(New List(Of Double) From {2, 3, 4, 5, 6})
        twice_indexed_list.Add(New List(Of Double) From {3, 4, 5, 6, 7})
        twice_indexed_list.Add(New List(Of Double) From {4, 5, 6, 7, 8})
        twice_indexed_list.Add(New List(Of Double) From {5, 6, 7, 8, 9})

        Dim averages = Post_processing.calculate_averages_by_warehouse(twice_indexed_list)

        Assert.AreEqual(averages(0), 3)
        Assert.AreEqual(averages(1), 4)
        Assert.AreEqual(averages(2), 5)
        Assert.AreEqual(averages(3), 6)
        Assert.AreEqual(averages(4), 7)
    End Sub

End Class
