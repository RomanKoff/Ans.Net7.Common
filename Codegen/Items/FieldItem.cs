using Ans.Net7.Common.Codegen.Schema;

namespace Ans.Net7.Common.Codegen.Items
{

	public class FieldItem
		: IProperty
	{

		/* ctors */


		public FieldItem()
		{
		}


		public FieldItem(
			PropertyXmlElement source)
			: this()
		{
			_import(source);
		}


		/* properties */


		public string Name { get; set; }
		public string Face { get; set; }


		public PropertyTypeEnum Type
		{
			get => _type;
			set
			{
				_type = value;
				switch (value)
				{
					case PropertyTypeEnum.Text50:
						TypeCSharp = typeof(string);
						MaxLength = 50;
						break;
					case PropertyTypeEnum.Text100:
						TypeCSharp = typeof(string);
						MaxLength = 100;
						break;
					case PropertyTypeEnum.Text250:
						TypeCSharp = typeof(string);
						MaxLength = 250;
						break;
					case PropertyTypeEnum.Text400:
						TypeCSharp = typeof(string);
						MaxLength = 400;
						break;
					case PropertyTypeEnum.TextBox400:
						TypeCSharp = typeof(string);
						MaxLength = 400;
						break;
					case PropertyTypeEnum.Memo:
						TypeCSharp = typeof(string);
						break;
					case PropertyTypeEnum.Doc:
						TypeCSharp = typeof(string);
						break;
					case PropertyTypeEnum.Name:
						TypeCSharp = typeof(string);
						MaxLength = 50;
						RegularExpression = _Consts.REGEX_NAME;
						break;
					case PropertyTypeEnum.Varname:
						TypeCSharp = typeof(string);
						MaxLength = 50;
						RegularExpression = _Consts.REGEX_VARNAME;
						break;
					case PropertyTypeEnum.Email:
						TypeCSharp = typeof(string);
						MaxLength = 50;
						RegularExpression = _Consts.REGEX_EMAIL;
						break;
					case PropertyTypeEnum.Int:
						TypeCSharp = typeof(int);
						DefaultSql = "0";
						break;
					case PropertyTypeEnum.Long:
						TypeCSharp = typeof(long);
						DefaultSql = "0";
						break;
					case PropertyTypeEnum.Float:
						TypeCSharp = typeof(float);
						DefaultSql = "0";
						break;
					case PropertyTypeEnum.Double:
						TypeCSharp = typeof(double);
						DefaultSql = "0";
						break;
					case PropertyTypeEnum.Decimal:
						TypeCSharp = typeof(decimal);
						DefaultSql = "0";
						break;
					case PropertyTypeEnum.Datetime:
						TypeCSharp = typeof(DateTime);
						IsNullable = true;
						break;
					case PropertyTypeEnum.Date:
						TypeCSharp = typeof(DateOnly);
						IsNullable = true;
						break;
					case PropertyTypeEnum.Time:
						TypeCSharp = typeof(TimeOnly);
						IsNullable = true;
						break;
					case PropertyTypeEnum.Bool:
						TypeCSharp = typeof(bool);
						DefaultSql = "false";
						break;
					case PropertyTypeEnum.Enum:
						TypeCSharp = typeof(int);
						IsRegistry = true;
						IsEnum = true;
						DefaultSql = "0";
						break;
					case PropertyTypeEnum.Set:
						TypeCSharp = typeof(string);
						break;
					case PropertyTypeEnum.Reference:
						TypeCSharp = typeof(int);
						Target = Name;
						Name = $"{RefPrefix}{Name}Ptr";
						IsNullable = true;
						IsRegistry = true;
						IsNullable = true;
						break;
				}
			}
		}
		private PropertyTypeEnum _type;


		public PropertyModeEnum Mode
		{
			get => _mode;
			set
			{
				_mode = value;
				if (value != PropertyModeEnum.Normal)
				{
					IsRequired = true;
					IsNullable = false;
					if (value != PropertyModeEnum.Required)
					{
						IsUnique = true;
						IsAbsoluteUnique = value != PropertyModeEnum.Unique;
					}
				}
			}
		}
		private PropertyModeEnum _mode;


		public int MinLength { get; set; }
		public int MaxLength { get; set; }
		public string RegularExpression { get; set; }


		public string Range
		{
			get => _range;
			set
			{
				_range = value;
				var a2 = new StringParser(value);
				MinValueString = a2.Get(0);
				MaxValueString = a2.Get(2);
			}
		}
		private string _range;


		public string RefPrefix { get; set; }
		public string EnumRegistry { get; set; }
		public string DefaultCSharp { get; set; }
		public string DefaultSql { get; set; }
		public string NullCSharp { get; set; }
		public string Remark { get; set; }
		public bool IsReadonly { get; set; }
		public bool IsNullable { get; set; }

		public string Target { get; set; }
		public TableItem TargetTable { get; set; }
		public string Manyref { get; set; }
		public TableItem ManyrefTable { get; set; }
		public string ManyrefFieldName { get; set; }


		/* readonly properties */


		public Type TypeCSharp { get; private set; }


		public string TypeCSharpName
		{
			get
			{
				if (_typeCSharpName == null)
				{
					var s1 = TypeCSharp.GetCSharpTypeName();
					_typeCSharpName = (IsNullable || s1.StartsWith("Date")) ? $"{s1}?" : s1;
				}
				return _typeCSharpName;
			}
		}
		private string _typeCSharpName;


		public string MinValueString { get; private set; }
		public string MaxValueString { get; private set; }
		public bool IsRequired { get; private set; }
		public bool IsUnique { get; private set; }
		public bool IsAbsoluteUnique { get; private set; }
		public bool IsRegistry { get; private set; }
		public bool IsEnum { get; private set; }


		/* functions */


		public IEnumerable<string> GetAttributes()
		{
			var items1 = new List<string>();
			if (IsRequired)
				items1.Add($"[AnsRequired]");
			if (MinLength > 0)
				items1.Add($"[AnsMinLength({MinLength})]");
			if (MaxLength > 0)
				items1.Add($"[AnsMaxLength({MaxLength})]");
			if (!string.IsNullOrEmpty(RegularExpression))
				items1.Add($"[AnsRegularExpression(\"{RegularExpression}\")]");
			//if (!string.IsNullOrEmpty(DefaultCSharp))
			//	items1.Add($"[DefaultValue({DefaultCSharp})]");
			return items1;
		}


		public string GetCSharpDerlace()
		{
			return $"{TypeCSharpName} {Name} {{ get; set; }}{Remark.Make(" // {0}")}";
		}


		/* privates */


		private void _import(
			IProperty source)
		{
			Name = source.Name;
			Face = source.Face;
			Type = source.Type;
			Mode = source.Mode;

			if (source.MinLength > 0)
				MinLength = source.MinLength;

			if (source.MaxLength > 0)
				MaxLength = source.MaxLength;

			if (!string.IsNullOrEmpty(source.RegularExpression))
				RegularExpression = source.RegularExpression;

			if (!string.IsNullOrEmpty(source.Range))
				Range = source.Range;

			if (!string.IsNullOrEmpty(source.RefPrefix))
				RefPrefix = source.RefPrefix;

			if (!string.IsNullOrEmpty(source.EnumRegistry))
				EnumRegistry = source.EnumRegistry;

			if (!string.IsNullOrEmpty(source.DefaultCSharp))
				DefaultCSharp = source.DefaultCSharp;

			if (!string.IsNullOrEmpty(source.DefaultSql))
				DefaultSql = source.DefaultSql;

			if (!string.IsNullOrEmpty(source.NullCSharp))
				NullCSharp = source.NullCSharp;

			if (!string.IsNullOrEmpty(source.Remark))
				Remark = source.Remark;

			if (source.IsReadonly)
				IsReadonly = true;

			if (source.IsNullable)
				IsNullable = true;
		}

	}

}