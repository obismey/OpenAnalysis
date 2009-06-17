Public Class ConstantValueSource(Of T)
    Implements IValueSource(Of T)


    Private Sub New()

    End Sub

    Private Value As T


    Public Function GetValue() As T Implements IValueSource(Of T).GetValue
        Return Value
    End Function
    Public Function GetValue(ByVal destination As Object) As T Implements IValueSource(Of T).GetValue
        Return Value
    End Function
    Public Function GetValue(ByVal destination As Object, ByVal destinationProperty As String) As T Implements IValueSource(Of T).GetValue
        Return Value
    End Function

    Public Shared Widening Operator CType(ByVal source As T) As ConstantValueSource(Of T)
        Return New ConstantValueSource(Of T)() With {.Value = source}
    End Operator
    Public Shared Widening Operator CType(ByVal source As ConstantValueSource(Of T)) As T
        Return source.Value
    End Operator

End Class
