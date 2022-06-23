using DiplomStore.Domain.Entity;

namespace DiplomStore.DAL.Interface
{
    public interface IUnitOfWork:IDisposable
    {
        IRepositoryBase<Titles> Titles { get; }
        IRepositoryBase<Categories> Categories { get; }
        IRepositoryBase<Tovars> Tovars { get; }
        IOrderRepository Orders { get; }
        void Save();
    }
}
