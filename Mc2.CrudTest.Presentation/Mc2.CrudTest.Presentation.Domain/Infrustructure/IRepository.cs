namespace Mc2.CrudTest.Presentation.Domain.Infrustructure
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
