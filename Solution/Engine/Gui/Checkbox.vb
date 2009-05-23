''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class Checkbox
    Inherits Control

    Private _ischecked As Boolean

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property IsChecked() As Boolean
        Get
            Return _ischecked
        End Get
        Set(ByVal value As Boolean)
            _ischecked = value
        End Set
    End Property



End Class
