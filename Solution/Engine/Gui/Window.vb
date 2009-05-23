Public Class Window
    Dim _root As ControlContainer
    Dim _active As Boolean = True

    Property Root() As ControlContainer
        Get
            Return _root
        End Get
        Set(ByVal value As ControlContainer)
            If _root IsNot Nothing Then
                _root._isRootContainer = False
            End If
            _root = value
            If value IsNot Nothing Then
                _root._isRootContainer = True
            End If
        End Set
    End Property

    Property Active() As Boolean
        Get
            Return _active
        End Get
        Set(ByVal value As Boolean)
            _active = value
        End Set
    End Property

    Public Sub Draw(ByVal elapsedTime As Double, ByVal totalTime As Double)
        If _root IsNot Nothing Then
            _root.Draw(elapsedTime, totalTime)
        End If
    End Sub

    Public Sub Update(ByVal elapsedTime As Double, ByVal totalTime As Double)
        If _root IsNot Nothing Then
            _root.Update(elapsedTime, totalTime)
        End If
    End Sub

    Public Overridable Sub InitializeComponents()

    End Sub

End Class
