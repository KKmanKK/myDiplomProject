using DiplomStore.Domain.Entity;

namespace DiplomStore.BLL.Services.Intefaces
{
    public interface IOrderService
    {
        IEnumerable<Order> Orders();
        void SaveOrder(Order order);
        void Delete(int id);
    }
}
