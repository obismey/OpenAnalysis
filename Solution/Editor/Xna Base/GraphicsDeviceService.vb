#Region "File Description"
'-----------------------------------------------------------------------------
' GraphicsDeviceService.cs
'
' Microsoft XNA Community Game Platform
' Copyright (C) Microsoft Corporation. All rights reserved.
'-----------------------------------------------------------------------------
#End Region

#Region "Using Statements"
Imports System
Imports System.Threading
Imports Microsoft.Xna.Framework.Graphics
#End Region

' The IGraphicsDeviceService interface requires a DeviceCreated event, but we
' always just create the device inside our constructor, so we have no place to
' raise that event. The C# compiler warns us that the event is never used, but
' we don't care so we just disable this warning.
'#Pragma warning disable 67

Namespace Xna
    ''' <summary>
    ''' Helper class responsible for creating and managing the GraphicsDevice.
    ''' All GraphicsDeviceControl instances share the same GraphicsDeviceService,
    ''' so even though there can be many controls, there will only ever be a single
    ''' underlying GraphicsDevice. This implements the standard IGraphicsDeviceService
    ''' interface, which provides notification events for when the device is reset
    ''' or disposed.
    ''' </summary>
    Public Class GraphicsDeviceService
        Implements IGraphicsDeviceService

#Region "Fields"

        ' Singleton device service instance.
        Friend Shared singletonInstance As GraphicsDeviceService

        ' Keep track of how many controls are sharing the singletonInstance.
        Shared referenceCount As Integer

#End Region


        ''' <summary>
        ''' Constructor is private, because this is a singleton class:
        ''' client controls should use the public AddRef method instead.
        ''' </summary>
        Private Sub New(ByVal windowHandle As IntPtr, ByVal width As Integer, ByVal height As Integer)
            parameters = New PresentationParameters()

            parameters.BackBufferWidth = Math.Max(width, 1)
            parameters.BackBufferHeight = Math.Max(height, 1)
            parameters.BackBufferFormat = SurfaceFormat.Color

            parameters.EnableAutoDepthStencil = True
            parameters.AutoDepthStencilFormat = DepthFormat.Depth24

            m_graphicsDevice = New GraphicsDevice(GraphicsAdapter.DefaultAdapter, DeviceType.Hardware, windowHandle, parameters)
        End Sub


        ''' <summary>
        ''' Gets a reference to the singleton instance.
        ''' </summary>
        Public Shared Function AddRef(ByVal windowHandle As IntPtr, ByVal width As Integer, ByVal height As Integer) As GraphicsDeviceService
            ' Increment the "how many controls sharing the device" reference count.
            If Interlocked.Increment(referenceCount) = 1 Then
                ' If this is the first control to start using the
                ' device, we must create the singleton instance.
                singletonInstance = New GraphicsDeviceService(windowHandle, width, height)
            End If

            Return singletonInstance
        End Function


        ''' <summary>
        ''' Releases a reference to the singleton instance.
        ''' </summary>
        Public Sub Release(ByVal disposing As Boolean)
            ' Decrement the "how many controls sharing the device" reference count.
            If Interlocked.Decrement(referenceCount) = 0 Then
                ' If this is the last control to finish using the
                ' device, we should dispose the singleton instance.
                If disposing Then
                    RaiseEvent DeviceDisposing(Me, EventArgs.Empty)

                    m_graphicsDevice.Dispose()
                End If

                m_graphicsDevice = Nothing
            End If
        End Sub


        ''' <summary>
        ''' Resets the graphics device to whichever is bigger out of the specified
        ''' resolution or its current size. This behavior means the device will
        ''' demand-grow to the largest of all its GraphicsDeviceControl clients.
        ''' </summary>
        Public Sub ResetDevice(ByVal width As Integer, ByVal height As Integer)
            RaiseEvent DeviceResetting(Me, EventArgs.Empty)

            parameters.BackBufferWidth = Math.Max(parameters.BackBufferWidth, width)
            parameters.BackBufferHeight = Math.Max(parameters.BackBufferHeight, height)

            m_graphicsDevice.Reset(parameters)
            RaiseEvent DeviceReset(Me, EventArgs.Empty)

        End Sub


        ''' <summary>
        ''' Gets the current graphics device.
        ''' </summary>
        Public ReadOnly Property GraphicsDevice() As GraphicsDevice Implements IGraphicsDeviceService.GraphicsDevice
            Get
                Return m_graphicsDevice
            End Get
        End Property

        Private m_graphicsDevice As GraphicsDevice
        ' Store the current device settings.
        Private parameters As PresentationParameters
        ' IGraphicsDeviceService events.

        Public Event DeviceCreated(ByVal sender As Object, ByVal e As System.EventArgs) Implements Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService.DeviceCreated
        Public Event DeviceDisposing(ByVal sender As Object, ByVal e As System.EventArgs) Implements Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService.DeviceDisposing
        Public Event DeviceReset(ByVal sender As Object, ByVal e As System.EventArgs) Implements Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService.DeviceReset
        Public Event DeviceResetting(ByVal sender As Object, ByVal e As System.EventArgs) Implements Microsoft.Xna.Framework.Graphics.IGraphicsDeviceService.DeviceResetting
    End Class
End Namespace


