Imports Microsoft.Xna.Framework
Imports Core

Public Class FreeCamera
    Implements ICamera

    Private _aspectRatio As Single
    Private _FarPlaneDistance As Single = 10000.0F
    Private _NearPlaneDistance As Single = 1.0F
    Private _Fov As Single = MathHelper.PiOver4
    Private _Position As Vector3 = Vector3.Zero
    Private _Direction As Vector3 = Vector3.UnitX
    Private _Up As Vector3 = Vector3.UnitY
    Private _proj As Matrix
    Private _view As Matrix

    Public PositionSource As IValueSource(Of Vector3) = Nothing
    Public DirectionSource As IValueSource(Of Vector3) = Nothing
    Public UpSource As IValueSource(Of Vector3) = Nothing

    Public Property AspectRatio() As Single Implements ICamera.AspectRatio
        Get
            Return _aspectRatio
        End Get
        Set(ByVal value As Single)
            _aspectRatio = value
        End Set
    End Property

    Public Property FarPlaneDistance() As Single Implements ICamera.FarPlaneDistance
        Get
            Return _FarPlaneDistance
        End Get
        Set(ByVal value As Single)
            _FarPlaneDistance = value
        End Set
    End Property

    Public Property NearPlaneDistance() As Single Implements ICamera.NearPlaneDistance
        Get
            Return _NearPlaneDistance
        End Get
        Set(ByVal value As Single)
            _NearPlaneDistance = value
        End Set
    End Property

    Public Property Fov() As Single Implements ICamera.Fov
        Get
            Return _Fov
        End Get
        Set(ByVal value As Single)
            _Fov = value
        End Set
    End Property

    Public ReadOnly Property Frustrum() As Microsoft.Xna.Framework.BoundingFrustum Implements ICamera.Frustrum
        Get
            Return New Microsoft.Xna.Framework.BoundingFrustum(viewMatrix * projMatrix)
        End Get
    End Property

    Public ReadOnly Property Position() As Vector3 Implements ICamera.Position
        Get
            Return If(PositionSource Is Nothing, _Position, PositionSource.GetValue(Me, "Position"))
        End Get
    End Property

    Public ReadOnly Property Direction() As Vector3 Implements ICamera.Direction
        Get
            Return If(DirectionSource Is Nothing, _Direction, DirectionSource.GetValue(Me, "Direction"))
        End Get
    End Property

    Public ReadOnly Property Up() As Vector3 Implements ICamera.Up
        Get
            Return If(UpSource Is Nothing, _Up, DirectionSource.GetValue(Me, "Up"))
        End Get
    End Property

    Public ReadOnly Property viewMatrix() As Microsoft.Xna.Framework.Matrix Implements ICamera.viewMatrix
        Get
            Return _view
        End Get
    End Property

    Public ReadOnly Property projMatrix() As Microsoft.Xna.Framework.Matrix Implements ICamera.projMatrix
        Get
            Return _proj
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="__position"></param>
    ''' <param name="__direction"></param>
    ''' <param name="__up"></param>
    ''' <remarks></remarks>
    Sub SetParams(ByVal __position As Vector3, ByVal __direction As Vector3, ByVal __up As Vector3)
        _Position = __position
        _Up = __up
        _Direction = __direction

        PositionSource = Nothing
        UpSource = Nothing
        DirectionSource = Nothing
    End Sub

    Public Sub UpdateCamera(ByVal elapsedSeconds As Single) Implements ICamera.UpdateCamera
        _view = Matrix.CreateLookAt(Position, Position + Direction, Up)
        _proj = Matrix.CreatePerspectiveFieldOfView(Fov, AspectRatio, NearPlaneDistance, FarPlaneDistance)
    End Sub
End Class
