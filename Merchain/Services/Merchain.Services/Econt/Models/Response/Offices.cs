namespace Merchain.Services.Econt.Models.Response
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class Offices
    {
        [XmlElement("e")]
        public List<Office> Info { get; set; }
    }
}
