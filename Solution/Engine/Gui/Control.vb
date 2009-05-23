Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Graphics
Imports System.ComponentModel

''' <summary>
''' 
''' </summary>
''' <remarks></remarks>
Public Class Control
    Implements INotifyPropertyChanged

    Dim _id As Guid
    Protected _drawrect As Rectangle
    Protected _type As String = ""

    Sub New()
        _id = Guid.NewGuid
        _hal = Alignment.Strech
        _val = Alignment.Strech
        _margin = New Thickness(0)
    End Sub

#Region "Infrastructure"
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="elapsedTime"></param>
    ''' <param name="totalTime"></param>
    ''' <remarks></remarks>
    Protected Friend Overridable Sub Draw(ByVal elapsedTime As Double, ByVal totalTime As Double)
        Dim dr As SpriteBatch = Root.Instance.Drawer
        DefaultBrush.Draw(DrawRect, dr)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="elapsedTime"></param>
    ''' <param name="totalTime"></param>
    ''' <remarks></remarks>
    Protected Friend Overridable Sub Update(ByVal elapsedTime As Double, ByVal totalTime As Double)
        _dimensionRect = ComputeRect(_container.GetAllowedDrawRect(Me))
        _drawrect = Rectangle.Intersect(_dimensionRect, _container.GetAllowedClipRect(Me))
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="previous"></param>
    ''' <param name="newv"></param>
    ''' <remarks></remarks>
    Protected Sub OnVisibilityChange(ByVal previous As Boolean, ByVal newv As Boolean)

    End Sub
#End Region

#Region "Positionnement and render transform"
    Dim _w, _h As Dimension
    Dim _hal, _val As Alignment
    Dim _margin As Thickness
    Dim _transformData() As Single = {0.0F, 0.0F, 0.0F, 0.0F, 1.0F, 1.0F, 0.0F}
    Protected _dimensionRect As Rectangle

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Height() As Dimension
        Get
            Return _h
        End Get
        Set(ByVal value As Dimension)
            _h = value
        End Set
    End Property

    ''' <summary>
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property Width() As Dimension
        Get
            Return _w
        End Get
        Set(ByVal value As Dimension)
            _w = value
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
    Property Margin() As Thickness
        Get
            Return _margin
        End Get
        Set(ByVal value As Thickness)
            _margin = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Transform() As Matrix
        Get

        End Get
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
        Dim m As Single() = Margin.ComputeAbsolute(parentWidth, parentHeight)

        Dim mw = Width.ComputeAbsolute(parentWidth)
        Dim mh = Height.ComputeAbsolute(parentHeight)

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
    Dim _aref As Single = 0.5F
    Dim _useAlpha As Boolean = False
    Dim _visibility As Visibility
    Dim _color As Brush = Color.AliceBlue
    Dim _enabled As Boolean
    Dim _focusable As Boolean

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
    Property UseAlphaAsClippingMask() As Boolean
        Get
            Return _useAlpha
        End Get
        Set(ByVal value As Boolean)
            _useAlpha = value
        End Set
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Property AlphaClipRef() As Single
        Get
            Return _aref
        End Get
        Set(ByVal value As Single)
            _aref = value
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
    ReadOnly Property Uid() As Guid
        Get
            Return _id
        End Get
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
            Dim tmp As Visibility = _visibility
            _visibility = value
            If tmp <> _visibility Then
                OnVisibilityChange(tmp, _visibility)
            End If
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

        End Get
        Set(ByVal value As Integer)

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

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Type() As String
        Get
            Return _type
        End Get
    End Property
#End Region

#Region "Input"
    Function SetFocus() As Boolean

    End Function
    Sub OnFocusLost(ByVal source As Control)

    End Sub
    Sub OnGainFocus()

    End Sub



#End Region

#Region "Keyboard Event"
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

#Region "Data Bindings"

    Sub AddBinding()

    End Sub
    Sub RemoveBinding()

    End Sub
    Friend Sub UpdateBinding()

    End Sub

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

    Protected Sub OnPropertyChange()

    End Sub

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
End Class

Public Enum Visibility
    Visible
    Collapsed
    Hidden
End Enum