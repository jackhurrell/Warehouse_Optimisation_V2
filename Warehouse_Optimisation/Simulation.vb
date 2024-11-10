Public Class Simulation


    Public list_of_warehouses As List(Of Warehouse)
    Public dictionary_of_warehouses As Dictionary(Of Integer, Warehouse)
    Public reorder_order As List(Of Integer)
    Public sim_length As Integer
    Public current_day As Integer
    ''' <summary>
    ''' Initializes a new instance of the <see cref="Simulation"/> class with the specified warehouse inputs, simulation length, reorder order, and warehouse relationships.
    ''' </summary>
    ''' <param name="list_of_warehouse_inputs">A list of inputs for each warehouse to be included in the simulation.</param>
    ''' <param name="sim_length">The length of the simulation in days.</param>
    ''' <param name="reorder_order">A list of warehouse IDs indicating the order in which reorders should be processed.</param>
    ''' <param name="warehouse_relationships">A list of tuples representing relationships between warehouses, where each tuple contains two warehouse IDs and reorder inputs.</param>
    ''' <remarks>
    ''' This constructor initializes the simulation by setting up the list and dictionary of warehouses based on the provided inputs.
    ''' It also establishes relationships between warehouses as specified in the warehouse relationships list.
    ''' </remarks>
    Public Sub New(list_of_warehouse_inputs As List(Of Warehouse_inputs), sim_length As Integer, reorder_order As List(Of Integer), warehouse_relationships As List(Of (Integer, Integer, Reorder_inputs)))
        Me.sim_length = sim_length
        Me.current_day = 0

        Me.reorder_order = reorder_order

        list_of_warehouses = New List(Of Warehouse)
        dictionary_of_warehouses = New Dictionary(Of Integer, Warehouse)

        For Each warehouse_input In list_of_warehouse_inputs
            If warehouse_input.site_type = SiteType.Base_Warehouse Then
                list_of_warehouses.Add(New Base_Warehouse(warehouse_input, sim_length))
            Else
                list_of_warehouses.Add(New Dependent_Warehouse(warehouse_input, sim_length))
            End If
        Next
        For Each warehouse In list_of_warehouses
            dictionary_of_warehouses.Add(warehouse.warehouse_id, warehouse)
        Next

        ''This add the relationships between the warehouses.
        For Each relationship In warehouse_relationships
            dictionary_of_warehouses(relationship.Item1).Add_distributor(dictionary_of_warehouses(relationship.Item2), relationship.Item3)
        Next
    End Sub

    ''' <summary>
    ''' Runs the simulation for the specified number of days.
    ''' </summary>
    ''' <param name="num_days">The number of days to run the simulation.</param>
    ''' <returns>A <see cref="Simulation_result"/> object containing the results of the simulation.</returns>
    ''' <exception cref="Exception">Thrown if the simulation is run for a longer period than it was set up for.</exception>
    ''' <remarks>
    ''' This method runs the simulation day by day, updating each warehouse and processing reorders in the specified order.
    ''' It collects service levels, reorder paths, and various costs for each warehouse, and returns these results in a <see cref="Simulation_result"/> object.
    ''' </remarks>
    Function Run_simulation(num_days As Integer)
        For i As Integer = 0 To num_days - 1
            If i > Me.sim_length Then
                Throw (New Exception("Ran the simulation for a longer period than it was set up for"))

            End If
            For Each warehouse In list_of_warehouses
                warehouse.Run_day()
            Next

            For Each warehouse_index In reorder_order
                dictionary_of_warehouses(warehouse_index).Calculate_reorder()
            Next
        Next

        Dim service_levels = New List(Of Double)
        Dim internal_service_levels = New List(Of Double)
        Dim reorder_paths = New List(Of Dictionary(Of Integer, Integer))
        Dim storage_costs = New List(Of Double)
        Dim reorder_costs = New List(Of Double)
        Dim lost_sales_costs = New List(Of Double)
        Dim demand_totals = New List(Of Integer)

        For Each warehouse In list_of_warehouses
            Dim warehouse_service_levels = warehouse.Calc_service_levels()
            Dim costs = warehouse.Calc_costs()
            Dim warehouse_reorder_paths = warehouse.Calc_reorder_paths()
            Dim total_demand = warehouse.calc_total_demand()

            service_levels.Add(warehouse_service_levels.Item1)
            internal_service_levels.Add(warehouse_service_levels.Item2)
            reorder_paths.Add(warehouse_reorder_paths)
            reorder_costs.Add(costs.Item1)
            storage_costs.Add(costs.Item2)
            lost_sales_costs.Add(costs.Item3)
            demand_totals.Add(total_demand)

        Next
        Return New Simulation_result(service_levels, internal_service_levels, reorder_paths, storage_costs, reorder_costs, lost_sales_costs, demand_totals)
    End Function

    ''' <summary>
    ''' Returns the order in which all of the results are returned in. Each warehouse is 
    ''' identified by a unique ID.
    ''' </summary>
    ''' <returns>A list of warehouse IDs in the order they were added to the simulation.</returns>
    Function return_warehouse_order() As List(Of Integer)
        Return list_of_warehouses.Select(Function(Warehouse) Warehouse.warehouse_id).ToList()
    End Function

End Class