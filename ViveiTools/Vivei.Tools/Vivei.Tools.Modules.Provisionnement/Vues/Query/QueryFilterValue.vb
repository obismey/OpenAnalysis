Imports System.Data.SQLite


Public Class QueryFilterValue
    Inherits Core.UI.UIObject

    Private _Row As QueryRow
    Public Property Row As QueryRow
        Get
            Return _Row
        End Get
        Set(ByVal value As QueryRow)
            _Row = value
            OnPropertyChanged("Row")
            OnPropertyChanged("StringValue")
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
            OnPropertyChanged("StringValue")
        End Set
    End Property

    Private _Constant As String = ""
    Public Property Constant As String
        Get
            Return _Constant
        End Get
        Set(ByVal value As String)
            _Constant = value
            OnPropertyChanged("Constant")
            OnPropertyChanged("StringValue")
        End Set
    End Property

    Private _Expression As String = ""
    Public Property Expression As String
        Get
            Return _Expression
        End Get
        Set(ByVal value As String)
            _Expression = value
            OnPropertyChanged("Expression")
            OnPropertyChanged("StringValue")
        End Set
    End Property

    Private _Type As QueryFilterValueType = QueryFilterValueType.Invalid
    Public Property Type As QueryFilterValueType
        Get
            Return _Type
        End Get
        Set(ByVal value As QueryFilterValueType)
            _Type = value
            OnPropertyChanged("Type")
        End Set
    End Property



    Public ReadOnly Property StringValue As String
        Get
            Return Me.ToString()
        End Get
    End Property

    Public Overrides Function ToString() As String
        If _Row IsNot Nothing Then
            Return "t1." & _Row.Name
        End If
        If Not String.IsNullOrEmpty(_OriginalRow) Then
            Return _OriginalRow
        End If
        If Not String.IsNullOrEmpty(_Constant) Then
            Return _Constant
        End If
        If Not String.IsNullOrEmpty(_Expression) Then
            Return _Expression
        End If
        Return "(Invalide)"
    End Function
End Class
