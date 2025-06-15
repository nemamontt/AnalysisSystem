using Common.Core;
using Common.Databases;
using CreatorOutcomesDatabase.MVVM.View;
using System.Collections.ObjectModel;
using ViewModels;

namespace CreatorOutcomesDatabase.MVVM.ViewModel
{
    public class ConsequenceViewModel : ViewModelBase
    {
        private MessageService messageService;
        public ObservableCollection<Consequence> Consequences { get; set; }
        public Consequence? SelectedItem { get => Get<Consequence?>(); set => Set(value); }
        private ManageConsequenceView? manager;

        public ConsequenceViewModel()
        {
            Consequences = [];
            messageService = new();
        }

        public RelayCommand ManageElement => GetCommand((bool addIt) =>
        {
            if (addIt)
            {
                manager = new(addIt, this);
                manager.Show();
            }
            else if(!addIt && SelectedItem is not null)
            {
                manager = new(addIt, this, SelectedItem);
                manager.Show();
            }
        });
        public RelayCommand DeleteElement => GetCommand(o =>
        {
            if (SelectedItem is not null)
            {
                Consequences.Remove(SelectedItem);
                SelectedItem = null;
            }
        });
        public RelayCommand GetHelp => GetCommand(o =>
        {
            messageService.ShowInfoMessage("ConsVM");
        });
    }
}