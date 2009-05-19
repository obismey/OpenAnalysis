Public Class LoadingWindow

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If Button1.Text = "Fermer" Then
            Hide()
            For Each c As Control In MainWindow.Controls
                c.Visible = True
            Next
            MainWindow.BackColor = Color.Gray
        End If
    End Sub
End Class