using System.ComponentModel.DataAnnotations;

namespace SharkGSSolutions.Models
{
    /// <summary>
    /// Bound model for the "Let's Innovate Together" contact form.
    /// </summary>
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Please tell us your name.")]
        [StringLength(100)]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "We need an email to get back to you.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email Address")]
        public string Email { get; set; } = string.Empty;

        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        [Display(Name = "Phone Number")]
        public string? Phone { get; set; }

        [StringLength(150)]
        [Display(Name = "Company")]
        public string? Company { get; set; }

        [Required(ErrorMessage = "Please choose the service you're interested in.")]
        [Display(Name = "Service of Interest")]
        public string ServiceInterest { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please write a short message so we know how to help.")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Message should be between 10 and 2000 characters.")]
        [Display(Name = "Your Message")]
        public string Message { get; set; } = string.Empty;
    }
}
