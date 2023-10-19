using System.Xml.Serialization;

namespace Ans.Net7.Common.Codegen.Schema
{

	public class FaceXmlElement
	{
		[XmlAttribute("key")]
		public string Key { get; set; }

		[XmlAttribute("value")]
		public string Value { get; set; }
	}

}
