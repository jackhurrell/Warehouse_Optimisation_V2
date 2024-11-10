'------------------------------------------------------------------------------
' Name: Reorder_report
' Author: Jack Hurrell
' Company: Prophit Systems
' 
' Description:
' This structure captures information about every reorder that is made througout 
'the simulation, this is to help debug
'------------------------


Public Structure Reorder_report
    Public warehouse_id As Integer
    Public reorder_day As Integer
    Public reordered_from As Integer 'This is the warehouse id of the other warehosue
    Public current_inventory As Integer
    Public distributor_inventory As Integer
    Public reorder_amount As Integer
    Public reorder_cost As Double
    Public is_valid As Integer 'This exists to tell if the report is valid or just empty

    Public Sub New(warehouse_id As Integer, reorder_day As Integer,
                   reordered_from As Integer, current_inventory As Integer,
                   distributor_inventory As Integer, reorder_amount As Integer,
                   reorder_cost As Double, is_valid As Integer)
        Me.warehouse_id = warehouse_id
        Me.reorder_day = reorder_day
        Me.reordered_from = reordered_from
        Me.current_inventory = current_inventory
        Me.distributor_inventory = distributor_inventory
        Me.reorder_amount = reorder_amount
        Me.reorder_cost = reorder_cost
        Me.is_valid = is_valid
    End Sub
End Structure
