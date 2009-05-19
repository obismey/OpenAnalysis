Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics

Public Class DrawingControl
    Inherits Xna.GraphicsDeviceControl

    Public cam As ArcBallCamera
    Friend content As Content.ContentManager
    Public model As ModelDrawer
    Public timer As Windows.Forms.Timer

    Protected Overrides Sub Draw()
        GraphicsDevice.Clear(Color.AliceBlue)
        cam.Update(Nothing)
        cam.AspectRatio = GraphicsDevice.Viewport.AspectRatio

        If model IsNot Nothing Then
            If model.Camera Is Nothing Then
                model.Camera = cam
            End If
            model.Draw()
        End If

    End Sub

    Protected Overrides Sub Initialize()
        ' DoubleBuffered = True
        content = New Content.ContentManager(Services)
        cam = New ArcBallCamera(4 / 3)
        timer = New Windows.Forms.Timer()
        timer.Interval = 20
        timer.Enabled = True
        timer.Start()

        AddHandler Application.Idle, AddressOf Invalidate
    End Sub

    Dim lastpos As Vector2
    Protected Overrides Sub OnMouseWheel(ByVal e As System.Windows.Forms.MouseEventArgs)

        cam.UpdateZoom(1.0F * Math.Sign(e.Delta))
        MyBase.OnMouseWheel(e)
    End Sub
    Protected Overrides Sub OnMouseMove(ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim ms As Drawing.Point = e.Location
        If e.Button = Windows.Forms.MouseButtons.Left Then
            Dim pos As Vector2 = New Vector2(ms.X, ms.Y)
            Dim dir As Vector2 = Vector2.Normalize(pos - lastpos)
            cam.Rotate(dir.X / 5, dir.Y / 5)
        End If

        lastpos = New Vector2(e.X, e.Y)
        MyBase.OnMouseMove(e)
    End Sub
    Protected Overrides Sub OnMouseClick(ByVal e As System.Windows.Forms.MouseEventArgs)
        Focus()
        MyBase.OnMouseClick(e)
    End Sub


End Class
