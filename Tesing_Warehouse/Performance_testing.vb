Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Warehouse_Optimisation

<TestClass>
Public Class Performance_testing

    <TestMethod>
    Public Sub normal_monte_carlo_250_by_500()

        Dim Warehouse_inputs_1 = New Warehouse_inputs(1, 24000, 0, 0, 1600, 18, 6, 0, SiteType.Base_Warehouse, 20, 700, 2000, 150)
        Dim Warehouse_inputs_2 = New Warehouse_inputs(2, 16000, 1000, 700, 5000, 8, -1, -1, SiteType.Dependent_Warehouse, 20, 560, 2000, -1)
        Dim Warehouse_inputs_3 = New Warehouse_inputs(3, 8000, 1000, 300, 3500, 5, -1, -1, SiteType.Dependent_Warehouse, 20, 840, 2000, -1)
        Dim warehouse_list = New List(Of Warehouse_inputs) From {Warehouse_inputs_1, Warehouse_inputs_2, Warehouse_inputs_3}

        Dim reorder_inputs_1 = (2, 1, New Reorder_inputs(6, 1, 200))
        Dim reorder_inputs_2 = (3, 1, New Reorder_inputs(5, 1, 100))
        Dim reorder_inputs_3 = (3, 2, New Reorder_inputs(3, 2, 200))
        Dim reorder_inputs_list = New List(Of (Integer, Integer, Reorder_inputs)) From {reorder_inputs_1, reorder_inputs_2, reorder_inputs_3}

        Dim test_Warehouse_Group = New Warehouse_Group(warehouse_list, reorder_inputs_list)
        Dim results = Nothing
        For i As Integer = 0 To 30
            results = test_Warehouse_Group.run_Monte_Carlo(250, 500)
        Next
    End Sub

    <TestMethod>
    Public Sub light_monte_carlo_250_by_500()

        Dim Warehouse_inputs_1 = New Warehouse_inputs(1, 24000, 0, 0, 1600, 18, 6, 0, SiteType.Base_Warehouse, 20, 700, 2000, 150)
        Dim Warehouse_inputs_2 = New Warehouse_inputs(2, 16000, 1000, 700, 5000, 8, -1, -1, SiteType.Dependent_Warehouse, 20, 560, 2000, -1)
        Dim Warehouse_inputs_3 = New Warehouse_inputs(3, 8000, 1000, 300, 3500, 5, -1, -1, SiteType.Dependent_Warehouse, 20, 840, 2000, -1)
        Dim warehouse_list = New List(Of Warehouse_inputs) From {Warehouse_inputs_1, Warehouse_inputs_2, Warehouse_inputs_3}

        Dim reorder_inputs_1 = (2, 1, New Reorder_inputs(6, 1, 200))
        Dim reorder_inputs_2 = (3, 1, New Reorder_inputs(5, 1, 100))
        Dim reorder_inputs_3 = (3, 2, New Reorder_inputs(3, 2, 200))
        Dim reorder_inputs_list = New List(Of (Integer, Integer, Reorder_inputs)) From {reorder_inputs_1, reorder_inputs_2, reorder_inputs_3}

        Dim test_Warehouse_Group = New Warehouse_Group(warehouse_list, reorder_inputs_list)
        Dim results = Nothing
        For i As Integer = 0 To 30
            results = test_Warehouse_Group.run_light_Monte_Carlo(250, 500)
        Next
    End Sub

    <TestMethod>
    Public Sub parrallel_monte_carlo_250_by_500()

        Dim Warehouse_inputs_1 = New Warehouse_inputs(1, 24000, 0, 0, 1600, 18, 6, 0, SiteType.Base_Warehouse, 20, 700, 2000, 150)
        Dim Warehouse_inputs_2 = New Warehouse_inputs(2, 16000, 1000, 700, 5000, 8, -1, -1, SiteType.Dependent_Warehouse, 20, 560, 2000, -1)
        Dim Warehouse_inputs_3 = New Warehouse_inputs(3, 8000, 1000, 300, 3500, 5, -1, -1, SiteType.Dependent_Warehouse, 20, 840, 2000, -1)
        Dim warehouse_list = New List(Of Warehouse_inputs) From {Warehouse_inputs_1, Warehouse_inputs_2, Warehouse_inputs_3}

        Dim reorder_inputs_1 = (2, 1, New Reorder_inputs(6, 1, 200))
        Dim reorder_inputs_2 = (3, 1, New Reorder_inputs(5, 1, 100))
        Dim reorder_inputs_3 = (3, 2, New Reorder_inputs(3, 2, 200))
        Dim reorder_inputs_list = New List(Of (Integer, Integer, Reorder_inputs)) From {reorder_inputs_1, reorder_inputs_2, reorder_inputs_3}

        Dim test_Warehouse_Group = New Warehouse_Group(warehouse_list, reorder_inputs_list)
        Dim results = Nothing
        For i As Integer = 0 To 30
            results = test_Warehouse_Group.run_light_Monte_Carlo(250, 500)
        Next
    End Sub

    <TestMethod>
    Public Sub normal_monte_carlo_3000_by_1000()

        Dim Warehouse_inputs_1 = New Warehouse_inputs(1, 24000, 0, 0, 1600, 18, 6, 0, SiteType.Base_Warehouse, 20, 700, 2000, 150)
        Dim Warehouse_inputs_2 = New Warehouse_inputs(2, 16000, 1000, 700, 5000, 8, -1, -1, SiteType.Dependent_Warehouse, 20, 560, 2000, -1)
        Dim Warehouse_inputs_3 = New Warehouse_inputs(3, 8000, 1000, 300, 3500, 5, -1, -1, SiteType.Dependent_Warehouse, 20, 840, 2000, -1)
        Dim warehouse_list = New List(Of Warehouse_inputs) From {Warehouse_inputs_1, Warehouse_inputs_2, Warehouse_inputs_3}

        Dim reorder_inputs_1 = (2, 1, New Reorder_inputs(6, 1, 200))
        Dim reorder_inputs_2 = (3, 1, New Reorder_inputs(5, 1, 100))
        Dim reorder_inputs_3 = (3, 2, New Reorder_inputs(3, 2, 200))
        Dim reorder_inputs_list = New List(Of (Integer, Integer, Reorder_inputs)) From {reorder_inputs_1, reorder_inputs_2, reorder_inputs_3}

        Dim test_Warehouse_Group = New Warehouse_Group(warehouse_list, reorder_inputs_list)
        Dim results = Nothing
        For i As Integer = 0 To 20
            results = test_Warehouse_Group.run_Monte_Carlo(3000, 1000)
        Next
    End Sub

    <TestMethod>
    Public Sub light_monte_carlo_3000_by_1000()

        Dim Warehouse_inputs_1 = New Warehouse_inputs(1, 24000, 0, 0, 1600, 18, 6, 0, SiteType.Base_Warehouse, 20, 700, 2000, 150)
        Dim Warehouse_inputs_2 = New Warehouse_inputs(2, 16000, 1000, 700, 5000, 8, -1, -1, SiteType.Dependent_Warehouse, 20, 560, 2000, -1)
        Dim Warehouse_inputs_3 = New Warehouse_inputs(3, 8000, 1000, 300, 3500, 5, -1, -1, SiteType.Dependent_Warehouse, 20, 840, 2000, -1)
        Dim warehouse_list = New List(Of Warehouse_inputs) From {Warehouse_inputs_1, Warehouse_inputs_2, Warehouse_inputs_3}

        Dim reorder_inputs_1 = (2, 1, New Reorder_inputs(6, 1, 200))
        Dim reorder_inputs_2 = (3, 1, New Reorder_inputs(5, 1, 100))
        Dim reorder_inputs_3 = (3, 2, New Reorder_inputs(3, 2, 200))
        Dim reorder_inputs_list = New List(Of (Integer, Integer, Reorder_inputs)) From {reorder_inputs_1, reorder_inputs_2, reorder_inputs_3}

        Dim test_Warehouse_Group = New Warehouse_Group(warehouse_list, reorder_inputs_list)
        Dim results = Nothing
        For i As Integer = 0 To 20
            results = test_Warehouse_Group.run_light_Monte_Carlo(3000, 1000)
        Next
    End Sub

    <TestMethod>
    Public Sub parrallel_monte_carlo_3000_by_1000()

        Dim Warehouse_inputs_1 = New Warehouse_inputs(1, 24000, 0, 0, 1600, 18, 6, 0, SiteType.Base_Warehouse, 20, 700, 2000, 150)
        Dim Warehouse_inputs_2 = New Warehouse_inputs(2, 16000, 1000, 700, 5000, 8, -1, -1, SiteType.Dependent_Warehouse, 20, 560, 2000, -1)
        Dim Warehouse_inputs_3 = New Warehouse_inputs(3, 8000, 1000, 300, 3500, 5, -1, -1, SiteType.Dependent_Warehouse, 20, 840, 2000, -1)
        Dim warehouse_list = New List(Of Warehouse_inputs) From {Warehouse_inputs_1, Warehouse_inputs_2, Warehouse_inputs_3}

        Dim reorder_inputs_1 = (2, 1, New Reorder_inputs(6, 1, 200))
        Dim reorder_inputs_2 = (3, 1, New Reorder_inputs(5, 1, 100))
        Dim reorder_inputs_3 = (3, 2, New Reorder_inputs(3, 2, 200))
        Dim reorder_inputs_list = New List(Of (Integer, Integer, Reorder_inputs)) From {reorder_inputs_1, reorder_inputs_2, reorder_inputs_3}

        Dim test_Warehouse_Group = New Warehouse_Group(warehouse_list, reorder_inputs_list)
        Dim results = Nothing
        For i As Integer = 0 To 20
            results = test_Warehouse_Group.run_light_Monte_Carlo(3000, 1000)
        Next
    End Sub

End Class
