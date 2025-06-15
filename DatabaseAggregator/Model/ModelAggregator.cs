using Aspose.Cells;
using Common.Databases;
using Common.DTO;
using Common.Enums.ForSolution;
using HtmlAgilityPack;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace DatabaseAggregator.Model
{
    public class ModelAggregator
    {
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions jsonSerializerOptions;

        public ModelAggregator()
        {
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
            httpClient = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(60) };
            jsonSerializerOptions = new()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
        }

        public void CreateConfig(ProgramConfiguration config)
        {
            var json = JsonSerializer.Serialize(config, jsonSerializerOptions);
            File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "config.json"), json);
        }
        public void SaveFIle(ObservableCollection<Vulnerabilitie> vulnerabilities)
        {
            SaveFileDialog saveFileDialog = new() { Filter = "Json files (*.json)|*.json|All files (*.*)|*.*" };
            if (saveFileDialog.ShowDialog() is true)
            {
                DTO<Vulnerabilitie> vulDto = new()
                {
                    Type = Common.Enums.ForSolution.FileType.Vulnerabilitie,
                    Value = vulnerabilities
                };
                File.WriteAllText(saveFileDialog.FileName, JsonSerializer.Serialize(vulDto, jsonSerializerOptions));
            }
        }
        public static ProgramConfiguration? GetConfig()
        {
            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "config.json")))
                return null;
            return JsonSerializer.Deserialize<ProgramConfiguration>(File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "config.json")));
        }
        public ObservableCollection<Vulnerabilitie> CreateDatabase()
        {
            var progConfig = GetConfig() ?? throw new ArgumentNullException("Отсутствует файл конфигурации");
            ObservableCollection<Vulnerabilitie> newDatabase = [];
            var counter = 0;
            foreach (var vul in UpdateByInstallationLink(progConfig.UrlFSTEC))
            {
                vul.Identifier = "GNA-" + string.Format("{0:d6}", counter);
                newDatabase.Add(vul);
                counter++;
            }
            return newDatabase;
        }
        public static ObservableCollection<Vulnerabilitie> GetDatabases()
        {
            OpenFileDialog openFileDialog = new() { Filter = "Json files (*.json)|*.json" };
            string jsonString = string.Empty;
            if (openFileDialog.ShowDialog() is not true)
                return [];

            var rawDto = JsonSerializer.Deserialize<DTO<Vulnerabilitie>>(File.ReadAllText(openFileDialog.FileName));
            if (rawDto.Type is not Common.Enums.ForSolution.FileType.Vulnerabilitie)
                return [];
            else
                return rawDto.Value;

        }
        private ObservableCollection<Vulnerabilitie> UpdateByInstallationLink(string link)
        {
            ObservableCollection<Vulnerabilitie> vulnerabilities = [];
            using HttpRequestMessage request = new(HttpMethod.Get, link);
            using HttpResponseMessage response = httpClient.Send(request);
            if (response.IsSuccessStatusCode)
            {
                using Stream stream = response.Content.ReadAsStream();
                using Workbook workbook = new(stream, new(LoadFormat.Xlsx));
                using Worksheet worksheet = workbook.Worksheets[0];
                var numberRows = worksheet.Cells.MaxDataRow;
                var numberColumn = worksheet.Cells.MaxDataColumn;

                for (int rowIterator = 3; rowIterator < numberRows; rowIterator++)
                {
                    Vulnerabilitie vulnerability = new() { Parameters = [] };
                    for (int lineIterator = 0; lineIterator < numberColumn; lineIterator++)
                    {
                        Parameter parameter = new()
                        {
                            From = "FSTEC",
                            Name = worksheet.Cells[2, lineIterator].Value?.ToString(),
                            Description = worksheet.Cells[rowIterator, lineIterator].Value?.ToString()
                        };
                        vulnerability.Parameters.Add(parameter);
                    }
                    vulnerabilities.Add(vulnerability);
                }
            }
            return vulnerabilities;
        }
        private ObservableCollection<Vulnerabilitie> UpdateByPageParsing(string link)
        {
            HtmlWeb web = new();
            List<string> linkVuls = [];
            ObservableCollection<Vulnerabilitie> vulnerabilities = [];

            var htmlDoc = web.Load(link);
            var linkNextStep = htmlDoc.DocumentNode.SelectSingleNode("//td[@class='pager_index_class']");//.LastChild.Attributes["href"].Value; ///html/body/div/div[3]/div[1]/div/div/div/table[1]/tbody/tr/td[2]/a[11] /tr/td[2]a[11]
            var numberVul = htmlDoc.DocumentNode.SelectNodes("//table[@class='result_class']/tr").Count;   //xpac
            for (int i = 2; i <= numberVul; i++)
            {
                var linkVul = htmlDoc.DocumentNode.SelectSingleNode($"//table[@class='result_class']/tr[{i}]/td[1]/a[1]").Attributes["href"].Value;
                linkVuls.Add("https://jvndb.jvn.jp" + linkVul);
            }
            for (int i = 0; i < linkVuls.Count; i++)
            {
                Vulnerabilitie vulnerability = new() { Parameters = [] };
                var htmlDocument = web.Load(linkVuls[i]);

                vulnerability.Parameters.Add(new()
                {
                    From = "JVN",
                    Name = "Описание",
                    Description = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='news-list']/table[1]/tr[5]").InnerText.Replace("\n", string.Empty)
                });
                vulnerability.Parameters.Add(new()
                {
                    From = "JVN",
                    Name = "Идентификаторы других систем описаний уязвимости",
                    Description = htmlDocument.DocumentNode.SelectSingleNode("//div[@id='news-list']/table[1]/tr[2]").InnerText.Replace("\n", string.Empty)
                });
                vulnerabilities.Add(vulnerability);
            }
            return vulnerabilities;
        }
        private string? GetJsonStringByApi(string apiKey, string link)
        {
            using HttpRequestMessage request = new(HttpMethod.Get, link);
            if (apiKey is not null)
            {
                request.Headers.Add("User-Agent", $"{apiKey}");
            }
            using HttpResponseMessage response = httpClient.Send(request);
            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadAsStringAsync().Result;
            }
            return null;
        }
        /*   private ObservableCollection<Vulnerabilitie> UpdateByApiRequest(string apiKey, string link)
           {
               ObservableCollection<Vulnerabilitie> vulnerabilities = [];

               VulNVD vulNVD = new();          
               vulNVD = JsonSerializer.Deserialize<VulNVD>(GetJsonStringByApi(apiKey, link));

               foreach (var vulN in vulNVD.vulnerabilities)
               {
                   Vulnerabilitie vul = new()
                   {
                       Parameters = []
                   };
                   vul.Parameters.Add(new()
                   {
                       ParameterAndDescription = []
                   });
                   foreach (var des in vulN.cve.descriptions)
                   {
                       vul.Parameters[0].ParameterAndDescription.Add("Описание уязвимости", des.value);
                       break;
                   }
                   vul.Parameters[0].ParameterAndDescription.Add("Дата выявления", vulN.cve.published.ToString());
                   vul.Parameters[0].ParameterAndDescription.Add("Статус уязвимости", vulN.cve.vulnStatus);
                   vul.Parameters[0].ParameterAndDescription.Add("Идентификаторы других систем описаний уязвимости", vulN.cve.id);
                   string sources = string.Empty;
                   foreach (var refer in vulN.cve.references)
                   {
                       sources += refer.url + "\n";
                   }
                   vul.Parameters[0].ParameterAndDescription.Add("Ссылки на источники", sources);
                   vulnerabilities.Add(vul);
               }
               return vulnerabilities;
           }     */
    }
    public class ProgramConfiguration
    {
        public string? UrlFSTEC { get; set; }
        public string? UrlNVD { get; set; }
        public string? ApiKeyNVD { get; set; }
        public string? UrlJVN { get; set; }
        public string? SettingText { get; set; }
    }

    class CvssData
    {
        public string version { get; set; }
        public string vectorString { get; set; }
        public string accessVector { get; set; }
        public string accessComplexity { get; set; }
        public string authentication { get; set; }
        public string confidentialityImpact { get; set; }
        public string integrityImpact { get; set; }
        public string availabilityImpact { get; set; }
        public double baseScore { get; set; }
    }
    class CvssMetricV2
    {
        public string source { get; set; }
        public string type { get; set; }
        public CvssData cvssData { get; set; }
        public string baseSeverity { get; set; }
        public double exploitabilityScore { get; set; }
        public double impactScore { get; set; }
        public bool acInsufInfo { get; set; }
        public bool obtainAllPrivilege { get; set; }
        public bool obtainUserPrivilege { get; set; }
        public bool obtainOtherPrivilege { get; set; }
        public bool userInteractionRequired { get; set; }
    }
    class Metrics
    {
        public IList<CvssMetricV2> cvssMetricV2 { get; set; }
    }
    class Description
    {
        public string lang { get; set; }
        public string value { get; set; }
    }
    class Weakness
    {
        public string source { get; set; }
        public string type { get; set; }
        public IList<Description> description { get; set; }
    }
    class CpeMatch
    {
        public bool vulnerable { get; set; }
        public string criteria { get; set; }
        public string versionEndExcluding { get; set; }
        public string matchCriteriaId { get; set; }
    }
    class Node
    {
        public string operatorr { get; set; }
        public bool negate { get; set; }
        public IList<CpeMatch> cpeMatch { get; set; }
    }
    class Configuration
    {
        public IList<Node> nodes { get; set; }
    }
    class Reference
    {
        public string url { get; set; }
        public string source { get; set; }
        public IList<string> tags { get; set; }
    }
    class Cve
    {
        public string id { get; set; }
        public string sourceIdentifier { get; set; }
        public DateTime published { get; set; }
        public DateTime lastModified { get; set; }
        public string vulnStatus { get; set; }
        public IList<object> cveTags { get; set; }
        public IList<Description> descriptions { get; set; }
        public Metrics metrics { get; set; }
        public IList<Weakness> weaknesses { get; set; }
        public IList<Configuration> configurations { get; set; }
        public IList<Reference> references { get; set; }
    }
    class Vulnerability
    {
        public Cve cve { get; set; }
    }
    class VulNVD
    {
        public int resultsPerPage { get; set; }
        public int startIndex { get; set; }
        public int totalResults { get; set; }
        public string format { get; set; }
        public string version { get; set; }
        public DateTime timestamp { get; set; }
        public IList<Vulnerability> vulnerabilities { get; set; }
    }
}