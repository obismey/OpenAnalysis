Public Class TableContainer
    Inherits ControlContainer
End Class
Public Class TableRow
    ReadOnly Property Parent() As TableContainer
        Get

        End Get
    End Property
End Class

Public Class TableCell

    ReadOnly Property Parent() As TableRow
        Get

        End Get
    End Property
End Class