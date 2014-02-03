Imports System.Windows.Controls
Imports System.Windows

Public Class Project
    Implements ComponentModel.INotifyPropertyChanged

    Friend Shared _workflow As SqlQueryWorkflow

    Public Shared ReadOnly Property Active As Project
        Get
            Return ProvisionnementModule._ActiveProject
        End Get
    End Property

    Sub New()
        DataModel = New DataModel()
    End Sub

    Public Property ExternalData As DataTable

    Public Property ExternalDataLocalDatabaseConnectionString As String

    Public Property InternalData As DataTable

    Public Property DataModel As DataModel

    Public Property InternalDataSegmentation As DataTable

    Public Property InternalDataModel As DefaultInternalData

    Public Property VbProject As Guid?

    Public Sub RefreshInternalData()
        Dim result = New DataTable

        For Each dpm In DataModel
            Dim c = New DataColumn()
            c.ColumnName = dpm.Name
            If dpm.Type = "Texte" Then
                c.DataType = GetType(String)
            ElseIf dpm.Type = "Nombre" Then
                c.DataType = GetType(Double)
            ElseIf dpm.Type = "Date" Then
                c.DataType = GetType(DateTime)
            End If
            result.Columns.Add(c)
        Next

        Dim q = (From dpm In DataModel
                 Where dpm.SourceColumn IsNot Nothing
                 Select dpm.SourceColumn.ColumnName, dpm.Name).ToList()

        result.BeginLoadData()
        If ExternalData IsNot Nothing Then

            For Each r As DataRow In ExternalData.Rows

                Dim nr = result.NewRow()

                For Each elt In q

                    nr(elt.Name) = r(elt.ColumnName)

                Next

                result.Rows.Add(nr)
            Next

        End If
        result.EndLoadData()

        InternalData = result

        Dim q2 = From dpm In DataModel _
                 Where (dpm.SourceColumn IsNot Nothing) _
                 And (dpm.Usage = "Segmentation" Or dpm.Usage = "Line Of Business" Or dpm.Usage = "Categorie Ministerielle" Or dpm.Usage = "Garantie") _
                 Order By dpm.Priority Select dpm.Name


        Me.InternalDataSegmentation = InternalData.DefaultView.ToTable(True, q2.ToArray())
        Me.InternalDataSegmentation.DefaultView.Sort = String.Join(",", (From s In q2 Select s & " ASC ").ToArray())
        Me.InternalDataSegmentation.TableName = "SegmentTable"

        Dim dset = New DataSet()
        dset.Tables.Add(Me.InternalDataSegmentation)

        ProvisionnementModule._RootTriangleStandard.Children.Clear()

        Dim fnode = New FilterNavigationNode(0)

        ProvisionnementModule._RootTriangleStandard.Children.Add(fnode)

        InternalDataModel = New DefaultInternalData(Me.InternalData, (From c In DataModel Select c.Usage).ToArray(), (From c In DataModel Select "").ToArray())
    End Sub

    Public Sub NotifyExternalDataChanged()
        RaiseEvent PropertyChanged(Me, New ComponentModel.PropertyChangedEventArgs("ExternalData"))
    End Sub

    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
End Class

Public Class FilterNavigationNode
    Inherits Core.UI.NavigationNode

    Private _VariableIndex As Integer
    Private _IsRootNode As Boolean
    Private _Children As Core.UI.NavigationNodeCollection
    Private _DataRow As DataRow

    Sub New(ByVal VariableIndex As Integer)
        ' TODO: Complete member initialization 
        _VariableIndex = VariableIndex
        _IsRootNode = True
        Me.ContextualMenu = New Core.UI.MenuItemCollection()
        Me.ContextualMenu.Add(New Core.UI.MenuItem("Masquer", Sub() Me.Parent.Children.Remove(Me)))
        'Me.ContextualMenu.Add(New Core.UI.MenuItem("Monter", Sub()
        '                                                         Dim TypedParent = TryCast(Me.Parent, FilterNavigationNode)
        '                                                         If TypedParent IsNot Nothing Then
        '                                                             Dim papa = Me.Parent.Parent
        '                                                             Me.Parent.Children.Remove(Me)
        '                                                             Me.Parent.Parent.Children.Remove(TypedParent)
        '                                                             Me.Children.Add(TypedParent)
        '                                                             papa.Children.Add(Me)
        '                                                         End If
        '                                                     End Sub))
        'Me.ContextualMenu.Add(New Core.UI.MenuItem("Descendre", Sub() Me.Parent.Children.Remove(Me)))
    End Sub

    Sub New(ByVal DataRow As DataRow, ByVal VariableIndex As Integer)
        ' TODO: Complete member initialization 
        _DataRow = DataRow
        _VariableIndex = VariableIndex
        _IsRootNode = False
        Me.ContextualMenu.Add(New Core.UI.MenuItem("Masquer", Sub() Me.Parent.Children.Remove(Me)))
    End Sub

    Public Overrides Property Caption As String
        Get
            If _IsRootNode Then
                Return "[" & Project.Active.InternalDataSegmentation.Columns(_VariableIndex).ColumnName & "]"
            Else
                If _DataRow IsNot Nothing Then
                    Return _DataRow(0)
                End If
            End If
            Return ""
            'Return GetFilter()
        End Get
        Set(ByVal value As String)

        End Set
    End Property

    Private Function GetFilter() As String
        Dim TypedParent = TryCast(Me.Parent, FilterNavigationNode)
        If TypedParent Is Nothing Then Return ""
        If _IsRootNode Then
            Return TypedParent.GetFilter()
        Else
            Dim filter = ""
            If Project.Active.InternalDataSegmentation.Columns(_VariableIndex).DataType Is GetType(String) Then
                filter = Project.Active.InternalDataSegmentation.Columns(_VariableIndex).ColumnName & " = '" & CStr(_DataRow(0)) & "'"
            Else
                filter = Project.Active.InternalDataSegmentation.Columns(_VariableIndex).ColumnName & " = " & CDbl(_DataRow(0)).ToString(Globalization.CultureInfo.InvariantCulture)
            End If
            filter = "(" & filter & ")"

            Dim parenfilter = TypedParent.GetFilter()
            Return If(parenfilter = "", filter, parenfilter & " AND " & filter)
        End If
    End Function

    Public Overrides Property Children As Core.UI.NavigationNodeCollection
        Get
            If _Children Is Nothing Then
                _Children = New Core.UI.NavigationNodeCollection(Me)

                If _VariableIndex < (Project.Active.InternalDataSegmentation.Columns.Count - 1) Then
                    Dim fnode = New FilterNavigationNode(_VariableIndex + 1)
                    _Children.Add(fnode)
                End If

                If _IsRootNode Then
                    Project.Active.InternalDataSegmentation.DefaultView.RowFilter = GetFilter()
                    Dim tbl = Project.Active.InternalDataSegmentation.DefaultView.ToTable(True, Project.Active.InternalDataSegmentation.Columns(_VariableIndex).ColumnName)
                    For Each r As DataRow In tbl.Rows
                        Dim fnode = New FilterNavigationNode(r, _VariableIndex)
                        _Children.Add(fnode)
                    Next
                End If
            End If
            Return _Children
        End Get
        Protected Set(ByVal value As Core.UI.NavigationNodeCollection)

        End Set
    End Property


    Public Overrides Property View As Core.UI.INavigationView
        Get
            Dim filter = Me.GetFilter()
            Dim TriangleStandard = TryCast(ProvisionnementModule._RootTriangleStandard.View, ViewTriangleStandard)
            TriangleStandard.SetFilter(filter)
            Return TriangleStandard
        End Get
        Set(ByVal value As Core.UI.INavigationView)

        End Set
    End Property

    Public Overrides Property Views As Core.UI.NavigationViewCollection
        Get
            Return Me.Parent.Views
        End Get
        Protected Set(ByVal value As Core.UI.NavigationViewCollection)

        End Set
    End Property
End Class

Public Interface IModelDataRow

    Function GetValue(ByVal PropertyName As String) As Object
    Function GetValueByUsage(ByVal Usage As String) As Object
End Interface
