Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework

Public Class ShaderVariableMapForm

    Private Sub ShaderVariableMapForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Dim opd As New Windows.Forms.OpenFileDialog
        opd.ShowDialog()
        ' "Create graphics device"

        Dim filepath As String = opd.FileName
        'System.Console.WriteLine("Creating Device...")

        Dim parameters As New PresentationParameters()
        parameters.BackBufferWidth = 1
        parameters.BackBufferHeight = 1

        Dim dummy As New Form()
        Dim device As New GraphicsDevice(GraphicsAdapter.DefaultAdapter, DeviceType.NullReference, dummy.Handle, parameters)

        ' "Create effect"
        'System.Console.WriteLine("Compiling effect: " + filePath + "" & Chr(10) & "")

        Dim windowsCompiledEffect As CompiledEffect = Effect.CompileEffectFromFile(filepath, Nothing, Nothing, CompilerOptions.None, TargetPlatform.Windows)

        FlowLayoutPanel1.Controls.Clear()

        If windowsCompiledEffect.Success Then
            Dim eff As New Effect(device, windowsCompiledEffect.GetEffectCode(), CompilerOptions.None, Nothing)
            For Each p As EffectParameter In eff.Parameters
                Dim mc As New MapControl
                mc.Label1.Text = p.Name
                FlowLayoutPanel1.Controls.Add(mc)
            Next
        End If
    End Sub
End Class