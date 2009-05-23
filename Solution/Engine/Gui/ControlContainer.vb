Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports System.Collections.Generic
Imports System.Collections

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class ControlContainer
    Inherits Control

    Protected Friend _isRootContainer As Boolean = False
    Dim _children As New List(Of Control)

    Overridable Sub ReArrange()

    End Sub

    ReadOnly Property Children() As ObjectModel.ReadOnlyCollection(Of Control)
        Get
            Return _children.AsReadOnly
        End Get
    End Property

    Public Overridable Sub AddChild(ByVal child As Control)
        If Not _children.Contains(child) Then
            _children.Add(child)
            child.SetParent(Me)
        End If
    End Sub

    Public Overridable Sub RemoveChild(ByVal Child As Control)
        _children.Remove(Child)
    End Sub
    Public Overridable Sub RemoveChild(ByVal index As Integer)
        _children.RemoveAt(index)
    End Sub
   
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="vis"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Friend Overridable Function GetAllowedDrawRect(ByVal vis As Control) As Rectangle
        Return DimensionRect
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="vis"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Protected Friend Overridable Function GetAllowedClipRect(ByVal vis As Control) As Rectangle
        Return DrawRect
    End Function

    Protected Friend Overrides Sub Draw(ByVal elapsedTime As Double, ByVal totalTime As Double)
        If _isRootContainer Then
            Dim dr As SpriteBatch = Root.Instance.Drawer

            Dim rect As Rectangle = ComputeRect(Root.Instance.ViewportRect)

            DefaultBrush.Draw(rect, dr)
        Else
            MyBase.Draw(elapsedTime, totalTime)
        End If

        For Each child In Children
            child.Draw(elapsedTime, totalTime)
        Next
    End Sub

    Protected Friend Overrides Sub Update(ByVal elapsedTime As Double, ByVal totalTime As Double)
        If _isRootContainer Then
            _drawrect = ComputeRect(Root.Instance.ViewportRect)
            _dimensionRect = ComputeRect(Root.Instance.ViewportRect)
        Else
            MyBase.Update(elapsedTime, totalTime)
        End If

        For Each child In Children
            child.Update(elapsedTime, totalTime)
        Next
    End Sub

    Protected Friend Overrides Sub OnMouseEvent(ByVal mCurrent As Microsoft.Xna.Framework.Input.MouseState, ByVal mOld As Microsoft.Xna.Framework.Input.MouseState, ByVal timeElapsed As System.TimeSpan)
        MyBase.OnMouseEvent(mCurrent, mOld, timeElapsed)
        For Each child In Children
            child.OnMouseEvent(mCurrent, mOld, timeElapsed)
        Next
    End Sub

End Class
