''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class Button
    Inherits Control

    Private _commandArgument As Object
    Private _commandName As String
    Private _isPressed As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsPressed() As Boolean
        Get
            Return _isPressed
        End Get
        Set(ByVal value As Boolean)
            _isPressed = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CommandName() As String
        Get
            Return _commandName
        End Get
        Set(ByVal value As String)
            _commandName = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property CommandArgument() As Object
        Get
            Return _commandName
        End Get
        Set(ByVal value As Object)
            _commandName = CStr(value)
        End Set
    End Property


End Class


