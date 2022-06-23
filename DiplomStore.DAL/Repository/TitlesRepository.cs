using DiplomStore.DAL.Interface;
using DiplomStore.Domain.Entity;

namespace DiplomStore.DAL.Repository
{
    public class TitlesRepository : IRepositoryBase<Titles>
    {
        private readonly AppDbContext context;
        public TitlesRepository(AppDbContext ctx)
        {
            context = ctx;
        }
        public IQueryable<Titles> GetAll => context.titles;

        public Titles Create(Titles entity)
        {
            context.titles.Add(entity);
            return entity;

        }

        public void Delete(int Id)
        {
            var titles = context.titles.FirstOrDefault(t => t.TitlesId == Id);

            if (Id != 0)
            {
                context.titles.Remove(titles);
            }
        }

        public Titles Edit(Titles entity)
        {
            var titles = context.titles.FirstOrDefault(c => c.TitlesId == entity.TitlesId);
            if (titles != null)
            {
                titles.NameImg = entity.NameImg;
                titles.name = entity.name;
                titles.isPopular= entity.isPopular;
                titles.tovars = entity.tovars;
            }
            return entity;
        }
    }
}
