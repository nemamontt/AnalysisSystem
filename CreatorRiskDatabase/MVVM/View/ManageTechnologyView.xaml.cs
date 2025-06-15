using Common.Databases;
using CreatorOutcomesDatabase.MVVM.ViewModel;
using System.Reflection;

namespace CreatorOutcomesDatabase.MVVM.View
{
    public partial class ManageTechnologyView
    {
        private TechnologyViewModel vm;
        private bool addIt;
        private Technology? technology;
        public ManageTechnologyView(bool addIt, TechnologyViewModel vm, Technology? technology = null)
        {
            InitializeComponent();
            this.vm = vm;
            this.addIt = addIt;
            if (!addIt)
            {
                this.technology = technology;
                Method.Text = technology.MethodName;
                Description.Text = technology.Description;
                Usage.Text = technology.Usage;
                Scale.Text = technology.Scale;
                Horizont.Text = technology.Horizont;
                Level.Text = technology.Level;
                Necessity.Text = technology.Necessity;
                Experience.Text = technology.Experience;
                Сharacteristic.Text = technology.Сharacteristic;
                Effort.Text = technology.Effort;
            }
        }

        private void ApplyButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
            var newItem = new Technology()
            {
                SequenceNumber = (vm.Technologys.Count + 1).ToString(),
                MethodName = Method.Text,
                Description = Description.Text,
                Usage = Usage.Text,
                Scale = Scale.Text,
                Horizont = Horizont.Text,
                Level = Level.Text,
                Necessity = Necessity.Text,
                Experience = Experience.Text,
                Сharacteristic = Сharacteristic.Text,
                Effort = Effort.Text,
            };

            if (addIt) 
            {
                vm.Technologys.Add(newItem);
            }
            else
            {
                var oldItem = vm.Technologys.FirstOrDefault(x => x.SequenceNumber == technology.SequenceNumber);
                if (oldItem != null)
                {
                    int index = vm.Technologys.IndexOf(oldItem);
                    vm.Technologys[index] = newItem;
                }
            }
            Close();
        }
    }
}
