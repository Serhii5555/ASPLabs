using System.ComponentModel.DataAnnotations;

namespace TicketBookingWebsite.ViewModels
{
    public class ForgotPasswordModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
