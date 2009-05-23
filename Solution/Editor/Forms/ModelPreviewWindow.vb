﻿Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework
Public Class ModelPreviewWindow
    Private mdl As Model


    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub ModelPreviewWindow_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Opd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            With DrawingControl1.content
                .RootDirectory = IO.Path.GetDirectoryName(Opd.FileName)
                mdl = .Load(Of Model)(IO.Path.GetFileNameWithoutExtension(Opd.FileName))
            End With

            DrawingControl1.model = New ModelDrawer(mdl, DrawingControl1.GraphicsDevice)

        Else
            Hide()
        End If

    End Sub

    Private Sub Button1_MouseMove(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Button1.MouseMove
        Dim ms As Drawing.Point = e.Location
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim pos As Vector2 = New Vector2(ms.X, ms.Y)
            Dim dir As Vector2 = Vector2.Normalize(pos - DrawingControl1.lastpos)
            DrawingControl1.cam.Rotate(dir.X / 5, dir.Y / 5)
        End If

        DrawingControl1.lastpos = New Vector2(e.X, e.Y)
        MyBase.OnMouseMove(e)
    End Sub
End Class