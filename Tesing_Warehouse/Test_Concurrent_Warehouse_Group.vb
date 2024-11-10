Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Warehouse_Optimisation

<TestClass>
Public Class Test_Concurrent_Warehouse_Group
    <TestMethod>
    Sub test_concurrent_warehouse_group_timing()

        Dim Warehouse_inputs_1 = New Warehouse_inputs(1, 24000, 0, 0, 12000, 18, 6, 0, SiteType.Base_Warehouse, 20, 700, 2000, 150)
        Dim Warehouse_inputs_2 = New Warehouse_inputs(2, 16000, 1000, 700, 5000, 8, -1, -1, SiteType.Dependent_Warehouse, 20, 560, 2000, -1)
        Dim Warehouse_inputs_3 = New Warehouse_inputs(3, 8000, 1000, 300, 3000, 4, -1, -1, SiteType.Dependent_Warehouse, 20, 840, 2000, -1)
        Dim warehouse_list = New List(Of Warehouse_inputs) From {Warehouse_inputs_1, Warehouse_inputs_2, Warehouse_inputs_3}

        Dim reorder_inputs_1 = (2, 1, New Reorder_inputs(6, 1, 200))
        Dim reorder_inputs_2 = (3, 1, New Reorder_inputs(5, 1, 100))
        Dim reorder_inputs_3 = (3, 2, New Reorder_inputs(3, 2, 200))
        Dim reorder_inputs_list = New List(Of (Integer, Integer, Reorder_inputs)) From {reorder_inputs_1, reorder_inputs_2, reorder_inputs_3}

        Dim test_Warehouse_Group = New Concurrent_Warehouse_Group(warehouse_list, reorder_inputs_list)
        Dim results As Monte_Carlo_results = Nothing
        For i As Integer = 0 To 20
            results = test_Warehouse_Group.run_Monte_Carlo(500, 1000)
        Next
        Dim service_levels = calculate_averages_by_warehouse(results.Service_levels)
        Dim storage_costs = calculate_averages_by_warehouse(results.Storage_costs)
        Dim reorder_costs = calculate_averages_by_warehouse(results.Reorder_costs)
        Dim lost_sales_costs = calculate_averages_by_warehouse(results.Lost_sales_costs)


        'For Each service_level In results.service_levels
        '    Console.WriteLine(service_level)
        'Next

        Console.WriteLine($"Service level for warehouse 1 is {service_levels(0)}")
        Console.WriteLine($"Service level for warehouse 2 is {service_levels(1)}")
        Console.WriteLine($"Service level for warehouse 3 is {service_levels(2)}")

        Console.WriteLine($"Storage cost for warehouse 1 is {storage_costs(0)}")
        Console.WriteLine($"Storage cost for warehouse 2 is {storage_costs(1)}")
        Console.WriteLine($"Storage cost for warehouse 3 is {storage_costs(2)}")

        Console.WriteLine($"Reorder cost for warehouse 1 is {reorder_costs(0)}")
        Console.WriteLine($"Reorder cost for warehouse 2 is {reorder_costs(1)}")
        Console.WriteLine($"Reorder cost for warehouse 3 is {reorder_costs(2)}")

        Console.WriteLine($"Lost sales cost for warehouse 1 is {lost_sales_costs(0)}")
        Console.WriteLine($"Lost sales cost for warehouse 2 is {lost_sales_costs(1)}")
        Console.WriteLine($"Lost sales cost for warehouse 3 is {lost_sales_costs(2)}")


    End Sub


    <TestMethod>
    Sub test_warehouse_group_timing()

        Dim Warehouse_inputs_1 = New Warehouse_inputs(1, 24000, 0, 0, 12000, 18, 6, 0, SiteType.Base_Warehouse, 20, 700, 2000, 150)
        Dim Warehouse_inputs_2 = New Warehouse_inputs(2, 16000, 1000, 700, 5000, 8, -1, -1, SiteType.Dependent_Warehouse, 20, 560, 2000, -1)
        Dim Warehouse_inputs_3 = New Warehouse_inputs(3, 8000, 1000, 300, 3000, 4, -1, -1, SiteType.Dependent_Warehouse, 20, 840, 2000, -1)
        Dim warehouse_list = New List(Of Warehouse_inputs) From {Warehouse_inputs_1, Warehouse_inputs_2, Warehouse_inputs_3}

        Dim reorder_inputs_1 = (2, 1, New Reorder_inputs(6, 1, 200))
        Dim reorder_inputs_2 = (3, 1, New Reorder_inputs(5, 1, 100))
        Dim reorder_inputs_3 = (3, 2, New Reorder_inputs(3, 2, 200))
        Dim reorder_inputs_list = New List(Of (Integer, Integer, Reorder_inputs)) From {reorder_inputs_1, reorder_inputs_2, reorder_inputs_3}

        Dim test_Warehouse_Group = New Warehouse_Group(warehouse_list, reorder_inputs_list)
        Dim results As Monte_Carlo_results = Nothing
        For i As Integer = 0 To 20
            results = test_Warehouse_Group.run_Monte_Carlo(500, 1000)
        Next
        Dim service_levels = calculate_averages_by_warehouse(results.Service_levels)
        Dim storage_costs = calculate_averages_by_warehouse(results.Storage_costs)
        Dim reorder_costs = calculate_averages_by_warehouse(results.Reorder_costs)
        Dim lost_sales_costs = calculate_averages_by_warehouse(results.Lost_sales_costs)


        'For Each service_level In results.service_levels
        '    Console.WriteLine(service_level)
        'Next

        Console.WriteLine($"Service level for warehouse 1 is {service_levels(0)}")
        Console.WriteLine($"Service level for warehouse 2 is {service_levels(1)}")
        Console.WriteLine($"Service level for warehouse 3 is {service_levels(2)}")

        Console.WriteLine($"Storage cost for warehouse 1 is {storage_costs(0)}")
        Console.WriteLine($"Storage cost for warehouse 2 is {storage_costs(1)}")
        Console.WriteLine($"Storage cost for warehouse 3 is {storage_costs(2)}")

        Console.WriteLine($"Reorder cost for warehouse 1 is {reorder_costs(0)}")
        Console.WriteLine($"Reorder cost for warehouse 2 is {reorder_costs(1)}")
        Console.WriteLine($"Reorder cost for warehouse 3 is {reorder_costs(2)}")

        Console.WriteLine($"Lost sales cost for warehouse 1 is {lost_sales_costs(0)}")
        Console.WriteLine($"Lost sales cost for warehouse 2 is {lost_sales_costs(1)}")
        Console.WriteLine($"Lost sales cost for warehouse 3 is {lost_sales_costs(2)}")


    End Sub

End Class
