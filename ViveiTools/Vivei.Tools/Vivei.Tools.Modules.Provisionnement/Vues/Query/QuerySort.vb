Imports System.Data.SQLite

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class QuerySort
    Inherits Core.UI.UIObject

    Private _Row As QueryRow
    Private _Direction As String

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Row As QueryRow
        Get
            Return _Row
        End Get
        Set(ByVal value As QueryRow)
            _Row = value
            OnPropertyChanged("Row")
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Direction As String
        Get
            Return _Direction
        End Get
        Set(ByVal value As String)
            _Direction = value
            OnPropertyChanged("Direction")
        End Set
    End Property

End Class
