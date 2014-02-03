Partial Public Class CsvOptionsWindow
    Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().

    End Sub

    Friend Sub CsvSeparateurTextBox_TextChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.TextChangedEventArgs) Handles CsvSeparateurTextBox.TextChanged
        Try
            CsvDatagrid.ItemsSource = New ObjectModel.ObservableCollection(Of ColumnOption)(From s In FirstLine.Split(New String() {CsvSeparateurTextBox.Text}, StringSplitOptions.None)
                                      Select New ColumnOption() With {.Name = s, .Type = "Texte", .Format = ""})
        Catch ex As Exception

        End Try
    End Sub


    Public Class ColumnOption
        Inherits Core.UI.UIObject

        Private _Name As String
        Public Property Name As String
            Get
                Return _Name
            End Get
            Set(ByVal value As String)
                _Name = value
                OnPropertyChanged("Name")
            End Set
        End Property

        Private _Type As String
        Public Property Type As String
            Get
                Return _Type
            End Get
            Set(ByVal value As String)
                _Type = value
                OnPropertyChanged("Type")
            End Set
        End Property

        Private _Format As String
        Public Property Format As String
            Get
                Return _Format
            End Get
            Set(ByVal value As String)
                _Format = value
                OnPropertyChanged("Format")
            End Set
        End Property


    End Class
    Public Property FirstLine As String
End Class
