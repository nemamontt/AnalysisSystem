using Common.Core;
using Common.Databases;
using CreatorOutcomesDatabase.MVVM.View;
using System.Collections.ObjectModel;
using ViewModels;

namespace CreatorOutcomesDatabase.MVVM.ViewModel
{
    public class TechnologyViewModel : ViewModelBase
    {
        private MessageService messageService;
        public ObservableCollection<Technology> Technologys { get; set; }
        public Technology? SelectedItem { get => Get<Technology?>(); set => Set(value); }
        private ManageTechnologyView? manager;

        public TechnologyViewModel()
        {
            Technologys = [];
            messageService = new();
        }

        public RelayCommand ManageElement => GetCommand((bool addIt) =>
        {
            if (addIt)
            {
                manager = new(addIt, this);
                manager.Show();
            }
            else if (!addIt && SelectedItem is not null)
            {
                manager = new(addIt, this, SelectedItem);
                manager.Show();
            }
        });
        public RelayCommand DeleteElement => GetCommand(o =>
        {
            if (SelectedItem is not null)
            {
                Technologys.Remove(SelectedItem);
                SelectedItem = null;
            }
        });
        public RelayCommand GetHelp => GetCommand(o =>
        {
            messageService.ShowInfoMessage("ConsVM");
        });
    }
}

