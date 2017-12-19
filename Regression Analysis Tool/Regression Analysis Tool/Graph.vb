Public Class Graph
    ''' <summary>
    ''' Defines the coordinate structure, which contains an x and y coordinate
    ''' </summary>
    Structure Coordinate
        Dim x As Decimal
        Dim y As Decimal
    End Structure

    Dim Points As New List(Of Coordinate)

    'Properties, General Booleans and Procedures

    ''' <summary>
    ''' Adds a point to the current graph
    ''' </summary>
    ''' <param name="x">The x value of the coordinate to be added</param>
    ''' <param name="y">The y value of the coordinate to be added</param>
    ''' <remarks></remarks>
    Sub Add(x As Decimal, y As Decimal)
        Dim temp As New Coordinate
        temp.x = x
        temp.y = y
        Points.Add(temp)
    End Sub

    'Readonly properties

    ''' <summary>
    ''' Returns maximum x value
    ''' </summary>
    ReadOnly Property max_x As Decimal
        Get
            Dim temp As Decimal = Points.LastOrDefault.x
            For Each p As Coordinate In Points
                If temp < p.x Then
                    temp = p.x
                End If
            Next

            Return temp
        End Get
    End Property

    ''' <summary>
    ''' Returns maximum y value
    ''' </summary>
    ReadOnly Property max_y As Decimal
        Get
            Dim temp As Decimal = Points.LastOrDefault.y
            For Each p As Coordinate In Points
                If temp < p.y Then
                    temp = p.y
                End If
            Next

            Return temp
        End Get
    End Property

    ''' <summary>
    ''' Returns minimum x value
    ''' </summary>
    ReadOnly Property min_x As Decimal
        Get
            Dim temp As Decimal = Points.LastOrDefault.x
            For Each p As Coordinate In Points
                If temp > p.x Then
                    temp = p.x
                End If
            Next

            Return temp
        End Get
    End Property

    ''' <summary>
    ''' Returns minimum y value
    ''' </summary>
    ReadOnly Property min_y As Decimal
        Get
            Dim temp As Decimal = Points.LastOrDefault.y
            For Each p As Coordinate In Points
                If temp > p.y Then
                    temp = p.y
                End If
            Next

            Return temp
        End Get
    End Property

    ''' <summary>
    ''' Returns the range of x values
    ''' </summary>
    ReadOnly Property range_x As Decimal
        Get
            Return Math.Abs(max_x - min_x)
        End Get
    End Property

    ''' <summary>
    ''' Returns the range of y values
    ''' </summary>
    ReadOnly Property range_y As Decimal
        Get
            Return Math.Abs(max_y - min_y)
        End Get
    End Property

    'User-editable properties
    Property top_border As Integer = 30
    Property bottom_border As Integer = 60
    Property left_border As Integer = 100
    Property right_border As Integer = 60
    Property cross_size As Integer = 4
    Property cross_weight As Integer = 1
    Property line_weight As Integer = 1
    Property dash_size As Integer = 5
    Property best_dash_no As Integer = 125
    Property big_dash_interval As Decimal = 10
    Property showResiduals As Boolean = False

    'Booleans which determine whether values are within the range of the given coordinates

    ''' <summary>
    ''' Determines whether an x coordinate is within the range of x values in the current graph
    ''' </summary>
    ''' <param name="x">The value to be determined</param>
    Function isInXRange(ByVal x As Decimal) As Boolean
        If x <= max_x And x >= min_x Then
            Return True
        Else
            Return False
        End If
    End Function

    ''' <summary>
    ''' Determines whether a y coordinate is within the range of y values in the current graph
    ''' </summary>
    ''' <param name="y">The value to be determined</param>
    Function isInYRange(ByVal y As Decimal) As Boolean
        If y <= max_y And y >= min_y Then
            Return True
        Else
            Return False
        End If
    End Function

    'Conversion from coordinate to pixel location

    ''' <summary>
    ''' Transforms an x coordinate into a pixel location
    ''' </summary>
    ''' <param name="x">The value of x to be transformed into a pixel location</param>
    ''' <param name="p">The PictureBox on which the graph is being plotted</param>
    Function x_toCoordinate(ByVal x As Decimal, ByVal p As PictureBox) As Integer
        Dim canvas_width As Integer = p.Width - left_border - right_border
        Dim new_x As Integer = ((x - min_x) / range_x) * canvas_width + left_border
        Return new_x
    End Function

    ''' <summary>
    ''' Transforms a y coordinate into a pixel location
    ''' </summary>
    ''' <param name="y">The value of y to be transformed into a pixel location</param>
    ''' <param name="p">The PictureBox on which the graph is being plotted</param>
    Function y_toCoordinate(ByVal y As Decimal, ByVal p As PictureBox) As Integer
        Dim canvas_height As Integer = p.Height - top_border - bottom_border
        Dim new_y As Integer = p.Height - ((y - min_y) / (range_y)) * canvas_height - bottom_border
        Return new_y
    End Function

    'Drawing Tools and Functions

    ''' <summary>
    ''' Draws a cross at an input pixel location
    ''' </summary>
    ''' <param name="canvas">The Graphics which the cross is being drawn to</param>
    ''' <param name="x">The x coordinate of the pixel</param>
    ''' <param name="y">The y coordinate of the pixel</param>
    Sub DrawCross(ByRef canvas As Graphics, ByVal x As Integer, ByVal y As Integer)
        Dim p As New Pen(Brushes.Red, cross_weight)
        canvas.DrawLine(p, x - cross_size, y - cross_size, x + cross_size, y + cross_size)
        canvas.DrawLine(p, x - cross_size, y + cross_size, x + cross_size, y - cross_size)
        'canvas.DrawEllipse(p, x - cross_size, y - cross_size, 2 * cross_size, 2 * cross_size)
    End Sub

    ''' <summary>
    ''' Draws a dash at an x coordinate on the x axis
    ''' </summary>
    ''' <param name="canvas">The Graphics which the dash is being drawn to</param>
    ''' <param name="x">The x coordinate of the pixel</param>
    ''' <param name="isBigDash">Boolean which determines whether to draw a big dash</param>
    ''' <param name="p">The PictureBox on which the graph is being plotted</param>
    Sub DrawXDash(ByRef canvas As Graphics, ByVal x As Integer, ByVal isBigDash As Boolean, ByVal p As PictureBox)
        If isBigDash Then
            canvas.DrawLine(Pens.Black, x, p.Height - bottom_border, x, p.Height - bottom_border + dash_size * 2)
        Else
            canvas.DrawLine(Pens.Black, x, p.Height - bottom_border, x, p.Height - bottom_border + dash_size)
        End If
    End Sub

    ''' <summary>
    ''' Draws a dash at a y coordinate on the x axis
    ''' </summary>
    ''' <param name="canvas">The Graphics which the dash is being drawn to</param>
    ''' <param name="y">The y coordinate of the pixel</param>
    ''' <param name="isBigDash">Boolean which determines whether to draw a big dash</param>
    ''' <param name="p">The PictureBox on which the graph is being plotted</param>
    Sub DrawYDash(ByRef canvas As Graphics, ByVal y As Integer, ByVal isBigDash As Boolean, ByVal p As PictureBox)
        If isBigDash Then
            canvas.DrawLine(Pens.Black, left_border, y, left_border - dash_size * 2, y)
        Else
            canvas.DrawLine(Pens.Black, left_border, y, left_border - dash_size, y)
        End If
    End Sub

    ''' <summary>
    ''' Finds the optimal dash width for a given range
    ''' The optimal dash width is defined as the dash width at which the number of displayed dashes is closest to the optimal dash number
    ''' The mantissa of the optimal dash width is either 1, 2 or 5 in base 10
    ''' </summary>
    ''' <param name="range">The range on which the optimal dash width is found</param>
    Function OptimalDashWidth(ByVal range As Decimal) As Decimal
        'the leading digit is eiher 1, 2 or 5
        Dim dash_no As Decimal = 1

        Dim mantissa As Integer = 1
        Dim exponent As Integer = Math.Log10(range) + 4

        Dim optimal_exponent As Decimal
        Dim optimal_mantissa As Decimal
        Dim optimal_dash_no As Decimal

        'keeps decreasing the dash width in order to increase the number of dashes displayed
        'if the number of dashes displayed is closer to optimal dash number then the mantissa and exponent are stored
        'if the number of dashes displayed is further from optimal dash number then the mantissa and exponent are not stored
        'the loop terminates when the current inspected combination of mantissa and exponent are greater than 10 times the optimal dash number
        Do Until dash_no > best_dash_no * 10
            mantissa = 1
            dash_no = range / (mantissa * 10 ^ exponent)

            If Math.Abs(dash_no - best_dash_no) < Math.Abs(optimal_dash_no - best_dash_no) Then
                optimal_exponent = exponent
                optimal_mantissa = mantissa
                optimal_dash_no = dash_no
            End If

            mantissa = 2
            dash_no = range / (mantissa * 10 ^ exponent)

            If Math.Abs(dash_no - best_dash_no) < Math.Abs(optimal_dash_no - best_dash_no) Then
                optimal_exponent = exponent
                optimal_mantissa = mantissa
                optimal_dash_no = dash_no
            End If

            mantissa = 5
            dash_no = range / (mantissa * 10 ^ exponent)

            If Math.Abs(dash_no - best_dash_no) < Math.Abs(optimal_dash_no - best_dash_no) Then
                optimal_exponent = exponent
                optimal_mantissa = mantissa
                optimal_dash_no = dash_no
            End If

            exponent = exponent - 1
        Loop

        Return optimal_mantissa * 10 ^ optimal_exponent
    End Function

    'Large Scale Drawing

    ''' <summary>
    ''' Sketches a curve on a given Graphic based on the content of the graph
    ''' </summary>
    ''' <param name="canvas">The Graphic on which the graph is drawn</param>
    ''' <param name="pb">The PictureBox which the graph is based on</param>
    ''' <param name="x_var_name">The name of the variable on the x axis</param>
    ''' <param name="y_var_name">The name of the variable on the y axis</param>
    Sub Sketch(ByRef canvas As Graphics, ByVal pb As PictureBox, _
                             Optional ByVal x_var_name As String = "Independent Variable", _
                             Optional ByVal y_var_name As String = "Dependent Variable")

        'there will be a big dash every 10 dashes
        Dim f As New Font("Calibri", 10)

        Dim dash_width_x As Decimal = OptimalDashWidth(range_x)
        Dim dash_width_y As Decimal = OptimalDashWidth(range_y)

        'defines canvas width and height
        Dim canvas_width As Integer = pb.Width - left_border - right_border
        Dim canvas_height As Integer = pb.Height - top_border - bottom_border

        'draws a red cross at each point
        For Each p As Coordinate In Points
            DrawCross(canvas, x_toCoordinate(p.x, pb), y_toCoordinate(p.y, pb))
        Next


        'starting from the next smallest rounded value at the given magnitude,
        'plots a line dash next to the x axis for every value within the range at that maxitude
        'plots a heavier dash every 10 values

        'plots numbers on the x axis
        'factor such that every number will be in standard form, reducing the length of numbers
        Dim x_factor_of_10 As Integer
        If min_x <> 0 And max_x <> 0 Then
            x_factor_of_10 = Math.Max(Math.Log10(Math.Abs(min_x)), Math.Log10(Math.Abs(max_x)))
        ElseIf min_x = 0 And max_x <> 0 Then
            x_factor_of_10 = Math.Log10(Math.Abs(max_x))
        ElseIf min_x <> 0 And max_x = 0 Then
            x_factor_of_10 = Math.Log10(Math.Abs(min_x))
        End If

        For x As Decimal = Math.Ceiling(min_x / dash_width_x) _
            * dash_width_x To max_x Step dash_width_x

            Dim new_x As Integer = x_toCoordinate(x, pb)

            If x Mod (big_dash_interval * dash_width_x) = 0 Then
                DrawXDash(canvas, new_x, True, pb)
            Else
                DrawXDash(canvas, new_x, False, pb)
            End If
        Next

        'does the same for y
        'plots numbers on the y axis
        'factor such that every number will be in standard form, reducing the length of numbers
        Dim y_factor_of_10 As Integer
        If min_y <> 0 And max_y <> 0 Then
            y_factor_of_10 = Math.Max(Math.Log10(Math.Abs(min_y)), Math.Log10(Math.Abs(max_y)))
        ElseIf min_y = 0 And max_y <> 0 Then
            y_factor_of_10 = Math.Log10(Math.Abs(max_y))
        ElseIf min_y <> 0 And max_y = 0 Then
            y_factor_of_10 = Math.Log10(Math.Abs(min_y))
        End If

        For y As Decimal = Math.Ceiling(min_y / dash_width_y) _
            * dash_width_y To max_y Step dash_width_y

            Dim new_y As Integer = pb.Height - ((y - min_y) / range_y) * canvas_height - bottom_border

            If y Mod (big_dash_interval * dash_width_y) = 0 Then
                DrawYDash(canvas, new_y, True, pb)
            Else
                DrawYDash(canvas, new_y, False, pb)
            End If
        Next

        'reduces the factors by 1 so that the output number is between 10 and -10
        x_factor_of_10 = x_factor_of_10 - 1
        y_factor_of_10 = y_factor_of_10 - 1

        'defines the style of the text
        Dim style As New StringFormat
        style.Alignment = StringAlignment.Center
        style.LineAlignment = StringAlignment.Center

        'draws numbers
        For x As Decimal = Math.Ceiling(min_x / dash_width_x) _
            * dash_width_x To max_x Step dash_width_x

            'decides whether the number needs to be plot
            If x Mod (big_dash_interval * dash_width_x) = 0 Then

                'finds coordinate for label
                Dim new_x As Integer = x_toCoordinate(x, pb)

                'calculates number to be plotted
                Dim output As Decimal = Math.Round(x, Math.Max(-x_factor_of_10 + 1, 4)) / 10 ^ x_factor_of_10

                'finds the point to plot the number to
                Dim r As New Rectangle(new_x - 50, pb.Height - bottom_border + 15, 100, 10)

                'plots point
                canvas.DrawString(output, f, Brushes.Black, r, style)
            End If
        Next

        'for y axis, the strings will appear to align at the right
        style.Alignment = StringAlignment.Far

        For y As Decimal = Math.Ceiling(min_y / dash_width_y) _
            * dash_width_y To max_y Step dash_width_y

            'decides whether the number needs to be plot
            If y Mod (big_dash_interval * dash_width_y) = 0 Then

                'finds coordinate for label
                Dim new_y As Integer = y_toCoordinate(y, pb)

                'calculates number to be plotted
                Dim output As Decimal = Math.Round(y, Math.Max(-y_factor_of_10 + 1, 4)) / 10 ^ y_factor_of_10

                'finds the point to plot the number to
                Dim r As New Rectangle(0, new_y - 8, left_border - dash_size * 2, 16)

                'plots point
                canvas.DrawString(output, f, Brushes.Black, r, style)
            End If
        Next

        'plots axis names

        style.Alignment = StringAlignment.Center
        'x axis

        'defines the box in which the text will appear
        Dim x_rectangle As New Rectangle(left_border, pb.Height - bottom_border + 20, canvas_width, 30)

        'draws text
        If x_factor_of_10 = 0 Then
            canvas.DrawString(x_var_name, f, Brushes.Black, x_rectangle, style)
        Else
            canvas.DrawString(x_var_name + " (× 10^" + x_factor_of_10.ToString + ")", f, Brushes.Black, x_rectangle, style)
        End If

        'y axis

        'defines the box in which the text will appear
        Dim y_rectangle As New Rectangle(10, top_border, 30, canvas_height)

        'y uses the same style as x, except its vertical
        style.FormatFlags = StringFormatFlags.DirectionVertical

        'draws text
        If y_factor_of_10 = 0 Then
            canvas.DrawString(y_var_name, f, Brushes.Black, y_rectangle, style)
        Else
            canvas.DrawString(y_var_name + " (× 10^" + y_factor_of_10.ToString + ")", f, Brushes.Black, y_rectangle, style)
        End If

        style.Dispose()
    End Sub

    ''' <summary>
    ''' Sketches a curve on a given Graphic based on the content of the graph
    ''' </summary>
    ''' <param name="canvas">The Graphic on which the graph is drawn</param>
    ''' <param name="pb">The PictureBox which the graph is based on</param>
    ''' <param name="line_expression">The expression of the line to be drawn</param>
    ''' <param name="x_var_name">The name of the variable on the x axis</param>
    ''' <param name="y_var_name">The name of the variable on the y axis</param>
    Sub SketchLine(ByRef canvas As Graphics, ByVal pb As PictureBox, ByVal line_expression As Expression, _
                             Optional ByVal x_var_name As String = "Independent Variable", _
                             Optional ByVal y_var_name As String = "Dependent Variable")



        'defines canvas width and height
        Dim canvas_width As Integer = pb.Width - left_border - right_border
        Dim canvas_height As Integer = pb.Height - top_border - bottom_border

        'defines the pen used to draw the points
        Dim p As New Pen(Brushes.Black, line_weight)

        For x As Integer = left_border To left_border + canvas_width - 1

            'transforms x from a coordinate in code to a coordinate in the graph
            Dim new_x As Decimal = ((x - left_border) / canvas_width) * range_x + min_x
            Dim next_x As Decimal = ((x + 1 - left_border) / canvas_width) * range_x + min_x

            Dim new_y As Integer
            Dim next_y As Integer
            Dim isCurrentValueInfinity As Boolean = False
            Dim isNextValueInfinity As Boolean = False

            'gets current and next y pixel coordinates of graph
            Try
                new_y = pb.Height - ((line_expression(new_x) - min_y) / (range_y)) * canvas_height - bottom_border
            Catch ex As System.OverflowException
                isCurrentValueInfinity = True
            End Try

            Try
                next_y = pb.Height - ((line_expression(next_x) - min_y) / (range_y)) * canvas_height - bottom_border
            Catch ex As System.OverflowException
                isNextValueInfinity = True
            End Try


            'if the current points and the next point are on the graph, join them with a line
            'if one of either the current point or next point is on the graph, draw a line from the visible point in the direction of the next point
            If Not isCurrentValueInfinity AndAlso isInYRange(line_expression(new_x)) Then

                If Not isNextValueInfinity AndAlso isInYRange(line_expression(next_x)) Then

                    'if both points are valid and on the graph draw line between them
                    canvas.DrawLine(p, x, new_y, x + 1, next_y)
                Else

                    'if the current point is on the graph and next point isnt then
                    'draw a line extending to the relevant border to the current point
                    If isNextValueInfinity Then
                        If line_expression(new_x) > 0 Then
                            canvas.DrawLine(p, x, new_y, x, top_border)
                        Else
                            canvas.DrawLine(p, x, new_y, x, pb.Height - bottom_border)
                        End If
                    Else
                        If line_expression(new_x) < line_expression(next_x) Then
                            canvas.DrawLine(p, x, new_y, x, top_border)
                        Else
                            canvas.DrawLine(p, x, new_y, x, pb.Height - bottom_border)
                        End If
                    End If
                End If

            Else
                If isCurrentValueInfinity Then

                    'if the current point is infinity and the next point is on the graph then 
                    'draw a line extending to the relevant border to the next point
                    If line_expression(next_x) > 0 And isInYRange(line_expression(next_x)) Then
                        canvas.DrawLine(p, x + 1, next_y, x + 1, top_border)
                    ElseIf line_expression(next_x) < 0 And isInYRange(line_expression(next_x)) Then
                        canvas.DrawLine(p, x + 1, next_y, x + 1, pb.Height - bottom_border)
                    End If
                Else

                    'if the current point is not infinity and the next point is on the graph then
                    'draw a line extending to the relevant border to the next point
                    If Not isNextValueInfinity AndAlso isInYRange(line_expression(next_x)) Then
                        If line_expression(new_x) > line_expression(next_x) Then
                            canvas.DrawLine(p, x + 1, next_y, x + 1, top_border)
                        Else
                            canvas.DrawLine(p, x + 1, next_y, x + 1, pb.Height - bottom_border)
                        End If
                    Else

                        'if neither point are on the graph then draw a line extending between both borders
                        If (line_expression(new_x) > max_y And line_expression(next_x) < min_y) _
                            And Not isCurrentValueInfinity And Not isNextValueInfinity Then
                            canvas.DrawLine(p, x, top_border, x + 1, pb.Height - bottom_border)

                        ElseIf (line_expression(new_x) < min_y And line_expression(next_x) > max_y) _
                            And Not isCurrentValueInfinity And Not isNextValueInfinity Then
                            canvas.DrawLine(p, x + 1, top_border, x, pb.Height - bottom_border)
                        End If
                    End If

                End If
            End If
        Next

    End Sub

    ''' <summary>
    ''' Sketches residuals on a curve
    ''' </summary>
    ''' <param name="canvas">The Graphic on which the graph is drawn</param>
    ''' <param name="pb">The PictureBox which the graph is based on</param>
    ''' <param name="line_expression">The expression of the line on which the residuals will be drawn</param>
    ''' <param name="x_var_name">The name of the variable on the x axis</param>
    ''' <param name="y_var_name">The name of the variable on the y axis</param>
    Sub SketchResidualLine(ByRef canvas As Graphics, ByVal pb As PictureBox, ByVal line_expression As Expression, _
                             Optional ByVal x_var_name As String = "Independent Variable", _
                             Optional ByVal y_var_name As String = "Dependent Variable")



        'defines canvas width and height
        Dim canvas_width As Integer = pb.Width - left_border - right_border
        Dim canvas_height As Integer = pb.Height - top_border - bottom_border

        'defines the pen used to draw the points
        Dim p As New Pen(Brushes.Black, line_weight)

        'draw residual lines
        For Each xy_coord As Coordinate In Points
            Dim y As Integer = y_toCoordinate(line_expression(xy_coord.x), pb)

            'if relevant portion of line is within range draw line from point to line
            If isInYRange(line_expression(xy_coord.x)) Then
                canvas.DrawLine(p, x_toCoordinate(xy_coord.x, pb), y, x_toCoordinate(xy_coord.x, pb), y_toCoordinate(xy_coord.y, pb))

                'if relevant portion of line is not within range draw line from point to relevant border
            ElseIf line_expression(xy_coord.x) > max_y Then
                canvas.DrawLine(p, x_toCoordinate(xy_coord.x, pb), top_border, x_toCoordinate(xy_coord.x, pb), y_toCoordinate(xy_coord.y, pb))
            ElseIf line_expression(xy_coord.x) < min_x Then
                canvas.DrawLine(p, x_toCoordinate(xy_coord.x, pb), pb.Height - bottom_border, x_toCoordinate(xy_coord.x, pb), y_toCoordinate(xy_coord.y, pb))
            End If
        Next

    End Sub
End Class