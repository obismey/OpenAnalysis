Imports Vivei.Tools.Modules.Provisionnement.Model.Interfaces

Public Class DefaultCoefficientProcessor
    Implements ITriangleDataProcessor

    Private _View As TriangleView
    Private _NoView As Boolean

    Public Sub Execute() Implements Model.Interfaces.IProcessor.Execute

    End Sub

    Public Function Transform(ByVal triangle As Model.Interfaces.ITriangleData) As Model.Interfaces.ITriangleData Implements Model.Interfaces.ITriangleDataProcessor.Transform

        Dim fun = Function(i As Integer, j As Integer)
                      If j = 0 Then Return Double.NaN


                      Dim n = triangle.GetValue(i, j)
                      Dim d = triangle.GetValue(i, j - 1)

                      If d = 0.0 Or Double.IsNaN(n) Or Double.IsNaN(d) Then Return Double.NaN

                      Return n / d
                  End Function

        Dim t = triangle.GetProvider().Create( _
                        fun, _
                        (From i In Enumerable.Range(1, triangle.RowCount - 1) Select triangle.GetRowLabel(i)).ToArray(), _
                        (From i In Enumerable.Range(1, triangle.ColumnCount - 1) Select triangle.GetColumnLabel(i)).ToArray())


        _View.TriangleGrid.SetLayout(triangle.RowCount + 1, triangle.ColumnCount + 1)

        For i = 0 To t.RowCount - 1
            For j = 0 To t.ColumnCount - 1
                _View.TriangleGrid.GetCell(i + 1, j + 1).Content = If(Double.IsNaN(t.GetValue(i, j)), "", If(t.GetValue(i, j) <> 0, t.GetValue(i, j).ToString("P0"), ""))
            Next
        Next

        For i = 1 To triangle.RowCount
            _View.TriangleGrid.GetCell(i, 0).Content = i
        Next
        For i = 1 To triangle.ColumnCount
            _View.TriangleGrid.GetCell(0, i).Content = i
        Next
        Return t
    End Function

    Public ReadOnly Property View As Object Implements Model.Interfaces.IProcessor.View
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

    Public Sub Initialize(ByVal noview As Boolean, ByVal stage As Model.AnalysisStage) Implements Model.Interfaces.IProcessor.Initialize
        Me._NoView = noview
    End Sub
End Class
