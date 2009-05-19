Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics

Public Class TextureUnit

    Private _rotation As Single
    Private _tiling As Vector3
    Private _offset As Vector3
    Private _map As Texture
    Private _texcoordIndex As UInteger

    Public Property TexCoordIndex() As UInteger
        Get
            Return _texcoordIndex
        End Get
        Set(ByVal value As UInteger)
            _texcoordIndex = value
        End Set
    End Property
    Public Property Map() As Texture
        Get
            Return _map
        End Get
        Set(ByVal value As Texture)
            _map = value
        End Set
    End Property
    Public Property Offset() As Vector3
        Get
            Return _offset
        End Get
        Set(ByVal value As Vector3)
            _offset = value
        End Set
    End Property
    Public Property Tiling() As Vector3
        Get
            Return _tiling
        End Get
        Set(ByVal value As Vector3)
            _tiling = value
        End Set
    End Property
    Public Property Rotation() As Single
        Get
            Return _rotation
        End Get
        Set(ByVal value As Single)
            _rotation = value
        End Set
    End Property

End Class
