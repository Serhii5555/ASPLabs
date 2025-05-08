using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBookingWebsite.Models
{
    public class Booking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Customer name is required.")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        public string CustomerName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string CustomerEmail { get; set; }

        [Required(ErrorMessage = "Please specify ticket quantity.")]
        [Range(1, 100, ErrorMessage = "You must book at least 1 ticket.")]
        public int Quantity { get; set; }

        public DateTime BookingDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "Event selection is required.")]
        public int EventId { get; set; }

        public Event? Event { get; set; }
    }
}
