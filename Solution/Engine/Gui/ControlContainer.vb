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

    ReadOnly Property Children() As ObjectModel.ReadOnlyCollection(Of Control)
        Get
            Return _children.AsReadOnly
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Overridable Sub ReArrange()

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="child"></param>
    ''' <remarks></remarks>
    Public Overridable Sub AddChild(ByVal child As Control)
        If Not _children.Contains(child) Then
            _children.Add(child)
            child.SetParent(Me)
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="Child"></param>
    ''' <remarks></remarks>
    Public Overridable Sub RemoveChild(ByVal Child As Control)
        _children.Remove(Child)
    End Sub
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="index"></param>
    ''' <remarks></remarks>
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

    Protected Friend Overrides Sub Draw(ByVal context As DrawContext, ByVal timeElapsed As Double?, ByVal totalTimeElapsed As Double?)
        If _isRootContainer Then
            Dim dr As SpriteBatch = Root.Instance.Drawer

            Dim rect As Rectangle = ComputeRect(Root.Instance.ViewportRect)

            DefaultBrush.Draw(rect, dr)
        Else
            MyBase.Draw(context, timeElapsed, totalTimeElapsed)
        End If

        For Each child In Children
            If child.Visiblility = Visibility.Visible Then
                child.Draw(context, timeElapsed, totalTimeElapsed)
            End If
        Next
    End Sub

    Protected Friend Overrides Sub Update(ByVal timeElapsed As Double?, ByVal totalTimeElapsed As Double?)
        If _isRootContainer Then
            _drawrect = ComputeRect(Root.Instance.ViewportRect)
            _dimensionRect = ComputeRect(Root.Instance.ViewportRect)
        Else
            MyBase.Update(timeElapsed, totalTimeElapsed)
        End If

        For Each child In Children
            If child.Visiblility <> Visibility.Collapsed Then
                child.Update(timeElapsed, totalTimeElapsed)
            End If
        Next
    End Sub

    Protected Friend Overrides Sub OnKeyboardEvent(ByVal kCurrent As Microsoft.Xna.Framework.Input.KeyboardState, ByVal kOld As Microsoft.Xna.Framework.Input.KeyboardState, ByVal timeElapsed As System.TimeSpan)
        MyBase.OnKeyboardEvent(kCurrent, kOld, timeElapsed)
    End Sub

    Protected Friend Overrides Sub OnMouseEvent(ByVal mCurrent As Microsoft.Xna.Framework.Input.MouseState, ByVal mOld As Microsoft.Xna.Framework.Input.MouseState, ByVal timeElapsed As System.TimeSpan)
        MyBase.OnMouseEvent(mCurrent, mOld, timeElapsed)
        For Each child In Children
            child.OnMouseEvent(mCurrent, mOld, timeElapsed)
        Next
    End Sub
End Class
