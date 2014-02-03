Imports System.Data
Imports System.Windows.Media
Imports System.Windows


Public Class StantardFonts

    Shared _Families As Collections.IEnumerable
    Shared _Weights As Collections.IEnumerable
    Shared _Styles As Collections.IEnumerable
    Shared _Stretchs As Collections.IEnumerable

    Public Shared ReadOnly Property Families As IEnumerable
        Get
            Return Fonts.SystemFontFamilies
            If _Families Is Nothing Then
                _Families = Fonts.SystemFontFamilies.Select(Function(f) New NamedObject(f.Source, f)).ToList()
            End If
            Return _Families
        End Get
    End Property
    Public Shared ReadOnly Property Weights As IEnumerable
        Get
            Return New FontWeight() {FontWeights.Normal, FontWeights.Bold}
            'If _Weights Is Nothing Then
            '    Dim props = GetType(FontWeights).GetProperties(Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static)
            '    _Weights = (From p In props Select New NamedObject(p.Name, p.GetValue(Nothing, Nothing))).ToList()
            'End If
            'Return _Weights
        End Get
    End Property
    Public Shared ReadOnly Property Styles As IEnumerable
        Get
            Return New FontStyle() {FontStyles.Normal, FontStyles.Italic}
            'If _Styles Is Nothing Then
            '    Dim props = GetType(FontStyles).GetProperties(Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static)
            '    _Styles = (From p In props Select New NamedObject(p.Name, p.GetValue(Nothing, Nothing))).ToList()
            'End If
            'Return _Styles
        End Get
    End Property
    Public Shared ReadOnly Property Stretchs As IEnumerable
        Get
            If _Stretchs Is Nothing Then
                Dim props = GetType(FontStretches).GetProperties(Reflection.BindingFlags.Public Or Reflection.BindingFlags.Static)
                _Stretchs = (From p In props Select New NamedObject(p.Name, p.GetValue(Nothing, Nothing))).ToList()
            End If
            Return _Stretchs
        End Get
    End Property
End Class
