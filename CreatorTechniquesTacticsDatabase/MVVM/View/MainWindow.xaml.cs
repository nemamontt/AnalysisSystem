using Common.Databases;
using CreatorTechniquesTacticsDatabase.MVVM.ViewModel;
using System.Windows;

namespace CreatorTechniquesTacticsDatabase.MVVM.View
{
    public partial class MainWindow
    {
        private readonly ViewModelTT vm;

        public MainWindow()
        {
            InitializeComponent();
            vm = new();
            DataContext = vm;
        }

        private void SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if(e.NewValue is Technique technique)
            {
                vm.SelectedItem = technique;
            }
            else if(e.NewValue is  Tactic tactic)
            {
                vm.SelectedItem = tactic;
            }
        }
    }
}