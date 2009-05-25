Imports System.Collections.Generic

Public Class DockContainer
    Inherits ControlContainer

    Private datas As SortedList(Of Control, DockState)
    Property DockState(ByVal child As Control) As DockState
        Get
            Return datas(child)
        End Get
        Set(ByVal value As DockState)
            datas(child) = value
        End Set
    End Property

    Protected Friend Overrides Function GetAllowedDrawRect(ByVal vis As Control) As Microsoft.Xna.Framework.Rectangle

    End Function
End Class

