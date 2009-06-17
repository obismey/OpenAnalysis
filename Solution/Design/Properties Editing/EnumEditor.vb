Imports System.Windows.Forms

Public Class EnumEditor
    Inherits ComboBox
    Implements Design.ITypeEditor


    Protected _type As Type
    Public Overridable Sub SetValue(ByVal value As Object) Implements Design.ITypeEditor.SetValue
        If _type Is Nothing Then _type = value.GetType()
        SelectedItem = [Enum].GetName(_type, value)
    End Sub

    Public Event ValueEdited(ByVal sender As Object, ByVal value As Object) Implements Design.ITypeEditor.ValueEdited

    Private Sub EnumEditor_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SelectedIndexChanged
        If SelectedIndex > 0 Then
            RaiseEvent ValueEdited(Me, [Enum].Parse(_type, SelectedItem))
        Else
            RaiseEvent ValueEdited(Me, Nothing)
        End If
    End Sub
End Class
