using System.Collections.Generic;
using TicketBookingWebsite.Models;

namespace TicketBookingWebsite.ViewModels
{
    public class EventListViewModel
    {
        public IEnumerable<Event> Events { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
