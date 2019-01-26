Imports Native.Csharp.Sdk.Cqp.Api
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Native.VB.App
    ''' <summary>
    ''' 用于存放 App 数据的公共类
    ''' </summary>
    Public Module Common

        ''' <summary>
        ''' 获取或设置 App 在运行期间所使用的数据路径
        ''' </summary>
        ''' <returns> App 运行期间的目录</returns>
        Public Property AppDirectory As String

        ''' <summary>
        ''' 获取或设置当前 App 是否处于运行状态
        ''' </summary>
        ''' <returns>运行状态</returns>
        Public Property IsRunning As Boolean

        ''' <summary>
        ''' 获取或设置当前 App 使用的 酷Q Api 接口实例
        ''' </summary>
        ''' <returns>Api 接口实例</returns>
        Public Property CqApi As CqApi
    End Module
End Namespace
