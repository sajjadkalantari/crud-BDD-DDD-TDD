using Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Application.Dtos
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string BankAccountNumber { get; set; }

        public static CustomerDTO FromCustomer(Customer customer)
        {
            return new CustomerDTO()
            {
                Id = customer.Id,
                Firstname = customer.Firstname,
                Lastname = customer.Lastname,
                DateOfBirth = customer.DateOfBirth,
                PhoneNumber = customer.PhoneNumber.ToString(),
                Email = customer.Email.ToString(),
                BankAccountNumber = customer.BankAccountNumber.ToString(),
            };
        }
    }
}
