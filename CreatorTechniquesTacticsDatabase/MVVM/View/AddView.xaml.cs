using Common.Databases;
using CreatorTechniquesTacticsDatabase.MVVM.ViewModel;
using System.Windows;

namespace CreatorTechniquesTacticsDatabase.MVVM.View
{
    public partial class AddView : Window
    {
        private IEntity? SelectedItemComboBox {  get; set; }
        private ViewModelTT ViewModelTT { get; set; }

        public AddView(bool add, ViewModelTT vm)
        {
            
            InitializeComponent();
            DataContext = vm;
            ViewModelTT = vm;
            IEntity modifiedElement = vm.SelectedItem;

            if (add)
            {
                NumberTextBlock.Text = (vm.Tactics.Count + 1).ToString();
            }
            else if(!add)
            {
                if (modifiedElement is Technique)
                {
                    NumberTextBlock.Text = modifiedElement.Name[1].ToString();
                    SubNumberTextBlock.Text = modifiedElement.Name[3].ToString();
                }
                else
                {
                    NumberTextBlock.Text = modifiedElement.Name[1].ToString();
                }
                DescriptionTextBox.Text = modifiedElement.Description;
                OptionCheckBox.IsEnabled = false;
                ListTechniqueComboBox.IsEnabled = false;
            }

            DoneButton.Click += (s, e) =>
            {
                if (add)
                {
                    if (OptionCheckBox.IsChecked == true && ListTechniqueComboBox.SelectedItem is not null)
                    {
                        Technique technique = new()
                        {
                            Id = Guid.NewGuid(),
                            Name = 'Т' + NumberTextBlock.Text + '.' + SubNumberTextBlock.Text,
                            Description = DescriptionTextBox.Text
                        };
                        vm.AddElement(technique, SelectedItemComboBox.Id);
                    }
                    else
                    {
                        Tactic tactic = new()
                        {
                            Id = Guid.NewGuid(),
                            Name = 'Т' + NumberTextBlock.Text,
                            Description = DescriptionTextBox.Text
                        };
                        vm.AddElement(tactic);
                    }
                }
                else if (!add)
                {
                    if (modifiedElement is Technique)
                    {
                        modifiedElement.Name = 'Т' + NumberTextBlock.Text + '.' + SubNumberTextBlock.Text;
                    }
                    else if (modifiedElement is Tactic)
                    {
                        modifiedElement.Name = 'Т' + NumberTextBlock.Text;
                    }
                    modifiedElement.Description = DescriptionTextBox.Text;
                    vm.ChangeElement(modifiedElement);
                }
                Close();
            };            
        }

        private void ListTechniqueComboBoxSelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (ListTechniqueComboBox.SelectedItem is null)
                return;

            SelectedItemComboBox = (IEntity)ListTechniqueComboBox.SelectedItem;
            foreach (var tactic in ViewModelTT.Tactics)
            {
                if(tactic.Id == SelectedItemComboBox.Id)
                {                  
                    NumberTextBlock.Text = SelectedItemComboBox.Name[1].ToString();
                    SubNumberTextBlock.Text = (tactic.Techniques.Count + 1).ToString();
                }
            }           
        }
        private void OptionCheckBoxChecked(object sender, RoutedEventArgs e)
        {
            ListTechniqueComboBox.IsEnabled = true;
        }
        private void OptionCheckBoxUnchecked(object sender, RoutedEventArgs e)
        {
            ListTechniqueComboBox.IsEnabled = false;
            ListTechniqueComboBox.SelectedItem = null;
            SubNumberTextBlock.Text = null;
            NumberTextBlock.Text = (ViewModelTT.Tactics.Count + 1).ToString();
        }
    }
}