using DataAccess;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace Bakdelar_API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<BakdelarAppDbContext>
            (options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("BakdelarDbConnection"));
            });

            //var tokenValidationParameters = new TokenValidationParameters
            //{
            //    ValidAudience = "https://localhost",
            //    ValidateAudience = true,
            //    ValidateIssuerSigningKey = true,
            //    ValidateIssuer = true,
            //    ValidIssuer = "https://localhost",
            //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("StrONGKAutHENTICATIONKEy")),
            //    ClockSkew = TimeSpan.Zero,
            //    ValidateLifetime = false
            //};            

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //.AddJwtBearer(options =>
            //  {                  
            //      options.TokenValidationParameters = tokenValidationParameters;
            //      options.Events = new JwtBearerEvents
            //      {
            //          OnAuthenticationFailed = context =>
            //          {
            //              if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            //              {
            //                  context.Response.Headers.Add("Token-Expired", "true");
            //              }
            //              return Task.CompletedTask;
            //          }
            //      };
            //  });

            //services.AddAuthorization(config =>
            //{
            //    config.AddPolicy("AdminRole", options =>
            //    {
            //        options.RequireAuthenticatedUser();
            //        options.AuthenticationSchemes.Add(
            //                JwtBearerDefaults.AuthenticationScheme);
            //        options.Requirements.Add(new AdminAuthorizationHandler());
            //    });
            //});

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Bakdelar_API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Bakdelar_API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
