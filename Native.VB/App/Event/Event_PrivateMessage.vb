Imports System
Imports Native.VB.Native.VB.App
Imports Native.VB.Native.VB.App.Model

Namespace Native.Csharp.App.[Event]
    Public Class Event_PrivateMessage
        Private Shared ReadOnly _instance As Lazy(Of Event_PrivateMessage) = New Lazy(Of Event_PrivateMessage)(Function() New Event_PrivateMessage())

        Public Shared ReadOnly Property Instance As Event_PrivateMessage
            Get
                Return _instance.Value
            End Get
        End Property

        Private Sub New()
        End Sub

        Public Sub ReceiveFriendMessage(ByVal sender As Object, ByVal e As PrivateMessageEventArgs)
            Common.CqApi.SendPrivateMessage(e.FromQQ, Common.CqApi.CqCode_At(e.FromQQ) & "你发送了这样的消息:" & e.Msg)
            e.Handled = True
        End Sub

        Public Sub ReceiveOnlineStatusMessage(ByVal sender As Object, ByVal e As PrivateMessageEventArgs)
            e.Handled = False
        End Sub

        Public Sub ReceiveGroupPrivateMessage(ByVal sender As Object, ByVal e As PrivateMessageEventArgs)
            e.Handled = False
        End Sub

        Public Sub ReceiveDiscussPrivateMessage(ByVal sender As Object, ByVal e As PrivateMessageEventArgs)
            e.Handled = False
        End Sub
    End Class
End Namespace
