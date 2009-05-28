Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Core
Public Class TextureUnit

    Public OffsetSource As IValueSource(Of Vector3)
    Public RotationSource As IValueSource(Of Single)
    Public TilingSource As IValueSource(Of Vector3)

    Private _Rotation As Single
    Private _Offset As Vector3
    Private _texcoordIndex As UInteger
    Private _map As String
    Private _Tiling As Vector3


    Public Property TexCoordIndex() As UInteger
        Get
            Return _texcoordIndex
        End Get
        Set(ByVal value As UInteger)
            _texcoordIndex = value
        End Set
    End Property
    Public Property Map() As String
        Get
            Return _map
        End Get
        Set(ByVal value As String)
            _map = value
        End Set
    End Property
    Public Property Offset() As Vector3
        Get
            Return If(OffsetSource Is Nothing, _Offset, OffsetSource.GetValue(Me, "Offset"))
        End Get
        Set(ByVal value As Vector3)
            _Offset = value
            OffsetSource = Nothing
        End Set
    End Property
    Public Property Tiling() As Vector3
        Get
            Return If(TilingSource Is Nothing, _Tiling, TilingSource.GetValue(Me, "Tiling"))
        End Get
        Set(ByVal value As Vector3)
            _Tiling = value
            TilingSource = Nothing
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
End Class
