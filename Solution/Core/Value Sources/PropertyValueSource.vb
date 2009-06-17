Imports System.Reflection

Public Class PropertyValueSource(Of T)
    Implements IValueSource(Of T)




    Public Function GetValue() As T Implements IValueSource(Of T).GetValue

    End Function

    Public Function GetValue(ByVal destination As Object) As T Implements IValueSource(Of T).GetValue

    End Function

    Public Function GetValue(ByVal destination As Object, ByVal destinationProperty As String) As T Implements IValueSource(Of T).GetValue

    End Function
    Private _sourceObject As Object
    Public Property SourceObject() As Object
        Get
            Return _sourceObject
        End Get
        Set(ByVal value As Object)
            _sourceObject = value
        End Set
    End Property
    Private _propertyName As String
    Public Property PropertyName() As String
        Get
            Return _propertyName
        End Get
        Set(ByVal value As String)
            _propertyName = value
        End Set
    End Property


   
End Class
