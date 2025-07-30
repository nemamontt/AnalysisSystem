using PragmaticAnalyzer.MVVM.ViewModel;

namespace PragmaticAnalyzer.MVVM.Views
{
    public partial class MainView
    {
        private MainViewModel _vm;

        public MainView()
        {
            InitializeComponent();
            MainViewModel vm = new();
            DataContext = vm;
            _vm = vm;

            ContentRendered += (s, e) => _vm.Initial?.Invoke();

            Closing += (s, e) => _vm.Complete?.Invoke();
        }
    }
}