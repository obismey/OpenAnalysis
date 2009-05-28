Imports Core
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics

Public Class Sprite

    Public PositionSource As IValueSource(Of Vector2)
    Public SourceRectSource As IValueSource(Of Rectangle)
    Public RotationSource As IValueSource(Of Single)
    Public ScaleSource As IValueSource(Of Vector2)

    Private _useAllMap As Boolean
    Private _SourceRect As Rectangle
    Private _Position As Vector2
    Private _rotationCenter As Vector2
    Private _Scale As Vector2
    Private _Rotation As Single
    Private _isMouseOver As Boolean

    Public Property SourceRect() As Rectangle
        Get
            Return If(SourceRectSource Is Nothing, _SourceRect, SourceRectSource.GetValue(Me, "SourceRect"))
        End Get
        Set(ByVal value As Rectangle)
            _SourceRect = value
            SourceRectSource = Nothing
        End Set
    End Property

    Public Property UseAllMap() As Boolean
        Get
            Return _useAllMap
        End Get
        Set(ByVal value As Boolean)
            _useAllMap = value
        End Set
    End Property

    Public Property Position() As Vector2
        Get
            Return If(PositionSource Is Nothing, _Position, PositionSource.GetValue(Me, "Position"))
        End Get
        Set(ByVal value As Vector2)
            _Position = value
            PositionSource = Nothing
        End Set
    End Property

    Public Property Rotation() As Single
        Get
            Return If(RotationSource Is Nothing, _Rotation, RotationSource.GetValue(Me, "Rotation"))
        End Get
        Set(ByVal value As Single)
            _Rotation = value
            RotationSource = Nothing
        End Set
    End Property

    Public Property Scale() As Vector2
        Get
            Return If(ScaleSource Is Nothing, _Scale, ScaleSource.GetValue(Me, "Scale"))
        End Get
        Set(ByVal value As Vector2)
            _Scale = value
            ScaleSource = Nothing
        End Set
    End Property

    Public Property RotationCenter() As Vector2
        Get
            Return _rotationCenter
        End Get
        Set(ByVal value As Vector2)
            _rotationCenter = value
        End Set
    End Property

    Public Property IsMouseOver() As Boolean
        Get
            Return _isMouseOver
        End Get
        Set(ByVal value As Boolean)
            _isMouseOver = value
        End Set
    End Property


End Class
