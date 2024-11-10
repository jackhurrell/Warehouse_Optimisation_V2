Imports System.Windows
Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Warehouse_Optimisation

<TestClass>
Public Class Warehouse_testing
    <TestMethod>
    Sub Test_warehouse_creation()

        Dim test_warehouse_inputs As New Warehouse_inputs(1, 2, 3, 0, 5, 6, 7, 8, SiteType.Base_Warehouse, 9, 10, 11, 12)
        Dim test_warehouse = New Base_Warehouse(test_warehouse_inputs, 100)
        Assert.AreEqual(test_warehouse.warehouse_id, 1)
        Assert.AreEqual(test_warehouse.sim_length, 100)
        Assert.AreEqual(test_warehouse.period, 0)
        Assert.AreEqual(test_warehouse.start_day_inv(0), 2)
        Assert.AreEqual(test_warehouse.end_day_inventory(0), 2)
        Assert.AreEqual(test_warehouse.demand.Length, 101)
        Assert.AreEqual(test_warehouse.demand(0), 3)
        Assert.AreEqual(test_warehouse.reorder_amount, 6)
        Assert.AreEqual(test_warehouse.current_incoming_shipment, -1)
        Assert.AreEqual(test_warehouse.current_incoming_shipment_size, 0)
        Assert.AreEqual(test_warehouse.lost_sales.Length, 101)
        Assert.AreEqual(test_warehouse.reorder_point, 5)
        Assert.AreEqual(test_warehouse.reorder_report_history.Length, 101)
        Assert.AreEqual(test_warehouse.site_type, SiteType.Base_Warehouse)
        'Assert.AreEqual(test_warehouse.warehouse_requests.Count, 20001)
        Assert.AreEqual(test_warehouse.warehouse_shipment_amounts.Length, 101)
        Assert.AreEqual(test_warehouse.reorder_cost, 12)
        Assert.AreEqual(test_warehouse.profit_per_sale, 9)
        Assert.AreEqual(test_warehouse.storage_cost_per_pallet, 10)
        Assert.AreEqual(test_warehouse.items_per_pallet, 11)
    End Sub

    <TestMethod>
    Sub Test_warehouse_run_day()
        Dim test_warehouse_inputs As New Warehouse_inputs(1, 1000, 50, 0, 200, 10, 3, 0, SiteType.Base_Warehouse, 100, 10, 100, 10)
        Dim test_warehouse = New Base_Warehouse(test_warehouse_inputs, 100)
        test_warehouse.Run_day()
        test_warehouse.Calculate_reorder()
        Assert.AreEqual(test_warehouse.period, 1)
        Assert.AreEqual(test_warehouse.reorder_point, 200)
        Assert.AreEqual(test_warehouse.reorder_amount, 10)
        Assert.AreEqual(test_warehouse.end_day_inventory(0), 1000)
        Assert.AreEqual(test_warehouse.end_day_inventory(1), 950)
        Assert.AreEqual(test_warehouse.start_day_inv(1), 1000)
        Assert.AreEqual(test_warehouse.demand(1), 50)
        'Assert.AreEqual(test_warehouse.warehouse_requests(0), 0)
        Assert.AreEqual(test_warehouse.warehouse_shipment_amounts(0), 0)

        For i As Integer = 0 To 15
            test_warehouse.Run_day()
            test_warehouse.Calculate_reorder()
        Next
        Assert.AreEqual(test_warehouse.period, 17)
        Assert.AreEqual(test_warehouse.end_day_inventory(17), 150)
        Assert.AreEqual(test_warehouse.start_day_inv(17), 200)
        Assert.AreEqual(test_warehouse.demand(17), 50)
        Assert.AreEqual(test_warehouse.current_incoming_shipment, 19)
        Assert.AreEqual(test_warehouse.current_incoming_shipment_size, 1000)

        For i As Integer = 0 To 2
            test_warehouse.Run_day()
            test_warehouse.Calculate_reorder()
        Next

        Assert.AreEqual(test_warehouse.period, 20)
        Assert.AreEqual(test_warehouse.end_day_inventory(20), 1000)
        Assert.AreEqual(test_warehouse.start_day_inv(20), 1050)
        Assert.AreEqual(test_warehouse.demand(20), 50)
        Assert.AreEqual(test_warehouse.current_incoming_shipment, -1)
        Assert.AreEqual(test_warehouse.current_incoming_shipment_size, 0)

        For i As Integer = 0 To 79
            test_warehouse.Run_day()
            test_warehouse.Calculate_reorder()
        Next
        Assert.AreEqual(test_warehouse.period, 100)
        Assert.AreEqual(test_warehouse.end_day_inventory(100), 1000)
    End Sub

    <TestMethod>
    Sub Test_calc_reorder_levels()
        Dim test_warehouse_inputs As New Warehouse_inputs(1, 1000, 50, 0, 200, 10, 3, 0, SiteType.Base_Warehouse, 100, 10, 100, 10)
        Dim test_warehouse = New Base_Warehouse(test_warehouse_inputs, 10)

        test_warehouse.demand = {0, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100}
        test_warehouse.lost_sales = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        test_warehouse.warehouse_shipment_amounts = {0, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100}
        test_warehouse.warehouse_requests = New List(Of Integer) From {0, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100}
        Dim service_level As (Double, Double) = test_warehouse.Calc_service_levels()
        Assert.AreEqual(service_level.Item1, 1.0)
        Assert.AreEqual(service_level.Item2, 1.0)

        test_warehouse.demand = {0, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100}
        test_warehouse.lost_sales = {10, 10, 10, 10, 10, 10, 10, 10, 10, 10, 10}
        test_warehouse.warehouse_shipment_amounts = {0, 90, 90, 90, 90, 90, 90, 90, 90, 90, 90}
        test_warehouse.warehouse_requests = New List(Of Integer) From {0, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100}
        service_level = test_warehouse.Calc_service_levels()
        Assert.AreEqual(service_level.Item1, 0.9)
        Assert.AreEqual(service_level.Item2, 0.9)

        test_warehouse.demand = {0, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100}
        test_warehouse.lost_sales = {0, 0, 40, 0, 0, 0, 0, 90, 0, 0, 0, 0}
        test_warehouse.warehouse_shipment_amounts = {0, 90, 90, 90, 90, 0, 100, 0, 0, 100, 0}
        test_warehouse.warehouse_requests = New List(Of Integer) From {0, 100, 100, 100, 100, 100, 100, 100, 100, 100, 100}
        service_level = test_warehouse.Calc_service_levels()
        Assert.AreEqual(service_level.Item1, 1 - 130 / 1000)
        Assert.AreEqual(service_level.Item2, 560 / 1000)

        test_warehouse.demand = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        test_warehouse.lost_sales = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        test_warehouse.warehouse_shipment_amounts = {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        test_warehouse.warehouse_requests = New List(Of Integer) From {0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}
        service_level = test_warehouse.Calc_service_levels()
        Assert.AreEqual(service_level.Item1, -1)
        Assert.AreEqual(service_level.Item2, -1)
    End Sub

    <TestMethod>
    Sub Test_calc_reorder_paths()
        Dim test_warehouse_inputs As New Warehouse_inputs(1, 2, 3, 0, 5, 6, 7, 8, SiteType.Base_Warehouse, 9, 10, 11, 12)
        Dim test_warehouse = New Base_Warehouse(test_warehouse_inputs, 100)

        test_warehouse.reorder_report_history(0) = New Reorder_report(1, 0, 2, 1000, 8000, 3000, 12.4, True)
        test_warehouse.reorder_report_history(1) = New Reorder_report(1, 0, 3, 1000, 8000, 3000, 12.4, True)
        test_warehouse.reorder_report_history(2) = New Reorder_report(1, 0, 4, 1000, 8000, 3000, 12.4, True)
        test_warehouse.reorder_report_history(3) = New Reorder_report(1, 0, 2, 1000, 8000, 3000, 12.4, True)
        test_warehouse.reorder_report_history(4) = New Reorder_report(1, 0, 3, 1000, 8000, 3000, 12.4, True)
        test_warehouse.reorder_report_history(5) = New Reorder_report(1, 0, 4, 1000, 8000, 3000, 12.4, True)
        test_warehouse.reorder_report_history(6) = New Reorder_report(1, 0, 3, 1000, 8000, 3000, 12.4, True)
        test_warehouse.reorder_report_history(7) = New Reorder_report(1, 0, 3, 1000, 8000, 3000, 12.4, True)
        test_warehouse.reorder_report_history(8) = New Reorder_report(1, 0, 4, 1000, 8000, 3000, 12.4, True)

        Dim test_reorder_paths As Dictionary(Of Integer, Integer) = test_warehouse.Calc_reorder_paths

        Assert.AreEqual(test_reorder_paths(2), 2)
        Assert.AreEqual(test_reorder_paths(3), 4)
        Assert.AreEqual(test_reorder_paths(4), 3)

        test_warehouse.reorder_report_history(9) = New Reorder_report(1, 0, 6, 1000, 8000, 3000, 12.4, True)
        test_warehouse.reorder_report_history(10) = New Reorder_report(1, 0, 6, 1000, 8000, 3000, 12.4, True)
        test_warehouse.reorder_report_history(11) = New Reorder_report(1, 0, 6, 1000, 8000, 3000, 12.4, True)
        test_warehouse.reorder_report_history(12) = New Reorder_report(1, 0, 6, 1000, 8000, 3000, 12.4, True)
        test_warehouse.reorder_report_history(13) = New Reorder_report(1, 0, 6, 1000, 8000, 3000, 12.4, True)

        test_reorder_paths = test_warehouse.Calc_reorder_paths()

        Assert.AreEqual(test_reorder_paths(2), 2)
        Assert.AreEqual(test_reorder_paths(3), 4)
        Assert.AreEqual(test_reorder_paths(4), 3)
        Assert.AreEqual(test_reorder_paths(6), 5)

        Dim test_warehouse_2 = New Base_Warehouse(test_warehouse_inputs, 100)
        test_reorder_paths = test_warehouse_2.Calc_reorder_paths()
        Assert.AreEqual(test_reorder_paths.Count, 0)

    End Sub

    <TestMethod>
    Sub Test_calc_costs()
        Dim test_warehouse_inputs As New Warehouse_inputs(1, 2, 3, 0, 5, 6, 7, 8, SiteType.Base_Warehouse, 9, 10, 11, 12)
        Dim test_warehouse = New Base_Warehouse(test_warehouse_inputs, 10)

        Dim test_costs = test_warehouse.Calc_costs()

        Assert.AreEqual(test_costs.Item1, 0)
        Assert.AreEqual(test_costs.Item2, 0)
        Assert.AreEqual(test_costs.Item3, 0)

        test_warehouse.reorder_report_history(0) = New Reorder_report(1, 0, 6, 1000, 8000, 3000, 12.4, True)
        test_warehouse.reorder_report_history(1) = New Reorder_report(1, 0, 6, 1000, 8000, 3000, 7.6, True)
        test_warehouse.reorder_report_history(2) = New Reorder_report(1, 0, 6, 1000, 8000, 3000, 10, True)
        test_warehouse.reorder_report_history(3) = New Reorder_report(1, 0, 6, 1000, 8000, 3000, 9, True)
        test_warehouse.reorder_report_history(4) = New Reorder_report(1, 0, 6, 1000, 8000, 3000, 8, True)
        test_warehouse.reorder_report_history(5) = New Reorder_report(1, 0, 6, 1000, 8000, 3000, 12, True)
        test_warehouse.reorder_report_history(6) = New Reorder_report(1, 0, 6, 1000, 8000, 3000, 12, True)

        test_costs = test_warehouse.Calc_costs()
        Assert.AreEqual(test_costs.Item1, 71)

        ''This is testing the lost sales costs - it adds an 11 as the first element 
        ''to test that the first element isnt used
        test_warehouse.lost_sales = {11, 0, 1, 2, 3, 124, 12, 0, 0, 43, 0}
        test_costs = test_warehouse.Calc_costs()

        Assert.AreEqual(test_costs.Item3, 185 * 9)

        test_warehouse.end_day_inventory = {0, 1, 2, 3, 4, 12, 11, 10, 2, 8, 3}
        test_costs = test_warehouse.Calc_costs()
        Assert.AreEqual(test_costs.Item2, (2 * 10) + (1 * 10))

        Dim test_warehouse_inputs_2 As New Warehouse_inputs(1, 2, 3, 0, 5, 6, 7, 8, SiteType.Base_Warehouse, 9, 10, 11, 12)
        Dim test_warehouse_2 = New Base_Warehouse(test_warehouse_inputs, 100)


        For i As Integer = 0 To 13
            test_warehouse_2.end_day_inventory((i * 7) + 2) = 12
        Next

        test_costs = test_warehouse_2.Calc_costs()
        Assert.AreEqual(test_costs.Item2, 2 * 14 * 10)

    End Sub


    <TestMethod>
    Sub test_calc_total_demand()
        Dim test_warehouse_inputs As New Warehouse_inputs(1, 2, 3, 0, 5, 6, 7, 8, SiteType.Base_Warehouse, 9, 10, 11, 12)
        Dim test_warehouse = New Base_Warehouse(test_warehouse_inputs, 100)
        test_warehouse.demand = {10, 10, 10, 10, 10, 10}
        Assert.AreEqual(test_warehouse.calc_total_demand(), 50)

        test_warehouse.demand = {10, 1, 2, 3, 4, 5}
        Assert.AreEqual(test_warehouse.calc_total_demand(), 15)
    End Sub
End Class