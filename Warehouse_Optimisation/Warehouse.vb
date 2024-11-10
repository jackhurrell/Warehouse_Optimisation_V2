Imports System.Diagnostics.CodeAnalysis
Imports Systems.Collections.Generic

Public MustInherit Class Warehouse

    Public warehouse_id As Integer
    Public sim_length As Integer
    Public period As Integer
    Public start_day_inv As Integer()
    Public end_day_inventory As Integer()
    Public demand As Integer()
    Public reorder_amount As Integer
    Public current_incoming_shipment As Integer '-1 if no shipment is incoming
    Public current_incoming_shipment_size As Integer
    Public lost_sales As Integer()
    Public reorder_point As Double
    Public reorder_report_history As Reorder_report()
    Public site_type As SiteType
    Public warehouse_requests As List(Of Integer)
    Public warehouse_shipment_amounts As Integer()
    Public reorder_cost As Double
    Public profit_per_sale As Double
    Public storage_cost_per_pallet As Double
    Public items_per_pallet As Integer

    Public Sub New(inputs As Warehouse_inputs, sim_length As Integer)
        Me.warehouse_id = inputs.warehouse_id
        Me.sim_length = sim_length
        Me.period = 0
        Me.start_day_inv = New Integer(sim_length) {}
        Me.start_day_inv(0) = inputs.initial_inventory
        Me.end_day_inventory = New Integer(sim_length) {}
        Me.end_day_inventory(0) = inputs.initial_inventory
        Me.demand = Utils.generate_normal_random_ints(inputs.demand_mean, inputs.demand_sd, sim_length + 1) ''Plus one allows to just call demand(period) to make it easier
        Me.reorder_amount = inputs.reorder_amount
        Me.current_incoming_shipment = -1
        Me.current_incoming_shipment_size = 0
        Me.lost_sales = New Integer(sim_length) {}
        Me.reorder_point = inputs.reorder_point
        Me.reorder_report_history = New Reorder_report(sim_length) {}

        Me.site_type = inputs.site_type
        ''This could break if a factory can supply more than 200 other factories
        Me.warehouse_requests = New List(Of Integer)
        Me.warehouse_shipment_amounts = New Integer(sim_length) {}
        Me.reorder_cost = inputs.reorder_cost
        Me.profit_per_sale = inputs.profit_per_sale
        Me.storage_cost_per_pallet = inputs.holding_cost_per_pallet
        Me.items_per_pallet = inputs.items_per_pallet
    End Sub

    ''' <summary>
    ''' Simulates the operations of the warehouse for a single day.
    ''' </summary>
    ''' <remarks>
    ''' This method updates the inventory levels, processes incoming shipments, and records lost sales for the day.
    ''' </remarks>
    Public Sub Run_day()
        Dim current_inventory As Integer = Me.end_day_inventory(Me.period)
        period += 1

        ''Deals with any shipments scheduled
        If current_incoming_shipment = period Then
            current_inventory += current_incoming_shipment_size
            current_incoming_shipment = -1
            current_incoming_shipment_size = 0
        End If

        Me.start_day_inv(Me.period) = current_inventory

        current_inventory -= Math.Max(0, demand(period))
        Dim daily_lost_sales As Integer = If(current_inventory <= 0, -1 * current_inventory, 0)
        lost_sales(period) = daily_lost_sales

        current_inventory = Math.Max(0, current_inventory)
        end_day_inventory(period) = current_inventory
        'Console.WriteLine("End Day Inventory at period" & period & "At warehouse " & warehouse_id & " is " & end_day_inventory(period))
    End Sub



    ''' <summary> 
    ''' Calculates the service level, both customer facing, and internally (to other warehouses) 
    ''' </summary>  
    ''' <returns> 
    ''' A Tuple of type (Double,Double) containing (Service_level, internal_service_level)
    ''' These values are set to -1 if there is was no demand (eg the warehouse doesn't have any customer facing demand)
    ''' </returns> 
    Public Function Calc_service_levels() As (Double, Double)
        ''minus the first day as that period is not counted
        Dim demand_sum As Long = demand.Sum - demand(0)
        Dim lost_sales_sum As Long = lost_sales.Sum - lost_sales(0)
        Dim service_level As Double = -1

        If demand_sum > 0 Then
            service_level = 1 - (lost_sales_sum / demand_sum)
        End If

        'Console.WriteLine($"Lost sales sum {lost_sales_sum}")
        'Console.WriteLine($"Demand sum {demand_sum}")
        'Console.WriteLine($"Service level {service_level}")
        'For i As Integer = 0 To period
        '    Console.WriteLine($"Warehouse_requests for period: = {warehouse_requests.Last()}")
        'Next


        Dim warehouse_requests_total As Long = 0
        If warehouse_requests.Count > 1 Then
            warehouse_requests_total = 0
            For i As Integer = 1 To warehouse_requests.Count - 1
                warehouse_requests_total += warehouse_requests(i)
            Next
        End If

        Dim warehouse_shipment_total As Long = warehouse_shipment_amounts.Sum - warehouse_shipment_amounts(0)
        Dim internal_service_level As Double = -1

        If warehouse_requests_total > 0 Then
            internal_service_level = warehouse_shipment_total / warehouse_requests_total
        End If

        Return (service_level, internal_service_level)

    End Function

    ''' <summary> 
    ''' Calculates the how many reorders are coming from each warehouse. This is very useful for graph making
    ''' </summary>  
    ''' <returns> 
    ''' This returns a dictionary that looks like this (Key:warehouse_id, value:How many times that warehouse was reordered from).
    ''' </returns> 
    Public Function Calc_reorder_paths() As Dictionary(Of Integer, Integer)
        Dim reorder_paths As New Dictionary(Of Integer, Integer)
        For i As Integer = 0 To sim_length
            If reorder_report_history(i).is_valid = True Then
                Dim reorder_warehouse_id As Integer = reorder_report_history(i).reordered_from
                If Not reorder_paths.TryAdd(reorder_warehouse_id, 1) Then
                    reorder_paths(reorder_warehouse_id) += 1
                End If
            End If
        Next

        Return reorder_paths
    End Function


    ''' <summary> 
    ''' Calculates three different types of costs. Firstly all the reorder costs for this warehouse. 
    ''' Secondly the storage costs - these are calculated using the max pallets stored per week. 
    ''' Lastly the lost sale cost is how much profit was lost due to sales being missed.
    ''' </summary>  
    ''' <returns> 
    ''' This returns a dictionary that looks like this (Key:warehouse_id, value:How many times that warehouse was reordered from).
    ''' </returns> 
    Public Function Calc_costs() As (Double, Double, Double)
        Dim reorder_costs As Double = 0
        For j As Integer = 0 To sim_length
            If reorder_report_history(j).is_valid = True Then
                reorder_costs += reorder_report_history(j).reorder_cost
            End If
        Next


        Dim lost_sales_cost As Double = Me.profit_per_sale * (lost_sales.Sum() - lost_sales(0))

        '''This will mostly be 0, but doen't take up too much extra room
        Dim max_pallets_per_week As Integer() = New Integer(sim_length) {}


        Dim i As Integer = 1
        While i < (sim_length - 5)
            Dim max_pallets_for_week As Integer = CInt(Math.Ceiling(end_day_inventory(i) / items_per_pallet))
            For j As Integer = 1 To 6
                max_pallets_for_week = Math.Max(CInt(Math.Ceiling(end_day_inventory(i + j) / items_per_pallet)), max_pallets_for_week)
            Next
            max_pallets_per_week(i) = max_pallets_for_week
            i += 7
        End While

        Dim max_pallets_last_week As Integer = 0
        While i < sim_length + 1
            max_pallets_last_week = Math.Max(max_pallets_last_week, end_day_inventory(i) / items_per_pallet)
            i += 1
        End While
        max_pallets_per_week(sim_length) = max_pallets_last_week

        Dim storage_costs As Double = max_pallets_per_week.Sum() * storage_cost_per_pallet

        Dim lost_sales_costs As Double = profit_per_sale * (lost_sales.Sum() - lost_sales(0))
        'Console.WriteLine("For Warehouse " & warehouse_id & " Reorder_costs" & reorder_costs)
        Return (reorder_costs, storage_costs, lost_sales_cost)

    End Function

    Public Function calc_total_demand() As Integer
        Return Me.demand.Sum() - Me.demand(0)
    End Function

    MustOverride Sub Calculate_reorder()
    MustOverride Function Shipment_requested(request_amount As Integer) As Integer
    MustOverride Sub Add_distributor(new_dependent_warehouse As Warehouse, distributor_information As Reorder_inputs)

End Class
