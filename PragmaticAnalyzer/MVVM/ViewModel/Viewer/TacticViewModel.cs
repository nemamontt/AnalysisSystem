using Common.Databases;
using System.Collections.ObjectModel;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class TacticViewModel : ViewModelBase
    {
        public ObservableCollection<Tactic> Tactics { get; set; } 
        public IEntity? SelectedItem { get => Get<IEntity>(); set => Set(value); }
    }
}
