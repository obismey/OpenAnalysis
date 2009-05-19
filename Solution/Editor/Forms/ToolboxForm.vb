Imports WeifenLuo.WinFormsUI.Docking
Public Class ToolboxForm
    Inherits DockContent

    Sub New()
        InitializeComponent()

    End Sub

    Friend WithEvents FlowLayoutPanel1 As System.Windows.Forms.FlowLayoutPanel
    Private Sub InitializeComponent()
        Me.FlowLayoutPanel1 = New System.Windows.Forms.FlowLayoutPanel
        Me.SuspendLayout()
        '
        'FlowLayoutPanel1
        '
        Me.FlowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.FlowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown
        Me.FlowLayoutPanel1.Location = New System.Drawing.Point(0, 0)
        Me.FlowLayoutPanel1.Name = "FlowLayoutPanel1"
        Me.FlowLayoutPanel1.Padding = New System.Windows.Forms.Padding(10)
        Me.FlowLayoutPanel1.Size = New System.Drawing.Size(182, 260)
        Me.FlowLayoutPanel1.TabIndex = 0
        '
        'ToolboxForm
        '
        Me.ClientSize = New System.Drawing.Size(182, 260)
        Me.Controls.Add(Me.FlowLayoutPanel1)
        Me.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft
        Me.Name = "ToolboxForm"
        Me.TabText = "Toolbox"
        Me.Text = "Toolbox"
        Me.ResumeLayout(False)

    End Sub

 
End Class

Public Class ToolboxBt
    Inherits Button

    Sub New()
        AutoSize = True
        BackColor = System.Drawing.Color.Transparent
        FlatAppearance.BorderSize = 0
        FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red
        FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(CType(CType(128, Byte), Integer), CType(CType(255, Byte), Integer), CType(CType(128, Byte), Integer))
        FlatStyle = System.Windows.Forms.FlatStyle.Flat


    End Sub


    Protected Overrides Sub OnMouseDown(ByVal mevent As System.Windows.Forms.MouseEventArgs)
        If mevent.Button = Windows.Forms.MouseButtons.Left Then
            DoDragDrop(Tag, DragDropEffects.Copy)
        Else
            Dim t As Type = Tag

            CodeForm.codeEdit.BeginUpdate()

            CodeForm.codeEdit.ActiveTextAreaControl.TextArea.InsertString(vbCrLf & "<" & t.Name & " />")

            CodeForm.codeEdit.EndUpdate()
        End If

        Debug.Print(CType(Tag, Type).FullName)
        MyBase.OnMouseDown(mevent)
    End Sub

    Protected Overrides Sub OnDoubleClick(ByVal e As System.EventArgs)
        Dim t As Type = Tag

        CodeForm.codeEdit.BeginUpdate()

        CodeForm.codeEdit.ActiveTextAreaControl.TextArea.InsertString(vbCrLf & "<" & t.Name & " />")

        CodeForm.codeEdit.EndUpdate()
        MyBase.OnDoubleClick(e)
    End Sub
End Class