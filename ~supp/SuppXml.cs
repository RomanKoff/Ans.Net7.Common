using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace Ans.Net7.Common
{

    public static class SuppXml
    {

        /*
         * string GetXmlStringFromObject(object obj);
		 * XmlDocument GetXmlDocumentFromObject(object obj);
		 * T GetObjectFromXml<T>(string source, string defaultNamespace = null);
		 * T GetObjectFromXml<T>(XmlDocument source, string defaultNamespace = null);
		 * T GetObjectFromXml<T>(Stream stream, string defaultNamespace = null);
		 * T GetObjectFromXmlFile<T>(string filename, string defaultNamespace = null);
		 * 
		 * void SaveObjectToXmlFile(object obj, string filename);
         */


        /* functions */


        public static string GetXmlStringFromObject(
            object obj)
        {
            if (obj == null)
                return string.Empty;
            var serializer1 = new XmlSerializer(obj.GetType()); // SerializerCache.GetSerializer(type)
            var ns1 = new XmlSerializerNamespaces(
                new XmlQualifiedName[] { new XmlQualifiedName(string.Empty) });
            var sb1 = new StringBuilder();
            using var writer1 = new StringWriter(
                sb1, CultureInfo.InvariantCulture);
            serializer1.Serialize(
                writer1, obj, ns1);
            return sb1.ToString();
        }


        public static XmlDocument GetXmlDocumentFromObject(
            object obj)
        {
            if (obj == null)
                return null;
            var xml1 = new XmlDocument();
            xml1.LoadXml(
                GetXmlStringFromObject(obj));
            return xml1;
        }


        public static T GetObjectFromXml<T>(
            string source,
            string defaultNamespace = null)
        {
            Type t1 = typeof(T);
            var serializer1 = new XmlSerializer(t1, defaultNamespace); // SerializerCache.GetSerializer(type)
            using var reader1 = new StringReader(source);
            return (T)serializer1.Deserialize(reader1);
        }


        public static T GetObjectFromXml<T>(
            XmlDocument source,
            string defaultNamespace = null)
        {
            return GetObjectFromXml<T>(
                source.InnerXml, defaultNamespace);
        }


        public static T GetObjectFromXml<T>(
            Stream stream,
            string defaultNamespace = null)
        {
            Type t1 = typeof(T);
            var serializer1 = new XmlSerializer(t1, defaultNamespace); // SerializerCache.GetSerializer(type)
            var obj1 = (T)serializer1.Deserialize(stream);
            return obj1;
        }


        public static T GetObjectFromXmlFile<T>(
            string filename,
            string defaultNamespace = null)
        {
            using var stream1 = new FileStream(
                filename, FileMode.Open, FileAccess.Read);
            return GetObjectFromXml<T>(
                stream1, defaultNamespace);
        }


        /* methods */


        public static void SaveObjectToXmlFile(
            object obj,
            string filename)
        {
            if (obj != null)
            {
                var serializer1 = new XmlSerializer(obj.GetType()); // SerializerCache.GetSerializer(type)
                var ns1 = new XmlSerializerNamespaces(
                    new XmlQualifiedName[] { new XmlQualifiedName(string.Empty) });
                using var stream1 = new FileStream(
                    filename, FileMode.Create, FileAccess.Write);
                serializer1.Serialize(
                    stream1, obj, ns1);
            }
        }

    }

}
