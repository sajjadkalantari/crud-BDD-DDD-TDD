using TechTalk.SpecFlow;
using Microsoft.Extensions.Hosting;
using TechTalk.SpecFlow;
using Mc2.CrudTest.Presentation.Server;
using Mc2.CrudTest.Presentation.Infrustructure;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.IntegrationTests.Hooks
{
    [Binding]
    public sealed class Hooks
    {
        private static IHost _host;

        [BeforeTestRun]
        public static void BeforeTestRun()
        {
            _host = Program.CreateHostBuilder(null).Build();
            _host.Start();
        }

        [AfterTestRun]
        public static void AfterTestRun()
        {
            var ctx = (CustomerContext) _host.Services.GetService(typeof(CustomerContext));
            var customers = ctx.Customers.ToListAsync().GetAwaiter().GetResult();
            ctx.RemoveRange(customers);
            ctx.SaveChanges();
            _host.StopAsync().Wait();
        }
    }
}
