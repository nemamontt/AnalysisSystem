using PragmaticAnalyzer.Abstractions;
using PragmaticAnalyzer.Configs;
using PragmaticAnalyzer.MVVM.ViewModel;
using PragmaticAnalyzer.MVVM.ViewModel.Viewer;

namespace PragmaticAnalyzer.Services
{
    public class ViewModelsService : IViewModelsService
    {
        public ThreatViewModel ThreatVm { get; }
        public VulnerabilitieViewModel VulnerabilitieVm { get; }
        public ExploitViewModel ExploitVm { get; set; }
        public OntologyViewModel OntologyVm { get; }
        public TacticViewModel TacticVm { get; }
        public ViolatorViewModel ViolatorVm { get; }
        public ProtectionMeasureViewModel ProtectionMeasureVm { get; }
        public SpecialistViewModel SpecialistVm { get; }
        public ReferenceStatusViewModel ReferenceStatusVm { get; }
        public CurrentStatusViewModel CurrentStatusVm { get; }
        public OutcomesViewModel OutcomeVm { get; }
        public SetViewModel SetVm { get; }
        public ViewerViewModel ViewerVm { get; }
        public ConnectionViewModel ConnectionVm { get; }
        public HelpViewModel HelpVm { get; }

        public ViewModelsService(Action<object> changeView, LastUpdateConfig lastUpdateConfig, ThreatConfig threatConfig, ExploitConfig exploitConfig, VulConfig vulConfig)
        {
            SetVm = new(lastUpdateConfig, this);
            ViewerVm = new(changeView, this);
            ConnectionVm = new(this);
            HelpVm = new();

            ThreatVm = new([], SetVm.UpdateConfig, threatConfig);
            VulnerabilitieVm = new([], SetVm.UpdateConfig, vulConfig);
            ExploitVm = new([], SetVm.UpdateConfig, exploitConfig);
            OntologyVm = new([]);
            TacticVm = new([], SetVm.UpdateConfig);
            ViolatorVm = new([], SetVm.UpdateConfig);
            ProtectionMeasureVm = new([], SetVm.UpdateConfig);
            SpecialistVm = new([], SetVm.UpdateConfig);
            ReferenceStatusVm = new([], SetVm.UpdateConfig);
            CurrentStatusVm = new([], SetVm.UpdateConfig);
            OutcomeVm = new(new(), SetVm.UpdateConfig);
        }
    }
}