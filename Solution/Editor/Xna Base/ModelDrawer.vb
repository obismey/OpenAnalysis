Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Public Class ModelDrawer

    Dim model As Model
    Dim v, p, w As Matrix
    Dim gdevice As GraphicsDevice
    Dim _cam As ICamera


    Sub New(ByVal mdl As Model, ByVal device As GraphicsDevice)
        model = mdl
        gdevice = device
        w = Matrix.Identity()
    End Sub

    Property Camera() As ICamera
        Get
            Return _cam
        End Get
        Set(ByVal value As ICamera)
            _cam = value
        End Set
    End Property

    Property World() As Matrix
        Get
            Return w
        End Get
        Set(ByVal value As Matrix)
            w = value
        End Set
    End Property

    ReadOnly Property Associate() As Model
        Get
            Return model
        End Get
    End Property

    ReadOnly Property Effet() As BasicEffect
        Get
            Return model.Meshes(0).Effects(0)
        End Get
    End Property

    Sub Draw()
        Dim transforms(model.Bones.Count) As Matrix
        Dim aspectRatio As Single = gdevice.Viewport.Width / gdevice.Viewport.Height
        model.CopyAbsoluteBoneTransformsTo(transforms)

        gdevice.RenderState.CullMode = CullMode.CullCounterClockwiseFace
        gdevice.RenderState.DepthBufferEnable = True
        gdevice.RenderState.DepthBufferWriteEnable = True
        gdevice.RenderState.AlphaBlendEnable = False
        For Each mesh As ModelMesh In model.Meshes
            For Each Effect As BasicEffect In mesh.Effects
                Effect.EnableDefaultLighting()
                Effect.View = Camera.viewMatrix 'WorkSpace.instance.context.View
                Effect.Projection = Camera.projMatrix 'WorkSpace.instance.context.Projection
                Effect.World = World * transforms(mesh.ParentBone.Index) 'WorkSpace.instance.context.
            Next
            mesh.Draw()
        Next
    End Sub
    Sub DrawOne(ByVal index As Integer)
        Dim aspectRatio As Single = gdevice.Viewport.Width / gdevice.Viewport.Height

        Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45.0F), aspectRatio, 1.0F, 1000.0F)
        gdevice.RenderState.CullMode = CullMode.CullCounterClockwiseFace
        gdevice.RenderState.DepthBufferEnable = True
        gdevice.RenderState.DepthBufferWriteEnable = True
        gdevice.RenderState.FillMode = FillMode.WireFrame

        For Each Effect As BasicEffect In model.Meshes(index).Effects

            Effect.EnableDefaultLighting()

            Effect.View = _cam.viewMatrix
            Effect.Projection = _cam.projMatrix
            Effect.World = World
        Next
        model.Meshes(index).Draw()

    End Sub
    Sub DrawPart(ByVal meshIndex As Integer, ByVal partIndex As Integer, ByVal effect As Effect)

        Dim part As ModelMeshPart = model.Meshes(meshIndex).MeshParts(partIndex)
        gdevice.VertexDeclaration = part.VertexDeclaration
        gdevice.Vertices(0).SetSource(model.Meshes(meshIndex).VertexBuffer, part.StreamOffset, part.VertexStride)
        gdevice.Indices = model.Meshes(meshIndex).IndexBuffer

       
        effect.Begin()
        For Each p As EffectPass In effect.CurrentTechnique.Passes
            p.Begin()
            gdevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, part.BaseVertex, 0, part.NumVertices, part.StartIndex, part.PrimitiveCount)
            p.End()
        Next
        effect.End()

    End Sub


    Dim worldParam As EffectParameter
    Dim viewParam As EffectParameter
    Dim ProjParam As EffectParameter
    Dim wvpParam As EffectParameter
    Dim rndParam As EffectParameter
    Dim rnd As New Random(DateTime.Now.Millisecond)
    Dim camDirParam As EffectParameter
    Dim camPosParam As EffectParameter
    Dim TimeParam As EffectParameter

    Sub setCustomEffect(ByVal eff As Effect)
        For Each m As ModelMesh In model.Meshes
            For Each mp As ModelMeshPart In m.MeshParts
                mp.Effect = eff
            Next
        Next

        worldParam = eff.Parameters("world")
        viewParam = eff.Parameters("view")
        ProjParam = eff.Parameters("proj")
        wvpParam = eff.Parameters("wvp")
        rndParam = eff.Parameters("rnd")

        camDirParam = eff.Parameters("camdir")
        TimeParam = eff.Parameters("time")
        camPosParam = eff.Parameters("campos")

        Dim rnd As New Random(DateTime.Now.Millisecond)
        Dim noises(32) As Single
        noises(0) = rnd.NextDouble()
        For i = 1 To 31
            noises(i) = Math.Cos(MathHelper.TwoPi * noises(i - 1)) + rnd.NextDouble()
            noises(i) += 1
            noises(i) = noises(i) / 3
        Next


        Dim noise As EffectParameter = eff.Parameters("noiseArray")
        noise.SetValue(noises)
    End Sub
    Sub DrawWithCustomEffect()
        Dim transforms(model.Bones.Count) As Matrix
        Dim aspectRatio As Single = gdevice.Viewport.Width / gdevice.Viewport.Height
        model.CopyAbsoluteBoneTransformsTo(transforms)

        gdevice.RenderState.CullMode = CullMode.CullCounterClockwiseFace
        gdevice.RenderState.DepthBufferEnable = True
        gdevice.RenderState.DepthBufferWriteEnable = True
        gdevice.RenderState.AlphaBlendEnable = False

        '    Dim cont As VCGE.Graphics.Core.DrawContext = WorkSpace.instance.context
        Dim eff As Effect = model.Meshes(0).Effects(0)

        '   eff.Parameters("worldIT").SetValue(cont.GetValueBySemantic(Graphics.Material.KDateTime.NownSemantics.WorldInverseTranspose))
        '  '   eff.Parameters("wvp").SetValue(cont.GetValueBySemantic(Graphics.Material.KDateTime.NownSemantics.WorldViewProjection))
        '  eff.Parameters("world").SetValue(cont.GetValueBySemantic(Graphics.Material.KDateTime.NownSemantics.World))
        '  eff.Parameters("viewIT").SetValue(cont.GetValueBySemantic(Graphics.Material.KDateTime.NownSemantics.ViewInverseTranspose))
        eff.CommitChanges()

        For i As Integer = 0 To model.Meshes.Count - 1
            For j As Integer = 0 To model.Meshes(i).MeshParts.Count - 1
                DrawPart(i, j, eff)
            Next
        Next



    End Sub
    Sub DrawWithCustomEffect2()
        gdevice.RenderState.CullMode = CullMode.CullCounterClockwiseFace
        gdevice.RenderState.DepthBufferEnable = True
        gdevice.RenderState.DepthBufferWriteEnable = True
        gdevice.RenderState.AlphaBlendEnable = False

        For Each m As ModelMesh In model.Meshes
            For Each e As Effect In m.Effects
                m.Draw()
            Next
        Next
    End Sub
    Sub DrawWithCustomEffect3()


        worldParam.SetValue(World)
        viewParam.SetValue(Camera.viewMatrix)
        ProjParam.SetValue(Camera.projMatrix)
        wvpParam.SetValue(World * Camera.viewMatrix * Camera.projMatrix)
        rndParam.SetValue(CSng(rnd.NextDouble()))

        If TimeParam IsNot Nothing Then
            'TimeParam.SetValue(Edition.GraphDocument.Renderer1.time)
        End If
        If camDirParam IsNot Nothing Then
            camDirParam.SetValue(Camera.CameraDirection)
        End If
        If camPosParam IsNot Nothing Then
            camPosParam.SetValue(Camera.CameraPosition)
        End If

        For Each m As ModelMesh In model.Meshes
            For Each e As Effect In m.Effects
                m.Draw()
            Next
        Next


    End Sub

    Sub DrawStandard()
        Dim transforms(model.Bones.Count) As Matrix
        model.CopyAbsoluteBoneTransformsTo(transforms)

        gdevice.RenderState.CullMode = CullMode.CullCounterClockwiseFace
        gdevice.RenderState.DepthBufferEnable = True
        gdevice.RenderState.DepthBufferWriteEnable = True
        gdevice.RenderState.AlphaBlendEnable = False
        For Each mesh As ModelMesh In model.Meshes
            ' For Each Effect As Shader.StandardMaterial In mesh.Effects
            'WorkSpace.instance.context.World = WorkSpace.instance.context.World * transforms(mesh.ParentBone.Index)
            ' Effect.UpdateSemantic(WorkSpace.instance.context)
            '  Next
            mesh.Draw()
        Next
    End Sub
End Class
