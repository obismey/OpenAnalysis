Public Class TechTreeNode

    ReadOnly Property Unlocks() As List(Of String)
        Get

        End Get
    End Property

    ReadOnly Property Improves() As List(Of String)
        Get

        End Get
    End Property

    'n%, n , n+
    ReadOnly Property ImprovementLevels() As List(Of String)
        Get

        End Get
    End Property

    ReadOnly Property Parents() As List(Of TechTreeNode)
        Get

        End Get
    End Property
End Class
