Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Native.VB.App.Model
    Public Class GroupMemberAlterEventArgs
        Inherits EventArgs

        Public Property SendTime As DateTime
        Public Property FromGroup As Long
        Public Property FromQQ As Long
        Public Property BeingOperateQQ As Long
        Public Property Handled As Boolean
    End Class
End Namespace
