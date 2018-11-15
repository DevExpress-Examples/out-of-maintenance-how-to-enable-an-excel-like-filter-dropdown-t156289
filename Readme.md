<!-- default file list -->
*Files to look at*:

* [FilteringBehavior.cs](./CS/FilteringBehavior.cs) (VB: [FilteringBehavior.vb](./VB/FilteringBehavior.vb))
* [MainWindow.xaml](./CS/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/MainWindow.xaml.cs) (VB: [MainWindow.xaml](./VB/MainWindow.xaml))
<!-- default file list end -->
# How to enable an Excel-like filter dropdown


<p>This example demonstrates how to enable an Excel-like filter dropdown in different DevExpress versions.<br><br><u>In version 16.2</u>, we have implemented the <a href="https://documentation.devexpress.com/#WPF/CustomDocument6133">Excel Style Filtering</a> functionality out of the box. This mode can be enabled by setting a single property - <a href="https://documentation.devexpress.com/WPF/DevExpressXpfGridColumnBase_FilterPopupModetopic.aspx">FilterPopupMode</a>.<br><br><u>In previous versions</u>, where this functionality is not available out of the box, it is possible to filter dropdown items based on the current filter.<br>First, DXGrid has the built-in <a href="https://documentation.devexpress.com/WPF/DevExpressXpfGridColumnBase_ShowAllTableValuesInCheckedFilterPopuptopic.aspx">ShowAllTableValuesInCheckedFilterPopup</a> property that allows enabling the following mode

* If the current column is not filtered, the filter dropdown displays only those items that meet the grid's filter condition.
* Otherwise, all data source items are displayed to simplify the process of the grid filter correction.<br>If this functionality is insufficient, and filtering dropdown items when the current column is filtered is required, use the approach demonstrated in this example. See the <strong>Implementation Details </strong>section for more information.</p>


<h3>Description</h3>

<p>Dropdown items can be filtered in the <a href="https://documentation.devexpress.com/#WPF/DevExpressXpfGridDataViewBase_ShowFilterPopuptopic">DataViewBase.ShowFilterPopup</a> event handler. To maintain items that were filtered by the <em>current column</em> and remove items filtered by other grid columns, perform the following steps:<br><br>1. Exclude the current column from the grid's filter. In this example, this operation is performed by the <strong>FilteringBehavior.RemoveColumn</strong> method.<br><br>2. Filter items with the resulting filter criteria. This task is accomplished using our internal <strong>ExpressionEvaluator</strong> class.</p>
<code lang="cs">ExpressionEvaluator evaluator = new ExpressionEvaluator(TypeDescriptor.GetProperties(source.FirstOrDefault()), criteria);
IEnumerable&lt;object&gt; filteredCollection = evaluator.Filter(source).OfType&lt;object&gt;();
</code>
<p><strong><em>Important:</em></strong><br><em>As ExpressionEvaluator receives standard property descriptors in this example, this makes this example incompatible with the </em><a href="https://documentation.devexpress.com/#WPF/CustomDocument6410"><em>DisplayText filter mode</em></a><em> and custom filtering.</em><br><br>3. Update the <strong>e.ComboBoxEdit.ItemsSource</strong> property based on the filtering results.</p>

<br/>


