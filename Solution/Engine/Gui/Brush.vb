Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class Brush

    Friend _color As Color = Microsoft.Xna.Framework.Graphics.Color.White

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
    ''' <param name="rect"></param>
    ''' <param name="drawer"></param>
    ''' <remarks></remarks>
    Protected Friend Overridable Sub Draw(ByVal rect As Rectangle, ByVal drawer As SpriteBatch)
        Dim col As Texture2D = Root.Instance.GetTexture("colormap")
        drawer.Draw(col, rect, Color)
    End Sub

    Shared Widening Operator CType(ByVal source As Color) As Brush
        Return New Brush With {._color = source}
    End Operator
    Shared Widening Operator CType(ByVal source As Graphics.Texture2D) As Brush
        Return New TextureBrush() With {._texture = source}
    End Operator
End Class
