using Ans.Net7.Common.Codegen.Schema;

namespace Ans.Net7.Common.Codegen.Items
{

	public class TableItem
	{

		/* ctors */


		protected TableItem(
			CatalogItem catalog,
			TableItem master,
			string name,
			int level)
		{
			Catalog = catalog;
			Master = master;
			Name = name;
			Level = level;
			if (master != null)
			{
				master.Slaves.Add(this);
				Fields.Add(new FieldItem
				{
					Name = "Master",
					Type = PropertyTypeEnum.Reference,
					Mode = PropertyModeEnum.Required,
					Target = master.Name,
					IsReadonly = true,
					IsNullable = false,
					Remark = $"^{master.EntityName}",
				});
			}
		}


		public TableItem(
			CatalogItem catalog,
			TableItem master,
			EntityXmlElement entity,
			string name,
			int level)
			: this(catalog, master, name, level)
		{
			_setProperties(entity);
			if (IsTree)
			{
				Fields.Add(new FieldItem
				{
					Name = "Parent",
					Type = PropertyTypeEnum.Reference,
					Mode = PropertyModeEnum.Required,
					Target = Name,
					IsNullable = true,
					Remark = $"^{master.EntityName}",
				});
			}
			if (IsOrdered)
			{
				Fields.Add(new FieldItem
				{
					Type = PropertyTypeEnum.Int,
					Name = "Order",
				});
			}
			if (UseTimestamp)
				_addTimestamp();
			_makeFields(entity.Properties);
		}


		public TableItem(
			CatalogItem catalog,
			TableItem master,
			ManyrefXmlElement manyref,
			int level)
			: this(catalog, null, $"{master.Name}_{manyref.Target}_manyref", level)
		{
			_setProperties(new EntityXmlElement
			{
				UseTimestamp = manyref.UseTimestamp,
				Remark = manyref.Remark
			});
			IsManyref = true;
			ManyrefField1 = new FieldItem
			{
				Type = PropertyTypeEnum.Reference,
				Name = master.Name,
				Target = master.Name,
				Manyref = manyref.Target,
				ManyrefFieldName = manyref.Target,
				IsNullable = false,
				Remark = $"^{master.EntityName}"
			};
			ManyrefField2 = new FieldItem
			{
				Type = PropertyTypeEnum.Reference,
				Name = manyref.Target,
				Target = manyref.Target,
				Manyref = master.Name,
				ManyrefFieldName = master.Name,
				IsNullable = false,
				Remark = $"^{manyref.Target}"
			};
			Fields.Add(ManyrefField1);
			Fields.Add(ManyrefField2);
			if (UseTimestamp)
				_addTimestamp();
			_makeFields(manyref.Properties);
		}


		/* readonly properties */


		public string Name { get; private set; }
		public int Level { get; private set; }
		public bool IsTree { get; private set; }
		public bool IsOrdered { get; private set; }
		public bool UseTimestamp { get; private set; }
		public string HeaderPl { get; private set; }
		public string HeaderWw { get; private set; }
		public string Remark { get; private set; }
		public bool IsManyref { get; private set; }
		public FieldItem ManyrefField1 { get; private set; }
		public FieldItem ManyrefField2 { get; private set; }

		public List<TableItem> Slaves { get; private set; } = new();
		public List<FieldItem> Fields { get; private set; } = new();
		public List<ReferenceItem> ReferenceMasters { get; private set; } = new();
		public List<ReferenceItem> ReferenceSlaves { get; private set; } = new();
		public CatalogItem Catalog { get; private set; }
		public TableItem Master { get; private set; }

		public IEnumerable<FieldItem> Constraints
		   => Fields.Where(x => x.IsUnique);

		public IEnumerable<ReferenceItem> PrimaryManyrefs
			=> ReferenceSlaves.Where(
				x => x.Table.IsManyref && x.Table.ManyrefField1.Target == Name);

		public IEnumerable<ReferenceItem> SecondaryManyrefs
			=> ReferenceSlaves.Where(
				x => x.Table.IsManyref && x.Table.ManyrefField2.Target == Name);

		public IEnumerable<FieldItem> RegistryFields
			=> Fields.Where(x => x.IsRegistry);

		public IEnumerable<FieldItem> EnumFields
			=> Fields.Where(x => x.IsEnum);

		public IEnumerable<FieldItem> RegistryNotEnumFields
			=> Fields.Where(x => x.IsRegistry && !x.IsEnum);

		public IEnumerable<FieldItem> DefaultCSharpFields
			=> Fields.Where(x => !string.IsNullOrEmpty(x.DefaultCSharp));

		public IEnumerable<FieldItem> DefaultSqlFields
			=> Fields.Where(x => !string.IsNullOrEmpty(x.DefaultSql));

		public string NamePluralize
			=> Name.GetPluralizeEn();

		public string EntityName
			=> $"{Name}";

		public string InterfaceName
			=> $"I{EntityName}";

		public string BaseClassName
			=> $"_{EntityName}_Base";

		public bool HasMaster
			=> Master != null;

		public bool HasSlave
			=> Slaves.Any();

		public bool HasReferenceMasters
			=> ReferenceMasters.Any();

		public bool HasReferenceSlaves
			=> ReferenceSlaves.Any();

		public bool HasNavigations
			=> HasMaster || HasSlave || HasReferenceMasters || HasReferenceSlaves;


		/* privates */


		private void _setProperties(
			EntityXmlElement entity)
		{
			IsTree = entity.Type == EntityTypesEnum.Tree;
			IsOrdered = entity.Type != EntityTypesEnum.Normal;
			var a1 = new StringParser(entity.Headers);
			HeaderPl = a1.Get(0);
			HeaderWw = a1.Get(1);
			UseTimestamp = entity.UseTimestamp;
			Remark = entity.Remark ?? "";
		}


		private void _addTimestamp()
		{
			Fields.Add(new FieldItem
			{
				Type = PropertyTypeEnum.Datetime,
				Mode = PropertyModeEnum.Required,
				Name = "DateCreate",
				IsNullable = false,
				DefaultSql = "LOCALTIMESTAMP",
			});
			Fields.Add(new FieldItem
			{
				Type = PropertyTypeEnum.Datetime,
				Name = "DateUpdate",
				DefaultSql = "LOCALTIMESTAMP"
			});
		}


		private void _makeFields(
		   IEnumerable<PropertyXmlElement> properties)
		{
			foreach (var item1 in properties)
				Fields.Add(new FieldItem(item1));
		}

	}

}