Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class MouseEventArgs
    Inherits EventArgs

    Friend _bt As MouseButton
    Dim _swv As Integer
    Dim _swd As Integer


    Friend Sub New(ByVal _button As MouseButton, ByVal sv As Integer, ByVal sd As Integer)
        _bt = Button
        _swv = sv
        _swd = sd
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Button() As MouseButton
        Get
            Return _bt
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ScrollWheelValue() As Integer
        Get
            Return _swv
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ScrollWheelDelta() As Integer
        Get
            Return _swd
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="relativeTo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPosition(ByVal relativeTo As Control) As Vector2

    End Function
End Class


