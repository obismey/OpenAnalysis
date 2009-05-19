Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework
Imports System.Runtime.InteropServices

<StructLayout(LayoutKind.Explicit, Size:=2)> _
Public Structure DepthColourCullState
    <FieldOffset(0)> _
    Friend mode As UShort

    ''' <summary></summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Shared Narrowing Operator CType(ByVal state As UShort) As DepthColourCullState
        Dim value As DepthColourCullState
        value.mode = state
        Return value
    End Operator

    ''' <summary></summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Shared Widening Operator CType(ByVal state As DepthColourCullState) As UShort
        Return state.mode
    End Operator

    ''' <summary></summary>
    ''' <param name="a"></param>
    ''' <param name="b"></param>
    ''' <returns></returns>
    Public Shared Operator =(ByVal a As DepthColourCullState, ByVal b As DepthColourCullState) As Boolean
        Return a.mode = b.mode
    End Operator

    ''' <summary></summary>
    ''' <param name="a"></param>
    ''' <param name="b"></param>
    ''' <returns></returns>
    Public Shared Operator <>(ByVal a As DepthColourCullState, ByVal b As DepthColourCullState) As Boolean
        Return a.mode <> b.mode
    End Operator

    ''' <summary></summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
        If TypeOf obj Is DepthColourCullState Then
            Return DirectCast(obj, DepthColourCullState).mode = Me.mode
        End If
        Return False
    End Function

    ''' <summary>
    ''' Gets the hash code. Returned value is the internal bitfield value
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Overrides Function GetHashCode() As Integer
        Return mode
    End Function

    ''' <summary>
    ''' Gets/Sets if depth testing is enabled
    ''' </summary>
    ''' <remarks><para>If depth testing is disabled, then pixels will always be drawn, even if they are behind another object on screen.</para></remarks>
    Public Property DepthTestEnabled() As Boolean
        Get
            Return (mode And 1) <> 1
        End Get
        Set(ByVal value As Boolean)
            mode = CUShort(((mode And Not 1) Or (If(value, 0, 1))))
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets if depth writing is enabled
    ''' </summary>
    ''' <remarks><para>If depth writing is disabled, pixels that are drawn will still go through normal depth testing, however they will not write a new depth value into the depth buffer. This is most useful for transparent effects, and any effect that does not have a physical representation (eg, a light cone, particle effects, etc).</para>
    ''' <para>Usually, 'solid' objects with depth writing enabled will be drawn first. Such as the world, characters, models, etc. Then non-solid and effect geometry is drawn without depth writing. If this order is reversed, the solid geometry can overwrite the effects.</para></remarks>
    Public Property DepthWriteEnabled() As Boolean
        Get
            Return (mode And 2) <> 2
        End Get
        Set(ByVal value As Boolean)
            mode = CUShort(((mode And Not 2) Or (If(value, 0, 2))))
        End Set
    End Property

    ''' <summary>
    ''' Changes the comparsion function used when depth testing. WARNING:  On some video cards, changing this value can disable hierarchical z-buffer optimizations for the rest of the frame
    ''' </summary>
    ''' <remarks>
    ''' <para>Changing the depth test function from Less to Greater midframe is <i>not recommended</i>.</para>
    ''' <para>Changing between <see cref="CompareFunction.LessEqual"/> and <see cref="CompareFunction.Equal"/> is usually OK.</para>
    ''' <para>On newer video cards, keeping the depth test function consistent throughout the frame will still maintain peek effciency, however some older cards are only full speed when using <see cref="CompareFunction.LessEqual"/> or <see cref="CompareFunction.Equal"/></para>
    ''' <para>Setting <see cref="DepthTestEnabled"/> to false is the preferred to using <see cref="CompareFunction.Always"/>.</para>
    ''' </remarks>
    Public Property DepthTestFunction() As CompareFunction
        Get
            Return DirectCast(((((mode >> 2) And 15) Xor 4)), CompareFunction)
        End Get
        Set(ByVal value As CompareFunction)
            mode = CUShort(((mode And Not (15 << 2)) Or (15 And CInt(value) Xor 4) << 2))
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets the backface culling render state. Default value of <see cref="Microsoft.Xna.Framework.Graphics.CullMode.CullCounterClockwiseFace">CullCounterClockwiseFace</see>
    ''' </summary>
    Public Property CullMode() As CullMode
        Get
            Return DirectCast((((((mode >> 6) And 3) Xor 2)) + 1), CullMode)
        End Get
        Set(ByVal value As CullMode)
            mode = CUShort(((mode And Not (3 << 6)) Or (3 And (CInt(value) - 1) Xor 2) << 6))
        End Set
    End Property

    Friend Function GetCullMode(ByVal reverseCull As Boolean) As CullMode
        Dim mode As CullMode = DirectCast((((((Me.mode >> 6) And 3) Xor 2)) + 1), CullMode)
        If reverseCull Then
            Select Case mode
                Case CullMode.CullClockwiseFace
                    Return CullMode.CullCounterClockwiseFace
                Case CullMode.CullCounterClockwiseFace
                    Return CullMode.CullClockwiseFace
            End Select
        End If
        Return mode
    End Function

    ''' <summary>
    ''' Gets/Sets a mask for the colour channels (RGBA) that are written to the colour buffer. Set to <see cref="ColorWriteChannels.None"/> to disable all writing to the colour buffer.
    ''' </summary>
    Public Property ColourWriteMask() As ColorWriteChannels
        Get
            Return DirectCast((((Not (mode >> 8)) And 15)), ColorWriteChannels)
        End Get
        Set(ByVal value As ColorWriteChannels)
            mode = CUShort(((mode And Not (15 << 8)) Or (15 And (Not CInt(value))) << 8))
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets a mask for the <see cref="FillMode"/> for the device (eg, <see cref="Microsoft.Xna.Framework.Graphics.FillMode.WireFrame"/> or <see cref="Microsoft.Xna.Framework.Graphics.FillMode.Solid"/>)
    ''' </summary>
    Public Property FillMode() As FillMode
        Get
            Return DirectCast((((((mode >> 12) And 3) Xor 2)) + 1), FillMode)
        End Get
        Set(ByVal value As FillMode)
            mode = CUShort(((mode And Not (3 << 12)) Or (3 And (CInt(value) - 1) Xor 2) << 12))
        End Set
    End Property

    Friend Sub ResetState(ByRef current As DepthColourCullState, ByVal device As GraphicsDevice, ByVal reverseCull As Boolean)
        device.RenderState.DepthBufferEnable = Me.DepthTestEnabled
        device.RenderState.DepthBufferWriteEnable = Me.DepthWriteEnabled
        device.RenderState.DepthBufferFunction = Me.DepthTestFunction
        device.RenderState.CullMode = Me.GetCullMode(reverseCull)
        device.RenderState.ColorWriteChannels = Me.ColourWriteMask
        device.RenderState.FillMode = Me.FillMode

        current.mode = Me.mode
    End Sub

    Friend Function ApplyState(ByRef current As DepthColourCullState, ByVal device As GraphicsDevice, ByVal reverseCull As Boolean) As Boolean
        Dim changed As Boolean = False
        ' bits 6 to 14 are used.. so ignore bits 1-5
        If (current.mode And (Not 63)) <> (mode And (Not 63)) Then

            Dim cull As CullMode = Me.CullMode
            If cull <> current.CullMode Then
                device.RenderState.CullMode = GetCullMode(reverseCull)
                current.CullMode = cull
            End If
            Dim channels As ColorWriteChannels = Me.ColourWriteMask
            If channels <> current.ColourWriteMask Then
                device.RenderState.ColorWriteChannels = channels
                current.ColourWriteMask = channels
            End If
            Dim fill As FillMode = Me.FillMode
            If fill <> current.FillMode Then
                device.RenderState.FillMode = fill
                current.FillMode = fill
            End If
        End If

        If Me.DepthTestEnabled Then
            If Not current.DepthTestEnabled Then
                device.RenderState.DepthBufferEnable = True
            End If
            If Me.DepthWriteEnabled <> current.DepthWriteEnabled Then
                device.RenderState.DepthBufferWriteEnable = Me.DepthWriteEnabled
            End If
            If Me.DepthTestFunction <> current.DepthTestFunction Then
                device.RenderState.DepthBufferFunction = Me.DepthTestFunction
            End If
            current.mode = Me.mode
        Else
            If current.DepthTestEnabled Then
                device.RenderState.DepthBufferEnable = False
                current.DepthTestEnabled = False
            End If
        End If
        Return changed
    End Function
End Structure
