using DiplomStore.DAL.Interface;
using DiplomStore.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace DiplomStore.DAL.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext context;
        public OrderRepository(AppDbContext _context)
        {
            context = _context;
        }
        public IQueryable<Order> Models => context.orders.Include(i=>i.Lines).ThenInclude(i=>i.tovar).Include(i=>i.User);

        public void Delete(int id)
        {
            var orders = context.orders.Include(p=>p.Lines).ThenInclude(i=>i.tovar).FirstOrDefault(p=>p.OrderID== id);
            context.Remove(orders);
        }

        public void SaveOrder(Order order)
        {
            context.AttachRange(order.Lines.Select(i=>i.tovar));
            if(order.OrderID == 0)
            {
                context.Add(order);
            }
        }
    }
}
