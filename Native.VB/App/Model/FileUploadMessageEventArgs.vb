Imports Native.Csharp.Sdk.Cqp.Model
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text

Namespace Native.VB.App.Model
    Public Class FileUploadMessageEventArgs
        Inherits EventArgs

        Public Property SendTime As DateTime
        Public Property FromGroup As Long
        Public Property FromQQ As Long
        Public Property File As GroupFile
        Public Property Handled As Boolean
    End Class
End Namespace
