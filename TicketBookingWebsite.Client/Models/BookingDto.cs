namespace TicketBookingWebsite.Client.Models
{
    public class BookingDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public string CustomerEmail { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public int EventId { get; set; }
    }
}
