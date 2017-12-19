'Superclass

''' <summary>
''' The superclass of regression drawing forms
''' </summary>
Public Class GraphForm
    'properties of a graph form

    Property BitmapImage As New Drawing.Bitmap(1920, 1080)
    Property Graphics As Graphics = Graphics.FromImage(BitmapImage)
    Property GraphData As New Graph
    Property LineOfBestFit As Expression
    Property D As Dataset
    Property independent As Integer
    Property dependent As Integer
    Property PreviousForm As AnalysisForm

    'Graph Drawing

    ''' <summary>
    ''' Loads the box and grid on which the graph will be drawn
    ''' </summary>
    Sub LoadGraphGrid()
        'resizes bitmap image
        BitmapImage = New Drawing.Bitmap(GraphPictureBox.Width, GraphPictureBox.Height)
        Graphics = Graphics.FromImage(BitmapImage)

        'draws graph with a border of 60 around every edge except the top edge, which has a border of 30
        Graphics.FillRectangle(Brushes.White, 0, 0, GraphPictureBox.Width, GraphPictureBox.Height)

        'draws the border
        Graphics.DrawRectangle(Pens.Black, GraphData.left_border, GraphData.top_border, _
                               (GraphPictureBox.Width - GraphData.left_border - GraphData.right_border), _
                               GraphPictureBox.Height - GraphData.top_border - GraphData.bottom_border)

        'imports the graph into the picture box
        GraphPictureBox.Image = BitmapImage
    End Sub

    ''' <summary>
    ''' Draws a line on the graph based on an expression
    ''' </summary>
    ''' <param name="e">The expression to be plot</param>
    Sub DrawExpression(ByVal e As Expression)
        'draws the line of best fit
        GraphData.SketchLine(Graphics, GraphPictureBox, e)

        'draws residuals if user has selected for them to be drawn
        If GraphData.showResiduals Then
            GraphData.SketchResidualLine(Graphics, GraphPictureBox, LineOfBestFit, D.Header(independent - 1), D.Header(dependent - 1))
        End If
    End Sub

    ''' <summary>
    ''' Plots a regression line based on two columns in a dataset
    ''' </summary>
    ''' <remarks></remarks>
    Overridable Sub Plot()
        Dim data(,) = D.getData

        'adds all the points to the graph object
        For i As Integer = 0 To D.Row - 1
            GraphData.Add(data(i, independent - 1), data(i, dependent - 1))
        Next

        'checks if the graph to be drawn is valid
        ValidateGraph()

        'loads the graph grid
        LoadGraphGrid()

        'sketches the graph
        GraphData.Sketch(Graphics, GraphPictureBox, D.Header(independent - 1), D.Header(dependent - 1))

        'draws the line of best fit
        DrawExpression(LineOfBestFit)

        'shows the form
        Me.Show()
    End Sub

    'Form Scaling

    ''' <summary>
    ''' Resizes the graph when the form is resized
    ''' </summary>
    Private Sub ScalePictureBox(sender As Object, e As EventArgs) Handles MyBase.ResizeEnd
        LoadGraphGrid()
        Plot()
    End Sub

    'Graph Settings

    ''' <summary>
    ''' Copies the content of the graph to the clipboard
    ''' </summary>
    Sub CopyGraphClick(sender As Object, e As EventArgs) Handles CopyGraphToolStripMenuItem.Click
        Clipboard.SetImage(GraphPictureBox.Image)
    End Sub

    ''' <summary>
    ''' Opens the graph settings
    ''' </summary>
    Sub GraphSettingsClick(sender As Object, e As EventArgs) Handles GraphSettingsToolStripMenuItem.Click
        'opens graph form settings for the current graph
        Dim settings As New GraphFormSettings(GraphData)

        'sets starting position
        settings.StartPosition = FormStartPosition.CenterParent

        'loads form as a dialog
        settings.ShowDialog()

        'resets graph image
        GraphPictureBox.Image = Nothing

        'reloads the graph
        LoadGraphGrid()

        'plots the new graph
        Plot()
    End Sub

    ''' <summary>
    ''' Saves the current graph to a selected file type and location
    ''' </summary>
    Private Sub SaveGraph(sender As Object, e As EventArgs) Handles SaveGraphToolStripMenuItem.Click
        Try
            Dim file_destination As String
            'defines format in which images can be saved
            Dim dialog As New SaveFileDialog()
            dialog.Title = "Save Summary Data"
            dialog.Filter = "PNG Image|*.png|JPEG Image|*.jpeg|GIF Image|*.gif"

            'if user has selected location then save to that location
            If dialog.ShowDialog() = Windows.Forms.DialogResult.OK Then
                file_destination = dialog.FileName
                BitmapImage.Save(dialog.FileName)
            End If

        Catch ex As Exception
            MessageBox.Show("An error has occured please try again")
        End Try
    End Sub

    'Error Prevention
    ''' <summary>
    ''' Throws an exception when an improper graph is about to be formed
    ''' </summary>
    Sub ValidateGraph()
        If D.Row < 2 OrElse (GraphData.range_x = 0 Or GraphData.range_y = 0) Then
            Throw New ImproperGraphException("There are not enough points to plot a proper, meaningful graph")
            Close()
        End If
    End Sub

    'Closure

    ''' <summary>
    ''' The procedure that acts upon closure of the graph form
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub UponClosure(sender As Object, e As EventArgs) Handles MyBase.FormClosed
        PreviousForm.Show()
    End Sub

    ''' <summary>
    ''' The procedure that acts upon leaving clicking the exit form button
    ''' </summary>
    Private Sub ExitForm(sender As Object, e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

End Class

'Subclasses

''' <summary>
''' The subclass which draws linear regression lines
''' </summary>
''' <remarks></remarks>
Class LinearGraph : Inherits GraphForm

    ''' <summary>
    ''' Creates a new Linear Regression graph
    ''' </summary>
    ''' <param name="Data">The Dataset on which the graph will be plotted</param>
    ''' <param name="x">The index of the independent variable</param>
    ''' <param name="y">The index of the dependent variable</param>
    Sub New(ByVal Data As Dataset, ByVal x As Integer, ByVal y As Integer, ByRef Parent As AnalysisForm)
        'defines properties of the graph
        D = Data
        independent = x
        dependent = y
        LineOfBestFit = Regression.SinglePolynomialExpression(D, dependent, independent, 1)
        PreviousForm = Parent
        Me.Text = "Linear Regression Graph"

        'imports points
        For i As Integer = 0 To D.Row - 1
            GraphData.Add(Data.Value(i, independent - 1), Data.Value(i, dependent - 1))
        Next
    End Sub

End Class

''' <summary>
''' The subclass which draws polynomial regression lines
''' </summary>
Class PolynomialGraph : Inherits GraphForm
    Property degree As Integer

    ''' <summary>
    ''' Creates a new Polynomial Regression graph
    ''' </summary>
    ''' <param name="Data">The Dataset on which the graph will be plotted</param>
    ''' <param name="x">The index of the independent variable</param>
    ''' <param name="y">The index of the dependent variable</param>
    ''' <param name="n">The degree of the polynomial</param>
    Sub New(ByVal Data As Dataset, ByVal x As Integer, ByVal y As Integer, ByVal n As Integer, ByRef Parent As AnalysisForm)
        'defines properties of the graph
        D = Data
        independent = x
        dependent = y
        degree = n
        LineOfBestFit = Regression.SinglePolynomialExpression(D, dependent, independent, degree)
        PreviousForm = Parent
        Me.Text = "Polynomial Regression Graph"

        'imports points
        For i As Integer = 0 To D.Row - 1
            GraphData.Add(Data.Value(i, independent - 1), Data.Value(i, dependent - 1))
        Next
    End Sub

End Class

''' <summary>
''' The subclass which draws graphs with no lines
''' </summary>
Class NoLineGraph : Inherits GraphForm

    ''' <summary>
    ''' Creates a new Polynomial Regression graph
    ''' </summary>
    ''' <param name="Data">The Dataset on which the graph will be plotted</param>
    ''' <param name="x">The index of the independent variable</param>
    ''' <param name="y">The index of the dependent variable</param>
    Sub New(ByVal Data As Dataset, ByVal x As Integer, ByVal y As Integer, ByRef Parent As AnalysisForm)
        'defines properties of the graph
        D = Data
        independent = x
        dependent = y
        PreviousForm = Parent
        Me.Text = "Graph"

        'imports points
        For i As Integer = 0 To D.Row - 1
            GraphData.Add(Data.Value(i, independent - 1), Data.Value(i, dependent - 1))
        Next
    End Sub

    ''' <summary>
    ''' Plots points on a graph
    ''' </summary>
    Overrides Sub Plot()
        Dim data(,) = D.getData

        'resets GraphData
        GraphData = New Graph

        'adds all the points to the graph object
        For i As Integer = 0 To D.Row - 1
            GraphData.Add(data(i, independent - 1), data(i, dependent - 1))
        Next

        'checks if the graph to be drawn is valid
        ValidateGraph()

        'loads the graph grid
        LoadGraphGrid()

        'sketches the graph
        GraphData.Sketch(Graphics, GraphPictureBox, D.Header(independent - 1), D.Header(dependent - 1))

        'shows the form
        Me.Show()
    End Sub

End Class