Public Class ViewTriangleStandard
    Implements Core.UI.INavigationView

    Dim _BaseData As List(Of Cell)
    Dim _maxderoulement As Integer
    Dim _maxsurvenance As Integer
    'Dim _minderoulement As Integer
    'Dim _minsurvenance As Integer


    Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().
        Dim q = From i In Enumerable.Range(0, 10), j In Enumerable.Range(0, 10)
                Where i + j <= 9
                Select Row = i, Column = j, Value = If(i = 0 And j = 0, "", If(i + j <= 9, i & ";" & j, ""))
        MainTriangleListBox.DataContext = New With {.RowCount = 10, .ColumnCount = 10, .Data = q}
        'CoeffTriangleListBox.DataContext = New With {.RowCount = 10, .ColumnCount = 10, .Data = q}
    End Sub

    Public Property Caption As String Implements Core.UI.INavigationView.Caption
        Get
            Return "Triangle Standard"
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


    Private Function GetColByUsage(ByVal usage As String) As String
        Return (From col In Project.Active.DataModel Where col.Usage = usage Select col.Name).FirstOrDefault()
    End Function

    Private Sub UpdateBaseData()
        Dim survcol = GetColByUsage("Survenance")
        Dim deroulcol = GetColByUsage("Deroulement")
        Dim sincol = GetColByUsage("Sinistre")

        If String.IsNullOrEmpty(survcol) Or String.IsNullOrEmpty(deroulcol) Or String.IsNullOrEmpty(sincol) Then
            MsgBox("Brrr")
        End If

        'Dim q0 = (From r As DataRowView In Project.Active.InternalData.DefaultView
        '          Where CInt(r(survcol)) > 0 And CInt(r(deroulcol)) > 0
        '          Group By Row = CInt(r(survcol)), _
        '                   Column = CInt(r(deroulcol)) _
        '              Into Value = Sum(CDbl(r(sincol)))).ToArray()

        Dim q0 = (From r As DataRowView In Project.Active.InternalData.DefaultView
                  Group By Row = CInt(r(survcol)), _
                           Column = CInt(r(deroulcol)) _
                      Into Value = Sum(CDbl(r(sincol)))).ToArray()

        Me._maxderoulement = Aggregate elt In q0 Into Max(elt.Column)

        Me._maxsurvenance = Aggregate elt In q0 Into Max(elt.Row)

        'Me._minderoulement = Aggregate elt In q0 Into Min(elt.Column)

        'Me._minsurvenance = Aggregate elt In q0 Into Min(elt.Row)

        'Dim tempdata(Me._maxsurvenance + 2, Me._maxderoulement + 2) As Double
        Dim tempdata(Me._maxsurvenance, Me._maxderoulement) As Double

        For Each elt In q0
            tempdata(elt.Row, elt.Column) = elt.Value
        Next
        For i = 0 To Me._maxsurvenance
            For j = 1 To Me._maxderoulement
                tempdata(i, j) = tempdata(i, j) + tempdata(i, j - 1)
            Next
        Next

        'Me._BaseData = New List(Of Cell)((Me._maxsurvenance - Me._minsurvenance) * (Me._maxderoulement - Me._minderoulement) / 2)
        Me._BaseData = New List(Of Cell)((Me._maxsurvenance + 1) * (Me._maxderoulement + 1) / 2)

        'For i = Me._minsurvenance To Me._maxsurvenance
        '    For j = Me._minderoulement To Math.Min(i, Me._maxderoulement)
        '        Me._BaseData.Add(New Cell() With
        '       {
        '           .OriginalRow = i, _
        '           .Row = Me._maxsurvenance - i, _
        '           .OriginalColumn = j, _
        '           .Column = j - Me._minderoulement, _
        '           .OriginalValue = tempdata(i, j), _
        '           .Value = tempdata(i, j).ToString("N0")
        '       })
        '    Next
        'Next

        For i = 0 To Me._maxsurvenance
            For j = 0 To Me._maxderoulement - i
                Me._BaseData.Add(New Cell() With
               {
                   .OriginalRow = i, _
                   .Row = i, _
                   .OriginalColumn = j, _
                   .Column = j, _
                   .OriginalValue = tempdata(i, j), _
                   .Value = tempdata(i, j).ToString("N0")
               })
            Next
        Next


        MaxSurvenanceComboBox.ItemsSource = Enumerable.Range(0, Me._maxsurvenance)
        MinDeroulementComboBox.ItemsSource = Enumerable.Range(0, Me._maxderoulement)
    End Sub
    Private Sub UpdateData()
        Dim p2 = (From elt In Me._BaseData
                  Where elt.OriginalRow >= If(MaxSurvenanceComboBox.SelectedItem Is Nothing, Me._maxsurvenance, CInt(MaxSurvenanceComboBox.SelectedItem)) _
                    And elt.OriginalColumn >= If(MinDeroulementComboBox.SelectedItem Is Nothing, 0, CInt(MinDeroulementComboBox.SelectedItem))
                  Select elt).ToList()

        MainTriangleListBox.DataContext = New With { _
           .RowCount = (Aggregate elt In p2 Into Max(elt.Row)) + 1, _
           .ColumnCount = (Aggregate elt In p2 Into Max(elt.Column)) + 1, _
           .Data = p2}

        Dim coeffquery2 = (From elt In p2
                   Join elt2 In p2 On elt.Column Equals (elt2.Column + 1)
                   Where elt.Column >= 1 And (elt.OriginalRow = elt2.OriginalRow))

        Dim coeffquery1 = (From elt In p2 Join elt2 In p2 On elt.Column Equals (elt2.Column + 1)
                           Where elt.Column >= 1 And (elt.OriginalRow = elt2.OriginalRow)
                           Select Column = elt.Column, Value = If(elt2.OriginalValue = 0.0, 0.0, elt.OriginalValue / elt2.OriginalValue))

        Dim aggcoeffquery2 = From elt In coeffquery2 _
                            Group By elt.elt.Column Into _
                            Numerateur = Sum(elt.elt.OriginalValue), _
                            Denominateur = Sum(elt.elt2.OriginalValue)

        '  CoeffTriangleSeries.DataSource = From elt In aggcoeffquery2 Order By elt.Column Select If(elt.Denominateur = 0, Double.NaN, elt.Numerateur / elt.Denominateur)
        CoeffTriangleSeries.DataSource = From elt In coeffquery1 Group By elt.Column Into Value = Average(elt.Value)
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles Button1.Click
        Try
            UpdateBaseData()
            UpdateData()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try

    End Sub

    Private Sub MainGridSplitter_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Input.MouseButtonEventArgs) Handles MainGridSplitter.MouseDoubleClick
        MainGrid.RowDefinitions(0).Height = New Windows.GridLength(0.0, Windows.GridUnitType.Auto)
    End Sub

    Sub SetFilter(ByVal Filter As String)
        Project.Active.InternalData.DefaultView.RowFilter = Filter
        Button1.Content = Filter
        Button1_Click(Nothing, Nothing)
    End Sub

    Public Class Cell

        Public Property Row As Integer

        Public Property OriginalRow As Integer

        Public Property Column As Integer

        Public Property OriginalColumn As Integer

        Public Property Value As String

        Public Property OriginalValue As Double


    End Class

    Private Sub MaxSurvenanceComboBox_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles MaxSurvenanceComboBox.SelectionChanged
        Try
            UpdateData()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub

    Private Sub MinDeroulementComboBox_SelectionChanged(ByVal sender As Object, ByVal e As System.Windows.Controls.SelectionChangedEventArgs) Handles MinDeroulementComboBox.SelectionChanged
        Try
            UpdateData()
        Catch ex As Exception
            MsgBox(ex.ToString())
        End Try
    End Sub
End Class
