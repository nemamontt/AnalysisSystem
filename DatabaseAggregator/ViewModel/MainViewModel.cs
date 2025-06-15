using ViewModels;

namespace DatabaseAggregator.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public SettingViewModel SVM { get; }
        public HelpViewModel HVM { get; }
        public ViewerViewModel VVM { get; }
        public object? CurrentViewModel { get => Get<object>(); private set => Set(value); }

        public MainViewModel()
        {
            SVM = new();
            HVM = new();
            VVM = new();
            CurrentViewModel = VVM;
        }

        public RelayCommand SetCurrentViewModel => GetCommand(vm => CurrentViewModel = vm);
    }
}