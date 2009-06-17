Imports System.Reflection

Public Class InstanceDescriptor


    Private _member As MemberInfo
    Public Property Member() As MemberInfo
        Get
            Return _member
        End Get
        Set(ByVal value As MemberInfo)
            _member = value
        End Set
    End Property



    Private _arguments As ICollection
    Public Property Arguments() As ICollection
        Get
            Return _arguments
        End Get
        Set(ByVal value As ICollection)
            _arguments = value
        End Set
    End Property


    Private _iscomplete As Boolean
    Public Property IsComplete() As Boolean
        Get
            Return _iscomplete
        End Get
        Set(ByVal value As Boolean)
            _iscomplete = value
        End Set
    End Property






End Class
