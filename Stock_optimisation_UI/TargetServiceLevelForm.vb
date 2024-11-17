Public Class TargetServiceLevelForm


    Dim targetServiceLevels As Dictionary(Of String, Double)
    Dim warehouseIDList As List(Of String)
    Dim desiredServicelevels As Dictionary(Of String, Double)


    Public Sub New(warehouseIDList As List(Of String))

        ' This call is required by the designer.
        InitializeComponent()
        Me.desiredServicelevels = New Dictionary(Of String, Double)
        Me.warehouseIDList = warehouseIDList
        SetUpDataGridView()
    End Sub

    Private Sub TargetServiceLevelForm_load(sender As Object, e As EventArgs) Handles MyBase.Load
        SetUpDataGridView()
    End Sub

    Private Sub SetUpDataGridView()
        DesiredServiceLevelDataGrid.AutoGenerateColumns = False
        DesiredServiceLevelDataGrid.Columns.Clear()
        DesiredServiceLevelDataGrid.RowCount = 2
        DesiredServiceLevelDataGrid.ColumnCount = warehouseIDList.Count
        DesiredServiceLevelDataGrid.Rows(0).HeaderCell.Value = "Warehouse ID"
        DesiredServiceLevelDataGrid.Rows(1).HeaderCell.Value = "Desired Service Level (%)"
        DesiredServiceLevelDataGrid.Rows(0).ReadOnly = True

        For i As Integer = 0 To warehouseIDList.Count - 1
            DesiredServiceLevelDataGrid.Rows(0).Cells(i).Value = "   " & warehouseIDList(i) & "  "
            DesiredServiceLevelDataGrid.Columns(i).Width = 65
        Next

        DesiredServiceLevelDataGrid.Rows(0).Height = 35
        DesiredServiceLevelDataGrid.Rows(1).Height = 35

    End Sub

    Public Function returnServicelevels() As Dictionary(Of String, Double)
        Return Me.desiredServicelevels
    End Function

    Private Sub SubmitServiceLevelsButton_Click(sender As Object, e As EventArgs) Handles SubmitServiceLevelsButton.Click
        For colIndex As Integer = 0 To DesiredServiceLevelDataGrid.ColumnCount - 1
            Try
                Dim warehouseID As String = Convert.ToString(DesiredServiceLevelDataGrid.Rows(0).Cells(colIndex).Value)
                Dim serviceLevel As Double = Convert.ToDouble(DesiredServiceLevelDataGrid.Rows(1).Cells(colIndex).Value) / 100
                desiredServicelevels.Add(warehouseID, serviceLevel)
            Catch exp As Exception
<<<<<<< HEAD
                MessageBox.Show("Please provide an warehouse ID as String and double value for service level")
=======
                MessageBox.Show("Please provide an warehouse ID as a String and double value for service level")
>>>>>>> e9c7d40 (Continued to convert the warehouse ID to a string. Further debugging and testing required)
                Exit Sub
            End Try
        Next
        Me.Close()
    End Sub

End Class