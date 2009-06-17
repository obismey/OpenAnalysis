<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ThicknessEditor
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
        Me.topLbl = New System.Windows.Forms.Label
        Me.topTxt = New System.Windows.Forms.TextBox
        Me.leftTxt = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.rightTxt = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.downTxt = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'topLbl
        '
        Me.topLbl.AutoSize = True
        Me.topLbl.Location = New System.Drawing.Point(4, 12)
        Me.topLbl.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.topLbl.Name = "topLbl"
        Me.topLbl.Size = New System.Drawing.Size(31, 15)
        Me.topLbl.TabIndex = 0
        Me.topLbl.Text = "Top"
        '
        'topTxt
        '
        Me.topTxt.Location = New System.Drawing.Point(43, 11)
        Me.topTxt.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.topTxt.Name = "topTxt"
        Me.topTxt.Size = New System.Drawing.Size(48, 21)
        Me.topTxt.TabIndex = 1
        '
        'leftTxt
        '
        Me.leftTxt.Location = New System.Drawing.Point(43, 38)
        Me.leftTxt.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.leftTxt.Name = "leftTxt"
        Me.leftTxt.Size = New System.Drawing.Size(48, 21)
        Me.leftTxt.TabIndex = 3
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(5, 42)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(31, 15)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Left"
        '
        'rightTxt
        '
        Me.rightTxt.Location = New System.Drawing.Point(99, 11)
        Me.rightTxt.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.rightTxt.Name = "rightTxt"
        Me.rightTxt.Size = New System.Drawing.Size(48, 21)
        Me.rightTxt.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(155, 12)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(41, 15)
        Me.Label2.TabIndex = 4
        Me.Label2.Text = "Right"
        '
        'downTxt
        '
        Me.downTxt.Location = New System.Drawing.Point(99, 38)
        Me.downTxt.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.downTxt.Name = "downTxt"
        Me.downTxt.Size = New System.Drawing.Size(48, 21)
        Me.downTxt.TabIndex = 7
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(155, 41)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(43, 15)
        Me.Label3.TabIndex = 6
        Me.Label3.Text = "Down"
        '
        'ThicknessEditor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Controls.Add(Me.downTxt)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.rightTxt)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.leftTxt)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.topTxt)
        Me.Controls.Add(Me.topLbl)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Margin = New System.Windows.Forms.Padding(4, 3, 4, 3)
        Me.MinimumSize = New System.Drawing.Size(210, 70)
        Me.Name = "ThicknessEditor"
        Me.Size = New System.Drawing.Size(210, 70)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents topLbl As System.Windows.Forms.Label
    Private WithEvents topTxt As System.Windows.Forms.TextBox
    Private WithEvents leftTxt As System.Windows.Forms.TextBox
    Private WithEvents Label1 As System.Windows.Forms.Label
    Private WithEvents rightTxt As System.Windows.Forms.TextBox
    Private WithEvents Label2 As System.Windows.Forms.Label
    Private WithEvents downTxt As System.Windows.Forms.TextBox
    Private WithEvents Label3 As System.Windows.Forms.Label

End Class
