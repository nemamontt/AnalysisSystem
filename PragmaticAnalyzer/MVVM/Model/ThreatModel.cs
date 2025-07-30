using Aspose.Cells;
using PragmaticAnalyzer.Databases;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;

namespace PragmaticAnalyzer.MVVM.Model
{
    public class ThreatModel
    {
        private readonly HttpClient httpClient;

        public ThreatModel()
        {
            var handler = new HttpClientHandler
            {
                ClientCertificateOptions = ClientCertificateOption.Manual,
                ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) => { return true; }
            };
            httpClient = new HttpClient(handler) { Timeout = TimeSpan.FromSeconds(60) };
        }

        public async Task<ObservableCollection<Threat>?> CreateDatabase(string url)
        {
            ObservableCollection<Threat> threats = [];

            using var response = await httpClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Ошибка при получении данных: {(int)response.StatusCode} {response.ReasonPhrase}");

            using var stream = await response.Content.ReadAsStreamAsync();
            using Workbook workbook = new(stream, new(LoadFormat.Xlsx));
            using Worksheet worksheet = workbook.Worksheets[0];
            var numberRows = worksheet.Cells.MaxDataRow;
            var numberColumn = worksheet.Cells.MaxDataColumn;

            for (int rowIterator = 2; rowIterator <= numberRows; rowIterator++)
            {
                Threat threat = new()
                {
                    Id = worksheet.Cells[rowIterator, 0].Value.ToString(),
                    Name = (string)worksheet.Cells[rowIterator, 1].Value,
                    Description = (string)worksheet.Cells[rowIterator, 2].Value,
                    Source = (string)worksheet.Cells[rowIterator, 3].Value,
                    ObjectInfluence = (string)worksheet.Cells[rowIterator, 4].Value,
                    PrivacyViolation = worksheet.Cells[rowIterator, 5].Value.ToString(),
                    IntegrityViolation = worksheet.Cells[rowIterator, 6].Value.ToString(),
                    AccessibilityViolation = worksheet.Cells[rowIterator, 7].Value.ToString(),
                    DateInclusion = worksheet.Cells[rowIterator, 8].Value.ToString(),
                    DateChange = worksheet.Cells[rowIterator, 9].Value.ToString()
                };
                threats.Add(threat);
            }
            return threats;
        }
    }
}
