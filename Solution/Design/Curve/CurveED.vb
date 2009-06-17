Imports System.Windows.Forms
Imports System.Drawing

Public Class CurveED
    Sub New()

        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        DoubleBuffered = True
    End Sub
    Private _mode As EditMode = EditMode.Add
    Property Mode() As EditMode
        Get
            Return _mode
        End Get
        Set(ByVal value As EditMode)
            _mode = value
        End Set
    End Property


    Private _drawAsContinuous As Boolean = True
    Public Property DrawAsContinuous() As Boolean
        Get
            Return _drawAsContinuous
        End Get
        Set(ByVal value As Boolean)
            _drawAsContinuous = value
        End Set
    End Property


    Public currentPoints As New List(Of PointF)
    Public drawedLines As New List(Of String)
    Private draggedPoint As PointF? = Nothing
    Private draggedIndex As Integer
    Private deletePos As PointF
    Protected Overrides Sub OnMouseClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Select Case Mode
                Case EditMode.Add
                    currentPoints.Add(e.Location)
                    If currentPoints.Count > 2 Then
                        currentPoints.Sort(AddressOf ComparePoint)
                    End If



            End Select
            Invalidate()

        End If

        MyBase.OnMouseClick(e)
    End Sub

    Protected Overrides Sub OnMouseDown(ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Middle Then
            If GetPointAt(e.Location) IsNot Nothing Then
                draggedPoint = GetPointAt(e.Location)
                draggedIndex = currentPoints.IndexOf(draggedPoint.Value)
            End If
            Invalidate()
        ElseIf e.Button = Windows.Forms.MouseButtons.Right Then
            deletePos = e.Location
        End If
        MyBase.OnMouseDown(e)
    End Sub

    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Middle Then
            If draggedPoint IsNot Nothing Then
                currentPoints(draggedIndex) = e.Location
            End If
            Invalidate()
        End If

        Label1.Text = "Position : " & e.Location.ToString()
        MyBase.OnMouseMove(e)
    End Sub
    Protected Overrides Sub OnMouseUp(ByVal e As System.Windows.Forms.MouseEventArgs)
        If e.Button = Windows.Forms.MouseButtons.Middle Then
            draggedPoint = Nothing
            Invalidate()
        End If
        MyBase.OnMouseUp(e)
    End Sub
    Protected Overrides Sub OnPaint(ByVal e As System.Windows.Forms.PaintEventArgs)
        DrawCadrillage(e.Graphics, _horizontalLineIntervalle, _VerticalLineIntervalle)

        For Each s In drawedLines
            If Not String.IsNullOrEmpty(s) Then
                Dim _drawPoints As IEnumerable(Of PointF) = editedLines(s)
                If _drawAsContinuous Then
                    If _drawPoints.Count > 3 Then
                        e.Graphics.DrawCurve(Pens.Black, _drawPoints.ToArray)
                        For Each p In _drawPoints
                            e.Graphics.FillEllipse(Brushes.Red, p.X - 5, p.Y - 5, 10, 10)
                        Next
                    End If
                Else
                    If currentPoints.Count > 2 Then
                        e.Graphics.DrawLines(Pens.Black, _drawPoints.ToArray())
                        For Each p In _drawPoints
                            e.Graphics.FillEllipse(Brushes.Red, p.X - 5, p.Y - 5, 10, 10)
                        Next
                    End If
                End If

            End If
        Next

        MyBase.OnPaint(e)
    End Sub

    Private Function ComparePoint(ByVal x As PointF, ByVal y As PointF) As Integer
        If x.X = y.X Then Return 0
        If x.X < y.X Then Return -1
        If x.X > y.X Then Return 1
    End Function
    Private Function GetPointAt(ByVal pos As PointF) As PointF?
        Dim q = From p In currentPoints _
              Where ((pos.X - p.X) ^ 2 + (pos.Y - p.Y) ^ 2) < 100 _
              Select p

        Return q.FirstOrDefault
    End Function

    Sub AddLines(ByVal parent As String, ByVal name As String, _
                   ByVal minX As Single, _
                   ByVal maxX As Single, _
                   ByVal minY As Single, _
                   ByVal maxY As Single)


        editedLines.Add(name, New List(Of PointF))
        editedLinesDimension.Add(name, New RectangleF(minX, minY, maxX, maxY))

        If String.IsNullOrEmpty(parent) Then
            Dim tnode As TreeNode = TreeView1.Nodes(0).Nodes.Add(name, name)
        Else
            Dim groupNode As TreeNode = FindNode(parent)
            If groupNode Is Nothing Then
                groupNode = TreeView1.Nodes(0).Nodes.Add(parent, parent)
                Dim tnode As TreeNode = groupNode.Nodes.Add(name, name)
            Else
                Dim tnode As TreeNode = groupNode.Nodes.Add(name, name)
            End If
        End If
    End Sub

    Private Function FindNode(ByVal key As String, ByVal parentNode As TreeNode) As TreeNode
        If parentNode.Nodes.Count > 0 Then
            If parentNode.Nodes.ContainsKey(key) Then
                Return parentNode.Nodes(key)
            Else
                Dim tmpNode As TreeNode = Nothing
                For Each node In parentNode.Nodes
                    tmpNode = FindNode(key, node)
                    If tmpNode IsNot Nothing Then Exit For
                Next
                Return tmpNode
            End If
        Else
            Return Nothing
        End If
    End Function
    Private Function FindNode(ByVal key As String) As TreeNode
        Return FindNode(key, TreeView1.Nodes(0))
    End Function


    Sub AddLines(ByVal name As String)

    End Sub
    Sub DrawCadrillage(ByVal g As Graphics, ByVal horizontal As Integer, ByVal vertical As Integer)
        Dim pp As New Pen(GridLinesColor, 1)
        Dim hc As Integer = Height \ horizontal
        Dim vc As Integer = Width \ vertical

        For i As Integer = 0 To hc + 1
            g.DrawLine(pp, 0, i * horizontal, Width, i * horizontal)
        Next
        For j As Integer = 0 To vc + 1
            g.DrawLine(pp, j * vertical, 0, j * vertical, Height)
        Next
    End Sub
    Private _horizontalLineIntervalle As Integer = 10
    Public Property HorizontalLineIntervalle() As Integer
        Get
            Return _horizontalLineIntervalle
        End Get
        Set(ByVal value As Integer)
            If value < 2 Then Return
            _horizontalLineIntervalle = value
            Invalidate()
        End Set
    End Property

    Private _VerticalLineIntervalle As Integer = 10
    Public Property VerticalLineIntervalle() As Integer
        Get
            Return _VerticalLineIntervalle
        End Get
        Set(ByVal value As Integer)
            If value < 2 Then Return
            _VerticalLineIntervalle = value
            Invalidate()
        End Set
    End Property


    Private editedLines As New SortedList(Of String, List(Of PointF))
    Private editedLinesDimension As New SortedList(Of String, RectangleF)


    Private _gridLinesColor As Color = Color.Yellow
    Public Property GridLinesColor() As Color
        Get
            Return _gridLinesColor
        End Get
        Set(ByVal value As Color)
            _gridLinesColor = value
        End Set
    End Property



    Private Sub DeleteToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles DeleteToolStripMenuItem.Click
        If GetPointAt(deletePos) IsNot Nothing Then
            deletePos = GetPointAt(deletePos)
            currentPoints.Remove(deletePos)
            Invalidate()
        End If
    End Sub

    Private Sub AddLineToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AddLineToolStripMenuItem.Click
        Dim parent As String = InputBox("")
        Dim name As String = InputBox("")
        AddLines(parent, name, -1, 1, -1, 1)
    End Sub

    Private Sub TreeView1_AfterCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles TreeView1.AfterCheck

        If Not e.Node.Checked And _
           drawedLines.Contains(e.Node.Text) And _
           editedLines.ContainsKey(e.Node.Text) Then

            drawedLines.Remove(e.Node.Text)
            Invalidate()
            Return
        End If

        If e.Node.Checked And _
           Not drawedLines.Contains(e.Node.Text) And _
           editedLines.ContainsKey(e.Node.Text) Then

            drawedLines.Add(e.Node.Text)
            Invalidate()
            Return
        End If
    End Sub



    Private Sub TreeView1_NodeMouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.TreeNodeMouseClickEventArgs) Handles TreeView1.NodeMouseDoubleClick
        TreeView1.SelectedNode = e.Node

        If editedLines.ContainsKey(e.Node.Text) Then
            e.Node.Checked = True
            currentPoints = editedLines(e.Node.Text)
            Invalidate()
        End If
    End Sub


End Class

Public Enum EditMode
    Delete
    Move
    Add
End Enum