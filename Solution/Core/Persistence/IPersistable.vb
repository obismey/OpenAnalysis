Public Interface IPersistable
    Sub Save(ByVal context As PersistenceContext)
    Sub Load(ByVal context As PersistenceContext)
    Property UniqueID() As Guid
End Interface
