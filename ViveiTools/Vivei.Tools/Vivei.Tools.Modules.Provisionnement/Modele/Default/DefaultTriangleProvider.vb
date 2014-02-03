Imports Vivei.Tools.Modules.Provisionnement.Model.Interfaces
Imports System.Collections.Generic

Public Class DefaultTriangleDataProvider
    Inherits Core.UI.UIObject
    Implements ITriangleDataProvider

    Private _View As TriangleView
    Private _NoView As Boolean

    Public Sub New()

    End Sub

    Public Function Compute(ByVal data As IInternalData) As ITriangleData Implements ITriangleDataProvider.Compute
        Dim survcol = GetSurvenanceColumn(data)
        Dim sincol = GetSinistreColumn(data)
        Dim dercol = GetDeroulementColumn(data)

        Dim q0 = (From r In data.Select()
                  Group By Row = r.GetValueAsNumber(survcol),
                           Column = r.GetValueAsNumber(dercol)
                  Into Value = Sum(r.GetValueAsNumber(sincol))).ToArray()


        Dim maxderoulement = Aggregate elt In q0 Into Max(elt.Column)

        Dim maxsurvenance = Aggregate elt In q0 Into Max(elt.Row)

        Dim result = New ArrayTriangleData()

        result._Provider = New DefaultTriangleDataProvider()
        result._RowLabels = (From i In Enumerable.Range(0, maxsurvenance + 1) Select CStr(i)).ToArray()
        result._ColumnLabels = (From i In Enumerable.Range(0, maxderoulement + 1) Select CStr(i)).ToArray()


        Dim tempdata(maxsurvenance, maxderoulement) As Double
        _View.TriangleGrid.SetLayout(maxsurvenance + 2, maxderoulement + 2)

        For Each elt In q0
            tempdata(elt.Row, elt.Column) = elt.Value
        Next
        For i = 0 To tempdata.GetLength(0) - 1
            For j = 1 To Math.Min(tempdata.GetLength(1) - 1, tempdata.GetLength(0) - i - 1)
                tempdata(i, j) = tempdata(i, j - 1) + tempdata(i, j)
                _View.TriangleGrid.GetCell(i + 1, j + 1).Content = tempdata(i, j).ToString("N0")
            Next
        Next
        For i = 0 To tempdata.GetLength(0) - 1
            _View.TriangleGrid.GetCell(i + 1, 1).Content = tempdata(i, 0).ToString("N0")
        Next

        For i = 1 To maxsurvenance + 1
            _View.TriangleGrid.GetCell(i, 0).Content = i
        Next
        For i = 1 To maxderoulement + 1
            _View.TriangleGrid.GetCell(0, i).Content = i
        Next
        result._Data = tempdata
        Return result

    End Function
    Public Function Compute(ByVal data As IInternalData, ByVal filter As String) As ITriangleData Implements ITriangleDataProvider.Compute
        Dim survcol = GetSurvenanceColumn(data)
        Dim sincol = GetSinistreColumn(data)
        Dim dercol = GetDeroulementColumn(data)

        Dim q0 = (From r In data.Select(filter, "")
                  Group By Row = r.GetValueAsNumber(survcol),
                           Column = r.GetValueAsNumber(dercol)
                  Into Value = Sum(r.GetValueAsNumber(sincol))).ToArray()


        Dim maxderoulement = Aggregate elt In q0 Into Max(elt.Column)

        Dim maxsurvenance = Aggregate elt In q0 Into Max(elt.Row)

        Dim result = New ArrayTriangleData()

        result._Provider = New DefaultTriangleDataProvider()
        result._RowLabels = (From i In Enumerable.Range(0, maxsurvenance + 1) Select CStr(i)).ToArray()
        result._ColumnLabels = (From i In Enumerable.Range(0, maxderoulement + 1) Select CStr(i)).ToArray()


        Dim tempdata(maxsurvenance, maxderoulement) As Double
        _View.TriangleGrid.SetLayout(maxsurvenance + 2, maxderoulement + 2)
        For Each elt In q0
            tempdata(elt.Row, elt.Column) = elt.Value
            _View.TriangleGrid.GetCell(elt.Row + 1, elt.Column + 1).Content = elt.Value.ToString("N0")
        Next
        For i = 1 To maxsurvenance + 1
            _View.TriangleGrid.GetCell(i, 0).Content = i
        Next
        For i = 1 To maxderoulement + 1
            _View.TriangleGrid.GetCell(0, i).Content = i
        Next
        result._Data = tempdata
        Return result

    End Function
    Public Function Compute(ByVal data As IInternalData, ByVal segment As IDataSegment) As ITriangleData Implements ITriangleDataProvider.Compute
        Dim survcol = GetSurvenanceColumn(data)
        Dim sincol = GetSinistreColumn(data)
        Dim dercol = GetDeroulementColumn(data)

        Dim q0 = (From r In data.Select(segment)
                  Group By Row = r.GetValueAsNumber(survcol),
                           Column = r.GetValueAsNumber(dercol)
                  Into Value = Sum(r.GetValueAsNumber(sincol))).ToArray()


        Dim maxderoulement = Aggregate elt In q0 Into Max(elt.Column)

        Dim maxsurvenance = Aggregate elt In q0 Into Max(elt.Row)

        Dim result = New ArrayTriangleData()

        result._Provider = New DefaultTriangleDataProvider()
        result._RowLabels = (From i In Enumerable.Range(0, maxsurvenance + 1) Select CStr(i)).ToArray()
        result._ColumnLabels = (From i In Enumerable.Range(0, maxderoulement + 1) Select CStr(i)).ToArray()


        Dim tempdata(maxsurvenance, maxderoulement) As Double
        _View.TriangleGrid.SetLayout(maxsurvenance + 2, maxderoulement + 2)
        For Each elt In q0
            tempdata(elt.Row, elt.Column) = elt.Value
            _View.TriangleGrid.GetCell(elt.Row + 1, elt.Column + 1).Content = elt.Value.ToString("N0")
        Next
        For i = 1 To maxsurvenance + 1
            _View.TriangleGrid.GetCell(i, 0).Content = i
        Next
        For i = 1 To maxderoulement + 1
            _View.TriangleGrid.GetCell(0, i).Content = i
        Next
        result._Data = tempdata
        Return result


    End Function

    Public Function Create(ByVal generator As System.Func(Of Integer, Integer, Double), ByVal rowlabels As System.Collections.Generic.IEnumerable(Of String), ByVal columnlabels As System.Collections.Generic.IEnumerable(Of String)) As Model.Interfaces.ITriangleData Implements Model.Interfaces.ITriangleDataProvider.Create
        Dim result = New ArrayTriangleData()
        result._RowLabels = rowlabels.ToArray()
        result._ColumnLabels = columnlabels.ToArray()
        result._DiagonalColumn = result._ColumnLabels.Length - 1
        result._DiagonalRow = result._RowLabels.Length - 1
        result._Provider = New DefaultTriangleDataProvider()

        Dim tempdata(result._RowLabels.Length, result._ColumnLabels.Length) As Double
        For i = 0 To result._RowLabels.Length - 1
            For j = 0 To result._ColumnLabels.Length - 1
                tempdata(i, j) = generator(i, j)
            Next
        Next

        result._Data = tempdata
        Return result
    End Function

    Private Function GetSurvenanceColumn(ByVal data As IInternalData) As Integer
        Try
            Return (From c In data.Columns Where c.Usage.Trim().ToLower() = "survenance" And c.Type = Model.ColumnType.Number Select c.Index).FirstOrDefault()
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Private Function GetSinistreColumn(ByVal data As IInternalData) As Integer
        Try
            Return (From c In data.Columns Where c.Usage.Trim().ToLower() = "sinistre" And c.Type = Model.ColumnType.Number Select c.Index).FirstOrDefault()
        Catch ex As Exception
            Return -1
        End Try
    End Function
    Private Function GetDeroulementColumn(ByVal data As IInternalData) As Integer
        Try
            Return (From c In data.Columns Where c.Usage.Trim().ToLower() = "deroulement" And c.Type = Model.ColumnType.Number Select c.Index).FirstOrDefault()
        Catch ex As Exception
            Return -1
        End Try
    End Function

    Public ReadOnly Property View As Object Implements Model.Interfaces.ITriangleDataProvider.View
        Get
            If Not Me._NoView Then
                If Me._View Is Nothing Then
                    Me._View = New TriangleView()
                End If
                Return Me._View
            Else
                Return Nothing
            End If
        End Get
    End Property

    Public Sub Execute() Implements Model.Interfaces.IProcessor.Execute
        Throw New NotImplementedException()
    End Sub

    Public Sub Initialize(ByVal noview As Boolean, ByVal stage As Model.AnalysisStage) Implements Model.Interfaces.IProcessor.Initialize
        Me._NoView = noview
    End Sub

    Public Function Create(ByVal generator As System.Func(Of Integer, Integer, Double), ByVal rowlabels As System.Func(Of Integer, String), ByVal columnlabels As System.Func(Of Integer, String)) As Model.Interfaces.ITriangleData Implements Model.Interfaces.ITriangleDataProvider.Create

    End Function

    <ProcessorArgument(Direction:=Model.ArgumentDirection.InArgument, Type:=Model.ArgumentType.InternalData)>
    Public Property InternalData As IInternalData

    <ProcessorArgument(Direction:=Model.ArgumentDirection.OutArgument, Type:=Model.ArgumentType.TriangleData)>
    Public Property TriangleData As ITriangleData

#Region "IDisposable Support"
    Private disposedValue As Boolean ' Pour détecter les appels redondants

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: supprimez l'état managé (objets managés).
            End If

            ' TODO: libérez les ressources non managées (objets non managés) et substituez la méthode Finalize() ci-dessous.
            ' TODO: définissez les champs volumineux à null.
        End If
        Me.disposedValue = True
    End Sub

    ' TODO: substituez Finalize() uniquement si Dispose(ByVal disposing As Boolean) ci-dessus comporte du code permettant de libérer des ressources non managées.
    'Protected Overrides Sub Finalize()
    '    ' Ne modifiez pas ce code. Ajoutez du code de nettoyage dans Dispose(ByVal disposing As Boolean) ci-dessus.
    '    Dispose(False)
    '    MyBase.Finalize()
    'End Sub

    ' Ce code a été ajouté par Visual Basic pour permettre l'implémentation correcte du modèle pouvant être supprimé.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Ne modifiez pas ce code. Ajoutez du code de nettoyage dans Dispose(ByVal disposing As Boolean) ci-dessus.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region


End Class
