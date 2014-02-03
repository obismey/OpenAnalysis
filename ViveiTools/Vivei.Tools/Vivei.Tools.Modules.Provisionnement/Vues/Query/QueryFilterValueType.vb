Imports System.Data.SQLite


Public Enum QueryFilterValueType
    Invalid
    Row
    OriginalRow
    Constant
    Expression
End Enum

'Public MustInherit Class EnumToBooleanConverter
'    Implements Windows.Data.IValueConverter


'    Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
'        Try
'            Return [Enum].Parse(value.GetType(), CStr(parameter)).Equals(value)
'        Catch ex As Exception
'            Return Nothing
'        End Try
'    End Function

'    Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
'        Try
'            If CBool(value) Then
'                Return
'            End If
'        Catch ex As Exception
'            Return Nothing
'        End Try
'    End Function
'End Class