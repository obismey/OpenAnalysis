Imports System
Imports System.IO
Imports System.Diagnostics
Imports System.Runtime.InteropServices
Imports Microsoft.Build.BuildEngine
Imports Microsoft.Build.Framework

''' <summary>
''' Custom implementation of the MSBuild ILogger interface records
''' content build errors so we can later display them to the user.
''' </summary>
Class ErrorLogger
    Implements ILogger

    Private m_errors As New List(Of String)

    ''' <summary>
    ''' Handles error notification events by storing the error message string.
    ''' </summary>
    Private Sub ErrorRaised(ByVal sender As Object, ByVal e As BuildErrorEventArgs)
        m_errors.Add(e.Message)
    End Sub

    ''' <summary>
    ''' Gets a list of all the errors that have been logged.
    ''' </summary>
    Public ReadOnly Property Errors() As List(Of String)
        Get
            Return m_errors
        End Get
    End Property

#Region "ILogger Members"

    ''' <summary>
    ''' Initializes the custom logger, hooking the ErrorRaised notification event.
    ''' </summary>
    Public Sub Initialize(ByVal eventSource As IEventSource) Implements ILogger.Initialize
        If eventSource IsNot Nothing Then
            AddHandler eventSource.ErrorRaised, AddressOf ErrorRaised
        End If
    End Sub

    ''' <summary>
    ''' Shuts down the custom logger.
    ''' </summary>
    Public Sub Shutdown() Implements ILogger.Shutdown
    End Sub

    ''' <summary>
    ''' Implement the ILogger.Parameters property.
    ''' </summary>
    Private Property ILogger_Parameters() As String Implements ILogger.Parameters
        Get
            Return parameters
        End Get
        Set(ByVal value As String)
            parameters = value
        End Set
    End Property

    Private parameters As String

    ''' <summary>
    ''' Implement the ILogger.Verbosity property.
    ''' </summary>
    Private Property ILogger_Verbosity() As LoggerVerbosity Implements ILogger.Verbosity
        Get
            Return verbosity
        End Get
        Set(ByVal value As LoggerVerbosity)
            verbosity = value
        End Set
    End Property

    Private verbosity As LoggerVerbosity = LoggerVerbosity.Normal

#End Region
End Class
