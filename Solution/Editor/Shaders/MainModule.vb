Imports System
Imports System.Collections.Generic
Imports System.Text
Imports System.IO
Imports System.CodeDom
Imports System.CodeDom.Compiler
Imports System.Text.RegularExpressions
Imports System.Windows.Forms
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics


Module MainModule
    Public Const LineOrColumnNull As UInteger = UInteger.MaxValue


    Private Function ConvertBytoToString(ByVal bytes As Byte()) As String
        Dim converted As New StringBuilder()

        Dim i As Integer
        For i = 0 To bytes.Length - 2
            converted.AppendFormat(String.Format("{0}, ", bytes(i)))
        Next
        converted.AppendFormat(String.Format("{0}" & Chr(10) & "", bytes(i)))

        Return converted.ToString()
    End Function

    Private Sub CompileEffect(ByVal filePath As String)
        System.Console.WriteLine("Compiling effect: " + filePath + "" & Chr(10) & "")

        Dim xboxEffect As CompiledEffect = Effect.CompileEffectFromFile(filePath, Nothing, Nothing, CompilerOptions.None, TargetPlatform.Xbox360)

        Dim windowsEffect As CompiledEffect = Effect.CompileEffectFromFile(filePath, Nothing, Nothing, CompilerOptions.None, TargetPlatform.Windows)

        ' Log error messages
        'System.Console.WriteLine("Xbox:" & Chr(10) & "" + xboxEffect.ErrorsAndWarnings)
        'System.Console.WriteLine("Windows:" & Chr(10) & "" + xboxEffect.ErrorsAndWarnings)

        If xboxEffect.Success AndAlso windowsEffect.Success Then
            Dim bytecode As String = "internal static byte[] Code = new byte[] {" & Chr(10) & "" + "#if XBOX360" & Chr(10) & "" + ConvertBytoToString(xboxEffect.GetEffectCode()) + "#else" & Chr(10) & "" + ConvertBytoToString(windowsEffect.GetEffectCode()) + "#endif" & Chr(10) & "};" & Chr(10) & ""

            Clipboard.SetText(bytecode)

            MessageBox.Show("Effect compiled successfully. Bytecodes copied to the clipboard.", "Success", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
        Else
            MessageBox.Show("Error compiling effect.", "Error", MessageBoxButtons.OK, MessageBoxIcon.None, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly)
        End If

    End Sub

    <STAThread()> _
    Public Sub Main(ByVal args As String())

        Dim openDialog As New OpenFileDialog()
        openDialog.Filter = "Effect Files (*.fx)|*.fx"
        openDialog.Multiselect = False
        If openDialog.ShowDialog() = DialogResult.OK Then
            Dim filePath As String = openDialog.FileName
            'CompileEffect(filePath)

            Dim fname As String = Path.GetFileNameWithoutExtension(filePath)
            Dim save As String = Path.Combine(Path.GetDirectoryName(filePath), fname & ".vb")
            'MsgBox(save)
            IO.File.WriteAllText(save, GenerateCodeForFile(filePath))
            'Clipboard.SetText(GenerateCodeForFile(filePath))
        End If

        ' Console.WriteLine(ex.ToString)
        Console.ReadKey()


    End Sub


    Public Sub Main0(ByVal args As String())

        Dim openFolder As New FolderBrowserDialog
        openFolder.RootFolder = Environment.SpecialFolder.Desktop
        openFolder.ShowDialog()
        Dim folder As String = openFolder.SelectedPath

        Dim additional As String = vbCrLf & vbCrLf & vbCrLf
        additional = "technique Tech0"
        additional = additional & vbCrLf & "{"
        additional = additional & vbCrLf & "     Pass P0"
        additional = additional & vbCrLf & "     {"
        additional = additional & vbCrLf & "           PixelShader = compile ps_3_0 main();"
        additional = additional & vbCrLf & "     }"
        additional = additional & vbCrLf & "}"

        For Each filepath As String In IO.Directory.GetFiles(folder, "*.fx")
            Dim fname As String = Path.GetFileNameWithoutExtension(filepath)
            Dim save As String = Path.Combine(Path.GetDirectoryName(filepath), fname & ".vb")
            IO.File.AppendAllText(filepath, additional)
            IO.File.WriteAllText(save, GenerateCodeForFile(filepath))
        Next



        Console.ReadKey()

    End Sub

    Private Function ParameterIsArray(ByVal parameter As EffectParameter) As Boolean
        Return parameter.Elements.Count <> 0
    End Function

    Private Function GetParameterType(ByVal effectParameter As EffectParameter) As Type
        Dim isArray As Boolean = ParameterIsArray(effectParameter)

        Select Case effectParameter.ParameterClass
            Case EffectParameterClass.MatrixColumns
                Throw New NotSupportedException()
            Case EffectParameterClass.MatrixRows

                If effectParameter.ParameterType = EffectParameterType.Single AndAlso effectParameter.RowCount = 4 AndAlso effectParameter.ColumnCount = 4 Then
                    Return GetType(Matrix)
                End If
                Exit Select
            Case EffectParameterClass.Object

                Select Case effectParameter.ParameterType
                    Case EffectParameterType.String
                        Return GetType(String)
                    Case EffectParameterType.Texture
                        If isArray Then
                            Throw New NotSupportedException("")
                        End If
                        Return GetType(Texture2D)
                    Case EffectParameterType.Texture2D
                        If isArray Then
                            Throw New NotSupportedException("")
                        End If
                        Return GetType(Texture2D)
                    Case EffectParameterType.Texture3D
                        If isArray Then
                            Throw New NotSupportedException("")
                        End If
                        Return GetType(Texture3D)
                    Case EffectParameterType.TextureCube
                        If isArray Then
                            Throw New NotSupportedException("")
                        End If
                        Return GetType(TextureCube)
                End Select
                Exit Select
            Case EffectParameterClass.Scalar

                Select Case effectParameter.ParameterType
                    Case EffectParameterType.Single
                        Return GetType(Single)
                    Case EffectParameterType.Bool
                        Return GetType(Boolean)
                    Case EffectParameterType.Int32
                        Return GetType(Integer)
                End Select
                Exit Select
            Case EffectParameterClass.Struct

                Throw New NotSupportedException("")
            Case EffectParameterClass.Vector
                'TODO: support structs

                Select Case effectParameter.ParameterType
                    Case EffectParameterType.Single
                        Select Case effectParameter.ColumnCount
                            Case 2
                                Return GetType(Vector2)
                            Case 3
                                Return GetType(Vector3)
                            Case 4
                                Return GetType(Vector4)
                        End Select
                        Exit Select
                End Select
                Exit Select
        End Select

        Throw New NotSupportedException("")
    End Function

    Public Function GenerateCodeForFile(ByVal filePath As String) As String
        ' "Create graphics device"
        System.Console.WriteLine("Creating Device...")

        Dim parameters As New PresentationParameters()
        parameters.BackBufferWidth = 1
        parameters.BackBufferHeight = 1

        Dim dummy As New Form()
        Dim device As New GraphicsDevice(GraphicsAdapter.DefaultAdapter, DeviceType.NullReference, dummy.Handle, parameters)

        ' "Create effect"
        System.Console.WriteLine("Compiling effect: " + filePath + "" & Chr(10) & "")

        Dim windowsCompiledEffect As CompiledEffect = Effect.CompileEffectFromFile(filePath, Nothing, Nothing, CompilerOptions.None, TargetPlatform.Windows)
        '  Dim xbox360CompiledEffect As CompiledEffect = Effect.CompileEffectFromFile(filePath, Nothing, Nothing, CompilerOptions.None, TargetPlatform.Xbox360)

        If Not String.IsNullOrEmpty(windowsCompiledEffect.ErrorsAndWarnings) Then
            Dim hasErrors As Boolean = False

            Dim findLineNumberRegEx As New Regex("\.fx\((?<line>\d+)\): ")
            Dim errorStr As String = ""
            For Each errorOrWarning As String In windowsCompiledEffect.ErrorsAndWarnings.Split(New Char() {Chr(10)})
                If String.IsNullOrEmpty(errorOrWarning) Then
                    Continue For
                End If

                Dim match As Match = findLineNumberRegEx.Match(errorOrWarning)
                If match.Success Then
                    Dim lineNumberGroup As Group = match.Groups("line")
                    Dim lineNumber As UInteger = UInteger.Parse(lineNumberGroup.Value)
                    Dim errorOrWarningStripped As String = errorOrWarning.Substring(match.Index + match.Length)

                    Const warningPrefix As String = "warning "
                    If errorOrWarningStripped.StartsWith(warningPrefix) Then
                        GeneratorWarning(4, errorOrWarningStripped.Substring(warningPrefix.Length), lineNumber - 1, LineOrColumnNull)
                    Else
                        hasErrors = True
                        GeneratorError(4, errorOrWarningStripped, lineNumber - 1, LineOrColumnNull)
                        errorStr = errorStr & errorOrWarning
                    End If
                Else
                    GeneratorError(4, errorOrWarning, LineOrColumnNull, LineOrColumnNull)
                    hasErrors = True
                End If
            Next

            If hasErrors Then
                Throw New Exception(errorStr)
            End If
        End If

        Dim windowsEffectCode As Byte() = windowsCompiledEffect.GetEffectCode()
        '  Dim xbox360EffectCode As Byte() = xbox360CompiledEffect.GetEffectCode()

        ' Use the windows code during generation time
        Dim effectInstance As New Effect(device, windowsEffectCode, CompilerOptions.None, Nothing)


        ' "Generate code"

        ' Create some shared code dom objects
        Dim nullExpression As New CodePrimitiveExpression(Nothing)
        Dim codeThisReferenceExpression As New CodeThisReferenceExpression()
        Dim codePropertySetValueReferenceExpression As New CodePropertySetValueReferenceExpression()
        Dim effectParameterTypeReference As New CodeTypeReference(GetType(EffectParameter))
        Dim parametersCollectionReference As New CodeMethodReferenceExpression(codeThisReferenceExpression, "Parameters")


        'TODO: generate header (generated by, "do not modify", generaton date/time)
        ' Create namespace

        Dim codeNamespace As New CodeNamespace("PostProcess")
        codeNamespace.[Imports].Add(New CodeNamespaceImport("Microsoft.Xna.Framework"))
        codeNamespace.[Imports].Add(New CodeNamespaceImport("Microsoft.Xna.Framework.Graphics"))
        'codeNamespace.Imports.Add(new CodeNamespaceImport("EffectGenerator"));

        ' Create effect type
        Dim codeTypeDeclaration As New CodeTypeDeclaration(Path.GetFileNameWithoutExtension(filePath))
        codeTypeDeclaration.BaseTypes.Add(GetType(Microsoft.Xna.Framework.Graphics.Effect))
        codeNamespace.Types.Add(codeTypeDeclaration)

        ' Create static shader code field
        Const EffectCodeFieldName As String = "effectCode"
        Dim effectCodeField As New CodeMemberField(GetType(Byte()), EffectCodeFieldName)
        effectCodeField.Attributes = MemberAttributes.Private Or MemberAttributes.Static
        codeTypeDeclaration.Members.Add(effectCodeField)
        Dim effectCodeVariableReferenceExpression As New CodeVariableReferenceExpression(EffectCodeFieldName)

        ' Create effect code initalizers
        Dim windowsEffectCodeInitalizers As CodeExpression() = New CodeExpression(windowsEffectCode.Length - 1) {}
        For i As Integer = 0 To windowsEffectCodeInitalizers.Length - 1
            windowsEffectCodeInitalizers(i) = New CodePrimitiveExpression(windowsEffectCode(i))
        Next
        '  Dim xbox360EffectCodeInitalizers As CodeExpression() = New CodeExpression(xbox360EffectCode.Length - 1) {}
        '   For i As Integer = 0 To xbox360EffectCodeInitalizers.Length - 1
        'xbox360EffectCodeInitalizers(i) = New CodePrimitiveExpression(xbox360EffectCode(i))
        'Next

        ' Create static constructor to initalize shader code field
        'TODO: Less hacky way to select between 360 and Windows
        Dim staticConstructor As New CodeTypeConstructor()
        staticConstructor.Statements.Add(New CodeSnippetExpression("#if XBOX360 "))
        '  staticConstructor.Statements.Add(New CodeAssignStatement(effectCodeVariableReferenceExpression, New CodeArrayCreateExpression(New CodeTypeReference(GetType(Byte())), xbox360EffectCodeInitalizers)))
        staticConstructor.Statements.Add(New CodeSnippetExpression("#else "))
        staticConstructor.Statements.Add(New CodeAssignStatement(effectCodeVariableReferenceExpression, New CodeArrayCreateExpression(New CodeTypeReference(GetType(Byte())), windowsEffectCodeInitalizers)))
        staticConstructor.Statements.Add(New CodeSnippetExpression("#endif "))
        codeTypeDeclaration.Members.Add(staticConstructor)

        ' Create constructor
        Dim constructor As New CodeConstructor()
        constructor.Attributes = MemberAttributes.[Public]
        Const graphicsDeviceParameterName As String = "graphicsDevice"
        Const effectPoolParameterName As String = "effectPool"
        Dim graphicsDeviceParameter As New CodeParameterDeclarationExpression(GetType(GraphicsDevice), graphicsDeviceParameterName)
        constructor.Parameters.Add(graphicsDeviceParameter)
        constructor.Parameters.Add(New CodeParameterDeclarationExpression(GetType(EffectPool), effectPoolParameterName))
        Dim graphicsDeviceParameterReference As New CodeVariableReferenceExpression(graphicsDeviceParameterName)
        constructor.BaseConstructorArgs.Add(graphicsDeviceParameterReference)
        constructor.BaseConstructorArgs.Add(effectCodeVariableReferenceExpression)
        constructor.BaseConstructorArgs.Add(New CodeFieldReferenceExpression(New CodeTypeReferenceExpression(GetType(CompilerOptions)), "None"))
        constructor.BaseConstructorArgs.Add(New CodeVariableReferenceExpression(effectPoolParameterName))
        codeTypeDeclaration.Members.Add(constructor)
        ' Note: constructor statements are generated during the effect parameter loop

        ' Create simplified constructor (defaults effectPool = null)
        Dim simplifiedConstructor As New CodeConstructor()
        simplifiedConstructor.Attributes = MemberAttributes.[Public]
        simplifiedConstructor.Parameters.Add(graphicsDeviceParameter)
        simplifiedConstructor.ChainedConstructorArgs.Add(graphicsDeviceParameterReference)
        simplifiedConstructor.ChainedConstructorArgs.Add(nullExpression)
        codeTypeDeclaration.Members.Add(simplifiedConstructor)
        For i As Integer = 0 To effectInstance.Parameters.Count - 1

            ' Create a field and property for each effect parameter
            Dim parameter As EffectParameter = effectInstance.Parameters(i)
            Dim parameterType As Type
            Try
                parameterType = GetParameterType(parameter)
            Catch notSupportedException As NotSupportedException
                ' Unable to create a property for this paramter
                GeneratorWarning(4, String.Format("NotSupportedEffectParameter", parameter.Name, notSupportedException.Message), LineOrColumnNull, LineOrColumnNull)
                Continue For
            End Try

            Dim isArray As Boolean = ParameterIsArray(parameter)

            Dim propertyTypeReference As CodeTypeReference
            If isArray Then
                propertyTypeReference = New CodeTypeReference(parameterType.Name + "ParameterCollection")
            Else
                propertyTypeReference = New CodeTypeReference(parameterType)
            End If

            ' Create field
            Dim fieldName As String = "_" + parameter.Name
            Dim memberField As New CodeMemberField(CType(IIf(isArray, propertyTypeReference, effectParameterTypeReference), CodeTypeReference), fieldName)

            codeTypeDeclaration.Members.Add(memberField)
            Dim fieldReference As New CodeFieldReferenceExpression(codeThisReferenceExpression, fieldName)

            ' Create property
            Dim memberProperty As New CodeMemberProperty()
            memberProperty.Type = propertyTypeReference
            memberProperty.Name = parameter.Name
            memberProperty.Attributes = MemberAttributes.[Public] Or MemberAttributes.Final
            codeTypeDeclaration.Members.Add(memberProperty)

            ' Attach summary doc comments to the property if the parameter has the annotation
            Dim sasUiDescriptionAnnotation As EffectAnnotation = parameter.Annotations("SasUiDescription")
            If sasUiDescriptionAnnotation IsNot Nothing Then
                memberProperty.Comments.Add(New CodeCommentStatement("<summary>", True))
                memberProperty.Comments.Add(New CodeCommentStatement(sasUiDescriptionAnnotation.GetValueString(), True))
                memberProperty.Comments.Add(New CodeCommentStatement("</summary>", True))
            End If

            ' Create property get
            memberProperty.HasGet = True
            If isArray Then
                memberProperty.GetStatements.Add(New CodeMethodReturnStatement(fieldReference))
            Else
                memberProperty.GetStatements.Add(New CodeMethodReturnStatement(New CodeMethodInvokeExpression(New CodeMethodReferenceExpression(fieldReference, "GetValue" + parameterType.Name))))
            End If

            If Not isArray Then
                ' Create property set`
                memberProperty.HasSet = True
                memberProperty.SetStatements.Add(New CodeMethodInvokeExpression(fieldReference, "SetValue", codePropertySetValueReferenceExpression))
            End If

            ' Create statement for this field in the constructor
            Dim parameterIndexerExpression As New CodeArrayIndexerExpression(parametersCollectionReference, New CodePrimitiveExpression(parameter.Name))
            If isArray Then
                constructor.Statements.Add(New CodeAssignStatement(fieldReference, New CodeObjectCreateExpression(propertyTypeReference, New CodePropertyReferenceExpression(parameterIndexerExpression, "Elements"))))
            Else
                constructor.Statements.Add(New CodeAssignStatement(fieldReference, parameterIndexerExpression))

                'TODO: constructor.Statements.Add for default values?
            End If
        Next

        'TODO: Generate properties for each technique and pass


        ' Generate source code
        Dim codeProvider As CodeDomProvider = GetCodeProvider()
        Dim writer As New StringWriter()
        codeProvider.GenerateCodeFromNamespace(codeNamespace, writer, Nothing)




        Return writer.ToString()
    End Function

    Public Sub GeneratorError(ByVal level As UInteger, ByVal message As String, ByVal line As UInteger, ByVal column As UInteger)

    End Sub
    Public Sub GeneratorWarning(ByVal level As UInteger, ByVal message As String, ByVal line As UInteger, ByVal column As UInteger)

    End Sub
    Public Function GetCodeProvider() As CodeDomProvider
        Return New Microsoft.VisualBasic.VBCodeProvider
    End Function
End Module
