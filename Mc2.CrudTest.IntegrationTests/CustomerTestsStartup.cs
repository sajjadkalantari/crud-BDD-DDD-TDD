using Mc2.CrudTest.Presentation.Server;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Mc2.CrudTest.FunctionalTests
{
    public class CustomerTestsStartup : Startup
    {
        public CustomerTestsStartup(IConfiguration env) : base(env)
        {
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            services.Configure<RouteOptions>(Configuration);
            base.ConfigureServices(services);
        }
     
    }
}
