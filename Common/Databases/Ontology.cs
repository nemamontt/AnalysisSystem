using Common.Core;
using System.Collections.ObjectModel;

namespace Common.Databases
{
    public class Ontology : ObservedObject
    {
        private string name { get; set; }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        private string description { get; set; }
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }
        public ObservableCollection<Entitie> Entities { get; set; }
    }
    public class Entitie : ObservedObject
    {
        private string name { get; set; }
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        private string description { get; set; }
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }
    }
}
