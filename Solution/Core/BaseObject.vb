Public Class BaseObject

    Sub SetValue(ByVal target As Object, ByVal value As Object)

    End Sub
    Sub SetValue(ByVal target As BaseProperty, ByVal value As Object)

    End Sub
    Sub SetValue(ByVal target As String, ByVal value As Object)

    End Sub
    Sub SetValue(Of T)(ByVal target As Object, ByVal value As T)

    End Sub
    Sub SetValue(Of T)(ByVal target As BaseProperty, ByVal value As T)

    End Sub
    Sub SetValue(Of T)(ByVal target As String, ByVal value As T)

    End Sub
End Class
