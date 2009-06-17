Public Class RessourceKeyAttribute
    Inherits BaseAttribute


    Private _ressourceType As RessourceType
    Public Property RessourceType() As RessourceType
        Get
            Return _ressourceType
        End Get
        Set(ByVal value As RessourceType)
            _ressourceType = value
        End Set
    End Property

    Private _type As Type
    Public Property Type() As Type
        Get
            Return _type
        End Get
        Set(ByVal value As Type)
            _type = value
        End Set
    End Property



End Class
