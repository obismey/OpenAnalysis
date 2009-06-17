Public Class ThicknessEditor
    Implements Design.ITypeEditor



    'Private _value As Engine.Thickness
    'Public Property Value() As Engine.Thickness
    '    Get
    '        Return _value
    '    End Get
    '    Set(ByVal value As Engine.Thickness)
    '        _value = value
    '    End Set
    'End Property

    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        lastcolor = Me.BackColor
        ' Add any initialization after the InitializeComponent() call.
        AddHandler leftTxt.TextChanged, AddressOf ParamTextChanged
        AddHandler rightTxt.TextChanged, AddressOf ParamTextChanged
        AddHandler downTxt.TextChanged, AddressOf ParamTextChanged
        AddHandler topTxt.TextChanged, AddressOf ParamTextChanged

    End Sub
    Dim lastcolor As System.Drawing.Color
    Public Sub SetValue(ByVal value As Object) Implements Design.ITypeEditor.SetValue
        Dim th As Engine.Thickness = value
        leftTxt.Text = th.Left
        rightTxt.Text = th.Right
        topTxt.Text = th.Top
        downTxt.Text = th.Down
    End Sub
    Protected Overrides Sub OnMouseClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        BackColor = lastcolor
        RaiseEvent ValueEdited(Me, New Engine.Thickness() With { _
                           .Down = CInt(downTxt.Text), _
                           .Left = CInt(leftTxt.Text), _
                           .Right = CInt(rightTxt.Text), _
                           .Top = CInt(topTxt.Text)})
        MyBase.OnMouseClick(e)
    End Sub
    Public Event ValueEdited(ByVal sender As Object, ByVal value As Object) Implements Design.ITypeEditor.ValueEdited

    Private Sub ParamTextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Me.BackColor = System.Drawing.Color.Red
    End Sub
End Class
