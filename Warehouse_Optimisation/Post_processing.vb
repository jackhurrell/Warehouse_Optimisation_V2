Public Module Post_processing

    ''' <summary>
    ''' Calculates the average values for each warehouse from a list of simulation results that
    ''' are currently index by each simulation and then by each warehouse.
    ''' </summary>
    ''' <param name="twice_indexed_list">A list of lists containing simulation results, where each inner list represents results for a single simulation.</param>
    ''' <returns>A list of average values for each warehouse.</returns>
    ''' <remarks>
    ''' This method processes a twice-indexed list of simulation results to calculate the average value for each warehouse.
    ''' It first transposes the list to group values by warehouse, then removes any values set to -1 (indicating they were not used in the simulation).
    ''' Finally, it calculates the average for each warehouse, returning -1 if no valid values are present.
    ''' </remarks>
    Public Function calculate_averages_by_warehouse(Of T As IConvertible)(twice_indexed_list As List(Of List(Of T))) As List(Of Double)
        Dim value_by_warehouse As New List(Of List(Of Double))

        For i As Integer = 0 To twice_indexed_list(0).Count - 1
            Dim templist As New List(Of Double)
            For Each sim_result As List(Of T) In twice_indexed_list
                templist.Add(Convert.ToDouble(sim_result(i)))
            Next
            value_by_warehouse.Add(templist)
        Next

        ''This part removes values that are set to -1, which means they are not used in the simulation
        For i As Integer = 0 To value_by_warehouse.Count - 1
            value_by_warehouse(i) = value_by_warehouse(i).Where(Function(x) x <> -1).ToList()
        Next

        Dim averages As New List(Of Double)

        For Each list In value_by_warehouse
            If list.Count = 0 Then
                averages.Add(-1)
            Else
                averages.Add(list.Average())
            End If
        Next

        Return averages
    End Function

    Public Function create_id_to_position_map(warehouse_order As List(Of Integer)) As Dictionary(Of Integer, Integer)
        Dim id_to_position As New Dictionary(Of Integer, Integer)
        For i As Integer = 0 To warehouse_order.Count - 1
            id_to_position.Add(warehouse_order(i), i)
        Next

        Return id_to_position
    End Function

    ''' <summary>
    ''' Takes a list of list, retruned from a monte Carlo simulation and puls
    ''' out the values that refer to a specific warehouse. then returns a list for 
    ''' just that warehouse
    ''' </summary>
    ''' <returns>A list of warehouses sorted by the order of the ids.</returns>
    ''' </summary>
    ''' <param name="twice_indexed_list"></param>
    ''' <returns></returns>
    Public Function RemapDoubleIndexList(twice_indexed_list As List(Of List(Of Double)), warehouse_index As Integer) As List(Of Double)
        Dim return_list As New List(Of Double)

        For Each sublist In twice_indexed_list
            return_list.Add(sublist(warehouse_index))
        Next
        Return return_list
    End Function

    ''' <summary>
    ''' This function calculates the total stock sold across a whole network
    ''' </summary>
    ''' <returns>Average Service Level</returns>
    Public Function calculate_total_stock_services(demandResults As List(Of List(Of Integer)), serviceLevelResults As List(Of List(Of Double))) As Double

        Dim average_demand = calculate_averages_by_warehouse(demandResults)
        Dim average_service_levels = calculate_averages_by_warehouse(serviceLevelResults)

        Dim total_provided As Double = 0.0

        For i As Integer = 0 To average_demand.Count() - 1
            If average_service_levels(i) <> -1 Then
                total_provided += average_demand(i) * average_service_levels(i)
            End If
        Next

        Return total_provided

    End Function

End Module
