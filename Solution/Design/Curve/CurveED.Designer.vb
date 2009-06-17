<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class CurveED
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
        Me.components = New System.ComponentModel.Container
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Root")
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.OptionsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EchelleToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MinToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MaxToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AddLineToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.Label1 = New System.Windows.Forms.Label
        Me.TreeView1 = New System.Windows.Forms.TreeView
        Me.CMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.DeleteToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SetPositionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ChangeColorToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SetNameToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MenuStrip1.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.CMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.OptionsToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(505, 24)
        Me.MenuStrip1.TabIndex = 0
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'OptionsToolStripMenuItem
        '
        Me.OptionsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.EchelleToolStripMenuItem, Me.AddLineToolStripMenuItem})
        Me.OptionsToolStripMenuItem.Name = "OptionsToolStripMenuItem"
        Me.OptionsToolStripMenuItem.Size = New System.Drawing.Size(56, 20)
        Me.OptionsToolStripMenuItem.Text = "Options"
        '
        'EchelleToolStripMenuItem
        '
        Me.EchelleToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MinToolStripMenuItem, Me.MaxToolStripMenuItem})
        Me.EchelleToolStripMenuItem.Name = "EchelleToolStripMenuItem"
        Me.EchelleToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.EchelleToolStripMenuItem.Text = "Echelle"
        '
        'MinToolStripMenuItem
        '
        Me.MinToolStripMenuItem.Name = "MinToolStripMenuItem"
        Me.MinToolStripMenuItem.Size = New System.Drawing.Size(105, 22)
        Me.MinToolStripMenuItem.Text = "Min"
        '
        'MaxToolStripMenuItem
        '
        Me.MaxToolStripMenuItem.Name = "MaxToolStripMenuItem"
        Me.MaxToolStripMenuItem.Size = New System.Drawing.Size(105, 22)
        Me.MaxToolStripMenuItem.Text = "Max"
        '
        'AddLineToolStripMenuItem
        '
        Me.AddLineToolStripMenuItem.Name = "AddLineToolStripMenuItem"
        Me.AddLineToolStripMenuItem.Size = New System.Drawing.Size(152, 22)
        Me.AddLineToolStripMenuItem.Text = "Add Line"
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(255, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(192, Byte), Integer))
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 264)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(505, 28)
        Me.Panel1.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(10, 6)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(51, 15)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Label1"
        '
        'TreeView1
        '
        Me.TreeView1.CheckBoxes = True
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Left
        Me.TreeView1.Location = New System.Drawing.Point(0, 24)
        Me.TreeView1.Name = "TreeView1"
        TreeNode1.Name = "Root"
        TreeNode1.Text = "Root"
        Me.TreeView1.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode1})
        Me.TreeView1.Size = New System.Drawing.Size(125, 240)
        Me.TreeView1.TabIndex = 2
        '
        'CMenu
        '
        Me.CMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.DeleteToolStripMenuItem, Me.SetPositionToolStripMenuItem, Me.ChangeColorToolStripMenuItem, Me.SetNameToolStripMenuItem})
        Me.CMenu.Name = "CMenu"
        Me.CMenu.Size = New System.Drawing.Size(151, 92)
        '
        'DeleteToolStripMenuItem
        '
        Me.DeleteToolStripMenuItem.Name = "DeleteToolStripMenuItem"
        Me.DeleteToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.DeleteToolStripMenuItem.Text = "Delete"
        '
        'SetPositionToolStripMenuItem
        '
        Me.SetPositionToolStripMenuItem.Name = "SetPositionToolStripMenuItem"
        Me.SetPositionToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.SetPositionToolStripMenuItem.Text = "Set Position"
        '
        'ChangeColorToolStripMenuItem
        '
        Me.ChangeColorToolStripMenuItem.Name = "ChangeColorToolStripMenuItem"
        Me.ChangeColorToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.ChangeColorToolStripMenuItem.Text = "Change Color"
        '
        'SetNameToolStripMenuItem
        '
        Me.SetNameToolStripMenuItem.Name = "SetNameToolStripMenuItem"
        Me.SetNameToolStripMenuItem.Size = New System.Drawing.Size(150, 22)
        Me.SetNameToolStripMenuItem.Text = "Set Name"
        '
        'CurveED
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ContextMenuStrip = Me.CMenu
        Me.Controls.Add(Me.TreeView1)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Name = "CurveED"
        Me.Size = New System.Drawing.Size(505, 292)
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.CMenu.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents OptionsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EchelleToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MinToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MaxToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents CMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents DeleteToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetPositionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ChangeColorToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SetNameToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AddLineToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem

End Class
