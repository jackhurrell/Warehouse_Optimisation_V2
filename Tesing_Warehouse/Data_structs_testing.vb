Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Warehouse_Optimisation

<TestClass>
Public Class Data_structs_testing
    <TestMethod>
    Sub Test_warehouse_inputs()
        Dim test_warehouse_inputs As New Warehouse_inputs(1, 2, 3, 4, 5, 6, 7, 8, SiteType.Base_Warehouse, 9, 10, 11, 12)
        Assert.AreEqual(test_warehouse_inputs.warehouse_id, 1)
        Assert.AreEqual(test_warehouse_inputs.initial_inventory, 2)
        Assert.AreEqual(test_warehouse_inputs.demand_mean, 3)
        Assert.AreEqual(test_warehouse_inputs.demand_sd, 4)
        Assert.AreEqual(test_warehouse_inputs.reorder_point, 5)
        Assert.AreEqual(test_warehouse_inputs.reorder_amount, 6)
        Assert.AreEqual(test_warehouse_inputs.lead_time_mean, 7)
        Assert.AreEqual(test_warehouse_inputs.lead_time_sd, 8)
        Assert.AreEqual(test_warehouse_inputs.site_type, SiteType.Base_Warehouse)
        Assert.AreEqual(test_warehouse_inputs.profit_per_sale, 9)
        Assert.AreEqual(test_warehouse_inputs.holding_cost_per_pallet, 10)
        Assert.AreEqual(test_warehouse_inputs.items_per_pallet, 11)
        Assert.AreEqual(test_warehouse_inputs.reorder_cost, 12)

        Dim test_factory_inputs2 As New Warehouse_inputs(1, 2, 3, 4, 5, 6, 7, 8, SiteType.Dependent_Warehouse, 9, 10, 11, 12)
        Assert.AreEqual(test_factory_inputs2.site_type, SiteType.Dependent_Warehouse)
    End Sub


    <TestMethod>
    Sub test_reorder_inputs()
        Dim reoder_input_test As New Reorder_inputs(12.3, 23.6, 18.1)
        Assert.AreEqual(reoder_input_test.lead_time_mean, 12.3)
        Assert.AreEqual(reoder_input_test.lead_time_sd, 23.6)
        Assert.AreEqual(reoder_input_test.reorder_cost, 18.1)
    End Sub

    <TestMethod>
    Sub test_reorder_report()
        Dim reorder_report_test As New Reorder_report(1, 2, 0, 1000, 8000, 3000, 12.4, True)
        Assert.AreEqual(reorder_report_test.warehouse_id, 1)
        Assert.AreEqual(reorder_report_test.reorder_day, 2)
        Assert.AreEqual(reorder_report_test.reordered_from, 0)
        Assert.AreEqual(reorder_report_test.current_inventory, 1000)
        Assert.AreEqual(reorder_report_test.distributor_inventory, 8000)
        Assert.AreEqual(reorder_report_test.reorder_amount, 3000)
        Assert.AreEqual(reorder_report_test.reorder_cost, 12.4)
    End Sub
End Class
