using System.Collections.ObjectModel;

namespace PragmaticAnalyzer.Databases
{
    public class Ontology
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<Entitie> Entities { get; set; }
    }
    public class Entitie
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}