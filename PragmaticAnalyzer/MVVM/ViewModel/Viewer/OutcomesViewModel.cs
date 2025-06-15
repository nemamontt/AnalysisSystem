using Common.Databases;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class OutcomesViewModel : ViewModelBase
    {
        public Outcomes Outcomes { get; set; }
        public Technology? SelectedItemTechnology { get => Get<Technology?>(); set => Set(value); }
        public Consequence? SelectedItemConsequence { get => Get<Consequence?>(); set => Set(value); }
    }
}
