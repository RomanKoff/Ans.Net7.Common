using Ans.Net7.Common.Codegen.Items;
using System.Text;

namespace Ans.Net7.Common.Codegen
{

	public partial class CodegenHelper
	{

		/* ==================================================================== */
		private static string _getViewList(
			 TableItem table)
		{
			var sb1 = new StringBuilder();
			sb1.Append(_getRazorAttention());
			sb1.Append(@$"@model IEnumerable<{table.Name}>
@{{
	Current.Page.RemoveParentLast();
	Current.Page.Title = ""{table.HeaderPl}"";
}}
<a class=""btn btn-primary"" asp-action=""Add"">Создать</a>
@if (Model?.Any() ?? false)
{{
	<table class=""table table-hovered"">
		@foreach (var item1 in Model)
		{{
			<tr>
				<td><a class=""text-success"" asp-action=""Edit"" asp-route-id=""@item1.Id"" title=""редактировать""><i class=""bi-pencil-square""></i></a></td>

{_getViewList_fields(table)}
				<td><a class=""text-danger"" asp-action=""Delete"" asp-route-id=""@item1.Id"" title=""удалить""><i class=""bi-x-square""></i></a></td>
			</tr>
		}}
	</table>
	<a class=""btn btn-primary"" asp-action=""Add"">Создать</a>
}}
else
{{
	<p>Нет элементов.</p>
}}
");
			return sb1.ToString();
		}
		/* ==================================================================== */


		/* -------------------------------------------------------------------- */
		private static string _getViewList_fields(
			 TableItem table)
		{
			var sb1 = new StringBuilder();
			foreach (var item1 in table.Fields)
			{
				sb1.Append(@$"				<td><ans-list-display for=""@item1.{item1.Name}"" /></td>
");
			}
			//<td><a asp-action=""Details"" asp-route-id=""@item1.Id"">@item1.Title</a></td>			
			return sb1.ToString();
		}
		/* -------------------------------------------------------------------- */

	}

}
