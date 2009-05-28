Imports Microsoft.Xna.Framework.Graphics

Public Class BaseMaterial

    Dim _blend As Boolean? = Nothing
    Dim _srcBlend As Blend? = Nothing
    Dim _destBlend As Blend? = Nothing
    Dim _cull As CullMode? = Nothing
    Dim _depthw As Boolean? = Nothing
    Dim _depthc As Boolean? = Nothing
    Dim _atest As Boolean? = Nothing
    Dim _aref As Single? = Nothing
    Dim _afunc As CompareFunction? = Nothing

    Overridable Property Blend() As Boolean?
        Get
            Return _blend
        End Get
        Set(ByVal value As Boolean?)
            _blend = value
        End Set
    End Property
    Overridable Property SourceBlend() As Blend?
        Get
            Return _srcBlend
        End Get
        Set(ByVal value As Blend?)
            _srcBlend = value
        End Set
    End Property
    Overridable Property DestinationBlend() As Blend?
        Get
            Return _destBlend
        End Get
        Set(ByVal value As Blend?)
            _destBlend = value
        End Set
    End Property
    Overridable Property Culling() As CullMode?
        Get
            Return _cull
        End Get
        Set(ByVal value As CullMode?)
            _cull = value
        End Set
    End Property
    Overridable Property DepthEnable() As Boolean?
        Get
            Return _depthc
        End Get
        Set(ByVal value As Boolean?)
            _depthc = value
        End Set
    End Property
    Overridable Property DepthWrite() As Boolean?
        Get
            Return _depthw
        End Get
        Set(ByVal value As Boolean?)
            _depthw = value
        End Set
    End Property
    Overridable Property AlphaTestEnable() As Boolean?
        Get
            Return _atest
        End Get
        Set(ByVal value As Boolean?)
            _atest = value
        End Set
    End Property
    Overridable Property AlphaRef() As Single?
        Get
            Return _aref
        End Get
        Set(ByVal value As Single?)
            _aref = value
        End Set
    End Property
    Overridable Property AlphaFunction() As CompareFunction?
        Get
            Return _afunc
        End Get
        Set(ByVal value As CompareFunction?)
            _afunc = value
        End Set
    End Property

End Class
