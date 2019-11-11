using DST.ReportingService.ServiceReference;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DST.ReportingService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            // Get hotel survey ID
            await DownloadXsdFile("1153000", new DateTime(2019, 08, 01), new DateTime(2019, 08, 31));

            // Get camping survey ID
            await DownloadXsdFile("1156000", new DateTime(2019, 08, 01), new DateTime(2019, 08, 31));
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
            if (!Directory.Exists(xsdFolderPath)) {
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
    }
}
