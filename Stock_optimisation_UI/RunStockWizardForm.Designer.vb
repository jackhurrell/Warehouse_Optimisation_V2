<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class RunStockWizardForm
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
        StockWizardProgressBar = New ProgressBar()
        Label1 = New Label()
        SuspendLayout()
        ' 
        ' StockWizardProgressBar
        ' 
        StockWizardProgressBar.Location = New Point(28, 91)
        StockWizardProgressBar.Name = "StockWizardProgressBar"
        StockWizardProgressBar.Size = New Size(414, 28)
        StockWizardProgressBar.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Font = New Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point)
        Label1.Location = New Point(28, 28)
        Label1.Name = "Label1"
        Label1.Size = New Size(414, 36)
        Label1.TabIndex = 1
        Label1.Text = "Stock Optimisation is in progress, results will automatically " & vbCrLf & "appear when the process is complete"
        Label1.TextAlign = ContentAlignment.MiddleCenter
        ' 
        ' RunStockWizardForm
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(494, 152)
        Controls.Add(Label1)
        Controls.Add(StockWizardProgressBar)
        Name = "RunStockWizardForm"
        Text = "Loading Form"
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Label1 As Label
    Public WithEvents StockWizardProgressBar As ProgressBar
End Class
