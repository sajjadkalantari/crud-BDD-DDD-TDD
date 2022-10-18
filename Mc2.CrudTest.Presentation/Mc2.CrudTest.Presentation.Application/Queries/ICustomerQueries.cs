using Mc2.CrudTest.Presentation.Application.Dtos;
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

    }
    public class CustomerQueries : ICustomerQueries
    {
        public Task<List<CustomerDTO>> GetCustomersAsync()
        {
            var result = new List<CustomerDTO>();

            return Task.FromResult(result);
        }
    }
}
