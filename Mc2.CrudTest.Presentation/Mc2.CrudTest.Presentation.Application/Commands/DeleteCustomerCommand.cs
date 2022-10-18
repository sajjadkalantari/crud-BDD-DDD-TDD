using Mc2.CrudTest.Presentation.Application.Dtos;
using Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Application.Commands
{
    public class DeleteCustomerCommand : IRequest<int>
    {
        public DeleteCustomerCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }

    }


    public class DeleteCommandHandler : IRequestHandler<DeleteCustomerCommand, int>
    {

        public DeleteCommandHandler()
        {
        }

        public Task<int> Handle(DeleteCustomerCommand message, CancellationToken cancellationToken)
        {
            //TODO: delete custommer by id
            return Task.FromResult(message.Id);
        }
    }
}
