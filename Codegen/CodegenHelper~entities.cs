using Ans.Net7.Common.Codegen.Items;
using System.Text;

namespace Ans.Net7.Common.Codegen
{

	public partial class CodegenHelper
	{

		public void MakeEntities()
		{
			var path1 = Path.Combine(DalPath, "Entities");
			SuppIO.CreateDirectoryIfNotExists(path1);
			foreach (var item1 in AllTables)
			{
				var filename1 = Path.Combine(path1, $"{item1.Name}.cs");
				SuppIO.FileWrite(filename1, _getEntity(item1));
			}
		}


		/* privates */


		/* ==================================================================== */
		private string _getEntity(
			 TableItem table)
		{
			var sb1 = new StringBuilder();
			sb1.Append(_getCSharpAttention());
			sb1.Append(@$"
using Ans.Net7.Common;
using Ans.Net7.Common.Attributes;
using Ans.Net7.Common.Crud;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace {DalNamespace}.Entities
{{

    public interface {table.InterfaceName}
		: I{(table.HasMaster ? $"Slave" : "Master")}Entity
	{{{_getEntityInterfaceFields(table)}
    }}


    public partial class {table.BaseClassName}
		: {table.InterfaceName}
	{{

		/* ctors */
		
		public {table.BaseClassName}()
		{{
			// todo: defaults
		}}

		public {table.BaseClassName}(
			{table.InterfaceName} source)
			: this()
		{{
			if (source != null)
				this.Import(source);
		}}

		/* fields */
{_getEntityFields(table)}
	}}


{_getEntityAttributes(table)}
	public partial class {table.EntityName}
		: {table.BaseClassName}
	{{

		/* ctors */

		public {table.EntityName}()
            : base()
        {{
        }}

		public {table.EntityName}(
            {table.InterfaceName} source)
            : base(source)
        {{
        }}
		{_getEntityNavigations(table)}
	}}



    public static partial class _e
	{{

		public static void Import(
			this {table.InterfaceName} item,
			{table.InterfaceName} source)
		{{
			item.Id = source.Id;{_getEntityImport(table)}
		}}

	}}

}}");
			return sb1.ToString();
		}
		/* ==================================================================== */





		/* -------------------------------------------------------------------- */
		private static string _getEntityInterfaceFields(
			TableItem table)
		{
			var sb1 = new StringBuilder();
			foreach (var item1 in table.Fields.Where(x => x.Name != "MasterPtr"))
			{
				sb1.Append(@$"
        {item1.GetCSharpDerlace()}");
			}
			return sb1.ToString();
		}
		/* -------------------------------------------------------------------- */


		/* -------------------------------------------------------------------- */
		private static string _getEntityFields(
			TableItem table)
		{
			var sb1 = new StringBuilder();
			sb1.Append(@$"
        [Key]
        public int Id {{ get; set; }}
");
			foreach (var item1 in table.Fields)
			{
				sb1.Append(@$"{item1.GetAttributes().MakeFromCollection(null, "\n\t\t{0}", null)}
        public {item1.GetCSharpDerlace()}
");
			}
			return sb1.ToString();
		}
		/* -------------------------------------------------------------------- */


		/* -------------------------------------------------------------------- */
		private static string _getEntityAttributes(
			TableItem table)
		{
			var sb1 = new StringBuilder();
			foreach (var item1 in table.Constraints)
				if (table.HasMaster && !item1.IsAbsoluteUnique)
				{
					sb1.Append(@$"
    [Index(nameof(MasterPtr), nameof({item1.Name}), IsUnique = true)]");
				}
				else
				{
					sb1.Append(@$"
    [Index(nameof({item1.Name}), IsUnique = true)]");
				}
			return sb1.ToString();
		}
		/* -------------------------------------------------------------------- */


		/* -------------------------------------------------------------------- */
		private static string _getEntityNavigations(
			TableItem table)
		{
			var sb1 = new StringBuilder();
			if (table.HasNavigations)
			{
				sb1.Append(@"
        /* navigations */
");
				if (table.HasMaster)
				{
					sb1.Append(@$"
		[NotMapped]
		[JsonIgnore]
		public virtual {table.Master.EntityName} Master {{ get; set; }}
");
				}
				if (table.HasReferenceMasters)
					foreach (var ref1 in table.ReferenceMasters)
					{
						sb1.Append(@$"
		[NotMapped]
		[JsonIgnore]
		public virtual {ref1.Table.EntityName} Ref_{ref1.Field.RefPrefix}{ref1.Table.EntityName} {{ get; set; }}
");
					}
				if (table.HasReferenceSlaves)
					foreach (var ref1 in table.ReferenceSlaves)
					{
						sb1.Append(@$"
		[NotMapped]
		[JsonIgnore]
		public virtual ICollection<{ref1.Table.EntityName}> Slave_{ref1.Field.RefPrefix}{ref1.Table.NamePluralize} {{ get; set; }}
");
					}
			}
			return sb1.ToString();
		}
		/* -------------------------------------------------------------------- */


		/* -------------------------------------------------------------------- */
		private static string _getEntityImport(
			TableItem table)
		{
			var sb1 = new StringBuilder();
			foreach (var item1 in table.Fields)
			{
				sb1.Append(@$"
            item.{item1.Name} = source.{item1.Name};");
			}
			return sb1.ToString();
		}
		/* -------------------------------------------------------------------- */

	}

}
