Imports System.Runtime.InteropServices

Partial Public Class MainWindow
    <StructLayout(LayoutKind.Sequential)> _
        Public Structure POINT
        ''' <summary>
        ''' x coordinate of point.
        ''' </summary>
        Public x As Integer
        ''' <summary>
        ''' y coordinate of point.
        ''' </summary>
        Public y As Integer

        ''' <summary>
        ''' Construct a point of coordinates (x,y).
        ''' </summary>
        Public Sub New(ByVal x As Integer, ByVal y As Integer)
            Me.x = x
            Me.y = y
        End Sub
    End Structure
    <StructLayout(LayoutKind.Sequential)> _
    Public Structure MINMAXINFO
        Public ptReserved As POINT
        Public ptMaxSize As POINT
        Public ptMaxPosition As POINT
        Public ptMinTrackSize As POINT
        Public ptMaxTrackSize As POINT
    End Structure
    <StructLayout(LayoutKind.Sequential, Pack:=0)> _
    Public Structure RECT
        ''' <summary> Win32 </summary>
        Public left As Integer
        ''' <summary> Win32 </summary>
        Public top As Integer
        ''' <summary> Win32 </summary>
        Public right As Integer
        ''' <summary> Win32 </summary>
        Public bottom As Integer

        ''' <summary> Win32 </summary>
        Public Shared ReadOnly Empty As New RECT()

        ''' <summary> Win32 </summary>
        Public ReadOnly Property Width() As Integer
            Get
                Return Math.Abs(right - left)
            End Get
        End Property

        ''' <summary> Win32 </summary>
        Public ReadOnly Property Height() As Integer
            Get
                Return bottom - top
            End Get
        End Property

        ''' <summary> Win32 </summary>
        Public Sub New(ByVal left As Integer, ByVal top As Integer, ByVal right As Integer, ByVal bottom As Integer)
            Me.left = left
            Me.top = top
            Me.right = right
            Me.bottom = bottom
        End Sub

        ''' <summary> Win32 </summary>
        Public Sub New(ByVal rcSrc As RECT)
            Me.left = rcSrc.left
            Me.top = rcSrc.top
            Me.right = rcSrc.right
            Me.bottom = rcSrc.bottom
        End Sub

        ''' <summary> Win32 </summary>
        Public ReadOnly Property IsEmpty() As Boolean
            Get
                ' BUGBUG : On Bidi OS (hebrew arabic) left > right
                Return left >= right OrElse top >= bottom
            End Get
        End Property

        ''' <summary> Return a user friendly representation of this struct </summary>
        Public Overloads Overrides Function ToString() As String
            If Me = RECT.Empty Then
                Return "RECT {Empty}"
            End If
            Return "RECT { left : " + left + " / top : " + top + " / right : " + right + " / bottom : " + bottom + " }"
        End Function

        ''' <summary> Determine if 2 RECT are equal (deep compare) </summary>
        Public Overloads Overrides Function Equals(ByVal obj As Object) As Boolean
            If Not (TypeOf obj Is RECT) Then
                Return False
            End If
            Return (Me = DirectCast(obj, RECT))
        End Function

        ''' <summary>Return the HashCode for this struct (not garanteed to be unique)</summary>
        Public Overloads Overrides Function GetHashCode() As Integer
            Return left.GetHashCode() + top.GetHashCode() + right.GetHashCode() + bottom.GetHashCode()
        End Function

        ''' <summary> Determine if 2 RECT are equal (deep compare)</summary>
        Public Shared Operator =(ByVal rect1 As RECT, ByVal rect2 As RECT) As Boolean
            Return (rect1.left = rect2.left AndAlso rect1.top = rect2.top AndAlso rect1.right = rect2.right AndAlso rect1.bottom = rect2.bottom)
        End Operator

        ''' <summary> Determine if 2 RECT are different(deep compare)</summary>
        Public Shared Operator <>(ByVal rect1 As RECT, ByVal rect2 As RECT) As Boolean
            Return Not (rect1 = rect2)
        End Operator

    End Structure
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Auto)> _
    Public Class MONITORINFO
        ''' <summary>
        ''' </summary>            
        Public cbSize As Integer = Marshal.SizeOf(GetType(MONITORINFO))

        ''' <summary>
        ''' </summary>            
        Public rcMonitor As New RECT()

        ''' <summary>
        ''' </summary>            
        Public rcWork As New RECT()

        ''' <summary>
        ''' </summary>            
        Public dwFlags As Integer = 0
    End Class

    <DllImport("User32")> _
    Friend Shared Function GetMonitorInfo(ByVal hMonitor As IntPtr, ByVal lpmi As MONITORINFO) As Boolean
    End Function
    <DllImport("User32")> _
    Friend Shared Function MonitorFromWindow(ByVal handle As IntPtr, ByVal flags As Integer) As IntPtr
    End Function


    Private Shared Function WindowProc(ByVal hwnd As System.IntPtr, ByVal msg As Integer, ByVal wParam As System.IntPtr, ByVal lParam As System.IntPtr, ByRef handled As Boolean) As System.IntPtr
        Select Case msg
            Case 36
                WmGetMinMaxInfo(hwnd, lParam)
                handled = True
        End Select

        Return CType(0, System.IntPtr)
    End Function
    Private Shared Sub WmGetMinMaxInfo(ByVal hwnd As System.IntPtr, ByVal lParam As System.IntPtr)

        Dim mmi As MINMAXINFO = DirectCast(Marshal.PtrToStructure(lParam, GetType(MINMAXINFO)), MINMAXINFO)

        ' Adjust the maximized size and position to fit the work area of the correct monitor
        Dim MONITOR_DEFAULTTONEAREST As Integer = 2
        Dim monitor As System.IntPtr = MonitorFromWindow(hwnd, MONITOR_DEFAULTTONEAREST)

        If monitor <> System.IntPtr.Zero Then

            Dim monitorInfo As New MONITORINFO()
            GetMonitorInfo(monitor, monitorInfo)
            Dim rcWorkArea As RECT = monitorInfo.rcWork
            Dim rcMonitorArea As RECT = monitorInfo.rcMonitor
            mmi.ptMaxPosition.x = Math.Abs(rcWorkArea.left - rcMonitorArea.left) 'rcWorkArea.left  'Math.Abs(rcWorkArea.left - rcMonitorArea.left)
            mmi.ptMaxPosition.y = Math.Abs(rcWorkArea.top - rcMonitorArea.top) ' rcWorkArea.top  'Math.Abs(rcWorkArea.top - rcMonitorArea.top)
            mmi.ptMaxSize.x = Math.Min(rcMonitorArea.Width, Math.Abs(rcWorkArea.right - rcWorkArea.left))
            mmi.ptMaxSize.y = Math.Abs(rcWorkArea.bottom - rcWorkArea.top)

            '  mmi.ptMaxSize.x = If(mmi.ptMaxSize.x <> 1366 Or mmi.ptMaxSize.x <> 1920, 1920, mmi.ptMaxSize.x)
        End If
        Marshal.StructureToPtr(mmi, lParam, True)
    End Sub
    Private Sub MainWindow_SourceInitialized(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.SourceInitialized
        Dim handle As System.IntPtr = (New Interop.WindowInteropHelper(Me)).Handle
        Interop.HwndSource.FromHwnd(handle).AddHook(New Interop.HwndSourceHook(AddressOf WindowProc))
        WindowState = Windows.WindowState.Maximized
    End Sub

    Private Sub CloseMainWindowButton_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles CloseMainWindowButton.Click
        Me.Close()
    End Sub

    Private Sub HideMainWindowButton_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles HideMainWindowButton.Click
        Me.WindowState = Windows.WindowState.Minimized
    End Sub

    Private Sub ResizeMainWindowButton_Click(ByVal sender As Object, ByVal e As System.Windows.RoutedEventArgs) Handles ResizeMainWindowButton.Click
        If Me.WindowState = Windows.WindowState.Maximized Then
            Me.WindowState = Windows.WindowState.Normal
        ElseIf Me.WindowState = Windows.WindowState.Normal Then
            Me.WindowState = Windows.WindowState.Maximized
        End If
    End Sub

    Private Sub DragMainWindowThumb_DragDelta(ByVal sender As Object, ByVal e As System.Windows.Controls.Primitives.DragDeltaEventArgs) Handles DragMainWindowThumb.DragDelta
        If Me.WindowState = Windows.WindowState.Normal Then
            Me.Top += e.VerticalChange
            Me.Left += e.HorizontalChange
        End If
    End Sub
End Class
