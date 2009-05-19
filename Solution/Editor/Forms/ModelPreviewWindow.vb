Imports Microsoft.Xna.Framework.Graphics

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

End Class