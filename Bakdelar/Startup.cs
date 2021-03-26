using Bakdelar.Areas.Identity.Data;
using Bakdelar.MethodClasses;
using Bakdelar.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace Bakdelar
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            GetFromApi.SetupLinks(Configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            services.AddHttpClient();
            services.AddRazorPages().AddRazorRuntimeCompilation();
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(20);
            });


            services.AddDbContext<OrderDbContext>
            (options => options.UseSqlServer(Configuration.GetConnectionString("AuthenticationDbContextConnection")));



            services.AddDbContext<AuthenticationDbContext>(options =>
                 options.UseSqlServer(Configuration.GetConnectionString("AuthenticationDbContextConnection")),
                 ServiceLifetime.Transient);

            services.AddDefaultIdentity<MyUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
               .AddEntityFrameworkStores<AuthenticationDbContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministratorRole",
                     policy => policy.RequireRole("Admin"));
            });
            //    .AddNewtonsoftJson();

            //services.AddHttpClient<IContactsClient,
            //               ContactsClient>(client =>
            // client.BaseAddress = new Uri(Configuration.GetSection("ContactsApi").Value));

            //    .AddRazorPagesOptions(options =>
            //{
            //    options.Conventions.AuthorizeFolder("/Admin");
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

               // app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseSession();
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
