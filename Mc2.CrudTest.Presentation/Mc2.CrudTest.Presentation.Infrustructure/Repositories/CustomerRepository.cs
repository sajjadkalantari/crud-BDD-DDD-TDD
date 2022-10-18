using Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate;
using Mc2.CrudTest.Presentation.Domain.Infrustructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Infrustructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerContext _context;
        public IUnitOfWork UnitOfWork => _context;

        public CustomerRepository(CustomerContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Customer Add(Customer customer)
        {
            if (customer.IsTransient())
                return _context.Customers.Add(customer).Entity;

            return customer;
        }

        public int Delete(Customer customer)
        {
            if (!customer.IsTransient())
                return _context.Customers.Remove(customer).Entity.Id;
            return customer.Id;
        }

        public Customer Update(Customer customer)
        {
            if (!customer.IsTransient())
                return _context.Customers.Update(customer).Entity;
            return customer;
        }

        public async Task<Customer> FindByIdAsync(int id)
        {
            return await _context.Customers.AsNoTracking().Where(b => b.Id == id).SingleOrDefaultAsync();
        }
    }

}
