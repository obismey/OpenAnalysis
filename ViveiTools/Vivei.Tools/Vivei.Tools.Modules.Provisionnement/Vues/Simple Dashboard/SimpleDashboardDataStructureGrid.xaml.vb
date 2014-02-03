Imports Vivei.Tools.Core.UI

Partial Public Class SimpleDashboardDataStructureGrid

    Private Container As NodeCollection

    Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().

        Container = New NodeCollection()


        'With Container.AddNode("Root")
        '    .Children.AddNode("Child1")
        '    .Children.AddNode("Child2")
        'End With

        DataStructureDataGrid.ItemsSource = Container
    End Sub

    Friend Sub SetDashboard(ByVal Dashboard As SimpleDashboard)
        Dim n = Container.AddNode("Dashboard")
        For Each ctrl In Dashboard.ContainerGrid.Children
            Dim z = TryCast(ctrl, SimpleDashboardZone)

            If z IsNot Nothing Then
                Dim nz = Container.AddNode(n, z.Content.GetType().Name)

                If z.Content.GetType() Is GetType(Windows.Controls.TextBlock) Then
                    Container.AddNode(nz, "Text")
                    Container.AddNode(nz, "Text Color")
                    Container.AddNode(nz, "Back Color")
                    Container.AddNode(nz, "Text Style")
                End If

                If z.Content.GetType() Is GetType(Infragistics.Windows.Chart.XamChart) Then
                    Dim chart As Infragistics.Windows.Chart.XamChart = z.Content

                    For Each s In chart.Series
                        Container.AddNode(nz, "Series")
                    Next
                End If


                If z.Content.GetType() Is GetType(Infragistics.Controls.Charts.XamRadialGauge) Then
                    Dim gauge As Infragistics.Controls.Charts.XamRadialGauge = z.Content

                    For Each sc In gauge.Scales

                        Dim nsc = Container.AddNode(nz, "Scale")
                        Container.AddNode(nsc, "Min Value")
                        Container.AddNode(nsc, "Max Value")

                        For Each rng In sc.Ranges
                            Dim nrng = Container.AddNode(nsc, "Range")
                            Container.AddNode(nrng, "Min Value")
                            Container.AddNode(nrng, "Max Value")
                        Next

                        For Each nee In sc.Needles
                            Dim nnee = Container.AddNode(nsc, "Needle")
                            Container.AddNode(nnee, "Value")
                        Next
                    Next
                End If
            End If
        Next
    End Sub



    Public Class Node
        Inherits UIObject

        Private _Parent As Node
        Private _Container As NodeCollection


        Sub New(ByVal Container As NodeCollection, ByVal Name As String)
            Me._Name = Name
            Me._Container = Container
            Me._Indentation = New Windows.Thickness(5, 0, 0, 0)
            Me._Expanded = True
            Me._Visibility = Windows.Visibility.Visible
        End Sub


        Sub New(ByVal Parent As Node, ByVal Name As String)
            ' TODO: Complete member initialization 
            Me._Parent = Parent
            Me._Name = Name
            Me._Container = Parent._Container
            Me._Indentation = New Windows.Thickness(Parent._Indentation.Left + 10, 0, 0, 0)
            Me._Expanded = False
            Me._Visibility = Windows.Visibility.Collapsed
        End Sub

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

        Private _Indentation As Windows.Thickness
        Public ReadOnly Property Indentation As Windows.Thickness
            Get
                Return _Indentation
            End Get
        End Property

        Private _Visibility As Windows.Visibility
        Public Property Visibility As Windows.Visibility
            Get
                Return _Visibility
            End Get
            Set(ByVal value As Windows.Visibility)
                For Each c In Children
                    c.Visibility = If(value, Windows.Visibility.Visible, Windows.Visibility.Collapsed)
                Next
                If _Visibility = value Then Return
                _Visibility = value
                OnPropertyChanged("Visibility")
            End Set
        End Property

        Private _Expanded As Boolean
        Public Property Expanded As Boolean
            Get
                Return _Expanded
            End Get
            Set(ByVal value As Boolean)
                If _Expanded = value Then Return
                For Each c In Children
                    c.Visibility = If(value, Windows.Visibility.Visible, Windows.Visibility.Collapsed)
                Next
                _Expanded = value
                OnPropertyChanged("Expanded")
            End Set
        End Property

        Public ReadOnly Property Children As IEnumerable(Of Node)
            Get
                'If _Children Is Nothing Then
                '    _Children = New NodeCollection(Me)
                'End If
                Return (From n In _Container Where n._Parent Is Me).ToArray()
            End Get
        End Property

        Public ReadOnly Property Container As NodeCollection
            Get
                Return _Container
            End Get
        End Property

        Private _Value As String
        Public Property Value As String
            Get
                Return _Value
            End Get
            Set(ByVal value As String)
                _Value = value
                OnPropertyChanged("Value")
            End Set
        End Property


    End Class

    Public Class NodeCollection
        Inherits ObjectModel.ObservableCollection(Of Node)

        'Private _IsContainer As Boolean
        Private _Owner As Node

        'Sub New()

        'End Sub

        'Sub New(ByVal Container As Boolean)
        '    ' TODO: Complete member initialization 
        '    _IsContainer = Container
        'End Sub

        'Sub New(ByVal Owner As Node)
        '    ' TODO: Complete member initialization 
        '    _Owner = Owner
        '    _IsContainer = False
        'End Sub

        Public Function AddNode(ByVal Name As String) As Node
            Dim n As Node
            'If _IsContainer Then
            n = New Node(Me, Name)
            Me.Add(n)
            'Else
            'n = New Node(_Owner, Name)

            'If Me.Count = 0 Then
            '    Me._Owner.Container.Insert(Me._Owner.Container.IndexOf(Me._Owner) + 1, n)
            'Else
            '    Me._Owner.Container.Insert(Me._Owner.Container.IndexOf(Me.Last()) + 1, n)
            'End If
            'Me.Add(n)
            'End If
            Return n
        End Function

        Public Function AddNode(ByVal Parent As Node, ByVal Name As String) As Node
            Dim n As Node
            'If _IsContainer Then
            'n = New Node(Me, Name)
            'Me.Add(n)
            'Else
            n = New Node(Parent, Name)

            'If Me.Count = 0 Then
            '    Me._Owner.Container.Insert(Me._Owner.Container.IndexOf(Me._Owner) + 1, n)
            'Else
            '    Me._Owner.Container.Insert(Me._Owner.Container.IndexOf(Me.Last()) + 1, n)
            'End If
            Me.Add(n)
            'End If
            Return n
        End Function
    End Class
End Class