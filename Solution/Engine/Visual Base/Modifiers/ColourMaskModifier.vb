Public Class ColourMaskModifier


    Private _alpha As Boolean
    Public Property Alpha() As Boolean
        Get
            Return _alpha
        End Get
        Set(ByVal value As Boolean)
            _alpha = value
        End Set
    End Property


    Private _blue As Boolean
    Public Property Blue() As Boolean
        Get
            Return _blue
        End Get
        Set(ByVal value As Boolean)
            _blue = value
        End Set
    End Property


    Private _green As Boolean
    Public Property Green() As Boolean
        Get
            Return _green
        End Get
        Set(ByVal value As Boolean)
            _green = value
        End Set
    End Property


    Private _red As Boolean
    Public Property Red() As Boolean
        Get
            Return _red
        End Get
        Set(ByVal value As Boolean)
            _red = value
        End Set
    End Property


    Private _enabled As Boolean
    Public Property Enabled() As Boolean
        Get
            Return _enabled
        End Get
        Set(ByVal value As Boolean)
            _enabled = value
        End Set
    End Property



    Shared ReadOnly Property Current() As ColourMaskModifier
        Get

        End Get
    End Property



End Class
