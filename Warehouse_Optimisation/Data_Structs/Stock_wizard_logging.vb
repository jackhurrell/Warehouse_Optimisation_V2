Public Structure Stock_wizard_logging

    Public reorder_points As Dictionary(Of Integer, List(Of Double))
    Public reorder_amounts As Dictionary(Of Integer, List(Of Double))
    Public costs As Dictionary(Of Integer, List(Of Double))
    Public service_levels As Dictionary(Of Integer, List(Of Double))
    Public gradient_point As Dictionary(Of Integer, List(Of Double))
    Public gradient_amount As Dictionary(Of Integer, List(Of Double))
    Public penalty As Dictionary(Of Integer, List(Of Double))

    Public Sub New(Warehouse_inputs As List(Of Warehouse_inputs))
        reorder_points = New Dictionary(Of Integer, List(Of Double))
        reorder_amounts = New Dictionary(Of Integer, List(Of Double))
        costs = New Dictionary(Of Integer, List(Of Double))
        service_levels = New Dictionary(Of Integer, List(Of Double))
        gradient_point = New Dictionary(Of Integer, List(Of Double))
        gradient_amount = New Dictionary(Of Integer, List(Of Double))
        penalty = New Dictionary(Of Integer, List(Of Double))

        For Each warehouse In Warehouse_inputs
            reorder_points.Add(warehouse.warehouse_id, New List(Of Double))
            reorder_amounts.Add(warehouse.warehouse_id, New List(Of Double))
            costs.Add(warehouse.warehouse_id, New List(Of Double))
            service_levels.Add(warehouse.warehouse_id, New List(Of Double))
            gradient_point.Add(warehouse.warehouse_id, New List(Of Double))
            gradient_amount.Add(warehouse.warehouse_id, New List(Of Double))
            penalty.Add(warehouse.warehouse_id, New List(Of Double))
        Next

    End Sub

    Public Sub add_reorder_points_and_amounts(warehouse_id As Integer, reorder_point As Double, reorder_amount As Double)
        reorder_points(warehouse_id).Add(reorder_point)
        reorder_amounts(warehouse_id).Add(reorder_amount)
    End Sub

    Public Sub add_costs(warehouse_id As Integer, cost As Double)
        costs(warehouse_id).Add(cost)
    End Sub

    Public Sub add_service_levels(warehouse_id As Integer, service_level As Double)
        service_levels(warehouse_id).Add(service_level)
    End Sub

    Public Sub add_gradient_points(warehouse_id As Integer, new_gradient_point As Double, new_gradient_amount As Double)
        gradient_point(warehouse_id).Add(new_gradient_point)
        gradient_amount(warehouse_id).Add(new_gradient_amount)
    End Sub

    Public Sub add_penalty(warehouse_id As Integer, new_penalty As Double)
        penalty(warehouse_id).Add(new_penalty)
    End Sub

    Public Sub pad_logging_vals()
        For Each warehouse_id In reorder_points.Keys
            For i As Integer = 1 To gradient_point(warehouse_id).Count - gradient_point(warehouse_id).Count
                gradient_amount(warehouse_id).Add(0)
            Next
        Next
    End Sub

    Public Sub print_outputs()
        For Each warehouse_id In reorder_points.Keys
            Console.Write("Reorder_amounts = [")
            print_list(reorder_amounts(warehouse_id))
            Console.WriteLine("];")
            Console.WriteLine("")
            Console.Write("Reorder_points = [")
            print_list(reorder_points(warehouse_id))
            Console.WriteLine("];")
            Console.WriteLine("")
            Console.Write("costs = [")
            print_list(costs(warehouse_id))
            Console.WriteLine("];")
            Console.WriteLine("")
            Console.Write("Service_level = [")
            print_list(service_levels(warehouse_id))
            Console.WriteLine("];")
            Console.WriteLine("")
            Console.Write("gradient_point = [")
            print_list(gradient_point(warehouse_id))
            Console.WriteLine("];")
            Console.WriteLine("")
            Console.Write("gradient_amount = [")
            print_list(gradient_amount(warehouse_id))
            Console.WriteLine("];")
            Console.WriteLine("")
            Console.Write("Penalty = [")
            print_list(penalty(warehouse_id))
            Console.WriteLine("];")

        Next
    End Sub

    Private Sub print_list(list_to_print As List(Of Double))
        For Each value In list_to_print
            Console.Write(value & ",")
        Next

    End Sub

End Structure
