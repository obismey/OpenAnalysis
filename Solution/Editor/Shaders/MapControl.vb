Imports Engine

Public Class MapControl
    Sub New()


        ' This call is required by the Windows Form Designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        ComboBox1.Items.Add("None")
        For Each e In [Enum].GetNames(GetType(KnownSemantics))
            ComboBox1.Items.Add(e)
        Next


    End Sub
End Class
