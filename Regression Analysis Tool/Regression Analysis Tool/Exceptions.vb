''' <summary>
''' Creates an exception for no inverse of a matrix
''' </summary>
Public Class NoInverseException
    Inherits Exception
    Public Sub New()
    End Sub
    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
End Class

''' <summary>
''' Creates an exception for non-compatibility of matrices
''' </summary>
Public Class MatrixCompatibilityException
    Inherits Exception
    Public Sub New()
    End Sub
    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
End Class

''' <summary>
''' Creates an exception for csv exceptions
''' </summary>
Public Class CSVException
    Inherits Exception
    Public Sub New()
    End Sub
    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
End Class

''' <summary>
''' Creates an exception for invalid input into methods
''' </summary>
Public Class InvalidInputException
    Inherits Exception
    Public Sub New()
    End Sub
    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
End Class

''' <summary>
''' Creates an exception for invalid input into graphs
''' </summary>
Public Class ImproperGraphException
    Inherits Exception
    Public Sub New()
    End Sub
    Public Sub New(ByVal message As String)
        MyBase.New(message)
    End Sub
End Class