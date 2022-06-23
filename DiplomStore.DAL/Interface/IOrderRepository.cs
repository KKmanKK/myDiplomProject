
using DiplomStore.Domain.Entity;

namespace DiplomStore.DAL.Interface
{
    public interface IOrderRepository
    {
        IQueryable<Order> Models { get; }
        void SaveOrder(Order order);
        void Delete(int id);
    }
}
