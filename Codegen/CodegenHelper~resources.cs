using Ans.Net7.Common.Codegen.Items;
using System.Text;

namespace Ans.Net7.Common.Codegen
{

	public partial class CodegenHelper
	{

		public void MakeResources()
		{
			var path1 = Path.Combine(DalPath, "Resources");
			SuppIO.CreateDirectoryIfNotExists(path1);
			SuppIO.FileWrite(Path.Combine(path1, "_Res_Common.resx"), _getResource(Faces));
			foreach (var item1 in AllTables)
			{
				var faces1 = item1.Fields
					.Where(x => !string.IsNullOrEmpty(x.Face))
					.Select(x => new { x.Name, x.Face })
					.ToDictionary(x => x.Name, x => x.Face);
				SuppIO.FileWrite(
					Path.Combine(path1, $"Res_{item1.NamePluralize}.resx"),
					_getResource(faces1));
			}
		}


		/* privates */


		/* ==================================================================== */
		private static string _getResource(
			Dictionary<string, string> items)
		{
			var sb1 = new StringBuilder();
			sb1.Append(@$"<?xml version=""1.0"" encoding=""utf-8""?>
<root>

");
			sb1.Append(_getXmlAttention());
			sb1.Append(@$"
	<resheader name=""resmimetype""><value>text/microsoft-resx</value></resheader>
	<resheader name=""version""><value>2.0</value></resheader>
	<resheader name=""reader""><value>System.Resources.ResXResourceReader, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value></resheader>
	<resheader name=""writer""><value>System.Resources.ResXResourceWriter, System.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089</value></resheader>

");
			foreach (var item1 in items)
			{
				sb1.Append(@$"	<data name=""{item1.Key}"" xml:space=""preserve""><value>{item1.Value}</value></data>
");
			}
			sb1.Append(@$"
</root>");
			return sb1.ToString();
		}
		/* ==================================================================== */

	}

}
