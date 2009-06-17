Imports System.Reflection

Public Class BaseObject

    Private valueSources As New Dictionary(Of String, Object)


    Sub New()

    End Sub

    Protected Function GetValue(Of T)(ByVal basePropertyName As String) As T
        Dim valueSource As IValueSource(Of T) = CType(valueSources, IValueSource(Of T))
        Return valueSource.GetValue()
    End Function
    Protected Sub SetValue(Of T)(ByVal propertyName As String, ByVal value As T)
        valueSources(propertyName) = CType(value, ConstantValueSource(Of T))
    End Sub

    Public Sub Attach(Of T)(ByVal propertyName As String, ByVal source As IValueSource(Of T))
        valueSources(propertyName) = source
    End Sub

    Public Property Loki() As Single
        Get
            Return GetValue(Of Single)("loki")
        End Get
        Set(ByVal value As Single)
            SetValue(Of Single)("loki", value)
        End Set
    End Property

    Shared lokiBase As BaseProperty(Of Single) = BasePropertyKey.Resgister(Of Single)("loki", True, GetType(BaseObject))
End Class
