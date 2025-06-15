using Common.Core;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Common.Databases
{
    public interface IEntity
    {
        string Name { get; set; }
        string Description { get; set; }
    }
    public class Tactic : ObservedObject, IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ObservableCollection<Technique> Techniques { get; set; }
        [JsonIgnore]
        public string DisplayName => $"Тактика: {Name}";
    }
    public class Technique : ObservedObject, IEntity
    {
        public string Name { get; set; }
        public string Description {  set; get; }
        [JsonIgnore]
        public string DisplayName => $"Техника: {Name}";
    } 
}