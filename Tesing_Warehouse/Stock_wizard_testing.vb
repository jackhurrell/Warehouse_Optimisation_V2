Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Warehouse_Optimisation

<TestClass>
Public Class Stock_wizard_tesing

    <TestMethod>
    Public Sub Create_stock_wizard_model()
        Dim Warehouse_inputs_1 = New Warehouse_inputs(1, 24000, 0, 0, 12000, 18, 6, 0, SiteType.Base_Warehouse, 20, 700, 2000, 150)
        Dim Warehouse_inputs_2 = New Warehouse_inputs(2, 16000, 1000, 700, 5000, 8, -1, -1, SiteType.Dependent_Warehouse, 20, 560, 2000, -1)
        Dim Warehouse_inputs_3 = New Warehouse_inputs(3, 8000, 1000, 300, 3000, 4, -1, -1, SiteType.Dependent_Warehouse, 20, 840, 2000, -1)
        Dim warehouse_list = New List(Of Warehouse_inputs) From {Warehouse_inputs_1, Warehouse_inputs_2, Warehouse_inputs_3}

        Dim reorder_inputs_1 = (2, 1, New Reorder_inputs(6, 1, 200))
        Dim reorder_inputs_2 = (3, 1, New Reorder_inputs(5, 1, 100))
        Dim reorder_inputs_3 = (3, 2, New Reorder_inputs(3, 2, 200))
        Dim reorder_inputs_list = New List(Of (Integer, Integer, Reorder_inputs)) From {reorder_inputs_1, reorder_inputs_2, reorder_inputs_3}

        '''These are the inpus that control the acctually paramaters of the stock wizard model
        Dim stock_wizard_inputs_round_1 = New Stock_wizard_iteration_inputs(40, 250, 200, 50, 35, delta_point:=0.1, delta_amount:=0.1, base_penalty:=350)
        Dim stock_wizard_inputs_round_2 = New Stock_wizard_iteration_inputs(30, 250, 200, 10, 7, delta_point:=0.0, delta_amount:=0.0, base_penalty:=700)
        Dim stock_wizard_inputs_round_4 = New Stock_wizard_iteration_inputs(20, 300, 500, 25, 30, num_var_to_optimise:=1, base_penalty:=1400)

        Dim test_iteration_params As New List(Of Stock_wizard_iteration_inputs) From {
            stock_wizard_inputs_round_1,
            stock_wizard_inputs_round_2,
            stock_wizard_inputs_round_4}

        Dim test_stock_wizard = New Stock_Wizard(warehouse_list, reorder_inputs_list, logging:=True)
        test_stock_wizard.Run_stock_wizard(3, test_iteration_params)
        Dim plotting_points = test_stock_wizard.logging_points
        plotting_points.pad_logging_vals()
        plotting_points.print_outputs()




    End Sub


    ''' <summary>
    ''' Lots of  this is used for testing using other graphing tools
    ''' This just produces the output to be later analysed
    ''' </summary>
    <TestMethod>
    Public Sub test_one_warehouse()

        Dim Warehouse_inputs = New Warehouse_inputs(2, 16000, 1000, 700, 12000, 13, 4, 2, SiteType.Base_Warehouse, 20, 200, 2000, 200)
        Dim reorder_inputs_list = New List(Of (Integer, Integer, Reorder_inputs)) From {}

        Dim warehouse_list = New List(Of Warehouse_inputs) From {Warehouse_inputs}

        Dim stock_wizard_inputs_round_1 = New Stock_wizard_iteration_inputs(30, 100, 1000, 35, 50, delta_point:=0.1, delta_amount:=0.1, num_var_to_optimise:=2, base_penalty:=350)
        Dim stock_wizard_inputs_round_2 = New Stock_wizard_iteration_inputs(20, 200, 1000, 20, 30, delta_point:=0.04, delta_amount:=0.04, num_var_to_optimise:=2, base_penalty:=650)
        Dim stock_wizard_inputs_round_3 = New Stock_wizard_iteration_inputs(20, 200, 1000, 25, 18, delta_point:=0.03, delta_amount:=0.03, num_var_to_optimise:=1, base_penalty:=2000)

        'Dim stock_wizard_inputs_round_2 = New Stock_wizard_iteration_inputs(30, 200, 365, 4, 8)
        'Dim stock_wizard_inputs_round_3 = New Stock_wizard_iteration_inputs(30, 200, 500, 3, 4)
        'Dim stock_wizard_inputs_round_4 = New Stock_wizard_iteration_inputs(20, 300, 500, 2, 2, num_var_to_optimise:=1, base_penalty:=1400)

        Dim test_iteration_params As New List(Of Stock_wizard_iteration_inputs) From {
            stock_wizard_inputs_round_1,
            stock_wizard_inputs_round_2,
        stock_wizard_inputs_round_3}
        'stock_wizard_inputs_round_3,
        'stock_wizard_inputs_round_4}


        Dim test_stock_wizard = New Stock_Wizard(warehouse_list, reorder_inputs_list, logging:=True)
        Dim recomended_stock_levels = test_stock_wizard.Run_stock_wizard(3, test_iteration_params)

        Dim logging_results As Stock_wizard_logging = test_stock_wizard.logging_points
        logging_results.pad_logging_vals()
        logging_results.print_outputs()


    End Sub

    ''' <summary>
    ''' This test tries to worj out how many iterations and simulations need to be donw by looking at the
    ''' variability between simulations
    ''' </summary>
    <TestMethod>
    Sub test_variability()


        Dim Warehouse_inputs = New Warehouse_inputs(2, 16000, 1000, 700, 13000, 8, 4, 2, SiteType.Base_Warehouse, 20, 200, 2000, 200)
        Dim reorder_inputs_list = New List(Of (Integer, Integer, Reorder_inputs)) From {}
        Dim warehouse_list = New List(Of Warehouse_inputs) From {Warehouse_inputs}
        Dim test_group = New Warehouse_Group(warehouse_list, reorder_inputs_list)
        Dim points_recorder = New Stock_wizard_logging(warehouse_list)


        For i As Integer = 0 To 12
            Dim results = test_group.run_Monte_Carlo(1000, 1000)
            Dim service_level_average = calculate_averages_by_warehouse(results.Service_levels)
            Dim service_levels = calculate_averages_by_warehouse(results.service_levels)
            Dim storage_costs = calculate_averages_by_warehouse(results.storage_costs)
            Dim reorder_costs = calculate_averages_by_warehouse(results.reorder_costs)
            points_recorder.add_gradient_points(2, 0, 0)
            points_recorder.add_reorder_points_and_amounts(2, Warehouse_inputs.reorder_point, Warehouse_inputs.reorder_amount * Warehouse_inputs.items_per_pallet)
            points_recorder.add_service_levels(2, service_levels(0))
            points_recorder.add_costs(2, storage_costs(0) + reorder_costs(0))
            points_recorder.add_penalty(2, 0)
            test_group.alter_reorder_point(2, -1000)
        Next

        points_recorder.pad_logging_vals()
        points_recorder.print_outputs()


    End Sub

End Class