using Common.Databases;
using System.Collections.ObjectModel;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class ViolatorViewModel : ViewModelBase
    {
        public ObservableCollection<Violator> Violators { get; set; }
        public Violator? SelectedViolator { get => Get<Violator?>(); set => Set(value); }
    }
}
