Imports System.Windows.Forms

Public Class TextEditor
    Inherits TextBox
    Implements Design.ITypeEditor

    Public Sub SetValue(ByVal value As Object) Implements Design.ITypeEditor.SetValue
        Text = value
    End Sub
    Protected Overrides Sub OnKeyDown(ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.Enter Then
            RaiseEvent ValueEdited(Me, Text)
        End If
        MyBase.OnKeyDown(e)
    End Sub
    Public Event ValueEdited(ByVal sender As Object, ByVal value As Object) Implements Design.ITypeEditor.ValueEdited
End Class
