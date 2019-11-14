using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DST.ReportingService.Models
{
    class SubmitReportStringResponse
    {
        [XmlElement(ElementName = "SubmitReportStringResponse", Namespace = "")]
        public SubmitReportStringResponseResult SubmitReportStringResponseResult { get; set; }
    }

    public class SubmitReportStringResponseResult
    {
        [XmlElement(ElementName = "SubmitReportStringResult")]
        public string SubmitReportStringResult { get; set; }
    }
}