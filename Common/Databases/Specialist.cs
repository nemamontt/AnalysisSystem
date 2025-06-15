using System.Text.Json.Serialization;

namespace Common.Databases
{
    public class Specialist
    {    
        public string NameOrgan { get; set; } //наименование ОВУ 
        public string NameHighestOrgan { get; set; } //наименование вышестоящего ОВУ 
        public string NameSubordinateOrgan { get; set; } //наименование подчиненного ОВУ 
        public string StatusVulnerability { get; set; } //статус по выявленной уязвимости
        public string ActionsTaken { get; set; } //предпринятые действия
        public string NameSoftware { get; set; } //наименование СС СОПКА
        public List<string> NameInteractingOrgans {  get; set; } //наименование взаимодействующих ОВУ 
        public List<ProtectionMeasure> UsingMeasures { get; set; } //список принятых мер защиты 
        [JsonIgnore]
        public List<string> DisplayedUsingMeasures => UsingMeasures.Select(pm => pm.NameGroup).ToList();

        public Specialist()
        {
            NameInteractingOrgans = [];
            UsingMeasures = [];
        }
    }
}
