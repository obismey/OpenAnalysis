Imports System.Xml.Serialization
Imports Microsoft.Xna.Framework
Imports System.Collections.Generic

<System.Serializable()> Public Class SkinSource

    Dim _textures As New List(Of String)
    Dim _font As New List(Of String)
    Dim _elements As New List(Of Element)
    Dim _name As String

    Friend Sub New()
        MyBase.New()
    End Sub

    <XmlAttribute()> _
    Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Property Textures() As List(Of String)
        Get
            Return _textures
        End Get
        Set(ByVal value As List(Of String))
            _textures = value
        End Set
    End Property

    'each element have absolute name
    Property Elements() As List(Of Element)
        Get
            Return _elements
        End Get
        Set(ByVal value As List(Of Element))
            _elements = value
        End Set
    End Property

    Public Shared Sub SaveToFile(ByVal skinsource As SkinSource, ByVal filename As String)
        Dim ser As New Xml.Serialization.XmlSerializer(GetType(SkinSource))
        Dim f As IO.FileStream = IO.File.Open(filename, IO.FileMode.OpenOrCreate)
        ser.Serialize(f, skinsource)
        f.Close()
    End Sub
    Public Shared Function loadFromFile(ByVal filename As String) As SkinSource
        Dim ser As New Xml.Serialization.XmlSerializer(GetType(SkinSource))
        Dim f As IO.FileStream = IO.File.Open(filename, IO.FileMode.OpenOrCreate)
        Dim result As SkinSource = CType(ser.Deserialize(f), SkinSource)
        f.Close()
        Return result
    End Function
    Public Shared ReadOnly Property Empty() As SkinSource
        Get
            Return New SkinSource()
        End Get
    End Property


    <System.Serializable()> _
    Class Element
        <XmlAttribute()> _
        Public name As String

        <XmlAttribute()> _
        Public IsFont As String

        <XmlAttribute()> _
        Public textureID As Integer

        <XmlAttribute()> _
        Public sourceRect As Rectangle

        <XmlAttribute()> _
        Public allTexture As Boolean = True

        <XmlAttribute()> _
        Public FontFile As String

        <XmlAttribute()> _
        Public FontSize As Single

        <XmlAttribute()> _
        Public FontBold As Boolean

        <XmlAttribute()> _
        Public FontItalic As Boolean
    End Class
End Class
