using PragmaticAnalyzer.Assistant;

namespace PragmaticAnalyzer.Configs
{
    public class VulConfig : ObservedObject
    {
        private string _urlFstec = "https://bdu.fstec.ru/files/documents/vullist.xlsx";
        private string _urlNvd = "";
        private string _urlJvn = "";
        private string _apiKeyNvd = "";

        public string UrlFstec
        {
            get => _urlFstec;
            set
            {
                _urlFstec = value;
                OnPropertyChanged(nameof(UrlFstec));
            }
        }
        public string UrlNvd
        {
            get => _urlNvd;
            set
            {
                _urlNvd = value;
                OnPropertyChanged(nameof(UrlNvd));
            }
        }
        public string UrlJvn
        {
            get => _urlJvn;
            set
            {
                _urlJvn = value;
                OnPropertyChanged(nameof(UrlJvn));
            }
        }
        public string ApiKeyNvd
        {
            get => _apiKeyNvd;
            set
            {
                _apiKeyNvd = value;
                OnPropertyChanged(nameof(ApiKeyNvd));
            }
        }
    }
}