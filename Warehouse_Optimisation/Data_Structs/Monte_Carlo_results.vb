'------------------------------------------------------------------------------
' Name: Monte_Carlo_results
' Author: Jack Hurrell
' Company: Prophit Systems
' 
' Description:
' This structure captures the results from Monte Carlo simulations for supply
' chain optimization. It stores various metrics including service levels, 
' reorder paths, and associated costs (storage, reorder, lost sales).
' most values are double nested loops. Each values in the firest level represents a
' new simulation. The second level represents each warehouse. The order the warehosue values
' are given in is stored in the variables Warehouse_order
'------------------------

Public Structure Monte_Carlo_results

    Dim Service_levels As List(Of List(Of Double))
    Dim Internal_service_levels As List(Of List(Of Double))
    Dim Reorder_paths As List(Of Dictionary(Of Integer, Integer))
    Dim Storage_costs As List(Of List(Of Double))
    Dim Reorder_costs As List(Of List(Of Double))
    Dim Lost_sales_costs As List(Of List(Of Double))
    Dim Demand_totals As List(Of List(Of Integer))
    Dim Warehouse_order As List(Of Integer)


    Public Sub New(Service_levels As List(Of List(Of Double)), Internal_service_levels As List(Of List(Of Double)),
                   Reorder_paths As List(Of Dictionary(Of Integer, Integer)), Storage_costs As List(Of List(Of Double)),
                   Reorder_costs As List(Of List(Of Double)), Lost_sales_costs As List(Of List(Of Double)), demand_totals As List(Of List(Of Integer)),
                   Warehouse_order As List(Of Integer))
        Me.Service_levels = Service_levels
        Me.Internal_service_levels = Internal_service_levels
        Me.Reorder_paths = Reorder_paths
        Me.Storage_costs = Storage_costs
        Me.Reorder_costs = Reorder_costs
        Me.Lost_sales_costs = Lost_sales_costs
        Me.Warehouse_order = Warehouse_order
        Me.Demand_totals = demand_totals
    End Sub


End Structure
