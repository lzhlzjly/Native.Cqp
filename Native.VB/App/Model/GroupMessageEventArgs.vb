Imports Native.Csharp.Sdk.Cqp.Model
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Native.VB.App.Model
    Public Class GroupMessageEventArgs
        Inherits EventArgs

        Public Property MsgId As Integer
        Public Property FromGroup As Long
        Public Property FromQQ As Long
        Public Property FromAnonymous As GroupAnonymous
        Public Property Msg As String
        Public Property Handled As Boolean
    End Class
End Namespace
