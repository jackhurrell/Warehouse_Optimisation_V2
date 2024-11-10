Imports System.Collections.Concurrent

Public Class Concurrent_Warehouse_Group
    Inherits Warehouse_Group


    Public Sub New(warehouse_inputs As List(Of Warehouse_inputs), warehouse_relationships As List(Of (Integer, Integer, Reorder_inputs)))
        MyBase.New(warehouse_inputs, warehouse_relationships)
        Me.warehouse_inputs = warehouse_inputs
        Me.warehouse_relationships = warehouse_relationships
        reorder_order = Me.calculate_reorder_order()
    End Sub

    ''' <summary>
    ''' Runs a Monte Carlo simulation for the specified number of simulations and days in parralell.
    ''' </summary>
    ''' <param name="number_simulations">The number of simulations to run.</param>
    ''' <param name="number_days">The number of days for each simulation.</param>
    ''' <returns>A <see cref="Monte_Carlo_results"/> object containing the aggregated results of all simulations.</returns>
    ''' <remarks>
    ''' This method initializes a new <see cref="Monte_Carlo_results"/> object to store the results.
    ''' It then runs the specified number of simulations in parallel, collecting the results in a <see cref="ConcurrentBag(Of Simulation_result)"/>.
    ''' After all simulations are complete, it aggregates the results into the <see cref="Monte_Carlo_results"/> object and returns it.
    ''' </remarks>
    Public Overrides Function run_Monte_Carlo(number_simulations As Integer, number_days As Integer)
        Dim results As New Monte_Carlo_results(New List(Of List(Of Double)), New List(Of List(Of Double)), New List(Of Dictionary(Of Integer, Integer)), New List(Of List(Of Double)), New List(Of List(Of Double)), New List(Of List(Of Double)), New List(Of List(Of Integer)), New List(Of Integer))

        Dim all_simulation_results As New ConcurrentBag(Of Simulation_result)
        Parallel.For(0, number_simulations, Sub(i)
                                                Dim new_simulation = New Simulation(warehouse_inputs, number_days, reorder_order, warehouse_relationships)
                                                Dim sim_results = new_simulation.Run_simulation(number_days)
                                                all_simulation_results.Add(sim_results)
                                            End Sub)
        For Each result In all_simulation_results
            results.Service_levels.Add(result.service_levels)
            results.Internal_service_levels.Add(result.internal_service_levels)
            results.Storage_costs.Add(result.storage_costs)
            results.Reorder_costs.Add(result.reorder_costs)
            results.Lost_sales_costs.Add(result.lost_sales_costs)
            results.Demand_totals.Add(result.Demand_totals)

            If results.Reorder_paths.Count = 0 Then
                results.Reorder_paths = result.reorder_paths
            Else
                For j As Integer = 0 To results.Reorder_paths.Count - 1
                    results.Reorder_paths(j) = Utils.Add_integer_dictionaries(results.Reorder_paths(j), result.reorder_paths(j))
                Next
            End If
        Next

        ''This part then adds the warehouse order

        Dim sim_for_warehouse_order = New Simulation(warehouse_inputs, number_days, reorder_order, warehouse_relationships)
        results.Warehouse_order = sim_for_warehouse_order.return_warehouse_order()
        Return results

    End Function
End Class
