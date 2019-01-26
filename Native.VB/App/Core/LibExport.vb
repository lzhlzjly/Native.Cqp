Imports System
Imports System.Runtime.InteropServices
Imports System.Text
Imports Native.Csharp
Imports Native.Csharp.Sdk.Cqp.[Enum]
Imports Native.Csharp.Tool
Imports Native.VB.Native.Csharp.App.Event
Imports Native.VB.Native.VB.App
Imports Native.VB.Native.VB.App.Event
Imports Native.VB.Native.VB.App.Model

Namespace Native.Csharp.App.Core
    Public Class LibExport
        Private Shared _instance As Lazy(Of LibExport) = New Lazy(Of LibExport)(Function() New LibExport())

        Public Shared ReadOnly Property Instance As LibExport
            Get
                Return _instance.Value
            End Get
        End Property

        Private Sub New()

        End Sub

        Public Shared Event AppInfoEventHandler As EventHandler(Of AppInfoEventArgs)
        Public Shared Event AppInitializeEventHandler As EventHandler(Of AppInitializeEventArgs)
        Public Shared Event CqStartup As EventHandler(Of EventArgs)
        Public Shared Event CqExit As EventHandler(Of EventArgs)
        Public Shared Event AppEnable As EventHandler(Of EventArgs)
        Public Shared Event AppDisable As EventHandler(Of EventArgs)
        Public Shared Event ReceiveFriendMessage As EventHandler(Of PrivateMessageEventArgs)
        Public Shared Event ReceiveQnlineStatusMessage As EventHandler(Of PrivateMessageEventArgs)
        Public Shared Event ReceiveGroupPrivateMessage As EventHandler(Of PrivateMessageEventArgs)
        Public Shared Event ReceiveDiscussPrivateMessage As EventHandler(Of PrivateMessageEventArgs)
        Public Shared Event ReceiveGroupMessage As EventHandler(Of GroupMessageEventArgs)
        Public Shared Event ReceiveDiscussMessage As EventHandler(Of DiscussMessageEventArgs)
        Public Shared Event ReceiveFileUploadMessage As EventHandler(Of FileUploadMessageEventArgs)
        Public Shared Event ReceiveManageIncrease As EventHandler(Of GroupManageAlterEventArgs)
        Public Shared Event ReceiveManageDecrease As EventHandler(Of GroupManageAlterEventArgs)
        Public Shared Event ReceiveMemberLeave As EventHandler(Of GroupMemberAlterEventArgs)
        Public Shared Event ReceiveMemberRemove As EventHandler(Of GroupMemberAlterEventArgs)
        Public Shared Event ReceiveMemberJoin As EventHandler(Of GroupMemberAlterEventArgs)
        Public Shared Event ReceiveMemberInvitee As EventHandler(Of GroupMemberAlterEventArgs)
        Public Shared Event ReceiveFriendIncrease As EventHandler(Of FriendIncreaseEventArgs)
        Public Shared Event ReceiveFriendAdd As EventHandler(Of FriendAddRequestEventArgs)
        Public Shared Event ReceiveGroupAddApply As EventHandler(Of GroupAddRequestEventArgs)
        Public Shared Event ReceiveGroupAddInvitee As EventHandler(Of GroupAddRequestEventArgs)

        <DllExport(ExportName:="AppInfo", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function AppInfo() As String
            Dim args As AppInfoEventArgs = New AppInfoEventArgs()
            RaiseEvent AppInfoEventHandler(Instance, args)
            Event_AppInitialize.Instance.AppInfo(Instance, args)
            Return String.Format("{0},{1}", args.ApiVer, args.AppId)
        End Function

        <DllExport(ExportName:="Initialize", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function Initialize(ByVal authCode As Integer) As Integer
            Dim args As AppInitializeEventArgs = New AppInitializeEventArgs()
            args.AuthCode = authCode
            RaiseEvent AppInitializeEventHandler(Instance, args)
            Event_AppInitialize.Instance.Initialize(Instance, args)
            Return 0
        End Function

        <DllExport(ExportName:="_eventStartup", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EventStartUp() As Integer
            RaiseEvent CqStartup(Instance, New EventArgs())
            Event_AppStatus.Instance.CqStartup(Instance, New EventArgs())
            Return 0
        End Function

        <DllExport(ExportName:="_eventExit", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EventExit() As Integer
            RaiseEvent CqExit(Instance, New EventArgs())
            Event_AppStatus.Instance.CqExit(Instance, New EventArgs())
            Return 0
        End Function

        <DllExport(ExportName:="_eventEnable", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EventEnable() As Integer
            RaiseEvent AppEnable(Instance, New EventArgs())
            Event_AppStatus.Instance.AppEnable(Instance, New EventArgs())
            Return 0
        End Function

        <DllExport(ExportName:="_eventDisable", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EventDisable() As Integer
            RaiseEvent AppDisable(Instance, New EventArgs())
            Event_AppStatus.Instance.AppDisable(Instance, New EventArgs())
            Return 0
        End Function

        <DllExport(ExportName:="_eventPrivateMsg", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EventPrivateMsg(ByVal subType As Integer, ByVal msgId As Integer, ByVal fromQQ As Long, ByVal msg As IntPtr, ByVal font As Integer) As Integer
            Dim args As PrivateMessageEventArgs = New PrivateMessageEventArgs()
            args.MsgId = msgId
            args.FromQQ = fromQQ
            args.Msg = NativeConvert.ToPtrString(msg, Encoding.GetEncoding("GB18030"))
            args.Handled = False

            Select Case subType
                Case 11
                    RaiseEvent ReceiveFriendMessage(Instance, args)
                    Event_PrivateMessage.Instance.ReceiveFriendMessage(Instance, args)
                Case 1
                    RaiseEvent ReceiveQnlineStatusMessage(Instance, args)
                    Event_PrivateMessage.Instance.ReceiveOnlineStatusMessage(Instance, args)
                Case 2
                    RaiseEvent ReceiveGroupPrivateMessage(Instance, args)
                    Event_PrivateMessage.Instance.ReceiveGroupPrivateMessage(Instance, args)
                Case 3
                    RaiseEvent ReceiveDiscussPrivateMessage(Instance, args)
                    Event_PrivateMessage.Instance.ReceiveDiscussPrivateMessage(Instance, args)
                Case Else
                    Common.CqApi.AddLoger(LogerLevel.Info, "提示", "新私聊类型, 请反馈给开发者或自行添加!")
            End Select

            Return CInt((If(args.Handled, MessageHanding.Intercept, MessageHanding.Ignored)))
        End Function

        <DllExport(ExportName:="_eventGroupMsg", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EventGroupMsg(ByVal subType As Integer, ByVal msgId As Integer, ByVal fromGroup As Long, ByVal fromQQ As Long, ByVal fromAnonymous As String, ByVal msg As IntPtr, ByVal font As Integer) As Integer
            Dim args As GroupMessageEventArgs = New GroupMessageEventArgs()
            args.MsgId = msgId
            args.FromGroup = fromGroup
            args.FromQQ = fromQQ
            args.Msg = NativeConvert.ToPtrString(msg, Encoding.GetEncoding("GB18030"))
            args.Handled = False

            If fromQQ = 80000000 AndAlso Not String.IsNullOrEmpty(fromAnonymous) Then
                args.FromAnonymous = Common.CqApi.GetAnonymous(fromAnonymous)
            Else
                args.FromAnonymous = Nothing
            End If

            Select Case subType
                Case 1
                    RaiseEvent ReceiveGroupMessage(Instance, args)
                    Event_GroupMessage.Instance.ReceiveGroupMessage(Instance, args)
                Case Else
                    Common.CqApi.AddLoger(LogerLevel.Info, "提示", "新群消息类型, 请反馈给开发者或自行添加!")
            End Select

            Return CInt((If(args.Handled, MessageHanding.Intercept, MessageHanding.Ignored)))
        End Function

        <DllExport(ExportName:="_eventDiscussMsg", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EventDiscussMsg(ByVal subType As Integer, ByVal msgId As Integer, ByVal fromDiscuss As Long, ByVal fromQQ As Long, ByVal msg As IntPtr, ByVal font As Integer) As Integer
            Dim args As DiscussMessageEventArgs = New DiscussMessageEventArgs()
            args.MsgId = msgId
            args.FromDiscuss = fromDiscuss
            args.FromQQ = fromQQ
            args.Msg = NativeConvert.ToPtrString(msg, Encoding.GetEncoding("GB18030"))
            args.Handled = False

            Select Case subType
                Case 1
                    RaiseEvent ReceiveDiscussMessage(Instance, args)
                    Event_DiscussMessage.Instance.ReceiveDiscussMessage(Instance, args)
                Case Else
                    Common.CqApi.AddLoger(LogerLevel.Info, "提示", "新讨论组消息类型, 请反馈给开发者或自行添加!")
            End Select

            Return CInt((If(args.Handled, MessageHanding.Intercept, MessageHanding.Ignored)))
        End Function

        <DllExport(ExportName:="_eventGroupUpload", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EventGroupUpload(ByVal subType As Integer, ByVal sendTime As Integer, ByVal fromGroup As Long, ByVal fromQQ As Long, ByVal file As String) As Integer
            Dim args As FileUploadMessageEventArgs = New FileUploadMessageEventArgs()
            args.SendTime = NativeConvert.FotmatUnixTime(sendTime.ToString())
            args.FromGroup = fromGroup
            args.FromQQ = fromQQ
            args.File = Common.CqApi.GetFile(file)
            RaiseEvent ReceiveFileUploadMessage(Instance, args)
            Event_GroupMessage.Instance.ReceiveGroupFileUpload(Instance, args)
            Return CInt((If(args.Handled, MessageHanding.Intercept, MessageHanding.Ignored)))
        End Function

        <DllExport(ExportName:="_eventSystem_GroupAdmin", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EventSystemGroupAdmin(ByVal subType As Integer, ByVal sendTime As Integer, ByVal fromGroup As Long, ByVal beingOperateQQ As Long) As Integer
            Dim args As GroupManageAlterEventArgs = New GroupManageAlterEventArgs()
            args.SendTime = NativeConvert.FotmatUnixTime(sendTime.ToString())
            args.FromGroup = fromGroup
            args.BeingOperateQQ = beingOperateQQ
            args.Handled = False

            Select Case subType
                Case 1
                    RaiseEvent ReceiveManageDecrease(Instance, args)
                    Event_GroupMessage.Instance.ReceiveGroupManageDecrease(Instance, args)
                Case 2
                    RaiseEvent ReceiveManageIncrease(Instance, args)
                    Event_GroupMessage.Instance.ReceiveGroupManageIncrease(Instance, args)
                Case Else
                    Common.CqApi.AddLoger(LogerLevel.Info, "提示", "新管理事件类型, 请反馈给开发者或自行添加!")
            End Select

            Return CInt((If(args.Handled, MessageHanding.Intercept, MessageHanding.Ignored)))
        End Function

        <DllExport(ExportName:="_eventSystem_GroupMemberDecrease", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EventSystemGroupMemberDecrease(ByVal subType As Integer, ByVal sendTime As Integer, ByVal fromGroup As Long, ByVal fromQQ As Long, ByVal beingOperateQQ As Long) As Integer
            Dim args As GroupMemberAlterEventArgs = New GroupMemberAlterEventArgs()
            args.SendTime = NativeConvert.FotmatUnixTime(sendTime.ToString())
            args.FromGroup = fromGroup
            args.FromQQ = fromQQ
            args.BeingOperateQQ = beingOperateQQ
            args.Handled = False

            Select Case subType
                Case 1
                    fromQQ = beingOperateQQ
                    RaiseEvent ReceiveMemberLeave(Instance, args)
                    Event_GroupMessage.Instance.ReceiveGroupMemberLeave(Instance, args)
                Case 2
                    RaiseEvent ReceiveMemberRemove(Instance, args)
                    Event_GroupMessage.Instance.ReceiveGroupMemberRemove(Instance, args)
                Case Else
                    Common.CqApi.AddLoger(LogerLevel.Info, "提示", "新群成员减少事件类型, 请反馈给开发者或自行添加!")
            End Select

            Return CInt((If(args.Handled, MessageHanding.Intercept, MessageHanding.Ignored)))
        End Function

        <DllExport(ExportName:="_eventSystem_GroupMemberIncrease", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EventSystemGroupMemberIncrease(ByVal subType As Integer, ByVal sendTime As Integer, ByVal fromGroup As Long, ByVal fromQQ As Long, ByVal beingOperateQQ As Long) As Integer
            Dim args As GroupMemberAlterEventArgs = New GroupMemberAlterEventArgs()
            args.SendTime = NativeConvert.FotmatUnixTime(sendTime.ToString())
            args.FromGroup = fromGroup
            args.FromQQ = fromQQ
            args.BeingOperateQQ = beingOperateQQ
            args.Handled = False

            Select Case subType
                Case 1
                    RaiseEvent ReceiveMemberJoin(Instance, args)
                    Event_GroupMessage.Instance.ReceiveGroupMemberJoin(Instance, args)
                Case 2
                    RaiseEvent ReceiveMemberInvitee(Instance, args)
                    Event_GroupMessage.Instance.ReceiveGroupMemberInvitee(Instance, args)
                Case Else
                    Common.CqApi.AddLoger(LogerLevel.Info, "提示", "新群成员增加事件类型, 请反馈给开发者或自行添加!")
            End Select

            Return CInt((If(args.Handled, MessageHanding.Intercept, MessageHanding.Ignored)))
        End Function

        <DllExport(ExportName:="_eventFriend_Add", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EventFriendAdd(ByVal subType As Integer, ByVal sendTime As Integer, ByVal fromQQ As Long) As Integer
            Dim args As FriendIncreaseEventArgs = New FriendIncreaseEventArgs()
            args.SendTime = NativeConvert.FotmatUnixTime(sendTime.ToString())
            args.FromQQ = fromQQ
            args.Handled = False

            Select Case subType
                Case 1
                    RaiseEvent ReceiveFriendIncrease(Instance, args)
                    Event_FriendMessage.Instance.ReceiveFriendIncrease(Instance, args)
                Case Else
                    Common.CqApi.AddLoger(LogerLevel.Info, "提示", "新好友事件类型, 请反馈给开发者或自行添加!")
            End Select

            Return CInt((If(args.Handled, MessageHanding.Intercept, MessageHanding.Ignored)))
        End Function

        <DllExport(ExportName:="_eventRequest_AddFriend", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EventRequestAddFriend(ByVal subType As Integer, ByVal sendTime As Integer, ByVal fromQQ As Long, ByVal msg As IntPtr, ByVal responseFlag As String) As Integer
            Dim args As FriendAddRequestEventArgs = New FriendAddRequestEventArgs()
            args.SendTime = NativeConvert.FotmatUnixTime(sendTime.ToString())
            args.FromQQ = fromQQ
            args.AppendMsg = NativeConvert.ToPtrString(msg, Encoding.GetEncoding("GB18030"))
            args.Tag = responseFlag
            args.Handled = False

            Select Case subType
                Case 1
                    RaiseEvent ReceiveFriendAdd(Instance, args)
                    Event_FriendMessage.Instance.ReceiveFriednAddRequest(Instance, args)
                Case Else
                    Common.CqApi.AddLoger(LogerLevel.Info, "提示", "新好友添加请求事件类型, 请反馈给开发者或自行添加!")
            End Select

            Return CInt((If(args.Handled, MessageHanding.Intercept, MessageHanding.Ignored)))
        End Function

        <DllExport(ExportName:="_eventRequest_AddGroup", CallingConvention:=CallingConvention.StdCall)>
        Private Shared Function EventRequestAddGroup(ByVal subType As Integer, ByVal sendTime As Integer, ByVal fromGroup As Long, ByVal fromQQ As Long, ByVal msg As IntPtr, ByVal responseFlag As String) As Integer
            Dim args As GroupAddRequestEventArgs = New GroupAddRequestEventArgs()
            args.SendTime = NativeConvert.FotmatUnixTime(sendTime.ToString())
            args.FromGroup = fromGroup
            args.FromQQ = fromQQ
            args.AppendMsg = NativeConvert.ToPtrString(msg, Encoding.GetEncoding("GB18030"))
            args.Tag = responseFlag
            args.Handled = False

            Select Case subType
                Case 1
                    RaiseEvent ReceiveGroupAddApply(Instance, args)
                    Event_GroupMessage.Instance.ReceiveGroupAddApply(Instance, args)
                Case 2
                    RaiseEvent ReceiveGroupAddInvitee(Instance, args)
                    Event_GroupMessage.Instance.ReceiveGroupAddInvitee(Instance, args)
                Case Else
                    Common.CqApi.AddLoger(LogerLevel.Info, "提示", "新群添加请求事件类型, 请反馈给开发者或自行添加!")
            End Select

            Return 0
        End Function
    End Class
End Namespace
