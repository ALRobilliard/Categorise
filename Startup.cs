using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Npgsql;
using CategoriseApi.Helpers;
using CategoriseApi.Models;
using CategoriseApi.Services;

namespace CategoriseApi
{
    public class Startup
    {
        private static string _connectionString;
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _currentEnvironment = environment;
        }

        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _currentEnvironment;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            if (_currentEnvironment.IsDevelopment())
            {
                var builder = new NpgsqlConnectionStringBuilder(
                Configuration.GetConnectionString("CategoriseContext"));
                builder.Username = Configuration["DB_USER"];
                builder.Password = Configuration["DB_PASSWORD"];
                _connectionString = builder.ConnectionString;
            }
            else if (_currentEnvironment.IsProduction())
            {
                _connectionString = ParseConnectionUri(Configuration["DATABASE_URL"]);
            }

            services.AddDbContext<CategoriseContext>(options =>
                options.UseNpgsql(_connectionString));
            services.AddAutoMapper(typeof(Startup));

            // Configure JWT authentication.
            var key = Encoding.ASCII.GetBytes(Configuration["APP_SECRET"]);
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => {
                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        var email = context.Principal.Identity.Name;
                        var user = userService.GetUserByEmail(email);
                        if (user == null)
                        {
                            // Return unauthorized if user no longer exists.
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters 
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
            services.AddScoped<IUserService, UserService>();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Categorise API V1");
            });
        }

        private static string ParseConnectionUri(string connectionUri)
        {
            var splitString = connectionUri.Split(new string[] {"postgres://", ":", "@", "/" }, StringSplitOptions.None);
            var connectionString = $"Host={splitString[3]};Database={splitString[5]};Port={splitString[4]};Username={splitString[1]};Password={splitString[2]}";
            return connectionString;
        }
    }
}