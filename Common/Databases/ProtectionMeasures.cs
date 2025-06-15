using Common.Core;

namespace Common.Databases
{
    public class ProtectionMeasure : ObservedObject
    {
        public string NameGroup { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SecurityClasses { get; set; }
    }
}
