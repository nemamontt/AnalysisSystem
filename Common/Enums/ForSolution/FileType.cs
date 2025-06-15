using System.ComponentModel;

namespace Common.Enums.ForSolution
{
    public enum FileType
    {
        [Description("Угроза")]
        Threat = 1,

        [Description("Уязвимость")]
        Vulnerabilitie = 2,

        [Description("Онтология")]
        Ontology = 3,

        [Description("Риск")]
        Outcomes = 4,

        [Description("Тактика")]
        TechniquesTactics = 5,

        [Description("Мера защиты")]
        ProtectionMeasures = 6,

        [Description("Эксплойт")]
        Exploit = 7,

        [Description("Нарушитель")]
        Violator = 8,

        [Description("Специалист")]
        Specialist = 9,

        [Description("Эталонное состояние")]
        ReferenceStatus = 10,

        [Description("Текущее состояние")]
        CurrentStatus = 11,

        [Description("Настройки программы")]
        Setting = 12,
    }
}