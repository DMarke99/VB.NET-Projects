Imports System.Text.RegularExpressions

''' <summary>
''' Defines the form for editing graph settings
''' </summary>
Public Class GraphFormSettings
    Property g_reference As Graph
    Property BigDashWidthOriginal As Integer

    'Initialisation

    ''' <summary>
    ''' Imports the graph settings
    ''' </summary>
    ''' <param name="g">The graph which the settings are imported from</param>
    Sub New(ByRef g As Graph)
        InitializeComponent()

        'loads the contents of the settings to the relevant textboxes
        TopBorderTextField.Text = g.top_border
        BottomBorderTextField.Text = g.bottom_border
        LeftBorderTextField.Text = g.left_border
        RightBorderTextField.Text = g.right_border
        CrossSizeTextField.Text = g.cross_size
        CrossWeightTextField.Text = g.cross_weight
        LineWidthTextField.Text = g.line_weight
        DashSizeTextField.Text = g.dash_size
        OptimalDashNoTextField.Text = g.best_dash_no
        BigDashTextField.Text = g.big_dash_interval
        BigDashWidthOriginal = BigDashTextField.Text
        If g.showResiduals Then
            ResidualsOptionBox.Text = "Shown"
        Else
            ResidualsOptionBox.Text = "Hidden"
        End If
        Me.KeyPreview = True
        g_reference = g
    End Sub

    'Buttons

    ''' <summary>
    ''' Exports the edited settings back to the form
    ''' </summary>
    Private Sub ConfirmSettingsClick(sender As Object, e As EventArgs) Handles ConfirmSettings.Click
        Select Case MsgBox("Are you sure you want to confirm any changes to settings?", MsgBoxStyle.YesNo, "Confirm Settings")
            Case MsgBoxResult.Yes

                'cancels the process if Optimal Dash Number is 0
                If Not IsPositiveInteger(OptimalDashNoTextField.Text) Then
                    MessageBox.Show("Optimal Dash Number must be a positive integer")
                    Exit Sub
                End If


                'exports the values back to the graph
                g_reference.top_border = TopBorderTextField.Text
                g_reference.bottom_border = BottomBorderTextField.Text
                g_reference.left_border = LeftBorderTextField.Text
                g_reference.right_border = RightBorderTextField.Text
                g_reference.cross_size = CrossSizeTextField.Text
                g_reference.cross_weight = CrossWeightTextField.Text
                g_reference.line_weight = LineWidthTextField.Text
                g_reference.dash_size = DashSizeTextField.Text
                g_reference.best_dash_no = OptimalDashNoTextField.Text
                g_reference.big_dash_interval = BigDashTextField.Text

                Close()
                If ResidualsOptionBox.Text = "Shown" Then
                    g_reference.showResiduals = True
                Else
                    g_reference.showResiduals = False
                End If

                'issues warning if dodgy dimensions are input
                If (g_reference.top_border + g_reference.bottom_border > GraphForm.GraphPictureBox.Height) Or _
                    (g_reference.left_border + g_reference.right_border > GraphForm.GraphPictureBox.Width) Then
                    MessageBox.Show("Due to dimensions selected some visual artifacts may display")
                End If
        End Select
    End Sub

    ''' <summary>
    ''' Event which occurs upon clicking the cancel button
    ''' </summary>
    Private Sub CancelSettingsChange_Click(sender As Object, e As EventArgs) Handles CancelSettingsChange.Click
        Close()
    End Sub

    ''' <summary>
    ''' Restores the default settings to the program
    ''' </summary>
    Private Sub RestoreDefault(sender As Object, e As EventArgs) Handles RestoreDefaultSettings.Click
        TopBorderTextField.Text = 30
        BottomBorderTextField.Text = 60
        LeftBorderTextField.Text = 100
        RightBorderTextField.Text = 60
        CrossSizeTextField.Text = 4
        CrossWeightTextField.Text = 1
        LineWidthTextField.Text = 1
        DashSizeTextField.Text = 5
        OptimalDashNoTextField.Text = 125
        BigDashTextField.Text = 10
        BigDashWidthOriginal = 10
    End Sub

    'Textbox Validation

    ''' <summary>
    ''' Ensures that only numerical values are input into the textboxes
    ''' </summary>
    Private Sub TextBoxTextChange(sender As Object, e As KeyPressEventArgs) Handles Me.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then e.KeyChar = ""
    End Sub

    ''' <summary>
    ''' Validates entries into the "Big Dash Interval" textbox
    ''' The entries can only be 1, 2 or 5 times a power of 10, that is an integer
    ''' </summary>
    Private Sub BigDashWidthChange(sender As Object, e As EventArgs) Handles BigDashTextField.Leave
        'the regular expression for this textbox
        Dim BigDashWidthValidator As New Regex("^[125]0*$")

        'if the field isn't a match then return to its value before the changes
        If Not BigDashWidthValidator.IsMatch(BigDashTextField.Text) Then
            BigDashTextField.Text = BigDashWidthOriginal
        Else
            BigDashWidthOriginal = BigDashTextField.Text
        End If
    End Sub

    ''' <summary>
    ''' Validates strings to check if they are valid integers
    ''' </summary>
    ''' <param name="s">The string to be validated</param>
    Function IsPositiveInteger(s As String)
        'the regular expression for positive integers
        Dim IntegerValidator As New Regex("^[1-9][0-9]*$")
        Return IntegerValidator.IsMatch(s)
    End Function

End Class