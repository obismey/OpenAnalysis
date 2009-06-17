Imports System.ComponentModel
Imports System.Windows.Forms

Public Class PropertyControl
    Inherits Panel


    Friend Sub ValueEdited(ByVal sender As Object, ByVal value As Object)
        Try
            If Parent IsNot Nothing Then
                Dim prop As PropertyDescriptor = Tag
                Dim browser As PropertyBrowser = Parent.Parent.Parent.Parent
                prop.SetValue(browser.Selected, value)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.ToString())
        End Try
    End Sub
End Class
