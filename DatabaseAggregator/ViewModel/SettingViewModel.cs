using Common.Core;
using DatabaseAggregator.Model;
using ViewModels;

namespace DatabaseAggregator.ViewModel
{
    public class SettingViewModel : ViewModelBase
    {
        private readonly ModelAggregator model;
        private readonly MessageService messageService;
        public ProgramConfiguration Config { get => Get<ProgramConfiguration>(); set => Set(value); }

        public SettingViewModel()
        {
            messageService = new();
            model = new();
            Config = ModelAggregator.GetConfig() ?? new();
        }

        public RelayCommand SaveData => GetCommand(o =>
        {
            model.CreateConfig(Config);
            messageService.ShowInfoMessage("Настройки программы успешно сохранены");
        });
    }
}