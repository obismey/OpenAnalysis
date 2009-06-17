Public Interface IBaseContainer


    Sub AddComponent(ByVal name As String, ByVal comp As IBaseComponent)
    Sub AddComponent(ByVal comp As IBaseComponent)
    Sub RemoveComponent(ByVal comp As IBaseComponent)
    Sub RemoveComponent(ByVal name As String)
    Sub Clear()

    ReadOnly Property Components() As IEnumerable(Of IBaseComponent)
    ReadOnly Property Item(ByVal index As Integer) As IBaseComponent
End Interface
