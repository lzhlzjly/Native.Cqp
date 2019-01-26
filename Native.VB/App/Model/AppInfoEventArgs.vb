Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Native.VB.App.Model
    Public Class AppInfoEventArgs
        Inherits EventArgs

        Public Property ApiVer As Integer
        Public Property AppId As String
    End Class
End Namespace
