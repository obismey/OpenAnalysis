Imports Microsoft.Xna.Framework

Public Class StackContainer
    Inherits ControlContainer

    Public Overrides Sub AddChild(ByVal child As Control)
        MyBase.AddChild(child)
    End Sub

    Protected Friend Overrides Function GetAllowedDrawRect(ByVal vis As Control) As Microsoft.Xna.Framework.Rectangle
        Dim index As Integer = Children.IndexOf(vis)

        If Direction = StackDirection.Vertical Then
            If index = 0 Then
                Return DimensionRect
            Else
                Dim prev As Control = Children(index - 1)
                Return New Rectangle(DimensionRect.X, _
                                     prev.DimensionRect.Bottom, _
                                     DimensionRect.Width, _
                                     DimensionRect.Height)
            End If

        ElseIf Direction = StackDirection.Horizontal Then
            If index = 0 Then
                Return DimensionRect
            Else
                Dim prev As Control = Children(index - 1)
                Return New Rectangle(prev.DimensionRect.Right, _
                                     DimensionRect.Y, _
                                     DimensionRect.Width, _
                                     DimensionRect.Height)
            End If
        End If
    End Function


    Private _wrap As Boolean
    Public Property Wrap() As Boolean
        Get
            Return _wrap
        End Get
        Set(ByVal value As Boolean)
            _wrap = value
        End Set
    End Property

    Private _direction As StackDirection
    Public Property Direction() As StackDirection
        Get
            Return _direction
        End Get
        Set(ByVal value As StackDirection)
            _direction = value
        End Set
    End Property

End Class

