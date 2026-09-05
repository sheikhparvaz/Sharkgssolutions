namespace SharkGSSolutions.Models
{
    /// <summary>
    /// Represents a single animated counter statistic shown in the hero/about section.
    /// </summary>
    public class StatItem
    {
        public int Value { get; set; }
        public string Suffix { get; set; } = "+";
        public string Label { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}
