Public Class NullableBooleanEditor
    Inherits BooleanEditor


    Public Overrides Sub SetValue(ByVal value As Object)
        'If TypeOf value Is NullEnum Then
        '    Dim v As NullEnum = value
        '    If v = NullEnum.Null Then
        '        Enabled = False

        '    End If
        'End If
        If value Is Nothing Then
            Enabled = False
            Return
        Else
            Dim v As Boolean? = value
            Enabled = True
            MyBase.SetValue(v.Value)
            Return
        End If

    End Sub

End Class
Public Class NullableNumericEditor
    Inherits NumericEditor

    Sub New(ByVal type As Type)
        MyBase.New(type)
    End Sub

   
    Public Overrides Sub SetValue(ByVal value As Object)
        'If TypeOf value Is NullEnum Then
        '    Dim v As NullEnum = value
        '    If v = NullEnum.Null Then
        '        Enabled = False

        '    End If
        'End If
        If value Is Nothing Then
            Enabled = False
            Return
        Else
            Dim v As Boolean? = value
            Enabled = True
            MyBase.SetValue(v.Value)
            Return
        End If

    End Sub

End Class

Public Class NullableEnumEditor
    Inherits EnumEditor


    Public Overrides Sub SetValue(ByVal value As Object)
        'If TypeOf value Is NullEnum Then
        '    Dim v As NullEnum = value
        '    If v = NullEnum.Null Then
        '        Enabled = False

        '    End If
        'End If
        If value Is Nothing Then
            SelectedIndex = 0

        Else
            Enabled = True
            If _type Is Nothing Then _type = value.GetType()
            SelectedItem = [Enum].GetName(_type, value)

        End If

    End Sub

End Class





Friend Class NullContextMenu
    Inherits System.Windows.Forms.ContextMenuStrip

    Private WithEvents UnsetToNullToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private WithEvents SetToNullToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Private te As ITypeEditor
    Sub New(ByVal typeeditor As ITypeEditor)
        Me.SetToNullToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.UnsetToNullToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.SetToNullToolStripMenuItem.Text = "Set To Null"
        Me.UnsetToNullToolStripMenuItem.Text = "Unset To Null"
        te = typeeditor
    End Sub

    Private Sub SetToNullToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles SetToNullToolStripMenuItem.Click
        te.SetValue(NullEnum.Null)
    End Sub
    Private Sub UnsetToNullToolStripMenuItem_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles UnsetToNullToolStripMenuItem.Click
        te.SetValue(NullEnum.NotNull)
    End Sub
End Class
Friend Enum NullEnum
    [Null]
    [NotNull]
End Enum