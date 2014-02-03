Imports System.Data.SQLite


Public Class QueryFormula
    Inherits Core.UI.UIObject

    Private _Formula As String
    Private _IsComputed As Boolean

    Sub New(ByVal Column As String)
        ' TODO: Complete member initialization 
        Me._Formula = Column
        Me._IsComputed = False
    End Sub

    Sub New()
        Me._Formula = "(Colonne calculee)"
        Me._IsComputed = True
    End Sub

    Public Property Formula As String
        Get
            Return _Formula
        End Get
        Set(ByVal value As String)
            _Formula = value
            OnPropertyChanged("Formula")
        End Set
    End Property

    Public ReadOnly Property IsComputed As Boolean
        Get
            Return _IsComputed
        End Get
    End Property
End Class
