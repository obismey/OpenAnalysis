'Public Class DefaultProvider
'    Inherits Model.Interfaces2.TriangleDataProvider

'    Private _Parameters As Model.Interfaces2.ProcessorParameter()

'    Sub New()
'        MyBase.New()
'        Me._Parameters = New Model.Interfaces2.ProcessorParameter() {
'            New Model.Interfaces2.ProcessorParameter(Me, "", Model.ColumnType.Text),
'            New Model.Interfaces2.ProcessorParameter(Me, "", Model.ColumnType.Text)}
'    End Sub

'    Public Overrides Function Compute(ByVal rows As System.Collections.Generic.IEnumerable(Of Model.Interfaces.IInternalDataRow)) As Model.Interfaces.ITriangleData

'    End Function

'    Public Overrides Function Create(ByVal generator As System.Func(Of Integer, Integer, Double), ByVal rowlabels As System.Collections.Generic.IEnumerable(Of String), ByVal columnlabels As System.Collections.Generic.IEnumerable(Of String)) As Model.Interfaces.ITriangleData
'        Dim result = New ArrayTriangleData()
'        result._RowLabels = rowlabels.ToArray()
'        result._ColumnLabels = columnlabels.ToArray()
'        'result._DiagonalColumn = result._ColumnLabels.Length - 1
'        'result._DiagonalRow = result._RowLabels.Length - 1
'        result._Provider = New DefaultTriangleDataProvider()

'        Dim tempdata(result._RowLabels.Length, result._RowLabels.Length) As Double
'        For i = 0 To result._RowLabels.Length - 1
'            For j = 0 To result._RowLabels.Length - 1
'                tempdata(i, j) = generator(i, j)
'            Next
'        Next

'        result._Data = tempdata
'        Return result
'    End Function

'    Public Overrides ReadOnly Property Parameters As System.Collections.Generic.IEnumerable(Of Model.Interfaces2.ProcessorParameter)
'        Get
'            Return _Parameters
'        End Get
'    End Property
'End Class
