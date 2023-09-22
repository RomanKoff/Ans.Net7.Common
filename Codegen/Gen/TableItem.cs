using Ans.Net7.Common.Codegen.Schema;
using System.Text;

namespace Ans.Net7.Common.Codegen.Gen
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
                var prop1 = new PropertyXmlElement
                {
                    Name = "ItemMasterPtr",
                    Type = PropertyTypesEnum.PtrInt,
                    Use = PropertyUsesEnum.Required,
                    IsReadonly = true,
                    Remark = $"^{master.Name}",
                };
                Fields.Add(new FieldItem(prop1)
                {
                    CSharpType = "int?",
                    TargetTable = master,
                    IsRegistry = true,
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
                var prop1 = new PropertyXmlElement
                {
                    Name = "ItemParentPtr",
                    Type = PropertyTypesEnum.PtrInt,
                };
                Fields.Add(new FieldItem(prop1)
                {
                    CSharpType = "int?",
                    TargetTable = this,
                    IsRegistry = true,
                    Remark = $"^{name}"
                });
            }
            if (IsOrdered)
            {
                var prop1 = new PropertyXmlElement
                {
                    Name = "ItemOrder",
                    Type = PropertyTypesEnum.Int,
                };
                Fields.Add(new FieldItem(prop1)
                {
                    CSharpType = "int",
                });
            }
            if (UseTimestamp)
                _appentTimestamp();
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
            var prop1 = new PropertyXmlElement
            {
                Name = "ItemRef" + master.Name + "Ptr",
                Type = PropertyTypesEnum.PtrInt
            };
            var prop2 = new PropertyXmlElement
            {
                Name = "ItemRef" + manyref.Target + "Ptr",
                Type = PropertyTypesEnum.PtrInt
            };
            ManyrefField1 = new FieldItem(prop1)
            {
                CSharpType = "int?",
                Target = master.Name,
                Manyref = manyref.Target,
                ManyrefFieldName = prop2.Name,
                IsRegistry = true,
                Remark = $"^{master.Name}"
            };
            ManyrefField2 = new FieldItem(prop2)
            {
                CSharpType = "int?",
                Target = manyref.Target,
                Manyref = master.Name,
                ManyrefFieldName = prop1.Name,
                IsRegistry = true,
                Remark = $"^{manyref.Target}"
            };
            Fields.Add(ManyrefField1);
            Fields.Add(ManyrefField2);
            if (UseTimestamp)
                _appentTimestamp();
            _makeFields(manyref.Properties);
        }


        /* properties */


        public string Name { get; set; }
        public int Level { get; set; }
        public bool IsTree { get; set; }
        public bool IsOrdered { get; set; }
        public bool UseTimestamp { get; set; }
        public string Remark { get; set; }
        public bool IsManyref { get; set; }
        public FieldItem ManyrefField1 { get; set; }
        public FieldItem ManyrefField2 { get; set; }


        /* readonly properties */


        public List<TableItem> Slaves { get; private set; } = new();
        public List<FieldItem> Fields { get; private set; } = new();
        public List<ReferenceItem> ReferenceMasters { get; private set; } = new();
        public List<ReferenceItem> ReferenceSlaves { get; private set; } = new();

        public CatalogItem Catalog { get; private set; }
        public TableItem Master { get; private set; }

        public string BaseClass { get; private set; }

        public string NamePluralize
            => Name.GetPluralizeEn();

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

        public IEnumerable<FieldItem> Constraints
            => Fields.Where(x => x.IsUnique);

        public IEnumerable<ReferenceItem> PrimaryManyrefs
            => ReferenceSlaves.Where(
                x => x.Table.IsManyref && x.Table.ManyrefField1.TargetTable == this);

        public IEnumerable<ReferenceItem> SecondaryManyrefs
            => ReferenceSlaves.Where(
                x => x.Table.IsManyref && x.Table.ManyrefField2.TargetTable == this);

        public IEnumerable<FieldItem> RegistryFields
            => Fields.Where(x => x.IsRegistry);

        public IEnumerable<FieldItem> EnumFields
            => Fields.Where(x => x.IsEnum);

        public IEnumerable<FieldItem> RegistryNotEnumFields
            => Fields.Where(x => x.IsRegistry && !x.IsEnum);

        public IEnumerable<FieldItem> DefaultValueFields
            => Fields.Where(x => !string.IsNullOrEmpty(x.DefaultValue));

        public IEnumerable<FieldItem> DefaultSqlFields
            => Fields.Where(x => !string.IsNullOrEmpty(x.DefaultSql));


        /* functions */


        public string GetEntityInterfaceFields()
        {
            var sb1 = new StringBuilder();
            //sb1.Append($"\n\t\tint Id {{ get; set; }}");
            foreach (var fld1 in Fields.Where(x => x.Name != "ItemMasterPtr"))
                sb1.Append($"\n\t\t{fld1.GetCSharpDerlace()}");
            return sb1.ToString();
        }


        public string GetEntityAttributes()
        {
            var sb1 = new StringBuilder();
            foreach (var fld1 in Constraints)
                if (HasMaster && !fld1.IsAbsoluteUnique)
                    sb1.Append($"\n\t[Index(nameof(ItemMasterPtr), nameof({fld1.Name}), IsUnique = true)]");
                else
                    sb1.Append($"\n\t[Index(nameof({fld1.Name}), IsUnique = true)]");
            return sb1.ToString();
        }


        public string GetEntityFields()
        {
            var sb1 = new StringBuilder();
            sb1.Append($"\n\t\t[Key]\n\t\tpublic int Id {{ get; set; }}\n");
            foreach (var fld1 in Fields)
                sb1.Append($"\n\t\t{fld1.GetCSharpAttribute()}\n\t\tpublic {fld1.GetCSharpDerlace()}\n");
            return sb1.ToString();
        }


        public string GetImportOperations()
        {
            var sb1 = new StringBuilder();
            foreach (var fld1 in Fields)
                sb1.Append($"\n\t\t\titem.{fld1.Name} = source.{fld1.Name};");
            return sb1.ToString();
        }


        public string GetNavigations()
        {
            var sb1 = new StringBuilder();
            if (HasNavigations)
            {
                sb1.Append("\n\n\t\t/* navigations */\n");
                if (HasMaster)
                {
                    sb1.Append(@$"
		[NotMapped]
		[JsonIgnore]
		public virtual {Master.Name} Master {{ get; set; }}
");
                }
                if (HasReferenceMasters)
                    foreach (var ref1 in ReferenceMasters)
                        sb1.Append(@$"
		[NotMapped]
		[JsonIgnore]
		public virtual {ref1.Table.Name} Ref_{ref1.Field.Prefix}{ref1.Table.Name} {{ get; set; }}
");
                if (HasReferenceSlaves)
                    foreach (var ref1 in ReferenceSlaves)
                        sb1.Append(@$"
		[NotMapped]
		[JsonIgnore]
		public virtual ICollection<{ref1.Table.Name}> Slave_{ref1.Field.Prefix}{ref1.Table.NamePluralize} {{ get; set; }}
");
            }
            return sb1.ToString();
        }


        /* privates */


        private void _setProperties(
            EntityXmlElement entity)
        {
            IsTree = entity.Type == EntityTypesEnum.Tree;
            IsOrdered = entity.Type != EntityTypesEnum.Normal;
            UseTimestamp = entity.UseTimestamp;
            Remark = entity.Remark ?? "";
        }


        private void _makeFields(
           IEnumerable<PropertyXmlElement> properties)
        {
            foreach (var prop1 in properties)
                Fields.Add(new FieldItem(prop1));
        }


        protected void _appentTimestamp()
        {
            Fields.Add(new FieldItem(
                new PropertyXmlElement
                {
                    Name = "ItemDateCreate",
                    Type = PropertyTypesEnum.Datetime,
                    DefaultSql = "LOCALTIMESTAMP"
                })
            {
                CSharpType = "DateTime?",
            });
            Fields.Add(new FieldItem(
                new PropertyXmlElement
                {
                    Name = "ItemDateUpdate",
                    Type = PropertyTypesEnum.Datetime,
                    DefaultSql = "LOCALTIMESTAMP"
                })
            {
                CSharpType = "DateTime?",
            });
        }

    }

}