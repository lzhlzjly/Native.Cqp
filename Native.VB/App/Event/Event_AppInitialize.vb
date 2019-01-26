Imports System
Imports System.Text
Imports Native.Csharp
Imports Native.Csharp.Sdk.Cqp.Api
Imports Native.VB.Native.Csharp.App.Core
Imports Native.VB.Native.VB.App.Model

Namespace Native.VB.App.[Event]
    Public Class Event_AppInitialize
        Private Shared ReadOnly _instance As Lazy(Of Event_AppInitialize) = New Lazy(Of Event_AppInitialize)(Function() New Event_AppInitialize())

        Public Shared ReadOnly Property Instance As Event_AppInitialize
            Get
                Return _instance.Value
            End Get
        End Property

        Private Sub New()
            'AddHandler LibExport.AppInfoEventHandler, AddressOf AppInfo
        End Sub

        Public Sub AppInfo(ByVal sender As Object, ByVal e As AppInfoEventArgs)
            e.ApiVer = 9
            e.AppId = "native.vb"
        End Sub

        Public Sub Initialize(ByVal sender As Object, ByVal e As AppInitializeEventArgs)
            Common.CqApi = New CqApi(e.AuthCode)
            AddHandler AppDomain.CurrentDomain.UnhandledException, AddressOf CurrentDomain_UnhandledException
        End Sub

        Private Sub CurrentDomain_UnhandledException(ByVal sender As Object, ByVal e As UnhandledExceptionEventArgs)
            Dim ex As Exception = TryCast(e.ExceptionObject, Exception)

            If ex IsNot Nothing Then
                Dim innerLog As StringBuilder = New StringBuilder()
                innerLog.AppendLine("NativeSDK 异常")
                innerLog.AppendFormat("[异常名称]: {0}{1}", ex.Source.ToString(), Environment.NewLine)
                innerLog.AppendFormat("[异常消息]: {0}{1}", ex.Message, Environment.NewLine)
                innerLog.AppendFormat("[异常堆栈]: {0}{1}", ex.StackTrace)

                If e.IsTerminating Then
                    Common.CqApi.AddFatalError(innerLog.ToString())
                Else
                    Common.CqApi.AddLoger(Sdk.Cqp.[Enum].LogerLevel.[Error], "Native 异常", innerLog.ToString())
                End If
            End If
        End Sub
    End Class
End Namespace
