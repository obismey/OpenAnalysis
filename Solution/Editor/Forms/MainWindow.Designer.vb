<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MainWindow
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MainWindow))
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.FichierToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.EditionToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ImportsToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ModelToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.TextureToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SoundToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.AffichageToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.ToolStripButton1 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton2 = New System.Windows.Forms.ToolStripButton
        Me.ToolStripButton3 = New System.Windows.Forms.ToolStripButton
        Me.Dock = New WeifenLuo.WinFormsUI.Docking.DockPanel
        Me.Opd = New System.Windows.Forms.OpenFileDialog
        Me.Bgw = New System.ComponentModel.BackgroundWorker
        Me.MenuStrip1.SuspendLayout()
        Me.ToolStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FichierToolStripMenuItem, Me.EditionToolStripMenuItem, Me.AffichageToolStripMenuItem})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(658, 24)
        Me.MenuStrip1.TabIndex = 1
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'FichierToolStripMenuItem
        '
        Me.FichierToolStripMenuItem.Name = "FichierToolStripMenuItem"
        Me.FichierToolStripMenuItem.Size = New System.Drawing.Size(50, 20)
        Me.FichierToolStripMenuItem.Text = "Fichier"
        '
        'EditionToolStripMenuItem
        '
        Me.EditionToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ImportsToolStripMenuItem})
        Me.EditionToolStripMenuItem.Name = "EditionToolStripMenuItem"
        Me.EditionToolStripMenuItem.Size = New System.Drawing.Size(51, 20)
        Me.EditionToolStripMenuItem.Text = "Edition"
        '
        'ImportsToolStripMenuItem
        '
        Me.ImportsToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ModelToolStripMenuItem, Me.TextureToolStripMenuItem, Me.SoundToolStripMenuItem})
        Me.ImportsToolStripMenuItem.Name = "ImportsToolStripMenuItem"
        Me.ImportsToolStripMenuItem.Size = New System.Drawing.Size(122, 22)
        Me.ImportsToolStripMenuItem.Text = "Imports"
        '
        'ModelToolStripMenuItem
        '
        Me.ModelToolStripMenuItem.Name = "ModelToolStripMenuItem"
        Me.ModelToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.ModelToolStripMenuItem.Text = "Model"
        '
        'TextureToolStripMenuItem
        '
        Me.TextureToolStripMenuItem.Name = "TextureToolStripMenuItem"
        Me.TextureToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.TextureToolStripMenuItem.Text = "Texture"
        '
        'SoundToolStripMenuItem
        '
        Me.SoundToolStripMenuItem.Name = "SoundToolStripMenuItem"
        Me.SoundToolStripMenuItem.Size = New System.Drawing.Size(123, 22)
        Me.SoundToolStripMenuItem.Text = "Sound"
        '
        'AffichageToolStripMenuItem
        '
        Me.AffichageToolStripMenuItem.Name = "AffichageToolStripMenuItem"
        Me.AffichageToolStripMenuItem.Size = New System.Drawing.Size(65, 20)
        Me.AffichageToolStripMenuItem.Text = "Affichage"
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.ToolStripButton1, Me.ToolStripButton2, Me.ToolStripButton3})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 24)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(658, 25)
        Me.ToolStrip1.TabIndex = 2
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'ToolStripButton1
        '
        Me.ToolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton1.Image = CType(resources.GetObject("ToolStripButton1.Image"), System.Drawing.Image)
        Me.ToolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton1.Name = "ToolStripButton1"
        Me.ToolStripButton1.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton1.Text = "ToolStripButton1"
        '
        'ToolStripButton2
        '
        Me.ToolStripButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton2.Image = CType(resources.GetObject("ToolStripButton2.Image"), System.Drawing.Image)
        Me.ToolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton2.Name = "ToolStripButton2"
        Me.ToolStripButton2.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton2.Text = "ToolStripButton2"
        '
        'ToolStripButton3
        '
        Me.ToolStripButton3.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripButton3.Image = CType(resources.GetObject("ToolStripButton3.Image"), System.Drawing.Image)
        Me.ToolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.ToolStripButton3.Name = "ToolStripButton3"
        Me.ToolStripButton3.Size = New System.Drawing.Size(23, 22)
        Me.ToolStripButton3.Text = "ToolStripButton3"
        '
        'Dock
        '
        Me.Dock.ActiveAutoHideContent = Nothing
        Me.Dock.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Dock.Location = New System.Drawing.Point(0, 49)
        Me.Dock.Name = "Dock"
        Me.Dock.Size = New System.Drawing.Size(658, 360)
        Me.Dock.TabIndex = 4
        '
        'Bgw
        '
        Me.Bgw.WorkerReportsProgress = True
        Me.Bgw.WorkerSupportsCancellation = True
        '
        'MainWindow
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(658, 409)
        Me.Controls.Add(Me.Dock)
        Me.Controls.Add(Me.ToolStrip1)
        Me.Controls.Add(Me.MenuStrip1)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "MainWindow"
        Me.Text = "The Editor"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents FichierToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents EditionToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents AffichageToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents ToolStripButton1 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton2 As System.Windows.Forms.ToolStripButton
    Friend WithEvents ToolStripButton3 As System.Windows.Forms.ToolStripButton
    Friend WithEvents Dock As WeifenLuo.WinFormsUI.Docking.DockPanel
    Friend WithEvents ImportsToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ModelToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents TextureToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SoundToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Opd As System.Windows.Forms.OpenFileDialog
    Friend WithEvents Bgw As System.ComponentModel.BackgroundWorker

End Class
