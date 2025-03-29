using TicketBookingWebsite.Models;

namespace TicketBookingWebsite.ViewModels
{
    public class CartViewModel
    {
        public List<Event> Events { get; set; }
        public decimal TotalPrice { get; set; }
    }

}
