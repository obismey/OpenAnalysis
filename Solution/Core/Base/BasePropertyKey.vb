Public Class BasePropertyKey

    Public Shared Function Resgister(Of T)(ByVal name As String, ByVal [readonly] As Boolean, ByVal ownerType As Type) As BaseProperty(Of T)
        If Properties Is Nothing Then
            Properties = New Dictionary(Of Type, Dictionary(Of String, Object))
        End If
        If Not Properties.ContainsKey(ownerType) Then
            Properties.Add(ownerType, New Dictionary(Of String, Object))
        End If

        Dim props As Dictionary(Of String, Object) = Properties(ownerType)
        If props.ContainsKey(name) Then
            Throw New Exception("Bad Property Registration")
        Else
            Dim result As New BaseProperty(Of T)
            props.Add(name, result)
            Return result
        End If
    End Function
    Public Shared Function Resgister(Of T)(ByVal name As String, ByVal ownerType As Type) As BaseProperty(Of T)
        Return Resgister(Of T)(name, False, ownerType)
    End Function

    Private Shared Properties As Dictionary(Of Type, Dictionary(Of String, Object))
End Class
