Imports Microsoft.Xna.Framework
Public Class TextureBrush
    Inherits Brush

    Private _sourceRect As Rectangle?
    Private _imageSource As String

    Property SourceRect() As Rectangle?
        Get
            Return _sourceRect
        End Get
        Set(ByVal value As Rectangle?)
            _sourceRect = value
        End Set
    End Property

    Property ImageSource() As String
        Get
            Return _imageSource
        End Get
        Set(ByVal value As String)
            _imageSource = value
        End Set
    End Property

    Friend _texture As Graphics.Texture2D
    Protected Friend Overrides Sub Draw(ByVal rect As Microsoft.Xna.Framework.Rectangle, ByVal drawer As Microsoft.Xna.Framework.Graphics.SpriteBatch)
        Dim texture As Graphics.Texture2D = Root.Instance.GetTexture(ImageSource)

        drawer.Draw(texture, rect, SourceRect, Color)
    End Sub

    
End Class
