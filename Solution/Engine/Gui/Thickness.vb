Imports System.ComponentModel
Imports System.Globalization


Public Structure Thickness

    Public Left, Top, Right, Down As Dimension


    Sub New(ByVal all As Single)
        Left = all
        Top = all
        Right = all
        Down = all
    End Sub
    Sub New(ByVal all As Single, ByVal relative As RelativeTo)
        Left = all
        Top = all
        Right = all
        Down = all

        Left.Relative = relative
        Top.Relative = relative
        Right.Relative = relative
        Down.Relative = relative
    End Sub
    Sub New(ByVal horizontal As Single, ByVal vertical As Single)
        Left = horizontal
        Top = vertical
        Right = horizontal
        Down = vertical
    End Sub
    Sub New(ByVal horizontal As Single, ByVal vertical As Single, ByVal relative As RelativeTo)
        Left = horizontal
        Top = vertical
        Right = horizontal
        Down = vertical

        Left.Relative = relative
        Top.Relative = relative
        Right.Relative = relative
        Down.Relative = relative
    End Sub

    'left,top,right,bottom
    Friend Function ComputeAbsolute(ByVal sourceWidth As Single, ByVal sourceHeigth As Single) As Single()
        Dim result(4) As Single

        result(0) = Left.ComputeAbsolute(sourceWidth)
        result(1) = Top.ComputeAbsolute(sourceHeigth)
        result(2) = Right.ComputeAbsolute(sourceWidth)
        result(3) = Down.ComputeAbsolute(sourceHeigth)

        Return result
    End Function

    'left,top,right,down
    Shared Widening Operator CType(ByVal source As String) As Thickness
        Dim s As New ComponentModel.SingleConverter

        Dim data() As String = source.Split(",")
        For Each st As String In data
            st = st.Trim()
        Next

        Dim dimensions(data.Length) As Dimension

        For i As Integer = 0 To data.Length - 1
            Dim relatifto As String = data(i)(data(i).Length - 1)
            Dim tmp As RelativeTo = RelativeTo.Absolute

            Select Case relatifto
                Case "a"
                    tmp = RelativeTo.Auto
                Case "i"
                    tmp = RelativeTo.Ignore
                Case "p"
                    tmp = RelativeTo.Parent
            End Select

            Dim value As String = ""

            If tmp <> RelativeTo.Absolute Then
                value = data(i).Substring(0, data(i).Length - 1)
            Else
                value = data(i)
            End If

            dimensions(i) = New Dimension(tmp, s.ConvertFromInvariantString(value))
        Next

        Return New Thickness With { _
                     .Down = dimensions(3), _
                     .Left = dimensions(0), _
                     .Right = dimensions(2), _
                     .Top = dimensions(1)}
    End Operator

    Public Overrides Function ToString() As String
        Dim fields() As Reflection.FieldInfo = Me.GetType().GetFields()
        Dim results(fields.Length) As String
        For i As Integer = 0 To fields.Length - 1
            results(i) = fields(i).Name & " : " & fields(i).GetValue(Me).ToString()
        Next
        Return String.Join(Environment.NewLine, results)
    End Function
End Structure



