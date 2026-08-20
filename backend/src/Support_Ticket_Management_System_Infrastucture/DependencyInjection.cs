using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Support_Ticket.Application.Common.Interfaces.IRepositories;
using Support_Ticket.Infrastucture.DataContext;
using Support_Ticket.Infrastucture.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Support_Ticket.Infrastucture
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configuration)
        {
            var connectionString =
                configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddIdentity<IdentityUser, IdentityRole<string>>()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();
            services.AddScoped<ICategoryRepository, CategoryRepository>();


            return services;
        }
    }
}
