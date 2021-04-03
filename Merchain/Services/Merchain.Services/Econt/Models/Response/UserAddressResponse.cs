namespace Merchain.Services.Econt.Models.Response
{
    using System.Xml.Serialization;

    [XmlRoot("address")]
    public class UserAddressResponse
    {
        [XmlElement("city")]
        public string City { get; set; }

        [XmlElement("post_code")]
        public string PostCode { get; set; }

        [XmlElement("office_code")]
        public string OfficeCode { get; set; }

        [XmlElement("quarter")]
        public string Quarter { get; set; }

        [XmlElement("street")]
        public string Street { get; set; }

        [XmlElement("street_num")]
        public string StreetNum { get; set; }

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

        [XmlElement("validation_status")]
        public string ValidationStatus { get; set; }

        [XmlElement("error")]
        public string Error { get; set; }

        [XmlElement("valid")]
        public string Valid { get; set; }
    }
}
