using System.Collections.ObjectModel;

namespace Common.Databases
{
    public class Vulnerabilitie
    {    
        public string Identifier { get; set; }
        public ObservableCollection<Parameter> Parameters { get; set; }
    }
    public class Parameter
    {
        public string From { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}