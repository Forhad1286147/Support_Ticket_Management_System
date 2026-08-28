using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Support_Ticket.Domain.Entities;
using Support_Ticket.Infrastucture.DataContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Infrastucture.SeedData
{
    public static class DbInitializer
    {
        public static async Task InitializeAsync(AppDbContext context, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            // Database migration
            //await context.Database.MigrateAsync();

            // Seed Roles
            await SeedRolesAsync(roleManager);

            // Seed Users
            await SeedUsersAsync(userManager);

            // Seed Categories
            await SeedCategoriesAsync(context);

            // Seed Tickets
            await SeedTicketsAsync(context);

            // Seed Ticket Comments
            await SeedTicketCommentsAsync(context);

            // Seed Notifications
            await SeedNotificationsAsync(context);


        }

        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            string[] roles =
            {
                "Admin",
                "Agent",
                "Customer"
            };

            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(
                        new IdentityRole(role));
                }
            }
        }

        private static async Task SeedUsersAsync(UserManager<IdentityUser> userManager)
        {
            var users = new[]
            {
                new
                {
                    Email = "admin@gmail.com",
                    Password = "Admin@123",
                    Role = "Admin"
                },
                new
                {
                    Email = "agent@gmail.com",
                    Password = "Agent@123",
                    Role = "Agent"
                },
                new
                {
                    Email = "customer@gmail.com",
                    Password = "Customer@123",
                    Role = "Customer"
                }
            };

            foreach (var item in users)
            {
                var user = await userManager.FindByEmailAsync(item.Email);
                if (user == null)
                {
                    user = new IdentityUser
                    {
                        UserName = item.Email,
                        Email = item.Email,
                        EmailConfirmed = true
                    };
                }

                var result = await userManager.CreateAsync(user, item.Password);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, item.Role);
                }
            }


        }

        private static async Task SeedCategoriesAsync(AppDbContext context)
        {
            if (await context.Categories.AnyAsync())
            {
                return;
            }
            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Technical Support",
                    IsActive = true
                },
                new Category
                {
                    Name = "Billing Support",
                    IsActive = true
                },
                new Category
                {
                    Name = "Account Management",
                    IsActive = true
                }
            };
            await context.Categories.AddRangeAsync(categories);

            await context.SaveChangesAsync();

        }

        private static async Task SeedTicketsAsync(AppDbContext context)
        {
            if (await context.Tickets.AnyAsync())
            {
                return;
            }
            var customer = await context.Users.FirstOrDefaultAsync(u => u.Email == "customer@gmail.com");
            if (customer == null)
            {
                return;
            }
            var categories = await context.Categories.ToListAsync();

            var technicalSupport = categories.FirstOrDefault(c => c.Name == "Technical Support");

            var billingSupport = categories.FirstOrDefault(c => c.Name == "Billing Support");

            var accountManagement = categories.FirstOrDefault(c => c.Name == "Account Management");

            if (technicalSupport == null || billingSupport == null || accountManagement == null)
            {
                return;
            }

            var tickets = new List<Ticket>
            {
                new Ticket
                {
                    Title = "Unable to Login",
                    Description =
                        "User cannot login even after entering the correct password.",
                    CategoryId = technicalSupport.Id,
                    CreatedBy = customer.Id,
                    Status = "Open",
                    Priority = "High",
                    CreatedAt = DateTime.UtcNow.ToString()
                },

                 new Ticket
                {
                    Title = "Payment Not Updated",
                    Description =
                  "Payment completed successfully but the payment status is not updated.",
                    CategoryId = billingSupport.Id,
                    CreatedBy = customer.Id,
                    Status = "In Progress",
                    Priority = "Medium",
                    CreatedAt = DateTime.UtcNow.ToString()
                },

                 new Ticket
                {
                    Title = "Password Reset Email Not Received",
                    Description =
                        "User requested a password reset but did not receive the reset email.",
                    CategoryId = accountManagement.Id,
                    CreatedBy = customer.Id,
                    Status = "Resolved",
                    Priority = "Low",
                    CreatedAt = DateTime.UtcNow.ToString()
                }
            };
            await context.Tickets.AddRangeAsync(tickets);

            await context.SaveChangesAsync();

        }

        private static async Task SeedTicketCommentsAsync(AppDbContext context)
        {
            if (await context.TicketComments.AnyAsync())
            {
                return;
            }
            var tickets = await context.Tickets.ToListAsync();
            var agent = await context.Users .FirstOrDefaultAsync(u => u.Email == "agent@gmail.com");
            if (agent == null)
            {
                return;
            }
            var ticket1 = tickets.FirstOrDefault(t => t.Title == "Unable to Login");
            var ticket2 = tickets .FirstOrDefault(t => t.Title == "Payment Not Updated");
            var ticket3 = tickets.FirstOrDefault(t => t.Title == "Password Reset Email Not Received");
            if (ticket1 == null ||ticket2 == null ||ticket3 == null)
            {
                return;
            }
            var comments = new List<TicketComment>
            {
                    new TicketComment
                    {
                        TicketId = ticket1.Id,
                        UserId = agent.Id,
                        Comment =
                            "We are investigating the login issue.",
                        CreatedAt = DateTime.UtcNow
                    },
                     new TicketComment
                    {
                        TicketId = ticket2.Id,
                        UserId = agent.Id,
                        Comment =
                            "We are checking the payment transaction details.",
                        CreatedAt = DateTime.UtcNow
                    },
                      new TicketComment
                    {
                        TicketId = ticket3.Id,
                        UserId = agent.Id,
                        Comment =
                            "The password reset email service has been checked.",
                        CreatedAt = DateTime.UtcNow
                    }


            };
            await context.TicketComments.AddRangeAsync(comments);

            await context.SaveChangesAsync();
        }

        private static async Task SeedNotificationsAsync(AppDbContext context)
        {
            if (await context.Notifications.AnyAsync())
            {
                return;
            }
            var admin = await context.Users.FirstOrDefaultAsync(u => u.Email == "admin@gmail.com");
            var agent = await context.Users .FirstOrDefaultAsync(u => u.Email == "agent@gmail.com");
            var customer = await context.Users .FirstOrDefaultAsync(u => u.Email == "customer@gmail.com");
            if (admin == null ||agent == null || customer == null)
            {
                return;
            }
            var notifications = new List<Notification>
            {
                 new Notification
                {
                    UserId = customer.Id,
                    Message = "Your ticket 'Unable to Login' has been created successfully.",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                },

                  new Notification
                {
                    UserId = agent.Id,
                    Message = "A new ticket has been assigned to you.",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                },

                           new Notification
                {
                    UserId = admin.Id,
                    Message =
                        "The ticket 'Payment Not Updated' has changed status to In Progress.",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                }

            };
             await context.Notifications.AddRangeAsync(notifications);

    await context.SaveChangesAsync();
        }
    }
}
