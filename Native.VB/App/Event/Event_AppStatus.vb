Imports System
Imports Native.VB.Native.VB.App

Namespace Native.Csharp.App.[Event]
    Public Class Event_AppStatus
        Private Shared ReadOnly _instance As Lazy(Of Event_AppStatus) = New Lazy(Of Event_AppStatus)(Function() New Event_AppStatus())

        Public Shared ReadOnly Property Instance As Event_AppStatus
            Get
                Return _instance.Value
            End Get
        End Property

        Private Sub New()
        End Sub

        Public Sub CqStartup(ByVal sender As Object, ByVal e As EventArgs)
            Common.AppDirectory = Common.CqApi.GetAppDirectory()
        End Sub

        Public Sub CqExit(ByVal sender As Object, ByVal e As EventArgs)
        End Sub

        Public Sub AppEnable(ByVal sender As Object, ByVal e As EventArgs)
            Common.IsRunning = True
        End Sub

        Public Sub AppDisable(ByVal sender As Object, ByVal e As EventArgs)
            Common.IsRunning = False
        End Sub
    End Class
End Namespace
