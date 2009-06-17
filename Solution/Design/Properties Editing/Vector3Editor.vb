Imports Microsoft.Xna.Framework

Public Class Vector3Editor
    Implements ITypeEditor



    Public Sub SetValue(ByVal value As Object) Implements ITypeEditor.SetValue

    End Sub

    Public Event ValueEdited(ByVal sender As Object, ByVal value As Object) Implements ITypeEditor.ValueEdited

    Private Sub nupX_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nupX.ValueChanged
        RaiseEvent ValueEdited(Me, New Vector3(nupX.Value, nupY.Value, nupZ.Value))
    End Sub

    Private Sub nupY_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nupY.ValueChanged
        RaiseEvent ValueEdited(Me, New Vector3(nupX.Value, nupY.Value, nupZ.Value))
    End Sub


    Private Sub nupZ_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles nupZ.ValueChanged
        RaiseEvent ValueEdited(Me, New Vector3(nupX.Value, nupY.Value, nupZ.Value))
    End Sub
End Class
