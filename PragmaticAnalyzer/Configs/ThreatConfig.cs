using PragmaticAnalyzer.Assistant;

namespace PragmaticAnalyzer.Configs
{
    public class ThreatConfig : ObservedObject
    {
        private string _parsingUrl = "https://bdu.fstec.ru/files/documents/thrlist.xlsx";
        public string ParsingUrl 
        {
            get => _parsingUrl;
            set
            {
                _parsingUrl = value;
                OnPropertyChanged(nameof(ParsingUrl));
            }
        }
    }
}