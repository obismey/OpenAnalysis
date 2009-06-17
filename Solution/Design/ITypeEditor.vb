Public Interface ITypeEditor
    Sub SetValue(ByVal value As Object)

    Event ValueEdited As ValueEdited


End Interface

Public Delegate Sub ValueEdited(ByVal sender As Object, ByVal value As Object)
