using System.Xml.Serialization;

namespace Ans.Net7.Common.Codegen.Schema
{

	public class EntityXmlElement
		: _EntityXmlElement_Base
	{
		[XmlElement("manyref")]
		public List<ManyrefXmlElement> Manyrefs { get; set; }

		[XmlElement("entity")]
		public List<EntityXmlElement> Entities { get; set; }

		[XmlAttribute("name")]
		public string Name { get; set; }

		[XmlAttribute("type")]
		public EntityTypesEnum Type { get; set; }
	}



	public class _EntityXmlElement_Base
	{
		[XmlElement("property")]
		public List<PropertyXmlElement> Properties { get; set; }

		[XmlAttribute("headers")]
		public string Headers { get; set; }

		[XmlAttribute("timestamp")]
		public bool UseTimestamp { get; set; }

		[XmlAttribute("rem")]
		public string Remark { get; set; }
	}



	public enum EntityTypesEnum
	{
		Normal,
		Tree,
		Ordered,
	}

}
