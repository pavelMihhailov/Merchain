namespace Merchain.Services.Econt.Models.Request
{
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class Credentials
    {
        [XmlElement("username")]
        public string Username { get; set; }

        [XmlElement("password")]
        public string Password { get; set; }
    }
}
