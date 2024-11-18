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
    Dim ReorderRelations As List(Of (String, String, Reorder_inputs))
    Dim WarehouseLocations As Dictionary(Of String, String)


    Private Sub Main_form_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LostSalesPenaltyFactorLabel.Visible = False
        LostSalesPenaltyFactorTextBox.Visible = False
    End Sub

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

        If CSVDumpCheckbox.Checked Then
            RunSimulationOutputToCSV(NumSimulationDays)
        End If


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

    Private Sub RunSimulationOutputToCSV(NumSimulationDays As Integer)
        Dim Warehousegroup = New Warehouse_Group(WarehouseInputs, ReorderRelations)

        Dim ListOfWarehouses = Warehousegroup.return_entire_simulation(NumSimulationDays)
        For Each warehouse In ListOfWarehouses

            Dim FileName = warehouse.warehouse_id & "_SIMULATION_OUTPUT.csv"
            Dim CSVString As String = $"Warehouse {warehouse.warehouse_id} located at {WarehouseLocations(warehouse.warehouse_id)}" & vbCrLf
            CSVString += "Day, Beginning Day Inventory, Demand, End Day Inventory, Lost Sales, Reorder Confirmed, Reordered From, lead time, Reorder recieved,Reorder Amount" & vbCrLf

            ''transforms the reorder information into a dictionary 
            Dim Reorder_day_dictionary As Dictionary(Of Integer, Reorder_report) = New Dictionary(Of Integer, Reorder_report)
            Dim Reorder_arrived_dictionary As Dictionary(Of Integer, Reorder_report) = New Dictionary(Of Integer, Reorder_report)


            For Each reorder In warehouse.reorder_report_history
                Reorder_day_dictionary(reorder.reorder_day) = reorder
                Reorder_arrived_dictionary(reorder.reorder_day + reorder.lead_time) = reorder
            Next

            For day As Integer = 1 To NumSimulationDays
                CSVString = CSVString & day & ", " & warehouse.start_day_inv(day) & ", " & warehouse.demand(day) & ", " & warehouse.end_day_inventory(day) & ", " & warehouse.lost_sales(day) & ","

                Dim reorder_string_one As String = ""
                Dim reorder_string_two As String = ""

                If Reorder_day_dictionary.Keys.Contains(day) Then
                    reorder_string_one = "Yes, " & Reorder_day_dictionary(day).reordered_from & "," & Reorder_day_dictionary(day).lead_time & ","
                Else
                    reorder_string_one = "No, N/A, N/A,"
                End If

                If Reorder_arrived_dictionary.Keys.Contains(day) Then
                    reorder_string_two = "Yes, " & Reorder_arrived_dictionary(day).reorder_amount & ""
                Else
                    reorder_string_two = "No, N/A"
                End If

                CSVString = CSVString & reorder_string_one & reorder_string_two & vbCrLf


            Next
            File.WriteAllText(FileName, CSVString)
        Next
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


        Dim lostSalesPenaltyFactor As Double = 1 '''Sets default value to 1 - ie no additional penalty

        If LostSalesPenaltyFactorTextBox.Visible And (Not LostSalesPenaltyFactorTextBox.Text = "") Then
            Try
                lostSalesPenaltyFactor = Convert.ToDouble(LostSalesPenaltyFactorTextBox.Text)
            Catch exp As Exception
                MessageBox.Show("Please enter a valid number in the lost sales penalty factor box - if left blank the penalty will default to 1")
                Exit Sub
            End Try
        End If

        Dim requiredServiceLevels As Dictionary(Of String, Double) = Nothing

        If OptimisationType = OptimisedFor.CostWithPenalty Or OptimisationType = OptimisedFor.CostsWithPenaltyAndLostSales Then
            Dim ListOfWarehouseIDs = New List(Of String)

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

            StockWizardIterationInputs.Add(New Stock_wizard_iteration_inputs(40, 250, 365, 50, 35, delta_point:=0.1, delta_amount:=0.1, base_penalty:=350, annealing:=True))
            StockWizardIterationInputs.Add(New Stock_wizard_iteration_inputs(30, 250, 365, 25, 20, delta_point:=0.05, delta_amount:=0.05, base_penalty:=700, annealing:=True))
            StockWizardIterationInputs.Add(New Stock_wizard_iteration_inputs(20, 300, 365, 25, 30, num_var_to_optimise:=1, base_penalty:=1400, annealing:=True))

        ElseIf OneVarRadioButton.Checked Then
            StockWizardIterationInputs.Add(New Stock_wizard_iteration_inputs(40, 250, 365, 50, 35, num_var_to_optimise:=1, delta_point:=0.8, delta_amount:=0.8, base_penalty:=350, annealing:=True))
            StockWizardIterationInputs.Add(New Stock_wizard_iteration_inputs(40, 350, 365, 50, 35, num_var_to_optimise:=1, delta_point:=0.3, delta_amount:=0.3, base_penalty:=500, annealing:=True))
            StockWizardIterationInputs.Add(New Stock_wizard_iteration_inputs(40, 350, 365, 50, 35, num_var_to_optimise:=1, delta_point:=0.2, delta_amount:=0.2, base_penalty:=650, annealing:=True))
        Else
            MessageBox.Show("Please Choose which values to Optimise for")
            Exit Sub
        End If

        If Me.WarehouseInputs Is Nothing Or Me.ReorderRelations Is Nothing Then
            MessageBox.Show("Warehouse Inputs are missing. Please Submit inputs before continuing.")
            Exit Sub
        End If

        Dim stockwizardform As New RunStockWizardForm(Me.WarehouseInputs, Me.ReorderRelations, StockWizardIterationInputs, requiredServiceLevels, OptimisationType, lostSalesPenaltyFactor)

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

    Private Sub CostFunctionComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CostFunctionComboBox.SelectedIndexChanged
        If CostFunctionComboBox.SelectedItem = "Balance Costs With Opportunity Costs" Or CostFunctionComboBox.SelectedItem = "Balance Opportunity Costs with a Min SL" Then
            LostSalesPenaltyFactorLabel.Visible = True
            LostSalesPenaltyFactorTextBox.Visible = True
        Else
            LostSalesPenaltyFactorLabel.Visible = False
            LostSalesPenaltyFactorTextBox.Visible = False
        End If
    End Sub

End Class
