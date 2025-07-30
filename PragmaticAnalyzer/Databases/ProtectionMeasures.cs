using PragmaticAnalyzer.Assistant;
using System.Text.Json.Serialization;

namespace PragmaticAnalyzer.Databases
{
    public class ProtectionMeasure : ObservedObject
    {
        private string _nameGroup;
        private string _name;
        private string _number;
        private string _description;
        private string _securityClasses;

        public string NameGroup
        {
            get => _nameGroup;
            set { _nameGroup = value; OnPropertyChanged(nameof(NameGroup)); OnPropertyChanged(nameof(DispayedName)); }
        }
        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); OnPropertyChanged(nameof(DispayedName)); }
        }
        public string Number
        {
            get => _number;
            set { _number = value; OnPropertyChanged(nameof(Number)); OnPropertyChanged(nameof(DispayedName)); }
        }   
        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(nameof(Description)); }
        }
        public string SecurityClasses
        {
            get => _securityClasses;
            set { _securityClasses = value; OnPropertyChanged(nameof(SecurityClasses)); }
        }
        [JsonIgnore]
        public string DispayedName => Name + " . " + Number;
    }

    public class ProtectionMeasure2
    {
      public string Id { get; set; }
        public string NameGroup { get; set; }
      
        public string Name { get; set; }

        public string Description { get; set; }

        public string SecurityClasses { get; set; }
    }
}