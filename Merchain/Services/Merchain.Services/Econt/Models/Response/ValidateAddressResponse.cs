namespace Merchain.Services.Econt.Models.Response
{
    using System.Xml.Serialization;

    [XmlRoot("response")]
    public class ValidateAddressResponse
    {
        [XmlElement("address")]
        public UserAddressResponse Address { get; set; }
    }
}
