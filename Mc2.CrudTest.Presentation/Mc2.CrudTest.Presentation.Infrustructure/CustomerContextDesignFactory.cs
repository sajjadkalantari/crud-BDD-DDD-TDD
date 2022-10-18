using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Infrustructure
{
    public class CustomerContextDesignFactory : IDesignTimeDbContextFactory<CustomerContext>
    {
        public CustomerContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>()
                .UseSqlServer("Server=.;Initial Catalog=CustomerDb;Integrated Security=true");

            return new CustomerContext(optionsBuilder.Options, new NoMediator());
        }

        class NoMediator : IMediator
        {
            public IAsyncEnumerable<TResponse> CreateStream<TResponse>(IStreamRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                return default(IAsyncEnumerable<TResponse>);
            }

            public IAsyncEnumerable<object?> CreateStream(object request, CancellationToken cancellationToken = default)
            {
                return default(IAsyncEnumerable<object?>);
            }

            public Task Publish<TNotification>(TNotification notification, CancellationToken cancellationToken = default) where TNotification : INotification
            {
                return Task.CompletedTask;
            }

            public Task Publish(object notification, CancellationToken cancellationToken = default)
            {
                return Task.CompletedTask;
            }

            public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult<TResponse>(default(TResponse));
            }

            public Task<object> Send(object request, CancellationToken cancellationToken = default)
            {
                return Task.FromResult(default(object));
            }
        }
    }
}
