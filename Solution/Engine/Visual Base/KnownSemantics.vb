Imports Microsoft.Xna.Framework

Public Enum KnownSemantics

    <SemanticType(GetType(Matrix))> _
    World
    <SemanticType(GetType(Matrix))> _
    WorldInverse
    <SemanticType(GetType(Matrix))> _
    WorldTranspose
    <SemanticType(GetType(Matrix))> _
    WorldInverseTranspose

    <SemanticType(GetType(Matrix))> _
    View
    <SemanticType(GetType(Matrix))> _
    ViewInverse
    <SemanticType(GetType(Matrix))> _
    ViewTranspose
    <SemanticType(GetType(Matrix))> _
    ViewInverseTranspose

    <SemanticType(GetType(Matrix))> _
    Projection
    <SemanticType(GetType(Matrix))> _
    ProjectionInverse
    <SemanticType(GetType(Matrix))> _
    ProjectionTranspose
    <SemanticType(GetType(Matrix))> _
    ProjectionInverseTranspose

    <SemanticType(GetType(Matrix))> _
    WorldView
    <SemanticType(GetType(Matrix))> _
    WorldViewTranspose
    <SemanticType(GetType(Matrix))> _
    WorldViewInverse
    <SemanticType(GetType(Matrix))> _
    WorldViewInverseTranspose

    <SemanticType(GetType(Matrix))> _
    ViewProjection
    <SemanticType(GetType(Matrix))> _
    ViewProjectionInverse
    <SemanticType(GetType(Matrix))> _
    ViewProjectionTranspose
    <SemanticType(GetType(Matrix))> _
    ViewProjectionInverseTranspose

    <SemanticType(GetType(Matrix))> _
    WorldViewProjection
    <SemanticType(GetType(Matrix))> _
    WorldViewProjectionTranspose
    <SemanticType(GetType(Matrix))> _
    WorldViewProjectionInverse
    <SemanticType(GetType(Matrix))> _
    WorldViewProjectionInverseTranspose

    <SemanticType(GetType(Vector3))> _
    CameraPosition
    <SemanticType(GetType(Vector3))> _
    CameraDirection
    <SemanticType(GetType(Vector3))> _
    CameraUp

    <SemanticType(GetType(Single))> _
     Time
    <SemanticType(GetType(Single))> _
     LastTime
    <SemanticType(GetType(Single))> _
     ElapsedTime

    <SemanticType(GetType(Single))> _
    Random

    <SemanticType(GetType(Vector2))> _
    MousePosition

    <SemanticType(GetType(Vector4))> _
    AmbientColor

End Enum

Public Class SemanticTypeAttribute
    Inherits Attribute

    Private _type As Type
    ReadOnly Property Type() As Type
        Get
            Return _type
        End Get
    End Property

    Sub New(ByVal __type As Type)
        _type = __type
    End Sub
End Class