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
    public class CreateCustomerCommand : IRequest<CustomerDTO>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }

        public static Customer ToCustomer(CreateCustomerCommand customer)
        {
            return new Customer(customer.Firstname, customer.Lastname, customer.DateOfBirth,
                customer.PhoneNumber, customer.Email, customer.BankAccountNumber);
        }

    }


    public class CreateCustomerCommandHandler
    : IRequestHandler<CreateCustomerCommand, CustomerDTO>
    {
        private readonly ICustomerRepository _customerRepository;

        public CreateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDTO> Handle(CreateCustomerCommand message, CancellationToken cancellationToken)
        {
            var customer = CreateCustomerCommand.ToCustomer(message);
            
            var customerEntity = _customerRepository.Add(customer);

            await _customerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return CustomerDTO.FromCustomer(customerEntity);
        }
    }
}
