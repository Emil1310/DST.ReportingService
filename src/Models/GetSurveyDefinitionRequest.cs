using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DST.ReportingService.Models
{
    public class GetSurveyDefinitionRequestBody
    {
        [XmlElement(ElementName = "GetSurveyDefinition", Namespace = "http://tempuri.org/")]
        public GetSurveyDefinitionRequest GetSurveyDefinitionRequest { get; set; }

    }
    public class GetSurveyDefinitionRequest
    {
        [XmlElement(ElementName = "surveyId")]
        //[System.ServiceModel.MessageBodyMember(Name = "surveyId", Namespace = "http://tempuri.org/", Order = 0)]
        public string SurveyId;

        [XmlElement(ElementName = "periodBegin")]
        //[System.ServiceModel.MessageBodyMember(Name = "periodBegin", Namespace = "http://tempuri.org/", Order = 1)]
        public System.DateTime PeriodBegin;

        [XmlElement(ElementName = "periodEnd")]
        //[System.ServiceModel.MessageBodyMember(Name = "periodEnd", Namespace = "http://tempuri.org/", Order = 2)]
        public System.DateTime PeriodEnd;

    }
}
