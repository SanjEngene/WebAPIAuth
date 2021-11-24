using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebAPIAuth.Models;
using WebAPIAuth.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WebAPIAuth.Services.AuthServices;
using AutoMapper;

namespace WebAPIAuth
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public IConfiguration Configuration { get; set; }
        public Startup()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
            Configuration = builder.Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = Configuration["ConnectionStrings:DefaultConnection"];
            services.AddDbContext<ApplicationContext>(opt => opt.UseSqlServer(connectionString));
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers(mvcOptions =>
            {
                mvcOptions.EnableEndpointRouting = true;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(configureOptions =>
                {
                    configureOptions.RequireHttpsMetadata = false;
                    configureOptions.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = AuthOptions.AUDIENCE,

                        ValidateIssuer = true,
                        ValidIssuer = AuthOptions.ISSUER,

                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = AuthOptions.GetSymmetricalKey(),

                        ValidateLifetime = true
                    };
                });

            services.AddTransient<UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

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
