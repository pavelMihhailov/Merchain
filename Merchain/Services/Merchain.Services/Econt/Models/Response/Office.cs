namespace Merchain.Services.Econt.Models.Response
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class Office
    {
        [XmlElement("id")]
        public string Id { get; set; }

        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("office_code")]
        public string OfficeCode { get; set; }

        [XmlElement("country_code")]
        public string CountryCode { get; set; }

        [XmlElement("id_city")]
        public string CityId { get; set; }

        [XmlElement("city_name")]
        public string CityName { get; set; }

        [XmlElement("post_code")]
        public string PostCode { get; set; }

        [XmlElement("address")]
        public string SimpleAddress { get; set; }

        [XmlElement("address_details")]
        public Address Address { get; set; }

        [XmlElement("office_details")]
        public Shipping Shipping { get; set; }

        [XmlElement("phone")]
        public string Phone { get; set; }

        [XmlElement("email")]
        public string Email { get; set; }

        [XmlElement("work_begin")]
        public string StartsWorking { get; set; }

        [XmlElement("work_end")]
        public string EndsWorking { get; set; }

        [XmlElement("work_begin_saturday")]
        public string SaturdayStartsWorking { get; set; }

        [XmlElement("work_end_saturday")]
        public string SaturdayEndsWorking { get; set; }
    }
}