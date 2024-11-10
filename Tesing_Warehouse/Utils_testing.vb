Imports Microsoft.VisualStudio.TestTools.UnitTesting
Imports Warehouse_Optimisation

<TestClass>
Public Class Utils_testing
    'Due to it's random nature it is difficult to test this function'
    'If you are unsure if it is performing, take the numbers and perform a qqplot'
    <TestMethod>
    Sub Test_generate_normal_random_ints()
        Dim test_no_sd As Integer() = Utils.generate_normal_random_ints(4, 0, 1000)
        For Each number As Integer In test_no_sd
            Assert.AreEqual(number, 4)
        Next

        Dim test_negative_numbers As Integer() = Utils.generate_normal_random_ints(-5, 1, 1000)
        Assert.IsTrue(test_negative_numbers.Average < 1 And test_negative_numbers.Average > -0.1)

        Dim test_0_nums As Integer() = Utils.generate_normal_random_ints(1, 2, 0)
        Assert.AreEqual(test_0_nums.Length, 0)

        Dim test_odd_num_values As Integer() = Utils.generate_normal_random_ints(0, 1, 1001)
        Assert.AreEqual(test_odd_num_values.Length, 1001)

        Dim test_1000_values As Integer() = Utils.generate_normal_random_ints(0, 1, 1000)
        Assert.AreEqual(test_1000_values.Length, 1000)

        Dim test_average_small As Integer() = Utils.generate_normal_random_ints(5, 2, 100)
        Assert.IsTrue(test_average_small.Average < 5.7 And test_average_small.Average > 4.3)
        Assert.IsTrue(calculate_standard_deviation(test_average_small) < 2.3 And calculate_standard_deviation(test_average_small) > 1.7)


        Dim test_average_large As Integer() = Utils.generate_normal_random_ints(5, 2, 10000)
        Assert.IsTrue(test_average_large.Average < 5.05 And test_average_large.Average > 4.95)
        Assert.IsTrue(calculate_standard_deviation(test_average_large) < 2.1 And calculate_standard_deviation(test_average_large) > 1.9)


        Dim test_standard_deviation As Integer() = Utils.generate_normal_random_ints(20, 4, 500)
        Assert.IsTrue(calculate_standard_deviation(test_standard_deviation) < 4.5 And calculate_standard_deviation(test_standard_deviation) > 3.5)
    End Sub

    'This is also agan difficult to test, but mostly relies on making sure
    'That the random normal number generator is working
    <TestMethod>
    Sub Test_calculate_lead_time()
        Assert.AreEqual(Utils.calculate_lead_time(5, 0), 5)


        Dim test_lead_time As Integer = Utils.calculate_lead_time(5, 1)
        Assert.IsTrue(test_lead_time > 3 And test_lead_time < 8)
    End Sub

    ''' <summary>
    ''' This test is not a unit test, but a test to check if the random number generator is working correctly.
    ''' It outputs a large number of values that can be plotted using a QQ plot to check if the values are normally distributed.
    ''' </summary>
    <TestMethod>
    Sub test_random_dist_using_matlab()

        Dim test_nums As Integer() = Utils.generate_normal_random_ints(4, 2, 1000)
        For Each num In test_nums
            Console.Write($"{num},")
        Next
    End Sub

    Private Function calculate_standard_deviation(numbers As Integer()) As Double
        Dim mean As Double = numbers.Average()
        Dim sumOfSquares As Double = 0

        For Each number As Integer In numbers
            sumOfSquares += (number - mean) ^ 2
        Next

        Dim variance As Double = sumOfSquares / numbers.Length
        Return Math.Sqrt(variance)

    End Function

End Class
