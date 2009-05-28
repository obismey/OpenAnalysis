Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Graphics
Imports System.ComponentModel
Imports Core

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class Control
    Implements Core.IUpdatable
    Implements IDrawable

    Protected _drawrect As Rectangle
    
    Sub New()
        _hal = Alignment.Strech
        _val = Alignment.Strech
        _margin = New Thickness(0)
    End Sub

#Region "Infrastructure"

    Protected Friend Overridable Sub Update(ByVal timeElapsed As Double?, ByVal totalTimeElapsed As Double?) Implements Core.IUpdatable.Update
        _dimensionRect = ComputeRect(_container.GetAllowedDrawRect(Me))
        _drawrect = Rectangle.Intersect(_dimensionRect, _container.GetAllowedClipRect(Me))

    End Sub
    Protected Friend Overridable Sub Draw(ByVal context As DrawContext, ByVal timeElapsed As Double?, ByVal totalTimeElapsed As Double?) Implements IDrawable.Draw
        Dim dr As SpriteBatch = Root.Instance.Drawer
        DefaultBrush.Draw(DrawRect, dr)
    End Sub

#End Region

#Region "Positionnement and render transform"
    Dim _hal, _val As Alignment
    Protected _dimensionRect As Rectangle
    Private _Width As Single
    Public WidthSource As IValueSource(Of Single)
    Public HeightSource As IValueSource(Of Single)
    Private _Height As Single
    Public MarginSource As IValueSource(Of Thickness)
    Private _Margin As Thickness

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Width() As Single
        Get
            Return If(WidthSource Is Nothing, _Width, WidthSource.GetValue(Me, "Width"))
        End Get
        Set(ByVal value As Single)
            _Width = value
            WidthSource = Nothing
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Height() As Single
        Get
            Return If(HeightSource Is Nothing, _Height, HeightSource.GetValue(Me, "Height"))
        End Get
        Set(ByVal value As Single)
            _Height = value
            HeightSource = Nothing
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property HAlign() As Alignment
        Get
            Return _hal
        End Get
        Set(ByVal value As Alignment)
            _hal = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property VAlign() As Alignment
        Get
            Return _val
        End Get
        Set(ByVal value As Alignment)
            _val = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property Margin() As Thickness
        Get
            Return If(MarginSource Is Nothing, _Margin, MarginSource.GetValue(Me, "Margin"))
        End Get
        Set(ByVal value As Thickness)
            _Margin = value
            MarginSource = Nothing
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property DimensionRect() As Rectangle
        Get
            Return _dimensionRect
        End Get
    End Property

    Protected Function ComputeRect(ByVal parentRect As Rectangle) As Rectangle
        Dim res() As Single = ComputeRect(parentRect.Width, parentRect.Height)
        res(0) += parentRect.Left
        res(1) += parentRect.Top

        Return New Rectangle(CInt(res(0)), CInt(res(1)), CInt(res(2)), CInt(res(3)))
    End Function
    Private Function ComputeRect(ByVal parentWidth As Single, ByVal parentHeight As Single) As Single()
        Dim m0 As Thickness = Margin
        Dim m As Single() = New Single() {m0.Left, m0.Top, m0.Right, m0.Down}


        Dim mw As Single = Width
        Dim mh As Single = Height

        Dim tlx As Single
        Dim tly As Single
        Dim wth As Single
        Dim hth As Single

        Select Case HAlign
            Case Alignment.Strech
                tlx = m(0)
                wth = parentWidth - (m(0) + m(2))

            Case Alignment.Center
                tlx = parentWidth / 2 + (m(0) - m(2)) - mw / 2
                wth = mw

            Case Alignment.Left
                tlx = m(0)
                wth = mw

            Case Alignment.Rigth
                tlx = parentWidth - (m(2) + mw)
                wth = mw

            Case Else
                '          Throw New ArgumentException("Non Valid Value for horizontal Alignment")
        End Select


        Select Case VAlign
            Case Alignment.Strech
                tly = m(1)
                hth = parentHeight - (m(1) + m(3))

            Case Alignment.Center
                tly = parentHeight / 2 + (m(1) - m(3)) - mh / 2
                hth = mh

            Case Alignment.Top
                tly = m(1)
                hth = mh

            Case Alignment.Bottom
                tly = parentHeight - (m(3) + mh)
                hth = mh

            Case Else
                '   Throw New ArgumentException("Non Valid Value for vertical Alignment")
        End Select


        Return New Single() {tlx, tly, wth, hth}
    End Function

#End Region

#Region "Public field "
    Dim _name As String = ""
    Dim _text As String = ""
    Dim _visibility As Visibility = Visibility.Visible
    Dim _color As Brush = Color.AliceBlue
    Dim _enabled As Boolean
    Dim _focusable As Boolean
    Dim _zindex As Integer
    Private _boderBrush As Brush = Color.Red

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Text() As String
        Get
            Return _text
        End Get
        Set(ByVal value As String)
            _text = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property DefaultBrush() As Brush
        Get
            Return _color
        End Get
        Set(ByVal value As Brush)
            _color = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Property BorderBrush() As Brush
        Get
            Return _boderBrush
        End Get
        Set(ByVal value As Brush)
            _boderBrush = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Visiblility() As Visibility
        Get
            Return _visibility
        End Get
        Set(ByVal value As Visibility)
            _visibility = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Enabled() As Boolean
        Get
            Return _enabled
        End Get
        Set(ByVal value As Boolean)
            _enabled = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Focusable() As Boolean
        Get
            Return _focusable
        End Get
        Set(ByVal value As Boolean)
            _focusable = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Zindex() As Integer
        Get
            Return _zindex
        End Get
        Set(ByVal value As Integer)
            _zindex = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="space"></param>
    ''' <param name="sourceIsrelative"></param>
    ''' <param name="resultIsRelative"></param>
    ''' <param name="point"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetPosition(ByVal space As Control, ByVal sourceIsrelative As Boolean, ByVal resultIsRelative As Boolean, ByVal point As Vector2) As Vector2

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property DrawRect() As Rectangle
        Get
            Return _drawrect
        End Get
    End Property

#End Region

#Region "Keyboard Event"
    Private _focused As Boolean

    Function SetFocus() As Boolean
        If _focusable Then
            If Not _focused Then
                _focused = True
                Root.Instance.NotifyFocusChange(Me)
            End If
        End If
    End Function
    Friend Sub OnFocusLost(ByVal source As Control)

    End Sub
    Protected Friend Overridable Sub OnKeyboardEvent(ByVal kCurrent As KeyboardState, ByVal kOld As KeyboardState, ByVal timeElapsed As TimeSpan)

    End Sub
    Event KeyPress As KeyboardEventHandler
    Event KeyDown As KeyboardEventHandler
    Event KeyUp As KeyboardEventHandler
#End Region

#Region "Mouse Event"
    Dim _mouseover As Boolean
    ReadOnly Property IsMouseOver() As Boolean
        Get
            Return _mouseover
        End Get
    End Property


    Protected Friend Overridable Sub OnMouseEvent(ByVal mCurrent As MouseState, ByVal mOld As MouseState, ByVal timeElapsed As TimeSpan)
        Dim bt As MouseButton = MouseButton.None
        If mCurrent.LeftButton = ButtonState.Pressed Then
            bt = MouseButton.Left
        Else
            If mCurrent.RightButton = ButtonState.Pressed Then
                bt = MouseButton.Rigth
            Else
                If mCurrent.MiddleButton = ButtonState.Pressed Then
                    bt = MouseButton.Middle
                Else
                    If mCurrent.XButton1 = ButtonState.Pressed Then
                        bt = MouseButton.X1
                    Else
                        If mCurrent.XButton2 = ButtonState.Pressed Then
                            bt = MouseButton.X2
                        End If
                    End If
                End If
            End If
        End If

        Dim bt2 As MouseButton = MouseButton.None
        If mOld.LeftButton = ButtonState.Pressed Then
            bt2 = MouseButton.Left
        Else
            If mOld.RightButton = ButtonState.Pressed Then
                bt2 = MouseButton.Rigth
            Else
                If mOld.MiddleButton = ButtonState.Pressed Then
                    bt2 = MouseButton.Middle
                Else
                    If mOld.XButton1 = ButtonState.Pressed Then
                        bt2 = MouseButton.X1
                    Else
                        If mOld.XButton2 = ButtonState.Pressed Then
                            bt2 = MouseButton.X2
                        End If
                    End If
                End If
            End If
        End If

        Dim sv As Integer = mCurrent.ScrollWheelValue
        Dim sd As Integer = mCurrent.ScrollWheelValue - mOld.ScrollWheelValue

        Dim arg As New MouseEventArgs(bt, sv, sd)
        '  Debug.Print(bt.ToString & " " & bt2.ToString)
        If DrawRect.Contains(mCurrent.X, mCurrent.Y) Then
            _mouseover = True
            If Not DrawRect.Contains(mOld.X, mOld.Y) Then
                RaiseEvent MouseEnter(Me, arg)
                Return
            End If

            If sd <> 0 Then
                RaiseEvent MouseScroll(Me, arg)
                Return
            End If

            If bt = bt2 Then
                If bt <> MouseButton.None Then
                    RaiseEvent MouseDown(Me, arg)
                    Return
                Else
                    RaiseEvent MouseMove(Me, arg)
                    Return
                End If
            Else
                If bt <> MouseButton.None And bt2 = MouseButton.None Then
                    RaiseEvent MouseClick(Me, arg)
                    Return
                End If
                If bt = MouseButton.None And bt2 <> MouseButton.None Then
                    arg._bt = bt2
                    RaiseEvent MouseUp(Me, arg)
                    Return
                End If
            End If
        Else
            If DrawRect.Contains(mOld.X, mOld.Y) Then
                RaiseEvent MouseExit(Me, arg)
                Return
            End If
        End If
    End Sub

    Event MouseMove As MouseEventHandler
    Event MouseDown As MouseEventHandler
    Event MouseUp As MouseEventHandler
    Event MouseEnter As MouseEventHandler
    Event MouseExit As MouseEventHandler
    Event MouseClick As MouseEventHandler
    Event MouseScroll As MouseEventHandler

#End Region

#Region "Hierarchy"

    Friend _container As ControlContainer

    Friend Sub SetParent(ByVal Parent As ControlContainer)
        _container = Parent
    End Sub

    ReadOnly Property Container() As ControlContainer
        Get
            Return _container
        End Get
    End Property
#End Region


End Class

