Public Class Source(Of T)

    Protected Overridable Function GetValue() As T

    End Function


    Shared Widening Operator CType(ByVal src As Source(Of T)) As T
        Return src.GetValue()
    End Operator

End Class
