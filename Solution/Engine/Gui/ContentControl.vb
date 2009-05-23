Public Class ContentControl
    Inherits Control

    Private _content As Object
    Private _contentTemplate As Template
    Private _padding As Thickness


    Public Property Padding() As Thickness
        Get
            Return _padding
        End Get
        Set(ByVal value As Thickness)
            _padding = value
        End Set
    End Property

    Public Property Content() As Object
        Get
            Return _content
        End Get
        Set(ByVal value As Object)
            _content = value
        End Set
    End Property

    Public Property ContentTeamplate() As Template
        Get
            Return _contentTemplate
        End Get
        Set(ByVal value As Template)
            _contentTemplate = value
        End Set
    End Property


End Class
