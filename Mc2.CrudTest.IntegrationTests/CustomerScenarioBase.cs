using Mc2.CrudTest.Presentation.Infrustructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace Mc2.CrudTest.FunctionalTests
{
    public class CustomerScenarioBase
    {
        public TestServer CreateServer()
        {
            var path = Assembly.GetAssembly(typeof(CustomerScenarioBase)).Location;

            var hostBuilder = new WebHostBuilder()
                .UseContentRoot(Path.GetDirectoryName(path))
                 .ConfigureAppConfiguration((hostingContext, config) =>
                 {
                     config.AddJsonFile("appsettings.json");
                     config.AddJsonFile("testappsettings.json", optional: true)
                           .AddEnvironmentVariables();

                 })
                 //.UseDefaultServiceProvider(options =>
                 //    options.ValidateScopes = false)
                 .UseStartup<CustomerTestsStartup>();

            var testServer = new TestServer(hostBuilder);

            testServer.Host.MigrateDbContext<CustomerContext>((context, services) =>
                {
                    context.Database.EnsureCreated();
                    context.Database.ExecuteSqlRaw(@"
                                            USE master;
                                            ALTER DATABASE [CustomerDb] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                                            DROP DATABASE [CustomerDb] ;
                                            ");

                    //context.Database.EnsureCreated();
                    context.Database.Migrate();
                });

            return testServer;
        }
       
    }

    public static class WebHostExtension
    {
        public static IWebHost MigrateDbContext<TContext>(this IWebHost webHost, Action<TContext, IServiceProvider> seeder) where TContext : DbContext
        {

            using var scope = webHost.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetService<TContext>();

            InvokeSeeder(seeder, context, services);

            return webHost;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services)
           where TContext : DbContext
        {            
            seeder(context, services);
        }
    }
}
