using System.Collections.ObjectModel;

namespace PragmaticAnalyzer.Databases
{
    public class Outcomes
    {
        public ObservableCollection<Technology> Technologys { get; set; }
        public ObservableCollection<Consequence> Consequences { get; set; }
    }
    public class Technology
    {
        public string SequenceNumber { get; set; }
        public string MethodName { get; set; }
        public string Description { get; set; }
        public string Usage { get; set; }
        public string Scale { get; set; }
        public string Horizont { get; set; }
        public string Level { get; set; }
        public string Necessity { get; set; }
        public string Experience { get; set; }
        public string Characteristic { get; set; }
        public string Effort { get; set; }
    }
    public class Consequence
    {
        public string Number { get; set; }
        public string Name { get; set; }
        public string Damage { get; set; }
        public ObservableCollection<string> NameThreats;
    }
}