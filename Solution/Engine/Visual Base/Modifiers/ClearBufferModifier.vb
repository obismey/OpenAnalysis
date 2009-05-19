Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics

Public Class ClearBufferModifier


    Private _color As Color?
    Public Property Color() As Color?
        Get
            Return _color
        End Get
        Set(ByVal value As Color?)
            _color = value
        End Set
    End Property


    Private _depth As Single?
    Public Property Depth() As Single?
        Get
            Return _depth
        End Get
        Set(ByVal value As Single?)
            _depth = value
        End Set
    End Property


    Private _stencil As Byte?
    Public Property Stencil() As Byte?
        Get
            Return _stencil
        End Get
        Set(ByVal value As Byte?)
            _stencil = value
        End Set
    End Property



    Private _enabled As Boolean
    Public Property Enabled() As Boolean
        Get
            Return _enabled
        End Get
        Set(ByVal value As Boolean)
            _enabled = value
        End Set
    End Property



    Shared ReadOnly Property Current() As ClearBufferModifier
        Get

        End Get
    End Property


End Class
