using Common.Databases;
using System.Collections.ObjectModel;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class ProtectionMeasureViewModel : ViewModelBase
    {
        public ObservableCollection<ProtectionMeasure> ProtectionMeasures { get; set; }
        public ProtectionMeasure? SelectedProtectionMeasure { get => Get<ProtectionMeasure?>(); set => Set(value); }
    }
}
