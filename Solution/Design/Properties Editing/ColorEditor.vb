Imports Microsoft.Xna.Framework.Graphics

Public Class ColorEditor
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
        Me.MaximumSize = New System.Drawing.Size(50, 50)
        Me.MinimumSize = New System.Drawing.Size(50, 50)
        Me.Size = New System.Drawing.Size(100, 100)
        Me.ResumeLayout(False)

    End Sub

    Public Sub SetValue(ByVal value As Object) Implements ITypeEditor.SetValue
        BackColor = XnaColorToGdiColor(value)
    End Sub

    Public Event ValueEdited(ByVal sender As Object, ByVal value As Object) Implements ITypeEditor.ValueEdited

    Protected Overrides Sub OnMouseClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim colordialog As New Windows.Forms.ColorDialog
            colordialog.ShowDialog()
            BackColor = colordialog.Color
            RaiseEvent ValueEdited(Me, GdiColorToXnaColor(BackColor))
        End If
        MyBase.OnMouseClick(e)
    End Sub

    Protected Shared Function GdiColorToXnaColor(ByVal inc As System.Drawing.Color) As Microsoft.Xna.Framework.Graphics.Color
        Return New Microsoft.Xna.Framework.Graphics.Color(inc.R, inc.G, inc.B, inc.A)
    End Function
    Protected Shared Function XnaColorToGdiColor(ByVal inc As Microsoft.Xna.Framework.Graphics.Color) As System.Drawing.Color
        Return System.Drawing.Color.FromArgb(inc.A, inc.R, inc.G, inc.B)
    End Function

End Class