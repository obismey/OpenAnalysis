Imports System.Windows.Forms

Public Class NumericEditor
    Inherits NumericUpDown
    Implements Design.ITypeEditor

    Protected Overrides Sub OnValueChanged(ByVal e As System.EventArgs)


        MyBase.OnValueChanged(e)
    End Sub


    Private Class ParameterForm
        Inherits Form

        Private _ie As NumericEditor
        Private txtMinimum As TextBox
        Private txtMaximum As TextBox
        Private txtInc As TextBox

        Sub New(ByVal ie As NumericEditor)
            Dim lblMinimum As New Label With {.Text = "Minimum", .Top = 5, .AutoSize = True}
            Dim lblMaximum As New Label With {.Text = "Maximum", .Top = lblMinimum.Bottom + 5, .AutoSize = True}
            Dim lblInc As New Label With {.Text = "Increment", .Top = lblMaximum.Bottom + 5, .AutoSize = True}

            txtMinimum = New TextBox With {.Text = ie.Minimum, .Top = 5, .Width = 50, .Left = lblMinimum.Right + 5}
            txtMaximum = New TextBox With {.Text = ie.Maximum, .Top = lblMinimum.Bottom + 5, .Width = 50, .Left = lblMaximum.Right + 5}
            txtInc = New TextBox With {.Text = ie.Increment, .Top = lblMaximum.Bottom + 5, .Width = 50, .Left = lblInc.Right + 5}


            Me.Controls.Add(lblMinimum)
            Me.Controls.Add(lblMaximum)
            Me.Controls.Add(lblInc)
            Me.Controls.Add(txtMinimum)
            Me.Controls.Add(txtMaximum)
            Me.Controls.Add(txtInc)


            Me.Width = txtInc.Right + 35
            Me.Height = txtInc.Bottom + 35
            Me.FormBorderStyle = Windows.Forms.FormBorderStyle.FixedToolWindow

            _ie = ie
        End Sub

        Protected Overrides Sub OnClosing(ByVal e As System.ComponentModel.CancelEventArgs)
            _ie.Minimum = txtMinimum.Text
            _ie.Maximum = txtMaximum.Text
            _ie.Increment = txtInc.Text
            MyBase.OnClosing(e)
        End Sub
    End Class

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Middle Then
            Dim param As New ParameterForm(Me)
            param.ShowDialog()

        End If
        MyBase.OnMouseDown(e)
    End Sub
    Protected Overrides Sub OnMouseWheel(ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim tmp As Decimal = Value + Math.Sign(e.Delta)
        Value = Math.Max(Math.Min(tmp, Maximum), Minimum)
        MyBase.OnMouseWheel(e)
    End Sub

    Public Overridable Sub SetValue(ByVal value As Object) Implements Design.ITypeEditor.SetValue
        If CDec(value) > Maximum Then
            Maximum = CDec(value)
            Me.Value = value
        ElseIf CDec(value) < Minimum Then
            Minimum = CDec(value)
            Me.Value = value
        Else
            Me.Value = value
        End If
    End Sub

    Public Event ValueEdited(ByVal sender As Object, ByVal value As Object) Implements Design.ITypeEditor.ValueEdited

    Private Sub IntegerEditor_ValueChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.ValueChanged
        If _type Is GetType(Integer) Then
            RaiseEvent ValueEdited(Me, CInt(Value))
        End If
        If _type Is GetType(Long) Then
            RaiseEvent ValueEdited(Me, CLng(Value))
        End If
        If _type Is GetType(Single) Then
            RaiseEvent ValueEdited(Me, CSng(Value))
        End If
        If _type Is GetType(Double) Then
            RaiseEvent ValueEdited(Me, CLng(Value))
        End If
        If _type Is GetType(Byte) Then
            RaiseEvent ValueEdited(Me, CByte(Value))
        End If
    End Sub

    Private _type As Type
    Sub New(ByVal type As Type)
        _type = type
    End Sub
    Private Sub InitializeComponent()
        CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'IntegerEditor
        '
        Me.DecimalPlaces = 3
        Me.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
End Class
