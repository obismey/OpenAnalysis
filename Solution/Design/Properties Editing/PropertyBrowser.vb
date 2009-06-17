Imports System.ComponentModel
Imports System.Windows.Forms

Public Class PropertyBrowser


    Public Shared editorWidth As Integer = 150
    Public Shared editorleft As Integer = 100

    Private _selected As Object
    Public Property Selected() As Object
        Get
            Return _selected
        End Get
        Set(ByVal value As Object)
            _selected = value
            Generate()
        End Set
    End Property

    Function GeneratePage(ByVal category As String, ByVal props As PropertyDescriptor()) As TabPage
        Dim page As New TabPage
        page.Text = category
        Dim panel As New FlowLayoutPanel
        panel.Dock = DockStyle.Fill
        page.Controls.Add(panel)

        'Dim maxwidth As Integer = 0
        For Each prop In props
            Dim propertyPanel As New PropertyControl
            propertyPanel.Tag = prop

            Dim namelbl As New Label With {.AutoSize = True, .Top = 5, .Left = 5}
            namelbl.Text = prop.Name

            'maxwidth = Math.Max(maxwidth, namelbl.Width)
            propertyPanel.Controls.Add(namelbl)

            
            Dim valuectrl As Control = GetEditor(AddressOf propertyPanel.ValueEdited, prop)
            valuectrl.Width = editorWidth
            valuectrl.Left = editorleft
            valuectrl.Top = 5
            propertyPanel.Controls.Add(valuectrl)


            propertyPanel.AutoSize = True
            panel.Controls.Add(propertyPanel)

            valuectrl.Left = valuectrl.Parent.Width - 5 - valuectrl.Width
            valuectrl.Anchor = AnchorStyles.Top Or AnchorStyles.Right
            propertyPanel.Anchor = AnchorStyles.Top Or AnchorStyles.Left Or AnchorStyles.Right

            'propertyPanel.BorderStyle = Windows.Forms.BorderStyle.FixedSingle
        Next

        Return page
    End Function

    Sub Generate()
        TabControl1.TabPages.Clear()
        If _selected IsNot Nothing Then

            Dim q = From e In TypeDescriptor.GetProperties(_selected).Cast(Of PropertyDescriptor)() _
                     Where Not e.IsReadOnly And e.IsBrowsable _
                  Group By category = e.Category Into props = Group

            For Each e In q
                TabControl1.TabPages.Add(GeneratePage(If(String.IsNullOrEmpty(e.category), "(Default)", e.category), e.props.ToArray()))
            Next
        End If
    End Sub

    Function GetEditor(ByVal editionFunction As Design.ValueEdited, ByVal prop As PropertyDescriptor) As Control
        If prop.PropertyType.IsEnum Then
            Dim result As New EnumEditor()
            For Each s In [Enum].GetNames(prop.PropertyType)
                result.Items.Add(s)
            Next
            result.DropDownStyle = ComboBoxStyle.DropDownList
            result.SetValue(prop.GetValue(_selected))
            AddHandler result.ValueEdited, editionFunction
            Return result


        ElseIf prop.PropertyType Is GetType(Boolean) Then
            Dim result As New BooleanEditor
            result.SetValue(prop.GetValue(_selected))
            AddHandler result.ValueEdited, editionFunction
            Return result


        ElseIf prop.PropertyType.IsPrimitive Then
            Dim result As New NumericEditor(prop.PropertyType)
            result.SetValue(prop.GetValue(_selected))
            AddHandler result.ValueEdited, editionFunction
            Return result

        ElseIf prop.PropertyType Is GetType(Boolean?) Then
            Dim result As New NullableBooleanEditor()
            result.SetValue(prop.GetValue(_selected))
            AddHandler result.ValueEdited, editionFunction
            Return result

        ElseIf prop.PropertyType.IsGenericType Then
            If prop.PropertyType.GetGenericTypeDefinition() Is GetType(Nullable(Of )) Then
                If prop.PropertyType.GetGenericArguments(0).IsEnum Then
                    Dim result As New NullableEnumEditor()
                    result.Items.Add("Null")
                    For Each s In [Enum].GetNames(prop.PropertyType.GetGenericArguments(0))
                        result.Items.Add(s)
                    Next
                    result.DropDownStyle = ComboBoxStyle.DropDownList
                    result.SetValue(prop.GetValue(_selected))
                    AddHandler result.ValueEdited, editionFunction
                    Return result
                Else
                    Return New TextBox() With {.Text = prop.PropertyType.FullName, .ReadOnly = True}
                End If
            Else
                Return New TextBox() With {.Text = prop.PropertyType.FullName, .ReadOnly = True}
            End If

        ElseIf prop.PropertyType Is GetType(Microsoft.Xna.Framework.Graphics.Color) Then
            Dim result As New ColorEditor()
            result.SetValue(prop.GetValue(_selected))
            AddHandler result.ValueEdited, editionFunction
            Return result

        ElseIf prop.PropertyType Is GetType(Microsoft.Xna.Framework.Vector2) Then
            Dim result As New Vector2Editor()
            result.SetValue(prop.GetValue(_selected))
            AddHandler result.ValueEdited, editionFunction
            Return result

        ElseIf prop.PropertyType Is GetType(Microsoft.Xna.Framework.Vector3) Then
            Dim result As New Vector3Editor()
            result.SetValue(prop.GetValue(_selected))
            AddHandler result.ValueEdited, editionFunction
            Return result


        ElseIf prop.PropertyType Is GetType(Engine.Thickness) Then
            Dim result As New ThicknessEditor()
            result.SetValue(prop.GetValue(_selected))
            AddHandler result.ValueEdited, editionFunction
            Return result

        ElseIf prop.PropertyType Is GetType(String) Then
            Dim key As Core.RessourceKeyAttribute = prop.Attributes(GetType(Core.RessourceKeyAttribute))
            If key Is Nothing Then
                Return New TextBox() With {.Text = prop.PropertyType.FullName, .ReadOnly = True}
            Else
                Select Case key.RessourceType
                    Case Core.RessourceType.Texture2D
                        Dim result As New TextureEditor()
                        result.SetValue(prop.GetValue(_selected))
                        AddHandler result.ValueEdited, editionFunction
                        Return result

                    Case Else
                        Return New TextBox() With {.Text = prop.PropertyType.FullName, .ReadOnly = True}
                End Select
            End If
        Else
            Return New TextBox() With {.Text = prop.PropertyType.FullName, .ReadOnly = True}
        End If
    End Function

End Class
