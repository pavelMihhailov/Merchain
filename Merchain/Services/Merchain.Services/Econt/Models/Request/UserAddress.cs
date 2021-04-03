namespace Merchain.Services.Econt.Models.Request
{
    using System.Xml.Serialization;

    [XmlRoot("address")]
    public class UserAddress
    {
        [XmlElement("city")]
        public string City { get; set; }

        [XmlElement("post_code")]
        public int PostCode { get; set; }

        [XmlElement("quarter")]
        public string Quarter { get; set; }

        [XmlElement("street")]
        public string Street { get; set; }

        [XmlElement("street_num")]
        public int StreetNum { get; set; }

        [XmlElement("street_bl")]
        public string StreetBl { get; set; }

        [XmlElement("street_vh")]
        public string StreetVh { get; set; }

        [XmlElement("street_et")]
        public string StreetEt { get; set; }

        [XmlElement("street_ap")]
        public string StreetAp { get; set; }

        [XmlElement("street_other")]
        public string StreetOther { get; set; }

        [XmlElement("email_on_delivery")]
        public string EmailOnDelivery { get; set; }
    }
}
