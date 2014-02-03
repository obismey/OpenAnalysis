Imports System.Windows.Controls
Imports System.Windows
Imports Infragistics.Windows.Chart
Imports Infragistics.Controls.Charts
Imports System.ComponentModel

Partial Public Class SimpleDashboard
    Implements Core.UI.INavigationView

    Public Function GetCode() As CodeDom.CodeTypeDeclaration
        Dim result = New CodeDom.CodeTypeDeclaration("IDashboardData")
        result.IsInterface = True
        result.Attributes = CodeDom.MemberAttributes.Public

        Dim unnamedtextblockcount = 0

        For Each c In ContainerGrid.Children

            Dim ctrl = TryCast(c, Control)

            If ctrl IsNot Nothing Then


                unnamedtextblockcount += 1

                Dim cp = New CodeDom.CodeMemberProperty()
                cp.Name = "Text" & unnamedtextblockcount
                cp.Type = New CodeDom.CodeTypeReference(GetType(String))
                result.Members.Add(cp)
            End If

        Next
        Return result
    End Function


    Public Shared Function Create(ByVal rowcount As Integer, ByVal columncount As Integer) As SimpleDashboard
        Dim result = New SimpleDashboard()
        If rowcount > 1 Then
            For i = 0 To rowcount - 1
                result.ContainerGrid.RowDefinitions.Add(New RowDefinition())
                If i < (rowcount - 1) Then
                    result.ContainerGrid.RowDefinitions.Add(New RowDefinition() With {.Height = New Windows.GridLength(2.0)})
                End If
            Next
        End If
        If columncount > 1 Then
            For i = 0 To columncount - 1
                result.ContainerGrid.ColumnDefinitions.Add(New ColumnDefinition())
                If i < (columncount - 1) Then
                    result.ContainerGrid.ColumnDefinitions.Add(New ColumnDefinition() With {.Width = New Windows.GridLength(2.0)})
                End If
            Next
        End If

        If rowcount > 1 Then
            For i = 0 To rowcount - 1
                If i < (rowcount - 1) Then
                    result.AddRowSplitter(i)
                End If
            Next
        End If
        If columncount > 1 Then
            For i = 0 To columncount - 1
                If i < (columncount - 1) Then
                    result.AddColumnSplitter(i)
                End If
            Next
        End If
        Return result

    End Function

    Private Sub AddRowSplitter(ByVal index As Integer)
        Dim splitter = New GridSplitter()
        splitter.Background = Windows.Media.Brushes.Black
        splitter.HorizontalAlignment = Windows.HorizontalAlignment.Stretch
        splitter.VerticalAlignment = Windows.VerticalAlignment.Stretch
        splitter.ResizeBehavior = GridResizeBehavior.PreviousAndNext
        splitter.ResizeDirection = GridResizeDirection.Rows
        SetGridLayout(splitter, 2 * index + 1, 1, 0, Math.Max(Me.ContainerGrid.ColumnDefinitions.Count, 1))
        ContainerGrid.Children.Add(splitter)
    End Sub
    Private Sub AddColumnSplitter(ByVal index As Integer)
        Dim splitter = New GridSplitter()
        splitter.Background = Windows.Media.Brushes.Black
        splitter.HorizontalAlignment = Windows.HorizontalAlignment.Stretch
        splitter.VerticalAlignment = Windows.VerticalAlignment.Stretch
        splitter.ResizeBehavior = GridResizeBehavior.PreviousAndNext
        splitter.ResizeDirection = GridResizeDirection.Columns
        SetGridLayout(splitter, 0, Math.Max(Me.ContainerGrid.RowDefinitions.Count, 1), 2 * index + 1, 1)
        ContainerGrid.Children.Add(splitter)
    End Sub
    Public Sub SetGridLayout(ByVal target As Windows.UIElement, ByVal Row As Integer, ByVal RowSpan As Integer, ByVal Column As Integer, ByVal ColumnSpan As Integer)
        Grid.SetRow(target, Row)
        Grid.SetRowSpan(target, RowSpan)
        Grid.SetColumn(target, Column)
        Grid.SetColumnSpan(target, ColumnSpan)
    End Sub
    Public Sub SetGridLayoutIgnoreSplitter(ByVal target As Windows.UIElement, ByVal Row As Integer, ByVal RowSpan As Integer, ByVal Column As Integer, ByVal ColumnSpan As Integer)
        Grid.SetRow(target, 2 * Row)
        Grid.SetRowSpan(target, 2 * RowSpan - 1)
        Grid.SetColumn(target, 2 * Column)
        Grid.SetColumnSpan(target, 2 * ColumnSpan - 1)
    End Sub

    Public Property Caption As String Implements Core.UI.INavigationView.Caption
        Get
            Return GetValue(CaptionProperty)
        End Get

        Set(ByVal value As String)
            SetValue(CaptionProperty, value)
        End Set
    End Property

    Public Shared ReadOnly CaptionProperty As DependencyProperty = _
                           DependencyProperty.Register("Caption", _
                           GetType(String), GetType(SimpleDashboard), _
                           New FrameworkPropertyMetadata(Nothing))

    Public Property Icone As String Implements Core.UI.INavigationView.Icone
        Get
            Return GetValue(IconeProperty)
        End Get

        Set(ByVal value As String)
            SetValue(IconeProperty, value)
        End Set
    End Property

    Public Shared ReadOnly IconeProperty As DependencyProperty = _
                           DependencyProperty.Register("Icone", _
                           GetType(String), GetType(SimpleDashboard), _
                           New FrameworkPropertyMetadata(Nothing))

    Public Property SelectedElement As Object
        Get
            Return GetValue(SelectedElementProperty)
        End Get

        Set(ByVal value As Object)
            SetValue(SelectedElementProperty, value)
        End Set
    End Property

    Public Shared ReadOnly SelectedElementProperty As DependencyProperty = _
                           DependencyProperty.Register("SelectedElement", _
                           GetType(Object), GetType(SimpleDashboard), _
                           New FrameworkPropertyMetadata(Nothing))



    Protected Overrides Sub OnMouseDoubleClick(ByVal e As System.Windows.Input.MouseButtonEventArgs)
        MyBase.OnMouseDoubleClick(e)
    End Sub



    Public Function AddTextZone(ByVal text As String, ByVal Row As Integer, ByVal RowSpan As Integer, ByVal Column As Integer, ByVal ColumnSpan As Integer) As SimpleDashboardZone
        Dim z = New SimpleDashboardZone(Me)
        Dim txt = New TextBlock()
        txt.HorizontalAlignment = Windows.HorizontalAlignment.Center
        txt.VerticalAlignment = Windows.VerticalAlignment.Center
        txt.Text = text
        z.Content = txt
        z.Background = Media.Brushes.White
        SetGridLayoutIgnoreSplitter(z, Row, RowSpan, Column, ColumnSpan)
        'SelectedElement = z
        z.ContextMenu = New ContextMenu()
        Dim mi = New MenuItem() With {.Header = "Modifier le texte"}
        AddHandler mi.Click, Sub() txt.Text = InputBox("Entrez le texte")
        z.ContextMenu.Items.Add(mi)
        mi = New MenuItem() With {.Header = "Modifier la connexion aux données"}
        AddHandler mi.Click, Sub()
                                 Dim b = New Data.Binding(InputBox("Entrez le chemin vers les données"))
                                 Data.BindingOperations.SetBinding(txt, TextBlock.TextProperty, b)
                             End Sub
        z.ContextMenu.Items.Add(mi)

        ContainerGrid.Children.Add(z)

        Return z
    End Function

    Public Function AddChartZone(ByVal Row As Integer, ByVal RowSpan As Integer, ByVal Column As Integer, ByVal ColumnSpan As Integer) As SimpleDashboardZone
        Dim z = New SimpleDashboardZone(Me)
        Dim chart = New XamChart()
        chart.HorizontalAlignment = Windows.HorizontalAlignment.Stretch
        chart.VerticalAlignment = Windows.VerticalAlignment.Stretch
        chart.Axes.Add(New Axis() With {.AxisType = AxisType.PrimaryX})
        chart.Axes.Add(New Axis() With {.AxisType = AxisType.PrimaryY})
        z.Content = chart
        z.Background = Media.Brushes.White
        SetGridLayoutIgnoreSplitter(z, Row, RowSpan, Column, ColumnSpan)
        'SelectedElement = z
        z.ContextMenu = New ContextMenu()
        Dim mi = New MenuItem() With {.Header = "Ajouter une serie"}
        AddHandler mi.Click, Sub()
                                 Dim s = New Series()
                                 s.DataPoints.Add(1.0)
                                 s.DataPoints.Add(1.5)
                                 s.DataPoints.Add(0.75)
                                 chart.Series.Add(s)
                             End Sub

        z.ContextMenu.Items.Add(mi)
        'mi = New MenuItem() With {.Header = "Modifier la connexion aux données"}
        'AddHandler mi.Click, Sub()
        '                         Dim b = New Data.Binding(InputBox("Entrez le chemin vers les données"))
        '                         Data.BindingOperations.SetBinding(txt, TextBlock.TextProperty, b)
        '                     End Sub
        'z.ContextMenu.Items.Add(mi)
        ContainerGrid.Children.Add(z)

        Return z
    End Function

    Public Function AddGaugeZone(ByVal Row As Integer, ByVal RowSpan As Integer, ByVal Column As Integer, ByVal ColumnSpan As Integer) As SimpleDashboardZone
        Dim z = New SimpleDashboardZone(Me)
        Dim gauge = New XamRadialGauge()
        gauge.HorizontalAlignment = Windows.HorizontalAlignment.Stretch
        gauge.VerticalAlignment = Windows.VerticalAlignment.Stretch
        z.Content = gauge
        z.Background = Media.Brushes.White
        SetGridLayoutIgnoreSplitter(z, Row, RowSpan, Column, ColumnSpan)
        'SelectedElement = z
        z.ContextMenu = New ContextMenu()
        Dim mi = New MenuItem() With {.Header = "Ajouter un scale"}
        AddHandler mi.Click, Sub()
                                 gauge.Scales.Add(New RadialGaugeScale())
                             End Sub
        z.ContextMenu.Items.Add(mi)
        mi = New MenuItem() With {.Header = "Scales"}
        z.ContextMenu.Items.Add(mi)

        AddHandler gauge.Scales.CollectionChanged, Sub(sender As Object, e As System.Collections.Specialized.NotifyCollectionChangedEventArgs)
                                                       Select Case e.Action
                                                           Case Specialized.NotifyCollectionChangedAction.Add
                                                               Dim mitemp = New MenuItem()
                                                               Dim scale As RadialGaugeScale = e.NewItems(0)
                                                               mitemp.Header = "Scale"
                                                               mi.Items.Add(mitemp)
                                                               Dim miscale = mitemp

                                                               mitemp = New MenuItem() With {.Header = "Ajouter un Range"}
                                                               AddHandler mitemp.Click, Sub() scale.Ranges.Add(New RadialGaugeRange())
                                                               miscale.Items.Add(mitemp)
                                                               mitemp = New MenuItem() With {.Header = "Ajouter une Aiguille"}
                                                               AddHandler mitemp.Click, Sub() scale.Needles.Add(New RadialGaugeNeedle())
                                                               miscale.Items.Add(mitemp)
                                                               mitemp = New MenuItem() With {.Header = "Ajouter un groupe de Tick"}
                                                               AddHandler mitemp.Click, Sub() scale.TickMarkGroups.Add(New RadialGaugeTickMarkGroup())
                                                               miscale.Items.Add(mitemp)
                                                               mitemp = New MenuItem() With {.Header = "Ajouter un groupe de Label"}
                                                               AddHandler mitemp.Click, Sub() scale.LabelGroups.Add(New RadialGaugeLabelGroup())
                                                               miscale.Items.Add(mitemp)

                                                           Case Specialized.NotifyCollectionChangedAction.Remove

                                                       End Select
                                                   End Sub
        ContainerGrid.Children.Add(z)

        Return z
    End Function

    Public Sub SetColumnSize(ByVal index As Integer, ByVal size As GridLength)
        If ContainerGrid.ColumnDefinitions.Count <= 1 Then Return

        ContainerGrid.ColumnDefinitions(2 * index).Width = size
    End Sub
    Public Sub SetRowSize(ByVal index As Integer, ByVal size As GridLength)
        If ContainerGrid.RowDefinitions.Count <= 1 Then Return

        ContainerGrid.RowDefinitions(2 * index).Height = size
    End Sub


    Public Function ToXElement() As XElement

        Dim result = <SimpleDashboard/>

        If ContainerGrid.RowDefinitions.Count > 1 Then
            For i = 0 To ContainerGrid.RowDefinitions.Count - 1
                If (i Mod 2) = 0 Then
                    Dim x = <GridRow Index=<%= i %> Size=<%= ContainerGrid.RowDefinitions(i).Height.ToString() %>/>
                    result.Add(x)
                End If
            Next
        End If

        If ContainerGrid.ColumnDefinitions.Count > 1 Then
            For i = 0 To ContainerGrid.ColumnDefinitions.Count - 1
                If (i Mod 2) = 0 Then
                    Dim x = <GridColumn Index=<%= i %> Size=<%= ContainerGrid.ColumnDefinitions(i).Width.ToString() %>/>
                    result.Add(x)
                End If
            Next
        End If

        Dim props = (From kvp In SimpleDashboardPropertyGrid._typeproperties(GetType(SimpleDashboardZone).FullName) Where kvp.Value Select kvp.Key).ToList()
        Dim txtprops = (From kvp In SimpleDashboardPropertyGrid._typeproperties(GetType(TextBlock).FullName) Where kvp.Value Select kvp.Key).ToList()
        Dim chartprops = (From kvp In SimpleDashboardPropertyGrid._typeproperties(GetType(XamChart).FullName) Where kvp.Value Select kvp.Key).ToList()
        Dim gaugeprops = (From kvp In SimpleDashboardPropertyGrid._typeproperties(GetType(XamRadialGauge).FullName) Where kvp.Value Select kvp.Key).ToList()


        Dim defaultzone = New SimpleDashboardZone()
        defaultzone.Background = Media.Brushes.White
        Dim defaulttextblock = New TextBlock()
        Dim defaultchart = New XamChart()
        Dim defaultgauge = New XamRadialGauge()

        For Each elt In ContainerGrid.Children
            Dim z = TryCast(elt, SimpleDashboardZone)

            If z IsNot Nothing Then

                Dim x = <Zone
                            Type=<%= z.Content.GetType().FullName %>
                            Row=<%= Grid.GetRow(z) %>
                            Column=<%= Grid.GetColumn(z) %>
                            RowSpan=<%= Grid.GetRowSpan(z) %>
                            ColumnSpan=<%= Grid.GetColumnSpan(z) %>/>

                For Each p In props
                    Dim vz = TypeDescriptor.GetProperties(GetType(SimpleDashboardZone))(p).GetValue(z)
                    Dim vd = TypeDescriptor.GetProperties(GetType(SimpleDashboardZone))(p).GetValue(defaultzone)

                    If Not Object.Equals(vz, vd) Then
                        x.Add(<Property Name=<%= p %>><%= If(vz Is Nothing, <Null/>, vz.ToString()) %></Property>)
                    End If
                Next


                If z.Content.GetType() Is GetType(TextBlock) Then
                    x.Add(ToXElement(CType(z.Content, TextBlock), defaulttextblock, txtprops))
                End If

                If z.Content.GetType() Is GetType(XamChart) Then
                    x.Add(ToXElement(CType(z.Content, XamChart), defaultchart, chartprops))
                End If

                If z.Content.GetType() Is GetType(XamRadialGauge) Then
                    x.Add(ToXElement(CType(z.Content, XamRadialGauge), defaultgauge, gaugeprops))
                End If
                result.Add(x)
            End If
        Next

        Return result
    End Function

    Private Function ToXElement(ByVal z As TextBlock, ByVal [default] As TextBlock, ByVal props As IEnumerable(Of String)) As XElement
        Dim x = <Content/>

        For Each p In props
            Dim vz = TypeDescriptor.GetProperties(GetType(TextBlock))(p).GetValue(z)
            Dim vd = TypeDescriptor.GetProperties(GetType(TextBlock))(p).GetValue([default])

            If Not Object.Equals(vz, vd) Then
                x.Add(<Property Name=<%= p %>><%= If(vz Is Nothing, <Null/>, vz.ToString()) %></Property>)
            End If

        Next

        Return x
    End Function

    Private Function ToXElement(ByVal z As XamChart, ByVal [default] As XamChart, ByVal props As IEnumerable(Of String)) As XElement
        Dim x = <Content/>

        For Each p In props
            Dim vz = TypeDescriptor.GetProperties(GetType(XamChart))(p).GetValue(z)
            Dim vd = TypeDescriptor.GetProperties(GetType(XamChart))(p).GetValue([default])

            If Not Object.Equals(vz, vd) Then
                x.Add(<Property Name=<%= p %>><%= If(vz Is Nothing, <Null/>, vz.ToString()) %></Property>)
            End If

        Next

        Dim axisprops = (From kvp In SimpleDashboardPropertyGrid._typeproperties(GetType(Axis).FullName) Where kvp.Value Select kvp.Key).ToList()
        Dim defaultaxis = New Axis()

        For Each a In z.Axes
            x.Add(ToXElement(<Axis/>, a, defaultaxis, GetType(Axis), axisprops))
        Next

        Dim seriesprops = (From kvp In SimpleDashboardPropertyGrid._typeproperties(GetType(Series).FullName) Where kvp.Value Select kvp.Key).ToList()
        Dim defaultseries = New Series()

        For Each s In z.Series
            x.Add(ToXElement(<Series/>, s, defaultseries, GetType(Series), seriesprops))
        Next

        Return x
    End Function

    Private Function ToXElement(ByVal x As XElement, ByVal z As Object, ByVal [default] As Object, ByVal type As Type, ByVal props As IEnumerable(Of String)) As XElement

        For Each p In props
            Dim vz = TypeDescriptor.GetProperties(type)(p).GetValue(z)
            Dim vd = TypeDescriptor.GetProperties(type)(p).GetValue([default])

            If Not Object.Equals(vz, vd) Then
                x.Add(<Property Name=<%= p %>><%= If(vz Is Nothing, <Null/>, vz.ToString()) %></Property>)
            End If

        Next
        Return x
    End Function
    Private Function ToXElement(ByVal x As XElement, ByVal z As Object, ByVal props As IEnumerable(Of String)) As XElement
        Return ToXElement(x, z, Activator.CreateInstance(z.GetType()), z.GetType(), props)
    End Function


    Private Function ToXElement(ByVal z As XamRadialGauge, ByVal [default] As XamRadialGauge, ByVal props As IEnumerable(Of String)) As XElement
        Dim x = <Content/>

        For Each p In props
            Dim vz = TypeDescriptor.GetProperties(GetType(XamRadialGauge))(p).GetValue(z)
            Dim vd = TypeDescriptor.GetProperties(GetType(XamRadialGauge))(p).GetValue([default])

            If Not Object.Equals(vz, vd) Then
                x.Add(<Property Name=<%= p %>><%= If(vz Is Nothing, <Null/>, vz.ToString()) %></Property>)
            End If

        Next

        Dim scprops = (From kvp In SimpleDashboardPropertyGrid._typeproperties(GetType(RadialGaugeScale).FullName) Where kvp.Value Select kvp.Key).ToList()
        Dim defaultscale = New RadialGaugeScale()

        For Each sc In z.Scales

            Dim scx = <Scale/>

            x.Add(ToXElement(scx, sc, defaultscale, GetType(RadialGaugeScale), scprops))

            Dim labelprops = (From kvp In SimpleDashboardPropertyGrid._typeproperties(GetType(RadialGaugeLabelGroup).FullName) Where kvp.Value Select kvp.Key).ToList()
            Dim needleprops = (From kvp In SimpleDashboardPropertyGrid._typeproperties(GetType(RadialGaugeNeedle).FullName) Where kvp.Value Select kvp.Key).ToList()
            Dim tickprops = (From kvp In SimpleDashboardPropertyGrid._typeproperties(GetType(RadialGaugeTickMarkGroup).FullName) Where kvp.Value Select kvp.Key).ToList()
            Dim rangeprops = (From kvp In SimpleDashboardPropertyGrid._typeproperties(GetType(RadialGaugeRange).FullName) Where kvp.Value Select kvp.Key).ToList()

            For Each elt In sc.Needles
                scx.Add(ToXElement(<Needle/>, elt, needleprops))
            Next

            For Each elt In sc.LabelGroups
                scx.Add(ToXElement(<LabelGroup/>, elt, labelprops))
            Next

            For Each elt In sc.TickMarkGroups
                scx.Add(ToXElement(<TickMarkGroup/>, elt, tickprops))
            Next

            For Each elt In sc.Ranges
                scx.Add(ToXElement(<Range/>, elt, rangeprops))
            Next

        Next

        Return x
    End Function

End Class
Public Class SimpleDashboardZone
    Inherits ContentControl

    Private _Owner As SimpleDashboard

    Public Sub New()

    End Sub
    Public Sub New(ByVal owner As SimpleDashboard)
        Me._Owner = owner
    End Sub

    Public Property CornerRadius As CornerRadius
        Get
            Return GetValue(CornerRadiusProperty)
        End Get

        Set(ByVal value As CornerRadius)
            SetValue(CornerRadiusProperty, value)
        End Set
    End Property

    Public Shared ReadOnly CornerRadiusProperty As DependencyProperty = _
                           DependencyProperty.Register("CornerRadius", _
                           GetType(CornerRadius), GetType(SimpleDashboardZone), _
                           New FrameworkPropertyMetadata(New CornerRadius(0)))

    ' Private _PropertyGrid As SimpleDashboardPropertyGrid
    Protected Overrides Sub OnMouseDoubleClick(ByVal e As System.Windows.Input.MouseButtonEventArgs)
        MyBase.OnMouseDoubleClick(e)
        Dim prop = New SimpleDashboardPropertyGrid()
        prop.SetObject(Me)
        prop.ShowInTaskbar = True
        prop.ShowDialog()
    End Sub


End Class

Public Interface IDashboardData

    Sub SetText(ByVal name As String, ByVal value As String)

    Sub SetGaugeNeedle(ByVal name As String, ByVal value As Double, Optional ByVal index As Integer = 0)

    Sub SetSerieData(ByVal name As String, ByVal data As IEnumerable, Optional ByVal index As Integer = 0)

End Interface