Imports Warehouse_Optimisation
Imports Warehouse_Optimisation.Stock_Wizard

Public Class RunStockWizardForm

    Dim stockWizard As Stock_Wizard
    Dim IterationInputs As List(Of Stock_wizard_iteration_inputs)
    Public Sub New(warehouseInputs As List(Of Warehouse_inputs), reorderRelations As List(Of (String, String, Reorder_inputs)), stockWizardIterationinputs As List(Of Stock_wizard_iteration_inputs), desiredServiceLevels As Dictionary(Of String, Double), optimisationType As OptimisedFor, Optional LostSalesPenaltyFactor As Double = 1)

        ' This call is required by the designer.
        InitializeComponent()

        Me.IterationInputs = stockWizardIterationinputs
        ''This is the progress bar that will be updated
        Dim ProgressCallback As ProgressDelegate = Sub(progress)
                                                       Update_progress_bar(progress)
                                                   End Sub

        ''This is a copy of the warehouse inputs that will be used to create the stock wizard
        Dim warehouseInputsCopy = New List(Of Warehouse_inputs)

        For Each warehouse In warehouseInputs
            warehouseInputsCopy.Add(New Warehouse_inputs(warehouse.warehouse_id, warehouse.initial_inventory, warehouse.demand_mean, warehouse.demand_sd, warehouse.reorder_point, warehouse.reorder_amount, warehouse.lead_time_mean, warehouse.lead_time_sd, warehouse.site_type, warehouse.profit_per_sale, warehouse.holding_cost_per_pallet, warehouse.items_per_pallet, warehouse.reorder_cost))
        Next


        Me.stockWizard = New Stock_Wizard(warehouseInputsCopy, reorderRelations, progressCallback:=ProgressCallback, desired_service_levels:=desiredServiceLevels, CostFunction:=optimisationType, LostSalesPenaltyFactor:=LostSalesPenaltyFactor)
    End Sub


    Public Sub RunStockWizard()

        Dim stockWizardResults As (Dictionary(Of String, List(Of Double)), Dictionary(Of String, List(Of Double))) = Nothing

        Task.Run(Sub()
                     stockWizardResults = Me.stockWizard.Run_stock_wizard(Me.IterationInputs)
                     StockWizardProgressBar.Invoke(New Action(Sub()

                                                                  Dim resultsForm As New StockRecomendationsForm(stockWizardResults, stockWizard.getWarehouseGroup())
                                                                  resultsForm.Show()
                                                                  Me.Close()
                                                              End Sub))
                 End Sub)
    End Sub


    Public Sub Update_progress_bar(progress As Integer)
        If StockWizardProgressBar.InvokeRequired Then
            StockWizardProgressBar.Invoke(New Action(Of Integer)(AddressOf Update_progress_bar), progress)
            Return
        Else
            StockWizardProgressBar.Value = progress
        End If
    End Sub

End Class
