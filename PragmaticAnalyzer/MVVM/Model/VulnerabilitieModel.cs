using Aspose.Cells;
using PragmaticAnalyzer.Configs;
using PragmaticAnalyzer.Databases;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Text.Json;

namespace PragmaticAnalyzer.MVVM.Model
{
    public class VulnerabilitieModel
    {
        private readonly HttpClient _httpClient;
        private VulConfig _vulConfig;

        public VulnerabilitieModel(VulConfig vulConfig)
        {
            _vulConfig = vulConfig;
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
            _httpClient = new HttpClient(handler)
            {
                Timeout = TimeSpan.FromSeconds(60)
            };
        }

        public async Task<ObservableCollection<Vulnerabilitie>> GetDatabase()
        {
            int counter = 1;
            ObservableCollection<Vulnerabilitie> allVulnerabilities = [];
            if (await GetByLink() is ObservableCollection<Vulnerabilitie> vulnerabilitiesLink)
            {
                foreach (var vulnerabilitie in vulnerabilitiesLink)
                {
                    vulnerabilitie.Identifier = $"GNA-{counter}";
                    allVulnerabilities.Add(vulnerabilitie);
                    counter++;
                }
            }
            if (await GetByPageParsing() is ObservableCollection<Vulnerabilitie> vulnerabilitiesPageParsing)
            {
                foreach (var vulnerabilitie in vulnerabilitiesPageParsing)
                {
                    vulnerabilitie.Identifier = $"GNA-{counter}";
                    allVulnerabilities.Add(vulnerabilitie);
                    counter++;
                }
            }
            if (await GetByApiRequest() is ObservableCollection<Vulnerabilitie> vulnerabilitiesApiRequest)
            {
                foreach (var vulnerabilitie in vulnerabilitiesApiRequest)
                {
                    vulnerabilitie.Identifier = $"GNA-{counter}";
                    allVulnerabilities.Add(vulnerabilitie);
                    counter++;
                }
            }
            return allVulnerabilities;
        }

        private async Task<ObservableCollection<Vulnerabilitie>> GetByLink()
        {
            ObservableCollection<Vulnerabilitie> vulnerabilities = [];
            using HttpRequestMessage request = new(HttpMethod.Get, _vulConfig.UrlFstec);
            using HttpResponseMessage response = await _httpClient.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                using Stream stream = await response.Content.ReadAsStreamAsync();
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
        private async Task<ObservableCollection<Vulnerabilitie>?> GetByPageParsing()
        {
            return null;
        }
        private async Task<ObservableCollection<Vulnerabilitie>?> GetByApiRequest()
        {
            return null;
        }
    }
}