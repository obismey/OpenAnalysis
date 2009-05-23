Imports Microsoft.Xna.Framework.Input

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class KeyboardEventArgs
    Inherits EventArgs

    Dim _key As Keys
    Dim _alt, _ctrl, _shift As Boolean

    Friend Sub New(ByVal k As Keys, ByVal ParamArray acs() As Boolean)
        _key = Keys.K
        Select Case acs.Length
            Case 1
                _alt = acs(0)
            Case 2
                _alt = acs(0)
                _ctrl = acs(1)
            Case 3
                _alt = acs(0)
                _ctrl = acs(1)
                _shift = acs(2)
        End Select
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Key() As Keys
        Get
            Return _key
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Alt() As Boolean
        Get
            Return _alt
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Shift() As Boolean
        Get
            Return _shift
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Ctrl() As Boolean
        Get
            Return _ctrl
        End Get
    End Property

End Class

Public Delegate Sub KeyboardEventHandler(ByVal sender As Object, ByVal e As KeyboardEventArgs)
