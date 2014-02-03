Imports Vivei.Tools.Modules.Provisionnement.Model.Interfaces

Public Class DefaultInternalData
    Implements IInternalData

    Friend _Data As DataView
    Private _Columns As List(Of DefaultInternalDataColumn)

    Friend Sub New(ByVal Data As DataTable, ByVal usage() As String, ByVal usagetag() As String)
        Me._Data = New DataView(Data)
        _Columns = Data.Columns.Cast(Of DataColumn).Select(Function(c, i) New DefaultInternalDataColumn(Me, c.ColumnName, i, c.DataType, usage(i), usagetag(i))).ToList()
    End Sub

    Public ReadOnly Property Columns As System.Collections.Generic.IEnumerable(Of Model.Interfaces.IInternalDataColumn) Implements Model.Interfaces.IInternalData.Columns
        Get
            Return _Columns
        End Get
    End Property

    Public Function [Select]() As System.Collections.Generic.IEnumerable(Of Model.Interfaces.IInternalDataRow) Implements Model.Interfaces.IInternalData.Select
        Me._Data.RowFilter = ""
        Me._Data.Sort = ""
        Return From r As DataRowView In _Data Select New DefaultInternalDataRow(Me, r)
    End Function

    Public Function [Select](ByVal filter As String, ByVal sort As String) As System.Collections.Generic.IEnumerable(Of Model.Interfaces.IInternalDataRow) Implements Model.Interfaces.IInternalData.Select
        Me._Data.RowFilter = filter
        Me._Data.Sort = sort
        Return From r As DataRowView In _Data Select New DefaultInternalDataRow(Me, r)
    End Function

    Public Function [Select](ByVal segement As Model.Interfaces.IDataSegment) As System.Collections.Generic.IEnumerable(Of Model.Interfaces.IInternalDataRow) Implements Model.Interfaces.IInternalData.Select
        Return Nothing
    End Function

    
End Class

