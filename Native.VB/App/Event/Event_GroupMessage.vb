Imports System
Imports Native.VB.Native.VB.App
Imports Native.VB.Native.VB.App.Model

Namespace Native.Csharp.App.[Event]
    Public Class Event_GroupMessage
        Private Shared _instance As Lazy(Of Event_GroupMessage) = New Lazy(Of Event_GroupMessage)(Function() New Event_GroupMessage())

        Public Shared ReadOnly Property Instance As Event_GroupMessage
            Get
                Return _instance.Value
            End Get
        End Property

        Private Sub New()
        End Sub

        Public Sub ReceiveGroupMessage(ByVal sender As Object, ByVal e As GroupMessageEventArgs)
            If e.FromAnonymous IsNot Nothing Then
                Common.CqApi.SendGroupMessage(e.FromGroup, e.FromAnonymous.CodeName & " 你发送了这样的消息: " & e.Msg)
                e.Handled = True
                Return
            End If

            Common.CqApi.SendGroupMessage(e.FromGroup, Common.CqApi.CqCode_At(e.FromQQ) & "你发送了这样的消息: " & e.Msg)
            e.Handled = True
        End Sub

        Public Sub ReceiveGroupFileUpload(ByVal sender As Object, ByVal e As FileUploadMessageEventArgs)
            e.Handled = False
        End Sub

        Public Sub ReceiveGroupManageIncrease(ByVal sender As Object, ByVal e As GroupManageAlterEventArgs)
            e.Handled = False
        End Sub

        Public Sub ReceiveGroupManageDecrease(ByVal sender As Object, ByVal e As GroupManageAlterEventArgs)
            e.Handled = False
        End Sub

        Public Sub ReceiveGroupMemberJoin(ByVal sender As Object, ByVal e As GroupMemberAlterEventArgs)
            e.Handled = False
        End Sub

        Public Sub ReceiveGroupMemberInvitee(ByVal sender As Object, ByVal e As GroupMemberAlterEventArgs)
            e.Handled = False
        End Sub

        Public Sub ReceiveGroupMemberLeave(ByVal sender As Object, ByVal e As GroupMemberAlterEventArgs)
            e.Handled = False
        End Sub

        Public Sub ReceiveGroupMemberRemove(ByVal sender As Object, ByVal e As GroupMemberAlterEventArgs)
            e.Handled = False
        End Sub

        Public Sub ReceiveGroupAddApply(ByVal sender As Object, ByVal e As GroupAddRequestEventArgs)
            e.Handled = False
        End Sub

        Public Sub ReceiveGroupAddInvitee(ByVal sender As Object, ByVal e As GroupAddRequestEventArgs)
            e.Handled = False
        End Sub
    End Class
End Namespace
