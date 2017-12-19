''' <summary>
''' Contains all the additional functions that are used in other classes and modules
''' </summary>
Public Class AddFunc

    ''' <summary>
    ''' Returns a decimal rounded to a number of significant figures
    ''' </summary>
    Public Shared Function SigFigs(ByVal n As Decimal, ByVal s As Integer) As Decimal
        If n = 0 Then
            Return 0
        Else
            'finds exponent
            Dim most_significant_digit_place As Integer = Math.Log10(Math.Abs(n))

            'returns rounded mantissa multiplied by 10 to the power of an exponent
            Return Math.Round((n / 10 ^ most_significant_digit_place), s - 1) * 10 ^ most_significant_digit_place
        End If
    End Function

    ''' <summary>
    ''' Returns a decimal number in standard form to
    ''' </summary>
    ''' <param name="n">The number to be converted to standard form</param>
    ''' <param name="sf">The number of significant figures the number will be returned to</param>
    Public Shared Function StandardForm(ByVal n As Decimal, Optional sf As Integer = 3) As String
        If n = 0 Then
            Return "0.00"
        Else
            'finds exponent
            Dim power As Integer = Math.Floor(Math.Log10(Math.Abs(n)))

            'finds mantissa
            Dim significant_bit As Double = n / 10 ^ power

            'outputs result
            If power = 0 Then
                Return significant_bit.ToString("F" & (sf - 1).ToString)
            Else
                Return significant_bit.ToString("F" & (sf - 1).ToString) & " × 10^" & Convert.ToString(power)
            End If
        End If
    End Function

    ''' <summary>
    ''' Performs Quicksort on tuples
    ''' </summary>
    ''' <param name="Values">the list of values to be sorted</param>
    Public Shared Sub QuickSort(ByRef Values As List(Of Tuple(Of Decimal, Integer)))
        QuickSort(Values, 0, Values.Count - 1)
    End Sub

    ''' <summary>
    '''  Performs Quicksort on tuples
    ''' </summary>
    ''' <param name="Values">the list of values to be sorted</param>
    ''' <param name="min">The minimum index to sort</param>
    ''' <param name="max">The maximum index to sort</param>
    Public Shared Sub QuickSort(ByRef Values As List(Of Tuple(Of Decimal, Integer)), min As Integer, max As Integer)
        If min >= max Then Exit Sub

        'the value about which values are swapped and moved
        Dim pivot As Decimal = Values(max).Item1

        'i initialises to one less than the minimum
        Dim i As Integer = min - 1

        For j As Integer = min To max - 1

            'if the item at j is less than the pivot it is swapped with the item at i + 1
            If Values(j).Item1 < pivot Then
                i = i + 1
                Swap(Values(i), Values(j))
            End If

        Next

        'the pivot is swapped with the item at i + 1
        Swap(Values(max), Values(i + 1))

        'the result is 2 partitions either side of the pivot
        'one partition contains numbers less than the pivot, and one  contains numbers greater than the pivot
        QuickSort(Values, min, i)
        QuickSort(Values, i + 2, max)
    End Sub

    ''' <summary>
    ''' Swaps two object
    ''' </summary>
    ''' <param name="i">The first object to be swapped</param>
    ''' <param name="j">The second object to be swapped</param>
    Public Shared Sub Swap(ByRef i As Object, ByRef j As Object)
        'if variables are of the same type swap them
        If i.GetType = j.GetType Then
            Dim temp As Object = i
            i = j
            j = temp
        Else
            Throw New System.Exception
        End If
    End Sub

End Class
