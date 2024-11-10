Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Warehouse_Optimisation

<TestClass>
Public Class Warehouse_Group_testing
    <TestMethod>
    Sub test_Warehouse_Group_creation()
        Dim Warehouse_inputs_1 = New Warehouse_inputs(1, 24000, 0, 0, 16000, 18, 6, 3, SiteType.Base_Warehouse, 20, 700, 2000, 200)
        Dim Warehouse_inputs_2 = New Warehouse_inputs(2, 16000, 1000, 300, 8000, 8, -1, -1, SiteType.Dependent_Warehouse, 20, 560, 2000, 200)
        Dim Warehouse_inputs_3 = New Warehouse_inputs(3, 8000, 1000, 700, 4800, 4, -1, -1, SiteType.Dependent_Warehouse, 20, 840, 2000, 120)
        Dim warehouse_list = New List(Of Warehouse_inputs) From {Warehouse_inputs_1, Warehouse_inputs_2, Warehouse_inputs_3}

        Dim reorder_inputs_1 = (2, 1, New Reorder_inputs(5, 1, 200))
        Dim reorder_inputs_2 = (3, 1, New Reorder_inputs(4, 1, 100))
        Dim reorder_inputs_3 = (3, 2, New Reorder_inputs(2, 1, 200))
        Dim reorder_inputs_list = New List(Of (Integer, Integer, Reorder_inputs)) From {reorder_inputs_1, reorder_inputs_2, reorder_inputs_3}

        Dim test_Warehouse_Group = New Warehouse_Group(warehouse_list, reorder_inputs_list)
        test_Warehouse_Group.run_Monte_Carlo(1000, 1000)
        Assert.AreEqual(test_Warehouse_Group.reorder_order(0), 3)
        Assert.AreEqual(test_Warehouse_Group.reorder_order(1), 2)
        Assert.AreEqual(test_Warehouse_Group.reorder_order(2), 1)

    End Sub

    <TestMethod>
    Sub test_warehouse_group_monte_carlo()

        Dim Warehouse_inputs_1 = New Warehouse_inputs(1, 24000, 0, 0, 13141, 18, 6, 0, SiteType.Base_Warehouse, 20, 700, 2000, 150)
        Dim Warehouse_inputs_2 = New Warehouse_inputs(2, 16000, 1000, 700, 8688, 8, -1, -1, SiteType.Dependent_Warehouse, 20, 560, 2000, -1)
        Dim Warehouse_inputs_3 = New Warehouse_inputs(3, 8000, 1000, 300, 5952, 5, -1, -1, SiteType.Dependent_Warehouse, 20, 840, 2000, -1)
        Dim warehouse_list = New List(Of Warehouse_inputs) From {Warehouse_inputs_1, Warehouse_inputs_2, Warehouse_inputs_3}

        Dim reorder_inputs_1 = (2, 1, New Reorder_inputs(6, 1, 200))
        Dim reorder_inputs_2 = (3, 1, New Reorder_inputs(5, 1, 100))
        Dim reorder_inputs_3 = (3, 2, New Reorder_inputs(3, 2, 200))
        Dim reorder_inputs_list = New List(Of (Integer, Integer, Reorder_inputs)) From {reorder_inputs_1, reorder_inputs_2, reorder_inputs_3}

        Dim test_Warehouse_Group = New Warehouse_Group(warehouse_list, reorder_inputs_list)
        Dim results = test_Warehouse_Group.run_Monte_Carlo(500, 1000)

        Dim service_levels = calculate_averages_by_warehouse(results.service_levels)
        Dim storage_costs = calculate_averages_by_warehouse(results.storage_costs)
        Dim reorder_costs = calculate_averages_by_warehouse(results.reorder_costs)
        Dim lost_sales_costs = calculate_averages_by_warehouse(results.lost_sales_costs)


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
    Public Sub test_light_monte_carlo()
        Dim Warehouse_inputs_1 = New Warehouse_inputs(1, 24000, 0, 0, 12000, 18, 6, 0, SiteType.Base_Warehouse, 20, 700, 2000, 150)
        Dim Warehouse_inputs_2 = New Warehouse_inputs(2, 16000, 1000, 700, 5000, 8, -1, -1, SiteType.Dependent_Warehouse, 20, 560, 2000, -1)
        Dim Warehouse_inputs_3 = New Warehouse_inputs(3, 8000, 1000, 300, 3000, 4, -1, -1, SiteType.Dependent_Warehouse, 20, 840, 2000, -1)
        Dim warehouse_list = New List(Of Warehouse_inputs) From {Warehouse_inputs_1, Warehouse_inputs_2, Warehouse_inputs_3}

        Dim reorder_inputs_1 = (2, 1, New Reorder_inputs(6, 1, 200))
        Dim reorder_inputs_2 = (3, 1, New Reorder_inputs(5, 1, 100))
        Dim reorder_inputs_3 = (3, 2, New Reorder_inputs(3, 2, 200))
        Dim reorder_inputs_list = New List(Of (Integer, Integer, Reorder_inputs)) From {reorder_inputs_1, reorder_inputs_2, reorder_inputs_3}

        Dim test_Warehouse_Group = New Warehouse_Group(warehouse_list, reorder_inputs_list)
        Dim monte_carlo_averages = test_Warehouse_Group.run_light_Monte_Carlo(500, 1000)

        Console.WriteLine("Printing service_levels")
        For Each service_level In monte_carlo_averages.Service_levels.Item1
            Console.WriteLine(service_level)
        Next

        Console.WriteLine("Printing storage_costs")
        For Each storage_cost In monte_carlo_averages.Storage_costs.Item1
            Console.WriteLine(storage_cost)
        Next

        Console.WriteLine("Printing reorder_costs")
        For Each reorder_cost In monte_carlo_averages.Reorder_costs.Item1
            Console.WriteLine(reorder_cost)

        Next
    End Sub
End Class
