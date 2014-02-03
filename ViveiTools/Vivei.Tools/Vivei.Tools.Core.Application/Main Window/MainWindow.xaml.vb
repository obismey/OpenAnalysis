Imports Vivei.Tools.Core.UI
Imports System.Runtime.InteropServices

Partial Public Class MainWindow


    Private Sub MainWindow_Loaded(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles Me.Loaded
        Dim ng = App.UIService.NavigationGroup.Add("Module Designer")
        ng.Nodes(0).Caption = "Racine"
        ng.Nodes(0).Children.Add("Views")
        ng.Nodes(0).Children.Add("Dashboards")
        ng.Nodes(0).Children.Add("Help")
    End Sub
End Class

Public Class ActionToCommandConverter
    Implements IValueConverter

    Private Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        Dim action As Action = TryCast(value, Action)
        If action Is Nothing Then Return Nothing
        Return New Command(action)
    End Function

    Private Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function

    Private Class Command
        Implements ICommand

        Private _action As System.Action

        Sub New(ByVal action As System.Action)
            ' TODO: Complete member initialization 
            _action = action
        End Sub

        Private Function CanExecute(ByVal parameter As Object) As Boolean Implements System.Windows.Input.ICommand.CanExecute
            Return True
        End Function

        Private Event CanExecuteChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements System.Windows.Input.ICommand.CanExecuteChanged

        Private Sub Execute(ByVal parameter As Object) Implements System.Windows.Input.ICommand.Execute
            _action()
        End Sub
    End Class
End Class

Public Class NodeCollectionToViewCollection
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        Dim node As NavigationNode = value
        If node Is Nothing Then Return Nothing
        If node.Parent Is Nothing Then Return Nothing
        Return (From elt In node.Parent.Children Select elt.View).ToList()
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
End Class