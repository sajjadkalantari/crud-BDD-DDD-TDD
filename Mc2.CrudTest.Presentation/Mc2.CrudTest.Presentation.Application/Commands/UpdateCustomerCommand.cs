using Mc2.CrudTest.Presentation.Application.Dtos;
using Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate;
using Mc2.CrudTest.Shared.Exceptions;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Application.Commands
{
    public class UpdateCustomerCommand : IRequest<CustomerDTO>
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }

        public static Customer ToCustomer(UpdateCustomerCommand customer)
        {
            return new Customer(customer.Id, customer.Firstname, customer.Lastname, customer.DateOfBirth,
                customer.PhoneNumber, customer.Email, customer.BankAccountNumber);
        }

    }

    public class UpdateCustomerCommandHandler: IRequestHandler<UpdateCustomerCommand, CustomerDTO>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<CustomerDTO> Handle(UpdateCustomerCommand message, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.FindByIdAsync(message.Id);
            
            if (customer == null) throw new EntityNotFoundException(nameof(Customer));

            var customerEntity = _customerRepository.Update(UpdateCustomerCommand.ToCustomer(message));

            await _customerRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);

            return CustomerDTO.FromCustomer(customerEntity);
        }

     
    }

}
