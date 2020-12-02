using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Categorise.Data;
using Categorise.Extensions;

namespace Categorise
{
    /// <summary>
    /// Program class.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Program main method.
        /// </summary>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args)
                .Build()
                .MigrateDatabase<CategoriseContext>()
                .Run();
        }

        /// <summary>
        /// Creates a HostBuilder for resource encapsulation.
        /// </summary>
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
