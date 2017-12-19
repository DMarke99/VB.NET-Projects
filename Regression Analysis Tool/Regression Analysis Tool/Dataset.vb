''' <summary>
''' Represents a dataset and it's properties
''' </summary>
Public Class Dataset
    Protected Rows As Integer
    Protected Columns
    Protected Headers() As String
    Protected Data(,) As Decimal

    'Properties

    ''' <summary>
    '''  Returns the number of columns in the dataset
    ''' </summary>
    ''' <returns>Returns the number of columns in the dataset</returns>
    ReadOnly Property Column
        Get
            Return Columns
        End Get
    End Property

    ''' <summary>
    '''  Returns the number of rows in the dataset
    ''' </summary>
    ''' <returns>Returns the number of rows in the dataset</returns>
    ReadOnly Property Row
        Get
            Return Rows
        End Get
    End Property

    ''' <summary>
    ''' Returns a particular header from the dataset
    ''' </summary>
    ''' <param name="i">The index of the header to be returned</param>
    ReadOnly Property Header(ByVal i As Integer)
        Get
            Return Headers(i)
        End Get
    End Property

    ''' <summary>
    ''' Returns a particular value from the dataset
    ''' </summary>
    ''' <param name="r">The row of the value to be returned</param>
    ''' <param name="c">The column of the value to be returned</param>
    ReadOnly Property Value(ByVal r As Integer, ByVal c As Integer)
        Get
            Return Data(r, c)
        End Get
    End Property

    'Getters

    ''' <summary>
    ''' Returns the data from a dataset
    ''' </summary>
    Function getData() As Decimal(,)
        Return Me.Data
    End Function

    ''' <summary>
    ''' Returns the ranks of the data of a dataset
    ''' </summary>
    Function getRankedData() As Decimal(,)
        Dim RankedData(Row, Column) As Decimal
        Dim TempData As New List(Of Tuple(Of Decimal, Integer))
        Dim row_index As New List(Of Integer)
        Dim num_added As Integer = 0
        Dim curr_val As Decimal

        'does ranking algorithm for every column
        For c As Integer = 0 To Column - 1
            TempData.Clear()
            row_index.Clear()
            num_added = 0

            'adds all items into a list with their row index
            For r As Integer = 0 To Row - 1
                TempData.Add(Tuple.Create(Data(r, c), r))
            Next

            'sorts the data in ascending order
            AddFunc.QuickSort(TempData)


            'goes through the list, adding the shared rank of the lowest element to the corresponding place in the rank array
            Do Until num_added = Row
                curr_val = TempData.First().Item1

                'removes all instances of item in the list
                Do While TempData.Count <> 0 AndAlso TempData.First().Item1 = curr_val
                    row_index.Add(TempData.First().Item2)
                    TempData.RemoveAt(0)
                Loop

                'calculates shared rank and adds rank to relevant place
                For Each r As Integer In row_index
                    RankedData(r, c) = num_added + (row_index.Count + 1) / 2
                Next

                'increases number added by the number of the current item
                num_added = num_added + row_index.Count

                'clears the index list
                row_index.Clear()
            Loop

        Next

        Return RankedData
    End Function

    ''' <summary>
    ''' Returns the headers from a dataset
    ''' </summary>
    Function getHeaders() As String()
        Return Me.Headers
    End Function

    ''' <summary>
    ''' Returns the length of the longest string in dataset
    ''' </summary>
    Function getMaxHeaderLength() As Integer
        getMaxHeaderLength = 1

        For i As Integer = 0 To Column - 1
            'if length is greater than current max then it becomes the new current max
            If Headers(i).Length > getMaxHeaderLength Then
                getMaxHeaderLength = Headers(i).Length
            End If
        Next
    End Function

    'Setters

    ''' <summary>
    ''' Imports a .csv table into the program
    ''' </summary>
    ''' <param name="address">The address of the .csv file</param>
    ''' <param name="contains_headers">Decides whether the .csv contains headers</param>
    Sub Import(ByVal address As String, ByVal contains_headers As Boolean)
        Dim r As New IO.StreamReader(address)

        'finds the number of columns
        Rows = 0
        Dim current_line As String = r.ReadLine

        'finds the number of columns
        'counts the number of elements in a comma delimited array
        Columns = current_line.Split(",").Length

        'finds the number of columns
        Do Until current_line Is Nothing
            Rows = Rows + 1
            current_line = r.ReadLine
        Loop

        If contains_headers Then
            Rows = Rows - 1
        End If

        'resets streamreader
        r.Close()
        r = New IO.StreamReader(address)

        ReDim Data(Rows - 1, Columns - 1)
        ReDim Headers(Columns - 1)

        'deals with the addition of headers
        If contains_headers Then
            current_line = r.ReadLine

            Dim temp() = current_line.Split(",")

            For y As Integer = 0 To Columns - 1
                Headers(y) = temp(y)
            Next
        Else
            For x As Integer = 1 To Columns
                Headers(x - 1) = "Variable " + x.ToString
            Next
        End If

        'imports values into array
        For x As Integer = 0 To Me.Rows - 1
            current_line = r.ReadLine

            If current_line.Split(",").Length <> Me.Columns Then
                Throw New CSVException("This .csv file contains a structural error")
            End If

            Dim temp() = current_line.Split(",")

            For y As Integer = 0 To Me.Columns - 1
                Data(x, y) = Convert.ToDecimal(temp(y))
            Next
        Next
    End Sub

    'Additional Procedures

    ''' <summary>
    ''' Prints the table inside the dataset
    ''' Exists purely for testing purposes rather than functional purposes
    ''' </summary>
    ''' <param name="decimal_places">The maximum number of decimal places to be displayed</param>
    Sub Print(Optional ByVal decimal_places As Integer = 3)
        For y As Integer = 1 To Me.Columns
            Console.Write("{0,10}", Headers(y))
        Next
        Console.WriteLine()

        For x As Integer = 1 To Me.Rows
            For y As Integer = 1 To Me.Columns
                Console.Write("{0,10}", Math.Round(Me.Data(x, y), decimal_places))
            Next
            Console.WriteLine()
        Next
    End Sub

End Class
