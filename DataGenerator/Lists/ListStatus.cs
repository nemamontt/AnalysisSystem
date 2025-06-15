using Common.Databases;
using System.Text.Json.Serialization;

namespace DataGenerator.Lists
{
    public class ListStatus
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> NameSoftwares { get; set; } //Наименование анализируемого СС 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> Dislocations { get; set; } //Месторасположение, дислокация СС 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> OperatingModes { get; set; } //Режима функционирования СС 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> Sources { get; set; } //Наименование ПО предоставившего ПД 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> AtributNames { get; set; } //Наименование атрибута ПД 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> AtributValues { get; set; } //Текущее значение атрибута ПД 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> AtributRequiredValues { get; set; } //Требуемое значение атрибута 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<string> ValueTegs { get; set; } //Тег значения атрибута ПД 

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Setting> Settings { get; set; } //

        public ListStatus()
        {
            NameSoftwares = [];
            Dislocations = [];
            OperatingModes = [];
            Sources = []; 
            AtributNames = [];
            AtributValues = [];
            AtributRequiredValues = [];
            ValueTegs = [];
            Settings = [];
        }
    }
}