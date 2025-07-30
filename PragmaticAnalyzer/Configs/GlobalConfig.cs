using PragmaticAnalyzer.Assistant;
using System.IO;

namespace PragmaticAnalyzer.Configs
{
    public static class GlobalConfig
    {
        public static readonly string LastUpdateConfig = Path.Combine(Environment.CurrentDirectory, "lastUpdateConfig.json");
        public static readonly string ExploitConfigPath = Path.Combine(Environment.CurrentDirectory, "exploitConfig.json");
        public static readonly string ThreatConfigPath = Path.Combine(Environment.CurrentDirectory, "threadConfig.json");
        public static readonly string VulConfigPath = Path.Combine(Environment.CurrentDirectory, "vulConfig.json");
        public static readonly string DatabasePath = Path.Combine(Environment.CurrentDirectory, "Database");
        public static readonly string VulPath = Path.Combine(DatabasePath, "vulnerabilitieDb.json");
        public static readonly string ThreatPath = Path.Combine(DatabasePath, "threatDb.json");
        public static readonly string TacticPath = Path.Combine(DatabasePath, "tacticDb.json");
        public static readonly string ProtectionMeasurePath = Path.Combine(DatabasePath, "protectionMeasureDb.json");
        public static readonly string OutcomesPath = Path.Combine(DatabasePath, "outcomesDb.json");
        public static readonly string ExploitPath = Path.Combine(DatabasePath, "exploitDb.json");
        public static readonly string ViolatorPath = Path.Combine(DatabasePath, "violatorDb.json");
        public static readonly string SpecialistPath = Path.Combine(DatabasePath, "specialistDb.json");
        public static readonly string RefStatPath = Path.Combine(DatabasePath, "refStatusDb.json");
        public static readonly string CurStatPath = Path.Combine(DatabasePath, "curStatusDb.json");
    }

    public class ViewPath : ObservedObject
    {
        private string _vulnerabilitiePath = string.Empty;
        private string _threatPath = string.Empty;
        private string _tacticPath = string.Empty;
        private string _protectionMeasurePath = string.Empty;
        private string _outcomesPath = string.Empty;
        private string _exploitPath = string.Empty;
        private string _violatorPath = string.Empty;
        private string _specialistPath = string.Empty;
        private string _refStatusPath = string.Empty;
        private string _curStatusPath = string.Empty;

        public string VulnerabilitiePath
        {
            get => _vulnerabilitiePath;
            set
            {
                _vulnerabilitiePath = value;
                OnPropertyChanged(nameof(VulnerabilitiePath));
            }
        }
        public string ThreatPath
        {
            get => _threatPath;
            set
            {
                _threatPath = value;
                OnPropertyChanged(nameof(ThreatPath));
            }
        }
        public string TacticPath
        {
            get => _tacticPath;
            set
            {
                _tacticPath = value;
                OnPropertyChanged(nameof(TacticPath));
            }
        }
        public string ProtectionMeasurePath
        {
            get => _protectionMeasurePath;
            set
            {
                _protectionMeasurePath = value;
                OnPropertyChanged(nameof(ProtectionMeasurePath));
            }
        }
        public string OutcomesPath
        {
            get => _outcomesPath;
            set
            {
                _outcomesPath = value;
                OnPropertyChanged(nameof(OutcomesPath));
            }
        }
        public string ExploitPath
        {
            get => _exploitPath;
            set
            {
                _exploitPath = value;
                OnPropertyChanged(nameof(ExploitPath));
            }
        }
        public string ViolatorPath
        {
            get => _violatorPath;
            set
            {
                _violatorPath = value;
                OnPropertyChanged(nameof(ViolatorPath));
            }
        }
        public string SpecialistPath
        {
            get => _specialistPath;
            set
            {
                _specialistPath = value;
                OnPropertyChanged(nameof(SpecialistPath));
            }
        }
        public string RefStatusPath
        {
            get => _refStatusPath;
            set
            {
                _refStatusPath = value;
                OnPropertyChanged(nameof(RefStatusPath));
            }
        }
        public string CurStatusPath
        {
            get => _curStatusPath;
            set
            {
                _curStatusPath = value;
                OnPropertyChanged(nameof(CurStatusPath));
            }
        }

    } //ПЕРЕНЕСТИ КУДА-НИБУДЬ
}