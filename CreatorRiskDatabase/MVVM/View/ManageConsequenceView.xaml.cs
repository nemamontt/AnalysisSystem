using Common.Databases;
using CreatorOutcomesDatabase.MVVM.ViewModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace CreatorOutcomesDatabase.MVVM.View
{
    public partial class ManageConsequenceView
    {
        private ConsequenceViewModel consequenceViewModel;
        private bool addIt;
        private Consequence? consequence;
        public ManageConsequenceView(bool addIt, ConsequenceViewModel consequenceViewModel, Consequence? consequence = null)
        {
            InitializeComponent();
            this.consequenceViewModel = consequenceViewModel;
            this.addIt  = addIt;
            if (!addIt)
            {
                this.consequence = consequence;
                Number.Text = consequence.Number;
                Name.Text = consequence.Name;
                Damage.Text = consequence.Damage;
                foreach (var threat in consequence.NameThreats)
                {
                    ThreatList.Items.Add(threat);
                }
            }
        }

        private void NumberPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!Regex.IsMatch(e.Text, @"^\d$"))
            {
                e.Handled = true;
            }
        }
        private void PlusButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            if (NamberThreat.Text == string.Empty)
                return;

            ThreatList.Items.Add(NamberThreat.Text);
        }
        private void MinusButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            ThreatList.Items.Remove(ThreatList.SelectedItem);
        }
        private void ApplyButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            Consequence newItem = new()
            {
                Number = Number.Text,
                Name = Name.Text,
                Damage = Damage.Text,
                NameThreats = ThreatList.Items.Cast<string>().ToList()
            };
            if (addIt)
            {
                consequenceViewModel.Consequences.Add(newItem);               
            }
            else
            {
                var oldItem = consequenceViewModel.Consequences.FirstOrDefault(x => x.Number == consequence.Number);
                if (oldItem != null)
                {
                    int index = consequenceViewModel.Consequences.IndexOf(oldItem);
                    consequenceViewModel.Consequences[index] = newItem;
                }
            }
            Close();
        }
    }
}
