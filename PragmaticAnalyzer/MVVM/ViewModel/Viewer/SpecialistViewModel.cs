using PragmaticAnalyzer.Databases;
using PragmaticAnalyzer.DTO;
using System.Collections.ObjectModel;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class SpecialistViewModel : ViewModelBase
    {
        private readonly Func<string, DataType, Task> UpdateConfig;
        public ObservableCollection<Specialist> Specialists { get; set; }
        public Specialist? SelectedSpecialist { get => Get<Specialist?>(); set => Set(value); }

        public SpecialistViewModel(ObservableCollection<Specialist> specialists, Func<string, DataType, Task> updateConfig)
        {
            Specialists = specialists;
            UpdateConfig += updateConfig;
        }
    }
}