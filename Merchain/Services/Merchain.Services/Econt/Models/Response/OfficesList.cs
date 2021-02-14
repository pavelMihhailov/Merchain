namespace Merchain.Services.Econt.Models.Response
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot("response")]
    [Serializable]
    public class OfficesList
    {
        [XmlElement("offices")]
        public Offices Offices { get; set; }
    }
}
