using Common.Core;
using Common.Enums.ForDatabase;
using System.Text.Json.Serialization;

namespace Common.Databases
{
    public class Violator : ObservedObject
    {
        public int Id { get; set; } //идентификатор
        public ViolatorSource Source { get; set; } //источник угрозы
        public ViolatorPotential Potential { get; set; } //потенциал
        public string Target { get; set; } //цель атаки 
        public string UsingTools { get; set; } //используемые инструменты 
        public string PreviousAttacks{ get; set; } //данные о предыдущих атаках 

        [JsonIgnore]
        public string DisplayedId => "VIO-" + Id; //идентификатор для отображение в UI
    }
}
