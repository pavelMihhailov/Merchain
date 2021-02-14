namespace Merchain.Common.Helpers
{
    using System.IO;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public static class XmlHelper
    {
        public static string SerializeXml<T>(T requestObject)
            where T : class
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            byte[] result = null;

            using (MemoryStream memoryStream = new MemoryStream())
            using (XmlWriter xmlWriter = XmlWriter.Create(memoryStream, new XmlWriterSettings { Encoding = Encoding.UTF8 }))
            {
                xmlSerializer.Serialize(xmlWriter, requestObject);
                result = memoryStream.ToArray();
            }

            return Encoding.UTF8.GetString(result);
        }

        public static T DeserializeXml<T>(string xmlContent)
            where T : class
        {
            TextReader xmlAsText = new StringReader(xmlContent);

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
            T result = (T)xmlSerializer.Deserialize(xmlAsText);

            xmlAsText.Dispose();

            return result;
        }
    }
}
