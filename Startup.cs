using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using AutoMapper;
using Npgsql;
using Categorise.Models;
using Categorise.Services;

namespace Categorise
{
    /// <summary>
    /// Startup class.
    /// </summary>
    public class Startup
    {
        private static string _connectionString;

        /// <summary>
        /// Startup constructor.
        /// </summary>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            _currentEnvironment = environment;
        }

        /// <summary>
        /// Startup configuration.
        /// </summary>
        public IConfiguration Configuration { get; }
        private readonly IWebHostEnvironment _currentEnvironment;

        /// <summary>
        /// Called by the runtime. Use this method to add services to the container.
        /// </summary>
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
            }).AddJwtBearer(options =>
            {
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
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });
        }

        /// <summary>
        /// Called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, CategoriseContext context, IWebHostEnvironment env)
        {
            CreateDefaultConfig(context);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Categorise API V1");
            });
        }

        private static string ParseConnectionUri(string connectionUri)
        {
            var splitString = connectionUri.Split(new string[] { "postgres://", ":", "@", "/" }, StringSplitOptions.None);
            var connectionString = $"Host={splitString[3]};Database={splitString[5]};Port={splitString[4]};Username={splitString[1]};Password={splitString[2]}";
            return connectionString;
        }

        private void CreateDefaultConfig(CategoriseContext context)
        {
            ConfigSettingService configSettingService = new ConfigSettingService(context);

            // Create required configs (safely).
            configSettingService.CreateConfigSetting("AllowRegistrations", "false", true);
        }
    }
}