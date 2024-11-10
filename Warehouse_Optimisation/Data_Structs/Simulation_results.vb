'------------------------------------------------------------------------------
' Name: Simulatiom results
' Author: Jack Hurrell
' Company: Prophit Systems
' 
' Description:
' This structure captures the results from a single simulation class
' It stores these values to make passing the results easier. 
'------------------------




Public Structure Simulation_result

    Public service_levels As List(Of Double)
    Public internal_service_levels As List(Of Double)
    Public reorder_paths As List(Of Dictionary(Of Integer, Integer))
    Public storage_costs As List(Of Double)
    Public reorder_costs As List(Of Double)
    Public lost_sales_costs As List(Of Double)
    Public Demand_totals As List(Of Integer)

    Public Sub New(service_levels As List(Of Double), internal_service_levels As List(Of Double),
                   reorder_paths As List(Of Dictionary(Of Integer, Integer)), storage_costs As List(Of Double),
                   reorder_costs As List(Of Double), lost_sales_costs As List(Of Double), Demand_totals As List(Of Integer))
        Me.service_levels = service_levels
        Me.internal_service_levels = internal_service_levels
        Me.reorder_paths = reorder_paths
        Me.storage_costs = storage_costs
        Me.reorder_costs = reorder_costs
        Me.lost_sales_costs = lost_sales_costs
        Me.Demand_totals = Demand_totals
    End Sub

End Structure
