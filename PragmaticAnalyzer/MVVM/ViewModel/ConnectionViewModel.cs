using Common.Databases;
using PragmaticAnalyzer.MVVM.ViewModel.Viewer;
using System.Collections.ObjectModel;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel
{
    public class ConnectionViewModel : ViewModelBase
    {
        public VulViewModel vulVm;
        public ThreatViewModel threatVm;
        public TacticViewModel tacticVm;
        public ProtectionMeasureViewModel protectionMeasureVm;
        public OutcomesViewModel outcomesVm;
        public ExploitViewModel exploitVm;
        public ViolatorViewModel violatorVm;
        public SpecialistViewModel specialistVm;
        public ReferenceStatusViewModel referenceStatusVm;
        public ObservableCollection<Report> Reports { get => Get<ObservableCollection<Report>>(); set => Set(value); }
        public Report SelectedReport { get => Get<Report>(); set => Set(value); }
        public string RequestText { get => Get<string>(); set => Set(value); }

        public ConnectionViewModel(VulViewModel vulViewModel, ThreatViewModel threatViewModel, TacticViewModel tacticViewModel,
                                                          ProtectionMeasureViewModel protectionMeasureViewModel, OutcomesViewModel outcomesViewModel,
                                                          ExploitViewModel exploitViewModel, ViolatorViewModel violatorViewModel, SpecialistViewModel specialistVewModel,
                                                          ReferenceStatusViewModel referenceStatusViewModel)
        {
            Reports = [];
            vulVm = vulViewModel;
            threatVm = threatViewModel;
            tacticVm = tacticViewModel;
            protectionMeasureVm = protectionMeasureViewModel;
            outcomesVm = outcomesViewModel;
            exploitVm = exploitViewModel;
            violatorVm = violatorViewModel;
            specialistVm = specialistVewModel;
            referenceStatusVm = referenceStatusViewModel;
        }

        public RelayCommand ShowReport => GetCommand(o =>
        {
            Reports.Clear();
            foreach (var report in Cap())
                Reports.Add(report);
        });
        private ObservableCollection<Report> Cap()
        {
            ObservableCollection<Report> reports = [];
            for (int i = 1; i <= 10; i++)
            {
                reports.Add(new()
                {
                    Coefficient = (float)Random.Shared.NextDouble(),
                });
            }
            foreach (var item in reports)
            {
                int rnd = Random.Shared.Next(2, 5);
                if (vulVm.Vulnerabilities is not null)
                    item.Vulnerabilitie = vulVm.Vulnerabilities[rnd];
                if (threatVm.Threats is not null)
                    item.Threat = threatVm.Threats[rnd];
                if (tacticVm.Tactics is not null)
                    item.Tactic = tacticVm.Tactics[rnd];
                if (protectionMeasureVm.ProtectionMeasures is not null)
                    item.ProtectionMeasure = protectionMeasureVm.ProtectionMeasures[rnd];
                if (outcomesVm.Outcomes.Consequences is not null)
                    item.Consequence = outcomesVm.Outcomes.Consequences[rnd];
                if (outcomesVm.Outcomes.Technologys is not null)
                    item.Technology = outcomesVm.Outcomes.Technologys[rnd];
                if(violatorVm.Violators is not null)
                    item.Violator = violatorVm.Violators[rnd];
                if (exploitVm.Exploits is not null)
                    item.Exploit = exploitVm.Exploits[rnd];
                if (specialistVm.Specialists is not null)
                    item.Specialist = specialistVm.Specialists[rnd];
                if (referenceStatusVm.ReferencesStatus is not null)
                    item.ReferenceStatus = referenceStatusVm.ReferencesStatus[1];
            }
            return reports;
        }
    }
    public class Report : ViewModelBase
    {
        public float Coefficient { get => Get<float>(); set => Set(value); }
        public Vulnerabilitie Vulnerabilitie { get => Get<Vulnerabilitie>(); set => Set(value); }
        public Threat Threat { get => Get<Threat>(); set => Set(value); }
        public Tactic Tactic { get => Get<Tactic>(); set => Set(value); }
        public ProtectionMeasure ProtectionMeasure { get => Get<ProtectionMeasure>(); set => Set(value); }
        public Technology Technology { get => Get<Technology>(); set => Set(value); }
        public Consequence Consequence { get => Get<Consequence>(); set => Set(value); }
        public Exploit Exploit { get => Get<Exploit>(); set => Set(value); }
        public Specialist Specialist { get => Get<Specialist>(); set => Set(value); }
        public Violator Violator { get => Get<Violator>(); set => Set(value); }
        public ReferenceStatus ReferenceStatus { get => Get<ReferenceStatus>(); set => Set(value); }
        public CurrentStatus CurrentStatus { get => Get<CurrentStatus>(); set => Set(value); }
    }
}
