Imports Vivei.Tools.Core.UI
Imports System.Data.SQLite

Public Class ViewExternalData
    Implements INavigationView


    Public Property Caption As String Implements Core.UI.INavigationView.Caption
        Get
            Return "Donnees Externes"
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

    Private Sub Button_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs)
        If SQLRadioButton.IsChecked Then
            Dim opd = New Microsoft.Win32.OpenFileDialog()
            opd.Filter = "Fichiers Base de données Local|*.localdb"
            opd.InitialDirectory = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules\ProvisionnementModule\Data")

            If opd.ShowDialog() Then
                FileTextBlock.Text = opd.FileName

                Dim cbuilder = New SQLiteConnectionStringBuilder()
                cbuilder.DataSource = opd.FileName
                cbuilder.FailIfMissing = True

                Project.Active.ExternalDataLocalDatabaseConnectionString = cbuilder.ConnectionString
                Dim adapter = New SQLiteDataAdapter("SELECT * FROM FINALTABLE", Project.Active.ExternalDataLocalDatabaseConnectionString)
                Dim result As New DataTable()
                adapter.Fill(result)
                ProvisionnementModule._ActiveProject.ExternalData = result
                ProvisionnementModule._ActiveProject.NotifyExternalDataChanged()
                ProvisionnementModule._ActiveProject.DataModel.NotifySourceColumnsChange()
                PreviewDataGrid.ItemsSource = ProvisionnementModule._ActiveProject.ExternalData.DefaultView
                If ProvisionnementModule._ActiveProject.DataModel.Count = 0 Then
                    For Each c As DataColumn In ProvisionnementModule._ActiveProject.ExternalData.Columns
                        Dim dmp = New DataModelProperty()
                        dmp.Name = c.ColumnName
                        dmp.SourceColumn = c
                        If c.DataType Is GetType(String) Then
                            dmp.Type = "Texte"
                        ElseIf c.DataType Is GetType(Double) Then
                            dmp.Type = "Nombre"
                        End If
                        ProvisionnementModule._ActiveProject.DataModel.Add(dmp)
                    Next
                End If
            End If
            Return
        End If

        If CSVRadioButton.IsChecked Then


            Dim opd = New Microsoft.Win32.OpenFileDialog()
            opd.Filter = "Fichiers Texte|*.txt;*.csv"
            If opd.ShowDialog() Then
                Dim cow = New CsvOptionsWindow()
                Dim fl = SimpleCsvReader.ReadFirstLine(opd.FileName)
                cow.FirstLine = fl(1)
                cow.CsvSeparateurTextBox.Text = fl(0)
                cow.CsvSeparateurTextBox_TextChanged(Nothing, Nothing)
                cow.ShowDialog()
                Dim conf = CType(cow.CsvDatagrid.ItemsSource, IEnumerable(Of CsvOptionsWindow.ColumnOption))
                ProvisionnementModule._ActiveProject.ExternalData = SimpleCsvReader.Read( _
                    opd.FileName, _
                    cow.CsvSeparateurTextBox.Text, _
                    If(cow.DecimalComboBox.SelectedIndex = 0, "."c, ","c), _
                    conf.Select(Function(c) c.Name).ToArray(), _
                    conf.Select(Function(c) c.Type).ToArray(), _
                    conf.Select(Function(c) c.Format).ToArray())

                'ProvisionnementModule._ActiveProject.ExternalData = SimpleCsvReader.Read(opd.FileName)
                ProvisionnementModule._ActiveProject.NotifyExternalDataChanged()
                ProvisionnementModule._ActiveProject.DataModel.NotifySourceColumnsChange()
                PreviewDataGrid.ItemsSource = ProvisionnementModule._ActiveProject.ExternalData.DefaultView
                If ProvisionnementModule._ActiveProject.DataModel.Count = 0 Then
                    For Each c As DataColumn In ProvisionnementModule._ActiveProject.ExternalData.Columns
                        Dim dmp = New DataModelProperty()
                        dmp.Name = c.ColumnName
                        dmp.SourceColumn = c
                        If c.DataType Is GetType(String) Then
                            dmp.Type = "Texte"
                        ElseIf c.DataType Is GetType(Double) Then
                            dmp.Type = "Nombre"
                        End If
                        ProvisionnementModule._ActiveProject.DataModel.Add(dmp)
                    Next
                End If
                FileTextBlock.Text = opd.FileName

                Dim cbuilder = New SQLiteConnectionStringBuilder()
                Dim datafolder = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules\ProvisionnementModule\Data")
                cbuilder.DataSource = IO.Path.Combine(datafolder, Guid.NewGuid().ToString().Replace("-", "") & ".localdb")
                cbuilder.FailIfMissing = False

                Dim fun = Function(ColumnOption As CsvOptionsWindow.ColumnOption)
                              If ColumnOption.Type = "Texte" Then
                                  Return ColumnOption.Name.ToUpper() & " TEXT"
                              ElseIf ColumnOption.Type = "Nombre" Then
                                  Return ColumnOption.Name.ToUpper() & " REAL"
                              ElseIf ColumnOption.Type = "Date" Then
                                  Return ColumnOption.Name.ToUpper() & " DATETIME"
                              End If

                              Return ""
                          End Function
                Dim con = New SQLiteConnection(cbuilder.ConnectionString)
                Dim cmd = New SQLiteCommand()
                cmd.CommandText = "DROP TABLE IF EXISTS MAINTABLE;CREATE TABLE MAINTABLE(" & String.Join(",", conf.Select(fun).ToArray()) & ")"

                cmd.Connection = con
                con.Open()
                cmd.ExecuteNonQuery()
                con.Close()
                con.Dispose()

                Project.Active.ExternalDataLocalDatabaseConnectionString = cbuilder.ConnectionString


                SimpleCsvReader.ReadToDB( _
                    opd.FileName, _
                    cow.CsvSeparateurTextBox.Text, _
                    If(cow.DecimalComboBox.SelectedIndex = 0, "."c, ","c), _
                    conf.Select(Function(c) c.Name.ToUpper()).ToArray(), _
                    conf.Select(Function(c) c.Type).ToArray(), _
                    conf.Select(Function(c) c.Format).ToArray(), _
                    "MAINTABLE", _
                    Project.Active.ExternalDataLocalDatabaseConnectionString)
            End If
        End If

    End Sub
End Class
