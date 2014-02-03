Imports Vivei.Tools.Core
Imports Vivei.Tools.Core.UI

Public Class ProvisionnementModule
    Implements IPlugin

    Friend Shared _ActiveProject As New Project
    Private _RootNode As NavigationNode
    Private _UIService As IUIService
    Friend Shared _RootTriangleStandard As NavigationNode
    Private _BatchNode As NavigationNode


    Public Function GetServices() As System.Collections.Generic.IEnumerable(Of Core.IService) Implements Core.IPlugin.GetServices
        Return New IService() {}
    End Function

    Private Sub CreateDefaultExtternalDataDashboard()

        Dim dbv = SimpleDashboard.Create(4, 2)
        Dim dbn = _RootNode.Children(0).Children(3).Children.Add( _
                    "Resume", _
                    _RootNode.Children(0).Children(3).Views, _
                    dbv)
        Windows.Data.BindingOperations.SetBinding(dbv, SimpleDashboard.DataContextProperty, New Windows.Data.Binding("ExternalData") With {.Source = Project.Active})
        dbn.View = dbv
        dbn.View.Caption = "Resume des donnees"

        'dbv.SetRowSize(0, Windows.GridLength.Auto)
        'dbv.SetColumnSize(0, Windows.GridLength.Auto)

        Dim txt As Windows.Controls.TextBlock = dbv.AddTextZone("Qualite des donnees", 0, 1, 0, 2).Content
        txt.Margin = New Windows.Thickness(20)
        Dim ch As Infragistics.Windows.Chart.XamChart = dbv.AddChartZone(1, 3, 0, 1).Content
        Dim s = New Infragistics.Windows.Chart.Series()
        s.DataPoints.Add(1.0)
        s.DataPoints.Add(1.5)
        s.DataPoints.Add(0.75)
        ch.Series.Add(s)

        Dim g1 As Infragistics.Controls.Charts.XamRadialGauge = dbv.AddGaugeZone(1, 1, 1, 1).Content
        Dim sc1 = New Infragistics.Controls.Charts.RadialGaugeScale()
        sc1.Needles.Add(New Infragistics.Controls.Charts.RadialGaugeNeedle())
        sc1.LabelGroups.Add(New Infragistics.Controls.Charts.RadialGaugeLabelGroup())
        g1.Scales.Add(sc1)

        Dim g2 = dbv.AddGaugeZone(2, 1, 1, 1).Content
        Dim sc2 = New Infragistics.Controls.Charts.RadialGaugeScale()
        sc2.Needles.Add(New Infragistics.Controls.Charts.RadialGaugeNeedle())
        sc2.LabelGroups.Add(New Infragistics.Controls.Charts.RadialGaugeLabelGroup())
        g2.Scales.Add(sc2)

        Dim g3 = dbv.AddGaugeZone(3, 1, 1, 1).Content
        Dim sc3 = New Infragistics.Controls.Charts.RadialGaugeScale()
        sc3.Needles.Add(New Infragistics.Controls.Charts.RadialGaugeNeedle())
        sc3.LabelGroups.Add(New Infragistics.Controls.Charts.RadialGaugeLabelGroup())
        g3.Scales.Add(sc3)

        dbv.SetRowSize(0, Windows.GridLength.Auto)
        dbv.SetColumnSize(1, Windows.GridLength.Auto)
    End Sub


    Public Sub Reset(ByVal Application As Core.IApplication) Implements Core.IPlugin.Reset
        Dim _UIService As IUIService = Nothing
        For Each s In Application.GetServices()
            _UIService = TryCast(s, IUIService)
            If _UIService IsNot Nothing Then Exit For
        Next


        _RootNode = New NavigationNode("Projet")
        _BatchNode = New NavigationNode("Batchs")
        Dim views = New NavigationViewCollection()
        _RootNode.Children.Add("Donnees")
        _RootNode.Children(0).Children.Add("Donnees Externes", views, New ViewExternalData())
        _RootNode.Children(0).Children.Add("Modele Interne", views, New ViewDataModel())
        _RootNode.Children(0).Children.Add("Donnees Modele", views, New ViewInternalData())
        _RootNode.Children(0).Children.Add("Dashboards", New NavigationViewCollection(), Nothing)
        Dim traitementnode = _RootNode.Children(0).Children.Add("Traitements", New NavigationViewCollection(), Nothing)
        traitementnode.ContextualMenu.Add(New MenuItem("Nouveau Traitement", Sub()
                                                                                 Dim temp = InputBox("Nom du Traitement")
                                                                                 traitementnode.Children.Add(temp, traitementnode.Views, New QueryWorkflowDesigner() With {.Caption = temp})
                                                                             End Sub))

        'traitementnode.ContextualMenu.Add(New MenuItem("Nouveau Traitement", Sub()
        '                                                                         Dim temp = InputBox("Nom du Traitement")
        '                                                                         traitementnode.Children.Add(temp, traitementnode.Views, New ViewTraitementCode() With {.Caption = temp})
        '                                                                     End Sub))

        CreateDefaultExtternalDataDashboard()
        _RootNode.Children.Add("Triangles Standards")
        Dim analysisnode = _RootNode.Children.Add("Analyses", New NavigationViewCollection(), Nothing)
        analysisnode.ContextualMenu.Add(New MenuItem("Nouvelle Analyse", Sub()
                                                                             Dim temp = InputBox("Nom de l'analyse")
                                                                             analysisnode.Children.Add(temp, analysisnode.Views, New ViewAnalysis() With {.Caption = temp})
                                                                         End Sub))


        _RootTriangleStandard = _RootNode.Children(1).Children.Add("Triangle Standard", views, New ViewTriangleStandard())
        _RootNode.Children(1).Children.Add("Dashboards")
        With _UIService.NavigationGroup(1).Nodes.Add("Module Provisionnement")
            .Children.Add(_RootNode)
            .Children.Add(_BatchNode)
        End With


        Dim newdashbordmenu = New MenuItem("Nouveau Dashboard Donnees", _
                         Sub()
                             Dim dow = New SimpleDashboardLayout()
                             dow.ShowDialog()
                             Dim dbv = SimpleDashboard.Create(dow.RowCountSlider.Value, dow.ColumnCountSlider.Value)
                             Dim dbn = _RootNode.Children(0).Children(3).Children.Add( _
                                         dow.DashboardNameTextBlock.Text.Trim(), _
                                         _RootNode.Children(0).Children(3).Views, _
                                         dbv)
                             Windows.Data.BindingOperations.SetBinding(dbv, SimpleDashboard.DataContextProperty, New Windows.Data.Binding("ExternalData") With {.Source = Project.Active})
                             dbn.View = dbv
                             dbn.View.Caption = dbn.Caption
                             Dim mi As MenuItem
                             mi = New MenuItem("Ajouter du Text", _
                                                   Sub()
                                                       Dim dzow = New SimpleDashboardZoneLayout()
                                                       dzow.RowSlider.Maximum = dow.RowCountSlider.Value - 1
                                                       dzow.ColumnSlider.Maximum = dow.ColumnCountSlider.Value - 1
                                                       dzow.RowCountSlider.Maximum = dow.RowCountSlider.Value
                                                       dzow.ColumnCountSlider.Maximum = dow.ColumnCountSlider.Value
                                                       dzow.ShowDialog()
                                                       dbv.AddTextZone("(Clic droit pour modifier le texte)", dzow.RowSlider.Value, _
                                                                       dzow.RowCountSlider.Value, _
                                                                       dzow.ColumnSlider.Value, _
                                                                       dzow.ColumnCountSlider.Value)
                                                   End Sub)
                             dbn.ContextualMenu.Add(mi)


                             mi = New MenuItem("Ajouter un Graphique", _
                                                   Sub()
                                                       Dim dzow = New SimpleDashboardZoneLayout()
                                                       dzow.RowSlider.Maximum = dow.RowCountSlider.Value - 1
                                                       dzow.ColumnSlider.Maximum = dow.ColumnCountSlider.Value - 1
                                                       dzow.RowCountSlider.Maximum = dow.RowCountSlider.Value
                                                       dzow.ColumnCountSlider.Maximum = dow.ColumnCountSlider.Value
                                                       dzow.ShowDialog()
                                                       dbv.AddChartZone(dzow.RowSlider.Value, _
                                                                       dzow.RowCountSlider.Value, _
                                                                       dzow.ColumnSlider.Value, _
                                                                       dzow.ColumnCountSlider.Value)
                                                   End Sub)
                             dbn.ContextualMenu.Add(mi)

                             mi = New MenuItem("Ajouter une Jauge", _
                                                  Sub()
                                                      Dim dzow = New SimpleDashboardZoneLayout()
                                                      dzow.RowSlider.Maximum = dow.RowCountSlider.Value - 1
                                                      dzow.ColumnSlider.Maximum = dow.ColumnCountSlider.Value - 1
                                                      dzow.RowCountSlider.Maximum = dow.RowCountSlider.Value
                                                      dzow.ColumnCountSlider.Maximum = dow.ColumnCountSlider.Value
                                                      dzow.ShowDialog()
                                                      dbv.AddGaugeZone(dzow.RowSlider.Value, _
                                                                      dzow.RowCountSlider.Value, _
                                                                      dzow.ColumnSlider.Value, _
                                                                      dzow.ColumnCountSlider.Value)
                                                  End Sub)
                             dbn.ContextualMenu.Add(mi)

                             Project._workflow = New SqlQueryWorkflow()
                             mi = New MenuItem("Connecter aux donnees", _
                                                Sub()
                                                    'Dim ddsg = New SimpleDashboardDataStructureGrid()
                                                    'ddsg.SetDashboard(dbv)
                                                    'ddsg.ShowDialog()
                                                    Dim qw = New QueryWindow()
                                                    qw.SetDataTable(_ActiveProject.ExternalData, Project._workflow)
                                                    qw.ShowDialog()

                                                    Project._workflow.Queries.Add(qw.GetQueryDataWithResults())
                                                End Sub)
                             dbn.ContextualMenu.Add(mi)

                             mi = New MenuItem("Serialiser", _
                                             Sub()
                                                 Try
                                                     'Dim str = New IO.StringWriter()
                                                     'Dim context = New Xaml.XamlSchemaContext(AppDomain.CurrentDomain.GetAssemblies())

                                                     'Dim ser = New Xaml.XamlObjectReader(dbv)
                                                     '            ser.
                                                     'For Each elt In dbv.ContainerGrid.Children
                                                     '    Dim z = TryCast(elt, SimpleDashboardZone)
                                                     '    If z IsNot Nothing Then
                                                     '        MsgBox(Xaml.XamlServices.Save(z))
                                                     '    End If
                                                     'Next
                                                     MsgBox(dbv.ToXElement().ToString())

                                                 Catch ex As Exception
                                                     MsgBox(ex.ToString())
                                                 End Try
                                             End Sub)

                             dbn.ContextualMenu.Add(mi)

                             mi = New MenuItem("Copier code", _
                                               Sub()
                                                   Dim codeprovider = New VBCodeProvider()
                                                   Dim str = New IO.StringWriter()
                                                   codeprovider.GenerateCodeFromType(dbv.GetCode(), str, New CodeDom.Compiler.CodeGeneratorOptions())

                                                   Windows.Clipboard.SetText(str.ToString())
                                               End Sub)
                             dbn.ContextualMenu.Add(mi)
                         End Sub)

        _RootNode.Children(0).Children(3).ContextualMenu.Add(newdashbordmenu)
    End Sub

    Public ReadOnly Property RootNode As NavigationNode
        Get
            Return _RootNode
        End Get
    End Property

    Public ReadOnly Property UIService As IUIService
        Get
            Return _UIService
        End Get
    End Property


End Class
