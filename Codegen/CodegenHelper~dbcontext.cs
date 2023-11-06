using System.Text;

namespace Ans.Net7.Common.Codegen
{

	public partial class CodegenHelper
	{

		public void MakeDbContext()
		{
			var path1 = Path.Combine(DalPath);
			SuppIO.CreateDirectoryIfNotExists(path1);
			var filename1 = Path.Combine(path1, $"{ContextName}DbContext.cs");
			SuppIO.FileWrite(filename1, _getDbContext());
		}


		/* privates */


		/* ==================================================================== */
		private string _getDbContext()
		{
			var sb1 = new StringBuilder();
			sb1.Append(_getCSharpAttention());
			sb1.Append(@$"
using {DalNamespace}.Entities;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace {DalNamespace}
{{

	public class {ContextName}DbContext
        : DbContext
    {{

		/* ctors */

		public {ContextName}DbContext()
        {{
        }}

		public {ContextName}DbContext(
            DbContextOptions<{ContextName}DbContext> options)
            : base(options)
        {{
        }}

		/* dbsets */
{_getDbSets()}

		/* voids */

		protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {{
			Debug.WriteLine(""[{DalNamespace}.{ContextName}DbContext] OnModelCreating()"");
{_getMapping()}{_getRequireds()}{_getDefaults()}{_getRelSlaves()}{_getRelReferenceMasters()}
		}}

	}}

}}
");
			return sb1.ToString();
		}
		/* ==================================================================== */





		/* -------------------------------------------------------------------- */
		private string _getDbSets()
		{
			var sb1 = new StringBuilder();
			foreach (var item1 in AllTables)
			{
				sb1.Append(@$"
        public DbSet<{item1.Name}> {item1.NamePluralize} {{ get; set; }}");
			}
			return sb1.ToString();
		}
		/* -------------------------------------------------------------------- */


		/* -------------------------------------------------------------------- */
		private string _getMapping()
		{
			var sb1 = new StringBuilder();
			foreach (var item1 in AllTables)
			{
				sb1.Append(@$"
			modelBuilder.Entity<{item1.Name}>()
				.ToTable(""{item1.NamePluralize}"");");
			}
			return sb1.ToString();
		}
		/* -------------------------------------------------------------------- */


		/* -------------------------------------------------------------------- */
		private string _getRequireds()
		{
			var sb1 = new StringBuilder();
			foreach (var item1 in AllTables)
			{
				foreach (var item2 in item1.RequiredFields)
				{
					sb1.Append(@$"

			modelBuilder.Entity<{item1.Name}>()
				.Property(x => x.{item2.Name})
				.IsRequired(true);");
				}
			}
			if (sb1.Length > 0)
			{
				sb1.Insert(0, @$"

            // requireds");
			}
			return sb1.ToString();
		}
		/* -------------------------------------------------------------------- */


		/* -------------------------------------------------------------------- */
		private string _getDefaults()
		{
			var sb1 = new StringBuilder();
			foreach (var item1 in AllTables)
			{
			//	foreach (var item2 in item1.DefaultCSharpFields)
			//	{
			//		sb1.Append(@$"

			//modelBuilder.Entity<{item1.Name}>()
			//	.Property(x => x.{item2.Name})
			//	.HasDefaultValue({item2.DefaultCSharp});");
			//	}
				foreach (var item2 in item1.DefaultSqlFields)
				{
					sb1.Append(@$"

			modelBuilder.Entity<{item1.Name}>()
				.Property(x => x.{item2.Name})
				.HasDefaultValueSql(""{item2.DefaultSql}"");");
				}
			}
			if (sb1.Length > 0)
			{
				sb1.Insert(0, @$"

            // defaults");
			}
			return sb1.ToString();
		}
		/* -------------------------------------------------------------------- */


		/* -------------------------------------------------------------------- */
		private string _getRelSlaves()
		{
			var sb1 = new StringBuilder();
			if (SlaveTables.Any())
			{
				sb1.Append(@$"

            // slaves");
				foreach (var item1 in SlaveTables)
				{
					sb1.Append(@$"

			modelBuilder.Entity<{item1.Name}>()
				.HasOne(x => x.Master)
				.WithMany(x => x.Slave_{item1.NamePluralize})
				.HasForeignKey(x => x.MasterPtr);");
				}
			}
			return sb1.ToString();
		}
		/* -------------------------------------------------------------------- */


		/* -------------------------------------------------------------------- */
		private string _getRelReferenceMasters()
		{
			var sb1 = new StringBuilder();
			if (ReferenceMasterTables.Any())
			{
				sb1.Append(@$"

            // refs ");
				foreach (var item1 in ReferenceMasterTables)
					foreach (var item2 in item1.ReferenceMasters)
					{
						sb1.Append(@$"

			modelBuilder.Entity<{item1.Name}>()
				.HasOne(x => x.Ref_{item2.Field.RefPrefix}{item2.Table.Name})
				.WithMany(x => x.Slave_{item2.Field.RefPrefix}{item1.NamePluralize})
				.HasForeignKey(x => x.{item2.Field.Name});");
					}
			}
			return sb1.ToString();
		}
		/* -------------------------------------------------------------------- */

	}

}
