using ViewModels;

namespace PragmaticAnalyzer.Core
{
    public class Paths : ViewModelBase
    {
        public string? VulPath { get => Get<string>(); set => Set(value); }
        public string? ThreatPath { get => Get<string>(); set => Set(value); }
        public string? TacticPath { get => Get<string>(); set => Set(value); }
        public string? ProtectionMeasurePath { get => Get<string>(); set => Set(value); }
        public string? OutcomesPath { get => Get<string>(); set => Set(value); }
        public string? ExploitPath { get => Get<string>(); set => Set(value); }
        public string? ViolatorPath { get => Get<string>(); set => Set(value); }
        public string? SpecialistPath { get => Get<string>(); set => Set(value); }
        public string? ReferenceStatusPath { get => Get<string>(); set => Set(value); }
        public string? CurrentStatusPath { get => Get<string>(); set => Set(value); }
        public List<string>? OntologiesPaths { get => Get<List<string>?>(); set => Set(value); }
    }
}
