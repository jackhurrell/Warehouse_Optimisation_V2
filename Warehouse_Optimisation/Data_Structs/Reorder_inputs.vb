'------------------------------------------------------------------------------
' Name: Reorder_inputs
' Author: Jack Hurrell
' Company: Prophit Systems
' Date: [Insert Date]
' 
' Description:
' This structure holds the parameters for reorder inputs for different
' relationships between two warehouses
'------------------------------------------------------------------------------

Public Class Reorder_inputs
    Public lead_time_mean As Double
    Public lead_time_sd As Double
    Public reorder_cost As Double

    Public Sub New(lead_time_mean As Double, lead_time_sd As Double,
                   reorder_cost As Double)
        Me.lead_time_mean = lead_time_mean
        Me.lead_time_sd = lead_time_sd
        Me.reorder_cost = reorder_cost
    End Sub


End Class
