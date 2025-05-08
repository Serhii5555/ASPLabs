using System.ComponentModel.DataAnnotations;

namespace TicketBookingWebsite.ViewModels
{
    public class UserProfileModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
    }

}
