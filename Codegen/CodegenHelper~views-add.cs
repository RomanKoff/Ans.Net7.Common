﻿using Ans.Net7.Common.Codegen.Items;
using System.Text;

namespace Ans.Net7.Common.Codegen
{

	public partial class CodegenHelper
	{

		/* ==================================================================== */
		private string _getViewAdd(
			 TableItem table)
		{
			var sb1 = new StringBuilder();
			sb1.Append(_getRazorAttention());
			sb1.Append(@$"@using {DalNamespace}.Resources;
@using Microsoft.AspNetCore.Mvc.ModelBinding;
@model {table.Name}
@{{
	Current.Page.Title = ""Создание {table.HeaderWw}"";
}}

<ans-from-resources entity=""Res_{table.NamePluralize}.ResourceManager"" common=""_Res_Common.ResourceManager"" />

<form asp-action=""Add"">
	<div asp-validation-summary=""ModelOnly"" class=""text-danger""></div>
{_getViewAdd_fields(table)}

	<div class=""my-3"">
		<input class=""btn btn-primary"" type=""submit"" value=""Создать"" />
		<a class=""btn btn-light"" asp-action=""List"">Отменить</a>
	</div>

</form>");
			return sb1.ToString();
		}
		/* ==================================================================== */


		/* -------------------------------------------------------------------- */
		private static string _getViewAdd_fields(
			 TableItem table)
		{
			var sb1 = new StringBuilder();
			foreach (var item1 in table.Fields)
			{
				sb1.Append(@$"
	<ans-field for=""@Model.{item1.Name}"">
		<input class=""form-control"" asp-for=""{item1.Name}"" />
	</ans-field>");
			}
			return sb1.ToString();
		}
		/* -------------------------------------------------------------------- */

	}

}
