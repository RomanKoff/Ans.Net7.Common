using System.Xml.Serialization;

namespace Ans.Net7.Common.Codegen.Schema
{

	public interface IProperty
	{
		string Name { get; set; }
		string Face { get; set; }
		PropertyTypeEnum Type { get; set; }
		PropertyModeEnum Mode { get; set; }
		int MinLength { get; set; }
		int MaxLength { get; set; }
		string RegularExpression { get; set; }
		string Range { get; set; }
		string RefPrefix { get; set; }
		string EnumRegistry { get; set; }
		string DefaultCSharp { get; set; }
		string DefaultSql { get; set; }
		string NullCSharp { get; set; }
		string Remark { get; set; }
		bool IsReadonly { get; set; }
		bool IsNullable { get; set; }
	}



	public class PropertyXmlElement
		: IProperty
	{
		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("face")]
		public string Face { get; set; }

		[XmlAttribute("type")]
		public PropertyTypeEnum Type { get; set; }

		[XmlAttribute("mode")]
		public PropertyModeEnum Mode { get; set; }

		[XmlAttribute("minLength")]
		public int MinLength { get; set; }

		[XmlAttribute("maxLength")]
		public int MaxLength { get; set; }

		[XmlAttribute("regex")]
		public string RegularExpression { get; set; }

		[XmlAttribute("range")]
		public string Range { get; set; }

		[XmlAttribute("prefix")]
		public string RefPrefix { get; set; }

		[XmlAttribute("enum")]
		public string EnumRegistry { get; set; }

		[XmlAttribute("defaultCSharp")]
		public string DefaultCSharp { get; set; }

		[XmlAttribute("defaultSql")]
		public string DefaultSql { get; set; }

		[XmlAttribute("nullCSharp")]
		public string NullCSharp { get; set; }

		[XmlAttribute("rem")]
		public string Remark { get; set; }

		[XmlAttribute("readonly")]
		public bool IsReadonly { get; set; }

		[XmlAttribute("nullable")]
		public bool IsNullable { get; set; }
	}



	public enum PropertyTypeEnum
	{
		Text50, Text100, Text250, Text400, TextBox400,
		Memo, Doc, Name, Varname, Email,
		Int, Long, Float, Double, Decimal,
		Datetime, Date, Time,
		Bool, Enum, Set,
		Reference,
	}



	public enum PropertyModeEnum
	{
		Normal,
		Required,
		Unique,
		AbsoluteUnique
	}

}
