Imports Microsoft.Xna.Framework
Imports Core

Public Class ArcBallCamera
    Implements ICamera

    ' Simply feed this camera the position of whatever you want its target to be
    'Protected _TargetPosition As Vector3 = Vector3.Zero

    Public ZoomSource As IValueSource(Of Single)
    Public VerticalAngleSource As IValueSource(Of Single)
    Public HorizontalAngleSource As IValueSource(Of Single)
    Public TargetPositionSource As IValueSource(Of Vector3)

    Private _HorizontalAngle As Single
    Private _Zoom As Single
    Private _VerticalAngle As Single
    Protected verticalAngleMin As Single = 0.01F
    Protected verticalAngleMax As Single = MathHelper.Pi - 0.01F
    Protected zoomMin As Single = 0.1F
    Protected zoomMax As Single = 5000.0F

    Private _TargetPosition As Vector3 = New Vector3(0, 0, -10)
    Private _aspectRatio As Single = 4 / 3
    Private _FarPlaneDistance As Single = 10000.0F
    Private _NearPlaneDistance As Single = 1.0F
    Private _Fov As Single = MathHelper.PiOver4
    Private _Position As Vector3 = Vector3.Zero
    Private _Direction As Vector3 = Vector3.UnitX
    Private _Up As Vector3 = Vector3.UnitY
    Private _proj As Matrix
    Private _view As Matrix
    Private RotationMatrix As Matrix

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

    Public Property AspectRatio() As Single Implements ICamera.AspectRatio
        Get
            Return _aspectRatio
        End Get
        Set(ByVal value As Single)
            _aspectRatio = value
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
            Return _Position
        End Get
    End Property

    Public ReadOnly Property Direction() As Vector3 Implements ICamera.Direction
        Get
            Return _Direction
        End Get
    End Property

    Public ReadOnly Property Up() As Vector3 Implements ICamera.Up
        Get
            Return _Up
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

    Public Property Zoom() As Single
        Get
            Return If(ZoomSource Is Nothing, _Zoom, ZoomSource.GetValue(Me, "Zoom"))
        End Get
        Set(ByVal value As Single)
            _Zoom = value
            ZoomSource = Nothing
        End Set
    End Property

    Public Property VerticalAngle() As Single
        Get
            Return If(VerticalAngleSource Is Nothing, _VerticalAngle, VerticalAngleSource.GetValue(Me, "VerticaleAngle"))
        End Get
        Set(ByVal value As Single)
            _VerticalAngle = value
            VerticalAngleSource = Nothing
        End Set
    End Property

    Public Property HorizontalAngle() As Single
        Get
            Return If(HorizontalAngleSource Is Nothing, _HorizontalAngle, HorizontalAngleSource.GetValue(Me, "HorizontalAngle"))
        End Get
        Set(ByVal value As Single)
            _HorizontalAngle = value
            HorizontalAngleSource = Nothing
        End Set
    End Property

    Public Property TargetPosition() As Vector3
        Get
            Return If(TargetPositionSource Is Nothing, _TargetPosition, TargetPositionSource.GetValue(Me, "TargetPosition"))
        End Get
        Set(ByVal value As Vector3)
            _TargetPosition = value
            TargetPositionSource = Nothing
        End Set
    End Property

    Public Sub ChangeSettings(ByVal VerticleMin As Single, _
                              ByVal VerticleMax As Single, _
                              ByVal ZoomMin As Single, _
                              ByVal ZoomMax As Single)
        ZoomMin = ZoomMin
        ZoomMax = ZoomMax
        verticalAngleMin = VerticleMin
        verticalAngleMax = VerticleMax
    End Sub

    Public Sub UpdateCamera(ByVal elapsedSeconds As Single) Implements ICamera.UpdateCamera
        ' Keep vertical angle within tolerances
        VerticalAngle = MathHelper.Clamp(VerticalAngle, verticalAngleMin, verticalAngleMax)

        ' Keep vertical angle within PI
        If HorizontalAngle > MathHelper.TwoPi Then
            HorizontalAngle -= MathHelper.TwoPi
        ElseIf HorizontalAngle < 0.0F Then
            HorizontalAngle += MathHelper.TwoPi
        End If

        ' Keep zoom within range
        Dim tempZoom = MathHelper.Clamp(Zoom, zoomMin, zoomMax)

        ' Start with an initial offset
        Dim cameraPosition As New Vector3(0.0F, Zoom, 0.0F)

        ' Rotate vertically
        cameraPosition = Vector3.Transform(cameraPosition, Matrix.CreateRotationX(VerticalAngle))

        ' Rotate horizontally
        cameraPosition = Vector3.Transform(cameraPosition, Matrix.CreateRotationY(HorizontalAngle))

        _Position = cameraPosition
        LookAt(TargetPosition)

        _Direction = RotationMatrix.Forward
        _Up = RotationMatrix.Up

        _view = Matrix.CreateLookAt(Position, TargetPosition, Up)
        _proj = Matrix.CreatePerspectiveFieldOfView(Fov, AspectRatio, NearPlaneDistance, FarPlaneDistance)
    End Sub

    Private Sub LookAt(ByVal TargetPosition As Vector3)
        Dim newForward As Vector3 = TargetPosition - Me.Position
        newForward.Normalize()
        _Direction = newForward
        Me.RotationMatrix.Forward = newForward

        Dim referenceVector As Vector3 = Vector3.UnitY

        ' On the slim chance that the camera is pointer perfectly parallel with the Y Axis, we cannot
        ' use cross product with a parallel axis, so we change the reference vector to the forward axis (Z).
        If Me.RotationMatrix.Forward.Y = referenceVector.Y OrElse Me.RotationMatrix.Forward.Y = -referenceVector.Y Then
            referenceVector = Vector3.UnitZ
        End If

        Me.RotationMatrix.Right = Vector3.Cross(Me.RotationMatrix.Forward, referenceVector)
        Me.RotationMatrix.Up = Vector3.Cross(Me.RotationMatrix.Right, Me.RotationMatrix.Forward)
    End Sub

End Class