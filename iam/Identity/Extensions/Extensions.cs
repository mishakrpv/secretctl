using System.Text.Json.Serialization;
using EventBus.Extensions;
using EventBus.RabbitMQ;
using Identity.API.Data;
using Identity.API.Events;
using Identity.API.Models;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Extensions;

public static class Extensions
{
    public static void AddServices(this WebApplicationBuilder builder)
    {
        services.AddDbContext<IdentityContext>(options =>
        {
            var connectionString = builder.Configuration.GetConnectionString("IdentityDb");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 39));
            options.UseMySql(connectionString, serverVersion);
        });
        
        builder.Services.AddDefaultIdentity<ApplicationUser>()
            .AddEntityFrameworkStores<IdentityContext>();
        builder.Services.AddRazorPages();

        builder.Services.Configure<IdentityOptions>(options =>
        {
            // Password settings
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;

            // Lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings
            options.User.RequireUniqueEmail = true;
        });
        
        builder.AddIdentityServer(builder.Configuration);
        
        builder.AddAuthentication();
    }
    
    public static void AddHealthChecks(this WebApplicationBuilder builder)
    {
        builder.Services.AddHealthChecks()
            .AddNpgSql(builder.Configuration.GetConnectionString(
                           Constants.POSTGRES_CONNECTION_NAME) ??
                       throw new InvalidOperationException(
                           $"ConnectionStrings missing value for {Constants.POSTGRES_CONNECTION_NAME}"),
                name: "PostgresCheck");
    }
}
