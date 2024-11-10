Imports Warehouse_Optimisation
Imports System.IO
Imports Warehouse_Optimisation.Stock_Wizard
Imports System.Configuration
Imports System.Diagnostics.Eventing.Reader

Public Class Main_form

    ''' <summary>
    ''' This variable can be adjusted based on how much variation you want when running 
    ''' The calculate service levels process. A higher number will give slighlt more accurate results
    ''' and less variation but will take longer to run
    ''' </summary>
    Const TotalDaysSimulated = 1000000

    Dim WarehouseInputs As List(Of Warehouse_inputs)
    Dim ReorderRelations As List(Of (Integer, Integer, Reorder_inputs))
    Dim WarehouseLocations As Dictionary(Of Integer, String)

    Private Sub btnOpenInputs_Click(sender As Object, e As EventArgs) Handles btnOpenInputs.Click


        ''Checks at least one of the boexes are tickted
        If Not CreateNetworkCheckBox.Checked And Not LoadNetworkCheckBox.Checked Then
            MessageBox.Show("Please select at least one of the Network options")
            Exit Sub
        End If

        If Not CreateNewSkuCheckBox.Checked And Not LoadSKUCheckBox.Checked Then
            MessageBox.Show("Please select at least one of the SKU options")
            Exit Sub
        End If


        Dim NewNetwork = CreateNetworkCheckBox.Checked
        Dim NewSKU = CreateNewSkuCheckBox.Checked

        Dim NetworkID As String = NetworkIDInputTextBox.Text
        Dim SKU As String = SkuInputTextBox.Text

        '''Checks the strings are not empty 
        If String.IsNullOrEmpty(NetworkID) Or String.IsNullOrEmpty(SKU) Then
            MessageBox.Show("Please enter a Network ID and SKU")
            Exit Sub
        End If

        Dim inputsForm As inputsForm
        Try
            inputsForm = New inputsForm(NetworkID, SKU, NewNetwork, NewSKU)
        Catch exp As Exception
            Exit Sub
        End Try

        inputsForm.ShowDialog()


        Dim inputs = inputsForm.return_inputs
        Me.WarehouseInputs = inputs.Item1
        Me.ReorderRelations = inputs.Item2
        Me.WarehouseLocations = inputs.Item3
    End Sub

    Private Sub btnCalcServiceLevel_Click(sender As Object, e As EventArgs) Handles btnCalcServiceLevels.Click
        Dim NumSimulationDays As Integer
        Dim NumSimulation As Integer

        Try
            NumSimulationDays = Convert.ToInt32(NumSimulationDaysTextBox.Text)

        Catch exp As Exception
            MessageBox.Show("Please enter the number of days as an Integer Values")
            Exit Sub
        End Try

        If NumSimulationDays > 10000 Or NumSimulationDays < 10 Then
            MessageBox.Show("Please enter a number of days between 10 and 10000")
            Exit Sub
        End If

        If WarehouseInputs Is Nothing Or ReorderRelations Is Nothing Then
            MessageBox.Show("Warehouse Inputs or Reorder Inputs are missing. Please Submit inputs before continuing.")
            Exit Sub
        End If

        NumSimulation = Convert.ToInt32(Math.Floor(TotalDaysSimulated / NumSimulationDays))

        'debug print
        'MessageBox.Show("Num simulations is" & NumSimulation)
        Try
            Dim warehouse_group_to_calc = New Concurrent_Warehouse_Group(WarehouseInputs, ReorderRelations)


            Dim monte_results = warehouse_group_to_calc.run_Monte_Carlo(NumSimulation, NumSimulationDays)
            Dim disp_results As New MonteResultsForm(monte_results, WarehouseInputs)
            disp_results.ShowDialog()

        Catch cycle As InvalidOperationException
            MessageBox.Show("Cycle detected in the inputs - provide reorder graph without a cycle")
            Exit Sub
        Catch exp As Exception
            MessageBox.Show("An error occured when calculating the service levels. Please check the inputs and try again")
            Exit Sub
        End Try
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        Dim Warehouse_inputs_1 = New Warehouse_inputs(1, 24000, 0, 0, 16000, 18, 6, 3, SiteType.Base_Warehouse, 5, 700, 2000, 200)
        Dim Warehouse_inputs_2 = New Warehouse_inputs(2, 16000, 1000, 300, 8000, 8, -1, -1, SiteType.Dependent_Warehouse, 5, 560, 2000, 200)
        Dim Warehouse_inputs_3 = New Warehouse_inputs(3, 8000, 1000, 700, 4800, 4, -1, -1, SiteType.Dependent_Warehouse, 5, 840, 2000, 120)
        WarehouseInputs = New List(Of Warehouse_inputs) From {Warehouse_inputs_1, Warehouse_inputs_3, Warehouse_inputs_2}

        Dim reorder_inputs_1 = (2, 1, New Reorder_inputs(5, 1, 200))
        Dim reorder_inputs_2 = (3, 1, New Reorder_inputs(4, 1, 100))
        Dim reorder_inputs_3 = (3, 2, New Reorder_inputs(2, 1, 200))
        ReorderRelations = New List(Of (Integer, Integer, Reorder_inputs)) From {reorder_inputs_1, reorder_inputs_2, reorder_inputs_3}

        Dim WarehouseLocations = New Dictionary(Of Integer, String) From {{1, "London"}, {2, "Birmingham"}, {3, "Manchester"}}
        Me.WarehouseLocations = WarehouseLocations
    End Sub

    Private Sub btnStockWizard_Click(sender As Object, e As EventArgs) Handles btnStockWizard.Click


        Dim OptimisationType As OptimisedFor

        Select Case CostFunctionComboBox.Text
            Case "Reduce Costs with a Target SL"
                OptimisationType = OptimisedFor.CostWithPenalty

            Case "Balance Costs With Opportunity Costs"
                OptimisationType = OptimisedFor.CostsWithLostSales

            Case "Balance Costs with a Min SL"
                OptimisationType = OptimisedFor.CostsWithPenaltyAndLostSales

            Case Else
                MessageBox.Show("Please Choose a Optimisation Type From The Drop Down List")
                Exit Sub
        End Select

        Dim requiredServiceLevels As Dictionary(Of Integer, Double) = Nothing

        If OptimisationType = OptimisedFor.CostWithPenalty Or OptimisationType = OptimisedFor.CostsWithPenaltyAndLostSales Then
            Dim ListOfWarehouseIDs = New List(Of Integer)

            For Each warehouse In WarehouseInputs
                If warehouse.demand_mean <> 0 Then
                    ListOfWarehouseIDs.Add(warehouse.warehouse_id)
                End If
            Next

            Dim serviceLevelForm = New TargetServiceLevelForm(ListOfWarehouseIDs)
            serviceLevelForm.ShowDialog()
            requiredServiceLevels = serviceLevelForm.returnServicelevels()

            ''used for debugging
            'Dim messageString = "Service levels are "

            'For Each warehouse In requiredServiceLevels.Keys()
            '    messageString = messageString & "Warehouse ID: " & warehouse & " Service Level: " & requiredServiceLevels(warehouse) & vbCrLf
            'Next
            'MessageBox.Show(messageString)
        End If


        Dim StockWizardIterationInputs = New List(Of Stock_wizard_iteration_inputs)

        ''Works out which box is checked
        If TwoVarRadioButton.Checked Then

            StockWizardIterationInputs.Add(New Stock_wizard_iteration_inputs(40, 250, 365, 50, 35, delta_point:=0.1, delta_amount:=0.1, base_penalty:=350))
            StockWizardIterationInputs.Add(New Stock_wizard_iteration_inputs(30, 250, 365, 25, 20, delta_point:=0.05, delta_amount:=0.05, base_penalty:=700))
            StockWizardIterationInputs.Add(New Stock_wizard_iteration_inputs(20, 300, 365, 25, 30, num_var_to_optimise:=1, base_penalty:=1400))

        ElseIf OneVarRadioButton.Checked Then
            StockWizardIterationInputs.Add(New Stock_wizard_iteration_inputs(40, 250, 365, 50, 35, num_var_to_optimise:=1, delta_point:=0.8, delta_amount:=0.8, base_penalty:=350))
            StockWizardIterationInputs.Add(New Stock_wizard_iteration_inputs(40, 350, 365, 50, 35, num_var_to_optimise:=1, delta_point:=0.3, delta_amount:=0.3, base_penalty:=500))
            StockWizardIterationInputs.Add(New Stock_wizard_iteration_inputs(40, 350, 365, 50, 35, num_var_to_optimise:=1, delta_point:=0.2, delta_amount:=0.2, base_penalty:=650))
        Else
            MessageBox.Show("Please Choose which values to Optimise for")
            Exit Sub
        End If

        If Me.WarehouseInputs Is Nothing Or Me.ReorderRelations Is Nothing Then
            MessageBox.Show("Warehouse Inputs are missing. Please Submit inputs before continuing.")
            Exit Sub
        End If

        Dim stockwizardform As New RunStockWizardForm(Me.WarehouseInputs, Me.ReorderRelations, StockWizardIterationInputs, requiredServiceLevels, OptimisationType)

        btnStockWizard.Enabled = False
        stockwizardform.RunStockWizard()
        stockwizardform.ShowDialog()
        btnStockWizard.Enabled = True
    End Sub

    Private Sub CreateNetwork_CheckedChanged(sender As Object, e As EventArgs) Handles CreateNetworkCheckBox.CheckedChanged
        If CreateNetworkCheckBox.Checked Then
            LoadNetworkCheckBox.Checked = False
        End If
    End Sub

    Private Sub LoadNetworkCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles LoadNetworkCheckBox.CheckedChanged
        If LoadNetworkCheckBox.Checked Then
            CreateNetworkCheckBox.Checked = False
        End If
    End Sub

    Private Sub CreateSkuCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles CreateNewSkuCheckBox.CheckedChanged
        If CreateNewSkuCheckBox.Checked Then
            LoadSKUCheckBox.Checked = False
        End If
    End Sub

    Private Sub LoadSKUCheckBox_CheckedChanged(sender As Object, e As EventArgs) Handles LoadSKUCheckBox.CheckedChanged
        If LoadSKUCheckBox.Checked Then
            CreateNewSkuCheckBox.Checked = False
        End If
    End Sub

End Class

