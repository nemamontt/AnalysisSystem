using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace PragmaticAnalyzer.Databases
{
    public interface IEntity
    {
        string Name { get; set; }
        string Description { get; set; }
    }
    public class Tactic : IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<Technique> Techniques { get; set; }
        [JsonIgnore]
        public string DisplayName => $"Тактика: {Name}";
    }
    public class Technique : IEntity
    {
        public string Name { get; set; }
        public string Description {  set; get; }
        [JsonIgnore]
        public string DisplayName => $"Техника: {Name}";
    } 
}