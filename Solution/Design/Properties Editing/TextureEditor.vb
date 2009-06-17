Public Class TextureEditor
    Inherits Windows.Forms.Button
    Implements ITypeEditor

    Sub New()
        InitializeComponent()

    End Sub
    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'ColorEditor
        '
        Me.FlatAppearance.BorderColor = System.Drawing.Color.Black
        Me.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.MaximumSize = New System.Drawing.Size(100, 100)
        Me.MinimumSize = New System.Drawing.Size(100, 100)
        Me.Size = New System.Drawing.Size(100, 100)
        Me.BackgroundImageLayout = Windows.Forms.ImageLayout.Stretch
        Me.ResumeLayout(False)

    End Sub


    Public Sub SetValue(ByVal value As Object) Implements ITypeEditor.SetValue

    End Sub
    Protected Overrides Sub OnMouseClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim frm As New listForm
            frm.ShowDialog()
            If frm.selected IsNot Nothing Then
                BackgroundImage = System.Drawing.Image.FromFile(frm.selected)
                RaiseEvent ValueEdited(Me, frm.selected)
            End If

        End If
        MyBase.OnMouseClick(e)
    End Sub
    Public Event ValueEdited(ByVal sender As Object, ByVal value As Object) Implements ITypeEditor.ValueEdited

    Private Class listForm
        Inherits Windows.Forms.Form

        Private WithEvents lst As Windows.Forms.ListBox
        Public selected As String
        Sub New()
            lst = New Windows.Forms.ListBox
            lst.Dock = Windows.Forms.DockStyle.Fill
            Dim q = From f In IO.Directory.GetFiles(IO.Directory.GetCurrentDirectory()) _
                  Select New lstEntry() With {.Path = f, .Text = IO.Path.GetFileNameWithoutExtension(f)}
            lst.Items.AddRange(q.ToArray())

            Controls.Add(lst)
        End Sub


        Protected Overrides Sub OnFormClosing(ByVal e As System.Windows.Forms.FormClosingEventArgs)
            If lst.SelectedItem Is Nothing Then
                selected = Nothing

            Else
                selected = CType(lst.SelectedItem, lstEntry).Path
            End If
            MyBase.OnFormClosing(e)
        End Sub
    End Class
    Private Class lstEntry
        Public Text As String
        Public Path As String
        Public Overrides Function ToString() As String
            Return Text
        End Function
    End Class


End Class
