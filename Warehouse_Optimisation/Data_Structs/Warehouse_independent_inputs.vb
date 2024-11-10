

Imports System.Runtime.Intrinsics.X86

''' <summary>
''' These are the inputs that are entirley independnet of the SKU
''' They can be stored seperately and added to warehouse inputs
''' This structure is mostly used for breaking apart the SKU and the warehouse
''' 'When it comes to database storage and retrievel
''' </summary>
Public Structure Warehouse_independent_inputs
    Public Address As String
    Public Warehouse_SiteType As SiteType
    Public Holding_cost_per_pallet As Double

    Public Sub New(address As String, warehouse_SiteType As SiteType, holding_cost_per_pallet As Double)
        Me.Address = address
        Me.Warehouse_SiteType = warehouse_SiteType
        Me.Holding_cost_per_pallet = holding_cost_per_pallet
    End Sub
End Structure
