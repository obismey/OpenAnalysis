Imports System.ComponentModel
Imports System.Globalization

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
<System.Serializable()> _
Public Structure Dimension

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Value As Single

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Public Relative As RelativeTo

    Sub New(ByVal r As RelativeTo, ByVal v As Single)
        Value = v
        Relative = r
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ComputeAbsolute(ByVal source As Single) As Single
        Select Case Relative
            Case RelativeTo.Absolute
                Return Value

            Case RelativeTo.Parent
                Return Value * source

            Case RelativeTo.Auto
                Return 0.0F

            Case RelativeTo.Ignore
                Return source
        End Select
    End Function


    Shared Narrowing Operator CType(ByVal _source As String) As Dimension
        Dim s As New ComponentModel.SingleConverter
        Dim source As String = _source.Trim()

        Dim val As Single = 0.0F
        Dim rel As RelativeTo

        Select Case source.ToLower
            Case "auto"
                rel = RelativeTo.Auto
                Exit Select
            Case "ignore"
                rel = RelativeTo.Ignore
                Exit Select
            Case "absolute"
                rel = RelativeTo.Absolute
                Exit Select
            Case "parent"
                rel = RelativeTo.Parent
                Exit Select
            Case Else
                If source.Contains("{") Then
                    source = source.Trim("{", " ", "}")
                    Dim s2() As String = source.Split(";")
                    rel = s2(1)
                    val = s2(0)
                Else
                    rel = RelativeTo.Absolute
                    val = CSng(s.ConvertFromInvariantString(source))
                End If
                Exit Select
        End Select

        Return New Dimension With {.Relative = rel, .Value = val}
    End Operator
    Shared Narrowing Operator CType(ByVal source As Dimension) As String
        Return "{ " & source.Value & ";" & source.Relative.ToString() & " }"
    End Operator

    Public Overrides Function ToString() As String
        Return "{ " & Value & ";" & Relative.ToString() & " }"
    End Function
End Structure



