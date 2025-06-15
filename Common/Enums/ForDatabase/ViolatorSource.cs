using System.ComponentModel;

namespace Common.Enums.ForDatabase
{
    public enum ViolatorSource
    {
        [Description("Внутренний нарушитель")]
        Internal = 1,

        [Description("Внешний нарушитель")]
        External = 2
    }
}
