namespace PragmaticAnalyzer.Databases
{
    public class Threat
    {
        public string Id {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Source { get; set; }
        public string ObjectInfluence { get; set; }
        public string PrivacyViolation { get; set; }
        public string IntegrityViolation { get; set; }
        public string AccessibilityViolation { get; set; }
        public string DateInclusion { get; set; }
        public string DateChange { get; set; }
    }
}