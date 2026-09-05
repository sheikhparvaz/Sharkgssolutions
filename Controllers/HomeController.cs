using Microsoft.AspNetCore.Mvc;
using SharkGSSolutions.Models;
using System.Diagnostics;

namespace SharkGSSolutions.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                Stats = GetStats(),
                Services = GetServices(),
                Industries = GetIndustries(),
                WhyChooseUs = GetWhyChooseUs()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitContact(ContactViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState
                    .Where(kvp => kvp.Value != null && kvp.Value.Errors.Count > 0)
                    .ToDictionary(
                        kvp => kvp.Key,
                        kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
                    );

                return Json(new { success = false, errors });
            }

            // In production this would enqueue an email / CRM lead via a service.
            _logger.LogInformation(
                "New enquiry received from {Name} ({Email}) about {Service}",
                model.Name, model.Email, model.ServiceInterest);

            return Json(new
            {
                success = true,
                message = $"Thanks, {model.Name}! Our team will reach out to you within one business day."
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // ---------------------------------------------------------------
        // Data sourced directly from the Shark GS Solutions company profile
        // ---------------------------------------------------------------

        private static List<StatItem> GetStats() => new()
        {
            new StatItem { Value = 20, Suffix = "+", Label = "Service Lines", Icon = "bi-layers" },
            new StatItem { Value = 4,  Suffix = "",  Label = "Industry Verticals", Icon = "bi-building" },
            new StatItem { Value = 24, Suffix = "/7", Label = "Support & Monitoring", Icon = "bi-headset" },
            new StatItem { Value = 100, Suffix = "%", Label = "Client-Centric Delivery", Icon = "bi-heart" },
        };

        private static List<ServiceItem> GetServices() => new()
        {
            new ServiceItem
            {
                Slug = "software-development", Icon = "bi-code-slash",
                Title = "Software Development",
                Summary = "Custom applications, web & mobile apps, desktop tools, APIs and microservices, plus legacy modernization.",
                Highlights = new() { "Custom Software", "Web & Mobile Apps", "API & Microservices", "Legacy Modernization" }
            },
            new ServiceItem
            {
                Slug = "cloud-computing", Icon = "bi-cloud-arrow-up",
                Title = "Cloud Computing",
                Summary = "Seamless cloud migration, cloud-native app development, infrastructure management and serverless computing.",
                Highlights = new() { "Cloud Migration", "Cloud-Native Apps", "Infrastructure Mgmt.", "Serverless Computing" }
            },
            new ServiceItem
            {
                Slug = "devops", Icon = "bi-diagram-3",
                Title = "DevOps & IT Infrastructure",
                Summary = "CI/CD pipelines, Infrastructure as Code, Docker & Kubernetes containerization, monitoring and logging.",
                Highlights = new() { "CI/CD Pipelines", "Infrastructure as Code", "Docker & Kubernetes", "Monitoring & Logging" }
            },
            new ServiceItem
            {
                Slug = "ai-ml", Icon = "bi-cpu",
                Title = "AI & Machine Learning",
                Summary = "Custom model development, natural language processing, computer vision and AI-powered automation.",
                Highlights = new() { "Model Development", "NLP", "Computer Vision", "AI-Powered Automation" }
            },
            new ServiceItem
            {
                Slug = "data-services", Icon = "bi-bar-chart-line",
                Title = "Data Services",
                Summary = "Big data solutions, data warehousing, business intelligence, predictive analytics and data migration.",
                Highlights = new() { "Big Data", "Data Warehousing", "Business Intelligence", "Predictive Analytics" }
            },
            new ServiceItem
            {
                Slug = "cybersecurity", Icon = "bi-shield-lock",
                Title = "Cybersecurity",
                Summary = "Vulnerability assessment, data encryption, identity & access management, audits and endpoint security.",
                Highlights = new() { "Vulnerability Assessment", "Data Encryption", "IAM", "Endpoint Security" }
            },
            new ServiceItem
            {
                Slug = "qa-testing", Icon = "bi-check2-square",
                Title = "Quality Assurance & Testing",
                Summary = "Manual & automated testing, performance checks, security assessments and user acceptance testing.",
                Highlights = new() { "Manual & Automated Testing", "Performance Checks", "Security Assessments", "UAT" }
            },
            new ServiceItem
            {
                Slug = "ui-ux", Icon = "bi-palette",
                Title = "UI/UX Design",
                Summary = "User research, wireframing, responsive design and iterative usability testing for delightful products.",
                Highlights = new() { "User Research", "Wireframing", "Responsive Design", "Usability Testing" }
            },
            new ServiceItem
            {
                Slug = "it-consulting", Icon = "bi-lightbulb",
                Title = "IT Consulting & Strategy",
                Summary = "Digital transformation, technology roadmapping, infrastructure advice and software architecture.",
                Highlights = new() { "Digital Transformation", "Technology Roadmapping", "Infrastructure Advice", "Software Architecture" }
            },
            new ServiceItem
            {
                Slug = "maintenance-support", Icon = "bi-tools",
                Title = "Maintenance & Support",
                Summary = "Software updates, technical support, application monitoring and patch management.",
                Highlights = new() { "Software Updates", "Technical Support", "App Monitoring", "Patch Management" }
            },
            new ServiceItem
            {
                Slug = "ecommerce", Icon = "bi-cart-check",
                Title = "E-Commerce Solutions",
                Summary = "Scalable platform development, payment gateway integration, cart customization and inventory management.",
                Highlights = new() { "Platform Development", "Payment Gateways", "Cart Customization", "Inventory Management" }
            },
            new ServiceItem
            {
                Slug = "blockchain", Icon = "bi-link-45deg",
                Title = "Blockchain Development",
                Summary = "Smart contracts, DApp creation, cryptocurrency solutions and blockchain integration consulting.",
                Highlights = new() { "Smart Contracts", "DApp Creation", "Cryptocurrency Solutions", "Consulting" }
            },
            new ServiceItem
            {
                Slug = "iot", Icon = "bi-router",
                Title = "Internet of Things (IoT)",
                Summary = "IoT device development, scalable platforms, data analytics and end-to-end security for connected systems.",
                Highlights = new() { "Device Development", "Platform Creation", "Data Analytics", "Security Measures" }
            },
            new ServiceItem
            {
                Slug = "ar-vr", Icon = "bi-badge-vr",
                Title = "AR & VR Services",
                Summary = "Immersive AR/VR application development, 3D modeling & simulation and interactive experiences.",
                Highlights = new() { "App Development", "3D Modeling", "Interactive Experiences", "Training Simulations" }
            },
            new ServiceItem
            {
                Slug = "enterprise-software", Icon = "bi-building-gear",
                Title = "Enterprise Software",
                Summary = "ERP, CRM, Supply Chain Management and HRMS solutions that streamline complex business processes.",
                Highlights = new() { "ERP", "CRM", "SCM", "HRMS" }
            },
            new ServiceItem
            {
                Slug = "marketing-analytics", Icon = "bi-graph-up-arrow",
                Title = "Marketing & Analytics",
                Summary = "SEO, digital marketing tools and customer analytics that turn insight into measurable growth.",
                Highlights = new() { "SEO", "Digital Marketing Tools", "Customer Analytics" }
            },
            new ServiceItem
            {
                Slug = "training-education", Icon = "bi-mortarboard",
                Title = "Training & Education",
                Summary = "Technical training, hands-on workshops and documentation services to upskill your teams.",
                Highlights = new() { "Technical Training", "Workshops", "Documentation Services" }
            },
            new ServiceItem
            {
                Slug = "open-source", Icon = "bi-git",
                Title = "Open Source Contributions",
                Summary = "Collaborative development and customizable, cost-efficient frameworks shared with the community.",
                Highlights = new() { "Collaborative Development", "Customization", "Cost Efficiency" }
            },
            new ServiceItem
            {
                Slug = "outsourcing", Icon = "bi-people",
                Title = "Outsourcing & Staff Augmentation",
                Summary = "Dedicated teams and project-based engagements that flex with your roadmap and headcount needs.",
                Highlights = new() { "Dedicated Teams", "Project-Based Solutions" }
            },
        };

        private static List<IndustryItem> GetIndustries() => new()
        {
            new IndustryItem { Name = "Healthcare", Icon = "bi-heart-pulse", Description = "Secure EHR systems, telehealth apps and workflow tools that improve patient outcomes." },
            new IndustryItem { Name = "Fintech", Icon = "bi-currency-exchange", Description = "Secure transactions, blockchain-backed compliance and seamless online financial services." },
            new IndustryItem { Name = "Edtech", Icon = "bi-book", Description = "Interactive learning platforms and AI-driven analytics that power remote and hybrid education." },
            new IndustryItem { Name = "Retail", Icon = "bi-shop", Description = "AI-optimized inventory, customer experience analytics and custom e-commerce & mobile apps." },
        };

        private static List<ServiceItem> GetWhyChooseUs() => new()
        {
            new ServiceItem { Icon = "bi-mortarboard-fill", Title = "Expert Knowledge", Summary = "A diverse team of industry experts delivering high-quality, tailored solutions." },
            new ServiceItem { Icon = "bi-people-fill", Title = "Client-Centric Approach", Summary = "Collaborative methodology building partnerships that extend beyond project completion." },
            new ServiceItem { Icon = "bi-rocket-takeoff-fill", Title = "Innovative Solutions", Summary = "Embracing the latest technologies to keep your organization competitive." },
            new ServiceItem { Icon = "bi-award-fill", Title = "Commitment to Success", Summary = "We treat your success as our own — continuously improving alongside your needs." },
        };
    }
}
