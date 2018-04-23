using DevExpress.Data.Filtering;
using DevExpress.Data.Filtering.Helpers;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;

namespace DXGridSample {
    public class FilteringBehavior : Behavior<TableView> {
        protected GridControl Grid { get { return AssociatedObject.Grid; } }
        protected TableView View { get { return AssociatedObject; } }
        protected IEnumerable<object> ItemsSource
        {
            get
            {
                if (Grid.ItemsSource is DataTable)
                    return ((DataTable)Grid.ItemsSource).DefaultView.Cast<object>();
                else if (Grid.ItemsSource is IEnumerable)
                    return ((IEnumerable)Grid.ItemsSource).Cast<object>();
                return null;
            }
        }
        protected Type DataType { get { return ItemsSource.ElementAt(0).GetType(); } }

        protected override void OnAttached() {
            base.OnAttached();
            View.ShowFilterPopup += ShowFilterPopup;
        }
        protected override void OnDetaching() {
            View.ShowFilterPopup -= ShowFilterPopup;
            base.OnDetaching();
        }

        protected virtual void ShowFilterPopup(object sender, FilterPopupEventArgs e) {
            if (e.Column.FilterPopupMode != FilterPopupMode.CheckedList || ItemsSource == null || ItemsSource.Count() == 0)
                return;
            var filter = RemoveColumn(Grid.FilterCriteria, e.Column.FieldName);
            if (ReferenceEquals(filter, null))
                return;
            e.ComboBoxEdit.ItemsSource = GetFilterItems(filter, e.Column);
        }
        protected virtual object GetFilterItems(CriteriaOperator filter, ColumnBase column) {
            var properties = TypeDescriptor.GetProperties(ItemsSource.ElementAt(0));
            var info = properties.Cast<PropertyDescriptor>().First(x => x.Name == column.FieldName);
            var expressionEvaluator = new ExpressionEvaluator(properties, filter);
            var distinctValues = expressionEvaluator.Filter(ItemsSource).Cast<object>().Select(obj => info.GetValue(obj)).Distinct();
            return distinctValues.Select(x => new CustomComboBoxItem { EditValue = x, DisplayValue = column.ActualEditSettings.GetDisplayTextFromEditor(x) }).ToList();
        }

        public static CriteriaOperator RemoveColumn(CriteriaOperator filter, string fieldName) {
            if (ReferenceEquals(filter, null))
                return null;
            IDictionary<OperandProperty, CriteriaOperator> splitted = CriteriaColumnAffinityResolver.SplitByColumns(filter);
            splitted.Remove(new OperandProperty(fieldName));
            return GroupOperator.And(splitted.Values);
        }
    }
}