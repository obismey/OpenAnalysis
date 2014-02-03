Imports System.Data.SQLite


Public Class QueryRow
    Inherits Core.UI.UIObject


    Private _Name As String
    Public Property Name As String
        Get
            If String.IsNullOrEmpty(_Name) Then
                If _Formula IsNot Nothing Then
                    If Not _Formula.IsComputed Then
                        If Not String.IsNullOrEmpty(_Aggregate) Then
                            Return _Aggregate & "_de_" & _Formula.Formula
                        End If
                        Return _Formula.Formula
                    End If
                End If
            End If
            Return _Name
        End Get
        Set(ByVal value As String)
            _Name = value
            OnPropertyChanged("Name")
        End Set
    End Property

    Private _Formula As QueryFormula
    Public Property Formula As QueryFormula
        Get
            Return _Formula
        End Get
        Set(ByVal value As QueryFormula)
            _Formula = value
            OnPropertyChanged("Formula")
            OnPropertyChanged("Name")
        End Set
    End Property

    Private _Aggregate As String = ""
    Public Property Aggregate As String
        Get
            Return _Aggregate
        End Get
        Set(ByVal value As String)
            _Aggregate = value
            OnPropertyChanged("Aggregate")
            OnPropertyChanged("Name")
        End Set
    End Property

End Class
