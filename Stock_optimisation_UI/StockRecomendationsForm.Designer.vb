<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class StockRecomendationsForm
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
        Dim ChartArea1 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend1 As System.Windows.Forms.DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series1 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Dim ChartArea2 As System.Windows.Forms.DataVisualization.Charting.ChartArea = New DataVisualization.Charting.ChartArea()
        Dim Legend2 As System.Windows.Forms.DataVisualization.Charting.Legend = New DataVisualization.Charting.Legend()
        Dim Series2 As System.Windows.Forms.DataVisualization.Charting.Series = New DataVisualization.Charting.Series()
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Label1 = New Label()
        ReorderAmountsTabPage = New TabPage()
        Label5 = New Label()
        Label6 = New Label()
        ReorderAmountsChart = New DataVisualization.Charting.Chart()
        ReorderPointTabPage = New TabPage()
        Label3 = New Label()
        Label4 = New Label()
        ReorderPointsChart = New DataVisualization.Charting.Chart()
        TabPage1 = New TabPage()
        Label2 = New Label()
        StockWizDataGrid = New DataGridView()
        lblSuggestedTitle = New Label()
        lblOriginalValTitle = New Label()
        lblHoldingCostRec = New Label()
        lblGrossRevenueRec = New Label()
        lblAveServicelvlRec = New Label()
        lblHoldingCostOrg = New Label()
        LblResultsSummaryTitle = New Label()
        lblSummaryTitle3 = New Label()
        lblSummaryTitle2 = New Label()
        lblGrossRevenueOrg = New Label()
        lblSummaryTitle1 = New Label()
        lblAveServicelvlOrg = New Label()
        TabControl1 = New TabControl()
        ReorderAmountsTabPage.SuspendLayout()
        CType(ReorderAmountsChart, ComponentModel.ISupportInitialize).BeginInit()
        ReorderPointTabPage.SuspendLayout()
        CType(ReorderPointsChart, ComponentModel.ISupportInitialize).BeginInit()
        TabPage1.SuspendLayout()
        CType(StockWizDataGrid, ComponentModel.ISupportInitialize).BeginInit()
        TabControl1.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial", 21.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label1.Location = New Point(226, 19)
        Label1.Name = "Label1"
        Label1.Size = New Size(310, 34)
        Label1.TabIndex = 0
        Label1.Text = "Stock Wizard Results"
        ' 
        ' ReorderAmountsTabPage
        ' 
        ReorderAmountsTabPage.Controls.Add(Label5)
        ReorderAmountsTabPage.Controls.Add(Label6)
        ReorderAmountsTabPage.Controls.Add(ReorderAmountsChart)
        ReorderAmountsTabPage.Location = New Point(4, 24)
        ReorderAmountsTabPage.Name = "ReorderAmountsTabPage"
        ReorderAmountsTabPage.Padding = New Padding(3)
        ReorderAmountsTabPage.Size = New Size(730, 393)
        ReorderAmountsTabPage.TabIndex = 2
        ReorderAmountsTabPage.Text = "Reorder Amonts"
        ReorderAmountsTabPage.UseVisualStyleBackColor = True
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        Label5.Location = New Point(6, 52)
        Label5.Name = "Label5"
        Label5.Size = New Size(686, 32)
        Label5.TabIndex = 12
        Label5.Text = "The below graph shows how the reorder amounts changed through the optimisation process. A line that flattens in the " & vbCrLf & "later iterations suggests an optimal solution. "
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Font = New Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point)
        Label6.Location = New Point(10, 18)
        Label6.Name = "Label6"
        Label6.RightToLeft = RightToLeft.No
        Label6.Size = New Size(246, 25)
        Label6.TabIndex = 11
        Label6.Text = "Reorder Amounts Graph"
        ' 
        ' ReorderAmountsChart
        ' 
        ChartArea1.Name = "ChartArea1"
        ReorderAmountsChart.ChartAreas.Add(ChartArea1)
        Legend1.Name = "Legend1"
        ReorderAmountsChart.Legends.Add(Legend1)
        ReorderAmountsChart.Location = New Point(10, 100)
        ReorderAmountsChart.Name = "ReorderAmountsChart"
        Series1.ChartArea = "ChartArea1"
        Series1.Legend = "Legend1"
        Series1.Name = "Series1"
        ReorderAmountsChart.Series.Add(Series1)
        ReorderAmountsChart.Size = New Size(702, 290)
        ReorderAmountsChart.TabIndex = 2
        ' 
        ' ReorderPointTabPage
        ' 
        ReorderPointTabPage.Controls.Add(Label3)
        ReorderPointTabPage.Controls.Add(Label4)
        ReorderPointTabPage.Controls.Add(ReorderPointsChart)
        ReorderPointTabPage.Location = New Point(4, 24)
        ReorderPointTabPage.Name = "ReorderPointTabPage"
        ReorderPointTabPage.Padding = New Padding(3)
        ReorderPointTabPage.Size = New Size(730, 393)
        ReorderPointTabPage.TabIndex = 1
        ReorderPointTabPage.Text = "Reorder Points"
        ReorderPointTabPage.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        Label3.Location = New Point(6, 52)
        Label3.Name = "Label3"
        Label3.Size = New Size(671, 32)
        Label3.TabIndex = 10
        Label3.Text = "The below graph shows how the reorder points changed through the optimisation process. A line that flattens in the " & vbCrLf & "later iterations suggests an optimal solution. "
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Arial", 16F, FontStyle.Regular, GraphicsUnit.Point)
        Label4.Location = New Point(10, 18)
        Label4.Name = "Label4"
        Label4.RightToLeft = RightToLeft.No
        Label4.Size = New Size(223, 25)
        Label4.TabIndex = 9
        Label4.Text = "Reorder Points Graph"
        ' 
        ' ReorderPointsChart
        ' 
        ChartArea2.Name = "ChartArea1"
        ReorderPointsChart.ChartAreas.Add(ChartArea2)
        Legend2.Name = "Legend1"
        ReorderPointsChart.Legends.Add(Legend2)
        ReorderPointsChart.Location = New Point(6, 97)
        ReorderPointsChart.Name = "ReorderPointsChart"
        Series2.ChartArea = "ChartArea1"
        Series2.Legend = "Legend1"
        Series2.Name = "Series1"
        ReorderPointsChart.Series.Add(Series2)
        ReorderPointsChart.Size = New Size(709, 290)
        ReorderPointsChart.TabIndex = 1
        ' 
        ' TabPage1
        ' 
        TabPage1.Controls.Add(Label2)
        TabPage1.Controls.Add(StockWizDataGrid)
        TabPage1.Controls.Add(lblSuggestedTitle)
        TabPage1.Controls.Add(lblOriginalValTitle)
        TabPage1.Controls.Add(lblHoldingCostRec)
        TabPage1.Controls.Add(lblGrossRevenueRec)
        TabPage1.Controls.Add(lblAveServicelvlRec)
        TabPage1.Controls.Add(lblHoldingCostOrg)
        TabPage1.Controls.Add(LblResultsSummaryTitle)
        TabPage1.Controls.Add(lblSummaryTitle3)
        TabPage1.Controls.Add(lblSummaryTitle2)
        TabPage1.Controls.Add(lblGrossRevenueOrg)
        TabPage1.Controls.Add(lblSummaryTitle1)
        TabPage1.Controls.Add(lblAveServicelvlOrg)
        TabPage1.Location = New Point(4, 24)
        TabPage1.Name = "TabPage1"
        TabPage1.Padding = New Padding(3)
        TabPage1.Size = New Size(730, 401)
        TabPage1.TabIndex = 0
        TabPage1.Text = "Results Summary"
        TabPage1.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial", 9.75F, FontStyle.Regular, GraphicsUnit.Point)
        Label2.Location = New Point(12, 173)
        Label2.Name = "Label2"
        Label2.Size = New Size(482, 16)
        Label2.TabIndex = 23
        Label2.Text = "The table below shows results for a simulation run at the final recomended values. "
        ' 
        ' StockWizDataGrid
        ' 
        StockWizDataGrid.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = Color.LightSkyBlue
        DataGridViewCellStyle1.SelectionBackColor = Color.White
        StockWizDataGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        StockWizDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        StockWizDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = SystemColors.ControlDark
        DataGridViewCellStyle2.Font = New Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle2.ForeColor = SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.ControlDark
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.Desktop
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
        StockWizDataGrid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        StockWizDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = Color.AliceBlue
        DataGridViewCellStyle3.Font = New Font("Arial", 8.25F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle3.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle3.SelectionBackColor = Color.AliceBlue
        DataGridViewCellStyle3.SelectionForeColor = SystemColors.ControlText
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
        StockWizDataGrid.DefaultCellStyle = DataGridViewCellStyle3
        StockWizDataGrid.Enabled = False
        StockWizDataGrid.Location = New Point(6, 195)
        StockWizDataGrid.MaximumSize = New Size(711, 308)
        StockWizDataGrid.Name = "StockWizDataGrid"
        StockWizDataGrid.ReadOnly = True
        StockWizDataGrid.RowTemplate.Height = 25
        StockWizDataGrid.Size = New Size(711, 200)
        StockWizDataGrid.TabIndex = 22
        ' 
        ' lblSuggestedTitle
        ' 
        lblSuggestedTitle.AutoSize = True
        lblSuggestedTitle.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point)
        lblSuggestedTitle.Location = New Point(12, 123)
        lblSuggestedTitle.Name = "lblSuggestedTitle"
        lblSuggestedTitle.Size = New Size(171, 19)
        lblSuggestedTitle.TabIndex = 20
        lblSuggestedTitle.Text = "Suggested Allocation"
        ' 
        ' lblOriginalValTitle
        ' 
        lblOriginalValTitle.AutoSize = True
        lblOriginalValTitle.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point)
        lblOriginalValTitle.Location = New Point(12, 95)
        lblOriginalValTitle.Name = "lblOriginalValTitle"
        lblOriginalValTitle.Size = New Size(147, 19)
        lblOriginalValTitle.TabIndex = 19
        lblOriginalValTitle.Text = "Original Allocation"
        ' 
        ' lblHoldingCostRec
        ' 
        lblHoldingCostRec.AutoSize = True
        lblHoldingCostRec.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point)
        lblHoldingCostRec.Location = New Point(584, 124)
        lblHoldingCostRec.Name = "lblHoldingCostRec"
        lblHoldingCostRec.Size = New Size(110, 18)
        lblHoldingCostRec.TabIndex = 18
        lblHoldingCostRec.Text = "$0,000,000.00"
        ' 
        ' lblGrossRevenueRec
        ' 
        lblGrossRevenueRec.AutoSize = True
        lblGrossRevenueRec.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point)
        lblGrossRevenueRec.Location = New Point(410, 124)
        lblGrossRevenueRec.Name = "lblGrossRevenueRec"
        lblGrossRevenueRec.Size = New Size(110, 18)
        lblGrossRevenueRec.TabIndex = 17
        lblGrossRevenueRec.Text = "$0,000,000.00"
        ' 
        ' lblAveServicelvlRec
        ' 
        lblAveServicelvlRec.AutoSize = True
        lblAveServicelvlRec.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point)
        lblAveServicelvlRec.Location = New Point(238, 124)
        lblAveServicelvlRec.Name = "lblAveServicelvlRec"
        lblAveServicelvlRec.Size = New Size(53, 18)
        lblAveServicelvlRec.TabIndex = 16
        lblAveServicelvlRec.Text = "00.4%"
        ' 
        ' lblHoldingCostOrg
        ' 
        lblHoldingCostOrg.AutoSize = True
        lblHoldingCostOrg.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point)
        lblHoldingCostOrg.Location = New Point(584, 95)
        lblHoldingCostOrg.Name = "lblHoldingCostOrg"
        lblHoldingCostOrg.Size = New Size(110, 18)
        lblHoldingCostOrg.TabIndex = 15
        lblHoldingCostOrg.Text = "$0,000,000.00"
        ' 
        ' LblResultsSummaryTitle
        ' 
        LblResultsSummaryTitle.AutoSize = True
        LblResultsSummaryTitle.Font = New Font("Arial", 15.75F, FontStyle.Regular, GraphicsUnit.Point)
        LblResultsSummaryTitle.Location = New Point(10, 18)
        LblResultsSummaryTitle.Name = "LblResultsSummaryTitle"
        LblResultsSummaryTitle.Size = New Size(197, 24)
        LblResultsSummaryTitle.TabIndex = 9
        LblResultsSummaryTitle.Text = "Summary of Results"
        ' 
        ' lblSummaryTitle3
        ' 
        lblSummaryTitle3.AutoSize = True
        lblSummaryTitle3.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point)
        lblSummaryTitle3.Location = New Point(584, 50)
        lblSummaryTitle3.Name = "lblSummaryTitle3"
        lblSummaryTitle3.Size = New Size(118, 19)
        lblSummaryTitle3.TabIndex = 14
        lblSummaryTitle3.Text = "Holding Costs"
        ' 
        ' lblSummaryTitle2
        ' 
        lblSummaryTitle2.AutoSize = True
        lblSummaryTitle2.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point)
        lblSummaryTitle2.Location = New Point(420, 50)
        lblSummaryTitle2.Name = "lblSummaryTitle2"
        lblSummaryTitle2.Size = New Size(100, 19)
        lblSummaryTitle2.TabIndex = 12
        lblSummaryTitle2.Text = "Gross Profit"
        ' 
        ' lblGrossRevenueOrg
        ' 
        lblGrossRevenueOrg.AutoSize = True
        lblGrossRevenueOrg.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point)
        lblGrossRevenueOrg.Location = New Point(410, 95)
        lblGrossRevenueOrg.Name = "lblGrossRevenueOrg"
        lblGrossRevenueOrg.Size = New Size(110, 18)
        lblGrossRevenueOrg.TabIndex = 13
        lblGrossRevenueOrg.Text = "$0,000,000.00"
        ' 
        ' lblSummaryTitle1
        ' 
        lblSummaryTitle1.AutoSize = True
        lblSummaryTitle1.Font = New Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point)
        lblSummaryTitle1.Location = New Point(185, 50)
        lblSummaryTitle1.Name = "lblSummaryTitle1"
        lblSummaryTitle1.Size = New Size(177, 19)
        lblSummaryTitle1.TabIndex = 10
        lblSummaryTitle1.Text = "Average Service Level"
        ' 
        ' lblAveServicelvlOrg
        ' 
        lblAveServicelvlOrg.AutoSize = True
        lblAveServicelvlOrg.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point)
        lblAveServicelvlOrg.Location = New Point(238, 95)
        lblAveServicelvlOrg.Name = "lblAveServicelvlOrg"
        lblAveServicelvlOrg.Size = New Size(53, 18)
        lblAveServicelvlOrg.TabIndex = 11
        lblAveServicelvlOrg.Text = "00.4%"
        ' 
        ' TabControl1
        ' 
        TabControl1.Controls.Add(TabPage1)
        TabControl1.Controls.Add(ReorderPointTabPage)
        TabControl1.Controls.Add(ReorderAmountsTabPage)
        TabControl1.Location = New Point(12, 56)
        TabControl1.Name = "TabControl1"
        TabControl1.SelectedIndex = 0
        TabControl1.Size = New Size(738, 429)
        TabControl1.TabIndex = 1
        ' 
        ' StockRecomendationsForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(757, 497)
        Controls.Add(TabControl1)
        Controls.Add(Label1)
        Name = "StockRecomendationsForm"
        Text = "Stock Optimisation Results"
        ReorderAmountsTabPage.ResumeLayout(False)
        ReorderAmountsTabPage.PerformLayout()
        CType(ReorderAmountsChart, ComponentModel.ISupportInitialize).EndInit()
        ReorderPointTabPage.ResumeLayout(False)
        ReorderPointTabPage.PerformLayout()
        CType(ReorderPointsChart, ComponentModel.ISupportInitialize).EndInit()
        TabPage1.ResumeLayout(False)
        TabPage1.PerformLayout()
        CType(StockWizDataGrid, ComponentModel.ISupportInitialize).EndInit()
        TabControl1.ResumeLayout(False)
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Label1 As Label
    Friend WithEvents ReorderAmountsTabPage As TabPage
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents ReorderAmountsChart As DataVisualization.Charting.Chart
    Friend WithEvents ReorderPointTabPage As TabPage
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents ReorderPointsChart As DataVisualization.Charting.Chart
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents Label2 As Label
    Friend WithEvents StockWizDataGrid As DataGridView
    Friend WithEvents lblSuggestedTitle As Label
    Friend WithEvents lblOriginalValTitle As Label
    Friend WithEvents lblHoldingCostRec As Label
    Friend WithEvents lblGrossRevenueRec As Label
    Friend WithEvents lblAveServicelvlRec As Label
    Friend WithEvents lblHoldingCostOrg As Label
    Friend WithEvents LblResultsSummaryTitle As Label
    Friend WithEvents lblSummaryTitle3 As Label
    Friend WithEvents lblSummaryTitle2 As Label
    Friend WithEvents lblGrossRevenueOrg As Label
    Friend WithEvents lblSummaryTitle1 As Label
    Friend WithEvents lblAveServicelvlOrg As Label
    Friend WithEvents TabControl1 As TabControl
End Class
