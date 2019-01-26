Imports System
Imports System.Runtime.InteropServices
Imports Native.Csharp

Namespace Native.Csharp.App.Core
    Public Class UserExport
        Private Shared _instance As Lazy(Of UserExport) = New Lazy(Of UserExport)(Function() New UserExport())

        Public Shared ReadOnly Property Instance As UserExport
            Get
                Return _instance.Value
            End Get
        End Property

        Private Sub New()
        End Sub

        Public Shared Event UserOpenConsole As EventHandler(Of EventArgs)

        <DllExport(ExportName:="_eventOpenConsole", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EventOpenConsole() As Integer
            RaiseEvent UserOpenConsole(Instance, New EventArgs())
            Return 0
        End Function
    End Class
End Namespace
