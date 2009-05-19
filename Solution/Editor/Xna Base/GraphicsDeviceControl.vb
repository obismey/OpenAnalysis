#Region "File Description"
'-----------------------------------------------------------------------------
' GraphicsDeviceControl.cs
'
' Microsoft XNA Community Game Platform
' Copyright (C) Microsoft Corporation. All rights reserved.
'-----------------------------------------------------------------------------
#End Region

#Region "Using Statements"
' System.Drawing and the XNA Framework both define Color and Rectangle
' types. To avoid conflicts, we specify exactly which ones to use.
Imports GdiColor = System.Drawing.Color
Imports XnaColor = Microsoft.Xna.Framework.Graphics.Color
Imports Rectangle = Microsoft.Xna.Framework.Rectangle
Imports System
Imports System.Drawing
Imports System.Windows.Forms
Imports Microsoft.Xna.Framework.Graphics
#End Region

Namespace Xna

    ''' <summary>
    ''' Custom control uses the XNA Framework GraphicsDevice to render onto
    ''' a Windows Form. Derived classes can override the Initialize and Draw
    ''' methods to add their own drawing code.
    ''' </summary>
    Public MustInherit Class GraphicsDeviceControl
        Inherits Control
#Region "Fields"
        ' However many GraphicsDeviceControl instances you have, they all share
        ' the same underlying GraphicsDevice, managed by this helper service.
        Private graphicsDeviceService As GraphicsDeviceService
        Private m_services As New ServiceContainer()
#End Region

#Region "Properties"
        ''' <summary>
        ''' Gets a GraphicsDevice that can be used to draw onto this control.
        ''' </summary>
        Public ReadOnly Property GraphicsDevice() As GraphicsDevice
            Get
                Return graphicsDeviceService.GraphicsDevice
            End Get
        End Property

        ''' <summary>
        ''' Gets an IServiceProvider containing our IGraphicsDeviceService.
        ''' This can be used with components such as the ContentManager,
        ''' which use this service to look up the GraphicsDevice.
        ''' </summary>
        Public ReadOnly Property Services() As ServiceContainer
            Get
                Return m_services
            End Get
        End Property
#End Region

#Region "Initialization"
        ''' <summary>
        ''' Initializes the control.
        ''' </summary>
        Protected Overloads Overrides Sub OnCreateControl()
            ' Don't initialize the graphics device if we are running in the designer.
            If Not DesignMode Then
                graphicsDeviceService = graphicsDeviceService.AddRef(Handle, ClientSize.Width, ClientSize.Height)

                ' Register the service, so components like ContentManager can find it.
                m_services.AddService(Of IGraphicsDeviceService)(graphicsDeviceService)

                ' Give derived classes a chance to initialize themselves.
                Initialize()
            End If
            MyBase.OnCreateControl()
        End Sub
        ''' <summary>
        ''' Disposes the control.
        ''' </summary>
        Protected Overloads Overrides Sub Dispose(ByVal disposing As Boolean)
            If graphicsDeviceService IsNot Nothing Then
                graphicsDeviceService.Release(disposing)
                graphicsDeviceService = Nothing
            End If

            MyBase.Dispose(disposing)
        End Sub
#End Region

#Region "Paint"
        ''' <summary>
        ''' Redraws the control in response to a WinForms paint message.
        ''' </summary>
        Protected Overloads Overrides Sub OnPaint(ByVal e As PaintEventArgs)
            Dim beginDrawError As String = BeginDraw()

            If String.IsNullOrEmpty(beginDrawError) Then
                ' Draw the control using the GraphicsDevice.
                Draw()
                EndDraw()
            Else
                ' If BeginDraw failed, show an error message using System.Drawing.
                PaintUsingSystemDrawing(e.Graphics, beginDrawError)
            End If
        End Sub

        ''' <summary>
        ''' Attempts to begin drawing the control. Returns an error message string
        ''' if this was not possible, which can happen if the graphics device is
        ''' lost, or if we are running inside the Form designer.
        ''' </summary>
        Private Function BeginDraw() As String
            ' If we have no graphics device, we must be running in the designer.
            If graphicsDeviceService Is Nothing Then
                Return Text & Environment.NewLine() & Environment.NewLine() & Me.GetType.FullName
            End If

            ' Make sure the graphics device is big enough, and is not lost.
            Dim deviceResetError As String = HandleDeviceReset()

            If Not String.IsNullOrEmpty(deviceResetError) Then
                Return deviceResetError
            End If

            ' Many GraphicsDeviceControl instances can be sharing the same
            ' GraphicsDevice. The device backbuffer will be resized to fit the
            ' largest of these controls. But what if we are currently drawing
            ' a smaller control? To avoid unwanted stretching, we set the
            ' viewport to only use the top left portion of the full backbuffer.
            Dim viewport As New Viewport()

            viewport.X = 0
            viewport.Y = 0

            viewport.Width = ClientSize.Width
            viewport.Height = ClientSize.Height

            viewport.MinDepth = 0
            viewport.MaxDepth = 1

            GraphicsDevice.Viewport = viewport

            Return Nothing
        End Function


        ''' <summary>
        ''' Ends drawing the control. This is called after derived classes
        ''' have finished their Draw method, and is responsible for presenting
        ''' the finished image onto the screen, using the appropriate WinForms
        ''' control handle to make sure it shows up in the right place.
        ''' </summary>
        Private Sub EndDraw()
            Try
                Dim sourceRectangle As New Rectangle(0, 0, ClientSize.Width, ClientSize.Height)

                GraphicsDevice.Present(sourceRectangle, Nothing, Me.Handle)
                ' Present might throw if the device became lost while we were
                ' drawing. The lost device will be handled by the next BeginDraw,
                ' so we just swallow the exception.
            Catch
            End Try
        End Sub


        ''' <summary>
        ''' Helper used by BeginDraw. This checks the graphics device status,
        ''' making sure it is big enough for drawing the current control, and
        ''' that the device is not lost. Returns an error string if the device
        ''' could not be reset.
        ''' </summary>
        Private Function HandleDeviceReset() As String
            Dim deviceNeedsReset As Boolean = False

            Select Case GraphicsDevice.GraphicsDeviceStatus
                Case GraphicsDeviceStatus.Lost
                    ' If the graphics device is lost, we cannot use it at all.
                    Return "Graphics device lost"
                Case GraphicsDeviceStatus.NotReset

                    ' If device is in the not-reset state, we should try to reset it.
                    deviceNeedsReset = True
                    Exit Select
                Case Else

                    ' If the device state is ok, check whether it is big enough.
                    Dim pp As PresentationParameters = GraphicsDevice.PresentationParameters

                    deviceNeedsReset = (ClientSize.Width > pp.BackBufferWidth) OrElse (ClientSize.Height > pp.BackBufferHeight)
                    Exit Select
            End Select

            ' Do we need to reset the device?
            If deviceNeedsReset Then
                Try
                    graphicsDeviceService.ResetDevice(ClientSize.Width, ClientSize.Height)
                Catch e As Exception
                    Return "Graphics device reset failed" & Environment.NewLine & Environment.NewLine & e.ToString()
                End Try
            End If

            Return Nothing
        End Function


        ''' <summary>
        ''' If we do not have a valid graphics device (for instance if the device
        ''' is lost, or if we are running inside the Form designer), we must use
        ''' regular System.Drawing method to display a status message.
        ''' </summary>
        Protected Overridable Sub PaintUsingSystemDrawing(ByVal graphics As System.Drawing.Graphics, ByVal text As String)
            graphics.Clear(GdiColor.CornflowerBlue)

            Using brush As Brush = New SolidBrush(GdiColor.Black)
                Using format As New StringFormat()
                    format.Alignment = StringAlignment.Center
                    format.LineAlignment = StringAlignment.Center
                    graphics.DrawString(text, Font, brush, ClientRectangle, format)
                End Using
            End Using
        End Sub


        ''' <summary>
        ''' Ignores WinForms paint-background messages. The default implementation
        ''' would clear the control to the current background color, causing
        ''' flickering when our OnPaint implementation then immediately draws some
        ''' other color over the top using the XNA Framework GraphicsDevice.
        ''' </summary>
        Protected Overloads Overrides Sub OnPaintBackground(ByVal pevent As PaintEventArgs)
        End Sub
#End Region

#Region "Abstract Methods"
        ''' <summary>
        ''' Derived classes override this to initialize their drawing code.
        ''' </summary>
        Protected MustOverride Sub Initialize()


        ''' <summary>
        ''' Derived classes override this to draw themselves using the GraphicsDevice.
        ''' </summary>
        Protected MustOverride Sub Draw()
#End Region


        Property AllowRendering() As Boolean
            Get

            End Get
            Set(ByVal value As Boolean)

            End Set
        End Property

        Shared Function GdiColorToXnaColor(ByVal inc As GdiColor) As XnaColor
            Return New XnaColor(inc.R, inc.G, inc.B, inc.A)
        End Function
        Shared Function XnaColorToGdiColor(ByVal inc As XnaColor) As GdiColor
            Return GdiColor.FromArgb(inc.PackedValue)
        End Function
    End Class


End Namespace





