using Mc2.CrudTest.Presentation.Domain.Infrustructure;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate
{
    public interface ICustomerRepository : IRepository<Customer>
    {
        Customer Add(Customer customer);
        int Delete(Customer customer);
        Task<Customer> FindByIdAsync(int id);
        Customer Update(Customer customer);
    }
}
