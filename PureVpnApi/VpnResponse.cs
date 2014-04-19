namespace PureVpnApi
{
    using System.Xml.Serialization;

    [XmlRoot("xml")]
    public class VpnResponse
    {
        [XmlElement("user")]
        public string User { get; set; }

        [XmlElement("vpn_password")]
        public string Password { get; set; }

        [XmlElement("result")]
        public int Result { get; set; }

        [XmlElement("status")]
        public string Status { get; set; }
    }
}
