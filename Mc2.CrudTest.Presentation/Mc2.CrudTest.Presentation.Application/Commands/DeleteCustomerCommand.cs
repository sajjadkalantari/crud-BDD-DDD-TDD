using Mc2.CrudTest.Presentation.Application.Dtos;
using Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate;
using Mc2.CrudTest.Shared.Exceptions;
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
        private readonly ICustomerRepository _customerRepository;

        public DeleteCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> Handle(DeleteCustomerCommand message, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.FindByIdAsync(message.Id);
            if (customer == null) throw new EntityNotFoundException(nameof(Customer));

            _customerRepository.Delete(customer);
            
            await _customerRepository.UnitOfWork.SaveChangesAsync();

            return message.Id;
        }

    }
}
