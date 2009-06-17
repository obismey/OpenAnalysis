Imports System.CodeDom
Imports System.Reflection

Public Class CodeDomSerializer

    Private classDecl As CodeTypeDeclaration
    Private methodDecl As CodeMemberMethod
    Shared inc As Integer = 1
    Sub Initialize(ByVal classname As String, ByVal basetype As Type, ByVal member As String)
        classDecl = New CodeTypeDeclaration() With {.Attributes = MemberAttributes.Public, .IsClass = True, .Name = classname, .TypeAttributes = Reflection.TypeAttributes.Public}
        methodDecl = New CodeMemberMethod() With {.Attributes = MemberAttributes.Private, .Name = If(member Is Nothing, "InitializeComponents", member), .ReturnType = Nothing}

    End Sub

    Sub AddMember(ByVal name As String, ByVal visibility As MemberVisibility, ByVal type As Type)
        Dim field As New CodeMemberField
        inc += 1
        field.Name = If(name Is Nothing, type.Name & "_" & inc, name)
        field.Type = New CodeTypeReference(type)
        If visibility = MemberVisibility.Friend Then field.Attributes = MemberAttributes.FamilyOrAssembly
        If visibility = MemberVisibility.Private Then field.Attributes = MemberAttributes.Public
        If visibility = MemberVisibility.Public Then field.Attributes = MemberAttributes.Private

        classDecl.Members.Add(field)
    End Sub

    Sub Assign(ByVal left As String, ByVal right As Object)
        Dim t As Type = right.GetType()

        Dim q = From elt In methodDecl.Statements _
                Where elt.GetType() Is GetType(CodeAssignStatement) _
                Let e As CodeAssignStatement = elt

        Dim assign As New CodeAssignStatement

        If t Is GetType(String) Then
            assign.Left = New CodeSnippetExpression(left)
            assign.Right = New CodePrimitiveExpression(right)

        ElseIf t.IsPrimitive Then
            assign.Left = New CodeSnippetExpression(left)
            assign.Right = New CodePrimitiveExpression(right)
        ElseIf t.IsEnum Then
            assign.Left = New CodeSnippetExpression(left)
            assign.Right = SerializeEnum(right)
        Else
     
            assign.Left = New CodeSnippetExpression(left)
            Dim ser As CodeExpression = SerializeObject(right)
            assign.Right = If(ser Is Nothing, New CodePrimitiveExpression(Nothing), ser)
        End If

        methodDecl.Statements.Add(assign)
    End Sub

    Private Function SerializeObject(ByVal value As Object) As CodeExpression

        Dim desc As Core.InstanceDescriptor = getInstanceDescriptor(value.GetType())
        If desc Is Nothing Then Return Nothing
        If desc.IsComplete Then

            If TypeOf desc.Member Is ConstructorInfo Then
                Dim result As New CodeObjectCreateExpression()
                result.CreateType = New CodeTypeReference(value.GetType())

                For Each elt In desc.Arguments
                    Dim t As Type = elt.GetType()

                    If t Is GetType(String) Then
                        result.Parameters.Add(New CodePrimitiveExpression(elt))

                    ElseIf t.IsPrimitive Then
                        result.Parameters.Add(New CodePrimitiveExpression(elt))

                    ElseIf t.IsEnum Then
                        result.Parameters.Add(SerializeEnum(elt))

                    Else
                        result.Parameters.Add(SerializeObject(elt))
                    End If
                Next

                Return result


            ElseIf TypeOf desc.Member Is MethodInfo Then
                Dim result As New CodeMethodInvokeExpression()
                result.Method = New CodeMethodReferenceExpression(New CodeTypeReferenceExpression(value.GetType()), desc.Member.Name)

                For Each elt In desc.Arguments
                    Dim t As Type = elt.GetType()

                    If t Is GetType(String) Then
                        result.Parameters.Add(New CodePrimitiveExpression(elt))

                    ElseIf t.IsPrimitive Then
                        result.Parameters.Add(New CodePrimitiveExpression(elt))

                    ElseIf t.IsEnum Then
                        result.Parameters.Add(SerializeEnum(elt))

                    Else
                        result.Parameters.Add(SerializeObject(elt))
                    End If
                Next

                Return result
            Else
                Return Nothing
            End If

        Else
            Return Nothing
        End If
    End Function

    Sub AddToCollection(ByVal member As String, ByVal memberMethod As String, ByVal obj As Object)

    End Sub
    Function GetInstanceDescriptor(ByVal t As Type) As Core.InstanceDescriptor
        Dim attr As Core.InstanceDescriptorProviderAttribute = ComponentModel.TypeDescriptor.GetAttributes(t)(GetType(Core.InstanceDescriptorProviderAttribute))
        If attr Is Nothing Then
            Return Nothing
        Else
            Dim descriptorProvider As Core.IInstanceDescriptorProvider = Activator.CreateInstance(attr.InstanceDescriptorType)
            Return descriptorProvider.GetDescriptor(Nothing)
        End If
    End Function

    Private Function SerializeEnum(ByVal value As Object) As CodeExpression
        Dim result As New CodeFieldReferenceExpression
        result.TargetObject = New CodeTypeReferenceExpression(value.GetType())

        Dim name As String = [Enum].GetName(value.GetType(), value)

        result.FieldName = name

        Return result
    End Function

    Sub SerializeComponent(ByVal component As Core.IBaseComponent)
        'Initialize("newClass", component.GetType(), Nothing)

    End Sub


    Function SerializeContainer(ByVal container As Core.IBaseContainer, ByVal name As String) As CodeTypeDeclaration
        Initialize(name, container.GetType(), Nothing)
        For Each prop As ComponentModel.PropertyDescriptor In ComponentModel.TypeDescriptor.GetProperties(container.GetType())
            If Not prop.IsReadOnly Then
                Assign(prop.Name, prop.GetValue(container))
            End If
        Next
        For Each component In container.Components
            AddMember(Nothing, MemberVisibility.Public, component.GetType())
        Next
        classDecl.Members.Add(methodDecl)
        Return classDecl
    End Function
End Class

Public Enum MemberVisibility
    [Public]
    [Private]
    [Friend]
End Enum