using Common.Databases;
using System.Collections.ObjectModel;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class SpecialistViewModel : ViewModelBase
    {
        public ObservableCollection<Specialist> Specialists { get; set; } 
        public Specialist? SelectedSpecialist { get => Get<Specialist?>(); set => Set(value); }
    }
}
