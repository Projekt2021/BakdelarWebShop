using DataAccess;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Globalization;

namespace Bakdelar_API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            var services = host.Services.CreateScope().ServiceProvider;

            try
            {
                var context = services.GetRequiredService<BakdelarAppDbContext>();

                //context.Products.RemoveRange(context.Products);
                //context.ProductImages.RemoveRange(context.ProductImages);
                //context.SaveChanges();
                SeedData.Seeding(context);
                SeedData.SeedingProducts(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred seeding the DB.");
            }

            CultureInfo ci = new CultureInfo("sv-SE");
            CultureInfo.DefaultThreadCurrentCulture = ci;
            CultureInfo.DefaultThreadCurrentUICulture = ci;

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseUrls("https://localhost:5005/");
                });
    }
}
