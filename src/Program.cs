using DST.ReportingService.ServiceReference;
using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace DST.ReportingService
{
    public class Program
    {
        private static XNamespace EnvelopeNamespace = "http://schemas.xmlsoap.org/soap/envelope/";
        //private static XNamespace ResultNamespace = "http://www.w3.org/2001/XMLSchema";
        private static XNamespace ResponseNamespace = "http://tempuri.org/";
        private static XNamespace EmptyNamespace = "";

        public static async Task Main(string[] args)
        {
            var campingXsdDefinition = await GetSurveyDefinitionResponse("1156000", new DateTime(2019, 08, 01), new DateTime(2019, 08, 31));
            var hotelXsdDefinition = await GetSurveyDefinitionResponse("1153000", new DateTime(2019, 08, 01), new DateTime(2019, 08, 31));
            //var client = new ReportingServiceClient();
            //var xsdMarkup = await client.GetSurveyDefinitionAsync(new GetSurveyDefinitionRequest(${surveyId}, {periodBegin.ToShortDateString()}, {periodEnd.ToShortDateString()}, new DateTime(2019, 08, 31)));
            //Get hotel surveyID;
            //await DownloadXsdFile("1153000"); //, new DateTime(2019, 08, 01), new DateTime(2019, 08, 31));

            //Get camping surveyID;
            //await DownloadXsdFile("1156000"); //, new DateTime(2019, 08, 01), new DateTime(2019, 08, 31));

            //var campingDefinition = new Models.Camping.IndberetningsServiceDataDefinition()
            //{
            //    CVR = new Models.Camping.IndberetningsServiceDataDefinitionCVR() { Value = "123456" },
            //    EnhedsID = new Models.Camping.IndberetningsServiceDataDefinitionEnhedsID() { Value = "Camping 1"}
            //};

            //await SubmitReport(campingDefinition.ToString());

            var hotelDefinition = new Models.Hotels.IndberetningsServiceDataDefinition()
            {
                CVR = new Models.Hotels.IndberetningsServiceDataDefinitionCVR() { Value = "RU012345" },
                EnhedsID = new Models.Hotels.IndberetningsServiceDataDefinitionEnhedsID() { Value = "Hotels 2" }
            };

            // Serializing data to XML
            var serializer = new XmlSerializer(typeof(Models.Hotels.IndberetningsServiceDataDefinition));
            var xml = "";
            using (var sww = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sww))
                {
                    serializer.Serialize(writer, hotelDefinition);
                    xml = sww.ToString(); // Your XML
                }
            }
            var myXDocument = XDocument.Parse(xml);


            // VALIDATE THAT myXDocument IS MATCHING THE DEFINITION SCHEMA FROM HOTELXSDDEFINITION... 
            //string data = 
            //XmlSchemaSet schemas = new XmlSchemaSet();
            //schemas.Add("", XmlReader.Create(new StringReader(hotelXsdDefinition)));
            //XDocument docHotels = new XDocument(new XElement("Root",
            //    new XElement("Child1", "c1"),
            //    new XElement("Child2", "c2")
            //)
            //);
            //Console.WriteLine("Validating doc1");
            //bool errors = false;
            //docHotels.Validate(schemas, (o, e) =>
            //{
            //    Console.WriteLine("{0}", e.Message);
            //    errors = true;
            //}, true);
            //Console.WriteLine("doc1 {0}", errors ? "did not validate" : "validated");
            //Console.WriteLine();
            //Console.WriteLine("Contents of doc1:");
            //Console.WriteLine(docHotels);
        }
        private static async Task<string> GetSurveyDefinitionResponse(string surveyId, DateTime periodBegin, DateTime periodEnd)
        {
            var sb = new StringBuilder();
            var settings = new XmlWriterSettings();
            settings.OmitXmlDeclaration = true;
            using (var writer = XmlWriter.Create(sb, settings))
            {
                var serializer = new XmlSerializer(typeof(Models.Envelope<Models.GetSurveyDefinitionRequestBody>));

                serializer.Serialize(writer, new Models.Envelope<Models.GetSurveyDefinitionRequestBody>
                {
                    Body = new Models.GetSurveyDefinitionRequestBody
                    {
                        GetSurveyDefinitionRequest = new Models.GetSurveyDefinitionRequest
                        {
                            SurveyId = surveyId,
                            PeriodBegin = periodBegin,
                            PeriodEnd = periodEnd
                        }
                    }
                });
            }
            var stringContent = sb.ToString();

            // "<?xml version=\"1.0\"                                                               <BodyTest xmlns=\"http://tempuri.org/\"><GetSurveyDefinitionRequest><SurveyId>1156000</SurveyId><PeriodBegin>2019-08-01T00:00:00</PeriodBegin><PeriodEnd>2019-08-31T00:00:00</PeriodEnd></GetSurveyDefinitionRequest></BodyTest></Envelope>"
            //var stringContent = $"<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\"><s:Body><GetSurveyDefinition xmlns=\"http://tempuri.org/\"><surveyId>{surveyId}</surveyId><periodBegin>{periodBegin.ToString("yyyy-MM-ddTHH:mm:ss")}</periodBegin><periodEnd>{periodEnd.ToString("yyyy-MM-ddTHH:mm:ss")}</periodEnd></GetSurveyDefinition></s:Body></s:Envelope>";

            var client = new HttpClient();
            var request = new HttpRequestMessage()
            {
                Content = new StringContent(stringContent,
                    Encoding.UTF8, "text/xml"),
                RequestUri = new Uri("https://srvtwebsvc1.dst.dk/ReportingService/ReportingService.svc"),
                Method = HttpMethod.Post
            };
            request.Headers.Add("SOAPAction", "http://tempuri.org/IReportingService/GetSurveyDefinition");
            var result = await client.SendAsync(request);
            string content = await result.Content.ReadAsStringAsync();

            // CONTENT SHOULD BE DESERIALIZED INTO A TYPED MODEL. 
            XDocument xDocument = XDocument.Parse(content);

            var serializer2 = new XmlSerializer(typeof(Models.Envelope<Models.GetSurveyDefinitionResponse>));
            Models.Envelope<Models.GetSurveyDefinitionResponse> envelope;
            using (XmlReader reader = xDocument.CreateReader())
            {
                envelope = (Models.Envelope<Models.GetSurveyDefinitionResponse>)serializer2.Deserialize(reader);
            }

            return envelope.Body.GetSurveyDefinitionResponseResult.GetSurveyDefinitionResult;
            //return xDocument.Descendants(ResponseNamespace + "GetSurveyDefinitionResponse").FirstOrDefault().Value;
        }

        /// <summary>
        /// Helper method to download XSD files from DST. 
        /// </summary>
        /// <param name="surveyId">Id of survey, i.e. 1153000</param>
        /// <param name="periodBegin">Survey begin time, i.e. DateTime(2019, 08, 01)</param>
        /// <param name="periodEnd">Survey end time, i.e. DateTime(2019, 08, 31)</param>
        private static async Task DownloadXsdFile(string surveyId, DateTime periodBegin, DateTime periodEnd)
        {
            // Create new WCF client
            var client = new ReportingServiceClient();
            // Path for XSD file, look in the XSD folder of the binary compiled file
            var xsdPath = Path.Combine(Directory.GetCurrentDirectory(), "XSD", $"{surveyId}-{periodBegin.ToShortDateString()}-{periodEnd.ToShortDateString()}.xsd");
            // Folder path of XSD file
            var xsdFolderPath = Path.GetDirectoryName(xsdPath);

            // Verify folder exists, before attempting to create file
            if (!Directory.Exists(xsdFolderPath))
            {
                Directory.CreateDirectory(xsdFolderPath);
            }

            // Create new file or re-use existing
            using var writer = File.CreateText(xsdPath);
            // Download XSD from DST
            var xsdMarkup = await client.GetSurveyDefinitionAsync(new GetSurveyDefinitionRequest(surveyId, periodBegin, periodEnd));
            // Print it in console (just for help)
            Console.WriteLine(xsdMarkup.GetSurveyDefinitionResult);
            // Write the actual content to the XSD file
            writer.Write(xsdMarkup.GetSurveyDefinitionResult);
        }

        private static async Task SubmitReport(string report)
        {
            var client = new ReportingServiceClient();

            // TODO: Verify report is in the correct format. 
            var response = await client.SubmitReportStringAsync(new SubmitReportStringRequest(report));

            Console.Write("Hopefully we've submitted the report now...");
        }
    }
}
