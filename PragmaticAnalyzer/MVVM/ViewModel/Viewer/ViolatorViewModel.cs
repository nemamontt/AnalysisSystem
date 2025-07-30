using PragmaticAnalyzer.Databases;
using PragmaticAnalyzer.DTO;
using System.Collections.ObjectModel;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class ViolatorViewModel : ViewModelBase
    {
        private readonly Func<string, DataType, Task> UpdateConfig;
        public ObservableCollection<Violator> Violators { get; set; }
        public Violator? SelectedViolator { get => Get<Violator?>(); set => Set(value); }

        public ViolatorViewModel(ObservableCollection<Violator> violators, Func<string, DataType, Task> updateConfig)
        {
            Violators = violators;
            UpdateConfig += updateConfig;
        }
    }
}