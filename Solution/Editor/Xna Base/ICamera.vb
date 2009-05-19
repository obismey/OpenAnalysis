Imports Microsoft.Xna.Framework
Public Interface ICamera

    ReadOnly Property projMatrix() As Matrix
    ReadOnly Property viewMatrix() As Matrix
    Property CameraAspectRatio() As Single
    Property CameraNearPlaneDistance() As Single
    Property CameraFarPlaneDistance() As Single
    Property CameraFov() As Single
    ReadOnly Property CameraPosition() As Vector3
    ReadOnly Property CameraDirection() As Vector3
    ReadOnly Property CameraUp() As Vector3


    Sub UpdateCamera(ByVal elapsedSeconds As Single)
End Interface
