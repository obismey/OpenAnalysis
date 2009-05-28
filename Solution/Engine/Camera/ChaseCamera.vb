'Public Class ChaseCamera
'    Implements ICamera






'#Region "Chased object properties (set externally each frame)"

'    ''' <summary>
'    ''' Position of object being chased.
'    ''' </summary>
'    Public Property ChasePosition() As Vector3
'        Get
'            Return m_chasePosition
'        End Get
'        Set(ByVal value As Vector3)
'            m_chasePosition = value
'        End Set
'    End Property
'    Private m_chasePosition As Vector3 = Vector3.Zero

'    ''' <summary>
'    ''' Direction the chased object is facing.
'    ''' </summary>
'    Public Property ChaseDirection() As Vector3
'        Get
'            Return m_chaseDirection
'        End Get
'        Set(ByVal value As Vector3)
'            m_chaseDirection = value
'        End Set
'    End Property
'    Private m_chaseDirection As Vector3 = -Vector3.UnitX

'    ''' <summary>
'    ''' Chased object's Up vector.
'    ''' </summary>
'    Public Property Up() As Vector3
'        Get
'            Return m_up
'        End Get
'        Set(ByVal value As Vector3)
'            m_up = value
'        End Set
'    End Property
'    Private m_up As Vector3 = Vector3.Up

'#End Region

'#Region "Desired camera positioning (set when creating camera or changing view)"

'    ''' <summary>
'    ''' Desired camera position in the chased object's coordinate system.
'    ''' </summary>
'    Public Property DesiredPositionOffset() As Vector3
'        Get
'            Return m_desiredPositionOffset
'        End Get
'        Set(ByVal value As Vector3)
'            m_desiredPositionOffset = value
'        End Set
'    End Property
'    Private m_desiredPositionOffset As New Vector3(25.0F, 10.0F, 0.0F)

'    ''' <summary>
'    ''' Desired camera position in world space.
'    ''' </summary>
'    Public ReadOnly Property DesiredPosition() As Vector3
'        Get
'            ' Ensure correct value even if update has not been called this frame
'            UpdateWorldPositions()

'            Return m_desiredPosition
'        End Get
'    End Property
'    Private m_desiredPosition As Vector3

'    ''' <summary>
'    ''' Look at point in the chased object's coordinate system.
'    ''' </summary>
'    Public Property LookAtOffset() As Vector3
'        Get
'            Return m_lookAtOffset
'        End Get
'        Set(ByVal value As Vector3)
'            m_lookAtOffset = value
'        End Set
'    End Property
'    Private m_lookAtOffset As New Vector3(-5.0F, 0.0F, 0.0F)


'    ''' <summary>
'    ''' Look at point in world space.
'    ''' </summary>
'    Public ReadOnly Property LookAt() As Vector3
'        Get
'            ' Ensure correct value even if update has not been called this frame
'            UpdateWorldPositions()

'            Return m_lookAt
'        End Get
'    End Property
'    Private m_lookAt As Vector3

'#End Region

'#Region "Camera physics (typically set when creating camera)"

'    ''' <summary>
'    ''' Physics coefficient which controls the influence of the camera's position
'    ''' over the spring force. The stiffer the spring, the closer it will stay to
'    ''' the chased object.
'    ''' </summary>
'    Public Property Stiffness() As Single
'        Get
'            Return m_stiffness
'        End Get
'        Set(ByVal value As Single)
'            m_stiffness = value
'        End Set
'    End Property
'    Private m_stiffness As Single = 1800.0F

'    ''' <summary>
'    ''' Physics coefficient which approximates internal friction of the spring.
'    ''' Sufficient damping will prevent the spring from oscillating infinitely.
'    ''' </summary>
'    Public Property Damping() As Single
'        Get
'            Return m_damping
'        End Get
'        Set(ByVal value As Single)
'            m_damping = value
'        End Set
'    End Property
'    Private m_damping As Single = 600.0F

'    ''' <summary>
'    ''' Mass of the camera body. Heaver objects require stiffer springs with less
'    ''' damping to move at the same rate as lighter objects.
'    ''' </summary>
'    Public Property Mass() As Single
'        Get
'            Return m_mass
'        End Get
'        Set(ByVal value As Single)
'            m_mass = value
'        End Set
'    End Property
'    Private m_mass As Single = 50.0F

'#End Region

'#Region "Current camera properties (updated by camera physics)"

'    ''' <summary>
'    ''' Position of camera in world space.
'    ''' </summary>
'    Public ReadOnly Property Position() As Vector3
'        Get
'            Return m_position
'        End Get
'    End Property
'    Private m_position As Vector3

'    ''' <summary>
'    ''' Velocity of camera.
'    ''' </summary>
'    Public ReadOnly Property Velocity() As Vector3
'        Get
'            Return m_velocity
'        End Get
'    End Property
'    Private m_velocity As Vector3

'#End Region

'#Region "Perspective properties"

'    ''' <summary>
'    ''' Perspective aspect ratio. Default value should be overriden by application.
'    ''' </summary>
'    Public Property AspectRatio() As Single
'        Get
'            Return m_aspectRatio
'        End Get
'        Set(ByVal value As Single)
'            m_aspectRatio = value
'        End Set
'    End Property
'    Private m_aspectRatio As Single = 4.0F / 3.0F

'    ''' <summary>
'    ''' Perspective field of view.
'    ''' </summary>
'    Public Property FieldOfView() As Single
'        Get
'            Return m_fieldOfView
'        End Get
'        Set(ByVal value As Single)
'            m_fieldOfView = value
'        End Set
'    End Property
'    Private m_fieldOfView As Single = MathHelper.ToRadians(45.0F)

'    ''' <summary>
'    ''' Distance to the near clipping plane.
'    ''' </summary>
'    Public Property NearPlaneDistance() As Single
'        Get
'            Return m_nearPlaneDistance
'        End Get
'        Set(ByVal value As Single)
'            m_nearPlaneDistance = value
'        End Set
'    End Property
'    Private m_nearPlaneDistance As Single = 1.0F

'    ''' <summary>
'    ''' Distance to the far clipping plane.
'    ''' </summary>
'    Public Property FarPlaneDistance() As Single
'        Get
'            Return m_farPlaneDistance
'        End Get
'        Set(ByVal value As Single)
'            m_farPlaneDistance = value
'        End Set
'    End Property
'    Private m_farPlaneDistance As Single = 10000.0F

'#End Region

'#Region "Matrix properties"

'    ''' <summary>
'    ''' View transform matrix.
'    ''' </summary>
'    Public ReadOnly Property View() As Matrix
'        Get
'            Return m_view
'        End Get
'    End Property
'    Private m_view As Matrix

'    ''' <summary>
'    ''' Projecton transform matrix.
'    ''' </summary>
'    Public ReadOnly Property Projection() As Matrix
'        Get
'            Return m_projection
'        End Get
'    End Property
'    Private m_projection As Matrix

'#End Region

'#Region "Methods"

'    ''' <summary>
'    ''' Rebuilds object space values in world space. Invoke before publicly
'    ''' returning or privately accessing world space values.
'    ''' </summary>
'    Private Sub UpdateWorldPositions()
'        ' Construct a matrix to transform from object space to worldspace
'        Dim transform As Matrix = Matrix.Identity
'        transform.Forward = ChaseDirection
'        transform.Up = Up
'        transform.Right = Vector3.Cross(Up, ChaseDirection)

'        ' Calculate desired camera properties in world space
'        m_desiredPosition = ChasePosition + Vector3.TransformNormal(DesiredPositionOffset, transform)
'        m_lookAt = ChasePosition + Vector3.TransformNormal(LookAtOffset, transform)
'    End Sub

'    ''' <summary>
'    ''' Rebuilds camera's view and projection matricies.
'    ''' </summary>
'    Private Sub UpdateMatrices()
'        m_view = Matrix.CreateLookAt(Me.Position, Me.LookAt, Me.Up)
'        m_projection = Matrix.CreatePerspectiveFieldOfView(FieldOfView, AspectRatio, NearPlaneDistance, FarPlaneDistance)
'    End Sub

'    ''' <summary>
'    ''' Forces camera to be at desired position and to stop moving. The is useful
'    ''' when the chased object is first created or after it has been teleported.
'    ''' Failing to call this after a large change to the chased object's position
'    ''' will result in the camera quickly flying across the world.
'    ''' </summary>
'    Public Sub Reset()
'        UpdateWorldPositions()

'        ' Stop motion
'        m_velocity = Vector3.Zero

'        ' Force desired position
'        m_position = m_desiredPosition

'        UpdateMatrices()
'    End Sub

'    ''' <summary>
'    ''' Animates the camera from its current position towards the desired offset
'    ''' behind the chased object. The camera's animation is controlled by a simple
'    ''' physical spring attached to the camera and anchored to the desired position.
'    ''' </summary>
'    Public Sub Update(ByVal gameTime As GameTime)
'        If gameTime Is Nothing Then
'            Throw New ArgumentNullException("gameTime")
'        End If

'        UpdateWorldPositions()

'        Dim elapsed As Single = CSng(gameTime.ElapsedGameTime.TotalSeconds)

'        ' Calculate spring force
'        Dim stretch As Vector3 = m_position - m_desiredPosition
'        Dim force As Vector3 = -m_stiffness * stretch - m_damping * m_velocity

'        ' Apply acceleration
'        Dim acceleration As Vector3 = force / m_mass
'        m_velocity += acceleration * elapsed

'        ' Apply velocity
'        m_position += m_velocity * elapsed

'        UpdateMatrices()
'    End Sub

'#End Region

'    Public ReadOnly Property projMatrix() As Microsoft.Xna.Framework.Matrix Implements ICamera.projMatrix
'        Get
'            Return Projection
'        End Get
'    End Property
'    Public ReadOnly Property viewMatrix() As Microsoft.Xna.Framework.Matrix Implements ICamera.viewMatrix
'        Get
'            Return View
'        End Get
'    End Property
'    Public Sub UpdateCamera(ByVal elapsedSeconds As Single) Implements ICamera.UpdateCamera
'        Update(New GameTime(Nothing, New TimeSpan(0, 0, 0, 0, 30), Nothing, New TimeSpan(0, 0, 0, 0, 30)))
'    End Sub
'    Public ReadOnly Property CameraDirection() As Microsoft.Xna.Framework.Vector3 Implements ICamera.CameraDirection
'        Get

'        End Get
'    End Property
'    Public ReadOnly Property CameraPosition() As Microsoft.Xna.Framework.Vector3 Implements ICamera.CameraPosition
'        Get

'        End Get
'    End Property
'    Public ReadOnly Property CameraUp() As Microsoft.Xna.Framework.Vector3 Implements ICamera.CameraUp
'        Get

'        End Get
'    End Property



'    Public Property CameraAspectRatio() As Single Implements ICamera.CameraAspectRatio
'        Get

'        End Get
'        Set(ByVal value As Single)

'        End Set
'    End Property

'    Public Property CameraFarPlaneDistance() As Single Implements ICamera.CameraFarPlaneDistance
'        Get

'        End Get
'        Set(ByVal value As Single)

'        End Set
'    End Property

'    Public Property CameraFov() As Single Implements ICamera.CameraFov
'        Get

'        End Get
'        Set(ByVal value As Single)

'        End Set
'    End Property

'    Public Property CameraNearPlaneDistance() As Single Implements ICamera.CameraNearPlaneDistance
'        Get

'        End Get
'        Set(ByVal value As Single)

'        End Set
'    End Property


'End Class