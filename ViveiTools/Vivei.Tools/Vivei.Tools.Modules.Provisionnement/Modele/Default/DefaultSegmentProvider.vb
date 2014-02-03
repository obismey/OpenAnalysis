Public Class DefaultSegmentProvider
    Implements Model.Interfaces.IDataSegmentProvider

    Dim _Table As Model.Interfaces.IInternalData
    Dim _Columns As IEnumerable(Of Model.Interfaces.IInternalDataColumn)
    Dim _DSet As DataSet

    Public Function GetFilter(ByVal segment As Model.Interfaces.IDataSegment) As String Implements Model.Interfaces.IDataSegmentProvider.GetFilter

    End Function

    Public Function GetSegments(ByVal column As Model.Interfaces.IInternalDataColumn) As System.Collections.Generic.IEnumerable(Of Model.Interfaces.IDataSegment) Implements Model.Interfaces.IDataSegmentProvider.GetSegments
        If Not _DSet.Tables.Contains("_" & column.Name & "_table_") Then
            Dim tbl = _DSet.Tables("_maintable_").DefaultView.ToTable(True, column.Name)
            tbl.TableName = "_" & column.Name & "_table_"
            _DSet.Tables.Add(tbl)
        End If

        Return _DSet.Tables("_" & column.Name & "_table_").DefaultView.Cast(Of DataRowView).Select(Function(r) New DefaultSegment(r, Me))
    End Function

    Public Sub Initialize(ByVal columns As System.Collections.Generic.IEnumerable(Of Model.Interfaces.IInternalDataColumn)) Implements Model.Interfaces.IDataSegmentProvider.Initialize
        Me._Table = columns.FirstOrDefault().InternalData
        Me._Columns = columns

        'Dim fun = Function(r As Model.Interfaces.IInternalDataRow) As Model.Interfaces.IInternalDataRow

        '          End Function

        Dim comp = New RowComparer(columns)
        Dim q = Me._Table.Select().GroupBy(Function(r) r, comp)
        '  q.FirstOrDefault().
        'Dim defaultdata As DefaultInternalData = Me._Table
        'Dim maintable = defaultdata._Data.ToTable(True, columns.Select(Function(c) c.Name).ToArray())
        'maintable.TableName = "_maintable_"
        'Dim dset = New DataSet()
        'dset.Tables.Add(maintable)
        'Me._DSet = dset

    End Sub

    Public Function Merge(ByVal segments As System.Collections.Generic.IEnumerable(Of Model.Interfaces.IDataSegment)) As Model.Interfaces.IDataSegment Implements Model.Interfaces.IDataSegmentProvider.Merge

    End Function

    Private Class RowComparer
        Implements IEqualityComparer(Of Model.Interfaces.IInternalDataRow)


        Private _columns As IEnumerable(Of Model.Interfaces.IInternalDataColumn)

        Sub New(ByVal columns As IEnumerable(Of Model.Interfaces.IInternalDataColumn))
            _columns = columns
        End Sub


        Public Function Equals1(ByVal x As Model.Interfaces.IInternalDataRow, ByVal y As Model.Interfaces.IInternalDataRow) As Boolean Implements System.Collections.Generic.IEqualityComparer(Of Model.Interfaces.IInternalDataRow).Equals

        End Function

        Public Function GetHashCode1(ByVal obj As Model.Interfaces.IInternalDataRow) As Integer Implements System.Collections.Generic.IEqualityComparer(Of Model.Interfaces.IInternalDataRow).GetHashCode

        End Function
    End Class
End Class



Public Class DefaultSegment
    Implements Model.Interfaces.IDataSegment

    Public Sub New(ByVal row As DataRowView, ByVal provider As DefaultSegmentProvider)

    End Sub

    Public Function GetChildren() As System.Collections.Generic.IEnumerable(Of Model.Interfaces.IDataSegment) Implements Model.Interfaces.IDataSegment.GetChildren

    End Function

    Public Function GetChildren(ByVal column As Model.Interfaces.IInternalDataColumn) As System.Collections.Generic.IEnumerable(Of Model.Interfaces.IDataSegment) Implements Model.Interfaces.IDataSegment.GetChildren

    End Function

    Public Function GetParent() As Model.Interfaces.IDataSegment Implements Model.Interfaces.IDataSegment.GetParent

    End Function

    Public Property Priority As Integer Implements Model.Interfaces.IDataSegment.Priority
        Get

        End Get
        Set(ByVal value As Integer)

        End Set
    End Property

    Public ReadOnly Property Provider As Model.Interfaces.IDataSegmentProvider Implements Model.Interfaces.IDataSegment.Provider
        Get

        End Get
    End Property

    Public ReadOnly Property Text As String Implements Model.Interfaces.IDataSegment.Text
        Get

        End Get
    End Property

    Public Function CompareTo(ByVal other As Model.Interfaces.IDataSegment) As Integer Implements System.IComparable(Of Model.Interfaces.IDataSegment).CompareTo

    End Function
End Class
