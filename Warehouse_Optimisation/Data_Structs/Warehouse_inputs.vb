'------------------------------------------------------------------------------
' Name: Warehouse Inputs
' Author: Jack Hurrell
' Company: Prophit Systems
' 
' Description:
' This structure captures all the warehouse inputs for both a dependent and base 
'warehouse. It allows you to pass around all the inormation about a simulation easier. 
'------------------------

REM a Base warehouse is one in which it's reorder points are not modelled
REM a Dependent warehouse is one in which it's reorder points are modelled
Public Enum SiteType
    Base_Warehouse
    Dependent_Warehouse
End Enum


Public Class Warehouse_inputs
    Public warehouse_id As Integer
    Public initial_inventory As Integer
    Public demand_mean As Double
    Public demand_sd As Double
    Public reorder_point As Double
    Public reorder_amount As Double
    Public lead_time_mean As Double
    Public lead_time_sd As Double
    Public site_type As SiteType
    Public profit_per_sale As Double
    Public holding_cost_per_pallet As Double
    Public items_per_pallet As Integer
    Public reorder_cost As Double



    Public Sub New(warehouse_id As Integer, initial_inventory As Integer,
                   demand_mean As Double, demand_sd As Double,
                   reorder_point As Integer, reorder_amount As Integer,
                   lead_time_mean As Double, lead_time_sd As Double,
                   site_type As SiteType, profit_per_sale As Double,
                   holding_cost_per_pallet As Double, items_per_pallet As Integer,
                   reorder_cost As Double)
        Me.warehouse_id = warehouse_id
        Me.initial_inventory = initial_inventory
        Me.demand_mean = demand_mean
        Me.demand_sd = demand_sd
        Me.reorder_point = reorder_point
        Me.reorder_amount = reorder_amount
        Me.lead_time_mean = lead_time_mean
        Me.lead_time_sd = lead_time_sd
        Me.site_type = site_type
        Me.profit_per_sale = profit_per_sale
        Me.holding_cost_per_pallet = holding_cost_per_pallet
        Me.items_per_pallet = items_per_pallet
        Me.reorder_cost = reorder_cost
    End Sub

End Class
