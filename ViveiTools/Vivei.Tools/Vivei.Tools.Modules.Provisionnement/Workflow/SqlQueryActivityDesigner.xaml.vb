Imports System.Data.SQLite

Class SqlQueryActivityDesigner
    Private _QueryData As SqlQueryData

    Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().

    End Sub

  

    Private Sub SqlQueryActivityDesigner_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        If Project.Active.ExternalData Is Nothing Then
            ErrorBorder.Visibility = Windows.Visibility.Visible
        End If
    End Sub

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If Project._workflow Is Nothing Then
            Project._workflow = New SqlQueryWorkflow()
        End If
        Dim qw = New QueryWindow()
        If _QueryData IsNot Nothing Then
            qw.SetQueryData(Me._QueryData, Project.Active.ExternalData, Project._workflow)
        Else
            qw.SetDataTable(Project.Active.ExternalData, Project._workflow)
        End If
        qw.ShowDialog()
        Me._QueryData = qw.GetQueryData()

        Project._workflow.Queries.Add(Me._QueryData)
    End Sub
End Class
