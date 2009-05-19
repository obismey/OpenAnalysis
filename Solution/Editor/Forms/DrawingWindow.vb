Imports WeifenLuo.WinFormsUI.Docking

Public Class DrawingWindow
    Inherits DockContent

    Sub New()
        InitializeComponent()
    End Sub
    Private Sub InitializeComponent()
        Me.DrawingCtrl = New Editor.DrawingControl
        Me.SuspendLayout()
        '
        'DrawingCtrl
        '
        Me.DrawingCtrl.AllowRendering = False
        Me.DrawingCtrl.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DrawingCtrl.Location = New System.Drawing.Point(0, 0)
        Me.DrawingCtrl.Name = "DrawingCtrl"
        Me.DrawingCtrl.Size = New System.Drawing.Size(292, 266)
        Me.DrawingCtrl.TabIndex = 0
        Me.DrawingCtrl.Text = "DrawingControl1"
        '
        'DrawingWindow
        '
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.DrawingCtrl)
        Me.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document
        Me.Name = "DrawingWindow"
        Me.TabText = "The drawibng window"
        Me.Text = "The drawibng window"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DrawingCtrl As Editor.DrawingControl
End Class