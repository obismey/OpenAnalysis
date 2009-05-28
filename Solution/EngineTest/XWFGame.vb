Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Engine

Public Class XWFGame
    Inherits Game

    Shared Sub Main()
        Dim game As New XWFGame()
        game.Run()
    End Sub

    Dim GraphManager As GraphicsDeviceManager
    Dim Xwf As Root

    Sub New()

        GraphManager = New GraphicsDeviceManager(Me)
        GraphManager.PreferredBackBufferHeight = 400
        GraphManager.PreferredBackBufferWidth = 750
        GraphManager.IsFullScreen = False
        GraphManager.PreferMultiSampling = True
        GraphManager.MinimumPixelShaderProfile = ShaderProfile.PS_3_0
        GraphManager.MinimumVertexShaderProfile = ShaderProfile.VS_3_0
        GraphManager.ApplyChanges()
        Me.Window.AllowUserResizing = True


    End Sub

    Protected Overrides Sub LoadContent()
        Xwf = New Root(Me)
        Xwf.Initialize()

        Dim wn As New TestWindow
        Xwf.RegisterWindow(wn)
        wn.InitializeComponents()
        MyBase.LoadContent()
    End Sub

    Protected Overrides Sub Update(ByVal gameTime As Microsoft.Xna.Framework.GameTime)
        Xwf.Update(gameTime)

        MyBase.Update(gameTime)
    End Sub
    Protected Overrides Sub Draw(ByVal gameTime As Microsoft.Xna.Framework.GameTime)
        GraphicsDevice.Clear(Color.Gray)
        Xwf.Draw(gameTime)

        MyBase.Draw(gameTime)
    End Sub
End Class
