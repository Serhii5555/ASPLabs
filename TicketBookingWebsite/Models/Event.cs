using System.ComponentModel.DataAnnotations.Schema;

namespace TicketBookingWebsite.Models
{
    public class Event
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Location { get; set; }
        [Column(TypeName = "decimal(8, 2)")]
        public decimal Price { get; set; } 
        public int AvailableTickets { get; set; } 
    }
}
