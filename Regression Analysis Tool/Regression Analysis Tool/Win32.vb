Imports System.Runtime.InteropServices
''' <summary>
''' Win32 allows for the opening of the console window within a forms applications
''' This exists purely for testing purposes, and is not necessary for the running
''' of the completed program.
''' </summary>
Public Class Win32

    'Open Console

    ''' <summary>
    ''' Opens the console window
    ''' </summary>
    <DllImport("kernel32.dll")> Public Shared Function AllocConsole() As Boolean
    End Function

    'Close Console

    ''' <summary>
    ''' Closes the console window
    ''' </summary>
    <DllImport("kernel32.dll")> Public Shared Function FreeConsole() As Boolean
    End Function

End Class
