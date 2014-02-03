Imports Vivei.Tools.Modules.Provisionnement.Model.Interfaces

Public Class ArrayTriangleData
    Implements ITriangleData


    Friend _Data(,) As Double
    Friend _RowLabels() As String
    Friend _ColumnLabels() As String
    Friend _DiagonalColumn As Integer
    Friend _DiagonalRow As Integer
    Friend _Provider As ITriangleDataProvider
    Dim _Format As TriangleFormat

    Public ReadOnly Property ColumnCount As Integer Implements Model.Interfaces.ITriangleData.ColumnCount
        Get
            Return _Data.GetLength(1)
        End Get
    End Property
    Public ReadOnly Property RowCount As Integer Implements Model.Interfaces.ITriangleData.RowCount
        Get
            Return _Data.GetLength(0)
        End Get
    End Property
    Public ReadOnly Property DiagonalColumn As Integer Implements Model.Interfaces.ITriangleData.DiagonalColumn
        Get
            Return Me._DiagonalColumn
        End Get
    End Property
    Public ReadOnly Property DiagonalRow As Integer Implements Model.Interfaces.ITriangleData.DiagonalRow
        Get
            Return Me._DiagonalRow
        End Get
    End Property
    Public Function GetColumnLabel(ByVal index As Integer) As String Implements Model.Interfaces.ITriangleData.GetColumnLabel
        Return Me._ColumnLabels(index)
    End Function
    Public Function GetProperty(ByVal name As String, ByVal rowindex As Integer, ByVal columnindex As Integer) As Model.Property Implements Model.Interfaces.ITriangleData.GetProperty
        Return Nothing
    End Function
    Public Function GetProvider() As Model.Interfaces.ITriangleDataProvider Implements Model.Interfaces.ITriangleData.GetProvider
        Return Me._Provider
    End Function
    Public Function GetRowLabel(ByVal index As Integer) As String Implements Model.Interfaces.ITriangleData.GetRowLabel
        Return Me._RowLabels(index)
    End Function
    Public Function GetValue(ByVal rowindex As Integer, ByVal columnindex As Integer) As Double Implements Model.Interfaces.ITriangleData.GetValue
        Return Me._Data(rowindex, columnindex)
    End Function

    Public ReadOnly Property Format As Model.Interfaces.TriangleFormat Implements Model.Interfaces.ITriangleData.Format
        Get
            Return _Format
        End Get
    End Property
End Class
