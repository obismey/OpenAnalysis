Public Class Building

    Property Design() As BuildingDesign
        Get

        End Get
        Set(ByVal value As BuildingDesign)

        End Set
    End Property

    Public location As String




    Private _active As Boolean
    Public Property Active() As Boolean
        Get
            Return _active
        End Get
        Set(ByVal value As Boolean)
            _active = value
        End Set
    End Property



End Class
