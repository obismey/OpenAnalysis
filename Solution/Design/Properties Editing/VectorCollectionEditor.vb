Public Class VectorCollectionEditor
    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        AddHandler TextBox1.KeyDown, AddressOf TextBox1KeyDown
        AddHandler TextBox2.KeyDown, AddressOf TextBox1KeyDown
        AddHandler TextBox3.KeyDown, AddressOf TextBox1KeyDown
        AddHandler TextBox4.KeyDown, AddressOf TextBox1KeyDown
    End Sub

    Private Sub SlideBtLocationChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim slide As SlideBt = CType(sender, SlideBt)
        Dim index As Integer = Panel1.Controls.IndexOf(slide)
        Label5.Text = "Key " & index & " at " & CInt(1000 * (slide.Left / Panel1.Width))
    End Sub
    Private Sub SlideBtMouseEnter(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim slide As SlideBt = CType(sender, SlideBt)
        Dim index As Integer = Panel1.Controls.IndexOf(slide)
        Label5.Text = "Key " & index & " at " & CInt(1000 * (slide.Left / Panel1.Width))


    End Sub

    Private current As SlideBt = Nothing
    Private Sub SlideBtGotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim slide As SlideBt = CType(sender, SlideBt)
        Dim data() As Single = slide.Tag
        TextBox1.Text = data(0)
        TextBox2.Text = data(1)
        TextBox3.Text = data(2)
        TextBox4.Text = data(3)
        current = slide
    End Sub

    Private Sub Panel1_ControlRemoved(ByVal sender As Object, ByVal e As System.Windows.Forms.ControlEventArgs) Handles Panel1.ControlRemoved
        If e.Control Is current Then
            current = Nothing
        End If
    End Sub

    Private Sub Panel1_MouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Panel1.MouseClick
        Dim slide As New SlideBt() With {.Left = e.X - 7.5, .Top = 1}
        slide.Tag = New Single() {0.5F, 0.6F, 0.11F, 0.17F}
        AddHandler slide.LocationChanged, AddressOf SlideBtLocationChanged
        AddHandler slide.MouseEnter, AddressOf SlideBtMouseEnter
        AddHandler slide.GotFocus, AddressOf SlideBtGotFocus
        Panel1.Controls.Add(slide)
    End Sub

    Private Class SlideBt
        Inherits Windows.Forms.Button

        Sub New()
            FlatStyle = Windows.Forms.FlatStyle.Flat
            Width = 15
            Height = 15
            BackColor = Drawing.Color.Yellow
        End Sub

        Private curX As Integer
        Protected Overrides Sub OnMouseDown(ByVal mevent As System.Windows.Forms.MouseEventArgs)
            If mevent.Button = Windows.Forms.MouseButtons.Left Then
                curX = mevent.X

            End If
            MyBase.OnMouseDown(mevent)
        End Sub

        Protected Overrides Sub OnMouseMove(ByVal mevent As System.Windows.Forms.MouseEventArgs)
            If mevent.Button = Windows.Forms.MouseButtons.Left Then
                Left += (mevent.X - curX)
            End If
            MyBase.OnMouseMove(mevent)
        End Sub

        Protected Overrides Sub OnMouseDoubleClick(ByVal e As System.Windows.Forms.MouseEventArgs)
            Focus()
            MyBase.OnMouseDoubleClick(e)
        End Sub

        Protected Overrides Sub OnMouseClick(ByVal e As System.Windows.Forms.MouseEventArgs)
            If e.Button = Windows.Forms.MouseButtons.Middle Then
                Parent.Controls.Remove(Me)
            End If
            MyBase.OnMouseClick(e)
        End Sub

        Protected Overrides Sub OnKeyDown(ByVal kevent As System.Windows.Forms.KeyEventArgs)
            If kevent.KeyCode = Windows.Forms.Keys.Delete Then
                Parent.Controls.Remove(Me)
            End If
            MyBase.OnKeyDown(kevent)
        End Sub
    End Class


    Function GetValues(ByVal dimension As Integer) As IEnumerable(Of Single)
        Dim result As New List(Of Single)
        For Each c As SlideBt In Panel1.Controls
            Dim data() As Single = c.Tag
            For i As Integer = 0 To dimension - 1
                result.Add(data(i))
            Next
        Next

        Return result
    End Function

    Function GetTimes() As IEnumerable(Of Single)
        Dim result As New List(Of Single)
        For Each c As SlideBt In Panel1.Controls
            result.Add(c.Left / Panel1.Width)
        Next
        Return result
    End Function

   
    Private Sub TextBox1KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Windows.Forms.Keys.Enter Then
            If current IsNot Nothing Then

                current.Tag = New Single() {CSng(TextBox1.Text), _
                                       CSng(TextBox2.Text), _
                                       CSng(TextBox3.Text), _
                                       CSng(TextBox4.Text)}

            End If

        End If

    End Sub
End Class
