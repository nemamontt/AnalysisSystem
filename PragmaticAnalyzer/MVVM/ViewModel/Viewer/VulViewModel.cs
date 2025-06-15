using Common.Databases;
using System.Collections.ObjectModel;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class VulViewModel : ViewModelBase
    {
        public ObservableCollection<Vulnerabilitie> Vulnerabilities { get;  set; }
    }
}
