using System.Collections.ObjectModel;
using System.Text.Json.Serialization;

namespace Tests.DB
{
    public class ChangePM
    {
        public void Change(string path)
        {
            var dto = Json
        }


    }
    public class ProtectionMeasure
    {
        public string NameGroup { get; set; }
       
        public string Name { get; set; }

        public string Number { get; set; }

        public string Description { get; set; }

        public string SecurityClasses { get; set; }

        [JsonIgnore]
        public string DispayedName => Name + " . " + Number;
    }
}