Imports System.Data.SQLite


Public Class QueryFilter
    Inherits Core.UI.UIObject

    Private _Row As QueryRow
    Public Property Row As QueryRow
        Get
            Return _Row
        End Get
        Set(ByVal value As QueryRow)
            _Row = value
            OnPropertyChanged("Row")
        End Set
    End Property

    Private _OriginalRow As String = ""
    Public Property OriginalRow As String
        Get
            Return _OriginalRow
        End Get
        Set(ByVal value As String)
            _OriginalRow = value
            OnPropertyChanged("OriginalRow")
        End Set
    End Property



    Private _Operator As String
    Public Property [Operator] As String
        Get
            Return _Operator
        End Get
        Set(ByVal value As String)
            _Operator = value
            OnPropertyChanged("Operator")
        End Set
    End Property

    Private _BooleanOperator As String
    Public Property BooleanOperator As String
        Get
            Return _BooleanOperator
        End Get
        Set(ByVal value As String)
            _BooleanOperator = value
            OnPropertyChanged("BooleanOperator")
        End Set
    End Property

    Private _Value As New QueryFilterValue()
    Public Property Value As QueryFilterValue
        Get
            Return _Value
        End Get
        Set(ByVal value As QueryFilterValue)
            _Value = value
            OnPropertyChanged("Value")
        End Set
    End Property

    Private _SimpleValue As String
    Public Property SimpleValue As String
        Get
            Return _SimpleValue
        End Get
        Set(ByVal value As String)
            _SimpleValue = value
            OnPropertyChanged("Simplevalue")
        End Set
    End Property


End Class
