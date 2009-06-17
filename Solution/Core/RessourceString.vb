Public Structure RessourceString
    Private Path As String
    Private key As String
    Private ressourceType As RessourceType
    Private clrType As Type

    Shared Widening Operator CType(ByVal source As String) As RessourceString

    End Operator
    Shared Widening Operator CType(ByVal source As RessourceString) As String

    End Operator
End Structure