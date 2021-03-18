using Bakdelar.Areas.Identity.Data;
using Fare;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Globalization;

namespace Bakdelar
{
    public class Program
    {
        public static void Main(string[] args)
        {


            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("http://*:5000;https://*:5001");
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddDbContext<AuthenticationDbContext>(options =>
                         options.UseSqlServer(context.Configuration.GetConnectionString("AuthenticationDbContextConnection")),
                         ServiceLifetime.Transient);

                    services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
                       .AddRoles<IdentityRole>()
                       .AddEntityFrameworkStores<AuthenticationDbContext>();

                    services.AddAuthorization(options =>
                    {
                        options.AddPolicy("RequireAdministratorRole",
                             policy => policy.RequireRole("Admin"));
                    });

                });
    }
}
