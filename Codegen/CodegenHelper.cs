using Ans.Net7.Common.Codegen.Items;
using Ans.Net7.Common.Codegen.Schema;

namespace Ans.Net7.Common.Codegen
{

	public partial class CodegenHelper
	{

		/* ctors */


		public CodegenHelper(
			SchemaXmlRoot schema,
			string projectName,
			string dirName,
			string contextName)
		{
			ContextName = contextName ?? "App";
			SolutionPath = SuppApp.SolutionPath;
			DalPath = Path.Combine(SolutionPath, projectName, dirName ?? "");
			DalNamespace = string.IsNullOrEmpty(dirName)
				? projectName : $"{projectName}.{dirName}";
			foreach (var face1 in schema.Faces)
				Faces.Add(face1.Key, face1.Value);
			foreach (var catalog1 in schema.Catalogs)
				Catalogs.Add(new CatalogItem(catalog1));
			foreach (var table1 in AllTables)
			{
				foreach (var field1 in table1.Fields)
				{
					if (!string.IsNullOrEmpty(field1.Target) && field1.TargetTable == null)
						field1.TargetTable = _getTable(field1.Target);
					if (field1.TargetTable != null)
					{
						if (field1.Name != "MasterPtr")
							table1.ReferenceMasters.Add(new ReferenceItem
							{
								Field = field1,
								Table = field1.TargetTable,
							});
						field1.TargetTable.ReferenceSlaves.Add(new ReferenceItem
						{
							Field = field1,
							Table = table1
						});
					}
					if (!string.IsNullOrEmpty(field1.Manyref))
						field1.ManyrefTable = _getTable(field1.Manyref);
				}
			}
		}


		public CodegenHelper(
			string projectName,
			string dirName,
			string contextName)
			: this(
				SuppXml.GetObjectFromXmlFile<SchemaXmlRoot>(
					Path.Combine(SuppApp.ProjectPath, "schema.xml"),
					"http://tempuri.org/Ans.Net7.Common.Codegen.xsd"),
				projectName, dirName, contextName)
		{
		}


		/* readonly properties */


		public string ContextName { get; private set; }
		public string SolutionPath { get; private set; }
		public string DalPath { get; private set; }
		public string DalNamespace { get; private set; }

		public Dictionary<string, string> Faces { get; private set; } = new();
		public List<CatalogItem> Catalogs { get; private set; } = new();

		public IEnumerable<TableItem> AllTables
			=> Catalogs.SelectMany(x => x.AllTables);

		public IEnumerable<TableItem> SlaveTables
			=> AllTables.Where(x => x.HasMaster);

		public IEnumerable<TableItem> ReferenceMasterTables
			=> AllTables.Where(x => x.HasReferenceMasters);


		/* privates */


		private TableItem _getTable(
			string name)
		{
			var a1 = AllTables.Where(x => x.Name == name);
			return a1.Count() switch
			{
				1 => a1.First(),
				0 => throw new Exception($"GenHelper: Table [{name}] not found!"),
				_ => throw new Exception($"GenHelper: More than one table named [{name}] found!")
			};
		}


		private static string _getCSharpAttention()
		{
			return @$"/*
 *
 * Attention!
 * This code is generated automatically.
 * The changes made may be lost during the next update
 *
 * Generation: {DateTime.Now}
 *
 */
";
		}


		private static string _getRazorAttention()
		{
			return @$"@*
Attention!
This code is generated automatically.
The changes made may be lost during the next update
Generation: {DateTime.Now}
*@
";
		}


		private static string _getXmlAttention()
		{
			return @$"	<!--
	Attention!
	This code is generated automatically.
	The changes made may be lost during the next update
	Generation: {DateTime.Now}
	-->
";
		}

	}

}
