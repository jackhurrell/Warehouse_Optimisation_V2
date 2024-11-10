Imports Warehouse_Optimisation
Imports System.Windows.Forms.DataVisualization.Charting
Imports System.Text.RegularExpressions
Public Class MonteResultsForm

    Dim results_to_disp As Monte_Carlo_results
    Dim inputs As List(Of Warehouse_inputs)
    Sub New(results As Monte_Carlo_results, inputs As List(Of Warehouse_inputs))

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.results_to_disp = results
        Me.inputs = inputs
    End Sub

    Private Sub MonteResultsForm_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''First the values are unpacked

        LoadTableOfResults()
        LoadServiceLevelChart()
        Calculate_display_summaries()
        LoadReorderCostChart()

        For i As Integer = 0 To results_to_disp.Warehouse_order.Count - 1
            LoadDynamicTab(i)
        Next
    End Sub


    ''' <summary>
    ''' This function calculate the three summary values and displays then on the first tab of teh 
    ''' Monte Carlo reults form
    ''' </summary>
    ''' <remarks>
    ''' This function calculates the total service level, gross revenue and holding costs for the simulation
    ''' and displays them on the first tab of the Monte Carlo results form.
    ''' </remarks>
    Private Sub Calculate_display_summaries()

        Dim total_sold = calculate_total_stock_services(Me.results_to_disp.Demand_totals, Me.results_to_disp.Service_levels)
        Dim total_demand = calculate_averages_by_warehouse(Me.results_to_disp.Demand_totals).Sum()

        Dim total_service_level = total_sold / total_demand

        If inputs.Count < 1 Then
            Throw New Exception("No inputs provided for the Monte Carlo Simulation")
        End If

        Dim gross_profit = total_sold * Me.inputs(0).profit_per_sale

        Dim total_storage_costs = calculate_averages_by_warehouse(Me.results_to_disp.Storage_costs).Sum()

        '''displays the values in correct formattig

        lblAveServicelvl.Text = total_service_level.ToString("P")
        lblGrossRevenue.Text = gross_profit.ToString("C")
        lblHoldingCost.Text = total_storage_costs.ToString("C")

    End Sub


    ''' <summary>
    ''' This function calculates the average service level for each warehouse
    ''' </summary>
    ''' <returns></returns>
    ''' <summary>
    ''' This function creates a dictionary that maps warehouse IDs to their position in the warehouse order list
    ''' </summary>
    ''' <param name="warehouse_order"></param>
    ''' <returns></returns>

    Private Sub LoadDynamicTab(warehouse_index As Integer)
        Dim warehouse_ID = results_to_disp.Warehouse_order(warehouse_index)

        Dim tabPage As New TabPage()
        tabPage.Name = "Warehouse_" & warehouse_ID & "_Results"

        tabPage.Text = "Warehouse " & warehouse_ID & " Results"

        '''This part adds all the labels to the pages
        Dim warehouseTitle As New Label()
        Dim lblServiceLevelTitle As New Label()
        Dim lblGrossProfitTitle As New Label()
        Dim lblHoldingCostTitle As New Label()
        Dim lblServiceLevel As New Label()
        Dim lblGrossProfit As New Label()
        Dim lblHoldingCost As New Label()

        Dim labels_list = New List(Of Label) From {warehouseTitle, lblServiceLevelTitle, lblGrossProfitTitle, lblHoldingCostTitle,
                                                    lblServiceLevel, lblGrossProfit, lblHoldingCost}

        Dim titleFont As New Font("Arial", 12, FontStyle.Bold)
        Dim valueFont As New Font("Arial", 12, FontStyle.Regular)

        warehouseTitle.Font = New Font("Arial", 16)
        lblServiceLevelTitle.Font = titleFont
        lblGrossProfitTitle.Font = titleFont
        lblHoldingCostTitle.Font = titleFont
        lblServiceLevel.Font = valueFont
        lblGrossProfit.Font = valueFont
        lblHoldingCost.Font = valueFont

        warehouseTitle.Location = New Point(6, 16)
        lblServiceLevelTitle.Location = New Point(41, 64)
        lblServiceLevel.Location = New Point(94, 109)
        lblGrossProfitTitle.Location = New Point(335, 64)
        lblGrossProfit.Location = New Point(335, 109)
        lblHoldingCostTitle.Location = New Point(564, 64)
        lblHoldingCost.Location = New Point(564, 109)

        warehouseTitle.Text = "Warehouse " & warehouse_ID & " Results"
        lblServiceLevelTitle.Text = "Average Service Level:"
        lblGrossProfitTitle.Text = "Gross Profit:"
        lblHoldingCostTitle.Text = "Holding Cost:"

        Dim summary_values = calculate_warehouse_summaries(warehouse_index)

        lblServiceLevel.Text = summary_values.Item1
        lblGrossProfit.Text = summary_values.Item2
        lblHoldingCost.Text = summary_values.Item3

        For Each label In labels_list
            label.AutoSize = True
            tabPage.Controls.Add(label)
        Next

        Dim histogramChart As New Chart
        histogramChart.Size = New Size(679, 264)
        histogramChart.Location = New Point(29, 155)

        Dim HistogramLoaded = LoadHistogramForWarehouse(histogramChart, warehouse_index)
        If HistogramLoaded Then
            tabPage.Controls.Add(histogramChart)
        Else
            Dim lblNoHistogram As New Label
            lblNoHistogram.Text = "No Service Levels Defined for this Warehouse" & System.Environment.NewLine &
                                  "              No Histogram Available"
            lblNoHistogram.AutoSize = True
            lblNoHistogram.Location = New Point(200, 155)
            lblNoHistogram.Font = titleFont
            tabPage.Controls.Add(lblNoHistogram)

        End If



        ' Add the TabPage to the TabControl
        ResultsTabControl.TabPages.Add(tabPage)

        Dim WebBrowser1 As New WebBrowser()


    End Sub


    Private Function calculate_warehouse_summaries(warehouse_index As Integer) As (String, String, String)

        Dim average_service_levels = calculate_averages_by_warehouse(results_to_disp.Service_levels)
        Dim warehouse_service_level = average_service_levels(warehouse_index)

        Dim average_reorder_costs = calculate_averages_by_warehouse(results_to_disp.Reorder_costs)
        Dim warehouse_reorder_cost = average_reorder_costs(warehouse_index)

        Dim average_demand = calculate_averages_by_warehouse(results_to_disp.Demand_totals)
        Dim warehouse_demand = average_demand(warehouse_index)

        Dim gross_profit = warehouse_service_level * warehouse_demand * inputs(warehouse_index).profit_per_sale

        Dim service_level_string As String
        Dim gross_profit_string As String
        Dim holding_cost_string = warehouse_reorder_cost.ToString("C")

        If warehouse_service_level = -1 Then
            service_level_string = "N/A"
        Else
            service_level_string = warehouse_service_level.ToString("P")
        End If

        If gross_profit = 0 Then
            gross_profit_string = "N/A"
        Else
            gross_profit_string = gross_profit.ToString("C")
        End If


        Return (service_level_string, gross_profit_string, holding_cost_string)
    End Function

    Private Function LoadHistogramForWarehouse(histogram As Chart, warehouse_index As Integer) As Boolean
        Dim serviceLevels = results_to_disp.Service_levels
        Dim warehouseServiceLevels = RemapDoubleIndexList(serviceLevels, warehouse_index)
        warehouseServiceLevels = warehouseServiceLevels.Where(Function(x) x <> -1).ToList()
        If warehouseServiceLevels.Count <= 0 Then
            Return False
        End If

        Dim warehouseID = results_to_disp.Warehouse_order(warehouse_index)

        Dim numHistBuckets As Integer = 9
        Dim maxServiceLevel As Double = warehouseServiceLevels.Max()
        Dim minServiceLevel As Double = warehouseServiceLevels.Min()
        Dim bucketSize As Double = (maxServiceLevel - minServiceLevel) / numHistBuckets
        If bucketSize = 0 Then
            numHistBuckets = 1
            minServiceLevel = 0.99
            bucketSize = 0.01
        End If

        Dim frequencies(numHistBuckets - 1) As Integer

        ''Now count the frequencies for each of the buckets we just made
        For Each Value In warehouseServiceLevels
            Dim bucketIndex As Integer = Math.Floor((Value - minServiceLevel) / bucketSize)
            If bucketIndex >= numHistBuckets Then bucketIndex = numHistBuckets - 1
            frequencies(bucketIndex) += 1
        Next

        histogram.Series.Clear()
        histogram.ChartAreas.Clear()
        histogram.ChartAreas.Add(New ChartArea)

        Dim series As New Series()
        series.ChartType = SeriesChartType.Column
        series.BorderWidth = 0

        For i As Integer = 0 To frequencies.Length - 1
            Dim bucketMin = minServiceLevel + (i * bucketSize)
            Dim bucketMax = bucketMin + bucketSize
            Dim bucketLabel = Format(bucketMin, "0.000") & " - " & Format(bucketMax, "0.000")

            series.Points.AddXY(bucketLabel, frequencies(i))
        Next

        histogram.Series.Add(series)

        series.IsXValueIndexed = True

        ' Customize the histogram chart appearance if needed
        histogram.Titles.Clear()
        histogram.Titles.Add("Histogram for Warehouse " & warehouseID)

        histogram.ChartAreas(0).AxisX.Title = "Service Levels"
        histogram.ChartAreas(0).AxisY.Title = "Frequencies"
        histogram.ChartAreas(0).AxisX.Interval = 1

        Return True

    End Function


    Private Sub LoadServiceLevelChart()
        Dim warehouse_IDs = results_to_disp.Warehouse_order
        Dim service_levels = calculate_averages_by_warehouse(results_to_disp.Service_levels)

        ' Clear any existing series
        ServiceLevelChart.Series.Clear()
        ServiceLevelChart.Titles.Clear()

        'Debug.WriteLine("Warehouse IDs: " & String.Join(", ", warehouse_IDs))
        'Debug.WriteLine("Service Levels: " & String.Join(", ", service_levels))


        ' Create a new series and set its chart type
        Dim series As New Series()
        series.Name = "Service Levels"
        series.ChartType = SeriesChartType.Column

        ' Customize chart appearance
        ServiceLevelChart.ChartAreas.Clear()
        Dim chartArea As New ChartArea()
        ServiceLevelChart.ChartAreas.Add(chartArea)

        '''This exists more for when the warehouse_ID is a string
        Dim name_to_position = New Dictionary(Of Integer, Integer)
        For i As Integer = 0 To warehouse_IDs.Count - 1
            name_to_position.Add(warehouse_IDs(i), i + 1)
        Next

        ' Add data points to the series
        For i As Integer = 0 To service_levels.Count - 1
            Dim x_value = name_to_position(warehouse_IDs(i))
            If service_levels(i) <> -1 Then
                series.Points.AddXY(x_value, service_levels(i))
            Else
                series.Points.AddXY(x_value, 0)
            End If
            ServiceLevelChart.ChartAreas(0).AxisX.CustomLabels.Add(x_value - 0.5, x_value + 0.5, "Warehouse " & warehouse_IDs(i).ToString())
        Next

        ' Add the series to the chart
        ServiceLevelChart.Series.Add(series)


        ' Set axis titles
        ChartArea.AxisX.Title = "Warehouses"
        chartArea.AxisY.Title = "Service Levels"

        ServiceLevelChart.Titles.Clear()
        Dim title As New Title("Service Levels by Warehouse")
        title.Font = New Font("Arial", 13, FontStyle.Bold)
        ServiceLevelChart.Titles.Add(title)


        ' Format Y-axis tick labels to 3 decimal places
        ServiceLevelChart.ChartAreas(0).AxisY.LabelStyle.Format = "0.##%"

        Dim min_service_level = Double.MaxValue
        For i As Integer = 0 To warehouse_IDs.Count - 1
            If service_levels(i) <> -1 Then
                min_service_level = Math.Min(min_service_level, service_levels(i))
            End If
        Next

        ServiceLevelChart.Legends(0).Enabled = False

        'Sets bounds on the y-axis
        ServiceLevelChart.ChartAreas(0).AxisY.Minimum = Math.Min(0.5, min_service_level - 0.05)
        ServiceLevelChart.ChartAreas(0).AxisY.Maximum = 1.0


        ServiceLevelChart.ChartAreas(0).AxisX.TitleFont = New Font("Arial", 10, FontStyle.Bold)
        ServiceLevelChart.ChartAreas(0).AxisY.TitleFont = New Font("Arial", 11, FontStyle.Bold)
        ServiceLevelChart.ChartAreas(0).AxisX.LabelStyle.Angle = -45
        ServiceLevelChart.ChartAreas(0).AxisX.LabelStyle.IsEndLabelVisible = True

        ' Refresh the chart
        ServiceLevelChart.Invalidate()
        ServiceLevelChart.Refresh()
    End Sub


    Private Sub LoadTableOfResults()

        Dim service_level_averages = calculate_averages_by_warehouse(results_to_disp.Service_levels)
        Dim reorder_cost_averages = calculate_averages_by_warehouse(results_to_disp.Reorder_costs)
        Dim storage_cost_averages = calculate_averages_by_warehouse(results_to_disp.Storage_costs)
        Dim lost_sales_averages = calculate_averages_by_warehouse(results_to_disp.Lost_sales_costs)
        Dim id_to_position = create_id_to_position_map(results_to_disp.Warehouse_order)

        Dim grid_data As New DataTable()

        grid_data.Columns.Add("Warehouse ID")
        grid_data.Columns.Add("Given Reorder Point")
        grid_data.Columns.Add("Given Reorder Amount")
        grid_data.Columns.Add("Service Level")
        grid_data.Columns.Add("Reorder Costs")
        grid_data.Columns.Add("Storage Costs")
        grid_data.Columns.Add("Missed Profit Due to Lost Sales")

        For Each warehouse In inputs
            Dim id = warehouse.warehouse_id
            Dim temp_service_level As String
            If service_level_averages(id_to_position(id)) = -1 Then
                temp_service_level = "N/A"
            Else
                temp_service_level = Format(service_level_averages(id_to_position(id)), "0.000%")

            End If

            grid_data.Rows.Add(id, warehouse.reorder_point, warehouse.reorder_amount,
                               temp_service_level, reorder_cost_averages(id_to_position(id)).ToString("C"),
                                storage_cost_averages(id_to_position(id)).ToString("C"), lost_sales_averages(id_to_position(id)).ToString("C"))

        Next
        ResultsSummaryDataGrid.AutoSize = True
        ResultsSummaryDataGrid.RowHeadersVisible = False

        ResultsSummaryDataGrid.DataSource = grid_data
        ResultsSummaryDataGrid.ClearSelection()
        ResultsSummaryDataGrid.CurrentCell = Nothing

        ''Çhanges the headers style
        With ResultsSummaryDataGrid.ColumnHeadersDefaultCellStyle
            .Font = New Font("Arial", 8, FontStyle.Bold)
            .BackColor = Color.LightGray
        End With


    End Sub


    '''' <summary>
    '''' This then deals with the loading of all the things that occuer when the tab is changed in results
    '''' </summary>
    '''' <param name="Sender"></param>
    '''' <param name="e"></param>
    'Private Sub ResultsTabControl_SelectedIndexChanged(Sender As Object, e As EventArgs) Handles ResultsTabControl.SelectedIndexChanged

    '    Dim tabText = ResultsTabControl.SelectedTab.Text


    '    Dim regex_pattern = "Warehouse (\d+) Results"
    '    Dim match = Regex.Match(tabText, regex_pattern)
    '    If match.Success Then


    '        Dim warehouse_ID = Integer.Parse(match.Groups(1).Value)
    '        Dim histogram = CType(ResultsTabControl.SelectedTab.Tag, Chart)
    '        If histogram.Series.Count = 0 Then
    '            LoadHistogramForWarehouse(histogram, warehouse_ID)
    '        End If
    '    End If
    'End Sub


    Private Sub LoadReorderCostChart()
        Dim warehouse_IDs = results_to_disp.Warehouse_order ' List of warehouse IDs
        Dim reorder_costs = calculate_averages_by_warehouse(results_to_disp.Reorder_costs) ' List of reorder costs
        Dim storage_costs = calculate_averages_by_warehouse(results_to_disp.Storage_costs)
        Dim lost_sales_costs = calculate_averages_by_warehouse(results_to_disp.Lost_sales_costs)

        ''The *2 and  +1 to make the bars appear next to each other and approiate offsets
        For i As Integer = 0 To warehouse_IDs.Count - 1
            Dim base_x_value = ("Warehouse " & warehouse_IDs(i).ToString).ToString

            Me.CostChart.Series("Reorder Costs").Points.AddXY(base_x_value, reorder_costs(i))
            Me.CostChart.Series("Storage Costs").Points.AddXY(base_x_value, storage_costs(i))
            Me.CostChart.Series("Lost Profits").Points.AddXY(base_x_value, lost_sales_costs(i))

        Next

    End Sub





End Class