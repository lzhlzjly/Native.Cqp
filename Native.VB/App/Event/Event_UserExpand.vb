Imports System

Namespace Native.Csharp.App.[Event]
    Public Class Event_UserExpand
        Private Shared ReadOnly _instance As Lazy(Of Event_UserExpand) = New Lazy(Of Event_UserExpand)(Function() New Event_UserExpand())

        Public Shared ReadOnly Property Instance As Event_UserExpand
            Get
                Return _instance.Value
            End Get
        End Property

        Private Sub New()
        End Sub

        Public Sub OpenConsoleWindow(ByVal sender As Object, ByVal e As EventArgs)
        End Sub
    End Class
End Namespace
