Imports System.Windows.Forms

Public Class BooleanEditor
    Inherits UserControl
    Implements Design.ITypeEditor


    Private WithEvents RadioButton1 As System.Windows.Forms.RadioButton
    Private WithEvents RadioButton2 As System.Windows.Forms.RadioButton
    Private Sub InitializeComponent()
        Me.RadioButton1 = New System.Windows.Forms.RadioButton
        Me.RadioButton2 = New System.Windows.Forms.RadioButton
        Me.SuspendLayout()
        '
        'RadioButton1
        '
        Me.RadioButton1.AutoSize = True
        Me.RadioButton1.Location = New System.Drawing.Point(3, 3)
        Me.RadioButton1.Name = "RadioButton1"
        Me.RadioButton1.Size = New System.Drawing.Size(47, 17)
        Me.RadioButton1.TabIndex = 0
        Me.RadioButton1.TabStop = True
        Me.RadioButton1.Text = "True"
        Me.RadioButton1.UseVisualStyleBackColor = True
        '
        'RadioButton2
        '
        Me.RadioButton2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RadioButton2.AutoSize = True
        Me.RadioButton2.Location = New System.Drawing.Point(100, 3)
        Me.RadioButton2.Name = "RadioButton2"
        Me.RadioButton2.Size = New System.Drawing.Size(50, 17)
        Me.RadioButton2.TabIndex = 1
        Me.RadioButton2.TabStop = True
        Me.RadioButton2.Text = "False"
        Me.RadioButton2.UseVisualStyleBackColor = True
        '
        'BooleanEditor
        '
        Me.AutoSize = True
        Me.Controls.Add(Me.RadioButton2)
        Me.Controls.Add(Me.RadioButton1)
        Me.MinimumSize = New System.Drawing.Size(150, 25)
        Me.Name = "BooleanEditor"
        Me.Size = New System.Drawing.Size(153, 25)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Sub New()
        InitializeComponent()

    End Sub




    Public Overridable Sub SetValue(ByVal value As Object) Implements Design.ITypeEditor.SetValue
        RadioButton1.Checked = value
        RadioButton2.Checked = Not RadioButton1.Checked
    End Sub

    Public Event ValueEdited(ByVal sender As Object, ByVal value As Object) Implements Design.ITypeEditor.ValueEdited

    Private Sub RadioButton1_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioButton1.CheckedChanged
        RaiseEvent ValueEdited(Me, RadioButton1.Checked)
    End Sub

   
End Class
