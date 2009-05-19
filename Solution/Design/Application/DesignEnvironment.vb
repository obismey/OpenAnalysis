Public NotInheritable Class DesignEnvironment
    Implements IServiceProvider

    Private Function GetService(ByVal serviceType As System.Type) As Object Implements System.IServiceProvider.GetService

    End Function

    Public Function GetService(Of T)() As T
        Return GetService(GetType(T))
    End Function
End Class
