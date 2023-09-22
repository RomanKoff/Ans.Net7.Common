using Ans.Net7.Common.Codegen.Gen;
using Ans.Net7.Common.Codegen.Schema;
using System.Text;

namespace Ans.Net7.Common.Codegen
{

    public class GenHelper
    {

        /* ctors */


        public GenHelper(
            SchemaXmlRoot schema,
            string projectDalName,
            string catalogDal)
        {
            SolutionPath = SuppApp.SolutionPath;
            DalPath = Path.Combine(SolutionPath, projectDalName, catalogDal);
            DalNs = string.IsNullOrEmpty(catalogDal)
                ? projectDalName
                : $"{projectDalName}.{catalogDal}";
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
                        if (field1.Name != "ItemMasterPtr")
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


        public GenHelper(
            string projectDalName,
            string catalogDal)
            : this(
                  SuppXml.GetObjectFromXmlFile<SchemaXmlRoot>(
                      Path.Combine(SuppApp.ProjectPath, "schema.xml"),
                      "http://tempuri.org/Ans.Net7.Common.Codegen.xsd"),
                  projectDalName,
                  catalogDal)
        {
        }



        /* properties */


        public string SolutionPath { get; private set; }
        public string DalPath { get; private set; }
        public string DalNs { get; private set; }



        /* readonly properties */


        public Dictionary<string, string> Faces { get; private set; } = new();
        public List<CatalogItem> Catalogs { get; private set; } = new();

        public IEnumerable<TableItem> AllTables
            => Catalogs.SelectMany(x => x.AllTables);

        public IEnumerable<TableItem> SlaveTables
            => AllTables.Where(x => x.HasMaster);

        public IEnumerable<TableItem> ReferenceMasterTables
            => AllTables.Where(x => x.HasReferenceMasters);



        /* funcs */


        public string GetDbSets()
        {
            var sb1 = new StringBuilder();
            foreach (var tab1 in AllTables)
                sb1.Append($"\n\t\tpublic DbSet<{tab1.Name}> {tab1.NamePluralize} {{ get; set; }}");
            return sb1.ToString();
        }


        public string GetModelMapping()
        {
            var sb1 = new StringBuilder();
            foreach (var tab1 in AllTables)
                sb1.Append(@$"
			modelBuilder.Entity<{tab1.Name}>()
				.ToTable(""{tab1.NamePluralize}"");");
            return sb1.ToString();
        }


        public string GetModelDefaults()
        {
            var sb1 = new StringBuilder();
            foreach (var tab1 in AllTables)
            {
                foreach (var fld1 in tab1.DefaultValueFields)
                    sb1.Append(@$"

			modelBuilder.Entity<{tab1.Name}>()
				.Property(x => x.{fld1.Name})
				.HasDefaultValue({fld1.DefaultValue});");

                foreach (var fld1 in tab1.DefaultSqlFields)
                    sb1.Append(@$"

			modelBuilder.Entity<{tab1.Name}>()
				.Property(x => x.{fld1.Name})
				.HasDefaultValueSql(""{fld1.DefaultSql}"");");
            }
            if (sb1.Length > 0)
                sb1.Insert(0, $"\n\n\t\t\t// defaults");
            return sb1.ToString();
        }


        public string GetModelRelSlaves()
        {
            var sb1 = new StringBuilder();
            if (SlaveTables.Any())
            {
                sb1.Append($"\n\n\t\t\t// slaves");
                foreach (var tab1 in SlaveTables)
                    sb1.Append(@$"

			modelBuilder.Entity<{tab1.Name}>()
				.HasOne(x => x.Master)
				.WithMany(x => x.Slave_{tab1.NamePluralize})
				.HasForeignKey(x => x.ItemMasterPtr);");
            }
            return sb1.ToString();
        }


        public string GetModelRelReferenceMasters()
        {
            var sb1 = new StringBuilder();
            if (ReferenceMasterTables.Any())
            {
                sb1.Append($"\n\n\t\t\t// refs ");
                foreach (var tab1 in ReferenceMasterTables)
                    foreach (var ref1 in tab1.ReferenceMasters)
                        sb1.Append(@$"

			modelBuilder.Entity<{tab1.Name}>()
				.HasOne(x => x.Ref_{ref1.Field.Prefix}{ref1.Table.Name})
				.WithMany(x => x.Slave_{ref1.Field.Prefix}{tab1.NamePluralize})
				.HasForeignKey(x => x.{ref1.Field.Name});");
            }
            return sb1.ToString();
        }


        public string GetDateUpdateTriggers()
        {
            var sb1 = new StringBuilder();
            foreach (var tab1 in AllTables.Where(x => x.UseTimestamp))
            {
                sb1.Append(@$"
				context1.CreateTrigger_ItemDateUpdate(""{tab1.NamePluralize}"");");
            }
            if (sb1.Length > 0)
            {
                sb1.Insert(0, @"
				// timestamp update triggers
				context1.CreateFunction_ItemDateUpdate();");
                sb1.AppendLine();
            }
            return sb1.ToString();
        }



        /* voids */


        public void MakeEntities()
        {
            var path1 = Path.Combine(DalPath, "Entities");
            SuppIO.CreateDirectoryIfNotExists(path1);
            foreach (var tab1 in AllTables)
            {
                var filename1 = Path.Combine(path1, $"{tab1.Name}.cs");
                SuppIO.FileWrite(filename1, _getEntities(tab1, DalNs));
            }
        }


        public void MakeRepositories()
        {
            var path1 = Path.Combine(DalPath, "Repositories");
            SuppIO.CreateDirectoryIfNotExists(path1);
            foreach (var tab1 in AllTables)
            {
                var filename1 = Path.Combine(path1, $"{tab1.NamePluralize}Repository.cs");
                SuppIO.FileWrite(filename1, _getRepositories(tab1, DalNs));
            }
        }


        public void MakeAppDbContext()
        {
            var path1 = Path.Combine(DalPath);
            SuppIO.CreateDirectoryIfNotExists(path1);
            var filename1 = Path.Combine(path1, $"AppDbContext.cs");
            SuppIO.FileWrite(filename1, _getAppDbContext(DalNs));
        }


        public void MakeAppDbInitializer()
        {
            var path1 = Path.Combine(DalPath);
            SuppIO.CreateDirectoryIfNotExists(path1);
            var filename1 = Path.Combine(path1, $"AppDbInitializer.cs");
            SuppIO.FileWrite(filename1, _getAppDbInitializer(DalNs));
        }



        /* privates */





        /// <summary>
        /// Table
        /// </summary>
        private TableItem _getTable(
            string name)
        {
            var a1 = AllTables.Where(x => x.Name == name).ToArray();
            if (a1.Length == 0)
                throw new Exception(
                    $"GenHelper: Table [{name}] not found!");
            if (a1.Length > 1)
                throw new Exception(
                    $"GenHelper: More than one table named [{name}] found!");
            return a1[0];
        }





        /// <summary>
        /// Attention
        /// </summary>
        private string _getAttention()
        {
            return @$"/*
 *
 * Attention!
 *
 * This code is generated automatically.
 * The changes made may be lost during the next update
 *
 * Generation: {DateTime.Now}
 *
 */
";
        }





        /// <summary>
        /// Entities
        /// </summary>
        private string _getEntities(
            TableItem table,
            string ns)
        {
            var sb1 = new StringBuilder(_getAttention());
            sb1.Append(@$"
using Ans.Net7.Common;
using Ans.Net7.Common.Attributes.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace {ns}.Entities
{{
	
	public interface _I{table.Name}
		: {(table.HasMaster ? "IEntitySlave" : "IEntity")}
	{{{table.GetEntityInterfaceFields()}
	}}



	public partial class _{table.Name}_Base
		: _I{table.Name},
		{(table.HasMaster ? "IEntitySlave" : "IEntity")}
	{{

		/* ctors */
		
		public _{table.Name}_Base()
		{{
			// todo: defaults
		}}

		public _{table.Name}_Base(
			_I{table.Name} source)
			: this()
		{{
			if (source != null)
				this.Import(source);
		}}


		/* fields */
{table.GetEntityFields()}
	}}


	{table.GetEntityAttributes()}
	public partial class {table.Name}
		: _{table.Name}_Base,
		_I{table.Name},
		{(table.HasMaster ? "IEntitySlave" : "IEntity")}
	{{

		/* ctors */

		public {table.Name}() : base() {{}}
		public {table.Name}(_I{table.Name} source) : base(source) {{}}
		{table.GetNavigations()}
	}}



	public static partial class _e
	{{

		public static void Import(
			this _I{table.Name} item,
			_I{table.Name} source)
		{{
			item.Id = source.Id;{table.GetImportOperations()}
		}}

	}}

}}");
            return sb1.ToString();
        }





        /// <summary>
        /// Repositories
        /// </summary>
        private string _getRepositories(
            TableItem table,
            string ns)
        {
            var sb1 = new StringBuilder(_getAttention());
            sb1.Append(@$"
using Ans.Net7.Common;
using Ans.Net7.Psql.Cruds;
using {ns}.Entities;
using Microsoft.EntityFrameworkCore;

namespace {ns}.Repositories
{{
	
	public partial class {table.NamePluralize}Repository
		: _CrudEntityRepository_Base<{table.Name}>,
		ICrudEntityRepository<{table.Name}>
	{{

		public {table.NamePluralize}Repository(
			DbContext dbContext)
			: base(dbContext, ""{table.Name}"")
		{{
		}}
");
            if (table.HasMaster)
                sb1.Append(@$"

		/* Slave table */


		public override IEnumerable<{table.Name}> GetItems(
			int ptr,
			string order,
			bool isDescending)
		{{
			if (order == null)
			{{
				order = ""Id""; // TitleField;
				isDescending = false;
			}}
			var items = GetItemsAsQueryable(
				x => x.ItemMasterPtr == ptr, order, isDescending)
					.ToList();
			return items;
		}}


		public override {table.Name} GetNew(
			int ptr)
		{{
			var item1 = new {table.Name} {{ ItemMasterPtr = ptr }};
			return item1;
		}}
");
            else
                sb1.Append(@$"

		/* Master table */


		public override IEnumerable<{table.Name}> GetItems(
			string order,
			bool isDescending)
		{{
			if (order == null)
			{{
				order = ""Id""; // TitleField;
				isDescending = false;
			}}
			var items = GetItemsAsQueryable(
				null, order, isDescending)
					.ToList();
			return items;
		}}


		public override {table.Name} GetNew()
		{{
			var item1 = new {table.Name}();
			return item1;
		}}
");
            sb1.Append(@$"
	}}

}}");
            return sb1.ToString();
        }





        /// <summary>
        /// AppDbContext
        /// </summary>
        private string _getAppDbContext(
            string ns)
        {
            var sb1 = new StringBuilder(_getAttention());
            sb1.Append(@$"
using {ns}.Entities;
using Microsoft.EntityFrameworkCore;

namespace {ns}
{{

	public class AppDbContext
        : DbContext
    {{

		/* ctors */


		public AppDbContext()
        {{
        }}


		public AppDbContext(
            DbContextOptions<AppDbContext> options)
            : base(options)
        {{
        }}


		/* dbsets */

{GetDbSets()}



		/* voids */


		protected override void OnModelCreating(
            ModelBuilder modelBuilder)
        {{{GetModelMapping()}{GetModelDefaults()}{GetModelRelSlaves()}{GetModelRelReferenceMasters()}
		}}

	}}

}}");
            return sb1.ToString();
        }





        /// <summary>
        /// AppDbInitializer
        /// </summary>
        private string _getAppDbInitializer(
            string ns)
        {
            var sb1 = new StringBuilder(_getAttention());
            sb1.Append(@$"
using Ans.Net7.Psql;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;

namespace {ns}
{{

	public static partial class AppDbInitializer
    {{

		public static void AppDbPrepare(
			this IHost host,
			bool useDbInit)
		{{
			using var scope1 = host.Services.CreateScope();
			var provider1 = scope1.ServiceProvider;
			var context1 = provider1.GetRequiredService<AppDbContext>();
			if (context1.Database.EnsureCreated())
			{{
				{GetDateUpdateTriggers()}
				Debug.WriteLine(""APP: Create DB"");
				//if (useDbInit)
				//	Initialize(context1);
			}}
		}}


		/*
		public static void Initialize(
			AppDbContext context)
		{{
			if (context.{AllTables.First().NamePluralize}.Any())
				return;
			Debug.WriteLine(""APP: Init DB"");
		}}
		*/

	}}

}}");
            return sb1.ToString();
        }

    }

}
