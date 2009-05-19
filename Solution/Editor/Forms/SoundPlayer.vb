Imports Microsoft.Xna.Framework.Media
Public Class SoundPlayer

    Private sng As Song
    Private Sub SoundPlayer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        If Opd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            With DrawingWindow.DrawingCtrl.content
                .RootDirectory = IO.Path.GetDirectoryName(Opd.FileName)
                sng = .Load(Of Song)(IO.Path.GetFileNameWithoutExtension(Opd.FileName))
            End With
        Else
            Hide()
        End If

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        MediaPlayer.Play(sng)
    End Sub
End Class