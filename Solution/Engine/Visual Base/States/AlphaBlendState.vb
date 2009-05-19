Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework
Imports System.Runtime.InteropServices

<StructLayout(LayoutKind.Explicit, Size:=4)> _
Public Structure AlphaBlendState
    <FieldOffset(0)> _
    Friend mode As Integer

    Private Shared ReadOnly _None As New AlphaBlendState()
    Private Shared ReadOnly _Alpha As New AlphaBlendState(Blend.SourceAlpha, Blend.InverseSourceAlpha)
    Private Shared ReadOnly _PremodulatedAlpha As New AlphaBlendState(Blend.One, Blend.InverseSourceAlpha)
    Private Shared ReadOnly _AlphaAdditive As New AlphaBlendState(Blend.SourceAlpha, Blend.One)
    Private Shared ReadOnly _Additive As New AlphaBlendState(Blend.One, Blend.One)
    Private Shared ReadOnly _AdditiveSaturate As New AlphaBlendState(Blend.InverseDestinationColor, Blend.One)
    Private Shared ReadOnly _Modulate As New AlphaBlendState(Blend.DestinationColor, Blend.Zero)
    Private Shared ReadOnly _ModulateAdd As New AlphaBlendState(Blend.DestinationColor, Blend.One)
    Private Shared ReadOnly _ModulateX2 As New AlphaBlendState(Blend.DestinationColor, Blend.SourceColor)

    ''' <summary>State that disables Alpha Blending</summary>
    Public Shared ReadOnly Property None() As AlphaBlendState
        Get
            Return _None
        End Get
    End Property

    ''' <summary>State that enables standard Alpha Blending (blending based on the alpha value of the source component, desitination colour is interpolated to the source colour based on source alpha)</summary>
    Public Shared ReadOnly Property Alpha() As AlphaBlendState
        Get
            Return _Alpha
        End Get
    End Property

    ''' <summary>State that enables Premodulated Alpha Blending (Assumes the source colour data has been premodulated with the source alpha value, useful for reducing colour bleeding and accuracy problems at alpha edges)</summary>
    Public Shared ReadOnly Property PremodulatedAlpha() As AlphaBlendState
        Get
            Return _PremodulatedAlpha
        End Get
    End Property

    ''' <summary>State that enables Additive Alpha Blending (blending based on the alpha value of the source component, the desitination colour is added to the source colour modulated by alpha)</summary>
    Public Shared ReadOnly Property AlphaAdditive() As AlphaBlendState
        Get
            Return _AlphaAdditive
        End Get
    End Property

    ''' <summary>State that enables standard Additive Blending (the alpha value is ignored, the desitination colour is added to the source colour)</summary>
    Public Shared ReadOnly Property Additive() As AlphaBlendState
        Get
            Return _Additive
        End Get
    End Property

    ''' <summary>State that enables Additive Saturate Blending (the alpha value is ignored, the desitination colour is added to the source colour, however the source colour is multipled by the inverse of the destination colour, preventing the addition from blowing out to pure white (eg, 0.75 + 0.75 * (1-0.75) = 0.9375))</summary>
    Public Shared ReadOnly Property AdditiveSaturate() As AlphaBlendState
        Get
            Return _AdditiveSaturate
        End Get
    End Property

    ''' <summary>State that enables Modulate (multiply) Blending (the alpha value is ignored, the desitination colour is multipled with the source colour)</summary>
    Public Shared ReadOnly Property Modulate() As AlphaBlendState
        Get
            Return _Modulate
        End Get
    End Property

    ''' <summary>State that enables Modulate Add (multiply+add) Blending (the alpha value is ignored, the desitination colour multipled with the source colour is added to the desitnation colour)</summary>
    Public Shared ReadOnly Property ModulateAdd() As AlphaBlendState
        Get
            Return _ModulateAdd
        End Get
    End Property

    ''' <summary>State that enables Modulate (multiply) Blending, scaled by 2 (the alpha value is ignored, the desitination colour is multipled with the source colour, scaled by two)</summary>
    Public Shared ReadOnly Property ModulateX2() As AlphaBlendState
        Get
            Return _ModulateX2
        End Get
    End Property

    ''' <summary></summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Shared Narrowing Operator CType(ByVal state As Integer) As AlphaBlendState
        Dim value As AlphaBlendState
        value.mode = state
        Return value
    End Operator

    ''' <summary></summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Shared Widening Operator CType(ByVal state As AlphaBlendState) As Integer
        Return state.mode
    End Operator

    ''' <summary></summary>
    ''' <param name="a"></param>
    ''' <param name="b"></param>
    ''' <returns></returns>
    Public Shared Operator =(ByVal a As AlphaBlendState, ByVal b As AlphaBlendState) As Boolean
        Return a.mode = b.mode
    End Operator

    ''' <summary></summary>
    ''' <param name="a"></param>
    ''' <param name="b"></param>
    ''' <returns></returns>
    Public Shared Operator <>(ByVal a As AlphaBlendState, ByVal b As AlphaBlendState) As Boolean
        Return a.mode <> b.mode
    End Operator

    ''' <summary></summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
        If TypeOf obj Is AlphaBlendState Then
            Return DirectCast(obj, AlphaBlendState).mode = Me.mode
        End If
        Return False
    End Function

    ''' <summary>
    ''' Gets the hash code, eqivalent to the internal bitfield
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Overrides Function GetHashCode() As Integer
        Return mode
    End Function


    ''' <summary>Set the render state to no Alpha Blending, resetting all states (This is not equivalent to setting <see cref="Enabled"/> to false, however it has the same effect)</summary>
    Public Sub SetToNoBlending()
        Me.mode = 0
    End Sub
    ''' <summary>Set the render state to standard Alpha Blending (blending based on the alpha value of the source component, desitination colour is interpolated to the source colour based on source alpha)</summary>
    Public Sub SetToAlphaBlending()
        Me.mode = _Alpha.mode
    End Sub
    ''' <summary>Set the render state to Additive Alpha Blending (blending based on the alpha value of the source component, the desitination colour is added to the source colour modulated by alpha)</summary>
    Public Sub SetToAdditiveBlending()
        Me.mode = _Additive.mode
    End Sub
    ''' <summary>Set the render state to Premodulated Alpha Blending (Assumes the source colour data has been premodulated with the inverse of the alpha value, useful for reducing colour bleeding and accuracy problems at alpha edges)</summary>
    Public Sub SetToPremodulatedAlphaBlending()
        Me.mode = _PremodulatedAlpha.mode
    End Sub
    ''' <summary>Set the render state to Additive Alpha Blending (blending based on the alpha value of the source component, the desitination colour is added to the source colour modulated by alpha)</summary>
    Public Sub SetToAlphaAdditiveBlending()
        Me.mode = _AlphaAdditive.mode
    End Sub
    ''' <summary>Set the render state to Additive Saturate Blending (the alpha value is ignored, the desitination colour is added to the source colour, however the source colour is multipled by the inverse of the destination colour, preventing the addition from blowing out to pure white (eg, 0.75 + 0.75 * (1-0.75) = 0.9375))</summary>
    Public Sub SetToAdditiveSaturateBlending()
        Me.mode = _AdditiveSaturate.mode
    End Sub
    ''' <summary>Set the render state to Modulate (multiply) Blending (the alpha value is ignored, the desitination colour is multipled with the source colour)</summary>
    Public Sub SetToModulateBlending()
        Me.mode = _Modulate.mode
    End Sub
    ''' <summary>Set the render state to Modulate Add (multiply+add) Blending (the alpha value is ignored, the desitination colour multipled with the source colour is added to the desitnation colour)</summary>
    Public Sub SetToModulateAddBlending()
        Me.mode = _ModulateAdd.mode
    End Sub
    ''' <summary>Set the render state to Modulate (multiply) Blending, scaled by 2 (the alpha value is ignored, the desitination colour is multipled with the source colour, scaled by two)</summary>
    Public Sub SetToModulateX2Blending()
        Me.mode = _ModulateX2.mode
    End Sub

    ''' <summary>
    ''' Create a alpha blend state with the given source and destination blend modes
    ''' </summary>
    ''' <param name="sourceBlend"></param>
    ''' <param name="destinationBlend"></param>
    Public Sub New(ByVal sourceBlend As Blend, ByVal destinationBlend As Blend)
        mode = 1
        Me.SourceBlend = sourceBlend
        Me.DestinationBlend = destinationBlend
    End Sub

    ''' <summary>
    ''' Gets/Sets if alpha blending is enabled
    ''' </summary>
    Public Property Enabled() As Boolean
        Get
            Return (mode And 1) = 1
        End Get
        Set(ByVal value As Boolean)
            mode = (mode And Not 1) Or (If(value, 1, 0))
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets if separate alpha blending is enabled (Separate alpha blending applies an alternative blend equation to the alpha channel than the RGB channels). See <see cref="BlendOperationAlpha"/>, <see cref="SourceBlendAlpha"/> and <see cref="DestinationBlendAlpha"/>
    ''' </summary>
    Public Property SeparateAlphaBlendEnabled() As Boolean
        Get
            Return (mode And 2) = 2
        End Get
        Set(ByVal value As Boolean)
            mode = (mode And Not 2) Or (If(value, 2, 0))
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets the blending function operation. See <see cref="AlphaBlendState"/> remarks for details
    ''' </summary>
    Public Property BlendOperation() As BlendFunction
        Get
            '1-5
            Return DirectCast((((mode >> 2) And 7) + 1), BlendFunction)
        End Get
        Set(ByVal value As BlendFunction)
            mode = (mode And Not (7 << 2)) Or (7 And (CInt(value) - 1)) << 2
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets the blending function operation, this value only effects the alpha channel and only when <see cref="SeparateAlphaBlendEnabled"/> is true. See <see cref="AlphaBlendState"/> remarks for details
    ''' </summary>
    Public Property BlendOperationAlpha() As BlendFunction
        Get
            Return DirectCast((((mode >> 5) And 7) + 1), BlendFunction)
        End Get
        Set(ByVal value As BlendFunction)
            mode = (mode And Not (7 << 5)) Or (7 And (CInt(value) - 1)) << 5
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets the blending function source (drawn pixel) input multiply value. See <see cref="AlphaBlendState"/> remarks for details
    ''' </summary>
    Public Property SourceBlend() As Blend
        Get
            Return DirectCast(((((mode >> 8) And 15) Xor 1) + 1), Blend)
        End Get
        Set(ByVal value As Blend)
            mode = (mode And Not (15 << 8)) Or (15 And (CInt(value) - 1) Xor 1) << 8
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets the blending function destination (existing pixel) input multiply value. See <see cref="AlphaBlendState"/> remarks for details
    ''' </summary>
    Public Property DestinationBlend() As Blend
        Get
            Return DirectCast((((mode >> 12) And 15) + 1), Blend)
        End Get
        Set(ByVal value As Blend)
            mode = (mode And Not (15 << 12)) Or (15 And (CInt(value) - 1)) << 12
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets the blending function source (drawn pixel) input multiply value, this value only effects the alpha channel and only when <see cref="SeparateAlphaBlendEnabled"/> is true. See <see cref="AlphaBlendState"/> remarks for details
    ''' </summary>
    Public Property SourceBlendAlpha() As Blend
        Get
            Return DirectCast(((((mode >> 16) And 15) Xor 1) + 1), Blend)
        End Get
        Set(ByVal value As Blend)
            mode = (mode And Not (15 << 16)) Or (15 And (CInt(value) - 1) Xor 1) << 16
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets the blending function destination (existing pixel) input multiply value, this value only effects the alpha channel and only when <see cref="SeparateAlphaBlendEnabled"/> is true. See <see cref="AlphaBlendState"/> remarks for details
    ''' </summary>
    Public Property DestinationBlendAlpha() As Blend
        Get
            Return DirectCast((((mode >> 20) And 15) + 1), Blend)
        End Get
        Set(ByVal value As Blend)
            mode = (mode And Not (15 << 20)) Or (15 And (CInt(value) - 1)) << 20
        End Set
    End Property

    Friend Sub ResetState(ByRef current As AlphaBlendState, ByVal device As GraphicsDevice)
        device.RenderState.AlphaBlendEnable = Me.Enabled
        device.RenderState.SeparateAlphaBlendEnabled = Me.SeparateAlphaBlendEnabled
        device.RenderState.BlendFunction = Me.BlendOperation
        device.RenderState.AlphaBlendOperation = current.BlendOperationAlpha
        device.RenderState.SourceBlend = Me.SourceBlend
        device.RenderState.DestinationBlend = Me.DestinationBlend
        device.RenderState.AlphaSourceBlend = Me.SourceBlendAlpha
        device.RenderState.AlphaDestinationBlend = Me.DestinationBlendAlpha
        current.mode = Me.mode
    End Sub

    Friend Function ApplyState(ByRef current As AlphaBlendState, ByVal device As GraphicsDevice) As Boolean
        Dim changed As Boolean = False

        If Me.Enabled Then

            If Not current.Enabled Then
                device.RenderState.AlphaBlendEnable = True
            End If

            If Me.SeparateAlphaBlendEnabled <> current.SeparateAlphaBlendEnabled Then
                device.RenderState.SeparateAlphaBlendEnabled = Me.SeparateAlphaBlendEnabled
            End If

            If Me.BlendOperation <> current.BlendOperation Then
                device.RenderState.BlendFunction = Me.BlendOperation
            End If

            If Me.BlendOperationAlpha <> current.BlendOperationAlpha Then
                device.RenderState.AlphaBlendOperation = current.BlendOperationAlpha
            End If

            If Me.SourceBlend <> current.SourceBlend Then
                device.RenderState.SourceBlend = Me.SourceBlend
            End If

            If Me.DestinationBlend <> current.DestinationBlend Then
                device.RenderState.DestinationBlend = Me.DestinationBlend
            End If

            If Me.SourceBlendAlpha <> current.SourceBlendAlpha Then
                device.RenderState.AlphaSourceBlend = Me.SourceBlendAlpha
            End If

            If Me.DestinationBlendAlpha <> current.DestinationBlendAlpha Then
                device.RenderState.AlphaDestinationBlend = Me.DestinationBlendAlpha
            End If

            current.mode = Me.mode
        Else
            If current.Enabled Then
                device.RenderState.AlphaBlendEnable = False
                current.Enabled = False

                changed = True
            End If
        End If
        Return changed
    End Function
End Structure

