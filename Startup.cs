using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Categorise.Areas.Identity;
using Categorise.Data;
using Categorise.Services;
using Npgsql;
using System.Linq;
using System.Threading.Tasks;

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
            CurrentEnvironment = environment;
        }

        /// <summary>
        /// Startup configuration.
        /// </summary>
        public IConfiguration Configuration { get; }
        private IWebHostEnvironment CurrentEnvironment;

        /// <summary>
        /// Called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            if (CurrentEnvironment.IsDevelopment())
            {
                var builder = new NpgsqlConnectionStringBuilder(
                Configuration.GetConnectionString("CategoriseContext"));
                builder.Username = Configuration["DB_USER"];
                builder.Password = Configuration["DB_PASSWORD"];
                _connectionString = builder.ConnectionString;
            }
            else if (CurrentEnvironment.IsProduction())
            {
                _connectionString = ParseConnectionUri(Configuration["DATABASE_URL"]);
            }

            services.AddDbContext<CategoriseContext>(options =>
                options.UseNpgsql(_connectionString));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<CategoriseContext>();
            services.Configure<IdentityOptions>(options =>
                options.ClaimsIdentity.UserIdClaimType = ClaimTypes.NameIdentifier);
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IVendorService, VendorService>();
            services.AddDatabaseDeveloperPageExceptionFilter();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, CategoriseContext context, IWebHostEnvironment env)
        {
            CreateDefaultConfig(context);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            var blockRegistration = context.ConfigSettings.Where(c => c.Name == "AllowRegistrations").FirstOrDefault().Value == "false";
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");

                if (blockRegistration)
                {
                    endpoints.MapGet("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true)));
                    endpoints.MapPost("/Identity/Account/Register", context => Task.Factory.StartNew(() => context.Response.Redirect("/Identity/Account/Login", true)));
                }
            });
        }

        private static string ParseConnectionUri(string connectionUri)
        {
            var splitString = connectionUri.Split(new string[] { "postgres://", ":", "@", "/" }, StringSplitOptions.None);
            var connectionString = $"Host={splitString[3]};Database={splitString[5]};Port={splitString[4]};Username={splitString[1]};Password={splitString[2]};SSL Mode=Require;Trust Server Certificate=true";
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
