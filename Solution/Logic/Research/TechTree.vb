Public Class TechTree



    Private _field As ResearchField
    Public Property Field() As ResearchField
        Get
            Return _field
        End Get
        Set(ByVal value As ResearchField)
            _field = value
        End Set
    End Property

    ReadOnly Property StartNodes() As List(Of TechTreeNode)
        Get

        End Get
    End Property

End Class
