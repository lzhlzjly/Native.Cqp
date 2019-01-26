Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Native.VB.App.Model
    Public Class DiscussMessageEventArgs
        Inherits EventArgs

        Public Property MsgId As Integer
        Public Property FromDiscuss As Long
        Public Property FromQQ As Long
        Public Property Msg As String
        Public Property Handled As Boolean
    End Class
End Namespace
