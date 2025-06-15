using Common.Databases;
using CreatorProtectionMeasuresDatabase.MVVM.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CreatorProtectionMeasuresDatabase.MVVM.View
{
    public partial class AddView : Window
    {
        public AddView(ViewModelPM vm, bool append, ProtectionMeasure protectionMeasure)
        {
            InitializeComponent();

            if (!append)
            {
                string nameMeasure = string.Empty;
                string numberMeasure = string.Empty;
                for (int i = 0; i < protectionMeasure.Name.Count(); i++)
                {
                    if (protectionMeasure.Name[i] == '.')
                    {
                        for (int j = 0; j < i; j++)
                        {
                            nameMeasure += protectionMeasure.Name[j];
                        }
                        for (int k = i + 1; k < protectionMeasure.Name.Count(); k++)
                        {
                            numberMeasure += protectionMeasure.Name[k];
                        }
                    }
                }
                for (int i = 0; i < protectionMeasure.SecurityClasses.Length; i++)
                {
                    if (protectionMeasure.SecurityClasses[i] == '1')
                    {
                        ProtectionClassOneCheckBox.IsChecked = true;
                    }
                    if (protectionMeasure.SecurityClasses[i] == '2')
                    {
                        ProtectionClassTwoCheckBox.IsChecked = true;
                    }
                    if (protectionMeasure.SecurityClasses[i] == '3')
                    {
                        ProtectionClassThreeCheckBox.IsChecked = true;
                    }
                }
                NameGroupMeasure.Text = protectionMeasure.NameGroup;
                NameMeasureTextBox.Text = nameMeasure;
                NumberMeasureTextBox.Text = numberMeasure;
                DescriptionMeasure.Text = protectionMeasure.Description;

            }

            DoneButton.Click += (s, e) =>
            {
                string securityClasses = string.Empty;
                if ((bool)ProtectionClassOneCheckBox.IsChecked)
                    securityClasses += "1";
                if ((bool)ProtectionClassTwoCheckBox.IsChecked)
                    securityClasses += " 2";
                if ((bool)ProtectionClassThreeCheckBox.IsChecked)
                    securityClasses += " 3";

                if (append)
                {
                    ProtectionMeasure protectionMeasure = new()
                    {
                        Id = Guid.NewGuid(),
                        NameGroup = NameGroupMeasure.Text,
                        Name = NameMeasureTextBox.Text + '.' + NumberMeasureTextBox.Text,
                        Description = DescriptionMeasure.Text,
                        SecurityClasses = securityClasses,
                    };
                    vm.AddElement(protectionMeasure);
                }
                else
                {
                    protectionMeasure.NameGroup = NameGroupMeasure.Text;
                    protectionMeasure.Name = NameMeasureTextBox.Text + '.' + NumberMeasureTextBox.Text;
                    protectionMeasure.Description = DescriptionMeasure.Text;
                    protectionMeasure.SecurityClasses = securityClasses;
                    vm.ChangeElement(protectionMeasure);
                }
                Close();
            };

            CancelButton.Click += (s, e) =>
            {
                Close();
            };
        }

        private void NameMeasureTextBoxPreviewKeyDown(object s, KeyEventArgs e)
        {
            var tb = (TextBox)s;
           if(e.Key != Key.Back && (e.Key < Key.A || e.Key > Key.Z) || (e.Key != Key.Back && tb.Text.Length == 3))
                e.Handled = true;
        }

        private void NumberMeasureTextBoxPreviewKeyDown(object s, KeyEventArgs e)
        {
            var tb = (TextBox)s;
            if (e.Key != Key.Back && (e.Key < Key.D0 || e.Key > Key.D9) || (e.Key != Key.Back && tb.Text.Length == 2))
                e.Handled = true;
        }
    }
}