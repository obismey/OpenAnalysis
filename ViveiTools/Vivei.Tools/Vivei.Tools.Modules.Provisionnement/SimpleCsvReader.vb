'Public Class Triangle

'    Public Property Deroulement As TriangleDimension

'    Public Property Payement As TriangleDimension

'    Public Property Survenance As TriangleDimension

'    Public Property Declaration As TriangleDimension
'End Class

'Public Class TriangleCell

'    Public Property Deroulement As TriangleDimensionValue

'    Public Property DatePayement As TriangleDimensionValue

'    Public Property DateSurvenance As TriangleDimensionValue

'    Public Property DateEtude As TriangleDimensionValue

'    Public Property DateDeclaration As TriangleDimensionValue
'End Class

'Public Class TriangleDimension
'    Public Property Name As String

'    Public Property Type As TriangleDimensionType

'    Public Property IsSegmenting As Boolean
'End Class

'Public Class TriangleDimensionValue

'End Class

'Public Enum TriangleDimensionType
'    Unknow
'    Numeric
'    DateTime
'    Text

'End Enum

'Public Interface IExternalData
'    ReadOnly Property Name As String
'End Interface

'Public Class IntenalModel

'End Class


'Public Interface IModelData

'End Interface

'Public Class MemoryModelData
'    Implements IModelData

'    Private data As DataTable
'End Class

Public Class SimpleCsvReader

    Public Shared Function Read(ByVal file As String) As DataTable
        Dim lines = IO.File.ReadLines(file).GetEnumerator()
        Dim result As DataTable = New DataTable()
        result.BeginLoadData()
        lines.MoveNext()
        Dim separator = ";"c
        Dim firstline = lines.Current.Split(separator)
        If firstline.Length = 1 Then
            If firstline(0).Contains(",") Then
                separator = ","c
                firstline = lines.Current.Split(separator)
            End If
        End If
        For Each s In firstline
            result.Columns.Add(s, GetType(String))
        Next

        While lines.MoveNext()
            result.Rows.Add(lines.Current.Split(separator))
        End While

        result.EndLoadData()
        Return result
    End Function

    Public Shared Function Read(ByVal file As String, _
                                ByVal separator As String, _
                                ByVal decimalCharacter As Char, _
                                ByVal colonneNames As String(), _
                                ByVal colonneTypes As String(), _
                                ByVal colonneFormats As String()) As DataTable
        Dim lines = IO.File.ReadLines(file).GetEnumerator()
        Dim result As DataTable = New DataTable()
        If decimalCharacter = "."c Then
            result.Locale = Globalization.CultureInfo.InvariantCulture
        ElseIf decimalCharacter = ","c Then
            result.Locale = Globalization.CultureInfo.GetCultureInfo("fr-FR")
        End If


        For i = 0 To colonneNames.Length - 1
            If colonneTypes(i) = "Texte" Then
                result.Columns.Add(colonneNames(i), GetType(String))
            ElseIf colonneTypes(i) = "Nombre" Then
                result.Columns.Add(colonneNames(i), GetType(Double))
            ElseIf colonneTypes(i) = "Date" Then
                result.Columns.Add(colonneNames(i), GetType(DateTime))
            End If
        Next

        lines.MoveNext()
        Dim firstline = lines.Current.Split(separator).ToList()
        Dim colonneIndexes(colonneNames.Length) As Integer
        For i = 0 To colonneNames.Length - 1
            colonneIndexes(i) = firstline.IndexOf(colonneNames(i))
        Next

        result.BeginLoadData()
        While lines.MoveNext()
            Dim line = lines.Current.Split(separator)
            Dim nr = result.NewRow()
            For i = 0 To colonneNames.Length - 1
                If colonneTypes(i) = "Texte" Then
                    nr(i) = line(colonneIndexes(i))
                ElseIf colonneTypes(i) = "Nombre" Then
                    If line(colonneIndexes(i)) <> "" Then
                        If decimalCharacter = "."c Then
                            nr(i) = Double.Parse(line(colonneIndexes(i)), Globalization.CultureInfo.InvariantCulture)
                        ElseIf decimalCharacter = ","c Then
                            nr(i) = Double.Parse(line(colonneIndexes(i)), Globalization.CultureInfo.GetCultureInfo("fr-FR"))
                        End If
                    Else
                        nr(i) = 0.0
                    End If
                ElseIf colonneTypes(i) = "Date" Then
                    'result.Columns.Add(colonneNames(i), GetType(DateTime))
                End If
            Next
            result.Rows.Add(nr)
        End While
        result.EndLoadData()

        Return result
    End Function

    Public Shared Sub ReadToDB(ByVal file As String, _
                                    ByVal separator As String, _
                                    ByVal decimalCharacter As Char, _
                                    ByVal colonneNames As String(), _
                                    ByVal colonneTypes As String(), _
                                    ByVal colonneFormats As String(), _
                                    ByVal tablename As String, _
                                    ByVal connectionstring As String)

        Dim lines = IO.File.ReadLines(file).GetEnumerator()

        lines.MoveNext()
        Dim firstline = lines.Current.Split(separator).ToList()
        Dim colonneIndexes(colonneNames.Length) As Integer
        For i = 0 To colonneNames.Length - 1
            colonneIndexes(i) = firstline.IndexOf(colonneNames(i))
        Next

        Dim con = New SQLite.SQLiteConnection(connectionstring)
        Dim inserttextbase = "INSERT INTO " & tablename.ToUpper() & "(" & String.Join(",", colonneNames) & ") VALUES("
        Dim cmd = New SQLite.SQLiteCommand()
        cmd.Connection = con
        con.Open()
        Dim trans = con.BeginTransaction()
        While lines.MoveNext()
            Dim line = lines.Current.Split(separator)
            Dim inserttext = inserttextbase
            For i = 0 To colonneNames.Length - 1
                If colonneTypes(i) = "Texte" Then
                    inserttext = inserttext & "'" & line(colonneIndexes(i)) & "'"
                ElseIf colonneTypes(i) = "Nombre" Then
                    If line(colonneIndexes(i)) <> "" Then
                        If decimalCharacter = "."c Then
                            '  nr(i) = Double.Parse(line(colonneIndexes(i)), Globalization.CultureInfo.InvariantCulture)
                            inserttext = inserttext & line(colonneIndexes(i))
                        ElseIf decimalCharacter = ","c Then
                            inserttext = inserttext & Double.Parse(line(colonneIndexes(i)), Globalization.CultureInfo.GetCultureInfo("fr-FR")).ToString(Globalization.CultureInfo.InvariantCulture)
                        End If
                    Else
                        inserttext = inserttext & "0.0"
                    End If
                ElseIf colonneTypes(i) = "Date" Then
                    'result.Columns.Add(colonneNames(i), GetType(DateTime))
                End If
                If i = colonneNames.Length - 1 Then
                    inserttext = inserttext & ")"
                Else
                    inserttext = inserttext & ","
                End If
            Next
            cmd.CommandText = inserttext
            cmd.ExecuteNonQuery()
        End While
        trans.Commit()
        con.Close()
        con.Dispose()
    End Sub

    Public Shared Function ReadFirstLine(ByVal file As String) As List(Of String)
        Dim lines = IO.File.ReadLines(file).GetEnumerator()
        lines.MoveNext()
        Dim separator = ";"c
        Dim linesCurrent = lines.Current
        Dim firstline = lines.Current.Split(separator)
        If firstline.Length = 1 Then
            If firstline(0).Contains(",") Then
                separator = ","c
                firstline = lines.Current.Split(separator)
            End If
        End If
        lines.Dispose()
        lines = Nothing
        Dim result = New List(Of String)()
        result.Add(separator)
        result.Add(linesCurrent)
        result.AddRange(firstline)
        Return result
    End Function
End Class
