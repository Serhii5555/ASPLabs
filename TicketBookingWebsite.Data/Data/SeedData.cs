using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using TicketBookingWebsite.Models;

namespace TicketBookingWebsite.Data
{
    public static class SeedData
    {
        public static async Task EnsurePopulated(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<WebsiteDbContext>();
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }

                await SeedRoles(roleManager);

                if (!context.Events.Any())
                {
                    context.Events.AddRange(
                        new Event
                        {
                            Name = "Rock Concert",
                            Description = "Live rock music event.",
                            Date = new DateTime(2025, 5, 20, 19, 0, 0),
                            Location = "Stadium A",
                            Price = 49.99m,
                            AvailableTickets = 200
                        },
                        new Event
                        {
                            Name = "Tech Conference",
                            Description = "Annual technology conference.",
                            Date = new DateTime(2025, 6, 10, 9, 0, 0),
                            Location = "Convention Center",
                            Price = 99.99m,
                            AvailableTickets = 500
                        },
                        new Event
                        {
                            Name = "Stand-up Comedy Show",
                            Description = "A night of laughter with top comedians.",
                            Date = new DateTime(2025, 4, 15, 20, 0, 0),
                            Location = "Comedy Club",
                            Price = 29.99m,
                            AvailableTickets = 150
                        },
                        new Event
                        {
                            Name = "Art Exhibition",
                            Description = "Showcasing modern art masterpieces.",
                            Date = new DateTime(2025, 7, 5, 10, 0, 0),
                            Location = "City Art Gallery",
                            Price = 15.00m,
                            AvailableTickets = 300
                        }
                    );
                    context.SaveChanges();
                }

                await SeedUsers(userManager);
            }
        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            string[] roleNames = { "Admin", "User" };

            foreach (var roleName in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    await roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private static async Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            var adminUser = await userManager.FindByEmailAsync("admin@ticketbooking.com");

            if (adminUser == null)
            {
                adminUser = new IdentityUser
                {
                    UserName = "admin@ticketbooking.com",
                    Email = "admin@ticketbooking.com"
                };
                var result = await userManager.CreateAsync(adminUser, "AdminPassword123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            var regularUser = await userManager.FindByEmailAsync("user@ticketbooking.com");

            if (regularUser == null)
            {
                regularUser = new IdentityUser
                {
                    UserName = "user@ticketbooking.com",
                    Email = "user@ticketbooking.com"
                };
                var result = await userManager.CreateAsync(regularUser, "UserPassword123!");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(regularUser, "User");
                }
            }
        }
    }
}
