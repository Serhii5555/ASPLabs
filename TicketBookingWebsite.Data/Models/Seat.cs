using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketBookingWebsite.Models
{
    public class Seat
    {
        public string Id { get; set; }
        public bool IsAvailable { get; set; } = true;
    }

}
