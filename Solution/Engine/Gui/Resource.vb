Imports System.Collections.Generic

Public Class Resource

    Private _key As String
    Private _params As New List(Of Param)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Key() As String
        Get
            Return _key
        End Get
        Set(ByVal value As String)
            _key = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Params() As IEnumerable(Of Param)
        Get
            Return _params
        End Get
    End Property

   

    Public Class Param
        Public Name As String
        Public value As Object
    End Class
End Class
