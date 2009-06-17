Public Interface IValueSource
    Inherits IExtentedProperty


 
End Interface
Public Interface IValueSource(Of T)
    Inherits IValueSource

    Function GetValue() As T
    Function GetValue(ByVal destination As Object) As T
    Function GetValue(ByVal destination As Object, ByVal destinationProperty As String) As T
End Interface