#Region "File Description"
'-----------------------------------------------------------------------------
' ServiceContainer.cs
'
' Microsoft XNA Community Game Platform
' Copyright (C) Microsoft Corporation. All rights reserved.
'-----------------------------------------------------------------------------
#End Region

#Region "Using Statements"
Imports System
Imports System.Collections.Generic
#End Region

Namespace Xna
    ''' <summary>
    ''' Container class implements the IServiceProvider interface. This is used
    ''' to pass shared services between different components, for instance the
    ''' ContentManager uses it to locate the IGraphicsDeviceService implementation.
    ''' </summary>
    Public Class ServiceContainer
        Implements IServiceProvider

        Private Shared services As New Dictionary(Of Type, Object)


        ''' <summary>
        ''' Adds a new service to the collection.
        ''' </summary>
        Public Sub AddService(Of T)(ByVal service As T)
            If Not services.ContainsKey(GetType(T)) Then
                services.Add(GetType(T), service)
            End If
        End Sub

        ''' <summary>
        ''' Looks up the specified service.
        ''' </summary>
        Public Function GetService(ByVal serviceType As Type) As Object Implements System.IServiceProvider.GetService
            Dim service As Object = Nothing

            services.TryGetValue(serviceType, service)

            Return service
        End Function
    End Class

End Namespace

