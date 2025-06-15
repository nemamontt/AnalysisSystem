using CreatorProtectionMeasuresDatabase.MVVM.ViewModel;
using System.Windows;

namespace CreatorProtectionMeasuresDatabase
{
    public partial class MainView : Window
    {
        private ViewModelPM vm;

        public MainView()
        {
            InitializeComponent();

            vm = new();
            DataContext = vm;

            OpenButton.Click += (s, e) =>
            {
                vm.OpenFile();
                CheckedAction();
            };

            SaveButton.Click += (s, e) =>
            {
                vm.SaveFile();
            };

            AddButton.Click += (s, e) =>
            {
                vm.ShowAddWindow(true);
                CheckedAction();
            };

            ChangeButton.Click += (s, e) =>
            {
                vm.ShowAddWindow(false);
            };

            DeleteButton.Click += (s, e) =>
            {
                vm.DeleteElement();
                CheckedAction();
            };
        }

        private void CheckedAction()
        {
            if (vm.ProtectionMeasures.Count == 0)
            {
                SaveButton.IsEnabled = false;
                ChangeButton.IsEnabled = false;
                DeleteButton.IsEnabled = false;
            }
            else
            {
                SaveButton.IsEnabled = true;
                ChangeButton.IsEnabled = true;
                DeleteButton.IsEnabled = true;
            }
        }
    }
}