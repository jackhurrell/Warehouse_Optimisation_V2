Public Class Warehouse_Group

    Public warehouse_inputs As List(Of Warehouse_inputs)
    Public warehouse_relationships As List(Of (Integer, Integer, Reorder_inputs))
    Public reorder_order As List(Of Integer)

    Public Sub New(warehouse_inputs As List(Of Warehouse_inputs), warehouse_relationships As List(Of (Integer, Integer, Reorder_inputs)))
        Me.warehouse_inputs = warehouse_inputs
        Me.warehouse_relationships = warehouse_relationships
        reorder_order = Me.calculate_reorder_order()
    End Sub

    Public Sub alter_reorder_point(warehouse_id As Integer, altered_by As Integer)
        For Each warehouse In Me.warehouse_inputs
            If warehouse.warehouse_id = warehouse_id Then
                Console.WriteLine("Reorder point for warehouse " & warehouse_id & " has been altered by " & altered_by)
                warehouse.reorder_point = warehouse.reorder_point + altered_by
            End If
        Next


    End Sub

    ''' <summary>
    ''' Runs a Monte Carlo simulation for the specified number of simulations and days.
    ''' </summary>
    ''' <param name="number_simulations">The number of simulations to run.</param>
    ''' <param name="number_days">The number of days to run each simulation.</param>
    ''' <returns>A <see cref="Monte_Carlo_results"/> object containing the aggregated results of all simulations.</returns>
    ''' <remarks>
    ''' This method initializes and runs multiple simulations, aggregating the results into a single <see cref="Monte_Carlo_results"/> object.
    ''' It collects various metrics such as service levels, storage costs, reorder costs, and lost sales costs.
    ''' The reorder paths are combined across all simulations, and the factory order is set based on the first simulation.
    ''' </remarks>
    Public Overridable Function run_Monte_Carlo(number_simulations As Integer, number_days As Integer)
        Dim results As New Monte_Carlo_results(New List(Of List(Of Double)), New List(Of List(Of Double)), New List(Of Dictionary(Of Integer, Integer)), New List(Of List(Of Double)), New List(Of List(Of Double)), New List(Of List(Of Double)), New List(Of List(Of Integer)), New List(Of Integer))

        For i As Integer = 0 To number_simulations - 1
            Dim new_simulation = New Simulation(warehouse_inputs, number_days, reorder_order, warehouse_relationships)
            Dim sim_results = new_simulation.Run_simulation(number_days)

            results.Service_levels.Add(sim_results.service_levels)
            results.Internal_service_levels.Add(sim_results.internal_service_levels)
            results.Storage_costs.Add(sim_results.storage_costs)
            results.Reorder_costs.Add(sim_results.reorder_costs)
            results.Lost_sales_costs.Add(sim_results.lost_sales_costs)
            results.Demand_totals.Add(sim_results.demand_totals)

            If results.Reorder_paths.Count = 0 Then
                results.Reorder_paths = sim_results.reorder_paths
            Else
                For j As Integer = 0 To results.Reorder_paths.Count - 1
                    results.Reorder_paths(j) = Utils.Add_integer_dictionaries(results.Reorder_paths(j), sim_results.reorder_paths(j))
                Next
            End If

            ''This provides the order that all these results are ordered in
            If i = 0 Then
                results.Warehouse_order = new_simulation.return_warehouse_order
            End If
        Next
        Return results

    End Function

    ''' <summary>
    ''' Runs a  Monte Carlo simulation but does less logging for the specified number of simulations and days.
    ''' This allows for less memory to be required.
    ''' </summary>
    ''' <param name="number_simulations">The number of simulations to run.</param>
    ''' <param name="number_days">The number of days to run each simulation.</param>
    ''' <returns>A <see cref="Monte_carlo_averages"/> object containing the rolling averages of the simulation results.</returns>
    ''' <remarks>
    ''' This method initializes and runs multiple simulations, updating the rolling averages of various metrics such as service levels, storage costs, reorder costs, and lost sales costs.
    ''' The warehouse order is set based on the first simulation.
    ''' </remarks>
    Public Overridable Function run_light_Monte_Carlo(number_simulations As Integer, number_days As Integer) As Monte_carlo_averages
        Dim results As Monte_carlo_averages = Nothing


        For i As Integer = 0 To number_simulations - 1
            Dim new_simulation = New Simulation(warehouse_inputs, number_days, reorder_order, warehouse_relationships)
            Dim sim_results = new_simulation.Run_simulation(number_days)

            If i = 0 Then
                results = New Monte_carlo_averages((sim_results.service_levels, 1), (sim_results.internal_service_levels, 1), (sim_results.storage_costs, 1), (sim_results.reorder_costs, 1), (sim_results.lost_sales_costs, 1), new_simulation.return_warehouse_order)
            Else
                results.Update_rolling_averages(sim_results.service_levels, sim_results.internal_service_levels, sim_results.storage_costs, sim_results.reorder_costs, sim_results.lost_sales_costs)
            End If

        Next
        Return results

    End Function

    ''' <summary>
    ''' Runs a single simulation for the specified number of days and returns the list of warehouses.
    ''' This i smostly used for debugging or as an example in post-processing.
    ''' </summary>
    ''' <param name="number_days">The number of days to run the simulation.</param>
    ''' <returns>A list of <see cref="Warehouse"/> objects representing the state of each warehouse after the simulation.</returns>
    ''' <remarks>
    ''' This method initializes a new simulation with the given parameters, runs it for the specified number of days, and returns the list of warehouses.
    ''' </remarks>
    Function return_entire_simulation(number_days As Integer) As List(Of Warehouse)
        Dim single_simulation = New Simulation(warehouse_inputs, number_days, reorder_order, warehouse_relationships)
        single_simulation.Run_simulation(number_days)
        Return single_simulation.list_of_warehouses
    End Function

    ''' <summary>
    ''' Calculates the reorder order of warehouses based on their relationships.
    ''' </summary>
    ''' <returns>A list of warehouse IDs in topological order.</returns>
    ''' <exception cref="InvalidOperationException">Thrown if the graph contains a cycle, indicating an error with the given factory relations.</exception>
    ''' <remarks>
    ''' This method uses Kahn's algorithm to perform a topological sort on the warehouse relationships graph.
    ''' It initializes the adjacency list and in-degrees for each warehouse, then processes the graph to determine the order in which reorders should be processed.
    ''' </remarks>
    Function calculate_reorder_order() As List(Of Integer)
        If warehouse_inputs.Count = 1 Then
            Return New List(Of Integer) From {warehouse_inputs(0).warehouse_id}
        End If
        Dim inDegree As New Dictionary(Of Integer, Integer)()
        Dim graph As New Dictionary(Of Integer, List(Of Integer))()

        For Each edge In warehouse_relationships
            Dim u As Integer = edge.Item1
            Dim v As Integer = edge.Item2

            ' Initialize the adjacency list
            If Not graph.ContainsKey(u) Then
                graph(u) = New List(Of Integer)()
            End If
            graph(u).Add(v)

            ' Initialize in-degrees
            If Not inDegree.ContainsKey(u) Then
                inDegree(u) = 0
            End If
            If Not inDegree.ContainsKey(v) Then
                inDegree(v) = 0
            End If

            ' Update in-degrees
            inDegree(v) += 1
        Next


        Dim queue As New Queue(Of Integer)()
        For Each kvp In inDegree
            If kvp.Value = 0 Then
                queue.Enqueue(kvp.Key)
            End If
        Next

        Dim topologicalOrder As New List(Of Integer)()
        While queue.Count > 0
            Dim u As Integer = queue.Dequeue()
            topologicalOrder.Add(u)

            If graph.ContainsKey(u) Then
                For Each v In graph(u)
                    inDegree(v) -= 1
                    If inDegree(v) = 0 Then
                        queue.Enqueue(v)
                    End If
                Next
            End If
        End While

        If topologicalOrder.Count <> inDegree.Count Then
            Throw New InvalidOperationException("The graph contains a cycle, so an error has occuered with the factory relations given")
        End If

        Return topologicalOrder

    End Function


End Class
