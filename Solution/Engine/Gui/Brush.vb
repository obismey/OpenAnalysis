Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public NotInheritable Class Brush

    Private _color As Color = Microsoft.Xna.Framework.Graphics.Color.White
    Private _sourceRect As Rectangle?
    Private _imageSource As String = Nothing
    Friend _texture As Graphics.Texture2D

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Color() As Color
        Get
            Return _color
        End Get
        Set(ByVal value As Color)
            _color = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property SourceRect() As Rectangle?
        Get
            Return _sourceRect
        End Get
        Set(ByVal value As Rectangle?)
            _sourceRect = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ImageSource() As String
        Get
            Return _imageSource
        End Get
        Set(ByVal value As String)
            _imageSource = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="rect"></param>
    ''' <param name="drawer"></param>
    ''' <remarks></remarks>
    Friend Sub Draw(ByVal rect As Rectangle, ByVal drawer As SpriteBatch)
        If String.IsNullOrEmpty(_imageSource) Then
            Dim col As Texture2D = Root.Instance.GetTexture("colormap")
            drawer.Draw(col, rect, Color)
        Else
            Dim texture As Graphics.Texture2D = Root.Instance.GetTexture(ImageSource)
            drawer.Draw(texture, rect, SourceRect, Color)
        End If
    End Sub

    Shared Widening Operator CType(ByVal source As Color) As Brush
        Dim result As New Brush With {._color = source, _
                                      ._imageSource = Nothing, _
                                      ._sourceRect = Nothing, _
                                      ._texture = Nothing}

        Return result
    End Operator
    Shared Widening Operator CType(ByVal source As String) As Brush
        Dim result As New Brush With {._color = Microsoft.Xna.Framework.Graphics.Color.White, _
                                      ._imageSource = source, _
                                      ._sourceRect = Nothing, _
                                      ._texture = Nothing}

        Return result
    End Operator
End Class
