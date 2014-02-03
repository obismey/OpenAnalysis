Imports System.Windows.Controls
Imports System.Windows
Imports Vivei.Tools.Core.UI
Imports System.Windows.Data


Public Class CustomGrid
    Inherits Grid



    Public Property Rows As Integer
        Get
            Return GetValue(RowsProperty)
        End Get

        Set(ByVal value As Integer)
            SetValue(RowsProperty, value)
        End Set
    End Property

    Public Shared ReadOnly RowsProperty As DependencyProperty = _
                           DependencyProperty.Register("Rows", _
                           GetType(Integer), GetType(CustomGrid), _
                           New FrameworkPropertyMetadata(1, AddressOf RowsChangedCallback))




    Public Property Columns As Integer
        Get
            Return GetValue(ColumnsProperty)
        End Get

        Set(ByVal value As Integer)
            SetValue(ColumnsProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ColumnsProperty As DependencyProperty = _
                           DependencyProperty.Register("Columns", _
                           GetType(Integer), GetType(CustomGrid), _
                           New FrameworkPropertyMetadata(1, AddressOf ColumnsChangedCallback))




    Public Shared Sub RowsChangedCallback(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        Dim cg As CustomGrid = d

        cg.RowDefinitions.Clear()

        Dim nv As Integer = e.NewValue

        If nv > 1 Then
            For i = 0 To nv - 1
                cg.RowDefinitions.Add(New RowDefinition() With {.Height = New GridLength(1, GridUnitType.Star)})
            Next
        End If
    End Sub

    Public Shared Sub ColumnsChangedCallback(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
        Dim cg As CustomGrid = d

        cg.ColumnDefinitions.Clear()

        Dim nv As Integer = e.NewValue

        If nv > 1 Then
            For i = 0 To nv - 1
                cg.ColumnDefinitions.Add(New ColumnDefinition() With {.Width = New GridLength(1, GridUnitType.Star)})
            Next
        End If
    End Sub
End Class

Public Class ColumnWidth
    Inherits UIObject

    Private _Value As GridLength
    Public Property Value As GridLength
        Get
            Return _Value
        End Get
        Set(ByVal value As GridLength)
            _Value = value
            OnPropertyChanged("Value")
        End Set
    End Property


End Class
Public Class SheetGrid
    Inherits Grid

    Private Shared Widths() As ColumnWidth = (From i In Enumerable.Range(0, 1000) Select New ColumnWidth() With {.Value = New GridLength(75, GridUnitType.Pixel)}).ToArray()

    Sub New()
        Media.RenderOptions.SetEdgeMode(Me, Media.EdgeMode.Aliased)
        'Grid.SetIsSharedSizeScope(Me, True)

    End Sub
    Private _ContentData(,) As ContentControl

    Friend Sub SetLayout(ByVal rowcount As Integer, ByVal columncount As Integer)

        Children.Clear()

        Me.RowDefinitions.Clear()

        Dim nv As Integer = rowcount

        If nv > 1 Then
            For i = 0 To nv - 1
                Me.RowDefinitions.Add(New RowDefinition() With {.Height = GridLength.Auto}) '
            Next
        End If

        Me.ColumnDefinitions.Clear()

        nv = columncount

        If nv > 1 Then
            For i = 0 To nv - 1
                Dim col = New ColumnDefinition()
                BindingOperations.SetBinding(col, ColumnDefinition.WidthProperty, New Binding("Value") With {.Mode = BindingMode.TwoWay, .Source = Widths(i)})
                Me.ColumnDefinitions.Add(col)
            Next
        End If

        Dim tempdata(rowcount - 1, columncount - 1) As ContentControl
        Me._ContentData = tempdata
        For i = 0 To rowcount - 1
            For j = 0 To columncount - 1
                tempdata(i, j) = New ContentControl() With {.HorizontalAlignment = Windows.HorizontalAlignment.Center, .VerticalAlignment = Windows.VerticalAlignment.Center}
                tempdata(i, j).Content = " "
                tempdata(i, j).Margin = New Thickness(5)
                Grid.SetRow(tempdata(i, j), i)
                Grid.SetColumn(tempdata(i, j), j)
                Children.Add(tempdata(i, j))
            Next
        Next

        If rowcount > 1 Then
            For i = 1 To rowcount - 1
                Dim Splitter = New GridSplitter()
                With Splitter
                    .Background = Media.Brushes.DarkGray
                    .Height = 2
                    .HorizontalAlignment = Windows.HorizontalAlignment.Stretch
                    .VerticalAlignment = Windows.VerticalAlignment.Top
                    .Margin = New Thickness(0, -1, 0, 0)
                End With
                Grid.SetRow(Splitter, i)
                Grid.SetRowSpan(Splitter, 1)
                Children.Add(Splitter)

                Dim Rect = New Shapes.Rectangle()
                With Rect
                    .Fill = Media.Brushes.Black
                    .Height = 1
                    .HorizontalAlignment = Windows.HorizontalAlignment.Stretch
                    .VerticalAlignment = Windows.VerticalAlignment.Top
                End With
                Grid.SetRow(Rect, i)
                Grid.SetRowSpan(Rect, 1)
                Grid.SetColumn(Rect, 1)
                Grid.SetColumnSpan(Rect, columncount)
                Children.Add(Rect)
            Next
        End If


        If columncount > 1 Then
            For i = 1 To columncount - 1
                Dim Splitter = New GridSplitter()
                With Splitter
                    .Background = Media.Brushes.DarkGray
                    .Width = 2
                    .HorizontalAlignment = Windows.HorizontalAlignment.Left
                    .VerticalAlignment = Windows.VerticalAlignment.Stretch
                    .Margin = New Thickness(-1, 0, 0, 0)
                End With
                Grid.SetColumn(Splitter, i)
                Grid.SetColumnSpan(Splitter, 1)
                Children.Add(Splitter)

                Dim Rect = New Shapes.Rectangle()
                With Rect
                    .Fill = Media.Brushes.Black
                    .Width = 1
                    .HorizontalAlignment = Windows.HorizontalAlignment.Left
                    .VerticalAlignment = Windows.VerticalAlignment.Stretch
                End With
                Grid.SetColumn(Rect, i)
                Grid.SetColumnSpan(Rect, 1)
                Grid.SetRow(Rect, 1)
                Grid.SetRowSpan(Rect, rowcount)
                Children.Add(Rect)
            Next
        End If
    End Sub
    Public Function GetCell(ByVal row As Integer, ByVal column As Integer) As ContentControl
        Return _ContentData(row, column)
    End Function

    'Public Property Rows As Integer
    '    Get
    '        Return GetValue(RowsProperty)
    '    End Get

    '    Set(ByVal value As Integer)
    '        SetValue(RowsProperty, value)
    '    End Set
    'End Property

    'Public Shared ReadOnly RowsProperty As DependencyProperty = _
    '                       DependencyProperty.Register("Rows", _
    '                       GetType(Integer), GetType(SheetGrid), _
    '                       New FrameworkPropertyMetadata(1, AddressOf RowsChangedCallback))

    'Public Property Columns As Integer
    '    Get
    '        Return GetValue(ColumnsProperty)
    '    End Get

    '    Set(ByVal value As Integer)
    '        SetValue(ColumnsProperty, value)
    '    End Set
    'End Property

    'Public Shared ReadOnly ColumnsProperty As DependencyProperty = _
    '                       DependencyProperty.Register("Columns", _
    '                       GetType(Integer), GetType(SheetGrid), _
    '                       New FrameworkPropertyMetadata(1, AddressOf ColumnsChangedCallback))


    'Private _ContentData(,) As ContentControl
    'Public Function GetCell(ByVal row As Integer, ByVal column As Integer) As ContentControl
    '    Return _ContentData(row, column)
    'End Function

    'Public Shared Sub RowsChangedCallback(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
    '    Dim sg As SheetGrid = d

    '    sg.RowDefinitions.Clear()

    '    Dim nv As Integer = e.NewValue

    '    If nv > 1 Then
    '        For i = 0 To nv - 1
    '            sg.RowDefinitions.Add(New RowDefinition() With {.Height = New GridLength(1, GridUnitType.Star)})
    '        Next
    '    End If
    'End Sub

    'Public Shared Sub ColumnsChangedCallback(ByVal d As System.Windows.DependencyObject, ByVal e As System.Windows.DependencyPropertyChangedEventArgs)
    '    Dim sg As SheetGrid = d

    '    sg.ColumnDefinitions.Clear()

    '    Dim nv As Integer = e.NewValue

    '    If nv > 1 Then
    '        For i = 0 To nv - 1
    '            sg.ColumnDefinitions.Add(New ColumnDefinition() With {.Width = New GridLength(1, GridUnitType.Star)})
    '        Next
    '    End If
    'End Sub

End Class