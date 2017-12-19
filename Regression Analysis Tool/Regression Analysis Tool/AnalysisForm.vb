Public Class AnalysisForm
    Dim D As New Dataset
    Property file_address As String
    Property file_name As String
    Property SummaryTextUndoStack As New Stack(Of String)
    Property SummaryTextRedoStack As New Stack(Of String)
    Property SummaryTextBoxTemp As String

    'Initialisation

    ''' <summary>
    ''' Creates an analysis form with no dataset
    ''' </summary>
    Sub New()
        InitializeComponent()
    End Sub

    ''' <summary>
    ''' Creates a new analysis form
    ''' </summary>
    ''' <param name="address">The address of the data to be analysed in the form</param>
    ''' <param name="containsHeaders">determines whether the dataset comes with headers</param>
    Sub New(ByVal address As String, ByVal containsHeaders As Boolean)
        InitializeComponent()
        D.Import(address, containsHeaders)
        file_address = address

        'gets filename
        file_name = file_address.Split("\")(file_address.Split("\").Length - 1)

        'sets form name
        Me.Text = "Analysis on " & file_name

        'gets data and headers
        Dim Data(,) As Decimal = D.getData
        Dim Headers() As String = D.getHeaders

        'sets the number of columns displayed to the number of columns
        For i As Integer = 0 To D.Column - 1
            csvViewer.Columns.Add(Headers(i), Headers(i))
        Next

        'sets the number of rows to the num
        Dim x As Integer = D.Row
        csvViewer.Rows.Add(x)

        'transfers values from the dataset to the csv Viewer
        For i As Integer = 0 To D.Row - 1
            For j As Integer = 0 To D.Column - 1
                csvViewer(j, i).Value = Data(i, j)
            Next
        Next

        'Disables the undo and redo buttons
        UndoToolStripMenuItem.Enabled = False
        RedoToolStripMenuItem.Enabled = False

        'Disables invalid commands for datasets with one or zero columns
        If D.Column < 2 Then
            AutomaticAnalysisToolStripMenuItem.Enabled = False
            PlotRegressionToolStripMenuItem.Enabled = False
            RegressionSummaryToolStripMenuItem.Enabled = False
            PMCCToolStripMenuItem.Enabled = False
            SpearmansRankToolStripMenuItem.Enabled = False
        End If

        'Disables invalid commands for datasets with less than two rows
        If D.Row = 1 Or D.Row = 0 Then
            MessageBox.Show("This dataset has one row, and so no useful information can be gathered from the data")
            Me.Close()
        End If

    End Sub

    'Graph Plotting

    ''' <summary>
    ''' Plots a linear regression line based on two columns of the dataset
    ''' </summary>
    Private Sub LinearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LinearToolStripMenuItem.Click
        Try
            'creates a linear graph object
            Dim P As New LinearGraph(D, _
                                               InputBox("Please input the index of the independent variable", "Input Independent", "1"), _
                                               InputBox("Please input the index of the dependent variable", "Input Dependent", "2"), Me)

            'plots the graph
            P.Plot()

            'hides the analysis form
            Hide()
        Catch ex As MatrixCompatibilityException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As NoInverseException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As System.OverflowException
            MessageBox.Show("These values are too large to compute with the given parameters")
        Catch ex As System.IndexOutOfRangeException
            MessageBox.Show("An index has been entered for a non-existent column")
        Catch ex As System.InvalidCastException
            'does nothing but exit after this exception
        Catch exception As ImproperGraphException
            MessageBox.Show("The graph couldn't be formed from the given points")
        End Try
    End Sub

    ''' <summary>
    ''' Plots a polynomial regression line based on two columns of the dataset
    ''' </summary>
    Private Sub PolynomialToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles PolynomialToolStripMenuItem.Click
        Try
            'creates a polynomial graph object
            Dim P As New PolynomialGraph(D, _
                                               InputBox("Please input the index of the independent variable", "Input Independent", "1"), _
                                               InputBox("Please input the index of the dependent variable", "Input Dependent", "2"), _
                                               InputBox("Please input the degree of the polynomial", "Input Degree", "1"), Me)
            'plots the graph
            P.Plot()

            'hides the analysis form
            Hide()
        Catch ex As MatrixCompatibilityException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As NoInverseException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As System.OverflowException
            MessageBox.Show("These values are too large to compute with the given parameters")
        Catch ex As System.IndexOutOfRangeException
            MessageBox.Show("An index has been entered for a non-existent column")
        Catch ex As System.InvalidCastException
            'does nothing but exit after this exception
        Catch exception As ImproperGraphException
            MessageBox.Show("The graph couldn't be formed from the given points")
        End Try
    End Sub

    ''' <summary>
    ''' Plots a graph with no line based on two columns of the dataset
    ''' </summary>
    Private Sub NoExpressionToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles NoExpressionToolStripMenuItem.Click
        Try
            'creates a scatter plot graph object
            Dim P As New NoLineGraph(D, _
                                               InputBox("Please input the index of the independent variable", "Input Independent", "1"), _
                                               InputBox("Please input the index of the dependent variable", "Input Dependent", "2"), Me)
            'plots the graph
            P.Plot()

            'hides the analysis form
            Hide()
        Catch ex As MatrixCompatibilityException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As NoInverseException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As System.OverflowException
            MessageBox.Show("These values are too large to compute with the given parameters")
        Catch ex As System.IndexOutOfRangeException
            MessageBox.Show("An index has been entered for a non-existent column")
        Catch ex As System.InvalidCastException
            'does nothing but exit after this exception
        Catch exception As ImproperGraphException
            MessageBox.Show("The graph couldn't be formed from the given points")
        End Try
    End Sub

    'General Summaries

    ''' <summary>
    ''' Returns the Spearman's Rank Coefficients for all combinations of pairs of columns
    ''' </summary>
    Private Sub SpearmansRankToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SpearmansRankToolStripMenuItem.Click
        Try
            'perfomrms a spearman's ranks on dataset and outputs results to SummaryTextbox
            Regression.SpearmansRankSummary(D, 4, SummaryTextbox)

            'indicates that the SummaryTextbox's content is changing
            TextboxContentChange()
        Catch ex As MatrixCompatibilityException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As NoInverseException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As System.OverflowException
            MessageBox.Show("These values are too large to compute with the given parameters")
        Catch ex As System.IndexOutOfRangeException
            MessageBox.Show("An index has been entered for a non-existent column")
        Catch ex As System.InvalidCastException
            'does nothing but cancel process after this exception
        End Try
    End Sub

    ''' <summary>
    ''' Returns the Product Moment Correlation Coefficients for all combinations of pairs of columns
    ''' </summary>
    Private Sub PMCCToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles PMCCToolStripMenuItem.Click
        Try
            'perfomrms a PMCC on dataset and outputs results to SummaryTextbox
            Regression.PMCCSummary(D, 4, SummaryTextbox)

            'indicates that the SummaryTextbox's content is changing
            TextboxContentChange()
        Catch ex As MatrixCompatibilityException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As NoInverseException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As System.OverflowException
            MessageBox.Show("These values are too large to compute with the given parameters")
        Catch ex As System.IndexOutOfRangeException
            MessageBox.Show("An index has been entered for a non-existent column")
        Catch ex As System.InvalidCastException
            'does nothing but cancel process after this exception
        End Try
    End Sub

    ''' <summary>
    ''' Returns the mean and variance of all columns
    ''' </summary>
    Private Sub SummaryStatisticsToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles SummaryStatisticsToolStripMenuItem.Click
        Try
            'perfomrms a statistical summary on dataset and outputs results to SummaryTextbox
            Regression.StatisticsSummary(D, 4, SummaryTextbox)

            'indicates that the SummaryTextbox's content is changing
            TextboxContentChange()
        Catch ex As MatrixCompatibilityException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As NoInverseException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As System.OverflowException
            MessageBox.Show("These values are too large to compute with the given parameters")
        Catch ex As System.IndexOutOfRangeException
            MessageBox.Show("An index has been entered for a non-existent column")
        Catch ex As System.InvalidCastException
            'does nothing but exit after this exception
        End Try
    End Sub

    'Regression Summaries

    ''' <summary>
    ''' Returns the summary of a Multiple Linear Regression on a dataset, based on one column as the dependent variable
    ''' </summary>
    Private Sub MultipleLinearToolStripMenuItem1_Click(sender As Object, e As EventArgs) Handles MultipleLinearToolStripMenuItem.Click
        Try
            'perfomrms a linear regression on dataset and outputs results to SummaryTextbox
            Regression.MultipleLinearSummary(D, InputBox("Please input the index of the dependent variable", "Input Dependent", "2"), False, SummaryTextbox)

            'indicates that the SummaryTextbox's content is changing
            TextboxContentChange()
        Catch ex As MatrixCompatibilityException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As NoInverseException
            MessageBox.Show("This Regression returns a matrix error. This could be because:" & Environment.NewLine & _
                            "- There is a linear dependence within the data" & Environment.NewLine & _
                            "- The input dataset is flawed in some way"
                            )
        Catch ex As System.OverflowException
            MessageBox.Show("These values are too large to compute with the given parameters")
        Catch ex As System.IndexOutOfRangeException
            MessageBox.Show("An index has been entered for a non-existent column")
        Catch ex As System.InvalidCastException
            'does nothing but exit after this exception
        End Try
    End Sub

    ''' <summary>
    ''' Returns the summary of a Linear Regression on a dataset, based on two columns, one as the independent and one as the dependent variable
    ''' </summary>
    Private Sub SingleLinearToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles SingleLinearToolStripMenuItem.Click
        Try
            'perfomrms a linear regression on two columns of the dataset and outputs results to SummaryTextbox
            Regression.SingleLinearSummary(D, _
                                               InputBox("Please input the index of the dependent variable", "Input Dependent", "1"), _
                                               InputBox("Please input the index of the independent variable", "Input Independent", "2"), _
                                               SummaryTextbox)

            'indicates that the SummaryTextbox's content is changing
            TextboxContentChange()
        Catch ex As MatrixCompatibilityException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As NoInverseException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As System.OverflowException
            MessageBox.Show("These values are too large to compute with the given parameters")
        Catch ex As System.IndexOutOfRangeException
            MessageBox.Show("An index has been entered for a non-existent column")
        Catch ex As System.InvalidCastException
            'does nothing but cancel process after this exception
        End Try
    End Sub

    ''' <summary>
    ''' Returns the summary of a Multiplicative Regression on a dataset, based on two columns, one as the independent and one as the dependent variable
    ''' </summary>
    Private Sub MultiplicativeToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MultiplicativeToolStripMenuItem.Click
        Try
            'performs a multiplicative regression on two columns of the dataset and outputs results to SummaryTextbox
            Select Case MsgBox("This regression will omit every row which contains 0 and will take modulus of all values. Are you sure you want to continue?", MsgBoxStyle.YesNo, "Warning")
                Case MsgBoxResult.Yes
                    Regression.MultiplicativeRegressionSummary(D, InputBox("Please input the index of the dependent variable", "Input Value", "2"), SummaryTextbox)

                    'indicates that the SummaryTextbox's content is changing
                    TextboxContentChange()
                Case MsgBoxResult.No
            End Select
        Catch ex As MatrixCompatibilityException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As NoInverseException
            MessageBox.Show("This Regression returns a matrix error. This could be because:" & Environment.NewLine & _
                            "- There is a multiplicative dependence within the data" & Environment.NewLine & _
                            "- The input dataset is flawed in some way"
                            )
        Catch ex As System.OverflowException
            MessageBox.Show("These values are too large to compute with the given parameters")
        Catch ex As System.IndexOutOfRangeException
            MessageBox.Show("An index has been entered for a non-existent column")
        Catch ex As System.InvalidCastException
            'does nothing but cancel process after this exception
        End Try
    End Sub

    ''' <summary>
    ''' Returns the summary of a Polynomial Regression on a dataset, based on two columns, one as the independent and one as the dependent variable
    ''' </summary>
    Private Sub PolynomialToolStripMenuItem2_Click(sender As Object, e As EventArgs) Handles PolynomialSummaryToolStripMenuItem.Click
        Try
            'performs a polynomial regression on two columns of the dataset and outputs results to SummaryTextbox
            Regression.SinglePolynomialSummary(D, _
                                               InputBox("Please input the index of the dependent variable", "Input Dependent", "1"), _
                                               InputBox("Please input the index of the independent variable", "Input Independent", "2"), _
                                               InputBox("Please input the degree of the polynomial", "Input Degree", "1"), SummaryTextbox)

            'indicates that the SummaryTextbox's content is changing
            TextboxContentChange()
        Catch ex As MatrixCompatibilityException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As NoInverseException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As System.OverflowException
            MessageBox.Show("These values are too large to compute with the given parameters")
        Catch ex As System.IndexOutOfRangeException
            MessageBox.Show("An index has been entered for a non-existent column")
        Catch ex As System.InvalidCastException
            'does nothing but cancel process after this exception
        End Try
    End Sub

    ''' <summary>
    ''' Performs an Automatic Regression on a dataset, with based on one column as the dependent
    ''' </summary>
    Private Sub AutomaticAnalysisToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles AutomaticAnalysisToolStripMenuItem.Click
        Try
            'performs an automatic analysis on two columns of the dataset and outputs results to SummaryTextbox
            Regression.AutomaticAnalysis(D, InputBox("Please input the index of the dependent variable", "Input Dependent", "1"), 4, SummaryTextbox)

            'indicates that the SummaryTextbox's content is changing
            TextboxContentChange()
        Catch ex As MatrixCompatibilityException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As NoInverseException
            MessageBox.Show("This Regression returns a matrix error, indicating that the input dataset is flawed in some way")
        Catch ex As System.OverflowException
            MessageBox.Show("These values are too large to compute with the given parameters")
        Catch ex As System.IndexOutOfRangeException
            MessageBox.Show("An index has been entered for a non-existent column")
        Catch ex As System.InvalidCastException
            'does nothing but cancel process after this exception
        End Try
    End Sub

    'Menu Functions

    ''' <summary>
    ''' Exits back to main menu, closing the analysis
    ''' </summary>
    Private Sub LoadNewProjectToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitToMenuToolStripMenuItem.Click
        MainMenu.Show()
        GraphForm.Close()
        Me.Close()
    End Sub

    ''' <summary>
    ''' Exits the program completely
    ''' </summary>
    Private Sub ExitProgramToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ExitProgramToolStripMenuItem.Click
        End
    End Sub

    ''' <summary>
    ''' Handles the closing of the analysis form
    ''' </summary>
    Private Sub RegressionFormClosing(sender As Object, e As EventArgs) Handles Me.FormClosed
        MainMenu.Show()
    End Sub

    'Independence Functions

    ''' <summary>
    ''' Searches the dataset for linear dependence
    ''' Linear dependence is when one column can be expressed as the sum of a multiple of any combination of columns
    ''' </summary>
    Private Sub LinearSearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles LinearSearchToolStripMenuItem.Click
        Try
            'performs a linear dependence search on the dataset and outputs results to SummaryTextbox
            Regression.LinearDependenceSearch(D, SummaryTextbox)

            'indicates that the SummaryTextbox's content is changing
            TextboxContentChange()
        Catch ex As Exception
            MessageBox.Show("An error has occured")
        End Try
    End Sub

    ''' <summary>
    ''' Searches the dataset for multiplicative dependence
    ''' Multiplicative dependence is when one column can be expressed as the product of powers of any combination of columns
    ''' </summary>
    Private Sub MultiplicativeSearchToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles MultiplicativeSearchToolStripMenuItem.Click
        Try
            'performs a multiplicative dependence search on the dataset and outputs results to SummaryTextbox
            Regression.MultiplicativeDependenceSearch(D, SummaryTextbox)

            'indicates that the SummaryTextbox's content is changing
            TextboxContentChange()
        Catch ex As Exception
            MessageBox.Show("An error has occured")
        End Try
    End Sub

    'Analysis Window Functions

    ''' <summary>
    ''' Saves the content of the analysis window to a selected location
    ''' </summary>
    Private Sub SaveAnalysisData(sender As Object, e As EventArgs) Handles SaveToolStripMenuItem.Click
        Try
            Dim file_destination As String

            'defines safe file dialog form
            Dim dialog As New SaveFileDialog()
            dialog.Title = "Save Summary Data"
            dialog.Filter = "Text File|*.txt"
            dialog.FileName = "Analysis on " & file_name.Replace(".csv", "")

            'if a file destination is selected save the textbox to that destination
            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                file_destination = dialog.FileName
                My.Computer.FileSystem.WriteAllText(file_destination, SummaryTextbox.Text, False)
            End If

        Catch ex As Exception
            MessageBox.Show("An error has occured please try again")
        End Try
    End Sub

    ''' <summary>
    ''' Clears the analysis window
    ''' </summary>
    Private Sub ClearToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles ClearToolStripMenuItem.Click
        'pushes the current text onto the undo stack, empties the redo stack, disables the redo button and clears the summarytextbox
        SummaryTextUndoStack.Push(SummaryTextbox.Text)
        RedoToolStripMenuItem.Enabled = False
        SummaryTextRedoStack.Clear()
        SummaryTextbox.Clear()
    End Sub

    ''' <summary>
    ''' Signals to the program that the analysis window's content has change, making suitable changes to the undo and redo stack
    ''' </summary>
    Private Sub TextboxContentChange()
        'pushes previous change onto undo stack
        SummaryTextUndoStack.Push(SummaryTextBoxTemp)

        'changes temp to current text
        SummaryTextBoxTemp = SummaryTextbox.Text

        'clears redo stack
        SummaryTextRedoStack.Clear()

        'enables undo button and disables redo button
        UndoToolStripMenuItem.Enabled = True
        RedoToolStripMenuItem.Enabled = False
    End Sub

    ''' <summary>
    ''' Undoes the previous change to the analysis window
    ''' </summary>
    Private Sub UndoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles UndoToolStripMenuItem.Click
        'pushes the current text onto the redo stack, pops text from the undo stack into the summarytextbox, and enables redo
        SummaryTextRedoStack.Push(SummaryTextbox.Text)
        SummaryTextbox.Text = SummaryTextUndoStack.Pop
        RedoToolStripMenuItem.Enabled = True

        'if the undo stack is empty then do not allow user to undo
        If SummaryTextUndoStack.Count = 0 Then
            UndoToolStripMenuItem.Enabled = False
        End If
    End Sub

    ''' <summary>
    ''' Redoes the previous undone change to the analysis window
    ''' </summary>
    Private Sub RedoToolStripMenuItem_Click(sender As Object, e As EventArgs) Handles RedoToolStripMenuItem.Click
        'pushes the current text onto the undo stack, pops text from the redo stack into the summarytextbox, and enables undo
        SummaryTextUndoStack.Push(SummaryTextbox.Text)
        SummaryTextbox.Text = SummaryTextRedoStack.Pop
        UndoToolStripMenuItem.Enabled = True

        'if the redo stack is empty then do not allow user to redo
        If SummaryTextRedoStack.Count = 0 Then
            RedoToolStripMenuItem.Enabled = False
        End If
    End Sub

End Class