namespace Merchain.Services.Econt.Models.Request
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot("request")]
    [Serializable]
    public class SimpleRequest
    {
        [XmlElement("client")]
        public Credentials Credentials { get; set; }

        [XmlElement("request_type")]
        public string Type { get; set; }
    }
}
