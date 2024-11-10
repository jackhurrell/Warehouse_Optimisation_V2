

Public Delegate Sub ProgressDelegate(progress As Integer)


Public Class Stock_Wizard

    ''This defnines what the system is optimising for. This only explicitally impacts the cost and penalty functions

    Enum OptimisedFor
        CostsWithLostSales
        CostWithPenalty
        CostsWithPenaltyAndLostSales
    End Enum

    Dim Warehouse_group_to_optimise As Warehouse_Group
    Dim optimisation_order As List(Of Integer)
    Dim desired_service_level As Dictionary(Of Integer, Double)
    'Dim iteration_params As List(Of Stock_wizard_iteration_inputs)
    Public logging_points As Stock_wizard_logging
    Dim logging As Boolean

    Dim current_iteration_round As Integer ''These rwo values are used for the progress bar
    Dim total_rounds As Integer
    Dim progressCallback As ProgressDelegate
    Dim CostFunction As OptimisedFor



    Public Sub New(list_of_warehouse_inputs As List(Of Warehouse_inputs), list_of_warehouse_relationships As List(Of (Integer, Integer, Reorder_inputs)),
                   Optional desired_service_levels As Dictionary(Of Integer, Double) = Nothing, Optional logging As Boolean = False,
                   Optional progressCallback As ProgressDelegate = Nothing, Optional CostFunction As OptimisedFor = OptimisedFor.CostsWithLostSales)

        Me.Warehouse_group_to_optimise = New Warehouse_Group(list_of_warehouse_inputs, list_of_warehouse_relationships)
        Me.optimisation_order = Warehouse_group_to_optimise.calculate_reorder_order()
        Me.optimisation_order.Reverse()
        Me.logging_points = New Stock_wizard_logging(list_of_warehouse_inputs)
        Me.logging = logging
        Me.progressCallback = progressCallback
        Me.CostFunction = CostFunction

        '''sets the desired service levels to 0.95 if none are provided
        If Me.desired_service_level Is Nothing Then
            Me.desired_service_level = New Dictionary(Of Integer, Double)
            For Each warehouse In Warehouse_group_to_optimise.warehouse_inputs
                Me.desired_service_level.Add(warehouse.warehouse_id, 0.95)
            Next
        Else
            Me.desired_service_level = desired_service_levels
        End If



    End Sub

    Public Function Run_stock_wizard(num_optimisating_rounds As Integer, iteration_params As List(Of Stock_wizard_iteration_inputs)) As (Dictionary(Of Integer, List(Of Double)), Dictionary(Of Integer, List(Of Double)))

        '''This guard is to ensure that the inputs are of the correct length
        If num_optimisating_rounds <> iteration_params.Count Then
            Throw New Exception("The number of dual and single simulation rounds must add up to the size of the params")
        End If

        ''Sets up the values for the progres bars
        Me.total_rounds = 0
        For Each iteration In iteration_params
            Me.total_rounds += iteration.num_iterations_for_round
        Next

        ''Sets up the returns dictionaries
        Dim reorder_points As New Dictionary(Of Integer, List(Of Double))
        Dim reorder_amounts As New Dictionary(Of Integer, List(Of Double))

        For Each warehouse In Warehouse_group_to_optimise.warehouse_inputs
            reorder_points.Add(warehouse.warehouse_id, New List(Of Double))
            reorder_amounts.Add(warehouse.warehouse_id, New List(Of Double))

            reorder_points(warehouse.warehouse_id).Add(warehouse.reorder_point)
            reorder_amounts(warehouse.warehouse_id).Add(warehouse.reorder_amount)
        Next

        ''The retun positions from the simulations can not match up with warehouse ID
        ''This maps the warehouse ID to the position in the return list for simulation results
        Dim warehouse_id_to_return_position As New Dictionary(Of Integer, Integer)
        For i As Integer = 0 To Warehouse_group_to_optimise.warehouse_inputs.Count - 1
            warehouse_id_to_return_position.Add(Warehouse_group_to_optimise.warehouse_inputs(i).warehouse_id, i)
        Next

        '''This sets up many simulation rounds with each with changing params
        For i As Integer = 0 To num_optimisating_rounds - 1
            '''Goes through runs this round of optimisation
            'Debug.WriteLine("Starting optimisation round")
            Dim next_points = run_round_of_optimisation(iteration_params(i), warehouse_id_to_return_position)
            reorder_points = Utils.Add_dictionaries_of_lists(reorder_points, next_points.Item1)
            reorder_amounts = Utils.Add_dictionaries_of_lists(reorder_amounts, next_points.Item2)
        Next


        Return (reorder_points, reorder_amounts)


    End Function


    ''' <summary>
    ''' Runs a round of optimization for the specified number of iterations and simulation length.
    ''' </summary>
    ''' <param name="number_iterations">The number of iterations to run the optimization.</param>
    ''' <param name="simulation_length">The length of each simulation in days.</param>
    ''' <param name="alpha_amount">The learning rate for adjusting reorder amounts.</param>
    ''' <param name="alpha_point">The learning rate for adjusting reorder points.</param>
    ''' <returns>
    ''' A tuple containing two dictionaries:
    ''' <list type="bullet">
    ''' <item><description>A dictionary mapping warehouse IDs to lists of optimized reorder points.</description></item>
    ''' <item><description>A dictionary mapping warehouse IDs to lists of optimized reorder amounts.</description></item>
    ''' </list>
    ''' </returns>
    ''' <remarks>
    ''' It checks the iteration parmams to see if there is one or two variables to optimise.
    ''' It updates the reorder points and amounts based on the calculated gradients and learning rates.
    ''' </remarks>
    Public Function run_round_of_optimisation(iteration_params As Stock_wizard_iteration_inputs, warehouse_id_to_pos_dict As Dictionary(Of Integer, Integer)) As (Dictionary(Of Integer, List(Of Double)), Dictionary(Of Integer, List(Of Double)))

        'Debug.WriteLine("New Round of optimisation Starting")

        Dim reorder_points As New Dictionary(Of Integer, List(Of Double))
        Dim reorder_amounts As New Dictionary(Of Integer, List(Of Double))

        For Each warehouse In Warehouse_group_to_optimise.warehouse_inputs
            reorder_points.Add(warehouse.warehouse_id, New List(Of Double))
            reorder_amounts.Add(warehouse.warehouse_id, New List(Of Double))
        Next

        If iteration_params.num_var_to_optimise = 2 Then
            For i As Integer = 0 To iteration_params.num_iterations_for_round - 1
                For Each warehouse_id In optimisation_order
                    'Debug.WriteLine("Optimising warehouse " & warehouse_id)
                    'Dim warehouse_inputs_to_optimise = Warehouse_group_to_optimise.warehouse_inputs(warehouse_id_to_pos_dict(warehouse_id))
                    Dim new_points = two_var_gradient_descent(Warehouse_group_to_optimise.warehouse_inputs(warehouse_id_to_pos_dict(warehouse_id)), iteration_params)
                    reorder_points(warehouse_id).Add(new_points.Item1)
                    reorder_amounts(warehouse_id).Add(new_points.Item2)
                Next

                Me.current_iteration_round += 1
                Debug.Write("Updating Progress Bar")
                If progressCallback IsNot Nothing Then
                    progressCallback(CInt((current_iteration_round / total_rounds) * 100))
                End If


            Next


        ElseIf iteration_params.num_var_to_optimise = 1 Then
            For i As Integer = 0 To iteration_params.num_iterations_for_round - 1
                For Each warehouse_id In optimisation_order
                    'Dim warehouse_inputs_to_optimise = Warehouse_group_to_optimise.warehouse_inputs(warehouse_id_to_pos_dict(warehouse_id))
                    Dim new_points = one_var_gradient_descent(Warehouse_group_to_optimise.warehouse_inputs(warehouse_id_to_pos_dict(warehouse_id)), iteration_params, annealing:=True, annealing_value:=i)
                    reorder_points(warehouse_id).Add(new_points.Item1)
                    reorder_amounts(warehouse_id).Add(new_points.Item2)
                Next

                If progressCallback IsNot Nothing Then
                    progressCallback(CInt(current_iteration_round / total_rounds) * 100)
                End If

            Next
        End If




        Return (reorder_points, reorder_amounts)
    End Function

    Public Function one_var_gradient_descent(ByRef warehouse_inputs As Warehouse_inputs, iteration_params As Stock_wizard_iteration_inputs, Optional annealing As Boolean = True, Optional annealing_value As Integer = 0, Optional Beta As Double = 0.07) As (Double, Double)
        Dim current_reorder_point = warehouse_inputs.reorder_point
        '''This now assumes you can only opperate in full pallets (assumes to be used in later optiisation rounds)
        Dim rounded_reorder_amount = CInt(Math.Round(warehouse_inputs.reorder_amount))
        rounded_reorder_amount = rounded_reorder_amount * warehouse_inputs.items_per_pallet

        Debug.WriteLine("Otpimising For Reorder point " & current_reorder_point & "  reorder_amount is constant ")

        Dim gradient = numerical_gradient_1d(warehouse_inputs, iteration_params, current_reorder_point, rounded_reorder_amount, iteration_params.delta_point * current_reorder_point)

        Dim updated_reorder_point As Double
        If annealing Then
            Dim alpha_this_iteration = iteration_params.alpha_point / (1 + (Beta * annealing_value))
            updated_reorder_point = current_reorder_point - alpha_this_iteration * gradient
        Else
            updated_reorder_point = current_reorder_point - iteration_params.alpha_point * gradient
        End If

        '''This is agaiun to ensure one simulation doesn't send the values flying in one direction
        If updated_reorder_point > 2 * (current_reorder_point + 50) Then
            updated_reorder_point = 2 * current_reorder_point + 50
        ElseIf updated_reorder_point < 0.5 * (current_reorder_point) Then
            updated_reorder_point = 0.5 * current_reorder_point
        End If


        Debug.WriteLine("Updated Reorder Point is " & updated_reorder_point)

        If logging Then
            Dim logging_iteration_params As Stock_wizard_iteration_inputs = New Stock_wizard_iteration_inputs(1, 200, 1000, 1, 1)
            Dim results_logging_test = run_simulation_with_changes(warehouse_inputs, logging_iteration_params, updated_reorder_point, rounded_reorder_amount)
            logging_points.add_costs(warehouse_inputs.warehouse_id, results_logging_test.Item3)
            logging_points.add_service_levels(warehouse_inputs.warehouse_id, results_logging_test.Item1(1) + results_logging_test.Item1(2))

            logging_points.add_reorder_points_and_amounts(warehouse_inputs.warehouse_id, updated_reorder_point, rounded_reorder_amount)
        End If


        warehouse_inputs.reorder_point = updated_reorder_point
        warehouse_inputs.reorder_amount = rounded_reorder_amount / warehouse_inputs.items_per_pallet



        Return (updated_reorder_point, rounded_reorder_amount)


    End Function



    Public Function two_var_gradient_descent(ByRef warehouse_inputs As Warehouse_inputs, iteration_params As Stock_wizard_iteration_inputs, Optional annealing As Boolean = False, Optional annealing_value As Double = 0, Optional Beta As Double = 0.05) As (Double, Double)

        Dim current_reorder_point = warehouse_inputs.reorder_point
        Dim current_reorder_amount = warehouse_inputs.reorder_amount * warehouse_inputs.items_per_pallet

        Debug.WriteLine("Otpimising For Reorder point " & current_reorder_point & "  reorder_amount " & current_reorder_amount)


        ''This just unwraps some of the data to make the code clearer
        Dim delta_point = iteration_params.delta_point
        Dim delta_amount = iteration_params.delta_amount
        Dim alpha_point = iteration_params.alpha_point
        Dim alpha_amount = iteration_params.alpha_amount


        If annealing Then
            alpha_point = alpha_point / (1 + (Beta * annealing_value))
            alpha_amount = alpha_amount / (1 + (Beta * annealing_value))
        End If


        Dim gradient = Numerical_gradient_2d(warehouse_inputs, iteration_params, current_reorder_point, current_reorder_amount, delta_point * current_reorder_point, delta_amount * current_reorder_amount)
        Dim updated_reorder_point = current_reorder_point - alpha_point * gradient.Item1

        ''This part makes sure one simulation doesn't explode or drop the values to unrealistic numbers
        If updated_reorder_point > 2 * (current_reorder_point + 50) Then
            updated_reorder_point = 2 * current_reorder_point + 50
        ElseIf updated_reorder_point < 0.5 * (current_reorder_point) Then
            updated_reorder_point = 0.5 * current_reorder_point
        End If

        Dim updated_reorder_amount = current_reorder_amount - alpha_amount * gradient.Item2
        If updated_reorder_amount > 2 * (current_reorder_amount + 10) Then
            updated_reorder_amount = 2 * current_reorder_amount + 10
        ElseIf updated_reorder_amount < 0.5 * (current_reorder_amount) Then
            updated_reorder_amount = 0.5 * current_reorder_amount
        End If

        Debug.WriteLine("Updated Reorder Point is " & updated_reorder_point & " Reorder_amount is " & updated_reorder_amount)


        If logging Then
            Dim logging_iteration_params As Stock_wizard_iteration_inputs = New Stock_wizard_iteration_inputs(1, 200, 1000, 1, 1)
            Dim results_logging_test = run_simulation_with_changes(warehouse_inputs, logging_iteration_params, updated_reorder_point, updated_reorder_amount)
            logging_points.add_costs(warehouse_inputs.warehouse_id, results_logging_test.Item3)
            logging_points.add_service_levels(warehouse_inputs.warehouse_id, results_logging_test.Item1(1) + results_logging_test.Item1(2))
            logging_points.add_reorder_points_and_amounts(warehouse_inputs.warehouse_id, updated_reorder_point, updated_reorder_amount)
        End If

        'Debug.WriteLine("Current_reorder_point is," & current_reorder_point & " New Reorder_point is " & updated_reorder_point)
        warehouse_inputs.reorder_point = updated_reorder_point
        warehouse_inputs.reorder_amount = updated_reorder_amount / warehouse_inputs.items_per_pallet
        Return (updated_reorder_point, updated_reorder_amount)
    End Function


    Public Function numerical_gradient_1d(warehouse_input As Warehouse_inputs, iteration_params As Stock_wizard_iteration_inputs, reorder_point As Double, reorder_amount As Double, delta_reorder_point As Double) As Double


        Dim forward_x = Cost_function(warehouse_input, iteration_params, reorder_point + delta_reorder_point, reorder_amount)
        Dim backward_x = Cost_function(warehouse_input, iteration_params, reorder_point - delta_reorder_point, reorder_amount)
        Dim gradient = (forward_x - backward_x) / (2 * delta_reorder_point)

        If logging Then
            logging_points.add_gradient_points(warehouse_input.warehouse_id, gradient, 0)
        End If
        Return gradient

    End Function

    ''' <summary>
    ''' Calculates the numerical gradient of the cost function with respect to reorder point and reorder amount.
    ''' </summary>
    ''' <param name="warehouse_inputs">The inputs for the warehouse being optimized.</param>
    ''' <param name="simulation_length">The length of each simulation in days.</param>
    ''' <param name="reorder_point">The current reorder point.</param>
    ''' <param name="reorder_amount">The current reorder amount.</param>
    ''' <param name="delta_reorder_point">The small change applied to the reorder point for gradient calculation.</param>
    ''' <param name="delta_reorder_amount">The small change applied to the reorder amount for gradient calculation.</param>
    ''' <returns>
    ''' A tuple containing two values:
    ''' <list type="bullet">
    ''' <item><description>The gradient with respect to the reorder point.</description></item>
    ''' <item><description>The gradient with respect to the reorder amount.</description></item>
    ''' </list>
    ''' </returns>
    ''' <remarks>
    ''' This method uses central difference to approximate the gradient of the cost function.
    ''' It calculates the gradient by evaluating the cost function at slightly perturbed values of reorder point and reorder amount.
    ''' </remarks>
    Public Function Numerical_gradient_2d(ByRef warehouse_inputs As Warehouse_inputs, iteration_params As Stock_wizard_iteration_inputs, reorder_point As Double, reorder_amount As Double, delta_reorder_point As Double, delta_reorder_amount As Double) As (Double, Double)
        Dim forward_x = Cost_function(warehouse_inputs, iteration_params, reorder_point + delta_reorder_point, reorder_amount)
        Dim backward_x = Cost_function(warehouse_inputs, iteration_params, reorder_point - delta_reorder_point, reorder_amount)
        Dim gradient_x = (forward_x - backward_x) / (2 * delta_reorder_point)


        Dim forward_y = Cost_function(warehouse_inputs, iteration_params, reorder_point, reorder_amount + delta_reorder_amount)
        Dim backward_y = Cost_function(warehouse_inputs, iteration_params, reorder_point, reorder_amount - delta_reorder_amount)
        Dim gradient_y = (forward_y - backward_y) / (2 * delta_reorder_amount)

        Debug.WriteLine("Point Gradient is " & gradient_x & " Amount Gradient is " & gradient_y)


        If logging Then
            logging_points.add_gradient_points(warehouse_inputs.warehouse_id, gradient_x, gradient_y)
        End If
        Return (gradient_x, gradient_y)
    End Function

    ''' <summary>
    ''' Calculates the cost function with a penalty for not meeting the required service level.
    ''' </summary>
    ''' <param name="warehouse_inputs">The inputs for the warehouse being optimized.</param>
    ''' <param name="simulation_length">The length of each simulation in days.</param>
    ''' <param name="reorder_point">The current reorder point.</param>
    ''' <param name="reorder_amount">The current reorder amount.</param>
    ''' <param name="required_SL">The required service level. Default is 0.95.</param>
    ''' <param name="base_penalty">The base penalty coefficient. Default is 1000.</param>
    ''' <returns>
    ''' The total cost including the penalty for not meeting the required service level.
    ''' </returns>
    ''' <remarks>
    ''' This method runs a simulation to calculate the service levels and costs, then applies a penalty
    ''' if the service levels do not meet the required threshold. The penalty is proportional to the
    ''' square of the difference between the required service level and the actual service level.
    ''' </remarks>
    Public Function Cost_function(ByRef warehouse_inputs As Warehouse_inputs, iteration_params As Stock_wizard_iteration_inputs, reorder_point As Double, reorder_amount As Double) As Double
        Dim service_levels_and_costs = run_simulation_with_changes(warehouse_inputs, iteration_params, reorder_amount, reorder_point)

        Dim service_levels = service_levels_and_costs.Item1
        Dim service_level_order = service_levels_and_costs.Item2
        Dim costs = service_levels_and_costs.Item3
        Dim lost_sales = service_levels_and_costs.Item4

        Dim total_costs = costs

        ''checks if lost sales needs to be added and then does so. 
        If Me.CostFunction <> OptimisedFor.CostWithPenalty Then
            total_costs += lost_sales
        End If


        Dim penatly As Double = 0
        Dim penalty_coefficient As Double = 0

        If Me.CostFunction <> OptimisedFor.CostsWithLostSales Then
            For i As Integer = 0 To service_levels.Count - 1
                If service_levels(i) <> -1 Then
                    penatly += (Math.Max(0, desired_service_level(service_level_order(i)) - service_levels(i)) ^ 2)
                End If
            Next

            penalty_coefficient = costs * iteration_params.base_penalty
        End If

        If logging Then
            logging_points.add_penalty(warehouse_inputs.warehouse_id, penalty_coefficient * penatly)
        End If
        Debug.WriteLine("Penalty is " & penalty_coefficient * penatly)
        Debug.WriteLine("Total_is " & costs + penalty_coefficient * penatly)



        Return total_costs + penalty_coefficient * penatly

        Return 0
    End Function



    ''' <summary>
    ''' Runs a simulation with the given changes to reorder amount and reorder point.
    ''' </summary>
    ''' <param name="warehouse_inputs">The inputs for the warehouse being optimized.</param>
    ''' <param name="simulation_length">The length of each simulation in days.</param>
    ''' <param name="reorder_amount">The current reorder amount.</param>
    ''' <param name="reorder_point">The current reorder point.</param>
    ''' <returns>
    ''' A tuple containing:
    ''' <list type="bullet">
    ''' <item><description>A list of service levels for each simulation run.</description></item>
    ''' <item><description>The total cost of the simulation.</description></item>
    ''' </list>
    ''' </returns>
    ''' <remarks>
    ''' This method updates the reorder point and amount for the warehouse inputs, then runs a Monte Carlo simulation
    ''' to calculate the service levels and total cost.
    ''' </remarks>
    Public Function run_simulation_with_changes(ByRef warehouse_inputs As Warehouse_inputs, iteration_params As Stock_wizard_iteration_inputs, reorder_amount As Double, reorder_point As Double) As (List(Of Double), List(Of Integer), Double, Double)
        Debug.WriteLine("Sub Simulation with reorder_point " & reorder_point & " reorder_amount" & reorder_amount)

        warehouse_inputs.reorder_point = reorder_point
        warehouse_inputs.reorder_amount = reorder_amount / warehouse_inputs.items_per_pallet


        'Dim results = Warehouse_group_to_optimise


        '''This is the light monte carlo method that is used to reduce memoery requirements
        Dim results = Warehouse_group_to_optimise.run_light_Monte_Carlo(iteration_params.number_simulations, iteration_params.number_days)

        Dim service_levels = results.Service_levels.Item1
        Dim service_level_order = results.Warehouse_order
        Dim storage_costs = results.Storage_costs.Item1
        Dim reorder_costs = results.Reorder_costs.Item1
        Dim lost_sales = results.Lost_sales_costs.Item1

        Debug.WriteLine("Service Levels are " & service_levels.Sum)
        Debug.WriteLine("Storage costs are " & storage_costs.Sum)
        Debug.WriteLine("Reorder_costs are " & reorder_costs.Sum)
        Debug.WriteLine("Reorder_costs are " & lost_sales.Sum)

        'If logging Then
        '    logging_points.add_reorder_points_and_amounts(warehouse_inputs.warehouse_id, reorder_point, reorder_amount)
        '    logging_points.add_service_levels(warehouse_inputs.warehouse_id, service_levels.Sum)
        '    logging_points.add_costs(warehouse_inputs.warehouse_id, storage_costs.Sum + reorder_costs.Sum)
        'End If

        Dim total_costs As Double = storage_costs.Sum + reorder_costs.Sum
        Dim lost_sales_sum = lost_sales.Sum

        Return (service_levels, service_level_order, total_costs, lost_sales_sum)
    End Function

    Public Function getWarehouseGroup() As Warehouse_Group
        Return Warehouse_group_to_optimise
    End Function
End Class
