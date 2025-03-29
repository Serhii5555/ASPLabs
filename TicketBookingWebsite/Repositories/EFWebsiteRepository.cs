using Microsoft.EntityFrameworkCore.Migrations;
using TicketBookingWebsite.Models;
using TicketBookingWebsite.Data;

namespace TicketBookingWebsite.Repositories
{
    public class EFWebsiteRepository : IWebsiteRepository
    {
        private WebsiteDbContext context;
        public EFWebsiteRepository(WebsiteDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Event> Events => context.Events;
    }

}
