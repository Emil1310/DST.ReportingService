using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DST.ReportingService.Models
{
    public class SubmitReportStringRequestbody
    {
        [XmlElement(ElementName = "SubmitReportString", Namespace = "")]
        public SubmitReportStringRequest SubmitReportStringRequest { get; set; }
    }

    public class SubmitReportStringRequest
    {
        [XmlElement(ElementName = "report")]
        public string Report { get; set; }
    }
}
