Public Class OutlineNodeIconeAttribute
    Inherits BaseAttribute


    Private _state As String
    Public Property State() As String
        Get
            Return _state
        End Get
        Set(ByVal value As String)
            _state = value
        End Set
    End Property


    Private _icone As String
    Public Property Icone() As String
        Get
            Return _icone
        End Get
        Set(ByVal value As String)
            _icone = value
        End Set
    End Property




End Class
