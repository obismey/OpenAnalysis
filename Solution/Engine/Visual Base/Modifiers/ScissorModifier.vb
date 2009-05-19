Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Public Class ScissorModifier



    Private _rect As Rectangle?
    Public Property Rect() As Rectangle?
        Get
            Return _rect
        End Get
        Set(ByVal value As Rectangle?)
            _rect = value
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



    Shared ReadOnly Property Current() As ScissorModifier
        Get

        End Get
    End Property


End Class
