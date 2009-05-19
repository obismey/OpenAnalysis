Imports WeifenLuo.WinFormsUI.Docking
Imports ICSharpCode.TextEditor.Document

Public Class CodeForm
    Inherits DockContent

    Sub New()
        InitializeComponent()
        codeEdit.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighterForFile("*.vb")
        _mode = "*.vb"

        codeEdit.ActiveTextAreaControl.TextArea.AllowDrop = True
        '   codeEdit.ActiveTextAreaControl.AllowDrop = True
        ' AddHandler codeEdit.ActiveTextAreaControl.DragEnter, AddressOf codeEdit_DragEnter
        '   AddHandler codeEdit.ActiveTextAreaControl.DragDrop, AddressOf codeEdit_DragDrop
        AddHandler codeEdit.ActiveTextAreaControl.TextArea.DragEnter, AddressOf cDragEnter
        AddHandler codeEdit.ActiveTextAreaControl.TextArea.DragDrop, AddressOf cDragDrop


    End Sub

    Private Sub InitializeComponent()
        Me.codeEdit = New ICSharpCode.TextEditor.TextEditorControl
        Me.SuspendLayout()
        '
        'codeEdit
        '
        Me.codeEdit.AllowDrop = True
        Me.codeEdit.Dock = System.Windows.Forms.DockStyle.Fill
        Me.codeEdit.IsReadOnly = False
        Me.codeEdit.Location = New System.Drawing.Point(0, 0)
        Me.codeEdit.Name = "codeEdit"
        Me.codeEdit.ShowEOLMarkers = True
        Me.codeEdit.ShowSpaces = True
        Me.codeEdit.ShowTabs = True
        Me.codeEdit.Size = New System.Drawing.Size(393, 266)
        Me.codeEdit.TabIndex = 0
        Me.codeEdit.Text = "TextEditorControl1"
        '
        'CodeForm
        '
        Me.ClientSize = New System.Drawing.Size(393, 266)
        Me.Controls.Add(Me.codeEdit)
        Me.DockAreas = CType(((WeifenLuo.WinFormsUI.Docking.DockAreas.DockTop Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockBottom) _
                    Or WeifenLuo.WinFormsUI.Docking.DockAreas.Document), WeifenLuo.WinFormsUI.Docking.DockAreas)
        Me.Name = "CodeForm"
        Me.TabText = "Code"
        Me.Text = "Code"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents codeEdit As ICSharpCode.TextEditor.TextEditorControl

    Private _mode As String
    Property Mode() As String
        Get
            Return _mode
        End Get
        Set(ByVal value As String)
            If _mode <> value Then
                codeEdit.Document.HighlightingStrategy = HighlightingManager.Manager.FindHighlighterForFile(value)
                _mode = value
            End If
        End Set
    End Property

  
    Private Sub cDragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
      
    End Sub

  
    Private Sub cDragEnter(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs)
        '    e.Effect = DragDropEffects.Copy
    End Sub

End Class
