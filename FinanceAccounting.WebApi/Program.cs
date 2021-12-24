using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System;
using FinanceAccounting.DataAccess.DbContext;
using FinanceAccounting.DataAccess.Initialization;
using Microsoft.Extensions.DependencyInjection;

namespace FinanceAccounting.WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            IHost host = CreateHostBuilder(args).Build();

            using (IServiceScope scope = host.Services.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<BookkeepingDbContext>();
                    DbInitializer.Initialize(context);
                }
                catch (Exception ex)
                {
                    //TODO: Add logger
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
