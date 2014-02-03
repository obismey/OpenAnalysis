Imports System.Data


Public Class NamedObject

    Dim _Name As String
    Dim _Value As Object

    Public Sub New(ByVal Name As String, ByVal Value As Object)
        ' TODO: Complete member initialization 
        Me._Name = Name
        Me._Value = Value
    End Sub
    Public Sub New()

    End Sub
    Public ReadOnly Property Name As String
        Get
            Return _Name
        End Get
    End Property
    Public ReadOnly Property Value As Object
        Get
            Return _Value
        End Get
    End Property

    Public Overrides Function GetHashCode() As Integer
        Return _Value.GetHashCode()
    End Function

    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Dim ObjNamedObject = TryCast(obj, NamedObject)
        If ObjNamedObject Is Nothing Then Return False
        Return Me._Value.Equals(ObjNamedObject._Value)
    End Function
End Class

Public Class ObjectToNamedObjectConverter
    Implements Windows.Data.IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        If value Is Nothing Then Return Nothing
        Return New NamedObject(targetType.Name, value)
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        Dim NamedObject = TryCast(value, NamedObject)
        If NamedObject Is Nothing Then Return Nothing
        Return NamedObject.Value
    End Function
End Class