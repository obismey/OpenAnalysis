''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class Template

    Private _key As String
    Private _targetType As Type
    Private _type As TemplateType

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property TargetType() As Type
        Get
            Return _targetType
        End Get
        Set(ByVal value As Type)
            _targetType = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Type() As TemplateType
        Get
            Return _type
        End Get
        Set(ByVal value As TemplateType)
            _type = value
        End Set
    End Property

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

End Class

Public Enum TemplateType
    Data
    Control
End Enum