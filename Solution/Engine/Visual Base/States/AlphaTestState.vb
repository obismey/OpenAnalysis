Imports System
Imports System.Collections.Generic
Imports System.Text
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework
Imports System.Runtime.InteropServices

<StructLayout(LayoutKind.Explicit, Size:=2)> _
Public Structure AlphaTestState
    <FieldOffset(0)> _
     Friend mode As UShort

    ''' <summary></summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Shared Narrowing Operator CType(ByVal state As UShort) As AlphaTestState
        Dim value As AlphaTestState
        value.mode = state
        Return value
    End Operator

    ''' <summary></summary>
    ''' <param name="state"></param>
    ''' <returns></returns>
    Public Shared Widening Operator CType(ByVal state As AlphaTestState) As UShort
        Return state.mode
    End Operator

    ''' <summary></summary>
    ''' <param name="a"></param>
    ''' <param name="b"></param>
    ''' <returns></returns>
    Public Shared Operator =(ByVal a As AlphaTestState, ByVal b As AlphaTestState) As Boolean
        Return a.mode = b.mode
    End Operator

    ''' <summary></summary>
    ''' <param name="a"></param>
    ''' <param name="b"></param>
    ''' <returns></returns>
    Public Shared Operator <>(ByVal a As AlphaTestState, ByVal b As AlphaTestState) As Boolean
        Return a.mode <> b.mode
    End Operator

    ''' <summary></summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
        If TypeOf obj Is AlphaTestState Then
            Return DirectCast(obj, AlphaTestState).mode = Me.mode
        End If
        Return False
    End Function

    ''' <summary>
    ''' Gets the hash code (direct copy of the internal bitfield value)
    ''' </summary>
    ''' <returns></returns>
    Public Overloads Overrides Function GetHashCode() As Integer
        Return mode
    End Function

    ''' <summary>
    ''' Gets/Sets if alpha testing is enabled
    ''' </summary>
    Public Property Enabled() As Boolean
        Get
            Return (mode And 1) = 1
        End Get
        Set(ByVal value As Boolean)
            mode = CUShort(((mode And Not 1) Or (If(value, 1, 0))))
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets the alpha testing comparison function
    ''' </summary>
    Public Property AlphaTestFunction() As CompareFunction
        Get
            Return DirectCast(((Not ((mode >> 1)) And 7) + 1), CompareFunction)
        End Get
        Set(ByVal value As CompareFunction)
            mode = CUShort(((CInt(mode) And Not (7 << 1)) Or (7 And (Not (CInt(value) - 1))) << 1))
        End Set
    End Property

    ''' <summary>
    ''' Gets/Sets the reference value used in the alpha testing comparison
    ''' </summary>
    Public Property ReferenceAlpha() As Byte
        Get
            Return CByte((((mode And (255 << 8)) >> 8)))
        End Get
        Set(ByVal value As Byte)
            mode = CUShort(((mode And Not (255 << 8)) Or (CInt(value) << 8)))
        End Set
    End Property

    Friend Sub ResetState(ByRef current As AlphaTestState, ByVal device As GraphicsDevice)
        device.RenderState.AlphaTestEnable = Me.Enabled
        device.RenderState.AlphaFunction = Me.AlphaTestFunction
        device.RenderState.ReferenceAlpha = Me.ReferenceAlpha

        current.mode = Me.mode
    End Sub

    Friend Function ApplyState(ByRef current As AlphaTestState, ByVal device As GraphicsDevice) As Boolean
        Dim changed As Boolean = False
        If Me.Enabled Then
            If Not current.Enabled Then
                device.RenderState.AlphaTestEnable = True
            End If

            If Me.AlphaTestFunction <> current.AlphaTestFunction Then
                device.RenderState.AlphaFunction = Me.AlphaTestFunction
            End If

            If Me.ReferenceAlpha <> current.ReferenceAlpha Then
                device.RenderState.ReferenceAlpha = Me.ReferenceAlpha
            End If

            current.mode = Me.mode
        Else
            If current.Enabled Then
                device.RenderState.AlphaTestEnable = False
                current.Enabled = False
            End If
        End If
        Return changed
    End Function
End Structure


