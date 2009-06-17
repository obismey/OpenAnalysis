Public Class EditorAttribute
    Inherits BaseAttribute



    Private _assembly As String
    Public Property Assembly() As String
        Get
            Return _assembly
        End Get
        Set(ByVal value As String)
            _assembly = value
        End Set
    End Property


    Private _classname As String
    Public Property ClassName() As String
        Get
            Return _classname
        End Get
        Set(ByVal value As String)
            _classname = value
        End Set
    End Property




End Class
