using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading.Tasks;
using FinanceAccounting.DataAccess.DbContext;
using FinanceAccounting.DataAccess.Initialization;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceAccounting.WebApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();
            await InitializeDatabase(host.Services);
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task InitializeDatabase(IServiceProvider hostServices)
        {
            using IServiceScope scope = hostServices.CreateScope();
            IServiceProvider services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<BookkeepingDbContext>();
                await DbInitializer.Initialize(context);
            }
            catch (Exception ex)
            {
                //TODO: Add logger
            }
        }
    }
}
