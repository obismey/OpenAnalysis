Imports Engine
Imports Microsoft.Xna.Framework.Graphics
Imports <xmlns:xs="http://www.w3.org/2001/XMLSchema">

Public Class TestWindow
    Inherits Window

    Dim WithEvents MainContainer As ControlContainer


    Public Overrides Sub InitializeComponents()
        MainContainer = New ControlContainer()

        'MainContainer.Rows.Add("0.20")
        'MainContainer.Rows.Add("0.40")
        'MainContainer.Rows.Add("0.30")

        'MainContainer.Columns.Add("0.40")
        'MainContainer.Columns.Add("0.30")
        'MainContainer.Columns.Add("0.20")

        Dim tmp As New TestControl
        MainContainer.AddChild(tmp)
        'MainContainer.Row(tmp) = 0
        'MainContainer.Column(tmp) = 0
        'MainContainer.RowSpan(tmp) = 1
        'MainContainer.ColumnSpan(tmp) = 1
        tmp.DefaultBrush = "bt"

        'Dim tmp2 As New TestControl
        'MainContainer.AddChild(tmp2)
        'MainContainer.Row(tmp2) = 0
        'MainContainer.Column(tmp2) = 3
        'MainContainer.RowSpan(tmp2) = 4
        'MainContainer.ColumnSpan(tmp2) = 1

        'Dim tmp3 As New TestControl
        'MainContainer.AddChild(tmp3)
        'MainContainer.Row(tmp3) = 1
        'MainContainer.Column(tmp3) = 0
        'MainContainer.RowSpan(tmp3) = 3
        'MainContainer.ColumnSpan(tmp3) = 3

        Root = MainContainer()
        Dim mpup As New keyMap With { _
           .Speed = 3.0F, _
           .maxValue = 500, _
           .minValue = 100, _
           .key = Microsoft.Xna.Framework.Input.Keys.Up}

        Dim mpdown As New keyMap With { _
        .Speed = -2.0F, _
        .maxValue = 500, _
        .minValue = 100, _
        .key = Microsoft.Xna.Framework.Input.Keys.Down}


        Dim ks As New KeySource
        ks.Maps.Add(mpup)
        ks.Maps.Add(mpdown)
        With Root
            .VAlign = Alignment.Center
            .HAlign = Alignment.Strech
            .Margin = New Thickness(50, 50)
            '.HeightSource = ks
            .Height = 300
        End With

    End Sub

    Private Sub MainContainer_MouseEnter(ByVal sender As Object, ByVal e As Engine.MouseEventArgs) Handles MainContainer.MouseEnter
        'Debug.Write(MainContainer.Children(0).DrawRect.ToString)
    End Sub
End Class

Public Class TestControl
    Inherits Control

    Sub New()
        VAlign = Alignment.Bottom
        HAlign = Alignment.Rigth
        Width = 150
        Height = 50
        Margin = New Thickness(5, 10)
        BorderBrush = Color.Red
        DefaultBrush = Color.Black
        Visiblility = Visibility.Visible
    End Sub

    Private Sub TestControl_MouseClick(ByVal sender As Object, ByVal e As Engine.MouseEventArgs) Handles Me.MouseClick
        DefaultBrush.Color = Color.Blue
    End Sub
    Private Sub TestControl_MouseEnter(ByVal sender As Object, ByVal e As Engine.MouseEventArgs) Handles Me.MouseEnter
        DefaultBrush.Color = Color.Violet

    End Sub
    Private Sub TestControl_MouseExit(ByVal sender As Object, ByVal e As Engine.MouseEventArgs) Handles Me.MouseExit
        DefaultBrush.Color = Color.Yellow
    End Sub

    Protected Overrides Sub Draw(ByVal context As Engine.DrawContext, ByVal timeElapsed As Double?, ByVal totalTimeElapsed As Double?)
        MyBase.Draw(context, timeElapsed, totalTimeElapsed)
        'Debug.Print(DrawRect.ToString())
    End Sub
    Protected Overrides Sub Update(ByVal timeElapsed As Double?, ByVal totalTimeElapsed As Double?)
        MyBase.Update(timeElapsed, totalTimeElapsed)
        'Debug.Print(DimensionRect.ToString() & " " & Container.DimensionRect.ToString())
    End Sub
End Class
