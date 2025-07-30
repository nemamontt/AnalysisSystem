using PragmaticAnalyzer.Abstractions;
using ViewModels;

namespace PragmaticAnalyzer.MVVM.ViewModel.Viewer
{
    public class ViewerViewModel : ViewModelBase
    {
        private readonly Action<object> _changeView;
        public IViewModelsService ViewModelsService { get => Get<IViewModelsService>(); private set => Set(value); }

        public ViewerViewModel(Action<object> changeView, IViewModelsService viewModelsService)
        {
            _changeView = changeView;
            ViewModelsService = viewModelsService;
        }

        public RelayCommand ChangeView => GetCommand(o => _changeView(o));
    }
}