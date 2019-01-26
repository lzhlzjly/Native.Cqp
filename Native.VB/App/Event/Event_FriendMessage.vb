Imports System
Imports Native.VB.Native.VB.App.Model

Namespace Native.Csharp.App.[Event]
    Public Class Event_FriendMessage
        Private Shared ReadOnly _instance As Lazy(Of Event_FriendMessage) = New Lazy(Of Event_FriendMessage)(Function() New Event_FriendMessage())

        Public Shared ReadOnly Property Instance As Event_FriendMessage
            Get
                Return _instance.Value
            End Get
        End Property

        Private Sub New()
        End Sub

        Public Sub ReceiveFriendIncrease(ByVal sender As Object, ByVal e As FriendIncreaseEventArgs)
            e.Handled = False
        End Sub

        Public Sub ReceiveFriednAddRequest(ByVal sender As Object, ByVal e As FriendAddRequestEventArgs)
            e.Handled = False
        End Sub
    End Class
End Namespace
