Imports Vivei.Tools.Modules.Provisionnement.Model.Interfaces

Public Class DefaultInternalDataColumn
    Implements IInternalDataColumn

    Private _InternalData As DefaultInternalData
    Private _Name As String
    Private _Usage As String
    Private _UsageTag As String
    Private _Type As Model.ColumnType
    Private _Index As Integer

    Sub New(ByVal InternalData As DefaultInternalData, ByVal Name As String, ByVal Index As Integer, ByVal Type As Type, ByVal Usage As String, ByVal UsageTag As String)
        ' TODO: Complete member initialization 
        Me._InternalData = InternalData
        Me._Name = Name
        Me._Index = Index
        Me._Usage = Usage
        Me._UsageTag = UsageTag
        Me._Type = Model.ColumnType.Unknown
        If Type Is GetType(String) Then
            Me._Type = Model.ColumnType.Text
        ElseIf Type Is GetType(Double) Then
            Me._Type = Model.ColumnType.Number
        ElseIf Type Is GetType(Date) Then
            Me._Type = Model.ColumnType.Date
        End If
    End Sub

    Public ReadOnly Property InternalData As Model.Interfaces.IInternalData Implements Model.Interfaces.IInternalDataColumn.InternalData
        Get
            Return _InternalData
        End Get
    End Property

    Public ReadOnly Property Name As String Implements Model.Interfaces.IInternalDataColumn.Name
        Get
            Return _Name
        End Get
    End Property

    Public ReadOnly Property Type As Model.ColumnType Implements Model.Interfaces.IInternalDataColumn.Type
        Get
            Return _Type
        End Get
    End Property

    Public ReadOnly Property Usage As String Implements Model.Interfaces.IInternalDataColumn.Usage
        Get
            Return _Usage
        End Get
    End Property

    Public ReadOnly Property UsageTag As String Implements Model.Interfaces.IInternalDataColumn.UsageTag
        Get
            Return _UsageTag
        End Get
    End Property

    Public ReadOnly Property Index As Integer Implements Model.Interfaces.IInternalDataColumn.Index
        Get
            Return _Index
        End Get
    End Property
End Class
