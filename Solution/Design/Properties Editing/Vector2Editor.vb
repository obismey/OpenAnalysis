Imports Microsoft.Xna.Framework

Public Class Vector2Editor
    Implements ITypeEditor


    Public Sub SetValue(ByVal value As Object) Implements ITypeEditor.SetValue

    End Sub

    Public Event ValueEdited(ByVal sender As Object, ByVal value As Object) Implements ITypeEditor.ValueEdited

    Private Sub nupX_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nupX.ValueChanged
        RaiseEvent ValueEdited(Me, New Vector2(nupX.Value, nupY.Value))

    End Sub

    Private Sub nupY_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nupY.ValueChanged
        RaiseEvent ValueEdited(Me, New Vector2(nupX.Value, nupY.Value))
    End Sub
End Class
