using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DST.ReportingService.Models
{
    [Serializable]
    [XmlRoot("Envelope", Namespace = "http://schemas.xmlsoap.org/soap/envelope/")]

    public class Envelope<TBody>
    {
        [XmlElement(ElementName = "Body")]
        public TBody Body { get; set; }
    }

    public class Envelope<TBody, THead> : Envelope<TBody>
    {
        public THead Head { get; set; }

    }
}
