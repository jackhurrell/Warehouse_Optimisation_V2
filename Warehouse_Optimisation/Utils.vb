Imports System.Runtime.CompilerServices
Imports System.Threading


' This module contains utility functions that are used throughout the project.
Public Module Utils


    ''' <summary> 
    ''' Generates an array of random numbers that follow a normal (Gaussian) distribution. 
    '''Ensures that the numbers are at least 0
    ''' </summary> 
    ''' <param name="count">The number of random numbers to generate.</param> 
    ''' <param name="mean">The mean (average) value of the normal distribution.</param> ''' <param name="stdDev">The standard deviation of the normal distribution, which controls the spread of the numbers.</param> 
    ''' <returns> 
    ''' An array of <c>Double</c> values, each representing a random number from a normal distribution with the specified mean and standard deviation. 
    ''' </returns> 
    ''' <exception cref="ArgumentException">
    ''' Thrown if <paramref name="count"/> is less than or equal to zero. 
    ''' </exception>
    Public Function generate_normal_random_ints(mean As Double, sd As Double, num_ints As Integer) As Integer()
        Dim rand As New Random()
        Dim numbers(num_ints - 1) As Integer

        For i As Integer = 0 To num_ints - 1 Step 2

            'This is using the Box Muller transform - wiki can explain more'
            Dim U1 As Double = rand.NextDouble()
            Dim U2 As Double = rand.NextDouble()

            Dim Z0 As Double = Math.Sqrt(-2 * Math.Log(U1)) * Math.Cos(2 * Math.PI * U2)
            Dim Z1 As Double = Math.Sqrt(-2 * Math.Log(U1)) * Math.Sin(2 * Math.PI * U2)

            ' This part then shift the values based on mean and Sd''
            numbers(i) = Math.Max(0, CInt(mean + Z0 * sd))
            If i + 1 < num_ints Then
                numbers(i + 1) = Math.Max(0, CInt(mean + Z1 * sd))
            End If

        Next
        Return numbers
    End Function

    ''' <summary>
    ''' Calculates the lead time based on a normal distribution with the specified mean and standard deviation.
    ''' </summary>
    ''' <param name="mean">The mean (average) value of the normal distribution.</param>
    ''' <param name="sd">The standard deviation of the normal distribution, which controls the spread of the numbers.</param>
    ''' <returns>
    ''' An <c>Integer</c> representing the calculated lead time. If the standard deviation is zero, the mean is returned.
    ''' </returns>
    ''' <remarks>
    ''' This function uses the <see cref="generate_normal_random_ints"/> function to generate a random integer from a normal distribution.
    ''' </remarks>
    Public Function calculate_lead_time(mean As Double, sd As Double) As Integer
        If (sd = 0) Then
            Return CInt(mean)
        End If

        Dim lead_time As Integer() = generate_normal_random_ints(mean, sd, 1)
        lead_time(0) = Math.Max(1, lead_time(0))
        Return lead_time(0)

    End Function

    Public Function Add_integer_dictionaries(cumulative_dictionary As Dictionary(Of Integer, Integer), new_dictionary As Dictionary(Of Integer, Integer)) As Dictionary(Of Integer, Integer)
        For Each key In new_dictionary.Keys
            If cumulative_dictionary.ContainsKey(key) Then
                cumulative_dictionary(key) += new_dictionary(key)
            Else
                cumulative_dictionary.Add(key, new_dictionary(key))
            End If
        Next
        Return cumulative_dictionary
    End Function

    ''' <summary>
    ''' Merges two dictionaries of lists by adding the elements of the new dictionary to the old dictionary.
    ''' </summary>
    ''' <param name="Old_dictionary">The original dictionary to which elements will be added.</param>
    ''' <param name="New_dictionary">The dictionary containing elements to add to the original dictionary.</param>
    ''' <returns>
    ''' A dictionary where each key contains a list of combined elements from both the old and new dictionaries.
    ''' </returns>
    ''' <remarks>
    ''' If a key from the new dictionary already exists in the old dictionary, the elements from the new dictionary's list are appended to the old dictionary's list.
    ''' If a key from the new dictionary does not exist in the old dictionary, a new key-value pair is added to the old dictionary.
    ''' </remarks>
    Public Function Add_dictionaries_of_lists(Old_dictionary As Dictionary(Of Integer, List(Of Double)), New_dictionary As Dictionary(Of Integer, List(Of Double))) As Dictionary(Of Integer, List(Of Double))
        For Each kvp As KeyValuePair(Of Integer, List(Of Double)) In New_dictionary
            If Old_dictionary.ContainsKey(kvp.Key) Then
                Old_dictionary(kvp.Key).AddRange(kvp.Value)
            Else
                Old_dictionary.Add(kvp.Key, kvp.Value)
            End If
        Next

        Return Old_dictionary

    End Function

End Module
