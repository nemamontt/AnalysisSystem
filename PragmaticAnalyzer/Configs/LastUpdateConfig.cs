using PragmaticAnalyzer.Assistant;

namespace PragmaticAnalyzer.Configs
{
    public class LastUpdateConfig : ObservedObject
    {
        private string _vulnerabilitie = string.Empty;
        private string _threat = string.Empty;
        private string _tactic = string.Empty;
        private string _protectionMeasure = string.Empty;
        private string _outcomes = string.Empty;
        private string _exploit = string.Empty;
        private string _violator = string.Empty;
        private string _specialist = string.Empty;
        private string _refStatus = string.Empty;
        private string _curStatus = string.Empty;

        public string Vulnerabilitie
        {
            get => _vulnerabilitie;
            set
            {
                _vulnerabilitie = value;
                OnPropertyChanged(nameof(Vulnerabilitie));
            }
        }
        public string Threat
        {
            get => _threat;
            set
            {
                _threat = value;
                OnPropertyChanged(nameof(Threat));
            }
        }
        public string Tactic
        {
            get => _tactic;
            set
            {
                _tactic = value;
                OnPropertyChanged(nameof(Tactic));
            }
        }
        public string ProtectionMeasure
        {
            get => _protectionMeasure;
            set
            {
                _protectionMeasure = value;
                OnPropertyChanged(nameof(ProtectionMeasure));
            }
        }
        public string Outcomes
        {
            get => _outcomes;
            set
            {
                _outcomes = value;
                OnPropertyChanged(nameof(Outcomes));
            }
        }
        public string Exploit
        {
            get => _exploit;
            set
            {
                _exploit = value;
                OnPropertyChanged(nameof(Exploit));
            }
        }
        public string Violator
        {
            get => _violator;
            set
            {
                _violator = value;
                OnPropertyChanged(nameof(Violator));
            }
        }
        public string Specialist
        {
            get => _specialist;
            set
            {
                _specialist = value;
                OnPropertyChanged(nameof(Specialist));
            }
        }
        public string RefStatus
        {
            get => _refStatus;
            set
            {
                _refStatus = value;
                OnPropertyChanged(nameof(RefStatus));
            }
        }
        public string CurStatus
        {
            get => _curStatus;
            set
            {
                _curStatus = value;
                OnPropertyChanged(nameof(CurStatus));
            }
        }
    }
}