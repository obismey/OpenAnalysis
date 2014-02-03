Imports System.Data.SQLite


Public Class QuerySource
    Inherits Core.UI.UIObject

    Private _DataTable As String
    Public Property DataTable As String
        Get
            Return _DataTable
        End Get
        Set(ByVal value As String)
            _DataTable = value
            OnPropertyChanged("DataTable")
        End Set
    End Property

    Private _QueryData As SqlQueryData
    Public Property QueryData As SqlQueryData
        Get
            Return _QueryData
        End Get
        Set(ByVal value As SqlQueryData)
            _QueryData = value
            OnPropertyChanged("QueryData")
        End Set
    End Property

    Public Overrides Function ToString() As String
        If Not String.IsNullOrEmpty(_DataTable) Then
            Return _DataTable
        End If
        If _QueryData IsNot Nothing Then
            Return _QueryData.Name
        End If
        Return MyBase.ToString()
    End Function
End Class
