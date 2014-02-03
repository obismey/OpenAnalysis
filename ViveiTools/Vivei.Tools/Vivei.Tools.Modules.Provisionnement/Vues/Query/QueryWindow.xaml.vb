Imports System.Data.SQLite

Partial Public Class QueryWindow

    Dim rows As ObjectModel.ObservableCollection(Of QueryRow)
    Dim sorts As ObjectModel.ObservableCollection(Of QuerySort)
    Dim filters As ObjectModel.ObservableCollection(Of QueryFilter)
    Dim originalfilters As ObjectModel.ObservableCollection(Of QueryFilter)
    Dim sources As ObjectModel.ObservableCollection(Of QuerySource)


    Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().

    End Sub

    Sub SetDataTable(ByVal dataTable As DataTable, ByVal workflow As SqlQueryWorkflow)
        If dataTable Is Nothing Then Return


        SourceComboBox.ItemsSource = workflow.Queries


        Me.rows = New ObjectModel.ObservableCollection(Of QueryRow)
        Me.sorts = New ObjectModel.ObservableCollection(Of QuerySort)
        Me.filters = New ObjectModel.ObservableCollection(Of QueryFilter)
        Me.originalfilters = New ObjectModel.ObservableCollection(Of QueryFilter)
        Me.sources = New ObjectModel.ObservableCollection(Of QuerySource)

        SortDataGrid.ItemsSource = sorts
        SelectionDataGrid.ItemsSource = rows
        FilterDataGrid.ItemsSource = filters
        OriginalFilterDataGrid.ItemsSource = originalfilters
        ColonnesTreeView.ItemsSource = sources


        Dim formulas = (From c As DataColumn In dataTable.Columns Select New QueryFormula(c.ColumnName)).ToList()

        formulas.Insert(0, New QueryFormula())

        CType(SelectionDataGrid.Columns(1), Windows.Controls.DataGridComboBoxColumn).ItemsSource = formulas

        CType(SortDataGrid.Columns(0), Windows.Controls.DataGridComboBoxColumn).ItemsSource = rows

        CType(FilterDataGrid.Columns(0), Windows.Controls.DataGridComboBoxColumn).ItemsSource = rows

        CType(OriginalFilterDataGrid.Columns(0), Windows.Controls.DataGridComboBoxColumn).ItemsSource = (From elt As DataColumn In dataTable.Columns Select elt.ColumnName).ToArray()

    End Sub

    Private Function PreviewSql() As String
        Dim fs = Function(s As QuerySort)
                     If s.Row Is Nothing Then Return ""
                     If String.IsNullOrEmpty(s.Direction) Then
                         Return s.Row.Name & " ASC "
                     End If
                     If s.Direction.ToLower().Contains("asc") Or s.Direction.ToLower() = "croissant" Then
                         Return s.Row.Name & " ASC "
                     End If
                     If s.Direction.ToLower().Contains("desc") Or s.Direction.ToLower() = "decroissant" Then
                         Return s.Row.Name & " DESC "
                     End If
                     Return ""
                 End Function

        Dim aggtosql = Function(agg As String)
                           If agg = "Somme" Then Return "sum"
                           If agg = "Moyenne" Then Return "avg"
                           If agg = "Minimum" Then Return "min"
                           If agg = "Maximum" Then Return "max"
                           If agg = "Variance" Then Return "stdev"
                           If agg = "Ecart-Type" Then Return "stdev"
                           If agg = "Nombre" Then Return "count"

                           Return ""
                       End Function

        Dim fr = Function(r As QueryRow)
                     If r.Formula Is Nothing Then Return ""
                     If r.Aggregate = "" Or r.Aggregate.ToLower() = "(aucun)" Then
                         Return "(t1." & r.Formula.Formula & ") AS " & r.Name
                     End If
                     Return "(" & aggtosql(r.Aggregate) & "(" & "t1." & r.Formula.Formula & ")) AS " & r.Name
                 End Function

        Dim opetosql = Function(ope As String)
                           If ope = "Egal" Then Return "="
                           If ope = "Different" Then Return "<>"
                           If ope = "Superieur ou Egal" Then Return ">="
                           If ope = "Superieur" Then Return ">"
                           If ope = "Inferieur ou Egal" Then Return "<="
                           If ope = "Inferieur" Then Return "<"
                           If ope = "Est Contenu Dans" Then Return "IN"
                           If ope = "Comme" Then Return ""
                           If ope = "Contient" Then Return ""
                           If ope = "Commence Par" Then Return ""
                           If ope = "Termine Par" Then Return ""

                           Return ""
                       End Function

        Dim ff = Function(f As QueryFilter)
                     If f.OriginalRow = "" And f.Row Is Nothing Then Return ""

                     If f.OriginalRow <> "" Then
                         Return "t1." & f.OriginalRow & " " & opetosql(f.Operator) & " " & f.SimpleValue
                     Else
                         Return f.Row.Name & " " & opetosql(f.Operator) & " " & f.SimpleValue
                     End If
                 End Function


        Dim selectpart = "SELECT " & If(DistinctCheckBox.IsChecked, " DISTINCT ", "") & vbCrLf & String.Join("," & vbCrLf, (From r In rows Select "  " & fr(r)).ToArray())
        Dim frompart = "FROM MAINTABLE AS t1 "
        Dim sortpart = If(sorts.Count = 0, "", "ORDER BY " & String.Join(",", (From s In sorts Select fs(s)).ToArray()))
        Dim grouppart = ""
        Dim colnotgrouped = From r In rows Where r.Aggregate = "" Or r.Aggregate.ToLower() = "(aucun)"

        If colnotgrouped.Count() <> rows.Count Then
            grouppart = vbCrLf & "GROUP BY " & String.Join(",", (From elt In colnotgrouped Select elt.Name).ToArray())
        End If

        Dim originalwherepart = String.Join("  " & vbCrLf & " AND ", (From f In originalfilters Select "(" & ff(f) & ")").ToArray())
        Dim computedwherepart = String.Join("  " & vbCrLf & " AND ", (From f In filters Where f.Row.Aggregate = "" Or f.Row.Aggregate.ToLower() = "(aucun)" Select "(" & ff(f) & ")").ToArray())
        Dim wherepart = ""
        If originalwherepart <> "" Then
            wherepart = originalwherepart
            If computedwherepart <> "" Then
                wherepart = wherepart & " AND " & computedwherepart
            End If
        Else
            If computedwherepart <> "" Then
                wherepart = computedwherepart
            End If
        End If
        If wherepart <> "" Then
            wherepart = "WHERE " & vbCrLf & wherepart
        End If
        Dim computedhavingpart = String.Join("  " & vbCrLf & " AND ", (From f In filters Where f.Row.Aggregate <> "" And f.Row.Aggregate.ToLower() <> "(aucun)" Select "(" & ff(f) & ")").ToArray())
        Dim havingpart = ""

        If computedhavingpart <> "" Then
            havingpart = vbCrLf & "HAVING " & vbCrLf & computedhavingpart
        End If

        Return selectpart & vbCrLf & frompart & wherepart & grouppart & havingpart & vbCrLf & sortpart

    End Function

    Private Sub PreviewSqlButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles PreviewSqlButton.Click

        PreviewSqlTextBox.Text = PreviewSql()

    End Sub

    Private Sub PreviewDataButton_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles PreviewDataButton.Click
        'Dim con = New SQLiteConnection(Project.Active.ExternalDataLocalDatabaseConnectionString)
        ''Dim dbcontext = New DbLinq.Data.Linq.DataContext(con, New DbLinq.Sqlite.SqliteVendor())
        'Dim dbcontext = New System.Data.Linq.DataContext(con)
        'Dim q = dbcontext.GetTable(Of MainTableRow)()

        'Dim cmd = dbcontext.GetCommand(From r In q Select r.Deroulement)

        'Dim o = 1
        ''   dbcontext.GetCommand
        ''Dim cmd = New SQLiteCommand()
        ''cmd.CommandText = "SELECT * FROM MAINTABLE"
        ''cmd.Connection = con
        ''con.Open()
        ''cmd.ExecuteReader()
        ''con.Close()
        ''con.Dispose()
        ''Return
        Try
            Dim adapter = New SQLiteDataAdapter(PreviewSqlTextBox.Text, Project.Active.ExternalDataLocalDatabaseConnectionString)
            Dim result As New DataTable()
            adapter.Fill(result)

            PreviewDataGrid.ItemsSource = result.DefaultView
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub PreviewDataGrid_AutoGeneratingColumn(ByVal sender As Object, ByVal e As System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs) Handles PreviewDataGrid.AutoGeneratingColumn
        Dim c = TryCast(e.Column, Windows.Controls.DataGridBoundColumn)
        If c IsNot Nothing Then
            Dim cmenu = New Windows.Controls.ContextMenu()
            Dim item = New Windows.Controls.MenuItem()
            item.Header = "Format"
            Dim path = CType(c.Binding, Windows.Data.Binding).Path.Path
            AddHandler item.Click, Sub() c.Binding = New Windows.Data.Binding(path) With {.StringFormat = InputBox("Format de la colonne")}
            cmenu.Items.Add(item)
            Dim s = New Windows.Style(GetType(Windows.Controls.Primitives.DataGridColumnHeader))
            s.Setters.Add(New Windows.Setter(Windows.Controls.Primitives.DataGridColumnHeader.ContextMenuProperty, cmenu))
            c.HeaderStyle = s
        End If
    End Sub

    Public Function GetQueryData() As SqlQueryData
        Return New SqlQueryData(rows, sorts, filters, originalfilters, sources)
    End Function
    Public Function GetQueryDataWithResults() As SqlQueryData
        Dim data = New SqlQueryData(rows, sorts, filters, originalfilters, sources)
        data.SqlQueryText = PreviewSqlTextBox.Text
        data.SqlQueryData = CType(PreviewDataGrid.ItemsSource, DataView).Table
        Return (data)
    End Function
    Public Sub SetQueryData(ByVal querydata As SqlQueryData, ByVal datatable As DataTable, ByVal workflow As SqlQueryWorkflow)

        ColonnesTreeView.ItemsSource = datatable.Columns

        Me.rows = querydata.Rows
        Me.sorts = querydata.Sorts
        Me.filters = querydata.Filters
        Me.originalfilters = querydata.Originalfilters


        Dim formulas = (From c As DataColumn In datatable.Columns Select New QueryFormula(c.ColumnName)).ToList()

        formulas.Insert(0, New QueryFormula())

        CType(SelectionDataGrid.Columns(1), Windows.Controls.DataGridComboBoxColumn).ItemsSource = formulas

        CType(SortDataGrid.Columns(0), Windows.Controls.DataGridComboBoxColumn).ItemsSource = rows

        CType(FilterDataGrid.Columns(0), Windows.Controls.DataGridComboBoxColumn).ItemsSource = rows

        CType(OriginalFilterDataGrid.Columns(0), Windows.Controls.DataGridComboBoxColumn).ItemsSource = (From elt As DataColumn In datatable.Columns Select elt.ColumnName).ToArray()

        SortDataGrid.ItemsSource = sorts
        SelectionDataGrid.ItemsSource = rows
        FilterDataGrid.ItemsSource = filters
        OriginalFilterDataGrid.ItemsSource = originalfilters

    End Sub

    Private Sub AddSourceButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        sources.Add(New QuerySource() With {.DataTable = "", .QueryData = SourceComboBox.SelectedItem})
    End Sub
End Class

<System.Data.Linq.Mapping.Table(name:="MainTable")>
Public Class MainTableRow

    <System.Data.Linq.Mapping.Column()>
    Public Property Survenance As Double

    <System.Data.Linq.Mapping.Column()>
    Public Property Deroulement As Double

    <System.Data.Linq.Mapping.Column()>
    Public Property Sinistre As Double
End Class


Public Class SqlQueryWorkflow
    Inherits Core.UI.UIObject

    Sub New()
        Me.Queries = New ObjectModel.ObservableCollection(Of SqlQueryData)()
        Me.Queries.Add(New SqlQueryData() With {.Name = "MAINTABLE"})

    End Sub
    Public Property Queries As ObjectModel.ObservableCollection(Of SqlQueryData)
End Class
