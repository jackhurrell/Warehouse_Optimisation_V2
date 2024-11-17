<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class inputsForm
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As DataGridViewCellStyle = New DataGridViewCellStyle()
        lblInputsTitle = New Label()
        InputsTabControl = New TabControl()
        WarehouseLocationsTab = New TabPage()
        BaseWarehouseRadioButton = New RadioButton()
        DependentWarehouseRadioButton = New RadioButton()
        HoldingCostTextBox = New TextBox()
        Label2 = New Label()
        lblWarehouseList = New Label()
        SubmitWarehouseLocationsBtn = New Button()
        WarehouseDataGrid = New DataGridView()
        Warehouse_ID_Location = New DataGridViewTextBoxColumn()
        HoldningCost = New DataGridViewTextBoxColumn()
        DependentWarehouse = New DataGridViewTextBoxColumn()
        Address = New DataGridViewTextBoxColumn()
        SubmitWarehouseBtn = New Button()
        Label1 = New Label()
        WarehouseIDTextBox = New TextBox()
        lblEnterWarehouseID = New Label()
        lblWarehosueLocationTitle = New Label()
        SuggestionsListBox = New ListBox()
        LblSearchAddress = New Label()
        SearchAddressTextBox = New TextBox()
        WarehouseRelationsTab = New TabPage()
        DeleteReorderRowButton = New Button()
        Label6 = New Label()
        Label5 = New Label()
        DataGridReorders = New DataGridView()
        Reorder_warehouse_id = New DataGridViewTextBoxColumn()
        Reordered_from = New DataGridViewTextBoxColumn()
        lead_time_mean = New DataGridViewTextBoxColumn()
        Lead_time_std_dev = New DataGridViewTextBoxColumn()
        Reorder_cost = New DataGridViewTextBoxColumn()
        btnSubmitReorderInputs = New Button()
        ItemInputsTab = New TabPage()
        Label3 = New Label()
        lblProfitPerSale = New Label()
        lblItemsPerPallet = New Label()
        txtboxProfitPerSale = New TextBox()
        txtboxItemsPerPallet = New TextBox()
        btnSubmitItemInputs = New Button()
        WarehouseAndSKUInputsTab = New TabPage()
        Label4 = New Label()
        DataGridWarehouse = New DataGridView()
        Warehouse_ID = New DataGridViewTextBoxColumn()
        Initial_inventory = New DataGridViewTextBoxColumn()
        Demand_mean = New DataGridViewTextBoxColumn()
        Demand_std_dev = New DataGridViewTextBoxColumn()
        DemandType = New DataGridViewTextBoxColumn()
        Reorder_point = New DataGridViewTextBoxColumn()
        Reorder_amount = New DataGridViewTextBoxColumn()
        Site_type = New DataGridViewComboBoxColumn()
        Holding_cost_per_pallet = New DataGridViewTextBoxColumn()
        btnNextWarehouse = New Button()
        ShowWarehouseTabPage = New TabPage()
        AlterInputsButton = New Button()
        FinaliseInputsButton = New Button()
        Label7 = New Label()
        MapsWebView = New Microsoft.Web.WebView2.WinForms.WebView2()
        computeSuggestions = New ComponentModel.BackgroundWorker()
        InputsTabControl.SuspendLayout()
        WarehouseLocationsTab.SuspendLayout()
        CType(WarehouseDataGrid, ComponentModel.ISupportInitialize).BeginInit()
        WarehouseRelationsTab.SuspendLayout()
        CType(DataGridReorders, ComponentModel.ISupportInitialize).BeginInit()
        ItemInputsTab.SuspendLayout()
        WarehouseAndSKUInputsTab.SuspendLayout()
        CType(DataGridWarehouse, ComponentModel.ISupportInitialize).BeginInit()
        ShowWarehouseTabPage.SuspendLayout()
        CType(MapsWebView, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' lblInputsTitle
        ' 
        lblInputsTitle.AutoSize = True
        lblInputsTitle.Font = New Font("Arial", 24F, FontStyle.Bold, GraphicsUnit.Point)
        lblInputsTitle.Location = New Point(222, 9)
        lblInputsTitle.Name = "lblInputsTitle"
        lblInputsTitle.Size = New Size(399, 37)
        lblInputsTitle.TabIndex = 0
        lblInputsTitle.Text = "Please Enter Your Inputs "
        ' 
        ' InputsTabControl
        ' 
        InputsTabControl.Controls.Add(WarehouseLocationsTab)
        InputsTabControl.Controls.Add(WarehouseRelationsTab)
        InputsTabControl.Controls.Add(ItemInputsTab)
        InputsTabControl.Controls.Add(WarehouseAndSKUInputsTab)
        InputsTabControl.Controls.Add(ShowWarehouseTabPage)
        InputsTabControl.Location = New Point(12, 49)
        InputsTabControl.Name = "InputsTabControl"
        InputsTabControl.SelectedIndex = 0
        InputsTabControl.Size = New Size(776, 404)
        InputsTabControl.TabIndex = 1
        ' 
        ' WarehouseLocationsTab
        ' 
        WarehouseLocationsTab.Controls.Add(BaseWarehouseRadioButton)
        WarehouseLocationsTab.Controls.Add(DependentWarehouseRadioButton)
        WarehouseLocationsTab.Controls.Add(HoldingCostTextBox)
        WarehouseLocationsTab.Controls.Add(Label2)
        WarehouseLocationsTab.Controls.Add(lblWarehouseList)
        WarehouseLocationsTab.Controls.Add(SubmitWarehouseLocationsBtn)
        WarehouseLocationsTab.Controls.Add(WarehouseDataGrid)
        WarehouseLocationsTab.Controls.Add(SubmitWarehouseBtn)
        WarehouseLocationsTab.Controls.Add(Label1)
        WarehouseLocationsTab.Controls.Add(WarehouseIDTextBox)
        WarehouseLocationsTab.Controls.Add(lblEnterWarehouseID)
        WarehouseLocationsTab.Controls.Add(lblWarehosueLocationTitle)
        WarehouseLocationsTab.Controls.Add(SuggestionsListBox)
        WarehouseLocationsTab.Controls.Add(LblSearchAddress)
        WarehouseLocationsTab.Controls.Add(SearchAddressTextBox)
        WarehouseLocationsTab.Location = New Point(4, 24)
        WarehouseLocationsTab.Name = "WarehouseLocationsTab"
        WarehouseLocationsTab.Padding = New Padding(3)
        WarehouseLocationsTab.Size = New Size(768, 376)
        WarehouseLocationsTab.TabIndex = 4
        WarehouseLocationsTab.Text = "Warehouse Inputs"
        WarehouseLocationsTab.UseVisualStyleBackColor = True
        ' 
        ' BaseWarehouseRadioButton
        ' 
        BaseWarehouseRadioButton.AutoSize = True
        BaseWarehouseRadioButton.Location = New Point(185, 116)
        BaseWarehouseRadioButton.Name = "BaseWarehouseRadioButton"
        BaseWarehouseRadioButton.Size = New Size(111, 19)
        BaseWarehouseRadioButton.TabIndex = 15
        BaseWarehouseRadioButton.TabStop = True
        BaseWarehouseRadioButton.Text = "Base Warehosue"
        BaseWarehouseRadioButton.UseVisualStyleBackColor = True
        ' 
        ' DependentWarehouseRadioButton
        ' 
        DependentWarehouseRadioButton.AutoSize = True
        DependentWarehouseRadioButton.Location = New Point(16, 116)
        DependentWarehouseRadioButton.Name = "DependentWarehouseRadioButton"
        DependentWarehouseRadioButton.Size = New Size(145, 19)
        DependentWarehouseRadioButton.TabIndex = 14
        DependentWarehouseRadioButton.TabStop = True
        DependentWarehouseRadioButton.Text = "Dependent Warehouse"
        DependentWarehouseRadioButton.UseVisualStyleBackColor = True
        ' 
        ' HoldingCostTextBox
        ' 
        HoldingCostTextBox.Font = New Font("Arial", 11.25F, FontStyle.Regular, GraphicsUnit.Point)
        HoldingCostTextBox.Location = New Point(152, 75)
        HoldingCostTextBox.Name = "HoldingCostTextBox"
        HoldingCostTextBox.Size = New Size(134, 25)
        HoldingCostTextBox.TabIndex = 13
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point)
        Label2.Location = New Point(52, 79)
        Label2.Name = "Label2"
        Label2.Size = New Size(94, 32)
        Label2.TabIndex = 12
        Label2.Text = "Holding Costs" & vbCrLf & vbCrLf
        ' 
        ' lblWarehouseList
        ' 
        lblWarehouseList.AutoSize = True
        lblWarehouseList.Font = New Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point)
        lblWarehouseList.Location = New Point(364, 14)
        lblWarehouseList.Name = "lblWarehouseList"
        lblWarehouseList.Size = New Size(153, 22)
        lblWarehouseList.TabIndex = 11
        lblWarehouseList.Text = "Warehouse List"
        ' 
        ' SubmitWarehouseLocationsBtn
        ' 
        SubmitWarehouseLocationsBtn.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        SubmitWarehouseLocationsBtn.Location = New Point(364, 347)
        SubmitWarehouseLocationsBtn.Name = "SubmitWarehouseLocationsBtn"
        SubmitWarehouseLocationsBtn.Size = New Size(191, 23)
        SubmitWarehouseLocationsBtn.TabIndex = 10
        SubmitWarehouseLocationsBtn.Text = "Submit Warehouse Inputs"
        SubmitWarehouseLocationsBtn.UseVisualStyleBackColor = True
        ' 
        ' WarehouseDataGrid
        ' 
        WarehouseDataGrid.AllowUserToAddRows = False
        DataGridViewCellStyle1.BackColor = Color.LightSkyBlue
        DataGridViewCellStyle1.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle1.SelectionForeColor = Color.Black
        WarehouseDataGrid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        WarehouseDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        WarehouseDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        WarehouseDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        WarehouseDataGrid.Columns.AddRange(New DataGridViewColumn() {Warehouse_ID_Location, HoldningCost, DependentWarehouse, Address})
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Color.AliceBlue
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle2.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.False
        WarehouseDataGrid.DefaultCellStyle = DataGridViewCellStyle2
        WarehouseDataGrid.Location = New Point(364, 56)
        WarehouseDataGrid.Name = "WarehouseDataGrid"
        WarehouseDataGrid.RowHeadersVisible = False
        WarehouseDataGrid.RowTemplate.Height = 25
        WarehouseDataGrid.Size = New Size(398, 274)
        WarehouseDataGrid.TabIndex = 9
        ' 
        ' Warehouse_ID_Location
        ' 
        Warehouse_ID_Location.FillWeight = 59F
        Warehouse_ID_Location.HeaderText = "Warehouse ID"
        Warehouse_ID_Location.Name = "Warehouse_ID_Location"
        ' 
        ' HoldningCost
        ' 
        HoldningCost.FillWeight = 59F
        HoldningCost.HeaderText = "Holding Cost "
        HoldningCost.Name = "HoldningCost"
        ' 
        ' DependentWarehouse
        ' 
        DependentWarehouse.FillWeight = 59F
        DependentWarehouse.HeaderText = "Warehouse Type"
        DependentWarehouse.Name = "DependentWarehouse"
        DependentWarehouse.ReadOnly = True
        ' 
        ' Address
        ' 
        Address.FillWeight = 100.7317F
        Address.HeaderText = "Address"
        Address.Name = "Address"
        ' 
        ' SubmitWarehouseBtn
        ' 
        SubmitWarehouseBtn.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        SubmitWarehouseBtn.Location = New Point(6, 347)
        SubmitWarehouseBtn.Name = "SubmitWarehouseBtn"
        SubmitWarehouseBtn.Size = New Size(191, 23)
        SubmitWarehouseBtn.TabIndex = 8
        SubmitWarehouseBtn.Text = "Add Warehouse To Network"
        SubmitWarehouseBtn.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial", 8F, FontStyle.Regular, GraphicsUnit.Point)
        Label1.Location = New Point(16, 316)
        Label1.Name = "Label1"
        Label1.Size = New Size(266, 14)
        Label1.TabIndex = 6
        Label1.Text = "Double Click on Suggested Address Above to Select It"
        ' 
        ' WarehouseIDTextBox
        ' 
        WarehouseIDTextBox.Font = New Font("Arial", 11.25F, FontStyle.Regular, GraphicsUnit.Point)
        WarehouseIDTextBox.Location = New Point(152, 43)
        WarehouseIDTextBox.Name = "WarehouseIDTextBox"
        WarehouseIDTextBox.Size = New Size(134, 25)
        WarehouseIDTextBox.TabIndex = 5
        ' 
        ' lblEnterWarehouseID
        ' 
        lblEnterWarehouseID.AutoSize = True
        lblEnterWarehouseID.Font = New Font("Arial", 10F, FontStyle.Regular, GraphicsUnit.Point)
        lblEnterWarehouseID.Location = New Point(12, 47)
        lblEnterWarehouseID.Name = "lblEnterWarehouseID"
        lblEnterWarehouseID.Size = New Size(134, 16)
        lblEnterWarehouseID.TabIndex = 4
        lblEnterWarehouseID.Text = "Enter Warehouse ID"
        ' 
        ' lblWarehosueLocationTitle
        ' 
        lblWarehosueLocationTitle.AutoSize = True
        lblWarehosueLocationTitle.Font = New Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point)
        lblWarehosueLocationTitle.Location = New Point(6, 14)
        lblWarehosueLocationTitle.Name = "lblWarehosueLocationTitle"
        lblWarehosueLocationTitle.Size = New Size(280, 22)
        lblWarehosueLocationTitle.TabIndex = 3
        lblWarehosueLocationTitle.Text = "Enter Warehouse Information"
        ' 
        ' SuggestionsListBox
        ' 
        SuggestionsListBox.FormattingEnabled = True
        SuggestionsListBox.ItemHeight = 15
        SuggestionsListBox.Location = New Point(12, 204)
        SuggestionsListBox.Name = "SuggestionsListBox"
        SuggestionsListBox.Size = New Size(322, 109)
        SuggestionsListBox.TabIndex = 2
        ' 
        ' LblSearchAddress
        ' 
        LblSearchAddress.AutoSize = True
        LblSearchAddress.Font = New Font("Arial", 11.25F, FontStyle.Regular, GraphicsUnit.Point)
        LblSearchAddress.Location = New Point(12, 153)
        LblSearchAddress.Name = "LblSearchAddress"
        LblSearchAddress.Size = New Size(144, 17)
        LblSearchAddress.TabIndex = 1
        LblSearchAddress.Text = "Enter Address Below"
        ' 
        ' SearchAddressTextBox
        ' 
        SearchAddressTextBox.Font = New Font("Arial", 11.25F, FontStyle.Regular, GraphicsUnit.Point)
        SearchAddressTextBox.Location = New Point(12, 173)
        SearchAddressTextBox.Name = "SearchAddressTextBox"
        SearchAddressTextBox.Size = New Size(322, 25)
        SearchAddressTextBox.TabIndex = 0
        ' 
        ' WarehouseRelationsTab
        ' 
        WarehouseRelationsTab.Controls.Add(DeleteReorderRowButton)
        WarehouseRelationsTab.Controls.Add(Label6)
        WarehouseRelationsTab.Controls.Add(Label5)
        WarehouseRelationsTab.Controls.Add(DataGridReorders)
        WarehouseRelationsTab.Controls.Add(btnSubmitReorderInputs)
        WarehouseRelationsTab.Location = New Point(4, 24)
        WarehouseRelationsTab.Name = "WarehouseRelationsTab"
        WarehouseRelationsTab.Padding = New Padding(3)
        WarehouseRelationsTab.Size = New Size(768, 376)
        WarehouseRelationsTab.TabIndex = 2
        WarehouseRelationsTab.Text = "Reorder Relationships"
        WarehouseRelationsTab.UseVisualStyleBackColor = True
        ' 
        ' DeleteReorderRowButton
        ' 
        DeleteReorderRowButton.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        DeleteReorderRowButton.Location = New Point(555, 274)
        DeleteReorderRowButton.Name = "DeleteReorderRowButton"
        DeleteReorderRowButton.Size = New Size(124, 46)
        DeleteReorderRowButton.TabIndex = 13
        DeleteReorderRowButton.Text = "Delete Selected Row" & vbCrLf
        DeleteReorderRowButton.UseVisualStyleBackColor = True
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(527, 51)
        Label6.Name = "Label6"
        Label6.Size = New Size(162, 45)
        Label6.TabIndex = 12
        Label6.Text = "For Base Warehouses, set the " & vbCrLf & "        Reorder From ID to -1. " & vbCrLf & vbCrLf
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Font = New Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point)
        Label5.Location = New Point(6, 14)
        Label5.Name = "Label5"
        Label5.Size = New Size(271, 22)
        Label5.TabIndex = 11
        Label5.Text = "Enter Reorder Relationships"
        ' 
        ' DataGridReorders
        ' 
        DataGridViewCellStyle3.BackColor = Color.LightSkyBlue
        DataGridViewCellStyle3.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle3.SelectionForeColor = Color.Black
        DataGridReorders.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle3
        DataGridReorders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridReorders.Columns.AddRange(New DataGridViewColumn() {Reorder_warehouse_id, Reordered_from, lead_time_mean, Lead_time_std_dev, Reorder_cost})
        DataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle4.BackColor = Color.AliceBlue
        DataGridViewCellStyle4.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle4.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle4.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle4.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle4.WrapMode = DataGridViewTriState.False
        DataGridReorders.DefaultCellStyle = DataGridViewCellStyle4
        DataGridReorders.Location = New Point(18, 51)
        DataGridReorders.Name = "DataGridReorders"
        DataGridReorders.RowHeadersVisible = False
        DataGridReorders.RowTemplate.Height = 25
        DataGridReorders.Size = New Size(503, 269)
        DataGridReorders.TabIndex = 1
        ' 
        ' Reorder_warehouse_id
        ' 
        Reorder_warehouse_id.HeaderText = "Warehouse ID"
        Reorder_warehouse_id.Name = "Reorder_warehouse_id"
        ' 
        ' Reordered_from
        ' 
        Reordered_from.HeaderText = "Reorder From ID"
        Reordered_from.Name = "Reordered_from"
        ' 
        ' lead_time_mean
        ' 
        lead_time_mean.HeaderText = "Lead Time Mean"
        lead_time_mean.Name = "lead_time_mean"
        ' 
        ' Lead_time_std_dev
        ' 
        Lead_time_std_dev.HeaderText = "Lead Time Std Dev"
        Lead_time_std_dev.Name = "Lead_time_std_dev"
        ' 
        ' Reorder_cost
        ' 
        Reorder_cost.HeaderText = "Reorder Cost Per Pallet"
        Reorder_cost.Name = "Reorder_cost"
        ' 
        ' btnSubmitReorderInputs
        ' 
        btnSubmitReorderInputs.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        btnSubmitReorderInputs.Location = New Point(6, 347)
        btnSubmitReorderInputs.Name = "btnSubmitReorderInputs"
        btnSubmitReorderInputs.Size = New Size(193, 23)
        btnSubmitReorderInputs.TabIndex = 0
        btnSubmitReorderInputs.Text = "Submit Reorder Relationships"
        btnSubmitReorderInputs.UseVisualStyleBackColor = True
        ' 
        ' ItemInputsTab
        ' 
        ItemInputsTab.Controls.Add(Label3)
        ItemInputsTab.Controls.Add(lblProfitPerSale)
        ItemInputsTab.Controls.Add(lblItemsPerPallet)
        ItemInputsTab.Controls.Add(txtboxProfitPerSale)
        ItemInputsTab.Controls.Add(txtboxItemsPerPallet)
        ItemInputsTab.Controls.Add(btnSubmitItemInputs)
        ItemInputsTab.Location = New Point(4, 24)
        ItemInputsTab.Name = "ItemInputsTab"
        ItemInputsTab.Padding = New Padding(3)
        ItemInputsTab.Size = New Size(768, 376)
        ItemInputsTab.TabIndex = 0
        ItemInputsTab.Text = "Item Inputs"
        ItemInputsTab.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Font = New Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point)
        Label3.Location = New Point(6, 14)
        Label3.Name = "Label3"
        Label3.Size = New Size(167, 22)
        Label3.TabIndex = 9
        Label3.Text = "Enter Item Inputs"
        ' 
        ' lblProfitPerSale
        ' 
        lblProfitPerSale.AutoSize = True
        lblProfitPerSale.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        lblProfitPerSale.Location = New Point(42, 137)
        lblProfitPerSale.Name = "lblProfitPerSale"
        lblProfitPerSale.Size = New Size(85, 15)
        lblProfitPerSale.TabIndex = 8
        lblProfitPerSale.Text = "Profit Per Sale"
        ' 
        ' lblItemsPerPallet
        ' 
        lblItemsPerPallet.AutoSize = True
        lblItemsPerPallet.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        lblItemsPerPallet.Location = New Point(33, 97)
        lblItemsPerPallet.Name = "lblItemsPerPallet"
        lblItemsPerPallet.Size = New Size(94, 15)
        lblItemsPerPallet.TabIndex = 7
        lblItemsPerPallet.Text = "Items Per Pallet"
        ' 
        ' txtboxProfitPerSale
        ' 
        txtboxProfitPerSale.Location = New Point(133, 129)
        txtboxProfitPerSale.Name = "txtboxProfitPerSale"
        txtboxProfitPerSale.Size = New Size(100, 23)
        txtboxProfitPerSale.TabIndex = 6
        ' 
        ' txtboxItemsPerPallet
        ' 
        txtboxItemsPerPallet.Location = New Point(133, 89)
        txtboxItemsPerPallet.Name = "txtboxItemsPerPallet"
        txtboxItemsPerPallet.Size = New Size(100, 23)
        txtboxItemsPerPallet.TabIndex = 5
        ' 
        ' btnSubmitItemInputs
        ' 
        btnSubmitItemInputs.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        btnSubmitItemInputs.Location = New Point(6, 347)
        btnSubmitItemInputs.Name = "btnSubmitItemInputs"
        btnSubmitItemInputs.Size = New Size(139, 23)
        btnSubmitItemInputs.TabIndex = 0
        btnSubmitItemInputs.Text = "Submit Item Inputs"
        btnSubmitItemInputs.UseVisualStyleBackColor = True
        ' 
        ' WarehouseAndSKUInputsTab
        ' 
        WarehouseAndSKUInputsTab.Controls.Add(Label4)
        WarehouseAndSKUInputsTab.Controls.Add(DataGridWarehouse)
        WarehouseAndSKUInputsTab.Controls.Add(btnNextWarehouse)
        WarehouseAndSKUInputsTab.Location = New Point(4, 24)
        WarehouseAndSKUInputsTab.Name = "WarehouseAndSKUInputsTab"
        WarehouseAndSKUInputsTab.Padding = New Padding(3)
        WarehouseAndSKUInputsTab.Size = New Size(768, 376)
        WarehouseAndSKUInputsTab.TabIndex = 1
        WarehouseAndSKUInputsTab.Text = "Warehouse Inputs"
        WarehouseAndSKUInputsTab.UseVisualStyleBackColor = True
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Font = New Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point)
        Label4.Location = New Point(6, 14)
        Label4.Name = "Label4"
        Label4.Size = New Size(232, 22)
        Label4.TabIndex = 10
        Label4.Text = "Enter Warehouse Inputs"
        ' 
        ' DataGridWarehouse
        ' 
        DataGridWarehouse.AllowUserToAddRows = False
        DataGridViewCellStyle5.BackColor = Color.LightSkyBlue
        DataGridViewCellStyle5.SelectionBackColor = Color.LightSkyBlue
        DataGridViewCellStyle5.SelectionForeColor = Color.Black
        DataGridWarehouse.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle5
        DataGridWarehouse.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DataGridWarehouse.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCellsExceptHeaders
        DataGridWarehouse.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DataGridWarehouse.Columns.AddRange(New DataGridViewColumn() {Warehouse_ID, Initial_inventory, Demand_mean, Demand_std_dev, DemandType, Reorder_point, Reorder_amount, Site_type, Holding_cost_per_pallet})
        DataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle6.BackColor = Color.AliceBlue
        DataGridViewCellStyle6.Font = New Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point)
        DataGridViewCellStyle6.ForeColor = SystemColors.ControlText
        DataGridViewCellStyle6.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle6.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle6.WrapMode = DataGridViewTriState.False
        DataGridWarehouse.DefaultCellStyle = DataGridViewCellStyle6
        DataGridWarehouse.Location = New Point(6, 54)
        DataGridWarehouse.Name = "DataGridWarehouse"
        DataGridWarehouse.RowHeadersVisible = False
        DataGridWarehouse.RowTemplate.Height = 25
        DataGridWarehouse.Size = New Size(746, 287)
        DataGridWarehouse.TabIndex = 1
        ' 
        ' Warehouse_ID
        ' 
        Warehouse_ID.HeaderText = "Warehouse ID"
        Warehouse_ID.Name = "Warehouse_ID"
        Warehouse_ID.ReadOnly = True
        ' 
        ' Initial_inventory
        ' 
        Initial_inventory.HeaderText = "Initial inventory"
        Initial_inventory.Name = "Initial_inventory"
        ' 
        ' Demand_mean
        ' 
        Demand_mean.HeaderText = "Demand Mean"
        Demand_mean.Name = "Demand_mean"
        ' 
        ' Demand_std_dev
        ' 
        Demand_std_dev.HeaderText = "Demand Std Dev"
        Demand_std_dev.Name = "Demand_std_dev"
        ' 
        ' DemandType
        ' 
        DemandType.HeaderText = "Demand Type"
        DemandType.Name = "DemandType"
        ' 
        ' Reorder_point
        ' 
        Reorder_point.HeaderText = "Reorder Point"
        Reorder_point.Name = "Reorder_point"
        ' 
        ' Reorder_amount
        ' 
        Reorder_amount.HeaderText = "Reorder Amount (pallets)"
        Reorder_amount.Name = "Reorder_amount"
        ' 
        ' Site_type
        ' 
        Site_type.HeaderText = "Site Type"
        Site_type.Items.AddRange(New Object() {"Dependent Warehouse", "Base Warehouse"})
        Site_type.Name = "Site_type"
        Site_type.ReadOnly = True
        ' 
        ' Holding_cost_per_pallet
        ' 
        Holding_cost_per_pallet.HeaderText = "Holding Cost Per Pallet"
        Holding_cost_per_pallet.Name = "Holding_cost_per_pallet"
        Holding_cost_per_pallet.ReadOnly = True
        ' 
        ' btnNextWarehouse
        ' 
        btnNextWarehouse.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        btnNextWarehouse.Location = New Point(6, 347)
        btnNextWarehouse.Name = "btnNextWarehouse"
        btnNextWarehouse.Size = New Size(215, 23)
        btnNextWarehouse.TabIndex = 0
        btnNextWarehouse.Text = "Submit Warehouse and SKU Data"
        btnNextWarehouse.UseVisualStyleBackColor = True
        ' 
        ' ShowWarehouseTabPage
        ' 
        ShowWarehouseTabPage.Controls.Add(AlterInputsButton)
        ShowWarehouseTabPage.Controls.Add(FinaliseInputsButton)
        ShowWarehouseTabPage.Controls.Add(Label7)
        ShowWarehouseTabPage.Controls.Add(MapsWebView)
        ShowWarehouseTabPage.Location = New Point(4, 24)
        ShowWarehouseTabPage.Name = "ShowWarehouseTabPage"
        ShowWarehouseTabPage.Padding = New Padding(3)
        ShowWarehouseTabPage.Size = New Size(768, 376)
        ShowWarehouseTabPage.TabIndex = 5
        ShowWarehouseTabPage.Text = "Show Warehouse"
        ShowWarehouseTabPage.UseVisualStyleBackColor = True
        ' 
        ' AlterInputsButton
        ' 
        AlterInputsButton.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point)
        AlterInputsButton.Location = New Point(443, 8)
        AlterInputsButton.Name = "AlterInputsButton"
        AlterInputsButton.Size = New Size(145, 48)
        AlterInputsButton.TabIndex = 13
        AlterInputsButton.Text = "Alter Inputs"
        AlterInputsButton.UseVisualStyleBackColor = True
        ' 
        ' FinaliseInputsButton
        ' 
        FinaliseInputsButton.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point)
        FinaliseInputsButton.Location = New Point(604, 8)
        FinaliseInputsButton.Name = "FinaliseInputsButton"
        FinaliseInputsButton.Size = New Size(145, 48)
        FinaliseInputsButton.TabIndex = 12
        FinaliseInputsButton.Text = "Submit All Inputs"
        FinaliseInputsButton.UseVisualStyleBackColor = True
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Font = New Font("Arial", 14F, FontStyle.Bold, GraphicsUnit.Point)
        Label7.Location = New Point(6, 14)
        Label7.Name = "Label7"
        Label7.Size = New Size(318, 22)
        Label7.TabIndex = 11
        Label7.Text = "Geographic Overview Of Network"
        ' 
        ' MapsWebView
        ' 
        MapsWebView.AllowExternalDrop = True
        MapsWebView.CreationProperties = Nothing
        MapsWebView.DefaultBackgroundColor = Color.White
        MapsWebView.Location = New Point(6, 62)
        MapsWebView.Name = "MapsWebView"
        MapsWebView.Size = New Size(756, 308)
        MapsWebView.TabIndex = 0
        MapsWebView.ZoomFactor = 1R
        ' 
        ' computeSuggestions
        ' 
        ' 
        ' inputsForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        ClientSize = New Size(800, 465)
        Controls.Add(InputsTabControl)
        Controls.Add(lblInputsTitle)
        Name = "inputsForm"
        Text = "Form1"
        InputsTabControl.ResumeLayout(False)
        WarehouseLocationsTab.ResumeLayout(False)
        WarehouseLocationsTab.PerformLayout()
        CType(WarehouseDataGrid, ComponentModel.ISupportInitialize).EndInit()
        WarehouseRelationsTab.ResumeLayout(False)
        WarehouseRelationsTab.PerformLayout()
        CType(DataGridReorders, ComponentModel.ISupportInitialize).EndInit()
        ItemInputsTab.ResumeLayout(False)
        ItemInputsTab.PerformLayout()
        WarehouseAndSKUInputsTab.ResumeLayout(False)
        WarehouseAndSKUInputsTab.PerformLayout()
        CType(DataGridWarehouse, ComponentModel.ISupportInitialize).EndInit()
        ShowWarehouseTabPage.ResumeLayout(False)
        ShowWarehouseTabPage.PerformLayout()
        CType(MapsWebView, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents lblInputsTitle As Label
    Friend WithEvents InputsTabControl As TabControl
    Friend WithEvents ItemInputsTab As TabPage
    Friend WithEvents WarehouseAndSKUInputsTab As TabPage
    Friend WithEvents btnSubmitItemInputs As Button
    Friend WithEvents btnNextWarehouse As Button
    Friend WithEvents lblProfitPerSale As Label
    Friend WithEvents lblItemsPerPallet As Label
    Friend WithEvents txtboxProfitPerSale As TextBox
    Friend WithEvents txtboxItemsPerPallet As TextBox
    Friend WithEvents DataGridWarehouse As DataGridView
    Friend WithEvents WarehouseRelationsTab As TabPage
    Friend WithEvents btnSubmitReorderInputs As Button
    Friend WithEvents computeSuggestions As System.ComponentModel.BackgroundWorker
    Friend WithEvents WarehouseLocationsTab As TabPage
    Friend WithEvents SuggestionsListBox As ListBox
    Friend WithEvents LblSearchAddress As Label
    Friend WithEvents SearchAddressTextBox As TextBox
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage4 As TabPage
    Friend WithEvents lblWarehosueLocationTitle As Label
    Friend WithEvents lblEnterWarehouseID As Label
    Friend WithEvents WarehouseIDTextBox As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents SubmitWarehouseBtn As Button
    Friend WithEvents WarehouseDataGrid As DataGridView
    Friend WithEvents SubmitWarehouseLocationsBtn As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents lblWarehouseList As Label
    Friend WithEvents HoldingCostTextBox As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents BaseWarehouseRadioButton As RadioButton
    Friend WithEvents DependentWarehouseRadioButton As RadioButton
    Friend WithEvents Warehouse_ID_Location As DataGridViewTextBoxColumn
    Friend WithEvents HoldningCost As DataGridViewTextBoxColumn
    Friend WithEvents DependentWarehouse As DataGridViewTextBoxColumn
    Friend WithEvents Address As DataGridViewTextBoxColumn
    Friend WithEvents Label6 As Label
    Friend WithEvents DataGridReorders As DataGridView
    Friend WithEvents Reorder_warehouse_id As DataGridViewTextBoxColumn
    Friend WithEvents Reordered_from As DataGridViewTextBoxColumn
    Friend WithEvents lead_time_mean As DataGridViewTextBoxColumn
    Friend WithEvents Lead_time_std_dev As DataGridViewTextBoxColumn
    Friend WithEvents Reorder_cost As DataGridViewTextBoxColumn
    Friend WithEvents ShowWarehouseTabPage As TabPage
    Friend WithEvents MapsWebView As Microsoft.Web.WebView2.WinForms.WebView2
    Friend WithEvents DeleteReorderRowButton As Button
    Friend WithEvents Label7 As Label
    Friend WithEvents AlterInputsButton As Button
    Friend WithEvents FinaliseInputsButton As Button
    Friend WithEvents Warehouse_ID As DataGridViewTextBoxColumn
    Friend WithEvents Initial_inventory As DataGridViewTextBoxColumn
    Friend WithEvents Demand_mean As DataGridViewTextBoxColumn
    Friend WithEvents Demand_std_dev As DataGridViewTextBoxColumn
    Friend WithEvents DemandType As DataGridViewTextBoxColumn
    Friend WithEvents Reorder_point As DataGridViewTextBoxColumn
    Friend WithEvents Reorder_amount As DataGridViewTextBoxColumn
    Friend WithEvents Site_type As DataGridViewComboBoxColumn
    Friend WithEvents Holding_cost_per_pallet As DataGridViewTextBoxColumn
End Class
