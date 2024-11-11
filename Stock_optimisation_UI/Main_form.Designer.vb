<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Main_form
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Title_lbl = New Label()
        btnOpenInputs = New Button()
        btnCalcServiceLevels = New Button()
        InputsBox = New GroupBox()
        CreateNewSkuCheckBox = New CheckBox()
        LoadSKUCheckBox = New CheckBox()
        LoadNetworkCheckBox = New CheckBox()
        CreateNetworkCheckBox = New CheckBox()
        lblSkuInput = New Label()
        SkuInputTextBox = New TextBox()
        lblNetworkIDInput = New Label()
        NetworkIDInputTextBox = New TextBox()
        lblInputsTitle = New Label()
        MonteBox = New GroupBox()
        Label1 = New Label()
        NumSimulationDaysTextBox = New TextBox()
        lblMonteTitle = New Label()
        btnStockWizard = New Button()
        lblStockWizardTitle = New Label()
        StockWizardBox = New GroupBox()
        Label2 = New Label()
        CostFunctionComboBox = New ComboBox()
        OneVarRadioButton = New RadioButton()
        TwoVarRadioButton = New RadioButton()
        CSVDumpCheckbox = New CheckBox()
        InputsBox.SuspendLayout()
        MonteBox.SuspendLayout()
        StockWizardBox.SuspendLayout()
        SuspendLayout()
        ' 
        ' Title_lbl
        ' 
        Title_lbl.AutoSize = True
        Title_lbl.Font = New Font("Arial", 21.75F, FontStyle.Bold, GraphicsUnit.Point)
        Title_lbl.Location = New Point(241, 32)
        Title_lbl.Name = "Title_lbl"
        Title_lbl.Size = New Size(363, 34)
        Title_lbl.TabIndex = 0
        Title_lbl.Text = "Stock Level Optimisation"
        ' 
        ' btnOpenInputs
        ' 
        btnOpenInputs.Location = New Point(8, 296)
        btnOpenInputs.Name = "btnOpenInputs"
        btnOpenInputs.Size = New Size(129, 42)
        btnOpenInputs.TabIndex = 1
        btnOpenInputs.Text = "Click Here to Enter Your Inputs"
        btnOpenInputs.UseVisualStyleBackColor = True
        ' 
        ' btnCalcServiceLevels
        ' 
        btnCalcServiceLevels.Location = New Point(8, 296)
        btnCalcServiceLevels.Name = "btnCalcServiceLevels"
        btnCalcServiceLevels.Size = New Size(134, 42)
        btnCalcServiceLevels.TabIndex = 2
        btnCalcServiceLevels.Text = "Calculate Current Service Levels"
        btnCalcServiceLevels.UseVisualStyleBackColor = True
        ' 
        ' InputsBox
        ' 
        InputsBox.BackColor = Color.Transparent
        InputsBox.Controls.Add(CreateNewSkuCheckBox)
        InputsBox.Controls.Add(LoadSKUCheckBox)
        InputsBox.Controls.Add(LoadNetworkCheckBox)
        InputsBox.Controls.Add(CreateNetworkCheckBox)
        InputsBox.Controls.Add(lblSkuInput)
        InputsBox.Controls.Add(SkuInputTextBox)
        InputsBox.Controls.Add(lblNetworkIDInput)
        InputsBox.Controls.Add(NetworkIDInputTextBox)
        InputsBox.Controls.Add(lblInputsTitle)
        InputsBox.Controls.Add(btnOpenInputs)
        InputsBox.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        InputsBox.Location = New Point(12, 94)
        InputsBox.Name = "InputsBox"
        InputsBox.Size = New Size(242, 344)
        InputsBox.TabIndex = 7
        InputsBox.TabStop = False
        ' 
        ' CreateNewSkuCheckBox
        ' 
        CreateNewSkuCheckBox.AutoSize = True
        CreateNewSkuCheckBox.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        CreateNewSkuCheckBox.Location = New Point(8, 131)
        CreateNewSkuCheckBox.Name = "CreateNewSkuCheckBox"
        CreateNewSkuCheckBox.Size = New Size(184, 19)
        CreateNewSkuCheckBox.TabIndex = 16
        CreateNewSkuCheckBox.Text = "Create New SKU Information"
        CreateNewSkuCheckBox.UseVisualStyleBackColor = True
        ' 
        ' LoadSKUCheckBox
        ' 
        LoadSKUCheckBox.AutoSize = True
        LoadSKUCheckBox.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        LoadSKUCheckBox.Location = New Point(8, 159)
        LoadSKUCheckBox.Name = "LoadSKUCheckBox"
        LoadSKUCheckBox.Size = New Size(206, 19)
        LoadSKUCheckBox.TabIndex = 15
        LoadSKUCheckBox.Text = "Load in Existing SKU Information"
        LoadSKUCheckBox.UseVisualStyleBackColor = True
        ' 
        ' LoadNetworkCheckBox
        ' 
        LoadNetworkCheckBox.AutoSize = True
        LoadNetworkCheckBox.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        LoadNetworkCheckBox.Location = New Point(8, 78)
        LoadNetworkCheckBox.Name = "LoadNetworkCheckBox"
        LoadNetworkCheckBox.Size = New Size(161, 19)
        LoadNetworkCheckBox.TabIndex = 14
        LoadNetworkCheckBox.Text = "Load in Existing Network"
        LoadNetworkCheckBox.UseVisualStyleBackColor = True
        ' 
        ' CreateNetworkCheckBox
        ' 
        CreateNetworkCheckBox.AutoSize = True
        CreateNetworkCheckBox.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        CreateNetworkCheckBox.Location = New Point(8, 53)
        CreateNetworkCheckBox.Name = "CreateNetworkCheckBox"
        CreateNetworkCheckBox.Size = New Size(206, 19)
        CreateNetworkCheckBox.TabIndex = 13
        CreateNetworkCheckBox.Text = "Create New Warehouse Network"
        CreateNetworkCheckBox.UseVisualStyleBackColor = True
        ' 
        ' lblSkuInput
        ' 
        lblSkuInput.AutoSize = True
        lblSkuInput.Font = New Font("Arial", 9F, FontStyle.Underline, GraphicsUnit.Point)
        lblSkuInput.Location = New Point(41, 248)
        lblSkuInput.Name = "lblSkuInput"
        lblSkuInput.Size = New Size(32, 15)
        lblSkuInput.TabIndex = 10
        lblSkuInput.Text = "SKU"
        ' 
        ' SkuInputTextBox
        ' 
        SkuInputTextBox.Location = New Point(79, 245)
        SkuInputTextBox.Name = "SkuInputTextBox"
        SkuInputTextBox.Size = New Size(100, 21)
        SkuInputTextBox.TabIndex = 9
        ' 
        ' lblNetworkIDInput
        ' 
        lblNetworkIDInput.AutoSize = True
        lblNetworkIDInput.Font = New Font("Arial", 9F, FontStyle.Underline, GraphicsUnit.Point)
        lblNetworkIDInput.Location = New Point(6, 210)
        lblNetworkIDInput.Name = "lblNetworkIDInput"
        lblNetworkIDInput.Size = New Size(67, 15)
        lblNetworkIDInput.TabIndex = 8
        lblNetworkIDInput.Text = "Network ID"
        ' 
        ' NetworkIDInputTextBox
        ' 
        NetworkIDInputTextBox.Location = New Point(79, 207)
        NetworkIDInputTextBox.Name = "NetworkIDInputTextBox"
        NetworkIDInputTextBox.Size = New Size(100, 21)
        NetworkIDInputTextBox.TabIndex = 7
        ' 
        ' lblInputsTitle
        ' 
        lblInputsTitle.AutoSize = True
        lblInputsTitle.Font = New Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point)
        lblInputsTitle.Location = New Point(6, 19)
        lblInputsTitle.Name = "lblInputsTitle"
        lblInputsTitle.Size = New Size(73, 22)
        lblInputsTitle.TabIndex = 2
        lblInputsTitle.Text = "Inputs "
        ' 
        ' MonteBox
        ' 
        MonteBox.Controls.Add(CSVDumpCheckbox)
        MonteBox.Controls.Add(Label1)
        MonteBox.Controls.Add(NumSimulationDaysTextBox)
        MonteBox.Controls.Add(lblMonteTitle)
        MonteBox.Controls.Add(btnCalcServiceLevels)
        MonteBox.Location = New Point(279, 94)
        MonteBox.Name = "MonteBox"
        MonteBox.Size = New Size(242, 344)
        MonteBox.TabIndex = 8
        MonteBox.TabStop = False
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial", 9F, FontStyle.Underline, GraphicsUnit.Point)
        Label1.Location = New Point(6, 92)
        Label1.Name = "Label1"
        Label1.Size = New Size(123, 15)
        Label1.TabIndex = 6
        Label1.Text = "Num Day to Simulate"
        ' 
        ' NumSimulationDaysTextBox
        ' 
        NumSimulationDaysTextBox.Location = New Point(136, 89)
        NumSimulationDaysTextBox.Name = "NumSimulationDaysTextBox"
        NumSimulationDaysTextBox.Size = New Size(100, 23)
        NumSimulationDaysTextBox.TabIndex = 4
        ' 
        ' lblMonteTitle
        ' 
        lblMonteTitle.AutoSize = True
        lblMonteTitle.Font = New Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point)
        lblMonteTitle.Location = New Point(6, 19)
        lblMonteTitle.Name = "lblMonteTitle"
        lblMonteTitle.Size = New Size(166, 44)
        lblMonteTitle.TabIndex = 3
        lblMonteTitle.Text = "Simulate Current" & vbCrLf & "Network"
        ' 
        ' btnStockWizard
        ' 
        btnStockWizard.Location = New Point(8, 296)
        btnStockWizard.Name = "btnStockWizard"
        btnStockWizard.Size = New Size(128, 42)
        btnStockWizard.TabIndex = 5
        btnStockWizard.Text = "Recommend Reorder Points"
        btnStockWizard.UseVisualStyleBackColor = True
        ' 
        ' lblStockWizardTitle
        ' 
        lblStockWizardTitle.AutoSize = True
        lblStockWizardTitle.Font = New Font("Arial", 14.25F, FontStyle.Bold, GraphicsUnit.Point)
        lblStockWizardTitle.Location = New Point(6, 19)
        lblStockWizardTitle.Name = "lblStockWizardTitle"
        lblStockWizardTitle.Size = New Size(127, 44)
        lblStockWizardTitle.TabIndex = 4
        lblStockWizardTitle.Text = "Run Reorder" & vbCrLf & "Optimisation"
        ' 
        ' StockWizardBox
        ' 
        StockWizardBox.Controls.Add(Label2)
        StockWizardBox.Controls.Add(CostFunctionComboBox)
        StockWizardBox.Controls.Add(OneVarRadioButton)
        StockWizardBox.Controls.Add(TwoVarRadioButton)
        StockWizardBox.Controls.Add(lblStockWizardTitle)
        StockWizardBox.Controls.Add(btnStockWizard)
        StockWizardBox.Location = New Point(546, 94)
        StockWizardBox.Name = "StockWizardBox"
        StockWizardBox.Size = New Size(242, 344)
        StockWizardBox.TabIndex = 9
        StockWizardBox.TabStop = False
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Font = New Font("Arial", 9F, FontStyle.Underline, GraphicsUnit.Point)
        Label2.Location = New Point(8, 159)
        Label2.Name = "Label2"
        Label2.Size = New Size(85, 15)
        Label2.TabIndex = 9
        Label2.Text = "Optimsing For"
        ' 
        ' CostFunctionComboBox
        ' 
        CostFunctionComboBox.FormattingEnabled = True
        CostFunctionComboBox.Items.AddRange(New Object() {"Reduce Costs with a Target SL", "Balance Costs With Opportunity Costs", "Balance Opportunity Costs with a Min SL"})
        CostFunctionComboBox.Location = New Point(8, 177)
        CostFunctionComboBox.Name = "CostFunctionComboBox"
        CostFunctionComboBox.Size = New Size(228, 23)
        CostFunctionComboBox.TabIndex = 8
        ' 
        ' OneVarRadioButton
        ' 
        OneVarRadioButton.AutoSize = True
        OneVarRadioButton.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        OneVarRadioButton.Location = New Point(8, 103)
        OneVarRadioButton.Name = "OneVarRadioButton"
        OneVarRadioButton.Size = New Size(188, 19)
        OneVarRadioButton.TabIndex = 7
        OneVarRadioButton.TabStop = True
        OneVarRadioButton.Text = "Optimise Only Reorder Points"
        OneVarRadioButton.UseVisualStyleBackColor = True
        ' 
        ' TwoVarRadioButton
        ' 
        TwoVarRadioButton.AutoSize = True
        TwoVarRadioButton.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        TwoVarRadioButton.Location = New Point(8, 78)
        TwoVarRadioButton.Name = "TwoVarRadioButton"
        TwoVarRadioButton.Size = New Size(229, 19)
        TwoVarRadioButton.TabIndex = 6
        TwoVarRadioButton.TabStop = True
        TwoVarRadioButton.Text = "Optimise Reorder Points and Amount"
        TwoVarRadioButton.UseVisualStyleBackColor = True
        ' 
        ' CSVDumpCheckbox
        ' 
        CSVDumpCheckbox.AutoSize = True
        CSVDumpCheckbox.Font = New Font("Arial", 9F, FontStyle.Regular, GraphicsUnit.Point)
        CSVDumpCheckbox.Location = New Point(8, 131)
        CSVDumpCheckbox.Name = "CSVDumpCheckbox"
        CSVDumpCheckbox.Size = New Size(217, 19)
        CSVDumpCheckbox.TabIndex = 7
        CSVDumpCheckbox.Text = "Output Final Simulation to CSV File"
        CSVDumpCheckbox.UseVisualStyleBackColor = True
        ' 
        ' Main_form
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(800, 450)
        Controls.Add(StockWizardBox)
        Controls.Add(MonteBox)
        Controls.Add(InputsBox)
        Controls.Add(Title_lbl)
        Name = "Main_form"
        Text = "Multi-Echelon Stock Optimisation"
        InputsBox.ResumeLayout(False)
        InputsBox.PerformLayout()
        MonteBox.ResumeLayout(False)
        MonteBox.PerformLayout()
        StockWizardBox.ResumeLayout(False)
        StockWizardBox.PerformLayout()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents Title_lbl As Label
    Friend WithEvents btnOpenInputs As Button
    Friend WithEvents btnCalcServiceLevels As Button
    Friend WithEvents InputsBox As GroupBox
    Friend WithEvents MonteBox As GroupBox
    Friend WithEvents lblInputsTitle As Label
    Friend WithEvents lblMonteTitle As Label
    Friend WithEvents btnStockWizard As Button
    Friend WithEvents lblStockWizardTitle As Label
    Friend WithEvents StockWizardBox As GroupBox
    Friend WithEvents Label1 As Label
    Friend WithEvents NumSimulationDaysTextBox As TextBox
    Friend WithEvents lblSkuInput As Label
    Friend WithEvents SkuInputTextBox As TextBox
    Friend WithEvents lblNetworkIDInput As Label
    Friend WithEvents NetworkIDInputTextBox As TextBox
    Friend WithEvents CreateNewSkuCheckBox As CheckBox
    Friend WithEvents LoadSKUCheckBox As CheckBox
    Friend WithEvents LoadNetworkCheckBox As CheckBox
    Friend WithEvents CreateNetworkCheckBox As CheckBox
    Friend WithEvents OneVarRadioButton As RadioButton
    Friend WithEvents TwoVarRadioButton As RadioButton
    Friend WithEvents CostFunctionComboBox As ComboBox
    Friend WithEvents Label2 As Label
    Friend WithEvents CSVDumpCheckbox As CheckBox

End Class
