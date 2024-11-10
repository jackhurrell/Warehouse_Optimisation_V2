Public Class Stock_wizard_iteration_inputs
    Public num_iterations_for_round As Integer
    Public num_var_to_optimise As Integer
    Public number_simulations As Integer
    Public number_days As Integer
    Public alpha_point As Double
    Public alpha_amount As Double
    Public delta_point As Double
    Public delta_amount As Double
    Public tolerance As Double
    Public base_penalty As Double

    Public Sub New(num_iterations_for_round As Integer, number_simulation As Integer, number_days As Integer, alpha_point As Double, alpha_amount As Double, Optional delta_point As Double = 0.05, Optional delta_amount As Double = 0.05, Optional num_var_to_optimise As Integer = 2, Optional tolerance As Double = 0.0001, Optional base_penalty As Double = 1000)
        Me.number_simulations = number_simulation
        Me.number_days = number_days
        Me.alpha_point = alpha_point
        Me.alpha_amount = alpha_amount
        Me.delta_point = delta_point
        Me.delta_amount = delta_amount
        Me.num_var_to_optimise = num_var_to_optimise
        Me.num_iterations_for_round = num_iterations_for_round
        Me.base_penalty = base_penalty
        Me.tolerance = tolerance

    End Sub



End Class
