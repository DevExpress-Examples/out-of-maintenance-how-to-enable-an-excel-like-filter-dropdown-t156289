Imports System.Collections.ObjectModel
Imports System.Windows

Namespace DXGridSample

    Public Partial Class MainWindow
        Inherits Window

        Public Property Items As ObservableCollection(Of Item)

        Public Sub New()
            Items = New ObservableCollection(Of Item)()
            For i As Integer = 0 To 100 - 1
                Items.Add(New Item With {.Id = i, .Name = "Name" & i, .IsEnabled = i Mod 2 = 0})
            Next

            DataContext = Me
            Me.InitializeComponent()
        End Sub
    End Class

    Public Class Item

        Public Property Id As Integer

        Public Property Name As String

        Public Property IsEnabled As Boolean
    End Class
End Namespace
