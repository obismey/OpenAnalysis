Public Class Window
    Implements Core.IUpdatable
    Implements IDrawable

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

    Public Overridable Sub InitializeComponents()

    End Sub

    Public Sub Update(ByVal timeElapsed As Double?, ByVal totalTimeElapsed As Double?) Implements Core.IUpdatable.Update
        If _root IsNot Nothing Then
            _root.Update(timeElapsed, totalTimeElapsed)
        End If
    End Sub

    Public Sub Draw(ByVal context As DrawContext, ByVal timeElapsed As Double?, ByVal totalTimeElapsed As Double?) Implements IDrawable.Draw
        If _root IsNot Nothing Then
            _root.Draw(timeElapsed, totalTimeElapsed)
        End If
    End Sub
End Class
