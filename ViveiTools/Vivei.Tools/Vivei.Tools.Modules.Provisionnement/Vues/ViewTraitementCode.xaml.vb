Imports Vivei.Tools.Core.UI
Imports System.CodeDom

Public Class ViewTraitementCode
    Implements INavigationView

    Dim _Caption As String
    Dim _SharpDevelopProcess As Process


    Public Property Caption As String Implements Core.UI.INavigationView.Caption
        Get
            Return Me._Caption
        End Get
        Set(ByVal value As String)
            Me._Caption = value
        End Set
    End Property

    Public Property Icone As String Implements Core.UI.INavigationView.Icone
        Get

        End Get
        Set(ByVal value As String)

        End Set
    End Property


    Private Sub ModifyButton_Click(ByVal sender As System.Object, ByVal e As System.Windows.RoutedEventArgs) Handles ModifyButton.Click
        '    Return

        Dim datafolder = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules\ProvisionnementModule\Data\")

        If Project.Active.VbProject Is Nothing Then
            Project.Active.VbProject = Guid.NewGuid()
            IO.Directory.CreateDirectory(IO.Path.Combine(datafolder, Project.Active.VbProject.Value.ToString().Replace("-", "")))

            Dim vbprojecttemplate = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules\ProvisionnementModule\VbProjectTemplate.xml")
            'Dim x = XElement.Load(vbprojecttemplate)
            'x.<PropertyGroup>()

            IO.File.Copy(vbprojecttemplate, IO.Path.Combine(datafolder, Project.Active.VbProject.Value.ToString().Replace("-", ""), "ProjectCode.vbproj"))

            Dim corefilename = "Core.vb"
            Dim corefilepath = IO.Path.Combine(IO.Path.Combine(datafolder, Project.Active.VbProject.Value.ToString().Replace("-", "")), corefilename)
            IO.File.AppendAllText(corefilepath, GetModelCode())

            Dim corex = XElement.Load(IO.Path.Combine(datafolder, Project.Active.VbProject.Value.ToString().Replace("-", ""), "ProjectCode.vbproj"))
            Dim corecodesection = corex.Elements.ElementAt(6)
            Dim corexcompile = New XElement(XName.Get("Compile", "http://schemas.microsoft.com/developer/msbuild/2003"))
            corexcompile.SetAttributeValue("Include", corefilename)
            corecodesection.Add(corexcompile)
            corex.Save(IO.Path.Combine(datafolder, Project.Active.VbProject.Value.ToString().Replace("-", ""), "ProjectCode.vbproj"))
        End If

        Dim projectfile = IO.Path.Combine(datafolder, Project.Active.VbProject.Value.ToString().Replace("-", ""), "ProjectCode.vbproj")
        Dim projectfolder = IO.Path.Combine(datafolder, Project.Active.VbProject.Value.ToString().Replace("-", ""))

        Dim path = IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Modules\SharpDevelopModule\4.3\bin\SharpDevelop.exe")
        Dim filename = Guid.NewGuid().ToString().Replace("-", "") & ".vb"
        Dim filepath = IO.Path.Combine(projectfolder, filename)
        IO.File.AppendAllText(filepath, GetDefaultCode())

        Dim x = XElement.Load(projectfile)
        Dim codesection = x.Elements.ElementAt(6)
        Dim xcompile = New XElement(XName.Get("Compile", "http://schemas.microsoft.com/developer/msbuild/2003"))
        xcompile.SetAttributeValue("Include", filename)

        codesection.Add(xcompile)
        x.Save(projectfile)
        ' Process.Start(path, """" & filepath & """")
        If Me._SharpDevelopProcess Is Nothing Then
            Me._SharpDevelopProcess = Process.Start(path, """" & projectfile & """")
        Else
            If Me._SharpDevelopProcess.HasExited Then
                Me._SharpDevelopProcess = Process.Start(path, """" & projectfile & """")
            Else
                Process.Start(path, """" & projectfile & """")
            End If
        End If

    End Sub

    Private Sub ViewTraitementCode_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        CodeViewer.SyntaxHighlighting = ICSharpCode.AvalonEdit.Highlighting.HighlightingManager.Instance.GetDefinition("VBNET")
    End Sub

    Public Function GetDefaultCode() As String
        Dim cu = New CodeCompileUnit()

        Dim ns = New CodeNamespace("Vivei.Tools.Modules.Provisionnement.Dashboard")
        cu.Namespaces.Add(ns)

        ns.Imports.Add(New CodeNamespaceImport("Vivei.Tools.Modules.Provisionnement.Model.Interfaces"))
        ns.Imports.Add(New CodeNamespaceImport("Vivei.Tools.Modules.Provisionnement"))

        Dim ct = New CodeTypeDeclaration("Traitement")
        ns.Types.Add(ct)

        ct.IsClass = True
        ct.Attributes = MemberAttributes.Public Or MemberAttributes.Final


        Dim meth = New CodeMemberMethod()
        meth.Name = "Compute"
        meth.Attributes = MemberAttributes.Public Or MemberAttributes.Final

        meth.Parameters.Add( _
            New CodeParameterDeclarationExpression("IInternalData", "data"))

        meth.Parameters.Add( _
            New CodeParameterDeclarationExpression("IDashboardData", "dashboard"))

        ct.Members.Add(meth)

        Dim provider = New VBCodeProvider()

        Dim str = New IO.StringWriter()

        provider.GenerateCodeFromCompileUnit(cu, str, New Compiler.CodeGeneratorOptions())

        Return str.ToString()

    End Function

    Public Function GetModelCode() As String
        Dim cu = New CodeCompileUnit()

        Dim ns = New CodeNamespace("Vivei.Tools.Modules.Provisionnement.Dashboard")
        cu.Namespaces.Add(ns)

        ns.Imports.Add(New CodeNamespaceImport("Vivei.Tools.Modules.Provisionnement.Model.Interfaces"))
        ns.Imports.Add(New CodeNamespaceImport("Vivei.Tools.Modules.Provisionnement"))
        ns.Imports.Add(New CodeNamespaceImport("System.Collections.Generic"))
        ns.Imports.Add(New CodeNamespaceImport("System"))

        Dim ct = New CodeTypeDeclaration("IProjectInternalData")
        ns.Types.Add(ct)

        ct.IsInterface = True
        ct.Attributes = MemberAttributes.Public

        Dim meth = New CodeMemberMethod()
        meth.Name = "Select"
        meth.Attributes = MemberAttributes.Public
        meth.ReturnType = New CodeTypeReference("IEnumerable", New CodeTypeReference("IProjectInternalDataRow"))

        ct.Members.Add(meth)

        Dim prop = New CodeMemberProperty()
        prop.Name = "InternalData"
        prop.HasGet = True
        prop.HasSet = False
        prop.Type = New CodeTypeReference("IInternalData")

        ct.Members.Add(prop)

        Dim ctr = New CodeTypeDeclaration("IProjectInternalDataRow")
        ns.Types.Add(ctr)

        ctr.IsInterface = True
        ctr.Attributes = MemberAttributes.Public
        ctr.BaseTypes.Add("IInternalDataRow")

        'Dim meth = New CodeMemberMethod()
        'meth.Name = "Compute"
        'meth.Attributes = MemberAttributes.Public Or MemberAttributes.Final
        For Each p In Project.Active.DataModel
            If Not String.IsNullOrEmpty(p.Usage) Then

                Dim pprop = New CodeMemberProperty()

                pprop.HasGet = True
                pprop.HasSet = False

                pprop.Name = p.Usage

                If p.Type = "Texte" Then
                    pprop.Type = New CodeTypeReference(GetType(String))
                ElseIf p.Type = "Nombre" Then
                    pprop.Type = New CodeTypeReference(GetType(Double))
                ElseIf p.Type = "Date" Then
                    pprop.Type = New CodeTypeReference(GetType(Date))
                End If

                ctr.Members.Add(pprop)
            End If
        Next
        'meth.Parameters.Add( _
        '    New CodeParameterDeclarationExpression("IInternalData", "data"))

        'meth.Parameters.Add( _
        '    New CodeParameterDeclarationExpression("IDashboardData", "dashboard"))

        'ct.Members.Add(meth)

        Dim provider = New VBCodeProvider()

        Dim str = New IO.StringWriter()

        provider.GenerateCodeFromCompileUnit(cu, str, New Compiler.CodeGeneratorOptions())

        Return str.ToString()

    End Function
End Class
