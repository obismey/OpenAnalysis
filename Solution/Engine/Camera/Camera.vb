Imports Microsoft.Xna.Framework

Public Class Camera
    Implements ICamera


    Protected Position As Vector3 = Vector3.Zero
    Protected Target As New Vector3(0, 0, -10)
    Protected Up As New Vector3(0, 1, 0)

    Public View As Matrix
    Public Projection As Matrix
    Protected RotationMatrix As Matrix

    Public AspectRatio As Single
    Protected Frustrum As BoundingFrustum

    Public ReadOnly Property Direction() As Vector3
        Get
            Return Target - Position
        End Get
    End Property
    Public Sub New(ByVal aspectratio As Single)
        Projection = GenerateProjection(MathHelper.PiOver4, aspectratio)
    End Sub
    Public Function GenerateProjection(ByVal FoV As Single, ByVal AspectRatio As Single) As Matrix
        Return Matrix.CreatePerspectiveFieldOfView(FoV, AspectRatio, 0.1F, 1000)
    End Function
    Public Overridable Sub Update(ByVal gameTime As Microsoft.Xna.Framework.GameTime)
        View = Matrix.CreateLookAt(Position, Target, Up)
        Frustrum = New BoundingFrustum(View * Projection)
    End Sub



    Public Sub UpdateCamera(ByVal elapsedSeconds As Single) Implements ICamera.UpdateCamera
        Update(Nothing)
    End Sub
    Public ReadOnly Property projMatrix() As Microsoft.Xna.Framework.Matrix Implements ICamera.projMatrix
        Get
            Return Projection
        End Get
    End Property
    Public ReadOnly Property viewMatrix() As Microsoft.Xna.Framework.Matrix Implements ICamera.viewMatrix
        Get
            Return View
        End Get
    End Property
    Public ReadOnly Property CameraDirection() As Microsoft.Xna.Framework.Vector3 Implements ICamera.CameraDirection
        Get
            Return Vector3.Normalize((Target - Position))
        End Get
    End Property
    Public ReadOnly Property CameraPosition() As Microsoft.Xna.Framework.Vector3 Implements ICamera.CameraPosition
        Get
            Return Position
        End Get
    End Property
    Public ReadOnly Property CameraUp() As Microsoft.Xna.Framework.Vector3 Implements ICamera.CameraUp
        Get
            Return Up
        End Get
    End Property


    Public Property CameraAspectRatio() As Single Implements ICamera.CameraAspectRatio
        Get
            Return AspectRatio
        End Get
        Set(ByVal value As Single)
            AspectRatio = value
        End Set
    End Property

    Public Property CameraFarPlaneDistance() As Single Implements ICamera.CameraFarPlaneDistance
        Get

        End Get
        Set(ByVal value As Single)

        End Set
    End Property

    Public Property CameraFov() As Single Implements ICamera.CameraFov
        Get

        End Get
        Set(ByVal value As Single)

        End Set
    End Property

    Public Property CameraNearPlaneDistance() As Single Implements ICamera.CameraNearPlaneDistance
        Get

        End Get
        Set(ByVal value As Single)

        End Set
    End Property

    
End Class

Public Class ArcBallCamera
    Inherits Camera


    ' Simply feed this camera the position of whatever you want its target to be
    Protected _TargetPosition As Vector3 = Vector3.Zero

    Protected horizontalAngle As Single = MathHelper.PiOver2
    Protected verticalAngle As Single = MathHelper.PiOver2

    Protected verticalAngleMin As Single = 0.01F
    Protected verticalAngleMax As Single = MathHelper.Pi - 0.01F

    Protected zoomMin As Single = 0.1F
    Protected zoomMax As Single = 5000.0F
    Public zoom As Single = 30.0F

    Public Sub New(ByVal aspectratio As Single)
        MyBase.New(aspectratio)
    End Sub

    Public Sub Rotate(ByVal Horizontal As Single, ByVal Vertical As Single)
        horizontalAngle += Horizontal
        verticalAngle += Vertical
    End Sub

    Public Sub UpdateZoom(ByVal ZoomDecal As Single)
        zoom += ZoomDecal
    End Sub

    Public Sub ChangeSettings(ByVal VerticleMin As Single, ByVal VerticleMax As Single, ByVal ZoomMin As Single, ByVal ZoomMax As Single, ByVal VerticleAngle As Single, ByVal HorizontalAngle As Single, _
     ByVal Zoomlevel As Single)
        HorizontalAngle = HorizontalAngle
        verticalAngle = VerticleAngle
        zoom = Zoomlevel
        ZoomMin = ZoomMin
        ZoomMax = ZoomMax
        verticalAngleMin = VerticleMin
        verticalAngleMax = VerticleMax
    End Sub

    Public Property TargetPosition() As Vector3
        Get
            Return _TargetPosition
        End Get
        Set(ByVal value As Vector3)
            _TargetPosition = value
        End Set
    End Property
    Public Overrides Sub Update(ByVal gameTime As GameTime)
        ' Keep vertical angle within tolerances
        verticalAngle = MathHelper.Clamp(verticalAngle, verticalAngleMin, verticalAngleMax)

        ' Keep vertical angle within PI
        If horizontalAngle > MathHelper.TwoPi Then
            horizontalAngle -= MathHelper.TwoPi
        ElseIf horizontalAngle < 0.0F Then
            horizontalAngle += MathHelper.TwoPi
        End If

        ' Keep zoom within range
        zoom = MathHelper.Clamp(zoom, zoomMin, zoomMax)

        ' Start with an initial offset
        Dim cameraPosition As New Vector3(0.0F, zoom, 0.0F)

        ' Rotate vertically
        cameraPosition = Vector3.Transform(cameraPosition, Matrix.CreateRotationX(verticalAngle))

        ' Rotate horizontally
        cameraPosition = Vector3.Transform(cameraPosition, Matrix.CreateRotationY(horizontalAngle))

        Position = cameraPosition + TargetPosition
        Me.LookAt(TargetPosition)

        Target = Position + Me.RotationMatrix.Forward
        Up = RotationMatrix.Up

        MyBase.Update(gameTime)
    End Sub

    ''' <summary>
    ''' Points camera in direction of any position.
    ''' </summary>
    ''' <param name="TargetPosition">Target position for camera to face.</param>
    Private Sub LookAt(ByVal TargetPosition As Vector3)
        Dim newForward As Vector3 = TargetPosition - Me.Position
        newForward.Normalize()
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

Public Class ChaseCamera
    Implements ICamera






#Region "Chased object properties (set externally each frame)"

    ''' <summary>
    ''' Position of object being chased.
    ''' </summary>
    Public Property ChasePosition() As Vector3
        Get
            Return m_chasePosition
        End Get
        Set(ByVal value As Vector3)
            m_chasePosition = value
        End Set
    End Property
    Private m_chasePosition As Vector3 = Vector3.Zero

    ''' <summary>
    ''' Direction the chased object is facing.
    ''' </summary>
    Public Property ChaseDirection() As Vector3
        Get
            Return m_chaseDirection
        End Get
        Set(ByVal value As Vector3)
            m_chaseDirection = value
        End Set
    End Property
    Private m_chaseDirection As Vector3 = -Vector3.UnitX

    ''' <summary>
    ''' Chased object's Up vector.
    ''' </summary>
    Public Property Up() As Vector3
        Get
            Return m_up
        End Get
        Set(ByVal value As Vector3)
            m_up = value
        End Set
    End Property
    Private m_up As Vector3 = Vector3.Up

#End Region

#Region "Desired camera positioning (set when creating camera or changing view)"

    ''' <summary>
    ''' Desired camera position in the chased object's coordinate system.
    ''' </summary>
    Public Property DesiredPositionOffset() As Vector3
        Get
            Return m_desiredPositionOffset
        End Get
        Set(ByVal value As Vector3)
            m_desiredPositionOffset = value
        End Set
    End Property
    Private m_desiredPositionOffset As New Vector3(25.0F, 10.0F, 0.0F)

    ''' <summary>
    ''' Desired camera position in world space.
    ''' </summary>
    Public ReadOnly Property DesiredPosition() As Vector3
        Get
            ' Ensure correct value even if update has not been called this frame
            UpdateWorldPositions()

            Return m_desiredPosition
        End Get
    End Property
    Private m_desiredPosition As Vector3

    ''' <summary>
    ''' Look at point in the chased object's coordinate system.
    ''' </summary>
    Public Property LookAtOffset() As Vector3
        Get
            Return m_lookAtOffset
        End Get
        Set(ByVal value As Vector3)
            m_lookAtOffset = value
        End Set
    End Property
    Private m_lookAtOffset As New Vector3(-5.0F, 0.0F, 0.0F)


    ''' <summary>
    ''' Look at point in world space.
    ''' </summary>
    Public ReadOnly Property LookAt() As Vector3
        Get
            ' Ensure correct value even if update has not been called this frame
            UpdateWorldPositions()

            Return m_lookAt
        End Get
    End Property
    Private m_lookAt As Vector3

#End Region

#Region "Camera physics (typically set when creating camera)"

    ''' <summary>
    ''' Physics coefficient which controls the influence of the camera's position
    ''' over the spring force. The stiffer the spring, the closer it will stay to
    ''' the chased object.
    ''' </summary>
    Public Property Stiffness() As Single
        Get
            Return m_stiffness
        End Get
        Set(ByVal value As Single)
            m_stiffness = value
        End Set
    End Property
    Private m_stiffness As Single = 1800.0F

    ''' <summary>
    ''' Physics coefficient which approximates internal friction of the spring.
    ''' Sufficient damping will prevent the spring from oscillating infinitely.
    ''' </summary>
    Public Property Damping() As Single
        Get
            Return m_damping
        End Get
        Set(ByVal value As Single)
            m_damping = value
        End Set
    End Property
    Private m_damping As Single = 600.0F

    ''' <summary>
    ''' Mass of the camera body. Heaver objects require stiffer springs with less
    ''' damping to move at the same rate as lighter objects.
    ''' </summary>
    Public Property Mass() As Single
        Get
            Return m_mass
        End Get
        Set(ByVal value As Single)
            m_mass = value
        End Set
    End Property
    Private m_mass As Single = 50.0F

#End Region

#Region "Current camera properties (updated by camera physics)"

    ''' <summary>
    ''' Position of camera in world space.
    ''' </summary>
    Public ReadOnly Property Position() As Vector3
        Get
            Return m_position
        End Get
    End Property
    Private m_position As Vector3

    ''' <summary>
    ''' Velocity of camera.
    ''' </summary>
    Public ReadOnly Property Velocity() As Vector3
        Get
            Return m_velocity
        End Get
    End Property
    Private m_velocity As Vector3

#End Region

#Region "Perspective properties"

    ''' <summary>
    ''' Perspective aspect ratio. Default value should be overriden by application.
    ''' </summary>
    Public Property AspectRatio() As Single
        Get
            Return m_aspectRatio
        End Get
        Set(ByVal value As Single)
            m_aspectRatio = value
        End Set
    End Property
    Private m_aspectRatio As Single = 4.0F / 3.0F

    ''' <summary>
    ''' Perspective field of view.
    ''' </summary>
    Public Property FieldOfView() As Single
        Get
            Return m_fieldOfView
        End Get
        Set(ByVal value As Single)
            m_fieldOfView = value
        End Set
    End Property
    Private m_fieldOfView As Single = MathHelper.ToRadians(45.0F)

    ''' <summary>
    ''' Distance to the near clipping plane.
    ''' </summary>
    Public Property NearPlaneDistance() As Single
        Get
            Return m_nearPlaneDistance
        End Get
        Set(ByVal value As Single)
            m_nearPlaneDistance = value
        End Set
    End Property
    Private m_nearPlaneDistance As Single = 1.0F

    ''' <summary>
    ''' Distance to the far clipping plane.
    ''' </summary>
    Public Property FarPlaneDistance() As Single
        Get
            Return m_farPlaneDistance
        End Get
        Set(ByVal value As Single)
            m_farPlaneDistance = value
        End Set
    End Property
    Private m_farPlaneDistance As Single = 10000.0F

#End Region

#Region "Matrix properties"

    ''' <summary>
    ''' View transform matrix.
    ''' </summary>
    Public ReadOnly Property View() As Matrix
        Get
            Return m_view
        End Get
    End Property
    Private m_view As Matrix

    ''' <summary>
    ''' Projecton transform matrix.
    ''' </summary>
    Public ReadOnly Property Projection() As Matrix
        Get
            Return m_projection
        End Get
    End Property
    Private m_projection As Matrix

#End Region

#Region "Methods"

    ''' <summary>
    ''' Rebuilds object space values in world space. Invoke before publicly
    ''' returning or privately accessing world space values.
    ''' </summary>
    Private Sub UpdateWorldPositions()
        ' Construct a matrix to transform from object space to worldspace
        Dim transform As Matrix = Matrix.Identity
        transform.Forward = ChaseDirection
        transform.Up = Up
        transform.Right = Vector3.Cross(Up, ChaseDirection)

        ' Calculate desired camera properties in world space
        m_desiredPosition = ChasePosition + Vector3.TransformNormal(DesiredPositionOffset, transform)
        m_lookAt = ChasePosition + Vector3.TransformNormal(LookAtOffset, transform)
    End Sub

    ''' <summary>
    ''' Rebuilds camera's view and projection matricies.
    ''' </summary>
    Private Sub UpdateMatrices()
        m_view = Matrix.CreateLookAt(Me.Position, Me.LookAt, Me.Up)
        m_projection = Matrix.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearPlaneDistance, FarPlaneDistance)
    End Sub

    ''' <summary>
    ''' Forces camera to be at desired position and to stop moving. The is useful
    ''' when the chased object is first created or after it has been teleported.
    ''' Failing to call this after a large change to the chased object's position
    ''' will result in the camera quickly flying across the world.
    ''' </summary>
    Public Sub Reset()
        UpdateWorldPositions()

        ' Stop motion
        m_velocity = Vector3.Zero

        ' Force desired position
        m_position = m_desiredPosition

        UpdateMatrices()
    End Sub

    ''' <summary>
    ''' Animates the camera from its current position towards the desired offset
    ''' behind the chased object. The camera's animation is controlled by a simple
    ''' physical spring attached to the camera and anchored to the desired position.
    ''' </summary>
    Public Sub Update(ByVal gameTime As GameTime)
        If gameTime Is Nothing Then
            Throw New ArgumentNullException("gameTime")
        End If

        UpdateWorldPositions()

        Dim elapsed As Single = CSng(gameTime.ElapsedGameTime.TotalSeconds)

        ' Calculate spring force
        Dim stretch As Vector3 = m_position - m_desiredPosition
        Dim force As Vector3 = -m_stiffness * stretch - m_damping * m_velocity

        ' Apply acceleration
        Dim acceleration As Vector3 = force / m_mass
        m_velocity += acceleration * elapsed

        ' Apply velocity
        m_position += m_velocity * elapsed

        UpdateMatrices()
    End Sub

#End Region

    Public ReadOnly Property projMatrix() As Microsoft.Xna.Framework.Matrix Implements ICamera.projMatrix
        Get
            Return Projection
        End Get
    End Property
    Public ReadOnly Property viewMatrix() As Microsoft.Xna.Framework.Matrix Implements ICamera.viewMatrix
        Get
            Return View
        End Get
    End Property
    Public Sub UpdateCamera(ByVal elapsedSeconds As Single) Implements ICamera.UpdateCamera
        Update(New GameTime(Nothing, New TimeSpan(0, 0, 0, 0, 30), Nothing, New TimeSpan(0, 0, 0, 0, 30)))
    End Sub
    Public ReadOnly Property CameraDirection() As Microsoft.Xna.Framework.Vector3 Implements ICamera.CameraDirection
        Get

        End Get
    End Property
    Public ReadOnly Property CameraPosition() As Microsoft.Xna.Framework.Vector3 Implements ICamera.CameraPosition
        Get

        End Get
    End Property
    Public ReadOnly Property CameraUp() As Microsoft.Xna.Framework.Vector3 Implements ICamera.CameraUp
        Get

        End Get
    End Property



    Public Property CameraAspectRatio() As Single Implements ICamera.CameraAspectRatio
        Get

        End Get
        Set(ByVal value As Single)

        End Set
    End Property

    Public Property CameraFarPlaneDistance() As Single Implements ICamera.CameraFarPlaneDistance
        Get

        End Get
        Set(ByVal value As Single)

        End Set
    End Property

    Public Property CameraFov() As Single Implements ICamera.CameraFov
        Get

        End Get
        Set(ByVal value As Single)

        End Set
    End Property

    Public Property CameraNearPlaneDistance() As Single Implements ICamera.CameraNearPlaneDistance
        Get

        End Get
        Set(ByVal value As Single)

        End Set
    End Property

    
End Class

