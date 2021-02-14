namespace Merchain.Services.Econt.Models.Response
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class Shipping
    {
        [XmlElement("courier")]
        public int Courier { get; set; }

        [XmlElement("post")]
        public int Post { get; set; }

        [XmlElement("cargo")]
        public int Cargo { get; set; }

        [XmlElement("cargo_express")]
        public int CargoExpress { get; set; }
    }
}
