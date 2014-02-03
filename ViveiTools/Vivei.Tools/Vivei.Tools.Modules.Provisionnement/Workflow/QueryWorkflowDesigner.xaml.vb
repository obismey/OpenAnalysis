Imports System.Activities.Presentation.Toolbox
Imports System.Activities.Statements
Imports System.Activities.Presentation

Public Class QueryWorkflowDesigner
    Implements Core.UI.INavigationView

    Dim WfDesigner As WorkflowDesigner
    Dim _Caption As String

    Sub New()

        ' Cet appel est requis par le concepteur.
        InitializeComponent()

        ' Ajoutez une initialisation quelconque après l'appel InitializeComponent().

    End Sub
    'MaxWidth="600" MaxHeight="400"

    Private Sub QueryWorkflowDesigner_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        If WfDesigner Is Nothing Then


            Dim md = New System.Activities.Core.Presentation.DesignerMetadata()
            md.Register()

            Dim toolboxControl = New System.Activities.Presentation.Toolbox.ToolboxControl()

            toolboxControl.Categories.Add(New ToolboxCategory("Control Flow") From { _
             New ToolboxItemWrapper(GetType(DoWhile)), _
             New ToolboxItemWrapper(GetType(ForEach(Of ))), _
             New ToolboxItemWrapper(GetType([If])), _
             New ToolboxItemWrapper(GetType(Parallel)), _
             New ToolboxItemWrapper(GetType(ParallelForEach(Of ))), _
             New ToolboxItemWrapper(GetType(Pick)), _
             New ToolboxItemWrapper(GetType(PickBranch)), _
             New ToolboxItemWrapper(GetType(Sequence)), _
             New ToolboxItemWrapper(GetType(Switch(Of ))), _
             New ToolboxItemWrapper(GetType([While])) _
            })

            toolboxControl.Categories.Add(New ToolboxCategory("Primitives") From { _
             New ToolboxItemWrapper(GetType(Assign)), _
             New ToolboxItemWrapper(GetType(Delay)), _
             New ToolboxItemWrapper(GetType(InvokeMethod)), _
             New ToolboxItemWrapper(GetType(WriteLine)), _
             New ToolboxItemWrapper(GetType(SqlActivity))
            })

            toolboxControl.Categories.Add(New ToolboxCategory("Error Handling") From { _
             New ToolboxItemWrapper(GetType(Rethrow)), _
             New ToolboxItemWrapper(GetType([Throw])), _
             New ToolboxItemWrapper(GetType(TryCatch)) _
            })

            ToolBox.Content = toolboxControl

            WfDesigner = New WorkflowDesigner()

            '        Me.WfDesigner.Context.Services.Publish(Of View.IExpressionEditorService)(New ExpressionEditorService())

            WfDesigner.Load("Modules\ProvisionnementModule\Workflow\template.xaml")

            Me.Designer.Content = WfDesigner.View

            Me.PropertyGrid.Content = WfDesigner.PropertyInspectorView

        End If
    End Sub

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
End Class

<System.ComponentModel.Designer(GetType(SqlQueryActivityDesigner))> _
Public Class SqlActivity
    Inherits System.Activities.CodeActivity


    Public Property SqlCode As System.Activities.InArgument(Of String)

    Protected Overrides Sub Execute(ByVal context As System.Activities.CodeActivityContext)

    End Sub
End Class