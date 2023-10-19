using Ans.Net7.Common.Codegen.Items;
using System.Text;

namespace Ans.Net7.Common.Codegen
{

	public partial class CodegenHelper
	{

		/* ==================================================================== */
		private static string _getViewStart(
			 TableItem table)
		{
			var sb1 = new StringBuilder();
			sb1.Append(_getRazorAttention());
			sb1.Append(@$"@{{
	Current.Page.AddParent(Url.Action(""List""), ""{table.HeaderPl}"");
	Current.Page.HideParentInTitle = true;
}}
");
			return sb1.ToString();
		}
		/* ==================================================================== */

	}

}
