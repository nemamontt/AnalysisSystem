using Common.Databases;
using PragmaticAnalyzer.Core;
using PragmaticAnalyzer.MVVM.ViewModel.Viewer;
using System.IO;
using System.Text.Json;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private readonly string settingPuth = Path.Combine(Environment.CurrentDirectory, "setting.json");
        public object? CurrentView { get => Get<object>(); private set => Set(value); }
        public SetViewModel SetVm { get; }
        public HelpViewModel HelpVm { get; }
        public ViewerViewModel ViewerVm { get; }
        public ConnectionViewModel ConnectionVm { get; }
        public VulViewModel VulVm { get; }
        public ThreatViewModel ThretVm { get; }
        public TacticViewModel TacticVm { get; }
        public ProtectionMeasureViewModel ProtectionMeasureVm { get; }
        public OutcomesViewModel OutcomesVm { get; }
        public OntologyViewModel OntologyVm { get; }
        public ExploitViewModel ExploitVm { get;}
        public ViolatorViewModel ViolatorVm { get; }
        public SpecialistViewModel SpecialistVm { get; }
        public ReferenceStatusViewModel ReferenceStatusVm { get; }
        public Paths PathsDatabases { get; }

        public MainViewModel()
        {
            if (File.Exists(settingPuth))
                PathsDatabases = JsonSerializer.Deserialize<Paths>(File.ReadAllText(settingPuth)) ?? new();
            else
                PathsDatabases = new();

            VulVm = new();
            ThretVm = new();
            TacticVm = new();
            ProtectionMeasureVm = new();
            OutcomesVm = new();    
            ExploitVm = new();
            ViolatorVm = new();
            SpecialistVm = new();
            ReferenceStatusVm = new();
            HelpVm = new();
            OntologyVm = new(PathsDatabases);
            SetVm = new(VulVm, ThretVm, TacticVm, ProtectionMeasureVm, OutcomesVm, ExploitVm, ViolatorVm, SpecialistVm, PathsDatabases, ReferenceStatusVm);        
            ViewerVm = new(SetCurrentViewAction, VulVm, ThretVm, TacticVm, ProtectionMeasureVm, OutcomesVm, ExploitVm, ViolatorVm, SpecialistVm, ReferenceStatusVm);
            ConnectionVm = new(VulVm, ThretVm, TacticVm, ProtectionMeasureVm, OutcomesVm, ExploitVm, ViolatorVm, SpecialistVm, ReferenceStatusVm);

            CurrentView = SetVm;
        }

        private void SetCurrentViewAction(object vm) => CurrentView = vm;

        public RelayCommand SetCurrentView => GetCommand(vm => CurrentView = vm);
    }

}