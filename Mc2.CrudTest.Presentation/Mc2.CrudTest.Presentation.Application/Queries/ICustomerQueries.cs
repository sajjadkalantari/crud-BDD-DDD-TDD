using Dapper;
using Mc2.CrudTest.Presentation.Application.Dtos;
using Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate;
using Mc2.CrudTest.Presentation.Infrustructure.ConfigModels;
using Mc2.CrudTest.Presentation.Infrustructure.Exceptions;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Application.Queries
{
    public interface ICustomerQueries
    {
        Task<List<CustomerDTO>> GetCustomersAsync();
        Task<CustomerDTO> GetCustomersByIdAsync(int id);

    }
    public class CustomerQueries : ICustomerQueries
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly SqlDbConfig _sqlconfigs;

        public CustomerQueries(ICustomerRepository customerRepository, IOptions<SqlDbConfig> sqlconfigs)
        {
            _customerRepository = customerRepository;
            _sqlconfigs = sqlconfigs.Value;
        }

        public async Task<List<CustomerDTO>> GetCustomersAsync()
        {
            using var connection = new SqlConnection(_sqlconfigs.ConnectionString);
            
            connection.Open();

            var cusotmmers = await connection.QueryAsync<Customer>(@"SELECT * FROM customer.customers");

            return cusotmmers.Select(CustomerDTO.FromCustomer).ToList();

        }

        public async Task<CustomerDTO> GetCustomersByIdAsync(int id)
        {
            var customer = await _customerRepository.FindByIdAsync(id);
            if (customer == null) throw new EntityNotFoundException(nameof(Customer));
            return CustomerDTO.FromCustomer(customer);
        }
    }
}
