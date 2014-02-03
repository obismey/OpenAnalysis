Imports Vivei.Tools.Modules.Provisionnement.Model.Interfaces

Public Class DefaultInternalDataRow
    Implements IInternalDataRow

    Private _InternalData As DefaultInternalData
    Private _RowView As DataRowView

    Sub New(ByVal InternalData As DefaultInternalData, ByVal RowView As DataRowView)
        ' TODO: Complete member initialization 
        _InternalData = InternalData
        _RowView = RowView
    End Sub

    Public ReadOnly Property InternalData As Model.Interfaces.IInternalData Implements Model.Interfaces.IInternalDataRow.InternalData
        Get
            Return _InternalData
        End Get
    End Property

    Public Function GetValue(ByVal columnindex As Integer) As Object Implements Model.Interfaces.IInternalDataRow.GetValue
        Return Me._RowView(columnindex)
    End Function

    Public Function GetValueAsDate(ByVal columnindex As Integer) As Date? Implements Model.Interfaces.IInternalDataRow.GetValueAsDate
        Return CDate(Me._RowView(columnindex))
    End Function

    Public Function GetValueAsNumber(ByVal columnindex As Integer) As Double? Implements Model.Interfaces.IInternalDataRow.GetValueAsNumber
        Return CDbl(Me._RowView(columnindex))
    End Function

    Public Function GetValueAsString(ByVal columnindex As Integer) As String Implements Model.Interfaces.IInternalDataRow.GetValueAsString
        Return CStr(Me._RowView(columnindex))
    End Function
End Class