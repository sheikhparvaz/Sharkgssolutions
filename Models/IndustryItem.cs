namespace SharkGSSolutions.Models
{
    /// <summary>
    /// Represents an industry-specific solution vertical (Healthcare, Fintech, etc.)
    /// </summary>
    public class IndustryItem
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}
