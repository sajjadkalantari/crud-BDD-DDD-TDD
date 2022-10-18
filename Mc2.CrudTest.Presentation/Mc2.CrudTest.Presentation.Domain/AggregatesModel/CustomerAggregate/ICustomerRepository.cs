using Mc2.CrudTest.Presentation.Domain.Infrustructure;
using System.Threading.Tasks;

namespace Mc2.CrudTest.Presentation.Domain.AggregatesModel.CustomerAggregate
{
    public interface ICustomerRepository : IRepository<Customer>
    {

    }
}
