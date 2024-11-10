Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Warehouse_Optimisation

<TestClass>
Public Class Base_Warehouse_tesing
    <TestMethod>
    Sub Test_Calculate_reorder()
        Dim test_warehouse_inputs As New Warehouse_inputs(1, 2, 3, 0, 5, 6, 3, 0, SiteType.Base_Warehouse, 9, 10, 11, 12)
        Dim test_base_warehouse As New Base_Warehouse(test_warehouse_inputs, 1000)

        test_base_warehouse.end_day_inventory(0) = 0
        test_base_warehouse.reorder_point = 50
        test_base_warehouse.reorder_amount = 10
        test_base_warehouse.reorder_cost = 1
        test_base_warehouse.Calculate_reorder()

        Assert.AreEqual(test_base_warehouse.current_incoming_shipment, 3)
        Assert.AreEqual(test_base_warehouse.current_incoming_shipment_size, 11 * 10)
        Assert.AreEqual(test_base_warehouse.reorder_report_history(0).reorder_amount, 10)
        Assert.AreEqual(test_base_warehouse.reorder_report_history(0).reorder_cost, 10)
        Assert.AreEqual(test_base_warehouse.reorder_report_history(0).reordered_from, -1)
        Assert.AreEqual(test_base_warehouse.reorder_report_history(0).current_inventory, 0)
        Assert.AreEqual(test_base_warehouse.reorder_report_history(0).distributor_inventory, -1)
        Assert.AreEqual(test_base_warehouse.reorder_report_history(0).reorder_day, 0)
        Assert.AreEqual(test_base_warehouse.reorder_report_history(0).warehouse_id, 1)

        ''This is testing that it won't reorder when the reorder is already coming
        test_base_warehouse.Run_day()
        test_base_warehouse.Run_day()
        test_base_warehouse.Calculate_reorder()

        Assert.AreEqual(test_base_warehouse.current_incoming_shipment, 3)
        test_base_warehouse.Run_day()

        Assert.AreEqual(test_base_warehouse.current_incoming_shipment, -1)
        Assert.AreEqual(test_base_warehouse.current_incoming_shipment_size, 0)
        Assert.AreEqual(test_base_warehouse.start_day_inv(3), 11 * 10)

        ''This part would test that it won't reorder if it has enough inventory
        test_base_warehouse.Calculate_reorder()
        Assert.AreEqual(test_base_warehouse.current_incoming_shipment, -1)
        Assert.AreEqual(test_base_warehouse.current_incoming_shipment_size, 0)

    End Sub


    <TestMethod>
    Sub Test_Shipment_requested()
        Dim test_warehouse_inputs As New Warehouse_inputs(1, 2, 3, 0, 5, 6, 3, 0, SiteType.Base_Warehouse, 9, 10, 11, 12)
        Dim test_base_warehouse As New Base_Warehouse(test_warehouse_inputs, 1000)

        test_base_warehouse.end_day_inventory(0) = 100
        Dim returned_amount = test_base_warehouse.Shipment_requested(50)
        Assert.AreEqual(returned_amount, 50)
        Assert.AreEqual(test_base_warehouse.end_day_inventory(0), 50)
        Assert.AreEqual(test_base_warehouse.warehouse_shipment_amounts(0), 50)

        test_base_warehouse.end_day_inventory(0) = 100
        returned_amount = test_base_warehouse.Shipment_requested(150)
        Assert.AreEqual(returned_amount, 100)
        Assert.AreEqual(test_base_warehouse.end_day_inventory(0), 0)
        Assert.AreEqual(test_base_warehouse.warehouse_shipment_amounts(0), 150)

        test_base_warehouse.end_day_inventory(0) = 100
        returned_amount = test_base_warehouse.Shipment_requested(250)
        Assert.AreEqual(returned_amount, 0)
        Assert.AreEqual(test_base_warehouse.end_day_inventory(0), 100)
        Assert.AreEqual(test_base_warehouse.warehouse_shipment_amounts(0), 150)

    End Sub
End Class
