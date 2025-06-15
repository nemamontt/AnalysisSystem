using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class ViewerViewModel : ViewModelBase
    {
        private readonly Action<object> _changeView;
        public VulViewModel VulVm { get; private set; }
        public ThreatViewModel ThretVm { get; private set; }
        public TacticViewModel TacticVm { get; private set; }
        public ProtectionMeasureViewModel ProtectionMeasureVm { get; private set; }
        public OutcomesViewModel OutcomesVm { get; private set; }
        public ExploitViewModel ExploitVm { get; private set; }
        public ViolatorViewModel ViolatorVm { get; private set; }
        public SpecialistViewModel SpecialistVm { get; private set; }
        public ReferenceStatusViewModel ReferenceStatusVm { get; private set; }

        public ViewerViewModel(Action<object> changeView, VulViewModel vulViewModel, ThreatViewModel threatViewModel, TacticViewModel tacticViewModel, 
                                                   ProtectionMeasureViewModel protectionMeasureViewModel, OutcomesViewModel outcomesViewModel,
                                                   ExploitViewModel exploitViewModel, ViolatorViewModel violatorViewModel, SpecialistViewModel specialistVewModel,
                                                   ReferenceStatusViewModel referenceStatusViewModel)
        {
            _changeView = changeView;
            VulVm = vulViewModel;
            ThretVm = threatViewModel;
            TacticVm = tacticViewModel;
            ProtectionMeasureVm = protectionMeasureViewModel;
            OutcomesVm = outcomesViewModel;
            ExploitVm = exploitViewModel;
            ViolatorVm = violatorViewModel;
            SpecialistVm = specialistVewModel;
            ReferenceStatusVm = referenceStatusViewModel;
        }

        public RelayCommand OpenVulViewCommand => GetCommand(_ => _changeView(VulVm));
        public RelayCommand OpenThredViewCommand => GetCommand(_ => _changeView(ThretVm));
        public RelayCommand OpenTacticViewCommand => GetCommand(_ => _changeView(TacticVm));
        public RelayCommand OpenProtectionMeasureViewCommand => GetCommand(_ => _changeView(ProtectionMeasureVm));
        public RelayCommand OpenOutcomesViewCommand => GetCommand(_ => _changeView(OutcomesVm));
        public RelayCommand OpenExploitViewCommand => GetCommand(_ => _changeView(ExploitVm));
        public RelayCommand OpenViolatorViewCommand => GetCommand(_ => _changeView(ViolatorVm));
        public RelayCommand OpenSpecialistViewCommand => GetCommand(_ => _changeView(SpecialistVm));
        public RelayCommand OpenReferenceStatusViewCommand => GetCommand(_ => _changeView(ReferenceStatusVm));
    }
}
