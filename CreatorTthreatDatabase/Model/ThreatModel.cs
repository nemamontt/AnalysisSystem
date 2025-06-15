using Aspose.Cells;
using Common.Databases;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;

namespace CreatorTthreatDatabase.Model
{
    public class ThreatModel
    {
        private readonly string parsingURL = "https://bdu.fstec.ru/files/documents/thrlist.xlsx";
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

        public ObservableCollection<Threat>? CreateDatabase()
        {
            ObservableCollection<Threat> threats = [];
            using HttpRequestMessage request = new(HttpMethod.Get, parsingURL);
            using HttpResponseMessage response = httpClient.Send(request);

            if (!response.IsSuccessStatusCode)
                throw new NotImplementedException();

            using Stream stream = response.Content.ReadAsStream();
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