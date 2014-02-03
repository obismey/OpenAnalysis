Public Class UIServiceImpl
    Implements Core.UI.IUIService

    Private _Menu As Core.UI.MenuItemCollection
    Private _NavigationGroup As Core.UI.NavigationGroupCollection

    Public ReadOnly Property Menu As Core.UI.MenuItemCollection Implements Core.UI.IUIService.Menu
        Get
            If _Menu Is Nothing Then
                Dim result = New Core.UI.MenuItemCollection()
                result.Add(New Core.UI.MenuItem("Fichier"))
                result.Add(New Core.UI.MenuItem("Edition"))
                result.Add(New Core.UI.MenuItem("Affichage"))
                result.Add(New Core.UI.MenuItem("Outils"))
                result.Add(New Core.UI.MenuItem("Aide"))

                result(0).Children.Add(New Core.UI.MenuItem("Ouvrir", Sub() MessageBox.Show("Ouvrir")))
                result(0).Children.Add(New Core.UI.MenuItem("Fermer", Sub() MessageBox.Show("Fermer")))
                _Menu = result
            End If
            Return _Menu
        End Get
    End Property

    Public Sub Reset() Implements Core.IService.Reset
        _Menu = Nothing
        _NavigationGroup = Nothing
    End Sub

    Public ReadOnly Property NavigationGroup As Core.UI.NavigationGroupCollection Implements Core.UI.IUIService.NavigationGroup
        Get
            If _NavigationGroup Is Nothing Then
                Dim result = New Core.UI.NavigationGroupCollection()
                result.Add(New Core.UI.NavigationGroup("Tarification"))
                result.Add(New Core.UI.NavigationGroup("Provisionnement"))
                result.Add(New Core.UI.NavigationGroup("Previsions"))
                result.Add(New Core.UI.NavigationGroup("Documentation"))

                Dim views = New Core.UI.NavigationViewCollection()
                result(0).Nodes(0).Children.Add(New Core.UI.NavigationNode("Child 1", views))
                result(0).Nodes(0).Children.Add(New Core.UI.NavigationNode("Child 2", views))
                result(0).Nodes(0).Children.Add(New Core.UI.NavigationNode("Child 3", views))
                result(0).Nodes(0).Children.Add(New Core.UI.NavigationNode("Child 4", views))


                result(0).Nodes(0).Children(2).Children.Add(New Core.UI.NavigationNode("Child 31", views))
                result(0).Nodes(0).Children(2).Children.Add(New Core.UI.NavigationNode("Child 32", views))
                result(0).Nodes(0).Children(2).Children.Add(New Core.UI.NavigationNode("Child 33", views))
                result(0).Nodes(0).Children(2).Children.Add(New Core.UI.NavigationNode("Child 34", views))

                _NavigationGroup = result
            End If
            Return _NavigationGroup
        End Get
    End Property
End Class