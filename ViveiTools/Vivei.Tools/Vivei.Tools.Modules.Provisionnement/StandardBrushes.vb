Imports System.Data
Imports System.Windows.Media
Imports System.Windows


Public Class StandardBrushes

    Shared _Items As Collections.IEnumerable

    Public Shared ReadOnly Property Items As IEnumerable
        Get
            If _Items Is Nothing Then


                Dim q = From i In Enumerable.Range(0, 4), j In Enumerable.Range(0, 4), k In Enumerable.Range(0, 4)
                        Select New SolidColorBrush(Color.FromScRgb(1.0F, (1 / 3) * i, (1 / 3) * j, (1 / 3) * k))


                'Dim q = From i In Enumerable.Range(0, 4), j In Enumerable.Range(0, 4), k In Enumerable.Range(0, 4)
                '        Select New NamedObject(i & j & k, New SolidColorBrush(Color.FromScRgb(1.0F, (1 / 3) * i, (1 / 3) * j, (1 / 3) * k)))

                _Items = q.ToList()

                'Return _Items
                'Dim props = GetType(Brushes).GetProperties(Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static)
                '_Items = (From p In props Select New NamedObject(p.Name, p.GetValue(Nothing, Nothing))).ToList()
                '_Items = (From i As NamedObject In _Items _
                '          Order By CType(i.Value, SolidColorBrush).Color.R, _
                '          CType(i.Value, SolidColorBrush).Color.G, _
                '          CType(i.Value, SolidColorBrush).Color.B Select i).ToList()
            End If
            Return _Items
        End Get
    End Property
End Class

Public Class StandardAlignment

    Shared _Horizontal As Collections.IEnumerable
    Shared _Vertical As Collections.IEnumerable

    Public Shared ReadOnly Property Horizontal As IEnumerable
        Get
            If _Horizontal Is Nothing Then
                _Horizontal = (From elt In [Enum].GetValues(GetType(HorizontalAlignment)) Select New NamedObject(elt.ToString(), elt)).ToList()
            End If
            Return _Horizontal
        End Get
    End Property

    Public Shared ReadOnly Property Vertical As IEnumerable
        Get
            If _Vertical Is Nothing Then
                _Vertical = (From elt In [Enum].GetValues(GetType(VerticalAlignment)) Select New NamedObject(elt.ToString(), elt)).ToList()
            End If
            Return _Vertical
        End Get
    End Property
End Class



Public Class StandardBorderThickness

    Shared _Items As Collections.IEnumerable

    Public Shared ReadOnly Property Items As IEnumerable
        Get
            If _Items Is Nothing Then
                _Items = (From elt In Enumerable.Range(0, 21) Select New NamedObject(elt.ToString(), New Thickness(elt))).ToList()
                '_Items = Enumerable.Range(0, 21).ToList()
            End If
            Return _Items
        End Get
    End Property
   
End Class

Public Class StandardCornerRadius

    Shared _Items As Collections.IEnumerable

    Public Shared ReadOnly Property Items As IEnumerable
        Get
            If _Items Is Nothing Then
                _Items = (From elt In Enumerable.Range(0, 21) Select New NamedObject(elt.ToString(), New CornerRadius(elt))).ToList()
                '_Items = Enumerable.Range(0, 21).ToList()
            End If
            Return _Items
        End Get
    End Property

End Class