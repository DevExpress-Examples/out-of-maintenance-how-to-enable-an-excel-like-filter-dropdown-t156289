Imports System
Imports System.Collections.ObjectModel
Imports System.Linq
Imports System.Windows

Namespace DXGridSample
    Partial Public Class MainWindow
        Inherits Window

        Public Property Items() As ObservableCollection(Of Item)
        Public Sub New()
            Items = New ObservableCollection(Of Item)()
            For i As Integer = 0 To 99
                Items.Add(New Item With { _
                    .Id = i, _
                    .Name = "Name" & i, _
                    .IsEnabled = i Mod 2 = 0 _
                })
            Next i
            DataContext = Me
            InitializeComponent()
        End Sub
    End Class
    Public Class Item
        Public Property Id() As Integer
        Public Property Name() As String
        Public Property IsEnabled() As Boolean
    End Class
End Namespace