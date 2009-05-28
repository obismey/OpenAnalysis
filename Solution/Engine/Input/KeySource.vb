Imports Microsoft.Xna.Framework.Input
Imports System.Collections.Generic
Imports Microsoft.Xna.Framework

Public Class KeySource
    Implements Core.IValueSource(Of Single)

    Private _maps As New List(Of keyMap)
    Private _previousKeyState As KeyboardState
    Private _currentKeyState As KeyboardState

    ReadOnly Property Maps() As List(Of keyMap)
        Get
            Return _maps
        End Get
    End Property

    Public Function GetValue() As Single Implements Core.IValueSource(Of Single).GetValue
        Dim result As Single
        _currentKeyState = Keyboard.GetState()

        For Each m In _maps
            UpdateMap(m)
            result += m.current
        Next

        _previousKeyState = _currentKeyState
        Return result
    End Function

    Public Function GetValue(ByVal destination As Object) As Single Implements Core.IValueSource(Of Single).GetValue
        Return GetValue()
    End Function

    Public Function GetValue(ByVal destination As Object, ByVal destinationProperty As String) As Single Implements Core.IValueSource(Of Single).GetValue
        Return GetValue()
    End Function


    Private Sub UpdateMap(ByVal map As keyMap)
        If _previousKeyState.IsKeyDown(map.key) _
            And _
            _currentKeyState.IsKeyDown(map.key) Then

            map.current += map.Speed

        End If

        'map.current = MathHelper.Clamp(map.current, map.minValue, map.maxValue)
    End Sub
End Class

Public Class keyMap
    Public Speed As Single
    Public minValue As Single
    Public maxValue As Single
    Public key As Keys
    Friend current As Single
End Class