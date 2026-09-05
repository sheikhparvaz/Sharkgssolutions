namespace SharkGSSolutions.Models
{
    /// <summary>
    /// Represents a single service offering displayed in the Services grid.
    /// </summary>
    public class ServiceItem
    {
        public string Title { get; set; } = string.Empty;
        public string Summary { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty; // Bootstrap Icon class
        public List<string> Highlights { get; set; } = new();
        public string AccentColor { get; set; } = "var(--accent-primary)";
        public string Slug { get; set; } = string.Empty;
    }
}
