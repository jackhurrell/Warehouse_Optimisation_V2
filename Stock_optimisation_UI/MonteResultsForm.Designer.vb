<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MonteResultsForm
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim DataGridViewCellStyle7 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim ChartArea5 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend5 As System.Windows.Forms.DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series9 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Dim Series10 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Dim Series11 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Dim Title3 As System.Windows.Forms.DataVisualization.Charting.Title = New DataVisualization.Charting.Title()
        Dim ChartArea6 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend6 As System.Windows.Forms.DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series12 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        LblMonteResultsTitle = New Label()
        BackgroundWorker1 = New ComponentModel.BackgroundWorker()
        TabularResultsTabPage = New TabPage()
        Label1 = New Label()
        lblTableResultsTitle = New Label()
        ResultsSummaryDataGrid = New DataGridView()
        resultsGraphTab = New TabPage()
        Label3 = New Label()
        Label2 = New Label()
        lblCostTitle = New Label()
        CostChart = New DataVisualization.Charting.Chart()
        ResultsSummary = New TabPage()
        ServiceLevelChart = New DataVisualization.Charting.Chart()
        lblHoldingCost = New Label()
        lblSummaryTitle3 = New Label()
        lblGrossRevenue = New Label()
        lblSummaryTitle2 = New Label()
        lblAveServicelvl = New Label()
        lblSummaryTitle1 = New Label()
        LblResultsSummaryTitle = New Label()
        ResultsTabControl = New TabControl()
        TabularResultsTabPage.SuspendLayout()
        CType(ResultsSummaryDataGrid, ComponentModel.ISupportInitialize).BeginInit()
        resultsGraphTab.SuspendLayout()
        CType(CostChart, ComponentModel.ISupportInitialize).BeginInit()
        ResultsSummary.SuspendLayout()
        CType(ServiceLevelChart, ComponentModel.ISupportInitialize).BeginInit()
        ResultsTabControl.SuspendLayout()
        SuspendLayout()
        ' 
        ' LblMonteResultsTitle
        ' 
        LblMonteResultsTitle.AutoSize = True
        LblMonteResultsTitle.Location = New Point(247, 18)
        LblMonteResultsTitle.Margin = New Padding(8, 0, 8, 0)
        LblMonteResultsTitle.Name = "LblMonteResultsTitle"
        LblMonteResultsTitle.Size = New Size(278, 36)
        LblMonteResultsTitle.TabIndex = 0
        LblMonteResultsTitle.Text = "Simulation Results"
        ' 
        ' TabularResultsTabPage
        ' 
        TabularResultsTabPage.Controls.Add(Label1)
        TabularResultsTabPage.Controls.Add(lblTableResultsTitle)
        TabularResultsTabPage.Controls.Add(ResultsSummaryDataGrid)
        TabularResultsTabPage.Location = New Point(4, 23)
        TabularResultsTabPage.Name = "TabularResultsTabPage"
        TabularResultsTabPage.Padding = New Padding(3)
        TabularResultsTabPage.Size = New Size(743, 428)
        TabularResultsTabPage.TabIndex = 2
        TabularResultsTabPage.Text = "Table of Results"
        TabularResultsTabPage.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        Label1.Location = New Point(6, 62)
        Label1.Name = "Label1"
        Label1.Size = New Size(300, 16)
        Label1.TabIndex = 6
        Label1.Text = "A full list of results can be seen in the table below. "
        ' 
        ' lblTableResultsTitle
        ' 
        lblTableResultsTitle.AutoSize = True
        lblTableResultsTitle.Font = New Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point)
        lblTableResultsTitle.Location = New Point(6, 16)
        lblTableResultsTitle.Name = "lblTableResultsTitle"
        lblTableResultsTitle.Size = New Size(159, 25)
        lblTableResultsTitle.TabIndex = 5
        lblTableResultsTitle.Text = "Tabular Results"
        ' 
        ' ResultsSummaryDataGrid
        ' 
        ResultsSummaryDataGrid.AllowUserToAddRows = False
        DataGridViewCellStyle7.BackColor = Color.LightSkyBlue
        DataGridViewCellStyle7.SelectionBackColor = Color.LightSkyBlue
        DataGridViewCellStyle7.SelectionForeColor = Color.Black
        ResultsSummaryDataGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle7
        ResultsSummaryDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        ResultsSummaryDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        DataGridViewCellStyle8.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle8.BackColor = SystemColors.ControlDark
        DataGridViewCellStyle8.Font = New Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle8.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle8.SelectionBackColor = SystemColors.ControlDark
        DataGridViewCellStyle8.SelectionForeColor = SystemColors.Desktop
        DataGridViewCellStyle8.WrapMode = DataGridViewTriState.True
        ResultsSummaryDataGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle8
        ResultsSummaryDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle9.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle9.BackColor = Color.AliceBlue
        DataGridViewCellStyle9.Font = New Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle9.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle9.SelectionBackColor = Color.AliceBlue
        DataGridViewCellStyle9.SelectionForeColor = SystemColors.ControlText
        DataGridViewCellStyle9.WrapMode = DataGridViewTriState.False
        ResultsSummaryDataGrid.DefaultCellStyle = DataGridViewCellStyle9
        ResultsSummaryDataGrid.Enabled = False
        ResultsSummaryDataGrid.Location = New Point(6, 104)
        ResultsSummaryDataGrid.MaximumSize = New Size(711, 308)
        ResultsSummaryDataGrid.Name = "ResultsSummaryDataGrid"
        ResultsSummaryDataGrid.ReadOnly = True
        ResultsSummaryDataGrid.RowTemplate.Height = 25
        ResultsSummaryDataGrid.Size = New Size(711, 283)
        ResultsSummaryDataGrid.TabIndex = 4
        ' 
        ' resultsGraphTab
        ' 
        resultsGraphTab.Controls.Add(Label3)
        resultsGraphTab.Controls.Add(Label2)
        resultsGraphTab.Controls.Add(lblCostTitle)
        resultsGraphTab.Controls.Add(CostChart)
        resultsGraphTab.Location = New Point(4, 23)
        resultsGraphTab.Name = "resultsGraphTab"
        resultsGraphTab.Padding = New Padding(3)
        resultsGraphTab.Size = New Size(743, 428)
        resultsGraphTab.TabIndex = 1
        resultsGraphTab.Text = "Graph of Costs"
        resultsGraphTab.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        Label3.Location = New Point(6, 52)
        Label3.Name = "Label3"
        Label3.Size = New Size(550, 16)
        Label3.TabIndex = 8
        Label3.Text = "The below graph shows the costs at each warehouse, and the lost profit due to missed sales. "
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point)
        Label2.Location = New Point(6, 16)
        Label2.Name = "Label2"
        Label2.RightToLeft = RightToLeft.No
        Label2.Size = New Size(134, 25)
        Label2.TabIndex = 7
        Label2.Text = "Costs Graph"
        ' 
        ' lblCostTitle
        ' 
        lblCostTitle.AutoSize = True
        lblCostTitle.Font = New Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point)
        lblCostTitle.Location = New Point(27, 14)
        lblCostTitle.Name = "lblCostTitle"
        lblCostTitle.Size = New Size(0, 25)
        lblCostTitle.TabIndex = 6
        ' 
        ' CostChart
        ' 
        ChartArea5.AxisX.Title = "Warehouses"
        ChartArea5.AxisX.TitleFont = New Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        ChartArea5.AxisY.Title = "Costs ($)"
        ChartArea5.AxisY.TitleFont = New Font("Arial", 9.75F, FontStyle.Bold, GraphicsUnit.Point)
        ChartArea5.Name = "ChartArea1"
        CostChart.ChartAreas.Add(ChartArea5)
        Legend5.Name = "Legend1"
        CostChart.Legends.Add(Legend5)
        CostChart.Location = New Point(18, 83)
        CostChart.Name = "CostChart"
        Series9.ChartArea = "ChartArea1"
        Series9.Color = Color.CornflowerBlue
        Series9.IsXValueIndexed = True
        Series9.Legend = "Legend1"
        Series9.Name = "Reorder Costs"
        Series9.XValueType = DataVisualization.Charting.ChartValueType.String
        Series10.ChartArea = "ChartArea1"
        Series10.Color = Color.LightBlue
        Series10.IsXValueIndexed = True
        Series10.Legend = "Legend1"
        Series10.Name = "Storage Costs"
        Series10.XValueType = DataVisualization.Charting.ChartValueType.String
        Series11.ChartArea = "ChartArea1"
        Series11.Color = Color.OrangeRed
        Series11.IsXValueIndexed = True
        Series11.Legend = "Legend1"
        Series11.Name = "Lost Profits"
        Series11.XValueType = DataVisualization.Charting.ChartValueType.String
        CostChart.Series.Add(Series9)
        CostChart.Series.Add(Series10)
        CostChart.Series.Add(Series11)
        CostChart.Size = New Size(692, 336)
        CostChart.TabIndex = 1
        CostChart.Text = "Chart1"
        Title3.Font = New Font("Arial", 12.75F, FontStyle.Bold, GraphicsUnit.Point)
        Title3.Name = "ReorderCostTitle"
        Title3.Text = "Reorder and Storage Costs Vs Lost Profit Costs by Warehouse"
        CostChart.Titles.Add(Title3)
        ' 
        ' ResultsSummary
        ' 
        ResultsSummary.Controls.Add(ServiceLevelChart)
        ResultsSummary.Controls.Add(lblHoldingCost)
        ResultsSummary.Controls.Add(lblSummaryTitle3)
        ResultsSummary.Controls.Add(lblGrossRevenue)
        ResultsSummary.Controls.Add(lblSummaryTitle2)
        ResultsSummary.Controls.Add(lblAveServicelvl)
        ResultsSummary.Controls.Add(lblSummaryTitle1)
        ResultsSummary.Controls.Add(LblResultsSummaryTitle)
        ResultsSummary.Location = New Point(4, 23)
        ResultsSummary.Name = "ResultsSummary"
        ResultsSummary.Padding = New Padding(3)
        ResultsSummary.Size = New Size(743, 428)
        ResultsSummary.TabIndex = 0
        ResultsSummary.Text = "Results Summary"
        ResultsSummary.UseVisualStyleBackColor = True
        ' 
        ' ServiceLevelChart
        ' 
        ServiceLevelChart.Anchor = AnchorStyles.Bottom
        ChartArea6.Name = "ChartArea1"
        ServiceLevelChart.ChartAreas.Add(ChartArea6)
        Legend6.Name = "Legend1"
        ServiceLevelChart.Legends.Add(Legend6)
        ServiceLevelChart.Location = New Point(36, 155)
        ServiceLevelChart.Name = "ServiceLevelChart"
        Series12.ChartArea = "ChartArea1"
        Series12.Legend = "Legend1"
        Series12.Name = "Series1"
        ServiceLevelChart.Series.Add(Series12)
        ServiceLevelChart.Size = New Size(679, 264)
        ServiceLevelChart.TabIndex = 10
        ServiceLevelChart.Text = "Service Level Chart"
        ' 
        ' lblHoldingCost
        ' 
        lblHoldingCost.AutoSize = True
        lblHoldingCost.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point)
        lblHoldingCost.Location = New Point(564, 109)
        lblHoldingCost.Name = "lblHoldingCost"
        lblHoldingCost.Size = New Size(110, 18)
        lblHoldingCost.TabIndex = 9
        lblHoldingCost.Text = "$0,000,000.00"
        ' 
        ' lblSummaryTitle3
        ' 
        lblSummaryTitle3.AutoSize = True
        lblSummaryTitle3.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point)
        lblSummaryTitle3.Location = New Point(564, 64)
        lblSummaryTitle3.Name = "lblSummaryTitle3"
        lblSummaryTitle3.Size = New Size(118, 19)
        lblSummaryTitle3.TabIndex = 8
        lblSummaryTitle3.Text = "Holding Costs"
        ' 
        ' lblGrossRevenue
        ' 
        lblGrossRevenue.AutoSize = True
        lblGrossRevenue.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point)
        lblGrossRevenue.Location = New Point(335, 109)
        lblGrossRevenue.Name = "lblGrossRevenue"
        lblGrossRevenue.Size = New Size(110, 18)
        lblGrossRevenue.TabIndex = 7
        lblGrossRevenue.Text = "$0,000,000.00"
        ' 
        ' lblSummaryTitle2
        ' 
        lblSummaryTitle2.AutoSize = True
        lblSummaryTitle2.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point)
        lblSummaryTitle2.Location = New Point(338, 64)
        lblSummaryTitle2.Name = "lblSummaryTitle2"
        lblSummaryTitle2.Size = New Size(100, 19)
        lblSummaryTitle2.TabIndex = 6
        lblSummaryTitle2.Text = "Gross Profit"
        ' 
        ' lblAveServicelvl
        ' 
        lblAveServicelvl.AutoSize = True
        lblAveServicelvl.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point)
        lblAveServicelvl.Location = New Point(94, 109)
        lblAveServicelvl.Name = "lblAveServicelvl"
        lblAveServicelvl.Size = New Size(53, 18)
        lblAveServicelvl.TabIndex = 5
        lblAveServicelvl.Text = "00.4%"
        ' 
        ' lblSummaryTitle1
        ' 
        lblSummaryTitle1.AutoSize = True
        lblSummaryTitle1.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point)
        lblSummaryTitle1.Location = New Point(41, 64)
        lblSummaryTitle1.Name = "lblSummaryTitle1"
        lblSummaryTitle1.Size = New Size(177, 19)
        lblSummaryTitle1.TabIndex = 4
        lblSummaryTitle1.Text = "Average Service Level"
        ' 
        ' LblResultsSummaryTitle
        ' 
        LblResultsSummaryTitle.AutoSize = True
        LblResultsSummaryTitle.Font = New Font("Arial", 15.75F, FontStyle.Regular, GraphicsUnit.Point)
        LblResultsSummaryTitle.Location = New Point(6, 16)
        LblResultsSummaryTitle.Name = "LblResultsSummaryTitle"
        LblResultsSummaryTitle.Size = New Size(197, 24)
        LblResultsSummaryTitle.TabIndex = 0
        LblResultsSummaryTitle.Text = "Summary of Results"
        ' 
        ' ResultsTabControl
        ' 
        ResultsTabControl.Controls.Add(ResultsSummary)
        ResultsTabControl.Controls.Add(resultsGraphTab)
        ResultsTabControl.Controls.Add(TabularResultsTabPage)
        ResultsTabControl.Font = New Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point)
        ResultsTabControl.Location = New Point(12, 57)
        ResultsTabControl.Name = "ResultsTabControl"
        ResultsTabControl.SelectedIndex = 0
        ResultsTabControl.Size = New Size(751, 455)
        ResultsTabControl.TabIndex = 1
        ' 
        ' MonteResultsForm
        ' 
        AutoScaleDimensions = New SizeF(18F, 36F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        ClientSize = New Size(761, 511)
        Controls.Add(LblMonteResultsTitle)
        Controls.Add(ResultsTabControl)
        Font = New Font("Arial", 24F, FontStyle.Regular, GraphicsUnit.Point)
        Margin = New Padding(8)
        Name = "MonteResultsForm"
        Text = "Simulation Results"
        TabularResultsTabPage.ResumeLayout(False)
        TabularResultsTabPage.PerformLayout()
        CType(ResultsSummaryDataGrid, ComponentModel.ISupportInitialize).EndInit()
        resultsGraphTab.ResumeLayout(False)
        resultsGraphTab.PerformLayout()
        CType(CostChart, ComponentModel.ISupportInitialize).EndInit()
        ResultsSummary.ResumeLayout(False)
        ResultsSummary.PerformLayout()
        CType(ServiceLevelChart, ComponentModel.ISupportInitialize).EndInit()
        ResultsTabControl.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents LblMonteResultsTitle As Label
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents TabularResultsTabPage As TabPage
    Friend WithEvents Label1 As Label
    Friend WithEvents lblTableResultsTitle As Label
    Friend WithEvents ResultsSummaryDataGrid As DataGridView
    Friend WithEvents resultsGraphTab As TabPage
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents lblCostTitle As Label
    Friend WithEvents CostChart As DataVisualization.Charting.Chart
    Friend WithEvents ResultsSummary As TabPage
    Friend WithEvents ServiceLevelChart As DataVisualization.Charting.Chart
    Friend WithEvents lblHoldingCost As Label
    Friend WithEvents lblSummaryTitle3 As Label
    Friend WithEvents lblGrossRevenue As Label
    Friend WithEvents lblSummaryTitle2 As Label
    Friend WithEvents lblAveServicelvl As Label
    Friend WithEvents lblSummaryTitle1 As Label
    Friend WithEvents LblResultsSummaryTitle As Label
    Friend WithEvents ResultsTabControl As TabControl
End Class
