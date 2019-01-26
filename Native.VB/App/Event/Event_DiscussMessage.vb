Imports System
Imports Native.VB.Native.VB.App.Model

Namespace Native.Csharp.App.[Event]
    Public Class Event_DiscussMessage
        Private Shared _instance As Lazy(Of Event_DiscussMessage) = New Lazy(Of Event_DiscussMessage)(Function() New Event_DiscussMessage())

        Public Shared ReadOnly Property Instance As Event_DiscussMessage
            Get
                Return _instance.Value
            End Get
        End Property

        Private Sub New()
        End Sub

        Public Sub ReceiveDiscussMessage(ByVal sender As Object, ByVal e As DiscussMessageEventArgs)
            e.Handled = False
        End Sub
    End Class
End Namespace
