using System.Text.Json.Serialization;

namespace DataGenerator.Lists
{
    public class ListViolator
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> Target { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> UsedTools { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> PreviousAttacks { get; set; } 

        public ListViolator()
        {
            Target = [];
            UsedTools = [];
            PreviousAttacks = [];
        }
    }
}
