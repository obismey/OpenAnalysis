Public Class StackContainer
    Inherits ControlContainer

    Public Overrides Sub AddChild(ByVal child As Control)
        MyBase.AddChild(child)
    End Sub

    Protected Friend Overrides Function GetAllowedDrawRect(ByVal vis As Control) As Microsoft.Xna.Framework.Rectangle
        Dim index As Integer = Children.IndexOf(vis)

    End Function
End Class

Public Enum StackDirection
    Vertical
    Horizontal
End Enum