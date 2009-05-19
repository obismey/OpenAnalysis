Imports WeifenLuo.WinFormsUI.Docking
Public Class StructureForm
    Inherits DockContent

    Sub New()
        InitializeComponent()
      
    End Sub

    Friend WithEvents TreeView1 As System.Windows.Forms.TreeView
    Private Sub InitializeComponent()
        Dim TreeNode1 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Images")
        Dim TreeNode2 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Sounds")
        Dim TreeNode3 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Ressources", New System.Windows.Forms.TreeNode() {TreeNode1, TreeNode2})
        Dim TreeNode4 As System.Windows.Forms.TreeNode = New System.Windows.Forms.TreeNode("Documents")
        Me.TreeView1 = New System.Windows.Forms.TreeView
        Me.SuspendLayout()
        '
        'TreeView1
        '
        Me.TreeView1.CheckBoxes = True
        Me.TreeView1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.TreeView1.HotTracking = True
        Me.TreeView1.LabelEdit = True
        Me.TreeView1.LineColor = System.Drawing.Color.Red
        Me.TreeView1.Location = New System.Drawing.Point(0, 0)
        Me.TreeView1.Name = "TreeView1"
        TreeNode1.Name = "Node1"
        TreeNode1.Text = "Images"
        TreeNode2.Name = "Node2"
        TreeNode2.Text = "Sounds"
        TreeNode3.Name = "Node0"
        TreeNode3.Text = "Ressources"
        TreeNode4.Name = "Node3"
        TreeNode4.Text = "Documents"
        Me.TreeView1.Nodes.AddRange(New System.Windows.Forms.TreeNode() {TreeNode3, TreeNode4})
        Me.TreeView1.ShowNodeToolTips = True
        Me.TreeView1.Size = New System.Drawing.Size(286, 260)
        Me.TreeView1.TabIndex = 0
        '
        'StructureForm
        '
        Me.ClientSize = New System.Drawing.Size(286, 260)
        Me.Controls.Add(Me.TreeView1)
        Me.DockAreas = CType((WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight), WeifenLuo.WinFormsUI.Docking.DockAreas)
        Me.Name = "StructureForm"
        Me.TabText = "20"
        Me.Text = "20"
        Me.ResumeLayout(False)

    End Sub

    ReadOnly Property DocumentNode() As TreeNode
        Get
            Return TreeView1.Nodes(1)
        End Get
    End Property

    Dim labletext As String
    Dim datatext(2) As String
    Private Sub TreeView1_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterCheck

    End Sub
    Private Sub TreeView1_AfterLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles TreeView1.AfterLabelEdit
        e.Node.Text = e.Node.Text & datatext(1)
        e.CancelEdit = False
    End Sub
    Private Sub TreeView1_BeforeCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewCancelEventArgs) Handles TreeView1.BeforeCheck
    End Sub
    Private Sub TreeView1_BeforeLabelEdit(ByVal sender As Object, ByVal e As System.Windows.Forms.NodeLabelEditEventArgs) Handles TreeView1.BeforeLabelEdit
        labletext = e.Label
        If labletext IsNot Nothing Then
            If labletext.Contains(")") Then
                Dim str() As String = labletext.Split("(")
                Dim name As String = str(0).Trim()
            End If
        End If

        If e.Node.Text.Contains(")") Then
            Dim str() As String = e.Node.Text.Split("(")
            datatext(0) = str(0).Trim()
            datatext(1) = str(1).Split(")")(0)
            e.Node.Text = datatext(0)
            e.CancelEdit = False

        End If
    End Sub


    Private Sub TreeView1_DrawNode(ByVal sender As Object, ByVal e As System.Windows.Forms.DrawTreeNodeEventArgs) Handles TreeView1.DrawNode
        e.Graphics.DrawRectangle(Pens.Blue, e.Bounds)
    End Sub
End Class

