Imports System.Reflection.Metadata.Ecma335
Imports System.Windows
Imports System.Windows.Forms.DataVisualization.Charting
Imports Warehouse_Optimisation

Public Class StockRecomendationsForm

    Dim reorderPoints As Dictionary(Of Integer, List(Of Double))
    Dim reorderAmounts As Dictionary(Of Integer, List(Of Double))
    Dim warehouseGroup As Warehouse_Group

    Public Sub New(results As (Dictionary(Of Integer, List(Of Double)), Dictionary(Of Integer, List(Of Double))), warehouseGroup As Warehouse_Group)

        ' This call is required by the designer.
        InitializeComponent()

        Me.reorderPoints = results.Item1
        Me.reorderAmounts = results.Item2
        Me.warehouseGroup = warehouseGroup

        ''These are some print statements if things are a little crazy

        ''''Debug print the reoder point and amount values
        'Debug.WriteLine("Reorder Points")
        'For Each warehouseID In reorderPoints.Keys
        '    Debug.WriteLine($"Warehouse {warehouseID}")
        '    For i As Integer = 0 To reorderPoints(warehouseID).Count - 1
        '        Debug.WriteLine($"Iteration {i}: {reorderPoints(warehouseID)(i)}")
        '    Next
        'Next
        'Debug.WriteLine("Reorder Amounts")
        'For Each warehouseID In reorderAmounts.Keys
        '    Debug.WriteLine($"Warehouse {warehouseID}")
        '    For i As Integer = 0 To reorderAmounts(warehouseID).Count - 1
        '        Debug.WriteLine($"Iteration {i}: {reorderAmounts(warehouseID)(i)}")
        '    Next
        'Next



        PlotReorderPointChart()
        PlotReorderAmountChart()
        Dim bothResults = RunComparativeSimulations()
        FillOutSummarylabels(bothResults)
        LoadSummaryResultsTable(bothResults.Item2)


    End Sub

    Private Sub LoadSummaryResultsTable(recomendedResults As Monte_Carlo_results)

        Dim service_level_averages = calculate_averages_by_warehouse(recomendedResults.Service_levels)
        Dim storage_cost_averages = calculate_averages_by_warehouse(recomendedResults.Storage_costs)
        Dim lost_sales_averages = calculate_averages_by_warehouse(recomendedResults.Lost_sales_costs)
        Dim id_to_position = create_id_to_position_map(recomendedResults.Warehouse_order)

        Dim itemsPerPallet = warehouseGroup.warehouse_inputs(0).items_per_pallet

        Dim gridData As New DataTable

        gridData.Columns.Add("Warehouse ID")
        gridData.Columns.Add("Recomended Reorder Point")
        gridData.Columns.Add("Recomended Reorder Amount (Pallets)")
        gridData.Columns.Add("Service Level")
        gridData.Columns.Add("Storage Costs")
        gridData.Columns.Add("Missed Profit Due to Lost Sales")

        For i As Integer = 0 To recomendedResults.Warehouse_order.Count - 1
            Dim warehouseID = recomendedResults.Warehouse_order(i)
            Dim tempServiceLevel As String
            If service_level_averages(i) = -1 Then
                tempServiceLevel = "N/A"
            Else
                tempServiceLevel = Format(service_level_averages(i), "0.000%")

            End If

            gridData.Rows.Add(recomendedResults.Warehouse_order(i), Format(Me.reorderPoints(warehouseID).Last(), "0"), (Me.reorderAmounts(warehouseID).Last()) / itemsPerPallet,
                                        tempServiceLevel, storage_cost_averages(i).ToString("C"),
                                        lost_sales_averages(i).ToString("C"))
        Next

        StockWizDataGrid.AutoSize = True
        StockWizDataGrid.RowHeadersVisible = False

        StockWizDataGrid.DataSource = gridData
        StockWizDataGrid.ClearSelection()
        StockWizDataGrid.CurrentCell = Nothing

        ''Çhanges the headers style
        With StockWizDataGrid.ColumnHeadersDefaultCellStyle
            .Font = New Font("Arial", 8, FontStyle.Bold)
            .BackColor = Color.LightGray
        End With


    End Sub

    Private Sub FillOutSummarylabels(bothResults As (Monte_Carlo_results, Monte_Carlo_results))
        Dim orgResults = bothResults.Item1
        Dim recResults = bothResults.Item2

        Dim profitPerSale = Me.warehouseGroup.warehouse_inputs(0).profit_per_sale

        Dim totaSoldOrg = calculate_total_stock_services(orgResults.Demand_totals, orgResults.Service_levels)
        Dim totaDemandOrg = calculate_averages_by_warehouse(orgResults.Demand_totals).Sum()
        Dim totalServiceLevelOrg = totaSoldOrg / totaDemandOrg
        Dim grossProfitOrg = totaSoldOrg * profitPerSale
        Dim totalStorageCostsOrg = calculate_averages_by_warehouse(orgResults.Storage_costs).Sum()

        Dim totaSoldRec = calculate_total_stock_services(recResults.Demand_totals, recResults.Service_levels)
        Dim totaDemandRec = calculate_averages_by_warehouse(recResults.Demand_totals).Sum()
        Dim totalServiceLevelRec = totaSoldRec / totaDemandRec
        Dim grossProfitRec = totaSoldRec * profitPerSale
        Dim totalStorageCostsRec = calculate_averages_by_warehouse(recResults.Storage_costs).Sum()



        '''displays the values in correct formattig

        lblAveServicelvlOrg.Text = totalServiceLevelOrg.ToString("P")
        lblGrossRevenueOrg.Text = grossProfitOrg.ToString("C")
        lblHoldingCostOrg.Text = totalStorageCostsOrg.ToString("C")

        lblAveServicelvlRec.Text = totalServiceLevelRec.ToString("P")
        lblGrossRevenueRec.Text = grossProfitRec.ToString("C")
        lblHoldingCostRec.Text = totalStorageCostsRec.ToString("C")


    End Sub


    ''' <summary>
    ''' 'This function runs a monte carlo simulation of the warehouse group, once with the original values
    ''' Provide and once with the recomneded values. This allows the comparison between the two to be made. 
    ''' </summary>
    Private Function RunComparativeSimulations(Optional NumDays As Integer = 365) As (Monte_Carlo_results, Monte_Carlo_results)

        Dim ItemsPerPallet = warehouseGroup.warehouse_inputs(0).items_per_pallet

        For Each warehouse In warehouseGroup.warehouse_inputs
            Dim warehouseID = warehouse.warehouse_id
            warehouse.reorder_point = Me.reorderPoints(warehouseID)(0)
            warehouse.reorder_amount = Me.reorderAmounts(warehouseID)(0)
        Next

        Dim originalMonteResults = warehouseGroup.run_Monte_Carlo(3000, NumDays)

        For Each warehouse In warehouseGroup.warehouse_inputs
            Dim warehouseID = warehouse.warehouse_id
            warehouse.reorder_point = Me.reorderPoints(warehouseID).Last()
            warehouse.reorder_amount = Me.reorderAmounts(warehouseID).Last() / ItemsPerPallet
        Next

        Dim recomendedMonteResults = warehouseGroup.run_Monte_Carlo(3000, NumDays)

        Return (originalMonteResults, recomendedMonteResults)
    End Function


    Private Sub StockRecomendationsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub PlotReorderPointChart()
        ReorderPointsChart.Series.Clear()

        For Each warehouseID In reorderPoints.Keys
            Dim reorderPointsSeries As New DataVisualization.Charting.Series($"Warehouse {warehouseID}")
            reorderPointsSeries.ChartType = DataVisualization.Charting.SeriesChartType.Line

            ' Add data points for each iteration
            For i As Integer = 0 To reorderPoints(warehouseID).Count - 1
                reorderPointsSeries.Points.AddXY(i, reorderPoints(warehouseID)(i))
            Next

            ' Add series to the chart
            ReorderPointsChart.Series.Add(reorderPointsSeries)
        Next

        '' Add 'titles to the charts and axis
        Dim graphTitle As New Title()
        graphTitle.Text = "Reorder Points throughout the Optimisation Process"
        graphTitle.Font = New Font("Arial", 12, FontStyle.Bold)
        ReorderPointsChart.Titles.Add(graphTitle)
        ReorderPointsChart.ChartAreas(0).AxisX.Title = "Iteration"
        ReorderPointsChart.ChartAreas(0).AxisX.TitleFont = New Font("Arial", 8, FontStyle.Bold)
        ReorderPointsChart.ChartAreas(0).AxisY.Title = "Reorder Points"
        ReorderPointsChart.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 8, FontStyle.Bold)

        ReorderPointsChart.ChartAreas(0).AxisX.Minimum = 0

    End Sub


    Private Sub PlotReorderAmountChart()

        ReorderAmountsChart.Series.Clear()

        For Each warehouseID In reorderAmounts.Keys
            Dim reorderAmountsSeries As New DataVisualization.Charting.Series($"Warehouse {warehouseID}")
            reorderAmountsSeries.ChartType = DataVisualization.Charting.SeriesChartType.Line

            ' Add data points for each iteration
            For i As Integer = 0 To reorderAmounts(warehouseID).Count - 1
                reorderAmountsSeries.Points.AddXY(i, reorderAmounts(warehouseID)(i))
            Next

            ' Add series to the chart
            ReorderAmountsChart.Series.Add(reorderAmountsSeries)
        Next

        '' Add 'titles to the charts and axis
        Dim graphTitle As New Title()
        graphTitle.Text = "Reorder Amounts throughout the Optimisation Process"
        graphTitle.Font = New Font("Arial", 12, FontStyle.Bold)
        ReorderAmountsChart.Titles.Add(graphTitle)
        ReorderAmountsChart.ChartAreas(0).AxisX.Title = "Iteration"
        ReorderAmountsChart.ChartAreas(0).AxisX.TitleFont = New Font("Arial", 8, FontStyle.Bold)
        ReorderAmountsChart.ChartAreas(0).AxisY.Title = "Reorder Amounts (Per Item)"
        ReorderAmountsChart.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 8, FontStyle.Bold)

        ReorderAmountsChart.ChartAreas(0).AxisX.Minimum = 0

    End Sub

End Class