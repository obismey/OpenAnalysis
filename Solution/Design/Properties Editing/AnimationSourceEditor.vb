Public Class AnimationSourceEditor
    Implements ITypeEditor



    Public Sub SetValue(ByVal value As Object) Implements ITypeEditor.SetValue

    End Sub

    Public Event ValueEdited(ByVal sender As Object, ByVal value As Object) Implements ITypeEditor.ValueEdited
End Class
