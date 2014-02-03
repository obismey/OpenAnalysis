Imports Vivei.Tools.Core.UI

Public Class ViewInternalData
    Implements INavigationView


    Public Property Caption As String Implements Core.UI.INavigationView.Caption
        Get
            Return "Donnees Modele"
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Public Property Icone As String Implements Core.UI.INavigationView.Icone
        Get

        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        Project.Active.RefreshInternalData()
        PreviewDataGrid.ItemsSource = Project.Active.InternalData.DefaultView
    End Sub
End Class
