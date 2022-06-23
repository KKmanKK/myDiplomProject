using DiplomStore.DAL.Interface;
using DiplomStore.Domain.Entity;

namespace DiplomStore.DAL.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;
        public TovarsRepository tovar;
        public TitlesRepository title;
        public CategoriesRepository category;
        public OrderRepository order;
        public UnitOfWork(AppDbContext ctx)
        {
            context = ctx;
        }
        public IRepositoryBase<Tovars> Tovars
        {
            get
            {
                if (tovar == null)
                {
                    tovar = new TovarsRepository(context);
                }
                return tovar;
            }
        }

        public IRepositoryBase<Titles> Titles
        {
            get
            {
                if(title == null)
                {
                    title = new TitlesRepository(context);
                }
                return title;
            }
        }

        public IRepositoryBase<Categories> Categories
        {
            get
            {
                if(category == null)
                {
                    category= new CategoriesRepository(context);
                }
                return category;
            }
        }

        public IOrderRepository Orders
        {
            get
            {
                if(order == null)
                {
                    order = new OrderRepository(context);
                }
                return order;
            }
        }

        private bool desposed = false;

        public void Dispose(bool disposing)
        {
            if (!this.desposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.desposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Save()
        {
            context.SaveChanges();
        }
    }
}
