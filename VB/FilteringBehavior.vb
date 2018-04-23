Imports DevExpress.Data.Filtering
Imports DevExpress.Data.Filtering.Helpers
Imports DevExpress.Mvvm.UI.Interactivity
Imports DevExpress.Xpf.Grid
Imports System
Imports System.Collections
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Linq

Namespace DXGridSample
    Public Class FilteringBehavior
        Inherits Behavior(Of TableView)

        Protected ReadOnly Property Grid() As GridControl
            Get
                Return AssociatedObject.Grid
            End Get
        End Property
        Protected ReadOnly Property View() As TableView
            Get
                Return AssociatedObject
            End Get
        End Property
        Protected ReadOnly Property ItemsSource() As IEnumerable(Of Object)
            Get
                If TypeOf Grid.ItemsSource Is DataTable Then
                    Return CType(Grid.ItemsSource, DataTable).DefaultView.Cast(Of Object)()
                ElseIf TypeOf Grid.ItemsSource Is IEnumerable Then
                    Return DirectCast(Grid.ItemsSource, IEnumerable).Cast(Of Object)()
                End If
                Return Nothing
            End Get
        End Property
        Protected ReadOnly Property DataType() As Type
            Get
                Return ItemsSource.ElementAt(0).GetType()
            End Get
        End Property

        Protected Overrides Sub OnAttached()
            MyBase.OnAttached()
            AddHandler View.ShowFilterPopup, AddressOf ShowFilterPopup
        End Sub
        Protected Overrides Sub OnDetaching()
            RemoveHandler View.ShowFilterPopup, AddressOf ShowFilterPopup
            MyBase.OnDetaching()
        End Sub

        Protected Overridable Sub ShowFilterPopup(ByVal sender As Object, ByVal e As FilterPopupEventArgs)
            If e.Column.FilterPopupMode <> FilterPopupMode.CheckedList OrElse ItemsSource Is Nothing OrElse ItemsSource.Count() = 0 Then
                Return
            End If
            Dim filter = RemoveColumn(Grid.FilterCriteria, e.Column.FieldName)
            If ReferenceEquals(filter, Nothing) Then
                Return
            End If
            e.ComboBoxEdit.ItemsSource = GetFilterItems(filter, e.Column)
        End Sub
        Protected Overridable Function GetFilterItems(ByVal filter As CriteriaOperator, ByVal column As ColumnBase) As Object
            Dim properties = TypeDescriptor.GetProperties(ItemsSource.ElementAt(0))
            Dim info = properties.Cast(Of PropertyDescriptor)().First(Function(x) x.Name = column.FieldName)
            Dim expressionEvaluator = New ExpressionEvaluator(properties, filter)
            Dim distinctValues = expressionEvaluator.Filter(ItemsSource).Cast(Of Object)().Select(Function(obj) info.GetValue(obj)).Distinct()
            Return distinctValues.Select(Function(x) New CustomComboBoxItem With { _
                .EditValue = x, _
                .DisplayValue = column.ActualEditSettings.GetDisplayTextFromEditor(x) _
            }).ToList()
        End Function

        Public Shared Function RemoveColumn(ByVal filter As CriteriaOperator, ByVal fieldName As String) As CriteriaOperator
            If ReferenceEquals(filter, Nothing) Then
                Return Nothing
            End If
            Dim splitted As IDictionary(Of OperandProperty, CriteriaOperator) = CriteriaColumnAffinityResolver.SplitByColumns(filter)
            splitted.Remove(New OperandProperty(fieldName))
            Return GroupOperator.And(splitted.Values)
        End Function
    End Class
End Namespace