Imports System.Data.SQLite


Public Class SqlQueryData

    Public Rows As ObjectModel.ObservableCollection(Of QueryRow)
    Public Sorts As ObjectModel.ObservableCollection(Of QuerySort)
    Public Filters As ObjectModel.ObservableCollection(Of QueryFilter)
    Public Originalfilters As ObjectModel.ObservableCollection(Of QueryFilter)
    Public Sources As ObjectModel.ObservableCollection(Of QuerySource)

    Public SqlQueryText As String
    Public SqlQueryData As DataTable

    Private _inc As Integer



    Sub New()
        _inc += 1
        Name = "Query " & _inc
    End Sub

    Public Property Name As String

    Public Sub New(ByVal rows, ByVal sorts, ByVal filters, ByVal originalfilters, ByVal sources)
        _inc += 1
        Name = "Query " & _inc
        Me.Rows = rows
        Me.Sorts = sorts
        Me.Filters = filters
        Me.Originalfilters = originalfilters
        Me.Sources = sources
    End Sub
End Class
