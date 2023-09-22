using System.Xml.Serialization;

namespace Ans.Net7.Common.Codegen.Schema
{

    public class ManyrefXmlElement
        : _EntityBaseXmlElement
    {

        [XmlAttribute("target")]
        public string Target { get; set; }

    }

}
