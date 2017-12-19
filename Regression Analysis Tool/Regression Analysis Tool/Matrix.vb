''' <summary>
''' Represents the mathematical data structure Matrix
''' </summary>
Public Class Matrix

    Protected r As Integer
    Protected c As Integer
    Protected e(,) As Decimal

    'Properties

    ''' <summary>
    ''' Represents the number of rows in the matrix
    ''' </summary>
    ''' <value>The value to replace the current number of rows</value>
    Property Row As Integer
        Get
            Return r
        End Get
        Set(value As Integer)

            r = value

            'creates a new array of values, and copies values across to that array
            Dim new_e(r - 1, c - 1) As Decimal

            For x As Integer = 0 To r - 1
                For y As Integer = 0 To Column - 1
                    new_e(x, y) = e(x, y)
                Next
            Next

            'sets the current array to the new array
            e = new_e.Clone
        End Set
    End Property

    ''' <summary>
    ''' Represents the number of columns in the matrix
    ''' </summary>
    ''' <value>The value to replace the current number of columns</value>
    Property Column As Integer
        Get
            Return c
        End Get
        Set(value As Integer)

            c = value

            'creates a new array of values, and copies values across to that array
            Dim new_e(r - 1, c - 1) As Decimal

            For x As Integer = 0 To Row - 1
                For y As Integer = 0 To c - 1
                    new_e(x, y) = e(x, y)
                Next
            Next

            'sets the current array to the new array
            e = new_e.Clone
        End Set
    End Property

    ''' <summary>
    ''' Represents an element in the matrix
    ''' </summary>
    ''' <param name="a">The row of the matrix</param>
    ''' <param name="b">The column of the matrix</param>
    ''' <value>The decimal value to be input into the matrix in the specified position</value>
    ''' <remarks>Also the default property of the matrix class</remarks>
    Default Property Element(ByVal a As Integer, ByVal b As Integer) As Decimal
        Get
            Return e(a - 1, b - 1)
        End Get
        Set(value As Decimal)
            e(a - 1, b - 1) = value
        End Set
    End Property

    'Initialisation

    ''' <summary>
    ''' Initialises a 2 by 2 square
    ''' </summary>
    Sub New()
        r = 2
        c = 2
        ReDim e(Row - 1, Column - 1)
    End Sub

    ''' <summary>
    ''' Initialises an n by n square
    ''' </summary>
    ''' <param name="n">The size of the matrix</param>
    ''' <remarks></remarks>
    Sub New(ByVal n As Integer)
        r = n
        c = n
        ReDim e(Row - 1, Column - 1)
    End Sub

    ''' <summary>
    ''' Initialises a matrix with two inputs it initialises to a m by n square
    ''' </summary>
    ''' <param name="no_of_rows">The number of rows in the matrix</param>
    ''' <param name="no_of_columns">The number of columns in the matrix</param>
    ''' <remarks></remarks>
    Sub New(ByVal no_of_rows As Integer, ByVal no_of_columns As Integer)
        r = no_of_rows
        c = no_of_columns
        ReDim e(Row - 1, Column - 1)
    End Sub

    ''' <summary>
    ''' Initialises a matrix with a two dimensional array input
    ''' </summary>
    ''' <param name="values">The array of values being converted into a matrix</param>
    Sub New(ByVal values(,) As Decimal)
        'gets dimensions of array
        Me.r = values.GetUpperBound(0) + 1
        Me.c = values.GetUpperBound(1) + 1
        ReDim e(Row - 1, Column - 1)

        'transfers values from the array to the matrix
        For x As Integer = 0 To Row - 1
            For y As Integer = 0 To Column - 1
                Element(x + 1, y + 1) = values(x, y)
            Next
        Next
    End Sub

    'General Functions

    ''' <summary>
    ''' Returns an identity matrix of size n
    ''' </summary>
    ''' <param name="n">The size of the matrix</param>
    Public Shared Function Identity(ByVal n As Integer)
        Dim M As New Matrix(n)

        'values along the leading diagonal are 0
        For x As Integer = 1 To n
            M(x, x) = 1
        Next

        Return M
    End Function

    ''' <summary>
    ''' Prints the subject matrix to the console
    ''' Exists purely for testing purposes, not necessary in code
    ''' </summary>
    Sub Print(Optional ByVal decimal_places As Integer = 3)
        'returns each row
        For x As Integer = 1 To Me.Row
            Console.Write("|")
            For y As Integer = 1 To Me.Column
                Console.Write("{0,6}", Math.Round(Me.Element(x, y), decimal_places))
            Next
            Console.Write("|")
            Console.WriteLine()
        Next
        Console.WriteLine()
    End Sub

    ''' <summary>
    ''' Omits a selected row from a matrix
    ''' </summary>
    ''' <param name="r">the index of the row to be omitted</param>
    Public Sub RemoveRow(ByVal r As Integer)
        'values below the row to be omitted are shifted up one place
        If r >= 1 And r <= Row Then
            For x As Integer = r + 1 To Row
                For y As Integer = 1 To Column
                    Element(x - 1, y) = Element(x, y)
                Next
            Next

            'one row is removed
            Row = Row - 1
        Else
            Throw New MatrixCompatibilityException("The index entered is invalid for the remove function")
        End If
    End Sub

    ''' <summary>
    ''' Omits a selected column from a matrix
    ''' </summary>
    ''' <param name="c">the index of the column to be omitted</param>
    Public Sub RemoveColumn(ByVal c As Integer)
        'values to the right of column to be removed are shifted one place to the right
        If c >= 1 And c <= Column Then
            For y As Integer = c + 1 To Column
                For x As Integer = 1 To Row
                    Element(x, y - 1) = Element(x, y)
                Next
            Next

            'one column is removed
            Column = Column - 1
        Else
            Throw New MatrixCompatibilityException("The index entered is invalid for the remove function")
        End If
    End Sub

    ''' <summary>
    ''' Creates a clone of the current matrix
    ''' This is to stop copies of the matrix not creating a separate instance of the matrix and referencing the same properties
    ''' </summary>
    Function Clone()
        Dim N As New Matrix(Row, Column)
        For i As Integer = 1 To Row
            For j As Integer = 1 To Column
                N(i, j) = Element(i, j)
            Next
        Next
        Return N
    End Function

    'Matrix Operations

    ''' <summary>
    ''' Addition for matrices
    ''' </summary>
    ''' <param name="A">The left matrix</param>
    ''' <param name="B">The right matrix</param>
    Public Shared Operator +(ByVal A As Matrix, ByVal B As Matrix) As Matrix
        If A.Row <> B.Row Or A.Column <> B.Column Then
            Throw New MatrixCompatibilityException("Matrices not compatible for this function")
        Else
            'initialises the resultant matrix
            Dim M As New Matrix(A.Row, A.Column)

            'adds each element in A to the corresponding element in B
            For x As Integer = 1 To A.Row
                For y As Integer = 1 To A.Column
                    M(x, y) = A(x, y) + B(x, y)
                Next
            Next

            Return M
        End If
    End Operator

    ''' <summary>
    ''' Subtraction of matrices
    ''' </summary>
    ''' <param name="A">The matrix being subtracted from</param>
    ''' <param name="B">The matrix that is being taken away from the other matrix</param>
    Public Shared Operator -(ByVal A As Matrix, ByVal B As Matrix) As Matrix
        If A.Row <> B.Row Or A.Column <> B.Column Then
            Throw New MatrixCompatibilityException("Matrices not compatible for this function")
        Else
            'initialises the resultant matrix
            Dim M As New Matrix(A.Row, A.Column)

            'subtracts each element in B fron the corresponding element in A
            For x As Integer = 1 To A.Row
                For y As Integer = 1 To A.Column
                    M(x, y) = A(x, y) - B(x, y)
                Next
            Next

            Return M
        End If
    End Operator

    ''' <summary>
    ''' Defines scalar multiplication for matrices in a separate orientation
    ''' </summary>
    ''' <param name="A">The matrix in the multiplication</param>
    ''' <param name="k">The constant in the multiplication</param>
    Public Shared Operator *(ByVal k As Decimal, ByVal A As Matrix) As Matrix
        'initialises the resultant matrix
        Dim M As New Matrix(A.Row, A.Column)

        'scales each element
        For x As Integer = 1 To A.Row
            For y As Integer = 1 To A.Column
                M(x, y) = A(x, y) * k
            Next
        Next

        Return M
    End Operator

    ''' <summary>
    ''' Defines scalar multiplication for matrices in a separate orientation
    ''' </summary>
    ''' <param name="A">The matrix in the multiplication</param>
    ''' <param name="k">The constant in the multiplication</param>
    Public Shared Operator *(ByVal A As Matrix, ByVal k As Decimal) As Matrix
        Return k * A
    End Operator

    ''' <summary>
    ''' Matrix-matrix multiplication
    ''' </summary>
    ''' <param name="A">The left matrix in the multiplication</param>
    ''' <param name="B">The right matrix in the multiplication</param>
    Public Shared Operator *(ByVal A As Matrix, ByVal B As Matrix) As Matrix
        'handles matrices that aren't compatible for multiplication
        If A.Column <> B.Row Then
            Throw New MatrixCompatibilityException("Matrices not compatible for this function")
        Else
            'initialises the resultant matrix
            Dim M As New Matrix(A.Row, B.Column)

            'sums the product of elements in A and their corresponding elements in B
            For x As Integer = 1 To A.Row
                For y As Integer = 1 To B.Column
                    For i As Integer = 1 To A.Column
                        M(x, y) = M(x, y) + A(x, i) * B(i, y)
                    Next
                Next
            Next

            Return M
        End If
    End Operator

    'Advanced Matrix Functions

    ''' <summary>
    ''' Omits a selected row and column from a matrix
    ''' </summary>
    ''' <param name="r">The index of the row to be omitted</param>
    ''' <param name="c">The column of the row to be omitted</param>
    Function MinorMatrix(ByVal r As Integer, ByVal c As Integer) As Matrix
        If Me.Row = 0 Or Me.Column = 0 Then
            Throw New MatrixCompatibilityException("Matrix isn't compatible with this function")
        Else
            MinorMatrix = Me.Clone

            'removes rth row and cth column
            MinorMatrix.RemoveColumn(c)
            MinorMatrix.RemoveRow(r)
        End If
    End Function

    ''' <summary>
    ''' Returns the transpose of the subject matrix
    ''' </summary>
    Function Transpose() As Matrix
        Transpose = New Matrix(Me.Column, Me.Row)

        'swaps an element's x and y coordinates
        For x As Integer = 1 To Me.Row
            For y As Integer = 1 To Me.Column
                Transpose.Element(y, x) = Element(x, y)
            Next
        Next
    End Function

    ''' <summary>
    ''' Returns the determinant of a matrix
    ''' </summary>
    Function Determinant() As Double
        'only allows square matrices
        If Me.Row <> Me.Column Then
            Throw New MatrixCompatibilityException("Matrix must be square to have a determinant")
        Else
            Try
                Dim L As New Matrix(Me.Row)
                Dim U As New Matrix(Me.Row)

                'This solves the equation M = LU
                'sets the elements that have a value of 0 or 1 in the L and U matrices
                For r As Integer = 1 To Me.Row
                    For c As Integer = 1 To Me.Column
                        Select Case r
                            Case Is = c
                                L(r, c) = 1
                            Case Is > c
                                L(r, c) = 0
                            Case Is < c
                                U(r, c) = 0
                        End Select
                    Next
                Next

                'finds the remaining values in the matrix one row at a time
                For n As Integer = 1 To Me.Row

                    'finds the values in the nth row of U
                    For x As Integer = n To Row

                        'sets the element in U to the corresponding element in A
                        U(n, x) = Me(n, x)

                        'removes the contribution to the element by the other elements in L and U
                        For i As Integer = 1 To (n - 1)
                            U(n, x) = U(n, x) - U(i, x) * L(n, i)
                        Next
                    Next

                    'finds the value in the nth column of L
                    For x As Integer = n + 1 To Me.Row

                        'sets the element in L to the corresponding element in A
                        L(x, n) = Me(x, n)

                        'removes the contribution to the element by the other elements in L and U
                        For i As Integer = 1 To (n - 1)
                            L(x, n) = L(x, n) - U(i, n) * L(x, i)
                        Next

                        'divides by the corresponding value along the leading diagonal of U
                        L(x, n) = L(x, n) / U(n, n)
                    Next
                Next

                'finds the product of all the values along the leading diagonal of U, which is the determinant
                Determinant = 1

                For n As Integer = 1 To Me.Row
                    Determinant = Determinant * U(n, n)
                Next

            Catch ex As System.DivideByZeroException
                'div by zero error only occurs when a value along the leading diagonal of u is 0 thus the determinant must be 0 anyway
                Return 0
            End Try
        End If
    End Function

    ''' <summary>
    ''' Uses Gaussian Elimination to find the inverse of a matrix
    ''' </summary>
    Function Inverse() As Matrix
        If Determinant() = 0 Or Me.Row <> Me.Column Then
            Throw New NoInverseException("Matrix has no inverse")
        Else
            'Inverse initializes to the identity matrix of the same size as M
            'Creates a copy of the current matrix which can be manipulated
            Dim M As New Matrix(Me.e)
            Inverse = Matrix.Identity(Me.Row)


            'uses EROs  to reduce all the elements below the current value in the leading diagonal to 0
            For n As Integer = 1 To Me.Row

                'finds the factor to reduce the rows by
                Dim Factor As Decimal = M(n, n)

                'reduce rows of matrix and inverse
                For i As Integer = 1 To Me.Row
                    M(n, i) = M(n, i) / Factor
                    Inverse.Element(n, i) = Inverse.Element(n, i) / Factor
                Next

                'cancels down values beneath the leading diagonal to 0
                For r As Integer = n + 1 To Me.Row

                    'finds the factor to reduce the rows by
                    Dim k As Decimal = M(r, n)

                    'reduce rows of matrix and inverse
                    For x As Integer = 1 To Row
                        M(r, x) = M(r, x) - k * M(n, x)
                        Inverse.Element(r, x) = Inverse.Element(r, x) - k * Inverse.Element(n, x)
                    Next

                Next
            Next

            'Uses back substitution to reduce the remaining elements above the leading diagonal to 0
            For r As Integer = Me.Row - 1 To 1 Step -1
                For c As Integer = (r + 1) To Me.Row

                    'finds the factor to reduce the rows by
                    Dim k As Decimal = M(r, c)

                    'reduce rows of matrix and inverse
                    For x As Integer = 1 To Row
                        Dim i As Decimal = M(c, x)

                        M(r, x) = M(r, x) - k * i
                        Inverse.Element(r, x) = Inverse.Element(r, x) - k * Inverse.Element(c, x)
                    Next

                Next
            Next

        End If
    End Function

    ''' <summary>
    ''' Returns the current matrix in reduced row echelon form
    ''' </summary>
    Function RowEchelonForm() As Matrix
        Dim r As Integer = 1
        Dim c As Integer = 0
        Dim M As Matrix = Me.Clone
        Dim Reduced As Boolean = True
        Dim pivot As Integer

        'repeat until pointer reaches the bottom or right of the matrix
        Do Until c = Column Or r > Row
            Reduced = True
            c = c + 1

            'finds pivot row
            For i As Integer = r To Row
                If Math.Abs(M(i, c)) <> 0 Then
                    Reduced = False
                    pivot = i
                    Exit For
                End If
            Next

            'if there is a row to pivot, pivot row and cancel rows below pivot row
            If Reduced = False Then
                Dim k As Decimal

                'swaps pivot row and current row
                For i As Integer = 1 To Column
                    AddFunc.Swap(M(r, i), M(pivot, i))
                Next

                'cancels rows below pivot row
                For i As Integer = r + 1 To Row
                    k = M(i, c) / M(r, c)


                    For j As Integer = 1 To Column
                        M(i, j) = M(i, j) - k * M(r, j)
                    Next
                Next

                k = M(r, c)

                'reduce current row
                For i As Integer = 1 To Column
                    M(r, i) = M(r, i) / k
                Next

                r = r + 1
            End If
        Loop
        Return M
    End Function

    ''' <summary>
    ''' Returns the rank of the current matrix
    ''' </summary>
    Function Rank() As Integer
        Dim M As Matrix = RowEchelonForm()

        'returns the index of the first row with a non-zero value
        For i As Integer = M.Row To 1 Step -1
            For c As Integer = 1 To M.Column
                If Math.Abs(M(i, c)) <> 0 Then
                    Return i
                End If
            Next
        Next

        'if no value is found then return 0
        Return 0
    End Function
End Class