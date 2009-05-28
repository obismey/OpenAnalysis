Public Class MainWindow

  
    Private Sub MainWindow_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DrawingWindow.Show(Dock)
    End Sub


    Private Sub ModelToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ModelToolStripMenuItem.Click
        For Each c As Control In Controls
            c.Visible = False
            BackColor = Color.Black
        Next

        Dim filter As String = "Model Files (*.fbx;*.x)|*.fbx;*.x|"
        filter = filter & "Fbx Files (*.fbx)|*.fbx|"
        filter = filter & "DirectX Files (*.x)|*.x"

        Opd.Filter = filter

        If Opd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Bgw.RunWorkerAsync(New String() {"model", Opd.FileName})
            LoadingWindow.Label1.Text = "Importing " & IO.Path.GetFileNameWithoutExtension(Opd.FileName) & " ... "
            Dim tip As New ToolTip()
            tip.SetToolTip(LoadingWindow.Label1, LoadingWindow.Label1.Text)
            LoadingWindow.ShowDialog(Me)
        End If
    End Sub
    Private Sub SoundToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SoundToolStripMenuItem.Click
        For Each c As Control In Controls
            c.Visible = False
            BackColor = Color.Black
        Next

        Dim filter As String = "Sound Files (*.wav;*.mp3;*.wma)|*.wav;*.mp3;*.wma|"
        filter = filter & "Wave Files (*.wav)|*.wav|"
        filter = filter & "Mp3 Files (*.mp3)|*.mp3|"
        filter = filter & "Windows Media Files (*.wma)|*.wma"

        Opd.Filter = filter

        If Opd.ShowDialog() = Windows.Forms.DialogResult.OK Then
            Bgw.RunWorkerAsync(New String() {"music", Opd.FileName})
            LoadingWindow.Label1.Text = "Importing " & IO.Path.GetFileNameWithoutExtension(Opd.FileName) & " ... "
            Dim tip As New ToolTip()
            tip.SetToolTip(LoadingWindow.Label1, LoadingWindow.Label1.Text)
            LoadingWindow.ShowDialog(Me)
        End If

    End Sub


    Private Sub Bgw_DoWork(ByVal sender As Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles Bgw.DoWork

        Dim data() As String = e.Argument

        If data(0).ToLower = "music" Then
            Dim builder As New ContentBuilder()

            Dim path As String = IO.Path.Combine(Application.StartupPath, "Compiled")

            If Not IO.Directory.Exists(path) Then IO.Directory.CreateDirectory(path)

            builder.Add(data(1), IO.Path.GetFileNameWithoutExtension(data(1)), Nothing, "SongProcessor")

            Dim result As String = builder.Build()

            If String.IsNullOrEmpty(result) Then
                e.Result = "Imported Successfully"
                IO.Directory.Move(builder.OutputDirectory, IO.Path.Combine(path, Guid.NewGuid().ToString().Replace("-", "")))
            Else
                e.Result = result
            End If

            builder.Dispose()

            builder = Nothing

            Return

        End If

        If data(0).ToLower = "model" Then
            Dim builder As New ContentBuilder()

            Dim path As String = IO.Path.Combine(Application.StartupPath, "Compiled")

            If Not IO.Directory.Exists(path) Then IO.Directory.CreateDirectory(path)

            builder.Add(data(1), IO.Path.GetFileNameWithoutExtension(data(1)), Nothing, "ModelProcessor")

            Dim result As String = builder.Build()

            If String.IsNullOrEmpty(result) Then
                e.Result = "Imported Successfully"
                IO.Directory.Move(builder.OutputDirectory, IO.Path.Combine(path, Guid.NewGuid().ToString().Replace("-", "")))
            Else
                e.Result = result
            End If

            builder.Dispose()

            builder = Nothing

            Return
        End If

        If data(0).ToLower = "shader" Then

        End If
    End Sub

    Private Sub Bgw_RunWorkerCompleted(ByVal sender As Object, ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs) Handles Bgw.RunWorkerCompleted
        LoadingWindow.Label1.Text = e.Result
        LoadingWindow.Button1.Text = "Fermer"
    End Sub

    Private Sub AffichageToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AffichageToolStripMenuItem.Click
        '      SoundPlayer.Show(Me)

        ModelPreviewWindow.Show(Me)

    End Sub

    Private Sub MusicToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MusicToolStripMenuItem.Click

    End Sub
End Class
