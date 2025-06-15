using Common.Databases;
using System.Collections.ObjectModel;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class ThreatViewModel : ViewModelBase
    {
        public ObservableCollection<Threat> Threats { get; set; }
        public Threat? SelectedThreat { get => Get<Threat?>(); set => Set(value); }
    }
}
