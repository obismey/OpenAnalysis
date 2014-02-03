
Class App
    Implements Core.IApplication

    Private _Services As New List(Of Core.IService)
    Private _Plugins As New List(Of Core.IPlugin)

    Protected Overrides Sub OnStartup(ByVal e As System.Windows.StartupEventArgs)
        _Services.Add(New UIServiceImpl())
        LoadPlugins()
        MyBase.OnStartup(e)
    End Sub

    Public Function GetPlugins() As System.Collections.Generic.IEnumerable(Of Core.IPlugin) Implements Core.IApplication.GetPlugins
        Return _Plugins
    End Function

    Public Function GetServices() As System.Collections.Generic.IEnumerable(Of Core.IService) Implements Core.IApplication.GetServices
        Return _Services
    End Function

    Public Shared ReadOnly Property UIService As UIServiceImpl
        Get
            Return CType(System.Windows.Application.Current, App)._Services(0)
        End Get
    End Property

    Public Sub LoadPlugins()
        Dim appfolder As String = IO.Path.GetDirectoryName(Reflection.Assembly.GetExecutingAssembly().Location)
        For Each folder In IO.Directory.EnumerateDirectories( _
           IO.Path.Combine(appfolder, "Modules"))

            AppDomain.CurrentDomain.AppendPrivatePath("Modules\" & New IO.DirectoryInfo(folder).Name)
        Next
        For Each folder In IO.Directory.EnumerateDirectories( _
              IO.Path.Combine(appfolder, "Modules"))
            For Each f In IO.Directory.EnumerateFiles(folder, "*.dll")
                Try
                    Dim ass = Reflection.Assembly.LoadFile(f)
                    LoadPlugin(ass)
                Catch ex As Exception
                    IO.File.AppendAllText(IO.Path.Combine(appfolder, "Logging.txt"), _
                        "[DEBUT=" & DateTime.Now.ToString() & "]" & vbCrLf & ex.ToString() & vbCrLf & "[FIN]")
                End Try
            Next
        Next
    End Sub

    Public Sub LoadPlugin(ByVal Assembly As Reflection.Assembly)
        Dim entrypoint As Core.EntryPointAttribute = _
                Assembly.GetCustomAttributes(GetType(Vivei.Tools.Core.EntryPointAttribute), True).FirstOrDefault()
        If entrypoint Is Nothing Then Return
        Dim plug As Core.IPlugin = Activator.CreateInstance(entrypoint.PluginType)
        _Plugins.Add(plug)
        plug.Reset(Me)
        _Services.AddRange(plug.GetServices())
    End Sub
End Class
