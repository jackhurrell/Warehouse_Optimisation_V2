<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class TargetServiceLevelForm
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
        DesiredServiceLevelDataGrid = New DataGridView()
        Label1 = New Label()
        SubmitServiceLevelsButton = New Button()
        CType(DesiredServiceLevelDataGrid, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' DesiredServiceLevelDataGrid
        ' 
        DesiredServiceLevelDataGrid.AllowUserToAddRows = False
        DesiredServiceLevelDataGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
        DesiredServiceLevelDataGrid.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells
        DesiredServiceLevelDataGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        DesiredServiceLevelDataGrid.ColumnHeadersVisible = False
        DesiredServiceLevelDataGrid.Location = New Point(12, 62)
        DesiredServiceLevelDataGrid.Name = "DesiredServiceLevelDataGrid"
        DesiredServiceLevelDataGrid.RowHeadersWidth = 200
        DesiredServiceLevelDataGrid.RowTemplate.Height = 25
        DesiredServiceLevelDataGrid.Size = New Size(622, 70)
        DesiredServiceLevelDataGrid.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial", 15.75F, FontStyle.Bold, GraphicsUnit.Point)
        Label1.Location = New Point(126, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(391, 24)
        Label1.TabIndex = 1
        Label1.Text = "Enter Your Target Service Levels Here"
        ' 
        ' SubmitServiceLevelsButton
        ' 
        SubmitServiceLevelsButton.Location = New Point(12, 158)
        SubmitServiceLevelsButton.Name = "SubmitServiceLevelsButton"
        SubmitServiceLevelsButton.Size = New Size(172, 23)
        SubmitServiceLevelsButton.TabIndex = 2
        SubmitServiceLevelsButton.Text = "Submit Target Service Levels"
        SubmitServiceLevelsButton.UseVisualStyleBackColor = True
        ' 
        ' TargetServiceLevelForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(644, 193)
        Controls.Add(SubmitServiceLevelsButton)
        Controls.Add(Label1)
        Controls.Add(DesiredServiceLevelDataGrid)
        Name = "TargetServiceLevelForm"
        Text = "Target Service Level Form"
        CType(DesiredServiceLevelDataGrid, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents DesiredServiceLevelDataGrid As DataGridView
    Friend WithEvents Label1 As Label
    Friend WithEvents SubmitServiceLevelsButton As Button
End Class
