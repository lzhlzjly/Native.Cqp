Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Native.VB.App.Model
    Public Class GroupAddRequestEventArgs
        Inherits EventArgs

        Public Property SendTime As DateTime
        Public Property FromGroup As Long
        Public Property FromQQ As Long
        Public Property AppendMsg As String
        Public Property Tag As String
        Public Property Handled As Boolean
    End Class
End Namespace
