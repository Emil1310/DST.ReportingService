using DST.ReportingService.ServiceReference;
using System;
using System.Collections.Generic;
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
            await DownloadXsdFile("1153000", new DateTime(2019, 08, 01), new DateTime(2019, 08, 31)); //, new DateTime(2019, 08, 01), new DateTime(2019, 08, 31));

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
                TaellingsraekkeID = new Models.Hotels.IndberetningsServiceDataDefinitionTaellingsraekkeID() { 
                    Value = "1153000" //Required, "CVR/CPR nummer"
                }, 
                PeriodeStart = new Models.Hotels.IndberetningsServiceDataDefinitionPeriodeStart()
                {
                    Value = new DateTime(2019, 08, 01)
                },
                PeriodeSlut = new Models.Hotels.IndberetningsServiceDataDefinitionPeriodeSlut()
                {
                    Value = new DateTime(2019, 08, 31)
                },
                CVR = new Models.Hotels.IndberetningsServiceDataDefinitionCVR() { 
                    Value = "32160603" // Regex: [0-9]{8}|[0-9]{10}
                }, 
                EnhedsID = new Models.Hotels.IndberetningsServiceDataDefinitionEnhedsID() {
                    Value = "123" //optional, "Journal/SE/LOEBENR..."
                }, 
                Kontaktinformation = new Models.Hotels.IndberetningsServiceDataDefinitionKontaktinformation { 
                    KontaktpersonNavn = "test", // Required, min 1. length (minLength value="1")
                    KontaktpersonTelefon = "81744080", // Required, pattern value="\d+"
                    KontaktpersonEmail = "emilfaba@gmail.com" // Required, pattern value="[^@]+@[^\.]+\..+"
                },                
                Indberetningsdefinition = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinition {
                    Ialt = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionIalt()
                    {
                        Value = "" //optional, "Antal gæstenætter ialt"
                    },
                    IndividForretning = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionIndividForretning()
                    {
                        Value = "" //optional, "Antal gæstenætter foretaget af individuelle forretningsrejsende"
                    },
                    IndividFerie = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionIndividFerie()
                    {
                        Value = "" //optional, "Antal gæstenætter foretaget af individuelle ferierejsende"
                    },
                    GruppeForretning = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionGruppeForretning()
                    {
                        Value = "" //optional, "Antal gæstenætter foretaget af forretningsrejsende, som rejste som en gruppe"
                    },
                    GruppeFerie = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionGruppeFerie()
                    {
                        Value = "" //optional, "Antal gæstenætter foretaget af ferierejsende, som rejste som en gruppe"
                    },
                    Oevrige = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionOevrige()
                    {
                        Value = "" //optional, "Antal gæstenætter foretaget af øvrige rejsende - KUN værelser til airline crews og andre gratis værelser (complimentary)"
                    },
                    UdlVaer = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionUdlVaer()
                    {
                        Value = "" //optional, "Antal udlejede værelser i måneden"
                    },
                    AntSenge = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionAntSenge()
                    {
                        Value = "" //optional, "Antal sengepladser i måneden, uden ekstraopredninger (kapacitet)"
                    },
                    AntVaer = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionAntVaer()
                    {
                        Value = "" //optional, "Antal værelser i måneden (kapacitet)"
                    },
                    AabnIAar = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionAabnIAar()
                    {
                        Value = "" //optional, "Dato for sæsonåbning i året"
                    },
                    LukIAar = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionLukIAar()
                    {
                        Value = "" //optional, "Dato for sæsonlukning i året"
                    },
                    Virk_bemaerkning = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionVirk_bemaerkning()
                    {
                        Value = "" //optional, "Bemærkning"
                    },
                    Kontaktpers_fornavn = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionKontaktpers_fornavn()
                    {
                        Value = "" //optional, "Kontaktpersonens fornavn"
                    },
                    Kontaktpers_efternavn = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionKontaktpers_efternavn()
                    {
                        Value = "" //optional, "Kontaktpersonens efternavn"
                    },
                    Kontaktpers_telefon = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionKontaktpers_telefon()
                    {
                        Value = "" //optional, "Kontaktpersonens telefon"
                    },
                    Kontaktpers_epost = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionKontaktpers_epost()
                    {
                        Value = "" //optional, "Kontaktpersonens e-mail"
                    },
                    AntGaesterIalt = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionAntGaesterIalt()
                    {
                        Value = "" //optional, "Antal gæster i alt (udenlandske og danske lagt sammen)"
                    },
                    AntGaesterDK = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionAntGaesterDK
                    {
                        Value = "" //optional, "Antal danske gæster i alt"
                    },
                    AfrikaA = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionAfrikaA()
                    {
                        Value = "" //optional, "Antal gæstenætter foretaget af gæster bosiddende i andre afrikanske lande "
                    },
                    AmerikaA = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionAmerikaA()
                    {
                        Value = "" //optional, "Antal gæstenætter foretaget af gæster bosiddende i andre mellem- og sydamerikanske lande (såsom Argentina, caribiske øer osv.) "
                    },
                    AsienA = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionAsienA()
                    {
                        Value = "" //optional, "Antal gæstenætter foretaget af gæster bosiddende i andre asiatiske lande (såsom Vietnam, Indonesien, Malaysia osv.)"
                    },
                    EuropaA = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionEuropaA()
                    {
                        Value = "" //optional, "Antal gæstenætter foretaget af gæster bosiddende i andre europæiske lande (såsom Hviderusland)"
                    },
                    OceanienA = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionOceanienA()
                    {
                        Value = "" //optional, "Antal gæstenætter foretaget af gæster bosiddende i andre oceaniske lande (såsom New Zealand, Tonga, Cook-øerne)"
                    },
                    Australien = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionAustralien()
                    {
                        Value = ""
                    },
                    Belgien = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionBelgien()
                    {
                        Value = ""
                    },
                    Bosnien_Hercegovina = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionBosnien_Hercegovina()
                    {
                        Value = ""
                    },
                    Brasilien = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionBrasilien()
                    {
                        Value = ""
                    },
                    Bulgarien = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionBulgarien()
                    {
                        Value = ""
                    },
                    Canada = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionCanada()
                    {
                        Value = ""
                    },
                    Cypern = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionCypern()
                    {
                        Value = ""
                    },
                    Danmark = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionDanmark()
                    {
                        Value = "123"
                    },
                    Estland = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionEstland()
                    {
                        Value = ""
                    },
                    Faeroerne = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionFaeroerne()
                    {
                        Value = ""
                    },
                    Finland = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionFinland()
                    {
                        Value = ""
                    },
                    Frankrig = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionFrankrig()
                    {
                        Value = ""
                    },
                    FR_Kina = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionFR_Kina()
                    {
                        Value = ""
                    },
                    FYR_Makedonien = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionFYR_Makedonien()
                    {
                        Value = ""
                    },
                    Graekenl = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionGraekenl()
                    {
                        Value = ""
                    },
                    Groenland = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionGroenland()
                    {
                        Value = ""
                    },
                    Holland = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionHolland()
                    {
                        Value = ""
                    },
                    Indien = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionIndien()
                    {
                        Value = ""
                    },
                    Irland = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionIrland()
                    {
                        Value = ""
                    },
                    Island = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionIsland()
                    {
                        Value = ""
                    },
                    Italien = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionItalien()
                    {
                        Value = ""
                    },
                    Japan = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionJapan()
                    {
                        Value = ""
                    },
                    Kroatien = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionKroatien()
                    {
                        Value = ""
                    },
                    Letland = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionLetland()
                    {
                        Value = ""
                    },
                    Litauen = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionLitauen()
                    {
                        Value = ""
                    },
                    Luxemborg = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionLuxemborg()
                    {
                        Value = ""
                    },
                    Malta = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionMalta()
                    {
                        Value = ""
                    },
                    Montenegro = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionMontenegro()
                    {
                        Value = ""
                    },
                    Norge = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionNorge()
                    {
                        Value = ""
                    },
                    Oestrig = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionOestrig()
                    {
                        Value = ""
                    },
                    Polen = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionPolen()
                    {
                        Value = ""
                    },
                    Portugal = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionPortugal()
                    {
                        Value = ""
                    },
                    Rumaenien = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionRumaenien()
                    {
                        Value = ""
                    },
                    Rusland = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionRusland()
                    {
                        Value = ""
                    },
                    Schweiz = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionSchweiz()
                    {
                        Value = ""
                    },
                    Serbien = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionSerbien()
                    {
                        Value = ""
                    },
                    Slovakiet = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionSlovakiet()
                    {
                        Value = ""
                    },
                    Slovenien = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionSlovenien()
                    {
                        Value = ""
                    },
                    Spanien = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionSpanien()
                    {
                        Value = ""
                    },
                    Sverige = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionSverige()
                    {
                        Value = ""
                    },
                    SydAfrika = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionSydAfrika()
                    {
                        Value = ""
                    },
                    SydKorea = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionSydKorea()
                    {
                        Value = ""
                    },
                    Thailand = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionThailand()
                    {
                        Value = ""
                    },
                    Tjekkiet = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionTjekkiet()
                    {
                        Value = ""
                    },
                    Tyrkiet = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionTyrkiet()
                    {
                        Value = ""
                    },
                    Tyskland = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionTyskland()
                    {
                        Value = ""
                    },
                    UK = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionUK()
                    {
                        Value = ""
                    },
                    Ukraine = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionUkraine()
                    {
                        Value = ""
                    },
                    Ungarn = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionUngarn()
                    {
                        Value = ""
                    },
                    USA = new Models.Hotels.IndberetningsServiceDataDefinitionIndberetningsdefinitionUSA()
                    {
                        Value = ""
                    }
                }
            };
            var campingDefinition = new Models.Camping.IndberetningsServiceDataDefinition()
            {
                TaellingsraekkeID = new Models.Camping.IndberetningsServiceDataDefinitionTaellingsraekkeID() { Value = "1156000" }, //Required, "Statistik identifikationsnummer"
                PeriodeStart = new Models.Camping.IndberetningsServiceDataDefinitionPeriodeStart()
                {
                    Value = new DateTime(2019, 08, 01) //Required, "Periode start"
                },
                PeriodeSlut = new Models.Camping.IndberetningsServiceDataDefinitionPeriodeSlut()
                {
                    Value = new DateTime(2019, 08, 31) //Required, "Periode slut"
                },
                CVR = new Models.Camping.IndberetningsServiceDataDefinitionCVR() { Value = "32160603" }, //Required, "CVR/CPR nummer"
                EnhedsID = new Models.Camping.IndberetningsServiceDataDefinitionEnhedsID() { Value = "546"}, //optional, "Journal/SE/LOEBENR..."
                Kontaktinformation = new Models.Camping.IndberetningsServiceDataDefinitionKontaktinformation { 
                    KontaktpersonNavn = "Emil", // Required, min 1. length (minLength value="1")
                    KontaktpersonTelefon = "51942004", // Required, pattern value="\d+"
                    KontaktpersonEmail = "emilfaba@gmail.com" // Required, pattern value="[^@]+@[^\.]+\..+"
                },
                Indberetningsdefinition = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinition { 
                    IaltIkkeFaste = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionIaltIkkeFaste()
                    {
                        Value = "" //optional, "Ikke-fastliggende gæstenætter i alt"
                    },
                    IkkeFasteLandeAntal = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionIkkeFasteLandeAntal[]
                    {
                        new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionIkkeFasteLandeAntal
                        {
                        IFLandekode = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionIkkeFasteLandeAntalIFLandekode(), //optional, "Landekode - Angives jævnfør ISO 3166. Vi benytter den kode, som består af tre bogstaver, fx SWE for Sverige."
                        IFOvernatningerFraLand = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionIkkeFasteLandeAntalIFOvernatningerFraLand() //optional, "Antal ikke-fastliggende gæstenætter fra specificeret land " + "Gruppe af landekoder - bruges til at fordele antal ikke-fastliggende gæstenætter på specifikke lande "
                        },
                        new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionIkkeFasteLandeAntal
                        {
                        IFLandekode = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionIkkeFasteLandeAntalIFLandekode(), //optional, "Landekode - Angives jævnfør ISO 3166. Vi benytter den kode, som består af tre bogstaver, fx SWE for Sverige."
                        IFOvernatningerFraLand = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionIkkeFasteLandeAntalIFOvernatningerFraLand() //optional, "Antal ikke-fastliggende gæstenætter fra specificeret land " + "Gruppe af landekoder - bruges til at fordele antal ikke-fastliggende gæstenætter på specifikke lande "
                        }
                    },
                    IaltFaste = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionIaltFaste()
                    {
                        Value = "" //optional, "Fast udlejede standpladser i alt" name="beskrivelse"
                    },
                    IaltO = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionIaltO()
                    {
                        Value = "" //optional, "Fast udlejede standpladser i alt omregnet til antal gæstenætter"
                    },
                    FasteLandeAntal = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionFasteLandeAntal[] 
                    {
                        new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionFasteLandeAntal
                        {
                            Landekode = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionFasteLandeAntalLandekode(), //optional, "Landekode - Angives jævnfør ISO 3166. Vi benytter den kode, som består af tre bogstaver, fx SWE for Sverige."
                            AntalUdlejedeFasteFraLand = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionFasteLandeAntalAntalUdlejedeFasteFraLand(), //optional, "Antal fast udlejede pladser fra specificeret land" name="beskrivelse"
                            AntalOvernatningerFraLand = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionFasteLandeAntalAntalOvernatningerFraLand() //optional, "Antal gæstenætter på fast udlejede standpladser fra specificeret land " + "Gruppe af landekoder - bruges til at fordele antal fastliggende gæstenætter på specifikke lande "
                        },
                        new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionFasteLandeAntal
                        {
                            Landekode = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionFasteLandeAntalLandekode(), //optional, "Landekode - Angives jævnfør ISO 3166. Vi benytter den kode, som består af tre bogstaver, fx SWE for Sverige."
                            AntalUdlejedeFasteFraLand = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionFasteLandeAntalAntalUdlejedeFasteFraLand(), //optional, "Antal fast udlejede pladser fra specificeret land" name="beskrivelse"
                            AntalOvernatningerFraLand = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionFasteLandeAntalAntalOvernatningerFraLand() //optional, "Antal gæstenætter på fast udlejede standpladser fra specificeret land " + "Gruppe af landekoder - bruges til at fordele antal fastliggende gæstenætter på specifikke lande "
                        }
                    },
                    IAltEnh = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionIAltEnh()
                    {
                        Value = "" //optional, "Kapacitet - Antal campingenheder"
                    },
                    AabnIAar = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionAabnIAar()
                    {
                        Value = "" // optional, "Felt til angivelse af åbningsperiode - Åbner"
                    },
                    LukIAar = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionLukIAar()
                    {
                        Value = "" // optional, "Felt til angivelse af åbningsperiode - Lukker"
                    },
                    Kontaktpers_telefon = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionKontaktpers_telefon()
                    {
                        Value = "" //optional, "Kontaktpersonens telefonnummer"
                    },
                    Kontaktpers_epost = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionKontaktpers_epost()
                    {
                        Value = "" //optional, "Kontaktpersonens e-mail"
                    },
                    AntGaesterIalt = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionAntGaesterIalt()
                    {
                        Value = "" //optional, "Antal gæster i alt (udenlandske og danske lagt sammen)"
                    },
                    AntGaesterDK = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionAntGaesterDK
                    {
                        Value = "123"
                    },
                    kontaktpers_navn = new Models.Camping.IndberetningsServiceDataDefinitionIndberetningsdefinitionKontaktpers_navn()
                    {
                        Value = "" //optional, "Kontaktpersonens navn" name="beskrivelse"
                    }
                }
            };

            // Serializing data to XML
            var serializerHotels = new XmlSerializer(typeof(Models.Hotels.IndberetningsServiceDataDefinition));
            var xmlHotels = "";
            using (var sww = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sww))
                {
                    serializerHotels.Serialize(writer, hotelDefinition);
                    xmlHotels = sww.ToString(); // Your XML
                }
            }
            var myXDocumentHotels = XDocument.Parse(xmlHotels);

            var serializerCamping = new XmlSerializer(typeof(Models.Camping.IndberetningsServiceDataDefinition));
            var xmlCamping = "";
            using (var sww = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sww))
                {
                    serializerCamping.Serialize(writer, campingDefinition);
                    xmlCamping = sww.ToString(); // Your XML
                }
            }
            var myXDocumentCamping = XDocument.Parse(xmlCamping);

            //VALIDATE THAT myXDocument IS MATCHING THE DEFINITION SCHEMA FROM HOTELXSDDEFINITION... 

            XmlSchemaSet schemasHotels = new XmlSchemaSet();
            schemasHotels.Add("", XmlReader.Create(new StringReader(hotelXsdDefinition)));
          
        
            Console.WriteLine("Validating doc1");
            bool errorsHotels = false;
            myXDocumentHotels.Validate(schemasHotels, (o, e) =>
            {
                Console.WriteLine("{0}", e.Message);
                errorsHotels = true;
            }, true);
            Console.WriteLine("doc1 {0}", errorsHotels ? "did not validate" : "validated");

            XmlSchemaSet schemasCamping = new XmlSchemaSet();
            schemasCamping.Add("", XmlReader.Create(new StringReader(campingXsdDefinition)));


            Console.WriteLine("Validating doc2");
            bool errorsCamping = false;
            myXDocumentCamping.Validate(schemasCamping, (o, e) =>
            {
                Console.WriteLine("{0}", e.Message);
                errorsCamping = true;
            }, true);
            Console.WriteLine("doc2 {0}", errorsCamping ? "did not validate" : "validated");


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
            //var Content = $"<s:Envelope xmlns:s=\"http://schemas.xmlsoap.org/soap/envelope/\"><s:Body><GetSurveyDefinition xmlns=\"http://tempuri.org/\"><surveyId>{surveyId}</surveyId><periodBegin>{periodBegin.ToString("yyyy-MM-ddTHH:mm:ss")}</periodBegin><periodEnd>{periodEnd.ToString("yyyy-MM-ddTHH:mm:ss")}</periodEnd></GetSurveyDefinition></s:Body></s:Envelope>";

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
