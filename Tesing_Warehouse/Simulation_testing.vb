Imports System.Numerics
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Warehouse_Optimisation

<TestClass>
Public Class Simulation_testing
    <TestMethod>
    Sub test_creating_simulation()
        Dim Warehouse_inputs_1 = New Warehouse_inputs(1, 24000, 0, 0, 18000, 20, 6, 3, SiteType.Base_Warehouse, 20, 700, 2000, 200)
        Dim Warehouse_inputs_2 = New Warehouse_inputs(2, 25000, 1000, 300, 13000, 10, -1, -1, SiteType.Dependent_Warehouse, 20, 560, 2000, 200)
        Dim Warehouse_inputs_3 = New Warehouse_inputs(3, 10000, 1000, 700, 7500, 5, -1, -1, SiteType.Dependent_Warehouse, 20, 840, 2000, 120)
        Dim warehouse_list = New List(Of Warehouse_inputs) From {Warehouse_inputs_1, Warehouse_inputs_2, Warehouse_inputs_3}

        Dim reorder_inputs_1 = (2, 1, New Reorder_inputs(5, 1, 200))
        Dim reorder_inputs_2 = (3, 1, New Reorder_inputs(4, 1, 100))
        Dim reorder_inputs_3 = (3, 2, New Reorder_inputs(2, 1, 200))
        Dim reorder_inputs_list = New List(Of (Integer, Integer, Reorder_inputs)) From {reorder_inputs_1, reorder_inputs_2, reorder_inputs_3}

        Dim reorder_order As New List(Of Integer) From {3, 2, 1}
        Dim test_simulation As New Simulation(warehouse_list, 200, reorder_order, reorder_inputs_list)

        Assert.AreEqual(test_simulation.list_of_warehouses.Count, 3)
        test_simulation.Run_simulation(200)

    End Sub

End Class
