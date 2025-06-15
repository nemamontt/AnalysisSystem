using Common.Core;
using System.Collections.ObjectModel;

namespace Common.Databases
{
    public class Outcomes : ObservedObject
    {
        public ObservableCollection<Technology> Technologys { get; set; }
        public ObservableCollection<Consequence> Consequences { get; set; }
    }
    public class Technology : ObservedObject
    {
        public string SequenceNumber
        {
            get => sequenceNumber;
            set
            {
                sequenceNumber = value;
                OnPropertyChanged();
            }
        }
        private string sequenceNumber;
        public string MethodName
        {
            get => methodName;
            set
            {
                methodName = value;
                OnPropertyChanged();
            }
        }
        private string methodName;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
            }
        }
        private string description;
        public string Usage
        {
            get => usage;
            set
            {
                usage = value;
                OnPropertyChanged();
            }
        }
        private string usage;
        public string Scale
        {
            get => scale;
            set
            {
                scale = value;
                OnPropertyChanged();
            }
        }
        private string scale;
        public string Horizont
        {
            get => horizont;
            set
            {
                horizont = value;
                OnPropertyChanged();
            }
        }
        private string horizont;
        public string Level
        {
            get => level;
            set
            {
                level = value;
                OnPropertyChanged();
            }
        }
        private string level;
        public string Necessity
        {
            get => necessity;
            set
            {
                necessity = value;
                OnPropertyChanged();
            }
        }
        private string necessity; 
        public string Experience
        {
            get => experience;
            set
            {
                experience = value;
                OnPropertyChanged();
            }
        }
        private string experience;
        public string Сharacteristic
        {
            get => characteristic;
            set
            {
                characteristic = value;
                OnPropertyChanged();
            }
        }
        private string characteristic;
        public string Effort
        {
            get => effort;
            set
            {
                effort = value;
                OnPropertyChanged();
            }
        }
        private string effort;
    }
    public class Consequence : ObservedObject
    {
        public string Number
        {
            get => number;
            set
            {
                number = value;
                OnPropertyChanged();
            }
        }
        private string number;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }
        private string name;
        public string Damage
        {
            get => damage;
            set
            {
                damage = value;
                OnPropertyChanged();
            }
        }
        private string damage;
        public List<string> NameThreats
        {
            get => nameThreats;
            set
            {
                nameThreats = value;
                OnPropertyChanged();
            }
        }
        private List<string> nameThreats;
    }
}
