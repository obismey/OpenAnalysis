Imports System
Imports System.IO
Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports Microsoft.Build.BuildEngine
Imports Microsoft.Build.Framework

''' <summary>
''' This class wraps the MSBuild functionality needed to build XNA Framework
''' content dynamically at runtime. It creates a temporary MSBuild project
''' in memory, and adds whatever content files you choose to this project.
''' It then builds the project, which will create compiled .xnb content files
''' in a temporary directory. After the build finishes, you can use a regular
''' ContentManager to load these temporary .xnb files in the usual way.
''' </summary>
Public Class ContentBuilder
    Implements IDisposable

#Region "Fields"

    ' What importers or processors should we load?
    Const xnaVersion As String = ", Version=3.0.0.0, PublicKeyToken=6d5c3888ef60e27d"

    Shared pipelineAssemblies As String() = {"Microsoft.Xna.Framework.Content.Pipeline.FBXImporter" & xnaVersion, _
                                             "Microsoft.Xna.Framework.Content.Pipeline.XImporter" & xnaVersion, _
                                             "Microsoft.Xna.Framework.Content.Pipeline.TextureImporter" & xnaVersion, _
                                             "Microsoft.Xna.Framework.Content.Pipeline.EffectImporter" & xnaVersion, _
                                             "Microsoft.Xna.Framework.Content.Pipeline.AudioImporters" & xnaVersion}

    ' MSBuild objects used to dynamically build content.
    Private msBuildEngine As Microsoft.Build.BuildEngine.Engine
    Private msBuildProject As Project
    Private errorLogger As ErrorLogger

    ' Temporary directories used by the content build.
    Private buildDirectory As String
    Private processDirectory As String
    Private baseDirectory As String

    ' Generate unique directory names if there is more than one ContentBuilder.
    Shared directorySalt As Integer

    ' Have we been disposed?
    Private isDisposed As Boolean

#End Region

#Region "Properties"

    ''' <summary>
    ''' Gets the output directory, which will contain the generated .xnb files.
    ''' </summary>
    Public ReadOnly Property OutputDirectory() As String
        Get
            Return Path.Combine(buildDirectory, "bin\Content")
        End Get
    End Property

#End Region

#Region "Initialization"

    ''' <summary>
    ''' Creates a new content builder.
    ''' </summary>
    Public Sub New()
        CreateTempDirectory()
        CreateBuildProject()
    End Sub

    ''' <summary>
    ''' Finalizes the content builder.
    ''' </summary>
    Protected Overrides Sub Finalize()
        Try

            Dispose(False)
        Finally
            MyBase.Finalize()
        End Try
    End Sub

    ''' <summary>
    ''' Disposes the content builder when it is no longer required.
    ''' </summary>
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)

        GC.SuppressFinalize(Me)
    End Sub

    ''' <summary>
    ''' Implements the standard .NET IDisposable pattern.
    ''' </summary>
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not isDisposed Then
            isDisposed = True

            DeleteTempDirectory()
        End If
    End Sub

#End Region

#Region "MSBuild"

    ''' <summary>
    ''' Creates a temporary MSBuild content project in memory.
    ''' </summary>
    Private Sub CreateBuildProject()
        Dim projectPath As String = Path.Combine(buildDirectory, "content.contentproj")
        Dim outputPath As String = Path.Combine(buildDirectory, "bin")

        ' Create the build engine.
        msBuildEngine = New Microsoft.Build.BuildEngine.Engine()
        msBuildEngine.DefaultToolsVersion = "3.5"

        ' Hook up our custom error logger.
        errorLogger = New ErrorLogger()

        msBuildEngine.RegisterLogger(errorLogger)

        ' Create the build project.
        msBuildProject = New Project(msBuildEngine)

        msBuildProject.FullFileName = projectPath

        msBuildProject.SetProperty("XnaPlatform", "Windows")
        msBuildProject.SetProperty("XnaFrameworkVersion", "v3.0")
        msBuildProject.SetProperty("Configuration", "Release")
        msBuildProject.SetProperty("OutputPath", outputPath)

        ' Register any custom importers or processors.
        For Each pipelineAssembly As String In pipelineAssemblies
            msBuildProject.AddNewItem("Reference", pipelineAssembly)
        Next

        ' Include the standard targets file that defines
        ' how to build XNA Framework content.
        msBuildProject.AddNewImport("$(MSBuildExtensionsPath)\Microsoft\XNA " & "Game Studio\v3.0\Microsoft.Xna.GameStudio" & ".ContentPipeline.targets", Nothing)
    End Sub

    ''' <summary>
    ''' Adds a new content file to the MSBuild project. The importer and
    ''' processor are optional: if you leave the importer null, it will
    ''' be autodetected based on the file extension, and if you leave the
    ''' processor null, data will be passed through without any processing.
    ''' </summary>
    Public Sub Add(ByVal filename As String, ByVal name As String, ByVal importer As String, ByVal processor As String)
        Dim buildItem As BuildItem = msBuildProject.AddNewItem("Compile", filename)

        buildItem.SetMetadata("Link", Path.GetFileName(filename))
        buildItem.SetMetadata("Name", name)

        If Not String.IsNullOrEmpty(importer) Then
            buildItem.SetMetadata("Importer", importer)
        End If

        If Not String.IsNullOrEmpty(processor) Then
            buildItem.SetMetadata("Processor", processor)
        End If
    End Sub

    ''' <summary>
    ''' Removes all content files from the MSBuild project.
    ''' </summary>
    Public Sub Clear()
        msBuildProject.RemoveItemsByName("Compile")
    End Sub

    ''' <summary>
    ''' Builds all the content files which have been added to the project,
    ''' dynamically creating .xnb files in the OutputDirectory.
    ''' Returns an error message if the build fails.
    ''' </summary>
    Public Function Build() As String
        ' Clear any previous errors.
        errorLogger.Errors.Clear()

        ' Build the project.
        If Not msBuildProject.Build() Then
            ' If the build failed, return an error string.
            Return String.Join(vbLf, errorLogger.Errors.ToArray())
        End If

        Return Nothing
    End Function

#End Region

#Region "Temp Directories"

    ''' <summary>
    ''' Creates a temporary directory in which to build content.
    ''' </summary>
    Private Sub CreateTempDirectory()
        ' Start with a standard base name:
        '
        '  %temp%\WinFormsContentLoading.ContentBuilder

        baseDirectory = Path.Combine(Path.GetTempPath(), [GetType]().FullName)

        ' Include our process ID, in case there is more than
        ' one copy of the program running at the same time:
        '
        '  %temp%\WinFormsContentLoading.ContentBuilder\<ProcessId>

        Dim processId As Integer = Process.GetCurrentProcess().Id

        processDirectory = Path.Combine(baseDirectory, processId.ToString())

        ' Include a salt value, in case the program
        ' creates more than one ContentBuilder instance:
        '
        '  %temp%\WinFormsContentLoading.ContentBuilder\<ProcessId>\<Salt>

        directorySalt += 1

        buildDirectory = Path.Combine(processDirectory, directorySalt.ToString())

        ' Create our temporary directory.
        Directory.CreateDirectory(buildDirectory)

        PurgeStaleTempDirectories()
    End Sub

    ''' <summary>
    ''' Deletes our temporary directory when we are finished with it.
    ''' </summary>
    Private Sub DeleteTempDirectory()
        Directory.Delete(buildDirectory, True)

        ' If there are no other instances of ContentBuilder still using their
        ' own temp directories, we can delete the process directory as well.
        If Directory.GetDirectories(processDirectory).Length = 0 Then
            Directory.Delete(processDirectory)

            ' If there are no other copies of the program still using their
            ' own temp directories, we can delete the base directory as well.
            If Directory.GetDirectories(baseDirectory).Length = 0 Then
                Directory.Delete(baseDirectory)
            End If
        End If
    End Sub

    ''' <summary>
    ''' Ideally, we want to delete our temp directory when we are finished using
    ''' it. The DeleteTempDirectory method (called by whichever happens first out
    ''' of Dispose or our finalizer) does exactly that. Trouble is, sometimes
    ''' these cleanup methods may never execute. For instance if the program
    ''' crashes, or is halted using the debugger, we never get a chance to do
    ''' our deleting. The next time we start up, this method checks for any temp
    ''' directories that were left over by previous runs which failed to shut
    ''' down cleanly. This makes sure these orphaned directories will not just
    ''' be left lying around forever.
    ''' </summary>
    Private Sub PurgeStaleTempDirectories()
        ' Check all subdirectories of our base location.
        For Each directory__1 As String In Directory.GetDirectories(baseDirectory)
            ' The subdirectory name is the ID of the process which created it.
            Dim processId As Integer

            If Integer.TryParse(Path.GetFileName(directory__1), processId) Then
                Try
                    ' Is the creator process still running?
                    Process.GetProcessById(processId)
                Catch generatedExceptionName As ArgumentException
                    ' If the process is gone, we can delete its temp directory.
                    Directory.Delete(directory__1, True)
                End Try
            End If
        Next
    End Sub

#End Region
End Class
