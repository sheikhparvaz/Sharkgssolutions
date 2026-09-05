namespace SharkGSSolutions.Models
{
    /// <summary>
    /// Aggregate view model that supplies every dynamic section on the single-page
    /// site: hero stats, services grid, industries, and the "Why choose us" pillars.
    /// </summary>
    public class HomeViewModel
    {
        public List<StatItem> Stats { get; set; } = new();
        public List<ServiceItem> Services { get; set; } = new();
        public List<IndustryItem> Industries { get; set; } = new();
        public List<ServiceItem> WhyChooseUs { get; set; } = new();
        public ContactViewModel Contact { get; set; } = new();
    }
}
