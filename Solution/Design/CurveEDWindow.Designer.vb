<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CurveEDWindow
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
        Me.components = New System.ComponentModel.Container
        Me.CurveED1 = New Design.CurveED
        Me.SuspendLayout()
        '
        'CurveED1
        '
        Me.CurveED1.BackColor = System.Drawing.Color.White
        Me.CurveED1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CurveED1.DrawAsContinuous = True
        Me.CurveED1.GridLinesColor = System.Drawing.Color.Black
        Me.CurveED1.HorizontalLineIntervalle = 20
        Me.CurveED1.Location = New System.Drawing.Point(0, 0)
        Me.CurveED1.Mode = Design.EditMode.Add
        Me.CurveED1.Name = "CurveED1"
        Me.CurveED1.Size = New System.Drawing.Size(696, 431)
        Me.CurveED1.TabIndex = 0
        Me.CurveED1.VerticalLineIntervalle = 20
        '
        'CurveEDWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(696, 431)
        Me.Controls.Add(Me.CurveED1)
        Me.Name = "CurveEDWindow"
        Me.Text = "CurveEDWindow"
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents CurveED1 As Design.CurveED
End Class
