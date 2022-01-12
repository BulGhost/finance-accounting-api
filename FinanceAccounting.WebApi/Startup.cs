using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;
using FinanceAccounting.Application;
using FinanceAccounting.Application.Common.Mappings;
using FinanceAccounting.DataAccess;
using FinanceAccounting.WebApi.Middleware;
using FinanceAccounting.WebApi.ViewModels.HelperClasses;

namespace FinanceAccounting.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication();
            string connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDataAccess(connection);
            services.AddTransient<ExceptionHandlingMiddleware>();
            services.AddCors();
            services.AddControllers().AddJsonOptions(opt =>
                opt.JsonSerializerOptions.Converters.Add(new DateTimeConverter()));
            services.AddCustomSwagger();
            services.AddCustomIdentity();
            services.AddCustomAuthentication(Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FinanceAccounting.WebApi v1"));
            }

            app.UseMiddleware<ExceptionHandlingMiddleware>();
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}