Public Class InstanceDescriptorProviderAttribute
    Inherits BaseAttribute


    Private _instanceDescriptorType As Type
    Public Property InstanceDescriptorType() As Type
        Get
            Return _instanceDescriptorType
        End Get
        Set(ByVal value As Type)
            _instanceDescriptorType = value
        End Set
    End Property


End Class
