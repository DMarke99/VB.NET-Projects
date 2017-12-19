Public Class MainMenu

    'Loading

    ''' <summary>
    ''' Creates a new session based on a table at a given location
    ''' </summary>
    Private Sub CreateNewSession_Click(sender As Object, e As EventArgs) Handles CreateNewSession.Click
        OpenFileDialog.Filter = ".csv table|*.csv"

        If OpenFileDialog.ShowDialog = Windows.Forms.DialogResult.OK Then
            Try
                'tries to import without headers, and if that returns an error try it without headers. If neither work then the .csv is faulty
                Try
                    Dim M As New AnalysisForm(OpenFileDialog.FileName, False)
                    M.Show()
                    Me.Hide()
                Catch ex As System.FormatException
                    Dim M As New AnalysisForm(OpenFileDialog.FileName, True)
                    M.Show()
                    Me.Hide()
                End Try
            Catch ex As CSVException
                MessageBox.Show("The selected file contains a structural error and thus will not be imported")
            Catch ex As System.FormatException
                MessageBox.Show("This .csv contains values which cannot be input into the program. Please fix the error before trying again")
            Catch ex As System.IO.IOException
                MessageBox.Show("This .csv is being used by another program")
            Catch ex As System.ObjectDisposedException
                'Continues as per usual
            End Try
        End If
    End Sub

    'Utility

    ''' <summary>
    ''' Displays the user guide for operation of the program
    ''' </summary>
    Private Sub UserGuideButton_Click(sender As Object, e As EventArgs) Handles UserGuideButton.Click
        'tries to open the user guide
        Try
            Process.Start("User Guide.txt")
        Catch ex As Exception
            MessageBox.Show("An error has occured.")
        End Try
    End Sub

    'Closure

    ''' <summary>
    ''' Procedure which acts upon clicking the exit button
    ''' </summary>
    Private Sub ExitProject_Click(sender As Object, e As EventArgs) Handles ExitProject.Click
        End
    End Sub

    ''' <summary>
    ''' Handles the closure of the project
    ''' </summary>
    Private Sub MainMenuClose(sender As Object, e As EventArgs) Handles MyBase.FormClosed
        End
    End Sub

End Class