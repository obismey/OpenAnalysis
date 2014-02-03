Imports System.Windows.Input

Public Class ViewAnalysis
    Implements Core.UI.INavigationView

    Private _Caption As String
    Private _Stages As New ObjectModel.ObservableCollection(Of AnalysisStageAdapter)

    Public Shared ProviderProcessors As New ObjectModel.ObservableCollection(Of Type)

    Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        If ProviderProcessors.Count = 0 Then
            ProviderProcessors.Add(GetType(DefaultTriangleDataProvider))
            ProviderProcessors.Add(GetType(DefaultCoefficientProcessor))
        End If

        StagesCombobox.ItemsSource = ProviderProcessors
        ProcessListbox.ItemsSource = _Stages
    End Sub

    Public Property Caption As String Implements Core.UI.INavigationView.Caption
        Get
            Return Me._Caption
        End Get
        Set(ByVal value As String)
            _Caption = value
        End Set
    End Property

    Public Property Icone As String Implements Core.UI.INavigationView.Icone
        Get

        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click

        If StagesCombobox.SelectedItem IsNot Nothing Then
            Try
                Dim obj = Activator.CreateInstance(CType(StagesCombobox.SelectedItem, Type))
                Dim processor = TryCast(obj, Model.Interfaces.ITriangleDataProcessor)
                If processor IsNot Nothing Then
                    Dim s = New AnalysisStageAdapter(processor) With {.Container = Me._Stages}
                    Me._Stages.Add(s)
                    processor.Initialize(False, s.ModelAnalysisStage)
                    Return
                End If
                Dim provider = TryCast(obj, Model.Interfaces.ITriangleDataProvider)
                If provider IsNot Nothing Then
                    Dim s = New AnalysisStageAdapter(provider) With {.Container = Me._Stages}
                    Me._Stages.Add(s)
                    provider.Initialize(False, s.ModelAnalysisStage)
                    Return
                End If
            Catch ex As Exception
                Dim o = 1
            End Try
        End If

    End Sub

    Public Class AnalysisStageAdapter
        Inherits Core.UI.UIObject
        Implements ICommand

        Dim _provider As Model.Interfaces.ITriangleDataProvider
        Dim _processor As Model.Interfaces.ITriangleDataProcessor
        Friend Container As ObjectModel.ObservableCollection(Of AnalysisStageAdapter)
        Dim _StageName As String

        Public ReadOnly Property ModelAnalysisStage As Model.AnalysisStage
            Get
                Return Nothing
            End Get
        End Property

        Sub New(ByVal provider As Model.Interfaces.ITriangleDataProvider)
            Me._provider = provider
        End Sub
        Sub New(ByVal processor As Model.Interfaces.ITriangleDataProcessor)
            Me._processor = processor
        End Sub
        Public ReadOnly Property Type As String
            Get
                If _processor IsNot Nothing Then
                    Return _processor.GetType().FullName
                End If
                Return _provider.GetType().FullName
            End Get
        End Property
        Public Property StageName As String
            Get
                If String.IsNullOrEmpty(_StageName) Then Return Type
                Return _StageName
            End Get
            Set(ByVal value As String)
                _StageName = value
            End Set
        End Property
        Public ReadOnly Property View As Object
            Get
                If _processor IsNot Nothing Then
                    Return _processor.View
                End If
                Return _provider.View
            End Get
        End Property
        Public Function CanExecute(ByVal parameter As Object) As Boolean Implements System.Windows.Input.ICommand.CanExecute
            Return True
        End Function

        Public Event CanExecuteChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements System.Windows.Input.ICommand.CanExecuteChanged

        Public Sub Execute(ByVal parameter As Object) Implements System.Windows.Input.ICommand.Execute
            If parameter = "Compute" Then
                ExecuteCompute()
            End If
            If parameter = "Edit" Then
                ExecuteEdit()
            End If
        End Sub

        Private Sub ExecuteCompute()
            If _provider IsNot Nothing Then
                _provider.Compute(Project.Active.InternalDataModel)
            End If
            If _processor IsNot Nothing Then
                Dim previous = Container(Container.IndexOf(Me) - 1)

                If previous._provider IsNot Nothing Then
                    _processor.Transform(previous._provider.Compute(Project.Active.InternalDataModel))
                End If
            End If
        End Sub


        Private Sub ExecuteEdit()
            Dim aw = New AnalysisWindow()
            Dim props As ComponentModel.PropertyDescriptorCollection = Nothing

            If _provider IsNot Nothing Then
                props = ComponentModel.TypeDescriptor.GetProperties(_provider)
            End If

            If _processor IsNot Nothing Then
                props = ComponentModel.TypeDescriptor.GetProperties(_processor)
            End If


            Dim IsProcessorArgument = _
                Function(p As System.ComponentModel.PropertyDescriptor) As Boolean
                    Return (Aggregate attr In p.Attributes
                            Where attr.GetType() Is GetType(Model.Interfaces.ProcessorArgumentAttribute)
                            Into Count()) > 0
                End Function


            Dim arguments = From p As ComponentModel.PropertyDescriptor In props
                            Where IsProcessorArgument(p)
                            Order By p.Name
                            Select New ArgumentBinding() With {.PropertyDescriptor = p}

            aw.ArgumentsGrid.ItemsSource = arguments.ToList()

            Dim cbcol = CType(aw.ArgumentsGrid.Columns(1), Windows.Controls.DataGridComboBoxColumn)


            cbcol.ItemsSource = Container


            aw.ShowDialog()
        End Sub
    End Class
    Public Class ArgumentBinding
        Public Property PropertyDescriptor As System.ComponentModel.PropertyDescriptor
        Public ReadOnly Property Name As String
            Get
                Return PropertyDescriptor.Name
            End Get
        End Property
        Public Property Stage As AnalysisStageAdapter
    End Class
End Class
