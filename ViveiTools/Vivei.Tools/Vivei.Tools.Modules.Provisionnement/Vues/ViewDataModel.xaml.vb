Imports Vivei.Tools.Core.UI

Public Class ViewDataModel
    Implements Vivei.Tools.Core.UI.INavigationView

    Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        DataModelEditingDataGrid.DataContext = ProvisionnementModule._ActiveProject.DataModel
    End Sub

    Public Property Caption As String Implements Core.UI.INavigationView.Caption
        Get
            Return "Modele Interne"
        End Get
        Set(ByVal value As String)
            'Throw New NotImplementedException()
        End Set
    End Property

    Public Property Icone As String Implements Core.UI.INavigationView.Icone
        Get
            Return ""
        End Get
        Set(ByVal value As String)
            ' Throw New NotImplementedException()
        End Set
    End Property

    Private Sub SaveButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim spd = New Microsoft.Win32.SaveFileDialog()
        spd.Filter = "Fichiers de modele de donnes|*.mdl"
        spd.AddExtension = True
        spd.DefaultExt = "mdl"
        spd.InitialDirectory = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules\ProvisionnementModule\Data")

        If spd.ShowDialog() Then

            Dim fun = Function(p As DataModelProperty)
                          Dim x = <Property
                                      SourceColumn=<%= If(p.SourceColumn Is Nothing, Nothing, p.SourceColumn.ColumnName) %>
                                      Name=<%= p.Name %>
                                      Type=<%= p.Type %>
                                      Formula=<%= p.Formula %>
                                      ConverterType=<%= p.ConverterType %>
                                      Usage=<%= p.Usage %>
                                      Priority=<%= p.Priority %>/>
                          Return x
                      End Function


            Dim xmodel = <Model>
                             <%= From p In Project.Active.DataModel Select fun(p) %>
                         </Model>

            xmodel.Save(spd.FileName)
        End If
    End Sub

    Private Sub LoadButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        Dim opd = New Microsoft.Win32.OpenFileDialog()
        opd.Filter = "Fichiers de modele de donnes|*.mdl"
        opd.InitialDirectory = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules\ProvisionnementModule\Data")

        If opd.ShowDialog() Then
            Dim xmodel = XElement.Load(opd.FileName)

            Dim fun = Function(x As XElement)
                          Dim p = New DataModelProperty()
                          p.Name = x.@Name
                          p.Type = x.@Type
                          p.Formula = x.@Formula
                          p.ConverterType = x.@ConverterType
                          p.Usage = x.@Usage
                          p.Priority = x.@Priority
                          If Project.Active.ExternalData IsNot Nothing Then
                              If Project.Active.ExternalData.Columns.Contains(x.@SourceColumn) Then
                                  p.SourceColumn = Project.Active.ExternalData.Columns(x.@SourceColumn)
                              End If
                          End If
                          Return p
                      End Function


            For Each x In xmodel.<Property>
                Project.Active.DataModel.Add(fun(x))
            Next
        End If
    End Sub
End Class

Public Class DataModel
    Inherits ObjectModel.ObservableCollection(Of DataModelProperty)


    Public ReadOnly Property SourceColumns As IEnumerable(Of DataColumn)
        Get
            If ProvisionnementModule._ActiveProject.ExternalData Is Nothing Then Return Nothing
            Dim data = ProvisionnementModule._ActiveProject.ExternalData.Columns.Cast(Of DataColumn).ToList()
            data.Insert(0, New DataColumn("(Aucun)", GetType(String)))
            Return data
        End Get
    End Property

    Public ReadOnly Property KnownTypes As String()
        Get
            Return New String() {"Nombre", "Texte", "Date"}
        End Get
    End Property

    Public ReadOnly Property KnownConverters As String()
        Get
            Return New String() {"(Aucun)", "TextToNumber", "TexteToDate"}
        End Get
    End Property

    Public ReadOnly Property KnownUsages As String()
        Get
            Return New String() {"(Aucun)", "Segmentation", "Line Of Business", "Categorie Ministerielle", "Garantie", "Survenance", "Declaration", "Deroulement", "Reglement", "Prime", "Sinistre", "Provision"}
        End Get
    End Property

    Friend Sub NotifySourceColumnsChange()
        Me.OnPropertyChanged(New ComponentModel.PropertyChangedEventArgs("SourceColumns"))
    End Sub
End Class

Public Class DataModelProperty
    Inherits UIObject

    Private Shared inc = 0
    Sub New()
        inc += 1
        _Name = "Colonne " & inc
        _Type = "Texte"
        _ConverterType = "(Aucun)"
        _Usage = "(Aucun)"
    End Sub

    Private _SourceColumn As DataColumn

    Public Property SourceColumn As DataColumn
        Get
            Return _SourceColumn
        End Get
        Set(ByVal value As DataColumn)
            _SourceColumn = value
            OnPropertyChanged("SourceColumn")
        End Set
    End Property

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

    Private _Formula As String
    Public Property Formula As String
        Get
            Return _Formula
        End Get
        Set(ByVal value As String)
            _Formula = value
            OnPropertyChanged("Formula")
        End Set
    End Property

    Private _ConverterType As String
    Public Property ConverterType As String
        Get
            Return _ConverterType
        End Get
        Set(ByVal value As String)
            _ConverterType = value
            OnPropertyChanged("Converter")
        End Set
    End Property

    Private _Usage As String
    Public Property Usage As String
        Get
            Return _Usage
        End Get
        Set(ByVal value As String)
            _Usage = value
            OnPropertyChanged("Usage")
        End Set
    End Property

    Private _Priority As Integer
    Public Property Priority As Integer
        Get
            Return _Priority
        End Get
        Set(ByVal value As Integer)
            _Priority = value
            OnPropertyChanged("Priority")
        End Set
    End Property


End Class

