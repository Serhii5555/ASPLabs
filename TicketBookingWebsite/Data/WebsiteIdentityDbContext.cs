using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace TicketBookingWebsite.Data
{
    public class WebsiteIdentityDbContext : IdentityDbContext<IdentityUser>
    {
        public WebsiteIdentityDbContext(DbContextOptions<WebsiteIdentityDbContext> options)
            : base(options) { }
    }
}
