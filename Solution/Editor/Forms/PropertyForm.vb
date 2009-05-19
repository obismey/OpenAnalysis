
Imports WeifenLuo.WinFormsUI.Docking
Public Class PropertyForm
    Inherits DockContent

    Sub New()
        InitializeComponent()
    End Sub

    Private Sub InitializeComponent()
        Me.PGrid = New System.Windows.Forms.PropertyGrid
        Me.SuspendLayout()
        '
        'PGrid
        '
        Me.PGrid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.PGrid.Location = New System.Drawing.Point(0, 0)
        Me.PGrid.Name = "PGrid"
        Me.PGrid.SelectedObject = Me
        Me.PGrid.Size = New System.Drawing.Size(292, 266)
        Me.PGrid.TabIndex = 0
        '
        'PropertyForm
        '
        Me.ClientSize = New System.Drawing.Size(292, 266)
        Me.Controls.Add(Me.PGrid)
        Me.DockAreas = CType(((WeifenLuo.WinFormsUI.Docking.DockAreas.Float Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) _
                    Or WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight), WeifenLuo.WinFormsUI.Docking.DockAreas)
        Me.Name = "PropertyForm"
        Me.TabText = "Properties"
        Me.Text = "Properties"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents PGrid As System.Windows.Forms.PropertyGrid

    Private Sub PGrid_PropertySortChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles PGrid.PropertySortChanged

    End Sub
    Private Sub PGrid_PropertyTabChanged(ByVal s As Object, ByVal e As System.Windows.Forms.PropertyTabChangedEventArgs) Handles PGrid.PropertyTabChanged

    End Sub
    Private Sub PGrid_PropertyValueChanged(ByVal s As Object, ByVal e As System.Windows.Forms.PropertyValueChangedEventArgs) Handles PGrid.PropertyValueChanged
        'If e.ChangedItem.Expandable Then
        '    If e.ChangedItem.Expanded Then
        '        Dim parent As GridItem = e.ChangedItem.Parent
        '        editedObject.setPropertyValue(parent.PropertyDescriptor.Name, parent.Value)
        '    Else
        '        editedObject.setPropertyValue(e.ChangedItem.PropertyDescriptor.Name, e.ChangedItem.Value)
        '    End If
        'Else
        '    editedObject.setPropertyValue(e.ChangedItem.PropertyDescriptor.Name, e.ChangedItem.Value)
        'End If


        'If e.ChangedItem.GridItemType = GridItemType.Property _
        '    And _
        '    e.ChangedItem.Parent.GridItemType = GridItemType.Property Then

        '    editedObject.setPropertyValue(e.ChangedItem.Parent.PropertyDescriptor.Name, e.ChangedItem.Parent.Value)
        'Else
        '    editedObject.setPropertyValue(e.ChangedItem.PropertyDescriptor.Name, e.ChangedItem.Value)
        'End If
    End Sub
    
    Public Function IsValid(ByVal propname As String) As Boolean

    End Function

    Private Sub PropertyForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
End Class
