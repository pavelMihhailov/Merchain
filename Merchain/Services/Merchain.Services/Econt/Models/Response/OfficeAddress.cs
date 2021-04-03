namespace Merchain.Services.Econt.Models.Response
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class OfficeAddress
    {
        [XmlElement("id_quarter")]
        public string QuarterId { get; set; }

        [XmlElement("quarter_name")]
        public string QuarterName { get; set; }

        [XmlElement("id_street")]
        public string StreetId { get; set; }

        [XmlElement("street_name")]
        public string StreetName { get; set; }

        [XmlElement("num")]
        public string Number { get; set; }

        [XmlElement("bl")]
        public string Blok { get; set; }

        [XmlElement("vh")]
        public string Vhod { get; set; }

        [XmlElement("et")]
        public string Etaj { get; set; }

        [XmlElement("ap")]
        public string Apartament { get; set; }

        [XmlElement("other")]
        public string Other { get; set; }
    }
}
