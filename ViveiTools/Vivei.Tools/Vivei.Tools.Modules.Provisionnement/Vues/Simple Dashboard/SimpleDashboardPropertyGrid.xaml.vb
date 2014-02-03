Imports Vivei.Tools.Core.UI

Partial Public Class SimpleDashboardPropertyGrid
    Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().

    End Sub

    Friend Shared _typeproperties As Dictionary(Of String, Dictionary(Of String, Boolean)) = LoadTypeProperties()
    Private _object As Object
    Public Sub SetObject(ByVal obj As Object)
        _object = obj

        'Dim zone = TryCast(obj, SimpleDashboardZone)

        'If zone IsNot Nothing Then

        '    Dim hierarchy = TryCast(zone.Tag, Hierarchy)

        '    If hierarchy IsNot Nothing Then

        '        PropertiesTreeView.ItemsSource = New Hierarchy() {hierarchy}
        '        Return
        '    Else

        '        hierarchy = New Hierarchy(zone)

        '        zone.Tag = hierarchy

        '        PropertiesTreeView.ItemsSource = New Hierarchy() {hierarchy}
        '        Return
        '    End If
        'End If

        PropertiesTreeView.ItemsSource = New Hierarchy() {New Hierarchy(obj)}

    End Sub

    Private Sub PropsCollectionChanged(ByVal sender As Object, ByVal e As Specialized.NotifyCollectionChangedEventArgs)
        If e.Action = Specialized.NotifyCollectionChangedAction.Remove Then
            Dim coll As ObjectModel.ObservableCollection(Of ComponentModel.PropertyDescriptor) = sender
            For Each prop As ComponentModel.PropertyDescriptor In e.OldItems
                _typeproperties(_object.GetType().FullName)(prop.Name) = False
            Next
        End If

        SaveTypeProperties()
    End Sub

    Private Shared Sub SaveTypeProperties()

        Dim q = From kvp In _typeproperties Select _
                 <Type Name=<%= kvp.Key %>>
                     <%= From pkvp In kvp.Value Select <Property Name=<%= pkvp.Key %> Visible=<%= pkvp.Value %>/> %>
                 </Type>

        Dim x = <TypeProperties><%= q %></TypeProperties>

        x.Save("TypeProperties.xml")
    End Sub
    Private Shared Function LoadTypeProperties() As Dictionary(Of String, Dictionary(Of String, Boolean))
        Dim x = XElement.Load("TypeProperties.xml")
        Dim result = New Dictionary(Of String, Dictionary(Of String, Boolean))()
        For Each xtype In x.<Type>
            Dim data = New Dictionary(Of String, Boolean)()

            For Each xprop In xtype.<Property>
                data.Add(xprop.@Name, CBool(xprop.@Visible))
            Next

            result.Add(xtype.@Name, data)
        Next

        Return result
    End Function


    Public Class Hierarchy
        Inherits UIObject

        Private _object As Object
        Private _data As New List(Of Hierarchy)
        Private _Children As ObjectModel.ObservableCollection(Of Hierarchy)
        Private _PropertyDescriptor As ComponentModel.PropertyDescriptor
        Private _Parent As Hierarchy
        Dim _Properties As ObjectModel.ObservableCollection(Of HierarchyProperty)
        Dim _AddItemCommand As HierarchyCommand
        Dim _RemoveItemCommand As HierarchyCommand
        Private _elt As Object

        Sub New(ByVal obj As Object)
            ' TODO: Complete member initialization 
            _object = obj
            _Caption = obj.GetType().Name
        End Sub

        Sub New(ByVal Parent As Hierarchy, ByVal propertyDescriptor As ComponentModel.PropertyDescriptor)
            ' TODO: Complete member initialization 
            _Parent = Parent
            _PropertyDescriptor = propertyDescriptor
            _Caption = propertyDescriptor.Name
            _object = propertyDescriptor.GetValue(Parent._object)
            If _object IsNot Nothing Then
            End If
        End Sub

        Private _Caption As String

        Sub New(ByVal Parent As Hierarchy, ByVal elt As Object, ByVal p3 As Boolean)
            ' TODO: Complete member initialization 
            _Parent = Parent
            _object = elt
            _Caption = elt.GetType().Name
        End Sub

        Public Property Caption As String
            Get
                Return _Caption
            End Get
            Set(ByVal value As String)
                _Caption = value
                OnPropertyChanged("Caption")
            End Set
        End Property

        Public ReadOnly Property Children As ObjectModel.ObservableCollection(Of Hierarchy)
            Get
                If _Children Is Nothing Then
                    If _Parent Is Nothing Then
                        _Children = New ObjectModel.ObservableCollection(Of Hierarchy)()
                        _Children.Add(New Hierarchy(Me, ComponentModel.TypeDescriptor.GetProperties(_object)("Content")))
                    Else
                        If _object Is Nothing Then Return Nothing
                        If _object.GetType() Is GetType(Windows.Controls.TextBlock) Then Return Nothing

                        If _object.GetType().GetInterfaces().Contains(GetType(Specialized.INotifyCollectionChanged)) Then
                            _Children = New ObjectModel.ObservableCollection(Of Hierarchy)()
                            For Each elt In _object
                                _Children.Add(New Hierarchy(Me, elt, True))
                            Next
                        Else
                            Dim q = From p As ComponentModel.PropertyDescriptor In ComponentModel.TypeDescriptor.GetProperties(_object)
                                    Where p.PropertyType.GetInterfaces().Contains(GetType(Specialized.INotifyCollectionChanged))
                                    Select New Hierarchy(Me, p)

                            _Children = New ObjectModel.ObservableCollection(Of Hierarchy)(q.ToList())
                        End If
                    End If
                End If
                Return _Children
            End Get
        End Property

        Public ReadOnly Property Properties As ObjectModel.ObservableCollection(Of HierarchyProperty)
            Get
                If _Properties Is Nothing Then
                    If _object IsNot Nothing Then

                        Dim q = (From prop As ComponentModel.PropertyDescriptor _
                                 In ComponentModel.TypeDescriptor.GetProperties(_object) _
                                 Where Not prop.IsReadOnly Order By prop.Name Select New HierarchyProperty(Me, prop)).ToList()

                        If Not _typeproperties.ContainsKey(_object.GetType().FullName) Then
                            Dim data = New Dictionary(Of String, Boolean)
                            For Each prop In q
                                data.Add(prop.Name, True)
                            Next
                            _typeproperties.Add(_object.GetType().FullName, data)

                            _Properties = New ObjectModel.ObservableCollection(Of HierarchyProperty)(q)

                            AddHandler _Properties.CollectionChanged, AddressOf PropsCollectionChanged
                            
                        Else
                            Dim p = (From kvp In _typeproperties(_object.GetType().FullName)
                                     Where kvp.Value
                                     Select New HierarchyProperty(Me, ComponentModel.TypeDescriptor.GetProperties(_object)(kvp.Key))).ToList()

                            _Properties = New ObjectModel.ObservableCollection(Of HierarchyProperty)(p)

                            AddHandler _Properties.CollectionChanged, AddressOf PropsCollectionChanged
                        End If
                    End If
                End If
                Return _Properties
            End Get
        End Property

        Public ReadOnly Property AddItemCommand As HierarchyCommand
            Get
                Return _AddItemCommand
            End Get
        End Property
        Public ReadOnly Property RemoveItemCommand As HierarchyCommand
            Get
                Return _RemoveItemCommand
            End Get
        End Property

        Public Class HierarchyProperty
            Inherits UIObject

            Private _hierarchy As Hierarchy
            Private _prop As ComponentModel.PropertyDescriptor
            Private _PossibleValues As Collections.IEnumerable
            Dim _EditCommand As HierarchyCommand

            Sub New(ByVal hierarchy As Hierarchy, ByVal prop As ComponentModel.PropertyDescriptor)
                ' TODO: Complete member initialization 
                _hierarchy = hierarchy
                _prop = prop
            End Sub

            Public ReadOnly Property Name As String
                Get
                    Return _prop.Name
                End Get
            End Property
            Public Property Value As Object
                Get
                    Return Me._prop.GetValue(Me._hierarchy._object)
                End Get
                Set(ByVal value As Object)
                    If value IsNot Nothing Then
                        If value.GetType() Is GetType(String) _
                            And Me._prop.PropertyType IsNot GetType(String) Then

                            Me._prop.SetValue(Me._hierarchy._object, Me._prop.Converter.ConvertFromString(value))
                            OnPropertyChanged("Value")
                        Else
                            Me._prop.SetValue(Me._hierarchy._object, value)
                            OnPropertyChanged("Value")
                        End If
                    Else
                        Me._prop.SetValue(Me._hierarchy._object, value)
                        OnPropertyChanged("Value")
                    End If
                End Set
            End Property

            Public ReadOnly Property PropertyDescriptor As ComponentModel.PropertyDescriptor
                Get
                    Return Me._prop
                End Get
            End Property

            Public ReadOnly Property PossibleValues As IEnumerable
                Get
                    If Me._prop.PropertyType Is GetType(Boolean) Then
                        Return New Boolean() {True, False}
                    End If
                    If Me._prop.PropertyType.IsEnum Then
                        Return [Enum].GetValues(Me._prop.PropertyType)
                    End If
                    If Me._prop.PropertyType Is GetType(Windows.Media.Brush) Then
                        Return StandardBrushes.Items
                    End If
                    If Me._prop.PropertyType Is GetType(Windows.Media.FontFamily) Then
                        Return StantardFonts.Families
                    End If
                    If Me._prop.PropertyType Is GetType(Windows.FontWeight) Then
                        Return StantardFonts.Weights
                    End If
                    If Me._prop.PropertyType Is GetType(Windows.FontStyle) Then
                        Return StantardFonts.Styles
                    End If
                    'End If
                    'Return _PossibleValues
                    Return Nothing
                End Get
            End Property

            Public ReadOnly Property EditCommand As HierarchyCommand
                Get
                    If _EditCommand Is Nothing Then
                        _EditCommand = New HierarchyCommand()
                        _EditCommand.Action = Sub(param As Object)
                                                  If Me.Value Is Nothing Then
                                                      Me.Value = Activator.CreateInstance(Me.PropertyDescriptor.PropertyType)
                                                  End If
                                                  'Dim propgrid = New SimpleDashboardPropertyGrid()
                                                  'propgrid.SetObject(Me.Value)
                                                  'propgrid.ShowInTaskbar = True
                                                  'propgrid.ShowDialog()
                                                  Me._hierarchy._Children.Add(New Hierarchy(Me._hierarchy, Me._prop))
                                              End Sub
                    End If
                    Return _EditCommand
                End Get
            End Property

        End Class

        Public Class HierarchyCommand
            Inherits UIObject
            Implements Windows.Input.ICommand


            Private _Action As Action(Of Object)
            Public Property Action As Action(Of Object)
                Get
                    Return _Action
                End Get
                Set(ByVal value As Action(Of Object))
                    _Action = value
                    OnPropertyChanged("Action")
                End Set
            End Property



            Public Function CanExecute(ByVal parameter As Object) As Boolean Implements System.Windows.Input.ICommand.CanExecute
                Return True
            End Function

            Public Event CanExecuteChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements System.Windows.Input.ICommand.CanExecuteChanged

            Public Sub Execute(ByVal parameter As Object) Implements System.Windows.Input.ICommand.Execute
                _Action(parameter)
            End Sub
        End Class

        Private Sub PropsCollectionChanged(ByVal sender As Object, ByVal e As Specialized.NotifyCollectionChangedEventArgs)
            If e.Action = Specialized.NotifyCollectionChangedAction.Remove Then
                Dim coll As ObjectModel.ObservableCollection(Of HierarchyProperty) = sender
                For Each prop As HierarchyProperty In e.OldItems
                    _typeproperties(_object.GetType().FullName)(prop.Name) = False
                Next
            End If

            SaveTypeProperties()
        End Sub


    End Class
End Class

Public Class PropertyGridCellTemplateSelector
    Inherits Windows.Controls.DataTemplateSelector

    Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As System.Windows.DependencyObject) As System.Windows.DataTemplate
        Dim window = Windows.Window.GetWindow(container)
        Dim prop = TryCast(item, SimpleDashboardPropertyGrid.Hierarchy.HierarchyProperty)
        If prop Is Nothing Then Return window.Resources("DefaultCellTemplate")
        Dim result = window.Resources(prop.PropertyDescriptor.PropertyType.Name & "CellTemplate")
        If result Is Nothing Then Return window.Resources("DefaultCellTemplate")
        Return result
        Return MyBase.SelectTemplate(item, container)
    End Function
End Class

Public Class PropertyGridCellEditingTemplateSelector
    Inherits Windows.Controls.DataTemplateSelector

    Public Overrides Function SelectTemplate(ByVal item As Object, ByVal container As System.Windows.DependencyObject) As System.Windows.DataTemplate
        Dim window = Windows.Window.GetWindow(container)
        Dim prop = TryCast(item, SimpleDashboardPropertyGrid.Hierarchy.HierarchyProperty)

        If prop IsNot Nothing Then
            If prop.PropertyDescriptor.PropertyType.IsEnum Then
                Return window.Resources("EnumCellEditingTemplate")
            End If
            If prop.PropertyDescriptor.PropertyType Is GetType(String) Then
                Return window.Resources("StringCellEditingTemplate")
            End If
            If prop.PropertyDescriptor.PropertyType Is GetType(Boolean) Then
                Return window.Resources("BooleanCellEditingTemplate")
            End If
            If prop.PropertyDescriptor.PropertyType Is GetType(Date) Then
                Return window.Resources("DateCellEditingTemplate")
            End If
            If prop.PropertyDescriptor.PropertyType.IsPrimitive Then
                Return window.Resources("NumericCellEditingTemplate")
            End If

            Dim template = window.Resources(prop.PropertyDescriptor.PropertyType.Name & "CellEditingTemplate")

            If template IsNot Nothing Then Return template

            If prop.PropertyDescriptor.PropertyType.IsClass And Not prop.PropertyDescriptor.PropertyType.IsAbstract Then
                Return window.Resources("ObjectCellEditingTemplate")
            End If
        End If
        '  Return window.Resources("DefaultCellTemplate")
        Return MyBase.SelectTemplate(item, container)
    End Function
End Class