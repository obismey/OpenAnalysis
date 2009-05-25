<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ModelPreviewWindow
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.DrawingControl1 = New Editor.DrawingControl
        Me.Opd = New System.Windows.Forms.OpenFileDialog
        Me.Button1 = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'DrawingControl1
        '
        Me.DrawingControl1.AllowRendering = False
        Me.DrawingControl1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.DrawingControl1.Location = New System.Drawing.Point(0, 0)
        Me.DrawingControl1.Name = "DrawingControl1"
        Me.DrawingControl1.Size = New System.Drawing.Size(311, 275)
        Me.DrawingControl1.TabIndex = 0
        Me.DrawingControl1.Text = "DrawingControl1"
        '
        'Opd
        '
        Me.Opd.FileName = "OpenFileDialog1"
        Me.Opd.Filter = "Compiled Files (*.xnb)|*.xnb"
        '
        'Button1
        '
        Me.Button1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Button1.Location = New System.Drawing.Point(12, 237)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(30, 30)
        Me.Button1.TabIndex = 1
        Me.Button1.UseVisualStyleBackColor = True
        '
        'ModelPreviewWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(311, 275)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.DrawingControl1)
        Me.Name = "ModelPreviewWindow"
        Me.Text = "ModelPreviewWindow"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents DrawingControl1 As Editor.DrawingControl
    Friend WithEvents Opd As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Button1 As System.Windows.Forms.Button
End Class
