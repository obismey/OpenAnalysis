Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Content
Imports Microsoft.Xna.Framework.Input
Imports Microsoft.Xna.Framework.Media
Imports Microsoft.Xna.Framework.Audio
Imports System.Collections.Generic


Public Class Root
    Implements Microsoft.Xna.Framework.IGameComponent
    Implements Microsoft.Xna.Framework.IDrawable
    Implements Microsoft.Xna.Framework.IUpdateable


    Private _game As Game

    Sub New(ByVal game As Game)
        If _inst Is Nothing Then
            _inst = Me
        Else
            Return
        End If

        game.Components.Add(Me)
        _game = game
        _content = _game.Content
        _graphicsDevice = _game.GraphicsDevice
    End Sub
    Sub New(ByVal content As ContentManager, ByVal device As GraphicsDevice)
        If _inst Is Nothing Then
            _inst = Me
        Else
            Return
        End If

        _content = content
        _graphicsDevice = device
    End Sub

#Region "Singleton Implementation"
    Private Shared _inst As Root = Nothing
    Shared ReadOnly Property Instance() As Root
        Get
            Return _inst
        End Get
    End Property
#End Region

#Region "Ressource Management"
    Dim _ressources As New SortedList(Of String, Object)
    Dim _content As ContentManager

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <typeparam name="T"></typeparam>
    ''' <param name="path"></param>
    ''' <param name="name"></param>
    ''' <remarks></remarks>
    Sub LoadRessource(Of T)(ByVal path As String, Optional ByVal name As String = Nothing)
        Dim contentName As String = IO.Path.GetFileNameWithoutExtension(path)
        If Not String.IsNullOrEmpty(name) Then
            contentName = name
        End If
        If _ressources.ContainsKey(contentName) Then
            Return
        End If
        If GetType(T) Is GetType(Texture2D) Then
            Dim texture As Texture2D = Nothing
            Try
                texture = _content.Load(Of Texture2D)(path)
            Catch ex1 As Exception
                Try
                    texture = Texture2D.FromFile(_graphicsDevice, path)
                Catch ex2 As Exception
                    Return
                End Try
            End Try

            If texture IsNot Nothing Then
                _ressources.Add(contentName, texture)
            End If

        Else
            Try
                _ressources.Add(contentName, _content.Load(Of T)(path))
            Catch ex As Exception
                Return
            End Try
        End If
    End Sub


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetTexture(ByVal id As String) As Texture2D
        Return _ressources(id)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function GetSound(ByVal id As String) As SoundEffect
        Return _ressources(id)
    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="id"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Function LoadMusic(ByVal id As String) As Song
        Return _ressources(id)
    End Function
#End Region

#Region "Gui Singleton"



#Region "Input Managment"
    Dim oldKeyBoardState As KeyboardState
    Dim oldMouseState As MouseState
    Dim _focused As Control = Nothing
    Dim _consumers As New List(Of Control)


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property Focused() As Control
        Get
            Return _focused
        End Get
    End Property

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="source"></param>
    ''' <remarks></remarks>
    Friend Sub NotifyFocusChange(ByVal source As Control)
        If _focused IsNot Nothing Then
            _focused.OnFocusLost(source)
        End If
        _focused = source
    End Sub

    Friend Sub AddControl(ByVal cons As Control)
        If _consumers.Contains(cons) Then
            Throw New Exception("Registered Already")
        Else
            _consumers.Add(cons)
        End If
    End Sub
    Friend Sub RemoveControl(ByVal cons As Control)
        _consumers.Remove(cons)
    End Sub
#End Region

#Region "Windows Managment"

    Dim _drawer As SpriteBatch
    Dim _graphicsDevice As GraphicsDevice
    Dim _text As Texture2D

    Dim _windows As New List(Of Window)
    Dim _activeWindow As Window

    Dim debugVisual As Control
    Dim debugTextVisual As Control

    Dim rnd As New Random(Date.Now.Millisecond)
    Dim mouseElt As Control
    Dim mouseRect As Rectangle
    Dim previoustate As KeyState
    Dim _initcall As Boolean = False

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="window"></param>
    ''' <remarks></remarks>
    Sub RegisterWindow(ByVal window As Window)
        If window Is Nothing Then Return
        If Not _windows.Contains(window) Then
            _windows.Add(window)
        End If
    End Sub

    ''' <summary>
    '''  
    ''' </summary>
    ''' <param name="window"></param>
    ''' <remarks>All ressource related to window will be destroy. Call once </remarks>
    Sub UnRegisterWindow(ByVal window As Window)
        _windows.Remove(window)
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    ReadOnly Property ActiveWindow() As Window
        Get
            Return _activeWindow
        End Get
    End Property

    Friend ReadOnly Property Drawer() As SpriteBatch
        Get
            Return _drawer
        End Get
    End Property
    Friend ReadOnly Property Gdevice() As GraphicsDevice
        Get
            Return _graphicsDevice
        End Get
    End Property
    Friend ReadOnly Property ViewportRect() As Rectangle
        Get
            Return New Rectangle(0, 0, Gdevice.Viewport.Width, Gdevice.Viewport.Height)
        End Get
    End Property
#End Region

    Private Sub GuiInitialize()
        LoadRessource(Of Texture2D)("mouse.dds", "mouse")
        Dim tex As New Texture2D(Gdevice, 1, 1)
        Dim data() As Color = New Color() {Color.White}
        tex.SetData(Of Color)(data)

        _ressources.Add("colormap", tex)
        LoadRessource(Of Texture2D)("bt.png", "bt")
        _drawer = New SpriteBatch(_graphicsDevice)
    End Sub

    Public Sub GuiDraw(ByVal context As DrawContext, ByVal timeElapsed As Double?, ByVal totalTimeElapsed As Double?)

        Drawer.Begin()
     
        For Each w As Window In _windows
            w.Draw(context, timeElapsed, totalTimeElapsed)
        Next

        Dim mpos As Vector2 = New Vector2(Mouse.GetState().X, Mouse.GetState().Y)


        Drawer.Draw(GetTexture("mouse"), New Rectangle(mpos.X, mpos.Y, 20, 20), Color.White)

        Drawer.End()

    End Sub
    Public Sub GuiUpdate(ByVal timeElapsed As Double?, ByVal totalTimeElapsed As Double?)
        Dim currentState As KeyboardState = Keyboard.GetState()
        If _focused IsNot Nothing Then
            If currentState <> oldKeyBoardState Then
                _focused.OnKeyboardEvent(currentState, oldKeyBoardState, TimeSpan.FromSeconds(timeElapsed))
            End If
        End If
        Dim mouseState As MouseState = Mouse.GetState()
        For Each c In _consumers
            c.OnMouseEvent(mouseState, oldMouseState, TimeSpan.FromSeconds(timeElapsed))
        Next
        For Each w As Window In _windows
            If w.Root IsNot Nothing Then
                w.Root.OnMouseEvent(mouseState, oldMouseState, TimeSpan.FromSeconds(timeElapsed))
            End If
            w.Update(timeElapsed, totalTimeElapsed)
        Next
        Dim mpos As Vector2 = New Vector2(Mouse.GetState().X, Mouse.GetState().Y)
        mouseRect = New Rectangle(mpos.X, mpos.Y, 20, 20)

        oldKeyBoardState = currentState
        oldMouseState = mouseState
    End Sub
    Sub DrawLines(ByVal points() As Vector2, ByVal close As Boolean, ByVal colour As Color, ByVal size As Single)
        For i As Integer = 1 To points.Length - 1
            DrawLine(points(i - 1), points(i), colour, size)
        Next
        If close Then
            DrawLine(points(points.Length - 1), points(0), colour, size)
        End If
    End Sub
    Sub DrawLine(ByVal start As Vector2, ByVal [end] As Vector2, ByVal colour As Color, ByVal size As Single)
        Dim angle As Single = Math.Atan2([end].Y - start.Y, [end].X - start.X)
        Dim length As Single = ([end] - start).Length
        Drawer.Draw(_ressources("colormap"), start, Nothing, colour, _
                    angle, Vector2.Zero, New Vector2(length, size), SpriteEffects.None, 0)

    End Sub

#End Region

#Region "Input Singleton"
    Sub Register(ByVal mouse As IMouse)

    End Sub
    Sub Register(ByVal keyboard As IKeyboard)

    End Sub
    Sub Register(ByVal pad As IPad)

    End Sub
#End Region

#Region "Logging Singleton"

#End Region

#Region "Sound Singleton"

#End Region

#Region "Interfaces Implementation"
    Dim _dorder, _uorder As Integer
    Dim _visible, _enabled As Boolean

    Public Sub Initialize() Implements Microsoft.Xna.Framework.IGameComponent.Initialize
        GuiInitialize()
    End Sub
    Public Sub Draw(ByVal gameTime As Microsoft.Xna.Framework.GameTime) Implements Microsoft.Xna.Framework.IDrawable.Draw
        GuiDraw(Nothing, gameTime.ElapsedGameTime.TotalSeconds, gameTime.TotalGameTime.TotalSeconds)
    End Sub
    Public Sub Update(ByVal gameTime As Microsoft.Xna.Framework.GameTime) Implements Microsoft.Xna.Framework.IUpdateable.Update
        GuiUpdate(gameTime.ElapsedGameTime.TotalSeconds, gameTime.TotalGameTime.TotalSeconds)
    End Sub

    Public ReadOnly Property DrawOrder() As Integer Implements Microsoft.Xna.Framework.IDrawable.DrawOrder
        Get
            Return _dorder
        End Get
    End Property
    Public ReadOnly Property Visible() As Boolean Implements Microsoft.Xna.Framework.IDrawable.Visible
        Get
            Return _visible
        End Get
    End Property
    Public ReadOnly Property Enabled() As Boolean Implements Microsoft.Xna.Framework.IUpdateable.Enabled
        Get
            Return _enabled
        End Get
    End Property
    Public ReadOnly Property UpdateOrder() As Integer Implements Microsoft.Xna.Framework.IUpdateable.UpdateOrder
        Get
            Return _uorder
        End Get
    End Property

    Public Event DrawOrderChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements Microsoft.Xna.Framework.IDrawable.DrawOrderChanged
    Public Event VisibleChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements Microsoft.Xna.Framework.IDrawable.VisibleChanged
    Public Event EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements Microsoft.Xna.Framework.IUpdateable.EnabledChanged
    Public Event UpdateOrderChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements Microsoft.Xna.Framework.IUpdateable.UpdateOrderChanged
#End Region
End Class


