Imports Warehouse_Optimisation
Imports Warehouse_Optimisation.Stock_Wizard


Public Class RunStockWizardForm

    Dim stockWizard As Stock_Wizard
    Dim IterationInputs As List(Of Stock_wizard_iteration_inputs)
    Public Sub New(warehouseInputs As List(Of Warehouse_inputs), reorderRelations As List(Of (Integer, Integer, Reorder_inputs)), stockWizardIterationinputs As List(Of Stock_wizard_iteration_inputs), desiredServiceLevels As Dictionary(Of Integer, Double), optimisationType As OptimisedFor)

        ' This call is required by the designer.
        InitializeComponent()

        Me.IterationInputs = stockWizardIterationinputs
        ''This is the progress bar that will be updated
        Dim ProgressCallback As ProgressDelegate = Sub(progress)
                                                       Update_progress_bar(progress)
                                                   End Sub
        Me.stockWizard = New Stock_Wizard(warehouseInputs, reorderRelations, progressCallback:=ProgressCallback, desired_service_levels:=desiredServiceLevels, CostFunction:=optimisationType)
    End Sub


    Public Sub RunStockWizard()

        Dim stockWizardResults As (Dictionary(Of Integer, List(Of Double)), Dictionary(Of Integer, List(Of Double))) = Nothing

        Task.Run(Sub()
                     stockWizardResults = Me.stockWizard.Run_stock_wizard(3, Me.IterationInputs)
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