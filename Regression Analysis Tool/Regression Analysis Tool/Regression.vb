Imports System.Text.RegularExpressions

''' <summary>
''' Contains all the types of regression
''' </summary>
Public Class Regression

    'Summary Statistics and Correlation Coefficients

    ''' <summary>
    ''' Returns summary statistics on the data
    ''' </summary>
    ''' <param name="D">The dataset on which the regression will be performed</param>
    ''' <remarks></remarks>
    Public Shared Sub StatisticsSummary(ByVal D As Dataset, ByVal dp As Integer, ByRef output_text_box As TextBox)
        'imports data
        Dim Data(,) As Decimal = D.getData
        Dim Headers() As String = D.getHeaders
        Dim Mean() As Decimal = Statistics.Mean(D)
        Dim Variance() As Decimal = Statistics.Variance(D)
        Dim MaxHeaderLength As Integer = D.getMaxHeaderLength
        Dim ColumnWidth As Integer = 10 + dp

        'summary statistics
        'formats such that each string in the same column is aligned
        output_text_box.AppendText("Summary Stats" & Environment.NewLine)
        output_text_box.AppendText(Space(MaxHeaderLength + 2) & "Mean" & Space(ColumnWidth - 2) & "Variance" & Environment.NewLine)

        'outputs means and variances
        For i As Integer = 0 To D.Column - 1
            Dim curr_header As String = Headers(i)
            Dim curr_mean As String = Convert.ToString(Mean(i)).Substring(0, Math.Min(10 + dp, Convert.ToString(Mean(i)).Length)).Trim(".")
            Dim curr_var As String = Convert.ToString(Variance(i)).Substring(0, Math.Min(10 + dp, Convert.ToString(Variance(i)).Length)).Trim(".")

            curr_mean = AddFunc.StandardForm(Mean(i), dp + 1)
            curr_var = AddFunc.StandardForm(Variance(i), dp + 1)

            output_text_box.AppendText(Headers(i) & Space(MaxHeaderLength - Headers(i).Length + 2) & _
                                       curr_mean & _
                                       Space(Math.Max(ColumnWidth + 2 - curr_mean.Length, 2)) & _
                                       curr_var & _
                                       Space(Math.Max(ColumnWidth + 2 - curr_var.Length, 2)) & _
                                       Environment.NewLine)
        Next

        output_text_box.AppendText(Environment.NewLine)
    End Sub

    ''' <summary>
    ''' Returns Spearmans rank correlation coefficients for on the data
    ''' </summary>
    ''' <param name="D">The dataset on which the regression will be performed</param>
    Public Shared Sub SpearmansRankSummary(ByVal D As Dataset, ByVal dp As Integer, ByRef output_text_box As TextBox)
        Dim SpearmansRank(,) As Decimal = Statistics.Spearmans(D)
        Dim Headers() As String = D.getHeaders
        Dim MaxHeaderLength As Integer = D.getMaxHeaderLength
        Dim ColumnWidth As Integer = Math.Max(2 + dp, MaxHeaderLength)

        'outputs spearman's rank coefficients for all combinations of the table
        'formats such that each string in the same column is aligned
        output_text_box.AppendText("Spearman's Rank Correlation Coefficients" & Environment.NewLine)
        output_text_box.AppendText(Space(MaxHeaderLength + 2))

        'outputs headers
        For i As Integer = 0 To D.Column - 1
            output_text_box.AppendText(Headers(i) & Space(ColumnWidth - Headers(i).Length + 2))
        Next

        output_text_box.AppendText(Environment.NewLine)
        Dim curr_coefficient As String

        'outputs and formats coefficients
        For i As Integer = 0 To D.Column - 1
            output_text_box.AppendText(Headers(i) & Space(MaxHeaderLength - Headers(i).Length + 2))

            For j As Integer = 0 To D.Column - 1

                If i = j Then
                    curr_coefficient = "/"
                Else
                    curr_coefficient = SpearmansRank(i, j).ToString("F" & dp.ToString)
                End If
                output_text_box.AppendText(curr_coefficient & Space(ColumnWidth - curr_coefficient.Length + 2))
            Next

            output_text_box.AppendText(Environment.NewLine)
        Next

        output_text_box.AppendText(Environment.NewLine)
    End Sub

    ''' <summary>
    ''' Returns PMCC for on the data
    ''' </summary>
    ''' <param name="D">The dataset on which the regression will be performed</param>
    Public Shared Sub PMCCSummary(ByVal D As Dataset, ByVal dp As Integer, ByRef output_text_box As TextBox)
        Dim PMCC(,) As Decimal = Statistics.PMCC(D)
        Dim Headers() As String = D.getHeaders
        Dim MaxHeaderLength As Integer = D.getMaxHeaderLength
        Dim ColumnWidth As Integer = Math.Max(2 + dp, MaxHeaderLength)

        'outputs PMCCs for all combinations of columns of the table
        'formats such that each string in the same column is aligned
        output_text_box.AppendText("Product Moment Correlation Coefficients" & Environment.NewLine)
        output_text_box.AppendText(Space(MaxHeaderLength + 2))

        'prints headers
        For i As Integer = 0 To D.Column - 1
            output_text_box.AppendText(Headers(i) & Space(ColumnWidth - Headers(i).Length + 2))
        Next

        output_text_box.AppendText(Environment.NewLine)
        Dim curr_coefficient As String

        'formats and prints coefficients
        For i As Integer = 0 To D.Column - 1
            output_text_box.AppendText(Headers(i) & Space(MaxHeaderLength - Headers(i).Length + 2))

            For j As Integer = 0 To D.Column - 1

                If i = j Then
                    curr_coefficient = "/"
                Else
                    curr_coefficient = PMCC(i, j).ToString("F" & dp.ToString)
                End If

                output_text_box.AppendText(curr_coefficient & Space(ColumnWidth - curr_coefficient.Length + 2))
            Next

            output_text_box.AppendText(Environment.NewLine)
        Next

        output_text_box.AppendText(Environment.NewLine)
    End Sub

    'Multiple Linear

    ''' <summary>
    ''' Perfoms a Multiple Linear Regression on a dataset, and returns summary statistics on the data
    ''' </summary>
    ''' <param name="D">The dataset on which the regression will be performed</param>
    ''' <param name="dependent">The index of the column of dependent variables</param>
    ''' <param name="ForceThroughOrigin">A boolean which decidess whether the line is forced through the origin</param>
    ''' <remarks></remarks>
    Public Shared Sub MultipleLinearSummary(ByVal D As Dataset, ByVal dependent As Integer, ByVal ForceThroughOrigin As Boolean, ByRef output_text_box As TextBox)
        'imports data into X matrix
        Dim X As New Matrix(D.getData)
        Dim Y As New Matrix(X.Row, 1)
        Dim headers() As String = D.getHeaders

        If D.Row < D.Column Then
            MessageBox.Show("Results may be inaccurate due to insufficient data entries")
        End If

        'imports values into Y
        For i As Integer = 1 To X.Row
            Y.Element(i, 1) = X.Element(i, dependent)
        Next

        'shifts all values to the left of the transferred column to the right by a column
        For i As Integer = dependent - 1 To 1 Step -1
            For j As Integer = 1 To X.Row
                X.Element(j, i + 1) = X.Element(j, i)
            Next
        Next

        'fills the first column with ones
        For j As Integer = 1 To X.Row
            X.Element(j, 1) = 1
        Next

        'if forced through the origin remove the first column
        If ForceThroughOrigin Then
            Dim temp As Matrix = X
            X.Column = X.Column - 1

            For r As Integer = 1 To X.Row
                For c As Integer = 1 To X.Column
                    X.Element(r, c) = temp.Element(r, c + 1)
                Next
            Next
        End If

        'calculates the matrix of coefficients, b, via a matrix method
        Dim b As New Matrix()
        b = (X.Transpose * X).Inverse * (X.Transpose * Y)

        'summary statistics
        output_text_box.AppendText("Summary Stats on Linear Regression on " & Convert.ToString(headers(dependent - 1)) & Environment.NewLine)
        output_text_box.AppendText("Equation is of form: y = c + m1 * x1 + m2 * x2 + ..." & Environment.NewLine)
        output_text_box.AppendText(Environment.NewLine)
        output_text_box.AppendText("Coefficients and Constants" & Environment.NewLine)
        If ForceThroughOrigin Then
            'prints the coefficient and the variable it corresponds to
            For i As Integer = 1 To X.Column
                If i >= dependent Then
                    output_text_box.AppendText(headers(i) & ": " & AddFunc.SigFigs(b.Element(i, 1), 4) & Environment.NewLine)
                Else
                    output_text_box.AppendText(headers(i - 1) & ": " & AddFunc.SigFigs(b.Element(i, 1), 4) & Environment.NewLine)
                End If
            Next
        Else
            output_text_box.AppendText("c: " & AddFunc.SigFigs(b.Element(1, 1), 4) & Environment.NewLine)

            'prints the coefficient and the variable it corresponds to
            For i As Integer = 1 To X.Column - 1
                If i >= dependent Then
                    output_text_box.AppendText(headers(i) & ": " & AddFunc.SigFigs(b.Element(i + 1, 1), 4) & Environment.NewLine)
                Else
                    output_text_box.AppendText(headers(i - 1) & ": " & AddFunc.SigFigs(b.Element(i + 1, 1), 4) & Environment.NewLine)
                End If
            Next

        End If
        output_text_box.AppendText(Environment.NewLine)
    End Sub

    ''' <summary>
    ''' Perfoms a Multiple Linear Regression on a dataset, and returns the Residual Sum of Squares
    ''' </summary>
    ''' <param name="D">The dataset on which the regression will be performed</param>
    ''' <param name="dependent">The index of the column of dependent variables</param>
    ''' <param name="ForceThroughOrigin">A boolean which decidess whether the line is forced through the origin</param>
    ''' <remarks></remarks>
    Public Shared Function MultipleLinearRSS(ByVal D As Dataset, ByVal dependent As Integer, ByVal ForceThroughOrigin As Boolean) As Decimal
        'imports data into X matrix
        Dim X As New Matrix(D.getData)
        Dim Y As New Matrix(X.Row, 1)

        'imports values into Y
        For i As Integer = 1 To X.Row
            Y.Element(i, 1) = X.Element(i, dependent)
        Next

        'shifts all values to the left of the transferred column to the right by a column
        For i As Integer = dependent - 1 To 1 Step -1
            For j As Integer = 1 To X.Row
                X.Element(j, i + 1) = X.Element(j, i)
            Next
        Next

        'fills the first column with ones
        For j As Integer = 1 To X.Row
            X.Element(j, 1) = 1
        Next

        'if forced through the origin remove the first column
        If ForceThroughOrigin Then
            Dim temp As Matrix = X
            X.Column = X.Column - 1

            For r As Integer = 1 To X.Row
                For c As Integer = 1 To X.Column
                    X.Element(r, c) = temp.Element(r, c + 1)
                Next
            Next
        End If

        'calculates the matrix of coefficients, b, via a matrix method
        Dim b As New Matrix()
        b = (X.Transpose * X).Inverse * (X.Transpose * Y)

        'calculates a matrix of residuals
        Dim Residual As Matrix = Y - X * b

        'finds the sum of square residuals
        Dim RSS As Decimal = (Residual.Transpose * Residual)(1, 1)
        Return RSS
    End Function

    'Multiplicative

    ''' <summary>
    ''' Perfoms a Multiplicative Regression on a dataset
    ''' </summary>
    ''' <param name="D">The dataset on which the regression will be performed</param>
    ''' <param name="dependent">The index of the column of dependent variables</param>
    Public Shared Sub MultiplicativeRegressionSummary(ByVal D As Dataset, ByVal dependent As Integer, ByRef output_text_box As TextBox)
        'imports data into X matrix
        Dim X As New Matrix(D.getData)
        Dim Y As New Matrix(X.Row, 1)
        Dim headers() As String = D.getHeaders

        If D.Row < D.Column Then
            MessageBox.Show("Results may be inaccurate due to insufficient data entries")
        End If

        'creates matrix to backup Y values and to indicate where values of zero occur
        Dim Y_backup As New Matrix(X.Row, 1)

        'imports values into Y
        For i As Integer = 1 To X.Row
            Y_backup.Element(i, 1) = X.Element(i, dependent)
        Next

        'shifts all values to the left of the transferred column to the right by a column
        For i As Integer = dependent - 1 To 1 Step -1
            For j As Integer = 1 To X.Row
                X.Element(j, i + 1) = X.Element(j, i)
            Next
        Next

        'omits all rows with values which are 0
        Dim curr_row As Integer = 1

        Do While curr_row <= X.Row
            For i As Integer = 1 To X.Column
                If X(curr_row, i) = 0 Or Y_backup(curr_row, 1) = 0 Then
                    X.RemoveRow(curr_row)
                    Y.RemoveRow(curr_row)
                    Y_backup.RemoveRow(curr_row)
                    curr_row = curr_row - 1
                    Exit For
                End If
            Next
            curr_row = curr_row + 1
        Loop

        'transforms all the X co-ordinates
        For i As Integer = 2 To X.Column
            For j As Integer = 1 To X.Row
                X.Element(j, i) = Math.Log(Math.Abs(X.Element(j, i)))
            Next
        Next

        'imports values into Y
        For i As Integer = 1 To X.Row
            Y.Element(i, 1) = Math.Log(Math.Abs(Y_backup.Element(i, 1)))
        Next

        'fills the first column with ones
        For j As Integer = 1 To X.Row
            X.Element(j, 1) = 1
        Next

        'calculates the matrix of coefficients, b, via a matrix method
        Dim b As New Matrix()
        b = (X.Transpose * X).Inverse * (X.Transpose * Y)

        'calculates predicted values
        Dim Predicted As Matrix = X * b

        'transforms the values
        For r As Integer = 1 To Predicted.Row
            Predicted(r, 1) = Math.Exp(Predicted(r, 1))
        Next

        'calculates a matrix of residuals
        Dim Residual As Matrix = Y_backup - Predicted

        'finds the sum of square residuals
        Dim RSS As Decimal = (Residual.Transpose * Residual)(1, 1)

        'summary statistics
        output_text_box.AppendText("Summary Stats on Multiplicative Regression on " & headers(dependent - 1) & Environment.NewLine)
        output_text_box.AppendText("Equation is of form: y = k * x1 ^ m1 * x2 ^ m2 * ..." & Environment.NewLine)
        output_text_box.AppendText(Environment.NewLine)
        output_text_box.AppendText("Exponents and Constants" & Environment.NewLine)
        output_text_box.AppendText("k: " & AddFunc.StandardForm(Math.Exp(b.Element(1, 1)), 4) & Environment.NewLine)

        'prints the coefficient and the variable it corresponds to
        For i As Integer = 1 To X.Column - 1
            If i >= dependent Then
                output_text_box.AppendText(headers(i) & ": " & AddFunc.SigFigs(b.Element(i + 1, 1), 4) & Environment.NewLine)
            Else
                output_text_box.AppendText(headers(i - 1) & ": " & AddFunc.SigFigs(b.Element(i + 1, 1), 4) & Environment.NewLine)
            End If
        Next
        output_text_box.AppendText(Environment.NewLine)
    End Sub

    ''' <summary>
    ''' Perfoms a Multiplicative Regression on a dataset, and returns the Residual Sum of Squares
    ''' </summary>
    ''' <param name="D">The dataset on which the regression will be performed</param>
    ''' <param name="dependent">The index of the column of dependent variables</param>
    Public Shared Function MultiplicativeRegressionRSS(ByVal D As Dataset, ByVal dependent As Integer) As Decimal
        'imports data into X matrix
        Dim X As New Matrix(D.getData)
        Dim Y As New Matrix(X.Row, 1)

        'creates matrix to backup Y values and to indicate where values of zero occur
        Dim Y_backup As New Matrix(X.Row, 1)

        'imports values into Y
        For i As Integer = 1 To X.Row
            Y_backup.Element(i, 1) = X.Element(i, dependent)
        Next

        'shifts all values to the left of the transferred column to the right by a column
        For i As Integer = dependent - 1 To 1 Step -1
            For j As Integer = 1 To X.Row
                X.Element(j, i + 1) = X.Element(j, i)
            Next
        Next

        'omits all rows with values which are 0
        Dim curr_row As Integer = 1

        Do While curr_row <= X.Row
            For i As Integer = 1 To X.Column
                If X(curr_row, i) = 0 Or Y_backup(curr_row, 1) = 0 Then
                    X.RemoveRow(curr_row)
                    Y.RemoveRow(curr_row)
                    Y_backup.RemoveRow(curr_row)
                    curr_row = curr_row - 1
                    Exit For
                End If
            Next
            curr_row = curr_row + 1
        Loop

        'transforms all the X co-ordinates
        For i As Integer = 2 To X.Column
            For j As Integer = 1 To X.Row
                X.Element(j, i) = Math.Log(Math.Abs(X.Element(j, i)))
            Next
        Next

        'imports values into Y
        For i As Integer = 1 To X.Row
            Y.Element(i, 1) = Math.Log(Math.Abs(Y_backup.Element(i, 1)))
        Next

        'fills the first column with ones
        For j As Integer = 1 To X.Row
            X.Element(j, 1) = 1
        Next

        'calculates the matrix of coefficients, b, via a matrix method
        Dim b As New Matrix()
        b = (X.Transpose * X).Inverse * (X.Transpose * Y)

        'calculates predicted values
        Dim Predicted As Matrix = X * b

        'transforms the values
        For r As Integer = 1 To Predicted.Row
            Predicted(r, 1) = Math.Exp(Predicted(r, 1))
            Y_backup(r, 1) = Math.Abs(Y_backup(r, 1))
        Next

        'calculates a matrix of residuals
        Dim Residual As Matrix = Y_backup - Predicted

        'finds the sum of square residuals
        Dim RSS As Decimal = (Residual.Transpose * Residual)(1, 1)
        Return RSS

    End Function

    'Single Polynomial (and thus Linear)

    ''' <summary>
    ''' Performs a single polynomial regression on a dataset
    ''' </summary>
    ''' <param name="D">The dataset on which the regression is being performed</param>
    ''' <param name="dependent">The index of the column of the dependent variable</param>
    ''' <param name="independent">The index of the column of the independent variable</param>
    ''' <param name="n">The degree of the resultant polynomial expression</param>
    Public Shared Sub SinglePolynomialSummary(ByVal D As Dataset, ByVal dependent As Integer, ByVal independent As Integer, ByVal n As Integer, ByRef output_text_box As TextBox)
        'imports data into X matrix
        Dim X As New Matrix(D.Row, n + 1)
        Dim Y As New Matrix(X.Row, 1)
        Dim headers() As String = D.getHeaders

        If D.Row < n + 1 Then
            MessageBox.Show("Results may be inaccurate due to insufficient data entries")
        End If

        Dim data(,) As Decimal = D.getData

        'imports values into X and Y
        'the third column of X is the values in the second column squared
        For i As Integer = 1 To X.Row
            For p As Integer = 0 To n
                X.Element(i, p + 1) = data(i - 1, independent - 1) ^ p
            Next
            Y.Element(i, 1) = data(i - 1, dependent - 1)
        Next

        'calculates the matrix of coefficients, b, via a matrix method
        Dim b As New Matrix()
        b = (X.Transpose * X).Inverse * (X.Transpose * Y)

        'summary statistics
        output_text_box.AppendText("Summary Stats on Polynomial Regression degree " & n & " on " & headers(dependent - 1) & " to " & headers(independent - 1) & Environment.NewLine)
        output_text_box.AppendText("Equation is of form: y = c + m1 * x^1 + m2 * x^2 + m3 * x^3 + ..." & Environment.NewLine)
        output_text_box.AppendText(Environment.NewLine)
        output_text_box.AppendText("Coefficients and Constants" & Environment.NewLine)
        output_text_box.AppendText("c: " & AddFunc.SigFigs(b.Element(1, 1), 4) & Environment.NewLine)

        'prints the coefficient and the variable it corresponds to
        For i As Integer = 2 To X.Column
            output_text_box.AppendText("x^" & i - 1 & ": " & AddFunc.SigFigs(b.Element(i, 1), 4) & Environment.NewLine)
        Next

        output_text_box.AppendText(Environment.NewLine)
    End Sub

    ''' <summary>
    ''' Performs a single polynomial regression on a dataset, and returns the Residual Sum of Squares
    ''' </summary>
    ''' <param name="D">The dataset on which the regression is being performed</param>
    ''' <param name="dependent">The index of the column of the dependent variable</param>
    ''' <param name="independent">The index of the column of the independent variable</param>
    ''' <param name="n">The degree of the resultant polynomial expression</param>
    Public Shared Function SinglePolynomialRSS(ByVal D As Dataset, ByVal dependent As Integer, ByVal independent As Integer, ByVal n As Integer) As Decimal
        'imports data into X matrix
        Dim X As New Matrix(D.Row, n + 1)
        Dim Y As New Matrix(X.Row, 1)

        Dim data(,) As Decimal = D.getData

        'imports values into X and Y
        'the third column of X is the values in the second column squared
        For i As Integer = 1 To X.Row
            For p As Integer = 0 To n
                X.Element(i, p + 1) = data(i - 1, independent - 1) ^ p
            Next
            Y.Element(i, 1) = data(i - 1, dependent - 1)
        Next

        'calculates the matrix of coefficients, b, via a matrix method
        Dim b As New Matrix()
        b = (X.Transpose * X).Inverse * (X.Transpose * Y)

        'calculates a matrix of residuals
        Dim R As Matrix = Y - X * b

        'finds the sum of square residuals
        Dim RSS As Decimal = (R.Transpose * R)(1, 1)
        Return RSS
    End Function

    ''' <summary>
    ''' Performs a single polynomial regression on a dataset, and returns the expression for the curve
    ''' </summary>
    ''' <param name="D">The dataset on which the regression is being performed</param>
    ''' <param name="dependent">The index of the column of the dependent variable</param>
    ''' <param name="independent">The index of the column of the independent variable</param>
    ''' <param name="n">The degree of the resultant polynomial expression</param>
    Public Shared Function SinglePolynomialExpression(ByVal D As Dataset, ByVal dependent As Integer, ByVal independent As Integer, ByVal n As Integer) As Expression
        'imports data into X matrix
        Dim X As New Matrix(D.Row, n + 1)
        Dim Y As New Matrix(X.Row, 1)

        Dim data(,) As Decimal = D.getData

        If D.Row < n + 1 Then
            MessageBox.Show("Results may be inaccurate due to insufficient data entries")
        End If

        'imports values into X and Y
        'the third column of X is the values in the second column squared
        For i As Integer = 1 To X.Row
            For p As Integer = 0 To n
                X.Element(i, p + 1) = data(i - 1, independent - 1) ^ p
            Next
            Y.Element(i, 1) = data(i - 1, dependent - 1)
        Next

        'fills the first column with ones


        'calculates the matrix of coefficients, b, via a matrix method
        Dim b As New Matrix()
        b = (X.Transpose * X).Inverse * (X.Transpose * Y)

        'constructs equation
        Dim LineOfBestFit As New Expression()

        Dim tempstring As String = b(1, 1)
        Dim nextnumber As String

        For i As Integer = 1 To n
            nextnumber = b(i + 1, 1)
            tempstring = tempstring + "+" + nextnumber + "* x ^ " + Convert.ToString(i)
        Next

        LineOfBestFit = tempstring

        Return LineOfBestFit
    End Function

    ''' <summary>
    ''' Performs a single linear regression on a dataset
    ''' </summary>
    ''' <param name="D">The dataset on which the regression is being performed</param>
    ''' <param name="dependent">The index of the column of the dependent variable</param>
    ''' <param name="independent">The index of the column of the independent variable</param>
    Public Shared Sub SingleLinearSummary(ByVal D As Dataset, ByVal dependent As Integer, ByVal independent As Integer, ByRef output_text_box As TextBox)
        'imports data into X matrix
        Dim X As New Matrix(D.Row, 2)
        Dim Y As New Matrix(X.Row, 1)
        Dim headers() As String = D.getHeaders
        Dim data(,) As Decimal = D.getData

        If D.Row < 2 Then
            MessageBox.Show("Results may be inaccurate due to insufficient data entries")
        End If

        'imports values into X and Y
        For i As Integer = 1 To X.Row
            For p As Integer = 0 To 1
                X.Element(i, p + 1) = data(i - 1, independent - 1) ^ p
            Next
            Y.Element(i, 1) = data(i - 1, dependent - 1)
        Next

        'fills the first column with ones


        'calculates the matrix of coefficients, b, via a matrix method
        Dim b As New Matrix()
        b = (X.Transpose * X).Inverse * (X.Transpose * Y)

        'summary statistics
        output_text_box.AppendText("Summary Stats of Linear Regression on " & headers(dependent - 1) & " to " & headers(independent - 1) & Environment.NewLine)
        output_text_box.AppendText("Equation is of form: y = c + m * x" & Environment.NewLine)
        output_text_box.AppendText("Coefficients and Constants" & Environment.NewLine)
        output_text_box.AppendText("c: " & AddFunc.SigFigs(b.Element(1, 1), 4) & Environment.NewLine)

        'prints the coefficient and the variable it corresponds to
        For i As Integer = 2 To X.Column
            output_text_box.AppendText("x: " & AddFunc.SigFigs(b.Element(i, 1), 4) & Environment.NewLine)
        Next

        output_text_box.AppendText(Environment.NewLine)
    End Sub

    'Automatic Analysis

    ''' <summary>
    ''' Performs an analysis on the dataset to determine which regression is most optimal
    ''' </summary>
    ''' <param name="D">The dataset on which the regression is being performed</param>
    ''' <param name="dependent">The index of the column of the dependent variable</param>
    ''' <param name="maxExponent">The max exponent of the polynomial regressions</param>
    Public Shared Sub AutomaticAnalysis(ByVal D As Dataset, ByVal dependent As Integer, ByVal maxExponent As Integer, ByRef output_text_box As TextBox)
        Dim headers() As String = D.getHeaders
        Dim currentRSS As Decimal
        Dim RegressionAndRSS As New Dictionary(Of String, Decimal)

        output_text_box.AppendText("Automatic Analysis on " & headers(dependent - 1) & Environment.NewLine)
        output_text_box.AppendText(Environment.NewLine)
        output_text_box.AppendText("Residual Sum of Squares for each regression:" & Environment.NewLine)

        'Tries to perform each regression, and returns an error when not possible
        'If possible add the regression and RSS to a dictionary

        'Multiple Linear Regression

        Try
            currentRSS = MultipleLinearRSS(D, dependent, False)
            output_text_box.AppendText("Multiple Linear: " & AddFunc.StandardForm(currentRSS) & Environment.NewLine)
            RegressionAndRSS.Add("Multiple Linear", currentRSS)
        Catch ex As Exception
            output_text_box.AppendText("An error occured while attempting to perform a Multiple Regression" & Environment.NewLine)
        End Try

        'Single Linear Regression

        For i As Integer = 1 To D.Column
            If i = dependent Then Continue For
            Try
                currentRSS = SinglePolynomialRSS(D, dependent, i, 1)
                output_text_box.AppendText("Linear Regression vs " & headers(i - 1) & ": " & AddFunc.StandardForm(currentRSS) & Environment.NewLine)
                RegressionAndRSS.Add("Linear Regression vs " & headers(i - 1), currentRSS)
            Catch ex As Exception
                output_text_box.AppendText("An error occured while attempting to perform a Linear Regression vs " & headers(i - 1) & Environment.NewLine)
            End Try
        Next

        'Multiple Regression

        Try
            currentRSS = MultiplicativeRegressionRSS(D, dependent)
            output_text_box.AppendText("Multiplicative: " & AddFunc.StandardForm(currentRSS) & Environment.NewLine)
            RegressionAndRSS.Add("Multiplicative", currentRSS)
        Catch ex As Exception
            output_text_box.AppendText("An error occured while attempting to perform a Multiplicative Regression" & Environment.NewLine)
        End Try

        'Polynomial Regression

        For i As Integer = 1 To D.Column
            If i = dependent Then Continue For
            Try
                currentRSS = SinglePolynomialRSS(D, dependent, i, Math.Min(maxExponent, D.Row - 1))
                output_text_box.AppendText("Polynomial Regression degree " & maxExponent & " vs " & headers(i - 1) & ": " & AddFunc.StandardForm(currentRSS) & Environment.NewLine)
                RegressionAndRSS.Add("Polynomial Regression degree " & maxExponent & " vs " & headers(i - 1), currentRSS)
            Catch ex As Exception
                output_text_box.AppendText("An error occured while attempting to perform a Polynomial Regression vs " & headers(i - 1) & Environment.NewLine)
            End Try
        Next

        output_text_box.AppendText(Environment.NewLine)

        'gets the minimum RSS from the analysis
        Dim minRSS As Decimal = RegressionAndRSS.Min(Function(m As KeyValuePair(Of String, Decimal)) m.Value)

        'gets the regression that corresponds to the minimum RSS
        Dim minRegression As String

        For Each k As KeyValuePair(Of String, Decimal) In RegressionAndRSS
            If k.Value = minRSS Then
                minRegression = k.Key
                Exit For
            End If
        Next

        output_text_box.AppendText("The regression with the most association is " & minRegression & Environment.NewLine)
        output_text_box.AppendText(Environment.NewLine)
    End Sub

    'Independence

    ''' <summary>
    ''' Searches for linear dependence
    ''' </summary>
    ''' <param name="D">The dataset being checked for linear dependence</param>
    Public Shared Sub LinearDependenceSearch(ByVal D As Dataset, ByRef output_text_box As TextBox)
        Dim temp1 As New Matrix
        Dim temp2 As New Matrix
        Dim N As New Matrix(D.getData)
        Dim M As Matrix = N.Transpose
        Dim headers() = D.getHeaders
        Dim index As New List(Of Integer)
        Dim output As String

        'gets the rank of the matrix
        Dim rank As Integer = M.Rank

        'saves the indexes of the relevant headers
        For i As Integer = 0 To M.Row - 1
            index.Add(i)
        Next

        'keeps removing rows which mean that the rank stays the same
        temp1 = M.Clone

        Do Until temp1.Row = rank
            For i As Integer = temp1.Row To 1 Step -1
                temp2 = temp1.Clone
                temp2.RemoveRow(i)

                'if the rank is the same after the removal remove it
                If temp2.Rank = rank Then
                    index.RemoveAt(i - 1)
                    temp1 = temp2.Clone
                    Exit For
                End If
            Next
        Loop

        'outputs linear dependence search results
        output = "Linear Dependence Search" & Environment.NewLine & _
            "Every variable in the dataset can be expressed in terms of:" & Environment.NewLine

        For Each i As Integer In index
            output = output & headers(i) & Environment.NewLine
        Next

        output = output & Environment.NewLine
        output_text_box.AppendText(output)

    End Sub

    ''' <summary>
    ''' Searches for multiplicative dependence
    ''' </summary>
    ''' <param name="D">The dataset being checked for multiplicative dependence</param>
    Public Shared Sub MultiplicativeDependenceSearch(ByVal D As Dataset, ByRef output_text_box As TextBox)
        Dim temp1 As New Matrix
        Dim temp2 As New Matrix
        Dim N As New Matrix(D.getData)
        Dim M As Matrix = N.Transpose
        Dim headers() = D.getHeaders
        Dim index As New List(Of Integer)
        Dim output As String

        For i As Integer = 1 To M.Row
            For j As Integer = 1 To M.Column
                If M(i, j) = 0 Then
                    M.RemoveColumn(j)
                    j = j - 1
                    Exit For
                Else
                    M(i, j) = Math.Log(Math.Abs(M(i, j)))
                End If
            Next
        Next

        'gets the rank of the matrix
        Dim rank As Integer = M.Rank

        'saves the indexes of the relevant headers
        For i As Integer = 0 To M.Row - 1
            index.Add(i)
        Next

        'keeps removing rows which mean that the rank stays the same
        temp1 = M.Clone

        Do Until temp1.Row = rank
            For i As Integer = temp1.Row To 1 Step -1
                temp2 = temp1.Clone
                temp2.RemoveRow(i)

                'if the rank is the same after the removal remove it
                If temp2.Rank = rank Then
                    index.RemoveAt(i - 1)
                    temp1 = temp2.Clone
                    Exit For
                End If
            Next
        Loop

        'outputs multiplicative dependence search results
        output = "Multiplicative Dependence Search" & Environment.NewLine & _
            "Every variable in the dataset can be expressed in terms of:" & Environment.NewLine

        For Each i As Integer In index
            output = output & headers(i) & Environment.NewLine
        Next

        output = output & Environment.NewLine
        output_text_box.AppendText(output)

    End Sub

End Class