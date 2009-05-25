Imports Microsoft.Xna.Framework
Imports System.Collections.Generic

Public Class GridContainer
    Inherits ControlContainer

    Dim datas As New Dictionary(Of Control, ControlData)
    Dim cdef As New List(Of GridDefinition)
    Dim rdef As New List(Of GridDefinition)

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="child"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Column(ByVal child As Control) As Integer
        Get
            Return datas(child).Column
        End Get
        Set(ByVal value As Integer)
            datas(child).Column = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="child"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Row(ByVal child As Control) As Integer
        Get
            Return datas(child).Row
        End Get
        Set(ByVal value As Integer)
            datas(child).Row = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="child"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property ColumnSpan(ByVal child As Control) As Integer
        Get
            Return datas(child).ColumnSpan
        End Get
        Set(ByVal value As Integer)
            datas(child).ColumnSpan = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="child"></param>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property RowSpan(ByVal child As Control) As Integer
        Get
            Return datas(child).RowSpan
        End Get
        Set(ByVal value As Integer)
            datas(child).RowSpan = value
        End Set
    End Property

    ReadOnly Property Columns() As IList(Of GridDefinition)
        Get
            Return cdef
        End Get
    End Property
    ReadOnly Property Rows() As IList(Of GridDefinition)
        Get
            Return rdef
        End Get
    End Property

    Private Class ControlData
        Public Row As Integer
        Public Column As Integer
        Public RowSpan As Integer
        Public ColumnSpan As Integer

        Sub New()
            Row = 0
            Column = 0
            RowSpan = 1
            ColumnSpan = 1
        End Sub


    End Class

    Public Overrides Sub AddChild(ByVal child As Control)
        MyBase.AddChild(child)
        datas.Add(child, New ControlData())
    End Sub

    Public Overrides Sub ReArrange()

    End Sub

    Protected Friend Overrides Function GetAllowedDrawRect(ByVal vis As Control) As Microsoft.Xna.Framework.Rectangle
        Dim crow As Integer = Math.Max(0, Row(vis))
        Dim ccol As Integer = Math.Max(0, Column(vis))

        Dim crows As Integer = Math.Max(1, Math.Min(Rows.Count + 1, RowSpan(vis)))
        Dim ccols As Integer = Math.Max(1, Math.Min(Columns.Count + 1, ColumnSpan(vis)))

        Dim rect As Rectangle

        If Rows.Count = 0 Then
            rect.Y = DimensionRect.Y
            rect.Height = DimensionRect.Height
        Else
            rect.Y = DimensionRect.Y + GetPos(False, crow)

            If (crow + crows) = (Rows.Count + 1) Then
                ''''''''
                rect.Height = DimensionRect.Height - GetPos(False, crow)
            Else
                rect.Height = GetPos(False, crow + crows) - GetPos(False, crow)
            End If
        End If


        If Columns.Count = 0 Then
            rect.X = DimensionRect.X
            rect.Width = DimensionRect.Width
        Else
            rect.X = DimensionRect.X + GetPos(True, ccol)

            If (ccol + ccols) = (Columns.Count + 1) Then
                ''''''''''''''
                rect.Width = DimensionRect.Width - GetPos(True, ccol)
            Else
                rect.Width = GetPos(True, ccol + ccols) - GetPos(True, ccol)
            End If
        End If

        Return rect
    End Function

    Private Function GetPos(ByVal colonne As Boolean, ByVal ID As Integer) As Single
        Dim total As Single = 0.0F
        If colonne Then
            For i = 0 To ID - 1
                If cdef(i).Absolute Then
                    total += cdef(i).Size
                Else
                    total += cdef(i).Size * DimensionRect.Width
                End If
            Next
        Else
            For i = 0 To ID - 1
                If rdef(i).Absolute Then
                    total += rdef(i).Size
                Else
                    total += rdef(i).Size * DimensionRect.Height
                End If
            Next
        End If

        Return total
    End Function
End Class

''' <summary>
''' 
''' </summary>
''' <remarks>Addiftif</remarks>
Public Structure GridDefinition
    Public Size As Single
    Public Absolute As Boolean

    Sub New(ByVal s As Single, ByVal a As Boolean)
        Size = s
        Absolute = a
    End Sub

    Shared Widening Operator CType(ByVal src As String) As GridDefinition
        Dim tmp As String = src.Trim
        If src.EndsWith("a") Then
            Return New GridDefinition( _
                      Single.Parse(tmp.Substring(0, src.Length - 1), System.Globalization.CultureInfo.InvariantCulture), True)
        Else
            Return New GridDefinition( _
              Single.Parse(tmp, System.Globalization.CultureInfo.InvariantCulture), False)
        End If
    End Operator
End Structure

