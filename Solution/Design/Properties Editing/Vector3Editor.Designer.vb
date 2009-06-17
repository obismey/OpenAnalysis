<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Vector3Editor
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
        Me.lblX = New System.Windows.Forms.Label
        Me.lblY = New System.Windows.Forms.Label
        Me.lblZ = New System.Windows.Forms.Label
        Me.nupZ = New System.Windows.Forms.NumericUpDown
        Me.nupY = New System.Windows.Forms.NumericUpDown
        Me.nupX = New System.Windows.Forms.NumericUpDown
        CType(Me.nupZ, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nupY, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.nupX, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblX
        '
        Me.lblX.AutoSize = True
        Me.lblX.Location = New System.Drawing.Point(5, 7)
        Me.lblX.Name = "lblX"
        Me.lblX.Size = New System.Drawing.Size(14, 13)
        Me.lblX.TabIndex = 1
        Me.lblX.Text = "X"
        '
        'lblY
        '
        Me.lblY.AutoSize = True
        Me.lblY.Location = New System.Drawing.Point(5, 33)
        Me.lblY.Name = "lblY"
        Me.lblY.Size = New System.Drawing.Size(14, 13)
        Me.lblY.TabIndex = 3
        Me.lblY.Text = "Y"
        '
        'lblZ
        '
        Me.lblZ.AutoSize = True
        Me.lblZ.Location = New System.Drawing.Point(5, 59)
        Me.lblZ.Name = "lblZ"
        Me.lblZ.Size = New System.Drawing.Size(14, 13)
        Me.lblZ.TabIndex = 5
        Me.lblZ.Text = "Z"
        '
        'nupZ
        '
        Me.nupZ.Location = New System.Drawing.Point(24, 57)
        Me.nupZ.Name = "nupZ"
        Me.nupZ.Size = New System.Drawing.Size(64, 20)
        Me.nupZ.TabIndex = 4
        '
        'nupY
        '
        Me.nupY.Location = New System.Drawing.Point(24, 31)
        Me.nupY.Name = "nupY"
        Me.nupY.Size = New System.Drawing.Size(64, 20)
        Me.nupY.TabIndex = 2
        '
        'nupX
        '
        Me.nupX.Location = New System.Drawing.Point(24, 5)
        Me.nupX.Name = "nupX"
        Me.nupX.Size = New System.Drawing.Size(64, 20)
        Me.nupX.TabIndex = 0
        '
        'Vector3Editor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.AutoSize = True
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.lblZ)
        Me.Controls.Add(Me.nupZ)
        Me.Controls.Add(Me.lblY)
        Me.Controls.Add(Me.nupY)
        Me.Controls.Add(Me.lblX)
        Me.Controls.Add(Me.nupX)
        Me.MinimumSize = New System.Drawing.Size(110, 85)
        Me.Name = "Vector3Editor"
        Me.Size = New System.Drawing.Size(108, 83)
        CType(Me.nupZ, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nupY, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.nupX, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblX As System.Windows.Forms.Label
    Friend WithEvents lblY As System.Windows.Forms.Label
    Friend WithEvents lblZ As System.Windows.Forms.Label
    Friend WithEvents nupZ As System.Windows.Forms.NumericUpDown
    Friend WithEvents nupY As System.Windows.Forms.NumericUpDown
    Friend WithEvents nupX As System.Windows.Forms.NumericUpDown

End Class
