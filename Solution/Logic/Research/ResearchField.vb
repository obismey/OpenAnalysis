Public Class ResearchField


    Private _category As ResearchCategory
    Public Property Category() As ResearchCategory
        Get
            Return _category
        End Get
        Set(ByVal value As ResearchCategory)
            _category = value
        End Set
    End Property

    Public Name As String

    Public Description As String


End Class

<Flags()> _
Public Enum ResearchCategory
    Military = 2
    Civilian = 4
    Fundamental = 8
End Enum
