using System.Collections.ObjectModel;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel
{
    public class ProgressViewModel : ViewModelBase
    {
        public ObservableCollection<string> Messages { get => Get<ObservableCollection<string>>(); set => Set(value); }
    }
}