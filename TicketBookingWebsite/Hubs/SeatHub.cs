using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace TicketBookingWebsite.Hubs
{
    public class SeatHub : Hub
    {
        public async Task NotifySeatUpdate(string seatId, bool isAvailable)
        {
            await Clients.All.SendAsync("ReceiveSeatUpdate", seatId, isAvailable);
        }
    }
}
