Imports System.Linq
Imports System.Xml.Linq
Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics

Public MustInherit Class PersistenceContext


    Private _currentType As Type
    Public Overridable Property CurrentType() As Type
        Get
            Return _currentType
        End Get
        Set(ByVal value As Type)
            _currentType = value
        End Set
    End Property

    Private _typeVersion As Integer? = Nothing
    Public Overridable Property TypeVersion() As Integer?
        Get
            Return _typeVersion
        End Get
        Set(ByVal value As Integer?)
            _typeVersion = value
        End Set
    End Property

    Sub Start()

    End Sub


    MustOverride Sub Write(ByVal member As String, ByVal value As Integer)
    MustOverride Sub Write(ByVal member As String, ByVal value As Single)
    MustOverride Sub Write(ByVal member As String, ByVal value As Long)
    MustOverride Sub Write(ByVal member As String, ByVal value As Double)
    MustOverride Sub Write(ByVal member As String, ByVal value As Boolean)
    MustOverride Sub Write(ByVal member As String, ByVal value As Vector2)
    MustOverride Sub Write(ByVal member As String, ByVal value As Vector3)
    MustOverride Sub Write(ByVal member As String, ByVal value As Vector4)
    MustOverride Sub Write(ByVal member As String, ByVal value As Matrix)
    MustOverride Sub Write(ByVal member As String, ByVal value As Plane)
    MustOverride Sub Write(ByVal member As String, ByVal value As BoundingBox)
    MustOverride Sub Write(ByVal member As String, ByVal value As BoundingSphere)
    MustOverride Sub Write(ByVal member As String, ByVal value As Point)
    MustOverride Sub Write(ByVal member As String, ByVal value As Color)
    MustOverride Sub Write(ByVal member As String, ByVal value As Rectangle)
    MustOverride Sub Write(ByVal member As String, ByVal value As String)
    MustOverride Sub Write(ByVal member As String, ByVal value As IPersistable)
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of Integer))
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of Single))
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of Long))
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of Double))
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of Boolean))
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of Vector2))
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of Vector3))
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of Vector4))
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of Matrix))
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of Plane))
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of BoundingBox))
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of BoundingSphere))
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of Point))
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of Color))
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of Rectangle))
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of String))
    MustOverride Sub Write(ByVal member As String, ByVal value As IEnumerable(Of IPersistable))


    MustOverride Function ReadInteger(ByVal member As String) As Integer
    MustOverride Function ReadSingle(ByVal member As String) As Single
    MustOverride Function ReadLong(ByVal member As String) As Long
    MustOverride Function ReadDouble(ByVal member As String) As Double
    MustOverride Function ReadBoolean(ByVal member As String) As Boolean
    MustOverride Function ReadVector2(ByVal member As String) As Vector2
    MustOverride Function ReadVector3(ByVal member As String) As Vector3
    MustOverride Function ReadVector4(ByVal member As String) As Vector4
    MustOverride Function ReadMatrix(ByVal member As String) As Matrix
    MustOverride Function ReadPlane(ByVal member As String) As Plane
    MustOverride Function ReadBoundingBox(ByVal member As String) As BoundingBox
    MustOverride Function ReadBoundingSphere(ByVal member As String) As BoundingSphere
    MustOverride Function ReadPoint(ByVal member As String) As Point
    MustOverride Function ReadColor(ByVal member As String) As Color
    MustOverride Function ReadRectangle(ByVal member As String) As Rectangle
    MustOverride Function ReadString(ByVal member As String) As String
    MustOverride Function ReadIPersistable(ByVal member As String) As IPersistable
    MustOverride Function ReadIntegerSet(ByVal member As String) As IEnumerable(Of Integer)
    MustOverride Function ReadSingleSet(ByVal member As String) As IEnumerable(Of Single)
    MustOverride Function ReadLongSet(ByVal member As String) As IEnumerable(Of Long)
    MustOverride Function ReadDoubleSet(ByVal member As String) As IEnumerable(Of Double)
    MustOverride Function ReadBooleanSet(ByVal member As String) As IEnumerable(Of Boolean)
    MustOverride Function ReadVector2Set(ByVal member As String) As IEnumerable(Of Vector2)
    MustOverride Function ReadVector3Set(ByVal member As String) As IEnumerable(Of Vector3)
    MustOverride Function ReadVector4Set(ByVal member As String) As IEnumerable(Of Vector4)
    MustOverride Function ReadMatrixSet(ByVal member As String) As IEnumerable(Of Matrix)
    MustOverride Function ReadPlaneSet(ByVal member As String) As IEnumerable(Of Plane)
    MustOverride Function ReadBoundingBoxSet(ByVal member As String) As IEnumerable(Of BoundingBox)
    MustOverride Function ReadBoundingSphereSet(ByVal member As String) As IEnumerable(Of BoundingSphere)
    MustOverride Function ReadPointSet(ByVal member As String) As IEnumerable(Of Point)
    MustOverride Function ReadColorSet(ByVal member As String) As IEnumerable(Of Color)
    MustOverride Function ReadRectangleSet(ByVal member As String) As IEnumerable(Of Rectangle)
    MustOverride Function ReadStringSet(ByVal member As String) As IEnumerable(Of String)
    MustOverride Function ReadIPersistableSet(ByVal member As String) As IEnumerable(Of IPersistable)


    MustOverride Function CheckExists(ByVal member As String) As Boolean
    MustOverride Sub WriteError(ByVal errorString As String, ByVal severity As Integer)
End Class
