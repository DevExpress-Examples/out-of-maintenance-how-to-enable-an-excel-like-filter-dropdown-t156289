<!-- default badges list -->
![](https://img.shields.io/endpoint?url=https://codecentral.devexpress.com/api/v1/VersionRange/128650251/22.2.2%2B)
[![](https://img.shields.io/badge/Open_in_DevExpress_Support_Center-FF7200?style=flat-square&logo=DevExpress&logoColor=white)](https://supportcenter.devexpress.com/ticket/details/T156289)
[![](https://img.shields.io/badge/📖_How_to_use_DevExpress_Examples-e9f6fc?style=flat-square)](https://docs.devexpress.com/GeneralInformation/403183)
<!-- default badges end -->
<!-- default file list -->
*Files to look at*:

* [MainWindow.xaml](./CS/MainWindow.xaml) (VB: [MainWindow.xaml](./VB/MainWindow.xaml))
* [MainWindow.xaml.cs](./CS/MainWindow.xaml.cs) (VB: [MainWindow.xaml.vb](./VB/MainWindow.xaml.vb))
<!-- default file list end -->
# How to enable an Excel-like filter dropdown


<p>This example demonstrates how to enable an Excel-like filter dropdown in different DevExpress versions.<br><br><u>In version 16.2</u>, we have implemented the <a href="https://documentation.devexpress.com/#WPF/CustomDocument6133">Excel Style Filtering</a> functionality out of the box. This mode can be enabled by setting a single property - <a href="https://documentation.devexpress.com/WPF/DevExpressXpfGridColumnBase_FilterPopupModetopic.aspx">FilterPopupMode</a>.<br><br><u>In previous versions</u>, where this functionality is not available out of the box, it is possible to filter dropdown items based on the current filter.<br>First, DXGrid has the built-in <a href="https://documentation.devexpress.com/WPF/DevExpressXpfGridColumnBase_ShowAllTableValuesInCheckedFilterPopuptopic.aspx">ShowAllTableValuesInCheckedFilterPopup</a> property that allows enabling the following mode

* If the current column is not filtered, the filter dropdown displays only those items that meet the grid's filter condition.
* Otherwise, all data source items are displayed to simplify the process of the grid filter correction.<br>If this functionality is insufficient, and filtering dropdown items when the current column is filtered is required, use the approach demonstrated in this example. See the <strong>Implementation Details </strong>section for more information.</p>

<br/>


