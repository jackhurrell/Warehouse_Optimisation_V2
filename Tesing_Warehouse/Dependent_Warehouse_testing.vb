Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Warehouse_Optimisation

<TestClass>
Public Class Dependent_Warehouse_testing
    <TestMethod>
    Sub Test_add_distributor()
        Dim distributor_one_inputs As New Warehouse_inputs(1, 2, 3, 0, 5, 6, 3, 0, SiteType.Dependent_Warehouse, 9, 10, 11, 12)
        Dim distributor_two_inputs As New Warehouse_inputs(2, 3, 4, 5, 6, 7, 8, 9, SiteType.Dependent_Warehouse, 10, 11, 12, 13)
        Dim distributor_three_inputs As New Warehouse_inputs(3, 3, 4, 5, 6, 7, 8, 9, SiteType.Base_Warehouse, 10, 11, 12, 13)

        Dim distributor_one As Warehouse = New Dependent_Warehouse(distributor_one_inputs, 100)
        Dim distributor_two As Warehouse = New Dependent_Warehouse(distributor_two_inputs, 100)
        Dim distributor_three As Warehouse = New Base_Warehouse(distributor_three_inputs, 100)

        Dim test_warehouse_inputs As New Warehouse_inputs(4, 5, 6, 7, 8, 9, 10, 11, SiteType.Dependent_Warehouse, 12, 13, 14, 15)
        Dim test_warehouse As New Dependent_Warehouse(test_warehouse_inputs, 100)

        Dim reorder_inputs_one As New Reorder_inputs(1, 2, 3)
        Dim reorder_inputs_two As New Reorder_inputs(4, 5, 6)
        Dim reorder_inputs_three As New Reorder_inputs(7, 8, 9)

        test_warehouse.Add_distributor(distributor_two, reorder_inputs_two)

        Assert.AreEqual(test_warehouse.distributors.Count, 1)
        Assert.AreEqual(test_warehouse.distributors(0).warehouse_id, 2)
        Assert.AreEqual(test_warehouse.distributors_reorder_info(2).lead_time_mean, 4)
        Assert.AreEqual(test_warehouse.distributors_reorder_info(2).lead_time_sd, 5)
        Assert.AreEqual(test_warehouse.distributors_reorder_info(2).reorder_cost, 6)


        test_warehouse.Add_distributor(distributor_one, reorder_inputs_one)
        Assert.AreEqual(test_warehouse.distributors.Count, 2)
        Assert.AreEqual(test_warehouse.distributors(0).warehouse_id, 1)
        Assert.AreEqual(test_warehouse.distributors_reorder_info(1).lead_time_mean, 1)
        Assert.AreEqual(test_warehouse.distributors_reorder_info(1).reorder_cost, 3)

        test_warehouse.Add_distributor(distributor_three, reorder_inputs_three)
        Assert.AreEqual(test_warehouse.distributors.Count, 3)
        Assert.AreEqual(test_warehouse.distributors(0).warehouse_id, 1)
        Assert.AreEqual(test_warehouse.distributors(1).warehouse_id, 2)
        Assert.AreEqual(test_warehouse.distributors(2).warehouse_id, 3)
        Assert.AreEqual(test_warehouse.distributors_reorder_info(3).lead_time_mean, 7)
        Assert.AreEqual(test_warehouse.distributors_reorder_info(3).reorder_cost, 9)
    End Sub

    <TestMethod>
    Sub test_calculate_reorder()
        Dim distributor_one_inputs As New Warehouse_inputs(1, 2, 3, 0, 5, 6, 3, 0, SiteType.Dependent_Warehouse, 9, 10, 11, 12)
        Dim distributor_two_inputs As New Warehouse_inputs(2, 3, 4, 0, 6, 7, 8, 9, SiteType.Dependent_Warehouse, 10, 11, 12, 13)
        Dim distributor_three_inputs As New Warehouse_inputs(3, 3, 4, 0, 6, 7, 8, 9, SiteType.Base_Warehouse, 10, 11, 12, 13)

        Dim distributor_one As Warehouse = New Dependent_Warehouse(distributor_one_inputs, 100)
        Dim distributor_two As Warehouse = New Dependent_Warehouse(distributor_two_inputs, 100)
        Dim distributor_three As Warehouse = New Base_Warehouse(distributor_three_inputs, 100)

        Dim reorder_inputs_one As New Reorder_inputs(1, 0, 3)
        Dim reorder_inputs_two As New Reorder_inputs(2, 0, 6)
        Dim reorder_inputs_three As New Reorder_inputs(3, 0, 9)

        Dim test_warehouse_inputs As New Warehouse_inputs(4, 5, 6, 0, 8, 9, 0, 11, SiteType.Dependent_Warehouse, 12, 13, 14, 15)
        Dim test_warehouse As New Dependent_Warehouse(test_warehouse_inputs, 100)

        test_warehouse.Add_distributor(distributor_one, reorder_inputs_one)
        test_warehouse.Add_distributor(distributor_two, reorder_inputs_two)
        test_warehouse.Add_distributor(distributor_three, reorder_inputs_three)

        distributor_one.end_day_inventory(0) = 1000
        distributor_two.end_day_inventory(0) = 2000
        distributor_three.end_day_inventory(0) = 3000

        test_warehouse.end_day_inventory(0) = 0
        test_warehouse.reorder_point = 100
        test_warehouse.reorder_amount = 10

        test_warehouse.Calculate_reorder()
        Assert.AreEqual(test_warehouse.current_incoming_shipment, 1)
        Assert.AreEqual(distributor_one.end_day_inventory(0), 860)
        Assert.AreEqual(test_warehouse.reorder_report_history(0).reorder_amount, 140)


        test_warehouse.Run_day()
        distributor_one.Run_day()
        distributor_two.Run_day()
        distributor_three.Run_day()
        Assert.AreEqual(test_warehouse.current_incoming_shipment, -1)
        Assert.AreEqual(test_warehouse.start_day_inv(1), 140)

        test_warehouse.end_day_inventory(1) = 0
        distributor_one.end_day_inventory(1) = 0
        test_warehouse.Calculate_reorder()
        Assert.AreEqual(test_warehouse.current_incoming_shipment, 3)
        Assert.AreEqual(distributor_two.end_day_inventory(1), 1856)

        ''Just running two days
        test_warehouse.Run_day()
        distributor_one.Run_day()
        distributor_two.Run_day()
        distributor_three.Run_day()
        test_warehouse.Run_day()
        distributor_one.Run_day()
        distributor_two.Run_day()
        distributor_three.Run_day()

        Assert.AreEqual(test_warehouse.current_incoming_shipment, -1)
        Assert.AreEqual(test_warehouse.start_day_inv(3), 140)

        test_warehouse.end_day_inventory(3) = 0
        distributor_one.end_day_inventory(3) = 0
        distributor_two.end_day_inventory(3) = 0

        Dim dist_3_inv = distributor_three.end_day_inventory(3)
        test_warehouse.Calculate_reorder()

        Assert.AreEqual(test_warehouse.current_incoming_shipment, 6)
        Assert.AreEqual(distributor_three.end_day_inventory(3), dist_3_inv - 140)
    End Sub

    <TestMethod>
    Sub Shipment_requested()
        Dim test_warehouse_inputs As New Warehouse_inputs(4, 5, 6, 7, 8, 9, 10, 11, SiteType.Dependent_Warehouse, 12, 13, 14, 15)
        Dim test_warehouse As New Dependent_Warehouse(test_warehouse_inputs, 100)

        test_warehouse.end_day_inventory(0) = 100
        test_warehouse.reorder_point = 30

        Dim returned_amount = test_warehouse.Shipment_requested(50)
        Assert.AreEqual(returned_amount, 50)
        Assert.AreEqual(test_warehouse.end_day_inventory(0), 50)

        returned_amount = test_warehouse.Shipment_requested(150)
        Assert.AreEqual(returned_amount, 0)
        Assert.AreEqual(test_warehouse.end_day_inventory(0), 50)

        returned_amount = test_warehouse.Shipment_requested(30)
        Assert.AreEqual(returned_amount, 30)
        Assert.AreEqual(test_warehouse.end_day_inventory(0), 20)

        test_warehouse.end_day_inventory(0) = 100
        returned_amount = test_warehouse.Shipment_requested(90)
        Assert.AreEqual(returned_amount, 0)
        Assert.AreEqual(test_warehouse.end_day_inventory(0), 100)

    End Sub
End Class
