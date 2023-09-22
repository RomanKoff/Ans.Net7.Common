using System.Xml.Serialization;

namespace Ans.Net7.Common.Codegen.Schema
{

    public class PropertyXmlElement
    {

        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("type")]
        public PropertyTypesEnum Type { get; set; }

        [XmlAttribute("use")]
        public PropertyUsesEnum Use { get; set; }

        [XmlAttribute("defaultValue")]
        public string DefaultValue { get; set; }

        [XmlAttribute("defaultSql")]
        public string DefaultSql { get; set; }

        [XmlAttribute("readonly")]
        public bool IsReadonly { get; set; }

        [XmlAttribute("perfix")]
        public string Prefix { get; set; }

        [XmlAttribute("enum")]
        public string Enum { get; set; }

        [XmlAttribute("face")]
        public string Face { get; set; }

        [XmlAttribute("rem")]
        public string Remark { get; set; }

    }



    public enum PropertyTypesEnum
    {
        Text50, Text100, Text250, Text400, TextBox400,
        Memo, Doc, Name, Varname, Email,
        Int, Long, Float, Double, Decimal,
        Datetime, Date, Time,
        Bool, Enum, Set,
        Reference, PtrInt
    }



    public enum PropertyUsesEnum
    {
        Normal,
        Required,
        Unique,
        AbsoluteUnique
    }

}
