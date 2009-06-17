<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Vector2Editor
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.lblY = New System.Windows.Forms.Label
        Me.nupY = New System.Windows.Forms.NumericUpDown
        Me.lblX = New System.Windows.Forms.Label
        Me.nupX = New System.Windows.Forms.NumericUpDown
        CType(Me.nupY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nupX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblY
        '
        Me.lblY.AutoSize = True
        Me.lblY.Location = New System.Drawing.Point(5, 30)
        Me.lblY.Name = "lblY"
        Me.lblY.Size = New System.Drawing.Size(14, 13)
        Me.lblY.TabIndex = 7
        Me.lblY.Text = "Y"
        '
        'nupY
        '
        Me.nupY.Location = New System.Drawing.Point(25, 29)
        Me.nupY.Name = "nupY"
        Me.nupY.Size = New System.Drawing.Size(64, 20)
        Me.nupY.TabIndex = 6
        '
        'lblX
        '
        Me.lblX.AutoSize = True
        Me.lblX.Location = New System.Drawing.Point(5, 5)
        Me.lblX.Name = "lblX"
        Me.lblX.Size = New System.Drawing.Size(14, 13)
        Me.lblX.TabIndex = 5
        Me.lblX.Text = "X"
        '
        'nupX
        '
        Me.nupX.Location = New System.Drawing.Point(24, 3)
        Me.nupX.Name = "nupX"
        Me.nupX.Size = New System.Drawing.Size(64, 20)
        Me.nupX.TabIndex = 4
        '
        'Vector2Editor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.lblY)
        Me.Controls.Add(Me.nupY)
        Me.Controls.Add(Me.lblX)
        Me.Controls.Add(Me.nupX)
        Me.MinimumSize = New System.Drawing.Size(110, 60)
        Me.Name = "Vector2Editor"
        Me.Size = New System.Drawing.Size(108, 58)
        CType(Me.nupY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nupX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblY As System.Windows.Forms.Label
    Friend WithEvents nupY As System.Windows.Forms.NumericUpDown
    Friend WithEvents lblX As System.Windows.Forms.Label
    Friend WithEvents nupX As System.Windows.Forms.NumericUpDown

End Class
