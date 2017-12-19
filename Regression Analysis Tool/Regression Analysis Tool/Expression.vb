''' <summary>
''' Represents a mathematical expression
''' </summary>
Public Class Expression

    Private e As String

    ''' <summary>
    ''' Represents a mathematical expression
    ''' </summary>
    ''' <value>The string to be input as the value</value>
    WriteOnly Property expression As String
        Set(value As String)
            e = value
        End Set
    End Property

    'Initialisation

    ''' <summary>
    ''' Creates a new expression
    ''' </summary>
    Sub New()
    End Sub

    ''' <summary>
    ''' Defines a new expression with an input string
    ''' </summary>
    ''' <param name="exp">The string to be used as the input</param>
    Sub New(ByVal exp As String)
        'expression becomes correctly formatted, or "legitimised"
        expression = Legitimise(exp)
    End Sub

    'Accessory Functions

    ''' <summary>
    ''' Converts a string into a string with every separate variable delimited by a single space
    ''' </summary>
    ''' <param name="input">The input string to be converted</param>
    ''' <returns>Returns a string with every separate variable delimited by a single space</returns>
    Function Legitimise(ByVal input As String) As String
        input = input.Replace(" ", "")
        Legitimise = ""

        'adds a space between every operator
        For i As Integer = 0 To input.Length - 1

            If (i + 2) < input.Length AndAlso (input(i) + input(i + 1) + input(i + 2)) = "mod" Then
                Legitimise = Legitimise + " mod "
                i = i + 2

            ElseIf (input(i) = "-") And i < input.Length AndAlso isNumber(input(i + 1)) Then
                Legitimise = Legitimise + " " + input(i)

            ElseIf (isOperator(input(i)) Or isClosedParentheses(input(i)) Or isOpenParentheses(input(i))) Then
                Legitimise = Legitimise + " " + input(i) + " "

            Else
                Legitimise = Legitimise + input(i)
            End If
        Next

        'Removes all the extra spaces
        Do While Legitimise.Contains("  ")
            Legitimise = Legitimise.Replace("  ", " ")
        Loop

        'trims leading zeros
        Legitimise = Legitimise.Trim(" ")
    End Function

    ''' <summary>
    ''' Returns the relative precedence level of an operator
    ''' </summary>
    ''' <param name="symbol">The symbol for which the precedence level will be found</param>
    ''' <returns>Returns the relative precedence level of an operator</returns>
    Function PrecedenceLevel(ByVal symbol As String) As Integer
        Select Case symbol
            Case "^"
                Return 3
            Case "*"
                Return 2
            Case "/"
                Return 2
            Case "mod"
                Return 2
            Case "+"
                Return 1
            Case "-"
                Return 1
            Case Else
                Return 0
        End Select
    End Function

    'Booleans

    ''' <summary>
    ''' Returns a boolean expression determining whether an input is an operator
    ''' </summary>
    ''' <param name="input">The input string</param>
    ''' <returns>Returns a boolean expression determining whether an input is an operator</returns>
    Function isOperator(ByVal input As String) As Boolean
        If input = "+" Or _
            input = "-" Or _
            input = "mod" Or _
            input = "*" Or _
            input = "/" Or _
            input = "^" Then
            isOperator = True
        Else
            isOperator = False
        End If
    End Function

    ''' <summary>
    ''' Returns a boolean expression determining whether an input is a number
    ''' </summary>
    ''' <param name="input">The input string</param>
    ''' <returns>Returns a boolean expression determining whether an input is a number</returns>
    Function isNumber(ByVal input As String) As Boolean
        Select Case input
            Case 0 To 9
                Return True
            Case "x"
                Return True
            Case Else
                Return False
        End Select
    End Function

    ''' <summary>
    ''' Returns a boolean expression determining whether an input is an operand
    ''' </summary>
    ''' <param name="input">The input string</param>
    ''' <returns>Returns a boolean expression determining whether an input is an operand</returns>
    Function isOperand(ByVal input As String) As Boolean
        If input = "+" Or _
            input = "-" Or _
            input = "*" Or _
            input = "/" Or _
            input = "^" Or _
            input = "mod" Or _
            input = "(" Or _
            input = ")" Then
            isOperand = False
        Else
            isOperand = True
        End If
    End Function

    ''' <summary>
    ''' Returns a boolean expression determining whether an input is an opening parenthesis
    ''' </summary>
    ''' <param name="input">The input string</param>
    ''' <returns>Returns a boolean expression determining whether an input is an opening parenthesis</returns>
    Function isOpenParentheses(ByVal input As String)
        If input = "(" Or _
            input = "{" Or _
            input = "[" Then
            isOpenParentheses = True
        Else
            isOpenParentheses = False
        End If
    End Function

    ''' <summary>
    ''' Returns a boolean expression determining whether an input is a closing parenthesis
    ''' </summary>
    ''' <param name="input">The input string</param>
    ''' <returns>Returns a boolean expression determining whether an input is a closing parenthesis</returns>
    Function isClosedParentheses(ByVal input As String)
        If input = ")" Or _
            input = "}" Or _
            input = "]" Then
            isClosedParentheses = True
        Else
            isClosedParentheses = False
        End If
    End Function

    'Expression Functions

    ''' <summary>
    ''' Converts an infix expression into an RPN expression
    ''' </summary>
    ''' <param name="exp">The input string to be converted</param>
    ''' <returns>Returns an RPN expression</returns>
    Function InfixToRPN(ByVal exp As String) As String
        Dim s As New Stack(Of String)
        Dim words() As String = exp.Split(" ")
        Dim res As String = ""

        For i As Integer = 0 To words.Length - 1
            'if item is a number then concatenate to result
            If isOperand(words(i)) Or words(i) = "!" Then
                res = res & " " & words(i)

                'if item is an operator then pop all operators of higher precedence from the stack, then push the operator onto the stack
            ElseIf isOperator(words(i)) Then
                While (s.Count <> 0 AndAlso Not isOpenParentheses(s.Peek) AndAlso (PrecedenceLevel(s.Peek()) >= PrecedenceLevel(words(i))))
                    res = res & " " & s.Pop()
                End While
                s.Push(words(i))

                'if item is an open parentheses, push onto stack
            ElseIf isOpenParentheses(words(i)) Then
                s.Push(words(i))

                'if item is a closed parentheses, pop all items from the stack
                'and concatenate with the RPN expression until an open parenthesis is encountered
            ElseIf isClosedParentheses(words(i)) Then
                While (s.Count <> 0 AndAlso Not isOpenParentheses(s.Peek) AndAlso (PrecedenceLevel(s.Peek()) >= PrecedenceLevel(words(i))))
                    res = res & " " & s.Pop
                End While
                s.Pop()

            End If
        Next

        'pop remaining items from the stack
        Do While s.Count <> 0
            res = res & " " & s.Pop
        Loop

        'Removes all the extra spaces
        Do While res.Contains("  ")
            res = res.Replace("  ", " ")
        Loop

        'removes leading and trailing spaces
        Return res.Trim(" ")
    End Function

    ''' <summary>
    ''' Evaluates an expression relative to x
    ''' </summary>
    ''' <param name="x">The parameter of the expression</param>
    ''' <returns>Returns an evaluated expression</returns>
    Function Evaluate(ByVal x As Double) As Double
        'converts infix expression into RPN
        'creates an array of all the values and operators
        Dim words() As String = InfixToRPN(Me.e).Split(" ")

        'evaluates rpn expression
        'creates a stack for the process
        Dim s As New Stack

        'if s is a value add to the stack
        'if s is an operator then do that operator to the last 2 values
        For Each w As String In words
            Select Case w
                'standard mathematical functions
                Case "+"
                    Dim temp2 As Double = s.Pop()
                    Dim temp1 As Double = s.Pop()
                    s.Push(temp1 + temp2)

                Case "−", "-"
                    Dim temp2 As Double = s.Pop()
                    If s.Count = 0 Then
                        s.Push(-temp2)
                    Else
                        Dim temp1 As Double = s.Pop()
                        s.Push(temp1 - temp2)
                    End If

                Case "*", "×"
                    Dim temp2 As Double = s.Pop()
                    Dim temp1 As Double = s.Pop()
                    s.Push(temp1 * temp2)

                Case "/"
                    Dim temp2 As Double = s.Pop()
                    Dim temp1 As Double = s.Pop()
                    s.Push(temp1 / temp2)

                Case "^"
                    Dim temp2 As Double = s.Pop()
                    Dim temp1 As Double = s.Pop()
                    s.Push(temp1 ^ temp2)

                Case "mod"
                    Dim temp2 As Double = s.Pop()
                    Dim temp1 As Double = s.Pop()
                    s.Push(temp1 Mod temp2)

                Case "x"
                    'handles the algebraic substitution of x
                    s.Push(x)

                Case "-x"
                    'deals with unary minus on x
                    s.Push(-x)

                Case "e"
                    s.Push(Math.E)

                Case Else
                    s.Push(w)

            End Select
        Next

        'return the top item in the stack, which is also the result
        If s.Count = 1 Then
            Return s.Pop
        Else
            Throw New System.Exception
        End If
    End Function

    ''' <summary>
    ''' Returns the evaluation of the expression, where x is an input value
    ''' </summary>
    ''' <param name="x">The parameter of the expression</param>
    Default ReadOnly Property ReturnEvaluation(ByVal x As Double) As Double
        Get
            Return Me.Evaluate(x)
        End Get
    End Property

    'Type Conversion

    ''' <summary>
    ''' Defines conversion from expression to string
    ''' </summary>
    ''' <param name="f">The function for which the string is returned from</param>
    ''' <returns>Returns a string from an expression</returns>
    Public Shared Widening Operator CType(ByVal f As Expression) As String
        Return f.e
    End Operator

    ''' <summary>
    ''' Defines conversion from string to expression
    ''' </summary>
    ''' <param name="s">The string to be converted into an expression</param>
    ''' <returns>Returns an expression from an input string</returns>
    Public Shared Narrowing Operator CType(ByVal s As String) As Expression
        Return New Expression(s)
    End Operator
End Class
