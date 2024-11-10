Imports System.ComponentModel.DataAnnotations

Public Class Base_Warehouse
    Inherits Warehouse

    Public lead_time_mean As Double
    Public lead_time_std As Double


    Public Sub New(inputs As Warehouse_inputs, sim_length As Integer)
        MyBase.New(inputs, sim_length)
        Me.lead_time_mean = inputs.lead_time_mean
        Me.lead_time_std = inputs.lead_time_sd
    End Sub

    ''' <summary>
    ''' Calculates the reorder for the warehouse based on the current inventory and reorder point.
    ''' </summary>
    ''' <remarks>
    ''' This method checks if the end day inventory is below the reorder point and if there is no current incoming shipment.
    ''' If both conditions are met, it calculates the lead time and schedules a new incoming shipment.
    ''' A new reorder report is also created and added to the reorder report history.
    ''' </remarks>
    Public Overrides Sub Calculate_reorder()
        If end_day_inventory(period) > reorder_point Or Not current_incoming_shipment = -1 Then
            Return
        End If

        Dim lead_time As Integer = Utils.calculate_lead_time(lead_time_mean, lead_time_std)
        current_incoming_shipment = lead_time + period
        current_incoming_shipment_size = reorder_amount * items_per_pallet
        Dim new_reorder_report As Reorder_report = New Reorder_report(warehouse_id, period, -1, end_day_inventory(period), -1, reorder_amount, reorder_amount * reorder_cost, True)
        reorder_report_history(period) = new_reorder_report

    End Sub

    Public Overrides Sub Add_distributor(new_dependent_warehouse As Warehouse, distributor_information As Reorder_inputs)
        Throw New NotImplementedException()
    End Sub

    ''' <summary>
    ''' Processes a shipment request for the specified amount.
    ''' </summary>
    ''' <param name="request_amount">The amount of items requested for shipment.</param>
    ''' <returns>The amount of items that can be shipped based on the current inventory.</returns>
    ''' <remarks>
    ''' This method adds the request amount to the warehouse requests for the current period.
    ''' If the end day inventory is greater than half of the request amount, it calculates the return amount,
    ''' updates the end day inventory and warehouse shipment amounts, and returns the return amount.
    ''' If the inventory is insufficient, it returns 0.
    ''' </remarks>
    Public Overrides Function Shipment_requested(request_amount As Integer) As Integer
        warehouse_requests.Add(request_amount)
        If end_day_inventory(period) > (request_amount / 2) Then
            Dim return_amount As Integer = Math.Min(end_day_inventory(period), request_amount)
            end_day_inventory(period) -= return_amount
            warehouse_shipment_amounts(period) += return_amount
            Return return_amount
        End If
        Return 0

    End Function
End Class
