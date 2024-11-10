Imports System.Data
Imports System.Linq.Expressions

Public Class Dependent_Warehouse
    Inherits Warehouse

    Public distributors As List(Of Warehouse)
    Public distributors_reorder_info As Dictionary(Of Integer, Reorder_inputs)


    Public Sub New(inputs As Warehouse_inputs, sim_length As Integer)
        MyBase.New(inputs, sim_length)
        distributors = New List(Of Warehouse)
        distributors_reorder_info = New Dictionary(Of Integer, Reorder_inputs)
    End Sub
    ''' <summary>
    ''' Calculates the reorder for the dependent warehouse based on the current inventory and reorder point.
    ''' </summary>
    ''' <remarks>
    ''' This method checks if the end day inventory is below the reorder point and if there is no current incoming shipment.
    ''' If both conditions are met, it requests a shipment from each distributor in the list.
    ''' If a distributor can fulfill the request, it calculates the lead time and schedules a new incoming shipment.
    ''' A new reorder report is also created and added to the reorder report history.
    ''' </remarks>
    Public Overrides Sub Calculate_reorder()
        If end_day_inventory(period) > reorder_point Or Not current_incoming_shipment = -1 Then
            Return
        End If

        For Each distributor In distributors
            Dim amount_given As Integer = distributor.Shipment_requested(reorder_amount * items_per_pallet)
            If amount_given > 0 Then
                Dim lead_time As Integer = Utils.calculate_lead_time(distributors_reorder_info(distributor.warehouse_id).lead_time_mean, distributors_reorder_info(distributor.warehouse_id).lead_time_sd)
                current_incoming_shipment = period + lead_time
                current_incoming_shipment_size = amount_given

                Dim curr_reorder_cost As Double = distributors_reorder_info(distributor.warehouse_id).reorder_cost * (Math.Ceiling(amount_given / items_per_pallet))

                Dim new_reorder_report As Reorder_report = New Reorder_report(warehouse_id, period, distributor.warehouse_id, end_day_inventory(period), distributor.end_day_inventory(period), amount_given, curr_reorder_cost, True)
                reorder_report_history(period) = new_reorder_report
                'Console.WriteLine("" & warehouse_id & " reordered from " & distributor.warehouse_id & " for " & amount_given & " at cost " & curr_reorder_cost)
                Return
            End If
        Next
    End Sub

    Public Overrides Sub Add_distributor(new_distributor As Warehouse, distributor_information As Reorder_inputs)
        ''This makes sure the distributors are added in order of their reorder cost
        Dim i As Integer = 0
        While i < distributors.Count
            If distributors_reorder_info(distributors(i).warehouse_id).reorder_cost > distributor_information.reorder_cost Then
                Exit While
            End If
            i += 1
        End While
        distributors.Insert(i, new_distributor)
        distributors_reorder_info.Add(new_distributor.warehouse_id, distributor_information)
        'For Each distributor In distributors
        'Console.WriteLine("ID" & warehouse_id & " and distributor ID" & distributor.warehouse_id & " Has reorder cost:" & distributors_reorder_info(distributor.warehouse_id).reorder_cost)
        'Next
    End Sub

    ''' <summary>
    ''' Processes a shipment request for the specified amount from a dependent warehouse.
    ''' </summary>
    ''' <param name="request_amount">The amount of items requested for shipment.</param>
    ''' <returns>The amount of items that can be shipped based on the current inventory.</returns>
    ''' <remarks>
    ''' This method adds the request amount to the warehouse requests for the current period.
    ''' If the end day inventory minus the request amount is greater than half of the reorder point,
    ''' it updates the end day inventory and warehouse shipment amounts, and returns the request amount.
    ''' If the inventory is insufficient, it returns 0.
    ''' </remarks>
    Public Overrides Function Shipment_requested(request_amount As Integer) As Integer
        warehouse_requests.Add(request_amount)
        If end_day_inventory(period) - request_amount > reorder_point / 2 Then
            end_day_inventory(period) -= request_amount
            warehouse_shipment_amounts(period) += request_amount
            Return request_amount
        End If

        Return 0
    End Function
End Class
