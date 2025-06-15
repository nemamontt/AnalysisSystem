using System.Text.Json.Serialization;

namespace DataGenerator.Lists
{
    public class ListSpecialist
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> NameOrgans { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> StatusVulnerabilitys { get; set; } 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> ActionsTakens { get; set; } 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> NameSoftwares { get; set; }

        public ListSpecialist()
        {
            NameOrgans = [];
            StatusVulnerabilitys = [];
            ActionsTakens = [];
            NameSoftwares = [];
        }
    }
}
