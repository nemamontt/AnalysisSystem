using Common.Databases;
using System.Collections.ObjectModel;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class ReferenceStatusViewModel : ViewModelBase
    {
        public ObservableCollection<ReferenceStatus> ReferencesStatus { get => Get<ObservableCollection<ReferenceStatus>>(); set => Set(value); }
        public ReferenceStatus SelectedReferenceStatus { get => Get<ReferenceStatus>(); set => Set(value); }
    }
}
