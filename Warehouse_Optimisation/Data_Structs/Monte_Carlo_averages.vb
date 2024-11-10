'------------------------------------------------------------------------------
' Name: Monte_Carlo_results
' Author: Jack Hurrell
' Company: Prophit Systems
' 
' Description:
' This structure captures the results from Monte Carlo simulations but does it using rolling avgerages
'This makes computation much faster and more efficient - useful when facing memory concerns
'------------------------


Public Class Monte_carlo_averages

    Public Service_levels As (List(Of Double), Integer)
    Public Internal_service_levels As (List(Of Double), Integer)
    Public Storage_costs As (List(Of Double), Integer)
    Public Reorder_costs As (List(Of Double), Integer)
    Public Lost_sales_costs As (List(Of Double), Integer)
    Public Warehouse_order As List(Of Integer)

    Public Sub New(service_levels As (List(Of Double), Integer), internal_service_levels As (List(Of Double), Integer), storage_costs As (List(Of Double), Integer), reorder_costs As (List(Of Double), Integer), lost_sales_costs As (List(Of Double), Integer), warehouse_order As List(Of Integer))
        Me.Service_levels = service_levels
        Me.Internal_service_levels = internal_service_levels
        Me.Storage_costs = storage_costs
        Me.Reorder_costs = reorder_costs
        Me.Lost_sales_costs = lost_sales_costs
        Me.Warehouse_order = warehouse_order
    End Sub

    Public Sub Update_rolling_averages(new_service_level_averages As List(Of Double), new_internal_service_levels As List(Of Double), new_storage_costs As List(Of Double), new_reorder_costs As List(Of Double), new_lost_sales_costs As List(Of Double))
        For i As Integer = 0 To Service_levels.Item1.Count - 1
            Service_levels.Item1(i) = (Service_levels.Item1(i) * Service_levels.Item2 + new_service_level_averages(i)) / (Service_levels.Item2 + 1)
            Internal_service_levels.Item1(i) = (Internal_service_levels.Item1(i) * Internal_service_levels.Item2 + new_internal_service_levels(i)) / (Internal_service_levels.Item2 + 1)
            Storage_costs.Item1(i) = (Storage_costs.Item1(i) * Storage_costs.Item2 + new_storage_costs(i)) / (Storage_costs.Item2 + 1)
            Reorder_costs.Item1(i) = (Reorder_costs.Item1(i) * Reorder_costs.Item2 + new_reorder_costs(i)) / (Reorder_costs.Item2 + 1)
            Lost_sales_costs.Item1(i) = (Lost_sales_costs.Item1(i) * Lost_sales_costs.Item2 + new_lost_sales_costs(i)) / (Lost_sales_costs.Item2 + 1)
        Next
        Service_levels.Item2 += 1
        Internal_service_levels.Item2 += 1
        Storage_costs.Item2 += 1
        Reorder_costs.Item2 += 1
        Lost_sales_costs.Item2 += 1


    End Sub

End Class
