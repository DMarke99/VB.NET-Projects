''' <summary>
''' Contains statistical functions that take datasets as parameters
''' </summary>
''' <remarks></remarks>
Public Class Statistics

    'Simple Statistics

    ''' <summary>
    ''' Gets the mean of all the columns in a dataset
    ''' </summary>
    ''' <param name="D">The dataset on which the means are found</param>
    Public Shared Function Mean(ByVal D As Dataset) As Decimal()
        Dim Means(D.Column - 1) As Decimal

        'gets data
        Dim Data(,) As Decimal = D.getData

        'adds all the values in a column, then divides by the number of terms
        For c As Integer = 0 To D.Column - 1
            For r As Integer = 0 To D.Row - 1
                Means(c) = Means(c) + Data(r, c)
            Next
            Means(c) = Means(c) / D.Row
        Next

        Return Means
    End Function

    ''' <summary>
    ''' Gets the sum of squares of all the columns in a dataset
    ''' </summary>
    ''' <param name="D">The dataset on which the means are found</param>
    Public Shared Function SumOfSquares(ByVal D As Dataset) As Decimal()
        Dim SS(D.Column - 1) As Decimal

        'gets data
        Dim Data(,) As Decimal = D.getData

        'adds the squares of all the values in a column
        For r As Integer = 0 To D.Row - 1
            For c As Integer = 0 To D.Column - 1
                SS(c) = SS(c) + Data(r, c) ^ 2
            Next
        Next

        Return SS
    End Function

    ''' <summary>
    ''' Gets the product sum of all combinations of columns in a dataset
    ''' </summary>
    ''' <param name="D">The dataset on which the means are found</param>
    Public Shared Function SumOfProducts(ByVal D As Dataset) As Decimal(,)
        Dim SumProduct(D.Column - 1, D.Column - 1) As Decimal

        'imports the data into a matrix
        Dim M As New Matrix(D.getData)

        'the matrix's transpose multiplied by the original matrix adds the product of corresponding values in the original matrix's columns
        'the result of this is a matrix of sums of products
        Dim SumProds As Matrix = M.Transpose * M

        'transfers values back to an array
        For x As Integer = 1 To D.Column
            For y As Integer = 1 To D.Column
                SumProduct(x - 1, y - 1) = SumProds(x, y)
            Next
        Next

        Return SumProduct
    End Function

    ''' <summary>
    ''' Gets the variance of all the columns in a dataset
    ''' </summary>
    ''' <param name="D">The dataset on which the means are found</param>
    Public Shared Function Variance(ByVal D As Dataset) As Decimal()
        Dim Var(D.Column - 1) As Decimal

        'gets mean and sum of squares
        Dim Means() As Decimal = Mean(D)
        Dim SS() As Decimal = SumOfSquares(D)

        'gets n, the number of items in a row
        Dim n As Integer = D.Row

        'Variance is defined as (SumOfSquares - n * Mean ^ 2)/(n - 1)
        For i As Integer = 0 To D.Column - 1
            Var(i) = (SS(i) - n * Means(i) ^ 2) / (n - 1)
        Next

        Return Var
    End Function

    'Complex Statistics

    ''' <summary>
    ''' Gets the covariance of all combinations of columns in a dataset
    ''' </summary>
    ''' <param name="D">The dataset on which the means are found</param>
    Public Shared Function Covariance(ByVal D As Dataset) As Decimal(,)
        Dim Covar(D.Column - 1, D.Column - 1) As Decimal

        'gets mean and sum of products
        Dim Means() As Decimal = Mean(D)
        Dim SumProduct(,) As Decimal = SumOfProducts(D)

        'gets n, the number of items in a row
        Dim n As Integer = D.Row

        'Covariance of X and Y is defined as SumOfProducts(X,Y) / n - Mean X * Mean Y
        For x As Integer = 0 To D.Column - 1
            For y As Integer = 0 To D.Column - 1
                Covar(x, y) = SumProduct(x, y) / n - Means(x) * Means(y)
            Next
        Next

        Return Covar
    End Function

    ''' <summary>
    ''' Gets the pearsons moment correlation coefficient of all combinations of columns in a dataset
    ''' </summary>
    ''' <param name="D">The dataset on which the means are found</param>
    Public Shared Function PMCC(ByVal D As Dataset) As Decimal(,)
        Dim Pearsons(D.Column - 1, D.Column - 1) As Decimal

        'gets the covariance array
        Dim Covar(,) As Decimal = Covariance(D)

        'PMCC is defined as Covar(X,Y)/sqrt(Var(X)*Var(Y))
        'Covar (X, X) is the same as Var(X), which saves needing to import it
        For x As Integer = 0 To D.Column - 1
            For y As Integer = 0 To D.Column - 1
                Pearsons(x, y) = Covar(x, y) / Math.Sqrt(Covar(x, x) * Covar(y, y))
            Next
        Next

        Return Pearsons
    End Function

    ''' <summary>
    ''' Gets the spearman's rank coefficient correlation of all combinations of columns in a dataset
    ''' </summary>
    ''' <param name="D">The dataset on which the means are found</param>
    Public Shared Function Spearmans(ByVal D As Dataset) As Decimal(,)
        Dim SpearmansRank(D.Column - 1, D.Column - 1) As Decimal

        'gets the array of RANKED data
        Dim Data(,) As Decimal = D.getRankedData

        'calculates sum of square differences, and therefore rank coefficients
        'Spearmans = 1 - 6 * SumOfSquareDifference / (n * 3 - n)
        For x As Integer = 0 To D.Column - 1
            For y As Integer = 0 To D.Column - 1
                For i As Integer = 0 To D.Row - 1
                    SpearmansRank(x, y) = SpearmansRank(x, y) + (Data(i, x) - Data(i, y)) ^ 2
                Next

                SpearmansRank(x, y) = 1 - 6 * SpearmansRank(x, y) / (D.Row ^ 3 - D.Row)
            Next
        Next

        Return SpearmansRank
    End Function

End Class