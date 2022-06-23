using DiplomStore.BLL.Services.Intefaces;
using DiplomStore.DAL.Interface;
using DiplomStore.Domain.Entity;

namespace DiplomStore.BLL.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork db;
        public OrderService(IUnitOfWork _unitOfWork)
        {
            db = _unitOfWork;
        }

        public void Delete(int id)
        {
            db.Orders.Delete(id);
            db.Save();
        }

        public IEnumerable<Order> Orders()
        {            
            return db.Orders.Models;
            
        }

        public void SaveOrder(Order order)
        {
            db.Orders.SaveOrder(order);
            db.Save();
        }
    }
}
