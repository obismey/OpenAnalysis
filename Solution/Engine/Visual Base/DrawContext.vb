Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Content



''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class DrawContext
    Dim _view, _proj, _world As Matrix
    Dim _eltime, _gtime As Double
    Dim _campos, _camdir, _camUp As Vector3
    Dim _rnd As New Random
    Dim _vindex As Integer
    Dim _mousepos As Vector2

    Friend _device As GraphicsDevice = Nothing

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub New(ByVal __device As GraphicsDevice)
        _device = __device
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property View() As Matrix
        Get
            Return _view
        End Get
        Set(ByVal value As Matrix)
            _view = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Projection() As Matrix
        Get
            Return _proj
        End Get
        Set(ByVal value As Matrix)
            _proj = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property World() As Matrix
        Get
            Return _world
        End Get
        Set(ByVal value As Matrix)
            _world = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ElapsedTime() As Double
        Get
            Return _eltime
        End Get
        Set(ByVal value As Double)
            _eltime = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property GameTime() As Double
        Get
            Return _gtime
        End Get
        Set(ByVal value As Double)
            _gtime = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CameraPosition() As Vector3
        Get
            Return _campos
        End Get
        Set(ByVal value As Vector3)
            _campos = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CameraDirection() As Vector3
        Get
            Return _camdir
        End Get
        Set(ByVal value As Vector3)
            _camdir = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property CameraUp() As Vector3
        Get
            Return _camUp
        End Get
        Set(ByVal value As Vector3)
            _camUp = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Device() As GraphicsDevice
        Get
            Return _device
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <param name="variante"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetVariante(ByVal source As MatrixSource, ByVal variante As MatrixVariante) As Matrix
        Select Case variante
            Case MatrixVariante.None
                Return GetMatrix(source)
            Case MatrixVariante.Inverse
                Return Matrix.Invert(GetMatrix(source))
            Case MatrixVariante.Transpose
                Return Matrix.Transpose(GetMatrix(source))
            Case MatrixVariante.InverseTranspose
                Return Matrix.Transpose(Matrix.Invert(GetMatrix(source)))
        End Select
    End Function

    Private Function GetMatrix(ByVal source As MatrixSource) As Matrix
        Select Case source
            Case MatrixSource.Projection
                Return Projection
            Case MatrixSource.View
                Return View
            Case MatrixSource.World
                Return World
            Case MatrixSource.WorldView
                Return World * View
            Case MatrixSource.ViewProjection
                Return View * Projection
            Case MatrixSource.WorldViewProjection
                Return World * View * Projection
        End Select
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="bound1"></param>
    ''' <param name="bound2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetRandom(ByVal bound1 As Single(), ByVal bound2 As Single()) As Single()

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ViewportIndex() As Integer
        Get
            Return _vindex
        End Get
        Set(ByVal value As Integer)
            _vindex = value
        End Set
    End Property

    Property MousePosition() As Vector2
        Get
            Return _mousepos
        End Get
        Set(ByVal value As Vector2)
            _mousepos = value
        End Set
    End Property

    Private Enum MatrixVariante
        None
        Inverse
        Transpose
        InverseTranspose
    End Enum
    Private Enum MatrixSource
        World
        View
        Projection
        ViewProjection
        WorldView
        WorldViewProjection
    End Enum

    Sub GetValueBySemantic(Of T)(ByVal name As String)

    End Sub
    Function GetValueBySemantic(Of T)(ByVal semantic As KnownSemantics) As T
        Return CType(GetValueBySemantic(semantic), T)
    End Function
    Function GetValueBySemantic(ByVal semantic As KnownSemantics) As Object
        Select Case semantic
            Case KnownSemantics.CameraDirection
                Return CameraDirection
            Case KnownSemantics.CameraPosition
                Return CameraPosition
            Case KnownSemantics.CameraUp
                Return CameraUp
            Case KnownSemantics.ElapsedTime
                Return ElapsedTime
            Case KnownSemantics.LastTime
                Return GameTime - ElapsedTime

            Case KnownSemantics.MousePosition
                Return MousePosition

            Case KnownSemantics.Projection
                Return GetVariante(MatrixSource.Projection, MatrixVariante.None)
            Case KnownSemantics.ProjectionInverse
                Return GetVariante(MatrixSource.Projection, MatrixVariante.Inverse)
            Case KnownSemantics.ProjectionInverseTranspose
                Return GetVariante(MatrixSource.Projection, MatrixVariante.InverseTranspose)
            Case KnownSemantics.ProjectionTranspose
                Return GetVariante(MatrixSource.Projection, MatrixVariante.Transpose)

            Case KnownSemantics.Random
                Return _rnd.NextDouble()

            Case KnownSemantics.Time
                Return GameTime

            Case KnownSemantics.View
                Return GetVariante(MatrixSource.View, MatrixVariante.None)
            Case KnownSemantics.ViewInverse
                Return GetVariante(MatrixSource.View, MatrixVariante.Inverse)
            Case KnownSemantics.ViewInverseTranspose
                Return GetVariante(MatrixSource.View, MatrixVariante.InverseTranspose)
            Case KnownSemantics.ViewTranspose
                Return GetVariante(MatrixSource.View, MatrixVariante.Transpose)

            Case KnownSemantics.ViewProjection
                Return GetVariante(MatrixSource.ViewProjection, MatrixVariante.None)
            Case KnownSemantics.ViewProjectionInverse
                Return GetVariante(MatrixSource.ViewProjection, MatrixVariante.Inverse)
            Case KnownSemantics.ViewProjectionInverseTranspose
                Return GetVariante(MatrixSource.ViewProjection, MatrixVariante.InverseTranspose)
            Case KnownSemantics.ViewProjectionTranspose
                Return GetVariante(MatrixSource.ViewProjection, MatrixVariante.Transpose)

            Case KnownSemantics.World
                Return GetVariante(MatrixSource.World, MatrixVariante.None)
            Case KnownSemantics.WorldInverse
                Return GetVariante(MatrixSource.World, MatrixVariante.Inverse)
            Case KnownSemantics.WorldInverseTranspose
                Return GetVariante(MatrixSource.World, MatrixVariante.InverseTranspose)
            Case KnownSemantics.WorldTranspose
                Return GetVariante(MatrixSource.World, MatrixVariante.Transpose)

            Case KnownSemantics.WorldView
                Return GetVariante(MatrixSource.WorldView, MatrixVariante.None)
            Case KnownSemantics.WorldViewInverse
                Return GetVariante(MatrixSource.WorldView, MatrixVariante.Inverse)
            Case KnownSemantics.WorldViewInverseTranspose
                Return GetVariante(MatrixSource.WorldView, MatrixVariante.InverseTranspose)
            Case KnownSemantics.WorldViewTranspose
                Return GetVariante(MatrixSource.WorldView, MatrixVariante.Transpose)

            Case KnownSemantics.WorldViewProjection
                Return GetVariante(MatrixSource.WorldViewProjection, MatrixVariante.None)
            Case KnownSemantics.WorldViewProjectionInverse
                Return GetVariante(MatrixSource.WorldViewProjection, MatrixVariante.Inverse)
            Case KnownSemantics.WorldViewProjectionInverseTranspose
                Return GetVariante(MatrixSource.WorldViewProjection, MatrixVariante.InverseTranspose)
            Case KnownSemantics.WorldViewProjectionTranspose
                Return GetVariante(MatrixSource.WorldViewProjection, MatrixVariante.Transpose)
        End Select
    End Function

    Public Event PropertyChanged(ByVal source As DrawContext, ByVal propertyName As String, ByVal oldValue As Object, ByVal NewValue As Object)
End Class

