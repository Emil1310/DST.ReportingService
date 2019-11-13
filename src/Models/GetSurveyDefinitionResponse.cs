using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DST.ReportingService.Models
{
    public class GetSurveyDefinitionResponse
    {
        [XmlElement(ElementName = "GetSurveyDefinitionResponse", Namespace = "http://tempuri.org/")]
        public GetSurveyDefinitionResponseResult GetSurveyDefinitionResponseResult { get; set; }
    }

    public class GetSurveyDefinitionResponseResult
    {
        [XmlElement(ElementName = "GetSurveyDefinitionResult")]
        public string GetSurveyDefinitionResult { get; set; }
    }
}
