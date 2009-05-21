Public Class BaseProperty

    Private _loadableFromDb As Boolean
    Private _supportChangeNotification As Boolean
    Private _multiLingue As Boolean



    ReadOnly Property Type() As Type
        Get

        End Get
    End Property
    ReadOnly Property DbKey() As String
        Get

        End Get
    End Property

    Public Property LoadableFromDb() As Boolean
        Get
            Return _loadableFromDb
        End Get
        Set(ByVal value As Boolean)
            _loadableFromDb = value
        End Set
    End Property

    Public Property Multilingue() As Boolean
        Get
            Return _multiLingue
        End Get
        Set(ByVal value As Boolean)
            _multiLingue = value
        End Set
    End Property

    Public Property SupportChangeNotification() As Boolean
        Get
            Return _supportChangeNotification
        End Get
        Set(ByVal value As Boolean)
            _supportChangeNotification = value
        End Set
    End Property


End Class
